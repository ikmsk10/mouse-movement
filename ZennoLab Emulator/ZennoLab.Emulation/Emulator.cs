// Decompiled with JetBrains decompiler
// Type: ZennoLab.Emulation.Emulator
// Assembly: ZennoLab.Emulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3402272C-5FE8-4A11-863F-404E8B64C3F1
// Assembly location: C:\Program Files (x86)\ZennoLab\ZennoPoster Demo\Progs\ZennoLab.Emulation.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ZennoLab.Emulation
{
  /// <summary>Represents methods for emulation of work with windows.</summary>
  /// <remarks>In this class represented the methods which can work with windows using the system handle or header (title) of the window.</remarks>
  /// <seealso cref="T:ZennoLab.Emulation.KeyboardEvent">KeyboardEvent Enumeration</seealso>
  /// <seealso cref="T:ZennoLab.Emulation.MouseButton">MouseButton Enumeration</seealso>
  /// <seealso cref="T:ZennoLab.Emulation.MouseButtonEvent">MouseButtonEvent Enumeration</seealso>
  /// <example>
  /// The following code example demonstrates uses of the some methods of <see cref="T:ZennoLab.Emulation.Emulator">Emulator</see> class.
  /// <code title="Example" description="" lang="C#">
  /// // set active window
  /// string result = Emulator.ActiveWindow("Simple window");
  /// // check result
  /// if (result == "ok" &amp;&amp; !Emulator.ErrorDetected)
  /// {
  ///     // send key "z"
  ///     result = Emulator.SendKey("Simple window", 100, 200, System.Windows.Forms.Keys.Z, KeyboardEvent.Press);
  ///     // check result
  ///     if (result == "ok" &amp;&amp; !Emulator.ErrorDetected)
  ///     {
  ///         // send text
  ///         result = Emulator.SendText("Simple window",  100, 200, "Simple text", false);
  ///         // check result
  ///         if (result == "ok" &amp;&amp; !Emulator.ErrorDetected)
  ///         {
  ///             // mouse move
  ///             Emulator.MouseMove("Simple window",350, 370);
  ///             // mouse click on button as result it will show save file dialog
  ///             result = Emulator.MouseClick("Simple window", MouseButton.Left, MouseButtonEvent.Click, 350, 370);
  ///             // check result
  ///             if (result == "ok" &amp;&amp; !Emulator.ErrorDetected)
  ///             {
  ///                 // click on button with text "Save"
  ///                 return Emulator.ButtonClick("Save as", "Save");
  ///             }
  ///             else return "Fail";
  ///         }
  ///         else return "Fail";
  ///     }
  ///     else return "Fail";
  /// }
  /// else return "Fail";
  /// // close window
  /// result = Emulator.CloseWindow("Simple window");
  /// // check result
  /// if (result != "ok" || Emulator.ErrorDetected) return "Fail";
  /// return "All done";</code><code title="Example2" description="" lang="PHP">
  /// // set active window
  /// $result = Emulator:ActiveWindow("Simple window");
  /// // check result
  /// if ($result == "ok" &amp;&amp; !Emulator::ErrorDetected)
  /// {
  ///     // send key "z"
  ///     $result = Emulator::SendKey("Simple window", 100, 200, System\Windows\Forms\Keys::Z, KeyboardEvent::Press);
  ///     // check result
  ///     if ($result == "ok" &amp;&amp; !Emulator::ErrorDetected)
  ///     {
  ///         // send text
  ///         $result = Emulator::SendText("Simple window",  100, 200, "Simple text", false);
  ///         // check result
  ///         if (result == "ok" &amp;&amp; !Emulator.ErrorDetected)
  ///         {
  ///             // mouse move
  ///             Emulator::MouseMove("Simple window", 350, 3700);
  ///             // mouse click on button as result it will show save file dialog
  ///             $result = Emulator::MouseClick("Simple window", MouseButton::Left, MouseButtonEvent::Click, 350, 370);
  ///             // check result
  ///             if ($result == "ok" &amp;&amp; !Emulator::ErrorDetected)
  ///             {
  ///                 // click on button with text "Save"
  ///                 return Emulator::ButtonClick("Save as", "Save");
  ///             }
  ///             else return "Fail";
  ///         }
  ///         else return "Fail";
  ///     }
  ///     else return "Fail";
  /// }
  /// else return "Fail";
  /// // close window
  /// $result = Emulator::CloseWindow("Simple window");
  /// // check result
  /// if ($result != "ok" || Emulator::ErrorDetected) return "Fail";
  /// return "All done";</code></example>
  /// <requirements>
  /// 	<para>
  /// 		Target Platforms:
  /// 		<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.</para>
  /// </requirements>
  public class Emulator
  {
    private static readonly object Locker = new object();
    private const int WM_KEYDOWN = 256;
    private const int WM_KEYUP = 257;
    private const int WM_CHAR = 258;
    private const int WM_SYSCOMMAND = 274;
    private const int WM_SETFOCUS = 7;
    private const int SC_CLOSE = 61536;
    private const int SW_MAXIMIZE = 3;
    private const int SW_MINIMIZE = 6;
    private const int WM_MOUSEMOVE = 512;
    private const int WM_MOUSEHOVER = 673;
    private const int WM_LBUTTONDOWN = 513;
    private const int WM_LBUTTONUP = 514;
    private const int WM_RBUTTONDOWN = 516;
    private const int WM_RBUTTONUP = 517;
    private const int GW_HWNDNEXT = 2;
    private const int GW_CHILD = 5;

    /// <summary>
    /// 
    /// Gets information about the error detected in the performance last command.
    /// </summary>
    /// <value>
    /// 	<para>
    /// 
    /// Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.boolean.aspx:">System.Boolean</see>
    /// 	</para>
    /// 	<para>
    /// 
    /// true if error detected in the performance last command; otherwise, false.
    /// 	</para>
    /// </value>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.ActiveWindow(System.String,System.Boolean)">ActiveWindow Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.ButtonClick(System.String,System.String,System.Boolean)">ButtonClick Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.CloseWindow(System.String)">CloseWindow Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseClick(System.String,ZennoLab.Emulation.MouseButton,ZennoLab.Emulation.MouseButtonEvent,System.Int32,System.Int32,System.Boolean)">MouseClick Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseMove(System.String,System.Int32,System.Int32,System.Boolean)">MouseMove Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.SendKey(System.String,System.Int32,System.Int32,System.Windows.Forms.Keys,ZennoLab.Emulation.KeyboardEvent,System.Boolean)">SendKey Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.SendText(System.String,System.Int32,System.Int32,System.String,System.Boolean)">SendText Method</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="P:ZennoLab.Emulation.Emulator.ErrorDetected">ErrorDetected</see> property.
    /// <code title="Example" description="" lang="C#">
    /// // send the text
    /// string result = Emulator.SendText("Window", 200, 200, "it's a simple text for send");
    /// // if error detected
    /// if (Emulator.ErrorDetected) return "Fail";
    /// return "Text was sent";</code><code title="Example2" description="" lang="PHP">
    /// // send the text
    /// $result = ZennoLab\Emulation\Emulator::SendText("Window", 200, 200, "it's a simple text for send");
    /// // if error detected
    /// if (ZennoLab\Emulation\Emulator::ErrorDetected) return "Fail";
    /// return "Text was sent";</code></example>
    /// <requirements>
    /// 	<para>
    /// 
    /// 			Target Platforms:
    /// 			<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.
    /// 	</para>
    /// </requirements>
    public static bool ErrorDetected { get; private set; }

    /// <summary>Установить позицию курсора</summary>
    /// <param name="x">Координат X</param>
    /// <param name="y">Координата Y</param>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int x, int y);

    /// <summary>Установить фокус на указаном окне</summary>
    /// <param name="hwnd">Handle окна</param>
    [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
    internal static extern IntPtr SetFocus(IntPtr hwnd);

    /// <summary>Найти окно по указанным параметрам</summary>
    /// <param name="lpClassName">Имя класса окна</param>
    /// <param name="lpWindowName">Имя окна (заголовок)</param>
    /// <returns>Handle окна</returns>
    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    /// <summary>Установить активноее окно</summary>
    /// <param name="hWnd">Handle окна</param>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SetActiveWindow(IntPtr hWnd);

    /// <summary>Показать окно</summary>
    /// <param name="hWnd">Handle окна</param>
    /// <param name="nCmdShow">Способ отображения</param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

    /// <summary>Переводит окно на передний план</summary>
    /// <param name="hWnd">Handle окна</param>
    [DllImport("user32.dll")]
    private static extern int SetForegroundWindow(IntPtr hWnd);

    /// <summary>Получает окно по координатам</summary>
    /// <param name="p">Координаты</param>
    /// <returns>Handle окна</returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr WindowFromPoint(Point p);

    /// <summary>Получает размер указанного окна</summary>
    /// <param name="hWnd">Handle окна</param>
    /// <param name="lpRect">Прямоугольник окна</param>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hWnd, ref Emulator.RECT lpRect);

    /// <summary>Получает текст окна</summary>
    /// <param name="hwnd">Handle окна</param>
    /// <param name="s">Куда записать</param>
    /// <param name="nMaxCount">Максимальное количество</param>
    [DllImport("User32.dll")]
    private static extern void GetWindowText(IntPtr hwnd, StringBuilder s, int nMaxCount);

    /// <summary>Установить полодение и размер окна</summary>
    /// <param name="hWnd">Handle окна</param>
    /// <param name="hWndInsertAfter">:(</param>
    /// <param name="X">Координата X</param>
    /// <param name="Y">Координата Y</param>
    /// <param name="W">Длина</param>
    /// <param name="H">Ширина</param>
    /// <param name="uFlags">Флаги</param>
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int W, int H, uint uFlags);

    /// <summary>Получить имя класса окна</summary>
    /// <param name="hWnd">Handle окна</param>
    /// <param name="s">Куда записать</param>
    /// <param name="nMaxCount">Максимальное количество</param>
    /// <returns></returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetClassName(IntPtr hWnd, StringBuilder s, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr PostMessage(IntPtr hWnd, IntPtr Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, IntPtr Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern int GetWindow(IntPtr hwnd, IntPtr wCmd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool IsIconic(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int MapVirtualKey(IntPtr uCode, int uMapType);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    private static IntPtr FindWindowInternal(string className, string name, bool topMost)
    {
      IntPtr window = Emulator.FindWindow(className, name);
      if ((int) window != 0)
      {
        Emulator.ShowWindow(window, Emulator.IsIconic(window) ? 9 : 5);
        Thread.Sleep(1000);
        Emulator.SetForegroundWindow(window);
        if (topMost)
          Emulator.SetWindowPos(window, (IntPtr) -1, 0, 0, 0, 0, 3U);
        else
          Emulator.SetWindowPos(window, (IntPtr) 0, 0, 0, 0, 0, 3U);
      }
      return window;
    }

    private static void SendChar(IntPtr handle, char c)
    {
      Emulator.ErrorDetected = false;
      try
      {
        Emulator.SendMessage(handle, (IntPtr) 256, (IntPtr) 65, IntPtr.Zero);
        Emulator.SendMessage(handle, (IntPtr) 258, (IntPtr) ((int) c), IntPtr.Zero);
        Emulator.SendMessage(handle, (IntPtr) 257, (IntPtr) 65, IntPtr.Zero);
      }
      catch (Exception ex)
      {
        Emulator.ErrorDetected = true;
      }
    }

    private static IntPtr GetKeyCode(Keys key)
    {
      switch (key)
      {
        case Keys.Shift:
          return (IntPtr) 16;
        case Keys.Control:
          return (IntPtr) 17;
        case Keys.Alt:
          return (IntPtr) 18;
        case Keys.Back:
          return (IntPtr) 8;
        case Keys.Tab:
          return (IntPtr) 9;
        case Keys.Return:
          return (IntPtr) 13;
        case Keys.Capital:
          return (IntPtr) 20;
        case Keys.Escape:
          return (IntPtr) 27;
        case Keys.Space:
          return (IntPtr) 32;
        case Keys.Prior:
          return (IntPtr) 33;
        case Keys.Next:
          return (IntPtr) 34;
        case Keys.End:
          return (IntPtr) 35;
        case Keys.Home:
          return (IntPtr) 36;
        case Keys.Left:
          return (IntPtr) 37;
        case Keys.Up:
          return (IntPtr) 38;
        case Keys.Right:
          return (IntPtr) 39;
        case Keys.Down:
          return (IntPtr) 40;
        case Keys.Insert:
          return (IntPtr) 45;
        case Keys.Delete:
          return (IntPtr) 46;
        case Keys.D0:
          return (IntPtr) 48;
        case Keys.D1:
          return (IntPtr) 49;
        case Keys.D2:
          return (IntPtr) 50;
        case Keys.D3:
          return (IntPtr) 51;
        case Keys.D4:
          return (IntPtr) 52;
        case Keys.D5:
          return (IntPtr) 53;
        case Keys.D6:
          return (IntPtr) 54;
        case Keys.D7:
          return (IntPtr) 55;
        case Keys.D8:
          return (IntPtr) 56;
        case Keys.D9:
          return (IntPtr) 57;
        case Keys.A:
          return (IntPtr) 65;
        case Keys.B:
          return (IntPtr) 66;
        case Keys.C:
          return (IntPtr) 67;
        case Keys.D:
          return (IntPtr) 68;
        case Keys.E:
          return (IntPtr) 69;
        case Keys.F:
          return (IntPtr) 70;
        case Keys.G:
          return (IntPtr) 71;
        case Keys.H:
          return (IntPtr) 72;
        case Keys.I:
          return (IntPtr) 73;
        case Keys.J:
          return (IntPtr) 74;
        case Keys.K:
          return (IntPtr) 75;
        case Keys.L:
          return (IntPtr) 76;
        case Keys.M:
          return (IntPtr) 77;
        case Keys.N:
          return (IntPtr) 78;
        case Keys.O:
          return (IntPtr) 79;
        case Keys.P:
          return (IntPtr) 80;
        case Keys.Q:
          return (IntPtr) 81;
        case Keys.R:
          return (IntPtr) 82;
        case Keys.S:
          return (IntPtr) 83;
        case Keys.T:
          return (IntPtr) 84;
        case Keys.U:
          return (IntPtr) 85;
        case Keys.V:
          return (IntPtr) 86;
        case Keys.W:
          return (IntPtr) 87;
        case Keys.X:
          return (IntPtr) 88;
        case Keys.Y:
          return (IntPtr) 89;
        case Keys.Z:
          return (IntPtr) 90;
        case Keys.NumPad0:
          return (IntPtr) 96;
        case Keys.NumPad1:
          return (IntPtr) 97;
        case Keys.NumPad2:
          return (IntPtr) 98;
        case Keys.NumPad3:
          return (IntPtr) 99;
        case Keys.NumPad4:
          return (IntPtr) 100;
        case Keys.NumPad5:
          return (IntPtr) 101;
        case Keys.NumPad6:
          return (IntPtr) 102;
        case Keys.NumPad7:
          return (IntPtr) 103;
        case Keys.NumPad8:
          return (IntPtr) 104;
        case Keys.NumPad9:
          return (IntPtr) 105;
        case Keys.Add:
          return (IntPtr) 187;
        case Keys.Subtract:
          return (IntPtr) 189;
        case Keys.F1:
          return (IntPtr) 112;
        case Keys.F2:
          return (IntPtr) 113;
        case Keys.F3:
          return (IntPtr) 114;
        case Keys.F4:
          return (IntPtr) 115;
        case Keys.F5:
          return (IntPtr) 116;
        case Keys.F6:
          return (IntPtr) 117;
        case Keys.F7:
          return (IntPtr) 118;
        case Keys.F8:
          return (IntPtr) 119;
        case Keys.F9:
          return (IntPtr) 120;
        case Keys.F10:
          return (IntPtr) 121;
        case Keys.LControlKey:
          return (IntPtr) 162;
        case Keys.RControlKey:
          return (IntPtr) 163;
        default:
          return (IntPtr) 0;
      }
    }

    /// <summary>Check for window exists.</summary>
    /// <param name="name">
    /// 	<para>Type: <see>System.String
    /// 	              </see></para>
    /// 	<para>The header of the window.</para>
    /// </param>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.boolean.aspx">System.Boolean</see></para>
    /// 	<para>The answer with information about the window existence. If current window exists, this answer is
    /// "<em>true</em>"; otherwise false.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.ButtonClick(System.String,System.String,System.Boolean)">ButtonClick Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseClick(System.String,ZennoLab.Emulation.MouseButton,ZennoLab.Emulation.MouseButtonEvent,System.Int32,System.Int32,System.Boolean)">MouseClick Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseMove(System.String,System.Int32,System.Int32,System.Boolean)">MouseMove Method</seealso>
    /// <seealso cref="P:ZennoLab.Emulation.Emulator.ErrorDetected">ErrorDetected Property</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.CloseWindow(System.String)">CloseWindow Method</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="M:ZennoLab.Emulation.Emulator.IsWindowExists(System.String)">IsWindowExists</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // activate the window just show it
    /// if (Emulator.IsWindowExists("First window"))
    /// {
    ///     string result = Emulator.ActiveWindow("First window");
    ///     // if was not any errors
    ///     if (result == "ok" &amp;&amp; !Emulator.ErrorDetected)
    ///         return "Window activated";
    /// }
    /// return "Fail in activation window";</code><code title="Example2" description="" lang="PHP">
    /// // activate the first window just show it
    /// if (ZennoLab\Emulation\Emulator::IsWindowExists("First window"))
    /// {
    ///     $result = ZennoLab\Emulation\Emulator::ActiveWindow("First window");
    ///     // if was not any errors
    ///     if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected)
    ///         return "Window activated";
    /// }
    /// else return "Fail: The first window";</code></example>
    /// <requirements>
    /// 	<para>
    /// 		Target Platforms:
    /// 		<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.</para>
    /// </requirements>
    public static bool IsWindowExists(string name)
    {
      lock (Emulator.Locker)
      {
        Emulator.ErrorDetected = false;
        try
        {
          if ((int) Emulator.FindWindowInternal((string) null, name, false) != 0)
            return true;
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
        }
        return false;
      }
    }

    /// <summary>Sets the active window.</summary>
    /// <param name="name">
    /// 	<para>Type: <see>System.String
    /// 	              </see></para>
    /// 	<para>The header of the window.</para>
    /// </param>
    /// <param name="topMost">
    /// 	<para>
    /// 
    /// Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.boolean.aspx">System.Boolean</see>
    /// 	</para>
    /// 	<para>
    /// 
    /// true if need to show the window on top of all; otherwise, false. Default value is false.
    /// 	</para>
    /// </param>
    /// <remarks>
    /// 	<para>There are several ways of calling this method. The parameter topMost have the default value and you can call the method without this
    /// parameter. See examples.</para>
    /// 	<para>This method calls automatically every time when you use methods from <see cref="N:ZennoLab.Emulation">ZennoLab.Emulation</see>.</para>
    /// </remarks>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.ButtonClick(System.String,System.String,System.Boolean)">ButtonClick Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseClick(System.String,ZennoLab.Emulation.MouseButton,ZennoLab.Emulation.MouseButtonEvent,System.Int32,System.Int32,System.Boolean)">MouseClick Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseMove(System.String,System.Int32,System.Int32,System.Boolean)">MouseMove Method</seealso>
    /// <seealso cref="P:ZennoLab.Emulation.Emulator.ErrorDetected">ErrorDetected Property</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.CloseWindow(System.String)">CloseWindow Method</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="M:ZennoLab.Emulation.Emulator.ActiveWindow(System.String,System.Boolean)">ActiveWindow</see> method. In this code activated the three windows. The first and the second
    /// are shown without parameter topMost (topMost is false). But the third window is shown on top of all.
    /// <code title="Example" description="" lang="C#">
    /// // activate the first window just show it
    /// string result = Emulator.ActiveWindow("First window");
    /// // if was not any errors
    /// if (result == "ok" &amp;&amp; !Emulator.ErrorDetected)
    /// {
    ///     // also activate the second window just show it but set the parameters topMost == false
    ///     result = Emulator.ActiveWindow("Second window", false);
    ///     // if was not any errors
    ///     if (result == "ok" &amp;&amp; !Emulator.ErrorDetected)
    ///     {
    ///         // also activate the third window and show on top of all. Set the parameters topMost == true
    ///         result = Emulator.ActiveWindow("Third window", true);
    ///         // if all nice
    ///         if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "All in openwork boss!";
    ///         // if bad
    ///         else return "Fail: The third window";
    ///     }
    ///     else return "Fail: The second window";
    /// }
    /// else return "Fail: The first window";</code><code title="Example2" description="" lang="PHP">
    /// // activate the first window just show it
    /// $result = ZennoLab\Emulation\Emulator::ActiveWindow("First window");
    /// // if was not any errors
    /// if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected)
    /// {
    ///     // also activate the second window just show it but set the parameters topMost == false
    ///     $result = ZennoLab\Emulation\Emulator::ActiveWindow("Second window", false);
    ///     // if was not any errors
    ///     if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected)
    ///     {
    ///         // also activate the third window and show on top of all. Set the parameters topMost == true
    ///         $result = ZennoLab\Emulation\Emulator::ActiveWindow("Third window", true);
    ///         // if all nice
    ///         if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected) return "All in openwork boss!";
    ///         // if bad
    ///         else return "Fail: The third window";
    ///     }
    ///     else return "Fail: The second window";
    /// }
    /// else return "Fail: The first window";</code></example>
    /// <requirements>
    /// 	<para>
    /// 		Target Platforms:
    /// 		<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.</para>
    /// </requirements>
    public static string ActiveWindow(string name, bool topMost = false)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          IntPtr local_1 = Emulator.FindWindowInternal((string) null, name, topMost);
          if ((int) local_1 != 0)
          {
            Emulator.SetActiveWindow(local_1);
            Emulator.SetForegroundWindow(local_1);
          }
          else
          {
            Emulator.ErrorDetected = true;
            local_0 = "Window not found";
          }
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <summary>Performs the maximization of the specified window.</summary>
    /// <param name="name">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The header of the window.</para>
    /// </param>
    /// <param name="topMost">
    /// 	<para>
    /// 
    /// Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.boolean.aspx">System.Boolean</see>
    /// 	</para>
    /// 	<para>
    /// 
    /// true if need to show the window on top of all; otherwise, false. Default value is false.
    /// 	</para>
    /// </param>
    /// <remarks>
    /// 	<para>For using of this method not required the call <see cref="M:ZennoLab.Emulation.Emulator.ActiveWindow(System.String,System.Boolean)">ActivateWindow</see> method.</para>
    /// </remarks>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.ActiveWindow(System.String,System.Boolean)">ActiveWindow Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MinimizeWindow(System.String,System.Boolean)">MinimizeWindow Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.CloseWindow(System.String)">CloseWindow Method</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="M:ZennoLab.Emulation.Emulator.MaximizeWindow(System.String,System.Boolean)">MaximizeWindow</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // maximize the window
    /// string result = Emulator.MaximizeWindow("TestForm");
    /// // check the result
    /// if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "Window was maximize";
    /// else return "Fail";</code><code title="Example2" description="" lang="PHP">
    /// // maximize the window
    /// $result = ZennoLab\Emulation\Emulator::MaximizeWindow("TestForm");
    /// // check the result
    /// if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected) return "Window was maximize";
    /// else return "Fail";</code></example>
    /// <requirements>
    /// 	<para>
    /// 		Target Platforms:
    /// 		<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.</para>
    /// </requirements>
    public static string MaximizeWindow(string name, bool topMost = false)
    {
      return Emulator.ChangeWindow(name, topMost, 3);
    }

    /// <summary>Performs the minimization of the specified window.</summary>
    /// <param name="name">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The header of the window.</para>
    /// </param>
    /// <param name="topMost">
    /// 	<para>
    /// 
    /// Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.boolean.aspx">System.Boolean</see>
    /// 	</para>
    /// 	<para>
    /// 
    /// true if need to show the window on top of all; otherwise, false. Default value is false.
    /// 	</para>
    /// </param>
    /// <remarks>
    /// 	<para>For using of this method not required the call <see cref="M:ZennoLab.Emulation.Emulator.ActiveWindow(System.String,System.Boolean)">ActivateWindow</see> method.</para>
    /// </remarks>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.ActiveWindow(System.String,System.Boolean)">ActiveWindow Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MaximizeWindow(System.String,System.Boolean)">MaximizeWindow Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.CloseWindow(System.String)">CloseWindow Method</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="M:ZennoLab.Emulation.Emulator.MinimizeWindow(System.String,System.Boolean)">MinimizeWindow</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // minimize the window
    /// string result = Emulator.MinimizeWindow("TestForm");
    /// // check the result
    /// if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "Window was minimize";
    /// else return "Fail";</code><code title="Example2" description="" lang="PHP">
    /// // minimize the window
    /// $result = ZennoLab\Emulation\Emulator::MinimizeWindow("TestForm");
    /// // check the result
    /// if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected) return "Window was minimize";
    /// else return "Fail";</code></example>
    /// <requirements>
    /// 	<para>
    /// 		Target Platforms:
    /// 		<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.</para>
    /// </requirements>
    public static string MinimizeWindow(string name, bool topMost = false)
    {
      return Emulator.ChangeWindow(name, topMost, 6);
    }

    private static string ChangeWindow(string name, bool topMost, int type)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          IntPtr local_1 = Emulator.FindWindowInternal((string) null, name, topMost);
          if ((int) local_1 != 0)
          {
            Emulator.SetActiveWindow(local_1);
            Emulator.SetForegroundWindow(local_1);
            Emulator.ShowWindow(local_1, type);
          }
          else
          {
            Emulator.ErrorDetected = true;
            local_0 = "Window not found";
          }
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <summary>Closes a window with specified header.</summary>
    /// <param name="name">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The header of the window.</para>
    /// </param>
    /// <remarks>
    /// 	<para>For using of this method not required the call <see cref="M:ZennoLab.Emulation.Emulator.ActiveWindow(System.String,System.Boolean)">ActivateWindow</see> method.</para>
    /// </remarks>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.ActiveWindow(System.String,System.Boolean)">ActiveWindow Method</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="M:ZennoLab.Emulation.Emulator.CloseWindow(System.String)">CloseWindow</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // close the window
    /// string result = Emulator.CloseWindow("TestForm");
    /// // check the result
    /// if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "Window was closed";
    /// else return "Fail";</code><code title="Example2" description="" lang="PHP">
    /// // close the window
    /// $result = ZennoLab\Emulation\Emulator::CloseWindow("TestForm");
    /// // check the result
    /// if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected) return "Window was closed";
    /// else return "Fail";</code></example>
    /// <requirements>
    /// 	<para>
    /// 
    /// 			Target Platforms:
    /// 			<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.
    /// 	</para>
    /// </requirements>
    public static string CloseWindow(string name)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          IntPtr local_1 = Emulator.FindWindowInternal((string) null, name, false);
          if ((int) local_1 != 0)
          {
            Emulator.SetActiveWindow(local_1);
            Emulator.SetForegroundWindow(local_1);
            Emulator.PostMessage(local_1, (IntPtr) 274, (IntPtr) 61536, IntPtr.Zero);
          }
          else
          {
            Emulator.ErrorDetected = true;
            local_0 = "Window not found";
          }
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <overloads>Sends the keyboard events.</overloads>
    /// <summary>Sends the keyboard events to the window with specified handle.</summary>
    /// <param name="key">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/en-us/library/system.windows.forms.keys.aspx">System.Windows.Forms.Keys</see></para>
    /// 	<para>The key for send.</para>
    /// </param>
    /// <param name="keyboardEvent">
    /// 	<para>Type: <paramref name="KeyboardEvent">ZennoLab.Emulation.KeyboardEvent</paramref></para>
    /// 	<para>The event of key for emulation.</para>
    /// </param>
    /// <param name="handle">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The handle of the window.</para>
    /// </param>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.SendText">SendText Method</seealso>
    /// <seealso cref="P:ZennoLab.Emulation.Emulator.ErrorDetected">ErrorDetected Property</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="O:ZennoLab.Emulation.Emulator.SendKey">SendKey</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // find the html element
    /// HtmlElement he = instance.ActiveTab.FindElementByTag("input:text", 0);
    /// // check the element
    /// if (!he.IsVoid)
    /// {
    ///     // send key
    ///     string result = Emulator.SendKey(instance.ActiveTab.Handle,System.Windows.Forms.Keys.Z, KeyboardEvent.Down);
    ///     // send result
    ///     if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "Key was sent";
    ///     return "Fail";
    /// }
    /// return "Element not found";</code><code title="Example2" description="" lang="PHP">
    /// // find the html element
    /// $he = $instance-&gt;ActiveTab-&gt;FindElementByTag("input:text", 0);
    /// // check the element
    /// if (!$he-&gt;IsVoid)
    /// {
    ///     // send key
    ///     $result = ZennoLab\Emulation\Emulator::SendKey($instance-&gt;ActiveTab-&gt;Handle, System\Windows\Forms\Keys::Z, KeyboardEvent::Down);
    ///     // send result
    ///     if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator.ErrorDetected) return "Key was sent";
    ///     return "Fail";
    /// }
    /// return "Element not found";</code></example>
    /// <requirements>
    /// 	<para>
    /// 
    /// 			Target Platforms:
    /// 			<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.
    /// 	</para>
    /// </requirements>
    public static string SendKey(int handle, Keys key, KeyboardEvent keyboardEvent)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          IntPtr local_1 = Emulator.GetKeyCode(key);
          switch (keyboardEvent)
          {
            case KeyboardEvent.Down:
              Emulator.PostMessage((IntPtr) handle, (IntPtr) 256, local_1, (IntPtr) 1);
              break;
            case KeyboardEvent.Up:
              Emulator.PostMessage((IntPtr) handle, (IntPtr) 257, local_1, (IntPtr) 1);
              break;
            case KeyboardEvent.Press:
              Emulator.PostMessage((IntPtr) handle, (IntPtr) 256, local_1, (IntPtr) 1);
              Emulator.PostMessage((IntPtr) handle, (IntPtr) 257, local_1, (IntPtr) 1);
              break;
          }
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <summary>Sends the keyboard events to the specified window.</summary>
    /// <overloads>Sends the keyboard events.</overloads>
    /// <param name="topMost">
    /// 	<para>
    /// 
    /// Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.boolean.aspx">System.Boolean</see>
    /// 	</para>
    /// 	<para>
    /// 
    /// true if need to show the window on top of all; otherwise, false. Default value is false.
    /// 	</para>
    /// </param>
    /// <param name="windowName">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The header of the window.</para>
    /// </param>
    /// <param name="x">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The x coordinate relative to the window.</para>
    /// </param>
    /// <param name="y">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The y coordinate relative to the window.</para>
    /// </param>
    /// <param name="keyboardEvent">
    /// 	<para>Type: <paramref name="KeyboardEvent">ZennoLab.Emulation.KeyboardEvent</paramref></para>
    /// 	<para>The event of key for emulation.</para>
    /// </param>
    /// <param name="key">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/en-us/library/system.windows.forms.keys.aspx">System.Windows.Forms.Keys</see></para>
    /// 	<para>The key for send.</para>
    /// </param>
    /// <remarks>
    /// 	<para>There are several ways of calling this method. The parameter topMost have the default value and you can call the method without this
    /// parameter.</para>
    /// 	<para>The x and y can be -1. In this case the key will be sent to the current window.</para>
    /// </remarks>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.SendText(System.String,System.Int32,System.Int32,System.String,System.Boolean)">SendText Method</seealso>
    /// <seealso cref="P:ZennoLab.Emulation.Emulator.ErrorDetected">ErrorDetected Property</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="O:ZennoLab.Emulation.Emulator.SendKey">SendKey</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // send the key "a"
    /// string result = Emulator.SendKey("Window", 200, 200, System.Windows.Forms.Keys.A, KeyboardEvent.Press);
    /// // check result
    /// if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "Key was sent";
    /// else return "Fail";</code><code title="Example2" description="" lang="PHP">
    /// // send the key "a"
    /// $result = ZennoLab\Emulation\Emulator::SendKey("Window", 200, 200, System\Windows\Forms\Keys.A, KeyboardEvent::Press);
    /// // check result
    /// if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected) return "Key was sent";
    /// else return "Fail";</code></example>
    /// <requirements>
    /// 	<para>
    /// 
    /// 			Target Platforms:
    /// 			<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.
    /// 	</para>
    /// </requirements>
    public static string SendKey(string windowName, int x, int y, Keys key, KeyboardEvent keyboardEvent, bool topMost = false)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          IntPtr local_1 = Emulator.FindWindowInternal((string) null, windowName, topMost);
          if ((int) local_1 != 0)
          {
            Emulator.RECT local_2 = new Emulator.RECT();
            Emulator.GetWindowRect(local_1, ref local_2);
            if (x != -1 && y != -1)
              local_1 = Emulator.WindowFromPoint(new Point(local_2.Left + x, local_2.Top + y));
            if ((int) local_1 != 0)
            {
              Emulator.SetForegroundWindow(local_1);
              IntPtr local_3 = Emulator.GetKeyCode(key);
              switch (keyboardEvent)
              {
                case KeyboardEvent.Down:
                  Emulator.PostMessage(local_1, (IntPtr) 256, local_3, (IntPtr) 1);
                  break;
                case KeyboardEvent.Up:
                  Emulator.PostMessage(local_1, (IntPtr) 257, local_3, (IntPtr) 1);
                  break;
                case KeyboardEvent.Press:
                  Emulator.PostMessage(local_1, (IntPtr) 256, local_3, (IntPtr) 1);
                  Emulator.PostMessage(local_1, (IntPtr) 257, local_3, (IntPtr) 1);
                  break;
              }
            }
            else
            {
              Emulator.ErrorDetected = true;
              local_0 = "Child window not found";
            }
          }
          else
          {
            Emulator.ErrorDetected = true;
            local_0 = "Window not found";
          }
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <summary>Sends a text to the window with specified handle.</summary>
    /// <overloads>Sends a text to the window.</overloads>
    /// <param name="text">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The text for send.</para>
    /// </param>
    /// <param name="handle">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The handle of the window.</para>
    /// </param>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.SendKey">SendKey Method</seealso>
    /// <seealso cref="P:ZennoLab.Emulation.Emulator.ErrorDetected">ErrorDetected Property</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="O:ZennoLab.Emulation.Emulator.SendText">SendText</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // find the html element
    /// HtmlElement he = instance.ActiveTab.FindElementByTag("input:text", 0);
    /// // check the element
    /// if (!he.IsVoid)
    /// {
    ///     // focus on this element
    ///     he.Focus();
    ///     // send text
    ///     string result = Emulator.SendText(instance.ActiveTab.Handle, "Simple text");
    ///     // send result
    ///     if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "Key was sent";
    ///     return "Fail";
    /// }
    /// return "Element not found";</code><code title="Example2" description="" lang="PHP">
    /// // find the html element
    /// $he = $instance-&gt;ActiveTab-&gt;FindElementByTag("input:text", 0);
    /// // check the element
    /// if (!$he-&gt;IsVoid)
    /// {
    ///     // focus on this element
    ///     $he-&gt;Focus();
    ///     // send text
    ///     $result = ZennoLab\Emulation\Emulator::SendText($instance-&gt;ActiveTab-&gt;Handle, "Simple text");
    ///     // send result
    ///     if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected) return "Key was sent";
    ///     return "Fail";
    /// }
    /// return "Element not found";</code></example>
    /// <requirements>
    /// 	<para>
    /// 
    /// 			Target Platforms:
    /// 			<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.
    /// 	</para>
    /// </requirements>
    public static string SendText(int handle, string text)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          foreach (char item_0 in text)
            Emulator.SendChar((IntPtr) handle, item_0);
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <overloads>Sends a text to the window.</overloads>
    /// <summary>Sends a text to the specified window.</summary>
    /// <param name="topMost">
    /// 	<para>
    /// 
    /// Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.boolean.aspx">System.Boolean</see>
    /// 	</para>
    /// 	<para>
    /// 
    /// true if need to show the window on top of all; otherwise, false. Default value is false.
    /// 	</para>
    /// </param>
    /// <param name="windowName">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The header of the window.</para>
    /// </param>
    /// <param name="x">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The x coordinate relative to the window.</para>
    /// </param>
    /// <param name="y">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The y coordinate relative to the window.</para>
    /// </param>
    /// <param name="text">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The text for send.</para>
    /// </param>
    /// <remarks>
    /// 	<para>There are several ways of calling this method. The parameter topMost have the default value and you can call the method without this
    /// parameter.</para>
    /// 	<para>The x and y can be -1. In this case the text will be sent to the current window.</para>
    /// </remarks>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.SendKey(System.String,System.Int32,System.Int32,System.Windows.Forms.Keys,ZennoLab.Emulation.KeyboardEvent,System.Boolean)">SendKey Method</seealso>
    /// <seealso cref="P:ZennoLab.Emulation.Emulator.ErrorDetected">ErrorDetected Property</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="O:ZennoLab.Emulation.Emulator.SendText">SendText</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // send the text
    /// string result = Emulator.SendText("Window", 200, 200, "it's a simple text for send");
    /// // check result
    /// if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "Text was sent";
    /// else return "Fail";</code><code title="Example2" description="" lang="PHP">
    /// // send the text
    /// $result = ZennoLab\Emulation\Emulator::SendText("Window", 200, 200, "it's a simple text for send");
    /// // check result
    /// if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected) return "Text was sent";
    /// else return "Fail";</code></example>
    /// <requirements>
    /// 	<para>
    /// 
    /// 			Target Platforms:
    /// 			<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.
    /// 	</para>
    /// </requirements>
    public static string SendText(string windowName, int x, int y, string text, bool topMost = false)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          IntPtr local_1 = Emulator.FindWindowInternal((string) null, windowName, topMost);
          if ((int) local_1 != 0)
          {
            Emulator.SetActiveWindow(local_1);
            Emulator.RECT local_2 = new Emulator.RECT();
            Emulator.GetWindowRect(local_1, ref local_2);
            if (x != -1 && y != -1)
              local_1 = Emulator.WindowFromPoint(new Point(local_2.Left + x, local_2.Top + y));
            if ((int) local_1 != 0)
            {
              Emulator.SetForegroundWindow(local_1);
              foreach (char item_0 in text)
                Emulator.SendChar(local_1, item_0);
            }
            else
            {
              Emulator.ErrorDetected = true;
              local_0 = "Child window not found";
            }
          }
          else
          {
            Emulator.ErrorDetected = true;
            local_0 = "Window not found";
          }
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <summary>Emulates the mouse's events in the specified window.</summary>
    /// <overloads>Emulates the mouse's events.</overloads>
    /// <param name="topMost">
    /// 	<para>
    /// 
    /// Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.boolean.aspx">System.Boolean</see>
    /// 	</para>
    /// 	<para>
    /// 
    /// true if need to show the window on top of all; otherwise, false. Default value is false.
    /// 	</para>
    /// </param>
    /// <param name="windowName">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The header of the window.</para>
    /// </param>
    /// <param name="buttonEvent">
    /// 	<para>Type: <see cref="T:ZennoLab.Emulation.MouseButtonEvent">ZennoLab.Emulation.MouseButtonEvent</see></para>
    /// 	<para>The event of mouse button for emulation.</para>
    /// </param>
    /// <param name="x">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The x coordinate relative to the window.</para>
    /// </param>
    /// <param name="y">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The y coordinate relative to the window.</para>
    /// </param>
    /// <param name="button">
    /// 	<para>Type: <see cref="T:ZennoLab.Emulation.MouseButton">ZennoLab.Emulation.MouseButton</see></para>
    /// 	<para>The mouse button for emulation.</para>
    /// </param>
    /// <remarks>
    /// 	<para>There are several ways of calling this method. The parameter topMost have the default value and you can call the method without this
    /// parameter.</para>
    /// 	<para>The x and y can be -1. In this case the click will be performed to the current window.</para>
    /// </remarks>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.ButtonClick(System.String,System.String,System.Boolean)">ButtonClick Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseMove(System.String,System.Int32,System.Int32,System.Boolean)">MouseMove Method</seealso>
    /// <seealso cref="T:ZennoLab.Emulation.MouseButton">MouseButton Enumeration</seealso>
    /// <seealso cref="T:ZennoLab.Emulation.MouseButtonEvent">MouseButtonEvent Enumeration</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="O:ZennoLab.Emulation.Emulator.MouseClick">MouseClick</see> method. First of all in this part of code call
    /// <see cref="M:ZennoLab.Emulation.Emulator.MouseClick(System.String,ZennoLab.Emulation.MouseButton,ZennoLab.Emulation.MouseButtonEvent,System.Int32,System.Int32,System.Boolean)">MouseClick</see> method and as result it opens the save file dialog. Then waits two seconds (the time to display the window) and clicks on button with
    /// caption " <em>Save</em> " in window with header " <em>Save as</em> " using <see cref="M:ZennoLab.Emulation.Emulator.ButtonClick(System.String,System.String,System.Boolean)">ButtonClick</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // show the save file dialog
    /// string result = Emulator.MouseClick("Simple window", MouseButton.Left, MouseButtonEvent.Click, 200, 200);
    /// // wait a little bit
    /// System.Threading.Thread.Sleep(2000);
    /// // if mouse click was successful
    /// if (result == "ok" &amp;&amp; !Emulator.ErrorDetected)
    /// {
    ///     // click on button with caption "Save"
    ///     result = Emulator.ButtonClick("Save as", "Save");
    ///     // make answer
    ///     if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "All done";
    ///     else return "Fail";
    /// }</code><code title="Example2" description="" lang="PHP">
    /// // show the save file dialog
    /// $result = ZennoLab\Emulation\Emulator::MouseClick("Simple window", MouseButton::Left, MouseButtonEvent::Click, 200, 200);
    /// // wait a little bit
    /// System\Threading\Thread::Sleep(2000);
    /// // if mouse click was successful
    /// if ($result == "ok" &amp;&amp; !Emulator::ErrorDetected)
    /// {
    ///     // click on button with caption "Save"
    ///     $result = ZennoLab\Emulation\Emulator::ButtonClick("Save as", "Save");
    ///     // make answer
    ///     if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected) return "All done";
    ///     else return "Fail";
    /// }</code></example>
    /// <requirements>
    /// 	<para>
    /// 
    /// 			Target Platforms:
    /// 			<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.
    /// 	</para>
    /// </requirements>
    public static string MouseClick(string windowName, MouseButton button, MouseButtonEvent buttonEvent, int x, int y, bool topMost = false)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          IntPtr local_1 = Emulator.FindWindowInternal((string) null, windowName, false);
          if ((int) local_1 != 0)
          {
            Emulator.SetActiveWindow(local_1);
            Emulator.RECT local_2 = new Emulator.RECT();
            Emulator.GetWindowRect(local_1, ref local_2);
            IntPtr local_1_1 = Emulator.WindowFromPoint(new Point(local_2.Left + x, local_2.Top + y));
            if ((int) local_1_1 != 0)
            {
              Emulator.SetForegroundWindow(local_1_1);
              int local_3 = 0;
              int local_4 = 0;
              switch (buttonEvent)
              {
                case MouseButtonEvent.Down:
                  local_3 = button == MouseButton.Left ? 513 : 516;
                  local_4 = 0;
                  break;
                case MouseButtonEvent.Up:
                  local_3 = button == MouseButton.Left ? 514 : 517;
                  local_4 = 0;
                  break;
                case MouseButtonEvent.Click:
                  local_3 = button == MouseButton.Left ? 513 : 516;
                  local_4 = button == MouseButton.Left ? 514 : 517;
                  break;
              }
              if (local_4 == 0)
              {
                Emulator.PostMessage(local_1_1, (IntPtr) local_3, IntPtr.Zero, (IntPtr) (y << 16 | x));
              }
              else
              {
                Emulator.PostMessage(local_1_1, (IntPtr) local_3, IntPtr.Zero, (IntPtr) (y << 16 | x));
                Emulator.PostMessage(local_1_1, (IntPtr) local_4, IntPtr.Zero, (IntPtr) (y << 16 | x));
              }
            }
            else
            {
              Emulator.ErrorDetected = true;
              local_0 = "Child window not found";
            }
          }
          else
          {
            Emulator.ErrorDetected = true;
            local_0 = "Window not found";
          }
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <summary>Emulates the mouse's events in the window with specified handle.</summary>
    /// <overloads>Emulates the mouse's events.</overloads>
    /// <param name="button">
    /// 	<para>Type: <see cref="T:ZennoLab.Emulation.MouseButton">ZennoLab.Emulation.MouseButton</see></para>
    /// 	<para>The mouse button for emulation.</para>
    /// </param>
    /// <param name="buttonEvent">
    /// 	<para>Type: <see cref="T:ZennoLab.Emulation.MouseButtonEvent">ZennoLab.Emulation.MouseButtonEvent</see></para>
    /// 	<para>The event of mouse button for emulation.</para>
    /// </param>
    /// <param name="x">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The x coordinate relative to the window.</para>
    /// </param>
    /// <param name="y">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The y coordinate relative to the window.</para>
    /// </param>
    /// <param name="handle">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The handle of the window.</para>
    /// </param>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.ButtonClick(System.String,System.String,System.Boolean)">ButtonClick Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseMove">MouseMove Method</seealso>
    /// <seealso cref="T:ZennoLab.Emulation.MouseButton">MouseButton Enumeration</seealso>
    /// <seealso cref="T:ZennoLab.Emulation.MouseButtonEvent">MouseButtonEvent Enumeration</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="O:ZennoLab.Emulation.Emulator.MouseClick">MouseClick</see> method. First of all this code searches the html
    /// element by specified search conditions. And next performs the click to the specified point from the active tab's window of the instance.
    /// <code title="Example" description="" lang="C#">
    /// // find the html element
    /// HtmlElement he = instance.ActiveTab.FindElementByTag("input:submit", 0);
    /// // check the element
    /// if (!he.IsVoid)
    /// {
    ///     // click
    ///     string result = Emulator.MouseClick(instance.ActiveTab.Handle,
    ///         MouseButton.Left, MouseButtonEvent.Click, he.DisplacementInTabWindow.X + 10, he.DisplacementInTabWindow.Y + 10);
    ///     // click was performed
    ///     if (result == "ok") return "All done";
    ///     // fail
    ///     return "Fail";
    /// }
    /// // element not found
    /// return "Element not found";</code><code title="Example2" description="" lang="PHP">
    /// // find the html element
    /// $he = $instance-&gt;ActiveTab-&gt;FindElementByTag("input:submit", 0);
    /// // check the element
    /// if (!$he-&gt;IsVoid)
    /// {
    ///     // click
    ///     $result = ZennoLab\Emulation\Emulator::MouseClick($instance-&gt;ActiveTab-&gt;Handle,
    ///         ZennoLab\Emulation\MouseButton::Left, MouseButtonEvent::Click, $he-&gt;DisplacementInTabWindow-&gt;X + 10, $he-&gt;DisplacementInTabWindow-&gt;Y + 10);
    ///     // click was performed
    ///     if ($result == "ok") return "All done";
    ///     // fail
    ///     return "Fail";
    /// }
    /// // element not found
    /// return "Element not found";</code></example>
    /// <requirements>
    /// 	<para>
    /// 
    /// 			Target Platforms:
    /// 			<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.
    /// 	</para>
    /// </requirements>
    public static string MouseClick(int handle, MouseButton button, MouseButtonEvent buttonEvent, int x, int y)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          Emulator.SetForegroundWindow((IntPtr) handle);
          int local_1 = 0;
          int local_2 = 0;
          switch (buttonEvent)
          {
            case MouseButtonEvent.Down:
              local_1 = button == MouseButton.Left ? 513 : 516;
              local_2 = 0;
              break;
            case MouseButtonEvent.Up:
              local_1 = button == MouseButton.Left ? 514 : 517;
              local_2 = 0;
              break;
            case MouseButtonEvent.Click:
              local_1 = button == MouseButton.Left ? 513 : 516;
              local_2 = button == MouseButton.Left ? 514 : 517;
              break;
          }
          if (local_2 == 0)
          {
            Emulator.PostMessage((IntPtr) handle, (IntPtr) local_1, IntPtr.Zero, (IntPtr) (y << 16 | x));
          }
          else
          {
            Emulator.PostMessage((IntPtr) handle, (IntPtr) local_1, IntPtr.Zero, (IntPtr) (y << 16 | x));
            Emulator.PostMessage((IntPtr) handle, (IntPtr) local_2, IntPtr.Zero, (IntPtr) (y << 16 | x));
          }
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <summary>Moves the mouse to a specified location in the specified window.</summary>
    /// <overloads>Moves the mouse to a specified location.</overloads>
    /// <param name="topMost">
    /// 	<para>
    /// 
    /// Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.boolean.aspx">System.Boolean</see>
    /// 	</para>
    /// 	<para>
    /// 
    /// true if need to show the window on top of all; otherwise, false. Default value is false.
    /// 	</para>
    /// </param>
    /// <param name="windowName">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The header of the window.</para>
    /// </param>
    /// <param name="x">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The x coordinate relative to the window.</para>
    /// </param>
    /// <param name="y">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The y coordinate relative to the window.</para>
    /// </param>
    /// <remarks>There are several ways of calling this method. The parameter topMost have the default value and you can call the method without this
    /// parameter.</remarks>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.ButtonClick(System.String,System.String,System.Boolean)">ButtonClick Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseClick(System.String,ZennoLab.Emulation.MouseButton,ZennoLab.Emulation.MouseButtonEvent,System.Int32,System.Int32,System.Boolean)">MouseClick Method</seealso>
    /// <seealso cref="P:ZennoLab.Emulation.Emulator.ErrorDetected">ErrorDetected Property</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="O:ZennoLab.Emulation.Emulator.MouseMove">MouseMove</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // random
    /// Random rnd = new Random();
    /// // x location
    /// int x = 200;
    /// // y location
    /// int y = 200;
    /// // offset X
    /// int offsetX = rnd.Next(100);
    /// // offset y
    /// int offsetY = rnd.Next(100);
    /// // move mouse to start location x = 200 and y = 200 on "Window" window
    /// string result = Emulator.MouseMove("Window", x, y);
    /// // check result
    /// if (result != "ok" &amp;&amp; Emulator.ErrorDetected) return "Fail";
    /// // mouse move to a new location
    /// result = Emulator.MouseMove("Window", x + offsetX, y + offsetY);
    /// // check result
    /// if (result != "ok" &amp;&amp; Emulator.ErrorDetected) return "Fail";
    /// // answer
    /// return String.Format("Mouse was moved from x = {0}; y = {1} to x = {2}; y = {3}", x, y, x + offsetX, y + offsetY);</code><code title="Example2" description="" lang="PHP">
    /// // random
    /// $rnd = new Random();
    /// // x location
    /// $x = 200;
    /// // y location
    /// $y = 200;
    /// // offset X
    /// $offsetX = $rnd-&gt;Next(100);
    /// // offset y
    /// $offsetY = $rnd-&gt;Next(100);
    /// // move mouse to start location x = 200 and y = 200 on "Window" window
    /// $result = ZennoLab\Emulation\Emulator::MouseMove("Window", $x, $y);
    /// // check result
    /// if ($result != "ok" &amp;&amp; ZennoLab\Emulation\Emulator::ErrorDetected) return "Fail";
    /// // mouse move to a new location
    /// $result = ZennoLab\Emulation\Emulator::MouseMove("Window", $x + $offsetX, $y + $offsetY);
    /// // check result
    /// if ($result != "ok" &amp;&amp; ZennoLab\Emulation\Emulator::ErrorDetected) return "Fail";
    /// // answer
    /// return String::Format("Mouse was moved from x = {0}; y = {1} to x = {2}; y = {3}", $x, $y, $x + $offsetX, $y + $offsetY);</code></example>
    /// <requirements>
    /// 	<para>
    /// 
    /// 			Target Platforms:
    /// 			<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.
    /// 	</para>
    /// </requirements>
    public static string MouseMove(string windowName, int x, int y, bool topMost = false)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          IntPtr local_1 = Emulator.FindWindowInternal((string) null, windowName, topMost);
          if ((int) local_1 != 0)
          {
            Emulator.SetActiveWindow(local_1);
            Emulator.RECT local_2 = new Emulator.RECT();
            Emulator.GetWindowRect(local_1, ref local_2);
            IntPtr local_1_1 = Emulator.WindowFromPoint(new Point(local_2.Left + x, local_2.Top + y));
            if ((int) local_1_1 != 0)
            {
              Emulator.SetForegroundWindow(local_1_1);
              Emulator.SetCursorPos(local_2.Left + x, local_2.Top + y);
            }
            else
            {
              Emulator.ErrorDetected = true;
              local_0 = "Child window not found";
            }
          }
          else
          {
            Emulator.ErrorDetected = true;
            local_0 = "Window not found";
          }
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <summary>Moves the mouse to a specified location in the window with specified handle.</summary>
    /// <overloads>Moves the mouse to a specified location.</overloads>
    /// <param name="handle">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The handle of the window.</para>
    /// </param>
    /// <param name="x">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The x coordinate relative to the window.</para>
    /// </param>
    /// <param name="y">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The y coordinate relative to the window.</para>
    /// </param>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseClick">MouseClick Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseMove">MouseMove Method</seealso>
    /// <seealso cref="P:ZennoLab.Emulation.Emulator.ErrorDetected">ErrorDetected Property</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="O:ZennoLab.Emulation.Emulator.MouseMove">MouseMove</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // find the html element
    /// HtmlElement he = instance.ActiveTab.FindElementByTag("input:submit", 0);
    /// // check the element
    /// if (!he.IsVoid)
    /// {
    ///     // move the mouse to the html element location
    ///     string result = Emulator.MouseMove(instance.ActiveTab.Handle, he.DisplacementInTabWindow.X + 10, he.DisplacementInTabWindow.Y + 10);
    ///     if (result == "ok" &amp;&amp; !Emulator.ErrorDetected)
    ///     {
    ///         // wait a little bit
    ///         System.Threading.Thread.Sleep(200);
    ///         // perform the click
    ///         result = Emulator.MouseClick(instance.ActiveTab.Handle, MouseButton.Left, MouseButtonEvent.Click, he.DisplacementInTabWindow.X + 10, he.DisplacementInTabWindow.Y + 10);
    ///         if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "All done";
    ///         return "Mouse click failed";
    ///     }
    ///     else return "Mouse move failed";
    /// }
    /// return "Element not found";</code><code title="Example2" description="" lang="PHP">
    /// // find the html element
    /// $he = $instance-&gt;ActiveTab-&gt;FindElementByTag("input:submit", 0);
    /// // check the element
    /// if (!$he-&gt;IsVoid)
    /// {
    ///     // move the mouse to the html element location
    ///     $result = ZennoLab\Emulation\Emulator::MouseMove($instance-&gt;ActiveTab-&gt;Handle, $he-&gt;DisplacementInTabWindow-&gt;X + 10, $he-&gt;DisplacementInTabWindow-&gt;Y + 10);
    ///     if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected)
    ///     {
    ///         // wait a little bit
    ///         System\Threading.\Thread\Sleep(200);
    ///         // perform the click
    ///         $result = ZennoLab\Emulation\Emulator::MouseClick($instance-&gt;ActiveTab-&gt;Handle, MouseButton::Left, MouseButtonEvent::Click, $he-&gt;DisplacementInTabWindow-&gt;X + 10, he-&gt;DisplacementInTabWindow-&gt;Y + 10);
    ///         if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected) return "All done";
    ///         return "Mouse click failed";
    ///     }
    ///     else return "Mouse move failed";
    /// }
    /// return "Element not found";</code></example>
    /// <requirements>
    /// 	<para>
    /// 
    /// 			Target Platforms:
    /// 			<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.
    /// 	</para>
    /// </requirements>
    public static string MouseMove(int handle, int x, int y)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          Emulator.RECT local_1 = new Emulator.RECT();
          Emulator.GetWindowRect((IntPtr) handle, ref local_1);
          Emulator.SetCursorPos(local_1.Left + x, local_1.Top + y);
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <summary>Clicks a button with specified caption in specified window.</summary>
    /// <param name="topMost">
    /// 	<para>
    /// 
    /// Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.boolean.aspx">System.Boolean</see>
    /// 	</para>
    /// 	<para>
    /// 
    /// true if need to show the window on top of all; otherwise, false. Default value is false.
    /// 	</para>
    /// </param>
    /// <param name="windowName">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The header of the window.</para>
    /// </param>
    /// <param name="buttonName">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The text displayed on the button.</para>
    /// </param>
    /// <remarks>
    /// 	<para>There are several ways of calling this method. The parameter topMost have the default value and you can call the method without this
    /// parameter.</para>
    /// 	<para>This method searches the button by the caption and does not case sensitive.</para>
    /// </remarks>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseClick(System.String,ZennoLab.Emulation.MouseButton,ZennoLab.Emulation.MouseButtonEvent,System.Int32,System.Int32,System.Boolean)">MouseClick Method</seealso>
    /// <seealso cref="M:ZennoLab.Emulation.Emulator.MouseMove(System.String,System.Int32,System.Int32,System.Boolean)">MouseMove Method</seealso>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="M:ZennoLab.Emulation.Emulator.ButtonClick(System.String,System.String,System.Boolean)">ButtonClick</see> method. First of all in this part of code call <see cref="M:ZennoLab.Emulation.Emulator.MouseClick(System.String,ZennoLab.Emulation.MouseButton,ZennoLab.Emulation.MouseButtonEvent,System.Int32,System.Int32,System.Boolean)">MouseClick</see> method and
    /// as result it opens the save file dialog. Then waits two seconds (the time to display the window) and clicks on button with caption "
    /// <em>Save</em> " in window with header " <em>Save as</em> " using <see cref="M:ZennoLab.Emulation.Emulator.ButtonClick(System.String,System.String,System.Boolean)">ButtonClick</see> method.
    /// <code title="Example" description="" lang="C#">
    /// // show the save file dialog
    /// string result = Emulator.MouseClick("Simple window", MouseButton.Left, MouseButtonEvent.Click, 200, 200);
    /// // wait a little bit
    /// System.Threading.Thread.Sleep(2000);
    /// // if mouse click was successful
    /// if (result == "ok" &amp;&amp; !Emulator.ErrorDetected)
    /// {
    ///     // click on button with caption "Save"
    ///     result = Emulator.ButtonClick("Save as", "Save");
    ///     // make answer
    ///     if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "All done";
    ///     else return "Fail";
    /// }</code><code title="Example2" description="" lang="PHP">
    /// // show the save file dialog
    /// $result = ZennoLab\Emulation\Emulator::MouseClick("Simple window", MouseButton::Left, MouseButtonEvent::Click, 200, 200);
    /// // wait a little bit
    /// System\Threading\Thread::Sleep(2000);
    /// // if mouse click was successful
    /// if ($result == "ok" &amp;&amp; !Emulator::ErrorDetected)
    /// {
    ///     // click on button with caption "Save"
    ///     $result = ZennoLab\Emulation\Emulator::ButtonClick("Save as", "Save");
    ///     // make answer
    ///     if ($result == "ok" &amp;&amp; !ZennoLab\Emulation\Emulator::ErrorDetected) return "All done";
    ///     else return "Fail";
    /// }</code></example>
    /// <requirements>
    /// 	<para>
    /// 		Target Platforms:
    /// 		<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.</para>
    /// </requirements>
    public static string ButtonClick(string windowName, string buttonName, bool topMost = false)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          IntPtr local_1 = Emulator.FindWindowInternal((string) null, windowName, topMost);
          if ((int) local_1 != 0)
          {
            Emulator.SetActiveWindow(local_1);
            int local_2 = Emulator.GetWindow(local_1, (IntPtr) 5);
            bool local_3 = false;
            for (; local_2 != 0; local_2 = Emulator.GetWindow((IntPtr) local_2, (IntPtr) 2))
            {
              if ((IntPtr) local_2 == local_1)
                local_2 = Emulator.GetWindow((IntPtr) local_2, (IntPtr) 2);
              Emulator.SetForegroundWindow((IntPtr) local_2);
              StringBuilder local_4 = new StringBuilder(1024);
              Emulator.GetWindowText((IntPtr) local_2, local_4, local_4.Capacity);
              if (local_4.ToString().ToLower() == buttonName.ToLower() || local_4.ToString().ToLower().Contains(buttonName.ToLower()) || local_4.ToString().ToLower().Replace("&", string.Empty).Contains(buttonName.ToLower()))
              {
                StringBuilder local_5 = new StringBuilder(1024);
                Emulator.GetClassName((IntPtr) local_2, local_5, local_5.Capacity);
                local_1 = (IntPtr) local_2;
                local_3 = true;
                break;
              }
            }
            if (local_3)
            {
              Emulator.SetActiveWindow(local_1);
              Emulator.SetForegroundWindow((IntPtr) local_2);
              Emulator.PostMessage(local_1, (IntPtr) 513, IntPtr.Zero, (IntPtr) 655370);
              Emulator.PostMessage(local_1, (IntPtr) 514, IntPtr.Zero, (IntPtr) 655370);
            }
            else
            {
              Emulator.ErrorDetected = true;
              local_0 = "Child window not found";
            }
          }
          else
          {
            Emulator.ErrorDetected = true;
            local_0 = "Window not found";
          }
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
          local_0 = exception_0.Message;
        }
        return local_0;
      }
    }

    /// <summary>Performs the drag and drop events inside specified window by handle.</summary>
    /// <param name="handle">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The handle of the window.</para>
    /// </param>
    /// <param name="fromX">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The x coordinate in the window for drag event.</para>
    /// </param>
    /// <param name="formY">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The y coordinate in the window for drag event.</para>
    /// </param>
    /// <param name="toX">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The x coordinate in the window for drop event.</para>
    /// </param>
    /// <param name="toY">
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.int32.aspx">System.Int32</see></para>
    /// 	<para>The y coordinate in the window for drop event.</para>
    /// </param>
    /// <returns>
    /// 	<para>Type: <see cref="!:http://msdn.microsoft.com/ru-ru/library/system.string.aspx">System.String</see></para>
    /// 	<para>The answer with information about the success of the current command's execution. If current command was successful then this answer is
    /// "<em>ok</em>"; otherwise message describing the error.</para>
    /// </returns>
    /// <example>
    /// The following code example demonstrates uses of the <see cref="M:ZennoLab.Emulation.Emulator.DragAndDrop(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)">DragAndDrop</see> method. This code just navigate to some page and perform the drag and drop events
    /// in specified coordinates.
    /// <code title="Example" description="" lang="C#">
    /// // go to page with drag and drop element
    /// if (instance.ActiveTab.IsNull || instance.ActiveTab.IsVoid) return "Fail";
    /// instance.ActiveTab.Navigate("dragdroppage.com");
    /// // wait downloading
    /// if (instance.ActiveTab.IsBusy) instance.ActiveTab.WaitDownloading();
    /// 
    /// // do drag and drop event
    /// return Emulator.DragAndDrop(instance.ActiveTab.Handle, 100, 100, 200);</code><code title="Example2" description="" lang="PHP">
    /// // go to page with drag and drop element
    /// if ($instance-&gt;ActiveTab-&gt;IsNull || $instance-&gt;ActiveTab-&gt;IsVoid) return "Fail";
    /// $instance-&gt;ActiveTab-&gt;Navigate("dragdroppage.com");
    /// // wait downloading
    /// if ($instance-&gt;ActiveTab-&gt;IsBusy) $instance-&gt;ActiveTab-&gt;WaitDownloading();
    /// 
    /// // do drag and drop event
    /// return ZennoLab\Emulation\Emulator::DragAndDrop($instance-&gt;ActiveTab-&gt;Handle, 100, 100, 200);</code></example>
    /// <requirements>
    /// 	<para>
    /// 
    /// 			Target Platforms:
    /// 			<em>Desktop:</em> Windows XP SP3 and older. <em>Server:</em> Windows Server 2003 and older.
    /// 	</para>
    /// </requirements>
    public static string DragAndDrop(int handle, int fromX, int formY, int toX, int toY)
    {
      lock (Emulator.Locker)
      {
        string local_0 = "ok";
        Emulator.ErrorDetected = false;
        try
        {
          if ((IntPtr) handle != IntPtr.Zero)
          {
            Emulator.SetActiveWindow((IntPtr) handle);
            Emulator.SetForegroundWindow((IntPtr) handle);
            Emulator.SetFocus((IntPtr) handle);
            Thread.Sleep(100);
            Emulator.PostMessage((IntPtr) handle, (IntPtr) 512, IntPtr.Zero, (IntPtr) (formY << 16 | fromX));
            Emulator.PostMessage((IntPtr) handle, (IntPtr) 513, IntPtr.Zero, (IntPtr) (formY << 16 | fromX));
            Emulator.PostMessage((IntPtr) handle, (IntPtr) 673, IntPtr.Zero, (IntPtr) (formY << 16 | fromX));
            Emulator.RECT local_1 = new Emulator.RECT();
            Emulator.GetWindowRect((IntPtr) handle, ref local_1);
            Emulator.SetCursorPos(local_1.Left + toX, local_1.Top + toY);
            Thread.Sleep(100);
            Emulator.PostMessage((IntPtr) handle, (IntPtr) 512, IntPtr.Zero, (IntPtr) (toY << 16 | toX));
            Emulator.PostMessage((IntPtr) handle, (IntPtr) 514, IntPtr.Zero, (IntPtr) (toY << 16 | toX));
            Emulator.PostMessage((IntPtr) handle, (IntPtr) 673, IntPtr.Zero, (IntPtr) (toY << 16 | toX));
          }
          else
          {
            Emulator.ErrorDetected = true;
            local_0 = "Window not found";
          }
        }
        catch (Exception exception_0)
        {
          local_0 = exception_0.ToString();
          Emulator.ErrorDetected = false;
        }
        return local_0;
      }
    }

    internal static void SetDialogText(string windowName, string text, bool topMost = false)
    {
      lock (Emulator.Locker)
      {
        Emulator.ErrorDetected = false;
        try
        {
          IntPtr local_0 = Emulator.FindWindowInternal((string) null, windowName, topMost);
          Emulator.SetActiveWindow(local_0);
          int local_1 = Emulator.GetWindow(local_0, (IntPtr) 5);
          bool local_2 = false;
          List<int> local_3 = new List<int>();
label_13:
          while (local_1 != 0)
          {
            if ((IntPtr) local_1 == local_0)
              local_1 = Emulator.GetWindow((IntPtr) local_1, (IntPtr) 2);
            Emulator.SetForegroundWindow((IntPtr) local_1);
            StringBuilder local_4 = new StringBuilder(1024);
            Emulator.GetClassName((IntPtr) local_1, local_4, local_4.Capacity);
            if (local_4.ToString().ToLower() == "edit")
            {
              local_0 = (IntPtr) local_1;
              local_2 = true;
              break;
            }
            local_1 = Emulator.GetWindow((IntPtr) local_1, (IntPtr) 2);
            if (local_1 == 0)
            {
              while (true)
              {
                if (local_1 == 0 && local_3.Count > 0)
                {
                  int local_5 = local_3[local_3.Count - 1];
                  local_3.RemoveAt(local_3.Count - 1);
                  Emulator.SetForegroundWindow((IntPtr) local_5);
                  local_1 = Emulator.GetWindow((IntPtr) local_5, (IntPtr) 5);
                }
                else
                  goto label_13;
              }
            }
            else if (!local_3.Contains(local_1))
              local_3.Add(local_1);
          }
          if (!local_2)
            return;
          Emulator.SetActiveWindow(local_0);
          Emulator.SetForegroundWindow((IntPtr) local_1);
          foreach (char item_0 in text)
            Emulator.SendChar(local_0, item_0);
        }
        catch (Exception exception_0)
        {
          Emulator.ErrorDetected = true;
        }
      }
    }

    private struct RECT
    {
      public readonly int Left;
      public readonly int Top;
      private readonly int Right;
      private readonly int Bottom;
    }
  }
}

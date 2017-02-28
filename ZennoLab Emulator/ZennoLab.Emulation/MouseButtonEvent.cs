// Decompiled with JetBrains decompiler
// Type: ZennoLab.Emulation.MouseButtonEvent
// Assembly: ZennoLab.Emulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3402272C-5FE8-4A11-863F-404E8B64C3F1
// Assembly location: C:\Program Files (x86)\ZennoLab\ZennoPoster Demo\Progs\ZennoLab.Emulation.dll

namespace ZennoLab.Emulation
{
  /// <summary>Specifies the mouse button event.</summary>
  /// <seealso cref="T:ZennoLab.Emulation.KeyboardEvent">KeyboardEvent Enumeration</seealso>
  /// <seealso cref="T:ZennoLab.Emulation.MouseButton">MouseButton Enumeration</seealso>
  /// <seealso cref="O:ZennoLab.Emulation.Emulator.MouseClick">MouseClick Method</seealso>
  /// <example>
  /// The following code example demonstrates uses of the <see cref="T:ZennoLab.Emulation.MouseButtonEvent">MouseButtonEvent</see> enumeration.
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
  /// $result = Emulator::MouseClick("Simple window", MouseButton::Left, MouseButtonEvent::Click, 200, 200);
  /// // wait a little bit
  /// System\Threading\Thread::Sleep(2000);
  /// // if mouse click was successful
  /// if ($result == "ok" &amp;&amp; !Emulator::ErrorDetected)
  /// {
  ///     // click on button with caption "Save"
  ///     $result = Emulator::ButtonClick("Save as", "Save");
  ///     // make answer
  ///     if ($result == "ok" &amp;&amp; !Emulator::ErrorDetected) return "All done";
  ///     else return "Fail";
  /// }</code></example>
  /// <requirements>
  /// 	<para>
  /// 
  /// 			<b>Target Platforms:</b> Windows XP Home Edition, Windows XP Professional, Windows Server 2003 family, Windows Vista, Windows Server 2008 family, Windows
  /// Seven
  /// 	</para>
  /// </requirements>
  public enum MouseButtonEvent
  {
    Down,
    Up,
    Click,
  }
}

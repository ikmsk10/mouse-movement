// Decompiled with JetBrains decompiler
// Type: ZennoLab.Emulation.MouseButton
// Assembly: ZennoLab.Emulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3402272C-5FE8-4A11-863F-404E8B64C3F1
// Assembly location: C:\Program Files (x86)\ZennoLab\ZennoPoster Demo\Progs\ZennoLab.Emulation.dll

namespace ZennoLab.Emulation
{
  /// <summary>Specifies the mouse button.</summary>
  /// <seealso cref="T:ZennoLab.Emulation.KeyboardEvent">KeyboardEvent Enumeration</seealso>
  /// <seealso cref="T:ZennoLab.Emulation.MouseButtonEvent">MouseButtonEvent Enumeration</seealso>
  /// <seealso cref="O:ZennoLab.Emulation.Emulator.MouseClick">MouseClick Method</seealso>
  /// <example>
  /// The following code example demonstrates uses of the <see cref="T:ZennoLab.Emulation.MouseButton">MouseButton</see> enumeration.
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
  /// }</code><code title="Example3" description="" lang="C#">
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
  /// return "Element not found";</code><code title="Example4" description="" lang="PHP">
  /// // find the html element
  /// $he = $instance-&gt;ActiveTab-&gt;FindElementByTag("input:submit", 0);
  /// // check the element
  /// if (!$he-&gt;IsVoid)
  /// {
  ///     // click
  ///     $result = Emulator::MouseClick($instance-&gt;ActiveTab-&gt;Handle,
  ///         MouseButton::Left, MouseButtonEvent::Click, $he-&gt;DisplacementInTabWindow-&gt;X + 10, $he-&gt;DisplacementInTabWindow-&gt;Y + 10);
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
  /// 			<b>Target Platforms:</b> Windows XP Home Edition, Windows XP Professional, Windows Server 2003 family, Windows Vista, Windows Server 2008 family, Windows
  /// Seven
  /// 	</para>
  /// </requirements>
  public enum MouseButton
  {
    Left,
    Right,
  }
}

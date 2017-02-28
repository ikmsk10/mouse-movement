// Decompiled with JetBrains decompiler
// Type: ZennoLab.Emulation.KeyboardEvent
// Assembly: ZennoLab.Emulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3402272C-5FE8-4A11-863F-404E8B64C3F1
// Assembly location: C:\Program Files (x86)\ZennoLab\ZennoPoster Demo\Progs\ZennoLab.Emulation.dll

namespace ZennoLab.Emulation
{
  /// <summary>Specifies the keyboard event.</summary>
  /// <seealso cref="T:ZennoLab.Emulation.MouseButton">MouseButton Enumeration</seealso>
  /// <seealso cref="T:ZennoLab.Emulation.MouseButtonEvent">MouseButtonEvent Enumeration</seealso>
  /// <seealso cref="O:ZennoLab.Emulation.Emulator.SendKey">SendKey Method</seealso>
  /// <example>
  /// The following code example demonstrates uses of the <see cref="T:ZennoLab.Emulation.KeyboardEvent">KeyboardEvent</see> enumeration.
  /// <code title="Example" description="" lang="C#">
  /// // send the key "a"
  /// string result = Emulator.SendKey("Window", 200, 200, System.Windows.Forms.Keys.A, KeyboardEvent.Press);
  /// // check result
  /// if (result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "Key was sent";
  /// else return "Fail";</code><code title="Example2" description="" lang="PHP">
  /// // send the key "a"
  /// $result = Emulator::SendKey("Window", 200, 200, System\Windows\Forms\Keys.A, KeyboardEvent::Press);
  /// // check result
  /// if ($result == "ok" &amp;&amp; !Emulator::ErrorDetected) return "Key was sent";
  /// else return "Fail";</code><code title="Example3" description="" lang="C#">
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
  /// return "Element not found";</code><code title="Example4" description="" lang="PHP">
  /// // find the html element
  /// $he = $instance-&gt;ActiveTab-&gt;FindElementByTag("input:text", 0);
  /// // check the element
  /// if (!$he-&gt;IsVoid)
  /// {
  ///     // send key
  ///     $result = Emulator::SendKey($instance-&gt;ActiveTab-&gt;Handle, System\Windows\Forms\Keys::Z, KeyboardEvent::Down);
  ///     // send result
  ///     if ($result == "ok" &amp;&amp; !Emulator.ErrorDetected) return "Key was sent";
  ///     return "Fail";
  /// }
  /// return "Element not found";</code></example>
  /// <requirements>
  /// 	<para>
  /// 
  /// 			<b>Target Platforms:</b> Windows XP Home Edition, Windows XP Professional, Windows Server 2003 family, Windows Vista, Windows Server 2008 family, Windows
  /// Seven
  /// 	</para>
  /// </requirements>
  public enum KeyboardEvent
  {
    Down,
    Up,
    Press,
  }
}

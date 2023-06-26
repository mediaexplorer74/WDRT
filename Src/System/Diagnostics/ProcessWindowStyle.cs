using System;

namespace System.Diagnostics
{
	/// <summary>Specified how a new window should appear when the system starts a process.</summary>
	// Token: 0x02000502 RID: 1282
	public enum ProcessWindowStyle
	{
		/// <summary>The normal, visible window style. The system displays a window with <see cref="F:System.Diagnostics.ProcessWindowStyle.Normal" /> style on the screen, in a default location. If a window is visible, the user can supply input to the window and view the window's output. Frequently, an application may initialize a new window to the <see cref="F:System.Diagnostics.ProcessWindowStyle.Hidden" /> style while it customizes the window's appearance, and then make the window style <see cref="F:System.Diagnostics.ProcessWindowStyle.Normal" />.</summary>
		// Token: 0x040028A1 RID: 10401
		Normal,
		/// <summary>The hidden window style. A window can be either visible or hidden. The system displays a hidden window by not drawing it. If a window is hidden, it is effectively disabled. A hidden window can process messages from the system or from other windows, but it cannot process input from the user or display output. Frequently, an application may keep a new window hidden while it customizes the window's appearance, and then make the window style <see cref="F:System.Diagnostics.ProcessWindowStyle.Normal" />. To use <see cref="F:System.Diagnostics.ProcessWindowStyle.Hidden" />, the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property must be <see langword="true" />.</summary>
		// Token: 0x040028A2 RID: 10402
		Hidden,
		/// <summary>The minimized window style. By default, the system reduces a minimized window to the size of its taskbar button and moves the minimized window to the taskbar.</summary>
		// Token: 0x040028A3 RID: 10403
		Minimized,
		/// <summary>The maximized window style. By default, the system enlarges a maximized window so that it fills the screen or, in the case of a child window, the parent window's client area. If the window has a title bar, the system automatically moves it to the top of the screen or to the top of the parent window's client area. Also, the system disables the window's sizing border and the window-positioning capability of the title bar so that the user cannot move the window by dragging the title bar.</summary>
		// Token: 0x040028A4 RID: 10404
		Maximized
	}
}

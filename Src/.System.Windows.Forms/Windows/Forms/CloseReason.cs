using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the reason that a form was closed.</summary>
	// Token: 0x0200014F RID: 335
	public enum CloseReason
	{
		/// <summary>The cause of the closure was not defined or could not be determined.</summary>
		// Token: 0x0400077A RID: 1914
		None,
		/// <summary>The operating system is closing all applications before shutting down.</summary>
		// Token: 0x0400077B RID: 1915
		WindowsShutDown,
		/// <summary>The parent form of this multiple document interface (MDI) form is closing.</summary>
		// Token: 0x0400077C RID: 1916
		MdiFormClosing,
		/// <summary>The user is closing the form through the user interface (UI), for example by clicking the Close button on the form window, selecting Close from the window's control menu, or pressing ALT+F4.</summary>
		// Token: 0x0400077D RID: 1917
		UserClosing,
		/// <summary>The Microsoft Windows Task Manager is closing the application.</summary>
		// Token: 0x0400077E RID: 1918
		TaskManagerClosing,
		/// <summary>The owner form is closing.</summary>
		// Token: 0x0400077F RID: 1919
		FormOwnerClosing,
		/// <summary>The <see cref="M:System.Windows.Forms.Application.Exit" /> method of the <see cref="T:System.Windows.Forms.Application" /> class was invoked.</summary>
		// Token: 0x04000780 RID: 1920
		ApplicationExitCall
	}
}

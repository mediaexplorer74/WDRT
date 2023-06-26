using System;

namespace System.Windows.Forms
{
	/// <summary>Defines the communication layer between a control and the Win32 API.</summary>
	// Token: 0x020002AF RID: 687
	public interface IWindowTarget
	{
		/// <summary>Sets the handle of the <see cref="T:System.Windows.Forms.IWindowTarget" /> to the specified handle.</summary>
		/// <param name="newHandle">The new handle of the <see cref="T:System.Windows.Forms.IWindowTarget" />.</param>
		// Token: 0x06002A56 RID: 10838
		void OnHandleChange(IntPtr newHandle);

		/// <summary>Processes the Windows messages.</summary>
		/// <param name="m">The Windows message to process.</param>
		// Token: 0x06002A57 RID: 10839
		void OnMessage(ref Message m);
	}
}

using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolTip.Popup" /> event.</summary>
	// Token: 0x0200031C RID: 796
	public class PopupEventArgs : CancelEventArgs
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.PopupEventArgs" /> class.</summary>
		/// <param name="associatedWindow">The <see cref="T:System.Windows.Forms.IWin32Window" /> that the ToolTip is bound to.</param>
		/// <param name="associatedControl">The <see cref="T:System.Windows.Forms.Control" /> that the ToolTip is being created for.</param>
		/// <param name="isBalloon">
		///   <see langword="true" /> to indicate that the associated ToolTip window has a balloon-style appearance; otherwise, <see langword="false" /> to indicate that the ToolTip window has a standard rectangular appearance.</param>
		/// <param name="size">The <see cref="T:System.Drawing.Size" /> of the ToolTip.</param>
		// Token: 0x060032F1 RID: 13041 RVA: 0x000E3856 File Offset: 0x000E1A56
		public PopupEventArgs(IWin32Window associatedWindow, Control associatedControl, bool isBalloon, Size size)
		{
			this.associatedWindow = associatedWindow;
			this.size = size;
			this.associatedControl = associatedControl;
			this.isBalloon = isBalloon;
		}

		/// <summary>Gets the window to which this <see cref="T:System.Windows.Forms.ToolTip" /> is bound.</summary>
		/// <returns>The window which owns the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x000E387B File Offset: 0x000E1A7B
		public IWin32Window AssociatedWindow
		{
			get
			{
				return this.associatedWindow;
			}
		}

		/// <summary>Gets the control for which the <see cref="T:System.Windows.Forms.ToolTip" /> is being drawn.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is associated with the <see cref="T:System.Windows.Forms.ToolTip" />, or <see langword="null" /> if the ToolTip is not associated with a control.</returns>
		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x000E3883 File Offset: 0x000E1A83
		public Control AssociatedControl
		{
			get
			{
				return this.associatedControl;
			}
		}

		/// <summary>Gets a value indicating whether the ToolTip is displayed as a standard rectangular or a balloon window.</summary>
		/// <returns>
		///   <see langword="true" /> if the ToolTip is displayed in a balloon window; otherwise, <see langword="false" /> if a standard rectangular window is used.</returns>
		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000E388B File Offset: 0x000E1A8B
		public bool IsBalloon
		{
			get
			{
				return this.isBalloon;
			}
		}

		/// <summary>Gets or sets the size of the ToolTip.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolTip" /> window.</returns>
		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000E3893 File Offset: 0x000E1A93
		// (set) Token: 0x060032F6 RID: 13046 RVA: 0x000E389B File Offset: 0x000E1A9B
		public Size ToolTipSize
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x04001E9B RID: 7835
		private IWin32Window associatedWindow;

		// Token: 0x04001E9C RID: 7836
		private Size size;

		// Token: 0x04001E9D RID: 7837
		private Control associatedControl;

		// Token: 0x04001E9E RID: 7838
		private bool isBalloon;
	}
}

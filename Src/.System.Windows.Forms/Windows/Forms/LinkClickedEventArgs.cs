using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.RichTextBox.LinkClicked" /> event.</summary>
	// Token: 0x020002C1 RID: 705
	[ComVisible(true)]
	public class LinkClickedEventArgs : EventArgs
	{
		/// <summary>Gets the text of the link being clicked.</summary>
		/// <returns>The text of the link that is clicked in the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</returns>
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x000C2225 File Offset: 0x000C0425
		public string LinkText
		{
			get
			{
				return this.linkText;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkClickedEventArgs" /> class.</summary>
		/// <param name="linkText">The text of the link that is clicked in the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</param>
		// Token: 0x06002B37 RID: 11063 RVA: 0x000C222D File Offset: 0x000C042D
		public LinkClickedEventArgs(string linkText)
		{
			this.linkText = linkText;
		}

		// Token: 0x04001228 RID: 4648
		private string linkText;
	}
}

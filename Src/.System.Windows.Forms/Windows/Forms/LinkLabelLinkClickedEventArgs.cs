using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.LinkLabel.LinkClicked" /> event.</summary>
	// Token: 0x020002C5 RID: 709
	[ComVisible(true)]
	public class LinkLabelLinkClickedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> class with the specified link.</summary>
		/// <param name="link">The <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked.</param>
		// Token: 0x06002BA6 RID: 11174 RVA: 0x000C4790 File Offset: 0x000C2990
		public LinkLabelLinkClickedEventArgs(LinkLabel.Link link)
		{
			this.link = link;
			this.button = MouseButtons.Left;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> class with the specified link and the specified mouse button.</summary>
		/// <param name="link">The <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked.</param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</param>
		// Token: 0x06002BA7 RID: 11175 RVA: 0x000C47AA File Offset: 0x000C29AA
		public LinkLabelLinkClickedEventArgs(LinkLabel.Link link, MouseButtons button)
			: this(link)
		{
			this.button = button;
		}

		/// <summary>Gets the mouse button that causes the link to be clicked.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</returns>
		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000C47BA File Offset: 0x000C29BA
		public MouseButtons Button
		{
			get
			{
				return this.button;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked.</summary>
		/// <returns>A link on the <see cref="T:System.Windows.Forms.LinkLabel" />.</returns>
		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x000C47C2 File Offset: 0x000C29C2
		public LinkLabel.Link Link
		{
			get
			{
				return this.link;
			}
		}

		// Token: 0x0400123C RID: 4668
		private readonly LinkLabel.Link link;

		// Token: 0x0400123D RID: 4669
		private readonly MouseButtons button;
	}
}

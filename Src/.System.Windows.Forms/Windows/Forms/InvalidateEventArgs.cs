using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event.</summary>
	// Token: 0x0200029F RID: 671
	public class InvalidateEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InvalidateEventArgs" /> class.</summary>
		/// <param name="invalidRect">The <see cref="T:System.Drawing.Rectangle" /> that contains the invalidated window area.</param>
		// Token: 0x06002A24 RID: 10788 RVA: 0x000BF8CF File Offset: 0x000BDACF
		public InvalidateEventArgs(Rectangle invalidRect)
		{
			this.invalidRect = invalidRect;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> that contains the invalidated window area.</summary>
		/// <returns>The invalidated window area.</returns>
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002A25 RID: 10789 RVA: 0x000BF8DE File Offset: 0x000BDADE
		public Rectangle InvalidRect
		{
			get
			{
				return this.invalidRect;
			}
		}

		// Token: 0x04001115 RID: 4373
		private readonly Rectangle invalidRect;
	}
}

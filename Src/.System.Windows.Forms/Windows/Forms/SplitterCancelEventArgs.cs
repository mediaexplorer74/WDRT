using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for splitter events.</summary>
	// Token: 0x0200036E RID: 878
	public class SplitterCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SplitterCancelEventArgs" /> class with the specified coordinates of the mouse pointer and the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <param name="mouseCursorX">The X coordinate of the mouse pointer in client coordinates.</param>
		/// <param name="mouseCursorY">The Y coordinate of the mouse pointer in client coordinates.</param>
		/// <param name="splitX">The X coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates.</param>
		/// <param name="splitY">The Y coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates.</param>
		// Token: 0x06003987 RID: 14727 RVA: 0x000FFB31 File Offset: 0x000FDD31
		public SplitterCancelEventArgs(int mouseCursorX, int mouseCursorY, int splitX, int splitY)
			: base(false)
		{
			this.mouseCursorX = mouseCursorX;
			this.mouseCursorY = mouseCursorY;
			this.splitX = splitX;
			this.splitY = splitY;
		}

		/// <summary>Gets the X coordinate of the mouse pointer in client coordinates.</summary>
		/// <returns>An integer representing the X coordinate of the mouse pointer in client coordinates.</returns>
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003988 RID: 14728 RVA: 0x000FFB57 File Offset: 0x000FDD57
		public int MouseCursorX
		{
			get
			{
				return this.mouseCursorX;
			}
		}

		/// <summary>Gets the Y coordinate of the mouse pointer in client coordinates.</summary>
		/// <returns>An integer representing the Y coordinate of the mouse pointer in client coordinates.</returns>
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003989 RID: 14729 RVA: 0x000FFB5F File Offset: 0x000FDD5F
		public int MouseCursorY
		{
			get
			{
				return this.mouseCursorY;
			}
		}

		/// <summary>Gets or sets the X coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates.</summary>
		/// <returns>An integer representing the X coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x0600398A RID: 14730 RVA: 0x000FFB67 File Offset: 0x000FDD67
		// (set) Token: 0x0600398B RID: 14731 RVA: 0x000FFB6F File Offset: 0x000FDD6F
		public int SplitX
		{
			get
			{
				return this.splitX;
			}
			set
			{
				this.splitX = value;
			}
		}

		/// <summary>Gets or sets the Y coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates.</summary>
		/// <returns>An integer representing the Y coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x0600398C RID: 14732 RVA: 0x000FFB78 File Offset: 0x000FDD78
		// (set) Token: 0x0600398D RID: 14733 RVA: 0x000FFB80 File Offset: 0x000FDD80
		public int SplitY
		{
			get
			{
				return this.splitY;
			}
			set
			{
				this.splitY = value;
			}
		}

		// Token: 0x040022C3 RID: 8899
		private readonly int mouseCursorX;

		// Token: 0x040022C4 RID: 8900
		private readonly int mouseCursorY;

		// Token: 0x040022C5 RID: 8901
		private int splitX;

		// Token: 0x040022C6 RID: 8902
		private int splitY;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for <see cref="E:System.Windows.Forms.Splitter.SplitterMoving" /> and the <see cref="E:System.Windows.Forms.Splitter.SplitterMoved" /> events.</summary>
	// Token: 0x02000370 RID: 880
	[ComVisible(true)]
	public class SplitterEventArgs : EventArgs
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.SplitterEventArgs" /> class with the specified coordinates of the mouse pointer and the coordinates of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> control.</summary>
		/// <param name="x">The x-coordinate of the mouse pointer (in client coordinates).</param>
		/// <param name="y">The y-coordinate of the mouse pointer (in client coordinates).</param>
		/// <param name="splitX">The x-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates).</param>
		/// <param name="splitY">The y-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates).</param>
		// Token: 0x06003992 RID: 14738 RVA: 0x000FFB89 File Offset: 0x000FDD89
		public SplitterEventArgs(int x, int y, int splitX, int splitY)
		{
			this.x = x;
			this.y = y;
			this.splitX = splitX;
			this.splitY = splitY;
		}

		/// <summary>Gets the x-coordinate of the mouse pointer (in client coordinates).</summary>
		/// <returns>The x-coordinate of the mouse pointer.</returns>
		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000FFBAE File Offset: 0x000FDDAE
		public int X
		{
			get
			{
				return this.x;
			}
		}

		/// <summary>Gets the y-coordinate of the mouse pointer (in client coordinates).</summary>
		/// <returns>The y-coordinate of the mouse pointer.</returns>
		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x000FFBB6 File Offset: 0x000FDDB6
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates).</summary>
		/// <returns>The x-coordinate of the upper-left corner of the control.</returns>
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06003995 RID: 14741 RVA: 0x000FFBBE File Offset: 0x000FDDBE
		// (set) Token: 0x06003996 RID: 14742 RVA: 0x000FFBC6 File Offset: 0x000FDDC6
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

		/// <summary>Gets or sets the y-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates).</summary>
		/// <returns>The y-coordinate of the upper-left corner of the control.</returns>
		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06003997 RID: 14743 RVA: 0x000FFBCF File Offset: 0x000FDDCF
		// (set) Token: 0x06003998 RID: 14744 RVA: 0x000FFBD7 File Offset: 0x000FDDD7
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

		// Token: 0x040022C7 RID: 8903
		private readonly int x;

		// Token: 0x040022C8 RID: 8904
		private readonly int y;

		// Token: 0x040022C9 RID: 8905
		private int splitX;

		// Token: 0x040022CA RID: 8906
		private int splitY;
	}
}

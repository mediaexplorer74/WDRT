using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderArrow" /> event.</summary>
	// Token: 0x020003B2 RID: 946
	public class ToolStripArrowRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> class.</summary>
		/// <param name="g">The graphics used to paint the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
		/// <param name="toolStripItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to paint the arrow.</param>
		/// <param name="arrowRectangle">The bounding area of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
		/// <param name="arrowColor">The color of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
		/// <param name="arrowDirection">The direction in which the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow points.</param>
		// Token: 0x06003EDB RID: 16091 RVA: 0x00110CE8 File Offset: 0x0010EEE8
		public ToolStripArrowRenderEventArgs(Graphics g, ToolStripItem toolStripItem, Rectangle arrowRectangle, Color arrowColor, ArrowDirection arrowDirection)
		{
			this.item = toolStripItem;
			this.graphics = g;
			this.arrowRect = arrowRectangle;
			this.defaultArrowColor = arrowColor;
			this.arrowDirection = arrowDirection;
		}

		/// <summary>Gets or sets the bounding area of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding area.</returns>
		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003EDC RID: 16092 RVA: 0x00110D49 File Offset: 0x0010EF49
		// (set) Token: 0x06003EDD RID: 16093 RVA: 0x00110D51 File Offset: 0x0010EF51
		public Rectangle ArrowRectangle
		{
			get
			{
				return this.arrowRect;
			}
			set
			{
				this.arrowRect = value;
			}
		}

		/// <summary>Gets or sets the color of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the arrow.</returns>
		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06003EDE RID: 16094 RVA: 0x00110D5A File Offset: 0x0010EF5A
		// (set) Token: 0x06003EDF RID: 16095 RVA: 0x00110D71 File Offset: 0x0010EF71
		public Color ArrowColor
		{
			get
			{
				if (this.arrowColorChanged)
				{
					return this.arrowColor;
				}
				return this.DefaultArrowColor;
			}
			set
			{
				this.arrowColor = value;
				this.arrowColorChanged = true;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x00110D81 File Offset: 0x0010EF81
		// (set) Token: 0x06003EE1 RID: 16097 RVA: 0x00110D89 File Offset: 0x0010EF89
		internal Color DefaultArrowColor
		{
			get
			{
				return this.defaultArrowColor;
			}
			set
			{
				this.defaultArrowColor = value;
			}
		}

		/// <summary>Gets or sets the direction in which the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow points.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ArrowDirection" /> values.</returns>
		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x00110D92 File Offset: 0x0010EF92
		// (set) Token: 0x06003EE3 RID: 16099 RVA: 0x00110D9A File Offset: 0x0010EF9A
		public ArrowDirection Direction
		{
			get
			{
				return this.arrowDirection;
			}
			set
			{
				this.arrowDirection = value;
			}
		}

		/// <summary>Gets the graphics used to paint the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint.</returns>
		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x00110DA3 File Offset: 0x0010EFA3
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to paint the arrow.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to paint the arrow.</returns>
		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x00110DAB File Offset: 0x0010EFAB
		public ToolStripItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x0400248A RID: 9354
		private Graphics graphics;

		// Token: 0x0400248B RID: 9355
		private Rectangle arrowRect = Rectangle.Empty;

		// Token: 0x0400248C RID: 9356
		private Color arrowColor = Color.Empty;

		// Token: 0x0400248D RID: 9357
		private Color defaultArrowColor = Color.Empty;

		// Token: 0x0400248E RID: 9358
		private ArrowDirection arrowDirection = ArrowDirection.Down;

		// Token: 0x0400248F RID: 9359
		private ToolStripItem item;

		// Token: 0x04002490 RID: 9360
		private bool arrowColorChanged;
	}
}

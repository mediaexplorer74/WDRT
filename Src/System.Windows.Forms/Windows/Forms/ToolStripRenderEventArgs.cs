using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderImageMargin(System.Windows.Forms.ToolStripRenderEventArgs)" />, <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderToolStripBorder(System.Windows.Forms.ToolStripRenderEventArgs)" />, and <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderToolStripBackground(System.Windows.Forms.ToolStripRenderEventArgs)" /> methods.</summary>
	// Token: 0x020003F9 RID: 1017
	public class ToolStripRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStrip" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to use for painting.</param>
		/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to paint.</param>
		// Token: 0x06004668 RID: 18024 RVA: 0x00128484 File Offset: 0x00126684
		public ToolStripRenderEventArgs(Graphics g, ToolStrip toolStrip)
		{
			this.toolStrip = toolStrip;
			this.graphics = g;
			this.affectedBounds = new Rectangle(Point.Empty, toolStrip.Size);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStrip" />, using the specified <see cref="T:System.Drawing.Graphics" /> to paint the specified bounds with the specified <see cref="T:System.Drawing.Color" />.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to use for painting.</param>
		/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to paint.</param>
		/// <param name="affectedBounds">The <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> that the background of the <see cref="T:System.Windows.Forms.ToolStrip" /> is painted with.</param>
		// Token: 0x06004669 RID: 18025 RVA: 0x001284D1 File Offset: 0x001266D1
		public ToolStripRenderEventArgs(Graphics g, ToolStrip toolStrip, Rectangle affectedBounds, Color backColor)
		{
			this.toolStrip = toolStrip;
			this.affectedBounds = affectedBounds;
			this.graphics = g;
			this.backColor = backColor;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted.</returns>
		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x0600466A RID: 18026 RVA: 0x0012850C File Offset: 0x0012670C
		public Rectangle AffectedBounds
		{
			get
			{
				return this.affectedBounds;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Color" /> that the background of the <see cref="T:System.Windows.Forms.ToolStrip" /> is painted with.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that the background of the <see cref="T:System.Windows.Forms.ToolStrip" /> is painted with.</returns>
		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x0600466B RID: 18027 RVA: 0x00128514 File Offset: 0x00126714
		public Color BackColor
		{
			get
			{
				if (this.backColor == Color.Empty)
				{
					this.backColor = this.toolStrip.RawBackColor;
					if (this.backColor == Color.Empty)
					{
						if (this.toolStrip is ToolStripDropDown)
						{
							this.backColor = SystemColors.Menu;
						}
						else if (this.toolStrip is MenuStrip)
						{
							this.backColor = SystemColors.MenuBar;
						}
						else
						{
							this.backColor = SystemColors.Control;
						}
					}
				}
				return this.backColor;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint.</returns>
		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x0600466C RID: 18028 RVA: 0x0012859B File Offset: 0x0012679B
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStrip" /> to be painted.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> to be painted.</returns>
		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x0600466D RID: 18029 RVA: 0x001285A3 File Offset: 0x001267A3
		public ToolStrip ToolStrip
		{
			get
			{
				return this.toolStrip;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> representing the overlap area between a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and its <see cref="P:System.Windows.Forms.ToolStripDropDown.OwnerItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> representing the overlap area between a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and its <see cref="P:System.Windows.Forms.ToolStripDropDown.OwnerItem" />.</returns>
		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x0600466E RID: 18030 RVA: 0x001285AC File Offset: 0x001267AC
		public Rectangle ConnectedArea
		{
			get
			{
				ToolStripDropDown toolStripDropDown = this.toolStrip as ToolStripDropDown;
				if (toolStripDropDown != null)
				{
					ToolStripDropDownItem toolStripDropDownItem = toolStripDropDown.OwnerItem as ToolStripDropDownItem;
					if (toolStripDropDownItem is MdiControlStrip.SystemMenuItem)
					{
						return Rectangle.Empty;
					}
					if (toolStripDropDownItem != null && toolStripDropDownItem.ParentInternal != null && !toolStripDropDownItem.IsOnDropDown)
					{
						Rectangle rectangle = new Rectangle(this.toolStrip.PointToClient(toolStripDropDownItem.TranslatePoint(Point.Empty, ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords)), toolStripDropDownItem.Size);
						Rectangle bounds = this.ToolStrip.Bounds;
						Rectangle clientRectangle = this.ToolStrip.ClientRectangle;
						clientRectangle.Inflate(1, 1);
						if (clientRectangle.IntersectsWith(rectangle))
						{
							switch (toolStripDropDownItem.DropDownDirection)
							{
							case ToolStripDropDownDirection.AboveLeft:
							case ToolStripDropDownDirection.AboveRight:
								return Rectangle.Empty;
							case ToolStripDropDownDirection.BelowLeft:
							case ToolStripDropDownDirection.BelowRight:
								clientRectangle.Intersect(rectangle);
								if (clientRectangle.Height == 2)
								{
									return new Rectangle(rectangle.X + 1, 0, rectangle.Width - 2, 2);
								}
								return Rectangle.Empty;
							case ToolStripDropDownDirection.Left:
							case ToolStripDropDownDirection.Right:
								return Rectangle.Empty;
							}
						}
					}
				}
				return Rectangle.Empty;
			}
		}

		// Token: 0x0400269D RID: 9885
		private ToolStrip toolStrip;

		// Token: 0x0400269E RID: 9886
		private Graphics graphics;

		// Token: 0x0400269F RID: 9887
		private Rectangle affectedBounds = Rectangle.Empty;

		// Token: 0x040026A0 RID: 9888
		private Color backColor = Color.Empty;
	}
}

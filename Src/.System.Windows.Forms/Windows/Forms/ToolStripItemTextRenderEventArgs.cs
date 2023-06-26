using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemText" /> event.</summary>
	// Token: 0x020003DC RID: 988
	public class ToolStripItemTextRenderEventArgs : ToolStripItemRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> class with the specified text and text properties format.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to draw the text.</param>
		/// <param name="text">The text to be drawn.</param>
		/// <param name="textRectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds to draw the text in.</param>
		/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> used to draw the text.</param>
		/// <param name="textFont">The <see cref="T:System.Drawing.Font" /> used to draw the text.</param>
		/// <param name="format">The display and layout information for text strings.</param>
		// Token: 0x06004350 RID: 17232 RVA: 0x0011CEA8 File Offset: 0x0011B0A8
		public ToolStripItemTextRenderEventArgs(Graphics g, ToolStripItem item, string text, Rectangle textRectangle, Color textColor, Font textFont, TextFormatFlags format)
			: base(g, item)
		{
			this.text = text;
			this.textRectangle = textRectangle;
			this.defaultTextColor = textColor;
			this.textFont = textFont;
			this.textAlignment = item.TextAlign;
			this.textFormat = format;
			this.textDirection = item.TextDirection;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> class with the specified text and text properties.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to draw the text.</param>
		/// <param name="text">The text to be drawn.</param>
		/// <param name="textRectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds to draw the text in.</param>
		/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> used to draw the text.</param>
		/// <param name="textFont">The <see cref="T:System.Drawing.Font" /> used to draw the text.</param>
		/// <param name="textAlign">The <see cref="T:System.Drawing.ContentAlignment" /> that specifies the vertical and horizontal alignment of the text in the bounding area.</param>
		// Token: 0x06004351 RID: 17233 RVA: 0x0011CF24 File Offset: 0x0011B124
		public ToolStripItemTextRenderEventArgs(Graphics g, ToolStripItem item, string text, Rectangle textRectangle, Color textColor, Font textFont, ContentAlignment textAlign)
			: base(g, item)
		{
			this.text = text;
			this.textRectangle = textRectangle;
			this.defaultTextColor = textColor;
			this.textFont = textFont;
			this.textFormat = ToolStripItemInternalLayout.ContentAlignToTextFormat(textAlign, item.RightToLeft == RightToLeft.Yes);
			this.textFormat = (item.ShowKeyboardCues ? this.textFormat : (this.textFormat | TextFormatFlags.HidePrefix));
			this.textDirection = item.TextDirection;
		}

		/// <summary>Gets or sets the text to be drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A string that represents the text to be painted on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06004352 RID: 17234 RVA: 0x0011CFC4 File Offset: 0x0011B1C4
		// (set) Token: 0x06004353 RID: 17235 RVA: 0x0011CFCC File Offset: 0x0011B1CC
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		/// <summary>Gets or sets the color of the <see cref="T:System.Windows.Forms.ToolStripItem" /> text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the <see cref="T:System.Windows.Forms.ToolStripItem" /> text.</returns>
		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06004354 RID: 17236 RVA: 0x0011CFD5 File Offset: 0x0011B1D5
		// (set) Token: 0x06004355 RID: 17237 RVA: 0x0011CFEC File Offset: 0x0011B1EC
		public Color TextColor
		{
			get
			{
				if (this.textColorChanged)
				{
					return this.textColor;
				}
				return this.DefaultTextColor;
			}
			set
			{
				this.textColor = value;
				this.textColorChanged = true;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06004356 RID: 17238 RVA: 0x0011CFFC File Offset: 0x0011B1FC
		// (set) Token: 0x06004357 RID: 17239 RVA: 0x0011D004 File Offset: 0x0011B204
		internal Color DefaultTextColor
		{
			get
			{
				return this.defaultTextColor;
			}
			set
			{
				this.defaultTextColor = value;
			}
		}

		/// <summary>Gets or sets the font of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x0011D00D File Offset: 0x0011B20D
		// (set) Token: 0x06004359 RID: 17241 RVA: 0x0011D015 File Offset: 0x0011B215
		public Font TextFont
		{
			get
			{
				return this.textFont;
			}
			set
			{
				this.textFont = value;
			}
		}

		/// <summary>Gets or sets the rectangle that represents the bounds to draw the text in.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds to draw the text in.</returns>
		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x0600435A RID: 17242 RVA: 0x0011D01E File Offset: 0x0011B21E
		// (set) Token: 0x0600435B RID: 17243 RVA: 0x0011D026 File Offset: 0x0011B226
		public Rectangle TextRectangle
		{
			get
			{
				return this.textRectangle;
			}
			set
			{
				this.textRectangle = value;
			}
		}

		/// <summary>Gets or sets the display and layout information of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values that specify the display and layout information of the drawn text.</returns>
		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x0600435C RID: 17244 RVA: 0x0011D02F File Offset: 0x0011B22F
		// (set) Token: 0x0600435D RID: 17245 RVA: 0x0011D037 File Offset: 0x0011B237
		public TextFormatFlags TextFormat
		{
			get
			{
				return this.textFormat;
			}
			set
			{
				this.textFormat = value;
			}
		}

		/// <summary>Gets or sets whether the text is drawn vertically or horizontally.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values.</returns>
		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x0600435E RID: 17246 RVA: 0x0011D040 File Offset: 0x0011B240
		// (set) Token: 0x0600435F RID: 17247 RVA: 0x0011D048 File Offset: 0x0011B248
		public ToolStripTextDirection TextDirection
		{
			get
			{
				return this.textDirection;
			}
			set
			{
				this.textDirection = value;
			}
		}

		// Token: 0x040025C0 RID: 9664
		private string text;

		// Token: 0x040025C1 RID: 9665
		private Rectangle textRectangle = Rectangle.Empty;

		// Token: 0x040025C2 RID: 9666
		private Color textColor = SystemColors.ControlText;

		// Token: 0x040025C3 RID: 9667
		private Font textFont;

		// Token: 0x040025C4 RID: 9668
		private ContentAlignment textAlignment;

		// Token: 0x040025C5 RID: 9669
		private ToolStripTextDirection textDirection = ToolStripTextDirection.Horizontal;

		// Token: 0x040025C6 RID: 9670
		private TextFormatFlags textFormat;

		// Token: 0x040025C7 RID: 9671
		private Color defaultTextColor = SystemColors.ControlText;

		// Token: 0x040025C8 RID: 9672
		private bool textColorChanged;
	}
}

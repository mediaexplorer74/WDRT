﻿using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolTip.Draw" /> event.</summary>
	// Token: 0x02000242 RID: 578
	public class DrawToolTipEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawToolTipEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> context used to draw the ToolTip.</param>
		/// <param name="associatedWindow">The <see cref="T:System.Windows.Forms.IWin32Window" /> that the ToolTip is bound to.</param>
		/// <param name="associatedControl">The <see cref="T:System.Windows.Forms.Control" /> that the ToolTip is being created for.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that outlines the area where the ToolTip is to be displayed.</param>
		/// <param name="toolTipText">A <see cref="T:System.String" /> containing the text for the ToolTip.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the ToolTip background.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> of the ToolTip text.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> used to draw the ToolTip text.</param>
		// Token: 0x060024EE RID: 9454 RVA: 0x000AD194 File Offset: 0x000AB394
		public DrawToolTipEventArgs(Graphics graphics, IWin32Window associatedWindow, Control associatedControl, Rectangle bounds, string toolTipText, Color backColor, Color foreColor, Font font)
		{
			this.graphics = graphics;
			this.associatedWindow = associatedWindow;
			this.associatedControl = associatedControl;
			this.bounds = bounds;
			this.toolTipText = toolTipText;
			this.backColor = backColor;
			this.foreColor = foreColor;
			this.font = font;
		}

		/// <summary>Gets the graphics surface used to draw the <see cref="T:System.Windows.Forms.ToolTip" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> on which to draw the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x000AD1E4 File Offset: 0x000AB3E4
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the window to which this <see cref="T:System.Windows.Forms.ToolTip" /> is bound.</summary>
		/// <returns>The window which owns the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x000AD1EC File Offset: 0x000AB3EC
		public IWin32Window AssociatedWindow
		{
			get
			{
				return this.associatedWindow;
			}
		}

		/// <summary>Gets the control for which the <see cref="T:System.Windows.Forms.ToolTip" /> is being drawn.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is associated with the <see cref="T:System.Windows.Forms.ToolTip" /> when the <see cref="E:System.Windows.Forms.ToolTip.Draw" /> event occurs. The return value will be <see langword="null" /> if the ToolTip is not associated with a control.</returns>
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x000AD1F4 File Offset: 0x000AB3F4
		public Control AssociatedControl
		{
			get
			{
				return this.associatedControl;
			}
		}

		/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ToolTip" /> to draw.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.ToolTip" /> to draw.</returns>
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x000AD1FC File Offset: 0x000AB3FC
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the text for the <see cref="T:System.Windows.Forms.ToolTip" /> that is being drawn.</summary>
		/// <returns>The text that is associated with the <see cref="T:System.Windows.Forms.ToolTip" /> when the <see cref="E:System.Windows.Forms.ToolTip.Draw" /> event occurs.</returns>
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x000AD204 File Offset: 0x000AB404
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
		}

		/// <summary>Gets the font used to draw the <see cref="T:System.Windows.Forms.ToolTip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> object.</returns>
		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x000AD20C File Offset: 0x000AB40C
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system background color.</summary>
		// Token: 0x060024F5 RID: 9461 RVA: 0x000AD214 File Offset: 0x000AB414
		public void DrawBackground()
		{
			Brush brush = new SolidBrush(this.backColor);
			this.Graphics.FillRectangle(brush, this.bounds);
			brush.Dispose();
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system text color and font.</summary>
		// Token: 0x060024F6 RID: 9462 RVA: 0x000AD245 File Offset: 0x000AB445
		public void DrawText()
		{
			this.DrawText(TextFormatFlags.HidePrefix | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system text color and font, and the specified text layout.</summary>
		/// <param name="flags">A <see cref="T:System.Windows.Forms.TextFormatFlags" /> containing a bitwise combination of values that specifies the display and layout for the <see cref="P:System.Windows.Forms.DrawToolTipEventArgs.ToolTipText" />.</param>
		// Token: 0x060024F7 RID: 9463 RVA: 0x000AD252 File Offset: 0x000AB452
		public void DrawText(TextFormatFlags flags)
		{
			TextRenderer.DrawText(this.graphics, this.toolTipText, this.font, this.bounds, this.foreColor, flags);
		}

		/// <summary>Draws the border of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system border color.</summary>
		// Token: 0x060024F8 RID: 9464 RVA: 0x000AD278 File Offset: 0x000AB478
		public void DrawBorder()
		{
			ControlPaint.DrawBorder(this.graphics, this.bounds, SystemColors.WindowFrame, ButtonBorderStyle.Solid);
		}

		// Token: 0x04000F4A RID: 3914
		private readonly Graphics graphics;

		// Token: 0x04000F4B RID: 3915
		private readonly IWin32Window associatedWindow;

		// Token: 0x04000F4C RID: 3916
		private readonly Control associatedControl;

		// Token: 0x04000F4D RID: 3917
		private readonly Rectangle bounds;

		// Token: 0x04000F4E RID: 3918
		private readonly string toolTipText;

		// Token: 0x04000F4F RID: 3919
		private readonly Color backColor;

		// Token: 0x04000F50 RID: 3920
		private readonly Color foreColor;

		// Token: 0x04000F51 RID: 3921
		private readonly Font font;
	}
}

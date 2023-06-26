using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a text box control with visual styles. This class cannot be inherited.</summary>
	// Token: 0x020003A1 RID: 929
	public sealed class TextBoxRenderer
	{
		// Token: 0x06003CDF RID: 15583 RVA: 0x00002843 File Offset: 0x00000A43
		private TextBoxRenderer()
		{
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.TextBoxRenderer" /> class can be used to draw a text box with visual styles.</summary>
		/// <returns>
		///   <see langword="true" /> if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x0002BBA7 File Offset: 0x00029DA7
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x001082B0 File Offset: 0x001064B0
		private static void DrawBackground(Graphics g, Rectangle bounds, TextBoxState state)
		{
			TextBoxRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			if (state != TextBoxState.Disabled)
			{
				Color color = TextBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.FillColor);
				if (color != SystemColors.Window)
				{
					Rectangle backgroundContentRectangle = TextBoxRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
					using (SolidBrush solidBrush = new SolidBrush(SystemColors.Window))
					{
						g.FillRectangle(solidBrush, backgroundContentRectangle);
					}
				}
			}
		}

		/// <summary>Draws a text box control in the specified state and bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CE2 RID: 15586 RVA: 0x00108328 File Offset: 0x00106528
		public static void DrawTextBox(Graphics g, Rectangle bounds, TextBoxState state)
		{
			TextBoxRenderer.InitializeRenderer((int)state);
			TextBoxRenderer.DrawBackground(g, bounds, state);
		}

		/// <summary>Draws a text box control in the specified state and bounds, and with the specified text.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CE3 RID: 15587 RVA: 0x00108338 File Offset: 0x00106538
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, TextBoxState state)
		{
			TextBoxRenderer.DrawTextBox(g, bounds, textBoxText, font, TextFormatFlags.TextBoxControl, state);
		}

		/// <summary>Draws a text box control in the specified state and bounds, and with the specified text and text bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="textBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CE4 RID: 15588 RVA: 0x0010834A File Offset: 0x0010654A
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, Rectangle textBounds, TextBoxState state)
		{
			TextBoxRenderer.DrawTextBox(g, bounds, textBoxText, font, textBounds, TextFormatFlags.TextBoxControl, state);
		}

		/// <summary>Draws a text box control in the specified state and bounds, and with the specified text and text formatting.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CE5 RID: 15589 RVA: 0x00108360 File Offset: 0x00106560
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, TextFormatFlags flags, TextBoxState state)
		{
			TextBoxRenderer.InitializeRenderer((int)state);
			Rectangle backgroundContentRectangle = TextBoxRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
			backgroundContentRectangle.Inflate(-2, -2);
			TextBoxRenderer.DrawTextBox(g, bounds, textBoxText, font, backgroundContentRectangle, flags, state);
		}

		/// <summary>Draws a text box control in the specified state and bounds, and with the specified text, text bounds, and text formatting.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="textBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CE6 RID: 15590 RVA: 0x0010839C File Offset: 0x0010659C
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, Rectangle textBounds, TextFormatFlags flags, TextBoxState state)
		{
			TextBoxRenderer.InitializeRenderer((int)state);
			TextBoxRenderer.DrawBackground(g, bounds, state);
			Color color = TextBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			TextRenderer.DrawText(g, textBoxText, font, textBounds, color, flags);
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x001083D8 File Offset: 0x001065D8
		private static void InitializeRenderer(int state)
		{
			if (TextBoxRenderer.visualStyleRenderer == null)
			{
				TextBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(TextBoxRenderer.TextBoxElement.ClassName, TextBoxRenderer.TextBoxElement.Part, state);
				return;
			}
			TextBoxRenderer.visualStyleRenderer.SetParameters(TextBoxRenderer.TextBoxElement.ClassName, TextBoxRenderer.TextBoxElement.Part, state);
		}

		// Token: 0x040023AC RID: 9132
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x040023AD RID: 9133
		private static readonly VisualStyleElement TextBoxElement = VisualStyleElement.TextBox.TextEdit.Normal;
	}
}

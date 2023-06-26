using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a combo box control with visual styles. This class cannot be inherited.</summary>
	// Token: 0x0200015F RID: 351
	public sealed class ComboBoxRenderer
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00002843 File Offset: 0x00000A43
		private ComboBoxRenderer()
		{
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ComboBoxRenderer" /> class can be used to draw a combo box with visual styles.</summary>
		/// <returns>
		///   <see langword="true" /> if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0002BBA7 File Offset: 0x00029DA7
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002BBB0 File Offset: 0x00029DB0
		private static void DrawBackground(Graphics g, Rectangle bounds, ComboBoxState state)
		{
			ComboBoxRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			if (state != ComboBoxState.Disabled)
			{
				Color color = ComboBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.FillColor);
				if (color != SystemColors.Window)
				{
					Rectangle backgroundContentRectangle = ComboBoxRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
					backgroundContentRectangle.Inflate(-2, -2);
					g.FillRectangle(SystemBrushes.Window, backgroundContentRectangle);
				}
			}
		}

		/// <summary>Draws a text box in the specified state and bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002BC10 File Offset: 0x00029E10
		public static void DrawTextBox(Graphics g, Rectangle bounds, ComboBoxState state)
		{
			if (ComboBoxRenderer.visualStyleRenderer == null)
			{
				ComboBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			else
			{
				ComboBoxRenderer.visualStyleRenderer.SetParameters(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			ComboBoxRenderer.DrawBackground(g, bounds, state);
		}

		/// <summary>Draws a text box in the specified state and bounds, with the specified text.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002BC6C File Offset: 0x00029E6C
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, ComboBoxState state)
		{
			ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, TextFormatFlags.TextBoxControl, state);
		}

		/// <summary>Draws a text box in the specified state and bounds, with the specified text and text bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds in which to draw <paramref name="comboBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06000EA7 RID: 3751 RVA: 0x0002BC7E File Offset: 0x00029E7E
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, Rectangle textBounds, ComboBoxState state)
		{
			ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, textBounds, TextFormatFlags.TextBoxControl, state);
		}

		/// <summary>Draws a text box in the specified state and bounds, with the specified text and text formatting.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002BC94 File Offset: 0x00029E94
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, TextFormatFlags flags, ComboBoxState state)
		{
			if (ComboBoxRenderer.visualStyleRenderer == null)
			{
				ComboBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			else
			{
				ComboBoxRenderer.visualStyleRenderer.SetParameters(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			Rectangle backgroundContentRectangle = ComboBoxRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
			backgroundContentRectangle.Inflate(-2, -2);
			ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, backgroundContentRectangle, flags, state);
		}

		/// <summary>Draws a text box in the specified state and bounds, with the specified text, text formatting, and text bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds in which to draw <paramref name="comboBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002BD10 File Offset: 0x00029F10
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, Rectangle textBounds, TextFormatFlags flags, ComboBoxState state)
		{
			if (ComboBoxRenderer.visualStyleRenderer == null)
			{
				ComboBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			else
			{
				ComboBoxRenderer.visualStyleRenderer.SetParameters(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			ComboBoxRenderer.DrawBackground(g, bounds, state);
			Color color = ComboBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			TextRenderer.DrawText(g, comboBoxText, font, textBounds, color, flags);
		}

		/// <summary>Draws a drop-down arrow with the current visual style of the operating system.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the drop-down arrow.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the drop-down arrow.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the drop-down arrow.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06000EAA RID: 3754 RVA: 0x0002BD8C File Offset: 0x00029F8C
		public static void DrawDropDownButton(Graphics g, Rectangle bounds, ComboBoxState state)
		{
			ComboBoxRenderer.DrawDropDownButtonForHandle(g, bounds, state, IntPtr.Zero);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0002BD9C File Offset: 0x00029F9C
		internal static void DrawDropDownButtonForHandle(Graphics g, Rectangle bounds, ComboBoxState state, IntPtr handle)
		{
			if (ComboBoxRenderer.visualStyleRenderer == null)
			{
				ComboBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(ComboBoxRenderer.ComboBoxElement.ClassName, ComboBoxRenderer.ComboBoxElement.Part, (int)state);
			}
			else
			{
				ComboBoxRenderer.visualStyleRenderer.SetParameters(ComboBoxRenderer.ComboBoxElement.ClassName, ComboBoxRenderer.ComboBoxElement.Part, (int)state);
			}
			ComboBoxRenderer.visualStyleRenderer.DrawBackground(g, bounds, handle);
		}

		// Token: 0x040007DD RID: 2013
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x040007DE RID: 2014
		private static readonly VisualStyleElement ComboBoxElement = VisualStyleElement.ComboBox.DropDownButton.Normal;

		// Token: 0x040007DF RID: 2015
		private static readonly VisualStyleElement TextBoxElement = VisualStyleElement.TextBox.TextEdit.Normal;
	}
}

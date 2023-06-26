using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render an option button control (also known as a radio button) with or without visual styles. This class cannot be inherited.</summary>
	// Token: 0x0200033D RID: 829
	public sealed class RadioButtonRenderer
	{
		// Token: 0x060035AE RID: 13742 RVA: 0x00002843 File Offset: 0x00000A43
		private RadioButtonRenderer()
		{
		}

		/// <summary>Gets or sets a value indicating whether the renderer uses the application state to determine rendering style.</summary>
		/// <returns>
		///   <see langword="true" /> if the application state is used to determine rendering style; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060035AF RID: 13743 RVA: 0x000F2DD1 File Offset: 0x000F0FD1
		// (set) Token: 0x060035B0 RID: 13744 RVA: 0x000F2DD8 File Offset: 0x000F0FD8
		public static bool RenderMatchingApplicationState
		{
			get
			{
				return RadioButtonRenderer.renderMatchingApplicationState;
			}
			set
			{
				RadioButtonRenderer.renderMatchingApplicationState = value;
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060035B1 RID: 13745 RVA: 0x000F2DE0 File Offset: 0x000F0FE0
		private static bool RenderWithVisualStyles
		{
			get
			{
				return !RadioButtonRenderer.renderMatchingApplicationState || Application.RenderWithVisualStyles;
			}
		}

		/// <summary>Indicates whether the background of the option button (also known as a radio button) has semitransparent or alpha-blended pieces.</summary>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		/// <returns>
		///   <see langword="true" /> if the background of the option button has semitransparent or alpha-blended pieces; otherwise, <see langword="false" />.</returns>
		// Token: 0x060035B2 RID: 13746 RVA: 0x000F2DF0 File Offset: 0x000F0FF0
		public static bool IsBackgroundPartiallyTransparent(RadioButtonState state)
		{
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer((int)state);
				return RadioButtonRenderer.visualStyleRenderer.IsBackgroundPartiallyTransparent();
			}
			return false;
		}

		/// <summary>Draws the background of a control's parent in the specified area.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the background of the parent of <paramref name="childControl" />.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which to draw the parent control's background. This rectangle should be inside the child control's bounds.</param>
		/// <param name="childControl">The control whose parent's background will be drawn.</param>
		// Token: 0x060035B3 RID: 13747 RVA: 0x000F2E0B File Offset: 0x000F100B
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer(0);
				RadioButtonRenderer.visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
			}
		}

		/// <summary>Draws an option button control (also known as a radio button) in the specified state and location.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		// Token: 0x060035B4 RID: 13748 RVA: 0x000F2E27 File Offset: 0x000F1027
		public static void DrawRadioButton(Graphics g, Point glyphLocation, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, state, IntPtr.Zero);
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000F2E38 File Offset: 0x000F1038
		internal static void DrawRadioButton(Graphics g, Point glyphLocation, RadioButtonState state, IntPtr hWnd)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, RadioButtonRenderer.GetGlyphSize(g, state, hWnd));
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer((int)state);
				RadioButtonRenderer.visualStyleRenderer.DrawBackground(g, rectangle, hWnd);
				return;
			}
			ControlPaint.DrawRadioButton(g, rectangle, RadioButtonRenderer.ConvertToButtonState(state));
		}

		/// <summary>Draws an option button control (also known as a radio button) in the specified state and location, with the specified text, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
		/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		// Token: 0x060035B6 RID: 13750 RVA: 0x000F2E7D File Offset: 0x000F107D
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, bool focused, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, focused, state);
		}

		/// <summary>Draws an option button control (also known as a radio button) in the specified state and location, with the specified text and text formatting, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
		/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		// Token: 0x060035B7 RID: 13751 RVA: 0x000F2E90 File Offset: 0x000F1090
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, bool focused, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, flags, focused, state, IntPtr.Zero);
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000F2EB4 File Offset: 0x000F10B4
		internal static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, bool focused, RadioButtonState state, IntPtr hWnd)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, RadioButtonRenderer.GetGlyphSize(g, state, hWnd));
			Color color;
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer((int)state);
				RadioButtonRenderer.visualStyleRenderer.DrawBackground(g, rectangle);
				color = RadioButtonRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				ControlPaint.DrawRadioButton(g, rectangle, RadioButtonRenderer.ConvertToButtonState(state));
				color = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, radioButtonText, font, textBounds, color, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, textBounds);
			}
		}

		/// <summary>Draws an option button control (also known as a radio button) in the specified state and location, with the specified text and image, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
		/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw with the option button.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="image" /> in.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		// Token: 0x060035B9 RID: 13753 RVA: 0x000F2F2C File Offset: 0x000F112C
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, Image image, Rectangle imageBounds, bool focused, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, image, imageBounds, focused, state);
		}

		/// <summary>Draws an option button control (also known as a radio button) in the specified state and location; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
		/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw with the option button.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="image" /> in.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		// Token: 0x060035BA RID: 13754 RVA: 0x000F2F50 File Offset: 0x000F1150
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, flags, image, imageBounds, focused, state, IntPtr.Zero);
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000F2F78 File Offset: 0x000F1178
		internal static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, RadioButtonState state, IntPtr hWnd)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, RadioButtonRenderer.GetGlyphSize(g, state, hWnd));
			Color color;
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer((int)state);
				RadioButtonRenderer.visualStyleRenderer.DrawImage(g, imageBounds, image);
				RadioButtonRenderer.visualStyleRenderer.DrawBackground(g, rectangle);
				color = RadioButtonRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				g.DrawImage(image, imageBounds);
				ControlPaint.DrawRadioButton(g, rectangle, RadioButtonRenderer.ConvertToButtonState(state));
				color = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, radioButtonText, font, textBounds, color, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, textBounds);
			}
		}

		/// <summary>Returns the size, in pixels, of the option button (also known as a radio button) glyph.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size, in pixels, of the option button glyph.</returns>
		// Token: 0x060035BC RID: 13756 RVA: 0x000F3008 File Offset: 0x000F1208
		public static Size GetGlyphSize(Graphics g, RadioButtonState state)
		{
			return RadioButtonRenderer.GetGlyphSize(g, state, IntPtr.Zero);
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000F3016 File Offset: 0x000F1216
		internal static Size GetGlyphSize(Graphics g, RadioButtonState state, IntPtr hWnd)
		{
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer((int)state);
				return RadioButtonRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw, hWnd);
			}
			return new Size(13, 13);
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000F303C File Offset: 0x000F123C
		internal static ButtonState ConvertToButtonState(RadioButtonState state)
		{
			switch (state)
			{
			case RadioButtonState.UncheckedPressed:
				return ButtonState.Pushed;
			case RadioButtonState.UncheckedDisabled:
				return ButtonState.Inactive;
			case RadioButtonState.CheckedNormal:
			case RadioButtonState.CheckedHot:
				return ButtonState.Checked;
			case RadioButtonState.CheckedPressed:
				return ButtonState.Checked | ButtonState.Pushed;
			case RadioButtonState.CheckedDisabled:
				return ButtonState.Checked | ButtonState.Inactive;
			default:
				return ButtonState.Normal;
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000F308C File Offset: 0x000F128C
		internal static RadioButtonState ConvertFromButtonState(ButtonState state, bool isHot)
		{
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return RadioButtonState.CheckedPressed;
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return RadioButtonState.CheckedDisabled;
				}
				if (isHot)
				{
					return RadioButtonState.CheckedHot;
				}
				return RadioButtonState.CheckedNormal;
			}
			else
			{
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return RadioButtonState.UncheckedPressed;
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return RadioButtonState.UncheckedDisabled;
				}
				if (isHot)
				{
					return RadioButtonState.UncheckedHot;
				}
				return RadioButtonState.UncheckedNormal;
			}
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000F30F4 File Offset: 0x000F12F4
		private static void InitializeRenderer(int state)
		{
			int num = RadioButtonRenderer.RadioElement.Part;
			if (AccessibilityImprovements.Level2 && SystemInformation.HighContrast && (state == 8 || state == 4) && VisualStyleRenderer.IsCombinationDefined(RadioButtonRenderer.RadioElement.ClassName, VisualStyleElement.Button.RadioButton.HighContrastDisabledPart))
			{
				num = VisualStyleElement.Button.RadioButton.HighContrastDisabledPart;
			}
			if (RadioButtonRenderer.visualStyleRenderer == null)
			{
				RadioButtonRenderer.visualStyleRenderer = new VisualStyleRenderer(RadioButtonRenderer.RadioElement.ClassName, num, state);
				return;
			}
			RadioButtonRenderer.visualStyleRenderer.SetParameters(RadioButtonRenderer.RadioElement.ClassName, num, state);
		}

		// Token: 0x04001F5B RID: 8027
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x04001F5C RID: 8028
		private static readonly VisualStyleElement RadioElement = VisualStyleElement.Button.RadioButton.UncheckedNormal;

		// Token: 0x04001F5D RID: 8029
		private static bool renderMatchingApplicationState = true;
	}
}

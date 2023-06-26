using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a check box control with or without visual styles. This class cannot be inherited.</summary>
	// Token: 0x0200014B RID: 331
	public sealed class CheckBoxRenderer
	{
		// Token: 0x06000D09 RID: 3337 RVA: 0x00002843 File Offset: 0x00000A43
		private CheckBoxRenderer()
		{
		}

		/// <summary>Gets or sets a value indicating whether the renderer uses the application state to determine rendering style.</summary>
		/// <returns>
		///   <see langword="true" /> if the application state is used to determine rendering style; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x000254A4 File Offset: 0x000236A4
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x000254AB File Offset: 0x000236AB
		public static bool RenderMatchingApplicationState
		{
			get
			{
				return CheckBoxRenderer.renderMatchingApplicationState;
			}
			set
			{
				CheckBoxRenderer.renderMatchingApplicationState = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x000254B3 File Offset: 0x000236B3
		private static bool RenderWithVisualStyles
		{
			get
			{
				return !CheckBoxRenderer.renderMatchingApplicationState || Application.RenderWithVisualStyles;
			}
		}

		/// <summary>Indicates whether the background of the check box has semitransparent or alpha-blended pieces.</summary>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
		/// <returns>
		///   <see langword="true" /> if the background of the check box has semitransparent or alpha-blended pieces; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D0D RID: 3341 RVA: 0x000254C3 File Offset: 0x000236C3
		public static bool IsBackgroundPartiallyTransparent(CheckBoxState state)
		{
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer((int)state);
				return CheckBoxRenderer.visualStyleRenderer.IsBackgroundPartiallyTransparent();
			}
			return false;
		}

		/// <summary>Draws the background of a control's parent in the specified area.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the background of the parent of <paramref name="childControl" />.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which to draw the parent control's background. This rectangle should be inside the child control's bounds.</param>
		/// <param name="childControl">The control whose parent's background will be drawn.</param>
		// Token: 0x06000D0E RID: 3342 RVA: 0x000254DE File Offset: 0x000236DE
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer(0);
				CheckBoxRenderer.visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
			}
		}

		/// <summary>Draws a check box control in the specified state and location.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
		// Token: 0x06000D0F RID: 3343 RVA: 0x000254FA File Offset: 0x000236FA
		public static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state)
		{
			CheckBoxRenderer.DrawCheckBox(g, glyphLocation, state, IntPtr.Zero);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002550C File Offset: 0x0002370C
		internal static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state, IntPtr hWnd)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, CheckBoxRenderer.GetGlyphSize(g, state, hWnd));
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer((int)state);
				CheckBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle, hWnd);
				return;
			}
			if (CheckBoxRenderer.IsMixed(state))
			{
				ControlPaint.DrawMixedCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
				return;
			}
			ControlPaint.DrawCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
		}

		/// <summary>Draws a check box control in the specified state and location, with the specified text, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="checkBoxText" /> in.</param>
		/// <param name="checkBoxText">The <see cref="T:System.String" /> to draw with the check box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="checkBoxText" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
		// Token: 0x06000D11 RID: 3345 RVA: 0x00025567 File Offset: 0x00023767
		public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, bool focused, CheckBoxState state)
		{
			CheckBoxRenderer.DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, focused, state);
		}

		/// <summary>Draws a check box control in the specified state and location, with the specified text and text formatting, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="checkBoxText" /> in.</param>
		/// <param name="checkBoxText">The <see cref="T:System.String" /> to draw with the check box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="checkBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
		// Token: 0x06000D12 RID: 3346 RVA: 0x0002557C File Offset: 0x0002377C
		public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, TextFormatFlags flags, bool focused, CheckBoxState state)
		{
			CheckBoxRenderer.DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, flags, focused, state, IntPtr.Zero);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x000255A0 File Offset: 0x000237A0
		internal static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, TextFormatFlags flags, bool focused, CheckBoxState state, IntPtr hWnd)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, CheckBoxRenderer.GetGlyphSize(g, state, hWnd));
			Color color;
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer((int)state);
				CheckBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle);
				color = CheckBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				if (CheckBoxRenderer.IsMixed(state))
				{
					ControlPaint.DrawMixedCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
				}
				else
				{
					ControlPaint.DrawCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
				}
				color = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, checkBoxText, font, textBounds, color, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, textBounds);
			}
		}

		/// <summary>Draws a check box control in the specified state and location, with the specified text and image, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="checkBoxText" /> in.</param>
		/// <param name="checkBoxText">The <see cref="T:System.String" /> to draw with the check box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="checkBoxText" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw with the check box.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
		// Token: 0x06000D14 RID: 3348 RVA: 0x00025630 File Offset: 0x00023830
		public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, Image image, Rectangle imageBounds, bool focused, CheckBoxState state)
		{
			CheckBoxRenderer.DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, image, imageBounds, focused, state);
		}

		/// <summary>Draws a check box control in the specified state and location; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="checkBoxText" /> in.</param>
		/// <param name="checkBoxText">The <see cref="T:System.String" /> to draw with the check box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="checkBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw with the check box.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
		// Token: 0x06000D15 RID: 3349 RVA: 0x00025654 File Offset: 0x00023854
		public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, CheckBoxState state)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, CheckBoxRenderer.GetGlyphSize(g, state));
			Color color;
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer((int)state);
				CheckBoxRenderer.visualStyleRenderer.DrawImage(g, imageBounds, image);
				CheckBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle);
				color = CheckBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				g.DrawImage(image, imageBounds);
				if (CheckBoxRenderer.IsMixed(state))
				{
					ControlPaint.DrawMixedCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
				}
				else
				{
					ControlPaint.DrawCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
				}
				color = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, checkBoxText, font, textBounds, color, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, textBounds);
			}
		}

		/// <summary>Returns the size of the check box glyph.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the check box glyph.</returns>
		// Token: 0x06000D16 RID: 3350 RVA: 0x000256FB File Offset: 0x000238FB
		public static Size GetGlyphSize(Graphics g, CheckBoxState state)
		{
			return CheckBoxRenderer.GetGlyphSize(g, state, IntPtr.Zero);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00025709 File Offset: 0x00023909
		internal static Size GetGlyphSize(Graphics g, CheckBoxState state, IntPtr hWnd)
		{
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer((int)state);
				return CheckBoxRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw, hWnd);
			}
			return new Size(13, 13);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00025730 File Offset: 0x00023930
		internal static ButtonState ConvertToButtonState(CheckBoxState state)
		{
			switch (state)
			{
			case CheckBoxState.UncheckedPressed:
				return ButtonState.Pushed;
			case CheckBoxState.UncheckedDisabled:
				return ButtonState.Inactive;
			case CheckBoxState.CheckedNormal:
			case CheckBoxState.CheckedHot:
				return ButtonState.Checked;
			case CheckBoxState.CheckedPressed:
				return ButtonState.Checked | ButtonState.Pushed;
			case CheckBoxState.CheckedDisabled:
				return ButtonState.Checked | ButtonState.Inactive;
			case CheckBoxState.MixedNormal:
			case CheckBoxState.MixedHot:
				return ButtonState.Checked;
			case CheckBoxState.MixedPressed:
				return ButtonState.Checked | ButtonState.Pushed;
			case CheckBoxState.MixedDisabled:
				return ButtonState.Checked | ButtonState.Inactive;
			default:
				return ButtonState.Normal;
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000257A0 File Offset: 0x000239A0
		internal static CheckBoxState ConvertFromButtonState(ButtonState state, bool isMixed, bool isHot)
		{
			if (isMixed)
			{
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return CheckBoxState.MixedPressed;
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return CheckBoxState.MixedDisabled;
				}
				if (isHot)
				{
					return CheckBoxState.MixedHot;
				}
				return CheckBoxState.MixedNormal;
			}
			else if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return CheckBoxState.CheckedPressed;
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return CheckBoxState.CheckedDisabled;
				}
				if (isHot)
				{
					return CheckBoxState.CheckedHot;
				}
				return CheckBoxState.CheckedNormal;
			}
			else
			{
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return CheckBoxState.UncheckedPressed;
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return CheckBoxState.UncheckedDisabled;
				}
				if (isHot)
				{
					return CheckBoxState.UncheckedHot;
				}
				return CheckBoxState.UncheckedNormal;
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00025836 File Offset: 0x00023A36
		private static bool IsMixed(CheckBoxState state)
		{
			return state - CheckBoxState.MixedNormal <= 3;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00025842 File Offset: 0x00023A42
		private static bool IsDisabled(CheckBoxState state)
		{
			return state == CheckBoxState.UncheckedDisabled || state == CheckBoxState.CheckedDisabled || state == CheckBoxState.MixedDisabled;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00025854 File Offset: 0x00023A54
		private static void InitializeRenderer(int state)
		{
			int num = CheckBoxRenderer.CheckBoxElement.Part;
			if (AccessibilityImprovements.Level2 && SystemInformation.HighContrast && CheckBoxRenderer.IsDisabled((CheckBoxState)state) && VisualStyleRenderer.IsCombinationDefined(CheckBoxRenderer.CheckBoxElement.ClassName, VisualStyleElement.Button.CheckBox.HighContrastDisabledPart))
			{
				num = VisualStyleElement.Button.CheckBox.HighContrastDisabledPart;
			}
			if (CheckBoxRenderer.visualStyleRenderer == null)
			{
				CheckBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(CheckBoxRenderer.CheckBoxElement.ClassName, num, state);
				return;
			}
			CheckBoxRenderer.visualStyleRenderer.SetParameters(CheckBoxRenderer.CheckBoxElement.ClassName, num, state);
		}

		// Token: 0x04000764 RID: 1892
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x04000765 RID: 1893
		private static readonly VisualStyleElement CheckBoxElement = VisualStyleElement.Button.CheckBox.UncheckedNormal;

		// Token: 0x04000766 RID: 1894
		private static bool renderMatchingApplicationState = true;
	}
}

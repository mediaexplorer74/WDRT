using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a button control with or without visual styles. This class cannot be inherited.</summary>
	// Token: 0x02000144 RID: 324
	public sealed class ButtonRenderer
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x00002843 File Offset: 0x00000A43
		private ButtonRenderer()
		{
		}

		/// <summary>Gets or sets a value indicating whether the renderer uses the application state to determine rendering style.</summary>
		/// <returns>
		///   <see langword="true" /> if the application state is used to determine rendering style; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x000249D0 File Offset: 0x00022BD0
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x000249D7 File Offset: 0x00022BD7
		public static bool RenderMatchingApplicationState
		{
			get
			{
				return ButtonRenderer.renderMatchingApplicationState;
			}
			set
			{
				ButtonRenderer.renderMatchingApplicationState = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x000249DF File Offset: 0x00022BDF
		private static bool RenderWithVisualStyles
		{
			get
			{
				return !ButtonRenderer.renderMatchingApplicationState || Application.RenderWithVisualStyles;
			}
		}

		/// <summary>Indicates whether the background of the button has semitransparent or alpha-blended pieces.</summary>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		/// <returns>
		///   <see langword="true" /> if the background of the button has semitransparent or alpha-blended pieces; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000CC6 RID: 3270 RVA: 0x000249EF File Offset: 0x00022BEF
		public static bool IsBackgroundPartiallyTransparent(PushButtonState state)
		{
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				return ButtonRenderer.visualStyleRenderer.IsBackgroundPartiallyTransparent();
			}
			return false;
		}

		/// <summary>Draws the background of a control's parent in the specified area.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the background of the parent of <paramref name="childControl" />.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which to draw the parent control's background. This rectangle should be inside the child control's bounds.</param>
		/// <param name="childControl">The control whose parent's background will be drawn.</param>
		// Token: 0x06000CC7 RID: 3271 RVA: 0x00024A0A File Offset: 0x00022C0A
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer(0);
				ButtonRenderer.visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
			}
		}

		/// <summary>Draws a button control in the specified state and bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		// Token: 0x06000CC8 RID: 3272 RVA: 0x00024A26 File Offset: 0x00022C26
		public static void DrawButton(Graphics g, Rectangle bounds, PushButtonState state)
		{
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				ButtonRenderer.visualStyleRenderer.DrawBackground(g, bounds);
				return;
			}
			ControlPaint.DrawButton(g, bounds, ButtonRenderer.ConvertToButtonState(state));
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00024A50 File Offset: 0x00022C50
		internal static void DrawButtonForHandle(Graphics g, Rectangle bounds, bool focused, PushButtonState state, IntPtr handle)
		{
			Rectangle rectangle;
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				ButtonRenderer.visualStyleRenderer.DrawBackground(g, bounds, handle);
				rectangle = ButtonRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
			}
			else
			{
				ControlPaint.DrawButton(g, bounds, ButtonRenderer.ConvertToButtonState(state));
				rectangle = Rectangle.Inflate(bounds, -3, -3);
			}
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		/// <summary>Draws a button control in the specified state and bounds, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle on the button; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		// Token: 0x06000CCA RID: 3274 RVA: 0x00024AA9 File Offset: 0x00022CA9
		public static void DrawButton(Graphics g, Rectangle bounds, bool focused, PushButtonState state)
		{
			ButtonRenderer.DrawButtonForHandle(g, bounds, focused, state, IntPtr.Zero);
		}

		/// <summary>Draws a button control in the specified state and bounds, with the specified text, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle on the button; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		// Token: 0x06000CCB RID: 3275 RVA: 0x00024AB9 File Offset: 0x00022CB9
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, bool focused, PushButtonState state)
		{
			ButtonRenderer.DrawButton(g, bounds, buttonText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, focused, state);
		}

		/// <summary>Draws a button control in the specified state and bounds, with the specified text and text formatting, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to <paramref name="buttonText" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle on the button; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		// Token: 0x06000CCC RID: 3276 RVA: 0x00024ACC File Offset: 0x00022CCC
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, TextFormatFlags flags, bool focused, PushButtonState state)
		{
			Rectangle rectangle;
			Color color;
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				ButtonRenderer.visualStyleRenderer.DrawBackground(g, bounds);
				rectangle = ButtonRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
				color = ButtonRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				ControlPaint.DrawButton(g, bounds, ButtonRenderer.ConvertToButtonState(state));
				rectangle = Rectangle.Inflate(bounds, -3, -3);
				color = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, buttonText, font, rectangle, color, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		/// <summary>Draws a button control in the specified state and bounds, with the specified image, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw on the button.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle on the button; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		// Token: 0x06000CCD RID: 3277 RVA: 0x00024B48 File Offset: 0x00022D48
		public static void DrawButton(Graphics g, Rectangle bounds, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
		{
			Rectangle rectangle;
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				ButtonRenderer.visualStyleRenderer.DrawBackground(g, bounds);
				ButtonRenderer.visualStyleRenderer.DrawImage(g, imageBounds, image);
				rectangle = ButtonRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
			}
			else
			{
				ControlPaint.DrawButton(g, bounds, ButtonRenderer.ConvertToButtonState(state));
				g.DrawImage(image, imageBounds);
				rectangle = Rectangle.Inflate(bounds, -3, -3);
			}
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		/// <summary>Draws a button control in the specified state and bounds, with the specified text and image, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw on the button.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle on the button; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		// Token: 0x06000CCE RID: 3278 RVA: 0x00024BB8 File Offset: 0x00022DB8
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
		{
			ButtonRenderer.DrawButton(g, bounds, buttonText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, image, imageBounds, focused, state);
		}

		/// <summary>Draws a button control in the specified state and bounds; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to <paramref name="buttonText" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw on the button.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle on the button; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		// Token: 0x06000CCF RID: 3279 RVA: 0x00024BD8 File Offset: 0x00022DD8
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
		{
			Rectangle rectangle;
			Color color;
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				ButtonRenderer.visualStyleRenderer.DrawBackground(g, bounds);
				ButtonRenderer.visualStyleRenderer.DrawImage(g, imageBounds, image);
				rectangle = ButtonRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
				color = ButtonRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				ControlPaint.DrawButton(g, bounds, ButtonRenderer.ConvertToButtonState(state));
				g.DrawImage(image, imageBounds);
				rectangle = Rectangle.Inflate(bounds, -3, -3);
				color = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, buttonText, font, rectangle, color, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00024C6D File Offset: 0x00022E6D
		internal static ButtonState ConvertToButtonState(PushButtonState state)
		{
			if (state == PushButtonState.Pressed)
			{
				return ButtonState.Pushed;
			}
			if (state != PushButtonState.Disabled)
			{
				return ButtonState.Normal;
			}
			return ButtonState.Inactive;
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00024C88 File Offset: 0x00022E88
		private static void InitializeRenderer(int state)
		{
			if (ButtonRenderer.visualStyleRenderer == null)
			{
				ButtonRenderer.visualStyleRenderer = new VisualStyleRenderer(ButtonRenderer.ButtonElement.ClassName, ButtonRenderer.ButtonElement.Part, state);
				return;
			}
			ButtonRenderer.visualStyleRenderer.SetParameters(ButtonRenderer.ButtonElement.ClassName, ButtonRenderer.ButtonElement.Part, state);
		}

		// Token: 0x04000740 RID: 1856
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x04000741 RID: 1857
		private static readonly VisualStyleElement ButtonElement = VisualStyleElement.Button.PushButton.Normal;

		// Token: 0x04000742 RID: 1858
		private static bool renderMatchingApplicationState = true;
	}
}

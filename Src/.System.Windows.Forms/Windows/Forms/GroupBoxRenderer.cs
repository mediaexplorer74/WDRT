using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a group box control with or without visual styles. This class cannot be inherited.</summary>
	// Token: 0x0200026C RID: 620
	public sealed class GroupBoxRenderer
	{
		// Token: 0x060027E6 RID: 10214 RVA: 0x00002843 File Offset: 0x00000A43
		private GroupBoxRenderer()
		{
		}

		/// <summary>Gets or sets a value indicating whether the renderer uses the application state to determine rendering style.</summary>
		/// <returns>
		///   <see langword="true" /> if the application state is used to determine rendering style; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x000B995C File Offset: 0x000B7B5C
		// (set) Token: 0x060027E8 RID: 10216 RVA: 0x000B9963 File Offset: 0x000B7B63
		public static bool RenderMatchingApplicationState
		{
			get
			{
				return GroupBoxRenderer.renderMatchingApplicationState;
			}
			set
			{
				GroupBoxRenderer.renderMatchingApplicationState = value;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x000B996B File Offset: 0x000B7B6B
		private static bool RenderWithVisualStyles
		{
			get
			{
				return !GroupBoxRenderer.renderMatchingApplicationState || Application.RenderWithVisualStyles;
			}
		}

		/// <summary>Indicates whether the background of the group box has any semitransparent or alpha-blended pieces.</summary>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		/// <returns>
		///   <see langword="true" /> if the background of the group box has semitransparent or alpha-blended pieces; otherwise, <see langword="false" />.</returns>
		// Token: 0x060027EA RID: 10218 RVA: 0x000B997B File Offset: 0x000B7B7B
		public static bool IsBackgroundPartiallyTransparent(GroupBoxState state)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.InitializeRenderer((int)state);
				return GroupBoxRenderer.visualStyleRenderer.IsBackgroundPartiallyTransparent();
			}
			return false;
		}

		/// <summary>Draws the background of a control's parent in the specified area.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the background of the parent of <paramref name="childControl" />.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which to draw the parent control's background. This rectangle should be inside the child control's bounds.</param>
		/// <param name="childControl">The control whose parent's background will be drawn.</param>
		// Token: 0x060027EB RID: 10219 RVA: 0x000B9996 File Offset: 0x000B7B96
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.InitializeRenderer(0);
				GroupBoxRenderer.visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
			}
		}

		/// <summary>Draws a group box control in the specified state and bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		// Token: 0x060027EC RID: 10220 RVA: 0x000B99B2 File Offset: 0x000B7BB2
		public static void DrawGroupBox(Graphics g, Rectangle bounds, GroupBoxState state)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.DrawThemedGroupBoxNoText(g, bounds, state);
				return;
			}
			GroupBoxRenderer.DrawUnthemedGroupBoxNoText(g, bounds, state);
		}

		/// <summary>Draws a group box control in the specified state and bounds, with the specified text and font.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="groupBoxText">The <see cref="T:System.String" /> to draw with the group box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		// Token: 0x060027ED RID: 10221 RVA: 0x000B99CC File Offset: 0x000B7BCC
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, GroupBoxState state)
		{
			GroupBoxRenderer.DrawGroupBox(g, bounds, groupBoxText, font, TextFormatFlags.Default, state);
		}

		/// <summary>Draws a group box control in the specified state and bounds, with the specified text, font, and color.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="groupBoxText">The <see cref="T:System.String" /> to draw with the group box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		// Token: 0x060027EE RID: 10222 RVA: 0x000B99DA File Offset: 0x000B7BDA
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, GroupBoxState state)
		{
			GroupBoxRenderer.DrawGroupBox(g, bounds, groupBoxText, font, textColor, TextFormatFlags.Default, state);
		}

		/// <summary>Draws a group box control in the specified state and bounds, with the specified text, font, and text formatting.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="groupBoxText">The <see cref="T:System.String" /> to draw with the group box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		// Token: 0x060027EF RID: 10223 RVA: 0x000B99EA File Offset: 0x000B7BEA
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, TextFormatFlags flags, GroupBoxState state)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.DrawThemedGroupBoxWithText(g, bounds, groupBoxText, font, GroupBoxRenderer.DefaultTextColor(state), flags, state);
				return;
			}
			GroupBoxRenderer.DrawUnthemedGroupBoxWithText(g, bounds, groupBoxText, font, GroupBoxRenderer.DefaultTextColor(state), flags, state);
		}

		/// <summary>Draws a group box control in the specified state and bounds, with the specified text, font, color, and text formatting.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="groupBoxText">The <see cref="T:System.String" /> to draw with the group box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		// Token: 0x060027F0 RID: 10224 RVA: 0x000B9A1C File Offset: 0x000B7C1C
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, TextFormatFlags flags, GroupBoxState state)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.DrawThemedGroupBoxWithText(g, bounds, groupBoxText, font, textColor, flags, state);
				return;
			}
			GroupBoxRenderer.DrawUnthemedGroupBoxWithText(g, bounds, groupBoxText, font, textColor, flags, state);
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000B9A44 File Offset: 0x000B7C44
		private static void DrawThemedGroupBoxNoText(Graphics g, Rectangle bounds, GroupBoxState state)
		{
			GroupBoxRenderer.InitializeRenderer((int)state);
			GroupBoxRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000B9A58 File Offset: 0x000B7C58
		private static void DrawThemedGroupBoxWithText(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, TextFormatFlags flags, GroupBoxState state)
		{
			GroupBoxRenderer.InitializeRenderer((int)state);
			Rectangle rectangle = bounds;
			rectangle.Width -= 14;
			Size size = TextRenderer.MeasureText(g, groupBoxText, font, new Size(rectangle.Width, rectangle.Height), flags);
			rectangle.Width = size.Width;
			rectangle.Height = size.Height;
			if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				rectangle.X = bounds.Right - rectangle.Width - 7 + 1;
			}
			else
			{
				rectangle.X += 6;
			}
			TextRenderer.DrawText(g, groupBoxText, font, rectangle, textColor, flags);
			Rectangle rectangle2 = bounds;
			rectangle2.Y += font.Height / 2;
			rectangle2.Height -= font.Height / 2;
			Rectangle rectangle3 = rectangle2;
			Rectangle rectangle4 = rectangle2;
			Rectangle rectangle5 = rectangle2;
			rectangle3.Width = 7;
			rectangle4.Width = Math.Max(0, rectangle.Width - 3);
			if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				rectangle3.X = rectangle2.Right - 7;
				rectangle4.X = rectangle3.Left - rectangle4.Width;
				rectangle5.Width = rectangle4.X - rectangle2.X;
			}
			else
			{
				rectangle4.X = rectangle3.Right;
				rectangle5.X = rectangle4.Right;
				rectangle5.Width = rectangle2.Right - rectangle5.X;
			}
			rectangle4.Y = rectangle.Bottom;
			rectangle4.Height -= rectangle.Bottom - rectangle2.Top;
			GroupBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle2, rectangle3);
			GroupBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle2, rectangle4);
			GroupBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle2, rectangle5);
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000B9C18 File Offset: 0x000B7E18
		private static void DrawUnthemedGroupBoxNoText(Graphics g, Rectangle bounds, GroupBoxState state)
		{
			Color control = SystemColors.Control;
			Pen pen = new Pen(ControlPaint.Light(control, 1f));
			Pen pen2 = new Pen(ControlPaint.Dark(control, 0f));
			try
			{
				g.DrawLine(pen, bounds.Left + 1, bounds.Top + 1, bounds.Left + 1, bounds.Height - 1);
				g.DrawLine(pen2, bounds.Left, bounds.Top + 1, bounds.Left, bounds.Height - 2);
				g.DrawLine(pen, bounds.Left, bounds.Height - 1, bounds.Width - 1, bounds.Height - 1);
				g.DrawLine(pen2, bounds.Left, bounds.Height - 2, bounds.Width - 1, bounds.Height - 2);
				g.DrawLine(pen, bounds.Left + 1, bounds.Top + 1, bounds.Width - 1, bounds.Top + 1);
				g.DrawLine(pen2, bounds.Left, bounds.Top, bounds.Width - 2, bounds.Top);
				g.DrawLine(pen, bounds.Width - 1, bounds.Top, bounds.Width - 1, bounds.Height - 1);
				g.DrawLine(pen2, bounds.Width - 2, bounds.Top, bounds.Width - 2, bounds.Height - 2);
			}
			finally
			{
				if (pen != null)
				{
					pen.Dispose();
				}
				if (pen2 != null)
				{
					pen2.Dispose();
				}
			}
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000B9DC4 File Offset: 0x000B7FC4
		private static void DrawUnthemedGroupBoxWithText(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, TextFormatFlags flags, GroupBoxState state)
		{
			Rectangle rectangle = bounds;
			rectangle.Width -= 8;
			Size size = TextRenderer.MeasureText(g, groupBoxText, font, new Size(rectangle.Width, rectangle.Height), flags);
			rectangle.Width = size.Width;
			rectangle.Height = size.Height;
			if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				rectangle.X = bounds.Right - rectangle.Width - 8;
			}
			else
			{
				rectangle.X += 8;
			}
			TextRenderer.DrawText(g, groupBoxText, font, rectangle, textColor, flags);
			if (rectangle.Width > 0)
			{
				rectangle.Inflate(2, 0);
			}
			Pen pen = new Pen(SystemColors.ControlLight);
			Pen pen2 = new Pen(SystemColors.ControlDark);
			int num = bounds.Top + font.Height / 2;
			g.DrawLine(pen, bounds.Left + 1, num, bounds.Left + 1, bounds.Height - 1);
			g.DrawLine(pen2, bounds.Left, num - 1, bounds.Left, bounds.Height - 2);
			g.DrawLine(pen, bounds.Left, bounds.Height - 1, bounds.Width, bounds.Height - 1);
			g.DrawLine(pen2, bounds.Left, bounds.Height - 2, bounds.Width - 1, bounds.Height - 2);
			g.DrawLine(pen, bounds.Left + 1, num, rectangle.X - 2, num);
			g.DrawLine(pen2, bounds.Left, num - 1, rectangle.X - 3, num - 1);
			g.DrawLine(pen, rectangle.X + rectangle.Width + 1, num, bounds.Width - 1, num);
			g.DrawLine(pen2, rectangle.X + rectangle.Width + 2, num - 1, bounds.Width - 2, num - 1);
			g.DrawLine(pen, bounds.Width - 1, num, bounds.Width - 1, bounds.Height - 1);
			g.DrawLine(pen2, bounds.Width - 2, num - 1, bounds.Width - 2, bounds.Height - 2);
			pen.Dispose();
			pen2.Dispose();
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000BA00A File Offset: 0x000B820A
		private static Color DefaultTextColor(GroupBoxState state)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.InitializeRenderer((int)state);
				return GroupBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			return SystemColors.ControlText;
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000BA030 File Offset: 0x000B8230
		private static void InitializeRenderer(int state)
		{
			int num = GroupBoxRenderer.GroupBoxElement.Part;
			if (AccessibilityImprovements.Level2 && SystemInformation.HighContrast && state == 2 && VisualStyleRenderer.IsCombinationDefined(GroupBoxRenderer.GroupBoxElement.ClassName, VisualStyleElement.Button.GroupBox.HighContrastDisabledPart))
			{
				num = VisualStyleElement.Button.GroupBox.HighContrastDisabledPart;
			}
			if (GroupBoxRenderer.visualStyleRenderer == null)
			{
				GroupBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(GroupBoxRenderer.GroupBoxElement.ClassName, num, state);
				return;
			}
			GroupBoxRenderer.visualStyleRenderer.SetParameters(GroupBoxRenderer.GroupBoxElement.ClassName, num, state);
		}

		// Token: 0x04001055 RID: 4181
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x04001056 RID: 4182
		private static readonly VisualStyleElement GroupBoxElement = VisualStyleElement.Button.GroupBox.Normal;

		// Token: 0x04001057 RID: 4183
		private const int textOffset = 8;

		// Token: 0x04001058 RID: 4184
		private const int boxHeaderWidth = 7;

		// Token: 0x04001059 RID: 4185
		private static bool renderMatchingApplicationState = true;
	}
}

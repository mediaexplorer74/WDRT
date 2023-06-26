using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a tab control with visual styles. This class cannot be inherited.</summary>
	// Token: 0x0200039D RID: 925
	public sealed class TabRenderer
	{
		// Token: 0x06003C9F RID: 15519 RVA: 0x00002843 File Offset: 0x00000A43
		private TabRenderer()
		{
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.TabRenderer" /> class can be used to draw a tab control with visual styles.</summary>
		/// <returns>
		///   <see langword="true" /> if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06003CA0 RID: 15520 RVA: 0x0002BBA7 File Offset: 0x00029DA7
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		/// <summary>Draws a tab in the specified state and bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CA1 RID: 15521 RVA: 0x00107537 File Offset: 0x00105737
		public static void DrawTabItem(Graphics g, Rectangle bounds, TabItemState state)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.TabItem.Normal, (int)state);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a tab in the specified state and bounds, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CA2 RID: 15522 RVA: 0x00107550 File Offset: 0x00105750
		public static void DrawTabItem(Graphics g, Rectangle bounds, bool focused, TabItemState state)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.TabItem.Normal, (int)state);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			Rectangle rectangle = Rectangle.Inflate(bounds, -3, -3);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		/// <summary>Draws a tab in the specified state and bounds, and with the specified text.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="tabItemText">The <see cref="T:System.String" /> to draw in the tab.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="tabItemText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CA3 RID: 15523 RVA: 0x00107589 File Offset: 0x00105789
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, tabItemText, font, false, state);
		}

		/// <summary>Draws a tab in the specified state and bounds, with the specified text, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="tabItemText">The <see cref="T:System.String" /> to draw in the tab.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="tabItemText" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CA4 RID: 15524 RVA: 0x00107597 File Offset: 0x00105797
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, bool focused, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, tabItemText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, focused, state);
		}

		/// <summary>Draws a tab in the specified state and bounds, with the specified text and text formatting, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="tabItemText">The <see cref="T:System.String" /> to draw in the tab.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="tabItemText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CA5 RID: 15525 RVA: 0x001075A8 File Offset: 0x001057A8
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, TextFormatFlags flags, bool focused, TabItemState state)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.TabItem.Normal, (int)state);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			Rectangle rectangle = Rectangle.Inflate(bounds, -3, -3);
			Color color = TabRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			TextRenderer.DrawText(g, tabItemText, font, rectangle, color, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		/// <summary>Draws a tab in the specified state and bounds, with the specified image, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw in the tab.</param>
		/// <param name="imageRectangle">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="image" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CA6 RID: 15526 RVA: 0x00107600 File Offset: 0x00105800
		public static void DrawTabItem(Graphics g, Rectangle bounds, Image image, Rectangle imageRectangle, bool focused, TabItemState state)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.TabItem.Normal, (int)state);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			Rectangle rectangle = Rectangle.Inflate(bounds, -3, -3);
			TabRenderer.visualStyleRenderer.DrawImage(g, imageRectangle, image);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		/// <summary>Draws a tab in the specified state and bounds, with the specified text and image, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="tabItemText">The <see cref="T:System.String" /> to draw in the tab.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="tabItemText" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw in the tab.</param>
		/// <param name="imageRectangle">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="image" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CA7 RID: 15527 RVA: 0x00107648 File Offset: 0x00105848
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, Image image, Rectangle imageRectangle, bool focused, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, tabItemText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, image, imageRectangle, focused, state);
		}

		/// <summary>Draws a tab in the specified state and bounds; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="tabItemText">The <see cref="T:System.String" /> to draw in the tab.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="tabItemText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw in the tab.</param>
		/// <param name="imageRectangle">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="image" />.</param>
		/// <param name="focused">
		///   <see langword="true" /> to draw a focus rectangle; otherwise, <see langword="false" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CA8 RID: 15528 RVA: 0x00107668 File Offset: 0x00105868
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, TextFormatFlags flags, Image image, Rectangle imageRectangle, bool focused, TabItemState state)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.TabItem.Normal, (int)state);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			Rectangle rectangle = Rectangle.Inflate(bounds, -3, -3);
			TabRenderer.visualStyleRenderer.DrawImage(g, imageRectangle, image);
			Color color = TabRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			TextRenderer.DrawText(g, tabItemText, font, rectangle, color, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		/// <summary>Draws a tab page in the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab page.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab page.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003CA9 RID: 15529 RVA: 0x001076CE File Offset: 0x001058CE
		public static void DrawTabPage(Graphics g, Rectangle bounds)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.Pane.Normal, 0);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x001076E7 File Offset: 0x001058E7
		private static void InitializeRenderer(VisualStyleElement element, int state)
		{
			if (TabRenderer.visualStyleRenderer == null)
			{
				TabRenderer.visualStyleRenderer = new VisualStyleRenderer(element.ClassName, element.Part, state);
				return;
			}
			TabRenderer.visualStyleRenderer.SetParameters(element.ClassName, element.Part, state);
		}

		// Token: 0x0400239A RID: 9114
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer;
	}
}

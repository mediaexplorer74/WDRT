using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a scroll bar control with visual styles. This class cannot be inherited.</summary>
	// Token: 0x02000356 RID: 854
	public sealed class ScrollBarRenderer
	{
		// Token: 0x0600383C RID: 14396 RVA: 0x00002843 File Offset: 0x00000A43
		private ScrollBarRenderer()
		{
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ScrollBarRenderer" /> class can be used to draw a scroll bar with visual styles.</summary>
		/// <returns>
		///   <see langword="true" /> if the user has enabled visual styles in the operating system and visual styles are applied to the client areas of application windows; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x0002BBA7 File Offset: 0x00029DA7
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		/// <summary>Draws a scroll arrow with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll arrow.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll arrow.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarArrowButtonState" /> values that specifies the visual state of the scroll arrow.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x0600383E RID: 14398 RVA: 0x000FA0EE File Offset: 0x000F82EE
		public static void DrawArrowButton(Graphics g, Rectangle bounds, ScrollBarArrowButtonState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftNormal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a horizontal scroll box (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x0600383F RID: 14399 RVA: 0x000FA107 File Offset: 0x000F8307
		public static void DrawHorizontalThumb(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a vertical scroll box (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003840 RID: 14400 RVA: 0x000FA120 File Offset: 0x000F8320
		public static void DrawVerticalThumb(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a grip on a horizontal scroll box (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box grip.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box grip.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box grip.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003841 RID: 14401 RVA: 0x000FA139 File Offset: 0x000F8339
		public static void DrawHorizontalThumbGrip(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.GripperHorizontal.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a grip on a vertical scroll box (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box grip.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box grip.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box grip.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003842 RID: 14402 RVA: 0x000FA152 File Offset: 0x000F8352
		public static void DrawVerticalThumbGrip(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.GripperVertical.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a horizontal scroll bar track with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003843 RID: 14403 RVA: 0x000FA16B File Offset: 0x000F836B
		public static void DrawRightHorizontalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.RightTrackHorizontal.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a horizontal scroll bar track with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003844 RID: 14404 RVA: 0x000FA184 File Offset: 0x000F8384
		public static void DrawLeftHorizontalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a vertical scroll bar track with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003845 RID: 14405 RVA: 0x000FA19D File Offset: 0x000F839D
		public static void DrawUpperVerticalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.UpperTrackVertical.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a vertical scroll bar track with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003846 RID: 14406 RVA: 0x000FA1B6 File Offset: 0x000F83B6
		public static void DrawLowerVerticalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.LowerTrackVertical.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a scroll bar sizing handle with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the sizing handle.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the sizing handle.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarSizeBoxState" /> values that specifies the visual state of the sizing handle.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003847 RID: 14407 RVA: 0x000FA1CF File Offset: 0x000F83CF
		public static void DrawSizeBox(Graphics g, Rectangle bounds, ScrollBarSizeBoxState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.SizeBox.LeftAlign, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Returns the size of the scroll box grip.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box grip.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size of the scroll box grip.</returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003848 RID: 14408 RVA: 0x000FA1E8 File Offset: 0x000F83E8
		public static Size GetThumbGripSize(Graphics g, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.GripperHorizontal.Normal, (int)state);
			return ScrollBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		/// <summary>Returns the size of the sizing handle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the sizing handle.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size of the sizing handle.</returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003849 RID: 14409 RVA: 0x000FA201 File Offset: 0x000F8401
		public static Size GetSizeBoxSize(Graphics g, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.SizeBox.LeftAlign, (int)state);
			return ScrollBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000FA21A File Offset: 0x000F841A
		private static void InitializeRenderer(VisualStyleElement element, int state)
		{
			if (ScrollBarRenderer.visualStyleRenderer == null)
			{
				ScrollBarRenderer.visualStyleRenderer = new VisualStyleRenderer(element.ClassName, element.Part, state);
				return;
			}
			ScrollBarRenderer.visualStyleRenderer.SetParameters(element.ClassName, element.Part, state);
		}

		// Token: 0x04002182 RID: 8578
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer;
	}
}

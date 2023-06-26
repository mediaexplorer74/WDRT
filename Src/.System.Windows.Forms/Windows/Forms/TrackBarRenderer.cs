using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a track bar control with visual styles. This class cannot be inherited.</summary>
	// Token: 0x0200040D RID: 1037
	public sealed class TrackBarRenderer
	{
		// Token: 0x0600483C RID: 18492 RVA: 0x00002843 File Offset: 0x00000A43
		private TrackBarRenderer()
		{
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.TrackBarRenderer" /> class can be used to draw a track bar with visual styles.</summary>
		/// <returns>
		///   <see langword="true" /> if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x0600483D RID: 18493 RVA: 0x0002BBA7 File Offset: 0x00029DA7
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		/// <summary>Draws the track for a horizontal track bar with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x0600483E RID: 18494 RVA: 0x00130110 File Offset: 0x0012E310
		public static void DrawHorizontalTrack(Graphics g, Rectangle bounds)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.Track.Normal, 1);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws the track for a vertical track bar with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x0600483F RID: 18495 RVA: 0x00130129 File Offset: 0x0012E329
		public static void DrawVerticalTrack(Graphics g, Rectangle bounds)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.TrackVertical.Normal, 1);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a horizontal track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06004840 RID: 18496 RVA: 0x00130142 File Offset: 0x0012E342
		public static void DrawHorizontalThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.Thumb.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a vertical track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06004841 RID: 18497 RVA: 0x0013015B File Offset: 0x0012E35B
		public static void DrawVerticalThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbVertical.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a left-pointing track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06004842 RID: 18498 RVA: 0x00130174 File Offset: 0x0012E374
		public static void DrawLeftPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbLeft.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a right-pointing track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06004843 RID: 18499 RVA: 0x0013018D File Offset: 0x0012E38D
		public static void DrawRightPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbRight.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws an upward-pointing track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06004844 RID: 18500 RVA: 0x001301A6 File Offset: 0x0012E3A6
		public static void DrawTopPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbTop.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a downward-pointing track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06004845 RID: 18501 RVA: 0x001301BF File Offset: 0x0012E3BF
		public static void DrawBottomPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbBottom.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws the specified number of horizontal track bar ticks with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the ticks.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the ticks.</param>
		/// <param name="numTicks">The number of ticks to draw.</param>
		/// <param name="edgeStyle">One of the <see cref="T:System.Windows.Forms.VisualStyles.EdgeStyle" /> values.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06004846 RID: 18502 RVA: 0x001301D8 File Offset: 0x0012E3D8
		public static void DrawHorizontalTicks(Graphics g, Rectangle bounds, int numTicks, EdgeStyle edgeStyle)
		{
			if (numTicks <= 0 || bounds.Height <= 0 || bounds.Width <= 0 || g == null)
			{
				return;
			}
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.Ticks.Normal, 1);
			if (numTicks == 1)
			{
				TrackBarRenderer.visualStyleRenderer.DrawEdge(g, new Rectangle(bounds.X, bounds.Y, 2, bounds.Height), Edges.Left, edgeStyle, EdgeEffects.None);
				return;
			}
			float num = ((float)bounds.Width - 2f) / ((float)numTicks - 1f);
			while (numTicks > 0)
			{
				float num2 = (float)bounds.X + (float)(numTicks - 1) * num;
				TrackBarRenderer.visualStyleRenderer.DrawEdge(g, new Rectangle((int)Math.Round((double)num2), bounds.Y, 2, bounds.Height), Edges.Left, edgeStyle, EdgeEffects.None);
				numTicks--;
			}
		}

		/// <summary>Draws the specified number of vertical track bar ticks with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the ticks.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the ticks.</param>
		/// <param name="numTicks">The number of ticks to draw.</param>
		/// <param name="edgeStyle">One of the <see cref="T:System.Windows.Forms.VisualStyles.EdgeStyle" /> values.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06004847 RID: 18503 RVA: 0x0013029C File Offset: 0x0012E49C
		public static void DrawVerticalTicks(Graphics g, Rectangle bounds, int numTicks, EdgeStyle edgeStyle)
		{
			if (numTicks <= 0 || bounds.Height <= 0 || bounds.Width <= 0 || g == null)
			{
				return;
			}
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.TicksVertical.Normal, 1);
			if (numTicks == 1)
			{
				TrackBarRenderer.visualStyleRenderer.DrawEdge(g, new Rectangle(bounds.X, bounds.Y, bounds.Width, 2), Edges.Top, edgeStyle, EdgeEffects.None);
				return;
			}
			float num = ((float)bounds.Height - 2f) / ((float)numTicks - 1f);
			while (numTicks > 0)
			{
				float num2 = (float)bounds.Y + (float)(numTicks - 1) * num;
				TrackBarRenderer.visualStyleRenderer.DrawEdge(g, new Rectangle(bounds.X, (int)Math.Round((double)num2), bounds.Width, 2), Edges.Top, edgeStyle, EdgeEffects.None);
				numTicks--;
			}
		}

		/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points to the left.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the slider.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06004848 RID: 18504 RVA: 0x0013035D File Offset: 0x0012E55D
		public static Size GetLeftPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbLeft.Normal, (int)state);
			return TrackBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points to the right.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the slider.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06004849 RID: 18505 RVA: 0x00130376 File Offset: 0x0012E576
		public static Size GetRightPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbRight.Normal, (int)state);
			return TrackBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points up.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the slider.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x0600484A RID: 18506 RVA: 0x0013038F File Offset: 0x0012E58F
		public static Size GetTopPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbTop.Normal, (int)state);
			return TrackBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points down.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x0600484B RID: 18507 RVA: 0x001303A8 File Offset: 0x0012E5A8
		public static Size GetBottomPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbBottom.Normal, (int)state);
			return TrackBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x001303C1 File Offset: 0x0012E5C1
		private static void InitializeRenderer(VisualStyleElement element, int state)
		{
			if (TrackBarRenderer.visualStyleRenderer == null)
			{
				TrackBarRenderer.visualStyleRenderer = new VisualStyleRenderer(element.ClassName, element.Part, state);
				return;
			}
			TrackBarRenderer.visualStyleRenderer.SetParameters(element.ClassName, element.Part, state);
		}

		// Token: 0x0400271A RID: 10010
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer;

		// Token: 0x0400271B RID: 10011
		private const int lineWidth = 2;
	}
}

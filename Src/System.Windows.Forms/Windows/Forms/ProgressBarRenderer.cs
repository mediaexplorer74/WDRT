using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a progress bar control with visual styles. This class cannot be inherited.</summary>
	// Token: 0x02000328 RID: 808
	public sealed class ProgressBarRenderer
	{
		// Token: 0x060033F7 RID: 13303 RVA: 0x00002843 File Offset: 0x00000A43
		private ProgressBarRenderer()
		{
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ProgressBarRenderer" /> class can be used to draw a progress bar control with visual styles.</summary>
		/// <returns>
		///   <see langword="true" /> if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060033F8 RID: 13304 RVA: 0x0002BBA7 File Offset: 0x00029DA7
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		/// <summary>Draws an empty progress bar control that fills in horizontally.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the progress bar.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the progress bar.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x060033F9 RID: 13305 RVA: 0x000EB981 File Offset: 0x000E9B81
		public static void DrawHorizontalBar(Graphics g, Rectangle bounds)
		{
			ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.Bar.Normal);
			ProgressBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws an empty progress bar control that fills in vertically.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the progress bar.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the progress bar.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x060033FA RID: 13306 RVA: 0x000EB999 File Offset: 0x000E9B99
		public static void DrawVerticalBar(Graphics g, Rectangle bounds)
		{
			ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.BarVertical.Normal);
			ProgressBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a set of progress bar pieces that fill a horizontal progress bar.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the progress bar.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds to be filled by progress bar pieces.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x060033FB RID: 13307 RVA: 0x000EB9B1 File Offset: 0x000E9BB1
		public static void DrawHorizontalChunks(Graphics g, Rectangle bounds)
		{
			ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
			ProgressBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a set of progress bar pieces that fill a vertical progress bar.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the progress bar.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds to be filled by progress bar pieces.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x060033FC RID: 13308 RVA: 0x000EB9C9 File Offset: 0x000E9BC9
		public static void DrawVerticalChunks(Graphics g, Rectangle bounds)
		{
			ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.ChunkVertical.Normal);
			ProgressBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Gets the width, in pixels, of a single inner piece of the progress bar.</summary>
		/// <returns>The width, in pixels, of a single inner piece of the progress bar.</returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060033FD RID: 13309 RVA: 0x000EB9E1 File Offset: 0x000E9BE1
		public static int ChunkThickness
		{
			get
			{
				ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
				return ProgressBarRenderer.visualStyleRenderer.GetInteger(IntegerProperty.ProgressChunkSize);
			}
		}

		/// <summary>Gets the width, in pixels, of the space between each inner piece of the progress bar.</summary>
		/// <returns>The width, in pixels, of the space between each inner piece of the progress bar.</returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.  
		///  -or-  
		///  Visual styles are disabled by the user in the operating system.  
		///  -or-  
		///  Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060033FE RID: 13310 RVA: 0x000EB9FC File Offset: 0x000E9BFC
		public static int ChunkSpaceThickness
		{
			get
			{
				ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
				return ProgressBarRenderer.visualStyleRenderer.GetInteger(IntegerProperty.ProgressSpaceSize);
			}
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000EBA17 File Offset: 0x000E9C17
		private static void InitializeRenderer(VisualStyleElement element)
		{
			if (ProgressBarRenderer.visualStyleRenderer == null)
			{
				ProgressBarRenderer.visualStyleRenderer = new VisualStyleRenderer(element);
				return;
			}
			ProgressBarRenderer.visualStyleRenderer.SetParameters(element);
		}

		// Token: 0x04001ECC RID: 7884
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer;
	}
}

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Provides information about the current visual style of the operating system.</summary>
	// Token: 0x02000453 RID: 1107
	public static class VisualStyleInformation
	{
		/// <summary>Gets a value indicating whether the operating system supports visual styles.</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system supports visual styles; otherwise, <see langword="false" />.</returns>
		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06004D67 RID: 19815 RVA: 0x0013FAFD File Offset: 0x0013DCFD
		public static bool IsSupportedByOS
		{
			get
			{
				return OSFeature.Feature.IsPresent(OSFeature.Themes);
			}
		}

		/// <summary>Gets a value indicating whether the user has enabled visual styles in the operating system.</summary>
		/// <returns>
		///   <see langword="true" /> if the user has enabled visual styles in an operating system that supports them; otherwise, <see langword="false" />.</returns>
		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06004D68 RID: 19816 RVA: 0x0013FB0E File Offset: 0x0013DD0E
		public static bool IsEnabledByUser
		{
			get
			{
				return VisualStyleInformation.IsSupportedByOS && SafeNativeMethods.IsAppThemed();
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06004D69 RID: 19817 RVA: 0x0013FB20 File Offset: 0x0013DD20
		internal static string ThemeFilename
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetCurrentThemeName(stringBuilder, stringBuilder.Capacity, null, 0, null, 0);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the color scheme of the current visual style.</summary>
		/// <returns>A string that specifies the color scheme of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06004D6A RID: 19818 RVA: 0x0013FB5C File Offset: 0x0013DD5C
		public static string ColorScheme
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetCurrentThemeName(null, 0, stringBuilder, stringBuilder.Capacity, null, 0);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a string that describes the size of the current visual style.</summary>
		/// <returns>A string that describes the size of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06004D6B RID: 19819 RVA: 0x0013FB98 File Offset: 0x0013DD98
		public static string Size
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetCurrentThemeName(null, 0, null, 0, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the display name of the current visual style.</summary>
		/// <returns>A string that specifies the display name of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06004D6C RID: 19820 RVA: 0x0013FBD4 File Offset: 0x0013DDD4
		public static string DisplayName
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.DisplayName, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the company that created the current visual style.</summary>
		/// <returns>A string that specifies the company that created the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06004D6D RID: 19821 RVA: 0x0013FC18 File Offset: 0x0013DE18
		public static string Company
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Company, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the author of the current visual style.</summary>
		/// <returns>A string that specifies the author of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06004D6E RID: 19822 RVA: 0x0013FC5C File Offset: 0x0013DE5C
		public static string Author
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Author, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the copyright of the current visual style.</summary>
		/// <returns>A string that specifies the copyright of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06004D6F RID: 19823 RVA: 0x0013FCA0 File Offset: 0x0013DEA0
		public static string Copyright
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Copyright, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a URL provided by the author of the current visual style.</summary>
		/// <returns>A string that specifies a URL provided by the author of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06004D70 RID: 19824 RVA: 0x0013FCE4 File Offset: 0x0013DEE4
		public static string Url
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Url, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the version of the current visual style.</summary>
		/// <returns>A string that indicates the version of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06004D71 RID: 19825 RVA: 0x0013FD28 File Offset: 0x0013DF28
		public static string Version
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Version, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a description of the current visual style.</summary>
		/// <returns>A string that describes the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06004D72 RID: 19826 RVA: 0x0013FD6C File Offset: 0x0013DF6C
		public static string Description
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Description, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a value indicating whether the current visual style supports flat menus.</summary>
		/// <returns>
		///   <see langword="true" /> if visual styles are enabled and the current visual style supports flat menus; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06004D73 RID: 19827 RVA: 0x0013FDB0 File Offset: 0x0013DFB0
		public static bool SupportsFlatMenus
		{
			get
			{
				if (Application.RenderWithVisualStyles)
				{
					if (VisualStyleInformation.visualStyleRenderer == null)
					{
						VisualStyleInformation.visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
					}
					else
					{
						VisualStyleInformation.visualStyleRenderer.SetParameters(VisualStyleElement.Window.Caption.Active);
					}
					return SafeNativeMethods.GetThemeSysBool(new HandleRef(null, VisualStyleInformation.visualStyleRenderer.Handle), VisualStyleSystemProperty.SupportsFlatMenus);
				}
				return false;
			}
		}

		/// <summary>Gets the minimum color depth for the current visual style.</summary>
		/// <returns>The minimum color depth for the current visual style if visual styles are enabled; otherwise, 0.</returns>
		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06004D74 RID: 19828 RVA: 0x0013FE08 File Offset: 0x0013E008
		public static int MinimumColorDepth
		{
			get
			{
				if (Application.RenderWithVisualStyles)
				{
					if (VisualStyleInformation.visualStyleRenderer == null)
					{
						VisualStyleInformation.visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
					}
					else
					{
						VisualStyleInformation.visualStyleRenderer.SetParameters(VisualStyleElement.Window.Caption.Active);
					}
					int num = 0;
					SafeNativeMethods.GetThemeSysInt(new HandleRef(null, VisualStyleInformation.visualStyleRenderer.Handle), VisualStyleSystemProperty.MinimumColorDepth, ref num);
					return num;
				}
				return 0;
			}
		}

		/// <summary>Gets the color that the current visual style uses to paint the borders of controls that contain text.</summary>
		/// <returns>If visual styles are enabled, the <see cref="T:System.Drawing.Color" /> that the current visual style uses to paint the borders of controls that contain text; otherwise, <see cref="P:System.Drawing.SystemColors.ControlDarkDark" />.</returns>
		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06004D75 RID: 19829 RVA: 0x0013FE68 File Offset: 0x0013E068
		public static Color TextControlBorder
		{
			get
			{
				if (Application.RenderWithVisualStyles)
				{
					if (VisualStyleInformation.visualStyleRenderer == null)
					{
						VisualStyleInformation.visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal);
					}
					else
					{
						VisualStyleInformation.visualStyleRenderer.SetParameters(VisualStyleElement.TextBox.TextEdit.Normal);
					}
					return VisualStyleInformation.visualStyleRenderer.GetColor(ColorProperty.BorderColor);
				}
				return SystemColors.WindowFrame;
			}
		}

		/// <summary>Gets the color that the current visual style uses to indicate the hot state of a control.</summary>
		/// <returns>If visual styles are enabled, the <see cref="T:System.Drawing.Color" /> used to paint a highlight on a control in the hot state; otherwise, <see cref="P:System.Drawing.SystemColors.ButtonHighlight" />.</returns>
		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06004D76 RID: 19830 RVA: 0x0013FEBC File Offset: 0x0013E0BC
		public static Color ControlHighlightHot
		{
			get
			{
				if (Application.RenderWithVisualStyles)
				{
					if (VisualStyleInformation.visualStyleRenderer == null)
					{
						VisualStyleInformation.visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
					}
					else
					{
						VisualStyleInformation.visualStyleRenderer.SetParameters(VisualStyleElement.Button.PushButton.Normal);
					}
					return VisualStyleInformation.visualStyleRenderer.GetColor(ColorProperty.AccentColorHint);
				}
				return SystemColors.ButtonHighlight;
			}
		}

		// Token: 0x0400324F RID: 12879
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer;
	}
}

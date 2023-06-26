using System;

namespace System.Windows.Forms
{
	/// <summary>Provides operating-system specific feature queries.</summary>
	// Token: 0x02000312 RID: 786
	public class OSFeature : FeatureSupport
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.OSFeature" /> class.</summary>
		// Token: 0x06003218 RID: 12824 RVA: 0x000E1622 File Offset: 0x000DF822
		protected OSFeature()
		{
		}

		/// <summary>Gets a <see langword="static" /> instance of the <see cref="T:System.Windows.Forms.OSFeature" /> class to use for feature queries. This property is read-only.</summary>
		/// <returns>An instance of the <see cref="T:System.Windows.Forms.OSFeature" /> class.</returns>
		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000E162A File Offset: 0x000DF82A
		public static OSFeature Feature
		{
			get
			{
				if (OSFeature.feature == null)
				{
					OSFeature.feature = new OSFeature();
				}
				return OSFeature.feature;
			}
		}

		/// <summary>Retrieves the version of the specified feature currently available on the system.</summary>
		/// <param name="feature">The feature whose version is requested, either <see cref="F:System.Windows.Forms.OSFeature.LayeredWindows" /> or <see cref="F:System.Windows.Forms.OSFeature.Themes" />.</param>
		/// <returns>A <see cref="T:System.Version" /> representing the version of the specified operating system feature currently available on the system; or <see langword="null" /> if the feature cannot be found.</returns>
		// Token: 0x0600321A RID: 12826 RVA: 0x000E1644 File Offset: 0x000DF844
		public override Version GetVersionPresent(object feature)
		{
			Version version = null;
			if (feature == OSFeature.LayeredWindows)
			{
				if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.CompareTo(new Version(5, 0, 0, 0)) >= 0)
				{
					version = new Version(0, 0, 0, 0);
				}
			}
			else if (feature == OSFeature.Themes)
			{
				if (!OSFeature.themeSupportTested)
				{
					try
					{
						SafeNativeMethods.IsAppThemed();
						OSFeature.themeSupport = true;
					}
					catch
					{
						OSFeature.themeSupport = false;
					}
					OSFeature.themeSupportTested = true;
				}
				if (OSFeature.themeSupport)
				{
					version = new Version(0, 0, 0, 0);
				}
			}
			return version;
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x000E16E0 File Offset: 0x000DF8E0
		internal bool OnXp
		{
			get
			{
				bool flag = false;
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					flag = Environment.OSVersion.Version.CompareTo(new Version(5, 1, 0, 0)) >= 0;
				}
				return flag;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000E171C File Offset: 0x000DF91C
		internal bool OnWin2k
		{
			get
			{
				bool flag = false;
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					flag = Environment.OSVersion.Version.CompareTo(new Version(5, 0, 0, 0)) >= 0;
				}
				return flag;
			}
		}

		/// <summary>Retrieves a value indicating whether the operating system supports the specified feature or metric.</summary>
		/// <param name="enumVal">A <see cref="T:System.Windows.Forms.SystemParameter" /> representing the feature to search for.</param>
		/// <returns>
		///   <see langword="true" /> if the feature is available on the system; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600321D RID: 12829 RVA: 0x000E1758 File Offset: 0x000DF958
		public static bool IsPresent(SystemParameter enumVal)
		{
			switch (enumVal)
			{
			case SystemParameter.DropShadow:
				return OSFeature.Feature.OnXp;
			case SystemParameter.FlatMenu:
				return OSFeature.Feature.OnXp;
			case SystemParameter.FontSmoothingContrastMetric:
				return OSFeature.Feature.OnXp;
			case SystemParameter.FontSmoothingTypeMetric:
				return OSFeature.Feature.OnXp;
			case SystemParameter.MenuFadeEnabled:
				return OSFeature.Feature.OnWin2k;
			case SystemParameter.SelectionFade:
				return OSFeature.Feature.OnWin2k;
			case SystemParameter.ToolTipAnimationMetric:
				return OSFeature.Feature.OnWin2k;
			case SystemParameter.UIEffects:
				return OSFeature.Feature.OnWin2k;
			case SystemParameter.CaretWidthMetric:
				return OSFeature.Feature.OnWin2k;
			case SystemParameter.VerticalFocusThicknessMetric:
				return OSFeature.Feature.OnXp;
			case SystemParameter.HorizontalFocusThicknessMetric:
				return OSFeature.Feature.OnXp;
			default:
				return false;
			}
		}

		/// <summary>Represents the layered, top-level windows feature. This field is read-only.</summary>
		// Token: 0x04001E58 RID: 7768
		public static readonly object LayeredWindows = new object();

		/// <summary>Represents the operating system themes feature. This field is read-only.</summary>
		// Token: 0x04001E59 RID: 7769
		public static readonly object Themes = new object();

		// Token: 0x04001E5A RID: 7770
		private static OSFeature feature = null;

		// Token: 0x04001E5B RID: 7771
		private static bool themeSupportTested = false;

		// Token: 0x04001E5C RID: 7772
		private static bool themeSupport = false;
	}
}

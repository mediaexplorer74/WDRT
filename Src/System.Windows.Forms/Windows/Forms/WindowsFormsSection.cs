using System;
using System.Configuration;

namespace System.Windows.Forms
{
	/// <summary>Defines a new <see cref="T:System.Configuration.ConfigurationSection" /> for parsing application settings. This class cannot be inherited.</summary>
	// Token: 0x02000441 RID: 1089
	public sealed class WindowsFormsSection : ConfigurationSection
	{
		// Token: 0x06004BAE RID: 19374 RVA: 0x0013AC50 File Offset: 0x00138E50
		internal static WindowsFormsSection GetSection()
		{
			WindowsFormsSection windowsFormsSection = null;
			try
			{
				windowsFormsSection = (WindowsFormsSection)PrivilegedConfigurationManager.GetSection("system.windows.forms");
			}
			catch
			{
				windowsFormsSection = new WindowsFormsSection();
			}
			return windowsFormsSection;
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x0013AC8C File Offset: 0x00138E8C
		private static ConfigurationPropertyCollection EnsureStaticPropertyBag()
		{
			if (WindowsFormsSection.s_properties == null)
			{
				WindowsFormsSection.s_propJitDebugging = new ConfigurationProperty("jitDebugging", typeof(bool), false, ConfigurationPropertyOptions.None);
				WindowsFormsSection.s_properties = new ConfigurationPropertyCollection { WindowsFormsSection.s_propJitDebugging };
			}
			return WindowsFormsSection.s_properties;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WindowsFormsSection" /> class.</summary>
		// Token: 0x06004BB0 RID: 19376 RVA: 0x0013ACDC File Offset: 0x00138EDC
		public WindowsFormsSection()
		{
			WindowsFormsSection.EnsureStaticPropertyBag();
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06004BB1 RID: 19377 RVA: 0x0013ACEA File Offset: 0x00138EEA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WindowsFormsSection.EnsureStaticPropertyBag();
			}
		}

		/// <summary>Gets or sets a value indicating whether just-in-time (JIT) debugging is used.</summary>
		/// <returns>
		///   <see langword="true" /> if JIT debugging is used; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06004BB2 RID: 19378 RVA: 0x0013ACF1 File Offset: 0x00138EF1
		// (set) Token: 0x06004BB3 RID: 19379 RVA: 0x0013AD03 File Offset: 0x00138F03
		[ConfigurationProperty("jitDebugging", DefaultValue = false)]
		public bool JitDebugging
		{
			get
			{
				return (bool)base[WindowsFormsSection.s_propJitDebugging];
			}
			set
			{
				base[WindowsFormsSection.s_propJitDebugging] = value;
			}
		}

		// Token: 0x04002830 RID: 10288
		internal const bool JitDebuggingDefault = false;

		// Token: 0x04002831 RID: 10289
		private static ConfigurationPropertyCollection s_properties;

		// Token: 0x04002832 RID: 10290
		private static ConfigurationProperty s_propJitDebugging;
	}
}

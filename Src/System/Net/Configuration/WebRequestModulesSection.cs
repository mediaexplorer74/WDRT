using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web request modules. This class cannot be inherited.</summary>
	// Token: 0x0200034C RID: 844
	public sealed class WebRequestModulesSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModulesSection" /> class.</summary>
		// Token: 0x06001E3C RID: 7740 RVA: 0x0008D993 File Offset: 0x0008BB93
		public WebRequestModulesSection()
		{
			this.properties.Add(this.webRequestModules);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0008D9D0 File Offset: 0x0008BBD0
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			try
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_config_section_permission", new object[] { "webRequestModules" }), ex);
			}
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x0008DA28 File Offset: 0x0008BC28
		protected override void InitializeDefault()
		{
			this.WebRequestModules.Add(new WebRequestModuleElement("https:", typeof(HttpRequestCreator)));
			this.WebRequestModules.Add(new WebRequestModuleElement("http:", typeof(HttpRequestCreator)));
			this.WebRequestModules.Add(new WebRequestModuleElement("file:", typeof(FileWebRequestCreator)));
			this.WebRequestModules.Add(new WebRequestModuleElement("ftp:", typeof(FtpWebRequestCreator)));
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x0008DAB1 File Offset: 0x0008BCB1
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets the collection of Web request modules in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebRequestModuleElementCollection" /> containing the registered Web request modules.</returns>
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x0008DAB9 File Offset: 0x0008BCB9
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public WebRequestModuleElementCollection WebRequestModules
		{
			get
			{
				return (WebRequestModuleElementCollection)base[this.webRequestModules];
			}
		}

		// Token: 0x04001CA6 RID: 7334
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001CA7 RID: 7335
		private readonly ConfigurationProperty webRequestModules = new ConfigurationProperty(null, typeof(WebRequestModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}

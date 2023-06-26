using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure Web proxy scripts. This class cannot be inherited.</summary>
	// Token: 0x02000349 RID: 841
	public sealed class WebProxyScriptElement : ConfigurationElement
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Net.Configuration.WebProxyScriptElement" /> class.</summary>
		// Token: 0x06001E1F RID: 7711 RVA: 0x0008D64C File Offset: 0x0008B84C
		public WebProxyScriptElement()
		{
			this.properties.Add(this.autoConfigUrlRetryInterval);
			this.properties.Add(this.downloadTimeout);
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0008D6FC File Offset: 0x0008B8FC
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
				throw new ConfigurationErrorsException(SR.GetString("net_config_element_permission", new object[] { "webProxyScript" }), ex);
			}
		}

		/// <summary>Gets or sets a value that defines the frequency (in seconds) that the WinHttpAutoProxySvc service attempts to retry the download of an AutoConfigUrl script.</summary>
		/// <returns>the frequency (in seconds) that the WinHttpAutoProxySvc service attempts to retry the download of an AutoConfigUrl script.</returns>
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0008D754 File Offset: 0x0008B954
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x0008D767 File Offset: 0x0008B967
		[ConfigurationProperty("autoConfigUrlRetryInterval", DefaultValue = 600)]
		public int AutoConfigUrlRetryInterval
		{
			get
			{
				return (int)base[this.autoConfigUrlRetryInterval];
			}
			set
			{
				base[this.autoConfigUrlRetryInterval] = value;
			}
		}

		/// <summary>Gets or sets the Web proxy script download timeout using the format hours:minutes:seconds.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object that contains the timeout value. The default download timeout is one minute.</returns>
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x0008D77B File Offset: 0x0008B97B
		// (set) Token: 0x06001E24 RID: 7716 RVA: 0x0008D78E File Offset: 0x0008B98E
		[ConfigurationProperty("downloadTimeout", DefaultValue = "00:01:00")]
		public TimeSpan DownloadTimeout
		{
			get
			{
				return (TimeSpan)base[this.downloadTimeout];
			}
			set
			{
				base[this.downloadTimeout] = value;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x0008D7A2 File Offset: 0x0008B9A2
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001CA0 RID: 7328
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001CA1 RID: 7329
		private readonly ConfigurationProperty autoConfigUrlRetryInterval = new ConfigurationProperty("autoConfigUrlRetryInterval", typeof(int), 600, null, new WebProxyScriptElement.RetryIntervalValidator(), ConfigurationPropertyOptions.None);

		// Token: 0x04001CA2 RID: 7330
		private readonly ConfigurationProperty downloadTimeout = new ConfigurationProperty("downloadTimeout", typeof(TimeSpan), TimeSpan.FromMinutes(1.0), null, new TimeSpanValidator(new TimeSpan(0, 0, 0), TimeSpan.MaxValue, false), ConfigurationPropertyOptions.None);

		// Token: 0x020007C6 RID: 1990
		private class RetryIntervalValidator : ConfigurationValidatorBase
		{
			// Token: 0x0600436D RID: 17261 RVA: 0x0011C302 File Offset: 0x0011A502
			public override bool CanValidate(Type type)
			{
				return type == typeof(int);
			}

			// Token: 0x0600436E RID: 17262 RVA: 0x0011C314 File Offset: 0x0011A514
			public override void Validate(object value)
			{
				int num = (int)value;
				if (num < 0)
				{
					throw new ArgumentOutOfRangeException("value", num, SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[] { 0, int.MaxValue }));
				}
			}
		}
	}
}

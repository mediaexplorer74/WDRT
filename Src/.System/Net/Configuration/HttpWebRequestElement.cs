using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the maximum length for response headers. This class cannot be inherited.</summary>
	// Token: 0x02000331 RID: 817
	public sealed class HttpWebRequestElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.HttpWebRequestElement" /> class.</summary>
		// Token: 0x06001D32 RID: 7474 RVA: 0x0008B300 File Offset: 0x00089500
		public HttpWebRequestElement()
		{
			this.properties.Add(this.maximumResponseHeadersLength);
			this.properties.Add(this.maximumErrorResponseLength);
			this.properties.Add(this.maximumUnauthorizedUploadLength);
			this.properties.Add(this.useUnsafeHeaderParsing);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0008B3E8 File Offset: 0x000895E8
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			PropertyInformation[] array = new PropertyInformation[]
			{
				base.ElementInformation.Properties["maximumResponseHeadersLength"],
				base.ElementInformation.Properties["maximumErrorResponseLength"]
			};
			foreach (PropertyInformation propertyInformation in array)
			{
				if (propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere)
				{
					try
					{
						ExceptionHelper.WebPermissionUnrestricted.Demand();
					}
					catch (Exception ex)
					{
						throw new ConfigurationErrorsException(SR.GetString("net_config_property_permission", new object[] { propertyInformation.Name }), ex);
					}
				}
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x0008B498 File Offset: 0x00089698
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the maximum length of an upload in response to an unauthorized error code.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length (in multiple of 1,024 byte units) of an upload in response to an unauthorized error code. A value of -1 indicates that no size limit will be imposed on the upload. Setting the <see cref="P:System.Net.Configuration.HttpWebRequestElement.MaximumUnauthorizedUploadLength" /> property to any other value will only send the request body if it is smaller than the number of bytes specified. So a value of 1 would indicate to only send the request body if it is smaller than 1,024 bytes.  
		///  The default value for this property is -1.</returns>
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0008B4A0 File Offset: 0x000896A0
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x0008B4B3 File Offset: 0x000896B3
		[ConfigurationProperty("maximumUnauthorizedUploadLength", DefaultValue = -1)]
		public int MaximumUnauthorizedUploadLength
		{
			get
			{
				return (int)base[this.maximumUnauthorizedUploadLength];
			}
			set
			{
				base[this.maximumUnauthorizedUploadLength] = value;
			}
		}

		/// <summary>Gets or sets the maximum allowed length of an error response.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length in kilobytes (1024 bytes) of the error response. The default value is 64.</returns>
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0008B4C7 File Offset: 0x000896C7
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x0008B4DA File Offset: 0x000896DA
		[ConfigurationProperty("maximumErrorResponseLength", DefaultValue = 64)]
		public int MaximumErrorResponseLength
		{
			get
			{
				return (int)base[this.maximumErrorResponseLength];
			}
			set
			{
				base[this.maximumErrorResponseLength] = value;
			}
		}

		/// <summary>Gets or sets the maximum allowed length of the response headers.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length in kilobytes (1024 bytes) of the response headers. The default value is 64.</returns>
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0008B4EE File Offset: 0x000896EE
		// (set) Token: 0x06001D3A RID: 7482 RVA: 0x0008B501 File Offset: 0x00089701
		[ConfigurationProperty("maximumResponseHeadersLength", DefaultValue = 64)]
		public int MaximumResponseHeadersLength
		{
			get
			{
				return (int)base[this.maximumResponseHeadersLength];
			}
			set
			{
				base[this.maximumResponseHeadersLength] = value;
			}
		}

		/// <summary>Setting this property ignores validation errors that occur during HTTP parsing.</summary>
		/// <returns>Boolean that indicates whether this property has been set.</returns>
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0008B515 File Offset: 0x00089715
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x0008B528 File Offset: 0x00089728
		[ConfigurationProperty("useUnsafeHeaderParsing", DefaultValue = false)]
		public bool UseUnsafeHeaderParsing
		{
			get
			{
				return (bool)base[this.useUnsafeHeaderParsing];
			}
			set
			{
				base[this.useUnsafeHeaderParsing] = value;
			}
		}

		// Token: 0x04001C1C RID: 7196
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C1D RID: 7197
		private readonly ConfigurationProperty maximumResponseHeadersLength = new ConfigurationProperty("maximumResponseHeadersLength", typeof(int), 64, ConfigurationPropertyOptions.None);

		// Token: 0x04001C1E RID: 7198
		private readonly ConfigurationProperty maximumErrorResponseLength = new ConfigurationProperty("maximumErrorResponseLength", typeof(int), 64, ConfigurationPropertyOptions.None);

		// Token: 0x04001C1F RID: 7199
		private readonly ConfigurationProperty maximumUnauthorizedUploadLength = new ConfigurationProperty("maximumUnauthorizedUploadLength", typeof(int), -1, ConfigurationPropertyOptions.None);

		// Token: 0x04001C20 RID: 7200
		private readonly ConfigurationProperty useUnsafeHeaderParsing = new ConfigurationProperty("useUnsafeHeaderParsing", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}

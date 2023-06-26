using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the HttpListener element in the configuration file. This class cannot be inherited.</summary>
	// Token: 0x02000332 RID: 818
	public sealed class HttpListenerElement : ConfigurationElement
	{
		// Token: 0x06001D3D RID: 7485 RVA: 0x0008B53C File Offset: 0x0008973C
		static HttpListenerElement()
		{
			HttpListenerElement.properties.Add(HttpListenerElement.unescapeRequestUrl);
			HttpListenerElement.properties.Add(HttpListenerElement.timeouts);
		}

		/// <summary>Gets a value that indicates if <see cref="T:System.Net.HttpListener" /> uses the raw unescaped URI instead of the converted URI.</summary>
		/// <returns>A Boolean value that indicates if <see cref="T:System.Net.HttpListener" /> uses the raw unescaped URI, rather than the converted URI.</returns>
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x0008B5AC File Offset: 0x000897AC
		[ConfigurationProperty("unescapeRequestUrl", DefaultValue = true, IsRequired = false)]
		public bool UnescapeRequestUrl
		{
			get
			{
				return (bool)base[HttpListenerElement.unescapeRequestUrl];
			}
		}

		/// <summary>Gets the default timeout elements used for an <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>The timeout elements used for an <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0008B5BE File Offset: 0x000897BE
		[ConfigurationProperty("timeouts")]
		public HttpListenerTimeoutsElement Timeouts
		{
			get
			{
				return (HttpListenerTimeoutsElement)base[HttpListenerElement.timeouts];
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x0008B5D0 File Offset: 0x000897D0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpListenerElement.properties;
			}
		}

		// Token: 0x04001C21 RID: 7201
		internal const bool UnescapeRequestUrlDefaultValue = true;

		// Token: 0x04001C22 RID: 7202
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C23 RID: 7203
		private static readonly ConfigurationProperty unescapeRequestUrl = new ConfigurationProperty("unescapeRequestUrl", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04001C24 RID: 7204
		private static readonly ConfigurationProperty timeouts = new ConfigurationProperty("timeouts", typeof(HttpListenerTimeoutsElement), null, ConfigurationPropertyOptions.None);
	}
}

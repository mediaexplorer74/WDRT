using System;

namespace System.Net
{
	/// <summary>Contains a global default proxy instance for all HTTP requests.</summary>
	// Token: 0x020000F2 RID: 242
	[Obsolete("This class has been deprecated. Please use WebRequest.DefaultWebProxy instead to access and set the global default proxy. Use 'null' instead of GetEmptyWebProxy. http://go.microsoft.com/fwlink/?linkid=14202")]
	public class GlobalProxySelection
	{
		/// <summary>Gets or sets the global HTTP proxy.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that every call to <see cref="M:System.Net.HttpWebRequest.GetResponse" /> uses.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation was <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission for the requested operation.</exception>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0002F4A4 File Offset: 0x0002D6A4
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x0002F4D2 File Offset: 0x0002D6D2
		public static IWebProxy Select
		{
			get
			{
				IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
				if (defaultWebProxy == null)
				{
					return GlobalProxySelection.GetEmptyWebProxy();
				}
				WebRequest.WebProxyWrapper webProxyWrapper = defaultWebProxy as WebRequest.WebProxyWrapper;
				if (webProxyWrapper != null)
				{
					return webProxyWrapper.WebProxy;
				}
				return defaultWebProxy;
			}
			set
			{
				WebRequest.DefaultWebProxy = value;
			}
		}

		/// <summary>Returns an empty proxy instance.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that contains no information.</returns>
		// Token: 0x06000876 RID: 2166 RVA: 0x0002F4DA File Offset: 0x0002D6DA
		public static IWebProxy GetEmptyWebProxy()
		{
			return new EmptyWebProxy();
		}
	}
}

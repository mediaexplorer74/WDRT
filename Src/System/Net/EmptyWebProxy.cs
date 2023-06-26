using System;

namespace System.Net
{
	// Token: 0x020001AE RID: 430
	[Serializable]
	internal sealed class EmptyWebProxy : IAutoWebProxy, IWebProxy
	{
		// Token: 0x060010E5 RID: 4325 RVA: 0x0005B906 File Offset: 0x00059B06
		public Uri GetProxy(Uri uri)
		{
			return uri;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0005B909 File Offset: 0x00059B09
		public bool IsBypassed(Uri uri)
		{
			return true;
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0005B90C File Offset: 0x00059B0C
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x0005B914 File Offset: 0x00059B14
		public ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			set
			{
				this.m_credentials = value;
			}
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0005B91D File Offset: 0x00059B1D
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			return new DirectProxy(destination);
		}

		// Token: 0x040013D9 RID: 5081
		[NonSerialized]
		private ICredentials m_credentials;
	}
}

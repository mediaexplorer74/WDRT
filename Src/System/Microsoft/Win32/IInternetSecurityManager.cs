using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32
{
	// Token: 0x02000028 RID: 40
	[ComVisible(false)]
	[Guid("79eac9ee-baf9-11ce-8c82-00aa004ba90b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IInternetSecurityManager
	{
		// Token: 0x06000275 RID: 629
		unsafe void SetSecuritySite(void* pSite);

		// Token: 0x06000276 RID: 630
		unsafe void GetSecuritySite(void** ppSite);

		// Token: 0x06000277 RID: 631
		[SuppressUnmanagedCodeSecurity]
		void MapUrlToZone([MarshalAs(UnmanagedType.BStr)] [In] string pwszUrl, out int pdwZone, [In] int dwFlags);

		// Token: 0x06000278 RID: 632
		unsafe void GetSecurityId(string pwszUrl, byte* pbSecurityId, int* pcbSecurityId, int dwReserved);

		// Token: 0x06000279 RID: 633
		unsafe void ProcessUrlAction(string pwszUrl, int dwAction, byte* pPolicy, int cbPolicy, byte* pContext, int cbContext, int dwFlags, int dwReserved);

		// Token: 0x0600027A RID: 634
		unsafe void QueryCustomPolicy(string pwszUrl, void* guidKey, byte** ppPolicy, int* pcbPolicy, byte* pContext, int cbContext, int dwReserved);

		// Token: 0x0600027B RID: 635
		void SetZoneMapping(int dwZone, string lpszPattern, int dwFlags);

		// Token: 0x0600027C RID: 636
		unsafe void GetZoneMappings(int dwZone, void** ppenumString, int dwFlags);
	}
}

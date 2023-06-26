using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000133 RID: 307
	public static class ODataMessageExtensions
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x0001A89C File Offset: 0x00018A9C
		public static ODataVersion GetDataServiceVersion(this IODataResponseMessage message, ODataVersion defaultVersion)
		{
			ODataMessage odataMessage = new ODataResponseMessage(message, false, false, long.MaxValue);
			return ODataUtilsInternal.GetDataServiceVersion(odataMessage, defaultVersion);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001A8C4 File Offset: 0x00018AC4
		public static ODataVersion GetDataServiceVersion(this IODataRequestMessage message, ODataVersion defaultVersion)
		{
			ODataMessage odataMessage = new ODataRequestMessage(message, false, false, long.MaxValue);
			return ODataUtilsInternal.GetDataServiceVersion(odataMessage, defaultVersion);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001A8EA File Offset: 0x00018AEA
		public static ODataPreferenceHeader PreferHeader(this IODataRequestMessage requestMessage)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			return new ODataPreferenceHeader(requestMessage);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001A8FD File Offset: 0x00018AFD
		public static ODataPreferenceHeader PreferenceAppliedHeader(this IODataResponseMessage responseMessage)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			return new ODataPreferenceHeader(responseMessage);
		}
	}
}

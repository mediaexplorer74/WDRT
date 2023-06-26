using System;
using System.Diagnostics;

namespace Microsoft.Data.OData
{
	// Token: 0x0200023A RID: 570
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataInnerError
	{
		// Token: 0x06001246 RID: 4678 RVA: 0x00044F0F File Offset: 0x0004310F
		public ODataInnerError()
		{
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00044F18 File Offset: 0x00043118
		public ODataInnerError(Exception exception)
		{
			ExceptionUtils.CheckArgumentNotNull<Exception>(exception, "exception");
			this.Message = exception.Message ?? string.Empty;
			this.TypeName = exception.GetType().FullName;
			this.StackTrace = exception.StackTrace;
			if (exception.InnerException != null)
			{
				this.InnerError = new ODataInnerError(exception.InnerException);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x00044F81 File Offset: 0x00043181
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x00044F89 File Offset: 0x00043189
		public string Message { get; set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00044F92 File Offset: 0x00043192
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x00044F9A File Offset: 0x0004319A
		public string TypeName { get; set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00044FA3 File Offset: 0x000431A3
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x00044FAB File Offset: 0x000431AB
		public string StackTrace { get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x00044FB4 File Offset: 0x000431B4
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x00044FBC File Offset: 0x000431BC
		public ODataInnerError InnerError { get; set; }
	}
}

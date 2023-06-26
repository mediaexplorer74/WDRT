using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020002B7 RID: 695
	internal static class Error
	{
		// Token: 0x06001B72 RID: 7026 RVA: 0x0005BFF7 File Offset: 0x0005A1F7
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0005BFFF File Offset: 0x0005A1FF
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0005C007 File Offset: 0x0005A207
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0005C00E File Offset: 0x0005A20E
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}

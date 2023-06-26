using System;

namespace Microsoft.Data.Edm
{
	// Token: 0x02000244 RID: 580
	internal static class Error
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x0002C393 File Offset: 0x0002A593
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0002C39B File Offset: 0x0002A59B
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0002C3AA File Offset: 0x0002A5AA
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}

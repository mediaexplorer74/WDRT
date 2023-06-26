using System;

namespace System.Spatial
{
	// Token: 0x0200008F RID: 143
	internal static class Error
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0000A027 File Offset: 0x00008227
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000A02F File Offset: 0x0000822F
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000A037 File Offset: 0x00008237
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000A03E File Offset: 0x0000823E
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}

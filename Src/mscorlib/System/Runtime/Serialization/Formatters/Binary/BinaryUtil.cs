using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200077D RID: 1917
	internal static class BinaryUtil
	{
		// Token: 0x060053D6 RID: 21462 RVA: 0x00128585 File Offset: 0x00126785
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, string value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x00128592 File Offset: 0x00126792
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, object value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}
	}
}

using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000762 RID: 1890
	internal static class SerTrace
	{
		// Token: 0x0600532A RID: 21290 RVA: 0x0012541A File Offset: 0x0012361A
		[Conditional("_LOGGING")]
		internal static void InfoLog(params object[] messages)
		{
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x0012541C File Offset: 0x0012361C
		[Conditional("SER_LOGGING")]
		internal static void Log(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			int num = 0;
			object obj = messages[0];
			messages[num] = ((obj != null) ? obj.ToString() : null) + " ";
		}
	}
}

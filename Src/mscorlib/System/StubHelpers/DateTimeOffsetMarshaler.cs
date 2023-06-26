using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000593 RID: 1427
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class DateTimeOffsetMarshaler
	{
		// Token: 0x060042F0 RID: 17136 RVA: 0x000FAE7C File Offset: 0x000F907C
		[SecurityCritical]
		internal static void ConvertToNative(ref DateTimeOffset managedDTO, out DateTimeNative dateTime)
		{
			long utcTicks = managedDTO.UtcTicks;
			dateTime.UniversalTime = utcTicks - 504911232000000000L;
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x000FAEA4 File Offset: 0x000F90A4
		[SecurityCritical]
		internal static void ConvertToManaged(out DateTimeOffset managedLocalDTO, ref DateTimeNative nativeTicks)
		{
			long num = 504911232000000000L + nativeTicks.UniversalTime;
			DateTimeOffset dateTimeOffset = new DateTimeOffset(num, TimeSpan.Zero);
			managedLocalDTO = dateTimeOffset.ToLocalTime(true);
		}

		// Token: 0x04001BE0 RID: 7136
		private const long ManagedUtcTicksAtNativeZero = 504911232000000000L;
	}
}

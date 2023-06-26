using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x0200010C RID: 268
	internal static class Mda
	{
		// Token: 0x0600104B RID: 4171
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportStreamWriterBufferedDataLost(string text);

		// Token: 0x0600104C RID: 4172
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsStreamWriterBufferedDataLostEnabled();

		// Token: 0x0600104D RID: 4173
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsStreamWriterBufferedDataLostCaptureAllocatedCallStack();

		// Token: 0x0600104E RID: 4174
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void MemberInfoCacheCreation();

		// Token: 0x0600104F RID: 4175
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DateTimeInvalidLocalFormat();

		// Token: 0x06001050 RID: 4176
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInvalidGCHandleCookieProbeEnabled();

		// Token: 0x06001051 RID: 4177
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FireInvalidGCHandleCookieProbe(IntPtr cookie);

		// Token: 0x06001052 RID: 4178
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportErrorSafeHandleRelease(Exception ex);

		// Token: 0x02000AEF RID: 2799
		internal static class StreamWriterBufferedDataLost
		{
			// Token: 0x170011EF RID: 4591
			// (get) Token: 0x06006A28 RID: 27176 RVA: 0x0016EB6E File Offset: 0x0016CD6E
			internal static bool Enabled
			{
				[SecuritySafeCritical]
				get
				{
					if (Mda.StreamWriterBufferedDataLost._enabledState == 0)
					{
						if (Mda.IsStreamWriterBufferedDataLostEnabled())
						{
							Mda.StreamWriterBufferedDataLost._enabledState = 1;
						}
						else
						{
							Mda.StreamWriterBufferedDataLost._enabledState = 2;
						}
					}
					return Mda.StreamWriterBufferedDataLost._enabledState == 1;
				}
			}

			// Token: 0x170011F0 RID: 4592
			// (get) Token: 0x06006A29 RID: 27177 RVA: 0x0016EB9C File Offset: 0x0016CD9C
			internal static bool CaptureAllocatedCallStack
			{
				[SecuritySafeCritical]
				get
				{
					if (Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState == 0)
					{
						if (Mda.IsStreamWriterBufferedDataLostCaptureAllocatedCallStack())
						{
							Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState = 1;
						}
						else
						{
							Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState = 2;
						}
					}
					return Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState == 1;
				}
			}

			// Token: 0x06006A2A RID: 27178 RVA: 0x0016EBCA File Offset: 0x0016CDCA
			[SecuritySafeCritical]
			internal static void ReportError(string text)
			{
				Mda.ReportStreamWriterBufferedDataLost(text);
			}

			// Token: 0x040031B5 RID: 12725
			private static volatile int _enabledState;

			// Token: 0x040031B6 RID: 12726
			private static volatile int _captureAllocatedCallStackState;
		}
	}
}

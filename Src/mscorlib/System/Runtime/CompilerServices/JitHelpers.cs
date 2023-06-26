using System;
using System.Security;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008D5 RID: 2261
	[FriendAccessAllowed]
	internal static class JitHelpers
	{
		// Token: 0x06005DDF RID: 24031 RVA: 0x0014B084 File Offset: 0x00149284
		[SecurityCritical]
		internal static StringHandleOnStack GetStringHandleOnStack(ref string s)
		{
			return new StringHandleOnStack(JitHelpers.UnsafeCastToStackPointer<string>(ref s));
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x0014B091 File Offset: 0x00149291
		[SecurityCritical]
		internal static ObjectHandleOnStack GetObjectHandleOnStack<T>(ref T o) where T : class
		{
			return new ObjectHandleOnStack(JitHelpers.UnsafeCastToStackPointer<T>(ref o));
		}

		// Token: 0x06005DE1 RID: 24033 RVA: 0x0014B09E File Offset: 0x0014929E
		[SecurityCritical]
		internal static StackCrawlMarkHandle GetStackCrawlMarkHandle(ref StackCrawlMark stackMark)
		{
			return new StackCrawlMarkHandle(JitHelpers.UnsafeCastToStackPointer<StackCrawlMark>(ref stackMark));
		}

		// Token: 0x06005DE2 RID: 24034 RVA: 0x0014B0AB File Offset: 0x001492AB
		[SecurityCritical]
		[FriendAccessAllowed]
		internal static T UnsafeCast<T>(object o) where T : class
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005DE3 RID: 24035 RVA: 0x0014B0B2 File Offset: 0x001492B2
		internal static int UnsafeEnumCast<T>(T val) where T : struct
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005DE4 RID: 24036 RVA: 0x0014B0B9 File Offset: 0x001492B9
		internal static long UnsafeEnumCastLong<T>(T val) where T : struct
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005DE5 RID: 24037 RVA: 0x0014B0C0 File Offset: 0x001492C0
		[SecurityCritical]
		internal static IntPtr UnsafeCastToStackPointer<T>(ref T val)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005DE6 RID: 24038
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UnsafeSetArrayElement(object[] target, int index, object element);

		// Token: 0x06005DE7 RID: 24039 RVA: 0x0014B0C7 File Offset: 0x001492C7
		[SecurityCritical]
		internal static PinningHelper GetPinningHelper(object o)
		{
			return JitHelpers.UnsafeCast<PinningHelper>(o);
		}

		// Token: 0x04002A40 RID: 10816
		internal const string QCall = "QCall";
	}
}

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

namespace System.Net
{
	// Token: 0x020001C3 RID: 451
	internal static class GlobalLog
	{
		// Token: 0x060011B2 RID: 4530 RVA: 0x0006004A File Offset: 0x0005E24A
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		private static BaseLoggingObject LoggingInitialize()
		{
			return new BaseLoggingObject();
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00060051 File Offset: 0x0005E251
		internal static ThreadKinds CurrentThreadKind
		{
			get
			{
				return ThreadKinds.Unknown;
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00060054 File Offset: 0x0005E254
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void SetThreadSource(ThreadKinds source)
		{
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00060056 File Offset: 0x0005E256
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void ThreadContract(ThreadKinds kind, string errorMsg)
		{
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00060058 File Offset: 0x0005E258
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void ThreadContract(ThreadKinds kind, ThreadKinds allowedSources, string errorMsg)
		{
			if ((kind & ThreadKinds.SourceMask) != ThreadKinds.Unknown || (allowedSources & ThreadKinds.SourceMask) != allowedSources)
			{
				throw new InternalException();
			}
			ThreadKinds currentThreadKind = GlobalLog.CurrentThreadKind;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00060084 File Offset: 0x0005E284
		[Conditional("TRAVE")]
		public static void AddToArray(string msg)
		{
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00060086 File Offset: 0x0005E286
		[Conditional("TRAVE")]
		public static void Ignore(object msg)
		{
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00060088 File Offset: 0x0005E288
		[Conditional("TRAVE")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Print(string msg)
		{
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0006008A File Offset: 0x0005E28A
		[Conditional("TRAVE")]
		public static void PrintHex(string msg, object value)
		{
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0006008C File Offset: 0x0005E28C
		[Conditional("TRAVE")]
		public static void Enter(string func)
		{
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0006008E File Offset: 0x0005E28E
		[Conditional("TRAVE")]
		public static void Enter(string func, string parms)
		{
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00060090 File Offset: 0x0005E290
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Assert(bool condition, string messageFormat, params object[] data)
		{
			if (!condition)
			{
				string text = string.Format(CultureInfo.InvariantCulture, messageFormat, data);
				int num = text.IndexOf('|');
				if (num != -1)
				{
					int num2 = text.Length - num - 1;
				}
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000600C5 File Offset: 0x0005E2C5
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Assert(string message)
		{
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000600C8 File Offset: 0x0005E2C8
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Assert(string message, string detailMessage)
		{
			try
			{
				GlobalLog.Logobject.DumpArray(false);
			}
			finally
			{
				UnsafeNclNativeMethods.DebugBreak();
				Debugger.Break();
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00060100 File Offset: 0x0005E300
		[Conditional("TRAVE")]
		public static void LeaveException(string func, Exception exception)
		{
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00060102 File Offset: 0x0005E302
		[Conditional("TRAVE")]
		public static void Leave(string func)
		{
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00060104 File Offset: 0x0005E304
		[Conditional("TRAVE")]
		public static void Leave(string func, string result)
		{
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00060106 File Offset: 0x0005E306
		[Conditional("TRAVE")]
		public static void Leave(string func, int returnval)
		{
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00060108 File Offset: 0x0005E308
		[Conditional("TRAVE")]
		public static void Leave(string func, bool returnval)
		{
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0006010A File Offset: 0x0005E30A
		[Conditional("TRAVE")]
		public static void DumpArray()
		{
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0006010C File Offset: 0x0005E30C
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer)
		{
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0006010E File Offset: 0x0005E30E
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00060110 File Offset: 0x0005E310
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00060112 File Offset: 0x0005E312
		[Conditional("TRAVE")]
		public static void Dump(IntPtr buffer, int offset, int length)
		{
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00060114 File Offset: 0x0005E314
		[Conditional("DEBUG")]
		internal static void DebugAddRequest(HttpWebRequest request, Connection connection, int flags)
		{
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00060116 File Offset: 0x0005E316
		[Conditional("DEBUG")]
		internal static void DebugRemoveRequest(HttpWebRequest request)
		{
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00060118 File Offset: 0x0005E318
		[Conditional("DEBUG")]
		internal static void DebugUpdateRequest(HttpWebRequest request, Connection connection, int flags)
		{
		}

		// Token: 0x04001465 RID: 5221
		private static BaseLoggingObject Logobject = GlobalLog.LoggingInitialize();
	}
}

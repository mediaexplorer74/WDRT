using System;
using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.Win32
{
	// Token: 0x02000018 RID: 24
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x0600014F RID: 335
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int GetTimeZoneInformation(out Win32Native.TimeZoneInformation lpTimeZoneInformation);

		// Token: 0x06000150 RID: 336
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int GetDynamicTimeZoneInformation(out Win32Native.DynamicTimeZoneInformation lpDynamicTimeZoneInformation);

		// Token: 0x06000151 RID: 337
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetFileMUIPath(int flags, [MarshalAs(UnmanagedType.LPWStr)] string filePath, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder language, ref int languageLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder fileMuiPath, ref int fileMuiPathLength, ref long enumerator);

		// Token: 0x06000152 RID: 338
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "LoadStringW", ExactSpelling = true, SetLastError = true)]
		internal static extern int LoadString(SafeLibraryHandle handle, int id, StringBuilder buffer, int bufferLength);

		// Token: 0x06000153 RID: 339
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeLibraryHandle LoadLibraryEx(string libFilename, IntPtr reserved, int flags);

		// Token: 0x06000154 RID: 340
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x06000155 RID: 341
		[SecurityCritical]
		[DllImport("combase.dll")]
		internal static extern int RoGetActivationFactory([MarshalAs(UnmanagedType.HString)] string activatableClassId, [In] ref Guid iid, [MarshalAs(UnmanagedType.IInspectable)] out object factory);

		// Token: 0x02000ABC RID: 2748
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		internal static class ManifestEtw
		{
			// Token: 0x060069A9 RID: 27049
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern uint EventRegister([In] ref Guid providerId, [In] UnsafeNativeMethods.ManifestEtw.EtwEnableCallback enableCallback, [In] void* callbackContext, [In] [Out] ref long registrationHandle);

			// Token: 0x060069AA RID: 27050
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal static extern uint EventUnregister([In] long registrationHandle);

			// Token: 0x060069AB RID: 27051
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern int EventWrite([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] int userDataCount, [In] EventProvider.EventData* userData);

			// Token: 0x060069AC RID: 27052
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal static extern int EventWriteString([In] long registrationHandle, [In] byte level, [In] long keyword, [In] string msg);

			// Token: 0x060069AD RID: 27053 RVA: 0x0016D070 File Offset: 0x0016B270
			internal unsafe static int EventWriteTransferWrapper(long registrationHandle, ref EventDescriptor eventDescriptor, Guid* activityId, Guid* relatedActivityId, int userDataCount, EventProvider.EventData* userData)
			{
				int num = UnsafeNativeMethods.ManifestEtw.EventWriteTransfer(registrationHandle, ref eventDescriptor, activityId, relatedActivityId, userDataCount, userData);
				if (num == 87 && relatedActivityId == null)
				{
					Guid empty = Guid.Empty;
					num = UnsafeNativeMethods.ManifestEtw.EventWriteTransfer(registrationHandle, ref eventDescriptor, activityId, &empty, userDataCount, userData);
				}
				return num;
			}

			// Token: 0x060069AE RID: 27054
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			private unsafe static extern int EventWriteTransfer([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] Guid* activityId, [In] Guid* relatedActivityId, [In] int userDataCount, [In] EventProvider.EventData* userData);

			// Token: 0x060069AF RID: 27055
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal static extern int EventActivityIdControl([In] UnsafeNativeMethods.ManifestEtw.ActivityControl ControlCode, [In] [Out] ref Guid ActivityId);

			// Token: 0x060069B0 RID: 27056
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern int EventSetInformation([In] long registrationHandle, [In] UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS informationClass, [In] void* eventInformation, [In] int informationLength);

			// Token: 0x060069B1 RID: 27057
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern int EnumerateTraceGuidsEx(UnsafeNativeMethods.ManifestEtw.TRACE_QUERY_INFO_CLASS TraceQueryInfoClass, void* InBuffer, int InBufferSize, void* OutBuffer, int OutBufferSize, ref int ReturnLength);

			// Token: 0x040030C1 RID: 12481
			internal const int ERROR_ARITHMETIC_OVERFLOW = 534;

			// Token: 0x040030C2 RID: 12482
			internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

			// Token: 0x040030C3 RID: 12483
			internal const int ERROR_MORE_DATA = 234;

			// Token: 0x040030C4 RID: 12484
			internal const int ERROR_NOT_SUPPORTED = 50;

			// Token: 0x040030C5 RID: 12485
			internal const int ERROR_INVALID_PARAMETER = 87;

			// Token: 0x040030C6 RID: 12486
			internal const int EVENT_CONTROL_CODE_DISABLE_PROVIDER = 0;

			// Token: 0x040030C7 RID: 12487
			internal const int EVENT_CONTROL_CODE_ENABLE_PROVIDER = 1;

			// Token: 0x040030C8 RID: 12488
			internal const int EVENT_CONTROL_CODE_CAPTURE_STATE = 2;

			// Token: 0x02000CED RID: 3309
			// (Invoke) Token: 0x060071E1 RID: 29153
			[SecurityCritical]
			internal unsafe delegate void EtwEnableCallback([In] ref Guid sourceId, [In] int isEnabled, [In] byte level, [In] long matchAnyKeywords, [In] long matchAllKeywords, [In] UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, [In] void* callbackContext);

			// Token: 0x02000CEE RID: 3310
			internal struct EVENT_FILTER_DESCRIPTOR
			{
				// Token: 0x040038EB RID: 14571
				public long Ptr;

				// Token: 0x040038EC RID: 14572
				public int Size;

				// Token: 0x040038ED RID: 14573
				public int Type;
			}

			// Token: 0x02000CEF RID: 3311
			internal enum ActivityControl : uint
			{
				// Token: 0x040038EF RID: 14575
				EVENT_ACTIVITY_CTRL_GET_ID = 1U,
				// Token: 0x040038F0 RID: 14576
				EVENT_ACTIVITY_CTRL_SET_ID,
				// Token: 0x040038F1 RID: 14577
				EVENT_ACTIVITY_CTRL_CREATE_ID,
				// Token: 0x040038F2 RID: 14578
				EVENT_ACTIVITY_CTRL_GET_SET_ID,
				// Token: 0x040038F3 RID: 14579
				EVENT_ACTIVITY_CTRL_CREATE_SET_ID
			}

			// Token: 0x02000CF0 RID: 3312
			internal enum EVENT_INFO_CLASS
			{
				// Token: 0x040038F5 RID: 14581
				BinaryTrackInfo,
				// Token: 0x040038F6 RID: 14582
				SetEnableAllKeywords,
				// Token: 0x040038F7 RID: 14583
				SetTraits
			}

			// Token: 0x02000CF1 RID: 3313
			internal enum TRACE_QUERY_INFO_CLASS
			{
				// Token: 0x040038F9 RID: 14585
				TraceGuidQueryList,
				// Token: 0x040038FA RID: 14586
				TraceGuidQueryInfo,
				// Token: 0x040038FB RID: 14587
				TraceGuidQueryProcess,
				// Token: 0x040038FC RID: 14588
				TraceStackTracingInfo,
				// Token: 0x040038FD RID: 14589
				MaxTraceSetInfoClass
			}

			// Token: 0x02000CF2 RID: 3314
			internal struct TRACE_GUID_INFO
			{
				// Token: 0x040038FE RID: 14590
				public int InstanceCount;

				// Token: 0x040038FF RID: 14591
				public int Reserved;
			}

			// Token: 0x02000CF3 RID: 3315
			internal struct TRACE_PROVIDER_INSTANCE_INFO
			{
				// Token: 0x04003900 RID: 14592
				public int NextOffset;

				// Token: 0x04003901 RID: 14593
				public int EnableCount;

				// Token: 0x04003902 RID: 14594
				public int Pid;

				// Token: 0x04003903 RID: 14595
				public int Flags;
			}

			// Token: 0x02000CF4 RID: 3316
			internal struct TRACE_ENABLE_INFO
			{
				// Token: 0x04003904 RID: 14596
				public int IsEnabled;

				// Token: 0x04003905 RID: 14597
				public byte Level;

				// Token: 0x04003906 RID: 14598
				public byte Reserved1;

				// Token: 0x04003907 RID: 14599
				public ushort LoggerId;

				// Token: 0x04003908 RID: 14600
				public int EnableProperty;

				// Token: 0x04003909 RID: 14601
				public int Reserved2;

				// Token: 0x0400390A RID: 14602
				public long MatchAnyKeyword;

				// Token: 0x0400390B RID: 14603
				public long MatchAllKeyword;
			}
		}
	}
}

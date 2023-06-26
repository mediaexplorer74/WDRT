using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Compression
{
	// Token: 0x02000424 RID: 1060
	internal static class ZLibNative
	{
		// Token: 0x06002797 RID: 10135 RVA: 0x000B64E0 File Offset: 0x000B46E0
		[SecurityCritical]
		public static ZLibNative.ErrorCode CreateZLibStreamForDeflate(out ZLibNative.ZLibStreamHandle zLibStreamHandle)
		{
			return ZLibNative.CreateZLibStreamForDeflate(out zLibStreamHandle, ZLibNative.CompressionLevel.DefaultCompression, -15, 8, ZLibNative.CompressionStrategy.DefaultStrategy);
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000B64ED File Offset: 0x000B46ED
		[SecurityCritical]
		public static ZLibNative.ErrorCode CreateZLibStreamForDeflate(out ZLibNative.ZLibStreamHandle zLibStreamHandle, ZLibNative.CompressionLevel level, int windowBits, int memLevel, ZLibNative.CompressionStrategy strategy)
		{
			zLibStreamHandle = new ZLibNative.ZLibStreamHandle();
			return zLibStreamHandle.DeflateInit2_(level, windowBits, memLevel, strategy);
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000B6502 File Offset: 0x000B4702
		[SecurityCritical]
		public static ZLibNative.ErrorCode CreateZLibStreamForInflate(out ZLibNative.ZLibStreamHandle zLibStreamHandle)
		{
			return ZLibNative.CreateZLibStreamForInflate(out zLibStreamHandle, -15);
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x000B650C File Offset: 0x000B470C
		[SecurityCritical]
		public static ZLibNative.ErrorCode CreateZLibStreamForInflate(out ZLibNative.ZLibStreamHandle zLibStreamHandle, int windowBits)
		{
			zLibStreamHandle = new ZLibNative.ZLibStreamHandle();
			return zLibStreamHandle.InflateInit2_(windowBits);
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x000B651D File Offset: 0x000B471D
		[SecurityCritical]
		public static int ZLibCompileFlags()
		{
			return ZLibNative.ZLibStreamHandle.ZLibCompileFlags();
		}

		// Token: 0x04002171 RID: 8561
		public const string ZLibNativeDllName = "clrcompression.dll";

		// Token: 0x04002172 RID: 8562
		private const string Kernel32DllName = "kernel32.dll";

		// Token: 0x04002173 RID: 8563
		public const string ZLibVersion = "1.2.11";

		// Token: 0x04002174 RID: 8564
		internal static readonly IntPtr ZNullPtr = (IntPtr)0;

		// Token: 0x04002175 RID: 8565
		public const int Deflate_DefaultWindowBits = -15;

		// Token: 0x04002176 RID: 8566
		public const int Deflate_DefaultMemLevel = 8;

		// Token: 0x02000817 RID: 2071
		public enum FlushCode
		{
			// Token: 0x04003582 RID: 13698
			NoFlush,
			// Token: 0x04003583 RID: 13699
			PartialFlush,
			// Token: 0x04003584 RID: 13700
			SyncFlush,
			// Token: 0x04003585 RID: 13701
			FullFlush,
			// Token: 0x04003586 RID: 13702
			Finish,
			// Token: 0x04003587 RID: 13703
			Block
		}

		// Token: 0x02000818 RID: 2072
		public enum ErrorCode
		{
			// Token: 0x04003589 RID: 13705
			Ok,
			// Token: 0x0400358A RID: 13706
			StreamEnd,
			// Token: 0x0400358B RID: 13707
			NeedDictionary,
			// Token: 0x0400358C RID: 13708
			ErrorNo = -1,
			// Token: 0x0400358D RID: 13709
			StreamError = -2,
			// Token: 0x0400358E RID: 13710
			DataError = -3,
			// Token: 0x0400358F RID: 13711
			MemError = -4,
			// Token: 0x04003590 RID: 13712
			BufError = -5,
			// Token: 0x04003591 RID: 13713
			VersionError = -6
		}

		// Token: 0x02000819 RID: 2073
		public enum CompressionLevel
		{
			// Token: 0x04003593 RID: 13715
			NoCompression,
			// Token: 0x04003594 RID: 13716
			BestSpeed,
			// Token: 0x04003595 RID: 13717
			BestCompression = 9,
			// Token: 0x04003596 RID: 13718
			DefaultCompression = -1
		}

		// Token: 0x0200081A RID: 2074
		public enum CompressionStrategy
		{
			// Token: 0x04003598 RID: 13720
			Filtered = 1,
			// Token: 0x04003599 RID: 13721
			HuffmanOnly,
			// Token: 0x0400359A RID: 13722
			Rle,
			// Token: 0x0400359B RID: 13723
			Fixed,
			// Token: 0x0400359C RID: 13724
			DefaultStrategy = 0
		}

		// Token: 0x0200081B RID: 2075
		public enum CompressionMethod
		{
			// Token: 0x0400359E RID: 13726
			Deflated = 8
		}

		// Token: 0x0200081C RID: 2076
		internal struct ZStream
		{
			// Token: 0x0400359F RID: 13727
			internal IntPtr nextIn;

			// Token: 0x040035A0 RID: 13728
			internal uint availIn;

			// Token: 0x040035A1 RID: 13729
			internal uint totalIn;

			// Token: 0x040035A2 RID: 13730
			internal IntPtr nextOut;

			// Token: 0x040035A3 RID: 13731
			internal uint availOut;

			// Token: 0x040035A4 RID: 13732
			internal uint totalOut;

			// Token: 0x040035A5 RID: 13733
			internal IntPtr msg;

			// Token: 0x040035A6 RID: 13734
			internal IntPtr state;

			// Token: 0x040035A7 RID: 13735
			internal IntPtr zalloc;

			// Token: 0x040035A8 RID: 13736
			internal IntPtr zfree;

			// Token: 0x040035A9 RID: 13737
			internal IntPtr opaque;

			// Token: 0x040035AA RID: 13738
			internal int dataType;

			// Token: 0x040035AB RID: 13739
			internal uint adler;

			// Token: 0x040035AC RID: 13740
			internal uint reserved;
		}

		// Token: 0x0200081D RID: 2077
		// (Invoke) Token: 0x060044E8 RID: 17640
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode DeflateInit2_Delegate(ZLibNative.ZStream* stream, ZLibNative.CompressionLevel level, ZLibNative.CompressionMethod method, int windowBits, int memLevel, ZLibNative.CompressionStrategy strategy, [MarshalAs(UnmanagedType.LPStr)] string version, int streamSize);

		// Token: 0x0200081E RID: 2078
		// (Invoke) Token: 0x060044EC RID: 17644
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode DeflateDelegate(ZLibNative.ZStream* stream, ZLibNative.FlushCode flush);

		// Token: 0x0200081F RID: 2079
		// (Invoke) Token: 0x060044F0 RID: 17648
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode DeflateEndDelegate(ZLibNative.ZStream* stream);

		// Token: 0x02000820 RID: 2080
		// (Invoke) Token: 0x060044F4 RID: 17652
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode InflateInit2_Delegate(ZLibNative.ZStream* stream, int windowBits, [MarshalAs(UnmanagedType.LPStr)] string version, int streamSize);

		// Token: 0x02000821 RID: 2081
		// (Invoke) Token: 0x060044F8 RID: 17656
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode InflateDelegate(ZLibNative.ZStream* stream, ZLibNative.FlushCode flush);

		// Token: 0x02000822 RID: 2082
		// (Invoke) Token: 0x060044FC RID: 17660
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode InflateEndDelegate(ZLibNative.ZStream* stream);

		// Token: 0x02000823 RID: 2083
		// (Invoke) Token: 0x06004500 RID: 17664
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private delegate int ZlibCompileFlagsDelegate();

		// Token: 0x02000824 RID: 2084
		private class NativeMethods
		{
			// Token: 0x06004503 RID: 17667
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
			internal static extern IntPtr GetProcAddress(ZLibNative.SafeLibraryHandle moduleHandle, string procName);

			// Token: 0x06004504 RID: 17668
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern ZLibNative.SafeLibraryHandle LoadLibrary(string libPath);

			// Token: 0x06004505 RID: 17669
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("kernel32.dll", ExactSpelling = true)]
			internal static extern bool FreeLibrary(IntPtr moduleHandle);
		}

		// Token: 0x02000825 RID: 2085
		[SecurityCritical]
		private class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06004507 RID: 17671 RVA: 0x001203B8 File Offset: 0x0011E5B8
			[SecurityCritical]
			internal SafeLibraryHandle()
				: base(true)
			{
			}

			// Token: 0x06004508 RID: 17672 RVA: 0x001203C4 File Offset: 0x0011E5C4
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[SecurityCritical]
			protected override bool ReleaseHandle()
			{
				bool flag = ZLibNative.NativeMethods.FreeLibrary(this.handle);
				this.handle = IntPtr.Zero;
				return flag;
			}
		}

		// Token: 0x02000826 RID: 2086
		[SecurityCritical]
		public sealed class ZLibStreamHandle : SafeHandleMinusOneIsInvalid
		{
			// Token: 0x06004509 RID: 17673 RVA: 0x001203E9 File Offset: 0x0011E5E9
			public unsafe ZLibStreamHandle()
				: base(true)
			{
				this.zStreamPtr = (ZLibNative.ZStream*)(void*)ZLibNative.ZLibStreamHandle.AllocWithZeroOut(sizeof(ZLibNative.ZStream));
				this.initializationState = ZLibNative.ZLibStreamHandle.State.NotInitialized;
				this.handle = IntPtr.Zero;
			}

			// Token: 0x17000FA4 RID: 4004
			// (get) Token: 0x0600450A RID: 17674 RVA: 0x0012041C File Offset: 0x0011E61C
			public ZLibNative.ZLibStreamHandle.State InitializationState
			{
				[SecurityCritical]
				get
				{
					return this.initializationState;
				}
			}

			// Token: 0x0600450B RID: 17675 RVA: 0x00120428 File Offset: 0x0011E628
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[SecurityCritical]
			protected unsafe override bool ReleaseHandle()
			{
				bool flag;
				try
				{
					if (ZLibNative.ZLibStreamHandle.zlibLibraryHandle == null || ZLibNative.ZLibStreamHandle.zlibLibraryHandle.IsInvalid)
					{
						flag = false;
					}
					else
					{
						switch (this.InitializationState)
						{
						case ZLibNative.ZLibStreamHandle.State.NotInitialized:
							flag = true;
							break;
						case ZLibNative.ZLibStreamHandle.State.InitializedForDeflate:
							flag = this.DeflateEnd() == ZLibNative.ErrorCode.Ok;
							break;
						case ZLibNative.ZLibStreamHandle.State.InitializedForInflate:
							flag = this.InflateEnd() == ZLibNative.ErrorCode.Ok;
							break;
						case ZLibNative.ZLibStreamHandle.State.Disposed:
							flag = true;
							break;
						default:
							flag = false;
							break;
						}
					}
				}
				finally
				{
					if (this.zStreamPtr != null)
					{
						Marshal.FreeHGlobal((IntPtr)((void*)this.zStreamPtr));
						this.zStreamPtr = null;
					}
				}
				return flag;
			}

			// Token: 0x17000FA5 RID: 4005
			// (get) Token: 0x0600450C RID: 17676 RVA: 0x001204C4 File Offset: 0x0011E6C4
			// (set) Token: 0x0600450D RID: 17677 RVA: 0x001204D1 File Offset: 0x0011E6D1
			public unsafe IntPtr NextIn
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->nextIn;
				}
				[SecurityCritical]
				set
				{
					if (this.zStreamPtr != null)
					{
						this.zStreamPtr->nextIn = value;
					}
				}
			}

			// Token: 0x17000FA6 RID: 4006
			// (get) Token: 0x0600450E RID: 17678 RVA: 0x001204E9 File Offset: 0x0011E6E9
			// (set) Token: 0x0600450F RID: 17679 RVA: 0x001204F6 File Offset: 0x0011E6F6
			public unsafe uint AvailIn
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->availIn;
				}
				[SecurityCritical]
				set
				{
					if (this.zStreamPtr != null)
					{
						this.zStreamPtr->availIn = value;
					}
				}
			}

			// Token: 0x17000FA7 RID: 4007
			// (get) Token: 0x06004510 RID: 17680 RVA: 0x0012050E File Offset: 0x0011E70E
			public unsafe uint TotalIn
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->totalIn;
				}
			}

			// Token: 0x17000FA8 RID: 4008
			// (get) Token: 0x06004511 RID: 17681 RVA: 0x0012051B File Offset: 0x0011E71B
			// (set) Token: 0x06004512 RID: 17682 RVA: 0x00120528 File Offset: 0x0011E728
			public unsafe IntPtr NextOut
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->nextOut;
				}
				[SecurityCritical]
				set
				{
					if (this.zStreamPtr != null)
					{
						this.zStreamPtr->nextOut = value;
					}
				}
			}

			// Token: 0x17000FA9 RID: 4009
			// (get) Token: 0x06004513 RID: 17683 RVA: 0x00120540 File Offset: 0x0011E740
			// (set) Token: 0x06004514 RID: 17684 RVA: 0x0012054D File Offset: 0x0011E74D
			public unsafe uint AvailOut
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->availOut;
				}
				[SecurityCritical]
				set
				{
					if (this.zStreamPtr != null)
					{
						this.zStreamPtr->availOut = value;
					}
				}
			}

			// Token: 0x17000FAA RID: 4010
			// (get) Token: 0x06004515 RID: 17685 RVA: 0x00120565 File Offset: 0x0011E765
			public unsafe uint TotalOut
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->totalOut;
				}
			}

			// Token: 0x17000FAB RID: 4011
			// (get) Token: 0x06004516 RID: 17686 RVA: 0x00120572 File Offset: 0x0011E772
			public unsafe int DataType
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->dataType;
				}
			}

			// Token: 0x17000FAC RID: 4012
			// (get) Token: 0x06004517 RID: 17687 RVA: 0x0012057F File Offset: 0x0011E77F
			public unsafe uint Adler
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->adler;
				}
			}

			// Token: 0x06004518 RID: 17688 RVA: 0x0012058C File Offset: 0x0011E78C
			[SecurityCritical]
			private void EnsureNotDisposed()
			{
				if (this.InitializationState == ZLibNative.ZLibStreamHandle.State.Disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
			}

			// Token: 0x06004519 RID: 17689 RVA: 0x001205A8 File Offset: 0x0011E7A8
			[SecurityCritical]
			private void EnsureState(ZLibNative.ZLibStreamHandle.State requiredState)
			{
				if (this.InitializationState != requiredState)
				{
					throw new InvalidOperationException("InitializationState != " + requiredState.ToString());
				}
			}

			// Token: 0x0600451A RID: 17690 RVA: 0x001205D0 File Offset: 0x0011E7D0
			[SecurityCritical]
			public unsafe ZLibNative.ErrorCode DeflateInit2_(ZLibNative.CompressionLevel level, int windowBits, int memLevel, ZLibNative.CompressionStrategy strategy)
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.NotInitialized);
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				ZLibNative.ErrorCode errorCode;
				try
				{
				}
				finally
				{
					errorCode = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateInit2_Delegate(this.zStreamPtr, level, ZLibNative.CompressionMethod.Deflated, windowBits, memLevel, strategy, "1.2.11", sizeof(ZLibNative.ZStream));
					this.initializationState = ZLibNative.ZLibStreamHandle.State.InitializedForDeflate;
					ZLibNative.ZLibStreamHandle.zlibLibraryHandle.DangerousAddRef(ref flag);
				}
				return errorCode;
			}

			// Token: 0x0600451B RID: 17691 RVA: 0x0012063C File Offset: 0x0011E83C
			[SecurityCritical]
			public ZLibNative.ErrorCode Deflate(ZLibNative.FlushCode flush)
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.InitializedForDeflate);
				return ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateDelegate(this.zStreamPtr, flush);
			}

			// Token: 0x0600451C RID: 17692 RVA: 0x0012065C File Offset: 0x0011E85C
			[SecurityCritical]
			public ZLibNative.ErrorCode DeflateEnd()
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.InitializedForDeflate);
				RuntimeHelpers.PrepareConstrainedRegions();
				ZLibNative.ErrorCode errorCode;
				try
				{
				}
				finally
				{
					errorCode = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateEndDelegate(this.zStreamPtr);
					this.initializationState = ZLibNative.ZLibStreamHandle.State.Disposed;
					ZLibNative.ZLibStreamHandle.zlibLibraryHandle.DangerousRelease();
				}
				return errorCode;
			}

			// Token: 0x0600451D RID: 17693 RVA: 0x001206B4 File Offset: 0x0011E8B4
			[SecurityCritical]
			public unsafe ZLibNative.ErrorCode InflateInit2_(int windowBits)
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.NotInitialized);
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				ZLibNative.ErrorCode errorCode;
				try
				{
				}
				finally
				{
					errorCode = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateInit2_Delegate(this.zStreamPtr, windowBits, "1.2.11", sizeof(ZLibNative.ZStream));
					this.initializationState = ZLibNative.ZLibStreamHandle.State.InitializedForInflate;
					ZLibNative.ZLibStreamHandle.zlibLibraryHandle.DangerousAddRef(ref flag);
				}
				return errorCode;
			}

			// Token: 0x0600451E RID: 17694 RVA: 0x0012071C File Offset: 0x0011E91C
			[SecurityCritical]
			public ZLibNative.ErrorCode Inflate(ZLibNative.FlushCode flush)
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.InitializedForInflate);
				return ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateDelegate(this.zStreamPtr, flush);
			}

			// Token: 0x0600451F RID: 17695 RVA: 0x0012073C File Offset: 0x0011E93C
			[SecurityCritical]
			public ZLibNative.ErrorCode InflateEnd()
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.InitializedForInflate);
				RuntimeHelpers.PrepareConstrainedRegions();
				ZLibNative.ErrorCode errorCode;
				try
				{
				}
				finally
				{
					errorCode = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateEndDelegate(this.zStreamPtr);
					this.initializationState = ZLibNative.ZLibStreamHandle.State.Disposed;
					ZLibNative.ZLibStreamHandle.zlibLibraryHandle.DangerousRelease();
				}
				return errorCode;
			}

			// Token: 0x06004520 RID: 17696 RVA: 0x00120794 File Offset: 0x0011E994
			[SecurityCritical]
			public unsafe string GetErrorMessage()
			{
				if (ZLibNative.ZNullPtr.Equals(this.zStreamPtr->msg))
				{
					return string.Empty;
				}
				return new string((sbyte*)(void*)this.zStreamPtr->msg);
			}

			// Token: 0x06004521 RID: 17697 RVA: 0x001207DD File Offset: 0x0011E9DD
			[SecurityCritical]
			internal static int ZLibCompileFlags()
			{
				return ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.zlibCompileFlagsDelegate();
			}

			// Token: 0x06004522 RID: 17698 RVA: 0x001207EC File Offset: 0x0011E9EC
			[SecurityCritical]
			private unsafe static IntPtr AllocWithZeroOut(int byteCount)
			{
				IntPtr intPtr = Marshal.AllocHGlobal(byteCount);
				byte* ptr = (byte*)(void*)intPtr;
				int num = byteCount / 4;
				int* ptr2 = (int*)ptr;
				for (int i = 0; i < num; i++)
				{
					ptr2[i] = 0;
				}
				num *= 4;
				ptr += num;
				int num2 = byteCount - num;
				for (int j = 0; j < num2; j++)
				{
					ptr[j] = 0;
				}
				return intPtr;
			}

			// Token: 0x040035AD RID: 13741
			[SecurityCritical]
			private static ZLibNative.SafeLibraryHandle zlibLibraryHandle;

			// Token: 0x040035AE RID: 13742
			[SecurityCritical]
			private unsafe ZLibNative.ZStream* zStreamPtr;

			// Token: 0x040035AF RID: 13743
			[SecurityCritical]
			private volatile ZLibNative.ZLibStreamHandle.State initializationState;

			// Token: 0x0200092D RID: 2349
			[SecurityCritical]
			private static class NativeZLibDLLStub
			{
				// Token: 0x0600467D RID: 18045 RVA: 0x00125E64 File Offset: 0x00124064
				[SecuritySafeCritical]
				private static void LoadZLibDLL()
				{
					new FileIOPermission(PermissionState.Unrestricted).Assert();
					string runtimeDirectory = RuntimeEnvironment.GetRuntimeDirectory();
					string text = Path.Combine(runtimeDirectory, "clrcompression.dll");
					if (!File.Exists(text))
					{
						throw new DllNotFoundException("clrcompression.dll");
					}
					ZLibNative.SafeLibraryHandle safeLibraryHandle = ZLibNative.NativeMethods.LoadLibrary(text);
					if (safeLibraryHandle.IsInvalid)
					{
						int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
						Marshal.ThrowExceptionForHR(hrforLastWin32Error, new IntPtr(-1));
						throw new InvalidOperationException();
					}
					ZLibNative.ZLibStreamHandle.zlibLibraryHandle = safeLibraryHandle;
				}

				// Token: 0x0600467E RID: 18046 RVA: 0x00125ED0 File Offset: 0x001240D0
				[SecurityCritical]
				private static DT CreateDelegate<DT>(string entryPointName)
				{
					IntPtr procAddress = ZLibNative.NativeMethods.GetProcAddress(ZLibNative.ZLibStreamHandle.zlibLibraryHandle, entryPointName);
					if (IntPtr.Zero == procAddress)
					{
						throw new EntryPointNotFoundException("clrcompression.dll!" + entryPointName);
					}
					return (DT)((object)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DT)));
				}

				// Token: 0x0600467F RID: 18047 RVA: 0x00125F1C File Offset: 0x0012411C
				[SecuritySafeCritical]
				private static void InitDelegates()
				{
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateInit2_Delegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.DeflateInit2_Delegate>("deflateInit2_");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateDelegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.DeflateDelegate>("deflate");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateEndDelegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.DeflateEndDelegate>("deflateEnd");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateInit2_Delegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.InflateInit2_Delegate>("inflateInit2_");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateDelegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.InflateDelegate>("inflate");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateEndDelegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.InflateEndDelegate>("inflateEnd");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.zlibCompileFlagsDelegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.ZlibCompileFlagsDelegate>("zlibCompileFlags");
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateInit2_Delegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateDelegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateEndDelegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateInit2_Delegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateDelegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateEndDelegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.zlibCompileFlagsDelegate);
				}

				// Token: 0x06004680 RID: 18048 RVA: 0x00125FD8 File Offset: 0x001241D8
				[SecuritySafeCritical]
				static NativeZLibDLLStub()
				{
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.LoadZLibDLL();
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.InitDelegates();
				}

				// Token: 0x04003DB2 RID: 15794
				[SecurityCritical]
				internal static ZLibNative.DeflateInit2_Delegate deflateInit2_Delegate;

				// Token: 0x04003DB3 RID: 15795
				[SecurityCritical]
				internal static ZLibNative.DeflateDelegate deflateDelegate;

				// Token: 0x04003DB4 RID: 15796
				[SecurityCritical]
				internal static ZLibNative.DeflateEndDelegate deflateEndDelegate;

				// Token: 0x04003DB5 RID: 15797
				[SecurityCritical]
				internal static ZLibNative.InflateInit2_Delegate inflateInit2_Delegate;

				// Token: 0x04003DB6 RID: 15798
				[SecurityCritical]
				internal static ZLibNative.InflateDelegate inflateDelegate;

				// Token: 0x04003DB7 RID: 15799
				[SecurityCritical]
				internal static ZLibNative.InflateEndDelegate inflateEndDelegate;

				// Token: 0x04003DB8 RID: 15800
				[SecurityCritical]
				internal static ZLibNative.ZlibCompileFlagsDelegate zlibCompileFlagsDelegate;
			}

			// Token: 0x0200092E RID: 2350
			public enum State
			{
				// Token: 0x04003DBA RID: 15802
				NotInitialized,
				// Token: 0x04003DBB RID: 15803
				InitializedForDeflate,
				// Token: 0x04003DBC RID: 15804
				InitializedForInflate,
				// Token: 0x04003DBD RID: 15805
				Disposed
			}
		}
	}
}

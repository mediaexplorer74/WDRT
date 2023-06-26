using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F3 RID: 499
	[ComVisible(false)]
	internal sealed class SafeOverlappedFree : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012FC RID: 4860 RVA: 0x00064181 File Offset: 0x00062381
		private SafeOverlappedFree()
			: base(true)
		{
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0006418A File Offset: 0x0006238A
		private SafeOverlappedFree(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00064194 File Offset: 0x00062394
		public static SafeOverlappedFree Alloc()
		{
			SafeOverlappedFree safeOverlappedFree = UnsafeNclNativeMethods.SafeNetHandlesSafeOverlappedFree.LocalAlloc(64, (UIntPtr)((ulong)((long)Win32.OverlappedSize)));
			if (safeOverlappedFree.IsInvalid)
			{
				safeOverlappedFree.SetHandleAsInvalid();
				throw new OutOfMemoryException();
			}
			return safeOverlappedFree;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000641CC File Offset: 0x000623CC
		public static SafeOverlappedFree Alloc(SafeCloseSocket socketHandle)
		{
			SafeOverlappedFree safeOverlappedFree = SafeOverlappedFree.Alloc();
			safeOverlappedFree._socketHandle = socketHandle;
			return safeOverlappedFree;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x000641E8 File Offset: 0x000623E8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close(bool resetOwner)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (resetOwner)
				{
					this._socketHandle = null;
				}
				base.Close();
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00064220 File Offset: 0x00062420
		protected override bool ReleaseHandle()
		{
			SafeCloseSocket socketHandle = this._socketHandle;
			if (socketHandle != null && !socketHandle.IsInvalid)
			{
				socketHandle.Dispose();
			}
			return UnsafeNclNativeMethods.SafeNetHandles.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x04001529 RID: 5417
		private const int LPTR = 64;

		// Token: 0x0400152A RID: 5418
		internal static readonly SafeOverlappedFree Zero = new SafeOverlappedFree(false);

		// Token: 0x0400152B RID: 5419
		private SafeCloseSocket _socketHandle;
	}
}

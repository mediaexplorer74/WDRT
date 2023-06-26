using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000503 RID: 1283
	internal sealed class OverlappedData
	{
		// Token: 0x06003CA7 RID: 15527 RVA: 0x000E6108 File Offset: 0x000E4308
		[SecurityCritical]
		internal void ReInitialize()
		{
			this.m_asyncResult = null;
			this.m_iocb = null;
			this.m_iocbHelper = null;
			this.m_overlapped = null;
			this.m_userObject = null;
			this.m_pinSelf = (IntPtr)0;
			this.m_userObjectInternal = (IntPtr)0;
			this.m_AppDomainId = 0;
			this.m_nativeOverlapped.EventHandle = (IntPtr)0;
			this.m_isArray = 0;
			this.m_nativeOverlapped.InternalLow = (IntPtr)0;
			this.m_nativeOverlapped.InternalHigh = (IntPtr)0;
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x000E6194 File Offset: 0x000E4394
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
		{
			if (!this.m_pinSelf.IsNull())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_Overlapped_Pack"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (iocb != null)
			{
				this.m_iocbHelper = new _IOCompletionCallback(iocb, ref stackCrawlMark);
				this.m_iocb = iocb;
			}
			else
			{
				this.m_iocbHelper = null;
				this.m_iocb = null;
			}
			this.m_userObject = userData;
			if (this.m_userObject != null)
			{
				if (this.m_userObject.GetType() == typeof(object[]))
				{
					this.m_isArray = 1;
				}
				else
				{
					this.m_isArray = 0;
				}
			}
			return this.AllocateNativeOverlapped();
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x000E622C File Offset: 0x000E442C
		[SecurityCritical]
		internal unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
		{
			if (!this.m_pinSelf.IsNull())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_Overlapped_Pack"));
			}
			this.m_userObject = userData;
			if (this.m_userObject != null)
			{
				if (this.m_userObject.GetType() == typeof(object[]))
				{
					this.m_isArray = 1;
				}
				else
				{
					this.m_isArray = 0;
				}
			}
			this.m_iocb = iocb;
			this.m_iocbHelper = null;
			return this.AllocateNativeOverlapped();
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06003CAA RID: 15530 RVA: 0x000E62A5 File Offset: 0x000E44A5
		// (set) Token: 0x06003CAB RID: 15531 RVA: 0x000E62B2 File Offset: 0x000E44B2
		[ComVisible(false)]
		internal IntPtr UserHandle
		{
			get
			{
				return this.m_nativeOverlapped.EventHandle;
			}
			set
			{
				this.m_nativeOverlapped.EventHandle = value;
			}
		}

		// Token: 0x06003CAC RID: 15532
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern NativeOverlapped* AllocateNativeOverlapped();

		// Token: 0x06003CAD RID: 15533
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void FreeNativeOverlapped(NativeOverlapped* nativeOverlappedPtr);

		// Token: 0x06003CAE RID: 15534
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern OverlappedData GetOverlappedFromNative(NativeOverlapped* nativeOverlappedPtr);

		// Token: 0x06003CAF RID: 15535
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void CheckVMForIOPacket(out NativeOverlapped* pOVERLAP, out uint errorCode, out uint numBytes);

		// Token: 0x040019B7 RID: 6583
		internal IAsyncResult m_asyncResult;

		// Token: 0x040019B8 RID: 6584
		[SecurityCritical]
		internal IOCompletionCallback m_iocb;

		// Token: 0x040019B9 RID: 6585
		internal _IOCompletionCallback m_iocbHelper;

		// Token: 0x040019BA RID: 6586
		internal Overlapped m_overlapped;

		// Token: 0x040019BB RID: 6587
		private object m_userObject;

		// Token: 0x040019BC RID: 6588
		private IntPtr m_pinSelf;

		// Token: 0x040019BD RID: 6589
		private IntPtr m_userObjectInternal;

		// Token: 0x040019BE RID: 6590
		private int m_AppDomainId;

		// Token: 0x040019BF RID: 6591
		private byte m_isArray;

		// Token: 0x040019C0 RID: 6592
		private byte m_toBeCleaned;

		// Token: 0x040019C1 RID: 6593
		internal NativeOverlapped m_nativeOverlapped;
	}
}

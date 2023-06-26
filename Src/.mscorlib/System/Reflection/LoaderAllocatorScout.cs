using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005EF RID: 1519
	internal sealed class LoaderAllocatorScout
	{
		// Token: 0x06004688 RID: 18056
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool Destroy(IntPtr nativeLoaderAllocator);

		// Token: 0x06004689 RID: 18057 RVA: 0x00103E54 File Offset: 0x00102054
		[SecuritySafeCritical]
		~LoaderAllocatorScout()
		{
			if (!this.m_nativeLoaderAllocator.IsNull())
			{
				if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload() && !LoaderAllocatorScout.Destroy(this.m_nativeLoaderAllocator))
				{
					GC.ReRegisterForFinalize(this);
				}
			}
		}

		// Token: 0x04001CDB RID: 7387
		internal IntPtr m_nativeLoaderAllocator;
	}
}

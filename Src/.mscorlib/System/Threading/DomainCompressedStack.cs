using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004F0 RID: 1264
	[Serializable]
	internal sealed class DomainCompressedStack
	{
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06003BE8 RID: 15336 RVA: 0x000E4588 File Offset: 0x000E2788
		internal PermissionListSet PLS
		{
			get
			{
				return this.m_pls;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x000E4590 File Offset: 0x000E2790
		internal bool ConstructionHalted
		{
			get
			{
				return this.m_bHaltConstruction;
			}
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x000E4598 File Offset: 0x000E2798
		[SecurityCritical]
		private static DomainCompressedStack CreateManagedObject(IntPtr unmanagedDCS)
		{
			DomainCompressedStack domainCompressedStack = new DomainCompressedStack();
			domainCompressedStack.m_pls = PermissionListSet.CreateCompressedState(unmanagedDCS, out domainCompressedStack.m_bHaltConstruction);
			return domainCompressedStack;
		}

		// Token: 0x06003BEB RID: 15339
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetDescCount(IntPtr dcs);

		// Token: 0x06003BEC RID: 15340
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetDomainPermissionSets(IntPtr dcs, out PermissionSet granted, out PermissionSet refused);

		// Token: 0x06003BED RID: 15341
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetDescriptorInfo(IntPtr dcs, int index, out PermissionSet granted, out PermissionSet refused, out Assembly assembly, out FrameSecurityDescriptor fsd);

		// Token: 0x06003BEE RID: 15342
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IgnoreDomain(IntPtr dcs);

		// Token: 0x04001985 RID: 6533
		private PermissionListSet m_pls;

		// Token: 0x04001986 RID: 6534
		private bool m_bHaltConstruction;
	}
}

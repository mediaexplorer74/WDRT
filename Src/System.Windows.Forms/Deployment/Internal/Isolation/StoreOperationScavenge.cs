using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000059 RID: 89
	internal struct StoreOperationScavenge
	{
		// Token: 0x0600019A RID: 410 RVA: 0x000075A0 File Offset: 0x000057A0
		[SecuritySafeCritical]
		public StoreOperationScavenge(bool Light, ulong SizeLimit, ulong RunLimit, uint ComponentLimit)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationScavenge));
			this.Flags = StoreOperationScavenge.OpFlags.Nothing;
			if (Light)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.Light;
			}
			this.SizeReclaimationLimit = SizeLimit;
			if (SizeLimit != 0UL)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitSize;
			}
			this.RuntimeLimit = RunLimit;
			if (RunLimit != 0UL)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitTime;
			}
			this.ComponentCountLimit = ComponentLimit;
			if (ComponentLimit != 0U)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitCount;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007624 File Offset: 0x00005824
		public StoreOperationScavenge(bool Light)
		{
			this = new StoreOperationScavenge(Light, 0UL, 0UL, 0U);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000070A6 File Offset: 0x000052A6
		public void Destroy()
		{
		}

		// Token: 0x0400017C RID: 380
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400017D RID: 381
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationScavenge.OpFlags Flags;

		// Token: 0x0400017E RID: 382
		[MarshalAs(UnmanagedType.U8)]
		public ulong SizeReclaimationLimit;

		// Token: 0x0400017F RID: 383
		[MarshalAs(UnmanagedType.U8)]
		public ulong RuntimeLimit;

		// Token: 0x04000180 RID: 384
		[MarshalAs(UnmanagedType.U4)]
		public uint ComponentCountLimit;

		// Token: 0x0200052F RID: 1327
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037C0 RID: 14272
			Nothing = 0,
			// Token: 0x040037C1 RID: 14273
			Light = 1,
			// Token: 0x040037C2 RID: 14274
			LimitSize = 2,
			// Token: 0x040037C3 RID: 14275
			LimitTime = 4,
			// Token: 0x040037C4 RID: 14276
			LimitCount = 8
		}
	}
}

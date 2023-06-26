using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A8 RID: 1704
	internal struct StoreOperationScavenge
	{
		// Token: 0x06004FF6 RID: 20470 RVA: 0x0011E444 File Offset: 0x0011C644
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

		// Token: 0x06004FF7 RID: 20471 RVA: 0x0011E4C8 File Offset: 0x0011C6C8
		public StoreOperationScavenge(bool Light)
		{
			this = new StoreOperationScavenge(Light, 0UL, 0UL, 0U);
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0011E4D6 File Offset: 0x0011C6D6
		public void Destroy()
		{
		}

		// Token: 0x0400225C RID: 8796
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400225D RID: 8797
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationScavenge.OpFlags Flags;

		// Token: 0x0400225E RID: 8798
		[MarshalAs(UnmanagedType.U8)]
		public ulong SizeReclaimationLimit;

		// Token: 0x0400225F RID: 8799
		[MarshalAs(UnmanagedType.U8)]
		public ulong RuntimeLimit;

		// Token: 0x04002260 RID: 8800
		[MarshalAs(UnmanagedType.U4)]
		public uint ComponentCountLimit;

		// Token: 0x02000C4E RID: 3150
		[Flags]
		public enum OpFlags
		{
			// Token: 0x0400378A RID: 14218
			Nothing = 0,
			// Token: 0x0400378B RID: 14219
			Light = 1,
			// Token: 0x0400378C RID: 14220
			LimitSize = 2,
			// Token: 0x0400378D RID: 14221
			LimitTime = 4,
			// Token: 0x0400378E RID: 14222
			LimitCount = 8
		}
	}
}

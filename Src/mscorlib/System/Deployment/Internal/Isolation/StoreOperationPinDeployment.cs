using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A1 RID: 1697
	internal struct StoreOperationPinDeployment
	{
		// Token: 0x06004FE3 RID: 20451 RVA: 0x0011E061 File Offset: 0x0011C261
		[SecuritySafeCritical]
		public StoreOperationPinDeployment(IDefinitionAppId AppId, StoreApplicationReference Ref)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationPinDeployment));
			this.Flags = StoreOperationPinDeployment.OpFlags.NeverExpires;
			this.Application = AppId;
			this.Reference = Ref.ToIntPtr();
			this.ExpirationTime = 0L;
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x0011E09B File Offset: 0x0011C29B
		public StoreOperationPinDeployment(IDefinitionAppId AppId, DateTime Expiry, StoreApplicationReference Ref)
		{
			this = new StoreOperationPinDeployment(AppId, Ref);
			this.Flags |= StoreOperationPinDeployment.OpFlags.NeverExpires;
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x0011E0B3 File Offset: 0x0011C2B3
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x0400223B RID: 8763
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400223C RID: 8764
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationPinDeployment.OpFlags Flags;

		// Token: 0x0400223D RID: 8765
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400223E RID: 8766
		[MarshalAs(UnmanagedType.I8)]
		public long ExpirationTime;

		// Token: 0x0400223F RID: 8767
		public IntPtr Reference;

		// Token: 0x02000C43 RID: 3139
		[Flags]
		public enum OpFlags
		{
			// Token: 0x0400376B RID: 14187
			Nothing = 0,
			// Token: 0x0400376C RID: 14188
			NeverExpires = 1
		}

		// Token: 0x02000C44 RID: 3140
		public enum Disposition
		{
			// Token: 0x0400376E RID: 14190
			Failed,
			// Token: 0x0400376F RID: 14191
			Pinned
		}
	}
}

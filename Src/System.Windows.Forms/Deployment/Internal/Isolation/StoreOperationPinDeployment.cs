using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000052 RID: 82
	internal struct StoreOperationPinDeployment
	{
		// Token: 0x06000187 RID: 391 RVA: 0x000071BB File Offset: 0x000053BB
		[SecuritySafeCritical]
		public StoreOperationPinDeployment(IDefinitionAppId AppId, StoreApplicationReference Ref)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationPinDeployment));
			this.Flags = StoreOperationPinDeployment.OpFlags.NeverExpires;
			this.Application = AppId;
			this.Reference = Ref.ToIntPtr();
			this.ExpirationTime = 0L;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000071F5 File Offset: 0x000053F5
		public StoreOperationPinDeployment(IDefinitionAppId AppId, DateTime Expiry, StoreApplicationReference Ref)
		{
			this = new StoreOperationPinDeployment(AppId, Ref);
			this.Flags |= StoreOperationPinDeployment.OpFlags.NeverExpires;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000720D File Offset: 0x0000540D
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x0400015B RID: 347
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400015C RID: 348
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationPinDeployment.OpFlags Flags;

		// Token: 0x0400015D RID: 349
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400015E RID: 350
		[MarshalAs(UnmanagedType.I8)]
		public long ExpirationTime;

		// Token: 0x0400015F RID: 351
		public IntPtr Reference;

		// Token: 0x02000524 RID: 1316
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037A1 RID: 14241
			Nothing = 0,
			// Token: 0x040037A2 RID: 14242
			NeverExpires = 1
		}

		// Token: 0x02000525 RID: 1317
		public enum Disposition
		{
			// Token: 0x040037A4 RID: 14244
			Failed,
			// Token: 0x040037A5 RID: 14245
			Pinned
		}
	}
}

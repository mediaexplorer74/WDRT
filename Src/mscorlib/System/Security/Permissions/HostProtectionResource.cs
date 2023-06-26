using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies categories of functionality potentially harmful to the host if invoked by a method or class.</summary>
	// Token: 0x020002E2 RID: 738
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum HostProtectionResource
	{
		/// <summary>Exposes no host resources.</summary>
		// Token: 0x04000E9A RID: 3738
		None = 0,
		/// <summary>Exposes synchronization.</summary>
		// Token: 0x04000E9B RID: 3739
		Synchronization = 1,
		/// <summary>Exposes state that might be shared between threads.</summary>
		// Token: 0x04000E9C RID: 3740
		SharedState = 2,
		/// <summary>Might create or destroy other processes.</summary>
		// Token: 0x04000E9D RID: 3741
		ExternalProcessMgmt = 4,
		/// <summary>Might exit the current process, terminating the server.</summary>
		// Token: 0x04000E9E RID: 3742
		SelfAffectingProcessMgmt = 8,
		/// <summary>Creates or manipulates threads other than its own, which might be harmful to the host.</summary>
		// Token: 0x04000E9F RID: 3743
		ExternalThreading = 16,
		/// <summary>Manipulates threads in a way that only affects user code.</summary>
		// Token: 0x04000EA0 RID: 3744
		SelfAffectingThreading = 32,
		/// <summary>Exposes the security infrastructure.</summary>
		// Token: 0x04000EA1 RID: 3745
		SecurityInfrastructure = 64,
		/// <summary>Exposes the user interface.</summary>
		// Token: 0x04000EA2 RID: 3746
		UI = 128,
		/// <summary>Might cause a resource leak on termination, if not protected by a safe handle or some other means of ensuring the release of resources.</summary>
		// Token: 0x04000EA3 RID: 3747
		MayLeakOnAbort = 256,
		/// <summary>Exposes all host resources.</summary>
		// Token: 0x04000EA4 RID: 3748
		All = 511
	}
}

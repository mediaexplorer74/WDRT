using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Specifies common roles to be used with <see cref="M:System.Security.Principal.WindowsPrincipal.IsInRole(System.String)" />.</summary>
	// Token: 0x02000330 RID: 816
	[ComVisible(true)]
	[Serializable]
	public enum WindowsBuiltInRole
	{
		/// <summary>Administrators have complete and unrestricted access to the computer or domain.</summary>
		// Token: 0x0400108B RID: 4235
		Administrator = 544,
		/// <summary>Users are prevented from making accidental or intentional system-wide changes. Thus, users can run certified applications, but not most legacy applications.</summary>
		// Token: 0x0400108C RID: 4236
		User,
		/// <summary>Guests are more restricted than users.</summary>
		// Token: 0x0400108D RID: 4237
		Guest,
		/// <summary>Power users possess most administrative permissions with some restrictions. Thus, power users can run legacy applications, in addition to certified applications.</summary>
		// Token: 0x0400108E RID: 4238
		PowerUser,
		/// <summary>Account operators manage the user accounts on a computer or domain.</summary>
		// Token: 0x0400108F RID: 4239
		AccountOperator,
		/// <summary>System operators manage a particular computer.</summary>
		// Token: 0x04001090 RID: 4240
		SystemOperator,
		/// <summary>Print operators can take control of a printer.</summary>
		// Token: 0x04001091 RID: 4241
		PrintOperator,
		/// <summary>Backup operators can override security restrictions for the sole purpose of backing up or restoring files.</summary>
		// Token: 0x04001092 RID: 4242
		BackupOperator,
		/// <summary>Replicators support file replication in a domain.</summary>
		// Token: 0x04001093 RID: 4243
		Replicator
	}
}

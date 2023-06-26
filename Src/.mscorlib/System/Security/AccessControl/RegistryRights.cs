using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to registry objects.</summary>
	// Token: 0x0200022C RID: 556
	[Flags]
	public enum RegistryRights
	{
		/// <summary>The right to query the name/value pairs in a registry key.</summary>
		// Token: 0x04000B91 RID: 2961
		QueryValues = 1,
		/// <summary>The right to create, delete, or set name/value pairs in a registry key.</summary>
		// Token: 0x04000B92 RID: 2962
		SetValue = 2,
		/// <summary>The right to create subkeys of a registry key.</summary>
		// Token: 0x04000B93 RID: 2963
		CreateSubKey = 4,
		/// <summary>The right to list the subkeys of a registry key.</summary>
		// Token: 0x04000B94 RID: 2964
		EnumerateSubKeys = 8,
		/// <summary>The right to request notification of changes on a registry key.</summary>
		// Token: 0x04000B95 RID: 2965
		Notify = 16,
		/// <summary>Reserved for system use.</summary>
		// Token: 0x04000B96 RID: 2966
		CreateLink = 32,
		/// <summary>Same as <see cref="F:System.Security.AccessControl.RegistryRights.ReadKey" />.</summary>
		// Token: 0x04000B97 RID: 2967
		ExecuteKey = 131097,
		/// <summary>The right to query the name/value pairs in a registry key, to request notification of changes, to enumerate its subkeys, and to read its access rules and audit rules.</summary>
		// Token: 0x04000B98 RID: 2968
		ReadKey = 131097,
		/// <summary>The right to create, delete, and set the name/value pairs in a registry key, to create or delete subkeys, to request notification of changes, to enumerate its subkeys, and to read its access rules and audit rules.</summary>
		// Token: 0x04000B99 RID: 2969
		WriteKey = 131078,
		/// <summary>The right to delete a registry key.</summary>
		// Token: 0x04000B9A RID: 2970
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a registry key.</summary>
		// Token: 0x04000B9B RID: 2971
		ReadPermissions = 131072,
		/// <summary>The right to change the access rules and audit rules associated with a registry key.</summary>
		// Token: 0x04000B9C RID: 2972
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a registry key.</summary>
		// Token: 0x04000B9D RID: 2973
		TakeOwnership = 524288,
		/// <summary>The right to exert full control over a registry key, and to modify its access rules and audit rules.</summary>
		// Token: 0x04000B9E RID: 2974
		FullControl = 983103
	}
}

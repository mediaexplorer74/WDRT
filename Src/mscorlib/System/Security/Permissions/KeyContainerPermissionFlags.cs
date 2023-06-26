using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of key container access allowed.</summary>
	// Token: 0x02000312 RID: 786
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum KeyContainerPermissionFlags
	{
		/// <summary>No access to a key container.</summary>
		// Token: 0x04000F66 RID: 3942
		NoFlags = 0,
		/// <summary>Create a key container.</summary>
		// Token: 0x04000F67 RID: 3943
		Create = 1,
		/// <summary>Open a key container and use the public key.</summary>
		// Token: 0x04000F68 RID: 3944
		Open = 2,
		/// <summary>Delete a key container.</summary>
		// Token: 0x04000F69 RID: 3945
		Delete = 4,
		/// <summary>Import a key into a key container.</summary>
		// Token: 0x04000F6A RID: 3946
		Import = 16,
		/// <summary>Export a key from a key container.</summary>
		// Token: 0x04000F6B RID: 3947
		Export = 32,
		/// <summary>Sign a file using a key.</summary>
		// Token: 0x04000F6C RID: 3948
		Sign = 256,
		/// <summary>Decrypt a key container.</summary>
		// Token: 0x04000F6D RID: 3949
		Decrypt = 512,
		/// <summary>View the access control list (ACL) for a key container.</summary>
		// Token: 0x04000F6E RID: 3950
		ViewAcl = 4096,
		/// <summary>Change the access control list (ACL) for a key container.</summary>
		// Token: 0x04000F6F RID: 3951
		ChangeAcl = 8192,
		/// <summary>Create, decrypt, delete, and open a key container; export and import a key; sign files using a key; and view and change the access control list for a key container.</summary>
		// Token: 0x04000F70 RID: 3952
		AllFlags = 13111
	}
}

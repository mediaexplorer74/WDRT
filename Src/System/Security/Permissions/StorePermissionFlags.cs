using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted access to X.509 certificate stores.</summary>
	// Token: 0x02000485 RID: 1157
	[Flags]
	[Serializable]
	public enum StorePermissionFlags
	{
		/// <summary>Permission is not given to perform any certificate or store operations.</summary>
		// Token: 0x04002648 RID: 9800
		NoFlags = 0,
		/// <summary>The ability to create a new store.</summary>
		// Token: 0x04002649 RID: 9801
		CreateStore = 1,
		/// <summary>The ability to delete a store.</summary>
		// Token: 0x0400264A RID: 9802
		DeleteStore = 2,
		/// <summary>The ability to enumerate the stores on a computer.</summary>
		// Token: 0x0400264B RID: 9803
		EnumerateStores = 4,
		/// <summary>The ability to open a store.</summary>
		// Token: 0x0400264C RID: 9804
		OpenStore = 16,
		/// <summary>The ability to add a certificate to a store.</summary>
		// Token: 0x0400264D RID: 9805
		AddToStore = 32,
		/// <summary>The ability to remove a certificate from a store.</summary>
		// Token: 0x0400264E RID: 9806
		RemoveFromStore = 64,
		/// <summary>The ability to enumerate the certificates in a store.</summary>
		// Token: 0x0400264F RID: 9807
		EnumerateCertificates = 128,
		/// <summary>The ability to perform all certificate and store operations.</summary>
		// Token: 0x04002650 RID: 9808
		AllFlags = 247
	}
}

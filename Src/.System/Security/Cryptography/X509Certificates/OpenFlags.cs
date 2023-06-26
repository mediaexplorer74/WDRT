using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the way to open the X.509 certificate store.</summary>
	// Token: 0x0200047E RID: 1150
	[Flags]
	public enum OpenFlags
	{
		/// <summary>Open the X.509 certificate store for reading only.</summary>
		// Token: 0x04002633 RID: 9779
		ReadOnly = 0,
		/// <summary>Open the X.509 certificate store for both reading and writing.</summary>
		// Token: 0x04002634 RID: 9780
		ReadWrite = 1,
		/// <summary>Open the X.509 certificate store for the highest access allowed.</summary>
		// Token: 0x04002635 RID: 9781
		MaxAllowed = 2,
		/// <summary>Opens only existing stores; if no store exists, the <see cref="M:System.Security.Cryptography.X509Certificates.X509Store.Open(System.Security.Cryptography.X509Certificates.OpenFlags)" /> method will not create a new store.</summary>
		// Token: 0x04002636 RID: 9782
		OpenExistingOnly = 4,
		/// <summary>Open the X.509 certificate store and include archived certificates.</summary>
		// Token: 0x04002637 RID: 9783
		IncludeArchived = 8
	}
}

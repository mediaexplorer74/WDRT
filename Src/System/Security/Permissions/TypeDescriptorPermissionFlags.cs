using System;

namespace System.Security.Permissions
{
	/// <summary>Defines permission settings for type descriptors.</summary>
	// Token: 0x02000486 RID: 1158
	[Flags]
	[Serializable]
	public enum TypeDescriptorPermissionFlags
	{
		/// <summary>No permission flags are set on the type descriptor.</summary>
		// Token: 0x04002652 RID: 9810
		NoFlags = 0,
		/// <summary>The type descriptor may be called from partially trusted code.</summary>
		// Token: 0x04002653 RID: 9811
		RestrictedRegistrationAccess = 1
	}
}

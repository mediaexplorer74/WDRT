using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the type of access control modification to perform. This enumeration is used by methods of the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> class and its descendents.</summary>
	// Token: 0x02000223 RID: 547
	public enum AccessControlModification
	{
		/// <summary>Add the specified authorization rule to the access control list (ACL).</summary>
		// Token: 0x04000B57 RID: 2903
		Add,
		/// <summary>Remove all authorization rules from the ACL, then add the specified authorization rule to the ACL.</summary>
		// Token: 0x04000B58 RID: 2904
		Set,
		/// <summary>Remove authorization rules that contain the same SID as the specified authorization rule from the ACL, and then add the specified authorization rule to the ACL.</summary>
		// Token: 0x04000B59 RID: 2905
		Reset,
		/// <summary>Remove authorization rules that contain the same security identifier (SID) and access mask as the specified authorization rule from the ACL.</summary>
		// Token: 0x04000B5A RID: 2906
		Remove,
		/// <summary>Remove authorization rules that contain the same SID as the specified authorization rule from the ACL.</summary>
		// Token: 0x04000B5B RID: 2907
		RemoveAll,
		/// <summary>Remove authorization rules that exactly match the specified authorization rule from the ACL.</summary>
		// Token: 0x04000B5C RID: 2908
		RemoveSpecific
	}
}

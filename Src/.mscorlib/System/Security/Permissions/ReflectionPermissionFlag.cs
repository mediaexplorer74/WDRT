using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted use of the <see cref="N:System.Reflection" /> and <see cref="N:System.Reflection.Emit" /> namespaces.</summary>
	// Token: 0x02000300 RID: 768
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum ReflectionPermissionFlag
	{
		/// <summary>Enumeration of types and members is allowed. Invocation operations are allowed on visible types and members.</summary>
		// Token: 0x04000F1E RID: 3870
		NoFlags = 0,
		/// <summary>This flag is obsolete. No flags are necessary to enumerate types and members and to examine their metadata. Use <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.NoFlags" /> instead.</summary>
		// Token: 0x04000F1F RID: 3871
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		TypeInformation = 1,
		/// <summary>Invocation operations on all members are allowed, regardless of grant set. If this flag is not set, invocation operations are allowed only on visible members.</summary>
		// Token: 0x04000F20 RID: 3872
		MemberAccess = 2,
		/// <summary>Emitting debug symbols is allowed. Beginning with the .NET Framework 2.0 Service Pack 1, this flag is no longer required to emit code.</summary>
		// Token: 0x04000F21 RID: 3873
		[Obsolete("This permission is no longer used by the CLR.")]
		ReflectionEmit = 4,
		/// <summary>Restricted member access is provided for partially trusted code. Partially trusted code can access nonpublic types and members, but only if the grant set of the partially trusted code includes all permissions in the grant set of the assembly that contains the nonpublic types and members being accessed. This flag is new in the .NET Framework 2.0 SP1.</summary>
		// Token: 0x04000F22 RID: 3874
		[ComVisible(false)]
		RestrictedMemberAccess = 8,
		/// <summary>
		///   <see langword="TypeInformation" /> , <see langword="MemberAccess" />, and <see langword="ReflectionEmit" /> are set. <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.AllFlags" /> does not include <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.RestrictedMemberAccess" />.</summary>
		// Token: 0x04000F23 RID: 3875
		[Obsolete("This permission has been deprecated. Use PermissionState.Unrestricted to get full access.")]
		AllFlags = 7
	}
}

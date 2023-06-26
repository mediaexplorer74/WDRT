using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines the privileges of the user account associated with the access token.</summary>
	// Token: 0x02000325 RID: 805
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TokenAccessLevels
	{
		/// <summary>The user can attach a primary token to a process.</summary>
		// Token: 0x0400101B RID: 4123
		AssignPrimary = 1,
		/// <summary>The user can duplicate the token.</summary>
		// Token: 0x0400101C RID: 4124
		Duplicate = 2,
		/// <summary>The user can impersonate a client.</summary>
		// Token: 0x0400101D RID: 4125
		Impersonate = 4,
		/// <summary>The user can query the token.</summary>
		// Token: 0x0400101E RID: 4126
		Query = 8,
		/// <summary>The user can query the source of the token.</summary>
		// Token: 0x0400101F RID: 4127
		QuerySource = 16,
		/// <summary>The user can enable or disable privileges in the token.</summary>
		// Token: 0x04001020 RID: 4128
		AdjustPrivileges = 32,
		/// <summary>The user can change the attributes of the groups in the token.</summary>
		// Token: 0x04001021 RID: 4129
		AdjustGroups = 64,
		/// <summary>The user can change the default owner, primary group, or discretionary access control list (DACL) of the token.</summary>
		// Token: 0x04001022 RID: 4130
		AdjustDefault = 128,
		/// <summary>The user can adjust the session identifier of the token.</summary>
		// Token: 0x04001023 RID: 4131
		AdjustSessionId = 256,
		/// <summary>The user has standard read rights and the <see cref="F:System.Security.Principal.TokenAccessLevels.Query" /> privilege for the token.</summary>
		// Token: 0x04001024 RID: 4132
		Read = 131080,
		/// <summary>The user has standard write rights and the <see cref="F:System.Security.Principal.TokenAccessLevels.AdjustPrivileges" />, <see cref="F:System.Security.Principal.TokenAccessLevels.AdjustGroups" /> and <see cref="F:System.Security.Principal.TokenAccessLevels.AdjustDefault" /> privileges for the token.</summary>
		// Token: 0x04001025 RID: 4133
		Write = 131296,
		/// <summary>The user has all possible access to the token.</summary>
		// Token: 0x04001026 RID: 4134
		AllAccess = 983551,
		/// <summary>The maximum value that can be assigned for the <see cref="T:System.Security.Principal.TokenAccessLevels" /> enumeration.</summary>
		// Token: 0x04001027 RID: 4135
		MaximumAllowed = 33554432
	}
}

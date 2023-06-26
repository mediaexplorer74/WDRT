using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Specifies the type of Windows account used.</summary>
	// Token: 0x02000327 RID: 807
	[ComVisible(true)]
	[Serializable]
	public enum WindowsAccountType
	{
		/// <summary>A standard user account.</summary>
		// Token: 0x0400102F RID: 4143
		Normal,
		/// <summary>A Windows guest account.</summary>
		// Token: 0x04001030 RID: 4144
		Guest,
		/// <summary>A Windows system account.</summary>
		// Token: 0x04001031 RID: 4145
		System,
		/// <summary>An anonymous account.</summary>
		// Token: 0x04001032 RID: 4146
		Anonymous
	}
}

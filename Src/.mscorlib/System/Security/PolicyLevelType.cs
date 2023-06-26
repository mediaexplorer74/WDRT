using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Specifies the type of a managed code policy level.</summary>
	// Token: 0x020001F2 RID: 498
	[ComVisible(true)]
	[Serializable]
	public enum PolicyLevelType
	{
		/// <summary>Security policy for all managed code that is run by the user.</summary>
		// Token: 0x04000A8B RID: 2699
		User,
		/// <summary>Security policy for all managed code that is run on the computer.</summary>
		// Token: 0x04000A8C RID: 2700
		Machine,
		/// <summary>Security policy for all managed code in an enterprise.</summary>
		// Token: 0x04000A8D RID: 2701
		Enterprise,
		/// <summary>Security policy for all managed code in an application.</summary>
		// Token: 0x04000A8E RID: 2702
		AppDomain
	}
}

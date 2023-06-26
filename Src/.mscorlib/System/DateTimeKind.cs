using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies whether a <see cref="T:System.DateTime" /> object represents a local time, a Coordinated Universal Time (UTC), or is not specified as either local time or UTC.</summary>
	// Token: 0x0200007E RID: 126
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum DateTimeKind
	{
		/// <summary>The time represented is not specified as either local time or Coordinated Universal Time (UTC).</summary>
		// Token: 0x040002E7 RID: 743
		[__DynamicallyInvokable]
		Unspecified,
		/// <summary>The time represented is UTC.</summary>
		// Token: 0x040002E8 RID: 744
		[__DynamicallyInvokable]
		Utc,
		/// <summary>The time represented is local time.</summary>
		// Token: 0x040002E9 RID: 745
		[__DynamicallyInvokable]
		Local
	}
}

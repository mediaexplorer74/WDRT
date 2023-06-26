using System;

namespace System.Net.NetworkInformation
{
	/// <summary>The scope level for an IPv6 address.</summary>
	// Token: 0x020002A3 RID: 675
	[global::__DynamicallyInvokable]
	public enum ScopeLevel
	{
		/// <summary>The scope level is not specified.</summary>
		// Token: 0x040018B8 RID: 6328
		[global::__DynamicallyInvokable]
		None,
		/// <summary>The scope is interface-level.</summary>
		// Token: 0x040018B9 RID: 6329
		[global::__DynamicallyInvokable]
		Interface,
		/// <summary>The scope is link-level.</summary>
		// Token: 0x040018BA RID: 6330
		[global::__DynamicallyInvokable]
		Link,
		/// <summary>The scope is subnet-level.</summary>
		// Token: 0x040018BB RID: 6331
		[global::__DynamicallyInvokable]
		Subnet,
		/// <summary>The scope is admin-level.</summary>
		// Token: 0x040018BC RID: 6332
		[global::__DynamicallyInvokable]
		Admin,
		/// <summary>The scope is site-level.</summary>
		// Token: 0x040018BD RID: 6333
		[global::__DynamicallyInvokable]
		Site,
		/// <summary>The scope is organization-level.</summary>
		// Token: 0x040018BE RID: 6334
		[global::__DynamicallyInvokable]
		Organization = 8,
		/// <summary>The scope is global.</summary>
		// Token: 0x040018BF RID: 6335
		[global::__DynamicallyInvokable]
		Global = 14
	}
}

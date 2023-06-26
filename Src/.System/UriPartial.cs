using System;

namespace System
{
	/// <summary>Defines the parts of a URI for the <see cref="M:System.Uri.GetLeftPart(System.UriPartial)" /> method.</summary>
	// Token: 0x02000041 RID: 65
	public enum UriPartial
	{
		/// <summary>The scheme segment of the URI.</summary>
		// Token: 0x04000444 RID: 1092
		Scheme,
		/// <summary>The scheme and authority segments of the URI.</summary>
		// Token: 0x04000445 RID: 1093
		Authority,
		/// <summary>The scheme, authority, and path segments of the URI.</summary>
		// Token: 0x04000446 RID: 1094
		Path,
		/// <summary>The scheme, authority, path, and query segments of the URI.</summary>
		// Token: 0x04000447 RID: 1095
		Query
	}
}

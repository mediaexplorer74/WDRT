using System;

namespace System.Runtime.Versioning
{
	/// <summary>Identifies the scope of a sharable resource.</summary>
	// Token: 0x0200071F RID: 1823
	[Flags]
	public enum ResourceScope
	{
		/// <summary>There is no shared state.</summary>
		// Token: 0x0400241B RID: 9243
		None = 0,
		/// <summary>The state is shared by objects within the machine.</summary>
		// Token: 0x0400241C RID: 9244
		Machine = 1,
		/// <summary>The state is shared within a process.</summary>
		// Token: 0x0400241D RID: 9245
		Process = 2,
		/// <summary>The state is shared by objects within an <see cref="T:System.AppDomain" />.</summary>
		// Token: 0x0400241E RID: 9246
		AppDomain = 4,
		/// <summary>The state is shared by objects within a library.</summary>
		// Token: 0x0400241F RID: 9247
		Library = 8,
		/// <summary>The resource is visible to only the type.</summary>
		// Token: 0x04002420 RID: 9248
		Private = 16,
		/// <summary>The resource is visible at an assembly scope.</summary>
		// Token: 0x04002421 RID: 9249
		Assembly = 32
	}
}

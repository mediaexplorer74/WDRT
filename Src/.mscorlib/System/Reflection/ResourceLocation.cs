using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the resource location.</summary>
	// Token: 0x020005F2 RID: 1522
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum ResourceLocation
	{
		/// <summary>Specifies an embedded (that is, non-linked) resource.</summary>
		// Token: 0x04001CE4 RID: 7396
		[__DynamicallyInvokable]
		Embedded = 1,
		/// <summary>Specifies that the resource is contained in another assembly.</summary>
		// Token: 0x04001CE5 RID: 7397
		[__DynamicallyInvokable]
		ContainedInAnotherAssembly = 2,
		/// <summary>Specifies that the resource is contained in the manifest file.</summary>
		// Token: 0x04001CE6 RID: 7398
		[__DynamicallyInvokable]
		ContainedInManifestFile = 4
	}
}

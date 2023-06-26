using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	/// <summary>Specifies whether a <see cref="T:System.Resources.ResourceManager" /> object looks for the resources of the app's default culture in the main assembly or in a satellite assembly.</summary>
	// Token: 0x0200039D RID: 925
	[ComVisible(true)]
	[Serializable]
	public enum UltimateResourceFallbackLocation
	{
		/// <summary>Fallback resources are located in the main assembly.</summary>
		// Token: 0x0400129C RID: 4764
		MainAssembly,
		/// <summary>Fallback resources are located in a satellite assembly.</summary>
		// Token: 0x0400129D RID: 4765
		Satellite
	}
}

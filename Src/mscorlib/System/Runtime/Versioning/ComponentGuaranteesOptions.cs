using System;

namespace System.Runtime.Versioning
{
	/// <summary>Describes the compatibility guarantee of a component, type, or type member that may span multiple versions.</summary>
	// Token: 0x0200071B RID: 1819
	[Flags]
	[Serializable]
	public enum ComponentGuaranteesOptions
	{
		/// <summary>The developer does not guarantee compatibility across versions. Consumers of the component, type, or member can expect future versions to break the existing client.</summary>
		// Token: 0x04002412 RID: 9234
		None = 0,
		/// <summary>The developer promises multi-version exchange compatibility for the type. Consumers of the type can expect compatibility across future versions and can use the type in all their interfaces. Versioning problems cannot be fixed by side-by-side execution.</summary>
		// Token: 0x04002413 RID: 9235
		Exchange = 1,
		/// <summary>The developer promises stable compatibility across versions. Consumers of the type can expect that future versions will not break the existing client. However, if they do and if the client has not used the type in its interfaces, side-by-side execution may fix the problem.</summary>
		// Token: 0x04002414 RID: 9236
		Stable = 2,
		/// <summary>The component has been tested to work when more than one version of the assembly is loaded into the same application domain. Future versions can break compatibility. However, when such breaking changes are made, the old version is not modified but continues to exist alongside the new version.</summary>
		// Token: 0x04002415 RID: 9237
		SideBySide = 4
	}
}

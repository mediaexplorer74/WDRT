using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>A class whose static <see cref="M:System.Runtime.CompilerServices.RuntimeFeature.IsSupported(System.String)" /> method checks whether a specified feature is supported by the common language runtime.</summary>
	// Token: 0x020008AA RID: 2218
	public static class RuntimeFeature
	{
		/// <summary>Determines whether a specified feature is supported by the common language runtime.</summary>
		/// <param name="feature">The name of the feature.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="feature" /> is supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005DAE RID: 23982 RVA: 0x0014AB74 File Offset: 0x00148D74
		public static bool IsSupported(string feature)
		{
			return feature == "PortablePdb" && !AppContextSwitches.IgnorePortablePDBsInStackTraces;
		}

		/// <summary>Gets the name of the portable PDB feature.</summary>
		// Token: 0x04002A18 RID: 10776
		public const string PortablePdb = "PortablePdb";
	}
}

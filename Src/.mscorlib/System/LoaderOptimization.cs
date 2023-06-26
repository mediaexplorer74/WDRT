using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>An enumeration used with the <see cref="T:System.LoaderOptimizationAttribute" /> class to specify loader optimizations for an executable.</summary>
	// Token: 0x020000A0 RID: 160
	[ComVisible(true)]
	[Serializable]
	public enum LoaderOptimization
	{
		/// <summary>Indicates that no optimizations for sharing internal resources are specified. If the default domain or hosting interface specified an optimization, then the loader uses that; otherwise, the loader uses <see cref="F:System.LoaderOptimization.SingleDomain" />.</summary>
		// Token: 0x040003B8 RID: 952
		NotSpecified,
		/// <summary>Indicates that the application will probably have a single domain, and loader must not share internal resources across application domains.</summary>
		// Token: 0x040003B9 RID: 953
		SingleDomain,
		/// <summary>Indicates that the application will probably have many domains that use the same code, and the loader must share maximal internal resources across application domains.</summary>
		// Token: 0x040003BA RID: 954
		MultiDomain,
		/// <summary>Indicates that the application will probably host unique code in multiple domains, and the loader must share resources across application domains only for globally available (strong-named) assemblies that have been added to the global assembly cache.</summary>
		// Token: 0x040003BB RID: 955
		MultiDomainHost,
		/// <summary>Do not use. This mask selects the domain-related values, screening out the unused <see cref="F:System.LoaderOptimization.DisallowBindings" /> flag.</summary>
		// Token: 0x040003BC RID: 956
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		DomainMask = 3,
		/// <summary>Ignored by the common language runtime.</summary>
		// Token: 0x040003BD RID: 957
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		DisallowBindings
	}
}

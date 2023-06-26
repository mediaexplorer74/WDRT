using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Defines the types of connections to a class object.</summary>
	// Token: 0x02000973 RID: 2419
	[Flags]
	public enum RegistrationConnectionType
	{
		/// <summary>Once an application is connected to a class object with <see langword="CoGetClassObject" />, the class object is removed from public view so that no other applications can connect to it. This value is commonly used for single document interface (SDI) applications.</summary>
		// Token: 0x04002BD6 RID: 11222
		SingleUse = 0,
		/// <summary>Multiple applications can connect to the class object through calls to <see langword="CoGetClassObject" />.</summary>
		// Token: 0x04002BD7 RID: 11223
		MultipleUse = 1,
		/// <summary>Registers separate CLSCTX_LOCAL_SERVER and CLSCTX_INPROC_SERVER class factories.</summary>
		// Token: 0x04002BD8 RID: 11224
		MultiSeparate = 2,
		/// <summary>Suspends registration and activation requests for the specified CLSID until there is a call to <see langword="CoResumeClassObjects" />.</summary>
		// Token: 0x04002BD9 RID: 11225
		Suspended = 4,
		/// <summary>The class object is a surrogate process used to run DLL servers.</summary>
		// Token: 0x04002BDA RID: 11226
		Surrogate = 8
	}
}

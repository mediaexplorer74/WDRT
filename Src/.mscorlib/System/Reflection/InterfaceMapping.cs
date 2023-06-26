using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Retrieves the mapping of an interface into the actual methods on a class that implements that interface.</summary>
	// Token: 0x020005EC RID: 1516
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public struct InterfaceMapping
	{
		/// <summary>Represents the type that was used to create the interface mapping.</summary>
		// Token: 0x04001CD7 RID: 7383
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public Type TargetType;

		/// <summary>Shows the type that represents the interface.</summary>
		// Token: 0x04001CD8 RID: 7384
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public Type InterfaceType;

		/// <summary>Shows the methods that implement the interface.</summary>
		// Token: 0x04001CD9 RID: 7385
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public MethodInfo[] TargetMethods;

		/// <summary>Shows the methods that are defined on the interface.</summary>
		// Token: 0x04001CDA RID: 7386
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public MethodInfo[] InterfaceMethods;
	}
}

using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that any private members contained in an assembly's types are not available to reflection.</summary>
	// Token: 0x020008B0 RID: 2224
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class DisablePrivateReflectionAttribute : Attribute
	{
		/// <summary>Initializes a new instances of the <see cref="T:System.Runtime.CompilerServices.DisablePrivateReflectionAttribute" /> class.</summary>
		// Token: 0x06005DBB RID: 23995 RVA: 0x0014AEDC File Offset: 0x001490DC
		[__DynamicallyInvokable]
		public DisablePrivateReflectionAttribute()
		{
		}
	}
}

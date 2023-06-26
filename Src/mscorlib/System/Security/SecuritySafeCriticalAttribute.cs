using System;

namespace System.Security
{
	/// <summary>Identifies types or members as security-critical and safely accessible by transparent code.</summary>
	// Token: 0x020001C9 RID: 457
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class SecuritySafeCriticalAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecuritySafeCriticalAttribute" /> class.</summary>
		// Token: 0x06001C22 RID: 7202 RVA: 0x00060E51 File Offset: 0x0005F051
		[__DynamicallyInvokable]
		public SecuritySafeCriticalAttribute()
		{
		}
	}
}

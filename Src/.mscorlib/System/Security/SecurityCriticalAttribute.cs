using System;

namespace System.Security
{
	/// <summary>Specifies that code or an assembly performs security-critical operations.</summary>
	// Token: 0x020001C7 RID: 455
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class SecurityCriticalAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityCriticalAttribute" /> class.</summary>
		// Token: 0x06001C1E RID: 7198 RVA: 0x00060E2A File Offset: 0x0005F02A
		[__DynamicallyInvokable]
		public SecurityCriticalAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityCriticalAttribute" /> class with the specified scope.</summary>
		/// <param name="scope">One of the enumeration values that specifies the scope of the attribute.</param>
		// Token: 0x06001C1F RID: 7199 RVA: 0x00060E32 File Offset: 0x0005F032
		public SecurityCriticalAttribute(SecurityCriticalScope scope)
		{
			this._val = scope;
		}

		/// <summary>Gets the scope for the attribute.</summary>
		/// <returns>One of the enumeration values that specifies the scope of the attribute. The default is <see cref="F:System.Security.SecurityCriticalScope.Explicit" />, which indicates that the attribute applies only to the immediate target.</returns>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x00060E41 File Offset: 0x0005F041
		[Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
		public SecurityCriticalScope Scope
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040009C5 RID: 2501
		private SecurityCriticalScope _val;
	}
}

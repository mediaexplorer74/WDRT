using System;

namespace System.Security
{
	/// <summary>Specifies that an assembly cannot cause an elevation of privilege.</summary>
	// Token: 0x020001CA RID: 458
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class SecurityTransparentAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityTransparentAttribute" /> class.</summary>
		// Token: 0x06001C23 RID: 7203 RVA: 0x00060E59 File Offset: 0x0005F059
		[__DynamicallyInvokable]
		public SecurityTransparentAttribute()
		{
		}
	}
}

using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that data should be marshaled from the caller to the callee, but not back to the caller.</summary>
	// Token: 0x0200092D RID: 2349
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class InAttribute : Attribute
	{
		// Token: 0x06006046 RID: 24646 RVA: 0x0014D245 File Offset: 0x0014B445
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsIn)
			{
				return null;
			}
			return new InAttribute();
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x0014D256 File Offset: 0x0014B456
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsIn;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InAttribute" /> class.</summary>
		// Token: 0x06006048 RID: 24648 RVA: 0x0014D25E File Offset: 0x0014B45E
		[__DynamicallyInvokable]
		public InAttribute()
		{
		}
	}
}

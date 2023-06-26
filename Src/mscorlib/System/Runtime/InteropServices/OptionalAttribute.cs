using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that a parameter is optional.</summary>
	// Token: 0x0200092F RID: 2351
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class OptionalAttribute : Attribute
	{
		// Token: 0x0600604C RID: 24652 RVA: 0x0014D287 File Offset: 0x0014B487
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOptional)
			{
				return null;
			}
			return new OptionalAttribute();
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x0014D298 File Offset: 0x0014B498
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOptional;
		}

		/// <summary>Initializes a new instance of the <see langword="OptionalAttribute" /> class with default values.</summary>
		// Token: 0x0600604E RID: 24654 RVA: 0x0014D2A0 File Offset: 0x0014B4A0
		[__DynamicallyInvokable]
		public OptionalAttribute()
		{
		}
	}
}

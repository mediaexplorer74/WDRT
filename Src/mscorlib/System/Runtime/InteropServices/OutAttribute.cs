using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that data should be marshaled from callee back to caller.</summary>
	// Token: 0x0200092E RID: 2350
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class OutAttribute : Attribute
	{
		// Token: 0x06006049 RID: 24649 RVA: 0x0014D266 File Offset: 0x0014B466
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOut)
			{
				return null;
			}
			return new OutAttribute();
		}

		// Token: 0x0600604A RID: 24650 RVA: 0x0014D277 File Offset: 0x0014B477
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOut;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.OutAttribute" /> class.</summary>
		// Token: 0x0600604B RID: 24651 RVA: 0x0014D27F File Offset: 0x0014B47F
		[__DynamicallyInvokable]
		public OutAttribute()
		{
		}
	}
}

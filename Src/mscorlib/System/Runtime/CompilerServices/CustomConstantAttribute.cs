using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines a constant value that a compiler can persist for a field or method parameter.</summary>
	// Token: 0x020008AC RID: 2220
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class CustomConstantAttribute : Attribute
	{
		/// <summary>Gets the constant value stored by this attribute.</summary>
		/// <returns>The constant value stored by this attribute.</returns>
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06005DB0 RID: 23984
		[__DynamicallyInvokable]
		public abstract object Value
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x06005DB1 RID: 23985 RVA: 0x0014AB98 File Offset: 0x00148D98
		internal static object GetRawConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return customAttributeNamedArgument.TypedValue.Value;
				}
			}
			return DBNull.Value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CustomConstantAttribute" /> class.</summary>
		// Token: 0x06005DB2 RID: 23986 RVA: 0x0014AC10 File Offset: 0x00148E10
		[__DynamicallyInvokable]
		protected CustomConstantAttribute()
		{
		}
	}
}

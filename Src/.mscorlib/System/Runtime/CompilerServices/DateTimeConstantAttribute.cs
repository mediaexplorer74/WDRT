using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Persists an 8-byte <see cref="T:System.DateTime" /> constant for a field or parameter.</summary>
	// Token: 0x020008AD RID: 2221
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DateTimeConstantAttribute : CustomConstantAttribute
	{
		/// <summary>Initializes a new instance of the <see langword="DateTimeConstantAttribute" /> class with the number of 100-nanosecond ticks that represent the date and time of this instance.</summary>
		/// <param name="ticks">The number of 100-nanosecond ticks that represent the date and time of this instance.</param>
		// Token: 0x06005DB3 RID: 23987 RVA: 0x0014AC18 File Offset: 0x00148E18
		[__DynamicallyInvokable]
		public DateTimeConstantAttribute(long ticks)
		{
			this.date = new DateTime(ticks);
		}

		/// <summary>Gets the number of 100-nanosecond ticks that represent the date and time of this instance.</summary>
		/// <returns>The number of 100-nanosecond ticks that represent the date and time of this instance.</returns>
		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06005DB4 RID: 23988 RVA: 0x0014AC2C File Offset: 0x00148E2C
		[__DynamicallyInvokable]
		public override object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.date;
			}
		}

		// Token: 0x06005DB5 RID: 23989 RVA: 0x0014AC3C File Offset: 0x00148E3C
		internal static DateTime GetRawDateTimeConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return new DateTime((long)customAttributeNamedArgument.TypedValue.Value);
				}
			}
			return new DateTime((long)attr.ConstructorArguments[0].Value);
		}

		// Token: 0x04002A19 RID: 10777
		private DateTime date;
	}
}

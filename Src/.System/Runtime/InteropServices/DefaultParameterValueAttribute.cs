using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Sets the default value of a parameter when called from a language that supports default parameters. This class cannot be inherited.</summary>
	// Token: 0x020003DB RID: 987
	[AttributeUsage(AttributeTargets.Parameter)]
	[global::__DynamicallyInvokable]
	public sealed class DefaultParameterValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DefaultParameterValueAttribute" /> class with the default value of a parameter.</summary>
		/// <param name="value">An object that represents the default value of a parameter.</param>
		// Token: 0x060025EB RID: 9707 RVA: 0x000B0260 File Offset: 0x000AE460
		[global::__DynamicallyInvokable]
		public DefaultParameterValueAttribute(object value)
		{
			this.value = value;
		}

		/// <summary>Gets the default value of a parameter.</summary>
		/// <returns>An object that represents the default value of a parameter.</returns>
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x000B026F File Offset: 0x000AE46F
		[global::__DynamicallyInvokable]
		public object Value
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400206C RID: 8300
		private object value;
	}
}

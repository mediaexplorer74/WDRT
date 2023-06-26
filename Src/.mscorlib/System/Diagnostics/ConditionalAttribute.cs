using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Indicates to compilers that a method call or attribute should be ignored unless a specified conditional compilation symbol is defined.</summary>
	// Token: 0x020003E3 RID: 995
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ConditionalAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConditionalAttribute" /> class.</summary>
		/// <param name="conditionString">A string that specifies the case-sensitive conditional compilation symbol that is associated with the attribute.</param>
		// Token: 0x06003311 RID: 13073 RVA: 0x000C62AA File Offset: 0x000C44AA
		[__DynamicallyInvokable]
		public ConditionalAttribute(string conditionString)
		{
			this.m_conditionString = conditionString;
		}

		/// <summary>Gets the conditional compilation symbol that is associated with the <see cref="T:System.Diagnostics.ConditionalAttribute" /> attribute.</summary>
		/// <returns>A string that specifies the case-sensitive conditional compilation symbol that is associated with the <see cref="T:System.Diagnostics.ConditionalAttribute" /> attribute.</returns>
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x000C62B9 File Offset: 0x000C44B9
		[__DynamicallyInvokable]
		public string ConditionString
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_conditionString;
			}
		}

		// Token: 0x040016A7 RID: 5799
		private string m_conditionString;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a trademark custom attribute for an assembly manifest.</summary>
	// Token: 0x020005B3 RID: 1459
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyTrademarkAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyTrademarkAttribute" /> class.</summary>
		/// <param name="trademark">The trademark information.</param>
		// Token: 0x0600446B RID: 17515 RVA: 0x000FDB33 File Offset: 0x000FBD33
		[__DynamicallyInvokable]
		public AssemblyTrademarkAttribute(string trademark)
		{
			this.m_trademark = trademark;
		}

		/// <summary>Gets trademark information.</summary>
		/// <returns>A <see langword="String" /> containing trademark information.</returns>
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x000FDB42 File Offset: 0x000FBD42
		[__DynamicallyInvokable]
		public string Trademark
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_trademark;
			}
		}

		// Token: 0x04001C06 RID: 7174
		private string m_trademark;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a company name custom attribute for an assembly manifest.</summary>
	// Token: 0x020005B5 RID: 1461
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyCompanyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCompanyAttribute" /> class.</summary>
		/// <param name="company">The company name information.</param>
		// Token: 0x0600446F RID: 17519 RVA: 0x000FDB61 File Offset: 0x000FBD61
		[__DynamicallyInvokable]
		public AssemblyCompanyAttribute(string company)
		{
			this.m_company = company;
		}

		/// <summary>Gets company name information.</summary>
		/// <returns>A string containing the company name.</returns>
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06004470 RID: 17520 RVA: 0x000FDB70 File Offset: 0x000FBD70
		[__DynamicallyInvokable]
		public string Company
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_company;
			}
		}

		// Token: 0x04001C08 RID: 7176
		private string m_company;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies which culture the assembly supports.</summary>
	// Token: 0x020005BC RID: 1468
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyCultureAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCultureAttribute" /> class with the culture supported by the assembly being attributed.</summary>
		/// <param name="culture">The culture supported by the attributed assembly.</param>
		// Token: 0x0600447D RID: 17533 RVA: 0x000FDC10 File Offset: 0x000FBE10
		[__DynamicallyInvokable]
		public AssemblyCultureAttribute(string culture)
		{
			this.m_culture = culture;
		}

		/// <summary>Gets the supported culture of the attributed assembly.</summary>
		/// <returns>A string containing the name of the supported culture.</returns>
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600447E RID: 17534 RVA: 0x000FDC1F File Offset: 0x000FBE1F
		[__DynamicallyInvokable]
		public string Culture
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_culture;
			}
		}

		// Token: 0x04001C0F RID: 7183
		private string m_culture;
	}
}

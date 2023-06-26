using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates whether a program element is compliant with the Common Language Specification (CLS). This class cannot be inherited.</summary>
	// Token: 0x020000BE RID: 190
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class CLSCompliantAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.CLSCompliantAttribute" /> class with a Boolean value indicating whether the indicated program element is CLS-compliant.</summary>
		/// <param name="isCompliant">
		///   <see langword="true" /> if CLS-compliant; otherwise, <see langword="false" />.</param>
		// Token: 0x06000AFC RID: 2812 RVA: 0x00022BAF File Offset: 0x00020DAF
		[__DynamicallyInvokable]
		public CLSCompliantAttribute(bool isCompliant)
		{
			this.m_compliant = isCompliant;
		}

		/// <summary>Gets the Boolean value indicating whether the indicated program element is CLS-compliant.</summary>
		/// <returns>
		///   <see langword="true" /> if the program element is CLS-compliant; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00022BBE File Offset: 0x00020DBE
		[__DynamicallyInvokable]
		public bool IsCompliant
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_compliant;
			}
		}

		// Token: 0x0400045F RID: 1119
		private bool m_compliant;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies that the assembly is not fully signed when created.</summary>
	// Token: 0x020005BF RID: 1471
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyDelaySignAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDelaySignAttribute" /> class.</summary>
		/// <param name="delaySign">
		///   <see langword="true" /> if the feature this attribute represents is activated; otherwise, <see langword="false" />.</param>
		// Token: 0x06004483 RID: 17539 RVA: 0x000FDC55 File Offset: 0x000FBE55
		[__DynamicallyInvokable]
		public AssemblyDelaySignAttribute(bool delaySign)
		{
			this.m_delaySign = delaySign;
		}

		/// <summary>Gets a value indicating the state of the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if this assembly has been built as delay-signed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06004484 RID: 17540 RVA: 0x000FDC64 File Offset: 0x000FBE64
		[__DynamicallyInvokable]
		public bool DelaySign
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_delaySign;
			}
		}

		// Token: 0x04001C12 RID: 7186
		private bool m_delaySign;
	}
}

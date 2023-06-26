using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls whether Unicode characters are converted to the closest matching ANSI characters.</summary>
	// Token: 0x0200093C RID: 2364
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class BestFitMappingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.BestFitMappingAttribute" /> class set to the value of the <see cref="P:System.Runtime.InteropServices.BestFitMappingAttribute.BestFitMapping" /> property.</summary>
		/// <param name="BestFitMapping">
		///   <see langword="true" /> to indicate that best-fit mapping is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</param>
		// Token: 0x06006074 RID: 24692 RVA: 0x0014D6F9 File Offset: 0x0014B8F9
		[__DynamicallyInvokable]
		public BestFitMappingAttribute(bool BestFitMapping)
		{
			this._bestFitMapping = BestFitMapping;
		}

		/// <summary>Gets the best-fit mapping behavior when converting Unicode characters to ANSI characters.</summary>
		/// <returns>
		///   <see langword="true" /> if best-fit mapping is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06006075 RID: 24693 RVA: 0x0014D708 File Offset: 0x0014B908
		[__DynamicallyInvokable]
		public bool BestFitMapping
		{
			[__DynamicallyInvokable]
			get
			{
				return this._bestFitMapping;
			}
		}

		// Token: 0x04002B36 RID: 11062
		internal bool _bestFitMapping;

		/// <summary>Enables or disables the throwing of an exception on an unmappable Unicode character that is converted to an ANSI '?' character.</summary>
		// Token: 0x04002B37 RID: 11063
		[__DynamicallyInvokable]
		public bool ThrowOnUnmappableChar;
	}
}

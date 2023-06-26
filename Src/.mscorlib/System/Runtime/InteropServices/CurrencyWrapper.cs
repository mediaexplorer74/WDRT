using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a <see langword="VT_CY" />.</summary>
	// Token: 0x0200095B RID: 2395
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class CurrencyWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.CurrencyWrapper" /> class with the <see langword="Decimal" /> to be wrapped and marshaled as type <see langword="VT_CY" />.</summary>
		/// <param name="obj">The <see langword="Decimal" /> to be wrapped and marshaled as <see langword="VT_CY" />.</param>
		// Token: 0x0600622E RID: 25134 RVA: 0x00150DD6 File Offset: 0x0014EFD6
		[__DynamicallyInvokable]
		public CurrencyWrapper(decimal obj)
		{
			this.m_WrappedObject = obj;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.CurrencyWrapper" /> class with the object containing the <see langword="Decimal" /> to be wrapped and marshaled as type <see langword="VT_CY" />.</summary>
		/// <param name="obj">The object containing the <see langword="Decimal" /> to be wrapped and marshaled as <see langword="VT_CY" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is not a <see cref="T:System.Decimal" /> type.</exception>
		// Token: 0x0600622F RID: 25135 RVA: 0x00150DE5 File Offset: 0x0014EFE5
		[__DynamicallyInvokable]
		public CurrencyWrapper(object obj)
		{
			if (!(obj is decimal))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDecimal"), "obj");
			}
			this.m_WrappedObject = (decimal)obj;
		}

		/// <summary>Gets the wrapped object to be marshaled as type <see langword="VT_CY" />.</summary>
		/// <returns>The wrapped object to be marshaled as type <see langword="VT_CY" />.</returns>
		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06006230 RID: 25136 RVA: 0x00150E16 File Offset: 0x0014F016
		[__DynamicallyInvokable]
		public decimal WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002B94 RID: 11156
		private decimal m_WrappedObject;
	}
}

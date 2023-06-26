using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Contains the <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> that were originally imported for this method from the COM type library.</summary>
	// Token: 0x02000925 RID: 2341
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibFuncAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="TypeLibFuncAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for the attributed method as found in the type library it was imported from.</param>
		// Token: 0x0600602F RID: 24623 RVA: 0x0014D00B File Offset: 0x0014B20B
		public TypeLibFuncAttribute(TypeLibFuncFlags flags)
		{
			this._val = flags;
		}

		/// <summary>Initializes a new instance of the <see langword="TypeLibFuncAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for the attributed method as found in the type library it was imported from.</param>
		// Token: 0x06006030 RID: 24624 RVA: 0x0014D01A File Offset: 0x0014B21A
		public TypeLibFuncAttribute(short flags)
		{
			this._val = (TypeLibFuncFlags)flags;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for this method.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for this method.</returns>
		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06006031 RID: 24625 RVA: 0x0014D029 File Offset: 0x0014B229
		public TypeLibFuncFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002AB0 RID: 10928
		internal TypeLibFuncFlags _val;
	}
}

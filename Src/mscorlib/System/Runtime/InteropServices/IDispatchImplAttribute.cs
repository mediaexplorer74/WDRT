using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates which <see langword="IDispatch" /> implementation the common language runtime uses when exposing dual interfaces and dispinterfaces to COM.</summary>
	// Token: 0x0200091E RID: 2334
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[Obsolete("This attribute is deprecated and will be removed in a future version.", false)]
	[ComVisible(true)]
	public sealed class IDispatchImplAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="IDispatchImplAttribute" /> class with specified <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value.</summary>
		/// <param name="implType">Indicates which <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> enumeration will be used.</param>
		// Token: 0x06006022 RID: 24610 RVA: 0x0014CEB5 File Offset: 0x0014B0B5
		public IDispatchImplAttribute(IDispatchImplType implType)
		{
			this._val = implType;
		}

		/// <summary>Initializes a new instance of the <see langword="IDispatchImplAttribute" /> class with specified <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value.</summary>
		/// <param name="implType">Indicates which <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> enumeration will be used.</param>
		// Token: 0x06006023 RID: 24611 RVA: 0x0014CEC4 File Offset: 0x0014B0C4
		public IDispatchImplAttribute(short implType)
		{
			this._val = (IDispatchImplType)implType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value used by the class.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value used by the class.</returns>
		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06006024 RID: 24612 RVA: 0x0014CED3 File Offset: 0x0014B0D3
		public IDispatchImplType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A82 RID: 10882
		internal IDispatchImplType _val;
	}
}

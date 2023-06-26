using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates whether a managed interface is dual, dispatch-only, or <see langword="IUnknown" /> -only when exposed to COM.</summary>
	// Token: 0x02000912 RID: 2322
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class InterfaceTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> enumeration member.</summary>
		/// <param name="interfaceType">One of the <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> values that describes how the interface should be exposed to COM clients.</param>
		// Token: 0x0600600E RID: 24590 RVA: 0x0014CDCA File Offset: 0x0014AFCA
		[__DynamicallyInvokable]
		public InterfaceTypeAttribute(ComInterfaceType interfaceType)
		{
			this._val = interfaceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> enumeration member.</summary>
		/// <param name="interfaceType">Describes how the interface should be exposed to COM clients.</param>
		// Token: 0x0600600F RID: 24591 RVA: 0x0014CDD9 File Offset: 0x0014AFD9
		[__DynamicallyInvokable]
		public InterfaceTypeAttribute(short interfaceType)
		{
			this._val = (ComInterfaceType)interfaceType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> value that describes how the interface should be exposed to COM.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> value that describes how the interface should be exposed to COM.</returns>
		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06006010 RID: 24592 RVA: 0x0014CDE8 File Offset: 0x0014AFE8
		[__DynamicallyInvokable]
		public ComInterfaceType Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A72 RID: 10866
		internal ComInterfaceType _val;
	}
}

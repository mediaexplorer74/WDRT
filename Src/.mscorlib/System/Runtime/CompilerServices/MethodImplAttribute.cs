using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the details of how a method is implemented. This class cannot be inherited.</summary>
	// Token: 0x020008BD RID: 2237
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class MethodImplAttribute : Attribute
	{
		// Token: 0x06005DCB RID: 24011 RVA: 0x0014AF88 File Offset: 0x00149188
		internal MethodImplAttribute(MethodImplAttributes methodImplAttributes)
		{
			MethodImplOptions methodImplOptions = MethodImplOptions.Unmanaged | MethodImplOptions.ForwardRef | MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall | MethodImplOptions.Synchronized | MethodImplOptions.NoInlining | MethodImplOptions.AggressiveInlining | MethodImplOptions.NoOptimization | MethodImplOptions.SecurityMitigations;
			this._val = (MethodImplOptions)(methodImplAttributes & (MethodImplAttributes)methodImplOptions);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value.</summary>
		/// <param name="methodImplOptions">A <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value specifying properties of the attributed method.</param>
		// Token: 0x06005DCC RID: 24012 RVA: 0x0014AFAA File Offset: 0x001491AA
		[__DynamicallyInvokable]
		public MethodImplAttribute(MethodImplOptions methodImplOptions)
		{
			this._val = methodImplOptions;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value.</summary>
		/// <param name="value">A bitmask representing the desired <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value which specifies properties of the attributed method.</param>
		// Token: 0x06005DCD RID: 24013 RVA: 0x0014AFB9 File Offset: 0x001491B9
		public MethodImplAttribute(short value)
		{
			this._val = (MethodImplOptions)value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class.</summary>
		// Token: 0x06005DCE RID: 24014 RVA: 0x0014AFC8 File Offset: 0x001491C8
		public MethodImplAttribute()
		{
		}

		/// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value describing the attributed method.</summary>
		/// <returns>The <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value describing the attributed method.</returns>
		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06005DCF RID: 24015 RVA: 0x0014AFD0 File Offset: 0x001491D0
		[__DynamicallyInvokable]
		public MethodImplOptions Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A31 RID: 10801
		internal MethodImplOptions _val;

		/// <summary>A <see cref="T:System.Runtime.CompilerServices.MethodCodeType" /> value indicating what kind of implementation is provided for this method.</summary>
		// Token: 0x04002A32 RID: 10802
		public MethodCodeType MethodCodeType;
	}
}

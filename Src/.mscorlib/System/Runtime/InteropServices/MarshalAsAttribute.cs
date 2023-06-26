using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how to marshal the data between managed and unmanaged code.</summary>
	// Token: 0x02000929 RID: 2345
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class MarshalAsAttribute : Attribute
	{
		// Token: 0x06006035 RID: 24629 RVA: 0x0014D057 File Offset: 0x0014B257
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			return MarshalAsAttribute.GetCustomAttribute(parameter.MetadataToken, parameter.GetRuntimeModule());
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x0014D06A File Offset: 0x0014B26A
		[SecurityCritical]
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return MarshalAsAttribute.GetCustomAttribute(parameter) != null;
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x0014D075 File Offset: 0x0014B275
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			return MarshalAsAttribute.GetCustomAttribute(field.MetadataToken, field.GetRuntimeModule());
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x0014D088 File Offset: 0x0014B288
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return MarshalAsAttribute.GetCustomAttribute(field) != null;
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x0014D094 File Offset: 0x0014B294
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(int token, RuntimeModule scope)
		{
			int num = 0;
			int num2 = 0;
			string text = null;
			string text2 = null;
			string text3 = null;
			int num3 = 0;
			ConstArray fieldMarshal = ModuleHandle.GetMetadataImport(scope.GetNativeHandle()).GetFieldMarshal(token);
			if (fieldMarshal.Length == 0)
			{
				return null;
			}
			UnmanagedType unmanagedType;
			VarEnum varEnum;
			UnmanagedType unmanagedType2;
			MetadataImport.GetMarshalAs(fieldMarshal, out unmanagedType, out varEnum, out text3, out unmanagedType2, out num, out num2, out text, out text2, out num3);
			RuntimeType runtimeType = ((text3 == null || text3.Length == 0) ? null : RuntimeTypeHandle.GetTypeByNameUsingCARules(text3, scope));
			RuntimeType runtimeType2 = null;
			try
			{
				runtimeType2 = ((text == null) ? null : RuntimeTypeHandle.GetTypeByNameUsingCARules(text, scope));
			}
			catch (TypeLoadException)
			{
			}
			return new MarshalAsAttribute(unmanagedType, varEnum, runtimeType, unmanagedType2, (short)num, num2, text, runtimeType2, text2, num3);
		}

		// Token: 0x0600603A RID: 24634 RVA: 0x0014D148 File Offset: 0x0014B348
		internal MarshalAsAttribute(UnmanagedType val, VarEnum safeArraySubType, RuntimeType safeArrayUserDefinedSubType, UnmanagedType arraySubType, short sizeParamIndex, int sizeConst, string marshalType, RuntimeType marshalTypeRef, string marshalCookie, int iidParamIndex)
		{
			this._val = val;
			this.SafeArraySubType = safeArraySubType;
			this.SafeArrayUserDefinedSubType = safeArrayUserDefinedSubType;
			this.IidParameterIndex = iidParamIndex;
			this.ArraySubType = arraySubType;
			this.SizeParamIndex = sizeParamIndex;
			this.SizeConst = sizeConst;
			this.MarshalType = marshalType;
			this.MarshalTypeRef = marshalTypeRef;
			this.MarshalCookie = marshalCookie;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> enumeration member.</summary>
		/// <param name="unmanagedType">The value the data is to be marshaled as.</param>
		// Token: 0x0600603B RID: 24635 RVA: 0x0014D1A8 File Offset: 0x0014B3A8
		[__DynamicallyInvokable]
		public MarshalAsAttribute(UnmanagedType unmanagedType)
		{
			this._val = unmanagedType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value.</summary>
		/// <param name="unmanagedType">The value the data is to be marshaled as.</param>
		// Token: 0x0600603C RID: 24636 RVA: 0x0014D1B7 File Offset: 0x0014B3B7
		[__DynamicallyInvokable]
		public MarshalAsAttribute(short unmanagedType)
		{
			this._val = (UnmanagedType)unmanagedType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as.</returns>
		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x0600603D RID: 24637 RVA: 0x0014D1C6 File Offset: 0x0014B3C6
		[__DynamicallyInvokable]
		public UnmanagedType Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B06 RID: 11014
		internal UnmanagedType _val;

		/// <summary>Indicates the element type of the <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
		// Token: 0x04002B07 RID: 11015
		[__DynamicallyInvokable]
		public VarEnum SafeArraySubType;

		/// <summary>Indicates the user-defined element type of the <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
		// Token: 0x04002B08 RID: 11016
		[__DynamicallyInvokable]
		public Type SafeArrayUserDefinedSubType;

		/// <summary>Specifies the parameter index of the unmanaged <see langword="iid_is" /> attribute used by COM.</summary>
		// Token: 0x04002B09 RID: 11017
		[__DynamicallyInvokable]
		public int IidParameterIndex;

		/// <summary>Specifies the element type of the unmanaged <see cref="F:System.Runtime.InteropServices.UnmanagedType.LPArray" /> or <see cref="F:System.Runtime.InteropServices.UnmanagedType.ByValArray" />.</summary>
		// Token: 0x04002B0A RID: 11018
		[__DynamicallyInvokable]
		public UnmanagedType ArraySubType;

		/// <summary>Indicates the zero-based parameter that contains the count of array elements, similar to <see langword="size_is" /> in COM.</summary>
		// Token: 0x04002B0B RID: 11019
		[__DynamicallyInvokable]
		public short SizeParamIndex;

		/// <summary>Indicates the number of elements in the fixed-length array or the number of characters (not bytes) in a string to import.</summary>
		// Token: 0x04002B0C RID: 11020
		[__DynamicallyInvokable]
		public int SizeConst;

		/// <summary>Specifies the fully qualified name of a custom marshaler.</summary>
		// Token: 0x04002B0D RID: 11021
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public string MarshalType;

		/// <summary>Implements <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalType" /> as a type.</summary>
		// Token: 0x04002B0E RID: 11022
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public Type MarshalTypeRef;

		/// <summary>Provides additional information to a custom marshaler.</summary>
		// Token: 0x04002B0F RID: 11023
		[__DynamicallyInvokable]
		public string MarshalCookie;
	}
}

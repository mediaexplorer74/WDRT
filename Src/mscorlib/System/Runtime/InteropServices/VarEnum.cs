using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how to marshal the array elements when an array is marshaled from managed to unmanaged code as a <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
	// Token: 0x02000927 RID: 2343
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum VarEnum
	{
		/// <summary>Indicates that a value was not specified.</summary>
		// Token: 0x04002AB3 RID: 10931
		[__DynamicallyInvokable]
		VT_EMPTY,
		/// <summary>Indicates a null value, similar to a null value in SQL.</summary>
		// Token: 0x04002AB4 RID: 10932
		[__DynamicallyInvokable]
		VT_NULL,
		/// <summary>Indicates a <see langword="short" /> integer.</summary>
		// Token: 0x04002AB5 RID: 10933
		[__DynamicallyInvokable]
		VT_I2,
		/// <summary>Indicates a <see langword="long" /> integer.</summary>
		// Token: 0x04002AB6 RID: 10934
		[__DynamicallyInvokable]
		VT_I4,
		/// <summary>Indicates a <see langword="float" /> value.</summary>
		// Token: 0x04002AB7 RID: 10935
		[__DynamicallyInvokable]
		VT_R4,
		/// <summary>Indicates a <see langword="double" /> value.</summary>
		// Token: 0x04002AB8 RID: 10936
		[__DynamicallyInvokable]
		VT_R8,
		/// <summary>Indicates a currency value.</summary>
		// Token: 0x04002AB9 RID: 10937
		[__DynamicallyInvokable]
		VT_CY,
		/// <summary>Indicates a DATE value.</summary>
		// Token: 0x04002ABA RID: 10938
		[__DynamicallyInvokable]
		VT_DATE,
		/// <summary>Indicates a BSTR string.</summary>
		// Token: 0x04002ABB RID: 10939
		[__DynamicallyInvokable]
		VT_BSTR,
		/// <summary>Indicates an <see langword="IDispatch" /> pointer.</summary>
		// Token: 0x04002ABC RID: 10940
		[__DynamicallyInvokable]
		VT_DISPATCH,
		/// <summary>Indicates an SCODE.</summary>
		// Token: 0x04002ABD RID: 10941
		[__DynamicallyInvokable]
		VT_ERROR,
		/// <summary>Indicates a Boolean value.</summary>
		// Token: 0x04002ABE RID: 10942
		[__DynamicallyInvokable]
		VT_BOOL,
		/// <summary>Indicates a VARIANT <see langword="far" /> pointer.</summary>
		// Token: 0x04002ABF RID: 10943
		[__DynamicallyInvokable]
		VT_VARIANT,
		/// <summary>Indicates an <see langword="IUnknown" /> pointer.</summary>
		// Token: 0x04002AC0 RID: 10944
		[__DynamicallyInvokable]
		VT_UNKNOWN,
		/// <summary>Indicates a <see langword="decimal" /> value.</summary>
		// Token: 0x04002AC1 RID: 10945
		[__DynamicallyInvokable]
		VT_DECIMAL,
		/// <summary>Indicates a <see langword="char" /> value.</summary>
		// Token: 0x04002AC2 RID: 10946
		[__DynamicallyInvokable]
		VT_I1 = 16,
		/// <summary>Indicates a <see langword="byte" />.</summary>
		// Token: 0x04002AC3 RID: 10947
		[__DynamicallyInvokable]
		VT_UI1,
		/// <summary>Indicates an <see langword="unsigned" /><see langword="short" />.</summary>
		// Token: 0x04002AC4 RID: 10948
		[__DynamicallyInvokable]
		VT_UI2,
		/// <summary>Indicates an <see langword="unsigned" /><see langword="long" />.</summary>
		// Token: 0x04002AC5 RID: 10949
		[__DynamicallyInvokable]
		VT_UI4,
		/// <summary>Indicates a 64-bit integer.</summary>
		// Token: 0x04002AC6 RID: 10950
		[__DynamicallyInvokable]
		VT_I8,
		/// <summary>Indicates an 64-bit unsigned integer.</summary>
		// Token: 0x04002AC7 RID: 10951
		[__DynamicallyInvokable]
		VT_UI8,
		/// <summary>Indicates an integer value.</summary>
		// Token: 0x04002AC8 RID: 10952
		[__DynamicallyInvokable]
		VT_INT,
		/// <summary>Indicates an <see langword="unsigned" /> integer value.</summary>
		// Token: 0x04002AC9 RID: 10953
		[__DynamicallyInvokable]
		VT_UINT,
		/// <summary>Indicates a C style <see langword="void" />.</summary>
		// Token: 0x04002ACA RID: 10954
		[__DynamicallyInvokable]
		VT_VOID,
		/// <summary>Indicates an HRESULT.</summary>
		// Token: 0x04002ACB RID: 10955
		[__DynamicallyInvokable]
		VT_HRESULT,
		/// <summary>Indicates a pointer type.</summary>
		// Token: 0x04002ACC RID: 10956
		[__DynamicallyInvokable]
		VT_PTR,
		/// <summary>Indicates a SAFEARRAY. Not valid in a VARIANT.</summary>
		// Token: 0x04002ACD RID: 10957
		[__DynamicallyInvokable]
		VT_SAFEARRAY,
		/// <summary>Indicates a C style array.</summary>
		// Token: 0x04002ACE RID: 10958
		[__DynamicallyInvokable]
		VT_CARRAY,
		/// <summary>Indicates a user defined type.</summary>
		// Token: 0x04002ACF RID: 10959
		[__DynamicallyInvokable]
		VT_USERDEFINED,
		/// <summary>Indicates a null-terminated string.</summary>
		// Token: 0x04002AD0 RID: 10960
		[__DynamicallyInvokable]
		VT_LPSTR,
		/// <summary>Indicates a wide string terminated by <see langword="null" />.</summary>
		// Token: 0x04002AD1 RID: 10961
		[__DynamicallyInvokable]
		VT_LPWSTR,
		/// <summary>Indicates a user defined type.</summary>
		// Token: 0x04002AD2 RID: 10962
		[__DynamicallyInvokable]
		VT_RECORD = 36,
		/// <summary>Indicates a FILETIME value.</summary>
		// Token: 0x04002AD3 RID: 10963
		[__DynamicallyInvokable]
		VT_FILETIME = 64,
		/// <summary>Indicates length prefixed bytes.</summary>
		// Token: 0x04002AD4 RID: 10964
		[__DynamicallyInvokable]
		VT_BLOB,
		/// <summary>Indicates that the name of a stream follows.</summary>
		// Token: 0x04002AD5 RID: 10965
		[__DynamicallyInvokable]
		VT_STREAM,
		/// <summary>Indicates that the name of a storage follows.</summary>
		// Token: 0x04002AD6 RID: 10966
		[__DynamicallyInvokable]
		VT_STORAGE,
		/// <summary>Indicates that a stream contains an object.</summary>
		// Token: 0x04002AD7 RID: 10967
		[__DynamicallyInvokable]
		VT_STREAMED_OBJECT,
		/// <summary>Indicates that a storage contains an object.</summary>
		// Token: 0x04002AD8 RID: 10968
		[__DynamicallyInvokable]
		VT_STORED_OBJECT,
		/// <summary>Indicates that a blob contains an object.</summary>
		// Token: 0x04002AD9 RID: 10969
		[__DynamicallyInvokable]
		VT_BLOB_OBJECT,
		/// <summary>Indicates the clipboard format.</summary>
		// Token: 0x04002ADA RID: 10970
		[__DynamicallyInvokable]
		VT_CF,
		/// <summary>Indicates a class ID.</summary>
		// Token: 0x04002ADB RID: 10971
		[__DynamicallyInvokable]
		VT_CLSID,
		/// <summary>Indicates a simple, counted array.</summary>
		// Token: 0x04002ADC RID: 10972
		[__DynamicallyInvokable]
		VT_VECTOR = 4096,
		/// <summary>Indicates a <see langword="SAFEARRAY" /> pointer.</summary>
		// Token: 0x04002ADD RID: 10973
		[__DynamicallyInvokable]
		VT_ARRAY = 8192,
		/// <summary>Indicates that a value is a reference.</summary>
		// Token: 0x04002ADE RID: 10974
		[__DynamicallyInvokable]
		VT_BYREF = 16384
	}
}

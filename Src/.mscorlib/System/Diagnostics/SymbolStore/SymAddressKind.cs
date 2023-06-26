using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Specifies address types for local variables, parameters, and fields in the methods <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.DefineLocalVariable(System.String,System.Reflection.FieldAttributes,System.Byte[],System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" />, <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.DefineParameter(System.String,System.Reflection.ParameterAttributes,System.Int32,System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32)" />, and <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.DefineField(System.Diagnostics.SymbolStore.SymbolToken,System.String,System.Reflection.FieldAttributes,System.Byte[],System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32)" /> of the <see cref="T:System.Diagnostics.SymbolStore.ISymbolWriter" /> interface.</summary>
	// Token: 0x02000403 RID: 1027
	[ComVisible(true)]
	[Serializable]
	public enum SymAddressKind
	{
		/// <summary>A Microsoft intermediate language (MSIL) offset. The <paramref name="addr1" /> parameter is the MSIL local variable or parameter index.</summary>
		// Token: 0x040016F7 RID: 5879
		ILOffset = 1,
		/// <summary>A native Relevant Virtual Address (RVA). The <paramref name="addr1" /> parameter is the RVA in the module.</summary>
		// Token: 0x040016F8 RID: 5880
		NativeRVA,
		/// <summary>A native register address. The <paramref name="addr1" /> parameter is the register in which the variable is stored.</summary>
		// Token: 0x040016F9 RID: 5881
		NativeRegister,
		/// <summary>A register-relative address. The <paramref name="addr1" /> parameter is the register, and the <paramref name="addr2" /> parameter is the offset.</summary>
		// Token: 0x040016FA RID: 5882
		NativeRegisterRelative,
		/// <summary>A native offset. The <paramref name="addr1" /> parameter is the offset from the start of the parent.</summary>
		// Token: 0x040016FB RID: 5883
		NativeOffset,
		/// <summary>A register-relative address. The <paramref name="addr1" /> parameter is the low-order register, and the <paramref name="addr2" /> parameter is the high-order register.</summary>
		// Token: 0x040016FC RID: 5884
		NativeRegisterRegister,
		/// <summary>A register-relative address. The <paramref name="addr1" /> parameter is the low-order register, the <paramref name="addr2" /> parameter is the stack register, and the <paramref name="addr3" /> parameter is the offset from the stack pointer to the high-order part of the value.</summary>
		// Token: 0x040016FD RID: 5885
		NativeRegisterStack,
		/// <summary>A register-relative address. The <paramref name="addr1" /> parameter is the stack register, the <paramref name="addr2" /> parameter is the offset from the stack pointer to the low-order part of the value, and the <paramref name="addr3" /> parameter is the high-order register.</summary>
		// Token: 0x040016FE RID: 5886
		NativeStackRegister,
		/// <summary>A bit field. The <paramref name="addr1" /> parameter is the position where the field starts, and the <paramref name="addr2" /> parameter is the field length.</summary>
		// Token: 0x040016FF RID: 5887
		BitField,
		/// <summary>A native section offset. The <paramref name="addr1" /> parameter is the section, and the <paramref name="addr2" /> parameter is the offset.</summary>
		// Token: 0x04001700 RID: 5888
		NativeSectionOffset
	}
}

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	/// <summary>Specifies the data types to use when storing values in the registry, or identifies the data type of a value in the registry.</summary>
	// Token: 0x02000016 RID: 22
	[ComVisible(true)]
	public enum RegistryValueKind
	{
		/// <summary>A null-terminated string. This value is equivalent to the Win32 API registry data type REG_SZ.</summary>
		// Token: 0x040001A2 RID: 418
		String = 1,
		/// <summary>A null-terminated string that contains unexpanded references to environment variables, such as %PATH%, that are expanded when the value is retrieved. This value is equivalent to the Win32 API registry data type REG_EXPAND_SZ.</summary>
		// Token: 0x040001A3 RID: 419
		ExpandString,
		/// <summary>Binary data in any form. This value is equivalent to the Win32 API registry data type REG_BINARY.</summary>
		// Token: 0x040001A4 RID: 420
		Binary,
		/// <summary>A 32-bit binary number. This value is equivalent to the Win32 API registry data type REG_DWORD.</summary>
		// Token: 0x040001A5 RID: 421
		DWord,
		/// <summary>An array of null-terminated strings, terminated by two null characters. This value is equivalent to the Win32 API registry data type REG_MULTI_SZ.</summary>
		// Token: 0x040001A6 RID: 422
		MultiString = 7,
		/// <summary>A 64-bit binary number. This value is equivalent to the Win32 API registry data type REG_QWORD.</summary>
		// Token: 0x040001A7 RID: 423
		QWord = 11,
		/// <summary>An unsupported registry data type. For example, the Microsoft Win32 API registry data type REG_RESOURCE_LIST is unsupported. Use this value to specify that the <see cref="M:Microsoft.Win32.RegistryKey.SetValue(System.String,System.Object)" /> method should determine the appropriate registry data type when storing a name/value pair.</summary>
		// Token: 0x040001A8 RID: 424
		Unknown = 0,
		/// <summary>No data type.</summary>
		// Token: 0x040001A9 RID: 425
		[ComVisible(false)]
		None = -1
	}
}

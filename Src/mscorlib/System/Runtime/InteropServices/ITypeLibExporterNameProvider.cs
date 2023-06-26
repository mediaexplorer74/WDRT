using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides control over the casing of names when exported to a type library.</summary>
	// Token: 0x02000970 RID: 2416
	[Guid("FA1F3615-ACB9-486d-9EAC-1BEF87E36B09")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface ITypeLibExporterNameProvider
	{
		/// <summary>Returns a list of names to control the casing of.</summary>
		/// <returns>An array of strings, where each element contains the name of a type to control casing for.</returns>
		// Token: 0x06006254 RID: 25172
		[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
		string[] GetNames();
	}
}

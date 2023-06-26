using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Holds the public GUIDs for language vendors to be used with the symbol store.</summary>
	// Token: 0x02000406 RID: 1030
	[ComVisible(true)]
	public class SymLanguageVendor
	{
		/// <summary>Specifies the GUID of the Microsoft language vendor.</summary>
		// Token: 0x0400170D RID: 5901
		public static readonly Guid Microsoft = new Guid(-1723120188, -6423, 4562, 144, 63, 0, 192, 79, 163, 2, 161);
	}
}

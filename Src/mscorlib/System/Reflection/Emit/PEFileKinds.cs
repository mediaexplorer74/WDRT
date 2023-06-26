using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Specifies the type of the portable executable (PE) file.</summary>
	// Token: 0x02000651 RID: 1617
	[ComVisible(true)]
	[Serializable]
	public enum PEFileKinds
	{
		/// <summary>The portable executable (PE) file is a DLL.</summary>
		// Token: 0x04001F70 RID: 8048
		Dll = 1,
		/// <summary>The application is a console (not a Windows-based) application.</summary>
		// Token: 0x04001F71 RID: 8049
		ConsoleApplication,
		/// <summary>The application is a Windows-based application.</summary>
		// Token: 0x04001F72 RID: 8050
		WindowApplication
	}
}

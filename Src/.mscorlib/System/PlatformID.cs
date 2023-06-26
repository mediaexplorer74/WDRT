using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Identifies the operating system, or platform, supported by an assembly.</summary>
	// Token: 0x02000122 RID: 290
	[ComVisible(true)]
	[Serializable]
	public enum PlatformID
	{
		/// <summary>The operating system is Win32s. This value is no longer in use.</summary>
		// Token: 0x040005E4 RID: 1508
		Win32S,
		/// <summary>The operating system is Windows 95 or Windows 98. This value is no longer in use.</summary>
		// Token: 0x040005E5 RID: 1509
		Win32Windows,
		/// <summary>The operating system is Windows NT or later.</summary>
		// Token: 0x040005E6 RID: 1510
		Win32NT,
		/// <summary>The operating system is Windows CE. This value is no longer in use.</summary>
		// Token: 0x040005E7 RID: 1511
		WinCE,
		/// <summary>The operating system is Unix.</summary>
		// Token: 0x040005E8 RID: 1512
		Unix,
		/// <summary>The development platform is Xbox 360. This value is no longer in use.</summary>
		// Token: 0x040005E9 RID: 1513
		Xbox,
		/// <summary>The operating system is Macintosh. This value was returned by Silverlight. On .NET Core, its replacement is Unix.</summary>
		// Token: 0x040005EA RID: 1514
		MacOSX
	}
}

using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Dictates which character set marshaled strings should use.</summary>
	// Token: 0x02000941 RID: 2369
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum CharSet
	{
		/// <summary>This value is obsolete and has the same behavior as <see cref="F:System.Runtime.InteropServices.CharSet.Ansi" />.</summary>
		// Token: 0x04002B42 RID: 11074
		None = 1,
		/// <summary>Marshal strings as multiple-byte character strings.</summary>
		// Token: 0x04002B43 RID: 11075
		[__DynamicallyInvokable]
		Ansi,
		/// <summary>Marshal strings as Unicode 2-byte characters.</summary>
		// Token: 0x04002B44 RID: 11076
		[__DynamicallyInvokable]
		Unicode,
		/// <summary>Automatically marshal strings appropriately for the target operating system. The default is <see cref="F:System.Runtime.InteropServices.CharSet.Unicode" /> on Windows NT, Windows 2000, Windows XP, and the Windows Server 2003 family; the default is <see cref="F:System.Runtime.InteropServices.CharSet.Ansi" /> on Windows 98 and Windows Me. Although the common language runtime default is <see cref="F:System.Runtime.InteropServices.CharSet.Auto" />, languages may override this default. For example, by default C# marks all methods and types as <see cref="F:System.Runtime.InteropServices.CharSet.Ansi" />.</summary>
		// Token: 0x04002B45 RID: 11077
		Auto
	}
}

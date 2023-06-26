using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Defines constants for drive types, including CDRom, Fixed, Network, NoRootDirectory, Ram, Removable, and Unknown.</summary>
	// Token: 0x0200017E RID: 382
	[ComVisible(true)]
	[Serializable]
	public enum DriveType
	{
		/// <summary>The type of drive is unknown.</summary>
		// Token: 0x04000824 RID: 2084
		Unknown,
		/// <summary>The drive does not have a root directory.</summary>
		// Token: 0x04000825 RID: 2085
		NoRootDirectory,
		/// <summary>The drive is a removable storage device, such as a floppy disk drive or a USB flash drive.</summary>
		// Token: 0x04000826 RID: 2086
		Removable,
		/// <summary>The drive is a fixed disk.</summary>
		// Token: 0x04000827 RID: 2087
		Fixed,
		/// <summary>The drive is a network drive.</summary>
		// Token: 0x04000828 RID: 2088
		Network,
		/// <summary>The drive is an optical disc device, such as a CD or DVD-ROM.</summary>
		// Token: 0x04000829 RID: 2089
		CDRom,
		/// <summary>The drive is a RAM disk.</summary>
		// Token: 0x0400082A RID: 2090
		Ram
	}
}

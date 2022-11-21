using System;
using System.Runtime.InteropServices;

namespace ComponentAce.Compression.ZipForge
{
	// Token: 0x02000085 RID: 133
	public class Compression1
	{
		// Token: 0x060005E0 RID: 1504
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int SetVolumeLabel([MarshalAs(UnmanagedType.LPTStr)] string lpRootPathName, [MarshalAs(UnmanagedType.LPTStr)] string lpVolumeName);
	}
}

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Microsoft.WindowsDeviceRecoveryTool.FawkesAdaptation
{
	// Token: 0x02000003 RID: 3
	public static class BitmapExtensions
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020E0 File Offset: 0x000002E0
		public static byte[] ToBytes(this Bitmap bitmap)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bitmap.Save(memoryStream, ImageFormat.Png);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				array = memoryStream.ToArray();
			}
			return array;
		}
	}
}

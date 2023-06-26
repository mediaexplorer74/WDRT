using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Helpers
{
	// Token: 0x02000008 RID: 8
	public static class BitmapExtensions
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002350 File Offset: 0x00000550
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

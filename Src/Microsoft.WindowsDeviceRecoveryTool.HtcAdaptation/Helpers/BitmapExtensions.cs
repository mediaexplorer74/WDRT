using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Microsoft.WindowsDeviceRecoveryTool.HtcAdaptation.Helpers
{
	// Token: 0x02000009 RID: 9
	public static class BitmapExtensions
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00003588 File Offset: 0x00001788
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

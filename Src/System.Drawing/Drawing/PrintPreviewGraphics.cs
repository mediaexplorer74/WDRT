using System;
using System.Drawing.Internal;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x0200004E RID: 78
	internal class PrintPreviewGraphics
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001C2AB File Offset: 0x0001A4AB
		public PrintPreviewGraphics(PrintDocument document, PrintPageEventArgs e)
		{
			this.printPageEventArgs = e;
			this.printDocument = document;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		public RectangleF VisibleClipBounds
		{
			get
			{
				IntPtr hdevmodeInternal = this.printPageEventArgs.PageSettings.PrinterSettings.GetHdevmodeInternal();
				RectangleF visibleClipBounds;
				using (DeviceContext deviceContext = this.printPageEventArgs.PageSettings.PrinterSettings.CreateDeviceContext(hdevmodeInternal))
				{
					using (Graphics graphics = Graphics.FromHdcInternal(deviceContext.Hdc))
					{
						if (this.printDocument.OriginAtMargins)
						{
							int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(deviceContext, deviceContext.Hdc), 88);
							int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(deviceContext, deviceContext.Hdc), 90);
							int deviceCaps3 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(deviceContext, deviceContext.Hdc), 112);
							int deviceCaps4 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(deviceContext, deviceContext.Hdc), 113);
							float num = (float)(deviceCaps3 * 100 / deviceCaps);
							float num2 = (float)(deviceCaps4 * 100 / deviceCaps2);
							graphics.TranslateTransform(-num, -num2);
							graphics.TranslateTransform((float)this.printDocument.DefaultPageSettings.Margins.Left, (float)this.printDocument.DefaultPageSettings.Margins.Top);
						}
						visibleClipBounds = graphics.VisibleClipBounds;
					}
				}
				return visibleClipBounds;
			}
		}

		// Token: 0x040005A6 RID: 1446
		private PrintPageEventArgs printPageEventArgs;

		// Token: 0x040005A7 RID: 1447
		private PrintDocument printDocument;
	}
}

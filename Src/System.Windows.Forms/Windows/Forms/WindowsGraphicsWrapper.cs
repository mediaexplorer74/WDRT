using System;
using System.Drawing;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms
{
	// Token: 0x0200044E RID: 1102
	internal sealed class WindowsGraphicsWrapper : IDisposable
	{
		// Token: 0x06004D50 RID: 19792 RVA: 0x0013F7F8 File Offset: 0x0013D9F8
		public WindowsGraphicsWrapper(IDeviceContext idc, TextFormatFlags flags)
		{
			if (idc is Graphics)
			{
				ApplyGraphicsProperties applyGraphicsProperties = ApplyGraphicsProperties.None;
				if ((flags & TextFormatFlags.PreserveGraphicsClipping) != TextFormatFlags.Default)
				{
					applyGraphicsProperties |= ApplyGraphicsProperties.Clipping;
				}
				if ((flags & TextFormatFlags.PreserveGraphicsTranslateTransform) != TextFormatFlags.Default)
				{
					applyGraphicsProperties |= ApplyGraphicsProperties.TranslateTransform;
				}
				if (applyGraphicsProperties != ApplyGraphicsProperties.None)
				{
					this.wg = WindowsGraphics.FromGraphics(idc as Graphics, applyGraphicsProperties);
				}
			}
			else
			{
				this.wg = idc as WindowsGraphics;
				if (this.wg != null)
				{
					this.idc = idc;
				}
			}
			if (this.wg == null)
			{
				this.idc = idc;
				this.wg = WindowsGraphics.FromHdc(idc.GetHdc());
			}
			if ((flags & TextFormatFlags.LeftAndRightPadding) != TextFormatFlags.Default)
			{
				this.wg.TextPadding = TextPaddingOptions.LeftAndRightPadding;
				return;
			}
			if ((flags & TextFormatFlags.NoPadding) != TextFormatFlags.Default)
			{
				this.wg.TextPadding = TextPaddingOptions.NoPadding;
			}
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06004D51 RID: 19793 RVA: 0x0013F8AC File Offset: 0x0013DAAC
		public WindowsGraphics WindowsGraphics
		{
			get
			{
				return this.wg;
			}
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x0013F8B4 File Offset: 0x0013DAB4
		~WindowsGraphicsWrapper()
		{
			this.Dispose(false);
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x0013F8E4 File Offset: 0x0013DAE4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x0013F8F4 File Offset: 0x0013DAF4
		public void Dispose(bool disposing)
		{
			if (this.wg != null)
			{
				if (this.wg != this.idc)
				{
					this.wg.Dispose();
					if (this.idc != null)
					{
						this.idc.ReleaseHdc();
					}
				}
				this.idc = null;
				this.wg = null;
			}
		}

		// Token: 0x040028C8 RID: 10440
		private IDeviceContext idc;

		// Token: 0x040028C9 RID: 10441
		private WindowsGraphics wg;
	}
}

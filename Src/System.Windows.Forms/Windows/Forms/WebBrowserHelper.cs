using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000437 RID: 1079
	internal static class WebBrowserHelper
	{
		// Token: 0x06004B67 RID: 19303 RVA: 0x0013A283 File Offset: 0x00138483
		internal static int Pix2HM(int pix, int logP)
		{
			return (2540 * pix + (logP >> 1)) / logP;
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x0013A292 File Offset: 0x00138492
		internal static int HM2Pix(int hm, int logP)
		{
			return (logP * hm + 1270) / 2540;
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06004B69 RID: 19305 RVA: 0x0013A2A4 File Offset: 0x001384A4
		internal static int LogPixelsX
		{
			get
			{
				if (WebBrowserHelper.logPixelsX == -1)
				{
					IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
					if (dc != IntPtr.Zero)
					{
						WebBrowserHelper.logPixelsX = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 88);
						UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
					}
				}
				return WebBrowserHelper.logPixelsX;
			}
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x0013A2FB File Offset: 0x001384FB
		internal static void ResetLogPixelsX()
		{
			WebBrowserHelper.logPixelsX = -1;
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06004B6B RID: 19307 RVA: 0x0013A304 File Offset: 0x00138504
		internal static int LogPixelsY
		{
			get
			{
				if (WebBrowserHelper.logPixelsY == -1)
				{
					IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
					if (dc != IntPtr.Zero)
					{
						WebBrowserHelper.logPixelsY = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 90);
						UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
					}
				}
				return WebBrowserHelper.logPixelsY;
			}
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x0013A35B File Offset: 0x0013855B
		internal static void ResetLogPixelsY()
		{
			WebBrowserHelper.logPixelsY = -1;
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x0013A364 File Offset: 0x00138564
		internal static ISelectionService GetSelectionService(Control ctl)
		{
			ISite site = ctl.Site;
			if (site != null)
			{
				object service = site.GetService(typeof(ISelectionService));
				if (service is ISelectionService)
				{
					return (ISelectionService)service;
				}
			}
			return null;
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x0013A39C File Offset: 0x0013859C
		internal static NativeMethods.COMRECT GetClipRect()
		{
			return new NativeMethods.COMRECT(new Rectangle(0, 0, 32000, 32000));
		}

		// Token: 0x04002817 RID: 10263
		internal static readonly int sinkAttached = BitVector32.CreateMask();

		// Token: 0x04002818 RID: 10264
		internal static readonly int manualUpdate = BitVector32.CreateMask(WebBrowserHelper.sinkAttached);

		// Token: 0x04002819 RID: 10265
		internal static readonly int setClientSiteFirst = BitVector32.CreateMask(WebBrowserHelper.manualUpdate);

		// Token: 0x0400281A RID: 10266
		internal static readonly int addedSelectionHandler = BitVector32.CreateMask(WebBrowserHelper.setClientSiteFirst);

		// Token: 0x0400281B RID: 10267
		internal static readonly int siteProcessedInputKey = BitVector32.CreateMask(WebBrowserHelper.addedSelectionHandler);

		// Token: 0x0400281C RID: 10268
		internal static readonly int inTransition = BitVector32.CreateMask(WebBrowserHelper.siteProcessedInputKey);

		// Token: 0x0400281D RID: 10269
		internal static readonly int processingKeyUp = BitVector32.CreateMask(WebBrowserHelper.inTransition);

		// Token: 0x0400281E RID: 10270
		internal static readonly int isMaskEdit = BitVector32.CreateMask(WebBrowserHelper.processingKeyUp);

		// Token: 0x0400281F RID: 10271
		internal static readonly int recomputeContainingControl = BitVector32.CreateMask(WebBrowserHelper.isMaskEdit);

		// Token: 0x04002820 RID: 10272
		private static int logPixelsX = -1;

		// Token: 0x04002821 RID: 10273
		private static int logPixelsY = -1;

		// Token: 0x04002822 RID: 10274
		private const int HMperInch = 2540;

		// Token: 0x04002823 RID: 10275
		private static Guid ifont_Guid = typeof(UnsafeNativeMethods.IFont).GUID;

		// Token: 0x04002824 RID: 10276
		internal static Guid windowsMediaPlayer_Clsid = new Guid("{22d6f312-b0f6-11d0-94ab-0080c74c7e95}");

		// Token: 0x04002825 RID: 10277
		internal static Guid comctlImageCombo_Clsid = new Guid("{a98a24c0-b06f-3684-8c12-c52ae341e0bc}");

		// Token: 0x04002826 RID: 10278
		internal static Guid maskEdit_Clsid = new Guid("{c932ba85-4374-101b-a56c-00aa003668dc}");

		// Token: 0x04002827 RID: 10279
		internal static readonly int REGMSG_MSG = SafeNativeMethods.RegisterWindowMessage(Application.WindowMessagesVersion + "_subclassCheck");

		// Token: 0x04002828 RID: 10280
		internal const int REGMSG_RETVAL = 123;

		// Token: 0x02000828 RID: 2088
		internal enum AXState
		{
			// Token: 0x0400433E RID: 17214
			Passive,
			// Token: 0x0400433F RID: 17215
			Loaded,
			// Token: 0x04004340 RID: 17216
			Running,
			// Token: 0x04004341 RID: 17217
			InPlaceActive = 4,
			// Token: 0x04004342 RID: 17218
			UIActive = 8
		}

		// Token: 0x02000829 RID: 2089
		internal enum AXEditMode
		{
			// Token: 0x04004344 RID: 17220
			None,
			// Token: 0x04004345 RID: 17221
			Object,
			// Token: 0x04004346 RID: 17222
			Host
		}

		// Token: 0x0200082A RID: 2090
		internal enum SelectionStyle
		{
			// Token: 0x04004348 RID: 17224
			NotSelected,
			// Token: 0x04004349 RID: 17225
			Selected,
			// Token: 0x0400434A RID: 17226
			Active
		}
	}
}

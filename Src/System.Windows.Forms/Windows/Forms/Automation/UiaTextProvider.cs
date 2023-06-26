using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Automation
{
	// Token: 0x020004F1 RID: 1265
	internal abstract class UiaTextProvider : UnsafeNativeMethods.UiaCore.ITextProvider
	{
		// Token: 0x06005240 RID: 21056
		public abstract UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetSelection();

		// Token: 0x06005241 RID: 21057
		public abstract UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetVisibleRanges();

		// Token: 0x06005242 RID: 21058
		public abstract UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement);

		// Token: 0x06005243 RID: 21059
		public abstract UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromPoint(Point screenLocation);

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06005244 RID: 21060
		public abstract UnsafeNativeMethods.UiaCore.ITextRangeProvider DocumentRange { get; }

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06005245 RID: 21061
		public abstract UnsafeNativeMethods.UiaCore.SupportedTextSelection SupportedTextSelection { get; }

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06005246 RID: 21062
		public abstract Rectangle BoundingRectangle { get; }

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06005247 RID: 21063
		public abstract int EditStyle { get; }

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06005248 RID: 21064
		public abstract int FirstVisibleLine { get; }

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06005249 RID: 21065
		public abstract bool IsMultiline { get; }

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x0600524A RID: 21066
		public abstract bool IsReadingRTL { get; }

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x0600524B RID: 21067
		public abstract bool IsReadOnly { get; }

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x0600524C RID: 21068
		public abstract bool IsScrollable { get; }

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x0600524D RID: 21069
		public abstract int LinesPerPage { get; }

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x0600524E RID: 21070
		public abstract int LinesCount { get; }

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x0600524F RID: 21071
		public abstract NativeMethods.LOGFONT Logfont { get; }

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06005250 RID: 21072
		public abstract string Text { get; }

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06005251 RID: 21073
		public abstract int TextLength { get; }

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06005252 RID: 21074
		public abstract int WindowExStyle { get; }

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06005253 RID: 21075
		public abstract int WindowStyle { get; }

		// Token: 0x06005254 RID: 21076
		public abstract int GetLineFromCharIndex(int charIndex);

		// Token: 0x06005255 RID: 21077
		public abstract int GetLineIndex(int line);

		// Token: 0x06005256 RID: 21078
		public abstract Point GetPositionFromChar(int charIndex);

		// Token: 0x06005257 RID: 21079
		public abstract Point GetPositionFromCharForUpperRightCorner(int startCharIndex, string text);

		// Token: 0x06005258 RID: 21080
		public abstract void GetVisibleRangePoints(out int visibleStart, out int visibleEnd);

		// Token: 0x06005259 RID: 21081
		public abstract bool LineScroll(int charactersHorizontal, int linesVertical);

		// Token: 0x0600525A RID: 21082
		public abstract Point PointToScreen(Point pt);

		// Token: 0x0600525B RID: 21083
		public abstract void SetSelection(int start, int end);

		// Token: 0x0600525C RID: 21084 RVA: 0x001555D9 File Offset: 0x001537D9
		public int GetEditStyle(HandleRef hWnd)
		{
			return (int)(long)UnsafeNativeMethods.GetWindowLong(hWnd, -16);
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x001555E9 File Offset: 0x001537E9
		public int GetWindowExStyle(HandleRef hWnd)
		{
			return (int)(long)UnsafeNativeMethods.GetWindowLong(hWnd, -20);
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x001555D9 File Offset: 0x001537D9
		public int GetWindowStyle(HandleRef hWnd)
		{
			return (int)(long)UnsafeNativeMethods.GetWindowLong(hWnd, -16);
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x001555FC File Offset: 0x001537FC
		public double[] RectListToDoubleArray(List<Rectangle> rectArray)
		{
			if (rectArray == null || rectArray.Count == 0)
			{
				return new double[0];
			}
			double[] array = new double[rectArray.Count * 4];
			int num = 0;
			for (int i = 0; i < rectArray.Count; i++)
			{
				array[num++] = (double)rectArray[i].X;
				array[num++] = (double)rectArray[i].Y;
				array[num++] = (double)rectArray[i].Width;
				array[num++] = (double)rectArray[i].Height;
			}
			return array;
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x00155699 File Offset: 0x00153899
		public int SendInput(int inputs, ref NativeMethods.INPUT input, int size)
		{
			return (int)UnsafeNativeMethods.SendInput((uint)inputs, new NativeMethods.INPUT[] { input }, size);
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x001556B8 File Offset: 0x001538B8
		public unsafe int SendKeyboardInputVK(short vk, bool press)
		{
			NativeMethods.INPUT input = default(NativeMethods.INPUT);
			input.type = 1;
			input.inputUnion.ki.wVk = vk;
			input.inputUnion.ki.wScan = 0;
			input.inputUnion.ki.dwFlags = (press ? 0 : 2);
			if (UiaTextProvider.IsExtendedKey(vk))
			{
				input.inputUnion.ki.dwFlags = input.inputUnion.ki.dwFlags | 1;
			}
			input.inputUnion.ki.time = 0;
			input.inputUnion.ki.dwExtraInfo = IntPtr.Zero;
			return this.SendInput(1, ref input, sizeof(NativeMethods.INPUT));
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x00155768 File Offset: 0x00153968
		private static bool IsExtendedKey(short vk)
		{
			return vk == 165 || vk == 163 || vk == 144 || vk == 45 || vk == 46 || vk == 36 || vk == 35 || vk == 33 || vk == 34 || vk == 38 || vk == 40 || vk == 37 || vk == 39 || vk == 93 || vk == 92 || vk == 91;
		}

		// Token: 0x0400363E RID: 13886
		public const int EndOfLineWidth = 2;
	}
}

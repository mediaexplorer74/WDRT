using System;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E4 RID: 1252
	internal sealed class WindowsFont : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x060051BF RID: 20927 RVA: 0x00153968 File Offset: 0x00151B68
		private void CreateFont()
		{
			this.hFont = IntUnsafeNativeMethods.CreateFontIndirect(this.logFont);
			if (this.hFont == IntPtr.Zero)
			{
				this.logFont.lfFaceName = "Microsoft Sans Serif";
				this.logFont.lfOutPrecision = 7;
				this.hFont = IntUnsafeNativeMethods.CreateFontIndirect(this.logFont);
			}
			IntUnsafeNativeMethods.GetObject(new HandleRef(this, this.hFont), this.logFont);
			this.ownHandle = true;
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x001539E4 File Offset: 0x00151BE4
		public WindowsFont(string faceName)
			: this(faceName, 8.25f, FontStyle.Regular, 1, WindowsFontQuality.Default)
		{
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x001539F5 File Offset: 0x00151BF5
		public WindowsFont(string faceName, float size)
			: this(faceName, size, FontStyle.Regular, 1, WindowsFontQuality.Default)
		{
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x00153A02 File Offset: 0x00151C02
		public WindowsFont(string faceName, float size, FontStyle style)
			: this(faceName, size, style, 1, WindowsFontQuality.Default)
		{
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x00153A10 File Offset: 0x00151C10
		public WindowsFont(string faceName, float size, FontStyle style, byte charSet, WindowsFontQuality fontQuality)
		{
			this.fontSize = -1f;
			base..ctor();
			this.logFont = new IntNativeMethods.LOGFONT();
			int num = (int)Math.Ceiling((double)((float)WindowsGraphicsCacheManager.MeasurementGraphics.DeviceContext.DpiY * size / 72f));
			this.logFont.lfHeight = -num;
			this.logFont.lfFaceName = ((faceName != null) ? faceName : "Microsoft Sans Serif");
			this.logFont.lfCharSet = charSet;
			this.logFont.lfOutPrecision = 4;
			this.logFont.lfQuality = (byte)fontQuality;
			this.logFont.lfWeight = (((style & FontStyle.Bold) == FontStyle.Bold) ? 700 : 400);
			this.logFont.lfItalic = (((style & FontStyle.Italic) == FontStyle.Italic) ? 1 : 0);
			this.logFont.lfUnderline = (((style & FontStyle.Underline) == FontStyle.Underline) ? 1 : 0);
			this.logFont.lfStrikeOut = (((style & FontStyle.Strikeout) == FontStyle.Strikeout) ? 1 : 0);
			this.style = style;
			this.CreateFont();
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x00153B0C File Offset: 0x00151D0C
		private WindowsFont(IntNativeMethods.LOGFONT lf, bool createHandle)
		{
			this.fontSize = -1f;
			base..ctor();
			this.logFont = lf;
			if (this.logFont.lfFaceName == null)
			{
				this.logFont.lfFaceName = "Microsoft Sans Serif";
			}
			this.style = FontStyle.Regular;
			if (lf.lfWeight == 700)
			{
				this.style |= FontStyle.Bold;
			}
			if (lf.lfItalic == 1)
			{
				this.style |= FontStyle.Italic;
			}
			if (lf.lfUnderline == 1)
			{
				this.style |= FontStyle.Underline;
			}
			if (lf.lfStrikeOut == 1)
			{
				this.style |= FontStyle.Strikeout;
			}
			if (createHandle)
			{
				this.CreateFont();
			}
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x00153BBE File Offset: 0x00151DBE
		public static WindowsFont FromFont(Font font)
		{
			return WindowsFont.FromFont(font, WindowsFontQuality.Default);
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x00153BC8 File Offset: 0x00151DC8
		public static WindowsFont FromFont(Font font, WindowsFontQuality fontQuality)
		{
			string text = font.FontFamily.Name;
			if (text != null && text.Length > 1 && text[0] == '@')
			{
				text = text.Substring(1);
			}
			return new WindowsFont(text, font.SizeInPoints, font.Style, font.GdiCharSet, fontQuality);
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x00153C1C File Offset: 0x00151E1C
		public static WindowsFont FromHdc(IntPtr hdc)
		{
			IntPtr currentObject = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(null, hdc), 6);
			return WindowsFont.FromHfont(currentObject);
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x00153C3D File Offset: 0x00151E3D
		public static WindowsFont FromHfont(IntPtr hFont)
		{
			return WindowsFont.FromHfont(hFont, false);
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x00153C48 File Offset: 0x00151E48
		public static WindowsFont FromHfont(IntPtr hFont, bool takeOwnership)
		{
			IntNativeMethods.LOGFONT logfont = new IntNativeMethods.LOGFONT();
			IntUnsafeNativeMethods.GetObject(new HandleRef(null, hFont), logfont);
			return new WindowsFont(logfont, false)
			{
				hFont = hFont,
				ownHandle = takeOwnership
			};
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x00153C80 File Offset: 0x00151E80
		~WindowsFont()
		{
			this.Dispose(false);
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x00153CB0 File Offset: 0x00151EB0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x00153CBC File Offset: 0x00151EBC
		internal void Dispose(bool disposing)
		{
			bool flag = false;
			if (this.ownHandle && (!this.ownedByCacheManager || !disposing) && (this.everOwnedByCacheManager || !disposing || !DeviceContexts.IsFontInUse(this)))
			{
				IntUnsafeNativeMethods.DeleteObject(new HandleRef(this, this.hFont));
				this.hFont = IntPtr.Zero;
				this.ownHandle = false;
				flag = true;
			}
			if (disposing && (flag || !this.ownHandle))
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x00153D2C File Offset: 0x00151F2C
		public override bool Equals(object font)
		{
			WindowsFont windowsFont = font as WindowsFont;
			return windowsFont != null && (windowsFont == this || (this.Name == windowsFont.Name && this.LogFontHeight == windowsFont.LogFontHeight && this.Style == windowsFont.Style && this.CharSet == windowsFont.CharSet && this.Quality == windowsFont.Quality));
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x00153D98 File Offset: 0x00151F98
		public override int GetHashCode()
		{
			return (int)((((int)this.Style << 13) | (this.Style >> 19)) ^ (FontStyle)(((int)this.CharSet << 26) | (int)((uint)this.CharSet >> 6)) ^ (FontStyle)(((uint)this.Size << 7) | ((uint)this.Size >> 25)));
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x00153DD5 File Offset: 0x00151FD5
		public object Clone()
		{
			return new WindowsFont(this.logFont, true);
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x00153DE4 File Offset: 0x00151FE4
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "[{0}: Name={1}, Size={2} points, Height={3} pixels, Sytle={4}]", new object[]
			{
				base.GetType().Name,
				this.logFont.lfFaceName,
				this.Size,
				this.Height,
				this.Style
			});
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x060051D1 RID: 20945 RVA: 0x00153E4C File Offset: 0x0015204C
		public IntPtr Hfont
		{
			get
			{
				return this.hFont;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x060051D2 RID: 20946 RVA: 0x00153E54 File Offset: 0x00152054
		public bool Italic
		{
			get
			{
				return this.logFont.lfItalic == 1;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x060051D3 RID: 20947 RVA: 0x00153E64 File Offset: 0x00152064
		// (set) Token: 0x060051D4 RID: 20948 RVA: 0x00153E6C File Offset: 0x0015206C
		public bool OwnedByCacheManager
		{
			get
			{
				return this.ownedByCacheManager;
			}
			set
			{
				if (value)
				{
					this.everOwnedByCacheManager = true;
				}
				this.ownedByCacheManager = value;
			}
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x060051D5 RID: 20949 RVA: 0x00153E7F File Offset: 0x0015207F
		public WindowsFontQuality Quality
		{
			get
			{
				return (WindowsFontQuality)this.logFont.lfQuality;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x060051D6 RID: 20950 RVA: 0x00153E8C File Offset: 0x0015208C
		public FontStyle Style
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x060051D7 RID: 20951 RVA: 0x00153E94 File Offset: 0x00152094
		public int Height
		{
			get
			{
				if (this.lineSpacing == 0)
				{
					WindowsGraphics measurementGraphics = WindowsGraphicsCacheManager.MeasurementGraphics;
					measurementGraphics.DeviceContext.SelectFont(this);
					IntNativeMethods.TEXTMETRIC textMetrics = measurementGraphics.GetTextMetrics();
					this.lineSpacing = textMetrics.tmHeight;
				}
				return this.lineSpacing;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x060051D8 RID: 20952 RVA: 0x00153ED5 File Offset: 0x001520D5
		public byte CharSet
		{
			get
			{
				return this.logFont.lfCharSet;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x060051D9 RID: 20953 RVA: 0x00153EE2 File Offset: 0x001520E2
		public int LogFontHeight
		{
			get
			{
				return this.logFont.lfHeight;
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x060051DA RID: 20954 RVA: 0x00153EEF File Offset: 0x001520EF
		public string Name
		{
			get
			{
				return this.logFont.lfFaceName;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x060051DB RID: 20955 RVA: 0x00153EFC File Offset: 0x001520FC
		public float Size
		{
			get
			{
				if (this.fontSize < 0f)
				{
					WindowsGraphics measurementGraphics = WindowsGraphicsCacheManager.MeasurementGraphics;
					measurementGraphics.DeviceContext.SelectFont(this);
					IntNativeMethods.TEXTMETRIC textMetrics = measurementGraphics.GetTextMetrics();
					int num = ((this.logFont.lfHeight > 0) ? textMetrics.tmHeight : (textMetrics.tmHeight - textMetrics.tmInternalLeading));
					this.fontSize = (float)num * 72f / (float)measurementGraphics.DeviceContext.DpiY;
				}
				return this.fontSize;
			}
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x00153F78 File Offset: 0x00152178
		public static WindowsFontQuality WindowsFontQualityFromTextRenderingHint(Graphics g)
		{
			if (g == null)
			{
				return WindowsFontQuality.Default;
			}
			switch (g.TextRenderingHint)
			{
			case TextRenderingHint.SingleBitPerPixelGridFit:
				return WindowsFontQuality.Proof;
			case TextRenderingHint.SingleBitPerPixel:
				return WindowsFontQuality.Draft;
			case TextRenderingHint.AntiAliasGridFit:
				return WindowsFontQuality.AntiAliased;
			case TextRenderingHint.AntiAlias:
				return WindowsFontQuality.AntiAliased;
			case TextRenderingHint.ClearTypeGridFit:
				if (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1)
				{
					return WindowsFontQuality.ClearTypeNatural;
				}
				return WindowsFontQuality.ClearType;
			}
			return WindowsFontQuality.Default;
		}

		// Token: 0x040035CF RID: 13775
		private const int LogFontNameOffset = 28;

		// Token: 0x040035D0 RID: 13776
		private IntPtr hFont;

		// Token: 0x040035D1 RID: 13777
		private float fontSize;

		// Token: 0x040035D2 RID: 13778
		private int lineSpacing;

		// Token: 0x040035D3 RID: 13779
		private bool ownHandle;

		// Token: 0x040035D4 RID: 13780
		private bool ownedByCacheManager;

		// Token: 0x040035D5 RID: 13781
		private bool everOwnedByCacheManager;

		// Token: 0x040035D6 RID: 13782
		private IntNativeMethods.LOGFONT logFont;

		// Token: 0x040035D7 RID: 13783
		private FontStyle style;

		// Token: 0x040035D8 RID: 13784
		private const string defaultFaceName = "Microsoft Sans Serif";

		// Token: 0x040035D9 RID: 13785
		private const float defaultFontSize = 8.25f;

		// Token: 0x040035DA RID: 13786
		private const int defaultFontHeight = 13;
	}
}

using System;
using System.Drawing.Text;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Drawing
{
	/// <summary>Defines a group of type faces having a similar basic design and certain variations in styles. This class cannot be inherited.</summary>
	// Token: 0x0200003C RID: 60
	public sealed class FontFamily : MarshalByRefObject, IDisposable
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x0001A19C File Offset: 0x0001839C
		private void SetNativeFamily(IntPtr family)
		{
			this.nativeFamily = family;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001A1A5 File Offset: 0x000183A5
		internal FontFamily(IntPtr family)
		{
			this.SetNativeFamily(family);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001A1B4 File Offset: 0x000183B4
		internal FontFamily(string name, bool createDefaultOnFail)
		{
			this.createDefaultOnFail = createDefaultOnFail;
			this.CreateFontFamily(name, null);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> with the specified name.</summary>
		/// <param name="name">The name of the new <see cref="T:System.Drawing.FontFamily" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="name" /> specifies a font that is not installed on the computer running the application.  
		/// -or-  
		/// <paramref name="name" /> specifies a font that is not a TrueType font.</exception>
		// Token: 0x060005F3 RID: 1523 RVA: 0x0001A1CB File Offset: 0x000183CB
		public FontFamily(string name)
		{
			this.CreateFontFamily(name, null);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> in the specified <see cref="T:System.Drawing.Text.FontCollection" /> with the specified name.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that represents the name of the new <see cref="T:System.Drawing.FontFamily" />.</param>
		/// <param name="fontCollection">The <see cref="T:System.Drawing.Text.FontCollection" /> that contains this <see cref="T:System.Drawing.FontFamily" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="name" /> specifies a font that is not installed on the computer running the application.  
		/// -or-  
		/// <paramref name="name" /> specifies a font that is not a TrueType font.</exception>
		// Token: 0x060005F4 RID: 1524 RVA: 0x0001A1DB File Offset: 0x000183DB
		public FontFamily(string name, FontCollection fontCollection)
		{
			this.CreateFontFamily(name, fontCollection);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001A1EC File Offset: 0x000183EC
		private void CreateFontFamily(string name, FontCollection fontCollection)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = ((fontCollection == null) ? IntPtr.Zero : fontCollection.nativeFontCollection);
			int num = SafeNativeMethods.Gdip.GdipCreateFontFamilyFromName(name, new HandleRef(fontCollection, intPtr2), out intPtr);
			if (num != 0)
			{
				if (this.createDefaultOnFail)
				{
					intPtr = FontFamily.GetGdipGenericSansSerif();
				}
				else
				{
					if (num == 14)
					{
						throw new ArgumentException(SR.GetString("GdiplusFontFamilyNotFound", new object[] { name }));
					}
					if (num == 16)
					{
						throw new ArgumentException(SR.GetString("GdiplusNotTrueTypeFont", new object[] { name }));
					}
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			this.SetNativeFamily(intPtr);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> from the specified generic font family.</summary>
		/// <param name="genericFamily">The <see cref="T:System.Drawing.Text.GenericFontFamilies" /> from which to create the new <see cref="T:System.Drawing.FontFamily" />.</param>
		// Token: 0x060005F6 RID: 1526 RVA: 0x0001A280 File Offset: 0x00018480
		public FontFamily(GenericFontFamilies genericFamily)
		{
			IntPtr zero = IntPtr.Zero;
			int num;
			switch (genericFamily)
			{
			case GenericFontFamilies.Serif:
				num = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySerif(out zero);
				goto IL_3C;
			case GenericFontFamilies.SansSerif:
				num = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySansSerif(out zero);
				goto IL_3C;
			}
			num = SafeNativeMethods.Gdip.GdipGetGenericFontFamilyMonospace(out zero);
			IL_3C:
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeFamily(zero);
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x060005F7 RID: 1527 RVA: 0x0001A2DC File Offset: 0x000184DC
		~FontFamily()
		{
			this.Dispose(false);
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001A30C File Offset: 0x0001850C
		internal IntPtr NativeFamily
		{
			get
			{
				return this.nativeFamily;
			}
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.FontFamily" /> and is identical to this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <param name="obj">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.FontFamily" /> and is identical to this <see cref="T:System.Drawing.FontFamily" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060005F9 RID: 1529 RVA: 0x0001A314 File Offset: 0x00018514
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			FontFamily fontFamily = obj as FontFamily;
			return fontFamily != null && fontFamily.NativeFamily == this.NativeFamily;
		}

		/// <summary>Converts this <see cref="T:System.Drawing.FontFamily" /> to a human-readable string representation.</summary>
		/// <returns>The string that represents this <see cref="T:System.Drawing.FontFamily" />.</returns>
		// Token: 0x060005FA RID: 1530 RVA: 0x0001A344 File Offset: 0x00018544
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "[{0}: Name={1}]", new object[]
			{
				base.GetType().Name,
				this.Name
			});
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.FontFamily" />.</returns>
		// Token: 0x060005FB RID: 1531 RVA: 0x0001A372 File Offset: 0x00018572
		public override int GetHashCode()
		{
			return this.GetName(0).GetHashCode();
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001A380 File Offset: 0x00018580
		private static int CurrentLanguage
		{
			get
			{
				return CultureInfo.CurrentUICulture.LCID;
			}
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.FontFamily" />.</summary>
		// Token: 0x060005FD RID: 1533 RVA: 0x0001A38C File Offset: 0x0001858C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001A39C File Offset: 0x0001859C
		private void Dispose(bool disposing)
		{
			if (this.nativeFamily != IntPtr.Zero)
			{
				try
				{
					SafeNativeMethods.Gdip.GdipDeleteFontFamily(new HandleRef(this, this.nativeFamily));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.nativeFamily = IntPtr.Zero;
				}
			}
		}

		/// <summary>Gets the name of this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the name of this <see cref="T:System.Drawing.FontFamily" />.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001A404 File Offset: 0x00018604
		public string Name
		{
			get
			{
				return this.GetName(FontFamily.CurrentLanguage);
			}
		}

		/// <summary>Returns the name, in the specified language, of this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <param name="language">The language in which the name is returned.</param>
		/// <returns>A <see cref="T:System.String" /> that represents the name, in the specified language, of this <see cref="T:System.Drawing.FontFamily" />.</returns>
		// Token: 0x06000600 RID: 1536 RVA: 0x0001A414 File Offset: 0x00018614
		public string GetName(int language)
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			int num = SafeNativeMethods.Gdip.GdipGetFamilyName(new HandleRef(this, this.NativeFamily), stringBuilder, language);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Returns an array that contains all the <see cref="T:System.Drawing.FontFamily" /> objects associated with the current graphics context.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects associated with the current graphics context.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001A44D File Offset: 0x0001864D
		public static FontFamily[] Families
		{
			get
			{
				return new InstalledFontCollection().Families;
			}
		}

		/// <summary>Gets a generic sans serif <see cref="T:System.Drawing.FontFamily" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontFamily" /> object that represents a generic sans serif font.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001A459 File Offset: 0x00018659
		public static FontFamily GenericSansSerif
		{
			get
			{
				return new FontFamily(FontFamily.GetGdipGenericSansSerif());
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001A468 File Offset: 0x00018668
		private static IntPtr GetGdipGenericSansSerif()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySansSerif(out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return zero;
		}

		/// <summary>Gets a generic serif <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontFamily" /> that represents a generic serif font.</returns>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001A48E File Offset: 0x0001868E
		public static FontFamily GenericSerif
		{
			get
			{
				return new FontFamily(FontFamily.GetNativeGenericSerif());
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001A49C File Offset: 0x0001869C
		private static IntPtr GetNativeGenericSerif()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySerif(out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return zero;
		}

		/// <summary>Gets a generic monospace <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontFamily" /> that represents a generic monospace font.</returns>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001A4C2 File Offset: 0x000186C2
		public static FontFamily GenericMonospace
		{
			get
			{
				return new FontFamily(FontFamily.GetNativeGenericMonospace());
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001A4D0 File Offset: 0x000186D0
		private static IntPtr GetNativeGenericMonospace()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipGetGenericFontFamilyMonospace(out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return zero;
		}

		/// <summary>Returns an array that contains all the <see cref="T:System.Drawing.FontFamily" /> objects available for the specified graphics context.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object from which to return <see cref="T:System.Drawing.FontFamily" /> objects.</param>
		/// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects available for the specified <see cref="T:System.Drawing.Graphics" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is <see langword="null" />.</exception>
		// Token: 0x06000608 RID: 1544 RVA: 0x0001A4F6 File Offset: 0x000186F6
		[Obsolete("Do not use method GetFamilies, use property Families instead")]
		public static FontFamily[] GetFamilies(Graphics graphics)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			return new InstalledFontCollection().Families;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Drawing.FontStyle" /> enumeration is available.</summary>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Drawing.FontStyle" /> is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000609 RID: 1545 RVA: 0x0001A510 File Offset: 0x00018710
		public bool IsStyleAvailable(FontStyle style)
		{
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsStyleAvailable(new HandleRef(this, this.NativeFamily), style, out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		/// <summary>Gets the height, in font design units, of the em square for the specified style.</summary>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> for which to get the em height.</param>
		/// <returns>The height of the em square.</returns>
		// Token: 0x0600060A RID: 1546 RVA: 0x0001A540 File Offset: 0x00018740
		public int GetEmHeight(FontStyle style)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetEmHeight(new HandleRef(this, this.NativeFamily), style, out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return num;
		}

		/// <summary>Returns the cell ascent, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style.</summary>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> that contains style information for the font.</param>
		/// <returns>The cell ascent for this <see cref="T:System.Drawing.FontFamily" /> that uses the specified <see cref="T:System.Drawing.FontStyle" />.</returns>
		// Token: 0x0600060B RID: 1547 RVA: 0x0001A570 File Offset: 0x00018770
		public int GetCellAscent(FontStyle style)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetCellAscent(new HandleRef(this, this.NativeFamily), style, out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return num;
		}

		/// <summary>Returns the cell descent, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style.</summary>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> that contains style information for the font.</param>
		/// <returns>The cell descent metric for this <see cref="T:System.Drawing.FontFamily" /> that uses the specified <see cref="T:System.Drawing.FontStyle" />.</returns>
		// Token: 0x0600060C RID: 1548 RVA: 0x0001A5A0 File Offset: 0x000187A0
		public int GetCellDescent(FontStyle style)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetCellDescent(new HandleRef(this, this.NativeFamily), style, out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return num;
		}

		/// <summary>Returns the line spacing, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style. The line spacing is the vertical distance between the base lines of two consecutive lines of text.</summary>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> to apply.</param>
		/// <returns>The distance between two consecutive lines of text.</returns>
		// Token: 0x0600060D RID: 1549 RVA: 0x0001A5D0 File Offset: 0x000187D0
		public int GetLineSpacing(FontStyle style)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetLineSpacing(new HandleRef(this, this.NativeFamily), style, out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return num;
		}

		// Token: 0x04000343 RID: 835
		private const int LANG_NEUTRAL = 0;

		// Token: 0x04000344 RID: 836
		private IntPtr nativeFamily;

		// Token: 0x04000345 RID: 837
		private bool createDefaultOnFail;
	}
}

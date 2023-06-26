using System;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Contains information about how bitmap and metafile colors are manipulated during rendering.</summary>
	// Token: 0x0200009E RID: 158
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ImageAttributes : ICloneable, IDisposable
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x00023CB8 File Offset: 0x00021EB8
		internal void SetNativeImageAttributes(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			this.nativeImageAttributes = handle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class.</summary>
		// Token: 0x06000956 RID: 2390 RVA: 0x00023CDC File Offset: 0x00021EDC
		public ImageAttributes()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateImageAttributes(out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeImageAttributes(zero);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00023D0E File Offset: 0x00021F0E
		internal ImageAttributes(IntPtr newNativeImageAttributes)
		{
			this.SetNativeImageAttributes(newNativeImageAttributes);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
		// Token: 0x06000958 RID: 2392 RVA: 0x00023D1D File Offset: 0x00021F1D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00023D2C File Offset: 0x00021F2C
		private void Dispose(bool disposing)
		{
			if (this.nativeImageAttributes != IntPtr.Zero)
			{
				try
				{
					SafeNativeMethods.Gdip.GdipDisposeImageAttributes(new HandleRef(this, this.nativeImageAttributes));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.nativeImageAttributes = IntPtr.Zero;
				}
			}
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x0600095A RID: 2394 RVA: 0x00023D94 File Offset: 0x00021F94
		~ImageAttributes()
		{
			this.Dispose(false);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object this class creates, cast as an object.</returns>
		// Token: 0x0600095B RID: 2395 RVA: 0x00023DC4 File Offset: 0x00021FC4
		public object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneImageAttributes(new HandleRef(this, this.nativeImageAttributes), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new ImageAttributes(zero);
		}

		/// <summary>Sets the color-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		// Token: 0x0600095C RID: 2396 RVA: 0x00023DFB File Offset: 0x00021FFB
		public void SetColorMatrix(ColorMatrix newColorMatrix)
		{
			this.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment matrix.</param>
		// Token: 0x0600095D RID: 2397 RVA: 0x00023E06 File Offset: 0x00022006
		public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag flags)
		{
			this.SetColorMatrix(newColorMatrix, flags, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix for a specified category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment matrix.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment matrix is set.</param>
		// Token: 0x0600095E RID: 2398 RVA: 0x00023E14 File Offset: 0x00022014
		public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag mode, ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesColorMatrix(new HandleRef(this, this.nativeImageAttributes), type, true, newColorMatrix, null, mode);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the color-adjustment matrix for the default category.</summary>
		// Token: 0x0600095F RID: 2399 RVA: 0x00023E42 File Offset: 0x00022042
		public void ClearColorMatrix()
		{
			this.ClearColorMatrix(ColorAdjustType.Default);
		}

		/// <summary>Clears the color-adjustment matrix for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment matrix is cleared.</param>
		// Token: 0x06000960 RID: 2400 RVA: 0x00023E4C File Offset: 0x0002204C
		public void ClearColorMatrix(ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesColorMatrix(new HandleRef(this, this.nativeImageAttributes), type, false, null, null, ColorMatrixFlag.Default);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		/// <param name="grayMatrix">The grayscale-adjustment matrix.</param>
		// Token: 0x06000961 RID: 2401 RVA: 0x00023E7A File Offset: 0x0002207A
		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix)
		{
			this.SetColorMatrices(newColorMatrix, grayMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		/// <param name="grayMatrix">The grayscale-adjustment matrix.</param>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment and grayscale-adjustment matrices.</param>
		// Token: 0x06000962 RID: 2402 RVA: 0x00023E86 File Offset: 0x00022086
		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag flags)
		{
			this.SetColorMatrices(newColorMatrix, grayMatrix, flags, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for a specified category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		/// <param name="grayMatrix">The grayscale-adjustment matrix.</param>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment and grayscale-adjustment matrices.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment and grayscale-adjustment matrices are set.</param>
		// Token: 0x06000963 RID: 2403 RVA: 0x00023E94 File Offset: 0x00022094
		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag mode, ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesColorMatrix(new HandleRef(this, this.nativeImageAttributes), type, true, newColorMatrix, grayMatrix, mode);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the threshold (transparency range) for the default category.</summary>
		/// <param name="threshold">A real number that specifies the threshold value.</param>
		// Token: 0x06000964 RID: 2404 RVA: 0x00023EC3 File Offset: 0x000220C3
		public void SetThreshold(float threshold)
		{
			this.SetThreshold(threshold, ColorAdjustType.Default);
		}

		/// <summary>Sets the threshold (transparency range) for a specified category.</summary>
		/// <param name="threshold">A threshold value from 0.0 to 1.0 that is used as a breakpoint to sort colors that will be mapped to either a maximum or a minimum value.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color threshold is set.</param>
		// Token: 0x06000965 RID: 2405 RVA: 0x00023ED0 File Offset: 0x000220D0
		public void SetThreshold(float threshold, ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesThreshold(new HandleRef(this, this.nativeImageAttributes), type, true, threshold);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the threshold value for the default category.</summary>
		// Token: 0x06000966 RID: 2406 RVA: 0x00023EFC File Offset: 0x000220FC
		public void ClearThreshold()
		{
			this.ClearThreshold(ColorAdjustType.Default);
		}

		/// <summary>Clears the threshold value for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the threshold is cleared.</param>
		// Token: 0x06000967 RID: 2407 RVA: 0x00023F08 File Offset: 0x00022108
		public void ClearThreshold(ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesThreshold(new HandleRef(this, this.nativeImageAttributes), type, false, 0f);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the gamma value for the default category.</summary>
		/// <param name="gamma">The gamma correction value.</param>
		// Token: 0x06000968 RID: 2408 RVA: 0x00023F38 File Offset: 0x00022138
		public void SetGamma(float gamma)
		{
			this.SetGamma(gamma, ColorAdjustType.Default);
		}

		/// <summary>Sets the gamma value for a specified category.</summary>
		/// <param name="gamma">The gamma correction value.</param>
		/// <param name="type">An element of the <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> enumeration that specifies the category for which the gamma value is set.</param>
		// Token: 0x06000969 RID: 2409 RVA: 0x00023F44 File Offset: 0x00022144
		public void SetGamma(float gamma, ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesGamma(new HandleRef(this, this.nativeImageAttributes), type, true, gamma);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Disables gamma correction for the default category.</summary>
		// Token: 0x0600096A RID: 2410 RVA: 0x00023F70 File Offset: 0x00022170
		public void ClearGamma()
		{
			this.ClearGamma(ColorAdjustType.Default);
		}

		/// <summary>Disables gamma correction for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which gamma correction is disabled.</param>
		// Token: 0x0600096B RID: 2411 RVA: 0x00023F7C File Offset: 0x0002217C
		public void ClearGamma(ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesGamma(new HandleRef(this, this.nativeImageAttributes), type, false, 0f);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Turns off color adjustment for the default category. You can call the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.ClearNoOp" /> method to reinstate the color-adjustment settings that were in place before the call to the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetNoOp" /> method.</summary>
		// Token: 0x0600096C RID: 2412 RVA: 0x00023FAC File Offset: 0x000221AC
		public void SetNoOp()
		{
			this.SetNoOp(ColorAdjustType.Default);
		}

		/// <summary>Turns off color adjustment for a specified category. You can call the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.ClearNoOp" /> method to reinstate the color-adjustment settings that were in place before the call to the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetNoOp" /> method.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which color correction is turned off.</param>
		// Token: 0x0600096D RID: 2413 RVA: 0x00023FB8 File Offset: 0x000221B8
		public void SetNoOp(ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesNoOp(new HandleRef(this, this.nativeImageAttributes), type, true);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the <see langword="NoOp" /> setting for the default category.</summary>
		// Token: 0x0600096E RID: 2414 RVA: 0x00023FE3 File Offset: 0x000221E3
		public void ClearNoOp()
		{
			this.ClearNoOp(ColorAdjustType.Default);
		}

		/// <summary>Clears the <see langword="NoOp" /> setting for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the <see langword="NoOp" /> setting is cleared.</param>
		// Token: 0x0600096F RID: 2415 RVA: 0x00023FEC File Offset: 0x000221EC
		public void ClearNoOp(ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesNoOp(new HandleRef(this, this.nativeImageAttributes), type, false);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color key for the default category.</summary>
		/// <param name="colorLow">The low color-key value.</param>
		/// <param name="colorHigh">The high color-key value.</param>
		// Token: 0x06000970 RID: 2416 RVA: 0x00024017 File Offset: 0x00022217
		public void SetColorKey(Color colorLow, Color colorHigh)
		{
			this.SetColorKey(colorLow, colorHigh, ColorAdjustType.Default);
		}

		/// <summary>Sets the color key (transparency range) for a specified category.</summary>
		/// <param name="colorLow">The low color-key value.</param>
		/// <param name="colorHigh">The high color-key value.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color key is set.</param>
		// Token: 0x06000971 RID: 2417 RVA: 0x00024024 File Offset: 0x00022224
		public void SetColorKey(Color colorLow, Color colorHigh, ColorAdjustType type)
		{
			int num = colorLow.ToArgb();
			int num2 = colorHigh.ToArgb();
			int num3 = SafeNativeMethods.Gdip.GdipSetImageAttributesColorKeys(new HandleRef(this, this.nativeImageAttributes), type, true, num, num2);
			if (num3 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num3);
			}
		}

		/// <summary>Clears the color key (transparency range) for the default category.</summary>
		// Token: 0x06000972 RID: 2418 RVA: 0x00024061 File Offset: 0x00022261
		public void ClearColorKey()
		{
			this.ClearColorKey(ColorAdjustType.Default);
		}

		/// <summary>Clears the color key (transparency range) for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color key is cleared.</param>
		// Token: 0x06000973 RID: 2419 RVA: 0x0002406C File Offset: 0x0002226C
		public void ClearColorKey(ColorAdjustType type)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipSetImageAttributesColorKeys(new HandleRef(this, this.nativeImageAttributes), type, false, num, num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
		}

		/// <summary>Sets the CMYK (cyan-magenta-yellow-black) output channel for the default category.</summary>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorChannelFlag" /> that specifies the output channel.</param>
		// Token: 0x06000974 RID: 2420 RVA: 0x0002409B File Offset: 0x0002229B
		public void SetOutputChannel(ColorChannelFlag flags)
		{
			this.SetOutputChannel(flags, ColorAdjustType.Default);
		}

		/// <summary>Sets the CMYK (cyan-magenta-yellow-black) output channel for a specified category.</summary>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorChannelFlag" /> that specifies the output channel.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel is set.</param>
		// Token: 0x06000975 RID: 2421 RVA: 0x000240A8 File Offset: 0x000222A8
		public void SetOutputChannel(ColorChannelFlag flags, ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesOutputChannel(new HandleRef(this, this.nativeImageAttributes), type, true, flags);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the CMYK (cyan-magenta-yellow-black) output channel setting for the default category.</summary>
		// Token: 0x06000976 RID: 2422 RVA: 0x000240D4 File Offset: 0x000222D4
		public void ClearOutputChannel()
		{
			this.ClearOutputChannel(ColorAdjustType.Default);
		}

		/// <summary>Clears the (cyan-magenta-yellow-black) output channel setting for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel setting is cleared.</param>
		// Token: 0x06000977 RID: 2423 RVA: 0x000240E0 File Offset: 0x000222E0
		public void ClearOutputChannel(ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesOutputChannel(new HandleRef(this, this.nativeImageAttributes), type, false, ColorChannelFlag.ColorChannelLast);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the output channel color-profile file for the default category.</summary>
		/// <param name="colorProfileFilename">The path name of a color-profile file. If the color-profile file is in the %SystemRoot%\System32\Spool\Drivers\Color directory, this parameter can be the file name. Otherwise, this parameter must be the fully qualified path name.</param>
		// Token: 0x06000978 RID: 2424 RVA: 0x0002410C File Offset: 0x0002230C
		public void SetOutputChannelColorProfile(string colorProfileFilename)
		{
			this.SetOutputChannelColorProfile(colorProfileFilename, ColorAdjustType.Default);
		}

		/// <summary>Sets the output channel color-profile file for a specified category.</summary>
		/// <param name="colorProfileFilename">The path name of a color-profile file. If the color-profile file is in the %SystemRoot%\System32\Spool\Drivers\Color directory, this parameter can be the file name. Otherwise, this parameter must be the fully qualified path name.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel color-profile file is set.</param>
		// Token: 0x06000979 RID: 2425 RVA: 0x00024118 File Offset: 0x00022318
		public void SetOutputChannelColorProfile(string colorProfileFilename, ColorAdjustType type)
		{
			IntSecurity.DemandReadFileIO(colorProfileFilename);
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesOutputChannelColorProfile(new HandleRef(this, this.nativeImageAttributes), type, true, colorProfileFilename);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the output channel color profile setting for the default category.</summary>
		// Token: 0x0600097A RID: 2426 RVA: 0x000240D4 File Offset: 0x000222D4
		public void ClearOutputChannelColorProfile()
		{
			this.ClearOutputChannel(ColorAdjustType.Default);
		}

		/// <summary>Clears the output channel color profile setting for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel profile setting is cleared.</param>
		// Token: 0x0600097B RID: 2427 RVA: 0x0002414C File Offset: 0x0002234C
		public void ClearOutputChannelColorProfile(ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesOutputChannel(new HandleRef(this, this.nativeImageAttributes), type, false, ColorChannelFlag.ColorChannelLast);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color-remap table for the default category.</summary>
		/// <param name="map">An array of color pairs of type <see cref="T:System.Drawing.Imaging.ColorMap" />. Each color pair contains an existing color (the first value) and the color that it will be mapped to (the second value).</param>
		// Token: 0x0600097C RID: 2428 RVA: 0x00024178 File Offset: 0x00022378
		public void SetRemapTable(ColorMap[] map)
		{
			this.SetRemapTable(map, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-remap table for a specified category.</summary>
		/// <param name="map">An array of color pairs of type <see cref="T:System.Drawing.Imaging.ColorMap" />. Each color pair contains an existing color (the first value) and the color that it will be mapped to (the second value).</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-remap table is set.</param>
		// Token: 0x0600097D RID: 2429 RVA: 0x00024184 File Offset: 0x00022384
		public void SetRemapTable(ColorMap[] map, ColorAdjustType type)
		{
			int num = map.Length;
			int num2 = 4;
			IntPtr intPtr = Marshal.AllocHGlobal(checked(num * num2 * 2));
			try
			{
				for (int i = 0; i < num; i++)
				{
					Marshal.StructureToPtr(map[i].OldColor.ToArgb(), (IntPtr)((long)intPtr + (long)(i * num2 * 2)), false);
					Marshal.StructureToPtr(map[i].NewColor.ToArgb(), (IntPtr)((long)intPtr + (long)(i * num2 * 2) + (long)num2), false);
				}
				int num3 = SafeNativeMethods.Gdip.GdipSetImageAttributesRemapTable(new HandleRef(this, this.nativeImageAttributes), type, true, num, new HandleRef(null, intPtr));
				if (num3 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num3);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Clears the color-remap table for the default category.</summary>
		// Token: 0x0600097E RID: 2430 RVA: 0x00024254 File Offset: 0x00022454
		public void ClearRemapTable()
		{
			this.ClearRemapTable(ColorAdjustType.Default);
		}

		/// <summary>Clears the color-remap table for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the remap table is cleared.</param>
		// Token: 0x0600097F RID: 2431 RVA: 0x00024260 File Offset: 0x00022460
		public void ClearRemapTable(ColorAdjustType type)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesRemapTable(new HandleRef(this, this.nativeImageAttributes), type, false, 0, NativeMethods.NullHandleRef);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color-remap table for the brush category.</summary>
		/// <param name="map">An array of <see cref="T:System.Drawing.Imaging.ColorMap" /> objects.</param>
		// Token: 0x06000980 RID: 2432 RVA: 0x00024291 File Offset: 0x00022491
		public void SetBrushRemapTable(ColorMap[] map)
		{
			this.SetRemapTable(map, ColorAdjustType.Brush);
		}

		/// <summary>Clears the brush color-remap table of this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
		// Token: 0x06000981 RID: 2433 RVA: 0x0002429B File Offset: 0x0002249B
		public void ClearBrushRemapTable()
		{
			this.ClearRemapTable(ColorAdjustType.Brush);
		}

		/// <summary>Sets the wrap mode that is used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area.</param>
		// Token: 0x06000982 RID: 2434 RVA: 0x000242A4 File Offset: 0x000224A4
		public void SetWrapMode(WrapMode mode)
		{
			this.SetWrapMode(mode, default(Color), false);
		}

		/// <summary>Sets the wrap mode and color used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area.</param>
		/// <param name="color">An <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object that specifies the color of pixels outside of a rendered image. This color is visible if the mode parameter is set to <see cref="F:System.Drawing.Drawing2D.WrapMode.Clamp" /> and the source rectangle passed to <see cref="Overload:System.Drawing.Graphics.DrawImage" /> is larger than the image itself.</param>
		// Token: 0x06000983 RID: 2435 RVA: 0x000242C2 File Offset: 0x000224C2
		public void SetWrapMode(WrapMode mode, Color color)
		{
			this.SetWrapMode(mode, color, false);
		}

		/// <summary>Sets the wrap mode and color used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area.</param>
		/// <param name="color">A color object that specifies the color of pixels outside of a rendered image. This color is visible if the mode parameter is set to <see cref="F:System.Drawing.Drawing2D.WrapMode.Clamp" /> and the source rectangle passed to <see cref="Overload:System.Drawing.Graphics.DrawImage" /> is larger than the image itself.</param>
		/// <param name="clamp">This parameter has no effect. Set it to <see langword="false" />.</param>
		// Token: 0x06000984 RID: 2436 RVA: 0x000242D0 File Offset: 0x000224D0
		public void SetWrapMode(WrapMode mode, Color color, bool clamp)
		{
			int num = SafeNativeMethods.Gdip.GdipSetImageAttributesWrapMode(new HandleRef(this, this.nativeImageAttributes), (int)mode, color.ToArgb(), clamp);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adjusts the colors in a palette according to the adjustment settings of a specified category.</summary>
		/// <param name="palette">A <see cref="T:System.Drawing.Imaging.ColorPalette" /> that on input contains the palette to be adjusted, and on output contains the adjusted palette.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category whose adjustment settings will be applied to the palette.</param>
		// Token: 0x06000985 RID: 2437 RVA: 0x00024304 File Offset: 0x00022504
		public void GetAdjustedPalette(ColorPalette palette, ColorAdjustType type)
		{
			IntPtr intPtr = palette.ConvertToMemory();
			try
			{
				int num = SafeNativeMethods.Gdip.GdipGetImageAttributesAdjustedPalette(new HandleRef(this, this.nativeImageAttributes), new HandleRef(null, intPtr), type);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				palette.ConvertFromMemory(intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x040008AA RID: 2218
		internal IntPtr nativeImageAttributes;
	}
}

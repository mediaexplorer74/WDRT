using System;
using System.Drawing.Drawing2D;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Describes the interior of a graphics shape composed of rectangles and paths. This class cannot be inherited.</summary>
	// Token: 0x0200002E RID: 46
	public sealed class Region : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" />.</summary>
		// Token: 0x060004A1 RID: 1185 RVA: 0x00015FD0 File Offset: 0x000141D0
		public Region()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateRegion(out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeRegion(zero);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that defines the interior of the new <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004A2 RID: 1186 RVA: 0x00016004 File Offset: 0x00014204
		public Region(RectangleF rect)
		{
			IntPtr zero = IntPtr.Zero;
			GPRECTF gprectf = rect.ToGPRECTF();
			int num = SafeNativeMethods.Gdip.GdipCreateRegionRect(ref gprectf, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeRegion(zero);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that defines the interior of the new <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004A3 RID: 1187 RVA: 0x00016040 File Offset: 0x00014240
		public Region(Rectangle rect)
		{
			IntPtr zero = IntPtr.Zero;
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCreateRegionRectI(ref gprect, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeRegion(zero);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that defines the new <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		// Token: 0x060004A4 RID: 1188 RVA: 0x0001607C File Offset: 0x0001427C
		public Region(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateRegionPath(new HandleRef(path, path.nativePath), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeRegion(zero);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from the specified data.</summary>
		/// <param name="rgnData">A <see cref="T:System.Drawing.Drawing2D.RegionData" /> that defines the interior of the new <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgnData" /> is <see langword="null" />.</exception>
		// Token: 0x060004A5 RID: 1189 RVA: 0x000160C8 File Offset: 0x000142C8
		public Region(RegionData rgnData)
		{
			if (rgnData == null)
			{
				throw new ArgumentNullException("rgnData");
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateRegionRgnData(rgnData.Data, rgnData.Data.Length, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeRegion(zero);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00016116 File Offset: 0x00014316
		internal Region(IntPtr nativeRegion)
		{
			this.SetNativeRegion(nativeRegion);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from a handle to the specified existing GDI region.</summary>
		/// <param name="hrgn">A handle to an existing <see cref="T:System.Drawing.Region" />.</param>
		/// <returns>The new <see cref="T:System.Drawing.Region" />.</returns>
		// Token: 0x060004A7 RID: 1191 RVA: 0x00016128 File Offset: 0x00014328
		public static Region FromHrgn(IntPtr hrgn)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateRegionHrgn(new HandleRef(null, hrgn), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new Region(zero);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00016164 File Offset: 0x00014364
		private void SetNativeRegion(IntPtr nativeRegion)
		{
			if (nativeRegion == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativeRegion");
			}
			this.nativeRegion = nativeRegion;
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Region" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Region" /> that this method creates.</returns>
		// Token: 0x060004A9 RID: 1193 RVA: 0x00016188 File Offset: 0x00014388
		public Region Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneRegion(new HandleRef(this, this.nativeRegion), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new Region(zero);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Region" />.</summary>
		// Token: 0x060004AA RID: 1194 RVA: 0x000161BF File Offset: 0x000143BF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000161D0 File Offset: 0x000143D0
		private void Dispose(bool disposing)
		{
			if (this.nativeRegion != IntPtr.Zero)
			{
				try
				{
					SafeNativeMethods.Gdip.GdipDeleteRegion(new HandleRef(this, this.nativeRegion));
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
					this.nativeRegion = IntPtr.Zero;
				}
			}
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x060004AC RID: 1196 RVA: 0x00016238 File Offset: 0x00014438
		~Region()
		{
			this.Dispose(false);
		}

		/// <summary>Initializes this <see cref="T:System.Drawing.Region" /> object to an infinite interior.</summary>
		// Token: 0x060004AD RID: 1197 RVA: 0x00016268 File Offset: 0x00014468
		public void MakeInfinite()
		{
			int num = SafeNativeMethods.Gdip.GdipSetInfinite(new HandleRef(this, this.nativeRegion));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Initializes this <see cref="T:System.Drawing.Region" /> to an empty interior.</summary>
		// Token: 0x060004AE RID: 1198 RVA: 0x00016294 File Offset: 0x00014494
		public void MakeEmpty()
		{
			int num = SafeNativeMethods.Gdip.GdipSetEmpty(new HandleRef(this, this.nativeRegion));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to intersect with this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004AF RID: 1199 RVA: 0x000162C0 File Offset: 0x000144C0
		public void Intersect(RectangleF rect)
		{
			GPRECTF gprectf = rect.ToGPRECTF();
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef(this, this.nativeRegion), ref gprectf, CombineMode.Intersect);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to intersect with this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004B0 RID: 1200 RVA: 0x000162F4 File Offset: 0x000144F4
		public void Intersect(Rectangle rect)
		{
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef(this, this.nativeRegion), ref gprect, CombineMode.Intersect);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to intersect with this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004B1 RID: 1201 RVA: 0x00016328 File Offset: 0x00014528
		public void Intersect(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef(this, this.nativeRegion), new HandleRef(path, path.nativePath), CombineMode.Intersect);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to intersect with this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004B2 RID: 1202 RVA: 0x0001636C File Offset: 0x0001456C
		public void Intersect(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), CombineMode.Intersect);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Releases the handle of the <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="regionHandle">The handle to the <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="regionHandle" /> is <see langword="null" />.</exception>
		// Token: 0x060004B3 RID: 1203 RVA: 0x000163B0 File Offset: 0x000145B0
		public void ReleaseHrgn(IntPtr regionHandle)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			if (regionHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("regionHandle");
			}
			SafeNativeMethods.IntDeleteObject(new HandleRef(this, regionHandle));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to unite with this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004B4 RID: 1204 RVA: 0x000163E4 File Offset: 0x000145E4
		public void Union(RectangleF rect)
		{
			GPRECTF gprectf = new GPRECTF(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef(this, this.nativeRegion), ref gprectf, CombineMode.Union);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to unite with this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004B5 RID: 1205 RVA: 0x00016418 File Offset: 0x00014618
		public void Union(Rectangle rect)
		{
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef(this, this.nativeRegion), ref gprect, CombineMode.Union);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to unite with this <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		// Token: 0x060004B6 RID: 1206 RVA: 0x0001644C File Offset: 0x0001464C
		public void Union(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef(this, this.nativeRegion), new HandleRef(path, path.nativePath), CombineMode.Union);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to unite with this <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="region" /> is <see langword="null" />.</exception>
		// Token: 0x060004B7 RID: 1207 RVA: 0x00016490 File Offset: 0x00014690
		public void Union(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), CombineMode.Union);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to <see cref="M:System.Drawing.Region.Xor(System.Drawing.Drawing2D.GraphicsPath)" /> with this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004B8 RID: 1208 RVA: 0x000164D4 File Offset: 0x000146D4
		public void Xor(RectangleF rect)
		{
			GPRECTF gprectf = new GPRECTF(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef(this, this.nativeRegion), ref gprectf, CombineMode.Xor);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to <see cref="Overload:System.Drawing.Region.Xor" /> with this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004B9 RID: 1209 RVA: 0x00016508 File Offset: 0x00014708
		public void Xor(Rectangle rect)
		{
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef(this, this.nativeRegion), ref gprect, CombineMode.Xor);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to <see cref="Overload:System.Drawing.Region.Xor" /> with this <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		// Token: 0x060004BA RID: 1210 RVA: 0x0001653C File Offset: 0x0001473C
		public void Xor(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef(this, this.nativeRegion), new HandleRef(path, path.nativePath), CombineMode.Xor);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to <see cref="Overload:System.Drawing.Region.Xor" /> with this <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="region" /> is <see langword="null" />.</exception>
		// Token: 0x060004BB RID: 1211 RVA: 0x00016580 File Offset: 0x00014780
		public void Xor(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), CombineMode.Xor);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to exclude from this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004BC RID: 1212 RVA: 0x000165C4 File Offset: 0x000147C4
		public void Exclude(RectangleF rect)
		{
			GPRECTF gprectf = new GPRECTF(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef(this, this.nativeRegion), ref gprectf, CombineMode.Exclude);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to exclude from this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004BD RID: 1213 RVA: 0x000165F8 File Offset: 0x000147F8
		public void Exclude(Rectangle rect)
		{
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef(this, this.nativeRegion), ref gprect, CombineMode.Exclude);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to exclude from this <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		// Token: 0x060004BE RID: 1214 RVA: 0x0001662C File Offset: 0x0001482C
		public void Exclude(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef(this, this.nativeRegion), new HandleRef(path, path.nativePath), CombineMode.Exclude);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to exclude from this <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="region" /> is <see langword="null" />.</exception>
		// Token: 0x060004BF RID: 1215 RVA: 0x00016670 File Offset: 0x00014870
		public void Exclude(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), CombineMode.Exclude);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.RectangleF" /> structure that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to complement this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004C0 RID: 1216 RVA: 0x000166B4 File Offset: 0x000148B4
		public void Complement(RectangleF rect)
		{
			GPRECTF gprectf = rect.ToGPRECTF();
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef(this, this.nativeRegion), ref gprectf, CombineMode.Complement);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.Rectangle" /> structure that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to complement this <see cref="T:System.Drawing.Region" />.</param>
		// Token: 0x060004C1 RID: 1217 RVA: 0x000166E8 File Offset: 0x000148E8
		public void Complement(Rectangle rect)
		{
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef(this, this.nativeRegion), ref gprect, CombineMode.Complement);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to complement this <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		// Token: 0x060004C2 RID: 1218 RVA: 0x0001671C File Offset: 0x0001491C
		public void Complement(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef(this, this.nativeRegion), new HandleRef(path, path.nativePath), CombineMode.Complement);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.Region" /> that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> object to complement this <see cref="T:System.Drawing.Region" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="region" /> is <see langword="null" />.</exception>
		// Token: 0x060004C3 RID: 1219 RVA: 0x00016760 File Offset: 0x00014960
		public void Complement(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), CombineMode.Complement);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Offsets the coordinates of this <see cref="T:System.Drawing.Region" /> by the specified amount.</summary>
		/// <param name="dx">The amount to offset this <see cref="T:System.Drawing.Region" /> horizontally.</param>
		/// <param name="dy">The amount to offset this <see cref="T:System.Drawing.Region" /> vertically.</param>
		// Token: 0x060004C4 RID: 1220 RVA: 0x000167A4 File Offset: 0x000149A4
		public void Translate(float dx, float dy)
		{
			int num = SafeNativeMethods.Gdip.GdipTranslateRegion(new HandleRef(this, this.nativeRegion), dx, dy);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Offsets the coordinates of this <see cref="T:System.Drawing.Region" /> by the specified amount.</summary>
		/// <param name="dx">The amount to offset this <see cref="T:System.Drawing.Region" /> horizontally.</param>
		/// <param name="dy">The amount to offset this <see cref="T:System.Drawing.Region" /> vertically.</param>
		// Token: 0x060004C5 RID: 1221 RVA: 0x000167D0 File Offset: 0x000149D0
		public void Translate(int dx, int dy)
		{
			int num = SafeNativeMethods.Gdip.GdipTranslateRegionI(new HandleRef(this, this.nativeRegion), dx, dy);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Transforms this <see cref="T:System.Drawing.Region" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to transform this <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="matrix" /> is <see langword="null" />.</exception>
		// Token: 0x060004C6 RID: 1222 RVA: 0x000167FC File Offset: 0x000149FC
		public void Transform(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			int num = SafeNativeMethods.Gdip.GdipTransformRegion(new HandleRef(this, this.nativeRegion), new HandleRef(matrix, matrix.nativeMatrix));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.RectangleF" /> structure that represents a rectangle that bounds this <see cref="T:System.Drawing.Region" /> on the drawing surface of a <see cref="T:System.Drawing.Graphics" /> object.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> on which this <see cref="T:System.Drawing.Region" /> is drawn.</param>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle for this <see cref="T:System.Drawing.Region" /> on the specified drawing surface.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is <see langword="null" />.</exception>
		// Token: 0x060004C7 RID: 1223 RVA: 0x00016840 File Offset: 0x00014A40
		public RectangleF GetBounds(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			GPRECTF gprectf = default(GPRECTF);
			int num = SafeNativeMethods.Gdip.GdipGetRegionBounds(new HandleRef(this, this.nativeRegion), new HandleRef(g, g.NativeGraphics), ref gprectf);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return gprectf.ToRectangleF();
		}

		/// <summary>Returns a Windows handle to this <see cref="T:System.Drawing.Region" /> in the specified graphics context.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> on which this <see cref="T:System.Drawing.Region" /> is drawn.</param>
		/// <returns>A Windows handle to this <see cref="T:System.Drawing.Region" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is <see langword="null" />.</exception>
		// Token: 0x060004C8 RID: 1224 RVA: 0x00016894 File Offset: 0x00014A94
		public IntPtr GetHrgn(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipGetRegionHRgn(new HandleRef(this, this.nativeRegion), new HandleRef(g, g.NativeGraphics), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return zero;
		}

		/// <summary>Tests whether this <see cref="T:System.Drawing.Region" /> has an empty interior on the specified drawing surface.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a drawing surface.</param>
		/// <returns>
		///   <see langword="true" /> if the interior of this <see cref="T:System.Drawing.Region" /> is empty when the transformation associated with <paramref name="g" /> is applied; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is <see langword="null" />.</exception>
		// Token: 0x060004C9 RID: 1225 RVA: 0x000168E0 File Offset: 0x00014AE0
		public bool IsEmpty(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsEmptyRegion(new HandleRef(this, this.nativeRegion), new HandleRef(g, g.NativeGraphics), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		/// <summary>Tests whether this <see cref="T:System.Drawing.Region" /> has an infinite interior on the specified drawing surface.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a drawing surface.</param>
		/// <returns>
		///   <see langword="true" /> if the interior of this <see cref="T:System.Drawing.Region" /> is infinite when the transformation associated with <paramref name="g" /> is applied; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is <see langword="null" />.</exception>
		// Token: 0x060004CA RID: 1226 RVA: 0x0001692C File Offset: 0x00014B2C
		public bool IsInfinite(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsInfiniteRegion(new HandleRef(this, this.nativeRegion), new HandleRef(g, g.NativeGraphics), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		/// <summary>Tests whether the specified <see cref="T:System.Drawing.Region" /> is identical to this <see cref="T:System.Drawing.Region" /> on the specified drawing surface.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to test.</param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a drawing surface.</param>
		/// <returns>
		///   <see langword="true" /> if the interior of region is identical to the interior of this region when the transformation associated with the <paramref name="g" /> parameter is applied; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> or <paramref name="region" /> is <see langword="null" />.</exception>
		// Token: 0x060004CB RID: 1227 RVA: 0x00016978 File Offset: 0x00014B78
		public bool Equals(Region region, Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsEqualRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), new HandleRef(g, g.NativeGraphics), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Drawing2D.RegionData" /> that represents the information that describes this <see cref="T:System.Drawing.Region" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.RegionData" /> that represents the information that describes this <see cref="T:System.Drawing.Region" />.</returns>
		// Token: 0x060004CC RID: 1228 RVA: 0x000169DC File Offset: 0x00014BDC
		public RegionData GetRegionData()
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetRegionDataSize(new HandleRef(this, this.nativeRegion), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			if (num == 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			num2 = SafeNativeMethods.Gdip.GdipGetRegionData(new HandleRef(this, this.nativeRegion), array, num, out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return new RegionData(array);
		}

		/// <summary>Tests whether the specified point is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <returns>
		///   <see langword="true" /> when the specified point is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004CD RID: 1229 RVA: 0x00016A3B File Offset: 0x00014C3B
		public bool IsVisible(float x, float y)
		{
			return this.IsVisible(new PointF(x, y), null);
		}

		/// <summary>Tests whether the specified <see cref="T:System.Drawing.PointF" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="point">The <see cref="T:System.Drawing.PointF" /> structure to test.</param>
		/// <returns>
		///   <see langword="true" /> when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004CE RID: 1230 RVA: 0x00016A4B File Offset: 0x00014C4B
		public bool IsVisible(PointF point)
		{
			return this.IsVisible(point, null);
		}

		/// <summary>Tests whether the specified point is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
		/// <returns>
		///   <see langword="true" /> when the specified point is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004CF RID: 1231 RVA: 0x00016A55 File Offset: 0x00014C55
		public bool IsVisible(float x, float y, Graphics g)
		{
			return this.IsVisible(new PointF(x, y), g);
		}

		/// <summary>Tests whether the specified <see cref="T:System.Drawing.PointF" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="point">The <see cref="T:System.Drawing.PointF" /> structure to test.</param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
		/// <returns>
		///   <see langword="true" /> when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004D0 RID: 1232 RVA: 0x00016A68 File Offset: 0x00014C68
		public bool IsVisible(PointF point, Graphics g)
		{
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsVisibleRegionPoint(new HandleRef(this, this.nativeRegion), point.X, point.Y, new HandleRef(g, (g == null) ? IntPtr.Zero : g.NativeGraphics), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		/// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test.</param>
		/// <param name="width">The width of the rectangle to test.</param>
		/// <param name="height">The height of the rectangle to test.</param>
		/// <returns>
		///   <see langword="true" /> when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004D1 RID: 1233 RVA: 0x00016ABB File Offset: 0x00014CBB
		public bool IsVisible(float x, float y, float width, float height)
		{
			return this.IsVisible(new RectangleF(x, y, width, height), null);
		}

		/// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.RectangleF" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to test.</param>
		/// <returns>
		///   <see langword="true" /> when any portion of <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004D2 RID: 1234 RVA: 0x00016ACE File Offset: 0x00014CCE
		public bool IsVisible(RectangleF rect)
		{
			return this.IsVisible(rect, null);
		}

		/// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test.</param>
		/// <param name="width">The width of the rectangle to test.</param>
		/// <param name="height">The height of the rectangle to test.</param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
		/// <returns>
		///   <see langword="true" /> when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004D3 RID: 1235 RVA: 0x00016AD8 File Offset: 0x00014CD8
		public bool IsVisible(float x, float y, float width, float height, Graphics g)
		{
			return this.IsVisible(new RectangleF(x, y, width, height), g);
		}

		/// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.RectangleF" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to test.</param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
		/// <returns>
		///   <see langword="true" /> when <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004D4 RID: 1236 RVA: 0x00016AEC File Offset: 0x00014CEC
		public bool IsVisible(RectangleF rect, Graphics g)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipIsVisibleRegionRect(new HandleRef(this, this.nativeRegion), rect.X, rect.Y, rect.Width, rect.Height, new HandleRef(g, (g == null) ? IntPtr.Zero : g.NativeGraphics), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return num != 0;
		}

		/// <summary>Tests whether the specified point is contained within this <see cref="T:System.Drawing.Region" /> object when drawn using the specified <see cref="T:System.Drawing.Graphics" /> object.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
		/// <returns>
		///   <see langword="true" /> when the specified point is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004D5 RID: 1237 RVA: 0x00016B4F File Offset: 0x00014D4F
		public bool IsVisible(int x, int y, Graphics g)
		{
			return this.IsVisible(new Point(x, y), g);
		}

		/// <summary>Tests whether the specified <see cref="T:System.Drawing.Point" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="point">The <see cref="T:System.Drawing.Point" /> structure to test.</param>
		/// <returns>
		///   <see langword="true" /> when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004D6 RID: 1238 RVA: 0x00016B5F File Offset: 0x00014D5F
		public bool IsVisible(Point point)
		{
			return this.IsVisible(point, null);
		}

		/// <summary>Tests whether the specified <see cref="T:System.Drawing.Point" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="point">The <see cref="T:System.Drawing.Point" /> structure to test.</param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
		/// <returns>
		///   <see langword="true" /> when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004D7 RID: 1239 RVA: 0x00016B6C File Offset: 0x00014D6C
		public bool IsVisible(Point point, Graphics g)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipIsVisibleRegionPointI(new HandleRef(this, this.nativeRegion), point.X, point.Y, new HandleRef(g, (g == null) ? IntPtr.Zero : g.NativeGraphics), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return num != 0;
		}

		/// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test.</param>
		/// <param name="width">The width of the rectangle to test.</param>
		/// <param name="height">The height of the rectangle to test.</param>
		/// <returns>
		///   <see langword="true" /> when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004D8 RID: 1240 RVA: 0x00016BC1 File Offset: 0x00014DC1
		public bool IsVisible(int x, int y, int width, int height)
		{
			return this.IsVisible(new Rectangle(x, y, width, height), null);
		}

		/// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.Rectangle" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to test.</param>
		/// <returns>This method returns <see langword="true" /> when any portion of <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004D9 RID: 1241 RVA: 0x00016BD4 File Offset: 0x00014DD4
		public bool IsVisible(Rectangle rect)
		{
			return this.IsVisible(rect, null);
		}

		/// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test.</param>
		/// <param name="width">The width of the rectangle to test.</param>
		/// <param name="height">The height of the rectangle to test.</param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
		/// <returns>
		///   <see langword="true" /> when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004DA RID: 1242 RVA: 0x00016BDE File Offset: 0x00014DDE
		public bool IsVisible(int x, int y, int width, int height, Graphics g)
		{
			return this.IsVisible(new Rectangle(x, y, width, height), g);
		}

		/// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.Rectangle" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to test.</param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
		/// <returns>
		///   <see langword="true" /> when any portion of the <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004DB RID: 1243 RVA: 0x00016BF4 File Offset: 0x00014DF4
		public bool IsVisible(Rectangle rect, Graphics g)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipIsVisibleRegionRectI(new HandleRef(this, this.nativeRegion), rect.X, rect.Y, rect.Width, rect.Height, new HandleRef(g, (g == null) ? IntPtr.Zero : g.NativeGraphics), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return num != 0;
		}

		/// <summary>Returns an array of <see cref="T:System.Drawing.RectangleF" /> structures that approximate this <see cref="T:System.Drawing.Region" /> after the specified matrix transformation is applied.</summary>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents a geometric transformation to apply to the region.</param>
		/// <returns>An array of <see cref="T:System.Drawing.RectangleF" /> structures that approximate this <see cref="T:System.Drawing.Region" /> after the specified matrix transformation is applied.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="matrix" /> is <see langword="null" />.</exception>
		// Token: 0x060004DC RID: 1244 RVA: 0x00016C58 File Offset: 0x00014E58
		public RectangleF[] GetRegionScans(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetRegionScansCount(new HandleRef(this, this.nativeRegion), out num, new HandleRef(matrix, matrix.nativeMatrix));
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			int num3 = Marshal.SizeOf(typeof(GPRECTF));
			IntPtr intPtr = Marshal.AllocHGlobal(checked(num3 * num));
			RectangleF[] array;
			try
			{
				num2 = SafeNativeMethods.Gdip.GdipGetRegionScans(new HandleRef(this, this.nativeRegion), intPtr, out num, new HandleRef(matrix, matrix.nativeMatrix));
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				GPRECTF gprectf = default(GPRECTF);
				array = new RectangleF[num];
				for (int i = 0; i < num; i++)
				{
					checked
					{
						array[i] = ((GPRECTF)UnsafeNativeMethods.PtrToStructure((IntPtr)((long)intPtr + unchecked((long)(checked(num3 * i)))), typeof(GPRECTF))).ToRectangleF();
					}
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return array;
		}

		// Token: 0x04000304 RID: 772
		internal IntPtr nativeRegion;
	}
}

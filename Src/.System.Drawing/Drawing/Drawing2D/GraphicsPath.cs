using System;
using System.ComponentModel;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Represents a series of connected lines and curves. This class cannot be inherited.</summary>
	// Token: 0x020000C0 RID: 192
	public sealed class GraphicsPath : MarshalByRefObject, ICloneable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with a <see cref="P:System.Drawing.Drawing2D.GraphicsPath.FillMode" /> value of <see cref="F:System.Drawing.Drawing2D.FillMode.Alternate" />.</summary>
		// Token: 0x06000A64 RID: 2660 RVA: 0x00025EF1 File Offset: 0x000240F1
		public GraphicsPath()
			: this(FillMode.Alternate)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration.</summary>
		/// <param name="fillMode">The <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the interior of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> is filled.</param>
		// Token: 0x06000A65 RID: 2661 RVA: 0x00025EFC File Offset: 0x000240FC
		public GraphicsPath(FillMode fillMode)
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreatePath((int)fillMode, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.nativePath = zero;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> array with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.PointF" /> arrays.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
		// Token: 0x06000A66 RID: 2662 RVA: 0x00025F2F File Offset: 0x0002412F
		public GraphicsPath(PointF[] pts, byte[] types)
			: this(pts, types, FillMode.Alternate)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> array with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.PointF" /> arrays and with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration element.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
		/// <param name="fillMode">A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</param>
		// Token: 0x06000A67 RID: 2663 RVA: 0x00025F3C File Offset: 0x0002413C
		public GraphicsPath(PointF[] pts, byte[] types, FillMode fillMode)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr zero = IntPtr.Zero;
			if (pts.Length != types.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			int num = types.Length;
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
			IntPtr intPtr2 = Marshal.AllocHGlobal(num);
			try
			{
				Marshal.Copy(types, 0, intPtr2, num);
				int num2 = SafeNativeMethods.Gdip.GdipCreatePath2(new HandleRef(null, intPtr), new HandleRef(null, intPtr2), num, (int)fillMode, out zero);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
			}
			this.nativePath = zero;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.Point" /> arrays.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
		// Token: 0x06000A68 RID: 2664 RVA: 0x00025FD8 File Offset: 0x000241D8
		public GraphicsPath(Point[] pts, byte[] types)
			: this(pts, types, FillMode.Alternate)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.Point" /> arrays and with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration element.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
		/// <param name="fillMode">A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</param>
		// Token: 0x06000A69 RID: 2665 RVA: 0x00025FE4 File Offset: 0x000241E4
		public GraphicsPath(Point[] pts, byte[] types, FillMode fillMode)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr zero = IntPtr.Zero;
			if (pts.Length != types.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			int num = types.Length;
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
			IntPtr intPtr2 = Marshal.AllocHGlobal(num);
			try
			{
				Marshal.Copy(types, 0, intPtr2, num);
				int num2 = SafeNativeMethods.Gdip.GdipCreatePath2I(new HandleRef(null, intPtr), new HandleRef(null, intPtr2), num, (int)fillMode, out zero);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
			}
			this.nativePath = zero;
		}

		/// <summary>Creates an exact copy of this path.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> this method creates, cast as an object.</returns>
		// Token: 0x06000A6A RID: 2666 RVA: 0x00026080 File Offset: 0x00024280
		public object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipClonePath(new HandleRef(this, this.nativePath), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new GraphicsPath(zero, 0);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000260B8 File Offset: 0x000242B8
		private GraphicsPath(IntPtr nativePath, int extra)
		{
			if (nativePath == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativePath");
			}
			this.nativePath = nativePath;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		// Token: 0x06000A6C RID: 2668 RVA: 0x000260DF File Offset: 0x000242DF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000260F0 File Offset: 0x000242F0
		private void Dispose(bool disposing)
		{
			if (this.nativePath != IntPtr.Zero)
			{
				try
				{
					SafeNativeMethods.Gdip.GdipDeletePath(new HandleRef(this, this.nativePath));
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
					this.nativePath = IntPtr.Zero;
				}
			}
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06000A6E RID: 2670 RVA: 0x00026158 File Offset: 0x00024358
		~GraphicsPath()
		{
			this.Dispose(false);
		}

		/// <summary>Empties the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> and <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> arrays and sets the <see cref="T:System.Drawing.Drawing2D.FillMode" /> to <see cref="F:System.Drawing.Drawing2D.FillMode.Alternate" />.</summary>
		// Token: 0x06000A6F RID: 2671 RVA: 0x00026188 File Offset: 0x00024388
		public void Reset()
		{
			int num = SafeNativeMethods.Gdip.GdipResetPath(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x000261B4 File Offset: 0x000243B4
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x000261E4 File Offset: 0x000243E4
		public FillMode FillMode
		{
			get
			{
				int num = 0;
				int num2 = SafeNativeMethods.Gdip.GdipGetPathFillMode(new HandleRef(this, this.nativePath), out num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				return (FillMode)num;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FillMode));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPathFillMode(new HandleRef(this, this.nativePath), (int)value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00026234 File Offset: 0x00024434
		private PathData _GetPathData()
		{
			int num = Marshal.SizeOf(typeof(GPPOINTF));
			int pointCount = this.PointCount;
			PathData pathData = new PathData();
			pathData.Types = new byte[pointCount];
			IntPtr intPtr = Marshal.AllocHGlobal(3 * IntPtr.Size);
			IntPtr intPtr2 = Marshal.AllocHGlobal(checked(num * pointCount));
			try
			{
				GCHandle gchandle = GCHandle.Alloc(pathData.Types, GCHandleType.Pinned);
				try
				{
					IntPtr intPtr3 = gchandle.AddrOfPinnedObject();
					Marshal.StructureToPtr(pointCount, intPtr, false);
					Marshal.StructureToPtr(intPtr2, (IntPtr)((long)intPtr + (long)IntPtr.Size), false);
					Marshal.StructureToPtr(intPtr3, (IntPtr)((long)intPtr + (long)(2 * IntPtr.Size)), false);
					int num2 = SafeNativeMethods.Gdip.GdipGetPathData(new HandleRef(this, this.nativePath), intPtr);
					if (num2 != 0)
					{
						throw SafeNativeMethods.Gdip.StatusException(num2);
					}
					pathData.Points = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(intPtr2, pointCount);
				}
				finally
				{
					gchandle.Free();
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
			}
			return pathData;
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Drawing2D.PathData" /> that encapsulates arrays of points (<paramref name="points" />) and types (<paramref name="types" />) for this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.PathData" /> that encapsulates arrays for both the points and types for this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00026348 File Offset: 0x00024548
		public PathData PathData
		{
			get
			{
				return this._GetPathData();
			}
		}

		/// <summary>Starts a new figure without closing the current figure. All subsequent points added to the path are added to this new figure.</summary>
		// Token: 0x06000A74 RID: 2676 RVA: 0x00026350 File Offset: 0x00024550
		public void StartFigure()
		{
			int num = SafeNativeMethods.Gdip.GdipStartPathFigure(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Closes the current figure and starts a new figure. If the current figure contains a sequence of connected lines and curves, the method closes the loop by connecting a line from the endpoint to the starting point.</summary>
		// Token: 0x06000A75 RID: 2677 RVA: 0x0002637C File Offset: 0x0002457C
		public void CloseFigure()
		{
			int num = SafeNativeMethods.Gdip.GdipClosePathFigure(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Closes all open figures in this path and starts a new figure. It closes each open figure by connecting a line from its endpoint to its starting point.</summary>
		// Token: 0x06000A76 RID: 2678 RVA: 0x000263A8 File Offset: 0x000245A8
		public void CloseAllFigures()
		{
			int num = SafeNativeMethods.Gdip.GdipClosePathFigures(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets a marker on this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		// Token: 0x06000A77 RID: 2679 RVA: 0x000263D4 File Offset: 0x000245D4
		public void SetMarkers()
		{
			int num = SafeNativeMethods.Gdip.GdipSetPathMarker(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears all markers from this path.</summary>
		// Token: 0x06000A78 RID: 2680 RVA: 0x00026400 File Offset: 0x00024600
		public void ClearMarkers()
		{
			int num = SafeNativeMethods.Gdip.GdipClearPathMarkers(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Reverses the order of points in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		// Token: 0x06000A79 RID: 2681 RVA: 0x0002642C File Offset: 0x0002462C
		public void Reverse()
		{
			int num = SafeNativeMethods.Gdip.GdipReversePath(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Gets the last point in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the last point in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		// Token: 0x06000A7A RID: 2682 RVA: 0x00026458 File Offset: 0x00024658
		public PointF GetLastPoint()
		{
			GPPOINTF gppointf = new GPPOINTF();
			int num = SafeNativeMethods.Gdip.GdipGetPathLastPoint(new HandleRef(this, this.nativePath), gppointf);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return gppointf.ToPoint();
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A7B RID: 2683 RVA: 0x0002648E File Offset: 0x0002468E
		public bool IsVisible(float x, float y)
		{
			return this.IsVisible(new PointF(x, y), null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="point">A <see cref="T:System.Drawing.PointF" /> that represents the point to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A7C RID: 2684 RVA: 0x0002649E File Offset: 0x0002469E
		public bool IsVisible(PointF point)
		{
			return this.IsVisible(point, null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> in the visible clip region of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A7D RID: 2685 RVA: 0x000264A8 File Offset: 0x000246A8
		public bool IsVisible(float x, float y, Graphics graphics)
		{
			return this.IsVisible(new PointF(x, y), graphics);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.PointF" /> that represents the point to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A7E RID: 2686 RVA: 0x000264B8 File Offset: 0x000246B8
		public bool IsVisible(PointF pt, Graphics graphics)
		{
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsVisiblePathPoint(new HandleRef(this, this.nativePath), pt.X, pt.Y, new HandleRef(graphics, (graphics != null) ? graphics.NativeGraphics : IntPtr.Zero), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A7F RID: 2687 RVA: 0x0002650B File Offset: 0x0002470B
		public bool IsVisible(int x, int y)
		{
			return this.IsVisible(new Point(x, y), null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> that represents the point to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A80 RID: 2688 RVA: 0x0002651B File Offset: 0x0002471B
		public bool IsVisible(Point point)
		{
			return this.IsVisible(point, null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />, using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A81 RID: 2689 RVA: 0x00026525 File Offset: 0x00024725
		public bool IsVisible(int x, int y, Graphics graphics)
		{
			return this.IsVisible(new Point(x, y), graphics);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that represents the point to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A82 RID: 2690 RVA: 0x00026538 File Offset: 0x00024738
		public bool IsVisible(Point pt, Graphics graphics)
		{
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsVisiblePathPointI(new HandleRef(this, this.nativePath), pt.X, pt.Y, new HandleRef(graphics, (graphics != null) ? graphics.NativeGraphics : IntPtr.Zero), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A83 RID: 2691 RVA: 0x0002658B File Offset: 0x0002478B
		public bool IsOutlineVisible(float x, float y, Pen pen)
		{
			return this.IsOutlineVisible(new PointF(x, y), pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="point">A <see cref="T:System.Drawing.PointF" /> that specifies the location to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A84 RID: 2692 RVA: 0x0002659C File Offset: 0x0002479C
		public bool IsOutlineVisible(PointF point, Pen pen)
		{
			return this.IsOutlineVisible(point, pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A85 RID: 2693 RVA: 0x000265A7 File Offset: 0x000247A7
		public bool IsOutlineVisible(float x, float y, Pen pen, Graphics graphics)
		{
			return this.IsOutlineVisible(new PointF(x, y), pen, graphics);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.PointF" /> that specifies the location to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A86 RID: 2694 RVA: 0x000265BC File Offset: 0x000247BC
		public bool IsOutlineVisible(PointF pt, Pen pen, Graphics graphics)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsOutlineVisiblePathPoint(new HandleRef(this, this.nativePath), pt.X, pt.Y, new HandleRef(pen, pen.NativePen), new HandleRef(graphics, (graphics != null) ? graphics.NativeGraphics : IntPtr.Zero), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A87 RID: 2695 RVA: 0x00026629 File Offset: 0x00024829
		public bool IsOutlineVisible(int x, int y, Pen pen)
		{
			return this.IsOutlineVisible(new Point(x, y), pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> that specifies the location to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A88 RID: 2696 RVA: 0x0002663A File Offset: 0x0002483A
		public bool IsOutlineVisible(Point point, Pen pen)
		{
			return this.IsOutlineVisible(point, pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A89 RID: 2697 RVA: 0x00026645 File Offset: 0x00024845
		public bool IsOutlineVisible(int x, int y, Pen pen, Graphics graphics)
		{
			return this.IsOutlineVisible(new Point(x, y), pen, graphics);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that specifies the location to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A8A RID: 2698 RVA: 0x00026658 File Offset: 0x00024858
		public bool IsOutlineVisible(Point pt, Pen pen, Graphics graphics)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsOutlineVisiblePathPointI(new HandleRef(this, this.nativePath), pt.X, pt.Y, new HandleRef(pen, pen.NativePen), new HandleRef(graphics, (graphics != null) ? graphics.NativeGraphics : IntPtr.Zero), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		/// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.PointF" /> that represents the starting point of the line.</param>
		/// <param name="pt2">A <see cref="T:System.Drawing.PointF" /> that represents the endpoint of the line.</param>
		// Token: 0x06000A8B RID: 2699 RVA: 0x000266C5 File Offset: 0x000248C5
		public void AddLine(PointF pt1, PointF pt2)
		{
			this.AddLine(pt1.X, pt1.Y, pt2.X, pt2.Y);
		}

		/// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the line.</param>
		/// <param name="y1">The y-coordinate of the starting point of the line.</param>
		/// <param name="x2">The x-coordinate of the endpoint of the line.</param>
		/// <param name="y2">The y-coordinate of the endpoint of the line.</param>
		// Token: 0x06000A8C RID: 2700 RVA: 0x000266EC File Offset: 0x000248EC
		public void AddLine(float x1, float y1, float x2, float y2)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathLine(new HandleRef(this, this.nativePath), x1, y1, x2, y2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Appends a series of connected line segments to the end of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the line segments to add.</param>
		// Token: 0x06000A8D RID: 2701 RVA: 0x0002671C File Offset: 0x0002491C
		public void AddLines(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathLine2(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.Point" /> that represents the starting point of the line.</param>
		/// <param name="pt2">A <see cref="T:System.Drawing.Point" /> that represents the endpoint of the line.</param>
		// Token: 0x06000A8E RID: 2702 RVA: 0x00026780 File Offset: 0x00024980
		public void AddLine(Point pt1, Point pt2)
		{
			this.AddLine(pt1.X, pt1.Y, pt2.X, pt2.Y);
		}

		/// <summary>Appends a line segment to the current figure.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the line.</param>
		/// <param name="y1">The y-coordinate of the starting point of the line.</param>
		/// <param name="x2">The x-coordinate of the endpoint of the line.</param>
		/// <param name="y2">The y-coordinate of the endpoint of the line.</param>
		// Token: 0x06000A8F RID: 2703 RVA: 0x000267A4 File Offset: 0x000249A4
		public void AddLine(int x1, int y1, int x2, int y2)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathLineI(new HandleRef(this, this.nativePath), x1, y1, x2, y2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Appends a series of connected line segments to the end of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the line segments to add.</param>
		// Token: 0x06000A90 RID: 2704 RVA: 0x000267D4 File Offset: 0x000249D4
		public void AddLines(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathLine2I(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangular bounds of the ellipse from which the arc is taken.</param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
		// Token: 0x06000A91 RID: 2705 RVA: 0x00026838 File Offset: 0x00024A38
		public void AddArc(RectangleF rect, float startAngle, float sweepAngle)
		{
			this.AddArc(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
		// Token: 0x06000A92 RID: 2706 RVA: 0x00026860 File Offset: 0x00024A60
		public void AddArc(float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathArc(new HandleRef(this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangular bounds of the ellipse from which the arc is taken.</param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
		// Token: 0x06000A93 RID: 2707 RVA: 0x00026892 File Offset: 0x00024A92
		public void AddArc(Rectangle rect, float startAngle, float sweepAngle)
		{
			this.AddArc(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
		// Token: 0x06000A94 RID: 2708 RVA: 0x000268B8 File Offset: 0x00024AB8
		public void AddArc(int x, int y, int width, int height, float startAngle, float sweepAngle)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathArcI(new HandleRef(this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.PointF" /> that represents the starting point of the curve.</param>
		/// <param name="pt2">A <see cref="T:System.Drawing.PointF" /> that represents the first control point for the curve.</param>
		/// <param name="pt3">A <see cref="T:System.Drawing.PointF" /> that represents the second control point for the curve.</param>
		/// <param name="pt4">A <see cref="T:System.Drawing.PointF" /> that represents the endpoint of the curve.</param>
		// Token: 0x06000A95 RID: 2709 RVA: 0x000268EC File Offset: 0x00024AEC
		public void AddBezier(PointF pt1, PointF pt2, PointF pt3, PointF pt4)
		{
			this.AddBezier(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the curve.</param>
		/// <param name="y1">The y-coordinate of the starting point of the curve.</param>
		/// <param name="x2">The x-coordinate of the first control point for the curve.</param>
		/// <param name="y2">The y-coordinate of the first control point for the curve.</param>
		/// <param name="x3">The x-coordinate of the second control point for the curve.</param>
		/// <param name="y3">The y-coordinate of the second control point for the curve.</param>
		/// <param name="x4">The x-coordinate of the endpoint of the curve.</param>
		/// <param name="y4">The y-coordinate of the endpoint of the curve.</param>
		// Token: 0x06000A96 RID: 2710 RVA: 0x00026938 File Offset: 0x00024B38
		public void AddBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathBezier(new HandleRef(this, this.nativePath), x1, y1, x2, y2, x3, y3, x4, y4);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds a sequence of connected cubic Bézier curves to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curves.</param>
		// Token: 0x06000A97 RID: 2711 RVA: 0x00026970 File Offset: 0x00024B70
		public void AddBeziers(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathBeziers(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.Point" /> that represents the starting point of the curve.</param>
		/// <param name="pt2">A <see cref="T:System.Drawing.Point" /> that represents the first control point for the curve.</param>
		/// <param name="pt3">A <see cref="T:System.Drawing.Point" /> that represents the second control point for the curve.</param>
		/// <param name="pt4">A <see cref="T:System.Drawing.Point" /> that represents the endpoint of the curve.</param>
		// Token: 0x06000A98 RID: 2712 RVA: 0x000269D4 File Offset: 0x00024BD4
		public void AddBezier(Point pt1, Point pt2, Point pt3, Point pt4)
		{
			this.AddBezier(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the curve.</param>
		/// <param name="y1">The y-coordinate of the starting point of the curve.</param>
		/// <param name="x2">The x-coordinate of the first control point for the curve.</param>
		/// <param name="y2">The y-coordinate of the first control point for the curve.</param>
		/// <param name="x3">The x-coordinate of the second control point for the curve.</param>
		/// <param name="y3">The y-coordinate of the second control point for the curve.</param>
		/// <param name="x4">The x-coordinate of the endpoint of the curve.</param>
		/// <param name="y4">The y-coordinate of the endpoint of the curve.</param>
		// Token: 0x06000A99 RID: 2713 RVA: 0x00026A20 File Offset: 0x00024C20
		public void AddBezier(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathBezierI(new HandleRef(this, this.nativePath), x1, y1, x2, y2, x3, y3, x4, y4);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds a sequence of connected cubic Bézier curves to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curves.</param>
		// Token: 0x06000A9A RID: 2714 RVA: 0x00026A58 File Offset: 0x00024C58
		public void AddBeziers(params Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathBeziersI(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a spline curve to the current figure. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
		// Token: 0x06000A9B RID: 2715 RVA: 0x00026ABC File Offset: 0x00024CBC
		public void AddCurve(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurve(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
		// Token: 0x06000A9C RID: 2716 RVA: 0x00026B20 File Offset: 0x00024D20
		public void AddCurve(PointF[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurve2(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
		/// <param name="offset">The index of the element in the <paramref name="points" /> array that is used as the first point in the curve.</param>
		/// <param name="numberOfSegments">The number of segments used to draw the curve. A segment can be thought of as a line connecting two points.</param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
		// Token: 0x06000A9D RID: 2717 RVA: 0x00026B84 File Offset: 0x00024D84
		public void AddCurve(PointF[] points, int offset, int numberOfSegments, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurve3(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, offset, numberOfSegments, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a spline curve to the current figure. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
		// Token: 0x06000A9E RID: 2718 RVA: 0x00026BEC File Offset: 0x00024DEC
		public void AddCurve(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurveI(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
		// Token: 0x06000A9F RID: 2719 RVA: 0x00026C50 File Offset: 0x00024E50
		public void AddCurve(Point[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurve2I(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
		/// <param name="offset">The index of the element in the <paramref name="points" /> array that is used as the first point in the curve.</param>
		/// <param name="numberOfSegments">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
		// Token: 0x06000AA0 RID: 2720 RVA: 0x00026CB4 File Offset: 0x00024EB4
		public void AddCurve(Point[] points, int offset, int numberOfSegments, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurve3I(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, offset, numberOfSegments, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
		// Token: 0x06000AA1 RID: 2721 RVA: 0x00026D1C File Offset: 0x00024F1C
		public void AddClosedCurve(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathClosedCurve(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
		/// <param name="tension">A value between from 0 through 1 that specifies the amount that the curve bends between points, with 0 being the smallest curve (sharpest corner) and 1 being the smoothest curve.</param>
		// Token: 0x06000AA2 RID: 2722 RVA: 0x00026D80 File Offset: 0x00024F80
		public void AddClosedCurve(PointF[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathClosedCurve2(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
		// Token: 0x06000AA3 RID: 2723 RVA: 0x00026DE4 File Offset: 0x00024FE4
		public void AddClosedCurve(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathClosedCurveI(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
		/// <param name="tension">A value between from 0 through 1 that specifies the amount that the curve bends between points, with 0 being the smallest curve (sharpest corner) and 1 being the smoothest curve.</param>
		// Token: 0x06000AA4 RID: 2724 RVA: 0x00026E48 File Offset: 0x00025048
		public void AddClosedCurve(Point[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathClosedCurve2I(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a rectangle to this path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle to add.</param>
		// Token: 0x06000AA5 RID: 2725 RVA: 0x00026EAC File Offset: 0x000250AC
		public void AddRectangle(RectangleF rect)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathRectangle(new HandleRef(this, this.nativePath), rect.X, rect.Y, rect.Width, rect.Height);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds a series of rectangles to this path.</summary>
		/// <param name="rects">An array of <see cref="T:System.Drawing.RectangleF" /> structures that represents the rectangles to add.</param>
		// Token: 0x06000AA6 RID: 2726 RVA: 0x00026EF4 File Offset: 0x000250F4
		public void AddRectangles(RectangleF[] rects)
		{
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathRectangles(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), rects.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a rectangle to this path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle to add.</param>
		// Token: 0x06000AA7 RID: 2727 RVA: 0x00026F58 File Offset: 0x00025158
		public void AddRectangle(Rectangle rect)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathRectangleI(new HandleRef(this, this.nativePath), rect.X, rect.Y, rect.Width, rect.Height);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds a series of rectangles to this path.</summary>
		/// <param name="rects">An array of <see cref="T:System.Drawing.Rectangle" /> structures that represents the rectangles to add.</param>
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00026FA0 File Offset: 0x000251A0
		public void AddRectangles(Rectangle[] rects)
		{
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathRectanglesI(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), rects.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the bounding rectangle that defines the ellipse.</param>
		// Token: 0x06000AA9 RID: 2729 RVA: 0x00027004 File Offset: 0x00025204
		public void AddEllipse(RectangleF rect)
		{
			this.AddEllipse(rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse.</param>
		// Token: 0x06000AAA RID: 2730 RVA: 0x00027028 File Offset: 0x00025228
		public void AddEllipse(float x, float y, float width, float height)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathEllipse(new HandleRef(this, this.nativePath), x, y, width, height);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle that defines the ellipse.</param>
		// Token: 0x06000AAB RID: 2731 RVA: 0x00027056 File Offset: 0x00025256
		public void AddEllipse(Rectangle rect)
		{
			this.AddEllipse(rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse.</param>
		// Token: 0x06000AAC RID: 2732 RVA: 0x0002707C File Offset: 0x0002527C
		public void AddEllipse(int x, int y, int width, int height)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathEllipseI(new HandleRef(this, this.nativePath), x, y, width, height);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds the outline of a pie shape to this path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />.</param>
		// Token: 0x06000AAD RID: 2733 RVA: 0x000270AA File Offset: 0x000252AA
		public void AddPie(Rectangle rect, float startAngle, float sweepAngle)
		{
			this.AddPie(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		/// <summary>Adds the outline of a pie shape to this path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />.</param>
		// Token: 0x06000AAE RID: 2734 RVA: 0x000270D0 File Offset: 0x000252D0
		public void AddPie(float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathPie(new HandleRef(this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds the outline of a pie shape to this path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />.</param>
		// Token: 0x06000AAF RID: 2735 RVA: 0x00027104 File Offset: 0x00025304
		public void AddPie(int x, int y, int width, int height, float startAngle, float sweepAngle)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathPieI(new HandleRef(this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds a polygon to this path.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the polygon to add.</param>
		// Token: 0x06000AB0 RID: 2736 RVA: 0x00027138 File Offset: 0x00025338
		public void AddPolygon(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathPolygon(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Adds a polygon to this path.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that defines the polygon to add.</param>
		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002719C File Offset: 0x0002539C
		public void AddPolygon(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathPolygonI(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to this path.</summary>
		/// <param name="addingPath">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to add.</param>
		/// <param name="connect">A Boolean value that specifies whether the first figure in the added path is part of the last figure in this path. A value of <see langword="true" /> specifies that (if possible) the first figure in the added path is part of the last figure in this path. A value of <see langword="false" /> specifies that the first figure in the added path is separate from the last figure in this path.</param>
		// Token: 0x06000AB2 RID: 2738 RVA: 0x00027200 File Offset: 0x00025400
		public void AddPath(GraphicsPath addingPath, bool connect)
		{
			if (addingPath == null)
			{
				throw new ArgumentNullException("addingPath");
			}
			int num = SafeNativeMethods.Gdip.GdipAddPathPath(new HandleRef(this, this.nativePath), new HandleRef(addingPath, addingPath.nativePath), connect);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add.</param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
		/// <param name="emSize">The height of the em square box that bounds the character.</param>
		/// <param name="origin">A <see cref="T:System.Drawing.PointF" /> that represents the point where the text starts.</param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
		// Token: 0x06000AB3 RID: 2739 RVA: 0x00027244 File Offset: 0x00025444
		public void AddString(string s, FontFamily family, int style, float emSize, PointF origin, StringFormat format)
		{
			GPRECTF gprectf = new GPRECTF(origin.X, origin.Y, 0f, 0f);
			int num = SafeNativeMethods.Gdip.GdipAddPathString(new HandleRef(this, this.nativePath), s, s.Length, new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero), style, emSize, ref gprectf, new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add.</param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
		/// <param name="emSize">The height of the em square box that bounds the character.</param>
		/// <param name="origin">A <see cref="T:System.Drawing.Point" /> that represents the point where the text starts.</param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
		// Token: 0x06000AB4 RID: 2740 RVA: 0x000272C8 File Offset: 0x000254C8
		public void AddString(string s, FontFamily family, int style, float emSize, Point origin, StringFormat format)
		{
			GPRECT gprect = new GPRECT(origin.X, origin.Y, 0, 0);
			int num = SafeNativeMethods.Gdip.GdipAddPathStringI(new HandleRef(this, this.nativePath), s, s.Length, new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero), style, emSize, ref gprect, new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add.</param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
		/// <param name="emSize">The height of the em square box that bounds the character.</param>
		/// <param name="layoutRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the text.</param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
		// Token: 0x06000AB5 RID: 2741 RVA: 0x00027344 File Offset: 0x00025544
		public void AddString(string s, FontFamily family, int style, float emSize, RectangleF layoutRect, StringFormat format)
		{
			GPRECTF gprectf = new GPRECTF(layoutRect);
			int num = SafeNativeMethods.Gdip.GdipAddPathString(new HandleRef(this, this.nativePath), s, s.Length, new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero), style, emSize, ref gprectf, new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add.</param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
		/// <param name="emSize">The height of the em square box that bounds the character.</param>
		/// <param name="layoutRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the text.</param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
		// Token: 0x06000AB6 RID: 2742 RVA: 0x000273B4 File Offset: 0x000255B4
		public void AddString(string s, FontFamily family, int style, float emSize, Rectangle layoutRect, StringFormat format)
		{
			GPRECT gprect = new GPRECT(layoutRect);
			int num = SafeNativeMethods.Gdip.GdipAddPathStringI(new HandleRef(this, this.nativePath), s, s.Length, new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero), style, emSize, ref gprect, new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Applies a transform matrix to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the transformation to apply.</param>
		// Token: 0x06000AB7 RID: 2743 RVA: 0x00027424 File Offset: 0x00025624
		public void Transform(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			if (matrix.nativeMatrix == IntPtr.Zero)
			{
				return;
			}
			int num = SafeNativeMethods.Gdip.GdipTransformPath(new HandleRef(this, this.nativePath), new HandleRef(matrix, matrix.nativeMatrix));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002747A File Offset: 0x0002567A
		public RectangleF GetBounds()
		{
			return this.GetBounds(null);
		}

		/// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when this path is transformed by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transformation to be applied to this path before the bounding rectangle is calculated. This path is not permanently transformed; the transformation is used only during the process of calculating the bounding rectangle.</param>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		// Token: 0x06000AB9 RID: 2745 RVA: 0x00027483 File Offset: 0x00025683
		public RectangleF GetBounds(Matrix matrix)
		{
			return this.GetBounds(matrix, null);
		}

		/// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when the current path is transformed by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> and drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transformation to be applied to this path before the bounding rectangle is calculated. This path is not permanently transformed; the transformation is used only during the process of calculating the bounding rectangle.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> with which to draw the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		// Token: 0x06000ABA RID: 2746 RVA: 0x00027490 File Offset: 0x00025690
		public RectangleF GetBounds(Matrix matrix, Pen pen)
		{
			GPRECTF gprectf = default(GPRECTF);
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			if (matrix != null)
			{
				intPtr = matrix.nativeMatrix;
			}
			if (pen != null)
			{
				intPtr2 = pen.NativePen;
			}
			int num = SafeNativeMethods.Gdip.GdipGetPathWorldBounds(new HandleRef(this, this.nativePath), ref gprectf, new HandleRef(matrix, intPtr), new HandleRef(pen, intPtr2));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return gprectf.ToRectangleF();
		}

		/// <summary>Converts each curve in this path into a sequence of connected line segments.</summary>
		// Token: 0x06000ABB RID: 2747 RVA: 0x000274F8 File Offset: 0x000256F8
		public void Flatten()
		{
			this.Flatten(null);
		}

		/// <summary>Applies the specified transform and then converts each curve in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into a sequence of connected line segments.</summary>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to transform this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> before flattening.</param>
		// Token: 0x06000ABC RID: 2748 RVA: 0x00027501 File Offset: 0x00025701
		public void Flatten(Matrix matrix)
		{
			this.Flatten(matrix, 0.25f);
		}

		/// <summary>Converts each curve in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into a sequence of connected line segments.</summary>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to transform this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> before flattening.</param>
		/// <param name="flatness">Specifies the maximum permitted error between the curve and its flattened approximation. A value of 0.25 is the default. Reducing the flatness value will increase the number of line segments in the approximation.</param>
		// Token: 0x06000ABD RID: 2749 RVA: 0x00027510 File Offset: 0x00025710
		public void Flatten(Matrix matrix, float flatness)
		{
			int num = SafeNativeMethods.Gdip.GdipFlattenPath(new HandleRef(this, this.nativePath), new HandleRef(matrix, (matrix == null) ? IntPtr.Zero : matrix.nativeMatrix), flatness);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adds an additional outline to the path.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates.</param>
		// Token: 0x06000ABE RID: 2750 RVA: 0x00027550 File Offset: 0x00025750
		public void Widen(Pen pen)
		{
			float num = 0.6666667f;
			this.Widen(pen, null, num);
		}

		/// <summary>Adds an additional outline to the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates.</param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transform to apply to the path before widening.</param>
		// Token: 0x06000ABF RID: 2751 RVA: 0x0002756C File Offset: 0x0002576C
		public void Widen(Pen pen, Matrix matrix)
		{
			float num = 0.6666667f;
			this.Widen(pen, matrix, num);
		}

		/// <summary>Replaces this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> with curves that enclose the area that is filled when this path is drawn by the specified pen.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates.</param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transform to apply to the path before widening.</param>
		/// <param name="flatness">A value that specifies the flatness for curves.</param>
		// Token: 0x06000AC0 RID: 2752 RVA: 0x00027588 File Offset: 0x00025788
		public void Widen(Pen pen, Matrix matrix, float flatness)
		{
			IntPtr intPtr;
			if (matrix == null)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = matrix.nativeMatrix;
			}
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			int num;
			SafeNativeMethods.Gdip.GdipGetPointCount(new HandleRef(this, this.nativePath), out num);
			if (num == 0)
			{
				return;
			}
			int num2 = SafeNativeMethods.Gdip.GdipWidenPath(new HandleRef(this, this.nativePath), new HandleRef(pen, pen.NativePen), new HandleRef(matrix, intPtr), flatness);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
		// Token: 0x06000AC1 RID: 2753 RVA: 0x000275FD File Offset: 0x000257FD
		public void Warp(PointF[] destPoints, RectangleF srcRect)
		{
			this.Warp(destPoints, srcRect, null);
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path.</param>
		// Token: 0x06000AC2 RID: 2754 RVA: 0x00027608 File Offset: 0x00025808
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix)
		{
			this.Warp(destPoints, srcRect, matrix, WarpMode.Perspective);
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that defines a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path.</param>
		/// <param name="warpMode">A <see cref="T:System.Drawing.Drawing2D.WarpMode" /> enumeration that specifies whether this warp operation uses perspective or bilinear mode.</param>
		// Token: 0x06000AC3 RID: 2755 RVA: 0x00027614 File Offset: 0x00025814
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix, WarpMode warpMode)
		{
			this.Warp(destPoints, srcRect, matrix, warpMode, 0.25f);
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path.</param>
		/// <param name="warpMode">A <see cref="T:System.Drawing.Drawing2D.WarpMode" /> enumeration that specifies whether this warp operation uses perspective or bilinear mode.</param>
		/// <param name="flatness">A value from 0 through 1 that specifies how flat the resulting path is. For more information, see the <see cref="M:System.Drawing.Drawing2D.GraphicsPath.Flatten" /> methods.</param>
		// Token: 0x06000AC4 RID: 2756 RVA: 0x00027628 File Offset: 0x00025828
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix, WarpMode warpMode, float flatness)
		{
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipWarpPath(new HandleRef(this, this.nativePath), new HandleRef(matrix, (matrix == null) ? IntPtr.Zero : matrix.nativeMatrix), new HandleRef(null, intPtr), destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, warpMode, flatness);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Gets the number of elements in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> or the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> array.</summary>
		/// <returns>An integer that specifies the number of elements in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> or the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> array.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x000276C0 File Offset: 0x000258C0
		public int PointCount
		{
			get
			{
				int num = 0;
				int num2 = SafeNativeMethods.Gdip.GdipGetPointCount(new HandleRef(this, this.nativePath), out num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				return num;
			}
		}

		/// <summary>Gets the types of the corresponding points in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array.</summary>
		/// <returns>An array of bytes that specifies the types of the corresponding points in the path.</returns>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x000276F0 File Offset: 0x000258F0
		public byte[] PathTypes
		{
			get
			{
				int pointCount = this.PointCount;
				byte[] array = new byte[pointCount];
				int num = SafeNativeMethods.Gdip.GdipGetPathTypes(new HandleRef(this, this.nativePath), array, pointCount);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return array;
			}
		}

		/// <summary>Gets the points in the path.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.PointF" /> objects that represent the path.</returns>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002772C File Offset: 0x0002592C
		public PointF[] PathPoints
		{
			get
			{
				int pointCount = this.PointCount;
				int num = Marshal.SizeOf(typeof(GPPOINTF));
				IntPtr intPtr = Marshal.AllocHGlobal(checked(pointCount * num));
				PointF[] array2;
				try
				{
					int num2 = SafeNativeMethods.Gdip.GdipGetPathPoints(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), pointCount);
					if (num2 != 0)
					{
						throw SafeNativeMethods.Gdip.StatusException(num2);
					}
					PointF[] array = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(intPtr, pointCount);
					array2 = array;
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return array2;
			}
		}

		// Token: 0x04000990 RID: 2448
		internal IntPtr nativePath;
	}
}

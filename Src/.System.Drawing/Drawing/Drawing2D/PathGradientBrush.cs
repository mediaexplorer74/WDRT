﻿using System;
using System.ComponentModel;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates a <see cref="T:System.Drawing.Brush" /> object that fills the interior of a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object with a gradient. This class cannot be inherited.</summary>
	// Token: 0x020000CD RID: 205
	public sealed class PathGradientBrush : Brush
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that make up the vertices of the path.</param>
		// Token: 0x06000B37 RID: 2871 RVA: 0x00029249 File Offset: 0x00027449
		public PathGradientBrush(PointF[] points)
			: this(points, WrapMode.Clamp)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points and wrap mode.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that make up the vertices of the path.</param>
		/// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled.</param>
		// Token: 0x06000B38 RID: 2872 RVA: 0x00029254 File Offset: 0x00027454
		public PathGradientBrush(PointF[] points, WrapMode wrapMode)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (!ClientUtils.IsEnumValid(wrapMode, (int)wrapMode, 0, 4))
			{
				throw new InvalidEnumArgumentException("wrapMode", (int)wrapMode, typeof(WrapMode));
			}
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipCreatePathGradient(new HandleRef(null, intPtr), points.Length, (int)wrapMode, out zero);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				base.SetNativeBrushInternal(zero);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that make up the vertices of the path.</param>
		// Token: 0x06000B39 RID: 2873 RVA: 0x000292F4 File Offset: 0x000274F4
		public PathGradientBrush(Point[] points)
			: this(points, WrapMode.Clamp)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points and wrap mode.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that make up the vertices of the path.</param>
		/// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled.</param>
		// Token: 0x06000B3A RID: 2874 RVA: 0x00029300 File Offset: 0x00027500
		public PathGradientBrush(Point[] points, WrapMode wrapMode)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (!ClientUtils.IsEnumValid(wrapMode, (int)wrapMode, 0, 4))
			{
				throw new InvalidEnumArgumentException("wrapMode", (int)wrapMode, typeof(WrapMode));
			}
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipCreatePathGradientI(new HandleRef(null, intPtr), points.Length, (int)wrapMode, out zero);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				base.SetNativeBrushInternal(zero);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified path.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that defines the area filled by this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</param>
		// Token: 0x06000B3B RID: 2875 RVA: 0x000293A0 File Offset: 0x000275A0
		public PathGradientBrush(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreatePathGradientFromPath(new HandleRef(path, path.nativePath), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeBrushInternal(zero);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0001BFE6 File Offset: 0x0001A1E6
		internal PathGradientBrush(IntPtr nativeBrush)
		{
			base.SetNativeBrushInternal(nativeBrush);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> this method creates, cast as an object.</returns>
		// Token: 0x06000B3D RID: 2877 RVA: 0x000293EC File Offset: 0x000275EC
		public override object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new PathGradientBrush(zero);
		}

		/// <summary>Gets or sets the color at the center of the path gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color at the center of the path gradient.</returns>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00029424 File Offset: 0x00027624
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00029458 File Offset: 0x00027658
		public Color CenterColor
		{
			get
			{
				int num2;
				int num = SafeNativeMethods.Gdip.GdipGetPathGradientCenterColor(new HandleRef(this, base.NativeBrush), out num2);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return Color.FromArgb(num2);
			}
			set
			{
				int num = SafeNativeMethods.Gdip.GdipSetPathGradientCenterColor(new HandleRef(this, base.NativeBrush), value.ToArgb());
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00029488 File Offset: 0x00027688
		private void _SetSurroundColors(Color[] colors)
		{
			int num2;
			int num = SafeNativeMethods.Gdip.GdipGetPathGradientSurroundColorCount(new HandleRef(this, base.NativeBrush), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			if (colors.Length > num2 || num2 <= 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			num2 = colors.Length;
			int[] array = new int[num2];
			for (int i = 0; i < colors.Length; i++)
			{
				array[i] = colors[i].ToArgb();
			}
			num = SafeNativeMethods.Gdip.GdipSetPathGradientSurroundColorsWithCount(new HandleRef(this, base.NativeBrush), array, ref num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002950C File Offset: 0x0002770C
		private Color[] _GetSurroundColors()
		{
			int num2;
			int num = SafeNativeMethods.Gdip.GdipGetPathGradientSurroundColorCount(new HandleRef(this, base.NativeBrush), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			int[] array = new int[num2];
			num = SafeNativeMethods.Gdip.GdipGetPathGradientSurroundColorsWithCount(new HandleRef(this, base.NativeBrush), array, ref num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			Color[] array2 = new Color[num2];
			for (int i = 0; i < num2; i++)
			{
				array2[i] = Color.FromArgb(array[i]);
			}
			return array2;
		}

		/// <summary>Gets or sets an array of colors that correspond to the points in the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Color" /> structures that represents the colors associated with each point in the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</returns>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00029586 File Offset: 0x00027786
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x0002958E File Offset: 0x0002778E
		public Color[] SurroundColors
		{
			get
			{
				return this._GetSurroundColors();
			}
			set
			{
				this._SetSurroundColors(value);
			}
		}

		/// <summary>Gets or sets the center point of the path gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the center point of the path gradient.</returns>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00029598 File Offset: 0x00027798
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x000295D0 File Offset: 0x000277D0
		public PointF CenterPoint
		{
			get
			{
				GPPOINTF gppointf = new GPPOINTF();
				int num = SafeNativeMethods.Gdip.GdipGetPathGradientCenterPoint(new HandleRef(this, base.NativeBrush), gppointf);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return gppointf.ToPoint();
			}
			set
			{
				int num = SafeNativeMethods.Gdip.GdipSetPathGradientCenterPoint(new HandleRef(this, base.NativeBrush), new GPPOINTF(value));
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00029600 File Offset: 0x00027800
		private RectangleF _GetRectangle()
		{
			GPRECTF gprectf = default(GPRECTF);
			int num = SafeNativeMethods.Gdip.GdipGetPathGradientRect(new HandleRef(this, base.NativeBrush), ref gprectf);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return gprectf.ToRectangleF();
		}

		/// <summary>Gets a bounding rectangle for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangular region that bounds the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</returns>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0002963A File Offset: 0x0002783A
		public RectangleF Rectangle
		{
			get
			{
				return this._GetRectangle();
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00029644 File Offset: 0x00027844
		private Blend _GetBlend()
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetPathGradientBlendCount(new HandleRef(this, base.NativeBrush), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			int num3 = num;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			Blend blend;
			try
			{
				int num4 = checked(4 * num3);
				intPtr = Marshal.AllocHGlobal(num4);
				intPtr2 = Marshal.AllocHGlobal(num4);
				num2 = SafeNativeMethods.Gdip.GdipGetPathGradientBlend(new HandleRef(this, base.NativeBrush), intPtr, intPtr2, num3);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				blend = new Blend(num3);
				Marshal.Copy(intPtr, blend.Factors, 0, num3);
				Marshal.Copy(intPtr2, blend.Positions, 0, num3);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
			return blend;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002971C File Offset: 0x0002791C
		private void _SetBlend(Blend blend)
		{
			int num = blend.Factors.Length;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				int num2 = checked(4 * num);
				intPtr = Marshal.AllocHGlobal(num2);
				intPtr2 = Marshal.AllocHGlobal(num2);
				Marshal.Copy(blend.Factors, 0, intPtr, num);
				Marshal.Copy(blend.Positions, 0, intPtr2, num);
				int num3 = SafeNativeMethods.Gdip.GdipSetPathGradientBlend(new HandleRef(this, base.NativeBrush), new HandleRef(null, intPtr), new HandleRef(null, intPtr2), num);
				if (num3 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num3);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.Blend" /> that specifies positions and factors that define a custom falloff for the gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.Blend" /> that represents a custom falloff for the gradient.</returns>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x000297D4 File Offset: 0x000279D4
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x000297DC File Offset: 0x000279DC
		public Blend Blend
		{
			get
			{
				return this._GetBlend();
			}
			set
			{
				this._SetBlend(value);
			}
		}

		/// <summary>Creates a gradient brush that changes color starting from the center of the path outward to the path's boundary. The transition from one color to another is based on a bell-shaped curve.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
		// Token: 0x06000B4C RID: 2892 RVA: 0x000297E5 File Offset: 0x000279E5
		public void SetSigmaBellShape(float focus)
		{
			this.SetSigmaBellShape(focus, 1f);
		}

		/// <summary>Creates a gradient brush that changes color starting from the center of the path outward to the path's boundary. The transition from one color to another is based on a bell-shaped curve.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
		/// <param name="scale">A value from 0 through 1 that specifies the maximum intensity of the center color that gets blended with the boundary color. A value of 1 causes the highest possible intensity of the center color, and it is the default value.</param>
		// Token: 0x06000B4D RID: 2893 RVA: 0x000297F4 File Offset: 0x000279F4
		public void SetSigmaBellShape(float focus, float scale)
		{
			int num = SafeNativeMethods.Gdip.GdipSetPathGradientSigmaBlend(new HandleRef(this, base.NativeBrush), focus, scale);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Creates a gradient with a center color and a linear falloff to one surrounding color.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
		// Token: 0x06000B4E RID: 2894 RVA: 0x0002981F File Offset: 0x00027A1F
		public void SetBlendTriangularShape(float focus)
		{
			this.SetBlendTriangularShape(focus, 1f);
		}

		/// <summary>Creates a gradient with a center color and a linear falloff to each surrounding color.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
		/// <param name="scale">A value from 0 through 1 that specifies the maximum intensity of the center color that gets blended with the boundary color. A value of 1 causes the highest possible intensity of the center color, and it is the default value.</param>
		// Token: 0x06000B4F RID: 2895 RVA: 0x00029830 File Offset: 0x00027A30
		public void SetBlendTriangularShape(float focus, float scale)
		{
			int num = SafeNativeMethods.Gdip.GdipSetPathGradientLinearBlend(new HandleRef(this, base.NativeBrush), focus, scale);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002985C File Offset: 0x00027A5C
		private ColorBlend _GetInterpolationColors()
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetPathGradientPresetBlendCount(new HandleRef(this, base.NativeBrush), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			if (num == 0)
			{
				return new ColorBlend();
			}
			int num3 = num;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			ColorBlend colorBlend;
			try
			{
				int num4 = checked(4 * num3);
				intPtr = Marshal.AllocHGlobal(num4);
				intPtr2 = Marshal.AllocHGlobal(num4);
				num2 = SafeNativeMethods.Gdip.GdipGetPathGradientPresetBlend(new HandleRef(this, base.NativeBrush), intPtr, intPtr2, num3);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				colorBlend = new ColorBlend(num3);
				int[] array = new int[num3];
				Marshal.Copy(intPtr, array, 0, num3);
				Marshal.Copy(intPtr2, colorBlend.Positions, 0, num3);
				colorBlend.Colors = new Color[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					colorBlend.Colors[i] = Color.FromArgb(array[i]);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
			return colorBlend;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002997C File Offset: 0x00027B7C
		private void _SetInterpolationColors(ColorBlend blend)
		{
			int num = blend.Colors.Length;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				int num2 = checked(4 * num);
				intPtr = Marshal.AllocHGlobal(num2);
				intPtr2 = Marshal.AllocHGlobal(num2);
				int[] array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = blend.Colors[i].ToArgb();
				}
				Marshal.Copy(array, 0, intPtr, num);
				Marshal.Copy(blend.Positions, 0, intPtr2, num);
				int num3 = SafeNativeMethods.Gdip.GdipSetPathGradientPresetBlend(new HandleRef(this, base.NativeBrush), new HandleRef(null, intPtr), new HandleRef(null, intPtr2), num);
				if (num3 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num3);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00029A60 File Offset: 0x00027C60
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x00029A68 File Offset: 0x00027C68
		public ColorBlend InterpolationColors
		{
			get
			{
				return this._GetInterpolationColors();
			}
			set
			{
				this._SetInterpolationColors(value);
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00029A74 File Offset: 0x00027C74
		private void _SetTransform(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			int num = SafeNativeMethods.Gdip.GdipSetPathGradientTransform(new HandleRef(this, base.NativeBrush), new HandleRef(matrix, matrix.nativeMatrix));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00029AB8 File Offset: 0x00027CB8
		private Matrix _GetTransform()
		{
			Matrix matrix = new Matrix();
			int num = SafeNativeMethods.Gdip.GdipGetPathGradientTransform(new HandleRef(this, base.NativeBrush), new HandleRef(matrix, matrix.nativeMatrix));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return matrix;
		}

		/// <summary>Gets or sets a copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a local geometric transform for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a geometric transform that applies only to fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00029AF4 File Offset: 0x00027CF4
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x00029AFC File Offset: 0x00027CFC
		public Matrix Transform
		{
			get
			{
				return this._GetTransform();
			}
			set
			{
				this._SetTransform(value);
			}
		}

		/// <summary>Resets the <see cref="P:System.Drawing.Drawing2D.PathGradientBrush.Transform" /> property to identity.</summary>
		// Token: 0x06000B58 RID: 2904 RVA: 0x00029B08 File Offset: 0x00027D08
		public void ResetTransform()
		{
			int num = SafeNativeMethods.Gdip.GdipResetPathGradientTransform(new HandleRef(this, base.NativeBrush));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Updates the brush's transformation matrix with the product of brush's transformation matrix multiplied by another matrix.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that will be multiplied by the brush's current transformation matrix.</param>
		// Token: 0x06000B59 RID: 2905 RVA: 0x00029B31 File Offset: 0x00027D31
		public void MultiplyTransform(Matrix matrix)
		{
			this.MultiplyTransform(matrix, MatrixOrder.Prepend);
		}

		/// <summary>Updates the brush's transformation matrix with the product of the brush's transformation matrix multiplied by another matrix.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that will be multiplied by the brush's current transformation matrix.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies in which order to multiply the two matrices.</param>
		// Token: 0x06000B5A RID: 2906 RVA: 0x00029B3C File Offset: 0x00027D3C
		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			int num = SafeNativeMethods.Gdip.GdipMultiplyPathGradientTransform(new HandleRef(this, base.NativeBrush), new HandleRef(matrix, matrix.nativeMatrix), order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Applies the specified translation to the local geometric transform. This method prepends the translation to the transform.</summary>
		/// <param name="dx">The value of the translation in x.</param>
		/// <param name="dy">The value of the translation in y.</param>
		// Token: 0x06000B5B RID: 2907 RVA: 0x00029B80 File Offset: 0x00027D80
		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified translation to the local geometric transform in the specified order.</summary>
		/// <param name="dx">The value of the translation in x.</param>
		/// <param name="dy">The value of the translation in y.</param>
		/// <param name="order">The order (prepend or append) in which to apply the translation.</param>
		// Token: 0x06000B5C RID: 2908 RVA: 0x00029B8C File Offset: 0x00027D8C
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipTranslatePathGradientTransform(new HandleRef(this, base.NativeBrush), dx, dy, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Scales the local geometric transform by the specified amounts. This method prepends the scaling matrix to the transform.</summary>
		/// <param name="sx">The transform scale factor in the x-axis direction.</param>
		/// <param name="sy">The transform scale factor in the y-axis direction.</param>
		// Token: 0x06000B5D RID: 2909 RVA: 0x00029BB8 File Offset: 0x00027DB8
		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		/// <summary>Scales the local geometric transform by the specified amounts in the specified order.</summary>
		/// <param name="sx">The transform scale factor in the x-axis direction.</param>
		/// <param name="sy">The transform scale factor in the y-axis direction.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the scaling matrix.</param>
		// Token: 0x06000B5E RID: 2910 RVA: 0x00029BC4 File Offset: 0x00027DC4
		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipScalePathGradientTransform(new HandleRef(this, base.NativeBrush), sx, sy, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Rotates the local geometric transform by the specified amount. This method prepends the rotation to the transform.</summary>
		/// <param name="angle">The angle (extent) of rotation.</param>
		// Token: 0x06000B5F RID: 2911 RVA: 0x00029BF0 File Offset: 0x00027DF0
		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		/// <summary>Rotates the local geometric transform by the specified amount in the specified order.</summary>
		/// <param name="angle">The angle (extent) of rotation.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the rotation matrix.</param>
		// Token: 0x06000B60 RID: 2912 RVA: 0x00029BFC File Offset: 0x00027DFC
		public void RotateTransform(float angle, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipRotatePathGradientTransform(new HandleRef(this, base.NativeBrush), angle, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Gets or sets the focus point for the gradient falloff.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the focus point for the gradient falloff.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00029C28 File Offset: 0x00027E28
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x00029C6C File Offset: 0x00027E6C
		public PointF FocusScales
		{
			get
			{
				float[] array = new float[1];
				float[] array2 = new float[1];
				int num = SafeNativeMethods.Gdip.GdipGetPathGradientFocusScales(new HandleRef(this, base.NativeBrush), array, array2);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return new PointF(array[0], array2[0]);
			}
			set
			{
				int num = SafeNativeMethods.Gdip.GdipSetPathGradientFocusScales(new HandleRef(this, base.NativeBrush), value.X, value.Y);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00029CA4 File Offset: 0x00027EA4
		private void _SetWrapMode(WrapMode wrapMode)
		{
			int num = SafeNativeMethods.Gdip.GdipSetPathGradientWrapMode(new HandleRef(this, base.NativeBrush), (int)wrapMode);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00029CD0 File Offset: 0x00027ED0
		private WrapMode _GetWrapMode()
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetPathGradientWrapMode(new HandleRef(this, base.NativeBrush), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return (WrapMode)num;
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that indicates the wrap mode for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00029CFE File Offset: 0x00027EFE
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x00029D06 File Offset: 0x00027F06
		public WrapMode WrapMode
		{
			get
			{
				return this._GetWrapMode();
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(WrapMode));
				}
				this._SetWrapMode(value);
			}
		}
	}
}

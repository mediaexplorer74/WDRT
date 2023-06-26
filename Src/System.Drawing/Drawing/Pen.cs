using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Defines an object used to draw lines and curves. This class cannot be inherited.</summary>
	// Token: 0x02000028 RID: 40
	public sealed class Pen : MarshalByRefObject, ISystemColorTracker, ICloneable, IDisposable
	{
		// Token: 0x06000381 RID: 897 RVA: 0x0001152A File Offset: 0x0000F72A
		private Pen(IntPtr nativePen)
		{
			this.SetNativePen(nativePen);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011539 File Offset: 0x0000F739
		internal Pen(Color color, bool immutable)
			: this(color)
		{
			this.immutable = immutable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified color.</summary>
		/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that indicates the color of this <see cref="T:System.Drawing.Pen" />.</param>
		// Token: 0x06000383 RID: 899 RVA: 0x00011549 File Offset: 0x0000F749
		public Pen(Color color)
			: this(color, 1f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Color" /> and <see cref="P:System.Drawing.Pen.Width" /> properties.</summary>
		/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that indicates the color of this <see cref="T:System.Drawing.Pen" />.</param>
		/// <param name="width">A value indicating the width of this <see cref="T:System.Drawing.Pen" />.</param>
		// Token: 0x06000384 RID: 900 RVA: 0x00011558 File Offset: 0x0000F758
		public Pen(Color color, float width)
		{
			this.color = color;
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreatePen1(color.ToArgb(), width, 0, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativePen(zero);
			if (this.color.IsSystemColor)
			{
				SystemColorTracker.Add(this);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Brush" />.</summary>
		/// <param name="brush">A <see cref="T:System.Drawing.Brush" /> that determines the fill properties of this <see cref="T:System.Drawing.Pen" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x06000385 RID: 901 RVA: 0x000115AD File Offset: 0x0000F7AD
		public Pen(Brush brush)
			: this(brush, 1f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="P:System.Drawing.Pen.Width" />.</summary>
		/// <param name="brush">A <see cref="T:System.Drawing.Brush" /> that determines the characteristics of this <see cref="T:System.Drawing.Pen" />.</param>
		/// <param name="width">The width of the new <see cref="T:System.Drawing.Pen" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x06000386 RID: 902 RVA: 0x000115BC File Offset: 0x0000F7BC
		public Pen(Brush brush, float width)
		{
			IntPtr zero = IntPtr.Zero;
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			int num = SafeNativeMethods.Gdip.GdipCreatePen2(new HandleRef(brush, brush.NativeBrush), width, 0, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativePen(zero);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001160A File Offset: 0x0000F80A
		internal void SetNativePen(IntPtr nativePen)
		{
			if (nativePen == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativePen");
			}
			this.nativePen = nativePen;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0001162B File Offset: 0x0000F82B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal IntPtr NativePen
		{
			get
			{
				return this.nativePen;
			}
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast to a <see cref="T:System.Drawing.Pen" />.</returns>
		// Token: 0x06000389 RID: 905 RVA: 0x00011634 File Offset: 0x0000F834
		public object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipClonePen(new HandleRef(this, this.NativePen), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new Pen(zero);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Pen" />.</summary>
		// Token: 0x0600038A RID: 906 RVA: 0x0001166B File Offset: 0x0000F86B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001167C File Offset: 0x0000F87C
		private void Dispose(bool disposing)
		{
			if (!disposing)
			{
				this.immutable = false;
			}
			else if (this.immutable)
			{
				throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Brush" }));
			}
			if (this.nativePen != IntPtr.Zero)
			{
				try
				{
					SafeNativeMethods.Gdip.GdipDeletePen(new HandleRef(this, this.NativePen));
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
					this.nativePen = IntPtr.Zero;
				}
			}
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x0600038C RID: 908 RVA: 0x00011718 File Offset: 0x0000F918
		~Pen()
		{
			this.Dispose(false);
		}

		/// <summary>Gets or sets the width of this <see cref="T:System.Drawing.Pen" />, in units of the <see cref="T:System.Drawing.Graphics" /> object used for drawing.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Width" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00011748 File Offset: 0x0000F948
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0001177C File Offset: 0x0000F97C
		public float Width
		{
			get
			{
				float[] array = new float[1];
				int num = SafeNativeMethods.Gdip.GdipGetPenWidth(new HandleRef(this, this.NativePen), array);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return array[0];
			}
			set
			{
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenWidth(new HandleRef(this, this.NativePen), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Sets the values that determine the style of cap used to end lines drawn by this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="startCap">A <see cref="T:System.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</param>
		/// <param name="endCap">A <see cref="T:System.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</param>
		/// <param name="dashCap">A <see cref="T:System.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the beginning or end of dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</param>
		// Token: 0x0600038F RID: 911 RVA: 0x000117CC File Offset: 0x0000F9CC
		public void SetLineCap(LineCap startCap, LineCap endCap, DashCap dashCap)
		{
			if (this.immutable)
			{
				throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
			}
			int num = SafeNativeMethods.Gdip.GdipSetPenLineCap197819(new HandleRef(this, this.NativePen), (int)startCap, (int)endCap, (int)dashCap);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Gets or sets the cap style used at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.LineCap" /> values that represents the cap style used at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.LineCap" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.StartCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00011820 File Offset: 0x0000FA20
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00011850 File Offset: 0x0000FA50
		public LineCap StartCap
		{
			get
			{
				int num = 0;
				int num2 = SafeNativeMethods.Gdip.GdipGetPenStartCap(new HandleRef(this, this.NativePen), out num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				return (LineCap)num;
			}
			set
			{
				if (value <= LineCap.ArrowAnchor)
				{
					if (value <= LineCap.Triangle || value - LineCap.NoAnchor <= 4)
					{
						goto IL_38;
					}
				}
				else if (value == LineCap.AnchorMask || value == LineCap.Custom)
				{
					goto IL_38;
				}
				throw new InvalidEnumArgumentException("value", (int)value, typeof(LineCap));
				IL_38:
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenStartCap(new HandleRef(this, this.NativePen), (int)value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets the cap style used at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.LineCap" /> values that represents the cap style used at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.LineCap" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.EndCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000392 RID: 914 RVA: 0x000118D8 File Offset: 0x0000FAD8
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00011908 File Offset: 0x0000FB08
		public LineCap EndCap
		{
			get
			{
				int num = 0;
				int num2 = SafeNativeMethods.Gdip.GdipGetPenEndCap(new HandleRef(this, this.NativePen), out num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				return (LineCap)num;
			}
			set
			{
				if (value <= LineCap.ArrowAnchor)
				{
					if (value <= LineCap.Triangle || value - LineCap.NoAnchor <= 4)
					{
						goto IL_38;
					}
				}
				else if (value == LineCap.AnchorMask || value == LineCap.Custom)
				{
					goto IL_38;
				}
				throw new InvalidEnumArgumentException("value", (int)value, typeof(LineCap));
				IL_38:
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenEndCap(new HandleRef(this, this.NativePen), (int)value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets the cap style used at the end of the dashes that make up dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.DashCap" /> values that represents the cap style used at the beginning and end of the dashes that make up dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.DashCap" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00011990 File Offset: 0x0000FB90
		// (set) Token: 0x06000395 RID: 917 RVA: 0x000119C0 File Offset: 0x0000FBC0
		public DashCap DashCap
		{
			get
			{
				int num = 0;
				int num2 = SafeNativeMethods.Gdip.GdipGetPenDashCap197819(new HandleRef(this, this.NativePen), out num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				return (DashCap)num;
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[] { 0, 2, 3 }))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DashCap));
				}
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenDashCap197819(new HandleRef(this, this.NativePen), (int)value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets the join style for the ends of two consecutive lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.LineJoin" /> that represents the join style for the ends of two consecutive lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.LineJoin" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00011A44 File Offset: 0x0000FC44
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00011A74 File Offset: 0x0000FC74
		public LineJoin LineJoin
		{
			get
			{
				int num = 0;
				int num2 = SafeNativeMethods.Gdip.GdipGetPenLineJoin(new HandleRef(this, this.NativePen), out num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				return (LineJoin)num;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(LineJoin));
				}
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenLineJoin(new HandleRef(this, this.NativePen), (int)value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets a custom cap to use at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> that represents the cap used at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.CustomStartCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00011AEC File Offset: 0x0000FCEC
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00011B24 File Offset: 0x0000FD24
		public CustomLineCap CustomStartCap
		{
			get
			{
				IntPtr zero = IntPtr.Zero;
				int num = SafeNativeMethods.Gdip.GdipGetPenCustomStartCap(new HandleRef(this, this.NativePen), out zero);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return CustomLineCap.CreateCustomLineCapObject(zero);
			}
			set
			{
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenCustomStartCap(new HandleRef(this, this.NativePen), new HandleRef(value, (value == null) ? IntPtr.Zero : value.nativeCap));
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets a custom cap to use at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> that represents the cap used at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.CustomEndCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00011B90 File Offset: 0x0000FD90
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00011BC8 File Offset: 0x0000FDC8
		public CustomLineCap CustomEndCap
		{
			get
			{
				IntPtr zero = IntPtr.Zero;
				int num = SafeNativeMethods.Gdip.GdipGetPenCustomEndCap(new HandleRef(this, this.NativePen), out zero);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return CustomLineCap.CreateCustomLineCapObject(zero);
			}
			set
			{
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenCustomEndCap(new HandleRef(this, this.NativePen), new HandleRef(value, (value == null) ? IntPtr.Zero : value.nativeCap));
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets the limit of the thickness of the join on a mitered corner.</summary>
		/// <returns>The limit of the thickness of the join on a mitered corner.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.MiterLimit" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00011C34 File Offset: 0x0000FE34
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00011C68 File Offset: 0x0000FE68
		public float MiterLimit
		{
			get
			{
				float[] array = new float[1];
				int num = SafeNativeMethods.Gdip.GdipGetPenMiterLimit(new HandleRef(this, this.NativePen), array);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return array[0];
			}
			set
			{
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenMiterLimit(new HandleRef(this, this.NativePen), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets the alignment for this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.PenAlignment" /> that represents the alignment for this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.PenAlignment" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Alignment" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00011CB8 File Offset: 0x0000FEB8
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00011CE8 File Offset: 0x0000FEE8
		public PenAlignment Alignment
		{
			get
			{
				PenAlignment penAlignment = PenAlignment.Center;
				int num = SafeNativeMethods.Gdip.GdipGetPenMode(new HandleRef(this, this.NativePen), out penAlignment);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return penAlignment;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PenAlignment));
				}
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenMode(new HandleRef(this, this.NativePen), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets a copy of the geometric transformation for this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the geometric transformation for this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Transform" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00011D60 File Offset: 0x0000FF60
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00011D9C File Offset: 0x0000FF9C
		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				int num = SafeNativeMethods.Gdip.GdipGetPenTransform(new HandleRef(this, this.NativePen), new HandleRef(matrix, matrix.nativeMatrix));
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return matrix;
			}
			set
			{
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenTransform(new HandleRef(this, this.NativePen), new HandleRef(value, value.nativeMatrix));
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Resets the geometric transformation matrix for this <see cref="T:System.Drawing.Pen" /> to identity.</summary>
		// Token: 0x060003A2 RID: 930 RVA: 0x00011E08 File Offset: 0x00010008
		public void ResetTransform()
		{
			int num = SafeNativeMethods.Gdip.GdipResetPenTransform(new HandleRef(this, this.NativePen));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Multiplies the transformation matrix for this <see cref="T:System.Drawing.Pen" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> object by which to multiply the transformation matrix.</param>
		// Token: 0x060003A3 RID: 931 RVA: 0x00011E31 File Offset: 0x00010031
		public void MultiplyTransform(Matrix matrix)
		{
			this.MultiplyTransform(matrix, MatrixOrder.Prepend);
		}

		/// <summary>Multiplies the transformation matrix for this <see cref="T:System.Drawing.Pen" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to multiply the transformation matrix.</param>
		/// <param name="order">The order in which to perform the multiplication operation.</param>
		// Token: 0x060003A4 RID: 932 RVA: 0x00011E3C File Offset: 0x0001003C
		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipMultiplyPenTransform(new HandleRef(this, this.NativePen), new HandleRef(matrix, matrix.nativeMatrix), order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Translates the local geometric transformation by the specified dimensions. This method prepends the translation to the transformation.</summary>
		/// <param name="dx">The value of the translation in x.</param>
		/// <param name="dy">The value of the translation in y.</param>
		// Token: 0x060003A5 RID: 933 RVA: 0x00011E72 File Offset: 0x00010072
		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		/// <summary>Translates the local geometric transformation by the specified dimensions in the specified order.</summary>
		/// <param name="dx">The value of the translation in x.</param>
		/// <param name="dy">The value of the translation in y.</param>
		/// <param name="order">The order (prepend or append) in which to apply the translation.</param>
		// Token: 0x060003A6 RID: 934 RVA: 0x00011E80 File Offset: 0x00010080
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipTranslatePenTransform(new HandleRef(this, this.NativePen), dx, dy, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Scales the local geometric transformation by the specified factors. This method prepends the scaling matrix to the transformation.</summary>
		/// <param name="sx">The factor by which to scale the transformation in the x-axis direction.</param>
		/// <param name="sy">The factor by which to scale the transformation in the y-axis direction.</param>
		// Token: 0x060003A7 RID: 935 RVA: 0x00011EAC File Offset: 0x000100AC
		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		/// <summary>Scales the local geometric transformation by the specified factors in the specified order.</summary>
		/// <param name="sx">The factor by which to scale the transformation in the x-axis direction.</param>
		/// <param name="sy">The factor by which to scale the transformation in the y-axis direction.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the scaling matrix.</param>
		// Token: 0x060003A8 RID: 936 RVA: 0x00011EB8 File Offset: 0x000100B8
		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipScalePenTransform(new HandleRef(this, this.NativePen), sx, sy, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Rotates the local geometric transformation by the specified angle. This method prepends the rotation to the transformation.</summary>
		/// <param name="angle">The angle of rotation.</param>
		// Token: 0x060003A9 RID: 937 RVA: 0x00011EE4 File Offset: 0x000100E4
		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		/// <summary>Rotates the local geometric transformation by the specified angle in the specified order.</summary>
		/// <param name="angle">The angle of rotation.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the rotation matrix.</param>
		// Token: 0x060003AA RID: 938 RVA: 0x00011EF0 File Offset: 0x000100F0
		public void RotateTransform(float angle, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipRotatePenTransform(new HandleRef(this, this.NativePen), angle, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00011F1C File Offset: 0x0001011C
		private void InternalSetColor(Color value)
		{
			int num = SafeNativeMethods.Gdip.GdipSetPenColor(new HandleRef(this, this.NativePen), this.color.ToArgb());
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.color = value;
		}

		/// <summary>Gets the style of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.PenType" /> enumeration that specifies the style of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00011F58 File Offset: 0x00010158
		public PenType PenType
		{
			get
			{
				int num = -1;
				int num2 = SafeNativeMethods.Gdip.GdipGetPenFillType(new HandleRef(this, this.NativePen), out num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				return (PenType)num;
			}
		}

		/// <summary>Gets or sets the color of this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the color of this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Color" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00011F88 File Offset: 0x00010188
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00011FDC File Offset: 0x000101DC
		public Color Color
		{
			get
			{
				if (this.color == Color.Empty)
				{
					int num = 0;
					int num2 = SafeNativeMethods.Gdip.GdipGetPenColor(new HandleRef(this, this.NativePen), out num);
					if (num2 != 0)
					{
						throw SafeNativeMethods.Gdip.StatusException(num2);
					}
					this.color = Color.FromArgb(num);
				}
				return this.color;
			}
			set
			{
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				if (value != this.color)
				{
					Color color = this.color;
					this.color = value;
					this.InternalSetColor(value);
					if (value.IsSystemColor && !color.IsSystemColor)
					{
						SystemColorTracker.Add(this);
					}
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Brush" /> that determines attributes of this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> that determines attributes of this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Brush" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0001204C File Offset: 0x0001024C
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x000120C4 File Offset: 0x000102C4
		public Brush Brush
		{
			get
			{
				Brush brush = null;
				switch (this.PenType)
				{
				case PenType.SolidColor:
					brush = new SolidBrush(this.GetNativeBrush());
					break;
				case PenType.HatchFill:
					brush = new HatchBrush(this.GetNativeBrush());
					break;
				case PenType.TextureFill:
					brush = new TextureBrush(this.GetNativeBrush());
					break;
				case PenType.PathGradient:
					brush = new PathGradientBrush(this.GetNativeBrush());
					break;
				case PenType.LinearGradient:
					brush = new LinearGradientBrush(this.GetNativeBrush());
					break;
				}
				return brush;
			}
			set
			{
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenBrushFill(new HandleRef(this, this.NativePen), new HandleRef(value, value.NativeBrush));
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012130 File Offset: 0x00010330
		private IntPtr GetNativeBrush()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipGetPenBrushFill(new HandleRef(this, this.NativePen), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return zero;
		}

		/// <summary>Gets or sets the style used for dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.DashStyle" /> that represents the style used for dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashStyle" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00012164 File Offset: 0x00010364
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00012194 File Offset: 0x00010394
		public DashStyle DashStyle
		{
			get
			{
				int num = 0;
				int num2 = SafeNativeMethods.Gdip.GdipGetPenDashStyle(new HandleRef(this, this.NativePen), out num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				return (DashStyle)num;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 5))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DashStyle));
				}
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenDashStyle(new HandleRef(this, this.NativePen), (int)value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				if (value == DashStyle.Custom)
				{
					this.EnsureValidDashPattern();
				}
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00012214 File Offset: 0x00010414
		private void EnsureValidDashPattern()
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetPenDashCount(new HandleRef(this, this.NativePen), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			if (num == 0)
			{
				this.DashPattern = new float[] { 1f };
			}
		}

		/// <summary>Gets or sets the distance from the start of a line to the beginning of a dash pattern.</summary>
		/// <returns>The distance from the start of a line to the beginning of a dash pattern.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashOffset" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00012258 File Offset: 0x00010458
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0001228C File Offset: 0x0001048C
		public float DashOffset
		{
			get
			{
				float[] array = new float[1];
				int num = SafeNativeMethods.Gdip.GdipGetPenDashOffset(new HandleRef(this, this.NativePen), array);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return array[0];
			}
			set
			{
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenDashOffset(new HandleRef(this, this.NativePen), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets an array of custom dashes and spaces.</summary>
		/// <returns>An array of real numbers that specifies the lengths of alternating dashes and spaces in dashed lines.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashPattern" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x000122DC File Offset: 0x000104DC
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00012360 File Offset: 0x00010560
		public float[] DashPattern
		{
			get
			{
				int num = 0;
				int num2 = SafeNativeMethods.Gdip.GdipGetPenDashCount(new HandleRef(this, this.NativePen), out num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				int num3 = num;
				IntPtr intPtr = Marshal.AllocHGlobal(checked(4 * num3));
				num2 = SafeNativeMethods.Gdip.GdipGetPenDashArray(new HandleRef(this, this.NativePen), intPtr, num3);
				float[] array;
				try
				{
					if (num2 != 0)
					{
						throw SafeNativeMethods.Gdip.StatusException(num2);
					}
					array = new float[num3];
					Marshal.Copy(intPtr, array, 0, num3);
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return array;
			}
			set
			{
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidDashPattern"));
				}
				int num = value.Length;
				IntPtr intPtr = Marshal.AllocHGlobal(checked(4 * num));
				try
				{
					Marshal.Copy(value, 0, intPtr, num);
					int num2 = SafeNativeMethods.Gdip.GdipSetPenDashArray(new HandleRef(this, this.NativePen), new HandleRef(intPtr, intPtr), num);
					if (num2 != 0)
					{
						throw SafeNativeMethods.Gdip.StatusException(num2);
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		/// <summary>Gets or sets an array of values that specifies a compound pen. A compound pen draws a compound line made up of parallel lines and spaces.</summary>
		/// <returns>An array of real numbers that specifies the compound array. The elements in the array must be in increasing order, not less than 0, and not greater than 1.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.CompoundArray" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00012404 File Offset: 0x00010604
		// (set) Token: 0x060003BA RID: 954 RVA: 0x00012458 File Offset: 0x00010658
		public float[] CompoundArray
		{
			get
			{
				int num = 0;
				int num2 = SafeNativeMethods.Gdip.GdipGetPenCompoundCount(new HandleRef(this, this.NativePen), out num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				float[] array = new float[num];
				num2 = SafeNativeMethods.Gdip.GdipGetPenCompoundArray(new HandleRef(this, this.NativePen), array, num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				return array;
			}
			set
			{
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[] { "Pen" }));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPenCompoundArray(new HandleRef(this, this.NativePen), value, value.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000124AB File Offset: 0x000106AB
		void ISystemColorTracker.OnSystemColorChanged()
		{
			if (this.NativePen != IntPtr.Zero)
			{
				this.InternalSetColor(this.color);
			}
		}

		// Token: 0x0400026C RID: 620
		private IntPtr nativePen;

		// Token: 0x0400026D RID: 621
		private Color color;

		// Token: 0x0400026E RID: 622
		private bool immutable;
	}
}

using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Represents an ordered pair of integer x- and y-coordinates that defines a point in a two-dimensional plane.</summary>
	// Token: 0x0200002A RID: 42
	[TypeConverter(typeof(PointConverter))]
	[ComVisible(true)]
	[Serializable]
	public struct Point
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class with the specified coordinates.</summary>
		/// <param name="x">The horizontal position of the point.</param>
		/// <param name="y">The vertical position of the point.</param>
		// Token: 0x0600044B RID: 1099 RVA: 0x00014FCF File Offset: 0x000131CF
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class from a <see cref="T:System.Drawing.Size" />.</summary>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the coordinates for the new <see cref="T:System.Drawing.Point" />.</param>
		// Token: 0x0600044C RID: 1100 RVA: 0x00014FDF File Offset: 0x000131DF
		public Point(Size sz)
		{
			this.x = sz.Width;
			this.y = sz.Height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class using coordinates specified by an integer value.</summary>
		/// <param name="dw">A 32-bit integer that specifies the coordinates for the new <see cref="T:System.Drawing.Point" />.</param>
		// Token: 0x0600044D RID: 1101 RVA: 0x00014FFB File Offset: 0x000131FB
		public Point(int dw)
		{
			this.x = (int)((short)Point.LOWORD(dw));
			this.y = (int)((short)Point.HIWORD(dw));
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Point" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if both <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> are 0; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00015017 File Offset: 0x00013217
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.x == 0 && this.y == 0;
			}
		}

		/// <summary>Gets or sets the x-coordinate of this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The x-coordinate of this <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0001502C File Offset: 0x0001322C
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x00015034 File Offset: 0x00013234
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The y-coordinate of this <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001503D File Offset: 0x0001323D
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00015045 File Offset: 0x00013245
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Point" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> to be converted.</param>
		/// <returns>The <see cref="T:System.Drawing.PointF" /> that results from the conversion.</returns>
		// Token: 0x06000453 RID: 1107 RVA: 0x0001504E File Offset: 0x0001324E
		public static implicit operator PointF(Point p)
		{
			return new PointF((float)p.X, (float)p.Y);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Point" /> structure to a <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> to be converted.</param>
		/// <returns>The <see cref="T:System.Drawing.Size" /> that results from the conversion.</returns>
		// Token: 0x06000454 RID: 1108 RVA: 0x00015065 File Offset: 0x00013265
		public static explicit operator Size(Point p)
		{
			return new Size(p.X, p.Y);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.Point" /> by a given <see cref="T:System.Drawing.Size" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to translate.</param>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to add to the coordinates of <paramref name="pt" />.</param>
		/// <returns>The translated <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x06000455 RID: 1109 RVA: 0x0001507A File Offset: 0x0001327A
		public static Point operator +(Point pt, Size sz)
		{
			return Point.Add(pt, sz);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.Point" /> by the negative of a given <see cref="T:System.Drawing.Size" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to translate.</param>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to subtract from the coordinates of <paramref name="pt" />.</param>
		/// <returns>A <see cref="T:System.Drawing.Point" /> structure that is translated by the negative of a given <see cref="T:System.Drawing.Size" /> structure.</returns>
		// Token: 0x06000456 RID: 1110 RVA: 0x00015083 File Offset: 0x00013283
		public static Point operator -(Point pt, Size sz)
		{
			return Point.Subtract(pt, sz);
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Point" /> objects. The result specifies whether the values of the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> properties of the two <see cref="T:System.Drawing.Point" /> objects are equal.</summary>
		/// <param name="left">A <see cref="T:System.Drawing.Point" /> to compare.</param>
		/// <param name="right">A <see cref="T:System.Drawing.Point" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values of <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000457 RID: 1111 RVA: 0x0001508C File Offset: 0x0001328C
		public static bool operator ==(Point left, Point right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Point" /> objects. The result specifies whether the values of the <see cref="P:System.Drawing.Point.X" /> or <see cref="P:System.Drawing.Point.Y" /> properties of the two <see cref="T:System.Drawing.Point" /> objects are unequal.</summary>
		/// <param name="left">A <see cref="T:System.Drawing.Point" /> to compare.</param>
		/// <param name="right">A <see cref="T:System.Drawing.Point" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values of either the <see cref="P:System.Drawing.Point.X" /> properties or the <see cref="P:System.Drawing.Point.Y" /> properties of <paramref name="left" /> and <paramref name="right" /> differ; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000458 RID: 1112 RVA: 0x000150B0 File Offset: 0x000132B0
		public static bool operator !=(Point left, Point right)
		{
			return !(left == right);
		}

		/// <summary>Adds the specified <see cref="T:System.Drawing.Size" /> to the specified <see cref="T:System.Drawing.Point" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to add.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> to add</param>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that is the result of the addition operation.</returns>
		// Token: 0x06000459 RID: 1113 RVA: 0x000150BC File Offset: 0x000132BC
		public static Point Add(Point pt, Size sz)
		{
			return new Point(pt.X + sz.Width, pt.Y + sz.Height);
		}

		/// <summary>Returns the result of subtracting specified <see cref="T:System.Drawing.Size" /> from the specified <see cref="T:System.Drawing.Point" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to be subtracted from.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> to subtract from the <see cref="T:System.Drawing.Point" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that is the result of the subtraction operation.</returns>
		// Token: 0x0600045A RID: 1114 RVA: 0x000150E1 File Offset: 0x000132E1
		public static Point Subtract(Point pt, Size sz)
		{
			return new Point(pt.X - sz.Width, pt.Y - sz.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> by rounding the values of the <see cref="T:System.Drawing.PointF" /> to the next higher integer values.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
		// Token: 0x0600045B RID: 1115 RVA: 0x00015106 File Offset: 0x00013306
		public static Point Ceiling(PointF value)
		{
			return new Point((int)Math.Ceiling((double)value.X), (int)Math.Ceiling((double)value.Y));
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> by truncating the values of the <see cref="T:System.Drawing.Point" />.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
		// Token: 0x0600045C RID: 1116 RVA: 0x00015129 File Offset: 0x00013329
		public static Point Truncate(PointF value)
		{
			return new Point((int)value.X, (int)value.Y);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> object by rounding the <see cref="T:System.Drawing.Point" /> values to the nearest integer.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
		// Token: 0x0600045D RID: 1117 RVA: 0x00015140 File Offset: 0x00013340
		public static Point Round(PointF value)
		{
			return new Point((int)Math.Round((double)value.X), (int)Math.Round((double)value.Y));
		}

		/// <summary>Specifies whether this <see cref="T:System.Drawing.Point" /> contains the same coordinates as the specified <see cref="T:System.Object" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Point" /> and has the same coordinates as this <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x0600045E RID: 1118 RVA: 0x00015164 File Offset: 0x00013364
		public override bool Equals(object obj)
		{
			if (!(obj is Point))
			{
				return false;
			}
			Point point = (Point)obj;
			return point.X == this.X && point.Y == this.Y;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x0600045F RID: 1119 RVA: 0x000151A2 File Offset: 0x000133A2
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		/// <summary>Translates this <see cref="T:System.Drawing.Point" /> by the specified amount.</summary>
		/// <param name="dx">The amount to offset the x-coordinate.</param>
		/// <param name="dy">The amount to offset the y-coordinate.</param>
		// Token: 0x06000460 RID: 1120 RVA: 0x000151B1 File Offset: 0x000133B1
		public void Offset(int dx, int dy)
		{
			this.X += dx;
			this.Y += dy;
		}

		/// <summary>Translates this <see cref="T:System.Drawing.Point" /> by the specified <see cref="T:System.Drawing.Point" />.</summary>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> used offset this <see cref="T:System.Drawing.Point" />.</param>
		// Token: 0x06000461 RID: 1121 RVA: 0x000151CF File Offset: 0x000133CF
		public void Offset(Point p)
		{
			this.Offset(p.X, p.Y);
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Point" /> to a human-readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x06000462 RID: 1122 RVA: 0x000151E8 File Offset: 0x000133E8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X=",
				this.X.ToString(CultureInfo.CurrentCulture),
				",Y=",
				this.Y.ToString(CultureInfo.CurrentCulture),
				"}"
			});
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00015244 File Offset: 0x00013444
		private static int HIWORD(int n)
		{
			return (n >> 16) & 65535;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00015250 File Offset: 0x00013450
		private static int LOWORD(int n)
		{
			return n & 65535;
		}

		/// <summary>Represents a <see cref="T:System.Drawing.Point" /> that has <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values set to zero.</summary>
		// Token: 0x040002FC RID: 764
		public static readonly Point Empty;

		// Token: 0x040002FD RID: 765
		private int x;

		// Token: 0x040002FE RID: 766
		private int y;
	}
}

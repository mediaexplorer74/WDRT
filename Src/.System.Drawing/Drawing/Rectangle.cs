using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Stores a set of four integers that represent the location and size of a rectangle</summary>
	// Token: 0x0200002C RID: 44
	[TypeConverter(typeof(RectangleConverter))]
	[ComVisible(true)]
	[Serializable]
	public struct Rectangle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle.</param>
		/// <param name="width">The width of the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		// Token: 0x0600046F RID: 1135 RVA: 0x0001553E File Offset: 0x0001373E
		public Rectangle(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
		/// <param name="location">A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the rectangular region.</param>
		/// <param name="size">A <see cref="T:System.Drawing.Size" /> that represents the width and height of the rectangular region.</param>
		// Token: 0x06000470 RID: 1136 RVA: 0x0001555D File Offset: 0x0001375D
		public Rectangle(Point location, Size size)
		{
			this.x = location.X;
			this.y = location.Y;
			this.width = size.Width;
			this.height = size.Height;
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Rectangle" /> structure with the specified edge locations.</summary>
		/// <param name="left">The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
		/// <param name="top">The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
		/// <param name="right">The x-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
		/// <returns>The new <see cref="T:System.Drawing.Rectangle" /> that this method creates.</returns>
		// Token: 0x06000471 RID: 1137 RVA: 0x00015593 File Offset: 0x00013793
		public static Rectangle FromLTRB(int left, int top, int right, int bottom)
		{
			return new Rectangle(left, top, right - left, bottom - top);
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x000155A2 File Offset: 0x000137A2
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x000155B5 File Offset: 0x000137B5
		[Browsable(false)]
		public Point Location
		{
			get
			{
				return new Point(this.X, this.Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}

		/// <summary>Gets or sets the size of this <see cref="T:System.Drawing.Rectangle" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the width and height of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x000155D1 File Offset: 0x000137D1
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x000155E4 File Offset: 0x000137E4
		[Browsable(false)]
		public Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
			set
			{
				this.Width = value.Width;
				this.Height = value.Height;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00015600 File Offset: 0x00013800
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x00015608 File Offset: 0x00013808
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

		/// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00015611 File Offset: 0x00013811
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00015619 File Offset: 0x00013819
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

		/// <summary>Gets or sets the width of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00015622 File Offset: 0x00013822
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x0001562A File Offset: 0x0001382A
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		/// <summary>Gets or sets the height of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The height of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00015633 File Offset: 0x00013833
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0001563B File Offset: 0x0001383B
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>Gets the x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00015644 File Offset: 0x00013844
		[Browsable(false)]
		public int Left
		{
			get
			{
				return this.X;
			}
		}

		/// <summary>Gets the y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001564C File Offset: 0x0001384C
		[Browsable(false)]
		public int Top
		{
			get
			{
				return this.Y;
			}
		}

		/// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00015654 File Offset: 0x00013854
		[Browsable(false)]
		public int Right
		{
			get
			{
				return this.X + this.Width;
			}
		}

		/// <summary>Gets the y-coordinate that is the sum of the <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00015663 File Offset: 0x00013863
		[Browsable(false)]
		public int Bottom
		{
			get
			{
				return this.Y + this.Height;
			}
		}

		/// <summary>Tests whether all numeric properties of this <see cref="T:System.Drawing.Rectangle" /> have values of zero.</summary>
		/// <returns>This property returns <see langword="true" /> if the <see cref="P:System.Drawing.Rectangle.Width" />, <see cref="P:System.Drawing.Rectangle.Height" />, <see cref="P:System.Drawing.Rectangle.X" />, and <see cref="P:System.Drawing.Rectangle.Y" /> properties of this <see cref="T:System.Drawing.Rectangle" /> all have values of zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00015672 File Offset: 0x00013872
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.height == 0 && this.width == 0 && this.x == 0 && this.y == 0;
			}
		}

		/// <summary>Tests whether <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure with the same location and size of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure and its <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties are equal to the corresponding properties of this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000483 RID: 1155 RVA: 0x00015698 File Offset: 0x00013898
		public override bool Equals(object obj)
		{
			if (!(obj is Rectangle))
			{
				return false;
			}
			Rectangle rectangle = (Rectangle)obj;
			return rectangle.X == this.X && rectangle.Y == this.Y && rectangle.Width == this.Width && rectangle.Height == this.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures have equal location and size.</summary>
		/// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the equality operator.</param>
		/// <returns>This operator returns <see langword="true" /> if the two <see cref="T:System.Drawing.Rectangle" /> structures have equal <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties.</returns>
		// Token: 0x06000484 RID: 1156 RVA: 0x000156F4 File Offset: 0x000138F4
		public static bool operator ==(Rectangle left, Rectangle right)
		{
			return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures differ in location or size.</summary>
		/// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the inequality operator.</param>
		/// <returns>This operator returns <see langword="true" /> if any of the <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" /> or <see cref="P:System.Drawing.Rectangle.Height" /> properties of the two <see cref="T:System.Drawing.Rectangle" /> structures are unequal; otherwise <see langword="false" />.</returns>
		// Token: 0x06000485 RID: 1157 RVA: 0x00015743 File Offset: 0x00013943
		public static bool operator !=(Rectangle left, Rectangle right)
		{
			return !(left == right);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> structure to a <see cref="T:System.Drawing.Rectangle" /> structure by rounding the <see cref="T:System.Drawing.RectangleF" /> values to the next higher integer values.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> structure to be converted.</param>
		/// <returns>Returns a <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x06000486 RID: 1158 RVA: 0x0001574F File Offset: 0x0001394F
		public static Rectangle Ceiling(RectangleF value)
		{
			return new Rectangle((int)Math.Ceiling((double)value.X), (int)Math.Ceiling((double)value.Y), (int)Math.Ceiling((double)value.Width), (int)Math.Ceiling((double)value.Height));
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> to a <see cref="T:System.Drawing.Rectangle" /> by truncating the <see cref="T:System.Drawing.RectangleF" /> values.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> to be converted.</param>
		/// <returns>The truncated value of the  <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x06000487 RID: 1159 RVA: 0x0001578E File Offset: 0x0001398E
		public static Rectangle Truncate(RectangleF value)
		{
			return new Rectangle((int)value.X, (int)value.Y, (int)value.Width, (int)value.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> to a <see cref="T:System.Drawing.Rectangle" /> by rounding the <see cref="T:System.Drawing.RectangleF" /> values to the nearest integer values.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> to be converted.</param>
		/// <returns>The rounded interger value of the <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x06000488 RID: 1160 RVA: 0x000157B5 File Offset: 0x000139B5
		public static Rectangle Round(RectangleF value)
		{
			return new Rectangle((int)Math.Round((double)value.X), (int)Math.Round((double)value.Y), (int)Math.Round((double)value.Width), (int)Math.Round((double)value.Height));
		}

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <returns>This method returns <see langword="true" /> if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
		// Token: 0x06000489 RID: 1161 RVA: 0x000157F4 File Offset: 0x000139F4
		public bool Contains(int x, int y)
		{
			return this.X <= x && x < this.X + this.Width && this.Y <= y && y < this.Y + this.Height;
		}

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the point represented by <paramref name="pt" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
		// Token: 0x0600048A RID: 1162 RVA: 0x0001582A File Offset: 0x00013A2A
		public bool Contains(Point pt)
		{
			return this.Contains(pt.X, pt.Y);
		}

		/// <summary>Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
		// Token: 0x0600048B RID: 1163 RVA: 0x00015840 File Offset: 0x00013A40
		public bool Contains(Rectangle rect)
		{
			return this.X <= rect.X && rect.X + rect.Width <= this.X + this.Width && this.Y <= rect.Y && rect.Y + rect.Height <= this.Y + this.Height;
		}

		/// <summary>Returns the hash code for this <see cref="T:System.Drawing.Rectangle" /> structure. For information about the use of hash codes, see <see cref="M:System.Object.GetHashCode" /> .</summary>
		/// <returns>An integer that represents the hash code for this rectangle.</returns>
		// Token: 0x0600048C RID: 1164 RVA: 0x000158AC File Offset: 0x00013AAC
		public override int GetHashCode()
		{
			return this.X ^ ((this.Y << 13) | (int)((uint)this.Y >> 19)) ^ ((this.Width << 26) | (int)((uint)this.Width >> 6)) ^ ((this.Height << 7) | (int)((uint)this.Height >> 25));
		}

		/// <summary>Enlarges this <see cref="T:System.Drawing.Rectangle" /> by the specified amount.</summary>
		/// <param name="width">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally.</param>
		/// <param name="height">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically.</param>
		// Token: 0x0600048D RID: 1165 RVA: 0x000158F9 File Offset: 0x00013AF9
		public void Inflate(int width, int height)
		{
			this.X -= width;
			this.Y -= height;
			this.Width += 2 * width;
			this.Height += 2 * height;
		}

		/// <summary>Enlarges this <see cref="T:System.Drawing.Rectangle" /> by the specified amount.</summary>
		/// <param name="size">The amount to inflate this rectangle.</param>
		// Token: 0x0600048E RID: 1166 RVA: 0x00015937 File Offset: 0x00013B37
		public void Inflate(Size size)
		{
			this.Inflate(size.Width, size.Height);
		}

		/// <summary>Creates and returns an enlarged copy of the specified <see cref="T:System.Drawing.Rectangle" /> structure. The copy is enlarged by the specified amount. The original <see cref="T:System.Drawing.Rectangle" /> structure remains unmodified.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to start. This rectangle is not modified.</param>
		/// <param name="x">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally.</param>
		/// <param name="y">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically.</param>
		/// <returns>The enlarged <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x0600048F RID: 1167 RVA: 0x00015950 File Offset: 0x00013B50
		public static Rectangle Inflate(Rectangle rect, int x, int y)
		{
			Rectangle rectangle = rect;
			rectangle.Inflate(x, y);
			return rectangle;
		}

		/// <summary>Replaces this <see cref="T:System.Drawing.Rectangle" /> with the intersection of itself and the specified <see cref="T:System.Drawing.Rectangle" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to intersect.</param>
		// Token: 0x06000490 RID: 1168 RVA: 0x0001596C File Offset: 0x00013B6C
		public void Intersect(Rectangle rect)
		{
			Rectangle rectangle = Rectangle.Intersect(rect, this);
			this.X = rectangle.X;
			this.Y = rectangle.Y;
			this.Width = rectangle.Width;
			this.Height = rectangle.Height;
		}

		/// <summary>Returns a third <see cref="T:System.Drawing.Rectangle" /> structure that represents the intersection of two other <see cref="T:System.Drawing.Rectangle" /> structures. If there is no intersection, an empty <see cref="T:System.Drawing.Rectangle" /> is returned.</summary>
		/// <param name="a">A rectangle to intersect.</param>
		/// <param name="b">A rectangle to intersect.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the intersection of <paramref name="a" /> and <paramref name="b" />.</returns>
		// Token: 0x06000491 RID: 1169 RVA: 0x000159BC File Offset: 0x00013BBC
		public static Rectangle Intersect(Rectangle a, Rectangle b)
		{
			int num = Math.Max(a.X, b.X);
			int num2 = Math.Min(a.X + a.Width, b.X + b.Width);
			int num3 = Math.Max(a.Y, b.Y);
			int num4 = Math.Min(a.Y + a.Height, b.Y + b.Height);
			if (num2 >= num && num4 >= num3)
			{
				return new Rectangle(num, num3, num2 - num, num4 - num3);
			}
			return Rectangle.Empty;
		}

		/// <summary>Determines if this rectangle intersects with <paramref name="rect" />.</summary>
		/// <param name="rect">The rectangle to test.</param>
		/// <returns>This method returns <see langword="true" /> if there is any intersection, otherwise <see langword="false" />.</returns>
		// Token: 0x06000492 RID: 1170 RVA: 0x00015A54 File Offset: 0x00013C54
		public bool IntersectsWith(Rectangle rect)
		{
			return rect.X < this.X + this.Width && this.X < rect.X + rect.Width && rect.Y < this.Y + this.Height && this.Y < rect.Y + rect.Height;
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> structure that contains the union of two <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
		/// <param name="a">A rectangle to union.</param>
		/// <param name="b">A rectangle to union.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> structure that bounds the union of the two <see cref="T:System.Drawing.Rectangle" /> structures.</returns>
		// Token: 0x06000493 RID: 1171 RVA: 0x00015AC0 File Offset: 0x00013CC0
		public static Rectangle Union(Rectangle a, Rectangle b)
		{
			int num = Math.Min(a.X, b.X);
			int num2 = Math.Max(a.X + a.Width, b.X + b.Width);
			int num3 = Math.Min(a.Y, b.Y);
			int num4 = Math.Max(a.Y + a.Height, b.Y + b.Height);
			return new Rectangle(num, num3, num2 - num, num4 - num3);
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="pos">Amount to offset the location.</param>
		// Token: 0x06000494 RID: 1172 RVA: 0x00015B4A File Offset: 0x00013D4A
		public void Offset(Point pos)
		{
			this.Offset(pos.X, pos.Y);
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="x">The horizontal offset.</param>
		/// <param name="y">The vertical offset.</param>
		// Token: 0x06000495 RID: 1173 RVA: 0x00015B60 File Offset: 0x00013D60
		public void Offset(int x, int y)
		{
			this.X += x;
			this.Y += y;
		}

		/// <summary>Converts the attributes of this <see cref="T:System.Drawing.Rectangle" /> to a human-readable string.</summary>
		/// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.Rectangle" /> structure ¾ for example, {X=20, Y=20, Width=100, Height=50}</returns>
		// Token: 0x06000496 RID: 1174 RVA: 0x00015B80 File Offset: 0x00013D80
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X=",
				this.X.ToString(CultureInfo.CurrentCulture),
				",Y=",
				this.Y.ToString(CultureInfo.CurrentCulture),
				",Width=",
				this.Width.ToString(CultureInfo.CurrentCulture),
				",Height=",
				this.Height.ToString(CultureInfo.CurrentCulture),
				"}"
			});
		}

		/// <summary>Represents a <see cref="T:System.Drawing.Rectangle" /> structure with its properties left uninitialized.</summary>
		// Token: 0x040002FF RID: 767
		public static readonly Rectangle Empty;

		// Token: 0x04000300 RID: 768
		private int x;

		// Token: 0x04000301 RID: 769
		private int y;

		// Token: 0x04000302 RID: 770
		private int width;

		// Token: 0x04000303 RID: 771
		private int height;
	}
}

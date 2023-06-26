using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents padding or margin information associated with a user interface (UI) element.</summary>
	// Token: 0x02000315 RID: 789
	[TypeConverter(typeof(PaddingConverter))]
	[Serializable]
	public struct Padding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding" /> class using the supplied padding size for all edges.</summary>
		/// <param name="all">The number of pixels to be used for padding for all edges.</param>
		// Token: 0x0600322F RID: 12847 RVA: 0x000E1AA0 File Offset: 0x000DFCA0
		public Padding(int all)
		{
			this._all = true;
			this._bottom = all;
			this._right = all;
			this._left = all;
			this._top = all;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding" /> class using a separate padding size for each edge.</summary>
		/// <param name="left">The padding size, in pixels, for the left edge.</param>
		/// <param name="top">The padding size, in pixels, for the top edge.</param>
		/// <param name="right">The padding size, in pixels, for the right edge.</param>
		/// <param name="bottom">The padding size, in pixels, for the bottom edge.</param>
		// Token: 0x06003230 RID: 12848 RVA: 0x000E1AD8 File Offset: 0x000DFCD8
		public Padding(int left, int top, int right, int bottom)
		{
			this._top = top;
			this._left = left;
			this._right = right;
			this._bottom = bottom;
			this._all = this._top == this._left && this._top == this._right && this._top == this._bottom;
		}

		/// <summary>Gets or sets the padding value for all the edges.</summary>
		/// <returns>The padding, in pixels, for all edges if the same; otherwise, -1.</returns>
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000E1B35 File Offset: 0x000DFD35
		// (set) Token: 0x06003232 RID: 12850 RVA: 0x000E1B48 File Offset: 0x000DFD48
		[RefreshProperties(RefreshProperties.All)]
		public int All
		{
			get
			{
				if (!this._all)
				{
					return -1;
				}
				return this._top;
			}
			set
			{
				if (!this._all || this._top != value)
				{
					this._all = true;
					this._bottom = value;
					this._right = value;
					this._left = value;
					this._top = value;
				}
			}
		}

		/// <summary>Gets or sets the padding value for the bottom edge.</summary>
		/// <returns>The padding, in pixels, for the bottom edge.</returns>
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000E1B8F File Offset: 0x000DFD8F
		// (set) Token: 0x06003234 RID: 12852 RVA: 0x000E1BA6 File Offset: 0x000DFDA6
		[RefreshProperties(RefreshProperties.All)]
		public int Bottom
		{
			get
			{
				if (this._all)
				{
					return this._top;
				}
				return this._bottom;
			}
			set
			{
				if (this._all || this._bottom != value)
				{
					this._all = false;
					this._bottom = value;
				}
			}
		}

		/// <summary>Gets or sets the padding value for the left edge.</summary>
		/// <returns>The padding, in pixels, for the left edge.</returns>
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x000E1BC7 File Offset: 0x000DFDC7
		// (set) Token: 0x06003236 RID: 12854 RVA: 0x000E1BDE File Offset: 0x000DFDDE
		[RefreshProperties(RefreshProperties.All)]
		public int Left
		{
			get
			{
				if (this._all)
				{
					return this._top;
				}
				return this._left;
			}
			set
			{
				if (this._all || this._left != value)
				{
					this._all = false;
					this._left = value;
				}
			}
		}

		/// <summary>Gets or sets the padding value for the right edge.</summary>
		/// <returns>The padding, in pixels, for the right edge.</returns>
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x000E1BFF File Offset: 0x000DFDFF
		// (set) Token: 0x06003238 RID: 12856 RVA: 0x000E1C16 File Offset: 0x000DFE16
		[RefreshProperties(RefreshProperties.All)]
		public int Right
		{
			get
			{
				if (this._all)
				{
					return this._top;
				}
				return this._right;
			}
			set
			{
				if (this._all || this._right != value)
				{
					this._all = false;
					this._right = value;
				}
			}
		}

		/// <summary>Gets or sets the padding value for the top edge.</summary>
		/// <returns>The padding, in pixels, for the top edge.</returns>
		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000E1C37 File Offset: 0x000DFE37
		// (set) Token: 0x0600323A RID: 12858 RVA: 0x000E1C3F File Offset: 0x000DFE3F
		[RefreshProperties(RefreshProperties.All)]
		public int Top
		{
			get
			{
				return this._top;
			}
			set
			{
				if (this._all || this._top != value)
				{
					this._all = false;
					this._top = value;
				}
			}
		}

		/// <summary>Gets the combined padding for the right and left edges.</summary>
		/// <returns>Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Left" /> and <see cref="P:System.Windows.Forms.Padding.Right" /> padding values.</returns>
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000E1C60 File Offset: 0x000DFE60
		[Browsable(false)]
		public int Horizontal
		{
			get
			{
				return this.Left + this.Right;
			}
		}

		/// <summary>Gets the combined padding for the top and bottom edges.</summary>
		/// <returns>Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Top" /> and <see cref="P:System.Windows.Forms.Padding.Bottom" /> padding values.</returns>
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x000E1C6F File Offset: 0x000DFE6F
		[Browsable(false)]
		public int Vertical
		{
			get
			{
				return this.Top + this.Bottom;
			}
		}

		/// <summary>Gets the padding information in the form of a <see cref="T:System.Drawing.Size" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> containing the padding information.</returns>
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000E1C7E File Offset: 0x000DFE7E
		[Browsable(false)]
		public Size Size
		{
			get
			{
				return new Size(this.Horizontal, this.Vertical);
			}
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Windows.Forms.Padding" /> values.</summary>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" />.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" />.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that contains the sum of the two specified <see cref="T:System.Windows.Forms.Padding" /> values.</returns>
		// Token: 0x0600323E RID: 12862 RVA: 0x000E1C91 File Offset: 0x000DFE91
		public static Padding Add(Padding p1, Padding p2)
		{
			return p1 + p2;
		}

		/// <summary>Subtracts one specified <see cref="T:System.Windows.Forms.Padding" /> value from another.</summary>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" />.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" />.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that contains the result of the subtraction of one specified <see cref="T:System.Windows.Forms.Padding" /> value from another.</returns>
		// Token: 0x0600323F RID: 12863 RVA: 0x000E1C9A File Offset: 0x000DFE9A
		public static Padding Subtract(Padding p1, Padding p2)
		{
			return p1 - p2;
		}

		/// <summary>Determines whether the value of the specified object is equivalent to the current <see cref="T:System.Windows.Forms.Padding" />.</summary>
		/// <param name="other">The object to compare to the current <see cref="T:System.Windows.Forms.Padding" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.Padding" /> objects are equivalent; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003240 RID: 12864 RVA: 0x000E1CA3 File Offset: 0x000DFEA3
		public override bool Equals(object other)
		{
			return other is Padding && (Padding)other == this;
		}

		/// <summary>Performs vector addition on the two specified <see cref="T:System.Windows.Forms.Padding" /> objects, resulting in a new <see cref="T:System.Windows.Forms.Padding" />.</summary>
		/// <param name="p1">The first <see cref="T:System.Windows.Forms.Padding" /> to add.</param>
		/// <param name="p2">The second <see cref="T:System.Windows.Forms.Padding" /> to add.</param>
		/// <returns>A new <see cref="T:System.Windows.Forms.Padding" /> that results from adding <paramref name="p1" /> and <paramref name="p2" />.</returns>
		// Token: 0x06003241 RID: 12865 RVA: 0x000E1CC0 File Offset: 0x000DFEC0
		public static Padding operator +(Padding p1, Padding p2)
		{
			return new Padding(p1.Left + p2.Left, p1.Top + p2.Top, p1.Right + p2.Right, p1.Bottom + p2.Bottom);
		}

		/// <summary>Performs vector subtraction on the two specified <see cref="T:System.Windows.Forms.Padding" /> objects, resulting in a new <see cref="T:System.Windows.Forms.Padding" />.</summary>
		/// <param name="p1">The <see cref="T:System.Windows.Forms.Padding" /> to subtract from (the minuend).</param>
		/// <param name="p2">The <see cref="T:System.Windows.Forms.Padding" /> to subtract from (the subtrahend).</param>
		/// <returns>The <see cref="T:System.Windows.Forms.Padding" /> result of subtracting <paramref name="p2" /> from <paramref name="p1" />.</returns>
		// Token: 0x06003242 RID: 12866 RVA: 0x000E1D10 File Offset: 0x000DFF10
		public static Padding operator -(Padding p1, Padding p2)
		{
			return new Padding(p1.Left - p2.Left, p1.Top - p2.Top, p1.Right - p2.Right, p1.Bottom - p2.Bottom);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Windows.Forms.Padding" /> objects are equivalent.</summary>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Windows.Forms.Padding" /> objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003243 RID: 12867 RVA: 0x000E1D60 File Offset: 0x000DFF60
		public static bool operator ==(Padding p1, Padding p2)
		{
			return p1.Left == p2.Left && p1.Top == p2.Top && p1.Right == p2.Right && p1.Bottom == p2.Bottom;
		}

		/// <summary>Tests whether two specified <see cref="T:System.Windows.Forms.Padding" /> objects are not equivalent.</summary>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Windows.Forms.Padding" /> objects are different; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003244 RID: 12868 RVA: 0x000E1DAF File Offset: 0x000DFFAF
		public static bool operator !=(Padding p1, Padding p2)
		{
			return !(p1 == p2);
		}

		/// <summary>Generates a hash code for the current <see cref="T:System.Windows.Forms.Padding" />.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003245 RID: 12869 RVA: 0x000E1DBB File Offset: 0x000DFFBB
		public override int GetHashCode()
		{
			return this.Left ^ WindowsFormsUtils.RotateLeft(this.Top, 8) ^ WindowsFormsUtils.RotateLeft(this.Right, 16) ^ WindowsFormsUtils.RotateLeft(this.Bottom, 24);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.Padding" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Padding" />.</returns>
		// Token: 0x06003246 RID: 12870 RVA: 0x000E1DEC File Offset: 0x000DFFEC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{Left=",
				this.Left.ToString(CultureInfo.CurrentCulture),
				",Top=",
				this.Top.ToString(CultureInfo.CurrentCulture),
				",Right=",
				this.Right.ToString(CultureInfo.CurrentCulture),
				",Bottom=",
				this.Bottom.ToString(CultureInfo.CurrentCulture),
				"}"
			});
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000E1E85 File Offset: 0x000E0085
		private void ResetAll()
		{
			this.All = 0;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000E1E8E File Offset: 0x000E008E
		private void ResetBottom()
		{
			this.Bottom = 0;
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000E1E97 File Offset: 0x000E0097
		private void ResetLeft()
		{
			this.Left = 0;
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000E1EA0 File Offset: 0x000E00A0
		private void ResetRight()
		{
			this.Right = 0;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000E1EA9 File Offset: 0x000E00A9
		private void ResetTop()
		{
			this.Top = 0;
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000E1EB4 File Offset: 0x000E00B4
		internal void Scale(float dx, float dy)
		{
			this._top = (int)((float)this._top * dy);
			this._left = (int)((float)this._left * dx);
			this._right = (int)((float)this._right * dx);
			this._bottom = (int)((float)this._bottom * dy);
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000E1F01 File Offset: 0x000E0101
		internal bool ShouldSerializeAll()
		{
			return this._all;
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000E1F09 File Offset: 0x000E0109
		[Conditional("DEBUG")]
		private void Debug_SanityCheck()
		{
			bool all = this._all;
		}

		// Token: 0x04001E63 RID: 7779
		private bool _all;

		// Token: 0x04001E64 RID: 7780
		private int _top;

		// Token: 0x04001E65 RID: 7781
		private int _left;

		// Token: 0x04001E66 RID: 7782
		private int _right;

		// Token: 0x04001E67 RID: 7783
		private int _bottom;

		/// <summary>Provides a <see cref="T:System.Windows.Forms.Padding" /> object with no padding.</summary>
		// Token: 0x04001E68 RID: 7784
		public static readonly Padding Empty = new Padding(0);
	}
}

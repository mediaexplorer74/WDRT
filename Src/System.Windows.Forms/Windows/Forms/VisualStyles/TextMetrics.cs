using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Provides basic information about the font specified by a visual style for a particular element.</summary>
	// Token: 0x0200047E RID: 1150
	public struct TextMetrics
	{
		/// <summary>Gets or sets the height of characters in the font.</summary>
		/// <returns>The height of characters in the font.</returns>
		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06004DAB RID: 19883 RVA: 0x0014154F File Offset: 0x0013F74F
		// (set) Token: 0x06004DAC RID: 19884 RVA: 0x00141557 File Offset: 0x0013F757
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

		/// <summary>Gets or sets the ascent of characters in the font.</summary>
		/// <returns>The ascent of characters in the font.</returns>
		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06004DAD RID: 19885 RVA: 0x00141560 File Offset: 0x0013F760
		// (set) Token: 0x06004DAE RID: 19886 RVA: 0x00141568 File Offset: 0x0013F768
		public int Ascent
		{
			get
			{
				return this.ascent;
			}
			set
			{
				this.ascent = value;
			}
		}

		/// <summary>Gets or sets the descent of characters in the font.</summary>
		/// <returns>The descent of characters in the font.</returns>
		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06004DAF RID: 19887 RVA: 0x00141571 File Offset: 0x0013F771
		// (set) Token: 0x06004DB0 RID: 19888 RVA: 0x00141579 File Offset: 0x0013F779
		public int Descent
		{
			get
			{
				return this.descent;
			}
			set
			{
				this.descent = value;
			}
		}

		/// <summary>Gets or sets the amount of leading inside the bounds set by the <see cref="P:System.Windows.Forms.VisualStyles.TextMetrics.Height" /> property.</summary>
		/// <returns>The amount of leading inside the bounds set by the <see cref="P:System.Windows.Forms.VisualStyles.TextMetrics.Height" /> property.</returns>
		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06004DB1 RID: 19889 RVA: 0x00141582 File Offset: 0x0013F782
		// (set) Token: 0x06004DB2 RID: 19890 RVA: 0x0014158A File Offset: 0x0013F78A
		public int InternalLeading
		{
			get
			{
				return this.internalLeading;
			}
			set
			{
				this.internalLeading = value;
			}
		}

		/// <summary>Gets or sets the amount of extra leading that the application adds between rows.</summary>
		/// <returns>The amount of extra leading (space) required between rows.</returns>
		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06004DB3 RID: 19891 RVA: 0x00141593 File Offset: 0x0013F793
		// (set) Token: 0x06004DB4 RID: 19892 RVA: 0x0014159B File Offset: 0x0013F79B
		public int ExternalLeading
		{
			get
			{
				return this.externalLeading;
			}
			set
			{
				this.externalLeading = value;
			}
		}

		/// <summary>Gets or sets the average width of characters in the font.</summary>
		/// <returns>The average width of characters in the font.</returns>
		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06004DB5 RID: 19893 RVA: 0x001415A4 File Offset: 0x0013F7A4
		// (set) Token: 0x06004DB6 RID: 19894 RVA: 0x001415AC File Offset: 0x0013F7AC
		public int AverageCharWidth
		{
			get
			{
				return this.aveCharWidth;
			}
			set
			{
				this.aveCharWidth = value;
			}
		}

		/// <summary>Gets or sets the width of the widest character in the font.</summary>
		/// <returns>The width of the widest character in the font.</returns>
		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06004DB7 RID: 19895 RVA: 0x001415B5 File Offset: 0x0013F7B5
		// (set) Token: 0x06004DB8 RID: 19896 RVA: 0x001415BD File Offset: 0x0013F7BD
		public int MaxCharWidth
		{
			get
			{
				return this.maxCharWidth;
			}
			set
			{
				this.maxCharWidth = value;
			}
		}

		/// <summary>Gets or sets the weight of the font.</summary>
		/// <returns>The weight of the font.</returns>
		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06004DB9 RID: 19897 RVA: 0x001415C6 File Offset: 0x0013F7C6
		// (set) Token: 0x06004DBA RID: 19898 RVA: 0x001415CE File Offset: 0x0013F7CE
		public int Weight
		{
			get
			{
				return this.weight;
			}
			set
			{
				this.weight = value;
			}
		}

		/// <summary>Gets or sets the extra width per string that may be added to some synthesized fonts.</summary>
		/// <returns>The extra width per string that may be added to some synthesized fonts.</returns>
		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06004DBB RID: 19899 RVA: 0x001415D7 File Offset: 0x0013F7D7
		// (set) Token: 0x06004DBC RID: 19900 RVA: 0x001415DF File Offset: 0x0013F7DF
		public int Overhang
		{
			get
			{
				return this.overhang;
			}
			set
			{
				this.overhang = value;
			}
		}

		/// <summary>Gets or sets the horizontal aspect of the device for which the font was designed.</summary>
		/// <returns>The horizontal aspect of the device for which the font was designed.</returns>
		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06004DBD RID: 19901 RVA: 0x001415E8 File Offset: 0x0013F7E8
		// (set) Token: 0x06004DBE RID: 19902 RVA: 0x001415F0 File Offset: 0x0013F7F0
		public int DigitizedAspectX
		{
			get
			{
				return this.digitizedAspectX;
			}
			set
			{
				this.digitizedAspectX = value;
			}
		}

		/// <summary>Gets or sets the vertical aspect of the device for which the font was designed.</summary>
		/// <returns>The vertical aspect of the device for which the font was designed.</returns>
		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06004DBF RID: 19903 RVA: 0x001415F9 File Offset: 0x0013F7F9
		// (set) Token: 0x06004DC0 RID: 19904 RVA: 0x00141601 File Offset: 0x0013F801
		public int DigitizedAspectY
		{
			get
			{
				return this.digitizedAspectY;
			}
			set
			{
				this.digitizedAspectY = value;
			}
		}

		/// <summary>Gets or sets the first character defined in the font.</summary>
		/// <returns>The first character defined in the font.</returns>
		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06004DC1 RID: 19905 RVA: 0x0014160A File Offset: 0x0013F80A
		// (set) Token: 0x06004DC2 RID: 19906 RVA: 0x00141612 File Offset: 0x0013F812
		public char FirstChar
		{
			get
			{
				return this.firstChar;
			}
			set
			{
				this.firstChar = value;
			}
		}

		/// <summary>Gets or sets the last character defined in the font.</summary>
		/// <returns>The last character defined in the font.</returns>
		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06004DC3 RID: 19907 RVA: 0x0014161B File Offset: 0x0013F81B
		// (set) Token: 0x06004DC4 RID: 19908 RVA: 0x00141623 File Offset: 0x0013F823
		public char LastChar
		{
			get
			{
				return this.lastChar;
			}
			set
			{
				this.lastChar = value;
			}
		}

		/// <summary>Gets or sets the character to be substituted for characters not in the font.</summary>
		/// <returns>The character to be substituted for characters not in the font.</returns>
		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06004DC5 RID: 19909 RVA: 0x0014162C File Offset: 0x0013F82C
		// (set) Token: 0x06004DC6 RID: 19910 RVA: 0x00141634 File Offset: 0x0013F834
		public char DefaultChar
		{
			get
			{
				return this.defaultChar;
			}
			set
			{
				this.defaultChar = value;
			}
		}

		/// <summary>Gets or sets the character used to define word breaks for text justification.</summary>
		/// <returns>The character used to define word breaks for text justification.</returns>
		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06004DC7 RID: 19911 RVA: 0x0014163D File Offset: 0x0013F83D
		// (set) Token: 0x06004DC8 RID: 19912 RVA: 0x00141645 File Offset: 0x0013F845
		public char BreakChar
		{
			get
			{
				return this.breakChar;
			}
			set
			{
				this.breakChar = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the font is italic.</summary>
		/// <returns>
		///   <see langword="true" /> if the font is italic; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06004DC9 RID: 19913 RVA: 0x0014164E File Offset: 0x0013F84E
		// (set) Token: 0x06004DCA RID: 19914 RVA: 0x00141656 File Offset: 0x0013F856
		public bool Italic
		{
			get
			{
				return this.italic;
			}
			set
			{
				this.italic = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the font is underlined.</summary>
		/// <returns>
		///   <see langword="true" /> if the font is underlined; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x0014165F File Offset: 0x0013F85F
		// (set) Token: 0x06004DCC RID: 19916 RVA: 0x00141667 File Offset: 0x0013F867
		public bool Underlined
		{
			get
			{
				return this.underlined;
			}
			set
			{
				this.underlined = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the font specifies a horizontal line through the characters.</summary>
		/// <returns>
		///   <see langword="true" /> if the font has a horizontal line through the characters; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06004DCD RID: 19917 RVA: 0x00141670 File Offset: 0x0013F870
		// (set) Token: 0x06004DCE RID: 19918 RVA: 0x00141678 File Offset: 0x0013F878
		public bool StruckOut
		{
			get
			{
				return this.struckOut;
			}
			set
			{
				this.struckOut = value;
			}
		}

		/// <summary>Gets or sets information about the pitch, technology, and family of a physical font.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.TextMetricsPitchAndFamilyValues" /> values that specifies the pitch, technology, and family of a physical font.</returns>
		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06004DCF RID: 19919 RVA: 0x00141681 File Offset: 0x0013F881
		// (set) Token: 0x06004DD0 RID: 19920 RVA: 0x00141689 File Offset: 0x0013F889
		public TextMetricsPitchAndFamilyValues PitchAndFamily
		{
			get
			{
				return this.pitchAndFamily;
			}
			set
			{
				this.pitchAndFamily = value;
			}
		}

		/// <summary>Gets or sets the character set of the font.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.VisualStyles.TextMetricsCharacterSet" /> values that specifies the character set of the font.</returns>
		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06004DD1 RID: 19921 RVA: 0x00141692 File Offset: 0x0013F892
		// (set) Token: 0x06004DD2 RID: 19922 RVA: 0x0014169A File Offset: 0x0013F89A
		public TextMetricsCharacterSet CharSet
		{
			get
			{
				return this.charSet;
			}
			set
			{
				this.charSet = value;
			}
		}

		// Token: 0x0400337C RID: 13180
		private int height;

		// Token: 0x0400337D RID: 13181
		private int ascent;

		// Token: 0x0400337E RID: 13182
		private int descent;

		// Token: 0x0400337F RID: 13183
		private int internalLeading;

		// Token: 0x04003380 RID: 13184
		private int externalLeading;

		// Token: 0x04003381 RID: 13185
		private int aveCharWidth;

		// Token: 0x04003382 RID: 13186
		private int maxCharWidth;

		// Token: 0x04003383 RID: 13187
		private int weight;

		// Token: 0x04003384 RID: 13188
		private int overhang;

		// Token: 0x04003385 RID: 13189
		private int digitizedAspectX;

		// Token: 0x04003386 RID: 13190
		private int digitizedAspectY;

		// Token: 0x04003387 RID: 13191
		private char firstChar;

		// Token: 0x04003388 RID: 13192
		private char lastChar;

		// Token: 0x04003389 RID: 13193
		private char defaultChar;

		// Token: 0x0400338A RID: 13194
		private char breakChar;

		// Token: 0x0400338B RID: 13195
		private bool italic;

		// Token: 0x0400338C RID: 13196
		private bool underlined;

		// Token: 0x0400338D RID: 13197
		private bool struckOut;

		// Token: 0x0400338E RID: 13198
		private TextMetricsPitchAndFamilyValues pitchAndFamily;

		// Token: 0x0400338F RID: 13199
		private TextMetricsCharacterSet charSet;
	}
}

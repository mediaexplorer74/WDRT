using System;
using System.Text;

namespace System.Collections.Specialized
{
	/// <summary>Provides a simple structure that stores Boolean values and small integers in 32 bits of memory.</summary>
	// Token: 0x020003A7 RID: 935
	public struct BitVector32
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.BitVector32" /> structure containing the data represented in an integer.</summary>
		/// <param name="data">An integer representing the data of the new <see cref="T:System.Collections.Specialized.BitVector32" />.</param>
		// Token: 0x060022D4 RID: 8916 RVA: 0x000A5CC0 File Offset: 0x000A3EC0
		public BitVector32(int data)
		{
			this.data = (uint)data;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.BitVector32" /> structure containing the data represented in an existing <see cref="T:System.Collections.Specialized.BitVector32" /> structure.</summary>
		/// <param name="value">A <see cref="T:System.Collections.Specialized.BitVector32" /> structure that contains the data to copy.</param>
		// Token: 0x060022D5 RID: 8917 RVA: 0x000A5CC9 File Offset: 0x000A3EC9
		public BitVector32(BitVector32 value)
		{
			this.data = value.data;
		}

		/// <summary>Gets or sets the state of the bit flag indicated by the specified mask.</summary>
		/// <param name="bit">A mask that indicates the bit to get or set.</param>
		/// <returns>
		///   <see langword="true" /> if the specified bit flag is on (1); otherwise, <see langword="false" />.</returns>
		// Token: 0x170008D2 RID: 2258
		public bool this[int bit]
		{
			get
			{
				return ((ulong)this.data & (ulong)((long)bit)) == (ulong)bit;
			}
			set
			{
				if (value)
				{
					this.data |= (uint)bit;
					return;
				}
				this.data &= (uint)(~(uint)bit);
			}
		}

		/// <summary>Gets or sets the value stored in the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</summary>
		/// <param name="section">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> that contains the value to get or set.</param>
		/// <returns>The value stored in the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
		// Token: 0x170008D3 RID: 2259
		public int this[BitVector32.Section section]
		{
			get
			{
				return (int)((this.data & (uint)((uint)section.Mask << (int)section.Offset)) >> (int)section.Offset);
			}
			set
			{
				value <<= (int)section.Offset;
				int num = (65535 & (int)section.Mask) << (int)section.Offset;
				this.data = (this.data & (uint)(~(uint)num)) | (uint)(value & num);
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Collections.Specialized.BitVector32" /> as an integer.</summary>
		/// <returns>The value of the <see cref="T:System.Collections.Specialized.BitVector32" /> as an integer.</returns>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x000A5D77 File Offset: 0x000A3F77
		public int Data
		{
			get
			{
				return (int)this.data;
			}
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000A5D80 File Offset: 0x000A3F80
		private static short CountBitsSet(short mask)
		{
			short num = 0;
			while ((mask & 1) != 0)
			{
				num += 1;
				mask = (short)(mask >> 1);
			}
			return num;
		}

		/// <summary>Creates the first mask in a series of masks that can be used to retrieve individual bits in a <see cref="T:System.Collections.Specialized.BitVector32" /> that is set up as bit flags.</summary>
		/// <returns>A mask that isolates the first bit flag in the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		// Token: 0x060022DC RID: 8924 RVA: 0x000A5DA2 File Offset: 0x000A3FA2
		public static int CreateMask()
		{
			return BitVector32.CreateMask(0);
		}

		/// <summary>Creates an additional mask following the specified mask in a series of masks that can be used to retrieve individual bits in a <see cref="T:System.Collections.Specialized.BitVector32" /> that is set up as bit flags.</summary>
		/// <param name="previous">The mask that indicates the previous bit flag.</param>
		/// <returns>A mask that isolates the bit flag following the one that <paramref name="previous" /> points to in <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="previous" /> indicates the last bit flag in the <see cref="T:System.Collections.Specialized.BitVector32" />.</exception>
		// Token: 0x060022DD RID: 8925 RVA: 0x000A5DAA File Offset: 0x000A3FAA
		public static int CreateMask(int previous)
		{
			if (previous == 0)
			{
				return 1;
			}
			if (previous == -2147483648)
			{
				throw new InvalidOperationException(SR.GetString("BitVectorFull"));
			}
			return previous << 1;
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000A5DCC File Offset: 0x000A3FCC
		private static short CreateMaskFromHighValue(short highValue)
		{
			short num = 16;
			while (((int)highValue & 32768) == 0)
			{
				num -= 1;
				highValue = (short)(highValue << 1);
			}
			ushort num2 = 0;
			while (num > 0)
			{
				num -= 1;
				num2 = (ushort)(num2 << 1);
				num2 |= 1;
			}
			return (short)num2;
		}

		/// <summary>Creates the first <see cref="T:System.Collections.Specialized.BitVector32.Section" /> in a series of sections that contain small integers.</summary>
		/// <param name="maxValue">A 16-bit signed integer that specifies the maximum value for the new <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</param>
		/// <returns>A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> that can hold a number from zero to <paramref name="maxValue" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="maxValue" /> is less than 1.</exception>
		// Token: 0x060022DF RID: 8927 RVA: 0x000A5E0B File Offset: 0x000A400B
		public static BitVector32.Section CreateSection(short maxValue)
		{
			return BitVector32.CreateSectionHelper(maxValue, 0, 0);
		}

		/// <summary>Creates a new <see cref="T:System.Collections.Specialized.BitVector32.Section" /> following the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" /> in a series of sections that contain small integers.</summary>
		/// <param name="maxValue">A 16-bit signed integer that specifies the maximum value for the new <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</param>
		/// <param name="previous">The previous <see cref="T:System.Collections.Specialized.BitVector32.Section" /> in the <see cref="T:System.Collections.Specialized.BitVector32" />.</param>
		/// <returns>A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> that can hold a number from zero to <paramref name="maxValue" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="maxValue" /> is less than 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="previous" /> includes the final bit in the <see cref="T:System.Collections.Specialized.BitVector32" />.  
		/// -or-  
		/// <paramref name="maxValue" /> is greater than the highest value that can be represented by the number of bits after <paramref name="previous" />.</exception>
		// Token: 0x060022E0 RID: 8928 RVA: 0x000A5E15 File Offset: 0x000A4015
		public static BitVector32.Section CreateSection(short maxValue, BitVector32.Section previous)
		{
			return BitVector32.CreateSectionHelper(maxValue, previous.Mask, previous.Offset);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000A5E2C File Offset: 0x000A402C
		private static BitVector32.Section CreateSectionHelper(short maxValue, short priorMask, short priorOffset)
		{
			if (maxValue < 1)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidValue", new object[] { "maxValue", 0 }), "maxValue");
			}
			short num = priorOffset + BitVector32.CountBitsSet(priorMask);
			if (num >= 32)
			{
				throw new InvalidOperationException(SR.GetString("BitVectorFull"));
			}
			return new BitVector32.Section(BitVector32.CreateMaskFromHighValue(maxValue), num);
		}

		/// <summary>Determines whether the specified object is equal to the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <param name="o">The object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the <see cref="T:System.Collections.Specialized.BitVector32" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060022E2 RID: 8930 RVA: 0x000A5E94 File Offset: 0x000A4094
		public override bool Equals(object o)
		{
			return o is BitVector32 && this.data == ((BitVector32)o).data;
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>A hash code for the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		// Token: 0x060022E3 RID: 8931 RVA: 0x000A5EB3 File Offset: 0x000A40B3
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <param name="value">The <see cref="T:System.Collections.Specialized.BitVector32" /> to represent.</param>
		/// <returns>A string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		// Token: 0x060022E4 RID: 8932 RVA: 0x000A5EC8 File Offset: 0x000A40C8
		public static string ToString(BitVector32 value)
		{
			StringBuilder stringBuilder = new StringBuilder(45);
			stringBuilder.Append("BitVector32{");
			int num = (int)value.data;
			for (int i = 0; i < 32; i++)
			{
				if (((long)num & (long)((ulong)(-2147483648))) != 0L)
				{
					stringBuilder.Append("1");
				}
				else
				{
					stringBuilder.Append("0");
				}
				num <<= 1;
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		// Token: 0x060022E5 RID: 8933 RVA: 0x000A5F38 File Offset: 0x000A4138
		public override string ToString()
		{
			return BitVector32.ToString(this);
		}

		// Token: 0x04001FA0 RID: 8096
		private uint data;

		/// <summary>Represents a section of the vector that can contain an integer number.</summary>
		// Token: 0x020007E6 RID: 2022
		public struct Section
		{
			// Token: 0x060043BB RID: 17339 RVA: 0x0011CE79 File Offset: 0x0011B079
			internal Section(short mask, short offset)
			{
				this.mask = mask;
				this.offset = offset;
			}

			/// <summary>Gets a mask that isolates this section within the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
			/// <returns>A mask that isolates this section within the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
			// Token: 0x17000F51 RID: 3921
			// (get) Token: 0x060043BC RID: 17340 RVA: 0x0011CE89 File Offset: 0x0011B089
			public short Mask
			{
				get
				{
					return this.mask;
				}
			}

			/// <summary>Gets the offset of this section from the start of the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
			/// <returns>The offset of this section from the start of the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
			// Token: 0x17000F52 RID: 3922
			// (get) Token: 0x060043BD RID: 17341 RVA: 0x0011CE91 File Offset: 0x0011B091
			public short Offset
			{
				get
				{
					return this.offset;
				}
			}

			/// <summary>Determines whether the specified object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</summary>
			/// <param name="o">The object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</param>
			/// <returns>
			///   <see langword="true" /> if the specified object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object; otherwise, <see langword="false" />.</returns>
			// Token: 0x060043BE RID: 17342 RVA: 0x0011CE99 File Offset: 0x0011B099
			public override bool Equals(object o)
			{
				return o is BitVector32.Section && this.Equals((BitVector32.Section)o);
			}

			/// <summary>Determines whether the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</summary>
			/// <param name="obj">The <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			/// <returns>
			///   <see langword="true" /> if the <paramref name="obj" /> parameter is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object; otherwise <see langword="false" />.</returns>
			// Token: 0x060043BF RID: 17343 RVA: 0x0011CEB1 File Offset: 0x0011B0B1
			public bool Equals(BitVector32.Section obj)
			{
				return obj.mask == this.mask && obj.offset == this.offset;
			}

			/// <summary>Determines whether two specified <see cref="T:System.Collections.Specialized.BitVector32.Section" /> objects are equal.</summary>
			/// <param name="a">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			/// <param name="b">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			/// <returns>
			///   <see langword="true" /> if the <paramref name="a" /> and <paramref name="b" /> parameters represent the same <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object, otherwise, <see langword="false" />.</returns>
			// Token: 0x060043C0 RID: 17344 RVA: 0x0011CED1 File Offset: 0x0011B0D1
			public static bool operator ==(BitVector32.Section a, BitVector32.Section b)
			{
				return a.Equals(b);
			}

			/// <summary>Determines whether two <see cref="T:System.Collections.Specialized.BitVector32.Section" /> objects have different values.</summary>
			/// <param name="a">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			/// <param name="b">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			/// <returns>
			///   <see langword="true" /> if the <paramref name="a" /> and <paramref name="b" /> parameters represent different <see cref="T:System.Collections.Specialized.BitVector32.Section" /> objects; otherwise, <see langword="false" />.</returns>
			// Token: 0x060043C1 RID: 17345 RVA: 0x0011CEDB File Offset: 0x0011B0DB
			public static bool operator !=(BitVector32.Section a, BitVector32.Section b)
			{
				return !(a == b);
			}

			/// <summary>Serves as a hash function for the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
			/// <returns>A hash code for the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			// Token: 0x060043C2 RID: 17346 RVA: 0x0011CEE7 File Offset: 0x0011B0E7
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			/// <summary>Returns a string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</summary>
			/// <param name="value">The <see cref="T:System.Collections.Specialized.BitVector32.Section" /> to represent.</param>
			/// <returns>A string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			// Token: 0x060043C3 RID: 17347 RVA: 0x0011CEFC File Offset: 0x0011B0FC
			public static string ToString(BitVector32.Section value)
			{
				return string.Concat(new string[]
				{
					"Section{0x",
					Convert.ToString(value.Mask, 16),
					", 0x",
					Convert.ToString(value.Offset, 16),
					"}"
				});
			}

			/// <summary>Returns a string that represents the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</summary>
			/// <returns>A string that represents the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			// Token: 0x060043C4 RID: 17348 RVA: 0x0011CF4E File Offset: 0x0011B14E
			public override string ToString()
			{
				return BitVector32.Section.ToString(this);
			}

			// Token: 0x040034D7 RID: 13527
			private readonly short mask;

			// Token: 0x040034D8 RID: 13528
			private readonly short offset;
		}
	}
}

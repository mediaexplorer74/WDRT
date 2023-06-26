using System;

namespace System.Drawing
{
	/// <summary>Specifies a range of character positions within a string.</summary>
	// Token: 0x02000046 RID: 70
	public struct CharacterRange
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.CharacterRange" /> structure, specifying a range of character positions within a string.</summary>
		/// <param name="First">The position of the first character in the range. For example, if <paramref name="First" /> is set to 0, the first position of the range is position 0 in the string.</param>
		/// <param name="Length">The number of positions in the range.</param>
		// Token: 0x060006B0 RID: 1712 RVA: 0x0001B6BE File Offset: 0x000198BE
		public CharacterRange(int First, int Length)
		{
			this.first = First;
			this.length = Length;
		}

		/// <summary>Gets or sets the position in the string of the first character of this <see cref="T:System.Drawing.CharacterRange" />.</summary>
		/// <returns>The first position of this <see cref="T:System.Drawing.CharacterRange" />.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001B6CE File Offset: 0x000198CE
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001B6D6 File Offset: 0x000198D6
		public int First
		{
			get
			{
				return this.first;
			}
			set
			{
				this.first = value;
			}
		}

		/// <summary>Gets or sets the number of positions in this <see cref="T:System.Drawing.CharacterRange" />.</summary>
		/// <returns>The number of positions in this <see cref="T:System.Drawing.CharacterRange" />.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001B6DF File Offset: 0x000198DF
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001B6E7 File Offset: 0x000198E7
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		/// <summary>Gets a value indicating whether this object is equivalent to the specified object.</summary>
		/// <param name="obj">The object to compare to for equality.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the specified object is an instance with the same <see cref="P:System.Drawing.CharacterRange.First" /> and <see cref="P:System.Drawing.CharacterRange.Length" /> value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060006B5 RID: 1717 RVA: 0x0001B6F0 File Offset: 0x000198F0
		public override bool Equals(object obj)
		{
			if (obj.GetType() != typeof(CharacterRange))
			{
				return false;
			}
			CharacterRange characterRange = (CharacterRange)obj;
			return this.first == characterRange.First && this.length == characterRange.Length;
		}

		/// <summary>Compares two <see cref="T:System.Drawing.CharacterRange" /> objects. Gets a value indicating whether the <see cref="P:System.Drawing.CharacterRange.First" /> and <see cref="P:System.Drawing.CharacterRange.Length" /> values of the two <see cref="T:System.Drawing.CharacterRange" /> objects are equal.</summary>
		/// <param name="cr1">A <see cref="T:System.Drawing.CharacterRange" /> to compare for equality.</param>
		/// <param name="cr2">A <see cref="T:System.Drawing.CharacterRange" /> to compare for equality.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the two <see cref="T:System.Drawing.CharacterRange" /> objects have the same <see cref="P:System.Drawing.CharacterRange.First" /> and <see cref="P:System.Drawing.CharacterRange.Length" /> values; otherwise, <see langword="false" />.</returns>
		// Token: 0x060006B6 RID: 1718 RVA: 0x0001B73D File Offset: 0x0001993D
		public static bool operator ==(CharacterRange cr1, CharacterRange cr2)
		{
			return cr1.First == cr2.First && cr1.Length == cr2.Length;
		}

		/// <summary>Compares two <see cref="T:System.Drawing.CharacterRange" /> objects. Gets a value indicating whether the <see cref="P:System.Drawing.CharacterRange.First" /> or <see cref="P:System.Drawing.CharacterRange.Length" /> values of the two <see cref="T:System.Drawing.CharacterRange" /> objects are not equal.</summary>
		/// <param name="cr1">A <see cref="T:System.Drawing.CharacterRange" /> to compare for inequality.</param>
		/// <param name="cr2">A <see cref="T:System.Drawing.CharacterRange" /> to compare for inequality.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the either the <see cref="P:System.Drawing.CharacterRange.First" /> or <see cref="P:System.Drawing.CharacterRange.Length" /> values of the two <see cref="T:System.Drawing.CharacterRange" /> objects differ; otherwise, <see langword="false" />.</returns>
		// Token: 0x060006B7 RID: 1719 RVA: 0x0001B761 File Offset: 0x00019961
		public static bool operator !=(CharacterRange cr1, CharacterRange cr2)
		{
			return !(cr1 == cr2);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x060006B8 RID: 1720 RVA: 0x0001B76D File Offset: 0x0001996D
		public override int GetHashCode()
		{
			return this.first << 8 + this.length;
		}

		// Token: 0x04000581 RID: 1409
		private int first;

		// Token: 0x04000582 RID: 1410
		private int length;
	}
}

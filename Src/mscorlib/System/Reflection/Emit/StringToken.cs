using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents a token that represents a string.</summary>
	// Token: 0x0200065F RID: 1631
	[ComVisible(true)]
	[Serializable]
	public struct StringToken
	{
		// Token: 0x06004D52 RID: 19794 RVA: 0x00119D58 File Offset: 0x00117F58
		internal StringToken(int str)
		{
			this.m_string = str;
		}

		/// <summary>Retrieves the metadata token for this string.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this string.</returns>
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06004D53 RID: 19795 RVA: 0x00119D61 File Offset: 0x00117F61
		public int Token
		{
			get
			{
				return this.m_string;
			}
		}

		/// <summary>Returns the hash code for this string.</summary>
		/// <returns>The underlying string token.</returns>
		// Token: 0x06004D54 RID: 19796 RVA: 0x00119D69 File Offset: 0x00117F69
		public override int GetHashCode()
		{
			return this.m_string;
		}

		/// <summary>Checks if the given object is an instance of <see langword="StringToken" /> and is equal to this instance.</summary>
		/// <param name="obj">The object to compare with this <see langword="StringToken" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="StringToken" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D55 RID: 19797 RVA: 0x00119D71 File Offset: 0x00117F71
		public override bool Equals(object obj)
		{
			return obj is StringToken && this.Equals((StringToken)obj);
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.StringToken" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D56 RID: 19798 RVA: 0x00119D89 File Offset: 0x00117F89
		public bool Equals(StringToken obj)
		{
			return obj.m_string == this.m_string;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.StringToken" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D57 RID: 19799 RVA: 0x00119D99 File Offset: 0x00117F99
		public static bool operator ==(StringToken a, StringToken b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.StringToken" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D58 RID: 19800 RVA: 0x00119DA3 File Offset: 0x00117FA3
		public static bool operator !=(StringToken a, StringToken b)
		{
			return !(a == b);
		}

		// Token: 0x040021AF RID: 8623
		internal int m_string;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The <see langword="MethodToken" /> struct is an object representation of a token that represents a method.</summary>
	// Token: 0x0200064D RID: 1613
	[ComVisible(true)]
	[Serializable]
	public struct MethodToken
	{
		// Token: 0x06004C18 RID: 19480 RVA: 0x00114918 File Offset: 0x00112B18
		internal MethodToken(int str)
		{
			this.m_method = str;
		}

		/// <summary>Returns the metadata token for this method.</summary>
		/// <returns>Read-only. Returns the metadata token for this method.</returns>
		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06004C19 RID: 19481 RVA: 0x00114921 File Offset: 0x00112B21
		public int Token
		{
			get
			{
				return this.m_method;
			}
		}

		/// <summary>Returns the generated hash code for this method.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x06004C1A RID: 19482 RVA: 0x00114929 File Offset: 0x00112B29
		public override int GetHashCode()
		{
			return this.m_method;
		}

		/// <summary>Tests whether the given object is equal to this <see langword="MethodToken" /> object.</summary>
		/// <param name="obj">The object to compare to this object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="MethodToken" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004C1B RID: 19483 RVA: 0x00114931 File Offset: 0x00112B31
		public override bool Equals(object obj)
		{
			return obj is MethodToken && this.Equals((MethodToken)obj);
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.MethodToken" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004C1C RID: 19484 RVA: 0x00114949 File Offset: 0x00112B49
		public bool Equals(MethodToken obj)
		{
			return obj.m_method == this.m_method;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.MethodToken" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004C1D RID: 19485 RVA: 0x00114959 File Offset: 0x00112B59
		public static bool operator ==(MethodToken a, MethodToken b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.MethodToken" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004C1E RID: 19486 RVA: 0x00114963 File Offset: 0x00112B63
		public static bool operator !=(MethodToken a, MethodToken b)
		{
			return !(a == b);
		}

		/// <summary>The default <see langword="MethodToken" /> with <see cref="P:System.Reflection.Emit.MethodToken.Token" /> value 0.</summary>
		// Token: 0x04001F5B RID: 8027
		public static readonly MethodToken Empty;

		// Token: 0x04001F5C RID: 8028
		internal int m_method;
	}
}

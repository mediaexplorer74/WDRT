using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The <see langword="FieldToken" /> struct is an object representation of a token that represents a field.</summary>
	// Token: 0x0200063A RID: 1594
	[ComVisible(true)]
	[Serializable]
	public struct FieldToken
	{
		// Token: 0x06004AAA RID: 19114 RVA: 0x0010F0B5 File Offset: 0x0010D2B5
		internal FieldToken(int field, Type fieldClass)
		{
			this.m_fieldTok = field;
			this.m_class = fieldClass;
		}

		/// <summary>Retrieves the metadata token for this field.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this field.</returns>
		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06004AAB RID: 19115 RVA: 0x0010F0C5 File Offset: 0x0010D2C5
		public int Token
		{
			get
			{
				return this.m_fieldTok;
			}
		}

		/// <summary>Generates the hash code for this field.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x06004AAC RID: 19116 RVA: 0x0010F0CD File Offset: 0x0010D2CD
		public override int GetHashCode()
		{
			return this.m_fieldTok;
		}

		/// <summary>Determines if an object is an instance of <see langword="FieldToken" /> and is equal to this instance.</summary>
		/// <param name="obj">The object to compare to this <see langword="FieldToken" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="FieldToken" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004AAD RID: 19117 RVA: 0x0010F0D5 File Offset: 0x0010D2D5
		public override bool Equals(object obj)
		{
			return obj is FieldToken && this.Equals((FieldToken)obj);
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.FieldToken" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004AAE RID: 19118 RVA: 0x0010F0ED File Offset: 0x0010D2ED
		public bool Equals(FieldToken obj)
		{
			return obj.m_fieldTok == this.m_fieldTok && obj.m_class == this.m_class;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.FieldToken" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004AAF RID: 19119 RVA: 0x0010F10D File Offset: 0x0010D30D
		public static bool operator ==(FieldToken a, FieldToken b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.FieldToken" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004AB0 RID: 19120 RVA: 0x0010F117 File Offset: 0x0010D317
		public static bool operator !=(FieldToken a, FieldToken b)
		{
			return !(a == b);
		}

		/// <summary>The default FieldToken with <see cref="P:System.Reflection.Emit.FieldToken.Token" /> value 0.</summary>
		// Token: 0x04001EC3 RID: 7875
		public static readonly FieldToken Empty;

		// Token: 0x04001EC4 RID: 7876
		internal int m_fieldTok;

		// Token: 0x04001EC5 RID: 7877
		internal object m_class;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the <see langword="Token" /> returned by the metadata to represent a type.</summary>
	// Token: 0x02000665 RID: 1637
	[ComVisible(true)]
	[Serializable]
	public struct TypeToken
	{
		// Token: 0x06004ECD RID: 20173 RVA: 0x0011D3C5 File Offset: 0x0011B5C5
		internal TypeToken(int str)
		{
			this.m_class = str;
		}

		/// <summary>Retrieves the metadata token for this class.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this type.</returns>
		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004ECE RID: 20174 RVA: 0x0011D3CE File Offset: 0x0011B5CE
		public int Token
		{
			get
			{
				return this.m_class;
			}
		}

		/// <summary>Generates the hash code for this type.</summary>
		/// <returns>The hash code for this type.</returns>
		// Token: 0x06004ECF RID: 20175 RVA: 0x0011D3D6 File Offset: 0x0011B5D6
		public override int GetHashCode()
		{
			return this.m_class;
		}

		/// <summary>Checks if the given object is an instance of <see langword="TypeToken" /> and is equal to this instance.</summary>
		/// <param name="obj">The object to compare with this TypeToken.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="TypeToken" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004ED0 RID: 20176 RVA: 0x0011D3DE File Offset: 0x0011B5DE
		public override bool Equals(object obj)
		{
			return obj is TypeToken && this.Equals((TypeToken)obj);
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.TypeToken" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004ED1 RID: 20177 RVA: 0x0011D3F6 File Offset: 0x0011B5F6
		public bool Equals(TypeToken obj)
		{
			return obj.m_class == this.m_class;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.TypeToken" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004ED2 RID: 20178 RVA: 0x0011D406 File Offset: 0x0011B606
		public static bool operator ==(TypeToken a, TypeToken b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.TypeToken" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004ED3 RID: 20179 RVA: 0x0011D410 File Offset: 0x0011B610
		public static bool operator !=(TypeToken a, TypeToken b)
		{
			return !(a == b);
		}

		/// <summary>The default <see langword="TypeToken" /> with <see cref="P:System.Reflection.Emit.TypeToken.Token" /> value 0.</summary>
		// Token: 0x040021DB RID: 8667
		public static readonly TypeToken Empty;

		// Token: 0x040021DC RID: 8668
		internal int m_class;
	}
}

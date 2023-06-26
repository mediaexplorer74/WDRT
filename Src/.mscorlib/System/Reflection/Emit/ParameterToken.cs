using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The <see langword="ParameterToken" /> struct is an opaque representation of the token returned by the metadata to represent a parameter.</summary>
	// Token: 0x0200065A RID: 1626
	[ComVisible(true)]
	[Serializable]
	public struct ParameterToken
	{
		// Token: 0x06004CE0 RID: 19680 RVA: 0x001186F4 File Offset: 0x001168F4
		internal ParameterToken(int tkParam)
		{
			this.m_tkParameter = tkParam;
		}

		/// <summary>Retrieves the metadata token for this parameter.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this parameter.</returns>
		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06004CE1 RID: 19681 RVA: 0x001186FD File Offset: 0x001168FD
		public int Token
		{
			get
			{
				return this.m_tkParameter;
			}
		}

		/// <summary>Generates the hash code for this parameter.</summary>
		/// <returns>The hash code for this parameter.</returns>
		// Token: 0x06004CE2 RID: 19682 RVA: 0x00118705 File Offset: 0x00116905
		public override int GetHashCode()
		{
			return this.m_tkParameter;
		}

		/// <summary>Checks if the given object is an instance of <see langword="ParameterToken" /> and is equal to this instance.</summary>
		/// <param name="obj">The object to compare to this object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="ParameterToken" /> and equals the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004CE3 RID: 19683 RVA: 0x0011870D File Offset: 0x0011690D
		public override bool Equals(object obj)
		{
			return obj is ParameterToken && this.Equals((ParameterToken)obj);
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.ParameterToken" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004CE4 RID: 19684 RVA: 0x00118725 File Offset: 0x00116925
		public bool Equals(ParameterToken obj)
		{
			return obj.m_tkParameter == this.m_tkParameter;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.ParameterToken" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004CE5 RID: 19685 RVA: 0x00118735 File Offset: 0x00116935
		public static bool operator ==(ParameterToken a, ParameterToken b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.ParameterToken" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004CE6 RID: 19686 RVA: 0x0011873F File Offset: 0x0011693F
		public static bool operator !=(ParameterToken a, ParameterToken b)
		{
			return !(a == b);
		}

		/// <summary>The default <see langword="ParameterToken" /> with <see cref="P:System.Reflection.Emit.ParameterToken.Token" /> value 0.</summary>
		// Token: 0x04002197 RID: 8599
		public static readonly ParameterToken Empty;

		// Token: 0x04002198 RID: 8600
		internal int m_tkParameter;
	}
}

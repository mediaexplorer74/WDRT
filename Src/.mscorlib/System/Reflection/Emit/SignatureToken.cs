using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the <see langword="Token" /> returned by the metadata to represent a signature.</summary>
	// Token: 0x0200065E RID: 1630
	[ComVisible(true)]
	public struct SignatureToken
	{
		// Token: 0x06004D4A RID: 19786 RVA: 0x00119CF8 File Offset: 0x00117EF8
		internal SignatureToken(int str, ModuleBuilder mod)
		{
			this.m_signature = str;
			this.m_moduleBuilder = mod;
		}

		/// <summary>Retrieves the metadata token for the local variable signature for this method.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this signature.</returns>
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06004D4B RID: 19787 RVA: 0x00119D08 File Offset: 0x00117F08
		public int Token
		{
			get
			{
				return this.m_signature;
			}
		}

		/// <summary>Generates the hash code for this signature.</summary>
		/// <returns>The hash code for this signature.</returns>
		// Token: 0x06004D4C RID: 19788 RVA: 0x00119D10 File Offset: 0x00117F10
		public override int GetHashCode()
		{
			return this.m_signature;
		}

		/// <summary>Checks if the given object is an instance of <see langword="SignatureToken" /> and is equal to this instance.</summary>
		/// <param name="obj">The object to compare with this <see langword="SignatureToken" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="SignatureToken" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D4D RID: 19789 RVA: 0x00119D18 File Offset: 0x00117F18
		public override bool Equals(object obj)
		{
			return obj is SignatureToken && this.Equals((SignatureToken)obj);
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.SignatureToken" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D4E RID: 19790 RVA: 0x00119D30 File Offset: 0x00117F30
		public bool Equals(SignatureToken obj)
		{
			return obj.m_signature == this.m_signature;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.SignatureToken" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D4F RID: 19791 RVA: 0x00119D40 File Offset: 0x00117F40
		public static bool operator ==(SignatureToken a, SignatureToken b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.SignatureToken" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D50 RID: 19792 RVA: 0x00119D4A File Offset: 0x00117F4A
		public static bool operator !=(SignatureToken a, SignatureToken b)
		{
			return !(a == b);
		}

		/// <summary>The default <see langword="SignatureToken" /> with <see cref="P:System.Reflection.Emit.SignatureToken.Token" /> value 0.</summary>
		// Token: 0x040021AC RID: 8620
		public static readonly SignatureToken Empty;

		// Token: 0x040021AD RID: 8621
		internal int m_signature;

		// Token: 0x040021AE RID: 8622
		internal ModuleBuilder m_moduleBuilder;
	}
}

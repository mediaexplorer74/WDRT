using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the name of a cryptographic hash algorithm.</summary>
	// Token: 0x02000269 RID: 617
	public struct HashAlgorithmName : IEquatable<HashAlgorithmName>
	{
		/// <summary>Gets a hash algorithm name that represents "MD5".</summary>
		/// <returns>A hash algorithm name that represents "MD5".</returns>
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x00077EEC File Offset: 0x000760EC
		public static HashAlgorithmName MD5
		{
			get
			{
				return new HashAlgorithmName("MD5");
			}
		}

		/// <summary>Gets a hash algorithm name that represents "SHA1".</summary>
		/// <returns>A hash algorithm name that represents "SHA1".</returns>
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x00077EF8 File Offset: 0x000760F8
		public static HashAlgorithmName SHA1
		{
			get
			{
				return new HashAlgorithmName("SHA1");
			}
		}

		/// <summary>Gets a hash algorithm name that represents "SHA256".</summary>
		/// <returns>A hash algorithm name that represents "SHA256".</returns>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x00077F04 File Offset: 0x00076104
		public static HashAlgorithmName SHA256
		{
			get
			{
				return new HashAlgorithmName("SHA256");
			}
		}

		/// <summary>Gets a hash algorithm name that represents "SHA384".</summary>
		/// <returns>A hash algorithm name that represents "SHA384".</returns>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x00077F10 File Offset: 0x00076110
		public static HashAlgorithmName SHA384
		{
			get
			{
				return new HashAlgorithmName("SHA384");
			}
		}

		/// <summary>Gets a hash algorithm name that represents "SHA512".</summary>
		/// <returns>A hash algorithm name that represents "SHA512".</returns>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x00077F1C File Offset: 0x0007611C
		public static HashAlgorithmName SHA512
		{
			get
			{
				return new HashAlgorithmName("SHA512");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> structure with a custom name.</summary>
		/// <param name="name">The custom hash algorithm name.</param>
		// Token: 0x060021E4 RID: 8676 RVA: 0x00077F28 File Offset: 0x00076128
		public HashAlgorithmName(string name)
		{
			this._name = name;
		}

		/// <summary>Gets the underlying string representation of the algorithm name.</summary>
		/// <returns>The string representation of the algorithm name, or <see langword="null" /> or <see cref="F:System.String.Empty" /> if no hash algorithm is available.</returns>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x00077F31 File Offset: 0x00076131
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Returns the string representation of the current <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> instance.</summary>
		/// <returns>The string representation of the current <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> instance.</returns>
		// Token: 0x060021E6 RID: 8678 RVA: 0x00077F39 File Offset: 0x00076139
		public override string ToString()
		{
			return this._name ?? string.Empty;
		}

		/// <summary>Returns a value that indicates whether the current instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> object and its <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> property is equal to that of the current instance. The comparison is ordinal and case-sensitive.</returns>
		// Token: 0x060021E7 RID: 8679 RVA: 0x00077F4A File Offset: 0x0007614A
		public override bool Equals(object obj)
		{
			return obj is HashAlgorithmName && this.Equals((HashAlgorithmName)obj);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> instances are equal.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> property of <paramref name="other" /> is equal to that of the current instance. The comparison is ordinal and case-sensitive.</returns>
		// Token: 0x060021E8 RID: 8680 RVA: 0x00077F62 File Offset: 0x00076162
		public bool Equals(HashAlgorithmName other)
		{
			return this._name == other._name;
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>The hash code for the current instance, or 0 if no <paramref name="name" /> value was supplied to the <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> constructor.</returns>
		// Token: 0x060021E9 RID: 8681 RVA: 0x00077F75 File Offset: 0x00076175
		public override int GetHashCode()
		{
			if (this._name != null)
			{
				return this._name.GetHashCode();
			}
			return 0;
		}

		/// <summary>Determines whether two specified <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if both <paramref name="left" /> and <paramref name="right" /> have the same <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021EA RID: 8682 RVA: 0x00077F8C File Offset: 0x0007618C
		public static bool operator ==(HashAlgorithmName left, HashAlgorithmName right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether two specified <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> objects are not equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if both <paramref name="left" /> and <paramref name="right" /> do not have the same <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021EB RID: 8683 RVA: 0x00077F96 File Offset: 0x00076196
		public static bool operator !=(HashAlgorithmName left, HashAlgorithmName right)
		{
			return !(left == right);
		}

		// Token: 0x04000C53 RID: 3155
		private readonly string _name;
	}
}

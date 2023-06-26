using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	/// <summary>Represents the public key information (called a blob) for a strong name. This class cannot be inherited.</summary>
	// Token: 0x02000309 RID: 777
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNamePublicKeyBlob
	{
		// Token: 0x06002782 RID: 10114 RVA: 0x00090CFA File Offset: 0x0008EEFA
		internal StrongNamePublicKeyBlob()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> class with raw bytes of the public key blob.</summary>
		/// <param name="publicKey">The array of bytes representing the raw public key data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="publicKey" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002783 RID: 10115 RVA: 0x00090D02 File Offset: 0x0008EF02
		public StrongNamePublicKeyBlob(byte[] publicKey)
		{
			if (publicKey == null)
			{
				throw new ArgumentNullException("PublicKey");
			}
			this.PublicKey = new byte[publicKey.Length];
			Array.Copy(publicKey, 0, this.PublicKey, 0, publicKey.Length);
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00090D37 File Offset: 0x0008EF37
		internal StrongNamePublicKeyBlob(string publicKey)
		{
			this.PublicKey = Hex.DecodeHexString(publicKey);
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x00090D4C File Offset: 0x0008EF4C
		private static bool CompareArrays(byte[] first, byte[] second)
		{
			if (first.Length != second.Length)
			{
				return false;
			}
			int num = first.Length;
			for (int i = 0; i < num; i++)
			{
				if (first[i] != second[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x00090D7E File Offset: 0x0008EF7E
		internal bool Equals(StrongNamePublicKeyBlob blob)
		{
			return blob != null && StrongNamePublicKeyBlob.CompareArrays(this.PublicKey, blob.PublicKey);
		}

		/// <summary>Gets or sets a value indicating whether the current public key blob is equal to the specified public key blob.</summary>
		/// <param name="obj">An object containing a public key blob.</param>
		/// <returns>
		///   <see langword="true" /> if the public key blob of the current object is equal to the public key blob of the <paramref name="o" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002787 RID: 10119 RVA: 0x00090D96 File Offset: 0x0008EF96
		public override bool Equals(object obj)
		{
			return obj != null && obj is StrongNamePublicKeyBlob && this.Equals((StrongNamePublicKeyBlob)obj);
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x00090DB4 File Offset: 0x0008EFB4
		private static int GetByteArrayHashCode(byte[] baData)
		{
			if (baData == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < baData.Length; i++)
			{
				num = (num << 8) ^ (int)baData[i] ^ (num >> 24);
			}
			return num;
		}

		/// <summary>Returns a hash code based on the public key.</summary>
		/// <returns>The hash code based on the public key.</returns>
		// Token: 0x06002789 RID: 10121 RVA: 0x00090DE4 File Offset: 0x0008EFE4
		public override int GetHashCode()
		{
			return StrongNamePublicKeyBlob.GetByteArrayHashCode(this.PublicKey);
		}

		/// <summary>Creates and returns a string representation of the public key blob.</summary>
		/// <returns>A hexadecimal version of the public key blob.</returns>
		// Token: 0x0600278A RID: 10122 RVA: 0x00090DF1 File Offset: 0x0008EFF1
		public override string ToString()
		{
			return Hex.EncodeHexString(this.PublicKey);
		}

		// Token: 0x04000F51 RID: 3921
		internal byte[] PublicKey;
	}
}

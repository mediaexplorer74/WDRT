using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class for Triple Data Encryption Standard algorithms from which all <see cref="T:System.Security.Cryptography.TripleDES" /> implementations must derive.</summary>
	// Token: 0x020002A0 RID: 672
	[ComVisible(true)]
	public abstract class TripleDES : SymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.TripleDES" /> class.</summary>
		// Token: 0x060023C2 RID: 9154 RVA: 0x00082407 File Offset: 0x00080607
		protected TripleDES()
		{
			this.KeySizeValue = 192;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = TripleDES.s_legalBlockSizes;
			this.LegalKeySizesValue = TripleDES.s_legalKeySizes;
		}

		/// <summary>Gets or sets the secret key for the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</summary>
		/// <returns>The secret key for the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the key to <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt was made to set a key whose length is invalid.  
		///  -or-  
		///  An attempt was made to set a weak key (see <see cref="M:System.Security.Cryptography.TripleDES.IsWeakKey(System.Byte[])" />).</exception>
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x00082444 File Offset: 0x00080644
		// (set) Token: 0x060023C4 RID: 9156 RVA: 0x00082474 File Offset: 0x00080674
		public override byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					do
					{
						this.GenerateKey();
					}
					while (TripleDES.IsWeakKey(this.KeyValue));
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!base.ValidKeySize(value.Length * 8))
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
				}
				if (TripleDES.IsWeakKey(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), "TripleDES");
				}
				this.KeyValue = (byte[])value.Clone();
				this.KeySizeValue = value.Length * 8;
			}
		}

		/// <summary>Creates an instance of a cryptographic object to perform the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</summary>
		/// <returns>An instance of a cryptographic object.</returns>
		// Token: 0x060023C5 RID: 9157 RVA: 0x000824E5 File Offset: 0x000806E5
		public new static TripleDES Create()
		{
			return TripleDES.Create("System.Security.Cryptography.TripleDES");
		}

		/// <summary>Creates an instance of a cryptographic object to perform the specified implementation of the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</summary>
		/// <param name="str">The name of the specific implementation of <see cref="T:System.Security.Cryptography.TripleDES" /> to use.</param>
		/// <returns>An instance of a cryptographic object.</returns>
		// Token: 0x060023C6 RID: 9158 RVA: 0x000824F1 File Offset: 0x000806F1
		public new static TripleDES Create(string str)
		{
			return (TripleDES)CryptoConfig.CreateFromName(str);
		}

		/// <summary>Determines whether the specified key is weak.</summary>
		/// <param name="rgbKey">The secret key to test for weakness.</param>
		/// <returns>
		///   <see langword="true" /> if the key is weak; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The size of the <paramref name="rgbKey" /> parameter is not valid.</exception>
		// Token: 0x060023C7 RID: 9159 RVA: 0x00082500 File Offset: 0x00080700
		public static bool IsWeakKey(byte[] rgbKey)
		{
			if (!TripleDES.IsLegalKeySize(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			byte[] array = Utils.FixupKeyParity(rgbKey);
			return TripleDES.EqualBytes(array, 0, 8, 8) || (array.Length == 24 && TripleDES.EqualBytes(array, 8, 16, 8));
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x00082550 File Offset: 0x00080750
		private static bool EqualBytes(byte[] rgbKey, int start1, int start2, int count)
		{
			if (start1 < 0)
			{
				throw new ArgumentOutOfRangeException("start1", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (start2 < 0)
			{
				throw new ArgumentOutOfRangeException("start2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (start1 + count > rgbKey.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (start2 + count > rgbKey.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			for (int i = 0; i < count; i++)
			{
				if (rgbKey[start1 + i] != rgbKey[start2 + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000825DA File Offset: 0x000807DA
		private static bool IsLegalKeySize(byte[] rgbKey)
		{
			return rgbKey != null && (rgbKey.Length == 16 || rgbKey.Length == 24);
		}

		// Token: 0x04000D00 RID: 3328
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000D01 RID: 3329
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 192, 64)
		};
	}
}

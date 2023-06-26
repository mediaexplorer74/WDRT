using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Message Authentication Code (MAC) using <see cref="T:System.Security.Cryptography.TripleDES" /> for the input data <see cref="T:System.Security.Cryptography.CryptoStream" />.</summary>
	// Token: 0x0200026E RID: 622
	[ComVisible(true)]
	public class MACTripleDES : KeyedHashAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.MACTripleDES" /> class.</summary>
		// Token: 0x06002205 RID: 8709 RVA: 0x00078438 File Offset: 0x00076638
		public MACTripleDES()
		{
			this.KeyValue = new byte[24];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			this.des = TripleDES.Create();
			this.HashSizeValue = this.des.BlockSize;
			this.m_bytesPerBlock = this.des.BlockSize / 8;
			this.des.IV = new byte[this.m_bytesPerBlock];
			this.des.Padding = PaddingMode.Zeros;
			this.m_encryptor = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.MACTripleDES" /> class with the specified key data.</summary>
		/// <param name="rgbKey">The secret key for <see cref="T:System.Security.Cryptography.MACTripleDES" /> encryption.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbKey" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002206 RID: 8710 RVA: 0x000784C0 File Offset: 0x000766C0
		public MACTripleDES(byte[] rgbKey)
			: this("System.Security.Cryptography.TripleDES", rgbKey)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.MACTripleDES" /> class with the specified key data and using the specified implementation of <see cref="T:System.Security.Cryptography.TripleDES" />.</summary>
		/// <param name="strTripleDES">The name of the <see cref="T:System.Security.Cryptography.TripleDES" /> implementation to use.</param>
		/// <param name="rgbKey">The secret key for <see cref="T:System.Security.Cryptography.MACTripleDES" /> encryption.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbKey" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <paramref name="strTripleDES" /> parameter is not a valid name of a <see cref="T:System.Security.Cryptography.TripleDES" /> implementation.</exception>
		// Token: 0x06002207 RID: 8711 RVA: 0x000784D0 File Offset: 0x000766D0
		public MACTripleDES(string strTripleDES, byte[] rgbKey)
		{
			if (rgbKey == null)
			{
				throw new ArgumentNullException("rgbKey");
			}
			if (strTripleDES == null)
			{
				this.des = TripleDES.Create();
			}
			else
			{
				this.des = TripleDES.Create(strTripleDES);
			}
			this.HashSizeValue = this.des.BlockSize;
			this.KeyValue = (byte[])rgbKey.Clone();
			this.m_bytesPerBlock = this.des.BlockSize / 8;
			this.des.IV = new byte[this.m_bytesPerBlock];
			this.des.Padding = PaddingMode.Zeros;
			this.m_encryptor = null;
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.MACTripleDES" />.</summary>
		// Token: 0x06002208 RID: 8712 RVA: 0x0007856B File Offset: 0x0007676B
		public override void Initialize()
		{
			this.m_encryptor = null;
		}

		/// <summary>Gets or sets the padding mode used in the hashing algorithm.</summary>
		/// <returns>The padding mode used in the hashing algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The property cannot be set because the padding mode is invalid.</exception>
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x00078574 File Offset: 0x00076774
		// (set) Token: 0x0600220A RID: 8714 RVA: 0x00078581 File Offset: 0x00076781
		[ComVisible(false)]
		public PaddingMode Padding
		{
			get
			{
				return this.des.Padding;
			}
			set
			{
				if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
				}
				this.des.Padding = value;
			}
		}

		/// <summary>Routes data written to the object into the <see cref="T:System.Security.Cryptography.TripleDES" /> encryptor for computing the Message Authentication Code (MAC).</summary>
		/// <param name="rgbData">The input data.</param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data.</param>
		/// <param name="cbSize">The number of bytes in the array to use as data.</param>
		// Token: 0x0600220B RID: 8715 RVA: 0x000785A8 File Offset: 0x000767A8
		protected override void HashCore(byte[] rgbData, int ibStart, int cbSize)
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.Write(rgbData, ibStart, cbSize);
		}

		/// <summary>Returns the computed Message Authentication Code (MAC) after all data is written to the object.</summary>
		/// <returns>The computed MAC.</returns>
		// Token: 0x0600220C RID: 8716 RVA: 0x00078620 File Offset: 0x00076820
		protected override byte[] HashFinal()
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.FlushFinalBlock();
			return this._ts.Buffer;
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Security.Cryptography.MACTripleDES" /> instance.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> if the method is called from an <see cref="M:System.IDisposable.Dispose" /> implementation; otherwise, <see langword="false" />.</param>
		// Token: 0x0600220D RID: 8717 RVA: 0x000786A0 File Offset: 0x000768A0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.des != null)
				{
					this.des.Clear();
				}
				if (this.m_encryptor != null)
				{
					this.m_encryptor.Dispose();
				}
				if (this._cs != null)
				{
					this._cs.Clear();
				}
				if (this._ts != null)
				{
					this._ts.Clear();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000C5A RID: 3162
		private ICryptoTransform m_encryptor;

		// Token: 0x04000C5B RID: 3163
		private CryptoStream _cs;

		// Token: 0x04000C5C RID: 3164
		private TailStream _ts;

		// Token: 0x04000C5D RID: 3165
		private const int m_bitsPerByte = 8;

		// Token: 0x04000C5E RID: 3166
		private int m_bytesPerBlock;

		// Token: 0x04000C5F RID: 3167
		private TripleDES des;
	}
}

using System;

namespace System.Security.Cryptography
{
	/// <summary>Represents Abstract Syntax Notation One (ASN.1)-encoded data.</summary>
	// Token: 0x0200044D RID: 1101
	public class AsnEncodedData
	{
		// Token: 0x060028AE RID: 10414 RVA: 0x000BA7C2 File Offset: 0x000B89C2
		internal AsnEncodedData(Oid oid)
		{
			this.m_oid = oid;
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x000BA7D1 File Offset: 0x000B89D1
		internal AsnEncodedData(string oid, CAPIBase.CRYPTOAPI_BLOB encodedBlob)
			: this(oid, CAPI.BlobToByteArray(encodedBlob))
		{
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000BA7E0 File Offset: 0x000B89E0
		internal AsnEncodedData(Oid oid, CAPIBase.CRYPTOAPI_BLOB encodedBlob)
			: this(oid, CAPI.BlobToByteArray(encodedBlob))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class.</summary>
		// Token: 0x060028B1 RID: 10417 RVA: 0x000BA7EF File Offset: 0x000B89EF
		protected AsnEncodedData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using a byte array.</summary>
		/// <param name="rawData">A byte array that contains Abstract Syntax Notation One (ASN.1)-encoded data.</param>
		// Token: 0x060028B2 RID: 10418 RVA: 0x000BA7F7 File Offset: 0x000B89F7
		public AsnEncodedData(byte[] rawData)
		{
			this.Reset(null, rawData);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using a byte array.</summary>
		/// <param name="oid">A string that represents <see cref="T:System.Security.Cryptography.Oid" /> information.</param>
		/// <param name="rawData">A byte array that contains Abstract Syntax Notation One (ASN.1)-encoded data.</param>
		// Token: 0x060028B3 RID: 10419 RVA: 0x000BA807 File Offset: 0x000B8A07
		public AsnEncodedData(string oid, byte[] rawData)
		{
			this.Reset(new Oid(oid), rawData);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using an <see cref="T:System.Security.Cryptography.Oid" /> object and a byte array.</summary>
		/// <param name="oid">An <see cref="T:System.Security.Cryptography.Oid" /> object.</param>
		/// <param name="rawData">A byte array that contains Abstract Syntax Notation One (ASN.1)-encoded data.</param>
		// Token: 0x060028B4 RID: 10420 RVA: 0x000BA81C File Offset: 0x000B8A1C
		public AsnEncodedData(Oid oid, byte[] rawData)
		{
			this.Reset(oid, rawData);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using an instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class.</summary>
		/// <param name="asnEncodedData">An instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		// Token: 0x060028B5 RID: 10421 RVA: 0x000BA82C File Offset: 0x000B8A2C
		public AsnEncodedData(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			this.Reset(asnEncodedData.m_oid, asnEncodedData.m_rawData);
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Oid" /> value for an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x000BA854 File Offset: 0x000B8A54
		// (set) Token: 0x060028B7 RID: 10423 RVA: 0x000BA85C File Offset: 0x000B8A5C
		public Oid Oid
		{
			get
			{
				return this.m_oid;
			}
			set
			{
				if (value == null)
				{
					this.m_oid = null;
					return;
				}
				this.m_oid = new Oid(value);
			}
		}

		/// <summary>Gets or sets the Abstract Syntax Notation One (ASN.1)-encoded data represented in a byte array.</summary>
		/// <returns>A byte array that represents the Abstract Syntax Notation One (ASN.1)-encoded data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is <see langword="null" />.</exception>
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x000BA875 File Offset: 0x000B8A75
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x000BA87D File Offset: 0x000B8A7D
		public byte[] RawData
		{
			get
			{
				return this.m_rawData;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_rawData = (byte[])value.Clone();
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to base the new object on.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		// Token: 0x060028BA RID: 10426 RVA: 0x000BA89E File Offset: 0x000B8A9E
		public virtual void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			this.Reset(asnEncodedData.m_oid, asnEncodedData.m_rawData);
		}

		/// <summary>Returns a formatted version of the Abstract Syntax Notation One (ASN.1)-encoded data as a string.</summary>
		/// <param name="multiLine">
		///   <see langword="true" /> if the return string should contain carriage returns; otherwise, <see langword="false" />.</param>
		/// <returns>A formatted string that represents the Abstract Syntax Notation One (ASN.1)-encoded data.</returns>
		// Token: 0x060028BB RID: 10427 RVA: 0x000BA8C0 File Offset: 0x000B8AC0
		public virtual string Format(bool multiLine)
		{
			if (this.m_rawData == null || this.m_rawData.Length == 0)
			{
				return string.Empty;
			}
			string text = string.Empty;
			if (this.m_oid != null && this.m_oid.Value != null)
			{
				text = this.m_oid.Value;
			}
			return CAPI.CryptFormatObject(1U, multiLine ? 1U : 0U, text, this.m_rawData);
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000BA91F File Offset: 0x000B8B1F
		private void Reset(Oid oid, byte[] rawData)
		{
			this.Oid = oid;
			this.RawData = rawData;
		}

		// Token: 0x04002267 RID: 8807
		internal Oid m_oid;

		// Token: 0x04002268 RID: 8808
		internal byte[] m_rawData;
	}
}

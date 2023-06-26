using System;

namespace System.Text
{
	/// <summary>Provides basic information about an encoding.</summary>
	// Token: 0x02000A72 RID: 2674
	[Serializable]
	public sealed class EncodingInfo
	{
		// Token: 0x06006890 RID: 26768 RVA: 0x0016227E File Offset: 0x0016047E
		internal EncodingInfo(int codePage, string name, string displayName)
		{
			this.iCodePage = codePage;
			this.strEncodingName = name;
			this.strDisplayName = displayName;
		}

		/// <summary>Gets the code page identifier of the encoding.</summary>
		/// <returns>The code page identifier of the encoding.</returns>
		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06006891 RID: 26769 RVA: 0x0016229B File Offset: 0x0016049B
		public int CodePage
		{
			get
			{
				return this.iCodePage;
			}
		}

		/// <summary>Gets the name registered with the Internet Assigned Numbers Authority (IANA) for the encoding.</summary>
		/// <returns>The IANA name for the encoding. For more information about the IANA, see www.iana.org.</returns>
		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06006892 RID: 26770 RVA: 0x001622A3 File Offset: 0x001604A3
		public string Name
		{
			get
			{
				return this.strEncodingName;
			}
		}

		/// <summary>Gets the human-readable description of the encoding.</summary>
		/// <returns>The human-readable description of the encoding.</returns>
		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06006893 RID: 26771 RVA: 0x001622AB File Offset: 0x001604AB
		public string DisplayName
		{
			get
			{
				return this.strDisplayName;
			}
		}

		/// <summary>Returns a <see cref="T:System.Text.Encoding" /> object that corresponds to the current <see cref="T:System.Text.EncodingInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.Encoding" /> object that corresponds to the current <see cref="T:System.Text.EncodingInfo" /> object.</returns>
		// Token: 0x06006894 RID: 26772 RVA: 0x001622B3 File Offset: 0x001604B3
		public Encoding GetEncoding()
		{
			return Encoding.GetEncoding(this.iCodePage);
		}

		/// <summary>Gets a value indicating whether the specified object is equal to the current <see cref="T:System.Text.EncodingInfo" /> object.</summary>
		/// <param name="value">An object to compare to the current <see cref="T:System.Text.EncodingInfo" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is a <see cref="T:System.Text.EncodingInfo" /> object and is equal to the current <see cref="T:System.Text.EncodingInfo" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006895 RID: 26773 RVA: 0x001622C0 File Offset: 0x001604C0
		public override bool Equals(object value)
		{
			EncodingInfo encodingInfo = value as EncodingInfo;
			return encodingInfo != null && this.CodePage == encodingInfo.CodePage;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Text.EncodingInfo" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06006896 RID: 26774 RVA: 0x001622E7 File Offset: 0x001604E7
		public override int GetHashCode()
		{
			return this.CodePage;
		}

		// Token: 0x04002EBD RID: 11965
		private int iCodePage;

		// Token: 0x04002EBE RID: 11966
		private string strEncodingName;

		// Token: 0x04002EBF RID: 11967
		private string strDisplayName;
	}
}

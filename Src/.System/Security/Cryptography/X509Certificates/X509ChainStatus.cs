using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides a simple structure for storing X509 chain status and error information.</summary>
	// Token: 0x0200046B RID: 1131
	public struct X509ChainStatus
	{
		/// <summary>Specifies the status of the X509 chain.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatusFlags" /> value.</returns>
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002A11 RID: 10769 RVA: 0x000C0139 File Offset: 0x000BE339
		// (set) Token: 0x06002A12 RID: 10770 RVA: 0x000C0141 File Offset: 0x000BE341
		public X509ChainStatusFlags Status
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		/// <summary>Specifies a description of the <see cref="P:System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus" /> value.</summary>
		/// <returns>A localizable string.</returns>
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002A13 RID: 10771 RVA: 0x000C014A File Offset: 0x000BE34A
		// (set) Token: 0x06002A14 RID: 10772 RVA: 0x000C0160 File Offset: 0x000BE360
		public string StatusInformation
		{
			get
			{
				if (this.m_statusInformation == null)
				{
					return string.Empty;
				}
				return this.m_statusInformation;
			}
			set
			{
				this.m_statusInformation = value;
			}
		}

		// Token: 0x040025E3 RID: 9699
		private X509ChainStatusFlags m_status;

		// Token: 0x040025E4 RID: 9700
		private string m_statusInformation;
	}
}

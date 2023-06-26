using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an element of an X.509 chain.</summary>
	// Token: 0x0200046D RID: 1133
	public class X509ChainElement
	{
		// Token: 0x06002A27 RID: 10791 RVA: 0x000C0A6A File Offset: 0x000BEC6A
		private X509ChainElement()
		{
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000C0A74 File Offset: 0x000BEC74
		internal unsafe X509ChainElement(IntPtr pChainElement)
		{
			CAPIBase.CERT_CHAIN_ELEMENT cert_CHAIN_ELEMENT = new CAPIBase.CERT_CHAIN_ELEMENT(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_ELEMENT)));
			uint num = (uint)Marshal.ReadInt32(pChainElement);
			if ((ulong)num > (ulong)((long)Marshal.SizeOf(cert_CHAIN_ELEMENT)))
			{
				num = (uint)Marshal.SizeOf(cert_CHAIN_ELEMENT);
			}
			X509Utils.memcpy(pChainElement, new IntPtr((void*)(&cert_CHAIN_ELEMENT)), num);
			this.m_certificate = new X509Certificate2(cert_CHAIN_ELEMENT.pCertContext);
			if (cert_CHAIN_ELEMENT.pwszExtendedErrorInfo == IntPtr.Zero)
			{
				this.m_description = string.Empty;
			}
			else
			{
				this.m_description = Marshal.PtrToStringUni(cert_CHAIN_ELEMENT.pwszExtendedErrorInfo);
			}
			if (cert_CHAIN_ELEMENT.dwErrorStatus == 0U)
			{
				this.m_chainStatus = new X509ChainStatus[0];
				return;
			}
			this.m_chainStatus = X509Chain.GetChainStatusInformation(cert_CHAIN_ELEMENT.dwErrorStatus);
		}

		/// <summary>Gets the X.509 certificate at a particular chain element.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</returns>
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x000C0B36 File Offset: 0x000BED36
		public X509Certificate2 Certificate
		{
			get
			{
				return this.m_certificate;
			}
		}

		/// <summary>Gets the error status of the current X.509 certificate in a chain.</summary>
		/// <returns>An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatus" /> objects.</returns>
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x000C0B3E File Offset: 0x000BED3E
		public X509ChainStatus[] ChainElementStatus
		{
			get
			{
				return this.m_chainStatus;
			}
		}

		/// <summary>Gets additional error information from an unmanaged certificate chain structure.</summary>
		/// <returns>A string representing the <see langword="pwszExtendedErrorInfo" /> member of the unmanaged <see langword="CERT_CHAIN_ELEMENT" /> structure in the Crypto API.</returns>
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000C0B46 File Offset: 0x000BED46
		public string Information
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x040025ED RID: 9709
		private X509Certificate2 m_certificate;

		// Token: 0x040025EE RID: 9710
		private X509ChainStatus[] m_chainStatus;

		// Token: 0x040025EF RID: 9711
		private string m_description;
	}
}

using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x02000011 RID: 17
	internal class CmiAuthenticodeSignerInfo
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002843 File Offset: 0x00000A43
		internal CmiAuthenticodeSignerInfo()
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003DB0 File Offset: 0x00001FB0
		internal CmiAuthenticodeSignerInfo(int errorCode)
		{
			this.m_error = errorCode;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003DC0 File Offset: 0x00001FC0
		internal CmiAuthenticodeSignerInfo(Win32.AXL_SIGNER_INFO signerInfo, Win32.AXL_TIMESTAMPER_INFO timestamperInfo)
		{
			this.m_error = (int)signerInfo.dwError;
			if (signerInfo.pChainContext != IntPtr.Zero)
			{
				this.m_signerChain = new X509Chain(signerInfo.pChainContext);
			}
			this.m_algHash = signerInfo.algHash;
			if (signerInfo.pwszHash != IntPtr.Zero)
			{
				this.m_hash = Marshal.PtrToStringUni(signerInfo.pwszHash);
			}
			if (signerInfo.pwszDescription != IntPtr.Zero)
			{
				this.m_description = Marshal.PtrToStringUni(signerInfo.pwszDescription);
			}
			if (signerInfo.pwszDescriptionUrl != IntPtr.Zero)
			{
				this.m_descriptionUrl = Marshal.PtrToStringUni(signerInfo.pwszDescriptionUrl);
			}
			if (timestamperInfo.dwError != 2148204800U)
			{
				this.m_timestamperInfo = new CmiAuthenticodeTimestamperInfo(timestamperInfo);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003E90 File Offset: 0x00002090
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003E98 File Offset: 0x00002098
		internal int ErrorCode
		{
			get
			{
				return this.m_error;
			}
			set
			{
				this.m_error = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003EA1 File Offset: 0x000020A1
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003EA9 File Offset: 0x000020A9
		internal uint HashAlgId
		{
			get
			{
				return this.m_algHash;
			}
			set
			{
				this.m_algHash = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003EB2 File Offset: 0x000020B2
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003EBA File Offset: 0x000020BA
		internal string Hash
		{
			get
			{
				return this.m_hash;
			}
			set
			{
				this.m_hash = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003EC3 File Offset: 0x000020C3
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003ECB File Offset: 0x000020CB
		internal string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003ED4 File Offset: 0x000020D4
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003EDC File Offset: 0x000020DC
		internal string DescriptionUrl
		{
			get
			{
				return this.m_descriptionUrl;
			}
			set
			{
				this.m_descriptionUrl = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003EE5 File Offset: 0x000020E5
		internal CmiAuthenticodeTimestamperInfo TimestamperInfo
		{
			get
			{
				return this.m_timestamperInfo;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003EED File Offset: 0x000020ED
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003EF5 File Offset: 0x000020F5
		internal X509Chain SignerChain
		{
			get
			{
				return this.m_signerChain;
			}
			set
			{
				this.m_signerChain = value;
			}
		}

		// Token: 0x040000D3 RID: 211
		private int m_error;

		// Token: 0x040000D4 RID: 212
		private X509Chain m_signerChain;

		// Token: 0x040000D5 RID: 213
		private uint m_algHash;

		// Token: 0x040000D6 RID: 214
		private string m_hash;

		// Token: 0x040000D7 RID: 215
		private string m_description;

		// Token: 0x040000D8 RID: 216
		private string m_descriptionUrl;

		// Token: 0x040000D9 RID: 217
		private CmiAuthenticodeTimestamperInfo m_timestamperInfo;
	}
}

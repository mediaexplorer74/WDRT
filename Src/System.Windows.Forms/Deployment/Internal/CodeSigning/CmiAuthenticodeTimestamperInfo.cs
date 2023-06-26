using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x02000012 RID: 18
	internal class CmiAuthenticodeTimestamperInfo
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002843 File Offset: 0x00000A43
		private CmiAuthenticodeTimestamperInfo()
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003F00 File Offset: 0x00002100
		internal CmiAuthenticodeTimestamperInfo(Win32.AXL_TIMESTAMPER_INFO timestamperInfo)
		{
			this.m_error = (int)timestamperInfo.dwError;
			this.m_algHash = timestamperInfo.algHash;
			long num = (long)(((ulong)timestamperInfo.ftTimestamp.dwHighDateTime << 32) | (ulong)timestamperInfo.ftTimestamp.dwLowDateTime);
			this.m_timestampTime = DateTime.FromFileTime(num);
			if (timestamperInfo.pChainContext != IntPtr.Zero)
			{
				this.m_timestamperChain = new X509Chain(timestamperInfo.pChainContext);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003F77 File Offset: 0x00002177
		internal int ErrorCode
		{
			get
			{
				return this.m_error;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003F7F File Offset: 0x0000217F
		internal uint HashAlgId
		{
			get
			{
				return this.m_algHash;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003F87 File Offset: 0x00002187
		internal DateTime TimestampTime
		{
			get
			{
				return this.m_timestampTime;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003F8F File Offset: 0x0000218F
		internal X509Chain TimestamperChain
		{
			get
			{
				return this.m_timestamperChain;
			}
		}

		// Token: 0x040000DA RID: 218
		private int m_error;

		// Token: 0x040000DB RID: 219
		private X509Chain m_timestamperChain;

		// Token: 0x040000DC RID: 220
		private DateTime m_timestampTime;

		// Token: 0x040000DD RID: 221
		private uint m_algHash;
	}
}

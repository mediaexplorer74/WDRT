using System;
using System.Security.Cryptography;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x02000010 RID: 16
	internal class CmiStrongNameSignerInfo
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002843 File Offset: 0x00000A43
		internal CmiStrongNameSignerInfo()
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003D67 File Offset: 0x00001F67
		internal CmiStrongNameSignerInfo(int errorCode, string publicKeyToken)
		{
			this.m_error = errorCode;
			this.m_publicKeyToken = publicKeyToken;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003D7D File Offset: 0x00001F7D
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00003D85 File Offset: 0x00001F85
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003D8E File Offset: 0x00001F8E
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003D96 File Offset: 0x00001F96
		internal string PublicKeyToken
		{
			get
			{
				return this.m_publicKeyToken;
			}
			set
			{
				this.m_publicKeyToken = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003D9F File Offset: 0x00001F9F
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003DA7 File Offset: 0x00001FA7
		internal AsymmetricAlgorithm PublicKey
		{
			get
			{
				return this.m_snKey;
			}
			set
			{
				this.m_snKey = value;
			}
		}

		// Token: 0x040000D0 RID: 208
		private int m_error;

		// Token: 0x040000D1 RID: 209
		private string m_publicKeyToken;

		// Token: 0x040000D2 RID: 210
		private AsymmetricAlgorithm m_snKey;
	}
}

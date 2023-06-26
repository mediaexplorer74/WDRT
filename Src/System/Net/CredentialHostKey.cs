using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020000DD RID: 221
	internal class CredentialHostKey
	{
		// Token: 0x06000780 RID: 1920 RVA: 0x00029BB9 File Offset: 0x00027DB9
		internal CredentialHostKey(string host, int port, string authenticationType)
		{
			this.Host = host;
			this.Port = port;
			this.AuthenticationType = authenticationType;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00029BD6 File Offset: 0x00027DD6
		internal bool Match(string host, int port, string authenticationType)
		{
			return host != null && authenticationType != null && string.Compare(authenticationType, this.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Host, host, StringComparison.OrdinalIgnoreCase) == 0 && port == this.Port;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00029C10 File Offset: 0x00027E10
		public override int GetHashCode()
		{
			if (!this.m_ComputedHashCode)
			{
				this.m_HashCode = this.AuthenticationType.ToUpperInvariant().GetHashCode() + this.Host.ToUpperInvariant().GetHashCode() + this.Port.GetHashCode();
				this.m_ComputedHashCode = true;
			}
			return this.m_HashCode;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00029C68 File Offset: 0x00027E68
		public override bool Equals(object comparand)
		{
			CredentialHostKey credentialHostKey = comparand as CredentialHostKey;
			return comparand != null && (string.Compare(this.AuthenticationType, credentialHostKey.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Host, credentialHostKey.Host, StringComparison.OrdinalIgnoreCase) == 0) && this.Port == credentialHostKey.Port;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00029CBC File Offset: 0x00027EBC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.Host.Length.ToString(NumberFormatInfo.InvariantInfo),
				"]:",
				this.Host,
				":",
				this.Port.ToString(NumberFormatInfo.InvariantInfo),
				":",
				ValidationHelper.ToString(this.AuthenticationType)
			});
		}

		// Token: 0x04000D19 RID: 3353
		internal string Host;

		// Token: 0x04000D1A RID: 3354
		internal string AuthenticationType;

		// Token: 0x04000D1B RID: 3355
		internal int Port;

		// Token: 0x04000D1C RID: 3356
		private int m_HashCode;

		// Token: 0x04000D1D RID: 3357
		private bool m_ComputedHashCode;
	}
}

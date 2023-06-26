using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020000DE RID: 222
	internal class CredentialKey
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x00029D39 File Offset: 0x00027F39
		internal CredentialKey(Uri uriPrefix, string authenticationType)
		{
			this.UriPrefix = uriPrefix;
			this.UriPrefixLength = this.UriPrefix.ToString().Length;
			this.AuthenticationType = authenticationType;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00029D6C File Offset: 0x00027F6C
		internal bool Match(Uri uri, string authenticationType)
		{
			return !(uri == null) && authenticationType != null && string.Compare(authenticationType, this.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && this.IsPrefix(uri, this.UriPrefix);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00029D9C File Offset: 0x00027F9C
		internal bool IsPrefix(Uri uri, Uri prefixUri)
		{
			if (prefixUri.Scheme != uri.Scheme || prefixUri.Host != uri.Host || prefixUri.Port != uri.Port)
			{
				return false;
			}
			int num = prefixUri.AbsolutePath.LastIndexOf('/');
			return num <= uri.AbsolutePath.LastIndexOf('/') && string.Compare(uri.AbsolutePath, 0, prefixUri.AbsolutePath, 0, num, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00029E17 File Offset: 0x00028017
		public override int GetHashCode()
		{
			if (!this.m_ComputedHashCode)
			{
				this.m_HashCode = this.AuthenticationType.ToUpperInvariant().GetHashCode() + this.UriPrefixLength + this.UriPrefix.GetHashCode();
				this.m_ComputedHashCode = true;
			}
			return this.m_HashCode;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00029E58 File Offset: 0x00028058
		public override bool Equals(object comparand)
		{
			CredentialKey credentialKey = comparand as CredentialKey;
			return comparand != null && string.Compare(this.AuthenticationType, credentialKey.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && this.UriPrefix.Equals(credentialKey.UriPrefix);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00029E9C File Offset: 0x0002809C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.UriPrefixLength.ToString(NumberFormatInfo.InvariantInfo),
				"]:",
				ValidationHelper.ToString(this.UriPrefix),
				":",
				ValidationHelper.ToString(this.AuthenticationType)
			});
		}

		// Token: 0x04000D1E RID: 3358
		internal Uri UriPrefix;

		// Token: 0x04000D1F RID: 3359
		internal int UriPrefixLength = -1;

		// Token: 0x04000D20 RID: 3360
		internal string AuthenticationType;

		// Token: 0x04000D21 RID: 3361
		private int m_HashCode;

		// Token: 0x04000D22 RID: 3362
		private bool m_ComputedHashCode;
	}
}

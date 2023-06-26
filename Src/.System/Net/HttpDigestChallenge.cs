using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace System.Net
{
	// Token: 0x020001AC RID: 428
	internal class HttpDigestChallenge
	{
		// Token: 0x060010CE RID: 4302 RVA: 0x0005A724 File Offset: 0x00058924
		internal void SetFromRequest(HttpWebRequest httpWebRequest)
		{
			this.HostName = httpWebRequest.ChallengedUri.Host;
			this.Method = httpWebRequest.CurrentMethod.Name;
			if (httpWebRequest.CurrentMethod.ConnectRequest)
			{
				this.Uri = httpWebRequest.RequestUri.GetParts(UriComponents.HostAndPort, UriFormat.UriEscaped);
			}
			else
			{
				this.Uri = "/" + httpWebRequest.GetRemoteResourceUri().GetParts(UriComponents.Path, UriFormat.UriEscaped);
			}
			this.ChallengedUri = httpWebRequest.ChallengedUri;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0005A7A4 File Offset: 0x000589A4
		internal HttpDigestChallenge CopyAndIncrementNonce()
		{
			HttpDigestChallenge httpDigestChallenge = null;
			lock (this)
			{
				httpDigestChallenge = base.MemberwiseClone() as HttpDigestChallenge;
				this.NonceCount++;
			}
			httpDigestChallenge.MD5provider = new MD5CryptoServiceProvider();
			return httpDigestChallenge;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0005A804 File Offset: 0x00058A04
		public bool defineAttribute(string name, string value)
		{
			name = name.Trim().ToLower(CultureInfo.InvariantCulture);
			if (name.Equals("algorithm"))
			{
				this.Algorithm = value;
			}
			else if (name.Equals("cnonce"))
			{
				this.ClientNonce = value;
			}
			else if (name.Equals("nc"))
			{
				this.NonceCount = int.Parse(value, NumberFormatInfo.InvariantInfo);
			}
			else if (name.Equals("nonce"))
			{
				this.Nonce = value;
			}
			else if (name.Equals("opaque"))
			{
				this.Opaque = value;
			}
			else if (name.Equals("qop"))
			{
				this.QualityOfProtection = value;
				this.QopPresent = this.QualityOfProtection != null && this.QualityOfProtection.Length > 0;
			}
			else if (name.Equals("realm"))
			{
				this.Realm = value;
			}
			else if (name.Equals("domain"))
			{
				this.Domain = value;
			}
			else if (!name.Equals("response"))
			{
				if (name.Equals("stale"))
				{
					this.Stale = value.ToLower(CultureInfo.InvariantCulture).Equals("true");
				}
				else if (name.Equals("uri"))
				{
					this.Uri = value;
				}
				else if (name.Equals("charset"))
				{
					this.Charset = value;
				}
				else if (!name.Equals("cipher") && !name.Equals("username"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0005A994 File Offset: 0x00058B94
		internal string ToBlob()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(HttpDigest.pair("realm", this.Realm, true));
			if (this.Algorithm != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("algorithm", this.Algorithm, true));
			}
			if (this.Charset != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("charset", this.Charset, false));
			}
			if (this.Nonce != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("nonce", this.Nonce, true));
			}
			if (this.Uri != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("uri", this.Uri, true));
			}
			if (this.ClientNonce != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("cnonce", this.ClientNonce, true));
			}
			if (this.NonceCount > 0)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("nc", this.NonceCount.ToString("x8", NumberFormatInfo.InvariantInfo), true));
			}
			if (this.QualityOfProtection != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("qop", this.QualityOfProtection, true));
			}
			if (this.Opaque != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("opaque", this.Opaque, true));
			}
			if (this.Domain != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("domain", this.Domain, true));
			}
			if (this.Stale)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("stale", "true", true));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040013A0 RID: 5024
		internal string HostName;

		// Token: 0x040013A1 RID: 5025
		internal string Realm;

		// Token: 0x040013A2 RID: 5026
		internal Uri ChallengedUri;

		// Token: 0x040013A3 RID: 5027
		internal string Uri;

		// Token: 0x040013A4 RID: 5028
		internal string Nonce;

		// Token: 0x040013A5 RID: 5029
		internal string Opaque;

		// Token: 0x040013A6 RID: 5030
		internal bool Stale;

		// Token: 0x040013A7 RID: 5031
		internal string Algorithm;

		// Token: 0x040013A8 RID: 5032
		internal string Method;

		// Token: 0x040013A9 RID: 5033
		internal string Domain;

		// Token: 0x040013AA RID: 5034
		internal string QualityOfProtection;

		// Token: 0x040013AB RID: 5035
		internal string ClientNonce;

		// Token: 0x040013AC RID: 5036
		internal int NonceCount;

		// Token: 0x040013AD RID: 5037
		internal string Charset;

		// Token: 0x040013AE RID: 5038
		internal string ServiceName;

		// Token: 0x040013AF RID: 5039
		internal string ChannelBinding;

		// Token: 0x040013B0 RID: 5040
		internal bool UTF8Charset;

		// Token: 0x040013B1 RID: 5041
		internal bool QopPresent;

		// Token: 0x040013B2 RID: 5042
		internal MD5CryptoServiceProvider MD5provider = new MD5CryptoServiceProvider();
	}
}

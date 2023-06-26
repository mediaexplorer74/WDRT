using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000085 RID: 133
	internal class HeaderCollection
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x000136F9 File Offset: 0x000118F9
		internal HeaderCollection(IEnumerable<KeyValuePair<string, string>> headers)
			: this()
		{
			this.SetHeaders(headers);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00013708 File Offset: 0x00011908
		internal HeaderCollection(IODataResponseMessage responseMessage)
			: this()
		{
			if (responseMessage != null)
			{
				this.SetHeaders(responseMessage.Headers);
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00013720 File Offset: 0x00011920
		internal HeaderCollection(WebHeaderCollection headers)
			: this()
		{
			foreach (string text in headers.AllKeys)
			{
				this.SetHeader(text, headers[text]);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001375A File Offset: 0x0001195A
		internal HeaderCollection()
		{
			this.headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00013772 File Offset: 0x00011972
		internal IDictionary<string, string> UnderlyingDictionary
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001377A File Offset: 0x0001197A
		internal IEnumerable<string> HeaderNames
		{
			get
			{
				return this.headers.Keys;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00013787 File Offset: 0x00011987
		internal void SetDefaultHeaders()
		{
			this.SetUserAgent();
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001378F File Offset: 0x0001198F
		internal bool TryGetHeader(string headerName, out string headerValue)
		{
			return this.headers.TryGetValue(headerName, out headerValue);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000137A0 File Offset: 0x000119A0
		internal string GetHeader(string headerName)
		{
			string text;
			if (!this.TryGetHeader(headerName, out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000137BB File Offset: 0x000119BB
		internal void SetHeader(string headerName, string headerValue)
		{
			if (headerValue == null)
			{
				this.headers.Remove(headerName);
				return;
			}
			this.headers[headerName] = headerValue;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000137DB File Offset: 0x000119DB
		internal void SetHeaders(IEnumerable<KeyValuePair<string, string>> headersToSet)
		{
			this.headers.SetRange(headersToSet);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000137E9 File Offset: 0x000119E9
		internal IEnumerable<KeyValuePair<string, string>> AsEnumerable()
		{
			return this.headers.AsEnumerable<KeyValuePair<string, string>>();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000137F8 File Offset: 0x000119F8
		internal void SetRequestVersion(Version requestVersion, Version maxProtocolVersion)
		{
			if (requestVersion != null)
			{
				if (requestVersion > maxProtocolVersion)
				{
					string text = Strings.Context_RequestVersionIsBiggerThanProtocolVersion(requestVersion.ToString(), maxProtocolVersion.ToString());
					throw Error.InvalidOperation(text);
				}
				if (requestVersion.Major > 0)
				{
					Version dataServiceVersion = this.GetDataServiceVersion();
					if (dataServiceVersion == null || requestVersion > dataServiceVersion)
					{
						this.SetHeader("DataServiceVersion", requestVersion + ";NetFx");
					}
				}
			}
			this.SetHeader("MaxDataServiceVersion", maxProtocolVersion + ";NetFx");
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001387E File Offset: 0x00011A7E
		internal void SetHeaderIfUnset(string headerToSet, string headerValue)
		{
			if (this.GetHeader(headerToSet) == null)
			{
				this.SetHeader(headerToSet, headerValue);
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00013891 File Offset: 0x00011A91
		internal void SetUserAgent()
		{
			this.SetHeader("User-Agent", "Microsoft ADO.NET Data Services");
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000138A3 File Offset: 0x00011AA3
		internal HeaderCollection Copy()
		{
			return new HeaderCollection(this.headers);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000138B0 File Offset: 0x00011AB0
		private Version GetDataServiceVersion()
		{
			string text;
			if (!this.TryGetHeader("DataServiceVersion", out text))
			{
				return null;
			}
			if (text.EndsWith(";NetFx", StringComparison.OrdinalIgnoreCase))
			{
				text = text.Substring(0, text.IndexOf(";NetFx", StringComparison.Ordinal));
			}
			return Version.Parse(text);
		}

		// Token: 0x040002EA RID: 746
		private readonly IDictionary<string, string> headers;
	}
}

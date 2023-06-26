using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x020001E7 RID: 487
	internal sealed class ODataBatchUrlResolver : IODataUrlResolver
	{
		// Token: 0x06000F0A RID: 3850 RVA: 0x00035C10 File Offset: 0x00033E10
		internal ODataBatchUrlResolver(IODataUrlResolver batchMessageUrlResolver)
		{
			this.batchMessageUrlResolver = batchMessageUrlResolver;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x00035C1F File Offset: 0x00033E1F
		internal IODataUrlResolver BatchMessageUrlResolver
		{
			get
			{
				return this.batchMessageUrlResolver;
			}
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00035C28 File Offset: 0x00033E28
		Uri IODataUrlResolver.ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(payloadUri, "payloadUri");
			if (this.contentIdCache != null && !payloadUri.IsAbsoluteUri)
			{
				string text = UriUtilsCommon.UriToString(payloadUri);
				if (text.Length > 0 && text[0] == '$')
				{
					int num = text.IndexOf('/', 1);
					string text2;
					if (num > 0)
					{
						text2 = text.Substring(1, num - 1);
					}
					else
					{
						text2 = text.Substring(1);
					}
					if (this.contentIdCache.Contains(text2))
					{
						return payloadUri;
					}
				}
			}
			if (this.batchMessageUrlResolver != null)
			{
				return this.batchMessageUrlResolver.ResolveUrl(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00035CB5 File Offset: 0x00033EB5
		internal void AddContentId(string contentId)
		{
			if (this.contentIdCache == null)
			{
				this.contentIdCache = new HashSet<string>(StringComparer.Ordinal);
			}
			this.contentIdCache.Add(contentId);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00035CDC File Offset: 0x00033EDC
		internal bool ContainsContentId(string contentId)
		{
			return this.contentIdCache != null && this.contentIdCache.Contains(contentId);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00035CF4 File Offset: 0x00033EF4
		internal void Reset()
		{
			if (this.contentIdCache != null)
			{
				this.contentIdCache.Clear();
			}
		}

		// Token: 0x04000536 RID: 1334
		private readonly IODataUrlResolver batchMessageUrlResolver;

		// Token: 0x04000537 RID: 1335
		private HashSet<string> contentIdCache;
	}
}

using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000128 RID: 296
	internal static class UriUtil
	{
		// Token: 0x060009EC RID: 2540 RVA: 0x00028529 File Offset: 0x00026729
		internal static string UriToString(Uri uri)
		{
			if (!uri.IsAbsoluteUri)
			{
				return uri.OriginalString;
			}
			return uri.AbsoluteUri;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00028540 File Offset: 0x00026740
		internal static Uri CreateUri(string value, UriKind kind)
		{
			if (value != null)
			{
				return new Uri(value, kind);
			}
			return null;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00028550 File Offset: 0x00026750
		internal static Uri CreateUri(Uri baseUri, Uri requestUri)
		{
			Util.CheckArgumentNull<Uri>(requestUri, "requestUri");
			if (!baseUri.IsAbsoluteUri)
			{
				return UriUtil.CreateUri(UriUtil.UriToString(baseUri) + '/' + UriUtil.UriToString(requestUri), UriKind.Relative);
			}
			if (requestUri.IsAbsoluteUri)
			{
				return requestUri;
			}
			return UriUtil.AppendBaseUriAndRelativeUri(baseUri, requestUri);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000285A4 File Offset: 0x000267A4
		private static Uri AppendBaseUriAndRelativeUri(Uri baseUri, Uri relativeUri)
		{
			string text = UriUtil.UriToString(baseUri);
			string text2 = UriUtil.UriToString(relativeUri);
			if (text.EndsWith("/", StringComparison.Ordinal))
			{
				if (text2.StartsWith("/", StringComparison.Ordinal))
				{
					relativeUri = new Uri(baseUri, UriUtil.CreateUri(text2.TrimStart(UriUtil.ForwardSlash), UriKind.Relative));
				}
				else
				{
					relativeUri = new Uri(baseUri, relativeUri);
				}
			}
			else
			{
				relativeUri = UriUtil.CreateUri(text + "/" + text2.TrimStart(UriUtil.ForwardSlash), UriKind.Absolute);
			}
			return relativeUri;
		}

		// Token: 0x040005AB RID: 1451
		internal static readonly char[] ForwardSlash = new char[] { '/' };
	}
}

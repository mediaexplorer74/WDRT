using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000296 RID: 662
	internal static class UriUtils
	{
		// Token: 0x06001655 RID: 5717 RVA: 0x00051B4E File Offset: 0x0004FD4E
		internal static Uri UriToAbsoluteUri(Uri baseUri, Uri relativeUri)
		{
			return new Uri(baseUri, relativeUri);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00051B58 File Offset: 0x0004FD58
		internal static Uri EnsureEscapedRelativeUri(Uri uri)
		{
			string components = uri.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped);
			if (string.CompareOrdinal(uri.OriginalString, components) == 0)
			{
				return uri;
			}
			return new Uri(components, UriKind.Relative);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00051B89 File Offset: 0x0004FD89
		internal static string EnsureEscapedFragment(string fragmentString)
		{
			return new Uri(UriUtils.ExampleMetadataAbsoluteUri, fragmentString).Fragment;
		}

		// Token: 0x040008CE RID: 2254
		private static Uri ExampleMetadataAbsoluteUri = new Uri("http://www.example.com/$metadata", UriKind.Absolute);
	}
}

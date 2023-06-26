using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000E0 RID: 224
	internal sealed class UriPathParser
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x000134EE File Offset: 0x000116EE
		internal UriPathParser(int maxSegments)
		{
			this.maxSegments = maxSegments;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00013500 File Offset: 0x00011700
		internal string[] ParsePath(string escapedRelativePathUri)
		{
			if (escapedRelativePathUri == null || string.IsNullOrEmpty(escapedRelativePathUri.Trim()))
			{
				return new string[0];
			}
			string[] array = escapedRelativePathUri.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length >= this.maxSegments)
			{
				throw new ODataException(Strings.UriQueryPathParser_TooManySegments);
			}
			return array;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00013550 File Offset: 0x00011750
		internal ICollection<string> ParsePathIntoSegments(Uri absoluteUri, Uri serviceBaseUri)
		{
			if (!UriUtils.UriInvariantInsensitiveIsBaseOf(serviceBaseUri, absoluteUri))
			{
				throw new ODataException(Strings.UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri(absoluteUri, serviceBaseUri));
			}
			ICollection<string> collection;
			try
			{
				int num = serviceBaseUri.Segments.Length;
				string[] segments = absoluteUri.Segments;
				List<string> list = new List<string>();
				for (int i = num; i < segments.Length; i++)
				{
					string text = segments[i];
					if (text.Length != 0 && text != "/")
					{
						if (text[text.Length - 1] == '/')
						{
							text = text.Substring(0, text.Length - 1);
						}
						if (list.Count == this.maxSegments)
						{
							throw new ODataException(Strings.UriQueryPathParser_TooManySegments);
						}
						list.Add(Uri.UnescapeDataString(text));
					}
				}
				collection = list.ToArray();
			}
			catch (UriFormatException ex)
			{
				throw new ODataException(Strings.UriQueryPathParser_SyntaxError, ex);
			}
			return collection;
		}

		// Token: 0x04000256 RID: 598
		private readonly int maxSegments;
	}
}

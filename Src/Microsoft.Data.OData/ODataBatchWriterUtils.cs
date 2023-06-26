using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x02000267 RID: 615
	internal static class ODataBatchWriterUtils
	{
		// Token: 0x06001452 RID: 5202 RVA: 0x0004BF34 File Offset: 0x0004A134
		internal static string CreateBatchBoundary(bool isResponse)
		{
			string text = (isResponse ? "batchresponse_{0}" : "batch_{0}");
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { Guid.NewGuid().ToString() });
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0004BF7C File Offset: 0x0004A17C
		internal static string CreateChangeSetBoundary(bool isResponse)
		{
			string text = (isResponse ? "changesetresponse_{0}" : "changeset_{0}");
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { Guid.NewGuid().ToString() });
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0004BFC4 File Offset: 0x0004A1C4
		internal static string CreateMultipartMixedContentType(string boundary)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}; {1}={2}", new object[] { "multipart/mixed", "boundary", boundary });
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0004BFFC File Offset: 0x0004A1FC
		internal static void WriteStartBoundary(TextWriter writer, string boundary, bool firstBoundary)
		{
			if (!firstBoundary)
			{
				writer.WriteLine();
			}
			writer.WriteLine("--{0}", boundary);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0004C013 File Offset: 0x0004A213
		internal static void WriteEndBoundary(TextWriter writer, string boundary, bool missingStartBoundary)
		{
			if (!missingStartBoundary)
			{
				writer.WriteLine();
			}
			writer.Write("--{0}--", boundary);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0004C02C File Offset: 0x0004A22C
		internal static void WriteRequestPreamble(TextWriter writer, string httpMethod, Uri uri)
		{
			writer.WriteLine("{0}: {1}", "Content-Type", "application/http");
			writer.WriteLine("{0}: {1}", "Content-Transfer-Encoding", "binary");
			writer.WriteLine();
			writer.WriteLine("{0} {1} {2}", httpMethod, UriUtilsCommon.UriToString(uri), "HTTP/1.1");
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0004C080 File Offset: 0x0004A280
		internal static void WriteResponsePreamble(TextWriter writer)
		{
			writer.WriteLine("{0}: {1}", "Content-Type", "application/http");
			writer.WriteLine("{0}: {1}", "Content-Transfer-Encoding", "binary");
			writer.WriteLine();
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0004C0B4 File Offset: 0x0004A2B4
		internal static void WriteChangeSetPreamble(TextWriter writer, string changeSetBoundary)
		{
			string text = ODataBatchWriterUtils.CreateMultipartMixedContentType(changeSetBoundary);
			writer.WriteLine("{0}: {1}", "Content-Type", text);
			writer.WriteLine();
		}
	}
}

using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001D8 RID: 472
	internal static class ODataBatchUtils
	{
		// Token: 0x06000EA7 RID: 3751 RVA: 0x00033790 File Offset: 0x00031990
		internal static Uri CreateOperationRequestUri(Uri uri, Uri baseUri, IODataUrlResolver urlResolver)
		{
			Uri uri2;
			if (urlResolver != null)
			{
				uri2 = urlResolver.ResolveUrl(baseUri, uri);
				if (uri2 != null)
				{
					return uri2;
				}
			}
			if (uri.IsAbsoluteUri)
			{
				uri2 = uri;
			}
			else
			{
				if (baseUri == null)
				{
					string text = (UriUtilsCommon.UriToString(uri).StartsWith("$", StringComparison.Ordinal) ? Strings.ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified(UriUtilsCommon.UriToString(uri)) : Strings.ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified(UriUtilsCommon.UriToString(uri)));
					throw new ODataException(text);
				}
				uri2 = UriUtils.UriToAbsoluteUri(baseUri, uri);
			}
			return uri2;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00033808 File Offset: 0x00031A08
		internal static ODataBatchOperationReadStream CreateBatchOperationReadStream(ODataBatchReaderStream batchReaderStream, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener)
		{
			string text;
			if (!headers.TryGetValue("Content-Length", out text))
			{
				return ODataBatchOperationReadStream.Create(batchReaderStream, operationListener);
			}
			int num = int.Parse(text, CultureInfo.InvariantCulture);
			if (num < 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidContentLengthSpecified(text));
			}
			return ODataBatchOperationReadStream.Create(batchReaderStream, operationListener, num);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00033850 File Offset: 0x00031A50
		internal static ODataBatchOperationWriteStream CreateBatchOperationWriteStream(Stream outputStream, IODataBatchOperationListener operationListener)
		{
			return new ODataBatchOperationWriteStream(outputStream, operationListener);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003385C File Offset: 0x00031A5C
		internal static void EnsureArraySize(ref byte[] buffer, int numberOfBytesInBuffer, int requiredByteCount)
		{
			int num = buffer.Length - numberOfBytesInBuffer;
			if (requiredByteCount <= num)
			{
				return;
			}
			int num2 = requiredByteCount - num;
			byte[] array = buffer;
			buffer = new byte[buffer.Length + num2];
			Buffer.BlockCopy(array, 0, buffer, 0, numberOfBytesInBuffer);
		}
	}
}

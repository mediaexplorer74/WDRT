using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200029C RID: 668
	public static class ODataConstants
	{
		// Token: 0x04000908 RID: 2312
		public const string MethodGet = "GET";

		// Token: 0x04000909 RID: 2313
		public const string MethodPost = "POST";

		// Token: 0x0400090A RID: 2314
		public const string MethodPut = "PUT";

		// Token: 0x0400090B RID: 2315
		public const string MethodDelete = "DELETE";

		// Token: 0x0400090C RID: 2316
		public const string MethodPatch = "PATCH";

		// Token: 0x0400090D RID: 2317
		public const string MethodMerge = "MERGE";

		// Token: 0x0400090E RID: 2318
		public const string ContentTypeHeader = "Content-Type";

		// Token: 0x0400090F RID: 2319
		public const string DataServiceVersionHeader = "DataServiceVersion";

		// Token: 0x04000910 RID: 2320
		public const string ContentIdHeader = "Content-ID";

		// Token: 0x04000911 RID: 2321
		internal const string ContentLengthHeader = "Content-Length";

		// Token: 0x04000912 RID: 2322
		internal const string HttpQValueParameter = "q";

		// Token: 0x04000913 RID: 2323
		internal const string HttpVersionInBatching = "HTTP/1.1";

		// Token: 0x04000914 RID: 2324
		internal const string Charset = "charset";

		// Token: 0x04000915 RID: 2325
		internal const string HttpMultipartBoundary = "boundary";

		// Token: 0x04000916 RID: 2326
		internal const string ContentTransferEncoding = "Content-Transfer-Encoding";

		// Token: 0x04000917 RID: 2327
		internal const string BatchContentTransferEncoding = "binary";

		// Token: 0x04000918 RID: 2328
		internal const ODataVersion ODataDefaultProtocolVersion = ODataVersion.V3;

		// Token: 0x04000919 RID: 2329
		internal const string BatchRequestBoundaryTemplate = "batch_{0}";

		// Token: 0x0400091A RID: 2330
		internal const string BatchResponseBoundaryTemplate = "batchresponse_{0}";

		// Token: 0x0400091B RID: 2331
		internal const string RequestChangeSetBoundaryTemplate = "changeset_{0}";

		// Token: 0x0400091C RID: 2332
		internal const string ResponseChangeSetBoundaryTemplate = "changesetresponse_{0}";

		// Token: 0x0400091D RID: 2333
		internal const string HttpWeakETagPrefix = "W/\"";

		// Token: 0x0400091E RID: 2334
		internal const string HttpWeakETagSuffix = "\"";

		// Token: 0x0400091F RID: 2335
		internal const int DefaultMaxRecursionDepth = 100;

		// Token: 0x04000920 RID: 2336
		internal const long DefaultMaxReadMessageSize = 1048576L;

		// Token: 0x04000921 RID: 2337
		internal const int DefaultMaxPartsPerBatch = 100;

		// Token: 0x04000922 RID: 2338
		internal const int DefulatMaxOperationsPerChangeset = 1000;

		// Token: 0x04000923 RID: 2339
		internal const int DefaultMaxEntityPropertyMappingsPerType = 100;

		// Token: 0x04000924 RID: 2340
		internal const ODataVersion MaxODataVersion = ODataVersion.V3;

		// Token: 0x04000925 RID: 2341
		internal const string UriSegmentSeparator = "/";

		// Token: 0x04000926 RID: 2342
		internal const char UriSegmentSeparatorChar = '/';

		// Token: 0x04000927 RID: 2343
		internal const string AssociationLinkSegmentName = "$links";

		// Token: 0x04000928 RID: 2344
		internal const string DefaultStreamSegmentName = "$value";
	}
}

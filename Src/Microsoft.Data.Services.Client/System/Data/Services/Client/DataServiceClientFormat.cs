using System;
using System.Data.Services.Common;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000059 RID: 89
	public sealed class DataServiceClientFormat
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000DC40 File Offset: 0x0000BE40
		internal DataServiceClientFormat(DataServiceContext context)
		{
			this.ODataFormat = ODataFormat.Atom;
			this.context = context;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000DC5A File Offset: 0x0000BE5A
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000DC62 File Offset: 0x0000BE62
		public ODataFormat ODataFormat { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000DC6B File Offset: 0x0000BE6B
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000DC73 File Offset: 0x0000BE73
		public Func<IEdmModel> LoadServiceModel { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		internal bool UsingAtom
		{
			get
			{
				return this.ODataFormat == ODataFormat.Atom;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000DC8B File Offset: 0x0000BE8B
		internal ODataFormat UriLiteralFormat
		{
			get
			{
				if (!this.UsingAtom)
				{
					return ODataFormat.Json;
				}
				return ODataFormat.VerboseJson;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000DCA0 File Offset: 0x0000BEA0
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		internal IEdmModel ServiceModel { get; private set; }

		// Token: 0x060002F6 RID: 758 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
		public void UseJson(IEdmModel serviceModel)
		{
			Util.CheckArgumentNull<IEdmModel>(serviceModel, "serviceModel");
			if (this.context.HasAtomEventHandlers)
			{
				throw new InvalidOperationException(Strings.DataServiceClientFormat_AtomEventsOnlySupportedWithAtomFormat);
			}
			if (this.context.MaxProtocolVersion < DataServiceProtocolVersion.V3)
			{
				throw new InvalidOperationException(Strings.DataServiceClientFormat_JsonUnsupportedForLessThanV3);
			}
			this.ODataFormat = ODataFormat.Json;
			this.ServiceModel = serviceModel;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000DD10 File Offset: 0x0000BF10
		public void UseJson()
		{
			IEdmModel edmModel = null;
			if (this.LoadServiceModel != null)
			{
				edmModel = this.LoadServiceModel();
			}
			if (edmModel == null)
			{
				throw new InvalidOperationException(Strings.DataServiceClientFormat_LoadServiceModelRequired);
			}
			this.UseJson(edmModel);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000DD48 File Offset: 0x0000BF48
		public void UseAtom()
		{
			this.ODataFormat = ODataFormat.Atom;
			this.ServiceModel = null;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000DD5C File Offset: 0x0000BF5C
		internal void SetRequestAcceptHeader(HeaderCollection headers)
		{
			this.SetAcceptHeaderAndCharset(headers, this.ChooseMediaType("application/atom+xml,application/xml", false));
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000DD71 File Offset: 0x0000BF71
		internal void SetRequestAcceptHeaderForQuery(HeaderCollection headers, QueryComponents components)
		{
			this.SetAcceptHeaderAndCharset(headers, this.ChooseMediaType("application/atom+xml,application/xml", components.HasSelectQueryOption));
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000DD8B File Offset: 0x0000BF8B
		internal void SetRequestAcceptHeaderForStream(HeaderCollection headers)
		{
			this.SetAcceptHeaderAndCharset(headers, "*/*");
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000DD99 File Offset: 0x0000BF99
		internal void SetRequestAcceptHeaderForCount(HeaderCollection headers)
		{
			this.SetAcceptHeaderAndCharset(headers, "text/plain");
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000DDA7 File Offset: 0x0000BFA7
		internal void SetRequestAcceptHeaderForBatch(HeaderCollection headers)
		{
			this.SetAcceptHeaderAndCharset(headers, "multipart/mixed");
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000DDB5 File Offset: 0x0000BFB5
		internal void SetRequestContentTypeForEntry(HeaderCollection headers)
		{
			this.SetRequestContentTypeHeader(headers, this.ChooseMediaType("application/atom+xml", false));
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000DDCA File Offset: 0x0000BFCA
		internal void SetRequestContentTypeForOperationParameters(HeaderCollection headers)
		{
			this.SetRequestContentTypeHeader(headers, this.ChooseMediaType("application/json;odata=verbose", false));
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000DDDF File Offset: 0x0000BFDF
		internal void SetRequestContentTypeForLinks(HeaderCollection headers)
		{
			this.SetRequestContentTypeHeader(headers, this.ChooseMediaType("application/xml", false));
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		internal void ValidateCanWriteRequestFormat(IODataRequestMessage requestMessage, bool isParameterPayload)
		{
			string header = requestMessage.GetHeader("Content-Type");
			this.ValidateContentType(header, isParameterPayload);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000DE18 File Offset: 0x0000C018
		internal void ValidateCanReadResponseFormat(IODataResponseMessage responseMessage)
		{
			string header = responseMessage.GetHeader("Content-Type");
			this.ValidateContentType(header, false);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000DE39 File Offset: 0x0000C039
		private static void ThrowInvalidOperationExceptionForJsonLightWithoutModel()
		{
			throw new InvalidOperationException(Strings.DataServiceClientFormat_ValidServiceModelRequiredForJson);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000DE45 File Offset: 0x0000C045
		private static void ThrowNotSupportedExceptionForJsonVerbose(string contentType)
		{
			throw new NotSupportedException(Strings.DataServiceClientFormat_JsonVerboseUnsupported(contentType));
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000DE7C File Offset: 0x0000C07C
		private void ValidateContentType(string contentType, bool isParameterPayload)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				return;
			}
			string text;
			ContentTypeUtil.MediaParameter[] array = ContentTypeUtil.ReadContentType(contentType, out text);
			if ("application/json".Equals(text, StringComparison.OrdinalIgnoreCase))
			{
				if (this.context.MaxProtocolVersion >= DataServiceProtocolVersion.V3)
				{
					if (array != null)
					{
						if (array.Any((ContentTypeUtil.MediaParameter p) => p.Name.Equals("odata", StringComparison.OrdinalIgnoreCase) && p.Value.Equals("verbose", StringComparison.OrdinalIgnoreCase)))
						{
							goto IL_56;
						}
					}
					if (this.ServiceModel == null)
					{
						DataServiceClientFormat.ThrowInvalidOperationExceptionForJsonLightWithoutModel();
						return;
					}
					return;
				}
				IL_56:
				if (!isParameterPayload)
				{
					DataServiceClientFormat.ThrowNotSupportedExceptionForJsonVerbose(contentType);
					return;
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000DEF6 File Offset: 0x0000C0F6
		private void SetRequestContentTypeHeader(HeaderCollection headers, string mediaType)
		{
			if (mediaType == "application/json;odata=minimalmetadata")
			{
				headers.SetRequestVersion(Util.DataServiceVersion3, this.context.MaxProtocolVersionAsVersion);
			}
			headers.SetHeaderIfUnset("Content-Type", mediaType);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000DF27 File Offset: 0x0000C127
		private void SetAcceptHeaderAndCharset(HeaderCollection headers, string mediaType)
		{
			headers.SetHeaderIfUnset("Accept", mediaType);
			headers.SetHeaderIfUnset("Accept-Charset", "UTF-8");
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000DF45 File Offset: 0x0000C145
		private string ChooseMediaType(string valueIfUsingAtom, bool hasSelectQueryOption)
		{
			if (this.UsingAtom)
			{
				return valueIfUsingAtom;
			}
			if (hasSelectQueryOption)
			{
				return "application/json;odata=fullmetadata";
			}
			return "application/json;odata=minimalmetadata";
		}

		// Token: 0x04000267 RID: 615
		private const string MimeApplicationAtom = "application/atom+xml";

		// Token: 0x04000268 RID: 616
		private const string MimeApplicationJson = "application/json";

		// Token: 0x04000269 RID: 617
		private const string MimeApplicationJsonODataLight = "application/json;odata=minimalmetadata";

		// Token: 0x0400026A RID: 618
		private const string MimeApplicationJsonODataLightWithAllMetadata = "application/json;odata=fullmetadata";

		// Token: 0x0400026B RID: 619
		private const string MimeApplicationJsonODataVerbose = "application/json;odata=verbose";

		// Token: 0x0400026C RID: 620
		private const string MimeODataParameterVerboseValue = "verbose";

		// Token: 0x0400026D RID: 621
		private const string MimeMultiPartMixed = "multipart/mixed";

		// Token: 0x0400026E RID: 622
		private const string MimeApplicationXml = "application/xml";

		// Token: 0x0400026F RID: 623
		private const string MimeApplicationAtomOrXml = "application/atom+xml,application/xml";

		// Token: 0x04000270 RID: 624
		private const string Utf8Encoding = "UTF-8";

		// Token: 0x04000271 RID: 625
		private const string HttpAcceptCharset = "Accept-Charset";

		// Token: 0x04000272 RID: 626
		private readonly DataServiceContext context;
	}
}

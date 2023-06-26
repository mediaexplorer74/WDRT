using System;
using System.Diagnostics;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001B8 RID: 440
	internal abstract class ODataVerboseJsonDeserializer : ODataDeserializer
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x0002F8B9 File Offset: 0x0002DAB9
		protected ODataVerboseJsonDeserializer(ODataVerboseJsonInputContext jsonInputContext)
			: base(jsonInputContext)
		{
			this.jsonInputContext = jsonInputContext;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0002F8C9 File Offset: 0x0002DAC9
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonInputContext.JsonReader;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0002F8D6 File Offset: 0x0002DAD6
		protected ODataVerboseJsonInputContext VerboseJsonInputContext
		{
			get
			{
				return this.jsonInputContext;
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002F8DE File Offset: 0x0002DADE
		internal void ReadPayloadStart(bool isReadingNestedPayload)
		{
			this.ReadPayloadStart(isReadingNestedPayload, true);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002F8E8 File Offset: 0x0002DAE8
		internal void ReadPayloadStart(bool isReadingNestedPayload, bool expectResponseWrapper)
		{
			if (!isReadingNestedPayload)
			{
				this.JsonReader.Read();
			}
			if (base.ReadingResponse && expectResponseWrapper)
			{
				this.JsonReader.ReadStartObject();
				while (this.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = this.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal("d", text) == 0)
					{
						break;
					}
					this.JsonReader.SkipValue();
				}
				if (this.JsonReader.NodeType == JsonNodeType.EndObject)
				{
					throw new ODataException(Strings.ODataJsonDeserializer_DataWrapperPropertyNotFound);
				}
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002F967 File Offset: 0x0002DB67
		internal void ReadPayloadEnd(bool isReadingNestedPayload)
		{
			this.ReadPayloadEnd(isReadingNestedPayload, true);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0002F974 File Offset: 0x0002DB74
		internal void ReadPayloadEnd(bool isReadingNestedPayload, bool expectResponseWrapper)
		{
			if (base.ReadingResponse && expectResponseWrapper)
			{
				while (this.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = this.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal("d", text) == 0)
					{
						throw new ODataException(Strings.ODataJsonDeserializer_DataWrapperMultipleProperties);
					}
					this.JsonReader.SkipValue();
				}
				this.JsonReader.ReadEndObject();
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002F9D6 File Offset: 0x0002DBD6
		internal Uri ProcessUriFromPayload(string uriFromPayload)
		{
			return this.ProcessUriFromPayload(uriFromPayload, true);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002F9E0 File Offset: 0x0002DBE0
		internal Uri ProcessUriFromPayload(string uriFromPayload, bool requireAbsoluteUri)
		{
			Uri uri = new Uri(uriFromPayload, UriKind.RelativeOrAbsolute);
			Uri uri2 = this.VerboseJsonInputContext.ResolveUri(base.MessageReaderSettings.BaseUri, uri);
			if (uri2 != null)
			{
				return uri2;
			}
			if (!uri.IsAbsoluteUri)
			{
				if (base.MessageReaderSettings.BaseUri != null)
				{
					uri = UriUtils.UriToAbsoluteUri(base.MessageReaderSettings.BaseUri, uri);
				}
				else if (requireAbsoluteUri)
				{
					throw new ODataException(Strings.ODataJsonDeserializer_RelativeUriUsedWithoutBaseUriSpecified(uriFromPayload));
				}
			}
			return uri;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002FA57 File Offset: 0x0002DC57
		[Conditional("DEBUG")]
		internal void AssertJsonCondition(params JsonNodeType[] allowedNodeTypes)
		{
		}

		// Token: 0x04000494 RID: 1172
		private readonly ODataVerboseJsonInputContext jsonInputContext;
	}
}

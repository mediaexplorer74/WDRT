using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200019B RID: 411
	internal sealed class ODataJsonLightPayloadKindDetectionDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x06000C70 RID: 3184 RVA: 0x0002AF73 File Offset: 0x00029173
		internal ODataJsonLightPayloadKindDetectionDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002AF7C File Offset: 0x0002917C
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataPayloadKindDetectionInfo detectionInfo)
		{
			base.JsonReader.DisableInStreamErrorDetection = true;
			IEnumerable<ODataPayloadKind> enumerable;
			try
			{
				base.ReadPayloadStart(ODataPayloadKind.Unsupported, null, false, false);
				enumerable = this.DetectPayloadKindImplementation(detectionInfo);
			}
			catch (ODataException)
			{
				enumerable = Enumerable.Empty<ODataPayloadKind>();
			}
			finally
			{
				base.JsonReader.DisableInStreamErrorDetection = false;
			}
			return enumerable;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002B010 File Offset: 0x00029210
		internal Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(ODataPayloadKindDetectionInfo detectionInfo)
		{
			base.JsonReader.DisableInStreamErrorDetection = true;
			return base.ReadPayloadStartAsync(ODataPayloadKind.Unsupported, null, false, false).FollowOnSuccessWith((Task t) => this.DetectPayloadKindImplementation(detectionInfo)).FollowOnFaultAndCatchExceptionWith((ODataException t) => Enumerable.Empty<ODataPayloadKind>())
				.FollowAlwaysWith(delegate(Task<IEnumerable<ODataPayloadKind>> t)
				{
					base.JsonReader.DisableInStreamErrorDetection = false;
				});
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002B090 File Offset: 0x00029290
		private IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(ODataPayloadKindDetectionInfo detectionInfo)
		{
			if (base.MetadataUriParseResult != null)
			{
				detectionInfo.SetPayloadKindDetectionFormatState(new ODataJsonLightPayloadKindDetectionState(base.MetadataUriParseResult));
				return base.MetadataUriParseResult.DetectedPayloadKinds;
			}
			ODataError odataError = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (!ODataJsonLightReaderUtils.IsAnnotationProperty(text))
				{
					return Enumerable.Empty<ODataPayloadKind>();
				}
				string text2;
				string text3;
				if (ODataJsonLightDeserializer.TryParsePropertyAnnotation(text, out text2, out text3))
				{
					return Enumerable.Empty<ODataPayloadKind>();
				}
				if (ODataJsonLightReaderUtils.IsODataAnnotationName(text))
				{
					if (string.CompareOrdinal("odata.error", text) != 0)
					{
						return Enumerable.Empty<ODataPayloadKind>();
					}
					if (odataError != null || !base.JsonReader.StartBufferingAndTryToReadInStreamErrorPropertyValue(out odataError))
					{
						return Enumerable.Empty<ODataPayloadKind>();
					}
					base.JsonReader.SkipValue();
				}
				else
				{
					base.JsonReader.SkipValue();
				}
			}
			if (odataError == null)
			{
				return Enumerable.Empty<ODataPayloadKind>();
			}
			return new ODataPayloadKind[] { ODataPayloadKind.Error };
		}
	}
}

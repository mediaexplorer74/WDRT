using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x0200029D RID: 669
	internal static class ODataUtilsInternal
	{
		// Token: 0x06001695 RID: 5781 RVA: 0x00052558 File Offset: 0x00050758
		internal static Version ToDataServiceVersion(this ODataVersion version)
		{
			switch (version)
			{
			case ODataVersion.V1:
				return new Version(1, 0);
			case ODataVersion.V2:
				return new Version(2, 0);
			case ODataVersion.V3:
				return new Version(3, 0);
			default:
			{
				string text = Strings.General_InternalError(InternalErrorCodes.ODataUtilsInternal_ToDataServiceVersion_UnreachableCodePath);
				throw new ODataException(text);
			}
			}
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000525A8 File Offset: 0x000507A8
		internal static void SetDataServiceVersion(ODataMessage message, ODataMessageWriterSettings settings)
		{
			string text = ODataUtils.ODataVersionToString(settings.Version.Value) + ";";
			message.SetHeader("DataServiceVersion", text);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000525E0 File Offset: 0x000507E0
		internal static ODataVersion GetDataServiceVersion(ODataMessage message, ODataVersion defaultVersion)
		{
			string header = message.GetHeader("DataServiceVersion");
			string text = header;
			if (!string.IsNullOrEmpty(text))
			{
				return ODataUtils.StringToODataVersion(text);
			}
			return defaultVersion;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0005260C File Offset: 0x0005080C
		internal static bool IsPayloadKindSupported(ODataPayloadKind payloadKind, bool inRequest)
		{
			switch (payloadKind)
			{
			case ODataPayloadKind.Feed:
			case ODataPayloadKind.EntityReferenceLinks:
			case ODataPayloadKind.Collection:
			case ODataPayloadKind.ServiceDocument:
			case ODataPayloadKind.MetadataDocument:
			case ODataPayloadKind.Error:
				return !inRequest;
			case ODataPayloadKind.Entry:
			case ODataPayloadKind.Property:
			case ODataPayloadKind.EntityReferenceLink:
			case ODataPayloadKind.Value:
			case ODataPayloadKind.BinaryValue:
			case ODataPayloadKind.Batch:
				return true;
			case ODataPayloadKind.Parameter:
				return inRequest;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataUtilsInternal_IsPayloadKindSupported_UnreachableCodePath));
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00052670 File Offset: 0x00050870
		internal static IEnumerable<T> ConcatEnumerables<T>(IEnumerable<T> enumerable1, IEnumerable<T> enumerable2)
		{
			if (enumerable1 == null)
			{
				return enumerable2;
			}
			if (enumerable2 == null)
			{
				return enumerable1;
			}
			return enumerable1.Concat(enumerable2);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00052683 File Offset: 0x00050883
		internal static SelectedPropertiesNode SelectedProperties(this ODataMetadataDocumentUri metadataDocumentUri)
		{
			if (metadataDocumentUri == null)
			{
				return SelectedPropertiesNode.Create(null);
			}
			return SelectedPropertiesNode.Create(metadataDocumentUri.SelectClause);
		}
	}
}

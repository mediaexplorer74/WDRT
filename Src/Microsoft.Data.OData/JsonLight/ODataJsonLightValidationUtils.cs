using System;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000141 RID: 321
	internal static class ODataJsonLightValidationUtils
	{
		// Token: 0x06000893 RID: 2195 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		internal static void ValidateMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			string text = propertyName;
			if (propertyName[0] == '#')
			{
				text = UriUtilsCommon.UriToString(metadataDocumentUri) + UriUtils.EnsureEscapedFragment(propertyName);
			}
			if (!Uri.IsWellFormedUriString(text, UriKind.Absolute) || !ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName) || propertyName[propertyName.Length - 1] == '#')
			{
				throw new ODataException(Strings.ValidationUtils_InvalidMetadataReferenceProperty(propertyName));
			}
			if (ODataJsonLightValidationUtils.IsOpenMetadataReferencePropertyName(metadataDocumentUri, propertyName))
			{
				throw new ODataException(Strings.ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported(propertyName, UriUtilsCommon.UriToString(metadataDocumentUri)));
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001BE28 File Offset: 0x0001A028
		internal static void ValidateOperation(Uri metadataDocumentUri, ODataOperation operation)
		{
			ValidationUtils.ValidateOperationMetadataNotNull(operation);
			string text = UriUtilsCommon.UriToString(operation.Metadata);
			if (metadataDocumentUri != null)
			{
				ODataJsonLightValidationUtils.ValidateMetadataReferencePropertyName(metadataDocumentUri, text);
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001BE57 File Offset: 0x0001A057
		internal static bool IsOpenMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			return ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName) && propertyName[0] != '#' && !propertyName.StartsWith(UriUtilsCommon.UriToString(metadataDocumentUri), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001BE7E File Offset: 0x0001A07E
		internal static void ValidateOperationPropertyValueIsNotNull(object propertyValue, string propertyName, string metadata)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonLightValidationUtils_OperationPropertyCannotBeNull(propertyName, metadata));
			}
		}
	}
}

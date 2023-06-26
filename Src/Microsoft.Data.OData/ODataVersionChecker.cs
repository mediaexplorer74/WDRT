using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000292 RID: 658
	internal static class ODataVersionChecker
	{
		// Token: 0x06001633 RID: 5683 RVA: 0x000514EF File Offset: 0x0004F6EF
		internal static void CheckCount(ODataVersion version)
		{
			if (version < ODataVersion.V2)
			{
				throw new ODataException(Strings.ODataVersionChecker_InlineCountNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00051506 File Offset: 0x0004F706
		internal static void CheckCollectionValueProperties(ODataVersion version, string propertyName)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_CollectionPropertiesNotSupported(propertyName, ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0005151E File Offset: 0x0004F71E
		internal static void CheckCollectionValue(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_CollectionNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00051535 File Offset: 0x0004F735
		internal static void CheckNextLink(ODataVersion version)
		{
			if (version < ODataVersion.V2)
			{
				throw new ODataException(Strings.ODataVersionChecker_NextLinkNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0005154C File Offset: 0x0004F74C
		internal static void CheckDeltaLink(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_DeltaLinkNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00051563 File Offset: 0x0004F763
		internal static void CheckStreamReferenceProperty(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_StreamPropertiesNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0005157A File Offset: 0x0004F77A
		internal static void CheckAssociationLinks(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_AssociationLinksNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00051591 File Offset: 0x0004F791
		internal static void CheckCustomTypeScheme(ODataVersion version)
		{
			if (version > ODataVersion.V2)
			{
				throw new ODataException(Strings.ODataVersionChecker_PropertyNotSupportedForODataVersionGreaterThanX("TypeScheme", ODataUtils.ODataVersionToString(ODataVersion.V2)));
			}
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000515AD File Offset: 0x0004F7AD
		internal static void CheckCustomDataNamespace(ODataVersion version)
		{
			if (version > ODataVersion.V2)
			{
				throw new ODataException(Strings.ODataVersionChecker_PropertyNotSupportedForODataVersionGreaterThanX("DataNamespace", ODataUtils.ODataVersionToString(ODataVersion.V2)));
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000515C9 File Offset: 0x0004F7C9
		internal static void CheckParameterPayload(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_ParameterPayloadNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000515E0 File Offset: 0x0004F7E0
		internal static void CheckEntityPropertyMapping(ODataVersion version, IEdmEntityType entityType, IEdmModel model)
		{
			ODataEntityPropertyMappingCache epmCache = model.GetEpmCache(entityType);
			if (epmCache != null && version < epmCache.EpmTargetTree.MinimumODataProtocolVersion)
			{
				throw new ODataException(Strings.ODataVersionChecker_EpmVersionNotSupported(entityType.ODataFullName(), ODataUtils.ODataVersionToString(epmCache.EpmTargetTree.MinimumODataProtocolVersion), ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0005162D File Offset: 0x0004F82D
		internal static void CheckSpatialValue(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_GeographyAndGeometryNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00051644 File Offset: 0x0004F844
		internal static void CheckVersionSupported(ODataVersion version, ODataMessageReaderSettings messageReaderSettings)
		{
			if (version > messageReaderSettings.MaxProtocolVersion)
			{
				throw new ODataException(Strings.ODataVersionChecker_MaxProtocolVersionExceeded(ODataUtils.ODataVersionToString(version), ODataUtils.ODataVersionToString(messageReaderSettings.MaxProtocolVersion)));
			}
		}
	}
}

using System;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200016A RID: 362
	internal abstract class ODataJsonLightMetadataUriBuilder
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A16 RID: 2582
		internal abstract Uri BaseUri { get; }

		// Token: 0x06000A17 RID: 2583 RVA: 0x00021048 File Offset: 0x0001F248
		internal static ODataJsonLightMetadataUriBuilder CreateFromSettings(JsonLightMetadataLevel metadataLevel, bool writingResponse, ODataMessageWriterSettings writerSettings, IEdmModel model)
		{
			if (!metadataLevel.ShouldWriteODataMetadataUri())
			{
				return ODataJsonLightMetadataUriBuilder.NullMetadataUriBuilder.Instance;
			}
			ODataMetadataDocumentUri metadataDocumentUri = writerSettings.MetadataDocumentUri;
			if (metadataDocumentUri != null)
			{
				return ODataJsonLightMetadataUriBuilder.CreateDirectlyFromUri(metadataDocumentUri, model, writingResponse);
			}
			if (writingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightOutputContext_MetadataDocumentUriMissing);
			}
			return ODataJsonLightMetadataUriBuilder.NullMetadataUriBuilder.Instance;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00021089 File Offset: 0x0001F289
		internal static ODataJsonLightMetadataUriBuilder CreateDirectlyFromUri(ODataMetadataDocumentUri metadataDocumentUri, IEdmModel model, bool writingResponse)
		{
			return new ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder(metadataDocumentUri, model, writingResponse);
		}

		// Token: 0x06000A19 RID: 2585
		internal abstract bool TryBuildFeedMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri);

		// Token: 0x06000A1A RID: 2586
		internal abstract bool TryBuildEntryMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri);

		// Token: 0x06000A1B RID: 2587
		internal abstract bool TryBuildMetadataUriForValue(ODataProperty property, out Uri metadataUri);

		// Token: 0x06000A1C RID: 2588
		internal abstract bool TryBuildEntityReferenceLinkMetadataUri(ODataEntityReferenceLinkSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri);

		// Token: 0x06000A1D RID: 2589
		internal abstract bool TryBuildEntityReferenceLinksMetadataUri(ODataEntityReferenceLinksSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri);

		// Token: 0x06000A1E RID: 2590
		internal abstract bool TryBuildCollectionMetadataUri(ODataCollectionStartSerializationInfo serializationInfo, IEdmTypeReference itemTypeReference, out Uri metadataUri);

		// Token: 0x06000A1F RID: 2591
		internal abstract bool TryBuildServiceDocumentMetadataUri(out Uri metadataUri);

		// Token: 0x0200016B RID: 363
		private sealed class DefaultMetadataUriBuilder : ODataJsonLightMetadataUriBuilder
		{
			// Token: 0x06000A21 RID: 2593 RVA: 0x0002109B File Offset: 0x0001F29B
			internal DefaultMetadataUriBuilder(ODataMetadataDocumentUri metadataDocumentUri, IEdmModel model, bool writingResponse)
			{
				this.metadataDocumentUri = metadataDocumentUri;
				this.model = model;
				this.writingResponse = writingResponse;
			}

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000210B8 File Offset: 0x0001F2B8
			internal override Uri BaseUri
			{
				get
				{
					return this.metadataDocumentUri.BaseUri;
				}
			}

			// Token: 0x06000A23 RID: 2595 RVA: 0x000210C5 File Offset: 0x0001F2C5
			internal override bool TryBuildFeedMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri)
			{
				metadataUri = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateFeedOrEntryMetadataUri(this.metadataDocumentUri, this.model, typeContext, false, this.writingResponse);
				return metadataUri != null;
			}

			// Token: 0x06000A24 RID: 2596 RVA: 0x000210EA File Offset: 0x0001F2EA
			internal override bool TryBuildEntryMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri)
			{
				metadataUri = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateFeedOrEntryMetadataUri(this.metadataDocumentUri, this.model, typeContext, true, this.writingResponse);
				return metadataUri != null;
			}

			// Token: 0x06000A25 RID: 2597 RVA: 0x00021110 File Offset: 0x0001F310
			internal override bool TryBuildMetadataUriForValue(ODataProperty property, out Uri metadataUri)
			{
				string metadataUriTypeNameForValue = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.GetMetadataUriTypeNameForValue(property);
				if (string.IsNullOrEmpty(metadataUriTypeNameForValue))
				{
					throw new ODataException(Strings.WriterValidationUtils_MissingTypeNameWithMetadata);
				}
				metadataUri = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateTypeMetadataUri(this.metadataDocumentUri, metadataUriTypeNameForValue);
				return metadataUri != null;
			}

			// Token: 0x06000A26 RID: 2598 RVA: 0x00021150 File Offset: 0x0001F350
			internal override bool TryBuildEntityReferenceLinkMetadataUri(ODataEntityReferenceLinkSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri)
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				bool flag = false;
				if (serializationInfo != null)
				{
					text = serializationInfo.SourceEntitySetName;
					text2 = serializationInfo.Typecast;
					text3 = serializationInfo.NavigationPropertyName;
					flag = serializationInfo.IsCollectionNavigationProperty;
				}
				else if (navigationProperty != null)
				{
					text = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.GetEntitySetName(entitySet, this.model);
					text2 = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.GetTypecast(entitySet, navigationProperty.DeclaringEntityType());
					text3 = navigationProperty.Name;
					flag = navigationProperty.Type.IsCollection();
				}
				metadataUri = ((text3 == null) ? null : ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateEntityContainerElementMetadataUri(this.metadataDocumentUri, text, text2, text3, flag));
				if (this.writingResponse && metadataUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriBuilder_EntitySetOrNavigationPropertyMissingForTopLevelEntityReferenceLinkResponse);
				}
				return metadataUri != null;
			}

			// Token: 0x06000A27 RID: 2599 RVA: 0x000211F8 File Offset: 0x0001F3F8
			internal override bool TryBuildEntityReferenceLinksMetadataUri(ODataEntityReferenceLinksSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri)
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				if (serializationInfo != null)
				{
					text = serializationInfo.SourceEntitySetName;
					text2 = serializationInfo.Typecast;
					text3 = serializationInfo.NavigationPropertyName;
				}
				else if (navigationProperty != null)
				{
					text = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.GetEntitySetName(entitySet, this.model);
					text2 = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.GetTypecast(entitySet, navigationProperty.DeclaringEntityType());
					text3 = navigationProperty.Name;
				}
				metadataUri = ((text3 == null) ? null : ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateEntityContainerElementMetadataUri(this.metadataDocumentUri, text, text2, text3, false));
				if (this.writingResponse && metadataUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriBuilder_EntitySetOrNavigationPropertyMissingForTopLevelEntityReferenceLinksResponse);
				}
				return metadataUri != null;
			}

			// Token: 0x06000A28 RID: 2600 RVA: 0x00021288 File Offset: 0x0001F488
			internal override bool TryBuildCollectionMetadataUri(ODataCollectionStartSerializationInfo serializationInfo, IEdmTypeReference itemTypeReference, out Uri metadataUri)
			{
				string text = null;
				if (serializationInfo != null)
				{
					text = serializationInfo.CollectionTypeName;
				}
				else if (itemTypeReference != null)
				{
					text = EdmLibraryExtensions.GetCollectionTypeName(itemTypeReference.ODataFullName());
				}
				metadataUri = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateTypeMetadataUri(this.metadataDocumentUri, text);
				if (this.writingResponse && metadataUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriBuilder_TypeNameMissingForTopLevelCollectionWhenWritingResponsePayload);
				}
				return metadataUri != null;
			}

			// Token: 0x06000A29 RID: 2601 RVA: 0x000212E5 File Offset: 0x0001F4E5
			internal override bool TryBuildServiceDocumentMetadataUri(out Uri metadataUri)
			{
				metadataUri = this.metadataDocumentUri.BaseUri;
				return true;
			}

			// Token: 0x06000A2A RID: 2602 RVA: 0x000212F8 File Offset: 0x0001F4F8
			private static string GetMetadataUriTypeNameForValue(ODataProperty property)
			{
				ODataValue odataValue = property.ODataValue;
				if (odataValue.IsNullValue)
				{
					return "Edm.Null";
				}
				SerializationTypeNameAnnotation annotation = odataValue.GetAnnotation<SerializationTypeNameAnnotation>();
				if (annotation != null && !string.IsNullOrEmpty(annotation.TypeName))
				{
					return annotation.TypeName;
				}
				ODataComplexValue odataComplexValue = odataValue as ODataComplexValue;
				if (odataComplexValue != null)
				{
					return odataComplexValue.TypeName;
				}
				ODataCollectionValue odataCollectionValue = odataValue as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					return odataCollectionValue.TypeName;
				}
				ODataPrimitiveValue odataPrimitiveValue = odataValue as ODataPrimitiveValue;
				if (odataPrimitiveValue == null)
				{
					throw new ODataException(Strings.ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry(property.Name));
				}
				return EdmLibraryExtensions.GetPrimitiveTypeReference(odataPrimitiveValue.Value.GetType()).ODataFullName();
			}

			// Token: 0x06000A2B RID: 2603 RVA: 0x00021390 File Offset: 0x0001F590
			private static string GetEntitySetName(IEdmEntitySet entitySet, IEdmModel edmModel)
			{
				if (entitySet == null)
				{
					return null;
				}
				IEdmEntityContainer container = entitySet.Container;
				string text;
				if (edmModel.IsDefaultEntityContainer(container))
				{
					text = entitySet.Name;
				}
				else
				{
					text = string.Concat(new string[] { container.Namespace, ".", container.Name, ".", entitySet.Name });
				}
				return text;
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x000213F8 File Offset: 0x0001F5F8
			private static string GetTypecast(IEdmEntitySet entitySet, IEdmEntityType entityType)
			{
				if (entitySet == null || entityType == null)
				{
					return null;
				}
				IEdmEntityType elementType = EdmTypeWriterResolver.Instance.GetElementType(entitySet);
				if (elementType.IsEquivalentTo(entityType))
				{
					return null;
				}
				if (!elementType.IsAssignableFrom(entityType))
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriBuilder_ValidateDerivedType(elementType.FullName(), entityType.FullName()));
				}
				return entityType.ODataFullName();
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x0002144A File Offset: 0x0001F64A
			private static Uri CreateTypeMetadataUri(ODataMetadataDocumentUri metadataDocumentUri, string fullTypeName)
			{
				if (fullTypeName != null)
				{
					return new Uri(metadataDocumentUri.BaseUri, '#' + fullTypeName);
				}
				return null;
			}

			// Token: 0x06000A2E RID: 2606 RVA: 0x0002146C File Offset: 0x0001F66C
			private static Uri CreateFeedOrEntryMetadataUri(ODataMetadataDocumentUri metadataDocumentUri, IEdmModel model, ODataFeedAndEntryTypeContext typeContext, bool isEntry, bool writingResponse)
			{
				string text = ((typeContext.EntitySetElementTypeName == typeContext.ExpectedEntityTypeName) ? null : typeContext.ExpectedEntityTypeName);
				return ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateEntityContainerElementMetadataUri(metadataDocumentUri, typeContext.EntitySetName, text, null, isEntry);
			}

			// Token: 0x06000A2F RID: 2607 RVA: 0x000214A8 File Offset: 0x0001F6A8
			private static Uri CreateEntityContainerElementMetadataUri(ODataMetadataDocumentUri metadataDocumentUri, string entitySetName, string typecast, string navigationPropertyName, bool appendItemSelector)
			{
				if (entitySetName == null)
				{
					return null;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append('#');
				stringBuilder.Append(entitySetName);
				if (typecast != null)
				{
					stringBuilder.Append('/');
					stringBuilder.Append(typecast);
				}
				if (navigationPropertyName != null)
				{
					stringBuilder.Append('/');
					stringBuilder.Append("$links");
					stringBuilder.Append('/');
					stringBuilder.Append(navigationPropertyName);
				}
				if (appendItemSelector)
				{
					stringBuilder.Append('/');
					stringBuilder.Append("@Element");
				}
				string selectClause = metadataDocumentUri.SelectClause;
				if (selectClause != null)
				{
					stringBuilder.Append('&');
					stringBuilder.Append("$select");
					stringBuilder.Append('=');
					stringBuilder.Append(selectClause);
				}
				return new Uri(metadataDocumentUri.BaseUri, stringBuilder.ToString());
			}

			// Token: 0x040003BB RID: 955
			private readonly ODataMetadataDocumentUri metadataDocumentUri;

			// Token: 0x040003BC RID: 956
			private readonly IEdmModel model;

			// Token: 0x040003BD RID: 957
			private readonly bool writingResponse;
		}

		// Token: 0x0200016C RID: 364
		private sealed class NullMetadataUriBuilder : ODataJsonLightMetadataUriBuilder
		{
			// Token: 0x06000A30 RID: 2608 RVA: 0x00021568 File Offset: 0x0001F768
			private NullMetadataUriBuilder()
			{
			}

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00021570 File Offset: 0x0001F770
			internal override Uri BaseUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06000A32 RID: 2610 RVA: 0x00021573 File Offset: 0x0001F773
			internal override bool TryBuildFeedMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x06000A33 RID: 2611 RVA: 0x00021579 File Offset: 0x0001F779
			internal override bool TryBuildEntryMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x06000A34 RID: 2612 RVA: 0x0002157F File Offset: 0x0001F77F
			internal override bool TryBuildMetadataUriForValue(ODataProperty property, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x00021585 File Offset: 0x0001F785
			internal override bool TryBuildEntityReferenceLinkMetadataUri(ODataEntityReferenceLinkSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x06000A36 RID: 2614 RVA: 0x0002158C File Offset: 0x0001F78C
			internal override bool TryBuildEntityReferenceLinksMetadataUri(ODataEntityReferenceLinksSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x06000A37 RID: 2615 RVA: 0x00021593 File Offset: 0x0001F793
			internal override bool TryBuildCollectionMetadataUri(ODataCollectionStartSerializationInfo serializationInfo, IEdmTypeReference itemTypeReference, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x06000A38 RID: 2616 RVA: 0x00021599 File Offset: 0x0001F799
			internal override bool TryBuildServiceDocumentMetadataUri(out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x040003BE RID: 958
			internal static readonly ODataJsonLightMetadataUriBuilder.NullMetadataUriBuilder Instance = new ODataJsonLightMetadataUriBuilder.NullMetadataUriBuilder();
		}
	}
}

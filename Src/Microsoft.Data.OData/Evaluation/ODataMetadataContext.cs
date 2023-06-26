using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.JsonLight;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000109 RID: 265
	internal sealed class ODataMetadataContext : IODataMetadataContext
	{
		// Token: 0x0600072F RID: 1839 RVA: 0x00018950 File Offset: 0x00016B50
		public ODataMetadataContext(bool isResponse, IEdmModel model, Uri metadataDocumentUri)
			: this(isResponse, null, EdmTypeWriterResolver.Instance, model, metadataDocumentUri)
		{
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00018964 File Offset: 0x00016B64
		public ODataMetadataContext(bool isResponse, Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified, EdmTypeResolver edmTypeResolver, IEdmModel model, Uri metadataDocumentUri)
		{
			this.isResponse = isResponse;
			this.operationsBoundToEntityTypeMustBeContainerQualified = operationsBoundToEntityTypeMustBeContainerQualified ?? new Func<IEdmEntityType, bool>(EdmLibraryExtensions.OperationsBoundToEntityTypeMustBeContainerQualified);
			this.edmTypeResolver = edmTypeResolver;
			this.model = model;
			this.metadataDocumentUri = metadataDocumentUri;
			this.alwaysBindableOperationsCache = new Dictionary<IEdmType, IEdmFunctionImport[]>(ReferenceEqualityComparer<IEdmType>.Instance);
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x000189BC File Offset: 0x00016BBC
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000189C4 File Offset: 0x00016BC4
		public Uri ServiceBaseUri
		{
			get
			{
				Uri uri;
				if ((uri = this.serviceBaseUri) == null)
				{
					uri = (this.serviceBaseUri = new Uri(this.MetadataDocumentUri, "./"));
				}
				return uri;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x000189F4 File Offset: 0x00016BF4
		public Uri MetadataDocumentUri
		{
			get
			{
				if (this.metadataDocumentUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightEntryMetadataContext_MetadataAnnotationMustBeInPayload("odata.metadata"));
				}
				return this.metadataDocumentUri;
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00018A1C File Offset: 0x00016C1C
		public ODataEntityMetadataBuilder GetEntityMetadataBuilderForReader(IODataJsonLightReaderEntryState entryState)
		{
			if (entryState.MetadataBuilder == null)
			{
				ODataEntry entry = entryState.Entry;
				if (this.isResponse)
				{
					ODataTypeAnnotation annotation = entry.GetAnnotation<ODataTypeAnnotation>();
					IEdmEntitySet entitySet = annotation.EntitySet;
					IEdmEntityType elementType = this.edmTypeResolver.GetElementType(entitySet);
					IODataFeedAndEntryTypeContext iodataFeedAndEntryTypeContext = ODataFeedAndEntryTypeContext.Create(null, entitySet, elementType, entryState.EntityType, this.model, true);
					IODataEntryMetadataContext iodataEntryMetadataContext = ODataEntryMetadataContext.Create(entry, iodataFeedAndEntryTypeContext, null, (IEdmEntityType)entry.GetEdmType().Definition, this, entryState.SelectedProperties);
					UrlConvention urlConvention = UrlConvention.ForUserSettingAndTypeContext(null, iodataFeedAndEntryTypeContext);
					ODataConventionalUriBuilder odataConventionalUriBuilder = new ODataConventionalUriBuilder(this.ServiceBaseUri, urlConvention);
					entryState.MetadataBuilder = new ODataConventionalEntityMetadataBuilder(iodataEntryMetadataContext, this, odataConventionalUriBuilder);
				}
				else
				{
					entryState.MetadataBuilder = new NoOpEntityMetadataBuilder(entry);
				}
			}
			return entryState.MetadataBuilder;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00018AE0 File Offset: 0x00016CE0
		public IEdmFunctionImport[] GetAlwaysBindableOperationsForType(IEdmType bindingType)
		{
			IEdmFunctionImport[] array;
			if (!this.alwaysBindableOperationsCache.TryGetValue(bindingType, out array))
			{
				array = MetadataUtils.CalculateAlwaysBindableOperationsForType(bindingType, this.model, this.edmTypeResolver);
				this.alwaysBindableOperationsCache.Add(bindingType, array);
			}
			return array;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00018B1E File Offset: 0x00016D1E
		public bool OperationsBoundToEntityTypeMustBeContainerQualified(IEdmEntityType entityType)
		{
			return this.operationsBoundToEntityTypeMustBeContainerQualified(entityType);
		}

		// Token: 0x040002B8 RID: 696
		private readonly IEdmModel model;

		// Token: 0x040002B9 RID: 697
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x040002BA RID: 698
		private readonly Dictionary<IEdmType, IEdmFunctionImport[]> alwaysBindableOperationsCache;

		// Token: 0x040002BB RID: 699
		private readonly bool isResponse;

		// Token: 0x040002BC RID: 700
		private readonly Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified;

		// Token: 0x040002BD RID: 701
		private readonly Uri metadataDocumentUri;

		// Token: 0x040002BE RID: 702
		private Uri serviceBaseUri;
	}
}

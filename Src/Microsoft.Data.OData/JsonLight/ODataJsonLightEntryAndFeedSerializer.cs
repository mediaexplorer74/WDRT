using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000190 RID: 400
	internal sealed class ODataJsonLightEntryAndFeedSerializer : ODataJsonLightPropertySerializer
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x000280EF File Offset: 0x000262EF
		internal ODataJsonLightEntryAndFeedSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext)
		{
			this.annotationGroups = new Dictionary<string, ODataJsonLightAnnotationGroup>(StringComparer.Ordinal);
			this.metadataUriBuilder = jsonLightOutputContext.CreateMetadataUriBuilder();
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00028114 File Offset: 0x00026314
		private Uri MetadataDocumentBaseUri
		{
			get
			{
				return this.metadataUriBuilder.BaseUri;
			}
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00028124 File Offset: 0x00026324
		internal void WriteAnnotationGroup(ODataEntry entry)
		{
			ODataJsonLightAnnotationGroup annotation = entry.GetAnnotation<ODataJsonLightAnnotationGroup>();
			if (annotation == null)
			{
				return;
			}
			if (!base.JsonLightOutputContext.WritingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_AnnotationGroupInRequest);
			}
			string name = annotation.Name;
			if (string.IsNullOrEmpty(name))
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_AnnotationGroupWithoutName);
			}
			ODataJsonLightAnnotationGroup odataJsonLightAnnotationGroup;
			if (!this.annotationGroups.TryGetValue(name, out odataJsonLightAnnotationGroup))
			{
				base.JsonWriter.WriteName("odata.annotationGroup");
				base.JsonWriter.StartObjectScope();
				base.JsonWriter.WriteName("name");
				base.JsonWriter.WritePrimitiveValue(name, base.JsonLightOutputContext.Version);
				if (annotation.Annotations != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair in annotation.Annotations)
					{
						string key = keyValuePair.Key;
						if (key.Length == 0)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberWithoutName(annotation.Name));
						}
						if (!ODataJsonLightReaderUtils.IsAnnotationProperty(key))
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberMustBeAnnotation(key, annotation.Name));
						}
						base.JsonWriter.WriteName(key);
						object value = keyValuePair.Value;
						string text = value as string;
						if (text == null && value != null)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberWithInvalidValue(key, annotation.Name, value.GetType().FullName));
						}
						base.JsonWriter.WritePrimitiveValue(text, base.JsonLightOutputContext.Version);
					}
				}
				base.JsonWriter.EndObjectScope();
				this.annotationGroups.Add(name, annotation);
				return;
			}
			if (!object.ReferenceEquals(odataJsonLightAnnotationGroup, annotation))
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_DuplicateAnnotationGroup(name));
			}
			base.JsonWriter.WriteName("odata.annotationGroupReference");
			base.JsonWriter.WritePrimitiveValue(name, base.JsonLightOutputContext.Version);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00028304 File Offset: 0x00026504
		internal void WriteEntryStartMetadataProperties(IODataJsonLightWriterEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			string entryTypeNameForWriting = base.JsonLightOutputContext.TypeNameOracle.GetEntryTypeNameForWriting(entryState.GetOrCreateTypeContext(base.Model, base.WritingResponse).ExpectedEntityTypeName, entry);
			if (entryTypeNameForWriting != null)
			{
				base.JsonWriter.WriteName("odata.type");
				base.JsonWriter.WriteValue(entryTypeNameForWriting);
			}
			string id = entry.Id;
			if (id != null)
			{
				base.JsonWriter.WriteName("odata.id");
				base.JsonWriter.WriteValue(id);
			}
			string etag = entry.ETag;
			if (etag != null)
			{
				base.JsonWriter.WriteName("odata.etag");
				base.JsonWriter.WriteValue(etag);
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000283AC File Offset: 0x000265AC
		internal void WriteEntryMetadataProperties(IODataJsonLightWriterEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			Uri editLink = entry.EditLink;
			if (editLink != null && !entryState.EditLinkWritten)
			{
				base.JsonWriter.WriteName("odata.editLink");
				base.JsonWriter.WriteValue(base.UriToString(editLink));
				entryState.EditLinkWritten = true;
			}
			Uri readLink = entry.ReadLink;
			if (readLink != null && !entryState.ReadLinkWritten)
			{
				base.JsonWriter.WriteName("odata.readLink");
				base.JsonWriter.WriteValue(base.UriToString(readLink));
				entryState.ReadLinkWritten = true;
			}
			ODataStreamReferenceValue mediaResource = entry.MediaResource;
			if (mediaResource != null)
			{
				Uri editLink2 = mediaResource.EditLink;
				if (editLink2 != null && !entryState.MediaEditLinkWritten)
				{
					base.JsonWriter.WriteName("odata.mediaEditLink");
					base.JsonWriter.WriteValue(base.UriToString(editLink2));
					entryState.MediaEditLinkWritten = true;
				}
				Uri readLink2 = mediaResource.ReadLink;
				if (readLink2 != null && !entryState.MediaReadLinkWritten)
				{
					base.JsonWriter.WriteName("odata.mediaReadLink");
					base.JsonWriter.WriteValue(base.UriToString(readLink2));
					entryState.MediaReadLinkWritten = true;
				}
				string contentType = mediaResource.ContentType;
				if (contentType != null && !entryState.MediaContentTypeWritten)
				{
					base.JsonWriter.WriteName("odata.mediaContentType");
					base.JsonWriter.WriteValue(contentType);
					entryState.MediaContentTypeWritten = true;
				}
				string etag = mediaResource.ETag;
				if (etag != null && !entryState.MediaETagWritten)
				{
					base.JsonWriter.WriteName("odata.mediaETag");
					base.JsonWriter.WriteValue(etag);
					entryState.MediaETagWritten = true;
				}
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00028548 File Offset: 0x00026748
		internal void WriteEntryEndMetadataProperties(IODataJsonLightWriterEntryState entryState, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			ODataEntry entry = entryState.Entry;
			for (ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = entry.MetadataBuilder.GetNextUnprocessedNavigationLink(); odataJsonLightReaderNavigationLinkInfo != null; odataJsonLightReaderNavigationLinkInfo = entry.MetadataBuilder.GetNextUnprocessedNavigationLink())
			{
				odataJsonLightReaderNavigationLinkInfo.NavigationLink.SetMetadataBuilder(entry.MetadataBuilder);
				this.WriteNavigationLinkMetadata(odataJsonLightReaderNavigationLinkInfo.NavigationLink, duplicatePropertyNamesChecker);
			}
			IEnumerable<ODataAction> actions = entry.Actions;
			if (actions != null)
			{
				this.WriteOperations(actions.Cast<ODataOperation>(), true);
			}
			IEnumerable<ODataFunction> functions = entry.Functions;
			if (functions != null)
			{
				this.WriteOperations(functions.Cast<ODataOperation>(), false);
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000285C8 File Offset: 0x000267C8
		internal void WriteNavigationLinkMetadata(ODataNavigationLink navigationLink, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			Uri url = navigationLink.Url;
			string name = navigationLink.Name;
			if (url != null)
			{
				base.JsonWriter.WritePropertyAnnotationName(name, "odata.navigationLinkUrl");
				base.JsonWriter.WriteValue(base.UriToString(url));
			}
			Uri associationLinkUrl = navigationLink.AssociationLinkUrl;
			if (associationLinkUrl != null)
			{
				duplicatePropertyNamesChecker.CheckForDuplicateAssociationLinkNames(new ODataAssociationLink
				{
					Name = name
				});
				this.WriteAssociationLink(navigationLink.Name, associationLinkUrl);
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00028698 File Offset: 0x00026898
		internal void WriteOperations(IEnumerable<ODataOperation> operations, bool isAction)
		{
			IEnumerable<IGrouping<string, ODataOperation>> enumerable = operations.GroupBy(delegate(ODataOperation o)
			{
				ValidationUtils.ValidateOperationNotNull(o, isAction);
				WriterValidationUtils.ValidateCanWriteOperation(o, this.JsonLightOutputContext.WritingResponse);
				ODataJsonLightValidationUtils.ValidateOperation(this.MetadataDocumentBaseUri, o);
				return this.GetOperationMetadataString(o);
			});
			foreach (IGrouping<string, ODataOperation> grouping in enumerable)
			{
				this.WriteOperationMetadataGroup(grouping);
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00028708 File Offset: 0x00026908
		internal void TryWriteEntryMetadataUri(ODataFeedAndEntryTypeContext typeContext)
		{
			Uri uri;
			if (this.metadataUriBuilder.TryBuildEntryMetadataUri(typeContext, out uri))
			{
				base.WriteMetadataUriProperty(uri);
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002872C File Offset: 0x0002692C
		internal void TryWriteFeedMetadataUri(ODataFeedAndEntryTypeContext typeContext)
		{
			Uri uri;
			if (this.metadataUriBuilder.TryBuildFeedMetadataUri(typeContext, out uri))
			{
				base.WriteMetadataUriProperty(uri);
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00028750 File Offset: 0x00026950
		private void WriteAssociationLink(string propertyName, Uri associationLinkUrl)
		{
			base.JsonWriter.WritePropertyAnnotationName(propertyName, "odata.associationLinkUrl");
			base.JsonWriter.WriteValue(base.UriToString(associationLinkUrl));
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00028778 File Offset: 0x00026978
		private string GetOperationMetadataString(ODataOperation operation)
		{
			string text = UriUtilsCommon.UriToString(operation.Metadata);
			if (this.MetadataDocumentBaseUri == null)
			{
				return operation.Metadata.Fragment;
			}
			return '#' + ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(this.MetadataDocumentBaseUri, text);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000287C3 File Offset: 0x000269C3
		private string GetOperationTargetUriString(ODataOperation operation)
		{
			if (!(operation.Target == null))
			{
				return base.UriToString(operation.Target);
			}
			return null;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000287F0 File Offset: 0x000269F0
		private void ValidateOperationMetadataGroup(IGrouping<string, ODataOperation> operations)
		{
			if (operations.Count<ODataOperation>() > 1)
			{
				if (operations.Any((ODataOperation o) => o.Target == null))
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustSpecifyTarget(operations.Key));
				}
			}
			foreach (IGrouping<string, ODataOperation> grouping in operations.GroupBy(new Func<ODataOperation, string>(this.GetOperationTargetUriString)))
			{
				if (grouping.Count<ODataOperation>() > 1)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget(operations.Key, grouping.Key));
				}
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000288A4 File Offset: 0x00026AA4
		private void WriteOperationMetadataGroup(IGrouping<string, ODataOperation> operations)
		{
			this.ValidateOperationMetadataGroup(operations);
			base.JsonLightOutputContext.JsonWriter.WriteName(operations.Key);
			bool flag = operations.Count<ODataOperation>() > 1;
			if (flag)
			{
				base.JsonLightOutputContext.JsonWriter.StartArrayScope();
			}
			foreach (ODataOperation odataOperation in operations)
			{
				this.WriteOperation(odataOperation);
			}
			if (flag)
			{
				base.JsonLightOutputContext.JsonWriter.EndArrayScope();
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002893C File Offset: 0x00026B3C
		private void WriteOperation(ODataOperation operation)
		{
			base.JsonLightOutputContext.JsonWriter.StartObjectScope();
			if (operation.Title != null)
			{
				base.JsonLightOutputContext.JsonWriter.WriteName("title");
				base.JsonLightOutputContext.JsonWriter.WriteValue(operation.Title);
			}
			if (operation.Target != null)
			{
				string operationTargetUriString = this.GetOperationTargetUriString(operation);
				base.JsonLightOutputContext.JsonWriter.WriteName("target");
				base.JsonLightOutputContext.JsonWriter.WriteValue(operationTargetUriString);
			}
			base.JsonLightOutputContext.JsonWriter.EndObjectScope();
		}

		// Token: 0x0400041E RID: 1054
		private readonly Dictionary<string, ODataJsonLightAnnotationGroup> annotationGroups;

		// Token: 0x0400041F RID: 1055
		private readonly ODataJsonLightMetadataUriBuilder metadataUriBuilder;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C7 RID: 455
	internal sealed class ODataVerboseJsonEntryAndFeedSerializer : ODataVerboseJsonPropertyAndValueSerializer
	{
		// Token: 0x06000E10 RID: 3600 RVA: 0x00031A48 File Offset: 0x0002FC48
		internal ODataVerboseJsonEntryAndFeedSerializer(ODataVerboseJsonOutputContext verboseJsonOutputContext)
			: base(verboseJsonOutputContext)
		{
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00031A54 File Offset: 0x0002FC54
		internal void WriteEntryMetadata(ODataEntry entry, ProjectedPropertiesAnnotation projectedProperties, IEdmEntityType entryEntityType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			base.JsonWriter.WriteName("__metadata");
			base.JsonWriter.StartObjectScope();
			string id = entry.Id;
			if (id != null)
			{
				base.JsonWriter.WriteName("id");
				base.JsonWriter.WriteValue(id);
			}
			Uri uri = entry.EditLink ?? entry.ReadLink;
			if (uri != null)
			{
				base.JsonWriter.WriteName("uri");
				base.JsonWriter.WriteValue(base.UriToAbsoluteUriString(uri));
			}
			string etag = entry.ETag;
			if (etag != null)
			{
				base.WriteETag("etag", etag);
			}
			string entryTypeNameForWriting = base.VerboseJsonOutputContext.TypeNameOracle.GetEntryTypeNameForWriting(entry);
			if (entryTypeNameForWriting != null)
			{
				base.JsonWriter.WriteName("type");
				base.JsonWriter.WriteValue(entryTypeNameForWriting);
			}
			ODataStreamReferenceValue mediaResource = entry.MediaResource;
			if (mediaResource != null)
			{
				WriterValidationUtils.ValidateStreamReferenceValue(mediaResource, true);
				base.WriteStreamReferenceValueContent(mediaResource);
			}
			IEnumerable<ODataAction> actions = entry.Actions;
			if (actions != null)
			{
				this.WriteOperations(actions.Cast<ODataOperation>(), "actions", true, false);
			}
			IEnumerable<ODataFunction> functions = entry.Functions;
			if (functions != null)
			{
				this.WriteOperations(functions.Cast<ODataOperation>(), "functions", false, false);
			}
			IEnumerable<ODataAssociationLink> associationLinks = entry.AssociationLinks;
			if (associationLinks != null)
			{
				bool flag = true;
				foreach (ODataAssociationLink odataAssociationLink in associationLinks)
				{
					ValidationUtils.ValidateAssociationLinkNotNull(odataAssociationLink);
					if (!projectedProperties.ShouldSkipProperty(odataAssociationLink.Name))
					{
						if (flag)
						{
							base.JsonWriter.WriteName("properties");
							base.JsonWriter.StartObjectScope();
							flag = false;
						}
						base.ValidateAssociationLink(odataAssociationLink, entryEntityType);
						this.WriteAssociationLink(odataAssociationLink, duplicatePropertyNamesChecker);
					}
				}
				if (!flag)
				{
					base.JsonWriter.EndObjectScope();
				}
			}
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00031C94 File Offset: 0x0002FE94
		internal void WriteOperations(IEnumerable<ODataOperation> operations, string operationName, bool isAction, bool writingJsonLight)
		{
			bool flag = true;
			IEnumerable<IGrouping<string, ODataOperation>> enumerable = operations.GroupBy(delegate(ODataOperation o)
			{
				ValidationUtils.ValidateOperationNotNull(o, isAction);
				WriterValidationUtils.ValidateCanWriteOperation(o, this.VerboseJsonOutputContext.WritingResponse);
				ValidationUtils.ValidateOperationMetadataNotNull(o);
				if (!writingJsonLight)
				{
					ValidationUtils.ValidateOperationTargetNotNull(o);
				}
				return this.UriToUriString(o.Metadata, false);
			});
			foreach (IGrouping<string, ODataOperation> grouping in enumerable)
			{
				if (flag)
				{
					base.VerboseJsonOutputContext.JsonWriter.WriteName(operationName);
					base.VerboseJsonOutputContext.JsonWriter.StartObjectScope();
					flag = false;
				}
				this.WriteOperationMetadataGroup(grouping);
			}
			if (!flag)
			{
				base.VerboseJsonOutputContext.JsonWriter.EndObjectScope();
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00031D4C File Offset: 0x0002FF4C
		private void WriteAssociationLink(ODataAssociationLink associationLink, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			duplicatePropertyNamesChecker.CheckForDuplicateAssociationLinkNames(associationLink);
			base.JsonWriter.WriteName(associationLink.Name);
			base.JsonWriter.StartObjectScope();
			base.JsonWriter.WriteName("associationuri");
			base.JsonWriter.WriteValue(base.UriToAbsoluteUriString(associationLink.Url));
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00031DB0 File Offset: 0x0002FFB0
		private void WriteOperationMetadataGroup(IGrouping<string, ODataOperation> operations)
		{
			bool flag = true;
			foreach (ODataOperation odataOperation in operations)
			{
				if (flag)
				{
					base.VerboseJsonOutputContext.JsonWriter.WriteName(operations.Key);
					base.VerboseJsonOutputContext.JsonWriter.StartArrayScope();
					flag = false;
				}
				this.WriteOperation(odataOperation);
			}
			base.VerboseJsonOutputContext.JsonWriter.EndArrayScope();
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00031E38 File Offset: 0x00030038
		private void WriteOperation(ODataOperation operation)
		{
			base.VerboseJsonOutputContext.JsonWriter.StartObjectScope();
			if (operation.Title != null)
			{
				base.VerboseJsonOutputContext.JsonWriter.WriteName("title");
				base.VerboseJsonOutputContext.JsonWriter.WriteValue(operation.Title);
			}
			if (operation.Target != null)
			{
				string text = base.UriToAbsoluteUriString(operation.Target);
				base.VerboseJsonOutputContext.JsonWriter.WriteName("target");
				base.VerboseJsonOutputContext.JsonWriter.WriteValue(text);
			}
			base.VerboseJsonOutputContext.JsonWriter.EndObjectScope();
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.JsonLight;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x0200010A RID: 266
	internal sealed class ODataMissingOperationGenerator
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x00018B2C File Offset: 0x00016D2C
		internal ODataMissingOperationGenerator(IODataEntryMetadataContext entryMetadataContext, IODataMetadataContext metadataContext)
		{
			this.entryMetadataContext = entryMetadataContext;
			this.metadataContext = metadataContext;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00018B42 File Offset: 0x00016D42
		internal IEnumerable<ODataAction> GetComputedActions()
		{
			this.ComputeMissingOperationsToEntry();
			return this.computedActions;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00018B50 File Offset: 0x00016D50
		internal IEnumerable<ODataFunction> GetComputedFunctions()
		{
			this.ComputeMissingOperationsToEntry();
			return this.computedFunctions;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00018B60 File Offset: 0x00016D60
		private static HashSet<IEdmFunctionImport> GetFunctionImportsInEntry(ODataEntry entry, IEdmModel model, Uri metadataDocumentUri)
		{
			HashSet<IEdmFunctionImport> hashSet = new HashSet<IEdmFunctionImport>(EqualityComparer<IEdmFunctionImport>.Default);
			IEnumerable<ODataOperation> enumerable = ODataUtilsInternal.ConcatEnumerables<ODataOperation>(entry.NonComputedActions, entry.NonComputedFunctions);
			if (enumerable != null)
			{
				foreach (ODataOperation odataOperation in enumerable)
				{
					string text = UriUtilsCommon.UriToString(odataOperation.Metadata);
					string uriFragmentFromMetadataReferencePropertyName = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(metadataDocumentUri, text);
					IEnumerable<IEdmFunctionImport> enumerable2 = model.ResolveFunctionImports(uriFragmentFromMetadataReferencePropertyName);
					if (enumerable2 != null)
					{
						foreach (IEdmFunctionImport edmFunctionImport in enumerable2)
						{
							hashSet.Add(edmFunctionImport);
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00018C30 File Offset: 0x00016E30
		private void ComputeMissingOperationsToEntry()
		{
			if (this.computedActions == null)
			{
				this.computedActions = new List<ODataAction>();
				this.computedFunctions = new List<ODataFunction>();
				HashSet<IEdmFunctionImport> functionImportsInEntry = ODataMissingOperationGenerator.GetFunctionImportsInEntry(this.entryMetadataContext.Entry, this.metadataContext.Model, this.metadataContext.MetadataDocumentUri);
				foreach (IEdmFunctionImport edmFunctionImport in this.entryMetadataContext.SelectedAlwaysBindableOperations)
				{
					if (!functionImportsInEntry.Contains(edmFunctionImport))
					{
						string text = '#' + ODataJsonLightUtils.GetMetadataReferenceName(edmFunctionImport);
						bool flag;
						ODataOperation odataOperation = ODataJsonLightUtils.CreateODataOperation(this.metadataContext.MetadataDocumentUri, text, edmFunctionImport, out flag);
						odataOperation.SetMetadataBuilder(this.entryMetadataContext.Entry.MetadataBuilder, this.metadataContext.MetadataDocumentUri);
						if (flag)
						{
							this.computedActions.Add((ODataAction)odataOperation);
						}
						else
						{
							this.computedFunctions.Add((ODataFunction)odataOperation);
						}
					}
				}
			}
		}

		// Token: 0x040002BF RID: 703
		private readonly IODataMetadataContext metadataContext;

		// Token: 0x040002C0 RID: 704
		private readonly IODataEntryMetadataContext entryMetadataContext;

		// Token: 0x040002C1 RID: 705
		private List<ODataAction> computedActions;

		// Token: 0x040002C2 RID: 706
		private List<ODataFunction> computedFunctions;
	}
}

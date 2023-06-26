using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000138 RID: 312
	internal sealed class ODataConventionalEntityMetadataBuilder : ODataEntityMetadataBuilder
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x0001AD94 File Offset: 0x00018F94
		internal ODataConventionalEntityMetadataBuilder(IODataEntryMetadataContext entryMetadataContext, IODataMetadataContext metadataContext, ODataUriBuilder uriBuilder)
		{
			this.entryMetadataContext = entryMetadataContext;
			this.uriBuilder = uriBuilder;
			this.metadataContext = metadataContext;
			this.processedNavigationLinks = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0001ADC1 File Offset: 0x00018FC1
		private string ComputedId
		{
			get
			{
				this.ComputeAndCacheId();
				return this.computedId;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0001ADCF File Offset: 0x00018FCF
		private Uri ComputedEntityInstanceUri
		{
			get
			{
				this.ComputeAndCacheId();
				return this.computedEntityInstanceUri;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001ADE0 File Offset: 0x00018FE0
		private ODataMissingOperationGenerator MissingOperationGenerator
		{
			get
			{
				ODataMissingOperationGenerator odataMissingOperationGenerator;
				if ((odataMissingOperationGenerator = this.missingOperationGenerator) == null)
				{
					odataMissingOperationGenerator = (this.missingOperationGenerator = new ODataMissingOperationGenerator(this.entryMetadataContext, this.metadataContext));
				}
				return odataMissingOperationGenerator;
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001AE14 File Offset: 0x00019014
		internal override Uri GetEditLink()
		{
			Uri nonComputedEditLink;
			if (!this.entryMetadataContext.Entry.HasNonComputedEditLink)
			{
				if ((nonComputedEditLink = this.computedEditLink) == null)
				{
					return this.computedEditLink = this.ComputeEditLink();
				}
			}
			else
			{
				nonComputedEditLink = this.entryMetadataContext.Entry.NonComputedEditLink;
			}
			return nonComputedEditLink;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001AE60 File Offset: 0x00019060
		internal override Uri GetReadLink()
		{
			Uri nonComputedReadLink;
			if (!this.entryMetadataContext.Entry.HasNonComputedReadLink)
			{
				if ((nonComputedReadLink = this.computedReadLink) == null)
				{
					return this.computedReadLink = this.GetEditLink();
				}
			}
			else
			{
				nonComputedReadLink = this.entryMetadataContext.Entry.NonComputedReadLink;
			}
			return nonComputedReadLink;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001AEAC File Offset: 0x000190AC
		internal override string GetId()
		{
			if (this.entryMetadataContext.Entry.HasNonComputedId)
			{
				return this.entryMetadataContext.Entry.NonComputedId;
			}
			if (this.entryMetadataContext.Entry.HasNonComputedReadLink)
			{
				return UriUtilsCommon.UriToString(this.entryMetadataContext.Entry.NonComputedReadLink);
			}
			if (this.entryMetadataContext.Entry.NonComputedEditLink != null)
			{
				return UriUtilsCommon.UriToString(this.entryMetadataContext.Entry.NonComputedEditLink);
			}
			return this.ComputedId;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001AF38 File Offset: 0x00019138
		internal override string GetETag()
		{
			if (this.entryMetadataContext.Entry.HasNonComputedETag)
			{
				return this.entryMetadataContext.Entry.NonComputedETag;
			}
			if (!this.etagComputed)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, object> keyValuePair in this.entryMetadataContext.ETagProperties)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(',');
					}
					else
					{
						stringBuilder.Append("W/\"");
					}
					string text;
					if (keyValuePair.Value == null)
					{
						text = "null";
					}
					else
					{
						text = LiteralFormatter.ForConstants.Format(keyValuePair.Value);
					}
					stringBuilder.Append(text);
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('"');
					this.computedETag = stringBuilder.ToString();
				}
				this.etagComputed = true;
			}
			return this.computedETag;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001B030 File Offset: 0x00019230
		internal override ODataStreamReferenceValue GetMediaResource()
		{
			if (this.entryMetadataContext.Entry.NonComputedMediaResource != null)
			{
				return this.entryMetadataContext.Entry.NonComputedMediaResource;
			}
			if (this.computedMediaResource == null && this.entryMetadataContext.TypeContext.IsMediaLinkEntry)
			{
				this.computedMediaResource = new ODataStreamReferenceValue();
				this.computedMediaResource.SetMetadataBuilder(this, null);
			}
			return this.computedMediaResource;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001B098 File Offset: 0x00019298
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataProperty>(nonComputedProperties, this.GetComputedStreamProperties(nonComputedProperties));
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001B0A7 File Offset: 0x000192A7
		internal override IEnumerable<ODataAction> GetActions()
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataAction>(this.entryMetadataContext.Entry.NonComputedActions, this.MissingOperationGenerator.GetComputedActions());
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001B0C9 File Offset: 0x000192C9
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataFunction>(this.entryMetadataContext.Entry.NonComputedFunctions, this.MissingOperationGenerator.GetComputedFunctions());
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001B0EB File Offset: 0x000192EB
		internal override void MarkNavigationLinkProcessed(string navigationPropertyName)
		{
			this.processedNavigationLinks.Add(navigationPropertyName);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001B110 File Offset: 0x00019310
		internal override ODataJsonLightReaderNavigationLinkInfo GetNextUnprocessedNavigationLink()
		{
			if (this.unprocessedNavigationLinks == null)
			{
				this.unprocessedNavigationLinks = this.entryMetadataContext.SelectedNavigationProperties.Where((IEdmNavigationProperty p) => !this.processedNavigationLinks.Contains(p.Name)).Select(new Func<IEdmNavigationProperty, ODataJsonLightReaderNavigationLinkInfo>(ODataJsonLightReaderNavigationLinkInfo.CreateProjectedNavigationLinkInfo)).GetEnumerator();
			}
			if (this.unprocessedNavigationLinks.MoveNext())
			{
				return this.unprocessedNavigationLinks.Current;
			}
			return null;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001B17E File Offset: 0x0001937E
		internal override Uri GetStreamEditLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return this.uriBuilder.BuildStreamEditLinkUri(this.GetEditLink(), streamPropertyName);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001B19D File Offset: 0x0001939D
		internal override Uri GetStreamReadLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return this.uriBuilder.BuildStreamReadLinkUri(this.GetReadLink(), streamPropertyName);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001B1BC File Offset: 0x000193BC
		internal override Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNavigationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			if (!hasNavigationLinkUrl)
			{
				return this.uriBuilder.BuildNavigationLinkUri(this.GetEditLink(), navigationPropertyName);
			}
			return navigationLinkUrl;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001B1E0 File Offset: 0x000193E0
		internal override Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			if (!hasAssociationLinkUrl)
			{
				return this.uriBuilder.BuildAssociationLinkUri(this.GetEditLink(), navigationPropertyName);
			}
			return associationLinkUrl;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001B204 File Offset: 0x00019404
		internal override Uri GetOperationTargetUri(string operationName, string bindingParameterTypeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			Uri editLink;
			if (string.IsNullOrEmpty(bindingParameterTypeName) || this.entryMetadataContext.Entry.NonComputedEditLink != null)
			{
				editLink = this.GetEditLink();
			}
			else
			{
				editLink = this.ComputedEntityInstanceUri;
			}
			return this.uriBuilder.BuildOperationTargetUri(editLink, operationName, bindingParameterTypeName);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001B25A File Offset: 0x0001945A
		internal override string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return operationName;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001B268 File Offset: 0x00019468
		private Uri ComputeEditLink()
		{
			Uri uri = this.ComputedEntityInstanceUri;
			if (this.entryMetadataContext.ActualEntityTypeName != this.entryMetadataContext.TypeContext.EntitySetElementTypeName)
			{
				uri = this.uriBuilder.AppendTypeSegment(uri, this.entryMetadataContext.ActualEntityTypeName);
			}
			return uri;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001B2B8 File Offset: 0x000194B8
		private void ComputeAndCacheId()
		{
			if (this.computedEntityInstanceUri == null)
			{
				Uri uri = this.uriBuilder.BuildBaseUri();
				uri = this.uriBuilder.BuildEntitySetUri(uri, this.entryMetadataContext.TypeContext.EntitySetName);
				uri = this.uriBuilder.BuildEntityInstanceUri(uri, this.entryMetadataContext.KeyProperties, this.entryMetadataContext.ActualEntityTypeName);
				this.computedEntityInstanceUri = uri;
				this.computedId = UriUtilsCommon.UriToString(uri);
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001B334 File Offset: 0x00019534
		private IEnumerable<ODataProperty> GetComputedStreamProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			if (this.computedStreamProperties == null)
			{
				IDictionary<string, IEdmStructuralProperty> selectedStreamProperties = this.entryMetadataContext.SelectedStreamProperties;
				if (nonComputedProperties != null)
				{
					foreach (ODataProperty odataProperty in nonComputedProperties)
					{
						selectedStreamProperties.Remove(odataProperty.Name);
					}
				}
				this.computedStreamProperties = new List<ODataProperty>();
				if (selectedStreamProperties.Count > 0)
				{
					foreach (string text in selectedStreamProperties.Keys)
					{
						ODataStreamReferenceValue odataStreamReferenceValue = new ODataStreamReferenceValue();
						odataStreamReferenceValue.SetMetadataBuilder(this, text);
						this.computedStreamProperties.Add(new ODataProperty
						{
							Name = text,
							Value = odataStreamReferenceValue
						});
					}
				}
			}
			return this.computedStreamProperties;
		}

		// Token: 0x04000321 RID: 801
		private readonly ODataUriBuilder uriBuilder;

		// Token: 0x04000322 RID: 802
		private readonly IODataEntryMetadataContext entryMetadataContext;

		// Token: 0x04000323 RID: 803
		private readonly IODataMetadataContext metadataContext;

		// Token: 0x04000324 RID: 804
		private readonly HashSet<string> processedNavigationLinks;

		// Token: 0x04000325 RID: 805
		private Uri computedEditLink;

		// Token: 0x04000326 RID: 806
		private Uri computedReadLink;

		// Token: 0x04000327 RID: 807
		private string computedETag;

		// Token: 0x04000328 RID: 808
		private bool etagComputed;

		// Token: 0x04000329 RID: 809
		private string computedId;

		// Token: 0x0400032A RID: 810
		private Uri computedEntityInstanceUri;

		// Token: 0x0400032B RID: 811
		private ODataStreamReferenceValue computedMediaResource;

		// Token: 0x0400032C RID: 812
		private List<ODataProperty> computedStreamProperties;

		// Token: 0x0400032D RID: 813
		private IEnumerator<ODataJsonLightReaderNavigationLinkInfo> unprocessedNavigationLinks;

		// Token: 0x0400032E RID: 814
		private ODataMissingOperationGenerator missingOperationGenerator;
	}
}

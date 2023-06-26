using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x0200013A RID: 314
	internal sealed class ODataConventionalUriBuilder : ODataUriBuilder
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x0001B4AE File Offset: 0x000196AE
		internal ODataConventionalUriBuilder(Uri serviceBaseUri, UrlConvention urlConvention)
		{
			this.serviceBaseUri = serviceBaseUri;
			this.urlConvention = urlConvention;
			this.keySerializer = KeySerializer.Create(this.urlConvention);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001B4D5 File Offset: 0x000196D5
		internal override Uri BuildBaseUri()
		{
			return this.serviceBaseUri;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001B4DD File Offset: 0x000196DD
		internal override Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, entitySetName, true);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001B4F4 File Offset: 0x000196F4
		internal override Uri BuildEntityInstanceUri(Uri baseUri, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder(UriUtilsCommon.UriToString(baseUri));
			this.AppendKeyExpression(stringBuilder, keyProperties, entityTypeName);
			return new Uri(stringBuilder.ToString(), UriKind.Absolute);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001B522 File Offset: 0x00019722
		internal override Uri BuildStreamEditLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			if (streamPropertyName == null)
			{
				return ODataConventionalUriBuilder.AppendSegment(baseUri, "$value", false);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, streamPropertyName, true);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001B547 File Offset: 0x00019747
		internal override Uri BuildStreamReadLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			if (streamPropertyName == null)
			{
				return ODataConventionalUriBuilder.AppendSegment(baseUri, "$value", false);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, streamPropertyName, true);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001B56C File Offset: 0x0001976C
		internal override Uri BuildNavigationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, navigationPropertyName, true);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001B584 File Offset: 0x00019784
		internal override Uri BuildAssociationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			Uri uri = ODataConventionalUriBuilder.AppendSegment(baseUri, "$links/", false);
			return ODataConventionalUriBuilder.AppendSegment(uri, navigationPropertyName, true);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001B5B4 File Offset: 0x000197B4
		internal override Uri BuildOperationTargetUri(Uri baseUri, string operationName, string bindingParameterTypeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			if (!string.IsNullOrEmpty(bindingParameterTypeName))
			{
				Uri uri = ODataConventionalUriBuilder.AppendSegment(baseUri, bindingParameterTypeName, true);
				return ODataConventionalUriBuilder.AppendSegment(uri, operationName, true);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, operationName, true);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001B5EE File Offset: 0x000197EE
		internal override Uri AppendTypeSegment(Uri baseUri, string typeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(typeName, "typeName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, typeName, true);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001B603 File Offset: 0x00019803
		[Conditional("DEBUG")]
		private static void ValidateBaseUri(Uri baseUri)
		{
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001B608 File Offset: 0x00019808
		private static Uri AppendSegment(Uri baseUri, string segment, bool escapeSegment)
		{
			string text = UriUtilsCommon.UriToString(baseUri);
			if (escapeSegment)
			{
				segment = Uri.EscapeDataString(segment);
			}
			if (text[text.Length - 1] != '/')
			{
				return new Uri(text + "/" + segment, UriKind.Absolute);
			}
			return new Uri(baseUri, segment);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001B653 File Offset: 0x00019853
		private static object ValidateKeyValue(string keyPropertyName, object keyPropertyValue, string entityTypeName)
		{
			if (keyPropertyValue == null)
			{
				throw new ODataException(Strings.ODataConventionalUriBuilder_NullKeyValue(keyPropertyName, entityTypeName));
			}
			return keyPropertyValue;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001B694 File Offset: 0x00019894
		private void AppendKeyExpression(StringBuilder builder, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			if (!keyProperties.Any<KeyValuePair<string, object>>())
			{
				throw new ODataException(Strings.ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties(entityTypeName));
			}
			this.keySerializer.AppendKeyExpression<KeyValuePair<string, object>>(builder, keyProperties, (KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => ODataConventionalUriBuilder.ValidateKeyValue(p.Key, p.Value, entityTypeName));
		}

		// Token: 0x0400032F RID: 815
		private readonly Uri serviceBaseUri;

		// Token: 0x04000330 RID: 816
		private readonly UrlConvention urlConvention;

		// Token: 0x04000331 RID: 817
		private readonly KeySerializer keySerializer;
	}
}

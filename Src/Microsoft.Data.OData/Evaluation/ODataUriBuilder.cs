using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000139 RID: 313
	internal abstract class ODataUriBuilder
	{
		// Token: 0x0600084E RID: 2126 RVA: 0x0001B428 File Offset: 0x00019628
		internal virtual Uri BuildBaseUri()
		{
			return null;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001B42B File Offset: 0x0001962B
		internal virtual Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			return null;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001B439 File Offset: 0x00019639
		internal virtual Uri BuildEntityInstanceUri(Uri baseUri, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			ExceptionUtils.CheckArgumentNotNull<ICollection<KeyValuePair<string, object>>>(keyProperties, "keyProperties");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entityTypeName, "entityTypeName");
			return null;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001B452 File Offset: 0x00019652
		internal virtual Uri BuildStreamEditLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001B460 File Offset: 0x00019660
		internal virtual Uri BuildStreamReadLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001B46E File Offset: 0x0001966E
		internal virtual Uri BuildNavigationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001B47C File Offset: 0x0001967C
		internal virtual Uri BuildAssociationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001B48A File Offset: 0x0001968A
		internal virtual Uri BuildOperationTargetUri(Uri baseUri, string operationName, string bindingParameterTypeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001B498 File Offset: 0x00019698
		internal virtual Uri AppendTypeSegment(Uri baseUri, string typeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(typeName, "typeName");
			return null;
		}
	}
}

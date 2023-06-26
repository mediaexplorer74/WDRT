using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x0200005D RID: 93
	internal static class SelectPathSegmentTokenBinder
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00009A48 File Offset: 0x00007C48
		public static ODataPathSegment ConvertNonTypeTokenToSegment(PathSegmentToken tokenIn, IEdmModel model, IEdmEntityType entityType)
		{
			ODataPathSegment odataPathSegment;
			if (SelectPathSegmentTokenBinder.TryBindAsDeclaredProperty(tokenIn, entityType, out odataPathSegment))
			{
				return odataPathSegment;
			}
			if ((!entityType.IsOpen || tokenIn.IsNamespaceOrContainerQualified()) && SelectPathSegmentTokenBinder.TryBindAsOperation(tokenIn, model, entityType, out odataPathSegment))
			{
				return odataPathSegment;
			}
			if (entityType.IsOpen)
			{
				return new OpenPropertySegment(tokenIn.Identifier);
			}
			throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(entityType.FullName(), tokenIn.Identifier));
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009AAC File Offset: 0x00007CAC
		public static bool TryBindAsWildcard(PathSegmentToken tokenIn, IEdmModel model, out SelectItem item)
		{
			bool flag = tokenIn.IsNamespaceOrContainerQualified();
			bool flag2 = tokenIn.Identifier.EndsWith("*", StringComparison.Ordinal);
			IEdmEntityContainer edmEntityContainer;
			if (flag && flag2 && UriEdmHelpers.TryGetEntityContainer(tokenIn.Identifier.Substring(0, tokenIn.Identifier.LastIndexOf('.')), model, out edmEntityContainer))
			{
				item = new ContainerQualifiedWildcardSelectItem(edmEntityContainer);
				return true;
			}
			if (tokenIn.Identifier == "*")
			{
				item = new WildcardSelectItem();
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009B24 File Offset: 0x00007D24
		private static bool TryBindAsOperation(PathSegmentToken pathToken, IEdmModel model, IEdmEntityType entityType, out ODataPathSegment segment)
		{
			IODataUriParserModelExtensions iodataUriParserModelExtensions = model as IODataUriParserModelExtensions;
			IEnumerable<IEdmFunctionImport> enumerable;
			if (iodataUriParserModelExtensions != null)
			{
				enumerable = iodataUriParserModelExtensions.FindFunctionImportsByBindingParameterTypeHierarchy(entityType, pathToken.Identifier);
			}
			else
			{
				enumerable = model.FindFunctionImportsByBindingParameterTypeHierarchy(entityType, pathToken.Identifier);
			}
			List<IEdmFunctionImport> list = enumerable.ToList<IEdmFunctionImport>();
			if (list.Count <= 0)
			{
				segment = null;
				return false;
			}
			segment = new OperationSegment(list, null);
			return true;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009B78 File Offset: 0x00007D78
		private static bool TryBindAsDeclaredProperty(PathSegmentToken tokenIn, IEdmEntityType entityType, out ODataPathSegment segment)
		{
			IEdmProperty edmProperty = entityType.FindProperty(tokenIn.Identifier);
			if (edmProperty == null)
			{
				segment = null;
				return false;
			}
			if (edmProperty.PropertyKind == EdmPropertyKind.Structural)
			{
				segment = new PropertySegment((IEdmStructuralProperty)edmProperty);
				return true;
			}
			if (edmProperty.PropertyKind == EdmPropertyKind.Navigation)
			{
				segment = new NavigationPropertySegment((IEdmNavigationProperty)edmProperty, null);
				return true;
			}
			throw new ODataException(Strings.SelectExpandBinder_UnknownPropertyType(edmProperty.Name));
		}
	}
}

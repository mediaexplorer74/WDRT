using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000149 RID: 329
	internal sealed class EdmTypeReaderResolver : EdmTypeResolver
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x0001C6CD File Offset: 0x0001A8CD
		public EdmTypeReaderResolver(IEdmModel model, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			this.model = model;
			this.readerBehavior = readerBehavior;
			this.version = version;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001C6EA File Offset: 0x0001A8EA
		internal override IEdmEntityType GetElementType(IEdmEntitySet entitySet)
		{
			if (entitySet != null)
			{
				return (IEdmEntityType)this.ResolveType(entitySet.ElementType);
			}
			return null;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001C702 File Offset: 0x0001A902
		internal override IEdmTypeReference GetReturnType(IEdmFunctionImport functionImport)
		{
			if (functionImport != null && functionImport.ReturnType != null)
			{
				return this.ResolveTypeReference(functionImport.ReturnType);
			}
			return null;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001C720 File Offset: 0x0001A920
		internal override IEdmTypeReference GetReturnType(IEnumerable<IEdmFunctionImport> functionImportGroup)
		{
			IEdmFunctionImport edmFunctionImport = functionImportGroup.FirstOrDefault<IEdmFunctionImport>();
			return this.GetReturnType(edmFunctionImport);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001C73B File Offset: 0x0001A93B
		internal override IEdmTypeReference GetParameterType(IEdmFunctionParameter functionParameter)
		{
			if (functionParameter != null)
			{
				return this.ResolveTypeReference(functionParameter.Type);
			}
			return null;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001C750 File Offset: 0x0001A950
		private IEdmTypeReference ResolveTypeReference(IEdmTypeReference typeReferenceToResolve)
		{
			if (this.readerBehavior.TypeResolver == null)
			{
				return typeReferenceToResolve;
			}
			return this.ResolveType(typeReferenceToResolve.Definition).ToTypeReference(typeReferenceToResolve.IsNullable);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001C788 File Offset: 0x0001A988
		private IEdmType ResolveType(IEdmType typeToResolve)
		{
			Func<IEdmType, string, IEdmType> typeResolver = this.readerBehavior.TypeResolver;
			if (typeResolver == null)
			{
				return typeToResolve;
			}
			IEdmCollectionType edmCollectionType = typeToResolve as IEdmCollectionType;
			EdmTypeKind edmTypeKind;
			if (edmCollectionType != null && edmCollectionType.ElementType.IsEntity())
			{
				IEdmTypeReference elementType = edmCollectionType.ElementType;
				IEdmType edmType = MetadataUtils.ResolveTypeName(this.model, null, elementType.FullName(), typeResolver, this.version, out edmTypeKind);
				return new EdmCollectionType(edmType.ToTypeReference(elementType.IsNullable));
			}
			return MetadataUtils.ResolveTypeName(this.model, null, typeToResolve.ODataFullName(), typeResolver, this.version, out edmTypeKind);
		}

		// Token: 0x04000355 RID: 853
		private readonly IEdmModel model;

		// Token: 0x04000356 RID: 854
		private readonly ODataReaderBehavior readerBehavior;

		// Token: 0x04000357 RID: 855
		private readonly ODataVersion version;
	}
}

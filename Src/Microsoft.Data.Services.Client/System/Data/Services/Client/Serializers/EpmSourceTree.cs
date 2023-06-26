using System;
using System.Data.Services.Client.Metadata;
using System.Linq;
using Microsoft.Data.Edm;

namespace System.Data.Services.Client.Serializers
{
	// Token: 0x02000017 RID: 23
	internal sealed class EpmSourceTree
	{
		// Token: 0x06000076 RID: 118 RVA: 0x0000397E File Offset: 0x00001B7E
		internal EpmSourceTree(EpmTargetTree epmTargetTree)
		{
			this.root = new EpmSourcePathSegment();
			this.epmTargetTree = epmTargetTree;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003998 File Offset: 0x00001B98
		internal EpmSourcePathSegment Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000039BC File Offset: 0x00001BBC
		internal void Add(EntityPropertyMappingInfo epmInfo)
		{
			EpmSourcePathSegment epmSourcePathSegment = this.Root;
			EpmSourcePathSegment epmSourcePathSegment2 = null;
			ClientTypeAnnotation clientTypeAnnotation = epmInfo.ActualPropertyType;
			for (int i = 0; i < epmInfo.PropertyValuePath.Length; i++)
			{
				string propertyName = epmInfo.PropertyValuePath[i];
				if (propertyName.Length == 0)
				{
					throw new InvalidOperationException(Strings.EpmSourceTree_InvalidSourcePath(epmInfo.DefiningType.Name, epmInfo.Attribute.SourcePath));
				}
				clientTypeAnnotation = EpmSourceTree.GetPropertyType(clientTypeAnnotation, propertyName);
				epmSourcePathSegment2 = epmSourcePathSegment.SubProperties.SingleOrDefault((EpmSourcePathSegment e) => e.PropertyName == propertyName);
				if (epmSourcePathSegment2 != null)
				{
					epmSourcePathSegment = epmSourcePathSegment2;
				}
				else
				{
					EpmSourcePathSegment epmSourcePathSegment3 = new EpmSourcePathSegment(propertyName);
					epmSourcePathSegment.SubProperties.Add(epmSourcePathSegment3);
					epmSourcePathSegment = epmSourcePathSegment3;
				}
			}
			if (clientTypeAnnotation != null && !PrimitiveType.IsKnownNullableType(clientTypeAnnotation.ElementType))
			{
				throw new InvalidOperationException(Strings.EpmSourceTree_EndsWithNonPrimitiveType(epmSourcePathSegment.PropertyName));
			}
			if (epmSourcePathSegment2 != null)
			{
				if (epmSourcePathSegment2.EpmInfo.DefiningTypesAreEqual(epmInfo))
				{
					throw new InvalidOperationException(Strings.EpmSourceTree_DuplicateEpmAttrsWithSameSourceName(epmInfo.Attribute.SourcePath, epmInfo.DefiningType.Name));
				}
				this.epmTargetTree.Remove(epmSourcePathSegment2.EpmInfo);
			}
			epmSourcePathSegment.EpmInfo = epmInfo;
			this.epmTargetTree.Add(epmInfo);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003AFA File Offset: 0x00001CFA
		internal void Validate(ClientTypeAnnotation resourceType)
		{
			EpmSourceTree.Validate(this.Root, resourceType);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003B08 File Offset: 0x00001D08
		private static void Validate(EpmSourcePathSegment pathSegment, ClientTypeAnnotation resourceType)
		{
			foreach (EpmSourcePathSegment epmSourcePathSegment in pathSegment.SubProperties)
			{
				ClientTypeAnnotation propertyType = EpmSourceTree.GetPropertyType(resourceType, epmSourcePathSegment.PropertyName);
				EpmSourceTree.Validate(epmSourcePathSegment, propertyType);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003B68 File Offset: 0x00001D68
		private static ClientTypeAnnotation GetPropertyType(ClientTypeAnnotation clientType, string propertyName)
		{
			ClientPropertyAnnotation property = clientType.GetProperty(propertyName, true);
			if (property == null)
			{
				throw Error.InvalidOperation(Strings.EpmSourceTree_InaccessiblePropertyOnType(propertyName, clientType.ElementTypeName));
			}
			if (property.IsStreamLinkProperty)
			{
				throw Error.InvalidOperation(Strings.EpmSourceTree_NamedStreamCannotBeMapped(propertyName, clientType.ElementTypeName));
			}
			if (property.IsSpatialType)
			{
				throw Error.InvalidOperation(Strings.EpmSourceTree_SpatialTypeCannotBeMapped(propertyName, clientType.ElementTypeName));
			}
			if (property.IsPrimitiveOrComplexCollection)
			{
				throw Error.InvalidOperation(Strings.EpmSourceTree_CollectionPropertyCannotBeMapped(propertyName, clientType.ElementTypeName));
			}
			ClientEdmModel model = property.Model;
			IEdmType orCreateEdmType = model.GetOrCreateEdmType(property.PropertyType);
			IEdmType edmType = orCreateEdmType;
			return model.GetClientTypeAnnotation(edmType);
		}

		// Token: 0x04000169 RID: 361
		private readonly EpmSourcePathSegment root;

		// Token: 0x0400016A RID: 362
		private readonly EpmTargetTree epmTargetTree;
	}
}

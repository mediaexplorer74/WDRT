﻿using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x020000FD RID: 253
	internal sealed class CollectionWithoutExpectedTypeValidator
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x00017E85 File Offset: 0x00016085
		internal CollectionWithoutExpectedTypeValidator(string itemTypeNameFromCollection)
		{
			if (itemTypeNameFromCollection != null)
			{
				this.itemTypeName = itemTypeNameFromCollection;
				this.itemTypeKind = CollectionWithoutExpectedTypeValidator.ComputeExpectedTypeKind(this.itemTypeName, out this.primitiveItemType);
				this.itemTypeDerivedFromCollectionValue = true;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00017EB5 File Offset: 0x000160B5
		internal string ItemTypeNameFromCollection
		{
			get
			{
				if (!this.itemTypeDerivedFromCollectionValue)
				{
					return null;
				}
				return this.itemTypeName;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00017EC7 File Offset: 0x000160C7
		internal EdmTypeKind ItemTypeKindFromCollection
		{
			get
			{
				if (!this.itemTypeDerivedFromCollectionValue)
				{
					return EdmTypeKind.None;
				}
				return this.itemTypeKind;
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00017EDC File Offset: 0x000160DC
		internal void ValidateCollectionItem(string collectionItemTypeName, EdmTypeKind collectionItemTypeKind)
		{
			if (collectionItemTypeKind != EdmTypeKind.Primitive && collectionItemTypeKind != EdmTypeKind.Complex)
			{
				throw new ODataException(Strings.CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind(collectionItemTypeKind));
			}
			if (this.itemTypeDerivedFromCollectionValue)
			{
				collectionItemTypeName = collectionItemTypeName ?? this.itemTypeName;
				this.ValidateCollectionItemTypeNameAndKind(collectionItemTypeName, collectionItemTypeKind);
				return;
			}
			if (this.itemTypeKind == EdmTypeKind.None)
			{
				this.itemTypeKind = ((collectionItemTypeName == null) ? collectionItemTypeKind : CollectionWithoutExpectedTypeValidator.ComputeExpectedTypeKind(collectionItemTypeName, out this.primitiveItemType));
				if (collectionItemTypeName == null)
				{
					this.itemTypeKind = collectionItemTypeKind;
					if (this.itemTypeKind == EdmTypeKind.Primitive)
					{
						this.itemTypeName = "Edm.String";
						this.primitiveItemType = EdmCoreModel.Instance.GetString(false).PrimitiveDefinition();
					}
					else
					{
						this.itemTypeName = null;
						this.primitiveItemType = null;
					}
				}
				else
				{
					this.itemTypeKind = CollectionWithoutExpectedTypeValidator.ComputeExpectedTypeKind(collectionItemTypeName, out this.primitiveItemType);
					this.itemTypeName = collectionItemTypeName;
				}
			}
			if (collectionItemTypeName == null && collectionItemTypeKind == EdmTypeKind.Primitive)
			{
				collectionItemTypeName = "Edm.String";
			}
			this.ValidateCollectionItemTypeNameAndKind(collectionItemTypeName, collectionItemTypeKind);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00017FB8 File Offset: 0x000161B8
		private static EdmTypeKind ComputeExpectedTypeKind(string typeName, out IEdmPrimitiveType primitiveItemType)
		{
			IEdmSchemaType edmSchemaType = EdmCoreModel.Instance.FindDeclaredType(typeName);
			if (edmSchemaType != null)
			{
				primitiveItemType = (IEdmPrimitiveType)edmSchemaType;
				return EdmTypeKind.Primitive;
			}
			primitiveItemType = null;
			return EdmTypeKind.Complex;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00017FE4 File Offset: 0x000161E4
		private void ValidateCollectionItemTypeNameAndKind(string collectionItemTypeName, EdmTypeKind collectionItemTypeKind)
		{
			if (this.itemTypeKind != collectionItemTypeKind)
			{
				throw new ODataException(Strings.CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind(collectionItemTypeKind, this.itemTypeKind));
			}
			if (this.itemTypeKind == EdmTypeKind.Primitive)
			{
				if (string.CompareOrdinal(this.itemTypeName, collectionItemTypeName) == 0)
				{
					return;
				}
				if (this.primitiveItemType.IsSpatial())
				{
					EdmPrimitiveTypeKind primitiveTypeKind = EdmCoreModel.Instance.GetPrimitiveTypeKind(collectionItemTypeName);
					IEdmPrimitiveType primitiveType = EdmCoreModel.Instance.GetPrimitiveType(primitiveTypeKind);
					if (this.itemTypeDerivedFromCollectionValue)
					{
						if (this.primitiveItemType.IsAssignableFrom(primitiveType))
						{
							return;
						}
					}
					else
					{
						IEdmPrimitiveType commonBaseType = this.primitiveItemType.GetCommonBaseType(primitiveType);
						if (commonBaseType != null)
						{
							this.primitiveItemType = commonBaseType;
							this.itemTypeName = commonBaseType.ODataFullName();
							return;
						}
					}
				}
				throw new ODataException(Strings.CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName(collectionItemTypeName, this.itemTypeName));
			}
			else
			{
				if (string.CompareOrdinal(this.itemTypeName, collectionItemTypeName) != 0)
				{
					throw new ODataException(Strings.CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName(collectionItemTypeName, this.itemTypeName));
				}
				return;
			}
		}

		// Token: 0x0400029A RID: 666
		private readonly bool itemTypeDerivedFromCollectionValue;

		// Token: 0x0400029B RID: 667
		private string itemTypeName;

		// Token: 0x0400029C RID: 668
		private IEdmPrimitiveType primitiveItemType;

		// Token: 0x0400029D RID: 669
		private EdmTypeKind itemTypeKind;
	}
}

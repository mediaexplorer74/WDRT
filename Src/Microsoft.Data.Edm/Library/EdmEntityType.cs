using System;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001D0 RID: 464
	public class EdmEntityType : EdmStructuredType, IEdmEntityType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000AFB RID: 2811 RVA: 0x00020428 File Offset: 0x0001E628
		public EdmEntityType(string namespaceName, string name)
			: this(namespaceName, name, null, false, false)
		{
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00020435 File Offset: 0x0001E635
		public EdmEntityType(string namespaceName, string name, IEdmEntityType baseType)
			: this(namespaceName, name, baseType, false, false)
		{
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00020442 File Offset: 0x0001E642
		public EdmEntityType(string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen)
			: base(isAbstract, isOpen, baseType)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00020475 File Offset: 0x0001E675
		public virtual IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return this.declaredKey;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0002047D File Offset: 0x0001E67D
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00020480 File Offset: 0x0001E680
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00020488 File Offset: 0x0001E688
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00020490 File Offset: 0x0001E690
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00020493 File Offset: 0x0001E693
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00020496 File Offset: 0x0001E696
		public void AddKeys(params IEdmStructuralProperty[] keyProperties)
		{
			this.AddKeys((IEnumerable<IEdmStructuralProperty>)keyProperties);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000204A4 File Offset: 0x0001E6A4
		public void AddKeys(IEnumerable<IEdmStructuralProperty> keyProperties)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmStructuralProperty>>(keyProperties, "keyProperties");
			foreach (IEdmStructuralProperty edmStructuralProperty in keyProperties)
			{
				if (this.declaredKey == null)
				{
					this.declaredKey = new List<IEdmStructuralProperty>();
				}
				this.declaredKey.Add(edmStructuralProperty);
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00020510 File Offset: 0x0001E710
		public EdmNavigationProperty AddUnidirectionalNavigation(EdmNavigationPropertyInfo propertyInfo)
		{
			return this.AddUnidirectionalNavigation(propertyInfo, this.FixUpDefaultPartnerInfo(propertyInfo, null));
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00020524 File Offset: 0x0001E724
		public EdmNavigationProperty AddUnidirectionalNavigation(EdmNavigationPropertyInfo propertyInfo, EdmNavigationPropertyInfo partnerInfo)
		{
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(propertyInfo, "propertyInfo");
			EdmNavigationProperty edmNavigationProperty = EdmNavigationProperty.CreateNavigationPropertyWithPartner(propertyInfo, this.FixUpDefaultPartnerInfo(propertyInfo, partnerInfo));
			base.AddProperty(edmNavigationProperty);
			return edmNavigationProperty;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00020554 File Offset: 0x0001E754
		public EdmNavigationProperty AddBidirectionalNavigation(EdmNavigationPropertyInfo propertyInfo, EdmNavigationPropertyInfo partnerInfo)
		{
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(propertyInfo, "propertyInfo");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(propertyInfo.Target, "propertyInfo.Target");
			EdmEntityType edmEntityType = propertyInfo.Target as EdmEntityType;
			if (edmEntityType == null)
			{
				throw new ArgumentException("propertyInfo.Target", Strings.Constructable_TargetMustBeStock(typeof(EdmEntityType).FullName));
			}
			EdmNavigationProperty edmNavigationProperty = EdmNavigationProperty.CreateNavigationPropertyWithPartner(propertyInfo, this.FixUpDefaultPartnerInfo(propertyInfo, partnerInfo));
			base.AddProperty(edmNavigationProperty);
			edmEntityType.AddProperty(edmNavigationProperty.Partner);
			return edmNavigationProperty;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000205D0 File Offset: 0x0001E7D0
		private EdmNavigationPropertyInfo FixUpDefaultPartnerInfo(EdmNavigationPropertyInfo propertyInfo, EdmNavigationPropertyInfo partnerInfo)
		{
			EdmNavigationPropertyInfo edmNavigationPropertyInfo = null;
			if (partnerInfo == null)
			{
				edmNavigationPropertyInfo = (partnerInfo = new EdmNavigationPropertyInfo());
			}
			if (partnerInfo.Name == null)
			{
				if (edmNavigationPropertyInfo == null)
				{
					edmNavigationPropertyInfo = partnerInfo.Clone();
				}
				edmNavigationPropertyInfo.Name = (propertyInfo.Name ?? string.Empty) + "Partner";
			}
			if (partnerInfo.Target == null)
			{
				if (edmNavigationPropertyInfo == null)
				{
					edmNavigationPropertyInfo = partnerInfo.Clone();
				}
				edmNavigationPropertyInfo.Target = this;
			}
			if (partnerInfo.TargetMultiplicity == EdmMultiplicity.Unknown)
			{
				if (edmNavigationPropertyInfo == null)
				{
					edmNavigationPropertyInfo = partnerInfo.Clone();
				}
				edmNavigationPropertyInfo.TargetMultiplicity = EdmMultiplicity.ZeroOrOne;
			}
			return edmNavigationPropertyInfo ?? partnerInfo;
		}

		// Token: 0x0400052E RID: 1326
		private readonly string namespaceName;

		// Token: 0x0400052F RID: 1327
		private readonly string name;

		// Token: 0x04000530 RID: 1328
		private List<IEdmStructuralProperty> declaredKey;
	}
}

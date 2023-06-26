using System;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001D7 RID: 471
	public sealed class EdmNavigationProperty : EdmProperty, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x00020ADE File Offset: 0x0001ECDE
		private EdmNavigationProperty(IEdmEntityType declaringType, string name, IEdmTypeReference type, IEnumerable<IEdmStructuralProperty> dependentProperties, bool containsTarget, EdmOnDeleteAction onDelete)
			: base(declaringType, name, type)
		{
			this.dependentProperties = dependentProperties;
			this.containsTarget = containsTarget;
			this.onDelete = onDelete;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00020B01 File Offset: 0x0001ED01
		public override EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Navigation;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00020B04 File Offset: 0x0001ED04
		public bool ContainsTarget
		{
			get
			{
				return this.containsTarget;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00020B0C File Offset: 0x0001ED0C
		public IEnumerable<IEdmStructuralProperty> DependentProperties
		{
			get
			{
				return this.dependentProperties;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00020B14 File Offset: 0x0001ED14
		public bool IsPrincipal
		{
			get
			{
				return this.DependentProperties == null && this.partner != null && this.partner.DependentProperties != null;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00020B39 File Offset: 0x0001ED39
		public IEdmEntityType DeclaringEntityType
		{
			get
			{
				return (IEdmEntityType)base.DeclaringType;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00020B46 File Offset: 0x0001ED46
		public EdmOnDeleteAction OnDelete
		{
			get
			{
				return this.onDelete;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00020B4E File Offset: 0x0001ED4E
		public IEdmNavigationProperty Partner
		{
			get
			{
				return this.partner;
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00020B58 File Offset: 0x0001ED58
		public static EdmNavigationProperty CreateNavigationPropertyWithPartner(EdmNavigationPropertyInfo propertyInfo, EdmNavigationPropertyInfo partnerInfo)
		{
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(propertyInfo, "propertyInfo");
			EdmUtil.CheckArgumentNull<string>(propertyInfo.Name, "propertyInfo.Name");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(propertyInfo.Target, "propertyInfo.Target");
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(partnerInfo, "partnerInfo");
			EdmUtil.CheckArgumentNull<string>(partnerInfo.Name, "partnerInfo.Name");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(partnerInfo.Target, "partnerInfo.Target");
			EdmNavigationProperty edmNavigationProperty = new EdmNavigationProperty(partnerInfo.Target, propertyInfo.Name, EdmNavigationProperty.CreateNavigationPropertyType(propertyInfo.Target, propertyInfo.TargetMultiplicity, "propertyInfo.TargetMultiplicity"), propertyInfo.DependentProperties, propertyInfo.ContainsTarget, propertyInfo.OnDelete);
			EdmNavigationProperty edmNavigationProperty2 = new EdmNavigationProperty(propertyInfo.Target, partnerInfo.Name, EdmNavigationProperty.CreateNavigationPropertyType(partnerInfo.Target, partnerInfo.TargetMultiplicity, "partnerInfo.TargetMultiplicity"), partnerInfo.DependentProperties, partnerInfo.ContainsTarget, partnerInfo.OnDelete);
			edmNavigationProperty.partner = edmNavigationProperty2;
			edmNavigationProperty2.partner = edmNavigationProperty;
			return edmNavigationProperty;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00020C44 File Offset: 0x0001EE44
		public static EdmNavigationProperty CreateNavigationPropertyWithPartner(string propertyName, IEdmTypeReference propertyType, IEnumerable<IEdmStructuralProperty> dependentProperties, bool containsTarget, EdmOnDeleteAction onDelete, string partnerPropertyName, IEdmTypeReference partnerPropertyType, IEnumerable<IEdmStructuralProperty> partnerDependentProperties, bool partnerContainsTarget, EdmOnDeleteAction partnerOnDelete)
		{
			EdmUtil.CheckArgumentNull<string>(propertyName, "propertyName");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(propertyType, "propertyType");
			EdmUtil.CheckArgumentNull<string>(partnerPropertyName, "partnerPropertyName");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(partnerPropertyType, "partnerPropertyType");
			IEdmEntityType entityType = EdmNavigationProperty.GetEntityType(partnerPropertyType);
			if (entityType == null)
			{
				throw new ArgumentException(Strings.Constructable_EntityTypeOrCollectionOfEntityTypeExpected, "partnerPropertyType");
			}
			IEdmEntityType entityType2 = EdmNavigationProperty.GetEntityType(propertyType);
			if (entityType2 == null)
			{
				throw new ArgumentException(Strings.Constructable_EntityTypeOrCollectionOfEntityTypeExpected, "propertyType");
			}
			EdmNavigationProperty edmNavigationProperty = new EdmNavigationProperty(entityType, propertyName, propertyType, dependentProperties, containsTarget, onDelete);
			EdmNavigationProperty edmNavigationProperty2 = new EdmNavigationProperty(entityType2, partnerPropertyName, partnerPropertyType, partnerDependentProperties, partnerContainsTarget, partnerOnDelete);
			edmNavigationProperty.partner = edmNavigationProperty2;
			edmNavigationProperty2.partner = edmNavigationProperty;
			return edmNavigationProperty;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		private static IEdmEntityType GetEntityType(IEdmTypeReference type)
		{
			IEdmEntityType edmEntityType = null;
			if (type.IsEntity())
			{
				edmEntityType = (IEdmEntityType)type.Definition;
			}
			else if (type.IsCollection())
			{
				type = ((IEdmCollectionType)type.Definition).ElementType;
				if (type.IsEntity())
				{
					edmEntityType = (IEdmEntityType)type.Definition;
				}
			}
			return edmEntityType;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00020D3C File Offset: 0x0001EF3C
		private static IEdmTypeReference CreateNavigationPropertyType(IEdmEntityType entityType, EdmMultiplicity multiplicity, string multiplicityParameterName)
		{
			switch (multiplicity)
			{
			case EdmMultiplicity.ZeroOrOne:
				return new EdmEntityTypeReference(entityType, true);
			case EdmMultiplicity.One:
				return new EdmEntityTypeReference(entityType, false);
			case EdmMultiplicity.Many:
				return EdmCoreModel.GetCollection(new EdmEntityTypeReference(entityType, false));
			default:
				throw new ArgumentOutOfRangeException(multiplicityParameterName, Strings.UnknownEnumVal_Multiplicity(multiplicity));
			}
		}

		// Token: 0x04000544 RID: 1348
		private readonly bool containsTarget;

		// Token: 0x04000545 RID: 1349
		private readonly EdmOnDeleteAction onDelete;

		// Token: 0x04000546 RID: 1350
		private EdmNavigationProperty partner;

		// Token: 0x04000547 RID: 1351
		private IEnumerable<IEdmStructuralProperty> dependentProperties;
	}
}

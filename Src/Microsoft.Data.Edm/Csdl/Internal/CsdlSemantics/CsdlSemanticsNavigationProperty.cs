using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000177 RID: 375
	internal class CsdlSemanticsNavigationProperty : CsdlSemanticsElement, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x00017364 File Offset: 0x00015564
		public CsdlSemanticsNavigationProperty(CsdlSemanticsEntityTypeDefinition declaringType, CsdlNavigationProperty navigationProperty)
			: base(navigationProperty)
		{
			this.declaringType = declaringType;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x000173BD File Offset: 0x000155BD
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringType.Model;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x000173CA File Offset: 0x000155CA
		public override CsdlElement Element
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x000173D2 File Offset: 0x000155D2
		public string Name
		{
			get
			{
				return this.navigationProperty.Name;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x000173E0 File Offset: 0x000155E0
		public bool IsPrincipal
		{
			get
			{
				CsdlSemanticsReferentialConstraint referentialConstraint = this.Association.ReferentialConstraint;
				return referentialConstraint != null && referentialConstraint.PrincipalEnd != this.To;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00017410 File Offset: 0x00015610
		public EdmOnDeleteAction OnDelete
		{
			get
			{
				IEdmAssociationEnd from = this.From;
				if (from == null)
				{
					return EdmOnDeleteAction.None;
				}
				return from.OnDelete;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001742F File Offset: 0x0001562F
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00017437 File Offset: 0x00015637
		public IEdmAssociationEnd To
		{
			get
			{
				return this.toCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeToFunc, null);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001744B File Offset: 0x0001564B
		public bool ContainsTarget
		{
			get
			{
				return this.navigationProperty.ContainsTarget;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00017458 File Offset: 0x00015658
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0001746C File Offset: 0x0001566C
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Navigation;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0001746F File Offset: 0x0001566F
		public IEdmNavigationProperty Partner
		{
			get
			{
				return this.partnerCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputePartnerFunc, null);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00017484 File Offset: 0x00015684
		public IEnumerable<EdmError> Errors
		{
			get
			{
				List<EdmError> list = new List<EdmError>();
				if (this.Association is UnresolvedAssociation)
				{
					list.AddRange(this.Association.Errors());
				}
				if (this.From is CsdlSemanticsNavigationProperty.BadCsdlSemanticsNavigationPropertyToEnd)
				{
					list.AddRange(this.From.Errors());
				}
				if (this.To is CsdlSemanticsNavigationProperty.BadCsdlSemanticsNavigationPropertyToEnd)
				{
					list.AddRange(this.To.Errors());
				}
				return list;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x000174F2 File Offset: 0x000156F2
		public IEdmAssociation Association
		{
			get
			{
				return this.associationCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeAssociationFunc, null);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00017508 File Offset: 0x00015708
		public IEnumerable<IEdmStructuralProperty> DependentProperties
		{
			get
			{
				CsdlSemanticsReferentialConstraint referentialConstraint = this.Association.ReferentialConstraint;
				if (referentialConstraint == null || referentialConstraint.PrincipalEnd != this.To)
				{
					return null;
				}
				return referentialConstraint.DependentProperties;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0001753A File Offset: 0x0001573A
		private IEdmAssociationEnd From
		{
			get
			{
				return this.fromCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeFromFunc, null);
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001754E File Offset: 0x0001574E
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringType.Context);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00017568 File Offset: 0x00015768
		private IEdmAssociation ComputeAssociation()
		{
			IEdmAssociation edmAssociation = this.declaringType.Context.FindAssociation(this.navigationProperty.Relationship) ?? new UnresolvedAssociation(this.navigationProperty.Relationship, base.Location);
			this.Model.SetAssociationNamespace(this, edmAssociation.Namespace);
			this.Model.SetAssociationName(this, edmAssociation.Name);
			CsdlSemanticsAssociation csdlSemanticsAssociation = edmAssociation as CsdlSemanticsAssociation;
			CsdlSemanticsAssociationEnd csdlSemanticsAssociationEnd = edmAssociation.End1 as CsdlSemanticsAssociationEnd;
			CsdlSemanticsAssociationEnd csdlSemanticsAssociationEnd2 = edmAssociation.End2 as CsdlSemanticsAssociationEnd;
			if (csdlSemanticsAssociation != null && csdlSemanticsAssociationEnd != null && csdlSemanticsAssociationEnd2 != null)
			{
				this.Model.SetAssociationAnnotations(this, csdlSemanticsAssociation.DirectValueAnnotations, ((this.navigationProperty.FromRole == csdlSemanticsAssociationEnd.Name) ? csdlSemanticsAssociationEnd : csdlSemanticsAssociationEnd2).DirectValueAnnotations, ((this.navigationProperty.FromRole == csdlSemanticsAssociationEnd.Name) ? csdlSemanticsAssociationEnd2 : csdlSemanticsAssociationEnd).DirectValueAnnotations, (edmAssociation.ReferentialConstraint != null) ? edmAssociation.ReferentialConstraint.DirectValueAnnotations : Enumerable.Empty<IEdmDirectValueAnnotation>());
			}
			return edmAssociation;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00017668 File Offset: 0x00015868
		private IEdmAssociationEnd ComputeFrom()
		{
			IEdmAssociation association = this.Association;
			string fromRole = this.navigationProperty.FromRole;
			if (association.End1.Name == fromRole)
			{
				return association.End1;
			}
			if (association.End2.Name == fromRole)
			{
				return association.End2;
			}
			return new CsdlSemanticsNavigationProperty.BadCsdlSemanticsNavigationPropertyToEnd(this.Association, fromRole, new EdmError[]
			{
				new EdmError(base.Location, EdmErrorCode.BadNavigationProperty, Strings.EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole(this.Name, fromRole, association.Name))
			});
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000176F4 File Offset: 0x000158F4
		private IEdmAssociationEnd ComputeTo()
		{
			string toRole = this.navigationProperty.ToRole;
			string fromRole = this.navigationProperty.FromRole;
			this.Model.SetAssociationEndName(this, fromRole);
			IEdmAssociation association = this.Association;
			if (toRole == fromRole)
			{
				return new CsdlSemanticsNavigationProperty.BadCsdlSemanticsNavigationPropertyToEnd(association, toRole, new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.BadNavigationProperty, Strings.EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame(this.Name))
				});
			}
			if (association.End1.Name == toRole)
			{
				return association.End1;
			}
			if (association.End2.Name == toRole)
			{
				return association.End2;
			}
			return new CsdlSemanticsNavigationProperty.BadCsdlSemanticsNavigationPropertyToEnd(this.Association, toRole, new EdmError[]
			{
				new EdmError(base.Location, EdmErrorCode.BadNavigationProperty, Strings.EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole(this.Name, toRole, association.Name))
			});
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000177D0 File Offset: 0x000159D0
		private IEdmNavigationProperty ComputePartner()
		{
			int num = 0;
			foreach (IEdmNavigationProperty edmNavigationProperty in this.declaringType.NavigationProperties())
			{
				if (edmNavigationProperty == this)
				{
					break;
				}
				CsdlSemanticsNavigationProperty csdlSemanticsNavigationProperty = edmNavigationProperty as CsdlSemanticsNavigationProperty;
				if (csdlSemanticsNavigationProperty != null && csdlSemanticsNavigationProperty.Association == this.Association && csdlSemanticsNavigationProperty.To == this.To)
				{
					num++;
				}
			}
			foreach (IEdmNavigationProperty edmNavigationProperty2 in this.To.EntityType.NavigationProperties())
			{
				CsdlSemanticsNavigationProperty csdlSemanticsNavigationProperty2 = edmNavigationProperty2 as CsdlSemanticsNavigationProperty;
				if (csdlSemanticsNavigationProperty2 != null)
				{
					if (csdlSemanticsNavigationProperty2.Association == this.Association && csdlSemanticsNavigationProperty2.To == this.From)
					{
						if (num == 0)
						{
							return edmNavigationProperty2;
						}
						num--;
					}
				}
				else if (edmNavigationProperty2.Partner == this)
				{
					return edmNavigationProperty2;
				}
			}
			return new CsdlSemanticsNavigationProperty.SilentPartner(this);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000178E4 File Offset: 0x00015AE4
		private IEdmTypeReference ComputeType()
		{
			switch (this.To.Multiplicity)
			{
			case EdmMultiplicity.ZeroOrOne:
				return new EdmEntityTypeReference(this.To.EntityType, true);
			case EdmMultiplicity.One:
				return new EdmEntityTypeReference(this.To.EntityType, false);
			case EdmMultiplicity.Many:
				return new EdmCollectionTypeReference(new EdmCollectionType(new EdmEntityTypeReference(this.To.EntityType, false)), false);
			default:
				return new BadEntityTypeReference(this.To.EntityType.FullName(), false, new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.NavigationPropertyTypeInvalidBecauseOfBadAssociation, Strings.EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType(this.To.EntityType.Name))
				});
			}
		}

		// Token: 0x04000417 RID: 1047
		private readonly CsdlNavigationProperty navigationProperty;

		// Token: 0x04000418 RID: 1048
		private readonly CsdlSemanticsEntityTypeDefinition declaringType;

		// Token: 0x04000419 RID: 1049
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsNavigationProperty, IEdmTypeReference>();

		// Token: 0x0400041A RID: 1050
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeType();

		// Token: 0x0400041B RID: 1051
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmAssociation> associationCache = new Cache<CsdlSemanticsNavigationProperty, IEdmAssociation>();

		// Token: 0x0400041C RID: 1052
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmAssociation> ComputeAssociationFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeAssociation();

		// Token: 0x0400041D RID: 1053
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmAssociationEnd> toCache = new Cache<CsdlSemanticsNavigationProperty, IEdmAssociationEnd>();

		// Token: 0x0400041E RID: 1054
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmAssociationEnd> ComputeToFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeTo();

		// Token: 0x0400041F RID: 1055
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmAssociationEnd> fromCache = new Cache<CsdlSemanticsNavigationProperty, IEdmAssociationEnd>();

		// Token: 0x04000420 RID: 1056
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmAssociationEnd> ComputeFromFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeFrom();

		// Token: 0x04000421 RID: 1057
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmNavigationProperty> partnerCache = new Cache<CsdlSemanticsNavigationProperty, IEdmNavigationProperty>();

		// Token: 0x04000422 RID: 1058
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmNavigationProperty> ComputePartnerFunc = (CsdlSemanticsNavigationProperty me) => me.ComputePartner();

		// Token: 0x02000179 RID: 377
		private class BadCsdlSemanticsNavigationPropertyToEnd : BadAssociationEnd
		{
			// Token: 0x06000859 RID: 2137 RVA: 0x00017B24 File Offset: 0x00015D24
			public BadCsdlSemanticsNavigationPropertyToEnd(IEdmAssociation declaringAssociation, string role, IEnumerable<EdmError> errors)
				: base(declaringAssociation, role, errors)
			{
			}
		}

		// Token: 0x0200017A RID: 378
		private class SilentPartner : EdmElement, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
		{
			// Token: 0x0600085A RID: 2138 RVA: 0x00017B30 File Offset: 0x00015D30
			public SilentPartner(CsdlSemanticsNavigationProperty partner)
			{
				this.partner = partner;
				partner.Model.SetAssociationNamespace(this, partner.Association.Namespace);
				partner.Model.SetAssociationName(this, partner.Association.Name);
				partner.Model.SetAssociationEndName(this, partner.To.Name);
			}

			// Token: 0x1700036C RID: 876
			// (get) Token: 0x0600085B RID: 2139 RVA: 0x00017B9A File Offset: 0x00015D9A
			public IEdmNavigationProperty Partner
			{
				get
				{
					return this.partner;
				}
			}

			// Token: 0x1700036D RID: 877
			// (get) Token: 0x0600085C RID: 2140 RVA: 0x00017BA2 File Offset: 0x00015DA2
			public EdmOnDeleteAction OnDelete
			{
				get
				{
					return this.partner.To.OnDelete;
				}
			}

			// Token: 0x1700036E RID: 878
			// (get) Token: 0x0600085D RID: 2141 RVA: 0x00017BB4 File Offset: 0x00015DB4
			public bool IsPrincipal
			{
				get
				{
					CsdlSemanticsReferentialConstraint referentialConstraint = this.partner.Association.ReferentialConstraint;
					return referentialConstraint != null && referentialConstraint.PrincipalEnd == this.partner.To;
				}
			}

			// Token: 0x1700036F RID: 879
			// (get) Token: 0x0600085E RID: 2142 RVA: 0x00017BEA File Offset: 0x00015DEA
			public bool ContainsTarget
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000370 RID: 880
			// (get) Token: 0x0600085F RID: 2143 RVA: 0x00017BF0 File Offset: 0x00015DF0
			public IEnumerable<IEdmStructuralProperty> DependentProperties
			{
				get
				{
					CsdlSemanticsReferentialConstraint referentialConstraint = this.partner.Association.ReferentialConstraint;
					if (referentialConstraint == null || this.IsPrincipal)
					{
						return null;
					}
					return referentialConstraint.DependentProperties;
				}
			}

			// Token: 0x17000371 RID: 881
			// (get) Token: 0x06000860 RID: 2144 RVA: 0x00017C21 File Offset: 0x00015E21
			public EdmPropertyKind PropertyKind
			{
				get
				{
					return EdmPropertyKind.Navigation;
				}
			}

			// Token: 0x17000372 RID: 882
			// (get) Token: 0x06000861 RID: 2145 RVA: 0x00017C24 File Offset: 0x00015E24
			public IEdmTypeReference Type
			{
				get
				{
					return this.typeCache.GetValue(this, CsdlSemanticsNavigationProperty.SilentPartner.ComputeTypeFunc, null);
				}
			}

			// Token: 0x17000373 RID: 883
			// (get) Token: 0x06000862 RID: 2146 RVA: 0x00017C38 File Offset: 0x00015E38
			public IEdmStructuredType DeclaringType
			{
				get
				{
					return this.partner.ToEntityType();
				}
			}

			// Token: 0x17000374 RID: 884
			// (get) Token: 0x06000863 RID: 2147 RVA: 0x00017C45 File Offset: 0x00015E45
			public string Name
			{
				get
				{
					return this.partner.From.Name;
				}
			}

			// Token: 0x06000864 RID: 2148 RVA: 0x00017C58 File Offset: 0x00015E58
			private IEdmTypeReference ComputeType()
			{
				switch (this.partner.From.Multiplicity)
				{
				case EdmMultiplicity.ZeroOrOne:
					return new EdmEntityTypeReference(this.partner.DeclaringEntityType(), true);
				case EdmMultiplicity.One:
					return new EdmEntityTypeReference(this.partner.DeclaringEntityType(), false);
				case EdmMultiplicity.Many:
					return new EdmCollectionTypeReference(new EdmCollectionType(new EdmEntityTypeReference(this.partner.DeclaringEntityType(), false)), false);
				default:
					return new BadEntityTypeReference(this.partner.DeclaringEntityType().FullName(), false, new EdmError[]
					{
						new EdmError(this.partner.To.Location(), EdmErrorCode.NavigationPropertyTypeInvalidBecauseOfBadAssociation, Strings.EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType(this.partner.DeclaringEntityType().Name))
					});
				}
			}

			// Token: 0x0400042D RID: 1069
			private readonly CsdlSemanticsNavigationProperty partner;

			// Token: 0x0400042E RID: 1070
			private readonly Cache<CsdlSemanticsNavigationProperty.SilentPartner, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsNavigationProperty.SilentPartner, IEdmTypeReference>();

			// Token: 0x0400042F RID: 1071
			private static readonly Func<CsdlSemanticsNavigationProperty.SilentPartner, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsNavigationProperty.SilentPartner me) => me.ComputeType();
		}
	}
}

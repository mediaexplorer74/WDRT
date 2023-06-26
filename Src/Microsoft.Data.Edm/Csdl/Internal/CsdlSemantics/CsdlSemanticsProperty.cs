using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Internal;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x020001AA RID: 426
	internal class CsdlSemanticsProperty : CsdlSemanticsElement, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x0001895B File Offset: 0x00016B5B
		public CsdlSemanticsProperty(CsdlSemanticsStructuredTypeDefinition declaringType, CsdlProperty property)
			: base(property)
		{
			this.property = property;
			this.declaringType = declaringType;
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0001897D File Offset: 0x00016B7D
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0001898A File Offset: 0x00016B8A
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00018992 File Offset: 0x00016B92
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x000189A6 File Offset: 0x00016BA6
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringType.Model;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x000189B3 File Offset: 0x00016BB3
		public string DefaultValueString
		{
			get
			{
				return this.property.DefaultValue;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x000189C0 File Offset: 0x00016BC0
		public EdmConcurrencyMode ConcurrencyMode
		{
			get
			{
				if (!this.property.IsFixedConcurrency)
				{
					return EdmConcurrencyMode.None;
				}
				return EdmConcurrencyMode.Fixed;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x000189D2 File Offset: 0x00016BD2
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Structural;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x000189D5 File Offset: 0x00016BD5
		public override CsdlElement Element
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000189DD File Offset: 0x00016BDD
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringType.Context);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000189F6 File Offset: 0x00016BF6
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.declaringType.Context, this.property.Type);
		}

		// Token: 0x04000478 RID: 1144
		protected CsdlProperty property;

		// Token: 0x04000479 RID: 1145
		private readonly CsdlSemanticsStructuredTypeDefinition declaringType;

		// Token: 0x0400047A RID: 1146
		private readonly Cache<CsdlSemanticsProperty, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsProperty, IEdmTypeReference>();

		// Token: 0x0400047B RID: 1147
		private static readonly Func<CsdlSemanticsProperty, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsProperty me) => me.ComputeType();
	}
}

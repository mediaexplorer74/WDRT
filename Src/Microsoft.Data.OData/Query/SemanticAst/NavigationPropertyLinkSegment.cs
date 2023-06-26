using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000049 RID: 73
	public sealed class NavigationPropertyLinkSegment : ODataPathSegment
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x0000796C File Offset: 0x00005B6C
		public NavigationPropertyLinkSegment(IEdmNavigationProperty navigationProperty, IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			this.navigationProperty = navigationProperty;
			base.TargetEdmEntitySet = entitySet;
			base.Identifier = navigationProperty.Name;
			base.TargetEdmType = navigationProperty.Type.Definition;
			base.SingleResult = !navigationProperty.Type.IsCollection();
			base.TargetKind = RequestTargetKind.Resource;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000079D0 File Offset: 0x00005BD0
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000079D8 File Offset: 0x00005BD8
		public IEdmEntitySet EntitySet
		{
			get
			{
				return base.TargetEdmEntitySet;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000079E0 File Offset: 0x00005BE0
		public override IEdmType EdmType
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000079F2 File Offset: 0x00005BF2
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007A06 File Offset: 0x00005C06
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007A1C File Offset: 0x00005C1C
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			NavigationPropertyLinkSegment navigationPropertyLinkSegment = other as NavigationPropertyLinkSegment;
			return navigationPropertyLinkSegment != null && navigationPropertyLinkSegment.NavigationProperty == this.navigationProperty;
		}

		// Token: 0x04000081 RID: 129
		private readonly IEdmNavigationProperty navigationProperty;
	}
}

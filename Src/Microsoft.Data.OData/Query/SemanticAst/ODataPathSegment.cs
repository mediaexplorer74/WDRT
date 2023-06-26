using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000048 RID: 72
	public abstract class ODataPathSegment : ODataAnnotatable
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x000078B7 File Offset: 0x00005AB7
		internal ODataPathSegment(ODataPathSegment other)
		{
			this.CopyValuesFrom(other);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000078C6 File Offset: 0x00005AC6
		protected ODataPathSegment()
		{
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001D6 RID: 470
		public abstract IEdmType EdmType { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000078CE File Offset: 0x00005ACE
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000078D6 File Offset: 0x00005AD6
		internal string Identifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000078DF File Offset: 0x00005ADF
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000078E7 File Offset: 0x00005AE7
		internal bool SingleResult
		{
			get
			{
				return this.singleResult;
			}
			set
			{
				this.singleResult = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000078F0 File Offset: 0x00005AF0
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000078F8 File Offset: 0x00005AF8
		internal IEdmEntitySet TargetEdmEntitySet
		{
			get
			{
				return this.targetEdmEntitySet;
			}
			set
			{
				this.targetEdmEntitySet = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007901 File Offset: 0x00005B01
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00007909 File Offset: 0x00005B09
		internal IEdmType TargetEdmType
		{
			get
			{
				return this.targetEdmType;
			}
			set
			{
				this.targetEdmType = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00007912 File Offset: 0x00005B12
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000791A File Offset: 0x00005B1A
		internal RequestTargetKind TargetKind
		{
			get
			{
				return this.targetKind;
			}
			set
			{
				this.targetKind = value;
			}
		}

		// Token: 0x060001E1 RID: 481
		public abstract T Translate<T>(PathSegmentTranslator<T> translator);

		// Token: 0x060001E2 RID: 482
		public abstract void Handle(PathSegmentHandler handler);

		// Token: 0x060001E3 RID: 483 RVA: 0x00007923 File Offset: 0x00005B23
		internal virtual bool Equals(ODataPathSegment other)
		{
			return object.ReferenceEquals(this, other);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000792C File Offset: 0x00005B2C
		internal void CopyValuesFrom(ODataPathSegment other)
		{
			this.Identifier = other.Identifier;
			this.SingleResult = other.SingleResult;
			this.TargetEdmEntitySet = other.TargetEdmEntitySet;
			this.TargetKind = other.TargetKind;
			this.TargetEdmType = other.TargetEdmType;
		}

		// Token: 0x0400007C RID: 124
		private string identifier;

		// Token: 0x0400007D RID: 125
		private bool singleResult;

		// Token: 0x0400007E RID: 126
		private IEdmEntitySet targetEdmEntitySet;

		// Token: 0x0400007F RID: 127
		private IEdmType targetEdmType;

		// Token: 0x04000080 RID: 128
		private RequestTargetKind targetKind;
	}
}

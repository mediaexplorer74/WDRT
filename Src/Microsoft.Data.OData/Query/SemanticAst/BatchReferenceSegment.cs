using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000064 RID: 100
	public sealed class BatchReferenceSegment : ODataPathSegment
	{
		// Token: 0x06000273 RID: 627 RVA: 0x00009FFC File Offset: 0x000081FC
		public BatchReferenceSegment(string contentId, IEdmType edmType, IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(edmType, "resultingType");
			ExceptionUtils.CheckArgumentNotNull<string>(contentId, "contentId");
			if (!ODataPathParser.ContentIdRegex.IsMatch(contentId))
			{
				throw new ODataException(Strings.BatchReferenceSegment_InvalidContentID(contentId));
			}
			this.edmType = edmType;
			this.entitySet = entitySet;
			this.contentId = contentId;
			base.Identifier = this.ContentId;
			base.TargetEdmType = edmType;
			base.TargetEdmEntitySet = this.EntitySet;
			base.SingleResult = true;
			base.TargetKind = RequestTargetKind.Resource;
			if (entitySet != null)
			{
				UriParserErrorHelper.ThrowIfTypesUnrelated(edmType, entitySet.ElementType, "BatchReferenceSegments");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000A094 File Offset: 0x00008294
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A09C File Offset: 0x0000829C
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000A0A4 File Offset: 0x000082A4
		public string ContentId
		{
			get
			{
				return this.contentId;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A0AC File Offset: 0x000082AC
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A0C0 File Offset: 0x000082C0
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A0D4 File Offset: 0x000082D4
		internal override bool Equals(ODataPathSegment other)
		{
			BatchReferenceSegment batchReferenceSegment = other as BatchReferenceSegment;
			return batchReferenceSegment != null && batchReferenceSegment.EdmType == this.edmType && batchReferenceSegment.EntitySet == this.entitySet && batchReferenceSegment.ContentId == this.contentId;
		}

		// Token: 0x040000A5 RID: 165
		private readonly IEdmType edmType;

		// Token: 0x040000A6 RID: 166
		private readonly IEdmEntitySet entitySet;

		// Token: 0x040000A7 RID: 167
		private readonly string contentId;
	}
}

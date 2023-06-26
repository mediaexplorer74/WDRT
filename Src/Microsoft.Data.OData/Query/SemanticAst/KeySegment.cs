using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200006D RID: 109
	public sealed class KeySegment : ODataPathSegment
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000A421 File Offset: 0x00008621
		public KeySegment(IEnumerable<KeyValuePair<string, object>> keys, IEdmEntityType edmType, IEdmEntitySet entitySet)
		{
			this.keys = new ReadOnlyCollection<KeyValuePair<string, object>>(keys.ToList<KeyValuePair<string, object>>());
			this.edmType = edmType;
			this.entitySet = entitySet;
			if (entitySet != null)
			{
				UriParserErrorHelper.ThrowIfTypesUnrelated(edmType, entitySet.ElementType, "KeySegments");
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000A45C File Offset: 0x0000865C
		public IEnumerable<KeyValuePair<string, object>> Keys
		{
			get
			{
				return this.keys.AsEnumerable<KeyValuePair<string, object>>();
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000A469 File Offset: 0x00008669
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000A471 File Offset: 0x00008671
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A479 File Offset: 0x00008679
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A48D File Offset: 0x0000868D
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A4A4 File Offset: 0x000086A4
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			KeySegment keySegment = other as KeySegment;
			return keySegment != null && keySegment.Keys.SequenceEqual(this.Keys) && keySegment.EdmType == this.edmType && keySegment.EntitySet == this.entitySet;
		}

		// Token: 0x040000B6 RID: 182
		private readonly ReadOnlyCollection<KeyValuePair<string, object>> keys;

		// Token: 0x040000B7 RID: 183
		private readonly IEdmEntityType edmType;

		// Token: 0x040000B8 RID: 184
		private readonly IEdmEntitySet entitySet;
	}
}

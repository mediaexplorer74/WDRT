using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200001A RID: 26
	internal sealed class ODataUriSemanticBinder
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000038D0 File Offset: 0x00001AD0
		public ODataUriSemanticBinder(BindingState bindingState, MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(bindingState, "bindingState");
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindingState = bindingState;
			this.bindMethod = bindMethod;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000038FC File Offset: 0x00001AFC
		public ODataUri BindTree(SyntacticTree syntax)
		{
			ExceptionUtils.CheckArgumentNotNull<SyntacticTree>(syntax, "syntax");
			ExceptionUtils.CheckArgumentNotNull<ICollection<string>>(syntax.Path, "syntax.Path");
			this.bindingState.QueryOptions = new List<CustomQueryOptionToken>(syntax.QueryOptions);
			long? num = null;
			long? num2 = null;
			InlineCountKind? inlineCountKind = null;
			ODataPath odataPath = ODataPathFactory.BindPath(syntax.Path, this.bindingState.Configuration);
			RangeVariable rangeVariable = NodeFactory.CreateImplicitRangeVariable(odataPath);
			if (rangeVariable != null)
			{
				this.bindingState.RangeVariables.Push(rangeVariable);
			}
			if (syntax.Filter != null || syntax.OrderByTokens.Any<OrderByToken>())
			{
				this.bindingState.ImplicitRangeVariable = this.bindingState.RangeVariables.Peek();
			}
			FilterClause filterClause = this.BindFilter(syntax, rangeVariable);
			OrderByClause orderByClause = this.BindOrderBy(syntax, rangeVariable, odataPath);
			num = ODataUriSemanticBinder.BindSkip(syntax, rangeVariable, odataPath);
			num2 = ODataUriSemanticBinder.BindTop(syntax, rangeVariable, odataPath);
			SelectExpandClause selectExpandClause = ODataUriSemanticBinder.BindSelectExpand(syntax, odataPath, this.bindingState.Configuration);
			inlineCountKind = ODataUriSemanticBinder.BindInlineCount(syntax, odataPath);
			List<QueryNode> list = MetadataBinder.ProcessQueryOptions(this.bindingState, this.bindMethod);
			this.bindingState.RangeVariables.Pop();
			this.bindingState.ImplicitRangeVariable = null;
			return new ODataUri(odataPath, list, selectExpandClause, filterClause, orderByClause, num, num2, inlineCountKind);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003A48 File Offset: 0x00001C48
		public static InlineCountKind? BindInlineCount(SyntacticTree syntax, ODataPath path)
		{
			if (syntax.InlineCount == null)
			{
				return null;
			}
			if (!path.EdmType().IsEntityCollection())
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$inlinecount"));
			}
			return new InlineCountKind?(syntax.InlineCount.Value);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public static SelectExpandClause BindSelectExpand(SyntacticTree syntax, ODataPath path, ODataUriParserConfiguration configuration)
		{
			if (syntax.Select == null && syntax.Expand == null)
			{
				return null;
			}
			if (!path.EdmType().IsEntityCollection() && !path.EdmType().IsEntity())
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$select or $expand"));
			}
			return SelectExpandSemanticBinder.Parse((IEdmEntityType)((IEdmCollectionTypeReference)path.EdmType()).ElementType().Definition, path.EntitySet(), syntax.Expand, syntax.Select, configuration);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003B1C File Offset: 0x00001D1C
		public static long? BindTop(SyntacticTree syntax, RangeVariable rangeVariable, ODataPath path)
		{
			if (syntax.Top == null)
			{
				return null;
			}
			if (rangeVariable == null || !path.EdmType().IsEntityCollection())
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$top"));
			}
			int? top = syntax.Top;
			return MetadataBinder.ProcessTop((top != null) ? new long?((long)top.GetValueOrDefault()) : null);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003B90 File Offset: 0x00001D90
		public static long? BindSkip(SyntacticTree syntax, RangeVariable rangeVariable, ODataPath path)
		{
			if (syntax.Skip == null)
			{
				return null;
			}
			if (rangeVariable == null || !path.EdmType().IsEntityCollection())
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$skip"));
			}
			int? skip = syntax.Skip;
			return MetadataBinder.ProcessSkip((skip != null) ? new long?((long)skip.GetValueOrDefault()) : null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003C04 File Offset: 0x00001E04
		public OrderByClause BindOrderBy(SyntacticTree syntax, RangeVariable rangeVariable, ODataPath path)
		{
			if (syntax.OrderByTokens == null || !syntax.OrderByTokens.Any<OrderByToken>())
			{
				return null;
			}
			if (rangeVariable == null || !path.EdmType().IsEntityCollection())
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$orderby"));
			}
			OrderByBinder orderByBinder = new OrderByBinder(this.bindMethod);
			return orderByBinder.BindOrderBy(this.bindingState, syntax.OrderByTokens);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003C68 File Offset: 0x00001E68
		public FilterClause BindFilter(SyntacticTree syntax, RangeVariable rangeVariable)
		{
			if (syntax.Filter == null)
			{
				return null;
			}
			if (rangeVariable == null)
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$filter"));
			}
			FilterBinder filterBinder = new FilterBinder(this.bindMethod, this.bindingState);
			return filterBinder.BindFilter(syntax.Filter);
		}

		// Token: 0x04000045 RID: 69
		private readonly BindingState bindingState;

		// Token: 0x04000046 RID: 70
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}

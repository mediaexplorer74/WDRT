using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200004E RID: 78
	public sealed class ODataUriParser
	{
		// Token: 0x06000200 RID: 512 RVA: 0x00007D9C File Offset: 0x00005F9C
		public ODataUriParser(IEdmModel model, Uri serviceRoot)
		{
			this.configuration = new ODataUriParserConfiguration(model, serviceRoot);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00007DB1 File Offset: 0x00005FB1
		public ODataUriParserSettings Settings
		{
			get
			{
				return this.configuration.Settings;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00007DBE File Offset: 0x00005FBE
		public IEdmModel Model
		{
			get
			{
				return this.configuration.Model;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00007DCB File Offset: 0x00005FCB
		public Uri ServiceRoot
		{
			get
			{
				return this.configuration.ServiceRoot;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00007DD8 File Offset: 0x00005FD8
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00007DE5 File Offset: 0x00005FE5
		public ODataUrlConventions UrlConventions
		{
			get
			{
				return this.configuration.UrlConventions;
			}
			set
			{
				this.configuration.UrlConventions = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00007DF3 File Offset: 0x00005FF3
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00007E00 File Offset: 0x00006000
		public Func<string, BatchReferenceSegment> BatchReferenceCallback
		{
			get
			{
				return this.configuration.BatchReferenceCallback;
			}
			set
			{
				this.configuration.BatchReferenceCallback = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00007E0E File Offset: 0x0000600E
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00007E1B File Offset: 0x0000601B
		public Func<string, string> FunctionParameterAliasCallback
		{
			get
			{
				return this.configuration.FunctionParameterAliasCallback;
			}
			set
			{
				this.configuration.FunctionParameterAliasCallback = value;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007E2C File Offset: 0x0000602C
		public static FilterClause ParseFilter(string filter, IEdmModel model, IEdmType elementType)
		{
			ODataUriParser odataUriParser = new ODataUriParser(model, null);
			return odataUriParser.ParseFilter(filter, elementType, null);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007E4C File Offset: 0x0000604C
		public static FilterClause ParseFilter(string filter, IEdmModel model, IEdmType elementType, IEdmEntitySet entitySet)
		{
			ODataUriParser odataUriParser = new ODataUriParser(model, null);
			return odataUriParser.ParseFilter(filter, elementType, entitySet);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007E6C File Offset: 0x0000606C
		public static OrderByClause ParseOrderBy(string orderBy, IEdmModel model, IEdmType elementType)
		{
			ODataUriParser odataUriParser = new ODataUriParser(model, null);
			return odataUriParser.ParseOrderBy(orderBy, elementType, null);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007E8C File Offset: 0x0000608C
		public static OrderByClause ParseOrderBy(string orderBy, IEdmModel model, IEdmType elementType, IEdmEntitySet entitySet)
		{
			ODataUriParser odataUriParser = new ODataUriParser(model, null);
			return odataUriParser.ParseOrderBy(orderBy, elementType, entitySet);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007EAA File Offset: 0x000060AA
		public FilterClause ParseFilter(string filter, IEdmType elementType, IEdmEntitySet entitySet)
		{
			return this.ParseFilterImplementation(filter, elementType, entitySet);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007EB5 File Offset: 0x000060B5
		public OrderByClause ParseOrderBy(string orderBy, IEdmType elementType, IEdmEntitySet entitySet)
		{
			return this.ParseOrderByImplementation(orderBy, elementType, entitySet);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007EC0 File Offset: 0x000060C0
		public ODataPath ParsePath(Uri pathUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(pathUri, "pathUri");
			if (this.configuration.ServiceRoot == null)
			{
				throw new ODataException(Strings.UriParser_NeedServiceRootForThisOverload);
			}
			if (!pathUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.UriParser_UriMustBeAbsolute(pathUri));
			}
			UriPathParser uriPathParser = new UriPathParser(this.Settings.PathLimit);
			ICollection<string> collection = uriPathParser.ParsePathIntoSegments(pathUri, this.configuration.ServiceRoot);
			return ODataPathFactory.BindPath(collection, this.configuration);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007F3A File Offset: 0x0000613A
		public SelectExpandClause ParseSelectAndExpand(string select, string expand, IEdmEntityType elementType, IEdmEntitySet entitySet)
		{
			return this.ParseSelectAndExpandImplementation(select, expand, elementType, entitySet);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007F47 File Offset: 0x00006147
		internal ODataUri ParseUri(Uri fullUri)
		{
			return this.ParseUriImplementation(fullUri);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007F50 File Offset: 0x00006150
		internal InlineCountKind ParseInlineCount(string inlineCount)
		{
			return this.ParseInlineCountImplementation(inlineCount);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007F5C File Offset: 0x0000615C
		private ODataUri ParseUriImplementation(Uri fullUri)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(this.configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<Uri>(this.configuration.ServiceRoot, "serviceRoot");
			ExceptionUtils.CheckArgumentNotNull<Uri>(fullUri, "fullUri");
			SyntacticTree syntacticTree = SyntacticTree.ParseUri(fullUri, this.configuration.ServiceRoot, this.Settings.FilterLimit);
			ExceptionUtils.CheckArgumentNotNull<SyntacticTree>(syntacticTree, "syntax");
			BindingState bindingState = new BindingState(this.configuration);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			ODataUriSemanticBinder odataUriSemanticBinder = new ODataUriSemanticBinder(bindingState, new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
			return odataUriSemanticBinder.BindTree(syntacticTree);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007FF4 File Offset: 0x000061F4
		private FilterClause ParseFilterImplementation(string filter, IEdmType elementType, IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(this.configuration, "this.configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(elementType, "elementType");
			ExceptionUtils.CheckArgumentNotNull<string>(filter, "filter");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(this.Settings.FilterLimit);
			QueryToken queryToken = uriQueryExpressionParser.ParseFilter(filter);
			BindingState bindingState = new BindingState(this.configuration);
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(elementType.ToTypeReference(), entitySet);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			FilterBinder filterBinder = new FilterBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), bindingState);
			return filterBinder.BindFilter(queryToken);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008098 File Offset: 0x00006298
		private OrderByClause ParseOrderByImplementation(string orderBy, IEdmType elementType, IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(this.configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(elementType, "elementType");
			ExceptionUtils.CheckArgumentNotNull<string>(orderBy, "orderBy");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(this.Settings.OrderByLimit);
			IEnumerable<OrderByToken> enumerable = uriQueryExpressionParser.ParseOrderBy(orderBy);
			BindingState bindingState = new BindingState(this.configuration);
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(elementType.ToTypeReference(), entitySet);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			OrderByBinder orderByBinder = new OrderByBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
			return orderByBinder.BindOrderBy(bindingState, enumerable);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008140 File Offset: 0x00006340
		private SelectExpandClause ParseSelectAndExpandImplementation(string select, string expand, IEdmEntityType elementType, IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(this.configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(elementType, "elementType");
			ISelectExpandTermParser selectExpandTermParser = SelectExpandTermParserFactory.Create(select, this.Settings);
			SelectToken selectToken = selectExpandTermParser.ParseSelect();
			ISelectExpandTermParser selectExpandTermParser2 = SelectExpandTermParserFactory.Create(expand, this.Settings);
			ExpandToken expandToken = selectExpandTermParser2.ParseExpand();
			return SelectExpandSemanticBinder.Parse(elementType, entitySet, expandToken, selectToken, this.configuration);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000081A8 File Offset: 0x000063A8
		private InlineCountKind ParseInlineCountImplementation(string inlineCount)
		{
			inlineCount = inlineCount.Trim();
			string text;
			if ((text = inlineCount) != null)
			{
				if (text == "allpages")
				{
					return InlineCountKind.AllPages;
				}
				if (text == "none")
				{
					return InlineCountKind.None;
				}
			}
			throw new ODataException(Strings.ODataUriParser_InvalidInlineCount(inlineCount));
		}

		// Token: 0x04000084 RID: 132
		private readonly ODataUriParserConfiguration configuration;
	}
}

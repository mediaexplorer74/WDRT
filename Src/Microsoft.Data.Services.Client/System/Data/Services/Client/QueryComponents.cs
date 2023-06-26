using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000BC RID: 188
	internal class QueryComponents
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x00017474 File Offset: 0x00015674
		internal QueryComponents(Uri uri, Version version, Type lastSegmentType, LambdaExpression projection, Dictionary<Expression, Expression> normalizerRewrites)
		{
			this.projection = projection;
			this.normalizerRewrites = normalizerRewrites;
			this.lastSegmentType = lastSegmentType;
			this.Uri = uri;
			this.version = version;
			this.httpMethod = "GET";
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000174AC File Offset: 0x000156AC
		internal QueryComponents(Uri uri, Version version, Type lastSegmentType, LambdaExpression projection, Dictionary<Expression, Expression> normalizerRewrites, string httpMethod, bool? singleResult, List<BodyOperationParameter> bodyOperationParameters, List<UriOperationParameter> uriOperationParameters)
		{
			this.projection = projection;
			this.normalizerRewrites = normalizerRewrites;
			this.lastSegmentType = lastSegmentType;
			this.Uri = uri;
			this.version = version;
			this.httpMethod = httpMethod;
			this.uriOperationParameters = uriOperationParameters;
			this.bodyOperationParameters = bodyOperationParameters;
			this.singleResult = singleResult;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00017504 File Offset: 0x00015704
		internal Dictionary<Expression, Expression> NormalizerRewrites
		{
			get
			{
				return this.normalizerRewrites;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001750C File Offset: 0x0001570C
		internal LambdaExpression Projection
		{
			get
			{
				return this.projection;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00017514 File Offset: 0x00015714
		internal Type LastSegmentType
		{
			get
			{
				return this.lastSegmentType;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001751C File Offset: 0x0001571C
		internal Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00017524 File Offset: 0x00015724
		internal string HttpMethod
		{
			get
			{
				return this.httpMethod;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0001752C File Offset: 0x0001572C
		internal List<UriOperationParameter> UriOperationParameters
		{
			get
			{
				return this.uriOperationParameters;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00017534 File Offset: 0x00015734
		internal List<BodyOperationParameter> BodyOperationParameters
		{
			get
			{
				return this.bodyOperationParameters;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001753C File Offset: 0x0001573C
		internal bool? SingleResult
		{
			get
			{
				return this.singleResult;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00017544 File Offset: 0x00015744
		internal bool HasSelectQueryOption
		{
			get
			{
				return this.Uri != null && QueryComponents.ContainsSelectQueryOption(UriUtil.UriToString(this.Uri));
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00017566 File Offset: 0x00015766
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0001756E File Offset: 0x0001576E
		internal Uri Uri { get; set; }

		// Token: 0x0600061F RID: 1567 RVA: 0x00017577 File Offset: 0x00015777
		private static bool ContainsSelectQueryOption(string queryString)
		{
			return queryString.Contains("?$select=") || queryString.Contains("&$select=");
		}

		// Token: 0x04000337 RID: 823
		private const string SelectQueryOption = "$select=";

		// Token: 0x04000338 RID: 824
		private const string SelectQueryOptionWithQuestionMark = "?$select=";

		// Token: 0x04000339 RID: 825
		private const string SelectQueryOptionWithAmpersand = "&$select=";

		// Token: 0x0400033A RID: 826
		private readonly Type lastSegmentType;

		// Token: 0x0400033B RID: 827
		private readonly Dictionary<Expression, Expression> normalizerRewrites;

		// Token: 0x0400033C RID: 828
		private readonly LambdaExpression projection;

		// Token: 0x0400033D RID: 829
		private readonly string httpMethod;

		// Token: 0x0400033E RID: 830
		private readonly List<UriOperationParameter> uriOperationParameters;

		// Token: 0x0400033F RID: 831
		private readonly List<BodyOperationParameter> bodyOperationParameters;

		// Token: 0x04000340 RID: 832
		private readonly bool? singleResult;

		// Token: 0x04000341 RID: 833
		private Version version;
	}
}

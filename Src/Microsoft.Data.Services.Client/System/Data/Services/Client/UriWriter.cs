using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace System.Data.Services.Client
{
	// Token: 0x020000DF RID: 223
	internal class UriWriter : DataServiceALinqExpressionVisitor
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x0001ECCD File Offset: 0x0001CECD
		private UriWriter(DataServiceContext context)
		{
			this.context = context;
			this.uriBuilder = new StringBuilder();
			this.uriVersion = Util.DataServiceVersion1;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001ED04 File Offset: 0x0001CF04
		internal static void Translate(DataServiceContext context, bool addTrailingParens, Expression e, out Uri uri, out Version version)
		{
			UriWriter uriWriter = new UriWriter(context);
			uriWriter.leafResourceSet = (addTrailingParens ? (e as ResourceSetExpression) : null);
			uriWriter.Visit(e);
			uri = UriUtil.CreateUri(uriWriter.uriBuilder.ToString(), UriKind.Absolute);
			version = uriWriter.uriVersion;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001ED4E File Offset: 0x0001CF4E
		internal override Expression VisitMethodCall(MethodCallExpression m)
		{
			throw Error.MethodNotSupported(m);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001ED56 File Offset: 0x0001CF56
		internal override Expression VisitUnary(UnaryExpression u)
		{
			throw new NotSupportedException(Strings.ALinq_UnaryNotSupported(u.NodeType.ToString()));
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001ED72 File Offset: 0x0001CF72
		internal override Expression VisitBinary(BinaryExpression b)
		{
			throw new NotSupportedException(Strings.ALinq_BinaryNotSupported(b.NodeType.ToString()));
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001ED8E File Offset: 0x0001CF8E
		internal override Expression VisitConstant(ConstantExpression c)
		{
			throw new NotSupportedException(Strings.ALinq_ConstantNotSupported(c.Value));
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001EDA0 File Offset: 0x0001CFA0
		internal override Expression VisitTypeIs(TypeBinaryExpression b)
		{
			throw new NotSupportedException(Strings.ALinq_TypeBinaryNotSupported);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001EDAC File Offset: 0x0001CFAC
		internal override Expression VisitConditional(ConditionalExpression c)
		{
			throw new NotSupportedException(Strings.ALinq_ConditionalNotSupported);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001EDB8 File Offset: 0x0001CFB8
		internal override Expression VisitParameter(ParameterExpression p)
		{
			throw new NotSupportedException(Strings.ALinq_ParameterNotSupported);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001EDC4 File Offset: 0x0001CFC4
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			throw new NotSupportedException(Strings.ALinq_MemberAccessNotSupported(m.Member.Name));
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001EDDB File Offset: 0x0001CFDB
		internal override Expression VisitLambda(LambdaExpression lambda)
		{
			throw new NotSupportedException(Strings.ALinq_LambdaNotSupported);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001EDE7 File Offset: 0x0001CFE7
		internal override NewExpression VisitNew(NewExpression nex)
		{
			throw new NotSupportedException(Strings.ALinq_NewNotSupported);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001EDF3 File Offset: 0x0001CFF3
		internal override Expression VisitMemberInit(MemberInitExpression init)
		{
			throw new NotSupportedException(Strings.ALinq_MemberInitNotSupported);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001EDFF File Offset: 0x0001CFFF
		internal override Expression VisitListInit(ListInitExpression init)
		{
			throw new NotSupportedException(Strings.ALinq_ListInitNotSupported);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001EE0B File Offset: 0x0001D00B
		internal override Expression VisitNewArray(NewArrayExpression na)
		{
			throw new NotSupportedException(Strings.ALinq_NewArrayNotSupported);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001EE17 File Offset: 0x0001D017
		internal override Expression VisitInvocation(InvocationExpression iv)
		{
			throw new NotSupportedException(Strings.ALinq_InvocationNotSupported);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001EE23 File Offset: 0x0001D023
		internal override Expression VisitNavigationPropertySingletonExpression(NavigationPropertySingletonExpression npse)
		{
			this.Visit(npse.Source);
			this.uriBuilder.Append('/').Append(this.ExpressionToString(npse.MemberExpression, true));
			this.VisitQueryOptions(npse);
			return npse;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001EE78 File Offset: 0x0001D078
		internal override Expression VisitResourceSetExpression(ResourceSetExpression rse)
		{
			if (rse.NodeType == (ExpressionType)10001)
			{
				this.Visit(rse.Source);
				this.uriBuilder.Append('/').Append(this.ExpressionToString(rse.MemberExpression, true));
			}
			else
			{
				string text = (string)((ConstantExpression)rse.MemberExpression).Value;
				this.uriBuilder.Append(this.context.BaseUriResolver.GetEntitySetUri(text));
			}
			WebUtil.RaiseVersion(ref this.uriVersion, rse.UriVersion);
			if (rse.ResourceTypeAs != null)
			{
				this.uriBuilder.Append('/');
				UriHelper.AppendTypeSegment(this.uriBuilder, rse.ResourceTypeAs, this.context, true, ref this.uriVersion);
			}
			if (rse.KeyPredicateConjuncts.Count > 0)
			{
				this.context.UrlConventions.AppendKeyExpression<KeyValuePair<PropertyInfo, ConstantExpression>>(rse.GetKeyProperties(), (KeyValuePair<PropertyInfo, ConstantExpression> kvp) => kvp.Key.Name, (KeyValuePair<PropertyInfo, ConstantExpression> kvp) => kvp.Value.Value, this.uriBuilder);
			}
			else if (rse == this.leafResourceSet)
			{
				this.uriBuilder.Append('(');
				this.uriBuilder.Append(')');
			}
			if (rse.CountOption == CountOption.ValueOnly)
			{
				this.uriBuilder.Append('/').Append('$').Append("count");
				WebUtil.RaiseVersion(ref this.uriVersion, Util.DataServiceVersion2);
			}
			this.VisitQueryOptions(rse);
			return rse;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001F00C File Offset: 0x0001D20C
		internal void VisitQueryOptions(ResourceExpression re)
		{
			if (re.HasQueryOptions)
			{
				this.uriBuilder.Append('?');
				ResourceSetExpression resourceSetExpression = re as ResourceSetExpression;
				if (resourceSetExpression != null)
				{
					foreach (object obj in resourceSetExpression.SequenceQueryOptions)
					{
						Expression expression = (Expression)obj;
						switch (expression.NodeType)
						{
						case (ExpressionType)10003:
							this.VisitQueryOptionExpression((TakeQueryOptionExpression)expression);
							break;
						case (ExpressionType)10004:
							this.VisitQueryOptionExpression((SkipQueryOptionExpression)expression);
							break;
						case (ExpressionType)10005:
							this.VisitQueryOptionExpression((OrderByQueryOptionExpression)expression);
							break;
						case (ExpressionType)10006:
							this.VisitQueryOptionExpression((FilterQueryOptionExpression)expression);
							break;
						}
					}
				}
				if (re.ExpandPaths.Count > 0)
				{
					this.VisitExpandOptions(re.ExpandPaths);
				}
				if (re.Projection != null && re.Projection.Paths.Count > 0)
				{
					this.VisitProjectionPaths(re.Projection.Paths);
				}
				if (re.CountOption == CountOption.InlineAll)
				{
					this.VisitCountOptions();
				}
				if (re.CustomQueryOptions.Count > 0)
				{
					this.VisitCustomQueryOptions(re.CustomQueryOptions);
				}
				this.AppendCachedQueryOptionsToUriBuilder();
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001F135 File Offset: 0x0001D335
		internal void VisitQueryOptionExpression(SkipQueryOptionExpression sqoe)
		{
			this.AddAsCachedQueryOption('$' + "skip", this.ExpressionToString(sqoe.SkipAmount, false));
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001F15B File Offset: 0x0001D35B
		internal void VisitQueryOptionExpression(TakeQueryOptionExpression tqoe)
		{
			this.AddAsCachedQueryOption('$' + "top", this.ExpressionToString(tqoe.TakeAmount, false));
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001F181 File Offset: 0x0001D381
		internal void VisitQueryOptionExpression(FilterQueryOptionExpression fqoe)
		{
			this.AddAsCachedQueryOption('$' + "filter", this.ExpressionToString(fqoe.GetPredicate(), false));
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001F1A8 File Offset: 0x0001D3A8
		internal void VisitQueryOptionExpression(OrderByQueryOptionExpression oboe)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (;;)
			{
				OrderByQueryOptionExpression.Selector selector = oboe.Selectors[num];
				stringBuilder.Append(this.ExpressionToString(selector.Expression, false));
				if (selector.Descending)
				{
					stringBuilder.Append(' ');
					stringBuilder.Append("desc");
				}
				if (++num == oboe.Selectors.Count)
				{
					break;
				}
				stringBuilder.Append(',');
			}
			this.AddAsCachedQueryOption('$' + "orderby", stringBuilder.ToString());
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001F238 File Offset: 0x0001D438
		internal void VisitExpandOptions(List<string> paths)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (;;)
			{
				stringBuilder.Append(paths[num]);
				if (++num == paths.Count)
				{
					break;
				}
				stringBuilder.Append(',');
			}
			this.AddAsCachedQueryOption('$' + "expand", stringBuilder.ToString());
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001F290 File Offset: 0x0001D490
		internal void VisitProjectionPaths(List<string> paths)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (;;)
			{
				string text = paths[num];
				stringBuilder.Append(text);
				if (++num == paths.Count)
				{
					break;
				}
				stringBuilder.Append(',');
			}
			this.AddAsCachedQueryOption('$' + "select", stringBuilder.ToString());
			WebUtil.RaiseVersion(ref this.uriVersion, Util.DataServiceVersion2);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001F2FA File Offset: 0x0001D4FA
		internal void VisitCountOptions()
		{
			this.AddAsCachedQueryOption('$' + "inlinecount", "allpages");
			WebUtil.RaiseVersion(ref this.uriVersion, Util.DataServiceVersion2);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001F328 File Offset: 0x0001D528
		internal void VisitCustomQueryOptions(Dictionary<ConstantExpression, ConstantExpression> options)
		{
			List<ConstantExpression> list = options.Keys.ToList<ConstantExpression>();
			List<ConstantExpression> list2 = options.Values.ToList<ConstantExpression>();
			for (int i = 0; i < list.Count; i++)
			{
				string text = string.Concat(list[i].Value);
				string text2 = string.Concat(list2[i].Value);
				this.AddAsCachedQueryOption(text, text2);
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001F38C File Offset: 0x0001D58C
		private void AddAsCachedQueryOption(string optionKey, string optionValue)
		{
			List<string> list = null;
			if (!this.cachedQueryOptions.TryGetValue(optionKey, out list))
			{
				list = new List<string>();
				this.cachedQueryOptions.Add(optionKey, list);
			}
			list.Add(optionValue);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001F3C8 File Offset: 0x0001D5C8
		private void AppendCachedQueryOptionsToUriBuilder()
		{
			int num = 0;
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.cachedQueryOptions)
			{
				if (num++ != 0)
				{
					this.uriBuilder.Append('&');
				}
				string key = keyValuePair.Key;
				string text = string.Join(",", keyValuePair.Value);
				this.uriBuilder.Append(key);
				this.uriBuilder.Append('=');
				this.uriBuilder.Append(text);
				if (key.Equals('$' + "inlinecount", StringComparison.Ordinal) || key.Equals('$' + "select", StringComparison.Ordinal))
				{
					WebUtil.RaiseVersion(ref this.uriVersion, Util.DataServiceVersion2);
				}
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		private string ExpressionToString(Expression expression, bool inPath)
		{
			return ExpressionWriter.ExpressionToString(this.context, expression, inPath, ref this.uriVersion);
		}

		// Token: 0x04000482 RID: 1154
		private readonly DataServiceContext context;

		// Token: 0x04000483 RID: 1155
		private readonly StringBuilder uriBuilder;

		// Token: 0x04000484 RID: 1156
		private Version uriVersion;

		// Token: 0x04000485 RID: 1157
		private ResourceSetExpression leafResourceSet;

		// Token: 0x04000486 RID: 1158
		private Dictionary<string, List<string>> cachedQueryOptions = new Dictionary<string, List<string>>(StringComparer.Ordinal);
	}
}

using System;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000B8 RID: 184
	internal abstract class DataServiceALinqExpressionVisitor : ALinqExpressionVisitor
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x00016D58 File Offset: 0x00014F58
		internal override Expression Visit(Expression exp)
		{
			if (exp == null)
			{
				return null;
			}
			ResourceExpressionType nodeType = (ResourceExpressionType)exp.NodeType;
			switch (nodeType)
			{
			case ResourceExpressionType.RootResourceSet:
			case ResourceExpressionType.ResourceNavigationProperty:
				return this.VisitResourceSetExpression((ResourceSetExpression)exp);
			case ResourceExpressionType.ResourceNavigationPropertySingleton:
				return this.VisitNavigationPropertySingletonExpression((NavigationPropertySingletonExpression)exp);
			default:
				if (nodeType != ResourceExpressionType.InputReference)
				{
					return base.Visit(exp);
				}
				return this.VisitInputReferenceExpression((InputReferenceExpression)exp);
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00016DC4 File Offset: 0x00014FC4
		internal virtual Expression VisitResourceSetExpression(ResourceSetExpression rse)
		{
			Expression expression = this.Visit(rse.Source);
			if (expression != rse.Source)
			{
				rse = new ResourceSetExpression(rse.Type, expression, rse.MemberExpression, rse.ResourceType, rse.ExpandPaths, rse.CountOption, rse.CustomQueryOptions, rse.Projection, rse.ResourceTypeAs, rse.UriVersion);
			}
			return rse;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00016E28 File Offset: 0x00015028
		internal virtual Expression VisitNavigationPropertySingletonExpression(NavigationPropertySingletonExpression npse)
		{
			Expression expression = this.Visit(npse.Source);
			if (expression != npse.Source)
			{
				npse = new NavigationPropertySingletonExpression(npse.Type, expression, npse.MemberExpression, npse.MemberExpression.Type, npse.ExpandPaths, npse.CountOption, npse.CustomQueryOptions, npse.Projection, npse.ResourceTypeAs, npse.UriVersion);
			}
			return npse;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00016E90 File Offset: 0x00015090
		internal virtual Expression VisitInputReferenceExpression(InputReferenceExpression ire)
		{
			ResourceExpression resourceExpression = (ResourceExpression)this.Visit(ire.Target);
			return resourceExpression.CreateReference();
		}
	}
}

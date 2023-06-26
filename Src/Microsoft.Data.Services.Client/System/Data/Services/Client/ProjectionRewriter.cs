using System;
using System.Data.Services.Client.Metadata;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Services.Client
{
	// Token: 0x020000B3 RID: 179
	internal class ProjectionRewriter : ALinqExpressionVisitor
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x00015C94 File Offset: 0x00013E94
		private ProjectionRewriter(Type proposedParameterType)
		{
			this.newLambdaParameter = Expression.Parameter(proposedParameterType, "it");
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00015CC8 File Offset: 0x00013EC8
		internal static LambdaExpression TryToRewrite(LambdaExpression le, ResourceExpression source)
		{
			Type proposedParameterType = source.ResourceType;
			LambdaExpression lambdaExpression;
			if (!ResourceBinder.PatternRules.MatchSingleArgumentLambda(le, out le) || ClientTypeUtil.TypeOrElementTypeIsEntity(le.Parameters[0].Type) || !le.Parameters[0].Type.GetProperties().Any((PropertyInfo p) => p.PropertyType == proposedParameterType))
			{
				lambdaExpression = le;
			}
			else
			{
				ProjectionRewriter projectionRewriter = new ProjectionRewriter(proposedParameterType);
				lambdaExpression = projectionRewriter.Rebind(le, source);
			}
			return lambdaExpression;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00015D4C File Offset: 0x00013F4C
		internal LambdaExpression Rebind(LambdaExpression lambda, ResourceExpression source)
		{
			this.successfulRebind = true;
			this.oldLambdaParameter = lambda.Parameters[0];
			this.projectionSource = source;
			Expression expression = this.Visit(lambda.Body);
			if (this.successfulRebind)
			{
				Type type = typeof(Func<, >).MakeGenericType(new Type[]
				{
					this.newLambdaParameter.Type,
					lambda.Body.Type
				});
				return Expression.Lambda(type, expression, new ParameterExpression[] { this.newLambdaParameter });
			}
			throw new NotSupportedException(Strings.ALinq_CanOnlyProjectTheLeaf);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00015DE8 File Offset: 0x00013FE8
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			if (m.Expression == this.oldLambdaParameter)
			{
				ResourceSetExpression resourceSetExpression = this.projectionSource as ResourceSetExpression;
				if (resourceSetExpression != null && resourceSetExpression.HasTransparentScope && resourceSetExpression.TransparentScope.Accessor == m.Member.Name)
				{
					return this.newLambdaParameter;
				}
				this.successfulRebind = false;
			}
			return base.VisitMemberAccess(m);
		}

		// Token: 0x04000318 RID: 792
		private readonly ParameterExpression newLambdaParameter;

		// Token: 0x04000319 RID: 793
		private ParameterExpression oldLambdaParameter;

		// Token: 0x0400031A RID: 794
		private ResourceExpression projectionSource;

		// Token: 0x0400031B RID: 795
		private bool successfulRebind;
	}
}

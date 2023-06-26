using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Services.Client
{
	// Token: 0x020000B6 RID: 182
	internal static class ProjectionAnalyzer
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x00016420 File Offset: 0x00014620
		internal static bool Analyze(LambdaExpression le, ResourceExpression re, bool matchMembers, DataServiceContext context)
		{
			if (le.Body.NodeType == ExpressionType.Constant)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(le.Body.Type))
				{
					throw new NotSupportedException(Strings.ALinq_CannotCreateConstantEntity);
				}
				re.Projection = new ProjectionQueryOptionExpression(le.Body.Type, le, new List<string>());
				return true;
			}
			else
			{
				if (le.Body.NodeType == ExpressionType.MemberInit || le.Body.NodeType == ExpressionType.New)
				{
					ProjectionAnalyzer.AnalyzeResourceExpression(le, re, context);
					return true;
				}
				if (matchMembers)
				{
					Expression expression = ProjectionAnalyzer.SkipConverts(le.Body);
					if (expression.NodeType == ExpressionType.MemberAccess)
					{
						ProjectionAnalyzer.AnalyzeResourceExpression(le, re, context);
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000164C4 File Offset: 0x000146C4
		private static void Analyze(LambdaExpression e, PathBox pb, DataServiceContext context)
		{
			bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(e.Body.Type);
			ParameterExpression parameterExpression = e.Parameters.Last<ParameterExpression>();
			bool flag2 = ClientTypeUtil.TypeOrElementTypeIsEntity(parameterExpression.Type);
			if (flag2)
			{
				pb.PushParamExpression(parameterExpression);
			}
			if (!flag)
			{
				ProjectionAnalyzer.NonEntityProjectionAnalyzer.Analyze(e.Body, pb, context);
			}
			else
			{
				ExpressionType nodeType = e.Body.NodeType;
				if (nodeType == ExpressionType.Constant)
				{
					throw new NotSupportedException(Strings.ALinq_CannotCreateConstantEntity);
				}
				if (nodeType != ExpressionType.MemberInit)
				{
					if (nodeType == ExpressionType.New)
					{
						throw new NotSupportedException(Strings.ALinq_CannotConstructKnownEntityTypes);
					}
					ProjectionAnalyzer.NonEntityProjectionAnalyzer.Analyze(e.Body, pb, context);
				}
				else
				{
					ProjectionAnalyzer.EntityProjectionAnalyzer.Analyze((MemberInitExpression)e.Body, pb, context);
				}
			}
			if (flag2)
			{
				pb.PopParamExpression();
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00016573 File Offset: 0x00014773
		internal static bool IsMethodCallAllowedEntitySequence(MethodCallExpression call)
		{
			return ReflectionUtil.IsSequenceMethod(call.Method, SequenceMethod.ToList) || ReflectionUtil.IsSequenceMethod(call.Method, SequenceMethod.Select);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00016598 File Offset: 0x00014798
		internal static void CheckChainedSequence(MethodCallExpression call, Type type)
		{
			if (ReflectionUtil.IsSequenceSelectMethod(call.Method))
			{
				MethodCallExpression methodCallExpression = ResourceBinder.StripTo<MethodCallExpression>(call.Arguments[0]);
				if (methodCallExpression != null && ReflectionUtil.IsSequenceSelectMethod(methodCallExpression.Method))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(type, call.ToString()));
				}
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000165E8 File Offset: 0x000147E8
		internal static bool IsCollectionProducingExpression(Expression e)
		{
			if (TypeSystem.FindIEnumerable(e.Type) != null)
			{
				Type elementType = TypeSystem.GetElementType(e.Type);
				Type dataServiceCollectionOfT = WebUtil.GetDataServiceCollectionOfT(new Type[] { elementType });
				if (typeof(List<>).MakeGenericType(new Type[] { elementType }).IsAssignableFrom(e.Type) || (dataServiceCollectionOfT != null && dataServiceCollectionOfT.IsAssignableFrom(e.Type)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00016668 File Offset: 0x00014868
		internal static bool IsDisallowedExpressionForMethodCall(Expression e, ClientEdmModel model)
		{
			MemberExpression memberExpression = e as MemberExpression;
			return (memberExpression == null || !ClientTypeUtil.TypeIsEntity(memberExpression.Expression.Type, model)) && ProjectionAnalyzer.IsCollectionProducingExpression(e);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001669C File Offset: 0x0001489C
		private static void Analyze(MemberInitExpression mie, PathBox pb, DataServiceContext context)
		{
			bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(mie.Type);
			if (flag)
			{
				ProjectionAnalyzer.EntityProjectionAnalyzer.Analyze(mie, pb, context);
				return;
			}
			ProjectionAnalyzer.NonEntityProjectionAnalyzer.Analyze(mie, pb, context);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000166CC File Offset: 0x000148CC
		private static void AnalyzeResourceExpression(LambdaExpression lambda, ResourceExpression resource, DataServiceContext context)
		{
			PathBox pathBox = new PathBox();
			ProjectionAnalyzer.Analyze(lambda, pathBox, context);
			resource.Projection = new ProjectionQueryOptionExpression(lambda.Body.Type, lambda, pathBox.ProjectionPaths.ToList<string>());
			resource.ExpandPaths = pathBox.ExpandPaths.Union(resource.ExpandPaths, StringComparer.Ordinal).ToList<string>();
			resource.RaiseUriVersion(pathBox.UriVersion);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00016738 File Offset: 0x00014938
		private static Expression SkipConverts(Expression expression)
		{
			Expression expression2 = expression;
			while (expression2.NodeType == ExpressionType.Convert || expression2.NodeType == ExpressionType.ConvertChecked)
			{
				expression2 = ((UnaryExpression)expression2).Operand;
			}
			return expression2;
		}

		// Token: 0x020000B7 RID: 183
		private class EntityProjectionAnalyzer : ALinqExpressionVisitor
		{
			// Token: 0x060005E4 RID: 1508 RVA: 0x0001676A File Offset: 0x0001496A
			private EntityProjectionAnalyzer(PathBox pb, Type type, DataServiceContext context)
			{
				this.box = pb;
				this.type = type;
				this.context = context;
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x00016788 File Offset: 0x00014988
			internal static void Analyze(MemberInitExpression mie, PathBox pb, DataServiceContext context)
			{
				ProjectionAnalyzer.EntityProjectionAnalyzer entityProjectionAnalyzer = new ProjectionAnalyzer.EntityProjectionAnalyzer(pb, mie.Type, context);
				MemberAssignmentAnalysis memberAssignmentAnalysis = null;
				foreach (MemberBinding memberBinding in mie.Bindings)
				{
					MemberAssignment memberAssignment = memberBinding as MemberAssignment;
					entityProjectionAnalyzer.Visit(memberAssignment.Expression);
					if (memberAssignment != null)
					{
						MemberAssignmentAnalysis memberAssignmentAnalysis2 = MemberAssignmentAnalysis.Analyze(pb.ParamExpressionInScope, memberAssignment.Expression);
						if (memberAssignmentAnalysis2.IncompatibleAssignmentsException != null)
						{
							throw memberAssignmentAnalysis2.IncompatibleAssignmentsException;
						}
						Type memberType = ClientTypeUtil.GetMemberType(memberAssignment.Member);
						Expression[] expressionsBeyondTargetEntity = memberAssignmentAnalysis2.GetExpressionsBeyondTargetEntity();
						if (expressionsBeyondTargetEntity.Length == 0)
						{
							throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(memberType, memberAssignment.Expression));
						}
						MemberExpression memberExpression = expressionsBeyondTargetEntity[expressionsBeyondTargetEntity.Length - 1] as MemberExpression;
						memberAssignmentAnalysis2.CheckCompatibleAssignments(mie.Type, ref memberAssignmentAnalysis);
						if (memberExpression != null)
						{
							if (memberExpression.Member.Name != memberAssignment.Member.Name)
							{
								throw new NotSupportedException(Strings.ALinq_PropertyNamesMustMatchInProjections(memberExpression.Member.Name, memberAssignment.Member.Name));
							}
							bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(memberType);
							bool flag2 = ClientTypeUtil.TypeOrElementTypeIsEntity(memberExpression.Type);
							if (flag2 && !flag)
							{
								throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(memberType, memberAssignment.Expression));
							}
						}
					}
				}
			}

			// Token: 0x060005E6 RID: 1510 RVA: 0x000168F8 File Offset: 0x00014AF8
			internal override Expression VisitUnary(UnaryExpression u)
			{
				if (ResourceBinder.PatternRules.MatchConvertToAssignable(u) || (u.NodeType == ExpressionType.TypeAs && this.leafExpressionIsMemberAccess))
				{
					return base.VisitUnary(u);
				}
				if (u.NodeType == ExpressionType.Convert || u.NodeType == ExpressionType.ConvertChecked)
				{
					Type type = Nullable.GetUnderlyingType(u.Operand.Type) ?? u.Operand.Type;
					Type type2 = Nullable.GetUnderlyingType(u.Type) ?? u.Type;
					if (PrimitiveType.IsKnownType(type) && PrimitiveType.IsKnownType(type2))
					{
						return base.Visit(u.Operand);
					}
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, u.ToString()));
			}

			// Token: 0x060005E7 RID: 1511 RVA: 0x000169A4 File Offset: 0x00014BA4
			internal override Expression VisitBinary(BinaryExpression b)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, b.ToString()));
			}

			// Token: 0x060005E8 RID: 1512 RVA: 0x000169BC File Offset: 0x00014BBC
			internal override Expression VisitTypeIs(TypeBinaryExpression b)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, b.ToString()));
			}

			// Token: 0x060005E9 RID: 1513 RVA: 0x000169D4 File Offset: 0x00014BD4
			internal override Expression VisitConditional(ConditionalExpression c)
			{
				ResourceBinder.PatternRules.MatchNullCheckResult matchNullCheckResult = ResourceBinder.PatternRules.MatchNullCheck(this.box.ParamExpressionInScope, c);
				if (matchNullCheckResult.Match)
				{
					this.Visit(matchNullCheckResult.AssignExpression);
					return c;
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, c.ToString()));
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x00016A22 File Offset: 0x00014C22
			internal override Expression VisitConstant(ConstantExpression c)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, c.ToString()));
			}

			// Token: 0x060005EB RID: 1515 RVA: 0x00016A3C File Offset: 0x00014C3C
			internal override Expression VisitMemberAccess(MemberExpression m)
			{
				this.leafExpressionIsMemberAccess = true;
				if (!ClientTypeUtil.TypeOrElementTypeIsEntity(m.Expression.Type) || ProjectionAnalyzer.IsCollectionProducingExpression(m.Expression))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, m.ToString()));
				}
				PropertyInfo propertyInfo;
				Expression expression;
				if (ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(m, out propertyInfo, out expression))
				{
					Expression expression2 = base.VisitMemberAccess(m);
					Type type;
					ResourceBinder.StripTo<Expression>(m.Expression, out type);
					this.box.AppendPropertyToPath(propertyInfo, type, this.context);
					this.leafExpressionIsMemberAccess = false;
					return expression2;
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, m.ToString()));
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x00016AF0 File Offset: 0x00014CF0
			internal override Expression VisitMethodCall(MethodCallExpression m)
			{
				if ((m.Object != null && (ProjectionAnalyzer.IsDisallowedExpressionForMethodCall(m.Object, this.context.Model) || !ClientTypeUtil.TypeOrElementTypeIsEntity(m.Object.Type))) || m.Arguments.Any((Expression a) => ProjectionAnalyzer.IsDisallowedExpressionForMethodCall(a, this.context.Model)) || (m.Object == null && !ClientTypeUtil.TypeOrElementTypeIsEntity(m.Arguments[0].Type)))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, m.ToString()));
				}
				if (ProjectionAnalyzer.IsMethodCallAllowedEntitySequence(m))
				{
					ProjectionAnalyzer.CheckChainedSequence(m, this.type);
					return base.VisitMethodCall(m);
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, m.ToString()));
			}

			// Token: 0x060005ED RID: 1517 RVA: 0x00016BB1 File Offset: 0x00014DB1
			internal override Expression VisitInvocation(InvocationExpression iv)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, iv.ToString()));
			}

			// Token: 0x060005EE RID: 1518 RVA: 0x00016BC9 File Offset: 0x00014DC9
			internal override Expression VisitLambda(LambdaExpression lambda)
			{
				ProjectionAnalyzer.Analyze(lambda, this.box, this.context);
				return lambda;
			}

			// Token: 0x060005EF RID: 1519 RVA: 0x00016BDE File Offset: 0x00014DDE
			internal override Expression VisitListInit(ListInitExpression init)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, init.ToString()));
			}

			// Token: 0x060005F0 RID: 1520 RVA: 0x00016BF6 File Offset: 0x00014DF6
			internal override Expression VisitNewArray(NewArrayExpression na)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, na.ToString()));
			}

			// Token: 0x060005F1 RID: 1521 RVA: 0x00016C0E File Offset: 0x00014E0E
			internal override Expression VisitMemberInit(MemberInitExpression init)
			{
				if (!ClientTypeUtil.TypeOrElementTypeIsEntity(init.Type))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, init.ToString()));
				}
				ProjectionAnalyzer.Analyze(init, this.box, this.context);
				return init;
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x00016C48 File Offset: 0x00014E48
			internal override NewExpression VisitNew(NewExpression nex)
			{
				if (ResourceBinder.PatternRules.MatchNewDataServiceCollectionOfT(nex))
				{
					if (ClientTypeUtil.TypeOrElementTypeIsEntity(nex.Type))
					{
						foreach (Expression expression in nex.Arguments)
						{
							if (expression.NodeType != ExpressionType.Constant)
							{
								base.Visit(expression);
							}
						}
						return nex;
					}
				}
				else if (ResourceBinder.PatternRules.MatchNewCollectionOfT(nex) && !ClientTypeUtil.TypeOrElementTypeIsEntity(nex.Type))
				{
					foreach (Expression expression2 in nex.Arguments)
					{
						if (expression2.NodeType != ExpressionType.Constant)
						{
							base.Visit(expression2);
						}
					}
					return nex;
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, nex.ToString()));
			}

			// Token: 0x060005F3 RID: 1523 RVA: 0x00016D30 File Offset: 0x00014F30
			internal override Expression VisitParameter(ParameterExpression p)
			{
				if (p != this.box.ParamExpressionInScope)
				{
					throw new NotSupportedException(Strings.ALinq_CanOnlyProjectTheLeaf);
				}
				this.box.StartNewPath();
				return p;
			}

			// Token: 0x0400032A RID: 810
			private readonly PathBox box;

			// Token: 0x0400032B RID: 811
			private readonly Type type;

			// Token: 0x0400032C RID: 812
			private bool leafExpressionIsMemberAccess;

			// Token: 0x0400032D RID: 813
			private readonly DataServiceContext context;
		}

		// Token: 0x020000B9 RID: 185
		private class NonEntityProjectionAnalyzer : DataServiceALinqExpressionVisitor
		{
			// Token: 0x060005FA RID: 1530 RVA: 0x00016EBD File Offset: 0x000150BD
			private NonEntityProjectionAnalyzer(PathBox pb, Type type, DataServiceContext context)
			{
				this.box = pb;
				this.type = type;
				this.context = context;
			}

			// Token: 0x060005FB RID: 1531 RVA: 0x00016EDC File Offset: 0x000150DC
			internal static void Analyze(Expression e, PathBox pb, DataServiceContext context)
			{
				ProjectionAnalyzer.NonEntityProjectionAnalyzer nonEntityProjectionAnalyzer = new ProjectionAnalyzer.NonEntityProjectionAnalyzer(pb, e.Type, context);
				MemberInitExpression memberInitExpression = e as MemberInitExpression;
				if (memberInitExpression != null)
				{
					using (IEnumerator<MemberBinding> enumerator = memberInitExpression.Bindings.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MemberBinding memberBinding = enumerator.Current;
							MemberAssignment memberAssignment = memberBinding as MemberAssignment;
							if (memberAssignment != null)
							{
								nonEntityProjectionAnalyzer.Visit(memberAssignment.Expression);
							}
						}
						return;
					}
				}
				nonEntityProjectionAnalyzer.Visit(e);
			}

			// Token: 0x060005FC RID: 1532 RVA: 0x00016F60 File Offset: 0x00015160
			internal override Expression VisitUnary(UnaryExpression u)
			{
				if (!ResourceBinder.PatternRules.MatchConvertToAssignable(u))
				{
					if (u.NodeType == ExpressionType.TypeAs && this.leafExpressionIsMemberAccess)
					{
						return base.VisitUnary(u);
					}
					if (ClientTypeUtil.TypeOrElementTypeIsEntity(u.Operand.Type))
					{
						throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, u.ToString()));
					}
				}
				return base.VisitUnary(u);
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x00016FC0 File Offset: 0x000151C0
			internal override Expression VisitBinary(BinaryExpression b)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(b.Left.Type) || ClientTypeUtil.TypeOrElementTypeIsEntity(b.Right.Type) || ProjectionAnalyzer.IsCollectionProducingExpression(b.Left) || ProjectionAnalyzer.IsCollectionProducingExpression(b.Right))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, b.ToString()));
				}
				return base.VisitBinary(b);
			}

			// Token: 0x060005FE RID: 1534 RVA: 0x00017029 File Offset: 0x00015229
			internal override Expression VisitTypeIs(TypeBinaryExpression b)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(b.Expression.Type) || ProjectionAnalyzer.IsCollectionProducingExpression(b.Expression))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, b.ToString()));
				}
				return base.VisitTypeIs(b);
			}

			// Token: 0x060005FF RID: 1535 RVA: 0x00017068 File Offset: 0x00015268
			internal override Expression VisitConditional(ConditionalExpression c)
			{
				ResourceBinder.PatternRules.MatchNullCheckResult matchNullCheckResult = ResourceBinder.PatternRules.MatchNullCheck(this.box.ParamExpressionInScope, c);
				if (matchNullCheckResult.Match)
				{
					this.Visit(matchNullCheckResult.AssignExpression);
					return c;
				}
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(c.Test.Type) || ClientTypeUtil.TypeOrElementTypeIsEntity(c.IfTrue.Type) || ClientTypeUtil.TypeOrElementTypeIsEntity(c.IfFalse.Type) || ProjectionAnalyzer.IsCollectionProducingExpression(c.Test) || ProjectionAnalyzer.IsCollectionProducingExpression(c.IfTrue) || ProjectionAnalyzer.IsCollectionProducingExpression(c.IfFalse))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, c.ToString()));
				}
				return base.VisitConditional(c);
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x0001711C File Offset: 0x0001531C
			internal override Expression VisitMemberAccess(MemberExpression m)
			{
				Type type = m.Expression.Type;
				this.leafExpressionIsMemberAccess = true;
				if (PrimitiveType.IsKnownNullableType(type))
				{
					this.leafExpressionIsMemberAccess = false;
					return base.VisitMemberAccess(m);
				}
				if (ProjectionAnalyzer.IsCollectionProducingExpression(m.Expression))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, m.ToString()));
				}
				PropertyInfo propertyInfo;
				Expression expression;
				if (ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(m, out propertyInfo, out expression))
				{
					Expression expression2 = base.VisitMemberAccess(m);
					if (ClientTypeUtil.TypeOrElementTypeIsEntity(type))
					{
						Type type2;
						ResourceBinder.StripTo<Expression>(m.Expression, out type2);
						this.box.AppendPropertyToPath(propertyInfo, type2, this.context);
						this.leafExpressionIsMemberAccess = false;
					}
					return expression2;
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, m.ToString()));
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x000171F4 File Offset: 0x000153F4
			internal override Expression VisitMethodCall(MethodCallExpression m)
			{
				if ((m.Object != null && ProjectionAnalyzer.IsDisallowedExpressionForMethodCall(m.Object, this.context.Model)) || m.Arguments.Any((Expression a) => ProjectionAnalyzer.IsDisallowedExpressionForMethodCall(a, this.context.Model)))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, m.ToString()));
				}
				ProjectionAnalyzer.CheckChainedSequence(m, this.type);
				if (ProjectionAnalyzer.IsMethodCallAllowedEntitySequence(m))
				{
					return base.VisitMethodCall(m);
				}
				if (m.Object == null || !ClientTypeUtil.TypeOrElementTypeIsEntity(m.Object.Type))
				{
					if (!m.Arguments.Any((Expression a) => ClientTypeUtil.TypeOrElementTypeIsEntity(a.Type)))
					{
						return base.VisitMethodCall(m);
					}
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, m.ToString()));
			}

			// Token: 0x06000602 RID: 1538 RVA: 0x000172EC File Offset: 0x000154EC
			internal override Expression VisitInvocation(InvocationExpression iv)
			{
				if (!ClientTypeUtil.TypeOrElementTypeIsEntity(iv.Expression.Type) && !ProjectionAnalyzer.IsCollectionProducingExpression(iv.Expression))
				{
					if (!iv.Arguments.Any((Expression a) => ClientTypeUtil.TypeOrElementTypeIsEntity(a.Type) || ProjectionAnalyzer.IsCollectionProducingExpression(a)))
					{
						return base.VisitInvocation(iv);
					}
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, iv.ToString()));
			}

			// Token: 0x06000603 RID: 1539 RVA: 0x00017360 File Offset: 0x00015560
			internal override Expression VisitLambda(LambdaExpression lambda)
			{
				ProjectionAnalyzer.Analyze(lambda, this.box, this.context);
				return lambda;
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x00017375 File Offset: 0x00015575
			internal override Expression VisitMemberInit(MemberInitExpression init)
			{
				ProjectionAnalyzer.Analyze(init, this.box, this.context);
				return init;
			}

			// Token: 0x06000605 RID: 1541 RVA: 0x0001738A File Offset: 0x0001558A
			internal override NewExpression VisitNew(NewExpression nex)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(nex.Type) && !ResourceBinder.PatternRules.MatchNewDataServiceCollectionOfT(nex))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, nex.ToString()));
				}
				return base.VisitNew(nex);
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x000173BF File Offset: 0x000155BF
			internal override Expression VisitParameter(ParameterExpression p)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(p.Type))
				{
					if (p != this.box.ParamExpressionInScope)
					{
						throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, p.ToString()));
					}
					this.box.StartNewPath();
				}
				return p;
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x000173FF File Offset: 0x000155FF
			internal override Expression VisitConstant(ConstantExpression c)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(c.Type))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, c.ToString()));
				}
				return base.VisitConstant(c);
			}

			// Token: 0x0400032E RID: 814
			private PathBox box;

			// Token: 0x0400032F RID: 815
			private Type type;

			// Token: 0x04000330 RID: 816
			private bool leafExpressionIsMemberAccess;

			// Token: 0x04000331 RID: 817
			private readonly DataServiceContext context;
		}
	}
}

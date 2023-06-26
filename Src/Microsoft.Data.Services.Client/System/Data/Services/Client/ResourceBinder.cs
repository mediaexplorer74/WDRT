using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Client.Metadata;
using System.Data.Services.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Data.Edm;

namespace System.Data.Services.Client
{
	// Token: 0x020000CD RID: 205
	internal class ResourceBinder : DataServiceALinqExpressionVisitor
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00019D54 File Offset: 0x00017F54
		private ClientEdmModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00019D61 File Offset: 0x00017F61
		private ResourceBinder(DataServiceContext context)
		{
			this.context = context;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00019D70 File Offset: 0x00017F70
		internal static Expression Bind(Expression e, DataServiceContext context)
		{
			ResourceBinder resourceBinder = new ResourceBinder(context);
			Expression expression = resourceBinder.Visit(e);
			ResourceBinder.VerifyKeyPredicates(expression);
			ResourceBinder.VerifyNotSelectManyProjection(expression);
			return expression;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00019D9C File Offset: 0x00017F9C
		internal static bool IsMissingKeyPredicates(Expression expression)
		{
			ResourceExpression resourceExpression = expression as ResourceExpression;
			if (resourceExpression != null)
			{
				if (ResourceBinder.IsMissingKeyPredicates(resourceExpression.Source))
				{
					return true;
				}
				if (resourceExpression.Source != null)
				{
					ResourceSetExpression resourceSetExpression = resourceExpression.Source as ResourceSetExpression;
					if (resourceSetExpression != null && !resourceSetExpression.HasKeyPredicate)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00019DE4 File Offset: 0x00017FE4
		internal static void VerifyKeyPredicates(Expression e)
		{
			if (ResourceBinder.IsMissingKeyPredicates(e))
			{
				throw new NotSupportedException(Strings.ALinq_CantNavigateWithoutKeyPredicate);
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00019DFC File Offset: 0x00017FFC
		internal static void VerifyNotSelectManyProjection(Expression expression)
		{
			ResourceSetExpression resourceSetExpression = expression as ResourceSetExpression;
			if (resourceSetExpression != null)
			{
				ProjectionQueryOptionExpression projection = resourceSetExpression.Projection;
				if (projection != null)
				{
					MethodCallExpression methodCallExpression = ResourceBinder.StripTo<MethodCallExpression>(projection.Selector.Body);
					if (methodCallExpression != null && methodCallExpression.Method.Name == "SelectMany")
					{
						throw new NotSupportedException(Strings.ALinq_UnsupportedExpression(methodCallExpression));
					}
				}
				else if (resourceSetExpression.HasTransparentScope)
				{
					throw new NotSupportedException(Strings.ALinq_UnsupportedExpression(resourceSetExpression));
				}
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00019E68 File Offset: 0x00018068
		private static Expression AnalyzePredicate(MethodCallExpression mce, ClientEdmModel model)
		{
			ResourceSetExpression resourceSetExpression;
			LambdaExpression lambdaExpression;
			if (!ResourceBinder.TryGetResourceSetMethodArguments(mce, out resourceSetExpression, out lambdaExpression))
			{
				ResourceBinder.ValidationRules.RequireNonSingleton(mce.Arguments[0]);
				return mce;
			}
			ResourceBinder.ValidationRules.CheckPredicate(lambdaExpression.Body, model);
			List<Expression> list = new List<Expression>();
			ResourceBinder.AddConjuncts(lambdaExpression.Body, list);
			Dictionary<ResourceSetExpression, List<Expression>> dictionary = new Dictionary<ResourceSetExpression, List<Expression>>(ReferenceEqualityComparer<ResourceSetExpression>.Instance);
			List<ResourceExpression> list2 = new List<ResourceExpression>();
			foreach (Expression expression in list)
			{
				ResourceBinder.ValidateFilter(expression, mce.Method.Name);
				Expression expression2 = InputBinder.Bind(expression, resourceSetExpression, lambdaExpression.Parameters[0], list2);
				if (list2.Count > 1)
				{
					return mce;
				}
				ResourceSetExpression resourceSetExpression2 = ((list2.Count == 0) ? resourceSetExpression : (list2[0] as ResourceSetExpression));
				if (resourceSetExpression2 == null)
				{
					return mce;
				}
				List<Expression> list3 = null;
				if (!dictionary.TryGetValue(resourceSetExpression2, out list3))
				{
					list3 = new List<Expression>();
					dictionary[resourceSetExpression2] = list3;
				}
				list3.Add(expression2);
				list2.Clear();
			}
			List<Expression> list4;
			if (dictionary.TryGetValue(resourceSetExpression, out list4))
			{
				dictionary.Remove(resourceSetExpression);
			}
			else
			{
				list4 = null;
			}
			foreach (KeyValuePair<ResourceSetExpression, List<Expression>> keyValuePair in dictionary)
			{
				ResourceSetExpression key = keyValuePair.Key;
				List<Expression> value = keyValuePair.Value;
				List<Expression> list6;
				List<Expression> list5 = ResourceBinder.ExtractKeyPredicate(key, value, model, out list6);
				if (list5 == null || (list6 != null && list6.Count > 0))
				{
					return mce;
				}
				key.SetKeyPredicate(list5);
			}
			if (list4 != null)
			{
				List<Expression> list7 = null;
				List<Expression> list8 = null;
				List<Expression> list9 = resourceSetExpression.KeyPredicateConjuncts.ToList<Expression>();
				if (resourceSetExpression.Filter != null && resourceSetExpression.Filter.PredicateConjuncts.Count > 0)
				{
					list9 = list9.Union(resourceSetExpression.Filter.PredicateConjuncts.Union(list4)).ToList<Expression>();
				}
				else
				{
					list9 = list9.Union(list4).ToList<Expression>();
				}
				if (!resourceSetExpression.ContainsNonKeyPredicate)
				{
					list7 = ResourceBinder.ExtractKeyPredicate(resourceSetExpression, list9, model, out list8);
				}
				if (list7 != null)
				{
					resourceSetExpression.SetKeyPredicate(list7);
					resourceSetExpression.RemoveFilterExpression();
				}
				if (list7 != null && resourceSetExpression.HasSequenceQueryOptions)
				{
					resourceSetExpression.ConvertKeyToFilterExpression();
				}
				if (list7 == null)
				{
					resourceSetExpression.ConvertKeyToFilterExpression();
					resourceSetExpression.AddFilter(list4);
				}
			}
			return resourceSetExpression;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001A0E4 File Offset: 0x000182E4
		private static void ValidateFilter(Expression exp, string method)
		{
			BinaryExpression binaryExpression = ResourceBinder.StripTo<BinaryExpression>(exp);
			if (binaryExpression != null)
			{
				ResourceBinder.ValidationRules.DisallowExpressionEndWithTypeAs(binaryExpression.Left, method);
				ResourceBinder.ValidationRules.DisallowExpressionEndWithTypeAs(binaryExpression.Right, method);
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001A114 File Offset: 0x00018314
		private static List<Expression> ExtractKeyPredicate(ResourceSetExpression target, List<Expression> predicates, ClientEdmModel edmModel, out List<Expression> nonKeyPredicates)
		{
			Dictionary<PropertyInfo, ConstantExpression> dictionary = null;
			nonKeyPredicates = null;
			List<Expression> list = null;
			foreach (Expression expression in predicates)
			{
				PropertyInfo propertyInfo;
				ConstantExpression constantExpression;
				if (ResourceBinder.PatternRules.MatchKeyComparison(expression, out propertyInfo, out constantExpression))
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<PropertyInfo, ConstantExpression>(EqualityComparer<PropertyInfo>.Default);
						list = new List<Expression>();
					}
					else if (dictionary.ContainsKey(propertyInfo))
					{
						throw Error.NotSupported(Strings.ALinq_CanOnlyApplyOneKeyPredicate);
					}
					dictionary.Add(propertyInfo, constantExpression);
					list.Add(expression);
				}
				else
				{
					if (nonKeyPredicates == null)
					{
						nonKeyPredicates = new List<Expression>();
					}
					nonKeyPredicates.Add(expression);
				}
			}
			if (nonKeyPredicates != null && nonKeyPredicates.Count > 0)
			{
				target.ContainsNonKeyPredicate = true;
				list = null;
			}
			else if (dictionary != null)
			{
				IEdmEntityType edmEntityType = (IEdmEntityType)edmModel.GetOrCreateEdmType(target.CreateReference().Type);
				if (edmEntityType.Key().Count<IEdmStructuralProperty>() != dictionary.Keys.Count)
				{
					dictionary = null;
					list = null;
				}
			}
			return list;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001A218 File Offset: 0x00018418
		private static void AddConjuncts(Expression e, List<Expression> conjuncts)
		{
			if (ResourceBinder.PatternRules.MatchAnd(e))
			{
				BinaryExpression binaryExpression = (BinaryExpression)e;
				ResourceBinder.AddConjuncts(binaryExpression.Left, conjuncts);
				ResourceBinder.AddConjuncts(binaryExpression.Right, conjuncts);
				return;
			}
			conjuncts.Add(e);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001A254 File Offset: 0x00018454
		internal bool AnalyzeProjection(MethodCallExpression mce, SequenceMethod sequenceMethod, out Expression e)
		{
			e = mce;
			bool flag = sequenceMethod == SequenceMethod.SelectManyResultSelector;
			ResourceExpression resourceExpression = this.Visit(mce.Arguments[0]) as ResourceExpression;
			if (resourceExpression == null)
			{
				return false;
			}
			if (sequenceMethod == SequenceMethod.SelectManyResultSelector)
			{
				Expression expression = mce.Arguments[1];
				if (!ResourceBinder.PatternRules.MatchParameterMemberAccess(expression))
				{
					return false;
				}
				Expression expression2 = mce.Arguments[2];
				LambdaExpression lambdaExpression;
				if (!ResourceBinder.PatternRules.MatchDoubleArgumentLambda(expression2, out lambdaExpression))
				{
					return false;
				}
				if (ResourceBinder.ExpressionPresenceVisitor.IsExpressionPresent(lambdaExpression.Parameters[0], lambdaExpression.Body))
				{
					return false;
				}
				List<ResourceExpression> list = new List<ResourceExpression>();
				LambdaExpression lambdaExpression2 = ResourceBinder.StripTo<LambdaExpression>(expression);
				Expression expression3 = InputBinder.Bind(lambdaExpression2.Body, resourceExpression, lambdaExpression2.Parameters[0], list);
				expression3 = ResourceBinder.StripCastMethodCalls(expression3);
				MemberExpression memberExpression;
				if (!ResourceBinder.PatternRules.MatchPropertyProjectionRelatedSet(resourceExpression, expression3, this.context, out memberExpression))
				{
					return false;
				}
				ResourceExpression resourceExpression2 = ResourceBinder.CreateResourceSetExpression(mce.Method.ReturnType, resourceExpression, memberExpression, TypeSystem.GetElementType(memberExpression.Type));
				if (!ResourceBinder.PatternRules.MatchMemberInitExpressionWithDefaultConstructor(resourceExpression2, lambdaExpression) && !ResourceBinder.PatternRules.MatchNewExpression(resourceExpression2, lambdaExpression))
				{
					return false;
				}
				lambdaExpression = Expression.Lambda(lambdaExpression.Body, new ParameterExpression[] { lambdaExpression.Parameters[1] });
				ResourceExpression resourceExpression3 = resourceExpression2.CreateCloneWithNewType(mce.Type);
				bool flag2;
				try
				{
					flag2 = ProjectionAnalyzer.Analyze(lambdaExpression, resourceExpression3, false, this.context);
				}
				catch (NotSupportedException)
				{
					flag2 = false;
				}
				if (!flag2)
				{
					return false;
				}
				e = resourceExpression3;
				ResourceBinder.ValidationRules.RequireCanProject(resourceExpression2);
			}
			else
			{
				LambdaExpression lambdaExpression3;
				if (!ResourceBinder.PatternRules.MatchSingleArgumentLambda(mce.Arguments[1], out lambdaExpression3))
				{
					return false;
				}
				lambdaExpression3 = ProjectionRewriter.TryToRewrite(lambdaExpression3, resourceExpression);
				ResourceExpression resourceExpression4 = resourceExpression.CreateCloneWithNewType(mce.Type);
				if (!ProjectionAnalyzer.Analyze(lambdaExpression3, resourceExpression4, flag, this.context))
				{
					return false;
				}
				ResourceBinder.ValidationRules.RequireCanProject(resourceExpression);
				e = resourceExpression4;
			}
			return true;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001A420 File Offset: 0x00018620
		internal static Expression AnalyzeNavigation(MethodCallExpression mce, DataServiceContext context)
		{
			Expression expression = mce.Arguments[0];
			LambdaExpression lambdaExpression;
			if (!ResourceBinder.PatternRules.MatchSingleArgumentLambda(mce.Arguments[1], out lambdaExpression))
			{
				return mce;
			}
			ResourceBinder.ValidationRules.DisallowExpressionEndWithTypeAs(lambdaExpression.Body, mce.Method.Name);
			if (ResourceBinder.PatternRules.MatchIdentitySelector(lambdaExpression))
			{
				return expression;
			}
			if (ResourceBinder.PatternRules.MatchTransparentIdentitySelector(expression, lambdaExpression, context))
			{
				return ResourceBinder.RemoveTransparentScope(mce.Method.ReturnType, (ResourceSetExpression)expression);
			}
			ResourceExpression resourceExpression;
			Expression expression2;
			MemberExpression memberExpression;
			if (ResourceBinder.IsValidNavigationSource(expression, out resourceExpression) && ResourceBinder.TryBindToInput(resourceExpression, lambdaExpression, out expression2) && ResourceBinder.PatternRules.MatchPropertyProjectionSingleton(resourceExpression, expression2, context, out memberExpression))
			{
				ResourceBinder.ValidationRules.DisallowMemberAccessInNavigation(memberExpression, context.Model);
				expression2 = memberExpression;
				return ResourceBinder.CreateNavigationPropertySingletonExpression(mce.Method.ReturnType, resourceExpression, expression2);
			}
			return mce;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001A4D7 File Offset: 0x000186D7
		private static bool IsValidNavigationSource(Expression input, out ResourceExpression sourceExpression)
		{
			ResourceBinder.ValidationRules.RequireCanNavigate(input);
			sourceExpression = input as ResourceExpression;
			return sourceExpression != null;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001A4F0 File Offset: 0x000186F0
		internal static Expression AnalyzeSelectMany(MethodCallExpression mce, DataServiceContext context)
		{
			if (mce.Arguments.Count != 2 && mce.Arguments.Count != 3)
			{
				return mce;
			}
			ResourceExpression resourceExpression;
			if (!ResourceBinder.IsValidNavigationSource(mce.Arguments[0], out resourceExpression))
			{
				return mce;
			}
			LambdaExpression lambdaExpression;
			if (!ResourceBinder.PatternRules.MatchSingleArgumentLambda(mce.Arguments[1], out lambdaExpression))
			{
				return mce;
			}
			ResourceBinder.ValidationRules.DisallowExpressionEndWithTypeAs(lambdaExpression.Body, mce.Method.Name);
			List<ResourceExpression> list = new List<ResourceExpression>();
			Expression expression = InputBinder.Bind(lambdaExpression.Body, resourceExpression, lambdaExpression.Parameters[0], list);
			ResourceSetExpression resourceSetExpression;
			if (!ResourceBinder.TryAnalyzeSelectManyCollector(resourceExpression, expression, context, out resourceSetExpression))
			{
				return mce;
			}
			if (resourceSetExpression.Type != mce.Method.ReturnType)
			{
				resourceSetExpression = resourceSetExpression.CreateCloneForTransparentScope(mce.Method.ReturnType);
			}
			if (mce.Arguments.Count == 3)
			{
				return ResourceBinder.AnalyzeSelectManySelector(mce, resourceSetExpression, context);
			}
			return resourceSetExpression;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001A5D4 File Offset: 0x000187D4
		private static bool TryAnalyzeSelectManyCollector(ResourceExpression input, Expression navPropRef, DataServiceContext context, out ResourceSetExpression result)
		{
			MethodCallExpression methodCallExpression = ResourceBinder.StripTo<MethodCallExpression>(navPropRef);
			SequenceMethod sequenceMethod;
			MemberExpression memberExpression;
			if (methodCallExpression != null && ReflectionUtil.TryIdentifySequenceMethod(methodCallExpression.Method, out sequenceMethod) && (sequenceMethod == SequenceMethod.Cast || sequenceMethod == SequenceMethod.OfType))
			{
				if (ResourceBinder.TryAnalyzeSelectManyCollector(input, methodCallExpression.Arguments[0], context, out result))
				{
					methodCallExpression = Expression.Call(methodCallExpression.Method, result);
					if (sequenceMethod == SequenceMethod.Cast)
					{
						result = ResourceBinder.AnalyzeCast(methodCallExpression) as ResourceSetExpression;
					}
					else
					{
						result = ResourceBinder.AnalyzeOfType(methodCallExpression, context.MaxProtocolVersion) as ResourceSetExpression;
					}
				}
				else
				{
					result = null;
				}
			}
			else if (ResourceBinder.PatternRules.MatchPropertyProjectionRelatedSet(input, navPropRef, context, out memberExpression))
			{
				Type elementType = TypeSystem.GetElementType(memberExpression.Type);
				result = ResourceBinder.CreateResourceSetExpression(memberExpression.Type, input, memberExpression, elementType);
			}
			else
			{
				result = null;
			}
			return result != null;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001A688 File Offset: 0x00018888
		private static Expression AnalyzeSelectManySelector(MethodCallExpression selectManyCall, ResourceSetExpression sourceResourceSet, DataServiceContext context)
		{
			LambdaExpression lambdaExpression = ResourceBinder.StripTo<LambdaExpression>(selectManyCall.Arguments[2]);
			ResourceSetExpression.TransparentAccessors transparentAccessors;
			Expression expression;
			if (ResourceBinder.PatternRules.MatchTransparentScopeSelector(sourceResourceSet, lambdaExpression, out transparentAccessors))
			{
				sourceResourceSet.TransparentScope = transparentAccessors;
				expression = sourceResourceSet;
			}
			else if (ResourceBinder.PatternRules.MatchIdentityProjectionResultSelector(lambdaExpression))
			{
				expression = sourceResourceSet;
			}
			else if (ResourceBinder.PatternRules.MatchMemberInitExpressionWithDefaultConstructor(sourceResourceSet, lambdaExpression) || ResourceBinder.PatternRules.MatchNewExpression(sourceResourceSet, lambdaExpression))
			{
				lambdaExpression = Expression.Lambda(lambdaExpression.Body, new ParameterExpression[] { lambdaExpression.Parameters[1] });
				if (!ProjectionAnalyzer.Analyze(lambdaExpression, sourceResourceSet, false, context))
				{
					expression = selectManyCall;
				}
				else
				{
					expression = sourceResourceSet;
				}
			}
			else
			{
				expression = selectManyCall;
			}
			return expression;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001A714 File Offset: 0x00018914
		internal static Expression ApplyOrdering(MethodCallExpression mce, bool descending, bool thenBy, ClientEdmModel model)
		{
			ResourceBinder.ValidationRules.CheckOrderBy(mce, model);
			ResourceSetExpression resourceSetExpression;
			LambdaExpression lambdaExpression;
			if (!ResourceBinder.TryGetResourceSetMethodArguments(mce, out resourceSetExpression, out lambdaExpression))
			{
				return mce;
			}
			ResourceBinder.ValidationRules.DisallowExpressionEndWithTypeAs(lambdaExpression.Body, mce.Method.Name);
			Expression expression;
			if (!ResourceBinder.TryBindToInput(resourceSetExpression, lambdaExpression, out expression))
			{
				return mce;
			}
			List<OrderByQueryOptionExpression.Selector> list;
			if (!thenBy)
			{
				list = new List<OrderByQueryOptionExpression.Selector>();
				ResourceBinder.AddSequenceQueryOption(resourceSetExpression, new OrderByQueryOptionExpression(mce.Type, list));
			}
			else
			{
				list = resourceSetExpression.OrderBy.Selectors;
			}
			list.Add(new OrderByQueryOptionExpression.Selector(expression, descending));
			if (resourceSetExpression.Type != mce.Method.ReturnType)
			{
				return resourceSetExpression.CreateCloneWithNewType(mce.Method.ReturnType);
			}
			return resourceSetExpression;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001A7BC File Offset: 0x000189BC
		private static Expression LimitCardinality(MethodCallExpression mce, int maxCardinality)
		{
			if (mce.Arguments.Count != 1)
			{
				return mce;
			}
			ResourceSetExpression resourceSetExpression = mce.Arguments[0] as ResourceSetExpression;
			if (resourceSetExpression != null)
			{
				if (!resourceSetExpression.HasKeyPredicate && resourceSetExpression.NodeType != (ExpressionType)10001 && (resourceSetExpression.Take == null || (int)resourceSetExpression.Take.TakeAmount.Value > maxCardinality))
				{
					ResourceBinder.AddSequenceQueryOption(resourceSetExpression, new TakeQueryOptionExpression(mce.Type, Expression.Constant(maxCardinality)));
				}
				return mce.Arguments[0];
			}
			if (mce.Arguments[0] is NavigationPropertySingletonExpression)
			{
				return mce.Arguments[0];
			}
			return mce;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001A870 File Offset: 0x00018A70
		private static Expression AnalyzeCast(MethodCallExpression mce)
		{
			ResourceExpression resourceExpression = mce.Arguments[0] as ResourceExpression;
			if (resourceExpression != null)
			{
				return resourceExpression.CreateCloneWithNewType(mce.Method.ReturnType);
			}
			return mce;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001A8A8 File Offset: 0x00018AA8
		private static Expression AnalyzeOfType(MethodCallExpression mce, DataServiceProtocolVersion maxProtocolVersion)
		{
			if (maxProtocolVersion < DataServiceProtocolVersion.V3)
			{
				throw new NotSupportedException(Strings.ALinq_MethodNotSupportedForMaxDataServiceVersionLessThanX(mce.Method.Name, Util.DataServiceVersion3.ToString(2)));
			}
			ResourceSetExpression resourceSetExpression = mce.Arguments[0] as ResourceSetExpression;
			if (resourceSetExpression != null)
			{
				Type type = mce.Method.GetGenericArguments().SingleOrDefault<Type>();
				if (type == null)
				{
					throw new InvalidOperationException(Strings.ALinq_OfTypeArgumentNotAvailable);
				}
				if (type.IsAssignableFrom(resourceSetExpression.ResourceType))
				{
					return resourceSetExpression;
				}
				if (resourceSetExpression.ResourceType.IsAssignableFrom(type))
				{
					if (resourceSetExpression.ResourceTypeAs != null)
					{
						throw new NotSupportedException(Strings.ALinq_CannotUseTypeFiltersMultipleTimes);
					}
					resourceSetExpression.ResourceTypeAs = type;
					return resourceSetExpression.CreateCloneWithNewType(mce.Method.ReturnType);
				}
			}
			return mce;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001A965 File Offset: 0x00018B65
		private static Expression AnalyzeAnyAll(MethodCallExpression mce, DataServiceProtocolVersion maxProtocolVersion)
		{
			if (maxProtocolVersion < DataServiceProtocolVersion.V3)
			{
				throw new NotSupportedException(Strings.ALinq_MethodNotSupportedForMaxDataServiceVersionLessThanX(mce.Method.Name, Util.DataServiceVersion3.ToString(2)));
			}
			return mce;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001A990 File Offset: 0x00018B90
		private static Expression StripConvert(Expression e)
		{
			UnaryExpression unaryExpression = e as UnaryExpression;
			if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert && unaryExpression.Type.IsGenericType() && (unaryExpression.Type.GetGenericTypeDefinition() == typeof(DataServiceQuery<>) || unaryExpression.Type.GetGenericTypeDefinition() == typeof(DataServiceQuery<>.DataServiceOrderedQuery)))
			{
				e = unaryExpression.Operand;
				ResourceExpression resourceExpression = e as ResourceExpression;
				if (resourceExpression != null)
				{
					e = resourceExpression.CreateCloneWithNewType(unaryExpression.Type);
				}
			}
			return e;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001AA18 File Offset: 0x00018C18
		private static Expression AnalyzeExpand(MethodCallExpression mce, DataServiceContext context)
		{
			Expression expression = ResourceBinder.StripConvert(mce.Object);
			ResourceExpression resourceExpression = expression as ResourceExpression;
			if (resourceExpression == null)
			{
				return mce;
			}
			ResourceBinder.ValidationRules.RequireCanExpand(resourceExpression);
			Expression expression2 = ResourceBinder.StripTo<Expression>(mce.Arguments[0]);
			string text = null;
			if (expression2.NodeType == ExpressionType.Constant)
			{
				text = (string)((ConstantExpression)expression2).Value;
			}
			else if (expression2.NodeType == ExpressionType.Lambda)
			{
				Version version;
				ResourceBinder.ValidationRules.ValidateExpandPath(expression2, context, out text, out version);
				resourceExpression.RaiseUriVersion(version);
			}
			if (!resourceExpression.ExpandPaths.Contains(text))
			{
				resourceExpression.ExpandPaths.Add(text);
			}
			return resourceExpression;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001AAAC File Offset: 0x00018CAC
		private static Expression AnalyzeAddCustomQueryOption(MethodCallExpression mce)
		{
			Expression expression = ResourceBinder.StripConvert(mce.Object);
			ResourceExpression resourceExpression = expression as ResourceExpression;
			if (resourceExpression == null)
			{
				return mce;
			}
			ResourceBinder.ValidationRules.RequireCanAddCustomQueryOption(resourceExpression);
			ConstantExpression constantExpression = ResourceBinder.StripTo<ConstantExpression>(mce.Arguments[0]);
			ConstantExpression constantExpression2 = ResourceBinder.StripTo<ConstantExpression>(mce.Arguments[1]);
			if (((string)constantExpression.Value).Trim() == '$' + "expand")
			{
				ResourceBinder.ValidationRules.RequireCanExpand(resourceExpression);
				resourceExpression.ExpandPaths = resourceExpression.ExpandPaths.Union(new string[] { (string)constantExpression2.Value }, StringComparer.Ordinal).ToList<string>();
			}
			else
			{
				ResourceBinder.ValidationRules.RequireLegalCustomQueryOption(mce.Arguments[0], resourceExpression);
				resourceExpression.CustomQueryOptions.Add(constantExpression, constantExpression2);
			}
			return resourceExpression;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001AB80 File Offset: 0x00018D80
		private static Expression AnalyzeAddCountOption(MethodCallExpression mce, CountOption countOption)
		{
			Expression expression = ResourceBinder.StripConvert(mce.Object);
			ResourceSetExpression resourceSetExpression = expression as ResourceSetExpression;
			if (resourceSetExpression == null)
			{
				return mce;
			}
			ResourceBinder.ValidationRules.RequireCanAddCount(resourceSetExpression);
			resourceSetExpression.ConvertKeyToFilterExpression();
			resourceSetExpression.CountOption = countOption;
			return resourceSetExpression;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001ABCC File Offset: 0x00018DCC
		private static ResourceSetExpression CreateResourceSetExpression(Type type, ResourceExpression source, Expression memberExpression, Type resourceType)
		{
			Type elementType = TypeSystem.GetElementType(type);
			Type type2 = typeof(IOrderedQueryable<>).MakeGenericType(new Type[] { elementType });
			ResourceSetExpression resourceSetExpression = new ResourceSetExpression(type2, source, memberExpression, resourceType, source.ExpandPaths.ToList<string>(), source.CountOption, source.CustomQueryOptions.ToDictionary((KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Key, (KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Value), null, null, source.UriVersion);
			source.ExpandPaths.Clear();
			source.CountOption = CountOption.None;
			source.CustomQueryOptions.Clear();
			return resourceSetExpression;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001AC94 File Offset: 0x00018E94
		private static NavigationPropertySingletonExpression CreateNavigationPropertySingletonExpression(Type type, ResourceExpression source, Expression memberExpression)
		{
			NavigationPropertySingletonExpression navigationPropertySingletonExpression = new NavigationPropertySingletonExpression(type, source, memberExpression, memberExpression.Type, source.ExpandPaths.ToList<string>(), source.CountOption, source.CustomQueryOptions.ToDictionary((KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Key, (KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Value), null, null, source.UriVersion);
			source.ExpandPaths.Clear();
			source.CountOption = CountOption.None;
			source.CustomQueryOptions.Clear();
			return navigationPropertySingletonExpression;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001AD2C File Offset: 0x00018F2C
		private static ResourceSetExpression RemoveTransparentScope(Type expectedResultType, ResourceSetExpression input)
		{
			ResourceSetExpression resourceSetExpression = new ResourceSetExpression(expectedResultType, input.Source, input.MemberExpression, input.ResourceType, input.ExpandPaths, input.CountOption, input.CustomQueryOptions, input.Projection, input.ResourceTypeAs, input.UriVersion);
			resourceSetExpression.SetKeyPredicate(input.KeyPredicateConjuncts);
			foreach (QueryOptionExpression queryOptionExpression in input.SequenceQueryOptions)
			{
				resourceSetExpression.AddSequenceQueryOption(queryOptionExpression);
			}
			resourceSetExpression.OverrideInputReference(input);
			return resourceSetExpression;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001ADCC File Offset: 0x00018FCC
		internal static Expression StripConvertToAssignable(Expression e)
		{
			UnaryExpression unaryExpression = e as UnaryExpression;
			Expression expression;
			if (unaryExpression != null && ResourceBinder.PatternRules.MatchConvertToAssignable(unaryExpression))
			{
				expression = unaryExpression.Operand;
			}
			else
			{
				expression = e;
			}
			return expression;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001ADF8 File Offset: 0x00018FF8
		internal static T StripTo<T>(Expression expression) where T : Expression
		{
			Expression expression2;
			do
			{
				expression2 = expression;
				expression = ((expression.NodeType == ExpressionType.Quote) ? ((UnaryExpression)expression).Operand : expression);
				expression = ResourceBinder.StripConvertToAssignable(expression);
			}
			while (expression2 != expression);
			return expression2 as T;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001AE38 File Offset: 0x00019038
		internal static T StripTo<T>(Expression expression, out Type convertedType) where T : Expression
		{
			convertedType = null;
			Expression expression2;
			for (;;)
			{
				expression2 = expression;
				expression = ((expression.NodeType == ExpressionType.Quote) ? ((UnaryExpression)expression).Operand : expression);
				if (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked || expression.NodeType == ExpressionType.TypeAs)
				{
					UnaryExpression unaryExpression = expression as UnaryExpression;
					if (unaryExpression != null)
					{
						if (ResourceBinder.PatternRules.MatchConvertToAssignable(unaryExpression))
						{
							expression = unaryExpression.Operand;
						}
						else if (expression.NodeType == ExpressionType.TypeAs)
						{
							if (convertedType != null)
							{
								break;
							}
							expression = unaryExpression.Operand;
							convertedType = unaryExpression.Type;
						}
					}
				}
				if (expression2 == expression)
				{
					goto Block_8;
				}
			}
			throw new NotSupportedException(Strings.ALinq_CannotUseTypeFiltersMultipleTimes);
			Block_8:
			return expression2 as T;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001AEE0 File Offset: 0x000190E0
		internal override Expression VisitResourceSetExpression(ResourceSetExpression rse)
		{
			if (rse.NodeType == (ExpressionType)10000)
			{
				return new ResourceSetExpression(rse.Type, rse.Source, rse.MemberExpression, rse.ResourceType, null, CountOption.None, null, null, rse.ResourceTypeAs, rse.UriVersion);
			}
			return rse;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001AF29 File Offset: 0x00019129
		private static bool TryGetResourceSetMethodArguments(MethodCallExpression mce, out ResourceSetExpression input, out LambdaExpression lambda)
		{
			input = null;
			lambda = null;
			input = mce.Arguments[0] as ResourceSetExpression;
			return input != null && ResourceBinder.PatternRules.MatchSingleArgumentLambda(mce.Arguments[1], out lambda);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001AF60 File Offset: 0x00019160
		private static bool TryBindToInput(ResourceExpression input, LambdaExpression le, out Expression bound)
		{
			List<ResourceExpression> list = new List<ResourceExpression>();
			bound = InputBinder.Bind(le.Body, input, le.Parameters[0], list);
			if (list.Count > 1 || (list.Count == 1 && list[0] != input))
			{
				bound = null;
			}
			return bound != null;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001AFB8 File Offset: 0x000191B8
		private static Expression AnalyzeResourceSetConstantMethod(MethodCallExpression mce, Func<MethodCallExpression, ResourceExpression, ConstantExpression, Expression> constantMethodAnalyzer)
		{
			ResourceExpression resourceExpression = (ResourceExpression)mce.Arguments[0];
			ConstantExpression constantExpression = ResourceBinder.StripTo<ConstantExpression>(mce.Arguments[1]);
			if (constantExpression == null)
			{
				return mce;
			}
			return constantMethodAnalyzer(mce, resourceExpression, constantExpression);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001AFF8 File Offset: 0x000191F8
		private static Expression AnalyzeCountMethod(MethodCallExpression mce)
		{
			ResourceSetExpression resourceSetExpression = mce.Arguments[0] as ResourceSetExpression;
			if (resourceSetExpression == null)
			{
				return mce;
			}
			ResourceBinder.ValidationRules.RequireCanAddCount(resourceSetExpression);
			resourceSetExpression.ConvertKeyToFilterExpression();
			resourceSetExpression.CountOption = CountOption.ValueOnly;
			return resourceSetExpression;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001B030 File Offset: 0x00019230
		private static void AddSequenceQueryOption(ResourceExpression target, QueryOptionExpression qoe)
		{
			ResourceSetExpression resourceSetExpression = (ResourceSetExpression)target;
			resourceSetExpression.ConvertKeyToFilterExpression();
			switch (qoe.NodeType)
			{
			case (ExpressionType)10004:
				if (resourceSetExpression.Take != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("skip", "top"));
				}
				break;
			case (ExpressionType)10005:
				if (resourceSetExpression.Skip != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("orderby", "skip"));
				}
				if (resourceSetExpression.Take != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("orderby", "top"));
				}
				if (resourceSetExpression.Projection != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("orderby", "select"));
				}
				break;
			case (ExpressionType)10006:
				if (resourceSetExpression.Skip != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "skip"));
				}
				if (resourceSetExpression.Take != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "top"));
				}
				if (resourceSetExpression.Projection != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "select"));
				}
				break;
			}
			resourceSetExpression.AddSequenceQueryOption(qoe);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001B144 File Offset: 0x00019344
		internal override Expression VisitBinary(BinaryExpression b)
		{
			Expression expression = base.VisitBinary(b);
			if (ResourceBinder.PatternRules.MatchStringAddition(expression))
			{
				BinaryExpression binaryExpression = ResourceBinder.StripTo<BinaryExpression>(expression);
				MethodInfo method = typeof(string).GetMethod("Concat", new Type[]
				{
					typeof(string),
					typeof(string)
				});
				return Expression.Call(method, new Expression[] { binaryExpression.Left, binaryExpression.Right });
			}
			return expression;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001B1C8 File Offset: 0x000193C8
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			Expression expression = base.VisitMemberAccess(m);
			MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(expression);
			PropertyInfo propertyInfo;
			Expression expression2;
			MethodInfo methodInfo;
			if (memberExpression != null && ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(memberExpression, out propertyInfo, out expression2) && TypeSystem.TryGetPropertyAsMethod(propertyInfo, out methodInfo))
			{
				return Expression.Call(memberExpression.Expression, methodInfo);
			}
			return expression;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001B238 File Offset: 0x00019438
		internal override Expression VisitMethodCall(MethodCallExpression mce)
		{
			SequenceMethod sequenceMethod;
			Expression expression;
			if (ReflectionUtil.TryIdentifySequenceMethod(mce.Method, out sequenceMethod) && (sequenceMethod == SequenceMethod.Select || sequenceMethod == SequenceMethod.SelectManyResultSelector) && this.AnalyzeProjection(mce, sequenceMethod, out expression))
			{
				return expression;
			}
			expression = base.VisitMethodCall(mce);
			mce = expression as MethodCallExpression;
			if (mce == null)
			{
				return expression;
			}
			if (ReflectionUtil.TryIdentifySequenceMethod(mce.Method, out sequenceMethod))
			{
				SequenceMethod sequenceMethod2 = sequenceMethod;
				if (sequenceMethod2 <= SequenceMethod.Skip)
				{
					switch (sequenceMethod2)
					{
					case SequenceMethod.Where:
						return ResourceBinder.AnalyzePredicate(mce, this.Model);
					case SequenceMethod.WhereOrdinal:
					case SequenceMethod.SelectOrdinal:
					case SequenceMethod.SelectManyOrdinal:
						break;
					case SequenceMethod.OfType:
						return ResourceBinder.AnalyzeOfType(mce, this.Model.MaxProtocolVersion);
					case SequenceMethod.Cast:
						return ResourceBinder.AnalyzeCast(mce);
					case SequenceMethod.Select:
						return ResourceBinder.AnalyzeNavigation(mce, this.context);
					case SequenceMethod.SelectMany:
					case SequenceMethod.SelectManyResultSelector:
						return ResourceBinder.AnalyzeSelectMany(mce, this.context);
					default:
						switch (sequenceMethod2)
						{
						case SequenceMethod.OrderBy:
							return ResourceBinder.ApplyOrdering(mce, false, false, this.Model);
						case SequenceMethod.OrderByComparer:
						case SequenceMethod.OrderByDescendingComparer:
						case SequenceMethod.ThenByComparer:
						case SequenceMethod.ThenByDescendingComparer:
							break;
						case SequenceMethod.OrderByDescending:
							return ResourceBinder.ApplyOrdering(mce, true, false, this.Model);
						case SequenceMethod.ThenBy:
							return ResourceBinder.ApplyOrdering(mce, false, true, this.Model);
						case SequenceMethod.ThenByDescending:
							return ResourceBinder.ApplyOrdering(mce, true, true, this.Model);
						case SequenceMethod.Take:
							return ResourceBinder.AnalyzeResourceSetConstantMethod(mce, delegate(MethodCallExpression callExp, ResourceExpression resource, ConstantExpression takeCount)
							{
								ResourceBinder.AddSequenceQueryOption(resource, new TakeQueryOptionExpression(callExp.Type, takeCount));
								return resource;
							});
						default:
							if (sequenceMethod2 == SequenceMethod.Skip)
							{
								return ResourceBinder.AnalyzeResourceSetConstantMethod(mce, delegate(MethodCallExpression callExp, ResourceExpression resource, ConstantExpression skipCount)
								{
									ResourceBinder.AddSequenceQueryOption(resource, new SkipQueryOptionExpression(callExp.Type, skipCount));
									return resource;
								});
							}
							break;
						}
						break;
					}
				}
				else
				{
					switch (sequenceMethod2)
					{
					case SequenceMethod.First:
					case SequenceMethod.FirstOrDefault:
						return ResourceBinder.LimitCardinality(mce, 1);
					case SequenceMethod.FirstPredicate:
						break;
					default:
						switch (sequenceMethod2)
						{
						case SequenceMethod.Single:
						case SequenceMethod.SingleOrDefault:
							return ResourceBinder.LimitCardinality(mce, 2);
						case SequenceMethod.SinglePredicate:
							break;
						default:
							switch (sequenceMethod2)
							{
							case SequenceMethod.Any:
							case SequenceMethod.AnyPredicate:
							case SequenceMethod.All:
								return ResourceBinder.AnalyzeAnyAll(mce, this.Model.MaxProtocolVersion);
							case SequenceMethod.Count:
							case SequenceMethod.LongCount:
								return ResourceBinder.AnalyzeCountMethod(mce);
							}
							break;
						}
						break;
					}
				}
				throw Error.MethodNotSupported(mce);
			}
			if (!mce.Method.DeclaringType.IsGenericType() || !(mce.Method.DeclaringType.GetGenericTypeDefinition() == typeof(DataServiceQuery<>)))
			{
				return mce;
			}
			Type type = typeof(DataServiceQuery<>).MakeGenericType(new Type[] { mce.Method.DeclaringType.GetGenericArguments()[0] });
			if (mce.Method.Name == "Expand" && mce.Method.DeclaringType == type)
			{
				return ResourceBinder.AnalyzeExpand(mce, this.context);
			}
			if (mce.Method.Name == "AddQueryOption" && mce.Method.DeclaringType == type)
			{
				return ResourceBinder.AnalyzeAddCustomQueryOption(mce);
			}
			if (mce.Method.Name == "IncludeTotalCount" && mce.Method.DeclaringType == type)
			{
				return ResourceBinder.AnalyzeAddCountOption(mce, CountOption.InlineAll);
			}
			throw Error.MethodNotSupported(mce);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001B558 File Offset: 0x00019758
		private static Expression StripCastMethodCalls(Expression expression)
		{
			MethodCallExpression methodCallExpression = ResourceBinder.StripTo<MethodCallExpression>(expression);
			while (methodCallExpression != null && ReflectionUtil.IsSequenceMethod(methodCallExpression.Method, SequenceMethod.Cast))
			{
				expression = methodCallExpression.Arguments[0];
				methodCallExpression = ResourceBinder.StripTo<MethodCallExpression>(expression);
			}
			return expression;
		}

		// Token: 0x04000412 RID: 1042
		private const string AddQueryOptionMethodName = "AddQueryOption";

		// Token: 0x04000413 RID: 1043
		private const string ExpandMethodName = "Expand";

		// Token: 0x04000414 RID: 1044
		private const string IncludeTotalCountMethodName = "IncludeTotalCount";

		// Token: 0x04000415 RID: 1045
		private readonly DataServiceContext context;

		// Token: 0x020000CE RID: 206
		internal static class PatternRules
		{
			// Token: 0x060006A6 RID: 1702 RVA: 0x0001B595 File Offset: 0x00019795
			internal static bool MatchConvertToAssignable(UnaryExpression expression)
			{
				return (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked || expression.NodeType == ExpressionType.TypeAs) && expression.Type.IsAssignableFrom(expression.Operand.Type);
			}

			// Token: 0x060006A7 RID: 1703 RVA: 0x0001B5D0 File Offset: 0x000197D0
			internal static bool MatchParameterMemberAccess(Expression expression)
			{
				LambdaExpression lambdaExpression = ResourceBinder.StripTo<LambdaExpression>(expression);
				if (lambdaExpression == null || lambdaExpression.Parameters.Count != 1)
				{
					return false;
				}
				ParameterExpression parameterExpression = lambdaExpression.Parameters[0];
				Expression expression2 = ResourceBinder.StripCastMethodCalls(lambdaExpression.Body);
				for (MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(expression2); memberExpression != null; memberExpression = ResourceBinder.StripTo<MemberExpression>(memberExpression.Expression))
				{
					if (memberExpression.Expression == parameterExpression)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060006A8 RID: 1704 RVA: 0x0001B634 File Offset: 0x00019834
			internal static bool MatchPropertyAccess(Expression e, DataServiceContext context, out MemberExpression member, out Expression instance, out List<string> propertyPath, out Version uriVersion)
			{
				instance = null;
				propertyPath = null;
				uriVersion = Util.DataServiceVersion1;
				MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(e);
				member = memberExpression;
				while (memberExpression != null)
				{
					PropertyInfo propertyInfo;
					Expression expression;
					if (ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(memberExpression, out propertyInfo, out expression))
					{
						if (propertyPath == null)
						{
							propertyPath = new List<string>();
						}
						propertyPath.Insert(0, propertyInfo.Name);
						e = memberExpression.Expression;
						Type type;
						memberExpression = ResourceBinder.StripTo<MemberExpression>(e, out type);
						if (type != null)
						{
							propertyPath.Insert(0, UriHelper.GetEntityTypeNameForUriAndValidateMaxProtocolVersion(type, context, ref uriVersion));
						}
					}
					else
					{
						memberExpression = null;
					}
				}
				if (propertyPath != null)
				{
					instance = e;
					return true;
				}
				return false;
			}

			// Token: 0x060006A9 RID: 1705 RVA: 0x0001B6C2 File Offset: 0x000198C2
			internal static bool MatchConstant(Expression e, out ConstantExpression constExpr)
			{
				constExpr = e as ConstantExpression;
				return constExpr != null;
			}

			// Token: 0x060006AA RID: 1706 RVA: 0x0001B6D4 File Offset: 0x000198D4
			internal static bool MatchAnd(Expression e)
			{
				BinaryExpression binaryExpression = e as BinaryExpression;
				return binaryExpression != null && (binaryExpression.NodeType == ExpressionType.And || binaryExpression.NodeType == ExpressionType.AndAlso);
			}

			// Token: 0x060006AB RID: 1707 RVA: 0x0001B704 File Offset: 0x00019904
			internal static bool MatchNonPrivateReadableProperty(Expression e, out PropertyInfo propInfo, out Expression target)
			{
				propInfo = null;
				target = null;
				MemberExpression memberExpression = e as MemberExpression;
				if (memberExpression == null)
				{
					return false;
				}
				if (PlatformHelper.IsProperty(memberExpression.Member))
				{
					PropertyInfo propertyInfo = (PropertyInfo)memberExpression.Member;
					if (propertyInfo.CanRead && !TypeSystem.IsPrivate(propertyInfo))
					{
						propInfo = propertyInfo;
						target = memberExpression.Expression;
						return true;
					}
				}
				return false;
			}

			// Token: 0x060006AC RID: 1708 RVA: 0x0001B75C File Offset: 0x0001995C
			internal static bool MatchKeyProperty(Expression expression, out PropertyInfo property)
			{
				property = null;
				PropertyInfo propertyInfo;
				Expression expression2;
				if (!ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(expression, out propertyInfo, out expression2))
				{
					return false;
				}
				Type reflectedType = propertyInfo.ReflectedType;
				if ((ClientTypeUtil.GetKeyPropertiesOnType(reflectedType) ?? ClientTypeUtil.EmptyPropertyInfoArray).Contains(propertyInfo, ResourceBinder.PropertyInfoEqualityComparer.Instance) && expression2 is InputReferenceExpression)
				{
					property = propertyInfo;
					return true;
				}
				return false;
			}

			// Token: 0x060006AD RID: 1709 RVA: 0x0001B7AC File Offset: 0x000199AC
			internal static bool MatchKeyComparison(Expression e, out PropertyInfo keyProperty, out ConstantExpression keyValue)
			{
				if (ResourceBinder.PatternRules.MatchBinaryEquality(e))
				{
					BinaryExpression binaryExpression = (BinaryExpression)e;
					if ((ResourceBinder.PatternRules.MatchKeyProperty(binaryExpression.Left, out keyProperty) && ResourceBinder.PatternRules.MatchConstant(binaryExpression.Right, out keyValue)) || (ResourceBinder.PatternRules.MatchKeyProperty(binaryExpression.Right, out keyProperty) && ResourceBinder.PatternRules.MatchConstant(binaryExpression.Left, out keyValue)))
					{
						return keyValue.Value != null;
					}
				}
				keyProperty = null;
				keyValue = null;
				return false;
			}

			// Token: 0x060006AE RID: 1710 RVA: 0x0001B818 File Offset: 0x00019A18
			internal static bool MatchReferenceEquals(Expression expression)
			{
				MethodCallExpression methodCallExpression = expression as MethodCallExpression;
				return methodCallExpression != null && methodCallExpression.Method == typeof(object).GetMethod("ReferenceEquals");
			}

			// Token: 0x060006AF RID: 1711 RVA: 0x0001B850 File Offset: 0x00019A50
			internal static bool MatchResource(Expression expression, out ResourceExpression resource)
			{
				resource = expression as ResourceExpression;
				return resource != null;
			}

			// Token: 0x060006B0 RID: 1712 RVA: 0x0001B862 File Offset: 0x00019A62
			internal static bool MatchDoubleArgumentLambda(Expression expression, out LambdaExpression lambda)
			{
				return ResourceBinder.PatternRules.MatchNaryLambda(expression, 2, out lambda);
			}

			// Token: 0x060006B1 RID: 1713 RVA: 0x0001B86C File Offset: 0x00019A6C
			internal static bool MatchIdentitySelector(LambdaExpression lambda)
			{
				ParameterExpression parameterExpression = lambda.Parameters[0];
				return parameterExpression == ResourceBinder.StripTo<ParameterExpression>(lambda.Body);
			}

			// Token: 0x060006B2 RID: 1714 RVA: 0x0001B894 File Offset: 0x00019A94
			internal static bool MatchSingleArgumentLambda(Expression expression, out LambdaExpression lambda)
			{
				return ResourceBinder.PatternRules.MatchNaryLambda(expression, 1, out lambda);
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x0001B8A0 File Offset: 0x00019AA0
			internal static bool MatchTransparentIdentitySelector(Expression input, LambdaExpression selector, DataServiceContext context)
			{
				if (selector.Parameters.Count != 1)
				{
					return false;
				}
				ResourceSetExpression resourceSetExpression = input as ResourceSetExpression;
				if (resourceSetExpression == null || resourceSetExpression.TransparentScope == null)
				{
					return false;
				}
				Expression body = selector.Body;
				ParameterExpression parameterExpression = selector.Parameters[0];
				MemberExpression memberExpression;
				Expression expression;
				List<string> list;
				Version version;
				return ResourceBinder.PatternRules.MatchPropertyAccess(body, context, out memberExpression, out expression, out list, out version) && (expression == parameterExpression && list.Count == 1) && list[0] == resourceSetExpression.TransparentScope.Accessor;
			}

			// Token: 0x060006B4 RID: 1716 RVA: 0x0001B924 File Offset: 0x00019B24
			internal static bool MatchIdentityProjectionResultSelector(Expression e)
			{
				LambdaExpression lambdaExpression = (LambdaExpression)e;
				return lambdaExpression.Body == lambdaExpression.Parameters[1];
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x0001B968 File Offset: 0x00019B68
			internal static bool MatchTransparentScopeSelector(ResourceSetExpression input, LambdaExpression resultSelector, out ResourceSetExpression.TransparentAccessors transparentScope)
			{
				transparentScope = null;
				if (resultSelector.Body.NodeType != ExpressionType.New)
				{
					return false;
				}
				NewExpression newExpression = (NewExpression)resultSelector.Body;
				if (newExpression.Arguments.Count < 2)
				{
					return false;
				}
				if (newExpression.Type.GetBaseType() != typeof(object))
				{
					return false;
				}
				ParameterInfo[] parameters = newExpression.Constructor.GetParameters();
				if (newExpression.Members.Count != parameters.Length)
				{
					return false;
				}
				ResourceSetExpression resourceSetExpression = input.Source as ResourceSetExpression;
				int num = -1;
				ParameterExpression parameterExpression = resultSelector.Parameters[0];
				ParameterExpression parameterExpression2 = resultSelector.Parameters[1];
				MemberInfo[] array = new MemberInfo[newExpression.Members.Count];
				IEnumerable<PropertyInfo> publicProperties = newExpression.Type.GetPublicProperties(true);
				Dictionary<string, Expression> dictionary = new Dictionary<string, Expression>(parameters.Length - 1, StringComparer.Ordinal);
				for (int i = 0; i < newExpression.Arguments.Count; i++)
				{
					Expression expression = newExpression.Arguments[i];
					MemberInfo member = newExpression.Members[i];
					if (!ResourceBinder.PatternRules.ExpressionIsSimpleAccess(expression, resultSelector.Parameters))
					{
						return false;
					}
					if (PlatformHelper.IsMethod(member))
					{
						member = publicProperties.Where((PropertyInfo property) => PlatformHelper.AreMembersEqual(member, property.GetGetMethod())).FirstOrDefault<PropertyInfo>();
						if (member == null)
						{
							return false;
						}
					}
					if (member.Name != parameters[i].Name)
					{
						return false;
					}
					array[i] = member;
					ParameterExpression parameterExpression3 = ResourceBinder.StripTo<ParameterExpression>(expression);
					if (parameterExpression2 == parameterExpression3)
					{
						if (num != -1)
						{
							return false;
						}
						num = i;
					}
					else if (parameterExpression == parameterExpression3)
					{
						dictionary[member.Name] = resourceSetExpression.CreateReference();
					}
					else
					{
						List<ResourceExpression> list = new List<ResourceExpression>();
						InputBinder.Bind(expression, resourceSetExpression, resultSelector.Parameters[0], list);
						if (list.Count != 1)
						{
							return false;
						}
						dictionary[member.Name] = list[0].CreateReference();
					}
				}
				if (num == -1)
				{
					return false;
				}
				string name = array[num].Name;
				transparentScope = new ResourceSetExpression.TransparentAccessors(name, dictionary);
				return true;
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x0001BBAD File Offset: 0x00019DAD
			internal static bool MatchPropertyProjectionRelatedSet(ResourceExpression input, Expression potentialPropertyRef, DataServiceContext context, out MemberExpression setNavigationMember)
			{
				return ResourceBinder.PatternRules.MatchPropertyProjection(input, potentialPropertyRef, true, context, out setNavigationMember);
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x0001BBB9 File Offset: 0x00019DB9
			internal static bool MatchPropertyProjectionSingleton(ResourceExpression input, Expression potentialPropertyRef, DataServiceContext context, out MemberExpression propertyMember)
			{
				return ResourceBinder.PatternRules.MatchPropertyProjection(input, potentialPropertyRef, false, context, out propertyMember);
			}

			// Token: 0x060006B8 RID: 1720 RVA: 0x0001BBC8 File Offset: 0x00019DC8
			private static bool MatchPropertyProjection(ResourceExpression input, Expression potentialPropertyRef, bool matchSetNavigationProperty, DataServiceContext context, out MemberExpression propertyMember)
			{
				Expression operand;
				List<string> list;
				Version version;
				if (ResourceBinder.PatternRules.MatchPropertyAccess(potentialPropertyRef, context, out propertyMember, out operand, out list, out version))
				{
					UnaryExpression unaryExpression = operand as UnaryExpression;
					if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.TypeAs)
					{
						operand = unaryExpression.Operand;
					}
					if (operand == input.CreateReference() && ResourceBinder.PatternRules.MatchSetNavigationProperty(propertyMember, context.Model) == matchSetNavigationProperty)
					{
						return true;
					}
				}
				propertyMember = null;
				return false;
			}

			// Token: 0x060006B9 RID: 1721 RVA: 0x0001BC24 File Offset: 0x00019E24
			internal static bool MatchMemberInitExpressionWithDefaultConstructor(Expression source, LambdaExpression e)
			{
				MemberInitExpression memberInitExpression = ResourceBinder.StripTo<MemberInitExpression>(e.Body);
				ResourceExpression resourceExpression;
				return ResourceBinder.PatternRules.MatchResource(source, out resourceExpression) && memberInitExpression != null && memberInitExpression.NewExpression.Arguments.Count == 0;
			}

			// Token: 0x060006BA RID: 1722 RVA: 0x0001BC60 File Offset: 0x00019E60
			internal static bool MatchNewExpression(Expression source, LambdaExpression e)
			{
				ResourceExpression resourceExpression;
				return ResourceBinder.PatternRules.MatchResource(source, out resourceExpression) && e.Body is NewExpression;
			}

			// Token: 0x060006BB RID: 1723 RVA: 0x0001BC87 File Offset: 0x00019E87
			internal static bool MatchNot(Expression expression)
			{
				return expression.NodeType == ExpressionType.Not;
			}

			// Token: 0x060006BC RID: 1724 RVA: 0x0001BC94 File Offset: 0x00019E94
			internal static bool MatchSetNavigationProperty(Expression e, ClientEdmModel model)
			{
				return TypeSystem.FindIEnumerable(e.Type) != null && e.Type != typeof(char[]) && e.Type != typeof(byte[]) && !WebUtil.IsCLRTypeCollection(e.Type, model);
			}

			// Token: 0x060006BD RID: 1725 RVA: 0x0001BCF4 File Offset: 0x00019EF4
			internal static ResourceBinder.PatternRules.MatchNullCheckResult MatchNullCheck(Expression entityInScope, ConditionalExpression conditional)
			{
				ResourceBinder.PatternRules.MatchNullCheckResult matchNullCheckResult = default(ResourceBinder.PatternRules.MatchNullCheckResult);
				ResourceBinder.PatternRules.MatchEqualityCheckResult matchEqualityCheckResult = ResourceBinder.PatternRules.MatchEquality(conditional.Test);
				if (!matchEqualityCheckResult.Match)
				{
					return matchNullCheckResult;
				}
				Expression expression;
				if (matchEqualityCheckResult.EqualityYieldsTrue)
				{
					if (!ResourceBinder.PatternRules.MatchNullConstant(conditional.IfTrue))
					{
						return matchNullCheckResult;
					}
					expression = conditional.IfFalse;
				}
				else
				{
					if (!ResourceBinder.PatternRules.MatchNullConstant(conditional.IfFalse))
					{
						return matchNullCheckResult;
					}
					expression = conditional.IfTrue;
				}
				Expression expression2;
				if (ResourceBinder.PatternRules.MatchNullConstant(matchEqualityCheckResult.TestLeft))
				{
					expression2 = matchEqualityCheckResult.TestRight;
				}
				else
				{
					if (!ResourceBinder.PatternRules.MatchNullConstant(matchEqualityCheckResult.TestRight))
					{
						return matchNullCheckResult;
					}
					expression2 = matchEqualityCheckResult.TestLeft;
				}
				MemberAssignmentAnalysis memberAssignmentAnalysis = MemberAssignmentAnalysis.Analyze(entityInScope, expression);
				if (memberAssignmentAnalysis.MultiplePathsFound)
				{
					return matchNullCheckResult;
				}
				MemberAssignmentAnalysis memberAssignmentAnalysis2 = MemberAssignmentAnalysis.Analyze(entityInScope, expression2);
				if (memberAssignmentAnalysis2.MultiplePathsFound)
				{
					return matchNullCheckResult;
				}
				Expression[] expressionsToTargetEntity = memberAssignmentAnalysis.GetExpressionsToTargetEntity();
				Expression[] expressionsToTargetEntity2 = memberAssignmentAnalysis2.GetExpressionsToTargetEntity();
				if (expressionsToTargetEntity2.Length > expressionsToTargetEntity.Length)
				{
					return matchNullCheckResult;
				}
				for (int i = 0; i < expressionsToTargetEntity2.Length; i++)
				{
					Expression expression3 = expressionsToTargetEntity[i];
					Expression expression4 = expressionsToTargetEntity2[i];
					if (expression3 != expression4)
					{
						if (expression3.NodeType != expression4.NodeType || expression3.NodeType != ExpressionType.MemberAccess)
						{
							return matchNullCheckResult;
						}
						if (((MemberExpression)expression3).Member != ((MemberExpression)expression4).Member)
						{
							return matchNullCheckResult;
						}
					}
				}
				matchNullCheckResult.AssignExpression = expression;
				matchNullCheckResult.Match = true;
				matchNullCheckResult.TestToNullExpression = expression2;
				return matchNullCheckResult;
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x0001BE50 File Offset: 0x0001A050
			internal static bool MatchNullConstant(Expression expression)
			{
				ConstantExpression constantExpression = expression as ConstantExpression;
				return constantExpression != null && constantExpression.Value == null;
			}

			// Token: 0x060006BF RID: 1727 RVA: 0x0001BE72 File Offset: 0x0001A072
			internal static bool MatchBinaryExpression(Expression e)
			{
				return e is BinaryExpression;
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x0001BE7D File Offset: 0x0001A07D
			internal static bool MatchBinaryEquality(Expression e)
			{
				return ResourceBinder.PatternRules.MatchBinaryExpression(e) && ((BinaryExpression)e).NodeType == ExpressionType.Equal;
			}

			// Token: 0x060006C1 RID: 1729 RVA: 0x0001BE98 File Offset: 0x0001A098
			internal static bool MatchStringAddition(Expression e)
			{
				if (e.NodeType == ExpressionType.Add)
				{
					BinaryExpression binaryExpression = e as BinaryExpression;
					return binaryExpression != null && binaryExpression.Left.Type == typeof(string) && binaryExpression.Right.Type == typeof(string);
				}
				return false;
			}

			// Token: 0x060006C2 RID: 1730 RVA: 0x0001BEF1 File Offset: 0x0001A0F1
			internal static bool MatchNewDataServiceCollectionOfT(NewExpression nex)
			{
				return nex.Type.IsGenericType() && WebUtil.IsDataServiceCollectionType(nex.Type.GetGenericTypeDefinition());
			}

			// Token: 0x060006C3 RID: 1731 RVA: 0x0001BF2C File Offset: 0x0001A12C
			internal static bool MatchNewCollectionOfT(NewExpression nex)
			{
				Type type = nex.Type;
				return type.GetInterfaces().Any((Type t) => t.GetGenericTypeDefinition() == typeof(ICollection<>));
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x0001BF68 File Offset: 0x0001A168
			internal static ResourceBinder.PatternRules.MatchEqualityCheckResult MatchEquality(Expression expression)
			{
				ResourceBinder.PatternRules.MatchEqualityCheckResult matchEqualityCheckResult = default(ResourceBinder.PatternRules.MatchEqualityCheckResult);
				matchEqualityCheckResult.Match = false;
				matchEqualityCheckResult.EqualityYieldsTrue = true;
				while (!ResourceBinder.PatternRules.MatchReferenceEquals(expression))
				{
					if (!ResourceBinder.PatternRules.MatchNot(expression))
					{
						BinaryExpression binaryExpression = expression as BinaryExpression;
						if (binaryExpression != null)
						{
							if (binaryExpression.NodeType == ExpressionType.NotEqual)
							{
								matchEqualityCheckResult.EqualityYieldsTrue = !matchEqualityCheckResult.EqualityYieldsTrue;
							}
							else if (binaryExpression.NodeType != ExpressionType.Equal)
							{
								return matchEqualityCheckResult;
							}
							matchEqualityCheckResult.TestLeft = binaryExpression.Left;
							matchEqualityCheckResult.TestRight = binaryExpression.Right;
							matchEqualityCheckResult.Match = true;
						}
						return matchEqualityCheckResult;
					}
					matchEqualityCheckResult.EqualityYieldsTrue = !matchEqualityCheckResult.EqualityYieldsTrue;
					expression = ((UnaryExpression)expression).Operand;
				}
				MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
				matchEqualityCheckResult.Match = true;
				matchEqualityCheckResult.TestLeft = methodCallExpression.Arguments[0];
				matchEqualityCheckResult.TestRight = methodCallExpression.Arguments[1];
				return matchEqualityCheckResult;
			}

			// Token: 0x060006C5 RID: 1733 RVA: 0x0001C048 File Offset: 0x0001A248
			private static bool ExpressionIsSimpleAccess(Expression argument, ReadOnlyCollection<ParameterExpression> expressions)
			{
				Expression expression = argument;
				MemberExpression memberExpression;
				do
				{
					memberExpression = expression as MemberExpression;
					if (memberExpression != null)
					{
						expression = memberExpression.Expression;
					}
				}
				while (memberExpression != null);
				ParameterExpression parameterExpression = expression as ParameterExpression;
				return parameterExpression != null && expressions.Contains(parameterExpression);
			}

			// Token: 0x060006C6 RID: 1734 RVA: 0x0001C080 File Offset: 0x0001A280
			private static bool MatchNaryLambda(Expression expression, int parameterCount, out LambdaExpression lambda)
			{
				lambda = null;
				LambdaExpression lambdaExpression = ResourceBinder.StripTo<LambdaExpression>(expression);
				if (lambdaExpression != null && lambdaExpression.Parameters.Count == parameterCount)
				{
					lambda = lambdaExpression;
				}
				return lambda != null;
			}

			// Token: 0x020000CF RID: 207
			internal struct MatchNullCheckResult
			{
				// Token: 0x0400041D RID: 1053
				internal Expression AssignExpression;

				// Token: 0x0400041E RID: 1054
				internal bool Match;

				// Token: 0x0400041F RID: 1055
				internal Expression TestToNullExpression;
			}

			// Token: 0x020000D0 RID: 208
			internal struct MatchEqualityCheckResult
			{
				// Token: 0x04000420 RID: 1056
				internal bool EqualityYieldsTrue;

				// Token: 0x04000421 RID: 1057
				internal bool Match;

				// Token: 0x04000422 RID: 1058
				internal Expression TestLeft;

				// Token: 0x04000423 RID: 1059
				internal Expression TestRight;
			}
		}

		// Token: 0x020000D1 RID: 209
		internal static class ValidationRules
		{
			// Token: 0x060006C8 RID: 1736 RVA: 0x0001C0B4 File Offset: 0x0001A2B4
			internal static void RequireCanNavigate(Expression e)
			{
				ResourceSetExpression resourceSetExpression = e as ResourceSetExpression;
				if (resourceSetExpression != null && resourceSetExpression.HasSequenceQueryOptions)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionsOnlyAllowedOnLeafNodes);
				}
				ResourceExpression resourceExpression;
				if (ResourceBinder.PatternRules.MatchResource(e, out resourceExpression) && resourceExpression.Projection != null)
				{
					throw new NotSupportedException(Strings.ALinq_ProjectionOnlyAllowedOnLeafNodes);
				}
			}

			// Token: 0x060006C9 RID: 1737 RVA: 0x0001C0FC File Offset: 0x0001A2FC
			internal static void RequireCanProject(Expression e)
			{
				ResourceExpression resourceExpression = (ResourceExpression)e;
				if (!ResourceBinder.PatternRules.MatchResource(e, out resourceExpression))
				{
					throw new NotSupportedException(Strings.ALinq_CanOnlyProjectTheLeaf);
				}
				if (resourceExpression.Projection != null)
				{
					throw new NotSupportedException(Strings.ALinq_ProjectionCanOnlyHaveOneProjection);
				}
				if (resourceExpression.ExpandPaths.Count > 0)
				{
					throw new NotSupportedException(Strings.ALinq_CannotProjectWithExplicitExpansion);
				}
			}

			// Token: 0x060006CA RID: 1738 RVA: 0x0001C154 File Offset: 0x0001A354
			internal static void RequireCanExpand(Expression e)
			{
				ResourceExpression resourceExpression = (ResourceExpression)e;
				if (!ResourceBinder.PatternRules.MatchResource(e, out resourceExpression))
				{
					throw new NotSupportedException(Strings.ALinq_CantExpand);
				}
				if (resourceExpression.Projection != null)
				{
					throw new NotSupportedException(Strings.ALinq_CannotProjectWithExplicitExpansion);
				}
			}

			// Token: 0x060006CB RID: 1739 RVA: 0x0001C190 File Offset: 0x0001A390
			internal static void RequireCanAddCount(Expression e)
			{
				ResourceExpression resourceExpression = (ResourceExpression)e;
				if (!ResourceBinder.PatternRules.MatchResource(e, out resourceExpression))
				{
					throw new NotSupportedException(Strings.ALinq_CannotAddCountOption);
				}
				if (resourceExpression.CountOption != CountOption.None)
				{
					throw new NotSupportedException(Strings.ALinq_CannotAddCountOptionConflict);
				}
			}

			// Token: 0x060006CC RID: 1740 RVA: 0x0001C1CC File Offset: 0x0001A3CC
			internal static void RequireCanAddCustomQueryOption(Expression e)
			{
				ResourceExpression resourceExpression = (ResourceExpression)e;
				if (!ResourceBinder.PatternRules.MatchResource(e, out resourceExpression))
				{
					throw new NotSupportedException(Strings.ALinq_CantAddQueryOption);
				}
			}

			// Token: 0x060006CD RID: 1741 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
			internal static void RequireNonSingleton(Expression e)
			{
				ResourceExpression resourceExpression = e as ResourceExpression;
				if (resourceExpression != null && resourceExpression.IsSingleton)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionsOnlyAllowedOnSingletons);
				}
			}

			// Token: 0x060006CE RID: 1742 RVA: 0x0001C248 File Offset: 0x0001A448
			internal static void RequireLegalCustomQueryOption(Expression e, ResourceExpression target)
			{
				string name = ((string)(e as ConstantExpression).Value).Trim();
				if (name[0] == '$')
				{
					if (target.CustomQueryOptions.Any((KeyValuePair<ConstantExpression, ConstantExpression> c) => (string)c.Key.Value == name))
					{
						throw new NotSupportedException(Strings.ALinq_CantAddDuplicateQueryOption(name));
					}
					ResourceSetExpression resourceSetExpression = target as ResourceSetExpression;
					if (resourceSetExpression != null)
					{
						string text;
						switch (text = name.Substring(1))
						{
						case "filter":
							if (resourceSetExpression.Filter != null)
							{
								throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
							}
							return;
						case "orderby":
							if (resourceSetExpression.OrderBy != null)
							{
								throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
							}
							return;
						case "expand":
							return;
						case "skip":
							if (resourceSetExpression.Skip != null)
							{
								throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
							}
							return;
						case "top":
							if (resourceSetExpression.Take != null)
							{
								throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
							}
							return;
						case "inlinecount":
							if (resourceSetExpression.CountOption != CountOption.None)
							{
								throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
							}
							return;
						case "select":
							if (resourceSetExpression.Projection != null)
							{
								throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
							}
							return;
						case "format":
							ResourceBinder.ValidationRules.ThrowNotSupportedExceptionForTheFormatOption();
							return;
						}
						throw new NotSupportedException(Strings.ALinq_CantAddQueryOptionStartingWithDollarSign(name));
					}
				}
			}

			// Token: 0x060006CF RID: 1743 RVA: 0x0001C443 File Offset: 0x0001A643
			private static void ThrowNotSupportedExceptionForTheFormatOption()
			{
				throw new NotSupportedException(Strings.ALinq_FormatQueryOptionNotSupported);
			}

			// Token: 0x060006D0 RID: 1744 RVA: 0x0001C450 File Offset: 0x0001A650
			internal static void CheckPredicate(Expression e, ClientEdmModel model)
			{
				ResourceBinder.ValidationRules.WhereAndOrderByChecker whereAndOrderByChecker = new ResourceBinder.ValidationRules.WhereAndOrderByChecker(model, SequenceMethod.Where);
				whereAndOrderByChecker.Visit(e);
			}

			// Token: 0x060006D1 RID: 1745 RVA: 0x0001C470 File Offset: 0x0001A670
			internal static void CheckOrderBy(Expression e, ClientEdmModel model)
			{
				ResourceBinder.ValidationRules.WhereAndOrderByChecker whereAndOrderByChecker = new ResourceBinder.ValidationRules.WhereAndOrderByChecker(model, SequenceMethod.OrderBy);
				whereAndOrderByChecker.Visit(e);
			}

			// Token: 0x060006D2 RID: 1746 RVA: 0x0001C490 File Offset: 0x0001A690
			internal static void DisallowMemberAccessInNavigation(Expression e, ClientEdmModel model)
			{
				for (MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(e); memberExpression != null; memberExpression = ResourceBinder.StripTo<MemberExpression>(memberExpression.Expression))
				{
					if (WebUtil.IsCLRTypeCollection(memberExpression.Expression.Type, model))
					{
						throw new NotSupportedException(Strings.ALinq_CollectionMemberAccessNotSupportedInNavigation(memberExpression.Member.Name));
					}
				}
			}

			// Token: 0x060006D3 RID: 1747 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
			internal static void DisallowExpressionEndWithTypeAs(Expression exp, string method)
			{
				Expression expression = ResourceBinder.StripTo<UnaryExpression>(exp);
				if (expression != null && expression.NodeType == ExpressionType.TypeAs)
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionCannotEndWithTypeAs(exp.ToString(), method));
				}
			}

			// Token: 0x060006D4 RID: 1748 RVA: 0x0001C514 File Offset: 0x0001A714
			internal static void ValidateExpandPath(Expression input, DataServiceContext context, out string expandPath, out Version uriVersion)
			{
				expandPath = null;
				uriVersion = Util.DataServiceVersion1;
				LambdaExpression lambdaExpression;
				if (ResourceBinder.PatternRules.MatchSingleArgumentLambda(input, out lambdaExpression))
				{
					MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(lambdaExpression.Body);
					MemberExpression memberExpression2;
					Expression operand;
					List<string> list;
					if (memberExpression != null && ResourceBinder.PatternRules.MatchPropertyAccess(memberExpression, context, out memberExpression2, out operand, out list, out uriVersion))
					{
						UnaryExpression unaryExpression = operand as UnaryExpression;
						if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.TypeAs)
						{
							operand = unaryExpression.Operand;
						}
						Type reflectedType = memberExpression2.Member.ReflectedType;
						if (operand == lambdaExpression.Parameters[0] && ClientTypeUtil.TypeOrElementTypeIsEntity(reflectedType))
						{
							expandPath = string.Join('/'.ToString(), list);
							return;
						}
					}
				}
				throw new NotSupportedException(Strings.ALinq_InvalidExpressionInNavigationPath(input));
			}

			// Token: 0x020000D2 RID: 210
			internal class WhereAndOrderByChecker : DataServiceALinqExpressionVisitor
			{
				// Token: 0x060006D5 RID: 1749 RVA: 0x0001C5BA File Offset: 0x0001A7BA
				internal WhereAndOrderByChecker(ClientEdmModel model, SequenceMethod checkedMethod)
				{
					this.model = model;
					this.checkedMethod = checkedMethod;
				}

				// Token: 0x060006D6 RID: 1750 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
				internal override Expression VisitMethodCall(MethodCallExpression mce)
				{
					SequenceMethod sequenceMethod;
					if (!ReflectionUtil.TryIdentifySequenceMethod(mce.Method, out sequenceMethod) || !ReflectionUtil.IsAnyAllMethod(sequenceMethod))
					{
						return base.VisitMethodCall(mce);
					}
					if (this.checkedMethod == SequenceMethod.OrderBy)
					{
						throw new NotSupportedException(Strings.ALinq_AnyAllNotSupportedInOrderBy(mce.Method.Name));
					}
					Type type = mce.Method.GetGenericArguments().SingleOrDefault<Type>();
					if (!ClientTypeUtil.TypeOrElementTypeIsEntity(type))
					{
						Expression expression = mce.Arguments[0];
						MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(expression);
						PropertyInfo propertyInfo;
						Expression expression2;
						if (memberExpression == null || !ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(memberExpression, out propertyInfo, out expression2) || !WebUtil.IsCLRTypeCollection(propertyInfo.PropertyType, this.model))
						{
							throw new NotSupportedException(Strings.ALinq_InvalidSourceForAnyAll(mce.Method.Name));
						}
					}
					if (mce.Arguments.Count == 2)
					{
						base.Visit(mce.Arguments[1]);
					}
					return mce;
				}

				// Token: 0x060006D7 RID: 1751 RVA: 0x0001C6AC File Offset: 0x0001A8AC
				internal override Expression VisitMemberAccess(MemberExpression m)
				{
					if (PlatformHelper.IsProperty(m.Member))
					{
						PropertyInfo propertyInfo = (PropertyInfo)m.Member;
						if (WebUtil.IsCLRTypeCollection(propertyInfo.PropertyType, this.model))
						{
							if (this.checkedMethod == SequenceMethod.Where)
							{
								throw new NotSupportedException(Strings.ALinq_CollectionPropertyNotSupportedInWhere(propertyInfo.Name));
							}
							throw new NotSupportedException(Strings.ALinq_CollectionPropertyNotSupportedInOrderBy(propertyInfo.Name));
						}
						else if (typeof(DataServiceStreamLink).IsAssignableFrom(propertyInfo.PropertyType))
						{
							throw new NotSupportedException(Strings.ALinq_LinkPropertyNotSupportedInExpression(propertyInfo.Name));
						}
					}
					return base.VisitMemberAccess(m);
				}

				// Token: 0x04000424 RID: 1060
				private readonly SequenceMethod checkedMethod;

				// Token: 0x04000425 RID: 1061
				private readonly ClientEdmModel model;
			}
		}

		// Token: 0x020000D3 RID: 211
		private sealed class PropertyInfoEqualityComparer : IEqualityComparer<PropertyInfo>
		{
			// Token: 0x060006D8 RID: 1752 RVA: 0x0001C73E File Offset: 0x0001A93E
			private PropertyInfoEqualityComparer()
			{
			}

			// Token: 0x060006D9 RID: 1753 RVA: 0x0001C748 File Offset: 0x0001A948
			public bool Equals(PropertyInfo left, PropertyInfo right)
			{
				return object.ReferenceEquals(left, right) || (!(null == left) && !(null == right) && object.ReferenceEquals(left.DeclaringType, right.DeclaringType) && left.Name.Equals(right.Name));
			}

			// Token: 0x060006DA RID: 1754 RVA: 0x0001C79A File Offset: 0x0001A99A
			public int GetHashCode(PropertyInfo obj)
			{
				if (!(null != obj))
				{
					return 0;
				}
				return obj.GetHashCode();
			}

			// Token: 0x04000426 RID: 1062
			internal static readonly ResourceBinder.PropertyInfoEqualityComparer Instance = new ResourceBinder.PropertyInfoEqualityComparer();
		}

		// Token: 0x020000D4 RID: 212
		private sealed class ExpressionPresenceVisitor : DataServiceALinqExpressionVisitor
		{
			// Token: 0x060006DC RID: 1756 RVA: 0x0001C7B9 File Offset: 0x0001A9B9
			private ExpressionPresenceVisitor(Expression target)
			{
				this.target = target;
			}

			// Token: 0x060006DD RID: 1757 RVA: 0x0001C7C8 File Offset: 0x0001A9C8
			internal static bool IsExpressionPresent(Expression target, Expression tree)
			{
				ResourceBinder.ExpressionPresenceVisitor expressionPresenceVisitor = new ResourceBinder.ExpressionPresenceVisitor(target);
				expressionPresenceVisitor.Visit(tree);
				return expressionPresenceVisitor.found;
			}

			// Token: 0x060006DE RID: 1758 RVA: 0x0001C7EC File Offset: 0x0001A9EC
			internal override Expression Visit(Expression exp)
			{
				Expression expression;
				if (this.found || object.ReferenceEquals(this.target, exp))
				{
					this.found = true;
					expression = exp;
				}
				else
				{
					expression = base.Visit(exp);
				}
				return expression;
			}

			// Token: 0x04000427 RID: 1063
			private readonly Expression target;

			// Token: 0x04000428 RID: 1064
			private bool found;
		}
	}
}

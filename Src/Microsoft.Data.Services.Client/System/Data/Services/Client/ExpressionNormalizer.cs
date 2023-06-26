using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Services.Client
{
	// Token: 0x020000C5 RID: 197
	internal class ExpressionNormalizer : DataServiceALinqExpressionVisitor
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x00018BE9 File Offset: 0x00016DE9
		private ExpressionNormalizer(Dictionary<Expression, Expression> normalizerRewrites)
		{
			this.normalizerRewrites = normalizerRewrites;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00018C08 File Offset: 0x00016E08
		internal Dictionary<Expression, Expression> NormalizerRewrites
		{
			get
			{
				return this.normalizerRewrites;
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00018C10 File Offset: 0x00016E10
		internal static Expression Normalize(Expression expression, Dictionary<Expression, Expression> rewrites)
		{
			ExpressionNormalizer expressionNormalizer = new ExpressionNormalizer(rewrites);
			return expressionNormalizer.Visit(expression);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00018C30 File Offset: 0x00016E30
		internal override Expression VisitBinary(BinaryExpression b)
		{
			BinaryExpression binaryExpression = (BinaryExpression)base.VisitBinary(b);
			if (binaryExpression.NodeType == ExpressionType.Equal)
			{
				Expression expression = ExpressionNormalizer.UnwrapObjectConvert(binaryExpression.Left);
				Expression expression2 = ExpressionNormalizer.UnwrapObjectConvert(binaryExpression.Right);
				if (expression != binaryExpression.Left || expression2 != binaryExpression.Right)
				{
					binaryExpression = ExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, expression, expression2);
				}
			}
			ExpressionNormalizer.Pattern pattern;
			if (this._patterns.TryGetValue(binaryExpression.Left, out pattern) && pattern.Kind == ExpressionNormalizer.PatternKind.Compare && ExpressionNormalizer.IsConstantZero(binaryExpression.Right))
			{
				ExpressionNormalizer.ComparePattern comparePattern = (ExpressionNormalizer.ComparePattern)pattern;
				BinaryExpression binaryExpression2;
				if (ExpressionNormalizer.TryCreateRelationalOperator(binaryExpression.NodeType, comparePattern.Left, comparePattern.Right, out binaryExpression2))
				{
					binaryExpression = binaryExpression2;
				}
			}
			this.RecordRewrite(b, binaryExpression);
			return binaryExpression;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00018CE4 File Offset: 0x00016EE4
		internal override Expression VisitUnary(UnaryExpression u)
		{
			UnaryExpression unaryExpression = (UnaryExpression)base.VisitUnary(u);
			Expression expression = unaryExpression;
			this.RecordRewrite(u, expression);
			if ((unaryExpression.NodeType == ExpressionType.Convert || unaryExpression.NodeType == ExpressionType.TypeAs) && unaryExpression.Type.IsAssignableFrom(unaryExpression.Operand.Type) && ((!PrimitiveType.IsKnownNullableType(unaryExpression.Operand.Type) && !PrimitiveType.IsKnownNullableType(unaryExpression.Type)) || unaryExpression.Operand.Type == unaryExpression.Type) && (!ClientTypeUtil.TypeOrElementTypeIsEntity(unaryExpression.Operand.Type) || !ProjectionAnalyzer.IsCollectionProducingExpression(unaryExpression.Operand)))
			{
				expression = unaryExpression.Operand;
			}
			return expression;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00018D94 File Offset: 0x00016F94
		private static Expression UnwrapObjectConvert(Expression input)
		{
			if (input.NodeType == ExpressionType.Constant && input.Type == typeof(object))
			{
				ConstantExpression constantExpression = (ConstantExpression)input;
				if (constantExpression.Value != null && constantExpression.Value.GetType() != typeof(object))
				{
					return Expression.Constant(constantExpression.Value, constantExpression.Value.GetType());
				}
			}
			while (ExpressionType.Convert == input.NodeType && typeof(object) == input.Type)
			{
				input = ((UnaryExpression)input).Operand;
			}
			return input;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00018E33 File Offset: 0x00017033
		private static bool IsConstantZero(Expression expression)
		{
			return expression.NodeType == ExpressionType.Constant && ((ConstantExpression)expression).Value.Equals(0);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00018E58 File Offset: 0x00017058
		internal override Expression VisitMethodCall(MethodCallExpression call)
		{
			Expression expression = this.VisitMethodCallNoRewrite(call);
			this.RecordRewrite(call, expression);
			return expression;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00018E78 File Offset: 0x00017078
		internal Expression VisitMethodCallNoRewrite(MethodCallExpression call)
		{
			MethodCallExpression methodCallExpression = (MethodCallExpression)base.VisitMethodCall(call);
			if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name.StartsWith("op_", StringComparison.Ordinal))
			{
				string name;
				if (methodCallExpression.Arguments.Count == 2 && (name = methodCallExpression.Method.Name) != null)
				{
					if (<PrivateImplementationDetails>{456BF077-23FE-4C39-B822-73951C943E75}.$$method0x6000621-1 == null)
					{
						<PrivateImplementationDetails>{456BF077-23FE-4C39-B822-73951C943E75}.$$method0x6000621-1 = new Dictionary<string, int>(14)
						{
							{ "op_Equality", 0 },
							{ "op_Inequality", 1 },
							{ "op_GreaterThan", 2 },
							{ "op_GreaterThanOrEqual", 3 },
							{ "op_LessThan", 4 },
							{ "op_LessThanOrEqual", 5 },
							{ "op_Multiply", 6 },
							{ "op_Subtraction", 7 },
							{ "op_Addition", 8 },
							{ "op_Division", 9 },
							{ "op_Modulus", 10 },
							{ "op_BitwiseAnd", 11 },
							{ "op_BitwiseOr", 12 },
							{ "op_ExclusiveOr", 13 }
						};
					}
					int num;
					if (<PrivateImplementationDetails>{456BF077-23FE-4C39-B822-73951C943E75}.$$method0x6000621-1.TryGetValue(name, out num))
					{
						switch (num)
						{
						case 0:
							return Expression.Equal(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
						case 1:
							return Expression.NotEqual(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
						case 2:
							return Expression.GreaterThan(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
						case 3:
							return Expression.GreaterThanOrEqual(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
						case 4:
							return Expression.LessThan(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
						case 5:
							return Expression.LessThanOrEqual(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
						case 6:
							return Expression.Multiply(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
						case 7:
							return Expression.Subtract(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
						case 8:
							return Expression.Add(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
						case 9:
							return Expression.Divide(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
						case 10:
							return Expression.Modulo(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
						case 11:
							return Expression.And(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
						case 12:
							return Expression.Or(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
						case 13:
							return Expression.ExclusiveOr(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
						}
					}
				}
				string name2;
				if (methodCallExpression.Arguments.Count == 1 && (name2 = methodCallExpression.Method.Name) != null)
				{
					if (<PrivateImplementationDetails>{456BF077-23FE-4C39-B822-73951C943E75}.$$method0x6000621-2 == null)
					{
						<PrivateImplementationDetails>{456BF077-23FE-4C39-B822-73951C943E75}.$$method0x6000621-2 = new Dictionary<string, int>(6)
						{
							{ "op_UnaryNegation", 0 },
							{ "op_UnaryPlus", 1 },
							{ "op_Explicit", 2 },
							{ "op_Implicit", 3 },
							{ "op_OnesComplement", 4 },
							{ "op_False", 5 }
						};
					}
					int num2;
					if (<PrivateImplementationDetails>{456BF077-23FE-4C39-B822-73951C943E75}.$$method0x6000621-2.TryGetValue(name2, out num2))
					{
						switch (num2)
						{
						case 0:
							return Expression.Negate(methodCallExpression.Arguments[0], methodCallExpression.Method);
						case 1:
							return Expression.UnaryPlus(methodCallExpression.Arguments[0], methodCallExpression.Method);
						case 2:
						case 3:
							return Expression.Convert(methodCallExpression.Arguments[0], methodCallExpression.Type, methodCallExpression.Method);
						case 4:
						case 5:
							return Expression.Not(methodCallExpression.Arguments[0], methodCallExpression.Method);
						}
					}
				}
			}
			if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name == "Equals" && methodCallExpression.Arguments.Count > 1)
			{
				return Expression.Equal(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
			}
			if (!methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name == "Equals" && methodCallExpression.Arguments.Count > 0)
			{
				return ExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, methodCallExpression.Object, methodCallExpression.Arguments[0]);
			}
			if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name == "CompareString" && methodCallExpression.Method.DeclaringType.FullName == "Microsoft.VisualBasic.CompilerServices.Operators")
			{
				return this.CreateCompareExpression(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1]);
			}
			if (!methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name == "CompareTo" && methodCallExpression.Arguments.Count == 1 && methodCallExpression.Method.ReturnType == typeof(int))
			{
				return this.CreateCompareExpression(methodCallExpression.Object, methodCallExpression.Arguments[0]);
			}
			if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name == "Compare" && methodCallExpression.Arguments.Count > 1 && methodCallExpression.Method.ReturnType == typeof(int))
			{
				return this.CreateCompareExpression(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1]);
			}
			MethodCallExpression methodCallExpression2 = ExpressionNormalizer.NormalizePredicateArgument(methodCallExpression);
			methodCallExpression2 = ExpressionNormalizer.NormalizeSelectWithTypeCast(methodCallExpression2);
			return ExpressionNormalizer.NormalizeEnumerableSource(methodCallExpression2);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00019508 File Offset: 0x00017708
		private static MethodCallExpression NormalizeEnumerableSource(MethodCallExpression callExpression)
		{
			MethodInfo method = callExpression.Method;
			SequenceMethod sequenceMethod;
			if (ReflectionUtil.TryIdentifySequenceMethod(callExpression.Method, out sequenceMethod) && (ReflectionUtil.IsAnyAllMethod(sequenceMethod) || sequenceMethod == SequenceMethod.OfType))
			{
				Expression expression = callExpression.Arguments[0];
				while (ExpressionType.Convert == expression.NodeType)
				{
					expression = ((UnaryExpression)expression).Operand;
				}
				if (expression != callExpression.Arguments[0])
				{
					if (sequenceMethod == SequenceMethod.Any || sequenceMethod == SequenceMethod.OfType)
					{
						return Expression.Call(method, expression);
					}
					return Expression.Call(method, expression, callExpression.Arguments[1]);
				}
			}
			return callExpression;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00019594 File Offset: 0x00017794
		private static MethodCallExpression NormalizePredicateArgument(MethodCallExpression callExpression)
		{
			int num;
			Expression expression;
			MethodCallExpression methodCallExpression;
			if (ExpressionNormalizer.HasPredicateArgument(callExpression, out num) && ExpressionNormalizer.TryMatchCoalescePattern(callExpression.Arguments[num], out expression))
			{
				List<Expression> list = new List<Expression>(callExpression.Arguments);
				list[num] = expression;
				methodCallExpression = Expression.Call(callExpression.Object, callExpression.Method, list);
			}
			else
			{
				methodCallExpression = callExpression;
			}
			return methodCallExpression;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000195EC File Offset: 0x000177EC
		private static bool HasPredicateArgument(MethodCallExpression callExpression, out int argumentOrdinal)
		{
			argumentOrdinal = 0;
			bool flag = false;
			SequenceMethod sequenceMethod;
			if (2 <= callExpression.Arguments.Count && ReflectionUtil.TryIdentifySequenceMethod(callExpression.Method, out sequenceMethod))
			{
				SequenceMethod sequenceMethod2 = sequenceMethod;
				if (sequenceMethod2 <= SequenceMethod.SkipWhileOrdinal)
				{
					switch (sequenceMethod2)
					{
					case SequenceMethod.Where:
					case SequenceMethod.WhereOrdinal:
						break;
					default:
						switch (sequenceMethod2)
						{
						case SequenceMethod.TakeWhile:
						case SequenceMethod.TakeWhileOrdinal:
						case SequenceMethod.SkipWhile:
						case SequenceMethod.SkipWhileOrdinal:
							break;
						case SequenceMethod.Skip:
							return flag;
						default:
							return flag;
						}
						break;
					}
				}
				else
				{
					switch (sequenceMethod2)
					{
					case SequenceMethod.FirstPredicate:
					case SequenceMethod.FirstOrDefaultPredicate:
					case SequenceMethod.LastPredicate:
					case SequenceMethod.LastOrDefaultPredicate:
					case SequenceMethod.SinglePredicate:
					case SequenceMethod.SingleOrDefaultPredicate:
						break;
					case SequenceMethod.FirstOrDefault:
					case SequenceMethod.Last:
					case SequenceMethod.LastOrDefault:
					case SequenceMethod.Single:
					case SequenceMethod.SingleOrDefault:
						return flag;
					default:
						switch (sequenceMethod2)
						{
						case SequenceMethod.AnyPredicate:
						case SequenceMethod.All:
						case SequenceMethod.CountPredicate:
						case SequenceMethod.LongCountPredicate:
							break;
						case SequenceMethod.Count:
						case SequenceMethod.LongCount:
							return flag;
						default:
							return flag;
						}
						break;
					}
				}
				argumentOrdinal = 1;
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000196B4 File Offset: 0x000178B4
		private static bool TryMatchCoalescePattern(Expression expression, out Expression normalized)
		{
			normalized = null;
			bool flag = false;
			if (expression.NodeType == ExpressionType.Quote)
			{
				UnaryExpression unaryExpression = (UnaryExpression)expression;
				if (ExpressionNormalizer.TryMatchCoalescePattern(unaryExpression.Operand, out normalized))
				{
					flag = true;
					normalized = Expression.Quote(normalized);
				}
			}
			else if (expression.NodeType == ExpressionType.Lambda)
			{
				LambdaExpression lambdaExpression = (LambdaExpression)expression;
				if (lambdaExpression.Body.NodeType == ExpressionType.Coalesce && lambdaExpression.Body.Type == typeof(bool))
				{
					BinaryExpression binaryExpression = (BinaryExpression)lambdaExpression.Body;
					if (binaryExpression.Right.NodeType == ExpressionType.Constant && false.Equals(((ConstantExpression)binaryExpression.Right).Value))
					{
						normalized = Expression.Lambda(lambdaExpression.Type, Expression.Convert(binaryExpression.Left, typeof(bool)), lambdaExpression.Parameters);
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00019798 File Offset: 0x00017998
		private static MethodCallExpression NormalizeSelectWithTypeCast(MethodCallExpression callExpression)
		{
			Type type;
			if (ExpressionNormalizer.TryMatchSelectWithConvert(callExpression, out type))
			{
				MethodInfo method = callExpression.Method.DeclaringType.GetMethod("Cast", true, true);
				if (method != null && method.IsGenericMethodDefinition && ReflectionUtil.IsSequenceMethod(method, SequenceMethod.Cast))
				{
					MethodInfo methodInfo = method.MakeGenericMethod(new Type[] { type });
					return Expression.Call(methodInfo, callExpression.Arguments[0]);
				}
			}
			return callExpression;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00019808 File Offset: 0x00017A08
		private static bool TryMatchSelectWithConvert(MethodCallExpression callExpression, out Type convertType)
		{
			convertType = null;
			return ReflectionUtil.IsSequenceMethod(callExpression.Method, SequenceMethod.Select) && ExpressionNormalizer.TryMatchConvertSingleArgument(callExpression.Arguments[1], out convertType);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00019830 File Offset: 0x00017A30
		private static bool TryMatchConvertSingleArgument(Expression expression, out Type convertType)
		{
			convertType = null;
			expression = ((expression.NodeType == ExpressionType.Quote) ? ((UnaryExpression)expression).Operand : expression);
			if (expression.NodeType == ExpressionType.Lambda)
			{
				LambdaExpression lambdaExpression = (LambdaExpression)expression;
				if (lambdaExpression.Parameters.Count == 1 && lambdaExpression.Body.NodeType == ExpressionType.Convert)
				{
					UnaryExpression unaryExpression = (UnaryExpression)lambdaExpression.Body;
					if (unaryExpression.Operand == lambdaExpression.Parameters[0])
					{
						convertType = unaryExpression.Type;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000198B3 File Offset: 0x00017AB3
		private static bool RelationalOperatorPlaceholder<TLeft, TRight>(TLeft left, TRight right)
		{
			return object.ReferenceEquals(left, right);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000198C8 File Offset: 0x00017AC8
		private static BinaryExpression CreateRelationalOperator(ExpressionType op, Expression left, Expression right)
		{
			BinaryExpression binaryExpression;
			ExpressionNormalizer.TryCreateRelationalOperator(op, left, right, out binaryExpression);
			return binaryExpression;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000198E4 File Offset: 0x00017AE4
		private static bool TryCreateRelationalOperator(ExpressionType op, Expression left, Expression right, out BinaryExpression result)
		{
			MethodInfo methodInfo = ExpressionNormalizer.s_relationalOperatorPlaceholderMethod.MakeGenericMethod(new Type[] { left.Type, right.Type });
			switch (op)
			{
			case ExpressionType.Equal:
				result = Expression.Equal(left, right, false, methodInfo);
				return true;
			case ExpressionType.ExclusiveOr:
			case ExpressionType.Invoke:
			case ExpressionType.Lambda:
			case ExpressionType.LeftShift:
				break;
			case ExpressionType.GreaterThan:
				result = Expression.GreaterThan(left, right, false, methodInfo);
				return true;
			case ExpressionType.GreaterThanOrEqual:
				result = Expression.GreaterThanOrEqual(left, right, false, methodInfo);
				return true;
			case ExpressionType.LessThan:
				result = Expression.LessThan(left, right, false, methodInfo);
				return true;
			case ExpressionType.LessThanOrEqual:
				result = Expression.LessThanOrEqual(left, right, false, methodInfo);
				return true;
			default:
				if (op == ExpressionType.NotEqual)
				{
					result = Expression.NotEqual(left, right, false, methodInfo);
					return true;
				}
				break;
			}
			result = null;
			return false;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000199A0 File Offset: 0x00017BA0
		private Expression CreateCompareExpression(Expression left, Expression right)
		{
			Expression expression = Expression.Condition(ExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, left, right), Expression.Constant(0), Expression.Condition(ExpressionNormalizer.CreateRelationalOperator(ExpressionType.GreaterThan, left, right), Expression.Constant(1), Expression.Constant(-1)));
			this._patterns[expression] = new ExpressionNormalizer.ComparePattern(left, right);
			return expression;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000199FF File Offset: 0x00017BFF
		private void RecordRewrite(Expression source, Expression rewritten)
		{
			if (source != rewritten)
			{
				this.NormalizerRewrites.Add(rewritten, source);
			}
		}

		// Token: 0x04000402 RID: 1026
		private const bool LiftToNull = false;

		// Token: 0x04000403 RID: 1027
		private readonly Dictionary<Expression, ExpressionNormalizer.Pattern> _patterns = new Dictionary<Expression, ExpressionNormalizer.Pattern>(ReferenceEqualityComparer<Expression>.Instance);

		// Token: 0x04000404 RID: 1028
		private readonly Dictionary<Expression, Expression> normalizerRewrites;

		// Token: 0x04000405 RID: 1029
		private static readonly MethodInfo s_relationalOperatorPlaceholderMethod = typeof(ExpressionNormalizer).GetMethod("RelationalOperatorPlaceholder", false, true);

		// Token: 0x020000C6 RID: 198
		private abstract class Pattern
		{
			// Token: 0x1700018D RID: 397
			// (get) Token: 0x06000665 RID: 1637
			internal abstract ExpressionNormalizer.PatternKind Kind { get; }
		}

		// Token: 0x020000C7 RID: 199
		private enum PatternKind
		{
			// Token: 0x04000407 RID: 1031
			Compare
		}

		// Token: 0x020000C8 RID: 200
		private sealed class ComparePattern : ExpressionNormalizer.Pattern
		{
			// Token: 0x06000667 RID: 1639 RVA: 0x00019A37 File Offset: 0x00017C37
			internal ComparePattern(Expression left, Expression right)
			{
				this.Left = left;
				this.Right = right;
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x06000668 RID: 1640 RVA: 0x00019A4D File Offset: 0x00017C4D
			internal override ExpressionNormalizer.PatternKind Kind
			{
				get
				{
					return ExpressionNormalizer.PatternKind.Compare;
				}
			}

			// Token: 0x04000408 RID: 1032
			internal readonly Expression Left;

			// Token: 0x04000409 RID: 1033
			internal readonly Expression Right;
		}
	}
}

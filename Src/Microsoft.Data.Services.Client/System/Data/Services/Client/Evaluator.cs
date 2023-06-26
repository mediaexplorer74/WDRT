using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000C2 RID: 194
	internal static class Evaluator
	{
		// Token: 0x06000645 RID: 1605 RVA: 0x00018A4C File Offset: 0x00016C4C
		internal static Expression PartialEval(Expression expression, Func<Expression, bool> canBeEvaluated)
		{
			Evaluator.Nominator nominator = new Evaluator.Nominator(canBeEvaluated);
			HashSet<Expression> hashSet = nominator.Nominate(expression);
			return new Evaluator.SubtreeEvaluator(hashSet).Eval(expression);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00018A74 File Offset: 0x00016C74
		internal static Expression PartialEval(Expression expression)
		{
			return Evaluator.PartialEval(expression, new Func<Expression, bool>(Evaluator.CanBeEvaluatedLocally));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00018A88 File Offset: 0x00016C88
		private static bool CanBeEvaluatedLocally(Expression expression)
		{
			return expression.NodeType != ExpressionType.Parameter && expression.NodeType != ExpressionType.Lambda && expression.NodeType != (ExpressionType)10000;
		}

		// Token: 0x020000C3 RID: 195
		internal class SubtreeEvaluator : DataServiceALinqExpressionVisitor
		{
			// Token: 0x06000648 RID: 1608 RVA: 0x00018AB0 File Offset: 0x00016CB0
			internal SubtreeEvaluator(HashSet<Expression> candidates)
			{
				this.candidates = candidates;
			}

			// Token: 0x06000649 RID: 1609 RVA: 0x00018ABF File Offset: 0x00016CBF
			internal Expression Eval(Expression exp)
			{
				return this.Visit(exp);
			}

			// Token: 0x0600064A RID: 1610 RVA: 0x00018AC8 File Offset: 0x00016CC8
			internal override Expression Visit(Expression exp)
			{
				if (exp == null)
				{
					return null;
				}
				if (this.candidates.Contains(exp))
				{
					return Evaluator.SubtreeEvaluator.Evaluate(exp);
				}
				return base.Visit(exp);
			}

			// Token: 0x0600064B RID: 1611 RVA: 0x00018AEC File Offset: 0x00016CEC
			private static Expression Evaluate(Expression e)
			{
				if (e.NodeType == ExpressionType.Constant)
				{
					return e;
				}
				LambdaExpression lambdaExpression = Expression.Lambda(e, new ParameterExpression[0]);
				Delegate @delegate = lambdaExpression.Compile();
				object obj = @delegate.DynamicInvoke(null);
				Type type = e.Type;
				if (obj != null && type.IsArray && type.GetElementType() == obj.GetType().GetElementType())
				{
					type = obj.GetType();
				}
				return Expression.Constant(obj, type);
			}

			// Token: 0x040003FE RID: 1022
			private HashSet<Expression> candidates;
		}

		// Token: 0x020000C4 RID: 196
		internal class Nominator : DataServiceALinqExpressionVisitor
		{
			// Token: 0x0600064C RID: 1612 RVA: 0x00018B59 File Offset: 0x00016D59
			internal Nominator(Func<Expression, bool> functionCanBeEvaluated)
			{
				this.functionCanBeEvaluated = functionCanBeEvaluated;
			}

			// Token: 0x0600064D RID: 1613 RVA: 0x00018B68 File Offset: 0x00016D68
			internal HashSet<Expression> Nominate(Expression expression)
			{
				this.candidates = new HashSet<Expression>(EqualityComparer<Expression>.Default);
				this.Visit(expression);
				return this.candidates;
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x00018B88 File Offset: 0x00016D88
			internal override Expression Visit(Expression expression)
			{
				if (expression != null)
				{
					bool flag = this.cannotBeEvaluated;
					this.cannotBeEvaluated = false;
					base.Visit(expression);
					if (!this.cannotBeEvaluated)
					{
						if (this.functionCanBeEvaluated(expression))
						{
							this.candidates.Add(expression);
						}
						else
						{
							this.cannotBeEvaluated = true;
						}
					}
					this.cannotBeEvaluated = this.cannotBeEvaluated || flag;
				}
				return expression;
			}

			// Token: 0x040003FF RID: 1023
			private Func<Expression, bool> functionCanBeEvaluated;

			// Token: 0x04000400 RID: 1024
			private HashSet<Expression> candidates;

			// Token: 0x04000401 RID: 1025
			private bool cannotBeEvaluated;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000C9 RID: 201
	internal class FilterQueryOptionExpression : QueryOptionExpression
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x00019A50 File Offset: 0x00017C50
		internal FilterQueryOptionExpression(Type type)
			: base(type)
		{
			this.individualExpressions = new List<Expression>();
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00019A64 File Offset: 0x00017C64
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10006;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00019A6B File Offset: 0x00017C6B
		internal ReadOnlyCollection<Expression> PredicateConjuncts
		{
			get
			{
				return new ReadOnlyCollection<Expression>(this.individualExpressions);
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00019A78 File Offset: 0x00017C78
		public void AddPredicateConjuncts(IEnumerable<Expression> predicates)
		{
			this.individualExpressions.AddRange(predicates);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00019A88 File Offset: 0x00017C88
		public Expression GetPredicate()
		{
			Expression expression = null;
			bool flag = true;
			foreach (Expression expression2 in this.individualExpressions)
			{
				if (flag)
				{
					expression = expression2;
					flag = false;
				}
				else
				{
					expression = Expression.And(expression, expression2);
				}
			}
			return expression;
		}

		// Token: 0x0400040A RID: 1034
		private readonly List<Expression> individualExpressions;
	}
}

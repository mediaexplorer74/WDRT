﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x02000075 RID: 117
	internal abstract class ALinqExpressionVisitor
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x00010600 File Offset: 0x0000E800
		internal virtual Expression Visit(Expression exp)
		{
			if (exp == null)
			{
				return exp;
			}
			switch (exp.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.And:
			case ExpressionType.AndAlso:
			case ExpressionType.ArrayIndex:
			case ExpressionType.Coalesce:
			case ExpressionType.Divide:
			case ExpressionType.Equal:
			case ExpressionType.ExclusiveOr:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LeftShift:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
			case ExpressionType.NotEqual:
			case ExpressionType.Or:
			case ExpressionType.OrElse:
			case ExpressionType.RightShift:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				return this.VisitBinary((BinaryExpression)exp);
			case ExpressionType.ArrayLength:
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
			case ExpressionType.Quote:
			case ExpressionType.TypeAs:
				return this.VisitUnary((UnaryExpression)exp);
			case ExpressionType.Call:
				return this.VisitMethodCall((MethodCallExpression)exp);
			case ExpressionType.Conditional:
				return this.VisitConditional((ConditionalExpression)exp);
			case ExpressionType.Constant:
				return this.VisitConstant((ConstantExpression)exp);
			case ExpressionType.Invoke:
				return this.VisitInvocation((InvocationExpression)exp);
			case ExpressionType.Lambda:
				return this.VisitLambda((LambdaExpression)exp);
			case ExpressionType.ListInit:
				return this.VisitListInit((ListInitExpression)exp);
			case ExpressionType.MemberAccess:
				return this.VisitMemberAccess((MemberExpression)exp);
			case ExpressionType.MemberInit:
				return this.VisitMemberInit((MemberInitExpression)exp);
			case ExpressionType.New:
				return this.VisitNew((NewExpression)exp);
			case ExpressionType.NewArrayInit:
			case ExpressionType.NewArrayBounds:
				return this.VisitNewArray((NewArrayExpression)exp);
			case ExpressionType.Parameter:
				return this.VisitParameter((ParameterExpression)exp);
			case ExpressionType.TypeIs:
				return this.VisitTypeIs((TypeBinaryExpression)exp);
			}
			throw new NotSupportedException(Strings.ALinq_UnsupportedExpression(exp.NodeType.ToString()));
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000107AC File Offset: 0x0000E9AC
		internal virtual MemberBinding VisitBinding(MemberBinding binding)
		{
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				return this.VisitMemberAssignment((MemberAssignment)binding);
			case MemberBindingType.MemberBinding:
				return this.VisitMemberMemberBinding((MemberMemberBinding)binding);
			case MemberBindingType.ListBinding:
				return this.VisitMemberListBinding((MemberListBinding)binding);
			default:
				throw new NotSupportedException(Strings.ALinq_UnsupportedExpression(binding.BindingType.ToString()));
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00010818 File Offset: 0x0000EA18
		internal virtual ElementInit VisitElementInitializer(ElementInit initializer)
		{
			ReadOnlyCollection<Expression> readOnlyCollection = this.VisitExpressionList(initializer.Arguments);
			if (readOnlyCollection == initializer.Arguments)
			{
				return initializer;
			}
			return Expression.ElementInit(initializer.AddMethod, readOnlyCollection);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001084C File Offset: 0x0000EA4C
		internal virtual Expression VisitUnary(UnaryExpression u)
		{
			Expression expression = this.Visit(u.Operand);
			if (expression == u.Operand)
			{
				return u;
			}
			return Expression.MakeUnary(u.NodeType, expression, u.Type, u.Method);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001088C File Offset: 0x0000EA8C
		internal virtual Expression VisitBinary(BinaryExpression b)
		{
			Expression expression = this.Visit(b.Left);
			Expression expression2 = this.Visit(b.Right);
			Expression expression3 = this.Visit(b.Conversion);
			if (expression == b.Left && expression2 == b.Right && expression3 == b.Conversion)
			{
				return b;
			}
			if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
			{
				return Expression.Coalesce(expression, expression2, expression3 as LambdaExpression);
			}
			return Expression.MakeBinary(b.NodeType, expression, expression2, b.IsLiftedToNull, b.Method);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00010918 File Offset: 0x0000EB18
		internal virtual Expression VisitTypeIs(TypeBinaryExpression b)
		{
			Expression expression = this.Visit(b.Expression);
			if (expression == b.Expression)
			{
				return b;
			}
			return Expression.TypeIs(expression, b.TypeOperand);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00010949 File Offset: 0x0000EB49
		internal virtual Expression VisitConstant(ConstantExpression c)
		{
			return c;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001094C File Offset: 0x0000EB4C
		internal virtual Expression VisitConditional(ConditionalExpression c)
		{
			Expression expression = this.Visit(c.Test);
			Expression expression2 = this.Visit(c.IfTrue);
			Expression expression3 = this.Visit(c.IfFalse);
			if (expression != c.Test || expression2 != c.IfTrue || expression3 != c.IfFalse)
			{
				return Expression.Condition(expression, expression2, expression3, expression2.Type.IsAssignableFrom(expression3.Type) ? expression2.Type : expression3.Type);
			}
			return c;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000109C6 File Offset: 0x0000EBC6
		internal virtual Expression VisitParameter(ParameterExpression p)
		{
			return p;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000109CC File Offset: 0x0000EBCC
		internal virtual Expression VisitMemberAccess(MemberExpression m)
		{
			Expression expression = this.Visit(m.Expression);
			if (expression == m.Expression)
			{
				return m;
			}
			return Expression.MakeMemberAccess(expression, m.Member);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00010A00 File Offset: 0x0000EC00
		internal virtual Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression expression = this.Visit(m.Object);
			IEnumerable<Expression> enumerable = this.VisitExpressionList(m.Arguments);
			if (expression == m.Object && enumerable == m.Arguments)
			{
				return m;
			}
			return Expression.Call(expression, m.Method, enumerable);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00010A48 File Offset: 0x0000EC48
		internal virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
		{
			List<Expression> list = null;
			int i = 0;
			int count = original.Count;
			while (i < count)
			{
				Expression expression = this.Visit(original[i]);
				if (list != null)
				{
					list.Add(expression);
				}
				else if (expression != original[i])
				{
					list = new List<Expression>(count);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(expression);
				}
				i++;
			}
			if (list != null)
			{
				return new ReadOnlyCollection<Expression>(list);
			}
			return original;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		internal virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
		{
			Expression expression = this.Visit(assignment.Expression);
			if (expression == assignment.Expression)
			{
				return assignment;
			}
			return Expression.Bind(assignment.Member, expression);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00010AFC File Offset: 0x0000ECFC
		internal virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			IEnumerable<MemberBinding> enumerable = this.VisitBindingList(binding.Bindings);
			if (enumerable == binding.Bindings)
			{
				return binding;
			}
			return Expression.MemberBind(binding.Member, enumerable);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00010B30 File Offset: 0x0000ED30
		internal virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
		{
			IEnumerable<ElementInit> enumerable = this.VisitElementInitializerList(binding.Initializers);
			if (enumerable == binding.Initializers)
			{
				return binding;
			}
			return Expression.ListBind(binding.Member, enumerable);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010B64 File Offset: 0x0000ED64
		internal virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
		{
			List<MemberBinding> list = null;
			int i = 0;
			int count = original.Count;
			while (i < count)
			{
				MemberBinding memberBinding = this.VisitBinding(original[i]);
				if (list != null)
				{
					list.Add(memberBinding);
				}
				else if (memberBinding != original[i])
				{
					list = new List<MemberBinding>(count);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(memberBinding);
				}
				i++;
			}
			if (list != null)
			{
				return list;
			}
			return original;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010BDC File Offset: 0x0000EDDC
		internal virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original)
		{
			List<ElementInit> list = null;
			int i = 0;
			int count = original.Count;
			while (i < count)
			{
				ElementInit elementInit = this.VisitElementInitializer(original[i]);
				if (list != null)
				{
					list.Add(elementInit);
				}
				else if (elementInit != original[i])
				{
					list = new List<ElementInit>(count);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(elementInit);
				}
				i++;
			}
			if (list != null)
			{
				return list;
			}
			return original;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00010C54 File Offset: 0x0000EE54
		internal virtual Expression VisitLambda(LambdaExpression lambda)
		{
			Expression expression = this.Visit(lambda.Body);
			if (expression != lambda.Body)
			{
				return Expression.Lambda(lambda.Type, expression, lambda.Parameters);
			}
			return lambda;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00010C8C File Offset: 0x0000EE8C
		internal virtual NewExpression VisitNew(NewExpression nex)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(nex.Arguments);
			if (enumerable == nex.Arguments)
			{
				return nex;
			}
			if (nex.Members == null)
			{
				return Expression.New(nex.Constructor, enumerable);
			}
			return Expression.New(nex.Constructor, enumerable, nex.Members);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00010CD8 File Offset: 0x0000EED8
		internal virtual Expression VisitMemberInit(MemberInitExpression init)
		{
			NewExpression newExpression = this.VisitNew(init.NewExpression);
			IEnumerable<MemberBinding> enumerable = this.VisitBindingList(init.Bindings);
			if (newExpression == init.NewExpression && enumerable == init.Bindings)
			{
				return init;
			}
			return Expression.MemberInit(newExpression, enumerable);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00010D1C File Offset: 0x0000EF1C
		internal virtual Expression VisitListInit(ListInitExpression init)
		{
			NewExpression newExpression = this.VisitNew(init.NewExpression);
			IEnumerable<ElementInit> enumerable = this.VisitElementInitializerList(init.Initializers);
			if (newExpression == init.NewExpression && enumerable == init.Initializers)
			{
				return init;
			}
			return Expression.ListInit(newExpression, enumerable);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00010D60 File Offset: 0x0000EF60
		internal virtual Expression VisitNewArray(NewArrayExpression na)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(na.Expressions);
			if (enumerable == na.Expressions)
			{
				return na;
			}
			if (na.NodeType != ExpressionType.NewArrayInit)
			{
				return Expression.NewArrayBounds(na.Type.GetElementType(), enumerable);
			}
			return Expression.NewArrayInit(na.Type.GetElementType(), enumerable);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00010DB4 File Offset: 0x0000EFB4
		internal virtual Expression VisitInvocation(InvocationExpression iv)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(iv.Arguments);
			Expression expression = this.Visit(iv.Expression);
			if (enumerable == iv.Arguments && expression == iv.Expression)
			{
				return iv;
			}
			return Expression.Invoke(expression, enumerable);
		}
	}
}

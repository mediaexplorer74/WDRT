using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x02000076 RID: 118
	internal class MemberAssignmentAnalysis : ALinqExpressionVisitor
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00010DFE File Offset: 0x0000EFFE
		private MemberAssignmentAnalysis(Expression entity)
		{
			this.entity = entity;
			this.pathFromEntity = new List<Expression>();
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00010E18 File Offset: 0x0000F018
		internal Exception IncompatibleAssignmentsException
		{
			get
			{
				return this.incompatibleAssignmentsException;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00010E20 File Offset: 0x0000F020
		internal bool MultiplePathsFound
		{
			get
			{
				return this.multiplePathsFound;
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00010E28 File Offset: 0x0000F028
		internal static MemberAssignmentAnalysis Analyze(Expression entityInScope, Expression assignmentExpression)
		{
			MemberAssignmentAnalysis memberAssignmentAnalysis = new MemberAssignmentAnalysis(entityInScope);
			memberAssignmentAnalysis.Visit(assignmentExpression);
			return memberAssignmentAnalysis;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00010E48 File Offset: 0x0000F048
		internal Exception CheckCompatibleAssignments(Type targetType, ref MemberAssignmentAnalysis previous)
		{
			if (previous == null)
			{
				previous = this;
				return null;
			}
			Expression[] expressionsToTargetEntity = previous.GetExpressionsToTargetEntity();
			Expression[] expressionsToTargetEntity2 = this.GetExpressionsToTargetEntity();
			return MemberAssignmentAnalysis.CheckCompatibleAssignments(targetType, expressionsToTargetEntity, expressionsToTargetEntity2);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010E75 File Offset: 0x0000F075
		internal override Expression Visit(Expression expression)
		{
			if (this.multiplePathsFound || this.incompatibleAssignmentsException != null)
			{
				return expression;
			}
			return base.Visit(expression);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010E90 File Offset: 0x0000F090
		internal override Expression VisitConditional(ConditionalExpression c)
		{
			ResourceBinder.PatternRules.MatchNullCheckResult matchNullCheckResult = ResourceBinder.PatternRules.MatchNullCheck(this.entity, c);
			Expression expression;
			if (matchNullCheckResult.Match)
			{
				this.Visit(matchNullCheckResult.AssignExpression);
				expression = c;
			}
			else
			{
				expression = base.VisitConditional(c);
			}
			return expression;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010ECE File Offset: 0x0000F0CE
		internal override Expression VisitParameter(ParameterExpression p)
		{
			if (p == this.entity)
			{
				if (this.pathFromEntity.Count != 0)
				{
					this.multiplePathsFound = true;
				}
				else
				{
					this.pathFromEntity.Add(p);
				}
			}
			return p;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010EFC File Offset: 0x0000F0FC
		internal override NewExpression VisitNew(NewExpression nex)
		{
			if (nex.Members == null)
			{
				return base.VisitNew(nex);
			}
			MemberAssignmentAnalysis memberAssignmentAnalysis = null;
			foreach (Expression expression in nex.Arguments)
			{
				if (!this.CheckCompatibleAssigmentExpression(expression, nex.Type, ref memberAssignmentAnalysis))
				{
					break;
				}
			}
			return nex;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010F68 File Offset: 0x0000F168
		internal override Expression VisitMemberInit(MemberInitExpression init)
		{
			MemberAssignmentAnalysis memberAssignmentAnalysis = null;
			foreach (MemberBinding memberBinding in init.Bindings)
			{
				MemberAssignment memberAssignment = memberBinding as MemberAssignment;
				if (memberAssignment != null && !this.CheckCompatibleAssigmentExpression(memberAssignment.Expression, init.Type, ref memberAssignmentAnalysis))
				{
					break;
				}
			}
			return init;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			Expression expression = base.VisitMemberAccess(m);
			Type type;
			Expression expression2 = ResourceBinder.StripTo<Expression>(m.Expression, out type);
			if (this.pathFromEntity.Contains(expression2))
			{
				this.pathFromEntity.Add(m);
			}
			return expression;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00011016 File Offset: 0x0000F216
		internal override Expression VisitMethodCall(MethodCallExpression call)
		{
			if (ReflectionUtil.IsSequenceMethod(call.Method, SequenceMethod.Select))
			{
				this.Visit(call.Arguments[0]);
				return call;
			}
			return base.VisitMethodCall(call);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00011044 File Offset: 0x0000F244
		internal Expression[] GetExpressionsBeyondTargetEntity()
		{
			if (this.pathFromEntity.Count <= 1)
			{
				return MemberAssignmentAnalysis.EmptyExpressionArray;
			}
			return new Expression[] { this.pathFromEntity[this.pathFromEntity.Count - 1] };
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00011088 File Offset: 0x0000F288
		internal Expression[] GetExpressionsToTargetEntity()
		{
			return this.GetExpressionsToTargetEntity(true);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00011094 File Offset: 0x0000F294
		internal Expression[] GetExpressionsToTargetEntity(bool ignoreLastExpression)
		{
			int num = (ignoreLastExpression ? 1 : 0);
			if (this.pathFromEntity.Count <= num)
			{
				return MemberAssignmentAnalysis.EmptyExpressionArray;
			}
			Expression[] array = new Expression[this.pathFromEntity.Count - num];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.pathFromEntity[i];
			}
			return array;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000110F0 File Offset: 0x0000F2F0
		private static Exception CheckCompatibleAssignments(Type targetType, Expression[] previous, Expression[] candidate)
		{
			if (previous.Length != candidate.Length)
			{
				throw MemberAssignmentAnalysis.CheckCompatibleAssignmentsFail(targetType, previous, candidate);
			}
			for (int i = 0; i < previous.Length; i++)
			{
				Expression expression = previous[i];
				Expression expression2 = candidate[i];
				if (expression.NodeType != expression2.NodeType)
				{
					throw MemberAssignmentAnalysis.CheckCompatibleAssignmentsFail(targetType, previous, candidate);
				}
				if (expression != expression2)
				{
					if (expression.NodeType != ExpressionType.MemberAccess)
					{
						return MemberAssignmentAnalysis.CheckCompatibleAssignmentsFail(targetType, previous, candidate);
					}
					if (((MemberExpression)expression).Member.Name != ((MemberExpression)expression2).Member.Name)
					{
						return MemberAssignmentAnalysis.CheckCompatibleAssignmentsFail(targetType, previous, candidate);
					}
				}
			}
			return null;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00011184 File Offset: 0x0000F384
		private static Exception CheckCompatibleAssignmentsFail(Type targetType, Expression[] previous, Expression[] candidate)
		{
			string text = Strings.ALinq_ProjectionMemberAssignmentMismatch(targetType.FullName, previous.LastOrDefault<Expression>(), candidate.LastOrDefault<Expression>());
			return new NotSupportedException(text);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000111B0 File Offset: 0x0000F3B0
		private bool CheckCompatibleAssigmentExpression(Expression expressionToAssign, Type initType, ref MemberAssignmentAnalysis previousNested)
		{
			MemberAssignmentAnalysis memberAssignmentAnalysis = MemberAssignmentAnalysis.Analyze(this.entity, expressionToAssign);
			if (memberAssignmentAnalysis.MultiplePathsFound)
			{
				this.multiplePathsFound = true;
				return false;
			}
			Exception ex = memberAssignmentAnalysis.CheckCompatibleAssignments(initType, ref previousNested);
			if (ex != null)
			{
				this.incompatibleAssignmentsException = ex;
				return false;
			}
			if (this.pathFromEntity.Count == 0)
			{
				this.pathFromEntity.AddRange(memberAssignmentAnalysis.GetExpressionsToTargetEntity());
			}
			return true;
		}

		// Token: 0x040002B9 RID: 697
		internal static readonly Expression[] EmptyExpressionArray = new Expression[0];

		// Token: 0x040002BA RID: 698
		private readonly Expression entity;

		// Token: 0x040002BB RID: 699
		private Exception incompatibleAssignmentsException;

		// Token: 0x040002BC RID: 700
		private bool multiplePathsFound;

		// Token: 0x040002BD RID: 701
		private List<Expression> pathFromEntity;
	}
}

using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x0200007E RID: 126
	internal class ProjectionPathBuilder
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x000119DC File Offset: 0x0000FBDC
		internal ProjectionPathBuilder()
		{
			this.entityInScope = new Stack<bool>();
			this.rewrites = new List<ProjectionPathBuilder.MemberInitRewrite>();
			this.parameterExpressions = new Stack<ParameterExpression>();
			this.parameterExpressionTypes = new Stack<Expression>();
			this.parameterEntries = new Stack<Expression>();
			this.parameterProjectionTypes = new Stack<Type>();
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00011A31 File Offset: 0x0000FC31
		internal bool CurrentIsEntity
		{
			get
			{
				return this.entityInScope.Peek();
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00011A3E File Offset: 0x0000FC3E
		internal Expression ExpectedParamTypeInScope
		{
			get
			{
				return this.parameterExpressionTypes.Peek();
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00011A4B File Offset: 0x0000FC4B
		internal bool HasRewrites
		{
			get
			{
				return this.rewrites.Count > 0;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00011A5B File Offset: 0x0000FC5B
		internal Expression LambdaParameterInScope
		{
			get
			{
				return this.parameterExpressions.Peek();
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00011A68 File Offset: 0x0000FC68
		internal Expression ParameterEntryInScope
		{
			get
			{
				return this.parameterEntries.Peek();
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00011A78 File Offset: 0x0000FC78
		public override string ToString()
		{
			string text = "ProjectionPathBuilder: ";
			if (this.parameterExpressions.Count == 0)
			{
				text += "(empty)";
			}
			else
			{
				object obj = text;
				text = string.Concat(new object[] { obj, "entity:", this.CurrentIsEntity, " param:", this.ParameterEntryInScope });
			}
			return text;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		internal void EnterLambdaScope(LambdaExpression lambda, Expression entry, Expression expectedType)
		{
			ParameterExpression parameterExpression = lambda.Parameters[0];
			Type type = lambda.Body.Type;
			bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(type);
			this.entityInScope.Push(flag);
			this.parameterExpressions.Push(parameterExpression);
			this.parameterExpressionTypes.Push(expectedType);
			this.parameterEntries.Push(entry);
			this.parameterProjectionTypes.Push(type);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00011B50 File Offset: 0x0000FD50
		internal void EnterMemberInit(MemberInitExpression init)
		{
			bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(init.Type);
			this.entityInScope.Push(flag);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00011B78 File Offset: 0x0000FD78
		internal Expression GetRewrite(Expression expression)
		{
			List<string> list = new List<string>();
			expression = ResourceBinder.StripTo<Expression>(expression);
			while (expression.NodeType == ExpressionType.MemberAccess || expression.NodeType == ExpressionType.TypeAs)
			{
				if (expression.NodeType == ExpressionType.MemberAccess)
				{
					MemberExpression memberExpression = (MemberExpression)expression;
					list.Add(memberExpression.Member.Name);
					expression = ResourceBinder.StripTo<Expression>(memberExpression.Expression);
				}
				else
				{
					expression = ResourceBinder.StripTo<Expression>(((UnaryExpression)expression).Operand);
				}
			}
			Expression expression2 = null;
			foreach (ProjectionPathBuilder.MemberInitRewrite memberInitRewrite in this.rewrites)
			{
				if (memberInitRewrite.Root == expression && list.Count == memberInitRewrite.MemberNames.Length)
				{
					bool flag = true;
					int num = 0;
					while (num < list.Count && num < memberInitRewrite.MemberNames.Length)
					{
						if (list[list.Count - num - 1] != memberInitRewrite.MemberNames[num])
						{
							flag = false;
							break;
						}
						num++;
					}
					if (flag)
					{
						expression2 = memberInitRewrite.RewriteExpression;
						break;
					}
				}
			}
			return expression2;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00011CA4 File Offset: 0x0000FEA4
		internal void LeaveLambdaScope()
		{
			this.entityInScope.Pop();
			this.parameterExpressions.Pop();
			this.parameterExpressionTypes.Pop();
			this.parameterEntries.Pop();
			this.parameterProjectionTypes.Pop();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00011CE2 File Offset: 0x0000FEE2
		internal void LeaveMemberInit()
		{
			this.entityInScope.Pop();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		internal void RegisterRewrite(Expression root, string[] names, Expression rewriteExpression)
		{
			this.rewrites.Add(new ProjectionPathBuilder.MemberInitRewrite
			{
				Root = root,
				MemberNames = names,
				RewriteExpression = rewriteExpression
			});
			this.parameterEntries.Push(rewriteExpression);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00011D30 File Offset: 0x0000FF30
		internal void RevokeRewrite(Expression root, string[] names)
		{
			for (int i = 0; i < this.rewrites.Count; i++)
			{
				if (this.rewrites[i].Root == root && names.Length == this.rewrites[i].MemberNames.Length)
				{
					bool flag = true;
					for (int j = 0; j < names.Length; j++)
					{
						if (names[j] != this.rewrites[i].MemberNames[j])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						this.rewrites.RemoveAt(i);
						this.parameterEntries.Pop();
						return;
					}
				}
			}
		}

		// Token: 0x040002CA RID: 714
		private readonly Stack<bool> entityInScope;

		// Token: 0x040002CB RID: 715
		private readonly List<ProjectionPathBuilder.MemberInitRewrite> rewrites;

		// Token: 0x040002CC RID: 716
		private readonly Stack<ParameterExpression> parameterExpressions;

		// Token: 0x040002CD RID: 717
		private readonly Stack<Expression> parameterExpressionTypes;

		// Token: 0x040002CE RID: 718
		private readonly Stack<Expression> parameterEntries;

		// Token: 0x040002CF RID: 719
		private readonly Stack<Type> parameterProjectionTypes;

		// Token: 0x0200007F RID: 127
		internal class MemberInitRewrite
		{
			// Token: 0x17000115 RID: 277
			// (get) Token: 0x0600044B RID: 1099 RVA: 0x00011DCF File Offset: 0x0000FFCF
			// (set) Token: 0x0600044C RID: 1100 RVA: 0x00011DD7 File Offset: 0x0000FFD7
			internal string[] MemberNames { get; set; }

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x0600044D RID: 1101 RVA: 0x00011DE0 File Offset: 0x0000FFE0
			// (set) Token: 0x0600044E RID: 1102 RVA: 0x00011DE8 File Offset: 0x0000FFE8
			internal Expression Root { get; set; }

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x0600044F RID: 1103 RVA: 0x00011DF1 File Offset: 0x0000FFF1
			// (set) Token: 0x06000450 RID: 1104 RVA: 0x00011DF9 File Offset: 0x0000FFF9
			internal Expression RewriteExpression { get; set; }
		}
	}
}

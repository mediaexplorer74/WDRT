using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Services.Client
{
	// Token: 0x020000CA RID: 202
	internal sealed class InputBinder : DataServiceALinqExpressionVisitor
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00019AEC File Offset: 0x00017CEC
		private InputBinder(ResourceExpression resource, ParameterExpression setReferenceParam)
		{
			this.input = resource;
			this.inputSet = resource as ResourceSetExpression;
			this.inputParameter = setReferenceParam;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00019B20 File Offset: 0x00017D20
		internal static Expression Bind(Expression e, ResourceExpression currentInput, ParameterExpression inputParameter, List<ResourceExpression> referencedInputs)
		{
			InputBinder inputBinder = new InputBinder(currentInput, inputParameter);
			Expression expression = inputBinder.Visit(e);
			referencedInputs.AddRange(inputBinder.referencedInputs);
			return expression;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00019B4C File Offset: 0x00017D4C
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			if (this.inputSet == null || !this.inputSet.HasTransparentScope)
			{
				return base.VisitMemberAccess(m);
			}
			ParameterExpression parameterExpression = null;
			Stack<PropertyInfo> stack = new Stack<PropertyInfo>();
			MemberExpression memberExpression = m;
			while (memberExpression != null && PlatformHelper.IsProperty(memberExpression.Member) && memberExpression.Expression != null)
			{
				stack.Push((PropertyInfo)memberExpression.Member);
				if (memberExpression.Expression.NodeType == ExpressionType.Parameter)
				{
					parameterExpression = (ParameterExpression)memberExpression.Expression;
				}
				memberExpression = memberExpression.Expression as MemberExpression;
			}
			if (parameterExpression != this.inputParameter || stack.Count == 0)
			{
				return m;
			}
			ResourceExpression resourceExpression = this.input;
			ResourceSetExpression resourceSetExpression = this.inputSet;
			bool flag = false;
			while (stack.Count > 0 && resourceSetExpression != null && resourceSetExpression.HasTransparentScope)
			{
				PropertyInfo propertyInfo = stack.Peek();
				if (propertyInfo.Name.Equals(resourceSetExpression.TransparentScope.Accessor, StringComparison.Ordinal))
				{
					resourceExpression = resourceSetExpression;
					stack.Pop();
					flag = true;
				}
				else
				{
					Expression expression;
					if (!resourceSetExpression.TransparentScope.SourceAccessors.TryGetValue(propertyInfo.Name, out expression))
					{
						break;
					}
					flag = true;
					stack.Pop();
					InputReferenceExpression inputReferenceExpression = expression as InputReferenceExpression;
					if (inputReferenceExpression == null)
					{
						resourceSetExpression = expression as ResourceSetExpression;
						if (resourceSetExpression == null || !resourceSetExpression.HasTransparentScope)
						{
							resourceExpression = (ResourceExpression)expression;
						}
					}
					else
					{
						resourceSetExpression = inputReferenceExpression.Target as ResourceSetExpression;
						resourceExpression = resourceSetExpression;
					}
				}
			}
			if (!flag)
			{
				return m;
			}
			Expression expression2 = this.CreateReference(resourceExpression);
			while (stack.Count > 0)
			{
				expression2 = Expression.Property(expression2, stack.Pop());
			}
			return expression2;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00019CDC File Offset: 0x00017EDC
		internal override Expression VisitParameter(ParameterExpression p)
		{
			if ((this.inputSet == null || !this.inputSet.HasTransparentScope) && p == this.inputParameter)
			{
				return this.CreateReference(this.input);
			}
			return base.VisitParameter(p);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00019D10 File Offset: 0x00017F10
		private Expression CreateReference(ResourceExpression resource)
		{
			this.referencedInputs.Add(resource);
			return resource.CreateReference();
		}

		// Token: 0x0400040B RID: 1035
		private readonly HashSet<ResourceExpression> referencedInputs = new HashSet<ResourceExpression>(EqualityComparer<ResourceExpression>.Default);

		// Token: 0x0400040C RID: 1036
		private readonly ResourceExpression input;

		// Token: 0x0400040D RID: 1037
		private readonly ResourceSetExpression inputSet;

		// Token: 0x0400040E RID: 1038
		private readonly ParameterExpression inputParameter;
	}
}

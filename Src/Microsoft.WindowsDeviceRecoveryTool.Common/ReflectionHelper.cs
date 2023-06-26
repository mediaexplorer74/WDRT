using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.Common
{
	// Token: 0x02000009 RID: 9
	[CompilerGenerated]
	public static class ReflectionHelper
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000026F8 File Offset: 0x000008F8
		public static string GetName<T>(this T instance, Expression<Func<T, object>> expression)
		{
			bool flag = expression == null;
			if (flag)
			{
				throw new ArgumentException("The expression cannot be null.");
			}
			return ReflectionHelper.GetName<T>(expression);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002724 File Offset: 0x00000924
		public static string GetName<T>(Expression<Func<T, object>> expression)
		{
			bool flag = expression == null;
			if (flag)
			{
				throw new ArgumentException("The expression cannot be null.");
			}
			return ReflectionHelper.GetName(expression.Body);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002758 File Offset: 0x00000958
		public static string GetName(this object instance, Expression<Action> expression)
		{
			bool flag = expression == null;
			if (flag)
			{
				throw new ArgumentException("The expression cannot be null.");
			}
			return ReflectionHelper.GetName(expression.Body);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000278C File Offset: 0x0000098C
		public static string GetName<T>(this object instance, Expression<Action<T>> expression)
		{
			bool flag = expression == null;
			if (flag)
			{
				throw new ArgumentException("The expression cannot be null.");
			}
			return ReflectionHelper.GetName(expression.Body);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027C0 File Offset: 0x000009C0
		public static string GetName<T>(this T instance, Expression<Action<T>> expression)
		{
			bool flag = expression == null;
			if (flag)
			{
				throw new ArgumentException("The expression cannot be null.");
			}
			return ReflectionHelper.GetName(expression.Body);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027F4 File Offset: 0x000009F4
		public static string GetName<T>(Expression<Action<T>> expression)
		{
			bool flag = expression == null;
			if (flag)
			{
				throw new ArgumentException("The expression cannot be null.");
			}
			return ReflectionHelper.GetName(expression.Body);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002828 File Offset: 0x00000A28
		public static string GetName<T>(Expression<Func<T>> expression)
		{
			bool flag = expression == null;
			if (flag)
			{
				throw new ArgumentException("The expression cannot be null.");
			}
			return ReflectionHelper.GetName(expression.Body);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000285C File Offset: 0x00000A5C
		private static string GetName(Expression expression)
		{
			bool flag = expression == null;
			if (flag)
			{
				throw new ArgumentException("The expression cannot be null.");
			}
			bool flag2 = expression is MemberExpression;
			string text;
			if (flag2)
			{
				MemberExpression memberExpression = (MemberExpression)expression;
				text = memberExpression.Member.Name;
			}
			else
			{
				bool flag3 = expression is MethodCallExpression;
				if (flag3)
				{
					MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
					text = methodCallExpression.Method.Name;
				}
				else
				{
					bool flag4 = expression is UnaryExpression;
					if (!flag4)
					{
						throw new ArgumentException("Invalid expression");
					}
					UnaryExpression unaryExpression = (UnaryExpression)expression;
					text = ReflectionHelper.GetName(unaryExpression);
				}
			}
			return text;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028F8 File Offset: 0x00000AF8
		private static string GetName(UnaryExpression unaryExpression)
		{
			bool flag = unaryExpression.Operand is MethodCallExpression;
			string text;
			if (flag)
			{
				MethodCallExpression methodCallExpression = (MethodCallExpression)unaryExpression.Operand;
				text = methodCallExpression.Method.Name;
			}
			else
			{
				text = ((MemberExpression)unaryExpression.Operand).Member.Name;
			}
			return text;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000294C File Offset: 0x00000B4C
		public static TAttr GetAttribute<TAttr>(this object currentObject) where TAttr : Attribute
		{
			Type type = currentObject.GetType();
			bool flag = type.IsDefined(typeof(TAttr), false);
			TAttr tattr;
			if (flag)
			{
				tattr = (TAttr)((object)type.GetCustomAttributes(typeof(TAttr), false).First<object>());
			}
			else
			{
				tattr = default(TAttr);
			}
			return tattr;
		}
	}
}

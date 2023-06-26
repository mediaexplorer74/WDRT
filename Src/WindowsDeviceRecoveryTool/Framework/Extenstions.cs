using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.Common;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x02000080 RID: 128
	public static class Extenstions
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x00016759 File Offset: 0x00014959
		public static void Run(this ICommand command)
		{
			command.Execute(null);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00016764 File Offset: 0x00014964
		public static void Run(this ICommand command, object parameter)
		{
			command.Execute(parameter);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00016770 File Offset: 0x00014970
		public static void Run(this IDictionary<string, IDelegateCommand> dictionary, Expression<Action> expression)
		{
			string name = dictionary.GetName(expression);
			Extenstions.Run(dictionary, expression.Body as MethodCallExpression, name, expression.Parameters);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000167A0 File Offset: 0x000149A0
		public static void Run<T>(this IDictionary<string, IDelegateCommand> dictionary, Expression<Action<T>> expression)
		{
			string name = ReflectionHelper.GetName<T>(expression);
			Extenstions.Run(dictionary, expression.Body as MethodCallExpression, name, expression.Parameters);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000167D0 File Offset: 0x000149D0
		public static void Run<T>(this IDictionary<string, IDelegateCommand> dictionary, Expression<Func<T>> expression)
		{
			string name = ReflectionHelper.GetName<T>(expression);
			Extenstions.Run(dictionary, expression.Body as MethodCallExpression, name, expression.Parameters);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00016800 File Offset: 0x00014A00
		public static void RunAsyncCommandSync<T>(this IDictionary<string, IDelegateCommand> dictionary, Expression<Action<T>> expression)
		{
			string name = ReflectionHelper.GetName<T>(expression);
			IAsyncDelegateCommand asyncDelegateCommand = dictionary[name] as IAsyncDelegateCommand;
			bool flag = asyncDelegateCommand != null;
			if (flag)
			{
				Extenstions.Run(dictionary, expression.Body as MethodCallExpression, name, expression.Parameters);
				asyncDelegateCommand.Wait();
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001684C File Offset: 0x00014A4C
		public static void RaiseCanExecuteChanged<T>(this IDictionary<string, IDelegateCommand> dictionary, Expression<Action<T>> expression)
		{
			string name = ReflectionHelper.GetName<T>(expression);
			dictionary[name].RaiseCanExecuteChanged();
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00016870 File Offset: 0x00014A70
		public static void RaiseCanExecuteChanged<T>(this IDictionary<string, IDelegateCommand> dictionary, Expression<Func<T>> expression)
		{
			string name = ReflectionHelper.GetName<T>(expression);
			dictionary[name].RaiseCanExecuteChanged();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00016894 File Offset: 0x00014A94
		public static T Get<T>(this ExportProvider container)
		{
			return container.GetExportedValue<T>();
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000168AC File Offset: 0x00014AAC
		public static T Get<T>(this ExportProvider container, string name)
		{
			return container.GetExportedValue<T>(name);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000168C8 File Offset: 0x00014AC8
		private static void Run(IDictionary<string, IDelegateCommand> dictionary, MethodCallExpression expressionBody, string commandName, IEnumerable<ParameterExpression> parameters)
		{
			bool flag = expressionBody.Arguments.Any<Expression>();
			if (flag)
			{
				Expression expression = expressionBody.Arguments[0];
				ConstantExpression constantExpression = expression as ConstantExpression;
				bool flag2 = constantExpression != null;
				if (flag2)
				{
					dictionary[commandName].Execute(constantExpression.Value);
					return;
				}
				bool flag3 = expression is MemberExpression || expression is NewExpression;
				if (flag3)
				{
					LambdaExpression lambdaExpression = Expression.Lambda(expression, parameters);
					Delegate @delegate = lambdaExpression.Compile();
					try
					{
						object obj = @delegate.DynamicInvoke(new object[expressionBody.Arguments.Count - 1]);
						dictionary[commandName].Execute(obj);
					}
					catch (TargetParameterCountException)
					{
						object obj2 = @delegate.DynamicInvoke(new object[expressionBody.Arguments.Count]);
						dictionary[commandName].Execute(obj2);
					}
					return;
				}
			}
			dictionary[commandName].Execute(null);
		}
	}
}

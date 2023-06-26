using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Windows.Input;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x02000085 RID: 133
	public class BaseController : IController
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x00016B4C File Offset: 0x00014D4C
		public BaseController(ICommandRepository commandRepository, EventAggregator eventAggregator)
		{
			this.commands = commandRepository;
			this.eventAggregator = eventAggregator;
			this.InitializeCommands();
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00016B6C File Offset: 0x00014D6C
		protected EventAggregator EventAggregator
		{
			get
			{
				return this.eventAggregator;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00016B84 File Offset: 0x00014D84
		protected ICommandRepository Commands
		{
			get
			{
				return this.commands;
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00016B9C File Offset: 0x00014D9C
		private static bool MethodHasAttribute<T>(MethodInfo method) where T : Attribute
		{
			return method.GetCustomAttributes(typeof(T), false).Any<object>();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00016BC4 File Offset: 0x00014DC4
		private static bool MethodTypeIsVoid(MethodInfo method)
		{
			return method.ReturnType == typeof(void);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00016BEC File Offset: 0x00014DEC
		private static bool MethodHasParameters(MethodInfo method, int number = 1)
		{
			return method.GetParameters().Count<ParameterInfo>() == number;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00016C0C File Offset: 0x00014E0C
		private bool MethodIsAsynchronous(MethodInfo method)
		{
			bool flag = BaseController.MethodHasAttribute<CustomCommandAttribute>(method);
			bool flag2;
			if (flag)
			{
				CustomCommandAttribute customCommandAttribute = method.GetCustomAttributes<CustomCommandAttribute>().FirstOrDefault<CustomCommandAttribute>();
				flag2 = customCommandAttribute.IsAsynchronous;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00016C40 File Offset: 0x00014E40
		private bool IsBadDesignedAsyncMethod(MethodInfo method)
		{
			bool flag = this.MethodIsAsynchronous(method);
			if (flag)
			{
				ParameterInfo[] parameters = method.GetParameters();
				bool flag2 = parameters.Count<ParameterInfo>() != 2 || !(parameters[1].ParameterType == typeof(CancellationToken));
				if (flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00016C98 File Offset: 0x00014E98
		private void InitializeCommands()
		{
			List<MethodInfo> list = base.GetType().GetMethods().ToList<MethodInfo>();
			List<MethodInfo> list2 = list.Where((MethodInfo method) => method.Name.StartsWith("CanExecute", StringComparison.Ordinal)).ToList<MethodInfo>();
			List<MethodInfo> list3 = list.Except(list2).ToList<MethodInfo>();
			foreach (MethodInfo methodInfo in list3)
			{
				bool flag = this.IsBadDesignedAsyncMethod(methodInfo);
				if (flag)
				{
					throw new MissingMethodException(string.Format("{0}.{1} method has wrong signature. It need to have two parameters and second should be CancellationToken.", base.GetType().Name, methodInfo.Name));
				}
				bool flag2 = BaseController.MethodHasAttribute<CustomCommandAttribute>(methodInfo) && BaseController.MethodTypeIsVoid(methodInfo) && methodInfo.GetParameters().Count<ParameterInfo>() < 2;
				if (flag2)
				{
					this.ProcessMethod(methodInfo, list2);
				}
				else
				{
					bool flag3 = this.MethodIsAsynchronous(methodInfo) && BaseController.MethodHasParameters(methodInfo, 2) && methodInfo.GetParameters()[1].ParameterType == typeof(CancellationToken);
					if (flag3)
					{
						this.ProcessAsyncMethod(methodInfo, list2);
					}
				}
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00016DE4 File Offset: 0x00014FE4
		private void ProcessMethod(MethodInfo method, List<MethodInfo> canMethods)
		{
			bool flag = BaseController.MethodHasParameters(method, 1);
			if (flag)
			{
				ParameterInfo parameterInfo = method.GetParameters().FirstOrDefault<ParameterInfo>();
				Type type = typeof(Action<>).MakeGenericType(new Type[] { parameterInfo.ParameterType });
				Type type2 = typeof(DelegateCommand<>).MakeGenericType(new Type[] { parameterInfo.ParameterType });
				Delegate @delegate = Delegate.CreateDelegate(type, this, method);
				Func<object, bool> canMethodInstance = this.GetCanMethodInstance(method, canMethods);
				KeyGesture keyGesture = this.GetKeyGesture(method);
				IDelegateCommand delegateCommand = (IDelegateCommand)Activator.CreateInstance(type2, new object[] { @delegate, canMethodInstance, keyGesture });
				this.Commands.Add(method.Name, delegateCommand);
			}
			else
			{
				bool flag2 = !method.GetParameters().Any<ParameterInfo>();
				if (flag2)
				{
					Func<object, bool> canMethodInstance2 = this.GetCanMethodInstance(method, canMethods);
					KeyGesture keyGesture2 = this.GetKeyGesture(method);
					this.Commands.Add(method.Name, new DelegateCommand<object>(delegate(object parameter)
					{
						method.Invoke(this, null);
					}, canMethodInstance2, keyGesture2));
				}
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00016F3C File Offset: 0x0001513C
		private void ProcessAsyncMethod(MethodInfo method, List<MethodInfo> canMethods)
		{
			ParameterInfo parameterInfo = method.GetParameters().FirstOrDefault<ParameterInfo>();
			Type type = typeof(Action<, >).MakeGenericType(new Type[]
			{
				parameterInfo.ParameterType,
				typeof(CancellationToken)
			});
			Type type2 = typeof(AsyncDelegateCommand<>).MakeGenericType(new Type[] { parameterInfo.ParameterType });
			Delegate @delegate = Delegate.CreateDelegate(type, this, method);
			Func<object, bool> canMethodInstance = this.GetCanMethodInstance(method, canMethods);
			KeyGesture keyGesture = this.GetKeyGesture(method);
			IAsyncDelegateCommand asyncDelegateCommand = (IAsyncDelegateCommand)Activator.CreateInstance(type2, new object[] { @delegate, canMethodInstance, keyGesture });
			this.Commands.Add(method.Name, asyncDelegateCommand);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00016FF4 File Offset: 0x000151F4
		private Func<object, bool> GetCanMethodInstance(MethodInfo method, IEnumerable<MethodInfo> canMethods)
		{
			MethodInfo methodInfo = canMethods.FirstOrDefault((MethodInfo cm) => cm.Name == "CanExecute" + method.Name);
			bool flag = methodInfo != null && BaseController.MethodHasParameters(methodInfo, 1) && methodInfo.ReturnType == typeof(bool);
			Func<object, bool> func;
			if (flag)
			{
				List<Type> list = (from p in methodInfo.GetParameters()
					select p.ParameterType).ToList<Type>();
				list.Add(methodInfo.ReturnType);
				Type funcType = Expression.GetFuncType(list.ToArray());
				func = (Func<object, bool>)Delegate.CreateDelegate(funcType, this, methodInfo);
			}
			else
			{
				func = null;
			}
			return func;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000170B4 File Offset: 0x000152B4
		private KeyGesture GetKeyGesture(MethodInfo method)
		{
			CustomCommandAttribute customCommandAttribute = (CustomCommandAttribute)method.GetCustomAttribute(typeof(CustomCommandAttribute));
			return customCommandAttribute.KeyGesture;
		}

		// Token: 0x0400021E RID: 542
		private readonly ICommandRepository commands;

		// Token: 0x0400021F RID: 543
		private readonly EventAggregator eventAggregator;
	}
}

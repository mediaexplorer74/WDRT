using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000039 RID: 57
	public class CallMethodAction : TriggerAction<DependencyObject>
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000848C File Offset: 0x0000668C
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00008499 File Offset: 0x00006699
		public object TargetObject
		{
			get
			{
				return base.GetValue(CallMethodAction.TargetObjectProperty);
			}
			set
			{
				base.SetValue(CallMethodAction.TargetObjectProperty, value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000084A7 File Offset: 0x000066A7
		// (set) Token: 0x060001FE RID: 510 RVA: 0x000084B9 File Offset: 0x000066B9
		public string MethodName
		{
			get
			{
				return (string)base.GetValue(CallMethodAction.MethodNameProperty);
			}
			set
			{
				base.SetValue(CallMethodAction.MethodNameProperty, value);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000084C7 File Offset: 0x000066C7
		public CallMethodAction()
		{
			this.methodDescriptors = new List<CallMethodAction.MethodDescriptor>();
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000084DA File Offset: 0x000066DA
		private object Target
		{
			get
			{
				return this.TargetObject ?? base.AssociatedObject;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000084EC File Offset: 0x000066EC
		protected override void Invoke(object parameter)
		{
			if (base.AssociatedObject != null)
			{
				CallMethodAction.MethodDescriptor methodDescriptor = this.FindBestMethod(parameter);
				if (methodDescriptor != null)
				{
					ParameterInfo[] parameters = methodDescriptor.Parameters;
					if (parameters.Length == 0)
					{
						methodDescriptor.MethodInfo.Invoke(this.Target, null);
						return;
					}
					if (parameters.Length == 2 && base.AssociatedObject != null && parameter != null && parameters[0].ParameterType.IsAssignableFrom(base.AssociatedObject.GetType()) && parameters[1].ParameterType.IsAssignableFrom(parameter.GetType()))
					{
						methodDescriptor.MethodInfo.Invoke(this.Target, new object[] { base.AssociatedObject, parameter });
						return;
					}
				}
				else if (this.TargetObject != null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.CallMethodActionValidMethodNotFoundExceptionMessage, new object[]
					{
						this.MethodName,
						this.TargetObject.GetType().Name
					}));
				}
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000085DF File Offset: 0x000067DF
		protected override void OnAttached()
		{
			base.OnAttached();
			this.UpdateMethodInfo();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000085ED File Offset: 0x000067ED
		protected override void OnDetaching()
		{
			this.methodDescriptors.Clear();
			base.OnDetaching();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008600 File Offset: 0x00006800
		private CallMethodAction.MethodDescriptor FindBestMethod(object parameter)
		{
			if (parameter != null)
			{
				parameter.GetType();
			}
			return this.methodDescriptors.FirstOrDefault((CallMethodAction.MethodDescriptor methodDescriptor) => !methodDescriptor.HasParameters || (parameter != null && methodDescriptor.SecondParameterType.IsAssignableFrom(parameter.GetType())));
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008648 File Offset: 0x00006848
		private void UpdateMethodInfo()
		{
			this.methodDescriptors.Clear();
			if (this.Target != null && !string.IsNullOrEmpty(this.MethodName))
			{
				foreach (MethodInfo methodInfo in this.Target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
				{
					if (this.IsMethodValid(methodInfo))
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (CallMethodAction.AreMethodParamsValid(parameters))
						{
							this.methodDescriptors.Add(new CallMethodAction.MethodDescriptor(methodInfo, parameters));
						}
					}
				}
				this.methodDescriptors = this.methodDescriptors.OrderByDescending(delegate(CallMethodAction.MethodDescriptor methodDescriptor)
				{
					int num = 0;
					if (methodDescriptor.HasParameters)
					{
						Type type = methodDescriptor.SecondParameterType;
						while (type != typeof(EventArgs))
						{
							num++;
							type = type.BaseType;
						}
					}
					return methodDescriptor.ParameterCount + num;
				}).ToList<CallMethodAction.MethodDescriptor>();
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000086FF File Offset: 0x000068FF
		private bool IsMethodValid(MethodInfo method)
		{
			return string.Equals(method.Name, this.MethodName, StringComparison.Ordinal) && !(method.ReturnType != typeof(void));
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008734 File Offset: 0x00006934
		private static bool AreMethodParamsValid(ParameterInfo[] methodParams)
		{
			if (methodParams.Length == 2)
			{
				if (methodParams[0].ParameterType != typeof(object))
				{
					return false;
				}
				if (!typeof(EventArgs).IsAssignableFrom(methodParams[1].ParameterType))
				{
					return false;
				}
			}
			else if (methodParams.Length != 0)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008784 File Offset: 0x00006984
		private static void OnMethodNameChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			((CallMethodAction)sender).UpdateMethodInfo();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008791 File Offset: 0x00006991
		private static void OnTargetObjectChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			((CallMethodAction)sender).UpdateMethodInfo();
		}

		// Token: 0x040000AC RID: 172
		private List<CallMethodAction.MethodDescriptor> methodDescriptors;

		// Token: 0x040000AD RID: 173
		public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register("TargetObject", typeof(object), typeof(CallMethodAction), new PropertyMetadata(new PropertyChangedCallback(CallMethodAction.OnTargetObjectChanged)));

		// Token: 0x040000AE RID: 174
		public static readonly DependencyProperty MethodNameProperty = DependencyProperty.Register("MethodName", typeof(string), typeof(CallMethodAction), new PropertyMetadata(new PropertyChangedCallback(CallMethodAction.OnMethodNameChanged)));

		// Token: 0x0200005B RID: 91
		private class MethodDescriptor
		{
			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000328 RID: 808 RVA: 0x0000CA80 File Offset: 0x0000AC80
			// (set) Token: 0x06000329 RID: 809 RVA: 0x0000CA88 File Offset: 0x0000AC88
			public MethodInfo MethodInfo { get; private set; }

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x0600032A RID: 810 RVA: 0x0000CA91 File Offset: 0x0000AC91
			public bool HasParameters
			{
				get
				{
					return this.Parameters.Length != 0;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x0600032B RID: 811 RVA: 0x0000CA9D File Offset: 0x0000AC9D
			public int ParameterCount
			{
				get
				{
					return this.Parameters.Length;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x0600032C RID: 812 RVA: 0x0000CAA7 File Offset: 0x0000ACA7
			// (set) Token: 0x0600032D RID: 813 RVA: 0x0000CAAF File Offset: 0x0000ACAF
			public ParameterInfo[] Parameters { get; private set; }

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x0600032E RID: 814 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
			public Type SecondParameterType
			{
				get
				{
					if (this.Parameters.Length >= 2)
					{
						return this.Parameters[1].ParameterType;
					}
					return null;
				}
			}

			// Token: 0x0600032F RID: 815 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
			public MethodDescriptor(MethodInfo methodInfo, ParameterInfo[] methodParams)
			{
				this.MethodInfo = methodInfo;
				this.Parameters = methodParams;
			}
		}
	}
}

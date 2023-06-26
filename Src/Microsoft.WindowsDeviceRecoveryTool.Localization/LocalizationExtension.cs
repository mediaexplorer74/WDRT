using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Microsoft.WindowsDeviceRecoveryTool.Localization
{
	// Token: 0x02000002 RID: 2
	public class LocalizationExtension : MarkupExtension, INotifyPropertyChanged
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public LocalizationExtension(string key, string arg1, string arg2)
		{
			this.key = key;
			this.parameterNames = new string[2];
			this.parameterNames[0] = arg1;
			this.parameterNames[1] = arg2;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207F File Offset: 0x0000027F
		public LocalizationExtension(string key, string arg1)
		{
			this.key = key;
			this.parameterNames = new string[1];
			this.parameterNames[0] = arg1;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A5 File Offset: 0x000002A5
		public LocalizationExtension(string key)
		{
			this.key = key;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000004 RID: 4 RVA: 0x000020B8 File Offset: 0x000002B8
		// (remove) Token: 0x06000005 RID: 5 RVA: 0x000020F0 File Offset: 0x000002F0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002128 File Offset: 0x00000328
		public object Value
		{
			get
			{
				bool flag = this.parameterValues == null;
				object obj;
				if (flag)
				{
					obj = this.translatedValue;
				}
				else
				{
					obj = string.Format(this.translatedValue, this.parameterValues);
				}
				return obj;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002164 File Offset: 0x00000364
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			this.frameworkElement = null;
			this.parameters = null;
			IProvideValueTarget provideValueTarget = serviceProvider as IProvideValueTarget;
			bool flag = provideValueTarget != null;
			if (flag)
			{
				this.frameworkElement = provideValueTarget.TargetObject as FrameworkElement;
				bool flag2 = this.frameworkElement != null;
				if (flag2)
				{
					bool flag3 = this.parameterNames != null;
					if (flag3)
					{
						bool flag4 = this.frameworkElement.DataContext == null;
						if (flag4)
						{
							this.frameworkElement.DataContextChanged += this.FrameworkElementOnDataContextChanged;
						}
						else
						{
							this.AssignParameters();
						}
					}
				}
			}
			Binding binding = new Binding("Value")
			{
				Source = this,
				Mode = BindingMode.OneWay
			};
			this.ConnectEvents();
			this.UpdateParameters();
			this.UpdateTranslation();
			return binding.ProvideValue(serviceProvider);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002238 File Offset: 0x00000438
		private void AssignParameters()
		{
			this.parameters = new PropertyInfo[this.parameterNames.Length];
			for (int i = 0; i < this.parameters.Length; i++)
			{
				PropertyInfo property = this.frameworkElement.DataContext.GetType().GetProperty(this.parameterNames[i]);
				bool flag = property != null;
				if (flag)
				{
					this.parameters[i] = property;
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022A6 File Offset: 0x000004A6
		private void FrameworkElementOnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			this.frameworkElement.DataContextChanged -= this.FrameworkElementOnDataContextChanged;
			this.AssignParameters();
			this.ConnectEvents();
			this.UpdateParameters();
			this.UpdateTranslation();
			this.OnValueChanged();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022E4 File Offset: 0x000004E4
		private void ConnectEvents()
		{
			bool flag = this.parameters != null;
			if (flag)
			{
				bool flag2 = this.parameters.Any((PropertyInfo t) => t != null);
				bool flag3 = flag2;
				if (flag3)
				{
					INotifyPropertyChanged notifyPropertyChanged = this.frameworkElement.DataContext as INotifyPropertyChanged;
					bool flag4 = notifyPropertyChanged != null;
					if (flag4)
					{
						notifyPropertyChanged.PropertyChanged += this.OnPropertyChanged;
					}
				}
			}
			LocalizationManager.Instance().LanguageChanged += this.OnLanguageChanged;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000237C File Offset: 0x0000057C
		private void UpdateParameters()
		{
			bool flag = this.parameters != null;
			if (flag)
			{
				this.parameterValues = new object[this.parameters.Length];
				for (int i = 0; i < this.parameters.Length; i++)
				{
					bool flag2 = this.parameters[i] != null;
					if (flag2)
					{
						this.parameterValues[i] = this.parameters[i].GetValue(this.frameworkElement.DataContext, null);
					}
					else
					{
						this.parameterValues[i] = "NOT FOUND";
					}
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000240B File Offset: 0x0000060B
		private void UpdateTranslation()
		{
			this.translatedValue = LocalizationManager.GetTranslation(this.key);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000241F File Offset: 0x0000061F
		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.UpdateParameters();
			this.OnValueChanged();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002430 File Offset: 0x00000630
		private void OnLanguageChanged(object sender, EventArgs e)
		{
			this.UpdateTranslation();
			this.OnValueChanged();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002444 File Offset: 0x00000644
		private void OnValueChanged()
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			bool flag = propertyChanged != null;
			if (flag)
			{
				propertyChanged(this, new PropertyChangedEventArgs("Value"));
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly string key;

		// Token: 0x04000002 RID: 2
		private readonly string[] parameterNames;

		// Token: 0x04000003 RID: 3
		private PropertyInfo[] parameters;

		// Token: 0x04000004 RID: 4
		private object[] parameterValues;

		// Token: 0x04000005 RID: 5
		private string translatedValue;

		// Token: 0x04000006 RID: 6
		private FrameworkElement frameworkElement;
	}
}

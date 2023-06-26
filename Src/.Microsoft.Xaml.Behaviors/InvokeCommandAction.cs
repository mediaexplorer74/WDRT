using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000015 RID: 21
	public sealed class InvokeCommandAction : TriggerAction<DependencyObject>
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003DB4 File Offset: 0x00001FB4
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003DC2 File Offset: 0x00001FC2
		public string CommandName
		{
			get
			{
				base.ReadPreamble();
				return this.commandName;
			}
			set
			{
				if (this.CommandName != value)
				{
					base.WritePreamble();
					this.commandName = value;
					base.WritePostscript();
				}
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003DE5 File Offset: 0x00001FE5
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003DF7 File Offset: 0x00001FF7
		public ICommand Command
		{
			get
			{
				return (ICommand)base.GetValue(InvokeCommandAction.CommandProperty);
			}
			set
			{
				base.SetValue(InvokeCommandAction.CommandProperty, value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003E05 File Offset: 0x00002005
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003E12 File Offset: 0x00002012
		public object CommandParameter
		{
			get
			{
				return base.GetValue(InvokeCommandAction.CommandParameterProperty);
			}
			set
			{
				base.SetValue(InvokeCommandAction.CommandParameterProperty, value);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003E20 File Offset: 0x00002020
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003E32 File Offset: 0x00002032
		public IValueConverter EventArgsConverter
		{
			get
			{
				return (IValueConverter)base.GetValue(InvokeCommandAction.EventArgsConverterProperty);
			}
			set
			{
				base.SetValue(InvokeCommandAction.EventArgsConverterProperty, value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003E40 File Offset: 0x00002040
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003E4D File Offset: 0x0000204D
		public object EventArgsConverterParameter
		{
			get
			{
				return base.GetValue(InvokeCommandAction.EventArgsConverterParameterProperty);
			}
			set
			{
				base.SetValue(InvokeCommandAction.EventArgsConverterParameterProperty, value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003E5B File Offset: 0x0000205B
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003E6D File Offset: 0x0000206D
		public string EventArgsParameterPath
		{
			get
			{
				return (string)base.GetValue(InvokeCommandAction.EventArgsParameterPathProperty);
			}
			set
			{
				base.SetValue(InvokeCommandAction.EventArgsParameterPathProperty, value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003E7B File Offset: 0x0000207B
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003E83 File Offset: 0x00002083
		public bool PassEventArgsToCommand { get; set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x00003E8C File Offset: 0x0000208C
		protected override void Invoke(object parameter)
		{
			if (base.AssociatedObject != null)
			{
				ICommand command = this.ResolveCommand();
				if (command != null)
				{
					object obj = this.CommandParameter;
					if (obj == null && !string.IsNullOrWhiteSpace(this.EventArgsParameterPath))
					{
						obj = this.GetEventArgsPropertyPathValue(parameter);
					}
					if (obj == null && this.EventArgsConverter != null)
					{
						obj = this.EventArgsConverter.Convert(parameter, typeof(object), this.EventArgsConverterParameter, CultureInfo.CurrentCulture);
					}
					if (obj == null && this.PassEventArgsToCommand)
					{
						obj = parameter;
					}
					if (command.CanExecute(obj))
					{
						command.Execute(obj);
					}
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003F14 File Offset: 0x00002114
		private object GetEventArgsPropertyPathValue(object parameter)
		{
			object obj = parameter;
			foreach (string text in this.EventArgsParameterPath.Split(new char[] { '.' }))
			{
				obj = obj.GetType().GetProperty(text).GetValue(obj, null);
			}
			return obj;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003F64 File Offset: 0x00002164
		private ICommand ResolveCommand()
		{
			ICommand command = null;
			if (this.Command != null)
			{
				command = this.Command;
			}
			else if (base.AssociatedObject != null)
			{
				foreach (PropertyInfo propertyInfo in base.AssociatedObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
				{
					if (typeof(ICommand).IsAssignableFrom(propertyInfo.PropertyType) && string.Equals(propertyInfo.Name, this.CommandName, StringComparison.Ordinal))
					{
						command = (ICommand)propertyInfo.GetValue(base.AssociatedObject, null);
					}
				}
			}
			return command;
		}

		// Token: 0x0400003E RID: 62
		private string commandName;

		// Token: 0x0400003F RID: 63
		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(InvokeCommandAction), null);

		// Token: 0x04000040 RID: 64
		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(InvokeCommandAction), null);

		// Token: 0x04000041 RID: 65
		public static readonly DependencyProperty EventArgsConverterProperty = DependencyProperty.Register("EventArgsConverter", typeof(IValueConverter), typeof(InvokeCommandAction), new PropertyMetadata(null));

		// Token: 0x04000042 RID: 66
		public static readonly DependencyProperty EventArgsConverterParameterProperty = DependencyProperty.Register("EventArgsConverterParameter", typeof(object), typeof(InvokeCommandAction), new PropertyMetadata(null));

		// Token: 0x04000043 RID: 67
		public static readonly DependencyProperty EventArgsParameterPathProperty = DependencyProperty.Register("EventArgsParameterPath", typeof(string), typeof(InvokeCommandAction), new PropertyMetadata(null));
	}
}

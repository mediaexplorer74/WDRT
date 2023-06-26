using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200001E RID: 30
	[DefaultTrigger(typeof(UIElement), typeof(EventTrigger), "MouseLeftButtonDown")]
	[DefaultTrigger(typeof(ButtonBase), typeof(EventTrigger), "Click")]
	public abstract class TriggerAction : Animatable, IAttachedObject
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004A48 File Offset: 0x00002C48
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00004A5A File Offset: 0x00002C5A
		public bool IsEnabled
		{
			get
			{
				return (bool)base.GetValue(TriggerAction.IsEnabledProperty);
			}
			set
			{
				base.SetValue(TriggerAction.IsEnabledProperty, value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004A6D File Offset: 0x00002C6D
		protected DependencyObject AssociatedObject
		{
			get
			{
				base.ReadPreamble();
				return this.associatedObject;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004A7B File Offset: 0x00002C7B
		protected virtual Type AssociatedObjectTypeConstraint
		{
			get
			{
				base.ReadPreamble();
				return this.associatedObjectTypeConstraint;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004A89 File Offset: 0x00002C89
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00004A97 File Offset: 0x00002C97
		internal bool IsHosted
		{
			get
			{
				base.ReadPreamble();
				return this.isHosted;
			}
			set
			{
				base.WritePreamble();
				this.isHosted = value;
				base.WritePostscript();
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004AAC File Offset: 0x00002CAC
		internal TriggerAction(Type associatedObjectTypeConstraint)
		{
			this.associatedObjectTypeConstraint = associatedObjectTypeConstraint;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004ABB File Offset: 0x00002CBB
		internal void CallInvoke(object parameter)
		{
			if (this.IsEnabled)
			{
				this.Invoke(parameter);
			}
		}

		// Token: 0x060000FF RID: 255
		protected abstract void Invoke(object parameter);

		// Token: 0x06000100 RID: 256 RVA: 0x00004ACC File Offset: 0x00002CCC
		protected virtual void OnAttached()
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004ACE File Offset: 0x00002CCE
		protected virtual void OnDetaching()
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004AD0 File Offset: 0x00002CD0
		protected override Freezable CreateInstanceCore()
		{
			return (Freezable)Activator.CreateInstance(base.GetType());
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004AE2 File Offset: 0x00002CE2
		DependencyObject IAttachedObject.AssociatedObject
		{
			get
			{
				return this.AssociatedObject;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004AEC File Offset: 0x00002CEC
		public void Attach(DependencyObject dependencyObject)
		{
			if (dependencyObject != this.AssociatedObject)
			{
				if (this.AssociatedObject != null)
				{
					throw new InvalidOperationException(ExceptionStringTable.CannotHostTriggerActionMultipleTimesExceptionMessage);
				}
				if (dependencyObject != null && !this.AssociatedObjectTypeConstraint.IsAssignableFrom(dependencyObject.GetType()))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.TypeConstraintViolatedExceptionMessage, new object[]
					{
						base.GetType().Name,
						dependencyObject.GetType().Name,
						this.AssociatedObjectTypeConstraint.Name
					}));
				}
				base.WritePreamble();
				this.associatedObject = dependencyObject;
				base.WritePostscript();
				this.OnAttached();
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004B8C File Offset: 0x00002D8C
		public void Detach()
		{
			this.OnDetaching();
			base.WritePreamble();
			this.associatedObject = null;
			base.WritePostscript();
		}

		// Token: 0x04000056 RID: 86
		private bool isHosted;

		// Token: 0x04000057 RID: 87
		private DependencyObject associatedObject;

		// Token: 0x04000058 RID: 88
		private Type associatedObjectTypeConstraint;

		// Token: 0x04000059 RID: 89
		public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(TriggerAction), new FrameworkPropertyMetadata(true));
	}
}

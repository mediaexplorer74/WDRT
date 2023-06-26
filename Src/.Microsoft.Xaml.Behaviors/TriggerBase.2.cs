using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000022 RID: 34
	[ContentProperty("Actions")]
	public abstract class TriggerBase : Animatable, IAttachedObject
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00004D10 File Offset: 0x00002F10
		internal TriggerBase(Type associatedObjectTypeConstraint)
		{
			this.associatedObjectTypeConstraint = associatedObjectTypeConstraint;
			TriggerActionCollection triggerActionCollection = new TriggerActionCollection();
			base.SetValue(TriggerBase.ActionsPropertyKey, triggerActionCollection);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004D3C File Offset: 0x00002F3C
		protected DependencyObject AssociatedObject
		{
			get
			{
				base.ReadPreamble();
				return this.associatedObject;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004D4A File Offset: 0x00002F4A
		protected virtual Type AssociatedObjectTypeConstraint
		{
			get
			{
				base.ReadPreamble();
				return this.associatedObjectTypeConstraint;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004D58 File Offset: 0x00002F58
		public TriggerActionCollection Actions
		{
			get
			{
				return (TriggerActionCollection)base.GetValue(TriggerBase.ActionsProperty);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000117 RID: 279 RVA: 0x00004D6C File Offset: 0x00002F6C
		// (remove) Token: 0x06000118 RID: 280 RVA: 0x00004DA4 File Offset: 0x00002FA4
		public event EventHandler<PreviewInvokeEventArgs> PreviewInvoke;

		// Token: 0x06000119 RID: 281 RVA: 0x00004DDC File Offset: 0x00002FDC
		protected void InvokeActions(object parameter)
		{
			if (this.PreviewInvoke != null)
			{
				PreviewInvokeEventArgs previewInvokeEventArgs = new PreviewInvokeEventArgs();
				this.PreviewInvoke(this, previewInvokeEventArgs);
				if (previewInvokeEventArgs.Cancelling)
				{
					return;
				}
			}
			foreach (TriggerAction triggerAction in this.Actions)
			{
				triggerAction.CallInvoke(parameter);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004E54 File Offset: 0x00003054
		protected virtual void OnAttached()
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004E56 File Offset: 0x00003056
		protected virtual void OnDetaching()
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004E58 File Offset: 0x00003058
		protected override Freezable CreateInstanceCore()
		{
			return (Freezable)Activator.CreateInstance(base.GetType());
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004E6A File Offset: 0x0000306A
		DependencyObject IAttachedObject.AssociatedObject
		{
			get
			{
				return this.AssociatedObject;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004E74 File Offset: 0x00003074
		public void Attach(DependencyObject dependencyObject)
		{
			if (dependencyObject != this.AssociatedObject)
			{
				if (this.AssociatedObject != null)
				{
					throw new InvalidOperationException(ExceptionStringTable.CannotHostTriggerMultipleTimesExceptionMessage);
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
				this.Actions.Attach(dependencyObject);
				this.OnAttached();
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004F20 File Offset: 0x00003120
		public void Detach()
		{
			this.OnDetaching();
			base.WritePreamble();
			this.associatedObject = null;
			base.WritePostscript();
			this.Actions.Detach();
		}

		// Token: 0x0400005B RID: 91
		private DependencyObject associatedObject;

		// Token: 0x0400005C RID: 92
		private Type associatedObjectTypeConstraint;

		// Token: 0x0400005D RID: 93
		private static readonly DependencyPropertyKey ActionsPropertyKey = DependencyProperty.RegisterReadOnly("Actions", typeof(TriggerActionCollection), typeof(TriggerBase), new FrameworkPropertyMetadata());

		// Token: 0x0400005E RID: 94
		public static readonly DependencyProperty ActionsProperty = TriggerBase.ActionsPropertyKey.DependencyProperty;
	}
}

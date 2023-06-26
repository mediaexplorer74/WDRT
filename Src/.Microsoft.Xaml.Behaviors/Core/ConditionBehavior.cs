using System;
using System.Windows;
using System.Windows.Markup;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x0200003F RID: 63
	[ContentProperty("Condition")]
	public class ConditionBehavior : Behavior<TriggerBase>
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00009358 File Offset: 0x00007558
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000936A File Offset: 0x0000756A
		public ICondition Condition
		{
			get
			{
				return (ICondition)base.GetValue(ConditionBehavior.ConditionProperty);
			}
			set
			{
				base.SetValue(ConditionBehavior.ConditionProperty, value);
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009380 File Offset: 0x00007580
		protected override void OnAttached()
		{
			base.OnAttached();
			base.AssociatedObject.PreviewInvoke += this.OnPreviewInvoke;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000939F File Offset: 0x0000759F
		protected override void OnDetaching()
		{
			base.AssociatedObject.PreviewInvoke -= this.OnPreviewInvoke;
			base.OnDetaching();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000093BE File Offset: 0x000075BE
		private void OnPreviewInvoke(object sender, PreviewInvokeEventArgs e)
		{
			if (this.Condition != null)
			{
				e.Cancelling = !this.Condition.Evaluate();
			}
		}

		// Token: 0x040000C2 RID: 194
		public static readonly DependencyProperty ConditionProperty = DependencyProperty.Register("Condition", typeof(ICondition), typeof(ConditionBehavior), new PropertyMetadata(null));
	}
}

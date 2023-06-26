using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000048 RID: 72
	public class PropertyChangedTrigger : TriggerBase<DependencyObject>
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000BD28 File Offset: 0x00009F28
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000BD35 File Offset: 0x00009F35
		public object Binding
		{
			get
			{
				return base.GetValue(PropertyChangedTrigger.BindingProperty);
			}
			set
			{
				base.SetValue(PropertyChangedTrigger.BindingProperty, value);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000BD43 File Offset: 0x00009F43
		protected virtual void EvaluateBindingChange(object args)
		{
			base.InvokeActions(args);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000BD4C File Offset: 0x00009F4C
		protected override void OnAttached()
		{
			base.OnAttached();
			base.PreviewInvoke += this.OnPreviewInvoke;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000BD66 File Offset: 0x00009F66
		protected override void OnDetaching()
		{
			base.PreviewInvoke -= this.OnPreviewInvoke;
			this.OnDetaching();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000BD80 File Offset: 0x00009F80
		private void OnPreviewInvoke(object sender, PreviewInvokeEventArgs e)
		{
			DataBindingHelper.EnsureDataBindingOnActionsUpToDate(this);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000BD88 File Offset: 0x00009F88
		private static void OnBindingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			((PropertyChangedTrigger)sender).EvaluateBindingChange(args);
		}

		// Token: 0x040000DC RID: 220
		public static readonly DependencyProperty BindingProperty = DependencyProperty.Register("Binding", typeof(object), typeof(PropertyChangedTrigger), new PropertyMetadata(new PropertyChangedCallback(PropertyChangedTrigger.OnBindingChanged)));
	}
}

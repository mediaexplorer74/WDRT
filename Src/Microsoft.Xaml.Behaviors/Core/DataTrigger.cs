using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000043 RID: 67
	public class DataTrigger : PropertyChangedTrigger
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000252 RID: 594 RVA: 0x000097F9 File Offset: 0x000079F9
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00009806 File Offset: 0x00007A06
		public object Value
		{
			get
			{
				return base.GetValue(DataTrigger.ValueProperty);
			}
			set
			{
				base.SetValue(DataTrigger.ValueProperty, value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009814 File Offset: 0x00007A14
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00009826 File Offset: 0x00007A26
		public ComparisonConditionType Comparison
		{
			get
			{
				return (ComparisonConditionType)base.GetValue(DataTrigger.ComparisonProperty);
			}
			set
			{
				base.SetValue(DataTrigger.ComparisonProperty, value);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009844 File Offset: 0x00007A44
		protected override void OnAttached()
		{
			base.OnAttached();
			FrameworkElement frameworkElement = base.AssociatedObject as FrameworkElement;
			if (frameworkElement != null)
			{
				frameworkElement.Loaded += this.OnElementLoaded;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009878 File Offset: 0x00007A78
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.UnsubscribeElementLoadedEvent();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009888 File Offset: 0x00007A88
		private void OnElementLoaded(object sender, RoutedEventArgs e)
		{
			try
			{
				this.EvaluateBindingChange(e);
			}
			finally
			{
				this.UnsubscribeElementLoadedEvent();
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000098B8 File Offset: 0x00007AB8
		private void UnsubscribeElementLoadedEvent()
		{
			FrameworkElement frameworkElement = base.AssociatedObject as FrameworkElement;
			if (frameworkElement != null)
			{
				frameworkElement.Loaded -= this.OnElementLoaded;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000098E6 File Offset: 0x00007AE6
		protected override void EvaluateBindingChange(object args)
		{
			if (this.Compare())
			{
				base.InvokeActions(args);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000098F7 File Offset: 0x00007AF7
		private static void OnValueChanged(object sender, DependencyPropertyChangedEventArgs args)
		{
			((DataTrigger)sender).EvaluateBindingChange(args);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000990A File Offset: 0x00007B0A
		private static void OnComparisonChanged(object sender, DependencyPropertyChangedEventArgs args)
		{
			((DataTrigger)sender).EvaluateBindingChange(args);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000991D File Offset: 0x00007B1D
		private bool Compare()
		{
			return base.AssociatedObject != null && ComparisonLogic.EvaluateImpl(base.Binding, this.Comparison, this.Value);
		}

		// Token: 0x040000C7 RID: 199
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(DataTrigger), new PropertyMetadata(new PropertyChangedCallback(DataTrigger.OnValueChanged)));

		// Token: 0x040000C8 RID: 200
		public static readonly DependencyProperty ComparisonProperty = DependencyProperty.Register("Comparison", typeof(ComparisonConditionType), typeof(DataTrigger), new PropertyMetadata(new PropertyChangedCallback(DataTrigger.OnComparisonChanged)));
	}
}

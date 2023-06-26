using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000D0 RID: 208
	public class TextBlockFixedWidthBehaviour : Behavior<TextBlock>
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001E6D4 File Offset: 0x0001C8D4
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0001E6F6 File Offset: 0x0001C8F6
		public string Text
		{
			get
			{
				return (string)base.GetValue(TextBlockFixedWidthBehaviour.TextProperty);
			}
			set
			{
				base.SetValue(TextBlockFixedWidthBehaviour.TextProperty, value);
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001E708 File Offset: 0x0001C908
		private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TextBlockFixedWidthBehaviour textBlockFixedWidthBehaviour = d as TextBlockFixedWidthBehaviour;
			bool flag = textBlockFixedWidthBehaviour != null;
			if (flag)
			{
				textBlockFixedWidthBehaviour.ChangeTextBlockSize(textBlockFixedWidthBehaviour.parent.ActualWidth);
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001E73C File Offset: 0x0001C93C
		protected override void OnAttached()
		{
			bool flag = base.AssociatedObject.Parent is FrameworkElement;
			if (flag)
			{
				this.parent = base.AssociatedObject.Parent as FrameworkElement;
				this.parent.SizeChanged += this.OnParentSizeChanged;
				Binding binding = new Binding("Text");
				binding.Source = base.AssociatedObject;
				BindingOperations.SetBinding(this, TextBlockFixedWidthBehaviour.TextProperty, binding);
			}
			base.OnAttached();
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
		private void OnParentSizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.ChangeTextBlockSize(e.NewSize.Width);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001E7E4 File Offset: 0x0001C9E4
		private void ChangeTextBlockSize(double parentWidth)
		{
			FormattedText formattedText = new FormattedText(base.AssociatedObject.Text, base.AssociatedObject.Language.GetEquivalentCulture(), base.AssociatedObject.FlowDirection, new Typeface(base.AssociatedObject.FontFamily, base.AssociatedObject.FontStyle, base.AssociatedObject.FontWeight, base.AssociatedObject.FontStretch), base.AssociatedObject.FontSize, base.AssociatedObject.Foreground);
			double num = parentWidth - 50.0;
			double num2 = Math.Min(num, formattedText.Width);
			base.AssociatedObject.Width = ((num2 > 0.0) ? num2 : 0.0);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001E8A2 File Offset: 0x0001CAA2
		protected override void OnDetaching()
		{
			this.parent.SizeChanged -= this.OnParentSizeChanged;
			this.parent = null;
			base.OnDetaching();
		}

		// Token: 0x040002F0 RID: 752
		private FrameworkElement parent = null;

		// Token: 0x040002F1 RID: 753
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextBlockFixedWidthBehaviour), new PropertyMetadata(null, new PropertyChangedCallback(TextBlockFixedWidthBehaviour.OnTextChanged)));
	}
}

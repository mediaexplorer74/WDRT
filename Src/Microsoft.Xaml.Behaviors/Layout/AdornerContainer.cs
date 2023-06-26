using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Microsoft.Xaml.Behaviors.Layout
{
	// Token: 0x02000033 RID: 51
	public class AdornerContainer : Adorner
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00006A1F File Offset: 0x00004C1F
		public AdornerContainer(UIElement adornedElement)
			: base(adornedElement)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006A28 File Offset: 0x00004C28
		protected override Size ArrangeOverride(Size finalSize)
		{
			if (this.child != null)
			{
				this.child.Arrange(new Rect(finalSize));
			}
			return finalSize;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006A44 File Offset: 0x00004C44
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00006A4C File Offset: 0x00004C4C
		public UIElement Child
		{
			get
			{
				return this.child;
			}
			set
			{
				base.AddVisualChild(value);
				this.child = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006A5C File Offset: 0x00004C5C
		protected override int VisualChildrenCount
		{
			get
			{
				if (this.child != null)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006A69 File Offset: 0x00004C69
		protected override Visual GetVisualChild(int index)
		{
			if (index != 0 || this.child == null)
			{
				return base.GetVisualChild(index);
			}
			return this.child;
		}

		// Token: 0x0400008A RID: 138
		private UIElement child;
	}
}

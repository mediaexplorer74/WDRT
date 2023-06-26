using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors.Core;

namespace Microsoft.Xaml.Behaviors.Layout
{
	// Token: 0x02000030 RID: 48
	public abstract class FluidMoveBehaviorBase : Behavior<FrameworkElement>
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000057EF File Offset: 0x000039EF
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00005801 File Offset: 0x00003A01
		public FluidMoveScope AppliesTo
		{
			get
			{
				return (FluidMoveScope)base.GetValue(FluidMoveBehaviorBase.AppliesToProperty);
			}
			set
			{
				base.SetValue(FluidMoveBehaviorBase.AppliesToProperty, value);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005814 File Offset: 0x00003A14
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00005826 File Offset: 0x00003A26
		public bool IsActive
		{
			get
			{
				return (bool)base.GetValue(FluidMoveBehaviorBase.IsActiveProperty);
			}
			set
			{
				base.SetValue(FluidMoveBehaviorBase.IsActiveProperty, value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005839 File Offset: 0x00003A39
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000584B File Offset: 0x00003A4B
		public TagType Tag
		{
			get
			{
				return (TagType)base.GetValue(FluidMoveBehaviorBase.TagProperty);
			}
			set
			{
				base.SetValue(FluidMoveBehaviorBase.TagProperty, value);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000585E File Offset: 0x00003A5E
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00005870 File Offset: 0x00003A70
		public string TagPath
		{
			get
			{
				return (string)base.GetValue(FluidMoveBehaviorBase.TagPathProperty);
			}
			set
			{
				base.SetValue(FluidMoveBehaviorBase.TagPathProperty, value);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000587E File Offset: 0x00003A7E
		protected static object GetIdentityTag(DependencyObject obj)
		{
			return obj.GetValue(FluidMoveBehaviorBase.IdentityTagProperty);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000588B File Offset: 0x00003A8B
		protected static void SetIdentityTag(DependencyObject obj, object value)
		{
			obj.SetValue(FluidMoveBehaviorBase.IdentityTagProperty, value);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005899 File Offset: 0x00003A99
		protected override void OnAttached()
		{
			base.OnAttached();
			base.AssociatedObject.LayoutUpdated += this.AssociatedObject_LayoutUpdated;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000058B8 File Offset: 0x00003AB8
		protected override void OnDetaching()
		{
			base.OnDetaching();
			base.AssociatedObject.LayoutUpdated -= this.AssociatedObject_LayoutUpdated;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000058D8 File Offset: 0x00003AD8
		private void AssociatedObject_LayoutUpdated(object sender, EventArgs e)
		{
			if (!this.IsActive)
			{
				return;
			}
			if (DateTime.Now - FluidMoveBehaviorBase.lastPurgeTick >= FluidMoveBehaviorBase.minTickDelta)
			{
				List<object> list = null;
				foreach (KeyValuePair<object, FluidMoveBehaviorBase.TagData> keyValuePair in FluidMoveBehaviorBase.TagDictionary)
				{
					if (keyValuePair.Value.Timestamp < FluidMoveBehaviorBase.nextToLastPurgeTick)
					{
						if (list == null)
						{
							list = new List<object>();
						}
						list.Add(keyValuePair.Key);
					}
				}
				if (list != null)
				{
					foreach (object obj in list)
					{
						FluidMoveBehaviorBase.TagDictionary.Remove(obj);
					}
				}
				FluidMoveBehaviorBase.nextToLastPurgeTick = FluidMoveBehaviorBase.lastPurgeTick;
				FluidMoveBehaviorBase.lastPurgeTick = DateTime.Now;
			}
			if (this.AppliesTo == FluidMoveScope.Self)
			{
				this.UpdateLayoutTransition(base.AssociatedObject);
				return;
			}
			Panel panel = base.AssociatedObject as Panel;
			if (panel != null)
			{
				foreach (object obj2 in panel.Children)
				{
					FrameworkElement frameworkElement = (FrameworkElement)obj2;
					this.UpdateLayoutTransition(frameworkElement);
				}
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005A50 File Offset: 0x00003C50
		private void UpdateLayoutTransition(FrameworkElement child)
		{
			if ((child.Visibility == Visibility.Collapsed || !child.IsLoaded) && this.ShouldSkipInitialLayout)
			{
				return;
			}
			FrameworkElement visualRoot = FluidMoveBehaviorBase.GetVisualRoot(child);
			FluidMoveBehaviorBase.TagData tagData = new FluidMoveBehaviorBase.TagData();
			tagData.Parent = VisualTreeHelper.GetParent(child) as FrameworkElement;
			tagData.ParentRect = ExtendedVisualStateManager.GetLayoutRect(child);
			tagData.Child = child;
			tagData.Timestamp = DateTime.Now;
			try
			{
				tagData.AppRect = FluidMoveBehaviorBase.TranslateRect(tagData.ParentRect, tagData.Parent, visualRoot);
			}
			catch (ArgumentException)
			{
				if (this.ShouldSkipInitialLayout)
				{
					return;
				}
			}
			this.EnsureTags(child);
			object obj = FluidMoveBehaviorBase.GetIdentityTag(child);
			if (obj == null)
			{
				obj = child;
			}
			this.UpdateLayoutTransitionCore(child, visualRoot, obj, tagData);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005B08 File Offset: 0x00003D08
		protected virtual bool ShouldSkipInitialLayout
		{
			get
			{
				return this.Tag == TagType.DataContext;
			}
		}

		// Token: 0x0600016A RID: 362
		internal abstract void UpdateLayoutTransitionCore(FrameworkElement child, FrameworkElement root, object tag, FluidMoveBehaviorBase.TagData newTagData);

		// Token: 0x0600016B RID: 363 RVA: 0x00005B13 File Offset: 0x00003D13
		protected virtual void EnsureTags(FrameworkElement child)
		{
			if (this.Tag == TagType.DataContext && !(child.ReadLocalValue(FluidMoveBehaviorBase.IdentityTagProperty) is BindingExpression))
			{
				child.SetBinding(FluidMoveBehaviorBase.IdentityTagProperty, new Binding(this.TagPath));
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005B48 File Offset: 0x00003D48
		private static FrameworkElement GetVisualRoot(FrameworkElement child)
		{
			for (;;)
			{
				FrameworkElement frameworkElement = VisualTreeHelper.GetParent(child) as FrameworkElement;
				if (frameworkElement == null)
				{
					break;
				}
				if (AdornerLayer.GetAdornerLayer(frameworkElement) == null)
				{
					return child;
				}
				child = frameworkElement;
			}
			return child;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005B74 File Offset: 0x00003D74
		internal static Rect TranslateRect(Rect rect, FrameworkElement from, FrameworkElement to)
		{
			if (from == null || to == null)
			{
				return rect;
			}
			Point point = new Point(rect.Left, rect.Top);
			point = from.TransformToVisual(to).Transform(point);
			return new Rect(point.X, point.Y, rect.Width, rect.Height);
		}

		// Token: 0x04000076 RID: 118
		public static readonly DependencyProperty AppliesToProperty = DependencyProperty.Register("AppliesTo", typeof(FluidMoveScope), typeof(FluidMoveBehaviorBase), new PropertyMetadata(FluidMoveScope.Self));

		// Token: 0x04000077 RID: 119
		public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(FluidMoveBehaviorBase), new PropertyMetadata(true));

		// Token: 0x04000078 RID: 120
		public static readonly DependencyProperty TagProperty = DependencyProperty.Register("Tag", typeof(TagType), typeof(FluidMoveBehaviorBase), new PropertyMetadata(TagType.Element));

		// Token: 0x04000079 RID: 121
		public static readonly DependencyProperty TagPathProperty = DependencyProperty.Register("TagPath", typeof(string), typeof(FluidMoveBehaviorBase), new PropertyMetadata(string.Empty));

		// Token: 0x0400007A RID: 122
		protected static readonly DependencyProperty IdentityTagProperty = DependencyProperty.RegisterAttached("IdentityTag", typeof(object), typeof(FluidMoveBehaviorBase), new PropertyMetadata(null));

		// Token: 0x0400007B RID: 123
		internal static Dictionary<object, FluidMoveBehaviorBase.TagData> TagDictionary = new Dictionary<object, FluidMoveBehaviorBase.TagData>();

		// Token: 0x0400007C RID: 124
		private static DateTime nextToLastPurgeTick = DateTime.MinValue;

		// Token: 0x0400007D RID: 125
		private static DateTime lastPurgeTick = DateTime.MinValue;

		// Token: 0x0400007E RID: 126
		private static TimeSpan minTickDelta = TimeSpan.FromSeconds(0.5);

		// Token: 0x02000059 RID: 89
		internal class TagData
		{
			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000319 RID: 793 RVA: 0x0000C967 File Offset: 0x0000AB67
			// (set) Token: 0x0600031A RID: 794 RVA: 0x0000C96F File Offset: 0x0000AB6F
			public FrameworkElement Child { get; set; }

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x0600031B RID: 795 RVA: 0x0000C978 File Offset: 0x0000AB78
			// (set) Token: 0x0600031C RID: 796 RVA: 0x0000C980 File Offset: 0x0000AB80
			public FrameworkElement Parent { get; set; }

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600031D RID: 797 RVA: 0x0000C989 File Offset: 0x0000AB89
			// (set) Token: 0x0600031E RID: 798 RVA: 0x0000C991 File Offset: 0x0000AB91
			public Rect ParentRect { get; set; }

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x0600031F RID: 799 RVA: 0x0000C99A File Offset: 0x0000AB9A
			// (set) Token: 0x06000320 RID: 800 RVA: 0x0000C9A2 File Offset: 0x0000ABA2
			public Rect AppRect { get; set; }

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000321 RID: 801 RVA: 0x0000C9AB File Offset: 0x0000ABAB
			// (set) Token: 0x06000322 RID: 802 RVA: 0x0000C9B3 File Offset: 0x0000ABB3
			public DateTime Timestamp { get; set; }

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000323 RID: 803 RVA: 0x0000C9BC File Offset: 0x0000ABBC
			// (set) Token: 0x06000324 RID: 804 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
			public object InitialTag { get; set; }
		}
	}
}

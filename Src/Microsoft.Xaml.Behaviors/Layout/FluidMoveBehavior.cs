using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Microsoft.Xaml.Behaviors.Layout
{
	// Token: 0x02000032 RID: 50
	public sealed class FluidMoveBehavior : FluidMoveBehaviorBase
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005D6F File Offset: 0x00003F6F
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00005D81 File Offset: 0x00003F81
		public Duration Duration
		{
			get
			{
				return (Duration)base.GetValue(FluidMoveBehavior.DurationProperty);
			}
			set
			{
				base.SetValue(FluidMoveBehavior.DurationProperty, value);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005D94 File Offset: 0x00003F94
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00005DA6 File Offset: 0x00003FA6
		public TagType InitialTag
		{
			get
			{
				return (TagType)base.GetValue(FluidMoveBehavior.InitialTagProperty);
			}
			set
			{
				base.SetValue(FluidMoveBehavior.InitialTagProperty, value);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00005DB9 File Offset: 0x00003FB9
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00005DCB File Offset: 0x00003FCB
		public string InitialTagPath
		{
			get
			{
				return (string)base.GetValue(FluidMoveBehavior.InitialTagPathProperty);
			}
			set
			{
				base.SetValue(FluidMoveBehavior.InitialTagPathProperty, value);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005DD9 File Offset: 0x00003FD9
		private static object GetInitialIdentityTag(DependencyObject obj)
		{
			return obj.GetValue(FluidMoveBehavior.initialIdentityTagProperty);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005DE6 File Offset: 0x00003FE6
		private static void SetInitialIdentityTag(DependencyObject obj, object value)
		{
			obj.SetValue(FluidMoveBehavior.initialIdentityTagProperty, value);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005DF4 File Offset: 0x00003FF4
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00005E06 File Offset: 0x00004006
		public bool FloatAbove
		{
			get
			{
				return (bool)base.GetValue(FluidMoveBehavior.FloatAboveProperty);
			}
			set
			{
				base.SetValue(FluidMoveBehavior.FloatAboveProperty, value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005E19 File Offset: 0x00004019
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00005E2B File Offset: 0x0000402B
		public IEasingFunction EaseX
		{
			get
			{
				return (IEasingFunction)base.GetValue(FluidMoveBehavior.EaseXProperty);
			}
			set
			{
				base.SetValue(FluidMoveBehavior.EaseXProperty, value);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005E39 File Offset: 0x00004039
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00005E4B File Offset: 0x0000404B
		public IEasingFunction EaseY
		{
			get
			{
				return (IEasingFunction)base.GetValue(FluidMoveBehavior.EaseYProperty);
			}
			set
			{
				base.SetValue(FluidMoveBehavior.EaseYProperty, value);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005E59 File Offset: 0x00004059
		private static object GetOverlay(DependencyObject obj)
		{
			return obj.GetValue(FluidMoveBehavior.overlayProperty);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005E66 File Offset: 0x00004066
		private static void SetOverlay(DependencyObject obj, object value)
		{
			obj.SetValue(FluidMoveBehavior.overlayProperty, value);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005E74 File Offset: 0x00004074
		private static object GetCacheDuringOverlay(DependencyObject obj)
		{
			return obj.GetValue(FluidMoveBehavior.cacheDuringOverlayProperty);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005E81 File Offset: 0x00004081
		private static void SetCacheDuringOverlay(DependencyObject obj, object value)
		{
			obj.SetValue(FluidMoveBehavior.cacheDuringOverlayProperty, value);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005E8F File Offset: 0x0000408F
		private static bool GetHasTransformWrapper(DependencyObject obj)
		{
			return (bool)obj.GetValue(FluidMoveBehavior.hasTransformWrapperProperty);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005EA1 File Offset: 0x000040A1
		private static void SetHasTransformWrapper(DependencyObject obj, bool value)
		{
			obj.SetValue(FluidMoveBehavior.hasTransformWrapperProperty, value);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00005EB4 File Offset: 0x000040B4
		protected override bool ShouldSkipInitialLayout
		{
			get
			{
				return base.ShouldSkipInitialLayout || this.InitialTag == TagType.DataContext;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005EC9 File Offset: 0x000040C9
		protected override void EnsureTags(FrameworkElement child)
		{
			base.EnsureTags(child);
			if (this.InitialTag == TagType.DataContext && !(child.ReadLocalValue(FluidMoveBehavior.initialIdentityTagProperty) is BindingExpression))
			{
				child.SetBinding(FluidMoveBehavior.initialIdentityTagProperty, new Binding(this.InitialTagPath));
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005F04 File Offset: 0x00004104
		internal override void UpdateLayoutTransitionCore(FrameworkElement child, FrameworkElement root, object tag, FluidMoveBehaviorBase.TagData newTagData)
		{
			bool flag = false;
			bool flag2 = false;
			object initialIdentityTag = FluidMoveBehavior.GetInitialIdentityTag(child);
			FluidMoveBehaviorBase.TagData tagData;
			bool flag3 = FluidMoveBehaviorBase.TagDictionary.TryGetValue(tag, out tagData);
			if (flag3 && tagData.InitialTag != initialIdentityTag)
			{
				flag3 = false;
				FluidMoveBehaviorBase.TagDictionary.Remove(tag);
			}
			Rect rect;
			if (!flag3)
			{
				FluidMoveBehaviorBase.TagData tagData2;
				if (initialIdentityTag != null && FluidMoveBehaviorBase.TagDictionary.TryGetValue(initialIdentityTag, out tagData2))
				{
					rect = FluidMoveBehaviorBase.TranslateRect(tagData2.AppRect, root, newTagData.Parent);
					flag = true;
					flag2 = true;
				}
				else
				{
					rect = Rect.Empty;
				}
				tagData = new FluidMoveBehaviorBase.TagData
				{
					ParentRect = Rect.Empty,
					AppRect = Rect.Empty,
					Parent = newTagData.Parent,
					Child = child,
					Timestamp = DateTime.Now,
					InitialTag = initialIdentityTag
				};
				FluidMoveBehaviorBase.TagDictionary.Add(tag, tagData);
			}
			else if (tagData.Parent != VisualTreeHelper.GetParent(child))
			{
				rect = FluidMoveBehaviorBase.TranslateRect(tagData.AppRect, root, newTagData.Parent);
				flag = true;
			}
			else
			{
				rect = tagData.ParentRect;
			}
			FrameworkElement originalChild = child;
			if ((!FluidMoveBehavior.IsEmptyRect(rect) && !FluidMoveBehavior.IsEmptyRect(newTagData.ParentRect) && (!FluidMoveBehavior.IsClose(rect.Left, newTagData.ParentRect.Left) || !FluidMoveBehavior.IsClose(rect.Top, newTagData.ParentRect.Top))) || (child != tagData.Child && FluidMoveBehavior.transitionStoryboardDictionary.ContainsKey(tag)))
			{
				Rect rect2 = rect;
				bool flag4 = false;
				Storyboard storyboard = null;
				if (FluidMoveBehavior.transitionStoryboardDictionary.TryGetValue(tag, out storyboard))
				{
					object overlay2 = FluidMoveBehavior.GetOverlay(tagData.Child);
					AdornerContainer adornerContainer = (AdornerContainer)overlay2;
					flag4 = overlay2 != null;
					FrameworkElement frameworkElement = tagData.Child;
					if (overlay2 != null)
					{
						Canvas canvas = adornerContainer.Child as Canvas;
						if (canvas != null)
						{
							frameworkElement = canvas.Children[0] as FrameworkElement;
						}
					}
					if (!flag2)
					{
						rect2 = FluidMoveBehavior.GetTransform(frameworkElement).TransformBounds(rect2);
					}
					FluidMoveBehavior.transitionStoryboardDictionary.Remove(tag);
					storyboard.Stop();
					storyboard = null;
					FluidMoveBehavior.RemoveTransform(frameworkElement);
					if (overlay2 != null)
					{
						AdornerLayer.GetAdornerLayer(root).Remove(adornerContainer);
						FluidMoveBehavior.TransferLocalValue(tagData.Child, FluidMoveBehavior.cacheDuringOverlayProperty, UIElement.RenderTransformProperty);
						FluidMoveBehavior.SetOverlay(tagData.Child, null);
					}
				}
				object overlay = null;
				if (flag4 || (flag && this.FloatAbove))
				{
					Canvas canvas2 = new Canvas
					{
						Width = newTagData.ParentRect.Width,
						Height = newTagData.ParentRect.Height,
						IsHitTestVisible = false
					};
					Rectangle rectangle = new Rectangle
					{
						Width = newTagData.ParentRect.Width,
						Height = newTagData.ParentRect.Height,
						IsHitTestVisible = false
					};
					rectangle.Fill = new VisualBrush(child);
					canvas2.Children.Add(rectangle);
					AdornerContainer adornerContainer2 = new AdornerContainer(child)
					{
						Child = canvas2
					};
					overlay = adornerContainer2;
					FluidMoveBehavior.SetOverlay(originalChild, overlay);
					AdornerLayer.GetAdornerLayer(root).Add(adornerContainer2);
					FluidMoveBehavior.TransferLocalValue(child, UIElement.RenderTransformProperty, FluidMoveBehavior.cacheDuringOverlayProperty);
					child.RenderTransform = new TranslateTransform(-10000.0, -10000.0);
					canvas2.RenderTransform = new TranslateTransform(10000.0, 10000.0);
					child = rectangle;
				}
				Rect parentRect = newTagData.ParentRect;
				Storyboard transitionStoryboard = this.CreateTransitionStoryboard(child, flag2, ref parentRect, ref rect2);
				FluidMoveBehavior.transitionStoryboardDictionary.Add(tag, transitionStoryboard);
				transitionStoryboard.Completed += delegate(object sender, EventArgs e)
				{
					Storyboard storyboard2;
					if (FluidMoveBehavior.transitionStoryboardDictionary.TryGetValue(tag, out storyboard2) && storyboard2 == transitionStoryboard)
					{
						FluidMoveBehavior.transitionStoryboardDictionary.Remove(tag);
						transitionStoryboard.Stop();
						FluidMoveBehavior.RemoveTransform(child);
						child.InvalidateMeasure();
						if (overlay != null)
						{
							AdornerLayer.GetAdornerLayer(root).Remove((AdornerContainer)overlay);
							FluidMoveBehavior.TransferLocalValue(originalChild, FluidMoveBehavior.cacheDuringOverlayProperty, UIElement.RenderTransformProperty);
							FluidMoveBehavior.SetOverlay(originalChild, null);
						}
					}
				};
				transitionStoryboard.Begin();
			}
			tagData.ParentRect = newTagData.ParentRect;
			tagData.AppRect = newTagData.AppRect;
			tagData.Parent = newTagData.Parent;
			tagData.Child = newTagData.Child;
			tagData.Timestamp = newTagData.Timestamp;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000638C File Offset: 0x0000458C
		private Storyboard CreateTransitionStoryboard(FrameworkElement child, bool usingBeforeLoaded, ref Rect layoutRect, ref Rect currentRect)
		{
			Duration duration = this.Duration;
			Storyboard storyboard = new Storyboard();
			storyboard.Duration = duration;
			double num = ((!usingBeforeLoaded || layoutRect.Width == 0.0) ? 1.0 : (currentRect.Width / layoutRect.Width));
			double num2 = ((!usingBeforeLoaded || layoutRect.Height == 0.0) ? 1.0 : (currentRect.Height / layoutRect.Height));
			double num3 = currentRect.Left - layoutRect.Left;
			double num4 = currentRect.Top - layoutRect.Top;
			FluidMoveBehavior.AddTransform(child, new TransformGroup
			{
				Children = 
				{
					new ScaleTransform
					{
						ScaleX = num,
						ScaleY = num2
					},
					new TranslateTransform
					{
						X = num3,
						Y = num4
					}
				}
			});
			string text = "(FrameworkElement.RenderTransform).";
			TransformGroup transformGroup = child.RenderTransform as TransformGroup;
			if (transformGroup != null && FluidMoveBehavior.GetHasTransformWrapper(child))
			{
				text = text + "(TransformGroup.Children)[" + (transformGroup.Children.Count - 1).ToString() + "].";
			}
			if (usingBeforeLoaded)
			{
				if (num != 1.0)
				{
					DoubleAnimation doubleAnimation = new DoubleAnimation
					{
						Duration = duration,
						From = new double?(num),
						To = new double?(1.0)
					};
					Storyboard.SetTarget(doubleAnimation, child);
					Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(text + "(TransformGroup.Children)[0].(ScaleTransform.ScaleX)", new object[0]));
					doubleAnimation.EasingFunction = this.EaseX;
					storyboard.Children.Add(doubleAnimation);
				}
				if (num2 != 1.0)
				{
					DoubleAnimation doubleAnimation2 = new DoubleAnimation
					{
						Duration = duration,
						From = new double?(num2),
						To = new double?(1.0)
					};
					Storyboard.SetTarget(doubleAnimation2, child);
					Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath(text + "(TransformGroup.Children)[0].(ScaleTransform.ScaleY)", new object[0]));
					doubleAnimation2.EasingFunction = this.EaseY;
					storyboard.Children.Add(doubleAnimation2);
				}
			}
			if (num3 != 0.0)
			{
				DoubleAnimation doubleAnimation3 = new DoubleAnimation
				{
					Duration = duration,
					From = new double?(num3),
					To = new double?(0.0)
				};
				Storyboard.SetTarget(doubleAnimation3, child);
				Storyboard.SetTargetProperty(doubleAnimation3, new PropertyPath(text + "(TransformGroup.Children)[1].(TranslateTransform.X)", new object[0]));
				doubleAnimation3.EasingFunction = this.EaseX;
				storyboard.Children.Add(doubleAnimation3);
			}
			if (num4 != 0.0)
			{
				DoubleAnimation doubleAnimation4 = new DoubleAnimation
				{
					Duration = duration,
					From = new double?(num4),
					To = new double?(0.0)
				};
				Storyboard.SetTarget(doubleAnimation4, child);
				Storyboard.SetTargetProperty(doubleAnimation4, new PropertyPath(text + "(TransformGroup.Children)[1].(TranslateTransform.Y)", new object[0]));
				doubleAnimation4.EasingFunction = this.EaseY;
				storyboard.Children.Add(doubleAnimation4);
			}
			return storyboard;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000066B8 File Offset: 0x000048B8
		private static void AddTransform(FrameworkElement child, Transform transform)
		{
			TransformGroup transformGroup = child.RenderTransform as TransformGroup;
			if (transformGroup == null)
			{
				transformGroup = new TransformGroup();
				transformGroup.Children.Add(child.RenderTransform);
				child.RenderTransform = transformGroup;
				FluidMoveBehavior.SetHasTransformWrapper(child, true);
			}
			transformGroup.Children.Add(transform);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006708 File Offset: 0x00004908
		private static Transform GetTransform(FrameworkElement child)
		{
			TransformGroup transformGroup = child.RenderTransform as TransformGroup;
			if (transformGroup != null && transformGroup.Children.Count > 0)
			{
				return transformGroup.Children[transformGroup.Children.Count - 1];
			}
			return new TranslateTransform();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006750 File Offset: 0x00004950
		private static void RemoveTransform(FrameworkElement child)
		{
			TransformGroup transformGroup = child.RenderTransform as TransformGroup;
			if (transformGroup != null)
			{
				if (FluidMoveBehavior.GetHasTransformWrapper(child))
				{
					child.RenderTransform = transformGroup.Children[0];
					FluidMoveBehavior.SetHasTransformWrapper(child, false);
					return;
				}
				transformGroup.Children.RemoveAt(transformGroup.Children.Count - 1);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000067A8 File Offset: 0x000049A8
		private static void TransferLocalValue(FrameworkElement element, DependencyProperty source, DependencyProperty dest)
		{
			object obj = element.ReadLocalValue(source);
			BindingExpressionBase bindingExpressionBase = obj as BindingExpressionBase;
			if (bindingExpressionBase != null)
			{
				element.SetBinding(dest, bindingExpressionBase.ParentBindingBase);
			}
			else if (obj == DependencyProperty.UnsetValue)
			{
				element.ClearValue(dest);
			}
			else
			{
				element.SetValue(dest, element.GetAnimationBaseValue(source));
			}
			element.ClearValue(source);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000067FD File Offset: 0x000049FD
		private static bool IsClose(double a, double b)
		{
			return Math.Abs(a - b) < 1E-07;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006813 File Offset: 0x00004A13
		private static bool IsEmptyRect(Rect rect)
		{
			return rect.IsEmpty || double.IsNaN(rect.Left) || double.IsNaN(rect.Top);
		}

		// Token: 0x0400007F RID: 127
		public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(Duration), typeof(FluidMoveBehavior), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(1.0))));

		// Token: 0x04000080 RID: 128
		public static readonly DependencyProperty InitialTagProperty = DependencyProperty.Register("InitialTag", typeof(TagType), typeof(FluidMoveBehavior), new PropertyMetadata(TagType.Element));

		// Token: 0x04000081 RID: 129
		public static readonly DependencyProperty InitialTagPathProperty = DependencyProperty.Register("InitialTagPath", typeof(string), typeof(FluidMoveBehavior), new PropertyMetadata(string.Empty));

		// Token: 0x04000082 RID: 130
		private static readonly DependencyProperty initialIdentityTagProperty = DependencyProperty.RegisterAttached("InitialIdentityTag", typeof(object), typeof(FluidMoveBehavior), new PropertyMetadata(null));

		// Token: 0x04000083 RID: 131
		public static readonly DependencyProperty FloatAboveProperty = DependencyProperty.Register("FloatAbove", typeof(bool), typeof(FluidMoveBehavior), new PropertyMetadata(true));

		// Token: 0x04000084 RID: 132
		public static readonly DependencyProperty EaseXProperty = DependencyProperty.Register("EaseX", typeof(IEasingFunction), typeof(FluidMoveBehavior), new PropertyMetadata(null));

		// Token: 0x04000085 RID: 133
		public static readonly DependencyProperty EaseYProperty = DependencyProperty.Register("EaseY", typeof(IEasingFunction), typeof(FluidMoveBehavior), new PropertyMetadata(null));

		// Token: 0x04000086 RID: 134
		private static readonly DependencyProperty overlayProperty = DependencyProperty.RegisterAttached("Overlay", typeof(object), typeof(FluidMoveBehavior), new PropertyMetadata(null));

		// Token: 0x04000087 RID: 135
		private static readonly DependencyProperty cacheDuringOverlayProperty = DependencyProperty.RegisterAttached("CacheDuringOverlay", typeof(object), typeof(FluidMoveBehavior), new PropertyMetadata(null));

		// Token: 0x04000088 RID: 136
		private static readonly DependencyProperty hasTransformWrapperProperty = DependencyProperty.RegisterAttached("HasTransformWrapper", typeof(bool), typeof(FluidMoveBehavior), new PropertyMetadata(false));

		// Token: 0x04000089 RID: 137
		private static Dictionary<object, Storyboard> transitionStoryboardDictionary = new Dictionary<object, Storyboard>();
	}
}

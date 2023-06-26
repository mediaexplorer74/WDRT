using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using Microsoft.Xaml.Behaviors.Media;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000044 RID: 68
	public class ExtendedVisualStateManager : VisualStateManager
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000099B5 File Offset: 0x00007BB5
		public static bool IsRunningFluidLayoutTransition
		{
			get
			{
				return ExtendedVisualStateManager.LayoutTransitionStoryboard != null;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000099BF File Offset: 0x00007BBF
		public static bool GetUseFluidLayout(DependencyObject obj)
		{
			return (bool)obj.GetValue(ExtendedVisualStateManager.UseFluidLayoutProperty);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000099D1 File Offset: 0x00007BD1
		public static void SetUseFluidLayout(DependencyObject obj, bool value)
		{
			obj.SetValue(ExtendedVisualStateManager.UseFluidLayoutProperty, value);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000099E4 File Offset: 0x00007BE4
		public static DependencyProperty GetRuntimeVisibilityProperty(DependencyObject obj)
		{
			return (DependencyProperty)obj.GetValue(ExtendedVisualStateManager.RuntimeVisibilityPropertyProperty);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000099F6 File Offset: 0x00007BF6
		public static void SetRuntimeVisibilityProperty(DependencyObject obj, DependencyProperty value)
		{
			obj.SetValue(ExtendedVisualStateManager.RuntimeVisibilityPropertyProperty, value);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009A04 File Offset: 0x00007C04
		internal static List<ExtendedVisualStateManager.OriginalLayoutValueRecord> GetOriginalLayoutValues(DependencyObject obj)
		{
			return (List<ExtendedVisualStateManager.OriginalLayoutValueRecord>)obj.GetValue(ExtendedVisualStateManager.OriginalLayoutValuesProperty);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009A16 File Offset: 0x00007C16
		internal static void SetOriginalLayoutValues(DependencyObject obj, List<ExtendedVisualStateManager.OriginalLayoutValueRecord> value)
		{
			obj.SetValue(ExtendedVisualStateManager.OriginalLayoutValuesProperty, value);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009A24 File Offset: 0x00007C24
		internal static Storyboard GetLayoutStoryboard(DependencyObject obj)
		{
			return (Storyboard)obj.GetValue(ExtendedVisualStateManager.LayoutStoryboardProperty);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009A36 File Offset: 0x00007C36
		internal static void SetLayoutStoryboard(DependencyObject obj, Storyboard value)
		{
			obj.SetValue(ExtendedVisualStateManager.LayoutStoryboardProperty, value);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009A44 File Offset: 0x00007C44
		internal static VisualState GetCurrentState(DependencyObject obj)
		{
			return (VisualState)obj.GetValue(ExtendedVisualStateManager.CurrentStateProperty);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009A56 File Offset: 0x00007C56
		internal static void SetCurrentState(DependencyObject obj, VisualState value)
		{
			obj.SetValue(ExtendedVisualStateManager.CurrentStateProperty, value);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009A64 File Offset: 0x00007C64
		public static TransitionEffect GetTransitionEffect(DependencyObject obj)
		{
			return (TransitionEffect)obj.GetValue(ExtendedVisualStateManager.TransitionEffectProperty);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009A76 File Offset: 0x00007C76
		public static void SetTransitionEffect(DependencyObject obj, TransitionEffect value)
		{
			obj.SetValue(ExtendedVisualStateManager.TransitionEffectProperty, value);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009A84 File Offset: 0x00007C84
		internal static Storyboard GetTransitionEffectStoryboard(DependencyObject obj)
		{
			return (Storyboard)obj.GetValue(ExtendedVisualStateManager.TransitionEffectStoryboardProperty);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009A96 File Offset: 0x00007C96
		internal static void SetTransitionEffectStoryboard(DependencyObject obj, Storyboard value)
		{
			obj.SetValue(ExtendedVisualStateManager.TransitionEffectStoryboardProperty, value);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009AA4 File Offset: 0x00007CA4
		internal static bool GetDidCacheBackground(DependencyObject obj)
		{
			return (bool)obj.GetValue(ExtendedVisualStateManager.DidCacheBackgroundProperty);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009AB6 File Offset: 0x00007CB6
		internal static void SetDidCacheBackground(DependencyObject obj, bool value)
		{
			obj.SetValue(ExtendedVisualStateManager.DidCacheBackgroundProperty, value);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009AC9 File Offset: 0x00007CC9
		internal static object GetCachedBackground(DependencyObject obj)
		{
			return obj.GetValue(ExtendedVisualStateManager.CachedBackgroundProperty);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009AD6 File Offset: 0x00007CD6
		internal static void SetCachedBackground(DependencyObject obj, object value)
		{
			obj.SetValue(ExtendedVisualStateManager.CachedBackgroundProperty, value);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009AE4 File Offset: 0x00007CE4
		internal static Effect GetCachedEffect(DependencyObject obj)
		{
			return (Effect)obj.GetValue(ExtendedVisualStateManager.CachedEffectProperty);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009AF6 File Offset: 0x00007CF6
		internal static void SetCachedEffect(DependencyObject obj, Effect value)
		{
			obj.SetValue(ExtendedVisualStateManager.CachedEffectProperty, value);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009B04 File Offset: 0x00007D04
		private static bool IsVisibilityProperty(DependencyProperty property)
		{
			return property == UIElement.VisibilityProperty || property.Name == "RuntimeVisibility";
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009B20 File Offset: 0x00007D20
		private static DependencyProperty LayoutPropertyFromTimeline(Timeline timeline, bool forceRuntimeProperty)
		{
			PropertyPath targetProperty = Storyboard.GetTargetProperty(timeline);
			if (targetProperty == null || targetProperty.PathParameters == null || targetProperty.PathParameters.Count == 0)
			{
				return null;
			}
			DependencyProperty dependencyProperty = targetProperty.PathParameters[0] as DependencyProperty;
			if (dependencyProperty != null)
			{
				if (dependencyProperty.Name == "RuntimeVisibility" && dependencyProperty.OwnerType.Name.EndsWith("DesignTimeProperties", StringComparison.Ordinal))
				{
					if (!ExtendedVisualStateManager.LayoutProperties.Contains(dependencyProperty))
					{
						ExtendedVisualStateManager.LayoutProperties.Add(dependencyProperty);
					}
					if (!forceRuntimeProperty)
					{
						return UIElement.VisibilityProperty;
					}
					return dependencyProperty;
				}
				else if (dependencyProperty.Name == "RuntimeWidth" && dependencyProperty.OwnerType.Name.EndsWith("DesignTimeProperties", StringComparison.Ordinal))
				{
					if (!ExtendedVisualStateManager.LayoutProperties.Contains(dependencyProperty))
					{
						ExtendedVisualStateManager.LayoutProperties.Add(dependencyProperty);
					}
					if (!forceRuntimeProperty)
					{
						return FrameworkElement.WidthProperty;
					}
					return dependencyProperty;
				}
				else if (dependencyProperty.Name == "RuntimeHeight" && dependencyProperty.OwnerType.Name.EndsWith("DesignTimeProperties", StringComparison.Ordinal))
				{
					if (!ExtendedVisualStateManager.LayoutProperties.Contains(dependencyProperty))
					{
						ExtendedVisualStateManager.LayoutProperties.Add(dependencyProperty);
					}
					if (!forceRuntimeProperty)
					{
						return FrameworkElement.HeightProperty;
					}
					return dependencyProperty;
				}
				else if (ExtendedVisualStateManager.LayoutProperties.Contains(dependencyProperty))
				{
					return dependencyProperty;
				}
			}
			return null;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009C60 File Offset: 0x00007E60
		protected override bool GoToStateCore(FrameworkElement control, FrameworkElement stateGroupsRoot, string stateName, VisualStateGroup group, VisualState state, bool useTransitions)
		{
			if (this.changingState)
			{
				return false;
			}
			if (group == null || state == null)
			{
				return false;
			}
			VisualState currentState = ExtendedVisualStateManager.GetCurrentState(group);
			if (currentState == state)
			{
				return true;
			}
			VisualTransition visualTransition = ExtendedVisualStateManager.FindTransition(group, currentState, state);
			bool flag = ExtendedVisualStateManager.PrepareTransitionEffectImage(stateGroupsRoot, useTransitions, visualTransition);
			if (!ExtendedVisualStateManager.GetUseFluidLayout(group))
			{
				return this.TransitionEffectAwareGoToStateCore(control, stateGroupsRoot, stateName, group, state, useTransitions, visualTransition, flag, currentState);
			}
			Storyboard storyboard = ExtendedVisualStateManager.ExtractLayoutStoryboard(state);
			List<ExtendedVisualStateManager.OriginalLayoutValueRecord> list = ExtendedVisualStateManager.GetOriginalLayoutValues(group);
			if (list == null)
			{
				list = new List<ExtendedVisualStateManager.OriginalLayoutValueRecord>();
				ExtendedVisualStateManager.SetOriginalLayoutValues(group, list);
			}
			if (!useTransitions)
			{
				if (ExtendedVisualStateManager.LayoutTransitionStoryboard != null)
				{
					ExtendedVisualStateManager.StopAnimations();
				}
				bool flag2 = this.TransitionEffectAwareGoToStateCore(control, stateGroupsRoot, stateName, group, state, useTransitions, visualTransition, flag, currentState);
				ExtendedVisualStateManager.SetLayoutStoryboardProperties(control, stateGroupsRoot, storyboard, list);
				return flag2;
			}
			if (storyboard.Children.Count == 0 && list.Count == 0)
			{
				return this.TransitionEffectAwareGoToStateCore(control, stateGroupsRoot, stateName, group, state, useTransitions, visualTransition, flag, currentState);
			}
			try
			{
				this.changingState = true;
				stateGroupsRoot.UpdateLayout();
				List<FrameworkElement> list2 = ExtendedVisualStateManager.FindTargetElements(control, stateGroupsRoot, storyboard, list, ExtendedVisualStateManager.MovingElements);
				Dictionary<FrameworkElement, Rect> rectsOfTargets = ExtendedVisualStateManager.GetRectsOfTargets(list2, ExtendedVisualStateManager.MovingElements);
				Dictionary<FrameworkElement, double> oldOpacities = ExtendedVisualStateManager.GetOldOpacities(control, stateGroupsRoot, storyboard, list, ExtendedVisualStateManager.MovingElements);
				if (ExtendedVisualStateManager.LayoutTransitionStoryboard != null)
				{
					stateGroupsRoot.LayoutUpdated -= ExtendedVisualStateManager.control_LayoutUpdated;
					ExtendedVisualStateManager.StopAnimations();
					stateGroupsRoot.UpdateLayout();
				}
				this.TransitionEffectAwareGoToStateCore(control, stateGroupsRoot, stateName, group, state, useTransitions, visualTransition, flag, currentState);
				ExtendedVisualStateManager.SetLayoutStoryboardProperties(control, stateGroupsRoot, storyboard, list);
				stateGroupsRoot.UpdateLayout();
				Dictionary<FrameworkElement, Rect> rectsOfTargets2 = ExtendedVisualStateManager.GetRectsOfTargets(list2, null);
				ExtendedVisualStateManager.MovingElements = new List<FrameworkElement>();
				foreach (FrameworkElement frameworkElement in list2)
				{
					if (rectsOfTargets[frameworkElement] != rectsOfTargets2[frameworkElement])
					{
						ExtendedVisualStateManager.MovingElements.Add(frameworkElement);
					}
				}
				foreach (FrameworkElement frameworkElement2 in oldOpacities.Keys)
				{
					if (!ExtendedVisualStateManager.MovingElements.Contains(frameworkElement2))
					{
						ExtendedVisualStateManager.MovingElements.Add(frameworkElement2);
					}
				}
				ExtendedVisualStateManager.WrapMovingElementsInCanvases(ExtendedVisualStateManager.MovingElements, rectsOfTargets, rectsOfTargets2);
				stateGroupsRoot.LayoutUpdated += ExtendedVisualStateManager.control_LayoutUpdated;
				ExtendedVisualStateManager.LayoutTransitionStoryboard = ExtendedVisualStateManager.CreateLayoutTransitionStoryboard(visualTransition, ExtendedVisualStateManager.MovingElements, oldOpacities);
				ExtendedVisualStateManager.LayoutTransitionStoryboard.Completed += delegate(object sender, EventArgs args)
				{
					stateGroupsRoot.LayoutUpdated -= ExtendedVisualStateManager.control_LayoutUpdated;
					ExtendedVisualStateManager.StopAnimations();
				};
				ExtendedVisualStateManager.LayoutTransitionStoryboard.Begin();
			}
			finally
			{
				this.changingState = false;
			}
			return true;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00009F70 File Offset: 0x00008170
		private static void control_LayoutUpdated(object sender, EventArgs e)
		{
			if (ExtendedVisualStateManager.LayoutTransitionStoryboard != null)
			{
				foreach (FrameworkElement frameworkElement in ExtendedVisualStateManager.MovingElements)
				{
					ExtendedVisualStateManager.WrapperCanvas wrapperCanvas = frameworkElement.Parent as ExtendedVisualStateManager.WrapperCanvas;
					if (wrapperCanvas != null)
					{
						Rect layoutRect = ExtendedVisualStateManager.GetLayoutRect(wrapperCanvas);
						Rect newRect = wrapperCanvas.NewRect;
						TranslateTransform translateTransform = wrapperCanvas.RenderTransform as TranslateTransform;
						double num = ((translateTransform == null) ? 0.0 : translateTransform.X);
						double num2 = ((translateTransform == null) ? 0.0 : translateTransform.Y);
						double num3 = newRect.Left - layoutRect.Left;
						double num4 = newRect.Top - layoutRect.Top;
						if (num != num3 || num2 != num4)
						{
							if (translateTransform == null)
							{
								translateTransform = new TranslateTransform();
								wrapperCanvas.RenderTransform = translateTransform;
							}
							translateTransform.X = num3;
							translateTransform.Y = num4;
						}
					}
				}
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A078 File Offset: 0x00008278
		private static void StopAnimations()
		{
			if (ExtendedVisualStateManager.LayoutTransitionStoryboard != null)
			{
				ExtendedVisualStateManager.LayoutTransitionStoryboard.Stop();
				ExtendedVisualStateManager.LayoutTransitionStoryboard = null;
			}
			if (ExtendedVisualStateManager.MovingElements != null)
			{
				ExtendedVisualStateManager.UnwrapMovingElementsFromCanvases(ExtendedVisualStateManager.MovingElements);
				ExtendedVisualStateManager.MovingElements = null;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A0A8 File Offset: 0x000082A8
		private static bool PrepareTransitionEffectImage(FrameworkElement stateGroupsRoot, bool useTransitions, VisualTransition transition)
		{
			TransitionEffect transitionEffect = ((transition == null) ? null : ExtendedVisualStateManager.GetTransitionEffect(transition));
			bool flag = false;
			if (transitionEffect != null)
			{
				transitionEffect = transitionEffect.CloneCurrentValue();
				if (useTransitions)
				{
					flag = true;
					int num = (int)Math.Max(1.0, stateGroupsRoot.ActualWidth);
					int num2 = (int)Math.Max(1.0, stateGroupsRoot.ActualHeight);
					RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(num, num2, 96.0, 96.0, PixelFormats.Pbgra32);
					renderTargetBitmap.Render(stateGroupsRoot);
					transitionEffect.OldImage = new ImageBrush
					{
						ImageSource = renderTargetBitmap
					};
				}
				Storyboard transitionEffectStoryboard = ExtendedVisualStateManager.GetTransitionEffectStoryboard(stateGroupsRoot);
				if (transitionEffectStoryboard != null)
				{
					transitionEffectStoryboard.Stop();
					ExtendedVisualStateManager.FinishTransitionEffectAnimation(stateGroupsRoot);
				}
				if (useTransitions)
				{
					ExtendedVisualStateManager.TransferLocalValue(stateGroupsRoot, UIElement.EffectProperty, ExtendedVisualStateManager.CachedEffectProperty);
					stateGroupsRoot.Effect = transitionEffect;
				}
			}
			return flag;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A174 File Offset: 0x00008374
		private bool TransitionEffectAwareGoToStateCore(FrameworkElement control, FrameworkElement stateGroupsRoot, string stateName, VisualStateGroup group, VisualState state, bool useTransitions, VisualTransition transition, bool animateWithTransitionEffect, VisualState previousState)
		{
			IEasingFunction easingFunction = null;
			if (animateWithTransitionEffect)
			{
				easingFunction = transition.GeneratedEasingFunction;
				transition.GeneratedEasingFunction = new ExtendedVisualStateManager.DummyEasingFunction
				{
					DummyValue = (ExtendedVisualStateManager.FinishesWithZeroOpacity(control, stateGroupsRoot, state, previousState) ? 0.01 : 0.0)
				};
			}
			bool flag = base.GoToStateCore(control, stateGroupsRoot, stateName, group, state, useTransitions);
			if (animateWithTransitionEffect)
			{
				transition.GeneratedEasingFunction = easingFunction;
				if (flag)
				{
					ExtendedVisualStateManager.AnimateTransitionEffect(stateGroupsRoot, transition);
				}
			}
			ExtendedVisualStateManager.SetCurrentState(group, state);
			return flag;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A1F4 File Offset: 0x000083F4
		private static bool FinishesWithZeroOpacity(FrameworkElement control, FrameworkElement stateGroupsRoot, VisualState state, VisualState previousState)
		{
			if (state.Storyboard != null)
			{
				foreach (Timeline timeline in state.Storyboard.Children)
				{
					if (ExtendedVisualStateManager.TimelineIsAnimatingRootOpacity(timeline, control, stateGroupsRoot))
					{
						bool flag;
						object valueFromTimeline = ExtendedVisualStateManager.GetValueFromTimeline(timeline, out flag);
						return flag && valueFromTimeline is double && (double)valueFromTimeline == 0.0;
					}
				}
			}
			if (previousState != null && previousState.Storyboard != null)
			{
				foreach (Timeline timeline2 in previousState.Storyboard.Children)
				{
					ExtendedVisualStateManager.TimelineIsAnimatingRootOpacity(timeline2, control, stateGroupsRoot);
				}
				return (double)stateGroupsRoot.GetAnimationBaseValue(UIElement.OpacityProperty) == 0.0;
			}
			return stateGroupsRoot.Opacity == 0.0;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A308 File Offset: 0x00008508
		private static bool TimelineIsAnimatingRootOpacity(Timeline timeline, FrameworkElement control, FrameworkElement stateGroupsRoot)
		{
			if (ExtendedVisualStateManager.GetTimelineTarget(control, stateGroupsRoot, timeline) != stateGroupsRoot)
			{
				return false;
			}
			PropertyPath targetProperty = Storyboard.GetTargetProperty(timeline);
			return targetProperty != null && targetProperty.PathParameters != null && targetProperty.PathParameters.Count != 0 && targetProperty.PathParameters[0] == UIElement.OpacityProperty;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A358 File Offset: 0x00008558
		private static void AnimateTransitionEffect(FrameworkElement stateGroupsRoot, VisualTransition transition)
		{
			Effect effect = stateGroupsRoot.Effect;
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.Duration = transition.GeneratedDuration;
			doubleAnimation.EasingFunction = transition.GeneratedEasingFunction;
			doubleAnimation.From = new double?(0.0);
			doubleAnimation.To = new double?((double)1);
			Storyboard sb = new Storyboard();
			sb.Duration = transition.GeneratedDuration;
			sb.Children.Add(doubleAnimation);
			Storyboard.SetTarget(doubleAnimation, stateGroupsRoot);
			DependencyObject dependencyObject = doubleAnimation;
			string text = "(0).(1)";
			object[] array = new DependencyProperty[]
			{
				UIElement.EffectProperty,
				TransitionEffect.ProgressProperty
			};
			Storyboard.SetTargetProperty(dependencyObject, new PropertyPath(text, array));
			Panel panel = stateGroupsRoot as Panel;
			if (panel != null && panel.Background == null)
			{
				ExtendedVisualStateManager.SetDidCacheBackground(panel, true);
				ExtendedVisualStateManager.TransferLocalValue(panel, Panel.BackgroundProperty, ExtendedVisualStateManager.CachedBackgroundProperty);
				panel.Background = Brushes.Transparent;
			}
			sb.Completed += delegate(object sender, EventArgs e)
			{
				if (ExtendedVisualStateManager.GetTransitionEffectStoryboard(stateGroupsRoot) == sb)
				{
					ExtendedVisualStateManager.FinishTransitionEffectAnimation(stateGroupsRoot);
				}
			};
			ExtendedVisualStateManager.SetTransitionEffectStoryboard(stateGroupsRoot, sb);
			sb.Begin();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A48A File Offset: 0x0000868A
		private static void FinishTransitionEffectAnimation(FrameworkElement stateGroupsRoot)
		{
			ExtendedVisualStateManager.SetTransitionEffectStoryboard(stateGroupsRoot, null);
			ExtendedVisualStateManager.TransferLocalValue(stateGroupsRoot, ExtendedVisualStateManager.CachedEffectProperty, UIElement.EffectProperty);
			if (ExtendedVisualStateManager.GetDidCacheBackground(stateGroupsRoot))
			{
				ExtendedVisualStateManager.TransferLocalValue(stateGroupsRoot, ExtendedVisualStateManager.CachedBackgroundProperty, Panel.BackgroundProperty);
				ExtendedVisualStateManager.SetDidCacheBackground(stateGroupsRoot, false);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A4C4 File Offset: 0x000086C4
		private static VisualTransition FindTransition(VisualStateGroup group, VisualState previousState, VisualState state)
		{
			string text = ((previousState != null) ? previousState.Name : string.Empty);
			string text2 = ((state != null) ? state.Name : string.Empty);
			int num = -1;
			VisualTransition visualTransition = null;
			if (group.Transitions != null)
			{
				foreach (object obj in group.Transitions)
				{
					VisualTransition visualTransition2 = (VisualTransition)obj;
					int num2 = 0;
					if (visualTransition2.From == text)
					{
						num2++;
					}
					else if (!string.IsNullOrEmpty(visualTransition2.From))
					{
						continue;
					}
					if (visualTransition2.To == text2)
					{
						num2 += 2;
					}
					else if (!string.IsNullOrEmpty(visualTransition2.To))
					{
						continue;
					}
					if (num2 > num)
					{
						num = num2;
						visualTransition = visualTransition2;
					}
				}
			}
			return visualTransition;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A5A8 File Offset: 0x000087A8
		private static Storyboard ExtractLayoutStoryboard(VisualState state)
		{
			Storyboard storyboard = null;
			if (state.Storyboard != null)
			{
				storyboard = ExtendedVisualStateManager.GetLayoutStoryboard(state.Storyboard);
				if (storyboard == null)
				{
					storyboard = new Storyboard();
					for (int i = state.Storyboard.Children.Count - 1; i >= 0; i--)
					{
						Timeline timeline = state.Storyboard.Children[i];
						if (ExtendedVisualStateManager.LayoutPropertyFromTimeline(timeline, false) != null)
						{
							state.Storyboard.Children.RemoveAt(i);
							storyboard.Children.Add(timeline);
						}
					}
					ExtendedVisualStateManager.SetLayoutStoryboard(state.Storyboard, storyboard);
				}
			}
			if (storyboard == null)
			{
				return new Storyboard();
			}
			return storyboard;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A640 File Offset: 0x00008840
		private static List<FrameworkElement> FindTargetElements(FrameworkElement control, FrameworkElement templateRoot, Storyboard layoutStoryboard, List<ExtendedVisualStateManager.OriginalLayoutValueRecord> originalValueRecords, List<FrameworkElement> movingElements)
		{
			List<FrameworkElement> list = new List<FrameworkElement>();
			if (movingElements != null)
			{
				list.AddRange(movingElements);
			}
			foreach (Timeline timeline in layoutStoryboard.Children)
			{
				FrameworkElement frameworkElement = (FrameworkElement)ExtendedVisualStateManager.GetTimelineTarget(control, templateRoot, timeline);
				if (frameworkElement != null)
				{
					if (!list.Contains(frameworkElement))
					{
						list.Add(frameworkElement);
					}
					if (ExtendedVisualStateManager.ChildAffectingLayoutProperties.Contains(ExtendedVisualStateManager.LayoutPropertyFromTimeline(timeline, false)))
					{
						Panel panel = frameworkElement as Panel;
						if (panel != null)
						{
							foreach (object obj in panel.Children)
							{
								FrameworkElement frameworkElement2 = (FrameworkElement)obj;
								if (!list.Contains(frameworkElement2) && !(frameworkElement2 is ExtendedVisualStateManager.WrapperCanvas))
								{
									list.Add(frameworkElement2);
								}
							}
						}
					}
				}
			}
			foreach (ExtendedVisualStateManager.OriginalLayoutValueRecord originalLayoutValueRecord in originalValueRecords)
			{
				if (!list.Contains(originalLayoutValueRecord.Element))
				{
					list.Add(originalLayoutValueRecord.Element);
				}
				if (ExtendedVisualStateManager.ChildAffectingLayoutProperties.Contains(originalLayoutValueRecord.Property))
				{
					Panel panel2 = originalLayoutValueRecord.Element as Panel;
					if (panel2 != null)
					{
						foreach (object obj2 in panel2.Children)
						{
							FrameworkElement frameworkElement3 = (FrameworkElement)obj2;
							if (!list.Contains(frameworkElement3) && !(frameworkElement3 is ExtendedVisualStateManager.WrapperCanvas))
							{
								list.Add(frameworkElement3);
							}
						}
					}
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				FrameworkElement frameworkElement4 = list[i];
				FrameworkElement frameworkElement5 = VisualTreeHelper.GetParent(frameworkElement4) as FrameworkElement;
				if (movingElements != null && movingElements.Contains(frameworkElement4) && frameworkElement5 is ExtendedVisualStateManager.WrapperCanvas)
				{
					frameworkElement5 = VisualTreeHelper.GetParent(frameworkElement5) as FrameworkElement;
				}
				if (frameworkElement5 != null)
				{
					if (!list.Contains(frameworkElement5))
					{
						list.Add(frameworkElement5);
					}
					for (int j = 0; j < VisualTreeHelper.GetChildrenCount(frameworkElement5); j++)
					{
						FrameworkElement frameworkElement6 = VisualTreeHelper.GetChild(frameworkElement5, j) as FrameworkElement;
						if (frameworkElement6 != null && !list.Contains(frameworkElement6) && !(frameworkElement6 is ExtendedVisualStateManager.WrapperCanvas))
						{
							list.Add(frameworkElement6);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		private static object GetTimelineTarget(FrameworkElement control, FrameworkElement templateRoot, Timeline timeline)
		{
			string targetName = Storyboard.GetTargetName(timeline);
			if (string.IsNullOrEmpty(targetName))
			{
				return null;
			}
			if (control is UserControl)
			{
				return control.FindName(targetName);
			}
			return templateRoot.FindName(targetName);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A920 File Offset: 0x00008B20
		private static Dictionary<FrameworkElement, Rect> GetRectsOfTargets(List<FrameworkElement> targets, List<FrameworkElement> movingElements)
		{
			Dictionary<FrameworkElement, Rect> dictionary = new Dictionary<FrameworkElement, Rect>();
			foreach (FrameworkElement frameworkElement in targets)
			{
				Rect rect;
				if (movingElements != null && movingElements.Contains(frameworkElement) && frameworkElement.Parent is ExtendedVisualStateManager.WrapperCanvas)
				{
					ExtendedVisualStateManager.WrapperCanvas wrapperCanvas = frameworkElement.Parent as ExtendedVisualStateManager.WrapperCanvas;
					rect = ExtendedVisualStateManager.GetLayoutRect(wrapperCanvas);
					TranslateTransform translateTransform = wrapperCanvas.RenderTransform as TranslateTransform;
					double left = Canvas.GetLeft(frameworkElement);
					double top = Canvas.GetTop(frameworkElement);
					rect = new Rect(rect.Left + (double.IsNaN(left) ? 0.0 : left) + ((translateTransform == null) ? 0.0 : translateTransform.X), rect.Top + (double.IsNaN(top) ? 0.0 : top) + ((translateTransform == null) ? 0.0 : translateTransform.Y), frameworkElement.ActualWidth, frameworkElement.ActualHeight);
				}
				else
				{
					rect = ExtendedVisualStateManager.GetLayoutRect(frameworkElement);
				}
				dictionary.Add(frameworkElement, rect);
			}
			return dictionary;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AA54 File Offset: 0x00008C54
		internal static Rect GetLayoutRect(FrameworkElement element)
		{
			double num = element.ActualWidth;
			double num2 = element.ActualHeight;
			if (element is Image || element is MediaElement)
			{
				if (element.Parent is Canvas)
				{
					num = (double.IsNaN(element.Width) ? num : element.Width);
					num2 = (double.IsNaN(element.Height) ? num2 : element.Height);
				}
				else
				{
					num = element.RenderSize.Width;
					num2 = element.RenderSize.Height;
				}
			}
			num = ((element.Visibility == Visibility.Collapsed) ? 0.0 : num);
			num2 = ((element.Visibility == Visibility.Collapsed) ? 0.0 : num2);
			Thickness margin = element.Margin;
			Rect layoutSlot = LayoutInformation.GetLayoutSlot(element);
			double num3 = 0.0;
			double num4 = 0.0;
			switch (element.HorizontalAlignment)
			{
			case HorizontalAlignment.Left:
				num3 = layoutSlot.Left + margin.Left;
				break;
			case HorizontalAlignment.Center:
				num3 = (layoutSlot.Left + margin.Left + layoutSlot.Right - margin.Right) / 2.0 - num / 2.0;
				break;
			case HorizontalAlignment.Right:
				num3 = layoutSlot.Right - margin.Right - num;
				break;
			case HorizontalAlignment.Stretch:
				num3 = Math.Max(layoutSlot.Left + margin.Left, (layoutSlot.Left + margin.Left + layoutSlot.Right - margin.Right) / 2.0 - num / 2.0);
				break;
			}
			switch (element.VerticalAlignment)
			{
			case VerticalAlignment.Top:
				num4 = layoutSlot.Top + margin.Top;
				break;
			case VerticalAlignment.Center:
				num4 = (layoutSlot.Top + margin.Top + layoutSlot.Bottom - margin.Bottom) / 2.0 - num2 / 2.0;
				break;
			case VerticalAlignment.Bottom:
				num4 = layoutSlot.Bottom - margin.Bottom - num2;
				break;
			case VerticalAlignment.Stretch:
				num4 = Math.Max(layoutSlot.Top + margin.Top, (layoutSlot.Top + margin.Top + layoutSlot.Bottom - margin.Bottom) / 2.0 - num2 / 2.0);
				break;
			}
			return new Rect(num3, num4, num, num2);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		private static Dictionary<FrameworkElement, double> GetOldOpacities(FrameworkElement control, FrameworkElement templateRoot, Storyboard layoutStoryboard, List<ExtendedVisualStateManager.OriginalLayoutValueRecord> originalValueRecords, List<FrameworkElement> movingElements)
		{
			Dictionary<FrameworkElement, double> dictionary = new Dictionary<FrameworkElement, double>();
			if (movingElements != null)
			{
				foreach (FrameworkElement frameworkElement in movingElements)
				{
					ExtendedVisualStateManager.WrapperCanvas wrapperCanvas = frameworkElement.Parent as ExtendedVisualStateManager.WrapperCanvas;
					if (wrapperCanvas != null)
					{
						dictionary.Add(frameworkElement, wrapperCanvas.Opacity);
					}
				}
			}
			for (int i = originalValueRecords.Count - 1; i >= 0; i--)
			{
				ExtendedVisualStateManager.OriginalLayoutValueRecord originalLayoutValueRecord = originalValueRecords[i];
				double num;
				if (ExtendedVisualStateManager.IsVisibilityProperty(originalLayoutValueRecord.Property) && !dictionary.TryGetValue(originalLayoutValueRecord.Element, out num))
				{
					num = (((Visibility)originalLayoutValueRecord.Element.GetValue(originalLayoutValueRecord.Property) == Visibility.Visible) ? 1.0 : 0.0);
					dictionary.Add(originalLayoutValueRecord.Element, num);
				}
			}
			foreach (Timeline timeline in layoutStoryboard.Children)
			{
				FrameworkElement frameworkElement2 = (FrameworkElement)ExtendedVisualStateManager.GetTimelineTarget(control, templateRoot, timeline);
				DependencyProperty dependencyProperty = ExtendedVisualStateManager.LayoutPropertyFromTimeline(timeline, true);
				double num2;
				if (frameworkElement2 != null && ExtendedVisualStateManager.IsVisibilityProperty(dependencyProperty) && !dictionary.TryGetValue(frameworkElement2, out num2))
				{
					num2 = (((Visibility)frameworkElement2.GetValue(dependencyProperty) == Visibility.Visible) ? 1.0 : 0.0);
					dictionary.Add(frameworkElement2, num2);
				}
			}
			return dictionary;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000AE74 File Offset: 0x00009074
		private static void SetLayoutStoryboardProperties(FrameworkElement control, FrameworkElement templateRoot, Storyboard layoutStoryboard, List<ExtendedVisualStateManager.OriginalLayoutValueRecord> originalValueRecords)
		{
			foreach (ExtendedVisualStateManager.OriginalLayoutValueRecord originalLayoutValueRecord in originalValueRecords)
			{
				ExtendedVisualStateManager.ReplaceCachedLocalValueHelper(originalLayoutValueRecord.Element, originalLayoutValueRecord.Property, originalLayoutValueRecord.Value);
			}
			originalValueRecords.Clear();
			foreach (Timeline timeline in layoutStoryboard.Children)
			{
				FrameworkElement frameworkElement = (FrameworkElement)ExtendedVisualStateManager.GetTimelineTarget(control, templateRoot, timeline);
				DependencyProperty dependencyProperty = ExtendedVisualStateManager.LayoutPropertyFromTimeline(timeline, true);
				if (frameworkElement != null && dependencyProperty != null)
				{
					bool flag;
					object valueFromTimeline = ExtendedVisualStateManager.GetValueFromTimeline(timeline, out flag);
					if (flag)
					{
						originalValueRecords.Add(new ExtendedVisualStateManager.OriginalLayoutValueRecord
						{
							Element = frameworkElement,
							Property = dependencyProperty,
							Value = ExtendedVisualStateManager.CacheLocalValueHelper(frameworkElement, dependencyProperty)
						});
						frameworkElement.SetValue(dependencyProperty, valueFromTimeline);
					}
				}
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000AF78 File Offset: 0x00009178
		private static object GetValueFromTimeline(Timeline timeline, out bool gotValue)
		{
			ObjectAnimationUsingKeyFrames objectAnimationUsingKeyFrames = timeline as ObjectAnimationUsingKeyFrames;
			if (objectAnimationUsingKeyFrames != null)
			{
				gotValue = true;
				return objectAnimationUsingKeyFrames.KeyFrames[0].Value;
			}
			DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = timeline as DoubleAnimationUsingKeyFrames;
			if (doubleAnimationUsingKeyFrames != null)
			{
				gotValue = true;
				return doubleAnimationUsingKeyFrames.KeyFrames[0].Value;
			}
			DoubleAnimation doubleAnimation = timeline as DoubleAnimation;
			if (doubleAnimation != null)
			{
				gotValue = true;
				return doubleAnimation.To;
			}
			ThicknessAnimationUsingKeyFrames thicknessAnimationUsingKeyFrames = timeline as ThicknessAnimationUsingKeyFrames;
			if (thicknessAnimationUsingKeyFrames != null)
			{
				gotValue = true;
				return thicknessAnimationUsingKeyFrames.KeyFrames[0].Value;
			}
			ThicknessAnimation thicknessAnimation = timeline as ThicknessAnimation;
			if (thicknessAnimation != null)
			{
				gotValue = true;
				return thicknessAnimation.To;
			}
			Int32AnimationUsingKeyFrames int32AnimationUsingKeyFrames = timeline as Int32AnimationUsingKeyFrames;
			if (int32AnimationUsingKeyFrames != null)
			{
				gotValue = true;
				return int32AnimationUsingKeyFrames.KeyFrames[0].Value;
			}
			Int32Animation int32Animation = timeline as Int32Animation;
			if (int32Animation != null)
			{
				gotValue = true;
				return int32Animation.To;
			}
			gotValue = false;
			return null;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B068 File Offset: 0x00009268
		private static void WrapMovingElementsInCanvases(List<FrameworkElement> movingElements, Dictionary<FrameworkElement, Rect> oldRects, Dictionary<FrameworkElement, Rect> newRects)
		{
			foreach (FrameworkElement frameworkElement in movingElements)
			{
				FrameworkElement frameworkElement2 = VisualTreeHelper.GetParent(frameworkElement) as FrameworkElement;
				ExtendedVisualStateManager.WrapperCanvas wrapperCanvas = new ExtendedVisualStateManager.WrapperCanvas();
				wrapperCanvas.OldRect = oldRects[frameworkElement];
				wrapperCanvas.NewRect = newRects[frameworkElement];
				object obj = ExtendedVisualStateManager.CacheLocalValueHelper(frameworkElement, FrameworkElement.DataContextProperty);
				frameworkElement.DataContext = frameworkElement.DataContext;
				bool flag = true;
				Panel panel = frameworkElement2 as Panel;
				if (panel != null && !panel.IsItemsHost)
				{
					int num = panel.Children.IndexOf(frameworkElement);
					panel.Children.RemoveAt(num);
					panel.Children.Insert(num, wrapperCanvas);
				}
				else
				{
					Decorator decorator = frameworkElement2 as Decorator;
					if (decorator != null)
					{
						decorator.Child = wrapperCanvas;
					}
					else
					{
						flag = false;
					}
				}
				if (flag)
				{
					wrapperCanvas.Children.Add(frameworkElement);
					ExtendedVisualStateManager.CopyLayoutProperties(frameworkElement, wrapperCanvas, false);
					ExtendedVisualStateManager.ReplaceCachedLocalValueHelper(frameworkElement, FrameworkElement.DataContextProperty, obj);
				}
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B180 File Offset: 0x00009380
		private static void UnwrapMovingElementsFromCanvases(List<FrameworkElement> movingElements)
		{
			foreach (FrameworkElement frameworkElement in movingElements)
			{
				ExtendedVisualStateManager.WrapperCanvas wrapperCanvas = frameworkElement.Parent as ExtendedVisualStateManager.WrapperCanvas;
				if (wrapperCanvas != null)
				{
					object obj = ExtendedVisualStateManager.CacheLocalValueHelper(frameworkElement, FrameworkElement.DataContextProperty);
					frameworkElement.DataContext = frameworkElement.DataContext;
					FrameworkElement frameworkElement2 = VisualTreeHelper.GetParent(wrapperCanvas) as FrameworkElement;
					wrapperCanvas.Children.Remove(frameworkElement);
					Panel panel = frameworkElement2 as Panel;
					if (panel != null)
					{
						int num = panel.Children.IndexOf(wrapperCanvas);
						panel.Children.RemoveAt(num);
						panel.Children.Insert(num, frameworkElement);
					}
					else
					{
						Decorator decorator = frameworkElement2 as Decorator;
						if (decorator != null)
						{
							decorator.Child = frameworkElement;
						}
					}
					ExtendedVisualStateManager.CopyLayoutProperties(wrapperCanvas, frameworkElement, true);
					ExtendedVisualStateManager.ReplaceCachedLocalValueHelper(frameworkElement, FrameworkElement.DataContextProperty, obj);
				}
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B274 File Offset: 0x00009474
		private static void CopyLayoutProperties(FrameworkElement source, FrameworkElement target, bool restoring)
		{
			ExtendedVisualStateManager.WrapperCanvas wrapperCanvas = (restoring ? source : target) as ExtendedVisualStateManager.WrapperCanvas;
			if (wrapperCanvas.LocalValueCache == null)
			{
				wrapperCanvas.LocalValueCache = new Dictionary<DependencyProperty, object>();
			}
			foreach (DependencyProperty dependencyProperty in ExtendedVisualStateManager.LayoutProperties)
			{
				if (!ExtendedVisualStateManager.ChildAffectingLayoutProperties.Contains(dependencyProperty))
				{
					if (restoring)
					{
						ExtendedVisualStateManager.ReplaceCachedLocalValueHelper(target, dependencyProperty, wrapperCanvas.LocalValueCache[dependencyProperty]);
					}
					else
					{
						object value = target.GetValue(dependencyProperty);
						object obj = ExtendedVisualStateManager.CacheLocalValueHelper(source, dependencyProperty);
						wrapperCanvas.LocalValueCache[dependencyProperty] = obj;
						if (ExtendedVisualStateManager.IsVisibilityProperty(dependencyProperty))
						{
							wrapperCanvas.DestinationVisibilityCache = (Visibility)source.GetValue(dependencyProperty);
						}
						else
						{
							target.SetValue(dependencyProperty, source.GetValue(dependencyProperty));
						}
						source.SetValue(dependencyProperty, value);
					}
				}
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B358 File Offset: 0x00009558
		private static Storyboard CreateLayoutTransitionStoryboard(VisualTransition transition, List<FrameworkElement> movingElements, Dictionary<FrameworkElement, double> oldOpacities)
		{
			Duration duration = ((transition != null) ? transition.GeneratedDuration : new Duration(TimeSpan.Zero));
			IEasingFunction easingFunction = ((transition != null) ? transition.GeneratedEasingFunction : null);
			Storyboard storyboard = new Storyboard();
			storyboard.Duration = duration;
			foreach (FrameworkElement frameworkElement in movingElements)
			{
				ExtendedVisualStateManager.WrapperCanvas wrapperCanvas = frameworkElement.Parent as ExtendedVisualStateManager.WrapperCanvas;
				if (wrapperCanvas != null)
				{
					DoubleAnimation doubleAnimation = new DoubleAnimation
					{
						From = new double?((double)1),
						To = new double?(0.0),
						Duration = duration
					};
					doubleAnimation.EasingFunction = easingFunction;
					Storyboard.SetTarget(doubleAnimation, wrapperCanvas);
					Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(ExtendedVisualStateManager.WrapperCanvas.SimulationProgressProperty));
					storyboard.Children.Add(doubleAnimation);
					wrapperCanvas.SimulationProgress = 1.0;
					Rect newRect = wrapperCanvas.NewRect;
					if (!ExtendedVisualStateManager.IsClose(wrapperCanvas.Width, newRect.Width))
					{
						DoubleAnimation doubleAnimation2 = new DoubleAnimation
						{
							From = new double?(newRect.Width),
							To = new double?(newRect.Width),
							Duration = duration
						};
						Storyboard.SetTarget(doubleAnimation2, wrapperCanvas);
						Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath(FrameworkElement.WidthProperty));
						storyboard.Children.Add(doubleAnimation2);
					}
					if (!ExtendedVisualStateManager.IsClose(wrapperCanvas.Height, newRect.Height))
					{
						DoubleAnimation doubleAnimation3 = new DoubleAnimation
						{
							From = new double?(newRect.Height),
							To = new double?(newRect.Height),
							Duration = duration
						};
						Storyboard.SetTarget(doubleAnimation3, wrapperCanvas);
						Storyboard.SetTargetProperty(doubleAnimation3, new PropertyPath(FrameworkElement.HeightProperty));
						storyboard.Children.Add(doubleAnimation3);
					}
					if (wrapperCanvas.DestinationVisibilityCache == Visibility.Collapsed)
					{
						Thickness margin = wrapperCanvas.Margin;
						if (!ExtendedVisualStateManager.IsClose(margin.Left, 0.0) || !ExtendedVisualStateManager.IsClose(margin.Top, 0.0) || !ExtendedVisualStateManager.IsClose(margin.Right, 0.0) || !ExtendedVisualStateManager.IsClose(margin.Bottom, 0.0))
						{
							ObjectAnimationUsingKeyFrames objectAnimationUsingKeyFrames = new ObjectAnimationUsingKeyFrames
							{
								Duration = duration
							};
							DiscreteObjectKeyFrame discreteObjectKeyFrame = new DiscreteObjectKeyFrame
							{
								KeyTime = TimeSpan.Zero,
								Value = default(Thickness)
							};
							objectAnimationUsingKeyFrames.KeyFrames.Add(discreteObjectKeyFrame);
							Storyboard.SetTarget(objectAnimationUsingKeyFrames, wrapperCanvas);
							Storyboard.SetTargetProperty(objectAnimationUsingKeyFrames, new PropertyPath(FrameworkElement.MarginProperty));
							storyboard.Children.Add(objectAnimationUsingKeyFrames);
						}
						if (!ExtendedVisualStateManager.IsClose(wrapperCanvas.MinWidth, 0.0))
						{
							DoubleAnimation doubleAnimation4 = new DoubleAnimation
							{
								From = new double?(0.0),
								To = new double?(0.0),
								Duration = duration
							};
							Storyboard.SetTarget(doubleAnimation4, wrapperCanvas);
							Storyboard.SetTargetProperty(doubleAnimation4, new PropertyPath(FrameworkElement.MinWidthProperty));
							storyboard.Children.Add(doubleAnimation4);
						}
						if (!ExtendedVisualStateManager.IsClose(wrapperCanvas.MinHeight, 0.0))
						{
							DoubleAnimation doubleAnimation5 = new DoubleAnimation
							{
								From = new double?(0.0),
								To = new double?(0.0),
								Duration = duration
							};
							Storyboard.SetTarget(doubleAnimation5, wrapperCanvas);
							Storyboard.SetTargetProperty(doubleAnimation5, new PropertyPath(FrameworkElement.MinHeightProperty));
							storyboard.Children.Add(doubleAnimation5);
						}
					}
				}
			}
			foreach (FrameworkElement frameworkElement2 in oldOpacities.Keys)
			{
				ExtendedVisualStateManager.WrapperCanvas wrapperCanvas2 = frameworkElement2.Parent as ExtendedVisualStateManager.WrapperCanvas;
				if (wrapperCanvas2 != null)
				{
					double num = oldOpacities[frameworkElement2];
					double num2 = ((wrapperCanvas2.DestinationVisibilityCache == Visibility.Visible) ? 1.0 : 0.0);
					if (!ExtendedVisualStateManager.IsClose(num, 1.0) || !ExtendedVisualStateManager.IsClose(num2, 1.0))
					{
						DoubleAnimation doubleAnimation6 = new DoubleAnimation
						{
							From = new double?(num),
							To = new double?(num2),
							Duration = duration
						};
						doubleAnimation6.EasingFunction = easingFunction;
						Storyboard.SetTarget(doubleAnimation6, wrapperCanvas2);
						Storyboard.SetTargetProperty(doubleAnimation6, new PropertyPath(UIElement.OpacityProperty));
						storyboard.Children.Add(doubleAnimation6);
					}
				}
			}
			return storyboard;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B81C File Offset: 0x00009A1C
		private static void TransferLocalValue(FrameworkElement element, DependencyProperty sourceProperty, DependencyProperty destProperty)
		{
			object obj = ExtendedVisualStateManager.CacheLocalValueHelper(element, sourceProperty);
			ExtendedVisualStateManager.ReplaceCachedLocalValueHelper(element, destProperty, obj);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B839 File Offset: 0x00009A39
		private static object CacheLocalValueHelper(DependencyObject dependencyObject, DependencyProperty property)
		{
			return dependencyObject.ReadLocalValue(property);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B844 File Offset: 0x00009A44
		private static void ReplaceCachedLocalValueHelper(FrameworkElement element, DependencyProperty property, object value)
		{
			if (value == DependencyProperty.UnsetValue)
			{
				element.ClearValue(property);
				return;
			}
			BindingExpressionBase bindingExpressionBase = value as BindingExpressionBase;
			if (bindingExpressionBase != null)
			{
				element.SetBinding(property, bindingExpressionBase.ParentBindingBase);
				return;
			}
			element.SetValue(property, value);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B882 File Offset: 0x00009A82
		private static bool IsClose(double a, double b)
		{
			return Math.Abs(a - b) < 1E-07;
		}

		// Token: 0x040000C9 RID: 201
		public static readonly DependencyProperty UseFluidLayoutProperty = DependencyProperty.RegisterAttached("UseFluidLayout", typeof(bool), typeof(ExtendedVisualStateManager), new PropertyMetadata(false));

		// Token: 0x040000CA RID: 202
		public static readonly DependencyProperty RuntimeVisibilityPropertyProperty = DependencyProperty.RegisterAttached("RuntimeVisibilityProperty", typeof(DependencyProperty), typeof(ExtendedVisualStateManager), new PropertyMetadata(null));

		// Token: 0x040000CB RID: 203
		internal static readonly DependencyProperty OriginalLayoutValuesProperty = DependencyProperty.RegisterAttached("OriginalLayoutValues", typeof(List<ExtendedVisualStateManager.OriginalLayoutValueRecord>), typeof(ExtendedVisualStateManager), new PropertyMetadata(null));

		// Token: 0x040000CC RID: 204
		internal static readonly DependencyProperty LayoutStoryboardProperty = DependencyProperty.RegisterAttached("LayoutStoryboard", typeof(Storyboard), typeof(ExtendedVisualStateManager), new PropertyMetadata(null));

		// Token: 0x040000CD RID: 205
		internal static readonly DependencyProperty CurrentStateProperty = DependencyProperty.RegisterAttached("CurrentState", typeof(VisualState), typeof(ExtendedVisualStateManager), new PropertyMetadata(null));

		// Token: 0x040000CE RID: 206
		public static readonly DependencyProperty TransitionEffectProperty = DependencyProperty.RegisterAttached("TransitionEffect", typeof(TransitionEffect), typeof(ExtendedVisualStateManager), new PropertyMetadata(null));

		// Token: 0x040000CF RID: 207
		internal static readonly DependencyProperty TransitionEffectStoryboardProperty = DependencyProperty.RegisterAttached("TransitionEffectStoryboard", typeof(Storyboard), typeof(ExtendedVisualStateManager), new PropertyMetadata(null));

		// Token: 0x040000D0 RID: 208
		internal static readonly DependencyProperty DidCacheBackgroundProperty = DependencyProperty.RegisterAttached("DidCacheBackground", typeof(bool), typeof(ExtendedVisualStateManager), new PropertyMetadata(false));

		// Token: 0x040000D1 RID: 209
		internal static readonly DependencyProperty CachedBackgroundProperty = DependencyProperty.RegisterAttached("CachedBackground", typeof(object), typeof(ExtendedVisualStateManager), new PropertyMetadata(null));

		// Token: 0x040000D2 RID: 210
		internal static readonly DependencyProperty CachedEffectProperty = DependencyProperty.RegisterAttached("CachedEffect", typeof(Effect), typeof(ExtendedVisualStateManager), new PropertyMetadata(null));

		// Token: 0x040000D3 RID: 211
		private static List<FrameworkElement> MovingElements;

		// Token: 0x040000D4 RID: 212
		private static Storyboard LayoutTransitionStoryboard;

		// Token: 0x040000D5 RID: 213
		private static List<DependencyProperty> LayoutProperties = new List<DependencyProperty>
		{
			Grid.ColumnProperty,
			Grid.ColumnSpanProperty,
			Grid.RowProperty,
			Grid.RowSpanProperty,
			Canvas.LeftProperty,
			Canvas.TopProperty,
			FrameworkElement.WidthProperty,
			FrameworkElement.HeightProperty,
			FrameworkElement.MinWidthProperty,
			FrameworkElement.MinHeightProperty,
			FrameworkElement.MaxWidthProperty,
			FrameworkElement.MaxHeightProperty,
			FrameworkElement.MarginProperty,
			FrameworkElement.HorizontalAlignmentProperty,
			FrameworkElement.VerticalAlignmentProperty,
			UIElement.VisibilityProperty,
			StackPanel.OrientationProperty
		};

		// Token: 0x040000D6 RID: 214
		private static List<DependencyProperty> ChildAffectingLayoutProperties = new List<DependencyProperty> { StackPanel.OrientationProperty };

		// Token: 0x040000D7 RID: 215
		private bool changingState;

		// Token: 0x0200005F RID: 95
		internal class WrapperCanvas : Canvas
		{
			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000337 RID: 823 RVA: 0x0000CBA5 File Offset: 0x0000ADA5
			// (set) Token: 0x06000338 RID: 824 RVA: 0x0000CBAD File Offset: 0x0000ADAD
			public Rect OldRect { get; set; }

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000339 RID: 825 RVA: 0x0000CBB6 File Offset: 0x0000ADB6
			// (set) Token: 0x0600033A RID: 826 RVA: 0x0000CBBE File Offset: 0x0000ADBE
			public Rect NewRect { get; set; }

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600033B RID: 827 RVA: 0x0000CBC7 File Offset: 0x0000ADC7
			// (set) Token: 0x0600033C RID: 828 RVA: 0x0000CBCF File Offset: 0x0000ADCF
			public Dictionary<DependencyProperty, object> LocalValueCache { get; set; }

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x0600033D RID: 829 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
			// (set) Token: 0x0600033E RID: 830 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
			public Visibility DestinationVisibilityCache { get; set; }

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x0600033F RID: 831 RVA: 0x0000CBE9 File Offset: 0x0000ADE9
			// (set) Token: 0x06000340 RID: 832 RVA: 0x0000CBFB File Offset: 0x0000ADFB
			public double SimulationProgress
			{
				get
				{
					return (double)base.GetValue(ExtendedVisualStateManager.WrapperCanvas.SimulationProgressProperty);
				}
				set
				{
					base.SetValue(ExtendedVisualStateManager.WrapperCanvas.SimulationProgressProperty, value);
				}
			}

			// Token: 0x06000341 RID: 833 RVA: 0x0000CC10 File Offset: 0x0000AE10
			private static void SimulationProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
			{
				ExtendedVisualStateManager.WrapperCanvas wrapperCanvas = d as ExtendedVisualStateManager.WrapperCanvas;
				double num = (double)e.NewValue;
				if (wrapperCanvas != null && wrapperCanvas.Children.Count > 0)
				{
					FrameworkElement frameworkElement = wrapperCanvas.Children[0] as FrameworkElement;
					frameworkElement.Width = Math.Max(0.0, wrapperCanvas.OldRect.Width * num + wrapperCanvas.NewRect.Width * (1.0 - num));
					frameworkElement.Height = Math.Max(0.0, wrapperCanvas.OldRect.Height * num + wrapperCanvas.NewRect.Height * (1.0 - num));
					Canvas.SetLeft(frameworkElement, num * (wrapperCanvas.OldRect.Left - wrapperCanvas.NewRect.Left));
					Canvas.SetTop(frameworkElement, num * (wrapperCanvas.OldRect.Top - wrapperCanvas.NewRect.Top));
				}
			}

			// Token: 0x0400011A RID: 282
			internal static readonly DependencyProperty SimulationProgressProperty = DependencyProperty.Register("SimulationProgress", typeof(double), typeof(ExtendedVisualStateManager.WrapperCanvas), new PropertyMetadata(0.0, new PropertyChangedCallback(ExtendedVisualStateManager.WrapperCanvas.SimulationProgressChanged)));
		}

		// Token: 0x02000060 RID: 96
		internal class OriginalLayoutValueRecord
		{
			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000344 RID: 836 RVA: 0x0000CD77 File Offset: 0x0000AF77
			// (set) Token: 0x06000345 RID: 837 RVA: 0x0000CD7F File Offset: 0x0000AF7F
			public FrameworkElement Element { get; set; }

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000346 RID: 838 RVA: 0x0000CD88 File Offset: 0x0000AF88
			// (set) Token: 0x06000347 RID: 839 RVA: 0x0000CD90 File Offset: 0x0000AF90
			public DependencyProperty Property { get; set; }

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000348 RID: 840 RVA: 0x0000CD99 File Offset: 0x0000AF99
			// (set) Token: 0x06000349 RID: 841 RVA: 0x0000CDA1 File Offset: 0x0000AFA1
			public object Value { get; set; }
		}

		// Token: 0x02000061 RID: 97
		private class DummyEasingFunction : EasingFunctionBase
		{
			// Token: 0x170000DB RID: 219
			// (get) Token: 0x0600034B RID: 843 RVA: 0x0000CDB2 File Offset: 0x0000AFB2
			// (set) Token: 0x0600034C RID: 844 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
			public double DummyValue
			{
				get
				{
					return (double)base.GetValue(ExtendedVisualStateManager.DummyEasingFunction.DummyValueProperty);
				}
				set
				{
					base.SetValue(ExtendedVisualStateManager.DummyEasingFunction.DummyValueProperty, value);
				}
			}

			// Token: 0x0600034D RID: 845 RVA: 0x0000CDD7 File Offset: 0x0000AFD7
			protected override Freezable CreateInstanceCore()
			{
				return new ExtendedVisualStateManager.DummyEasingFunction();
			}

			// Token: 0x0600034F RID: 847 RVA: 0x0000CDE6 File Offset: 0x0000AFE6
			protected override double EaseInCore(double normalizedTime)
			{
				return this.DummyValue;
			}

			// Token: 0x0400011E RID: 286
			public static readonly DependencyProperty DummyValueProperty = DependencyProperty.Register("DummyValue", typeof(double), typeof(ExtendedVisualStateManager.DummyEasingFunction), new PropertyMetadata(0.0));
		}
	}
}

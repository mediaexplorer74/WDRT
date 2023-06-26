using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000013 RID: 19
	public static class Interaction
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000036BA File Offset: 0x000018BA
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000036C1 File Offset: 0x000018C1
		internal static bool ShouldRunInDesignMode { get; set; }

		// Token: 0x06000086 RID: 134 RVA: 0x000036CC File Offset: 0x000018CC
		public static TriggerCollection GetTriggers(DependencyObject obj)
		{
			TriggerCollection triggerCollection = (TriggerCollection)obj.GetValue(Interaction.TriggersProperty);
			if (triggerCollection == null)
			{
				triggerCollection = new TriggerCollection();
				obj.SetValue(Interaction.TriggersProperty, triggerCollection);
			}
			return triggerCollection;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003700 File Offset: 0x00001900
		public static BehaviorCollection GetBehaviors(DependencyObject obj)
		{
			BehaviorCollection behaviorCollection = (BehaviorCollection)obj.GetValue(Interaction.BehaviorsProperty);
			if (behaviorCollection == null)
			{
				behaviorCollection = new BehaviorCollection();
				obj.SetValue(Interaction.BehaviorsProperty, behaviorCollection);
			}
			return behaviorCollection;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003734 File Offset: 0x00001934
		private static void OnBehaviorsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			BehaviorCollection behaviorCollection = (BehaviorCollection)args.OldValue;
			BehaviorCollection behaviorCollection2 = (BehaviorCollection)args.NewValue;
			if (behaviorCollection != behaviorCollection2)
			{
				if (behaviorCollection != null && ((IAttachedObject)behaviorCollection).AssociatedObject != null)
				{
					behaviorCollection.Detach();
				}
				if (behaviorCollection2 != null && obj != null)
				{
					if (((IAttachedObject)behaviorCollection2).AssociatedObject != null)
					{
						throw new InvalidOperationException(ExceptionStringTable.CannotHostBehaviorCollectionMultipleTimesExceptionMessage);
					}
					behaviorCollection2.Attach(obj);
				}
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003790 File Offset: 0x00001990
		private static void OnTriggersChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			TriggerCollection triggerCollection = args.OldValue as TriggerCollection;
			TriggerCollection triggerCollection2 = args.NewValue as TriggerCollection;
			if (triggerCollection != triggerCollection2)
			{
				if (triggerCollection != null && ((IAttachedObject)triggerCollection).AssociatedObject != null)
				{
					triggerCollection.Detach();
				}
				if (triggerCollection2 != null && obj != null)
				{
					if (((IAttachedObject)triggerCollection2).AssociatedObject != null)
					{
						throw new InvalidOperationException(ExceptionStringTable.CannotHostTriggerCollectionMultipleTimesExceptionMessage);
					}
					triggerCollection2.Attach(obj);
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000037EC File Offset: 0x000019EC
		internal static bool IsElementLoaded(FrameworkElement element)
		{
			return element.IsLoaded;
		}

		// Token: 0x0400002C RID: 44
		private static readonly DependencyProperty TriggersProperty = DependencyProperty.RegisterAttached("ShadowTriggers", typeof(TriggerCollection), typeof(Interaction), new FrameworkPropertyMetadata(new PropertyChangedCallback(Interaction.OnTriggersChanged)));

		// Token: 0x0400002D RID: 45
		private static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached("ShadowBehaviors", typeof(BehaviorCollection), typeof(Interaction), new FrameworkPropertyMetadata(new PropertyChangedCallback(Interaction.OnBehaviorsChanged)));
	}
}

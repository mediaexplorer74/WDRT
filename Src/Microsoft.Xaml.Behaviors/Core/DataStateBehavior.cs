using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000041 RID: 65
	public class DataStateBehavior : Behavior<FrameworkElement>
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000940F File Offset: 0x0000760F
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000941C File Offset: 0x0000761C
		public object Binding
		{
			get
			{
				return base.GetValue(DataStateBehavior.BindingProperty);
			}
			set
			{
				base.SetValue(DataStateBehavior.BindingProperty, value);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000942A File Offset: 0x0000762A
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00009437 File Offset: 0x00007637
		public object Value
		{
			get
			{
				return base.GetValue(DataStateBehavior.ValueProperty);
			}
			set
			{
				base.SetValue(DataStateBehavior.ValueProperty, value);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009445 File Offset: 0x00007645
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00009457 File Offset: 0x00007657
		public string TrueState
		{
			get
			{
				return (string)base.GetValue(DataStateBehavior.TrueStateProperty);
			}
			set
			{
				base.SetValue(DataStateBehavior.TrueStateProperty, value);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009465 File Offset: 0x00007665
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00009477 File Offset: 0x00007677
		public string FalseState
		{
			get
			{
				return (string)base.GetValue(DataStateBehavior.FalseStateProperty);
			}
			set
			{
				base.SetValue(DataStateBehavior.FalseStateProperty, value);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009485 File Offset: 0x00007685
		private FrameworkElement TargetObject
		{
			get
			{
				return VisualStateUtilities.FindNearestStatefulControl(base.AssociatedObject);
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009492 File Offset: 0x00007692
		protected override void OnAttached()
		{
			base.OnAttached();
			this.ValidateStateNamesDeferred();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000094A0 File Offset: 0x000076A0
		private void ValidateStateNamesDeferred()
		{
			FrameworkElement frameworkElement = base.AssociatedObject.Parent as FrameworkElement;
			if (frameworkElement != null && DataStateBehavior.IsElementLoaded(frameworkElement))
			{
				this.ValidateStateNames();
				return;
			}
			base.AssociatedObject.Loaded += delegate(object o, RoutedEventArgs e)
			{
				this.ValidateStateNames();
			};
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000094E7 File Offset: 0x000076E7
		internal static bool IsElementLoaded(FrameworkElement element)
		{
			return element.IsLoaded;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000094EF File Offset: 0x000076EF
		private void ValidateStateNames()
		{
			this.ValidateStateName(this.TrueState);
			this.ValidateStateName(this.FalseState);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000950C File Offset: 0x0000770C
		private void ValidateStateName(string stateName)
		{
			if (base.AssociatedObject != null && !string.IsNullOrEmpty(stateName))
			{
				foreach (VisualState visualState in this.TargetedVisualStates)
				{
					if (stateName == visualState.Name)
					{
						return;
					}
				}
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.DataStateBehaviorStateNameNotFoundExceptionMessage, new object[]
				{
					stateName,
					(this.TargetObject != null) ? this.TargetObject.GetType().Name : "null"
				}));
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000095B8 File Offset: 0x000077B8
		private IEnumerable<VisualState> TargetedVisualStates
		{
			get
			{
				List<VisualState> list = new List<VisualState>();
				if (this.TargetObject != null)
				{
					foreach (object obj in VisualStateUtilities.GetVisualStateGroups(this.TargetObject))
					{
						foreach (object obj2 in ((VisualStateGroup)obj).States)
						{
							VisualState visualState = (VisualState)obj2;
							list.Add(visualState);
						}
					}
				}
				return list;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000966C File Offset: 0x0000786C
		private static void OnBindingChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			((DataStateBehavior)obj).Evaluate();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009679 File Offset: 0x00007879
		private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			((DataStateBehavior)obj).Evaluate();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009686 File Offset: 0x00007886
		private static void OnTrueStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			DataStateBehavior dataStateBehavior = (DataStateBehavior)obj;
			dataStateBehavior.ValidateStateName(dataStateBehavior.TrueState);
			dataStateBehavior.Evaluate();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000969F File Offset: 0x0000789F
		private static void OnFalseStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			DataStateBehavior dataStateBehavior = (DataStateBehavior)obj;
			dataStateBehavior.ValidateStateName(dataStateBehavior.FalseState);
			dataStateBehavior.Evaluate();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000096B8 File Offset: 0x000078B8
		private void Evaluate()
		{
			if (this.TargetObject != null)
			{
				string text;
				if (ComparisonLogic.EvaluateImpl(this.Binding, ComparisonConditionType.Equal, this.Value))
				{
					text = this.TrueState;
				}
				else
				{
					text = this.FalseState;
				}
				VisualStateUtilities.GoToState(this.TargetObject, text, true);
			}
		}

		// Token: 0x040000C3 RID: 195
		public static readonly DependencyProperty BindingProperty = DependencyProperty.Register("Binding", typeof(object), typeof(DataStateBehavior), new PropertyMetadata(new PropertyChangedCallback(DataStateBehavior.OnBindingChanged)));

		// Token: 0x040000C4 RID: 196
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(DataStateBehavior), new PropertyMetadata(new PropertyChangedCallback(DataStateBehavior.OnValueChanged)));

		// Token: 0x040000C5 RID: 197
		public static readonly DependencyProperty TrueStateProperty = DependencyProperty.Register("TrueState", typeof(string), typeof(DataStateBehavior), new PropertyMetadata(new PropertyChangedCallback(DataStateBehavior.OnTrueStateChanged)));

		// Token: 0x040000C6 RID: 198
		public static readonly DependencyProperty FalseStateProperty = DependencyProperty.Register("FalseState", typeof(string), typeof(DataStateBehavior), new PropertyMetadata(new PropertyChangedCallback(DataStateBehavior.OnFalseStateChanged)));
	}
}

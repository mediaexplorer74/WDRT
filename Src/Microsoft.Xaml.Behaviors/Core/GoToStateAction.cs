using System;
using System.Globalization;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000045 RID: 69
	public class GoToStateAction : TargetedTriggerAction<FrameworkElement>
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000BB2B File Offset: 0x00009D2B
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000BB3D File Offset: 0x00009D3D
		public bool UseTransitions
		{
			get
			{
				return (bool)base.GetValue(GoToStateAction.UseTransitionsProperty);
			}
			set
			{
				base.SetValue(GoToStateAction.UseTransitionsProperty, value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000BB50 File Offset: 0x00009D50
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000BB62 File Offset: 0x00009D62
		public string StateName
		{
			get
			{
				return (string)base.GetValue(GoToStateAction.StateNameProperty);
			}
			set
			{
				base.SetValue(GoToStateAction.StateNameProperty, value);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000BB70 File Offset: 0x00009D70
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000BB78 File Offset: 0x00009D78
		private FrameworkElement StateTarget { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000BB81 File Offset: 0x00009D81
		private bool IsTargetObjectSet
		{
			get
			{
				return base.ReadLocalValue(TargetedTriggerAction.TargetObjectProperty) != DependencyProperty.UnsetValue;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000BB98 File Offset: 0x00009D98
		protected override void OnTargetChanged(FrameworkElement oldTarget, FrameworkElement newTarget)
		{
			base.OnTargetChanged(oldTarget, newTarget);
			FrameworkElement frameworkElement = null;
			if (string.IsNullOrEmpty(base.TargetName) && !this.IsTargetObjectSet)
			{
				if (!VisualStateUtilities.TryFindNearestStatefulControl(base.AssociatedObject as FrameworkElement, out frameworkElement) && frameworkElement != null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.GoToStateActionTargetHasNoStateGroups, new object[] { frameworkElement.Name }));
				}
			}
			else
			{
				frameworkElement = base.Target;
			}
			this.StateTarget = frameworkElement;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000BC0D File Offset: 0x00009E0D
		protected override void Invoke(object parameter)
		{
			if (base.AssociatedObject != null)
			{
				this.InvokeImpl(this.StateTarget);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000BC23 File Offset: 0x00009E23
		internal void InvokeImpl(FrameworkElement stateTarget)
		{
			if (stateTarget != null)
			{
				VisualStateUtilities.GoToState(stateTarget, this.StateName, this.UseTransitions);
			}
		}

		// Token: 0x040000D8 RID: 216
		public static readonly DependencyProperty UseTransitionsProperty = DependencyProperty.Register("UseTransitions", typeof(bool), typeof(GoToStateAction), new PropertyMetadata(true));

		// Token: 0x040000D9 RID: 217
		public static readonly DependencyProperty StateNameProperty = DependencyProperty.Register("StateName", typeof(string), typeof(GoToStateAction), new PropertyMetadata(string.Empty));
	}
}

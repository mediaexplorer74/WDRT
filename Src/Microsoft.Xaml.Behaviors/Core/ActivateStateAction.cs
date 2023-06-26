using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x0200004A RID: 74
	public sealed class ActivateStateAction : PrototypingActionBase
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000BE39 File Offset: 0x0000A039
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000BE4B File Offset: 0x0000A04B
		public string TargetScreen
		{
			get
			{
				return base.GetValue(ActivateStateAction.TargetScreenProperty) as string;
			}
			set
			{
				base.SetValue(ActivateStateAction.TargetScreenProperty, value);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000BE59 File Offset: 0x0000A059
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000BE6B File Offset: 0x0000A06B
		public string TargetState
		{
			get
			{
				return base.GetValue(ActivateStateAction.TargetStateProperty) as string;
			}
			set
			{
				base.SetValue(ActivateStateAction.TargetStateProperty, value);
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000BE7C File Offset: 0x0000A07C
		protected override void Invoke(object parameter)
		{
			string text = this.TargetScreen;
			if (string.IsNullOrEmpty(text))
			{
				text = base.GetContainingScreen().GetType().ToString();
			}
			InteractionContext.GoToState(text, this.TargetState);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000BEB5 File Offset: 0x0000A0B5
		protected override Freezable CreateInstanceCore()
		{
			return new ActivateStateAction();
		}

		// Token: 0x040000DD RID: 221
		public static readonly DependencyProperty TargetScreenProperty = DependencyProperty.Register("TargetScreen", typeof(string), typeof(ActivateStateAction), new PropertyMetadata(null));

		// Token: 0x040000DE RID: 222
		public static readonly DependencyProperty TargetStateProperty = DependencyProperty.Register("TargetState", typeof(string), typeof(ActivateStateAction), new PropertyMetadata(null));
	}
}

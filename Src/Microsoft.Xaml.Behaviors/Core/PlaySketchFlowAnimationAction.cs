using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x0200004E RID: 78
	public sealed class PlaySketchFlowAnimationAction : PrototypingActionBase
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000BFEA File Offset: 0x0000A1EA
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		public string TargetScreen
		{
			get
			{
				return base.GetValue(PlaySketchFlowAnimationAction.TargetScreenProperty) as string;
			}
			set
			{
				base.SetValue(PlaySketchFlowAnimationAction.TargetScreenProperty, value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000C00A File Offset: 0x0000A20A
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000C01C File Offset: 0x0000A21C
		public string SketchFlowAnimation
		{
			get
			{
				return base.GetValue(PlaySketchFlowAnimationAction.SketchFlowAnimationProperty) as string;
			}
			set
			{
				base.SetValue(PlaySketchFlowAnimationAction.SketchFlowAnimationProperty, value);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000C02C File Offset: 0x0000A22C
		protected override void Invoke(object parameter)
		{
			string text = this.TargetScreen;
			if (string.IsNullOrEmpty(text))
			{
				text = base.GetContainingScreen().GetType().ToString();
			}
			InteractionContext.PlaySketchFlowAnimation(this.SketchFlowAnimation, text);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000C065 File Offset: 0x0000A265
		protected override Freezable CreateInstanceCore()
		{
			return new PlaySketchFlowAnimationAction();
		}

		// Token: 0x040000E0 RID: 224
		public static readonly DependencyProperty TargetScreenProperty = DependencyProperty.Register("TargetScreen", typeof(string), typeof(PlaySketchFlowAnimationAction), new PropertyMetadata(null));

		// Token: 0x040000E1 RID: 225
		public static readonly DependencyProperty SketchFlowAnimationProperty = DependencyProperty.Register("StateAnimation", typeof(string), typeof(PlaySketchFlowAnimationAction), new PropertyMetadata(null));
	}
}

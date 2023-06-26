using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Microsoft.Xaml.Behaviors.Media
{
	// Token: 0x0200002A RID: 42
	[CLSCompliant(false)]
	public class ControlStoryboardAction : StoryboardAction
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005496 File Offset: 0x00003696
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000054A8 File Offset: 0x000036A8
		public ControlStoryboardOption ControlStoryboardOption
		{
			get
			{
				return (ControlStoryboardOption)base.GetValue(ControlStoryboardAction.ControlStoryboardProperty);
			}
			set
			{
				base.SetValue(ControlStoryboardAction.ControlStoryboardProperty, value);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000054BC File Offset: 0x000036BC
		protected override void Invoke(object parameter)
		{
			if (base.AssociatedObject != null && base.Storyboard != null)
			{
				switch (this.ControlStoryboardOption)
				{
				case ControlStoryboardOption.Play:
					base.Storyboard.Begin();
					return;
				case ControlStoryboardOption.Stop:
					base.Storyboard.Stop();
					return;
				case ControlStoryboardOption.TogglePlayPause:
				{
					ClockState clockState = ClockState.Stopped;
					bool flag = false;
					try
					{
						clockState = base.Storyboard.GetCurrentState();
						flag = base.Storyboard.GetIsPaused();
					}
					catch (InvalidOperationException)
					{
					}
					if (clockState == ClockState.Stopped)
					{
						base.Storyboard.Begin();
						return;
					}
					if (flag)
					{
						base.Storyboard.Resume();
						return;
					}
					base.Storyboard.Pause();
					return;
				}
				case ControlStoryboardOption.Pause:
					base.Storyboard.Pause();
					return;
				case ControlStoryboardOption.Resume:
					base.Storyboard.Resume();
					return;
				case ControlStoryboardOption.SkipToFill:
					base.Storyboard.SkipToFill();
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0400006B RID: 107
		public static readonly DependencyProperty ControlStoryboardProperty = DependencyProperty.Register("ControlStoryboardOption", typeof(ControlStoryboardOption), typeof(ControlStoryboardAction));
	}
}

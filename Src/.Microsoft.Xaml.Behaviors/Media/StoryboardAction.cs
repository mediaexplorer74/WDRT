using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Microsoft.Xaml.Behaviors.Media
{
	// Token: 0x02000028 RID: 40
	public abstract class StoryboardAction : TriggerAction<DependencyObject>
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000540F File Offset: 0x0000360F
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00005421 File Offset: 0x00003621
		public Storyboard Storyboard
		{
			get
			{
				return (Storyboard)base.GetValue(StoryboardAction.StoryboardProperty);
			}
			set
			{
				base.SetValue(StoryboardAction.StoryboardProperty, value);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005430 File Offset: 0x00003630
		private static void OnStoryboardChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			StoryboardAction storyboardAction = sender as StoryboardAction;
			if (storyboardAction != null)
			{
				storyboardAction.OnStoryboardChanged(args);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000544E File Offset: 0x0000364E
		protected virtual void OnStoryboardChanged(DependencyPropertyChangedEventArgs args)
		{
		}

		// Token: 0x04000063 RID: 99
		public static readonly DependencyProperty StoryboardProperty = DependencyProperty.Register("Storyboard", typeof(Storyboard), typeof(StoryboardAction), new FrameworkPropertyMetadata(new PropertyChangedCallback(StoryboardAction.OnStoryboardChanged)));
	}
}

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Microsoft.Xaml.Behaviors.Media
{
	// Token: 0x0200002B RID: 43
	public abstract class StoryboardTrigger : TriggerBase<DependencyObject>
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000055C1 File Offset: 0x000037C1
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000055D3 File Offset: 0x000037D3
		public Storyboard Storyboard
		{
			get
			{
				return (Storyboard)base.GetValue(StoryboardTrigger.StoryboardProperty);
			}
			set
			{
				base.SetValue(StoryboardTrigger.StoryboardProperty, value);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000055E4 File Offset: 0x000037E4
		private static void OnStoryboardChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			StoryboardTrigger storyboardTrigger = sender as StoryboardTrigger;
			if (storyboardTrigger != null)
			{
				storyboardTrigger.OnStoryboardChanged(args);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005602 File Offset: 0x00003802
		protected virtual void OnStoryboardChanged(DependencyPropertyChangedEventArgs args)
		{
		}

		// Token: 0x0400006C RID: 108
		public static readonly DependencyProperty StoryboardProperty = DependencyProperty.Register("Storyboard", typeof(Storyboard), typeof(StoryboardTrigger), new FrameworkPropertyMetadata(new PropertyChangedCallback(StoryboardTrigger.OnStoryboardChanged)));
	}
}

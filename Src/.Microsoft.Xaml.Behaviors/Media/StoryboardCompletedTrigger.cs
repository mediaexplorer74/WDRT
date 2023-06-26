using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Microsoft.Xaml.Behaviors.Media
{
	// Token: 0x0200002C RID: 44
	public class StoryboardCompletedTrigger : StoryboardTrigger
	{
		// Token: 0x0600014E RID: 334 RVA: 0x0000564A File Offset: 0x0000384A
		protected override void OnDetaching()
		{
			base.OnDetaching();
			if (base.Storyboard != null)
			{
				base.Storyboard.Completed -= this.Storyboard_Completed;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005674 File Offset: 0x00003874
		protected override void OnStoryboardChanged(DependencyPropertyChangedEventArgs args)
		{
			Storyboard storyboard = args.OldValue as Storyboard;
			Storyboard storyboard2 = args.NewValue as Storyboard;
			if (storyboard != storyboard2)
			{
				if (storyboard != null)
				{
					storyboard.Completed -= this.Storyboard_Completed;
				}
				if (storyboard2 != null)
				{
					storyboard2.Completed += this.Storyboard_Completed;
				}
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000056C9 File Offset: 0x000038C9
		private void Storyboard_Completed(object sender, EventArgs e)
		{
			base.InvokeActions(e);
		}
	}
}

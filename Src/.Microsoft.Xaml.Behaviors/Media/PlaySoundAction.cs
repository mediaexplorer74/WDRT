using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Microsoft.Xaml.Behaviors.Media
{
	// Token: 0x02000027 RID: 39
	public class PlaySoundAction : TriggerAction<DependencyObject>
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000052BE File Offset: 0x000034BE
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000052D0 File Offset: 0x000034D0
		public Uri Source
		{
			get
			{
				return (Uri)base.GetValue(PlaySoundAction.SourceProperty);
			}
			set
			{
				base.SetValue(PlaySoundAction.SourceProperty, value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000052DE File Offset: 0x000034DE
		// (set) Token: 0x06000138 RID: 312 RVA: 0x000052F0 File Offset: 0x000034F0
		public double Volume
		{
			get
			{
				return (double)base.GetValue(PlaySoundAction.VolumeProperty);
			}
			set
			{
				base.SetValue(PlaySoundAction.VolumeProperty, value);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005303 File Offset: 0x00003503
		protected virtual void SetMediaElementProperties(MediaElement mediaElement)
		{
			if (mediaElement != null)
			{
				mediaElement.Source = this.Source;
				mediaElement.Volume = this.Volume;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005320 File Offset: 0x00003520
		protected override void Invoke(object parameter)
		{
			if (this.Source == null || base.AssociatedObject == null)
			{
				return;
			}
			Popup popup = new Popup();
			MediaElement mediaElement = new MediaElement();
			popup.Child = mediaElement;
			mediaElement.Visibility = Visibility.Collapsed;
			this.SetMediaElementProperties(mediaElement);
			mediaElement.MediaEnded += delegate
			{
				popup.Child = null;
				popup.IsOpen = false;
			};
			mediaElement.MediaFailed += delegate
			{
				popup.Child = null;
				popup.IsOpen = false;
			};
			popup.IsOpen = true;
		}

		// Token: 0x04000061 RID: 97
		public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(Uri), typeof(PlaySoundAction), null);

		// Token: 0x04000062 RID: 98
		public static readonly DependencyProperty VolumeProperty = DependencyProperty.Register("Volume", typeof(double), typeof(PlaySoundAction), new PropertyMetadata(0.5));
	}
}

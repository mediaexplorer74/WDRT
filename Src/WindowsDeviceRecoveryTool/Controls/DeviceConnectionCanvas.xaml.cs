using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000DA RID: 218
	public partial class DeviceConnectionCanvas : Grid
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x0001F654 File Offset: 0x0001D854
		public DeviceConnectionCanvas()
		{
			this.InitializeComponent();
			base.Loaded += this.OnDeviceConnectionCanvasLoaded;
			base.Unloaded += this.OnDeviceConnectionCanvasUnLoaded;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001F68C File Offset: 0x0001D88C
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0001F6AE File Offset: 0x0001D8AE
		public bool PlayAnimation
		{
			get
			{
				return (bool)base.GetValue(DeviceConnectionCanvas.PlayAnimationProperty);
			}
			set
			{
				base.SetValue(DeviceConnectionCanvas.PlayAnimationProperty, value);
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001F6C4 File Offset: 0x0001D8C4
		private void OnDeviceConnectionCanvasLoaded(object sender, RoutedEventArgs e)
		{
			Storyboard storyboard = base.FindResource("FadeInOutAnimation") as Storyboard;
			bool flag = storyboard != null;
			if (flag)
			{
				bool playAnimation = this.PlayAnimation;
				if (playAnimation)
				{
					storyboard.Begin();
				}
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001F700 File Offset: 0x0001D900
		private void OnDeviceConnectionCanvasUnLoaded(object sender, RoutedEventArgs e)
		{
			Storyboard storyboard = base.FindResource("FadeInOutAnimation") as Storyboard;
			bool flag = storyboard != null;
			if (flag)
			{
				bool playAnimation = this.PlayAnimation;
				if (playAnimation)
				{
					storyboard.Stop();
				}
			}
		}

		// Token: 0x04000316 RID: 790
		public static readonly DependencyProperty PlayAnimationProperty = DependencyProperty.Register("PlayAnimation", typeof(bool), typeof(DeviceConnectionCanvas), new PropertyMetadata(false));
	}
}

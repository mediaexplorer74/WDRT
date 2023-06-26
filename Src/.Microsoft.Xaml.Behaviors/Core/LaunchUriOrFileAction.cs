using System;
using System.Diagnostics;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000047 RID: 71
	public class LaunchUriOrFileAction : TriggerAction<DependencyObject>
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000BCC6 File Offset: 0x00009EC6
		public string Path
		{
			get
			{
				return (string)base.GetValue(LaunchUriOrFileAction.PathProperty);
			}
			set
			{
				base.SetValue(LaunchUriOrFileAction.PathProperty, value);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		protected override void Invoke(object parameter)
		{
			if (base.AssociatedObject != null && !string.IsNullOrEmpty(this.Path))
			{
				Process.Start(new ProcessStartInfo(this.Path)
				{
					UseShellExecute = true
				});
			}
		}

		// Token: 0x040000DB RID: 219
		public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof(string), typeof(LaunchUriOrFileAction));
	}
}

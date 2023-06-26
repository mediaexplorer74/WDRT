using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000CB RID: 203
	public partial class DeviceSwInfoControl : ContentControl
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x0001DD33 File Offset: 0x0001BF33
		public DeviceSwInfoControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001DD44 File Offset: 0x0001BF44
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0001DD66 File Offset: 0x0001BF66
		public Visibility AkVersionVisibility
		{
			get
			{
				return (Visibility)base.GetValue(DeviceSwInfoControl.AkVersionVisibilityProperty);
			}
			set
			{
				base.SetValue(DeviceSwInfoControl.AkVersionVisibilityProperty, value);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001DD7C File Offset: 0x0001BF7C
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0001DD9E File Offset: 0x0001BF9E
		public Visibility FirmwareVersionVisibility
		{
			get
			{
				return (Visibility)base.GetValue(DeviceSwInfoControl.FirmwareVersionVisibilityProperty);
			}
			set
			{
				base.SetValue(DeviceSwInfoControl.FirmwareVersionVisibilityProperty, value);
			}
		}

		// Token: 0x040002D6 RID: 726
		public static readonly DependencyProperty AkVersionVisibilityProperty = DependencyProperty.Register("AkVersionVisibility", typeof(Visibility), typeof(DeviceSwInfoControl), null);

		// Token: 0x040002D7 RID: 727
		public static readonly DependencyProperty FirmwareVersionVisibilityProperty = DependencyProperty.Register("FirmwareVersionVisibility", typeof(Visibility), typeof(DeviceSwInfoControl), null);
	}
}

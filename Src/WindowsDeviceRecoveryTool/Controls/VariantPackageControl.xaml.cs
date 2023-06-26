using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000D1 RID: 209
	public partial class VariantPackageControl : UserControl
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x0001E912 File Offset: 0x0001CB12
		public VariantPackageControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001E924 File Offset: 0x0001CB24
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x0001E946 File Offset: 0x0001CB46
		public bool IsControlVisible
		{
			get
			{
				return (bool)base.GetValue(VariantPackageControl.IsControlVisibleProperty);
			}
			set
			{
				base.SetValue(VariantPackageControl.IsControlVisibleProperty, value);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001E95C File Offset: 0x0001CB5C
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x0001E97E File Offset: 0x0001CB7E
		public Visibility AkVersionVisibility
		{
			get
			{
				return (Visibility)base.GetValue(VariantPackageControl.AkVersionVisibilityProperty);
			}
			set
			{
				base.SetValue(VariantPackageControl.AkVersionVisibilityProperty, value);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001E994 File Offset: 0x0001CB94
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0001E9B6 File Offset: 0x0001CBB6
		public Visibility PlatformIdVisibility
		{
			get
			{
				return (Visibility)base.GetValue(VariantPackageControl.PlatformIdVisibilityProperty);
			}
			set
			{
				base.SetValue(VariantPackageControl.PlatformIdVisibilityProperty, value);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001E9CC File Offset: 0x0001CBCC
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0001E9EE File Offset: 0x0001CBEE
		public Visibility FirmwareVersionVisibility
		{
			get
			{
				return (Visibility)base.GetValue(VariantPackageControl.FirmwareVersionVisibilityProperty);
			}
			set
			{
				base.SetValue(VariantPackageControl.FirmwareVersionVisibilityProperty, value);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001EA04 File Offset: 0x0001CC04
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x0001EA26 File Offset: 0x0001CC26
		public Visibility BuildVersionVisibility
		{
			get
			{
				return (Visibility)base.GetValue(VariantPackageControl.BuildVersionVisibilityProperty);
			}
			set
			{
				base.SetValue(VariantPackageControl.BuildVersionVisibilityProperty, value);
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001EA3C File Offset: 0x0001CC3C
		private static void OnIsControlVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			VariantPackageControl variantPackageControl = d as VariantPackageControl;
			bool flag = variantPackageControl != null;
			if (flag)
			{
				variantPackageControl.MainStackPanel.Visibility = (Visibility)new BooleanToVisibilityConverter().Convert(e.NewValue, null, null, null);
			}
		}

		// Token: 0x040002F2 RID: 754
		public static readonly DependencyProperty AkVersionVisibilityProperty = DependencyProperty.Register("AkVersionVisibility", typeof(Visibility), typeof(VariantPackageControl), null);

		// Token: 0x040002F3 RID: 755
		public static readonly DependencyProperty PlatformIdVisibilityProperty = DependencyProperty.Register("PlatformIdVisibility", typeof(Visibility), typeof(VariantPackageControl), null);

		// Token: 0x040002F4 RID: 756
		public static readonly DependencyProperty IsControlVisibleProperty = DependencyProperty.Register("IsControlVisible", typeof(bool), typeof(VariantPackageControl), new PropertyMetadata(false, new PropertyChangedCallback(VariantPackageControl.OnIsControlVisibleChanged)));

		// Token: 0x040002F5 RID: 757
		public static readonly DependencyProperty FirmwareVersionVisibilityProperty = DependencyProperty.Register("FirmwareVersionVisibility", typeof(Visibility), typeof(VariantPackageControl), null);

		// Token: 0x040002F6 RID: 758
		public static readonly DependencyProperty BuildVersionVisibilityProperty = DependencyProperty.Register("BuildVersionVisibility", typeof(Visibility), typeof(VariantPackageControl), null);
	}
}

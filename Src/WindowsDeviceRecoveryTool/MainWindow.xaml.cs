using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.WindowsDeviceRecoveryTool.Localization;

namespace Microsoft.WindowsDeviceRecoveryTool
{
	// Token: 0x02000007 RID: 7
	[Export("ShellWindow")]
	public sealed partial class MainWindow : Window
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000024A3 File Offset: 0x000006A3
		public MainWindow()
		{
			this.InitializeComponent();
			LocalizationManager.Instance().LanguageChanged += this.OnLanguageChanged;
			this.OnLanguageChanged(this, null);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024D4 File Offset: 0x000006D4
		private void OnLanguageChanged(object sender, EventArgs e)
		{
			FlowDirection flowDirection = (LocalizationManager.Instance().CurrentLanguage.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight);
			base.SetCurrentValue(FrameworkElement.FlowDirectionProperty, flowDirection);
		}
	}
}

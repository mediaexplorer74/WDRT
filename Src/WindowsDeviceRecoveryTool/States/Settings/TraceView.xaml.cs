using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.WindowsDeviceRecoveryTool.Framework;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x02000034 RID: 52
	[Export]
	[Region(new string[] { "SettingsMainArea" })]
	public partial class TraceView : StackPanel
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public TraceView()
		{
			this.InitializeComponent();
		}
	}
}

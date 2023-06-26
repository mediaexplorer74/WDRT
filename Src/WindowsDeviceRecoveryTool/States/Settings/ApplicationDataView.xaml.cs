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
	// Token: 0x02000025 RID: 37
	[Export]
	[Region(new string[] { "SettingsMainArea" })]
	public partial class ApplicationDataView : StackPanel
	{
		// Token: 0x0600019A RID: 410 RVA: 0x0000A17C File Offset: 0x0000837C
		public ApplicationDataView()
		{
			this.InitializeComponent();
		}
	}
}

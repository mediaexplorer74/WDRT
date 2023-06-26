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
	// Token: 0x0200002A RID: 42
	[Export]
	[Region(new string[] { "SettingsMainArea" })]
	public partial class PackagesView : StackPanel
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x0000BE56 File Offset: 0x0000A056
		public PackagesView()
		{
			this.InitializeComponent();
		}
	}
}

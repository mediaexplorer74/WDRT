using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.WindowsDeviceRecoveryTool.Framework;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x0200003E RID: 62
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class BatteryCheckingView : Grid
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000EB5A File Offset: 0x0000CD5A
		public BatteryCheckingView()
		{
			this.InitializeComponent();
		}
	}
}

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
	// Token: 0x02000040 RID: 64
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class AppUpdateCheckingView : Grid
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000F173 File Offset: 0x0000D373
		public AppUpdateCheckingView()
		{
			this.InitializeComponent();
		}
	}
}

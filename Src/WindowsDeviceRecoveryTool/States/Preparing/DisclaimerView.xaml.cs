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
	// Token: 0x02000050 RID: 80
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class DisclaimerView : Grid
	{
		// Token: 0x0600031B RID: 795 RVA: 0x00011654 File Offset: 0x0000F854
		public DisclaimerView()
		{
			this.InitializeComponent();
		}
	}
}

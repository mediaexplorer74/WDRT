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
	// Token: 0x0200004E RID: 78
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class ManualHtcRestartView : Grid
	{
		// Token: 0x0600030C RID: 780 RVA: 0x000111D8 File Offset: 0x0000F3D8
		public ManualHtcRestartView()
		{
			this.InitializeComponent();
		}
	}
}

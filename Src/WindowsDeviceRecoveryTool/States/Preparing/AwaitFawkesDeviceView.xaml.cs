using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;
using Microsoft.WindowsDeviceRecoveryTool.Framework;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x02000038 RID: 56
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class AwaitFawkesDeviceView : Grid
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000DFE5 File Offset: 0x0000C1E5
		public AwaitFawkesDeviceView()
		{
			this.InitializeComponent();
		}
	}
}

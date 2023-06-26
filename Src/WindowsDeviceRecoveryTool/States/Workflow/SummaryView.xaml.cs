using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.WindowsDeviceRecoveryTool.Framework;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Workflow
{
	// Token: 0x0200001F RID: 31
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class SummaryView : Grid
	{
		// Token: 0x06000165 RID: 357 RVA: 0x0000948F File Offset: 0x0000768F
		public SummaryView()
		{
			this.InitializeComponent();
		}
	}
}

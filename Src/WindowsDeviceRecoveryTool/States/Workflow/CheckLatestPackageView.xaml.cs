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
	// Token: 0x02000012 RID: 18
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class CheckLatestPackageView : Grid
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00005C58 File Offset: 0x00003E58
		public CheckLatestPackageView()
		{
			this.InitializeComponent();
		}
	}
}

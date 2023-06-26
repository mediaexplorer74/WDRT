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
	// Token: 0x0200001C RID: 28
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class ManualPackageSelectionView : Grid
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00008C98 File Offset: 0x00006E98
		public ManualPackageSelectionView()
		{
			this.InitializeComponent();
		}
	}
}

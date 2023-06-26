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
	// Token: 0x02000014 RID: 20
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class AutomaticPackageSelectionView : Grid
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x0000725A File Offset: 0x0000545A
		public AutomaticPackageSelectionView()
		{
			this.InitializeComponent();
		}
	}
}

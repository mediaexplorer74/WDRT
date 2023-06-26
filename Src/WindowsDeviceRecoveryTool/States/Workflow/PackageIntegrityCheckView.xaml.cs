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
	// Token: 0x02000019 RID: 25
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class PackageIntegrityCheckView : Grid
	{
		// Token: 0x0600012C RID: 300 RVA: 0x000085D8 File Offset: 0x000067D8
		public PackageIntegrityCheckView()
		{
			this.InitializeComponent();
		}
	}
}

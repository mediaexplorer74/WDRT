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
	// Token: 0x0200000D RID: 13
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class DownloadEmergencyPackageView : Grid
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000511C File Offset: 0x0000331C
		public DownloadEmergencyPackageView()
		{
			this.InitializeComponent();
		}
	}
}

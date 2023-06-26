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
	// Token: 0x02000017 RID: 23
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class DownloadPackageView : Grid
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00007FC8 File Offset: 0x000061C8
		public DownloadPackageView()
		{
			this.InitializeComponent();
		}
	}
}

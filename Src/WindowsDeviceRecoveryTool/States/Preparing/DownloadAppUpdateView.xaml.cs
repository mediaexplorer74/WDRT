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
	// Token: 0x02000048 RID: 72
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class DownloadAppUpdateView : Grid
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x0000FED4 File Offset: 0x0000E0D4
		public DownloadAppUpdateView()
		{
			this.InitializeComponent();
		}
	}
}

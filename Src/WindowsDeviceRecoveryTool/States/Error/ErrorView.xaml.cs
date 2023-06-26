using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.WindowsDeviceRecoveryTool.Framework;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Error
{
	// Token: 0x02000070 RID: 112
	[Export]
	[Region(new string[] { "MainArea" })]
	public sealed partial class ErrorView : Grid
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x00015074 File Offset: 0x00013274
		public ErrorView()
		{
			this.InitializeComponent();
		}
	}
}

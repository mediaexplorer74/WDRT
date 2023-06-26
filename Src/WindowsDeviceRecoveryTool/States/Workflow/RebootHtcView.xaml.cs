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
	// Token: 0x02000011 RID: 17
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class RebootHtcView : Grid
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00005BE8 File Offset: 0x00003DE8
		public RebootHtcView()
		{
			this.InitializeComponent();
		}
	}
}

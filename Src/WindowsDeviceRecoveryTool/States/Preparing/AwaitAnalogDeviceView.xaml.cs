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
	// Token: 0x02000045 RID: 69
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class AwaitAnalogDeviceView : Grid
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000FA0D File Offset: 0x0000DC0D
		public AwaitAnalogDeviceView()
		{
			this.InitializeComponent();
		}
	}
}

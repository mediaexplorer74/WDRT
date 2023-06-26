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
	// Token: 0x02000043 RID: 67
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class AbsoluteConfirmationView : Grid
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x0000F7E9 File Offset: 0x0000D9E9
		public AbsoluteConfirmationView()
		{
			this.InitializeComponent();
		}
	}
}

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
	// Token: 0x0200003A RID: 58
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class AwaitGenericDeviceView : Grid
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000E305 File Offset: 0x0000C505
		public AwaitGenericDeviceView()
		{
			this.InitializeComponent();
		}
	}
}

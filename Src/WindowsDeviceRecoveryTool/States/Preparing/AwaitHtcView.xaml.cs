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
	// Token: 0x0200003B RID: 59
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class AwaitHtcView : Grid
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000E358 File Offset: 0x0000C558
		public AwaitHtcView()
		{
			this.InitializeComponent();
		}
	}
}

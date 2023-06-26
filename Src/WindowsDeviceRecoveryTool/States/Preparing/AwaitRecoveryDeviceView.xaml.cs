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
	// Token: 0x02000056 RID: 86
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class AwaitRecoveryDeviceView : Grid
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00012EEE File Offset: 0x000110EE
		public AwaitRecoveryDeviceView()
		{
			this.InitializeComponent();
		}
	}
}

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
	// Token: 0x02000023 RID: 35
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class UnsupportedDeviceView : Grid
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000A0DA File Offset: 0x000082DA
		public UnsupportedDeviceView()
		{
			this.InitializeComponent();
		}
	}
}

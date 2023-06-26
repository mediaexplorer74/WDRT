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
	// Token: 0x02000052 RID: 82
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class ReadingDeviceInfoView : Grid
	{
		// Token: 0x0600032F RID: 815 RVA: 0x000120FE File Offset: 0x000102FE
		public ReadingDeviceInfoView()
		{
			this.InitializeComponent();
		}
	}
}

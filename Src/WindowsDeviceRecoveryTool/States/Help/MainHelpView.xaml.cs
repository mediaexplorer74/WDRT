using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.WindowsDeviceRecoveryTool.Framework;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Help
{
	// Token: 0x0200006D RID: 109
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class MainHelpView : ScrollViewer
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0001486E File Offset: 0x00012A6E
		public MainHelpView()
		{
			this.InitializeComponent();
		}
	}
}

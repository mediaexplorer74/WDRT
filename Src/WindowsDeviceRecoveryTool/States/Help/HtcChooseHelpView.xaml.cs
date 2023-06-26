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
	// Token: 0x02000061 RID: 97
	[Export]
	[Region(new string[] { "HelpMainArea" })]
	public partial class HtcChooseHelpView : UserControl
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x000143C3 File Offset: 0x000125C3
		public HtcChooseHelpView()
		{
			this.InitializeComponent();
		}
	}
}

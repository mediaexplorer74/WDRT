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
	// Token: 0x0200006B RID: 107
	[Export]
	[Region(new string[] { "HelpMainArea" })]
	public partial class LumiaNormalHelpView : UserControl
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x000147E3 File Offset: 0x000129E3
		public LumiaNormalHelpView()
		{
			this.InitializeComponent();
		}
	}
}

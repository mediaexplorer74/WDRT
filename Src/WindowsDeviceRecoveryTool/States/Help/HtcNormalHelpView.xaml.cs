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
	// Token: 0x02000063 RID: 99
	[Export]
	[Region(new string[] { "HelpMainArea" })]
	public partial class HtcNormalHelpView : UserControl
	{
		// Token: 0x060003AA RID: 938 RVA: 0x000144FF File Offset: 0x000126FF
		public HtcNormalHelpView()
		{
			this.InitializeComponent();
		}
	}
}

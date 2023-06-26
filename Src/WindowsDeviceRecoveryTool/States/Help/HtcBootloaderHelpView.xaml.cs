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
	// Token: 0x0200005F RID: 95
	[Export]
	[Region(new string[] { "HelpMainArea" })]
	public partial class HtcBootloaderHelpView : UserControl
	{
		// Token: 0x0600039D RID: 925 RVA: 0x000142F7 File Offset: 0x000124F7
		public HtcBootloaderHelpView()
		{
			this.InitializeComponent();
		}
	}
}

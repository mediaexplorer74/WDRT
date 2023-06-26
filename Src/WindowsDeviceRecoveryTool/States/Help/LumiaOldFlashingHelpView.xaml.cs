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
	// Token: 0x0200005C RID: 92
	[Export]
	[Region(new string[] { "HelpMainArea" })]
	public partial class LumiaOldFlashingHelpView : UserControl
	{
		// Token: 0x0600038C RID: 908 RVA: 0x00013C79 File Offset: 0x00011E79
		public LumiaOldFlashingHelpView()
		{
			this.InitializeComponent();
		}
	}
}

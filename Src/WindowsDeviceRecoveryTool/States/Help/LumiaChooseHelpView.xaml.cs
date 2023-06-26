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
	// Token: 0x02000066 RID: 102
	[Export]
	[Region(new string[] { "HelpMainArea" })]
	public partial class LumiaChooseHelpView : UserControl
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x000145BF File Offset: 0x000127BF
		public LumiaChooseHelpView()
		{
			this.InitializeComponent();
		}
	}
}

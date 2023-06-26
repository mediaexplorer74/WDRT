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
	// Token: 0x02000069 RID: 105
	[Export]
	[Region(new string[] { "HelpMainArea" })]
	public partial class LumiaFlashingHelpView : UserControl
	{
		// Token: 0x060003BA RID: 954 RVA: 0x00014765 File Offset: 0x00012965
		public LumiaFlashingHelpView()
		{
			this.InitializeComponent();
		}
	}
}

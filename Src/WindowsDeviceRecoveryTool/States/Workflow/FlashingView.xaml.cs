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
	// Token: 0x0200001A RID: 26
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class FlashingView : Grid
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00008651 File Offset: 0x00006851
		public FlashingView()
		{
			this.InitializeComponent();
		}
	}
}

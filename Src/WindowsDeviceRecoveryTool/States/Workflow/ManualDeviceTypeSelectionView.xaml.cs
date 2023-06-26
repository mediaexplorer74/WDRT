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
	// Token: 0x02000010 RID: 16
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class ManualDeviceTypeSelectionView : Grid
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00005B97 File Offset: 0x00003D97
		public ManualDeviceTypeSelectionView()
		{
			this.InitializeComponent();
		}
	}
}

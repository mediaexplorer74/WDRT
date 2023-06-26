using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.WindowsDeviceRecoveryTool.Framework;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x02000036 RID: 54
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class AutomaticManufacturerSelectionView : Grid
	{
		// Token: 0x06000249 RID: 585 RVA: 0x0000DAAB File Offset: 0x0000BCAB
		public AutomaticManufacturerSelectionView()
		{
			this.InitializeComponent();
		}
	}
}

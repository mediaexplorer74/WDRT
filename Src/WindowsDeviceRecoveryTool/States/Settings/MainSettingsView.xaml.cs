using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.WindowsDeviceRecoveryTool.Framework;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x0200002D RID: 45
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class MainSettingsView : Grid
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x0000BFA7 File Offset: 0x0000A1A7
		public MainSettingsView()
		{
			this.InitializeComponent();
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
			};
		}
	}
}

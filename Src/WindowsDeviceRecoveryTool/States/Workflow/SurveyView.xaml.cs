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
	// Token: 0x02000021 RID: 33
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class SurveyView : Grid
	{
		// Token: 0x06000179 RID: 377 RVA: 0x000098CE File Offset: 0x00007ACE
		public SurveyView()
		{
			this.InitializeComponent();
		}
	}
}

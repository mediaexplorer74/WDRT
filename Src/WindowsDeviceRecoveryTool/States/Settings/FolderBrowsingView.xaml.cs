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
	// Token: 0x02000027 RID: 39
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class FolderBrowsingView : Grid
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x0000AD94 File Offset: 0x00008F94
		public FolderBrowsingView()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		private void MetroBrowseDialogOnMouseWheel(object sender, MouseWheelEventArgs e)
		{
			int num = e.Delta * -1;
			bool flag = num < 0;
			if (flag)
			{
				bool flag2 = this.FolderScrollViewer.HorizontalOffset + (double)num > 0.0;
				if (flag2)
				{
					this.FolderScrollViewer.ScrollToHorizontalOffset(this.FolderScrollViewer.HorizontalOffset + (double)num);
				}
				else
				{
					this.FolderScrollViewer.ScrollToLeftEnd();
				}
			}
			else
			{
				bool flag3 = this.FolderScrollViewer.ExtentWidth > this.FolderScrollViewer.HorizontalOffset + (double)num;
				if (flag3)
				{
					this.FolderScrollViewer.ScrollToHorizontalOffset(this.FolderScrollViewer.HorizontalOffset + (double)num);
				}
				else
				{
					this.FolderScrollViewer.ScrollToRightEnd();
				}
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000AF0C File Offset: 0x0000910C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 4)
			{
				((WrapPanel)target).MouseWheel += this.MetroBrowseDialogOnMouseWheel;
			}
		}
	}
}

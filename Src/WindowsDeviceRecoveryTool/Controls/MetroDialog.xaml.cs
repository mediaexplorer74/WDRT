using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000D7 RID: 215
	public abstract partial class MetroDialog : Window
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x0001F194 File Offset: 0x0001D394
		protected MetroDialog()
		{
			Window mainWindow = Application.Current.MainWindow;
			base.Owner = mainWindow;
			this.InitializeComponent();
			base.Width = mainWindow.ActualWidth;
			base.Height = mainWindow.ActualHeight;
			this.dialogResult = false;
			bool isRightToLeft = Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft;
			if (isRightToLeft)
			{
				this.MetroDialogWindow.FlowDirection = FlowDirection.RightToLeft;
			}
			else
			{
				this.MetroDialogWindow.FlowDirection = FlowDirection.LeftToRight;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001F21C File Offset: 0x0001D41C
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0001F224 File Offset: 0x0001D424
		public bool Warning { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001F230 File Offset: 0x0001D430
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0001F24D File Offset: 0x0001D44D
		public string Message
		{
			get
			{
				return this.BodyMessage.Text;
			}
			set
			{
				this.BodyMessage.Text = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001F260 File Offset: 0x0001D460
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0001F27D File Offset: 0x0001D47D
		public string MessageTitle
		{
			get
			{
				return this.TitleMessage.Text;
			}
			set
			{
				this.TitleMessage.Text = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001F290 File Offset: 0x0001D490
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0001F2B2 File Offset: 0x0001D4B2
		protected string NoButtonText
		{
			get
			{
				return this.ButtonNo.Content as string;
			}
			set
			{
				this.ButtonNo.Content = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001F2C4 File Offset: 0x0001D4C4
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0001F2E6 File Offset: 0x0001D4E6
		protected string YesButtonText
		{
			get
			{
				return this.ButtonYes.Content as string;
			}
			set
			{
				this.ButtonYes.Content = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001F2F8 File Offset: 0x0001D4F8
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0001F31C File Offset: 0x0001D51C
		protected bool YesButtonFocused
		{
			get
			{
				return object.Equals(FocusManager.GetFocusedElement(this), this.ButtonYes);
			}
			set
			{
				if (value)
				{
					FocusManager.SetFocusedElement(this, this.ButtonYes);
				}
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001F340 File Offset: 0x0001D540
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			Window mainWindow = Application.Current.MainWindow;
			mainWindow.Left = base.Left;
			mainWindow.Top = base.Top;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001F37B File Offset: 0x0001D57B
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0001F383 File Offset: 0x0001D583
		public RoutedEventHandler YesButtonClicked { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001F38C File Offset: 0x0001D58C
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0001F394 File Offset: 0x0001D594
		public RoutedEventHandler NoButtonClicked { get; set; }

		// Token: 0x060006CF RID: 1743 RVA: 0x0001F39D File Offset: 0x0001D59D
		private void OnCloseDialog()
		{
			this.FadeoutBackground();
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001F3A8 File Offset: 0x0001D5A8
		private void FadeoutBackground()
		{
			Storyboard storyboard = (Storyboard)base.FindResource("FadeoutBackground");
			storyboard.Completed += this.FadeoutBackgroundCompleted;
			storyboard.Begin(this);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001F3E2 File Offset: 0x0001D5E2
		private void FadeoutBackgroundCompleted(object sender, EventArgs e)
		{
			base.DialogResult = new bool?(this.dialogResult);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		private void OnNoButtonClicked(object sender, RoutedEventArgs e)
		{
			RoutedEventHandler noButtonClicked = this.NoButtonClicked;
			bool flag = noButtonClicked != null;
			if (flag)
			{
				noButtonClicked(this, e);
			}
			bool flag2 = !e.Handled;
			if (flag2)
			{
				this.dialogResult = false;
				this.OnCloseDialog();
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001F440 File Offset: 0x0001D640
		private void OnYesButtonClicked(object sender, RoutedEventArgs e)
		{
			RoutedEventHandler yesButtonClicked = this.YesButtonClicked;
			bool flag = yesButtonClicked != null;
			if (flag)
			{
				yesButtonClicked(this, e);
			}
			bool flag2 = !e.Handled;
			if (flag2)
			{
				this.dialogResult = true;
				this.OnCloseDialog();
			}
		}

		// Token: 0x04000308 RID: 776
		private bool dialogResult;
	}
}

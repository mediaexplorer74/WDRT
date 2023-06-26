using System;
using System.Windows;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Localization;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000D4 RID: 212
	public class DialogMessageManager
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x0001EC07 File Offset: 0x0001CE07
		public DialogMessageManager()
		{
			this.ButtonLabel = LocalizationManager.GetTranslation("Close");
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001EC22 File Offset: 0x0001CE22
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x0001EC2A File Offset: 0x0001CE2A
		public Action NoButtonAction { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001EC33 File Offset: 0x0001CE33
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x0001EC3B File Offset: 0x0001CE3B
		public Action YesButtonAction { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001EC44 File Offset: 0x0001CE44
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x0001EC4C File Offset: 0x0001CE4C
		public string NoButtonLabel { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0001EC55 File Offset: 0x0001CE55
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0001EC5D File Offset: 0x0001CE5D
		public string YesButtonLabel { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001EC66 File Offset: 0x0001CE66
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001EC6E File Offset: 0x0001CE6E
		public string ButtonLabel { get; set; }

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001EC78 File Offset: 0x0001CE78
		public void ShowInfoDialog(object data, bool stretchContent = false, string message = null, string title = null)
		{
			RoutedEventHandler <>9__1;
			RoutedEventHandler <>9__2;
			AppDispatcher.Execute(delegate
			{
				MetroInformationDialog metroInformationDialog = new MetroInformationDialog
				{
					Message = string.Empty,
					ButtonText = this.ButtonLabel,
					MessageTitle = "Information"
				};
				this.activeDialog = metroInformationDialog;
				bool flag = !string.IsNullOrEmpty(message);
				if (flag)
				{
					metroInformationDialog.Message = message;
				}
				bool flag2 = !string.IsNullOrEmpty(title);
				if (flag2)
				{
					metroInformationDialog.MessageTitle = title;
				}
				bool flag3 = this.YesButtonAction != null;
				if (flag3)
				{
					metroInformationDialog.ButtonYes.Visibility = Visibility.Visible;
					MetroInformationDialog metroInformationDialog2 = metroInformationDialog;
					Delegate yesButtonClicked = metroInformationDialog2.YesButtonClicked;
					RoutedEventHandler routedEventHandler;
					if ((routedEventHandler = <>9__1) == null)
					{
						routedEventHandler = (<>9__1 = delegate(object o, RoutedEventArgs a)
						{
							a.Handled = true;
							this.NoButtonAction();
						});
					}
					metroInformationDialog2.YesButtonClicked = (RoutedEventHandler)Delegate.Combine(yesButtonClicked, routedEventHandler);
					metroInformationDialog.ButtonYes.Content = this.YesButtonLabel;
				}
				bool flag4 = this.NoButtonAction != null;
				if (flag4)
				{
					metroInformationDialog.ButtonNo.Visibility = Visibility.Visible;
					MetroInformationDialog metroInformationDialog3 = metroInformationDialog;
					Delegate noButtonClicked = metroInformationDialog3.NoButtonClicked;
					RoutedEventHandler routedEventHandler2;
					if ((routedEventHandler2 = <>9__2) == null)
					{
						routedEventHandler2 = (<>9__2 = delegate(object o, RoutedEventArgs a)
						{
							a.Handled = true;
							this.NoButtonAction();
						});
					}
					metroInformationDialog3.NoButtonClicked = (RoutedEventHandler)Delegate.Combine(noButtonClicked, routedEventHandler2);
					metroInformationDialog.ButtonNo.Content = this.NoButtonLabel;
				}
				metroInformationDialog.ShowDialog();
			}, true);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001ECB8 File Offset: 0x0001CEB8
		public void ShowInfoDialog(string message, string title = null)
		{
			string titleMessage = ((!string.IsNullOrEmpty(title)) ? title : "Information");
			AppDispatcher.Execute(delegate
			{
				MetroInformationDialog metroInformationDialog = new MetroInformationDialog
				{
					MessageTitle = titleMessage,
					Message = message,
					ButtonText = this.ButtonLabel
				};
				this.activeDialog = metroInformationDialog;
				metroInformationDialog.ShowDialog();
			}, false);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001ED04 File Offset: 0x0001CF04
		public bool? ShowQuestionDialog(string message, string title = null, bool defaultButtonLabels = true)
		{
			bool? result = null;
			AppDispatcher.Execute(delegate
			{
				string text = ((!string.IsNullOrEmpty(title)) ? title : LocalizationManager.GetTranslation("AreYouSure"));
				MetroQuestionDialog metroQuestionDialog = new MetroQuestionDialog
				{
					Message = message,
					MessageTitle = text
				};
				result = this.ShowQuestionDialog(metroQuestionDialog);
			}, true);
			return result;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001ED56 File Offset: 0x0001CF56
		public void CloseActiveDialog()
		{
			AppDispatcher.Execute(delegate
			{
				bool flag = this.activeDialog != null && this.activeDialog.IsActive;
				if (flag)
				{
					this.activeDialog.Close();
				}
			}, false);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001ED6C File Offset: 0x0001CF6C
		private bool? ShowQuestionDialog(MetroQuestionDialog confirmDialog)
		{
			this.activeDialog = confirmDialog;
			bool flag = !string.IsNullOrEmpty(this.NoButtonLabel);
			if (flag)
			{
				confirmDialog.NoButtonText = this.NoButtonLabel;
			}
			bool flag2 = !string.IsNullOrEmpty(this.YesButtonLabel);
			if (flag2)
			{
				confirmDialog.YesButtonText = this.YesButtonLabel;
			}
			return confirmDialog.ShowDialog();
		}

		// Token: 0x040002FB RID: 763
		private MetroDialog activeDialog;
	}
}

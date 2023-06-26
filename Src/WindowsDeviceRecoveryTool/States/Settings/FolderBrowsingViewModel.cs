using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Controls;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x02000028 RID: 40
	[Export]
	public class FolderBrowsingViewModel : BaseViewModel, ICanHandle<SettingsPreviousStateMessage>, ICanHandle, ICanHandle<SelectedPathMessage>
	{
		// Token: 0x060001BA RID: 442 RVA: 0x0000AF40 File Offset: 0x00009140
		public FolderBrowsingViewModel()
		{
			this.SelectFolderCommand = new DelegateCommand<FolderItem>(new Action<FolderItem>(this.OnFolderSelection));
			this.OkClickedCommand = new DelegateCommand<object>(new Action<object>(this.OkClicked));
			this.GoUpCommand = new DelegateCommand<object>(new Action<object>(this.GoUpButtonClicked));
			this.NewFolderCommand = new DelegateCommand<object>(new Action<object>(this.NewFolderCreation));
			this.CancelCommand = new DelegateCommand<object>(new Action<object>(this.CancelClicked));
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000AFCD File Offset: 0x000091CD
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000AFD5 File Offset: 0x000091D5
		public ICommand SelectFolderCommand { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000AFDE File Offset: 0x000091DE
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000AFE6 File Offset: 0x000091E6
		public ICommand OkClickedCommand { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000AFEF File Offset: 0x000091EF
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000AFF7 File Offset: 0x000091F7
		public ICommand CancelCommand { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000B000 File Offset: 0x00009200
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000B008 File Offset: 0x00009208
		public ICommand GoUpCommand { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000B011 File Offset: 0x00009211
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000B019 File Offset: 0x00009219
		public ICommand NewFolderCommand { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000B024 File Offset: 0x00009224
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000B03C File Offset: 0x0000923C
		public ObservableCollection<FolderItem> RootCollection
		{
			get
			{
				return this.rootCollection;
			}
			set
			{
				base.SetValue<ObservableCollection<FolderItem>>(() => this.RootCollection, ref this.rootCollection, value);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000B07C File Offset: 0x0000927C
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000B094 File Offset: 0x00009294
		public ObservableCollection<FolderItem> FolderListItems
		{
			get
			{
				return this.folderListItems;
			}
			set
			{
				base.SetValue<ObservableCollection<FolderItem>>(() => this.folderListItems, ref this.folderListItems, value);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000B0D0 File Offset: 0x000092D0
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000B0E8 File Offset: 0x000092E8
		public FolderItem SelectedRoot
		{
			get
			{
				return this.selectedRoot;
			}
			set
			{
				base.SetValue<FolderItem>(() => this.SelectedRoot, ref this.selectedRoot, value);
				this.OnRootListSelectionChanged(this.selectedRoot);
				this.selectedRoot = null;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000B148 File Offset: 0x00009348
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000B160 File Offset: 0x00009360
		public bool GoUpButtonEnable
		{
			get
			{
				return this.enableGoUpButton;
			}
			set
			{
				base.SetValue<bool>(() => this.GoUpButtonEnable, ref this.enableGoUpButton, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000B1A0 File Offset: 0x000093A0
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public bool OkButtonEnable
		{
			get
			{
				return this.enableOkButton;
			}
			set
			{
				base.SetValue<bool>(() => this.OkButtonEnable, ref this.enableOkButton, value);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000B1F8 File Offset: 0x000093F8
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000B210 File Offset: 0x00009410
		public string SelectedPath
		{
			get
			{
				return this.selectedPath;
			}
			set
			{
				base.SetValue<string>(() => this.SelectedPath, ref this.selectedPath, value);
				this.OkButtonEnable = (this.GoUpButtonEnable = !string.IsNullOrWhiteSpace(this.selectedPath));
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000B27C File Offset: 0x0000947C
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("PleaseSelectAFolder"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			this.InitializeRootCollection();
			base.RaisePropertyChanged<FolderItem>(() => this.SelectedRoot);
			base.RaisePropertyChanged<ObservableCollection<FolderItem>>(() => this.RootCollection);
			base.RaisePropertyChanged<string>(() => this.SelectedPath);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000B368 File Offset: 0x00009568
		private void InitializeRootCollection()
		{
			this.rootCollection = new ObservableCollection<FolderItem>();
			FolderItem folderItem = new FolderItem
			{
				Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
				Title = LocalizationManager.GetTranslation("Desktop"),
				Type = FolderType.Desktop
			};
			this.rootCollection.Add(folderItem);
			FolderItem folderItem2 = new FolderItem
			{
				Path = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
				Title = LocalizationManager.GetTranslation("MyComputer"),
				Type = FolderType.Folder
			};
			this.rootCollection.Add(folderItem2);
			FolderItem folderItem3 = new FolderItem
			{
				Path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
				Title = Environment.UserName,
				Type = FolderType.User
			};
			this.rootCollection.Add(folderItem3);
			this.selectedRoot = null;
			bool flag = string.IsNullOrWhiteSpace(this.SelectedPath);
			if (flag)
			{
				this.FolderListItems = this.GetSubfolders(this.SelectedPath);
			}
			else
			{
				this.FolderListItems = this.GetSubfolders(Path.GetDirectoryName(this.SelectedPath));
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000B470 File Offset: 0x00009670
		private ObservableCollection<FolderItem> GetSubfolders(string path)
		{
			ObservableCollection<FolderItem> observableCollection = new ObservableCollection<FolderItem>();
			try
			{
				bool flag = string.IsNullOrWhiteSpace(path);
				if (flag)
				{
					foreach (FolderItem folderItem in from drive in Directory.GetLogicalDrives()
						select new FolderItem
						{
							Path = drive,
							Title = drive,
							Items = this.GetSubfolders(drive)
						})
					{
						observableCollection.Add(folderItem);
					}
				}
				else
				{
					foreach (FolderItem folderItem2 in from dir in Directory.GetDirectories(path)
						let info = new DirectoryInfo(dir)
						where info.Exists && !info.Attributes.HasFlag(FileAttributes.Hidden)
						select new FolderItem
						{
							Path = dir,
							Title = dir.Substring(dir.LastIndexOf("\\", StringComparison.Ordinal) + 1),
							Type = FolderType.Folder
						})
					{
						observableCollection.Add(folderItem2);
					}
				}
			}
			catch (Exception)
			{
			}
			return observableCollection;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000B5E0 File Offset: 0x000097E0
		private void OnFolderSelection(FolderItem folder)
		{
			bool flag = folder == null;
			if (!flag)
			{
				ObservableCollection<FolderItem> subfolders = this.GetSubfolders(folder.Path);
				bool flag2 = subfolders.Count > 0;
				if (flag2)
				{
					this.FolderListItems = subfolders;
				}
				this.SelectedPath = folder.Path;
				this.CheckBackButtonEnable(folder.Path);
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000B638 File Offset: 0x00009838
		private void OnRootListSelectionChanged(FolderItem item)
		{
			bool flag = item != null;
			if (flag)
			{
				this.FolderListItems = this.GetSubfolders(item.Path);
				this.SelectedPath = item.Path;
				this.CheckBackButtonEnable(item.Path);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000B67D File Offset: 0x0000987D
		private void CheckBackButtonEnable(string path)
		{
			this.GoUpButtonEnable = !string.IsNullOrWhiteSpace(path);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000B690 File Offset: 0x00009890
		private void GoUpButtonClicked(object sender)
		{
			bool flag = this.FolderListItems == null;
			if (!flag)
			{
				string text = string.Empty;
				bool flag2 = this.FolderListItems.Count > 0;
				if (flag2)
				{
					FolderItem folderItem = this.FolderListItems[0];
					bool flag3 = folderItem != null;
					if (flag3)
					{
						text = Path.GetDirectoryName(Path.GetDirectoryName(folderItem.Path));
						this.SelectedPath = text;
						this.FolderListItems = this.GetSubfolders(text);
					}
				}
				else
				{
					text = Path.GetDirectoryName(this.SelectedPath);
					this.SelectedPath = text;
					this.FolderListItems = this.GetSubfolders(text);
				}
				this.CheckBackButtonEnable(text);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000B73C File Offset: 0x0000993C
		private void OkClicked(object sender)
		{
			base.Commands.Run((AppController c) => c.SwitchToState("SettingsState"));
			base.Commands.Run((AppController c) => c.SwitchSettingsState(this.previousState));
			bool flag = "PackagesState".Equals(this.previousState);
			if (flag)
			{
				base.Commands.Run((SettingsController s) => s.SetPackagesPathDirectory(this.selectedPath, CancellationToken.None));
			}
			else
			{
				base.EventAggregator.Publish<TraceParametersMessage>(new TraceParametersMessage(this.selectedPath, false));
				base.Commands.Run((SettingsController s) => s.CollectLogs(this.selectedPath, CancellationToken.None));
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000B95C File Offset: 0x00009B5C
		private void CancelClicked(object sender)
		{
			base.Commands.Run((AppController c) => c.SwitchToState("SettingsState"));
			base.Commands.Run((AppController c) => c.SwitchSettingsState(this.previousState));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000BA3C File Offset: 0x00009C3C
		private void NewFolderCreation(object sender)
		{
			bool flag = string.IsNullOrWhiteSpace(this.selectedPath);
			if (!flag)
			{
				string text = this.selectedPath;
				MetroTextBlockDialog metroTextBlockDialog = new MetroTextBlockDialog
				{
					MessageTitle = LocalizationManager.GetTranslation("CreatingNewFolderMessage"),
					InputText = LocalizationManager.GetTranslation("ButtonNewFolder"),
					NoButtonText = LocalizationManager.GetTranslation("ButtonCancel"),
					YesButtonText = LocalizationManager.GetTranslation("ButtonOk")
				};
				bool? flag2 = metroTextBlockDialog.ShowDialog();
				bool flag3 = true;
				bool flag4 = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
				if (flag4)
				{
					try
					{
						string text2 = Path.Combine(text, metroTextBlockDialog.InputText);
						Directory.CreateDirectory(text2);
						this.FolderListItems = this.GetSubfolders(text2);
						this.SelectedPath = text2;
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000BB1C File Offset: 0x00009D1C
		public void Handle(SettingsPreviousStateMessage message)
		{
			bool flag = !string.IsNullOrEmpty(message.PreviousState);
			if (flag)
			{
				this.previousState = message.PreviousState;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000BB4C File Offset: 0x00009D4C
		public void Handle(SelectedPathMessage message)
		{
			bool flag = !string.IsNullOrEmpty(message.SelectedPath);
			if (flag)
			{
				this.SelectedPath = message.SelectedPath;
			}
		}

		// Token: 0x040000DF RID: 223
		private string selectedPath;

		// Token: 0x040000E0 RID: 224
		private ObservableCollection<FolderItem> rootCollection;

		// Token: 0x040000E1 RID: 225
		private ObservableCollection<FolderItem> folderListItems;

		// Token: 0x040000E2 RID: 226
		private FolderItem selectedRoot;

		// Token: 0x040000E3 RID: 227
		private bool enableGoUpButton;

		// Token: 0x040000E4 RID: 228
		private bool enableOkButton;

		// Token: 0x040000E5 RID: 229
		private string previousState;
	}
}

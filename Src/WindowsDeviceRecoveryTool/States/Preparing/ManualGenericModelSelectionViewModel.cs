using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.BusinessLogic.Services;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x0200004A RID: 74
	[Export]
	public class ManualGenericModelSelectionViewModel : BaseViewModel, ICanHandle<SupportedAdaptationModelsMessage>, ICanHandle
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x000104B4 File Offset: 0x0000E6B4
		[ImportingConstructor]
		public ManualGenericModelSelectionViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext, AdaptationManager adaptationManager)
		{
			this.appContext = appContext;
			this.adaptationManager = adaptationManager;
			this.SelectTileCommand = new DelegateCommand<object>(new Action<object>(this.TileSelected));
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000104EF File Offset: 0x0000E6EF
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x000104F7 File Offset: 0x0000E6F7
		public ICommand SelectTileCommand { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00010500 File Offset: 0x0000E700
		public override string PreviousStateName
		{
			get
			{
				return "ManualManufacturerSelectionState";
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00010518 File Offset: 0x0000E718
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00010530 File Offset: 0x0000E730
		public ObservableCollection<Tile> Tiles
		{
			get
			{
				return this.tiles;
			}
			set
			{
				base.SetValue<ObservableCollection<Tile>>(() => this.Tiles, ref this.tiles, value);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00010570 File Offset: 0x0000E770
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00010588 File Offset: 0x0000E788
		public Tile SelectedModel
		{
			get
			{
				return this.selectedModel;
			}
			set
			{
				base.SetValue<Tile>(() => this.SelectedModel, ref this.selectedModel, value);
				bool flag = value != null;
				if (flag)
				{
					this.appContext.CurrentPhone = new Phone
					{
						SalesName = value.Phone.SalesName,
						HardwareModel = value.Phone.HardwareModel,
						HardwareVariant = value.Phone.HardwareVariant,
						ModelIdentificationInstruction = value.Phone.ModelIdentificationInstruction,
						Type = value.Phone.Type,
						ImageData = value.Phone.ImageData,
						QueryParameters = value.Phone.QueryParameters
					};
					this.goingForward = true;
					BaseAdaptation adaptation = this.adaptationManager.GetAdaptation(value.Phone.Type);
					bool flag2 = adaptation.ManuallySupportedVariants(value.Phone).Any<Phone>();
					string nextState;
					if (flag2)
					{
						nextState = "ManualGenericVariantSelectionState";
					}
					else
					{
						nextState = "CheckLatestPackageState";
					}
					base.Commands.Run((AppController c) => c.SwitchToState(nextState));
				}
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00010734 File Offset: 0x0000E934
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("ManualModelSelection"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			this.goingForward = false;
			bool flag = this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.Type == PhoneTypes.UnknownWp;
			if (flag)
			{
				this.appContext.CurrentPhone.Type = this.appContext.SelectedManufacturer;
			}
			base.Commands.Run((FlowController c) => c.GetSupportedAdaptationModels(this.appContext.SelectedManufacturer));
			base.Commands.Run((FlowController c) => c.StartSessionFlow(string.Empty, CancellationToken.None));
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000108D4 File Offset: 0x0000EAD4
		public override void OnStopped()
		{
			base.OnStopped();
			bool flag = !this.goingForward && this.appContext.CurrentPhone != null;
			if (flag)
			{
				this.appContext.CurrentPhone = null;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00010915 File Offset: 0x0000EB15
		private void TileSelected(object obj)
		{
			this.SelectedModel = obj as Tile;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00010928 File Offset: 0x0000EB28
		public void Handle(SupportedAdaptationModelsMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				this.Tiles.Clear();
				foreach (Phone phone in message.Models)
				{
					this.Tiles.Add(new Tile
					{
						Title = phone.SalesName,
						PhoneType = phone.Type,
						Image = this.GetImage(phone.ImageData),
						Phone = phone,
						IsEnabled = true
					});
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000109E4 File Offset: 0x0000EBE4
		private BitmapImage GetImage(byte[] imageData)
		{
			bool flag = imageData != null;
			if (flag)
			{
				using (MemoryStream memoryStream = new MemoryStream(imageData))
				{
					BitmapImage bitmapImage = new BitmapImage();
					bitmapImage.BeginInit();
					bitmapImage.StreamSource = memoryStream;
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.EndInit();
					bitmapImage.Freeze();
					return bitmapImage;
				}
			}
			return null;
		}

		// Token: 0x0400014E RID: 334
		private readonly Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x0400014F RID: 335
		private readonly AdaptationManager adaptationManager;

		// Token: 0x04000150 RID: 336
		private bool goingForward;

		// Token: 0x04000151 RID: 337
		private Tile selectedModel;

		// Token: 0x04000152 RID: 338
		private ObservableCollection<Tile> tiles = new ObservableCollection<Tile>();
	}
}

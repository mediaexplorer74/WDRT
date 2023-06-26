using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x0200005A RID: 90
	[Export]
	public class ManualManufacturerSelectionViewModel : BaseViewModel, ICanHandle<SupportedManufacturersMessage>, ICanHandle
	{
		// Token: 0x06000375 RID: 885 RVA: 0x00013450 File Offset: 0x00011650
		[ImportingConstructor]
		public ManualManufacturerSelectionViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.appContext = appContext;
			this.SelectTileCommand = new DelegateCommand<object>(new Action<object>(this.TileSelected));
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00013484 File Offset: 0x00011684
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0001348C File Offset: 0x0001168C
		public ICommand SelectTileCommand { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00013498 File Offset: 0x00011698
		// (set) Token: 0x06000379 RID: 889 RVA: 0x000134B0 File Offset: 0x000116B0
		public ManualManuFacturerSelectionViewState ViewState
		{
			get
			{
				return this.viewState;
			}
			set
			{
				bool flag = this.viewState != value;
				if (flag)
				{
					this.viewState = value;
					base.RaisePropertyChanged<ManualManuFacturerSelectionViewState>(() => this.ViewState);
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00013510 File Offset: 0x00011710
		public override string PreviousStateName
		{
			get
			{
				return (this.ViewState == ManualManuFacturerSelectionViewState.OtherManufacturerSelection) ? "ManualManufacturerSelectionState" : "AutomaticManufacturerSelectionState";
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00013538 File Offset: 0x00011738
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00013550 File Offset: 0x00011750
		public Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext AppContext
		{
			get
			{
				return this.appContext;
			}
			set
			{
				base.SetValue<Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext>(() => this.AppContext, ref this.appContext, value);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00013590 File Offset: 0x00011790
		// (set) Token: 0x0600037E RID: 894 RVA: 0x000135A8 File Offset: 0x000117A8
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

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000135E8 File Offset: 0x000117E8
		// (set) Token: 0x06000380 RID: 896 RVA: 0x00013600 File Offset: 0x00011800
		public Tile SelectedManufacturer
		{
			get
			{
				return this.selectedManufacturer;
			}
			set
			{
				base.SetValue<Tile>(() => this.SelectedManufacturer, ref this.selectedManufacturer, value);
				this.AppContext.SelectedManufacturer = ((value == null) ? PhoneTypes.Generic : value.PhoneType);
				bool flag = value != null;
				if (flag)
				{
					string manufacturerStartingState;
					switch (this.AppContext.SelectedManufacturer)
					{
					case PhoneTypes.Lumia:
						manufacturerStartingState = "AwaitRecoveryDeviceState";
						goto IL_141;
					case PhoneTypes.Htc:
						manufacturerStartingState = "AwaitHtcState";
						goto IL_141;
					case PhoneTypes.Analog:
						manufacturerStartingState = "AwaitAnalogDeviceState";
						goto IL_141;
					case PhoneTypes.Mcj:
					case PhoneTypes.Blu:
					case PhoneTypes.Alcatel:
					case PhoneTypes.Acer:
					case PhoneTypes.Trinity:
					case PhoneTypes.Unistrong:
					case PhoneTypes.YEZZ:
					case PhoneTypes.Micromax:
					case PhoneTypes.Funker:
					case PhoneTypes.Diginnos:
					case PhoneTypes.VAIO:
					case PhoneTypes.HP:
					case PhoneTypes.Inversenet:
					case PhoneTypes.Freetel:
					case PhoneTypes.XOLO:
					case PhoneTypes.KM:
					case PhoneTypes.Jenesis:
					case PhoneTypes.Gomobile:
					case PhoneTypes.Lenovo:
					case PhoneTypes.Zebra:
					case PhoneTypes.Honeywell:
					case PhoneTypes.Panasonic:
					case PhoneTypes.TrekStor:
					case PhoneTypes.Wileyfox:
						manufacturerStartingState = "ManualGenericModelSelectionState";
						goto IL_141;
					case PhoneTypes.HoloLensAccessory:
						manufacturerStartingState = "AwaitFawkesDeviceState";
						goto IL_141;
					}
					manufacturerStartingState = "AwaitGenericDeviceState";
					IL_141:
					base.EventAggregator.Publish<DetectionTypeMessage>(new DetectionTypeMessage(DetectionType.RecoveryMode));
					base.Commands.Run((AppController c) => c.SwitchToState(manufacturerStartingState));
				}
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000137D4 File Offset: 0x000119D4
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("ManufacturerHeader"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			this.SelectedManufacturer = null;
			this.ViewState = ManualManuFacturerSelectionViewState.InitialManufacturerSelection;
			bool flag = this.AppContext.CurrentPhone != null && this.AppContext.CurrentPhone.Type != PhoneTypes.UnknownWp;
			if (flag)
			{
				this.AppContext.CurrentPhone = null;
			}
			base.Commands.Run((FlowController c) => c.GetSupportedManufacturers());
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000138C4 File Offset: 0x00011AC4
		private void TileSelected(object parameter)
		{
			GroupTile groupTile = parameter as GroupTile;
			bool flag = groupTile != null;
			if (flag)
			{
				this.Tiles = new ObservableCollection<Tile>(groupTile.TilesInGroup);
				this.ViewState = ManualManuFacturerSelectionViewState.OtherManufacturerSelection;
			}
			else
			{
				Tile tile = parameter as Tile;
				this.SelectedManufacturer = tile;
				ParameterExpression parameterExpression;
				base.Commands.Run(Expression.Lambda<Action<FlowController>>(Expression.Call(parameterExpression, methodof(FlowController.StartSessionFlow(string, CancellationToken)), new Expression[]
				{
					Expression.Field(null, fieldof(string.Empty)),
					Expression.Property(Expression.New(typeof(CancellationTokenSource)), methodof(CancellationTokenSource.get_Token()))
				}), new ParameterExpression[] { parameterExpression }));
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00013998 File Offset: 0x00011B98
		public void Handle(SupportedManufacturersMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				this.Tiles.Clear();
				Phone currentPhone = this.AppContext.CurrentPhone;
				foreach (ManufacturerInfo manufacturerInfo in message.Manufacturers.Where(new Func<ManufacturerInfo, bool>(this.IsPreferredManufacturer)))
				{
					bool recoverySupport = manufacturerInfo.RecoverySupport;
					if (recoverySupport)
					{
						bool flag = currentPhone != null && currentPhone.Type == PhoneTypes.UnknownWp;
						if (flag)
						{
							bool flag2 = !currentPhone.MatchedAdaptationTypes.Contains(manufacturerInfo.Type);
							if (flag2)
							{
								continue;
							}
						}
						this.Tiles.Add(this.ConvertToTile(manufacturerInfo));
					}
				}
				GroupTile groupTile = new GroupTile(message.Manufacturers.Where((ManufacturerInfo m) => !this.IsPreferredManufacturer(m)).Select(new Func<ManufacturerInfo, Tile>(this.ConvertToTile)))
				{
					IsEnabled = true,
					IsWaiting = false,
					PhoneType = PhoneTypes.UnknownWp,
					Title = LocalizationManager.GetTranslation("OtherOEMs_GroupTile_Title"),
					Image = new BitmapImage(new Uri("pack://application:,,,/Resources/unknown_wp.png"))
				};
				this.Tiles.Add(groupTile);
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00013AF0 File Offset: 0x00011CF0
		private Tile ConvertToTile(ManufacturerInfo manufacturerInfo)
		{
			return new Tile
			{
				Title = manufacturerInfo.Name,
				PhoneType = manufacturerInfo.Type,
				Image = this.GetImage(manufacturerInfo.ImageData),
				IsEnabled = true,
				IsWaiting = false
			};
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00013B44 File Offset: 0x00011D44
		private bool IsPreferredManufacturer(ManufacturerInfo manufacturerInfo)
		{
			PhoneTypes type = manufacturerInfo.Type;
			PhoneTypes phoneTypes = type;
			return phoneTypes == PhoneTypes.Lumia || phoneTypes == PhoneTypes.Analog || phoneTypes == PhoneTypes.HoloLensAccessory;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00013B78 File Offset: 0x00011D78
		private int FavorFirstPartyDevices(ManufacturerInfo manufacturerInfo)
		{
			return this.IsPreferredManufacturer(manufacturerInfo) ? 0 : 1;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00013B98 File Offset: 0x00011D98
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

		// Token: 0x04000186 RID: 390
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000187 RID: 391
		private Tile selectedManufacturer;

		// Token: 0x04000188 RID: 392
		private ObservableCollection<Tile> tiles = new ObservableCollection<Tile>();

		// Token: 0x04000189 RID: 393
		private ManualManuFacturerSelectionViewState viewState;
	}
}

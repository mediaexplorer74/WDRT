using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.BusinessLogic.Services;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x0200004C RID: 76
	[Export]
	public class ManualGenericVariantSelectionViewModel : BaseViewModel
	{
		// Token: 0x060002FB RID: 763 RVA: 0x00010C68 File Offset: 0x0000EE68
		[ImportingConstructor]
		public ManualGenericVariantSelectionViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext, AdaptationManager adaptationManager)
		{
			this.appContext = appContext;
			this.adaptationManager = adaptationManager;
			this.SelectTileCommand = new DelegateCommand<object>(new Action<object>(this.TileSelected));
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00010CA4 File Offset: 0x0000EEA4
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00010CBC File Offset: 0x0000EEBC
		public CollectionObservable<Tile> Tiles
		{
			get
			{
				return this.tiles;
			}
			set
			{
				base.SetValue<CollectionObservable<Tile>>(() => this.Tiles, ref this.tiles, value);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00010CFC File Offset: 0x0000EEFC
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00010D14 File Offset: 0x0000EF14
		public string ModelIdentificationInstruction
		{
			get
			{
				return this.modelIdentificationInstruction;
			}
			set
			{
				base.SetValue<string>(() => this.ModelIdentificationInstruction, ref this.modelIdentificationInstruction, value);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00010D54 File Offset: 0x0000EF54
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00010D5C File Offset: 0x0000EF5C
		public ICommand SelectTileCommand { get; private set; }

		// Token: 0x06000302 RID: 770 RVA: 0x00010D68 File Offset: 0x0000EF68
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			this.Tiles.Clear();
			Phone currentPhone = this.appContext.CurrentPhone;
			this.ModelIdentificationInstruction = currentPhone.ModelIdentificationInstruction;
			BaseAdaptation adaptation = this.adaptationManager.GetAdaptation(currentPhone.Type);
			List<Phone> list = adaptation.ManuallySupportedVariants(currentPhone);
			list.ForEach(new Action<Phone>(this.AddTile));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00010DE4 File Offset: 0x0000EFE4
		private void AddTile(Phone phone)
		{
			Tile tile = new Tile
			{
				Phone = phone,
				PhoneType = phone.Type,
				Title = phone.SalesName,
				IsEnabled = true,
				Image = this.GetImage(phone.ImageData)
			};
			this.Tiles.Add(tile);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00010E44 File Offset: 0x0000F044
		private void TileSelected(object obj)
		{
			Tile tile = obj as Tile;
			bool flag = tile != null;
			if (flag)
			{
				this.appContext.CurrentPhone.SalesName = tile.Phone.SalesName;
				this.appContext.CurrentPhone.HardwareModel = tile.Phone.HardwareModel;
				this.appContext.CurrentPhone.HardwareVariant = tile.Phone.HardwareVariant;
				this.appContext.CurrentPhone.QueryParameters = tile.Phone.QueryParameters;
				base.Commands.Run((AppController c) => c.SwitchToState("CheckLatestPackageState"));
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00010F38 File Offset: 0x0000F138
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

		// Token: 0x04000156 RID: 342
		private readonly Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000157 RID: 343
		private readonly AdaptationManager adaptationManager;

		// Token: 0x04000158 RID: 344
		private CollectionObservable<Tile> tiles = new CollectionObservable<Tile>();

		// Token: 0x04000159 RID: 345
		private string modelIdentificationInstruction;
	}
}

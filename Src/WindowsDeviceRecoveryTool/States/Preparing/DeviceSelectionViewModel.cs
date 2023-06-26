using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.BusinessLogic.Services;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Detection;
using Microsoft.WindowsDeviceRecoveryTool.Detection.LegacySupport;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x02000053 RID: 83
	[Export]
	public sealed class DeviceSelectionViewModel : BaseViewModel, INotifyLiveRegionChanged
	{
		// Token: 0x06000332 RID: 818 RVA: 0x00012150 File Offset: 0x00010350
		[ImportingConstructor]
		internal DeviceSelectionViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext, AdaptationManager adaptationManager, DetectionHandlerFactory detectionHandlerFactory, PhoneFactory phoneFactory)
		{
			this.detectionHandlerFactory = detectionHandlerFactory;
			this.phoneFactory = phoneFactory;
			this.appContext = appContext;
			this.adaptationManager = adaptationManager;
			this.cancelTimer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(0.6)
			};
			this.cancelTimer.Tick += this.OnCancelTimerTick;
			this.SelectTileCommand = new DelegateCommand<object>(new Action<object>(this.OnTileSelectedCommandExecuted), new Func<object, bool>(this.OnCanExecuteSelectTileCommand));
			this.DeviceNotDetectedCommand = new DelegateCommand<object>(new Action<object>(this.OnDeviceNotDetectedCommandExecuted));
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00012204 File Offset: 0x00010404
		private bool OnCanExecuteSelectTileCommand(object arg)
		{
			Tile tile = arg as Tile;
			bool flag = tile == null;
			return !flag && tile.IsEnabled;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000334 RID: 820 RVA: 0x00012230 File Offset: 0x00010430
		// (remove) Token: 0x06000335 RID: 821 RVA: 0x00012268 File Offset: 0x00010468
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LiveRegionChanged;

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0001229D File Offset: 0x0001049D
		// (set) Token: 0x06000337 RID: 823 RVA: 0x000122A5 File Offset: 0x000104A5
		public ICommand DeviceNotDetectedCommand { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000338 RID: 824 RVA: 0x000122AE File Offset: 0x000104AE
		// (set) Token: 0x06000339 RID: 825 RVA: 0x000122B6 File Offset: 0x000104B6
		public ICommand SelectTileCommand { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600033A RID: 826 RVA: 0x000122C0 File Offset: 0x000104C0
		// (set) Token: 0x0600033B RID: 827 RVA: 0x000122D8 File Offset: 0x000104D8
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00012318 File Offset: 0x00010518
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00012330 File Offset: 0x00010530
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00012370 File Offset: 0x00010570
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00012388 File Offset: 0x00010588
		public Tile SelectedPhoneTile
		{
			get
			{
				return this.selectedPhoneTile;
			}
			set
			{
				base.SetValue<Tile>(() => this.SelectedPhoneTile, ref this.selectedPhoneTile, value);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000340 RID: 832 RVA: 0x000123C8 File Offset: 0x000105C8
		// (set) Token: 0x06000341 RID: 833 RVA: 0x000123E0 File Offset: 0x000105E0
		public string LiveText
		{
			get
			{
				return this.liveText;
			}
			set
			{
				base.SetValue<string>(() => this.LiveText, ref this.liveText, value);
				bool flag = !string.IsNullOrWhiteSpace(this.liveText);
				if (flag)
				{
					this.OnLiveRegionChanged();
				}
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00012448 File Offset: 0x00010648
		public override void OnStarted()
		{
			/*
An exception occurred when decompiling this method (06000342)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Microsoft.WindowsDeviceRecoveryTool.States.Preparing.DeviceSelectionViewModel::OnStarted()
 ---> System.Collections.Generic.KeyNotFoundException: The given key was not present in the dictionary.
   at System.ThrowHelper.ThrowKeyNotFoundException()
   at System.Collections.Generic.Dictionary`2.get_Item(TKey key)
   at ICSharpCode.Decompiler.ILAst.GotoRemoval.<GetParents>d__14.MoveNext() in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\GotoRemoval.cs:line 213
   at System.Linq.Enumerable.<OfTypeIterator>d__95`1.MoveNext()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ICSharpCode.Decompiler.ILAst.GotoRemoval.Enter(ILNode node, HashSet`1 visitedNodes) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\GotoRemoval.cs:line 279
   at ICSharpCode.Decompiler.ILAst.GotoRemoval.TrySimplifyGoto(ILExpression gotoExpr) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\GotoRemoval.cs:line 228
   at ICSharpCode.Decompiler.ILAst.GotoRemoval.RemoveGotosCore(ILBlock method) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\GotoRemoval.cs:line 102
   at ICSharpCode.Decompiler.ILAst.GotoRemoval.RemoveGotos(DecompilerContext context, ILBlock method) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\GotoRemoval.cs:line 57
   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 358
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 125
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 88
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 92
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
*/;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00012484 File Offset: 0x00010684
		public override void OnStopped()
		{
			bool flag = this.externalTokenSource != null;
			if (flag)
			{
				this.externalTokenSource.Cancel();
			}
			this.cancelTimer.IsEnabled = false;
			base.OnStopped();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000124C4 File Offset: 0x000106C4
		private void OnCompleted(Phone phone)
		{
			this.AppContext.CurrentPhone = phone;
			this.AppContext.SelectedManufacturer = phone.Type;
			PhoneTypes selectedManufacturer = this.appContext.SelectedManufacturer;
			PhoneTypes phoneTypes = selectedManufacturer;
			string nextState;
			switch (phoneTypes)
			{
			case PhoneTypes.Lumia:
			case PhoneTypes.Analog:
				nextState = "ReadingDeviceInfoState";
				goto IL_13A;
			case PhoneTypes.Htc:
				nextState = "AwaitHtcState";
				goto IL_13A;
			case PhoneTypes.Lg:
				break;
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
			{
				BaseAdaptation adaptation = this.adaptationManager.GetAdaptation(phone.Type);
				bool flag = adaptation.ManuallySupportedVariants(phone).Any<Phone>();
				if (flag)
				{
					nextState = "ManualGenericVariantSelectionState";
				}
				else
				{
					nextState = "CheckLatestPackageState";
				}
				goto IL_13A;
			}
			case PhoneTypes.HoloLensAccessory:
				nextState = "AwaitFawkesDeviceState";
				goto IL_13A;
			default:
				if (phoneTypes == PhoneTypes.UnknownWp)
				{
					nextState = "ManualManufacturerSelectionState";
					goto IL_13A;
				}
				break;
			}
			nextState = "AwaitGenericDeviceState";
			IL_13A:
			base.EventAggregator.Publish<DetectionTypeMessage>(new DetectionTypeMessage(DetectionType.NormalMode));
			base.EventAggregator.Publish<SelectedDeviceMessage>(new SelectedDeviceMessage(phone));
			base.Commands.Run((AppController c) => c.SwitchToState(nextState));
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001269F File Offset: 0x0001089F
		private void OnDeviceNotDetectedCommandExecuted(object obj)
		{
			this.internalTokenSource.Cancel();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000126B0 File Offset: 0x000108B0
		private void OnTileSelectedCommandExecuted(object obj)
		{
			this.SelectedPhoneTile = obj as Tile;
			this.itemSelectedTaskCompletionSource.SetResult(obj as Tile);
			ParameterExpression parameterExpression;
			base.Commands.Run(Expression.Lambda<Action<FlowController>>(Expression.Call(parameterExpression, methodof(FlowController.StartSessionFlow(string, CancellationToken)), new Expression[]
			{
				Expression.Field(null, fieldof(string.Empty)),
				Expression.Property(Expression.New(typeof(CancellationTokenSource)), methodof(CancellationTokenSource.get_Token()))
			}), new ParameterExpression[] { parameterExpression }));
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00012764 File Offset: 0x00010964
		private void PurgeCompletedTasks(List<Task> tasks)
		{
			Task[] array = tasks.Where((Task t) => t.IsCanceled || t.IsCompleted || t.IsFaulted).ToArray<Task>();
			foreach (Task task in array)
			{
				tasks.Remove(task);
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000127BC File Offset: 0x000109BC
		private async Task ProcessDeviceAttachedAsync(IDetectionHandler detectionHandler, DeviceInfo deviceInfo, bool isEnumerated, CancellationToken cancellationToken)
		{
			Tile tile = this.CreateNewTile(deviceInfo.DeviceIdentifier);
			this.AddTile(tile);
			bool flag = !isEnumerated;
			if (flag)
			{
				this.LiveText = LocalizationManager.GetTranslation("DeviceConnected");
			}
			Phone phone;
			try
			{
				await detectionHandler.UpdateDeviceInfoAsync(deviceInfo, cancellationToken);
				Phone phone2 = await this.phoneFactory.CreateAsync(deviceInfo, cancellationToken);
				phone = phone2;
				phone2 = null;
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception ex)
			{
				Tracer<DeviceSelectionViewModel>.WriteError(ex);
				return;
			}
			if (!deviceInfo.IsDeviceSupported)
			{
				Tracer<DeviceSelectionViewModel>.WriteInformation("Device not supppoted: {0}", new object[] { deviceInfo.DeviceIdentifier });
				this.UpdateTileInformationForNotSupported(tile);
			}
			else
			{
				Tracer<DeviceSelectionViewModel>.WriteInformation("Device supppoted: {0}", new object[] { deviceInfo.DeviceIdentifier });
				this.UpdateTileInformation(tile, phone, deviceInfo);
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00012820 File Offset: 0x00010A20
		private Task ProcessDeviceDetachedAsync(DeviceInfo deviceInfo)
		{
			TaskCompletionSource<object> source = new TaskCompletionSource<object>();
			foreach (Tile tile2 in this.Tiles)
			{
				tile2.ShowStartAnimation = false;
			}
			Tile tileToRemove = this.Tiles.FirstOrDefault((Tile tile) => string.Equals(tile.DevicePath, deviceInfo.DeviceIdentifier, StringComparison.CurrentCultureIgnoreCase));
			bool flag = tileToRemove != null;
			if (flag)
			{
				this.LiveText = LocalizationManager.GetTranslation("DeviceDisconnected");
				EventHandler onTimerElapsed = null;
				onTimerElapsed = delegate(object sender, EventArgs args)
				{
					tileToRemove.OnRemoveTimerElapsed -= onTimerElapsed;
					this.RemoveTile(tileToRemove);
					source.SetResult(null);
				};
				tileToRemove.OnRemoveTimerElapsed += onTimerElapsed;
				tileToRemove.IsDeleted = true;
				Tracer<DeviceSelectionViewModel>.WriteInformation("Removed device: {0}", new object[] { deviceInfo.DeviceIdentifier });
			}
			else
			{
				Tracer<DeviceSelectionViewModel>.WriteError("Tile not found !!!", new object[0]);
				source.SetResult(null);
			}
			return source.Task;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00012984 File Offset: 0x00010B84
		private void RemoveTile(Tile tile)
		{
			this.Tiles.Remove(tile);
			bool flag = this.Tiles.Count == 0;
			if (flag)
			{
				this.cancelTimer.Start();
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000129BF File Offset: 0x00010BBF
		private void AddTile(Tile tile)
		{
			this.Tiles.Add(tile);
			this.cancelTimer.IsEnabled = false;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000129DC File Offset: 0x00010BDC
		private void OnCancelTimerTick(object sender, EventArgs eventArgs)
		{
			this.cancelTimer.IsEnabled = false;
			bool flag = this.Tiles.Count == 0;
			if (flag)
			{
				this.itemSelectedTaskCompletionSource.SetCanceled();
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00012A18 File Offset: 0x00010C18
		private Tile CreateNewTile(string path)
		{
			return new Tile
			{
				DevicePath = path,
				IsEnabled = false,
				IsWaiting = true,
				ShowStartAnimation = true,
				Title = LocalizationManager.GetTranslation("ReadingDeviceInfo"),
				Image = new BitmapImage(new Uri("pack://application:,,,/Resources/unknown_wp.png"))
			};
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00012A78 File Offset: 0x00010C78
		private void OnLiveRegionChanged()
		{
			EventHandler liveRegionChanged = this.LiveRegionChanged;
			bool flag = liveRegionChanged != null;
			if (flag)
			{
				EventArgs empty = EventArgs.Empty;
				liveRegionChanged(this, empty);
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00012AA6 File Offset: 0x00010CA6
		private void UpdateTileInformationForNotSupported(Tile tile)
		{
			tile.Title = LocalizationManager.GetTranslation("ManufacturerDetectionFailed");
			tile.IsWaiting = false;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00012AC4 File Offset: 0x00010CC4
		private void UpdateTileInformation(Tile tile, Phone phone, DeviceInfo deviceInfo)
		{
			tile.Phone = phone;
			tile.PhoneType = phone.Type;
			tile.Image = DeviceSelectionViewModel.LoadBitmapImageFromBytes(deviceInfo.DeviceBitmapBytes);
			tile.Title = deviceInfo.DeviceSalesName;
			tile.SupportId = deviceInfo.SupportId;
			tile.IsWaiting = false;
			tile.IsEnabled = true;
			tile.BasicDeviceInformation = deviceInfo;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00012B2C File Offset: 0x00010D2C
		private static BitmapImage LoadBitmapImageFromBytes(byte[] bytes)
		{
			bool flag = bytes == null;
			BitmapImage bitmapImage;
			if (flag)
			{
				bitmapImage = null;
			}
			else
			{
				using (MemoryStream memoryStream = new MemoryStream(bytes))
				{
					BitmapImage bitmapImage2 = new BitmapImage();
					bitmapImage2.BeginInit();
					bitmapImage2.StreamSource = memoryStream;
					bitmapImage2.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage2.EndInit();
					bitmapImage = bitmapImage2;
				}
			}
			return bitmapImage;
		}

		// Token: 0x0400016B RID: 363
		private readonly DetectionHandlerFactory detectionHandlerFactory;

		// Token: 0x0400016C RID: 364
		private readonly PhoneFactory phoneFactory;

		// Token: 0x0400016D RID: 365
		private readonly AdaptationManager adaptationManager;

		// Token: 0x0400016E RID: 366
		private readonly DispatcherTimer cancelTimer;

		// Token: 0x0400016F RID: 367
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000170 RID: 368
		private Tile selectedPhoneTile;

		// Token: 0x04000171 RID: 369
		private string liveText;

		// Token: 0x04000172 RID: 370
		private CollectionObservable<Tile> tiles = new CollectionObservable<Tile>();

		// Token: 0x04000173 RID: 371
		private TaskCompletionSource<Tile> itemSelectedTaskCompletionSource;

		// Token: 0x04000174 RID: 372
		private CancellationTokenSource externalTokenSource;

		// Token: 0x04000175 RID: 373
		private CancellationTokenSource internalTokenSource;
	}
}

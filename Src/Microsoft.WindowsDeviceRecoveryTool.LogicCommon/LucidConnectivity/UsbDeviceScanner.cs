using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;
using Nokia.Lucid;
using Nokia.Lucid.DeviceDetection;
using Nokia.Lucid.DeviceInformation;
using Nokia.Lucid.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.LucidConnectivity
{
	// Token: 0x0200002F RID: 47
	public class UsbDeviceScanner
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000AF18 File Offset: 0x00009118
		public UsbDeviceScanner(IList<Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier> deviceIdentifiers)
		{
			this.supportedDeviceIdentifiers = deviceIdentifiers;
			DeviceTypeMap deviceTypeMap = new DeviceTypeMap(this.usbDeviceClassGuid, DeviceType.PhysicalDevice);
			deviceTypeMap = DeviceTypeMap.DefaultMap.InterfaceClasses.Aggregate(deviceTypeMap, (DeviceTypeMap current, Guid guid) => current.SetMapping(guid, DeviceType.Interface));
			this.SupportedDevicesMap = deviceTypeMap;
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060002EB RID: 747 RVA: 0x0000AFC8 File Offset: 0x000091C8
		// (remove) Token: 0x060002EC RID: 748 RVA: 0x0000B000 File Offset: 0x00009200
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<UsbDeviceEventArgs> DeviceConnected;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060002ED RID: 749 RVA: 0x0000B038 File Offset: 0x00009238
		// (remove) Token: 0x060002EE RID: 750 RVA: 0x0000B070 File Offset: 0x00009270
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<UsbDeviceEventArgs> DeviceDisconnected;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060002EF RID: 751 RVA: 0x0000B0A8 File Offset: 0x000092A8
		// (remove) Token: 0x060002F0 RID: 752 RVA: 0x0000B0E0 File Offset: 0x000092E0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<UsbDeviceEventArgs> DeviceEndpointConnected;

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000B118 File Offset: 0x00009318
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000B130 File Offset: 0x00009330
		public DeviceTypeMap SupportedDevicesMap
		{
			get
			{
				return this.supportedGuidMap;
			}
			internal set
			{
				this.supportedGuidMap = value;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B13C File Offset: 0x0000933C
		public Expression<Func<Nokia.Lucid.Primitives.DeviceIdentifier, bool>> GetSupportedVidAndPidExpression()
		{
			bool flag = this.supportedDeviceIdentifiers == null || this.supportedDeviceIdentifiers.Count == 0;
			Expression<Func<Nokia.Lucid.Primitives.DeviceIdentifier, bool>> expression;
			if (flag)
			{
				expression = null;
			}
			else
			{
				Func<Nokia.Lucid.Primitives.DeviceIdentifier, bool> returnFunc = null;
				foreach (Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier deviceIdentifier in this.supportedDeviceIdentifiers)
				{
					bool flag2 = returnFunc == null;
					if (flag2)
					{
						returnFunc = this.GetDeviceIntifierVidPidExpression(deviceIdentifier);
					}
					else
					{
						Func<Nokia.Lucid.Primitives.DeviceIdentifier, bool> currentFunc = this.GetDeviceIntifierVidPidExpression(deviceIdentifier);
						Func<Nokia.Lucid.Primitives.DeviceIdentifier, bool> oldReturnFunc = returnFunc;
						returnFunc = (Nokia.Lucid.Primitives.DeviceIdentifier s) => oldReturnFunc(s) || currentFunc(s);
					}
				}
				expression = (Nokia.Lucid.Primitives.DeviceIdentifier s) => returnFunc(s);
			}
			return expression;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B26C File Offset: 0x0000946C
		private Func<Nokia.Lucid.Primitives.DeviceIdentifier, bool> GetDeviceIntifierVidPidExpression(Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier deviceIdentifier)
		{
			bool flag = deviceIdentifier.Mi != null && deviceIdentifier.Mi.Length != 0;
			Func<Nokia.Lucid.Primitives.DeviceIdentifier, bool> func;
			if (flag)
			{
				func = (Nokia.Lucid.Primitives.DeviceIdentifier s) => s.Vid(deviceIdentifier.Vid) && s.Pid(deviceIdentifier.Pid) && s.MI(deviceIdentifier.Mi);
			}
			else
			{
				bool flag2 = !string.IsNullOrWhiteSpace(deviceIdentifier.Vid) && !string.IsNullOrWhiteSpace(deviceIdentifier.Pid);
				if (flag2)
				{
					func = (Nokia.Lucid.Primitives.DeviceIdentifier s) => s.Vid(deviceIdentifier.Vid) && s.Pid(deviceIdentifier.Pid);
				}
				else
				{
					func = (Nokia.Lucid.Primitives.DeviceIdentifier s) => s.Vid(deviceIdentifier.Vid);
				}
			}
			return func;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B308 File Offset: 0x00009508
		public void Start()
		{
			Tracer<UsbDeviceScanner>.WriteInformation(">>>> Starting USB device scanner <<<<");
			this.deviceDictionary.Clear();
			bool flag = this.deviceWatcher == null;
			if (flag)
			{
				this.deviceWatcher = this.CreateDeviceWatcher();
				this.deviceWatcher.DeviceChanged += this.DeviceWatcherOnDeviceChanged;
				this.deviceWatcherDisposableToken = this.deviceWatcher.Start();
			}
			Tracer<UsbDeviceScanner>.WriteInformation(">>>> USB device scanner started <<<<");
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B37C File Offset: 0x0000957C
		public void Stop()
		{
			Tracer<UsbDeviceScanner>.WriteInformation(">>>> Stopping USB device scanner <<<<");
			try
			{
				bool flag = this.deviceWatcherDisposableToken != null;
				if (flag)
				{
					this.deviceWatcherDisposableToken.Dispose();
					this.deviceWatcherDisposableToken = null;
				}
				bool flag2 = this.deviceWatcher != null;
				if (flag2)
				{
					Tracer<UsbDeviceScanner>.WriteInformation(">>>> Detach and reset Device Watcher <<<<");
					this.deviceWatcher.DeviceChanged -= this.DeviceWatcherOnDeviceChanged;
					this.deviceWatcher = null;
				}
			}
			catch (Exception ex)
			{
				Tracer<UsbDeviceScanner>.WriteError(ex, "Stopping Lucid device watcher failed", new object[0]);
			}
			Tracer<UsbDeviceScanner>.WriteInformation(">>>> USB device scanner stopped <<<<");
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B428 File Offset: 0x00009628
		private DeviceWatcher CreateDeviceWatcher()
		{
			Expression<Func<Nokia.Lucid.Primitives.DeviceIdentifier, bool>> supportedVidAndPidExpression = this.GetSupportedVidAndPidExpression();
			bool flag = supportedVidAndPidExpression != null;
			DeviceWatcher deviceWatcher;
			if (flag)
			{
				deviceWatcher = new DeviceWatcher
				{
					DeviceTypeMap = this.SupportedDevicesMap,
					Filter = supportedVidAndPidExpression
				};
			}
			else
			{
				deviceWatcher = new DeviceWatcher
				{
					DeviceTypeMap = this.SupportedDevicesMap
				};
			}
			return deviceWatcher;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B47C File Offset: 0x0000967C
		private DeviceInfoSet CreateDeviceInfoSet()
		{
			Expression<Func<Nokia.Lucid.Primitives.DeviceIdentifier, bool>> supportedVidAndPidExpression = this.GetSupportedVidAndPidExpression();
			bool flag = supportedVidAndPidExpression != null;
			DeviceInfoSet deviceInfoSet;
			if (flag)
			{
				deviceInfoSet = new DeviceInfoSet
				{
					DeviceTypeMap = this.SupportedDevicesMap,
					Filter = supportedVidAndPidExpression
				};
			}
			else
			{
				deviceInfoSet = new DeviceInfoSet
				{
					DeviceTypeMap = this.SupportedDevicesMap
				};
			}
			return deviceInfoSet;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B4D0 File Offset: 0x000096D0
		public ReadOnlyCollection<UsbDevice> GetDevices()
		{
			Tracer<UsbDeviceScanner>.WriteInformation(">>Getting list of connected USB devices");
			List<UsbDevice> list = new List<UsbDevice>();
			try
			{
				DeviceInfoSet deviceInfoSet = this.CreateDeviceInfoSet();
				Tracer<UsbDeviceScanner>.WriteInformation("-> USB devices enumeration start <-");
				int num = 0;
				foreach (DeviceInfo deviceInfo in deviceInfoSet.EnumeratePresentDevices())
				{
					num++;
					Tracer<UsbDeviceScanner>.WriteInformation("({0}) : {1}", new object[] { num, deviceInfo.InstanceId });
					DeviceType deviceType = deviceInfo.DeviceType;
					DeviceType deviceType2 = deviceType;
					if (deviceType2 != DeviceType.PhysicalDevice)
					{
						if (deviceType2 == DeviceType.Interface)
						{
							Tracer<UsbDeviceScanner>.WriteInformation("INTERFACE");
							string locationPath = this.GetLocationPath(deviceInfo);
							bool flag = !string.IsNullOrEmpty(locationPath);
							if (flag)
							{
								string dictionaryKeyFromLocationPath = this.GetDictionaryKeyFromLocationPath(locationPath);
								bool flag2 = !string.IsNullOrEmpty(dictionaryKeyFromLocationPath);
								if (flag2)
								{
									bool flag3 = !LucidConnectivityHelper.IsWrongDefaultNcsdInterface(deviceInfo);
									if (flag3)
									{
										UsbDevice item = this.deviceDictionary[dictionaryKeyFromLocationPath].Item2;
										item.AddInterface(deviceInfo.Path);
										Tracer<UsbDeviceScanner>.WriteInformation("Endpoint {0} added to device connected to {1}", new object[] { deviceInfo.Path, item.PortId });
									}
									else
									{
										Tracer<UsbDeviceScanner>.WriteWarning("Wrong interface {0} for NCSd communication. Ignoring.", new object[] { deviceInfo.Path });
									}
								}
								else
								{
									Tracer<UsbDeviceScanner>.WriteWarning("No physical device entry found for this interface endpoint", new object[0]);
								}
							}
						}
					}
					else
					{
						Tracer<UsbDeviceScanner>.WriteInformation("PHYSICAL USB DEVICE");
						UsbDevice usbDevice = this.GetUsbDevice(deviceInfo);
						bool flag4 = usbDevice == null;
						if (!flag4)
						{
							this.InsertDeviceToDictionary(deviceInfo.InstanceId, usbDevice);
							list.Add(usbDevice);
							Tracer<UsbDeviceScanner>.WriteInformation("Device added: {0}&{1} at {2}", new object[] { usbDevice.Vid, usbDevice.Pid, usbDevice.PortId });
						}
					}
				}
			}
			catch (Exception ex)
			{
				Tracer<UsbDeviceScanner>.WriteError(ex, "Can't get devices: {0}", new object[] { ex.Message });
			}
			Tracer<UsbDeviceScanner>.WriteInformation("<< List of connected USB devices retrieved ({0} devices found)", new object[] { list.Count });
			Tracer<UsbDeviceScanner>.WriteInformation("-> USB devices enumeration end <-");
			return list.AsReadOnly();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B75C File Offset: 0x0000995C
		public void FillDeviceInfo(Phone phone, CancellationToken token)
		{
			using (JsonCommunication jsonCommunication = UsbDeviceScanner.JsonConnectivity.CreateJsonConnectivity(phone.LocationPath))
			{
				phone.HardwareVariant = this.ReadDeviceInfo(jsonCommunication, InfoType.ProductCode, token);
				phone.SoftwareVersion = this.ReadDeviceInfo(jsonCommunication, InfoType.SwVersion, token);
				phone.Imei = this.ReadDeviceInfo(jsonCommunication, InfoType.SerialNumber, token);
				int num;
				bool flag = int.TryParse(this.ReadDeviceInfo(jsonCommunication, InfoType.BatteryLevel, token), out num);
				if (flag)
				{
					phone.BatteryLevel = num;
				}
				phone.AkVersion = this.ReadDeviceInfo(jsonCommunication, InfoType.AkVersion, token);
				token.ThrowIfCancellationRequested();
				Tracer<UsbDeviceScanner>.WriteInformation("Device information read: Product code: {0} | SW version: {1} | Imei: {2} | Battery level: {3} | Ak version: {4}", new object[] { phone.HardwareVariant, phone.SoftwareVersion, phone.Imei, phone.BatteryLevel, phone.AkVersion });
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B844 File Offset: 0x00009A44
		public int ReadBatteryLevel(Phone phone)
		{
			using (JsonCommunication jsonCommunication = UsbDeviceScanner.JsonConnectivity.CreateJsonConnectivity(phone.LocationPath))
			{
				int num;
				bool flag = int.TryParse(this.ReadDeviceInfo(jsonCommunication, InfoType.BatteryLevel, CancellationToken.None), out num);
				if (flag)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B8A8 File Offset: 0x00009AA8
		internal string GetLocationPath(IDevicePropertySet propertySet)
		{
			for (int i = 0; i < 40; i++)
			{
				Tracer<UsbDeviceScanner>.WriteInformation("Reading location paths (attempt {0})", new object[] { i });
				try
				{
					propertySet.EnumeratePropertyKeys();
					Tracer<UsbDeviceScanner>.WriteInformation("Property keys enumerated");
					string[] array = propertySet.ReadLocationPaths();
					bool flag = array.Length != 0;
					if (flag)
					{
						Tracer<UsbDeviceScanner>.WriteInformation("Location path: {0}", new object[] { array[0] });
						return array[0];
					}
				}
				catch (Exception ex)
				{
					Tracer<UsbDeviceScanner>.WriteWarning(ex, "Location paths not found", new object[0]);
					bool flag2 = i < 39;
					if (flag2)
					{
						Tracer<UsbDeviceScanner>.WriteWarning("Retrying after delay", new object[0]);
						Thread.Sleep(100 * i + 100);
					}
				}
			}
			Tracer<UsbDeviceScanner>.WriteError("Location paths not found (after all retries).", new object[0]);
			return string.Empty;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		internal bool GetNeededProperties(DeviceInfo deviceInfo, out string locationPath, out string locationInfo, out string busReportedDeviceDescription)
		{
			locationPath = string.Empty;
			locationInfo = string.Empty;
			busReportedDeviceDescription = string.Empty;
			try
			{
				locationPath = this.GetLocationPath(deviceInfo);
				Tracer<UsbDeviceScanner>.WriteInformation("Location path = {0}", new object[] { locationPath });
			}
			catch (Exception ex)
			{
				Tracer<UsbDeviceScanner>.WriteWarning(ex, "Location path: not found", new object[0]);
			}
			try
			{
				busReportedDeviceDescription = deviceInfo.ReadBusReportedDeviceDescription();
				Tracer<UsbDeviceScanner>.WriteInformation("Bus reported device description = {0}", new object[] { busReportedDeviceDescription });
			}
			catch (Exception ex2)
			{
				Tracer<UsbDeviceScanner>.WriteWarning(ex2, "Bus reported device description: not found", new object[0]);
			}
			try
			{
				locationInfo = deviceInfo.ReadLocationInformation();
				Tracer<UsbDeviceScanner>.WriteInformation("Location info = {0}", new object[] { locationInfo });
			}
			catch (Exception ex3)
			{
				Tracer<UsbDeviceScanner>.WriteWarning(ex3, "Location info: not found", new object[0]);
			}
			bool flag = string.IsNullOrEmpty(locationPath);
			bool flag2;
			if (flag)
			{
				Tracer<UsbDeviceScanner>.WriteWarning("Location path is empty", new object[0]);
				flag2 = false;
			}
			else
			{
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000BACC File Offset: 0x00009CCC
		internal void DetermineDeviceTypeDesignatorAndSalesName(string pid, string busReportedDeviceDescription, out string typeDesignator, out string salesName)
		{
			LucidConnectivityHelper.ParseTypeDesignatorAndSalesName(busReportedDeviceDescription, out typeDesignator, out salesName);
			Tracer<UsbDeviceScanner>.WriteInformation("Type designator: {0}, Sales name: {1}", new object[] { typeDesignator, salesName });
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		private string ReadDeviceInfo(JsonCommunication jsonCommunication, InfoType infoType, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			string text3;
			try
			{
				string text = "{\"jsonrpc\":\"2.0\",\"method\":\"Read" + infoType.ToString() + "\",\"params\":{\"MessageVersion\":0}}\0";
				string text2 = this.SendAndReceive(text, jsonCommunication);
				text3 = this.ParseValue(text2, infoType);
			}
			catch
			{
				text3 = string.Empty;
			}
			return text3;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000BB5C File Offset: 0x00009D5C
		private string SendAndReceive(string message, JsonCommunication jsonCommunication)
		{
			jsonCommunication.Send(message);
			return jsonCommunication.ReceiveJson(TimeSpan.FromMilliseconds(500.0));
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000BB8C File Offset: 0x00009D8C
		private string ParseValue(string message, InfoType infoType)
		{
			bool flag = string.IsNullOrEmpty(message) || message.IndexOf(infoType.ToString(), StringComparison.InvariantCultureIgnoreCase) < 0;
			if (flag)
			{
				throw new InvalidOperationException();
			}
			message = Regex.Replace(message, "\\t|\\n|\\r|\\s|\\\\r|\\\\n", string.Empty);
			bool flag2 = infoType.ToString() == "BatteryLevel";
			string text;
			if (flag2)
			{
				message = message.Substring(message.IndexOf(infoType.ToString(), StringComparison.InvariantCultureIgnoreCase) + infoType.ToString().Length + 2);
				text = Regex.Match(message, "\\d+").Value;
			}
			else
			{
				message = message.Substring(message.IndexOf(infoType.ToString(), StringComparison.InvariantCultureIgnoreCase) + infoType.ToString().Length + 3);
				text = message.Substring(0, message.IndexOf("}", StringComparison.InvariantCultureIgnoreCase) - 1);
			}
			return text;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000BC84 File Offset: 0x00009E84
		internal string GetPhysicalPortId(string locationPath, string locationInfo)
		{
			string text = string.Empty;
			bool flag = !string.IsNullOrEmpty(locationPath);
			if (flag)
			{
				text = LucidConnectivityHelper.LocationPath2ControllerId(locationPath);
			}
			string text2 = string.Format("{0}:{1}", text, LucidConnectivityHelper.GetHubAndPort(locationInfo));
			Tracer<UsbDeviceScanner>.WriteInformation("Parsed port identifier: {0}", new object[] { text2 });
			return text2;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000BCDC File Offset: 0x00009EDC
		private string FindGuidFromDevicePath(string devicePath)
		{
			int num = devicePath.LastIndexOf('{');
			return (num > 0) ? devicePath.Substring(num) : string.Empty;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000BD0C File Offset: 0x00009F0C
		private void DeviceWatcherOnDeviceChanged(object sender, DeviceChangedEventArgs e)
		{
			string guid = this.FindGuidFromDevicePath(e.Path);
			Tracer<UsbDeviceScanner>.WriteInformation("<LUCID>: DeviceChanged '{0}' event handling START ({1})", new object[] { e.Action, guid });
			Task.Factory.StartNew(delegate
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				bool flag = e.Action == DeviceChangeAction.Attach;
				if (flag)
				{
					DeviceType deviceType = e.DeviceType;
					DeviceType deviceType2 = deviceType;
					if (deviceType2 != DeviceType.PhysicalDevice)
					{
						if (deviceType2 == DeviceType.Interface)
						{
							this.HandleInterfaceAdded(e);
						}
					}
					else
					{
						this.HandleDeviceAdded(e);
					}
				}
				else
				{
					bool flag2 = e.DeviceType == DeviceType.PhysicalDevice;
					if (flag2)
					{
						this.HandleDeviceRemoved(e);
					}
				}
				Tracer<UsbDeviceScanner>.WriteInformation("<LUCID>: DeviceChanged '{0}' event handling END ({1}). Duration = {2}", new object[] { e.Action, guid, stopwatch.Elapsed });
			});
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000BD8C File Offset: 0x00009F8C
		private void HandleDeviceAdded(DeviceChangedEventArgs e)
		{
			try
			{
				Tracer<UsbDeviceScanner>.WriteInformation("Device connected");
				DeviceInfoSet deviceInfoSet = this.CreateDeviceInfoSet();
				DeviceInfo device = deviceInfoSet.GetDevice(e.Path);
				Tracer<UsbDeviceScanner>.WriteInformation("Device path: {0}", new object[] { device.Path });
				Tracer<UsbDeviceScanner>.WriteInformation("InstanceId: {0}", new object[] { device.InstanceId });
				string instanceId = device.InstanceId;
				this.interfaceLocks.CreateLock(instanceId);
				this.interfaceLocks.Lock(instanceId);
				UsbDevice usbDevice = this.GetUsbDevice(device);
				bool flag = usbDevice == null;
				if (flag)
				{
					Tracer<UsbDeviceScanner>.WriteInformation("USB device was null => Ignored");
				}
				else
				{
					this.InsertDeviceToDictionary(device.InstanceId, usbDevice);
					this.SendConnectionAddedEvent(usbDevice);
					Tracer<UsbDeviceScanner>.WriteInformation("Device added event sent: {0}/{1}&{2}", new object[] { usbDevice.PortId, usbDevice.Vid, usbDevice.Pid });
					this.interfaceLocks.Unlock(instanceId);
				}
			}
			catch (Exception ex)
			{
				Tracer<UsbDeviceScanner>.WriteError(ex, "Error handling connected device", new object[0]);
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000BEAC File Offset: 0x0000A0AC
		private void HandleInterfaceAdded(DeviceChangedEventArgs e)
		{
			try
			{
				Tracer<UsbDeviceScanner>.WriteInformation("Interface added event handling started");
				DeviceInfoSet deviceInfoSet = this.CreateDeviceInfoSet();
				DeviceInfo device = deviceInfoSet.GetDevice(e.Path);
				string interfaceUnlockKey = this.GetInterfaceUnlockKey(device);
				this.interfaceLocks.Wait(interfaceUnlockKey, 5000);
				this.interfaceLocks.Discard(interfaceUnlockKey);
				bool flag = LucidConnectivityHelper.IsWrongDefaultNcsdInterface(device);
				if (flag)
				{
					Tracer<UsbDeviceScanner>.WriteWarning("Wrong interface {0} for NCSd communication. Ignoring.", new object[] { device.Path });
				}
				else
				{
					Tracer<UsbDeviceScanner>.WriteInformation("Device path: {0}", new object[] { device.Path });
					Tracer<UsbDeviceScanner>.WriteInformation("InstanceId: {0}", new object[] { device.InstanceId });
					string locationPath = this.GetLocationPath(device);
					bool flag2 = !string.IsNullOrEmpty(locationPath);
					if (flag2)
					{
						string dictionaryKeyFromLocationPath = this.GetDictionaryKeyFromLocationPath(locationPath);
						bool flag3 = !string.IsNullOrEmpty(dictionaryKeyFromLocationPath);
						if (flag3)
						{
							this.deviceDictionary[dictionaryKeyFromLocationPath].Item2.AddInterface(e.Path);
							this.SendConnectionEndpointAddedEvent(this.deviceDictionary[dictionaryKeyFromLocationPath].Item2);
						}
					}
					else
					{
						Tracer<UsbDeviceScanner>.WriteInformation("Ignored");
					}
					Tracer<UsbDeviceScanner>.WriteInformation("Interface added event handling ended");
				}
			}
			catch (Exception ex)
			{
				Tracer<UsbDeviceScanner>.WriteError(ex, "Error handling interface added", new object[0]);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C020 File Offset: 0x0000A220
		private void HandleDeviceRemoved(DeviceChangedEventArgs e)
		{
			try
			{
				Tracer<UsbDeviceScanner>.WriteInformation("Device disconnected");
				DeviceInfoSet deviceInfoSet = this.CreateDeviceInfoSet();
				DeviceInfo device = deviceInfoSet.GetDevice(e.Path);
				string dictionaryKeyFromInstanceId = this.GetDictionaryKeyFromInstanceId(device.InstanceId);
				bool flag = !string.IsNullOrEmpty(dictionaryKeyFromInstanceId);
				if (flag)
				{
					UsbDevice item = this.deviceDictionary[dictionaryKeyFromInstanceId].Item2;
					this.SendConnectionRemovedEvent(item);
					Tracer<UsbDeviceScanner>.WriteInformation("Device removed event sent: {0}/{1}&{2}", new object[] { item.PortId, item.Vid, item.Pid });
					this.deviceDictionary.Remove(dictionaryKeyFromInstanceId);
				}
				else
				{
					Tracer<UsbDeviceScanner>.WriteInformation("Ignored");
				}
			}
			catch (Exception ex)
			{
				Tracer<UsbDeviceScanner>.WriteError(ex, "Error handling disconnected device", new object[0]);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C100 File Offset: 0x0000A300
		private void InsertDeviceToDictionary(string instanceId, UsbDevice usbDevice)
		{
			string dictionaryKeyFromLocationPath = this.GetDictionaryKeyFromLocationPath(usbDevice.LocationPath);
			bool flag = !string.IsNullOrEmpty(dictionaryKeyFromLocationPath);
			if (flag)
			{
				this.deviceDictionary[dictionaryKeyFromLocationPath] = new Tuple<string, UsbDevice>(instanceId, usbDevice);
			}
			else
			{
				this.deviceDictionary.Add(usbDevice.LocationPath, new Tuple<string, UsbDevice>(instanceId, usbDevice));
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C15C File Offset: 0x0000A35C
		private string GetDictionaryKeyFromLocationPath(string locationPath)
		{
			foreach (string text in this.deviceDictionary.Keys)
			{
				bool flag = locationPath.StartsWith(text);
				if (flag)
				{
					return text;
				}
			}
			return string.Empty;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		private string GetDictionaryKeyFromInstanceId(string instanceId)
		{
			IEnumerable<KeyValuePair<string, Tuple<string, UsbDevice>>> enumerable = this.deviceDictionary;
			Func<KeyValuePair<string, Tuple<string, UsbDevice>>, bool> <>9__0;
			Func<KeyValuePair<string, Tuple<string, UsbDevice>>, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (KeyValuePair<string, Tuple<string, UsbDevice>> item) => instanceId == item.Value.Item1);
			}
			using (IEnumerator<KeyValuePair<string, Tuple<string, UsbDevice>>> enumerator = enumerable.Where(func).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<string, Tuple<string, UsbDevice>> keyValuePair = enumerator.Current;
					return keyValuePair.Key;
				}
			}
			return string.Empty;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C260 File Offset: 0x0000A460
		private UsbDevice GetUsbDevice(DeviceInfo deviceInfo)
		{
			try
			{
				Tracer<UsbDeviceScanner>.WriteInformation("Getting USB device");
				string instanceId = deviceInfo.InstanceId;
				Tracer<UsbDeviceScanner>.WriteInformation("Device detected: {0}", new object[] { instanceId });
				string text;
				string text2;
				string text3;
				bool flag = !this.GetNeededProperties(deviceInfo, out text, out text2, out text3);
				if (flag)
				{
					Tracer<UsbDeviceScanner>.WriteError("Needed properties are not available", new object[0]);
					return null;
				}
				string physicalPortId = this.GetPhysicalPortId(text, text2);
				string text4;
				string text5;
				LucidConnectivityHelper.GetVidAndPid(instanceId, out text4, out text5);
				string text6;
				string text7;
				this.DetermineDeviceTypeDesignatorAndSalesName(text5, text3, out text6, out text7);
				Tracer<UsbDeviceScanner>.WriteInformation("USB device: {0}/{1}&{2}", new object[] { physicalPortId, text4, text5 });
				return new UsbDevice(physicalPortId, text4, text5, text, text6, text7, deviceInfo.Path, deviceInfo.InstanceId);
			}
			catch (Exception ex)
			{
				Tracer<UsbDeviceScanner>.WriteError(ex, "Cannot get USB device", new object[0]);
			}
			Tracer<UsbDeviceScanner>.WriteInformation("Device not compatible");
			return null;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C364 File Offset: 0x0000A564
		private void SendConnectionAddedEvent(UsbDevice connection)
		{
			Tracer<UsbDeviceScanner>.WriteInformation("SendConnectionAddedEvent: portID: {0}, VID&PID: {1}", new object[]
			{
				connection.PortId,
				connection.Vid + "&" + connection.Pid
			});
			EventHandler<UsbDeviceEventArgs> deviceConnected = this.DeviceConnected;
			bool flag = deviceConnected != null;
			if (flag)
			{
				deviceConnected(this, new UsbDeviceEventArgs(connection));
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		private void SendConnectionEndpointAddedEvent(UsbDevice connection)
		{
			Tracer<UsbDeviceScanner>.WriteInformation("SendConnectionEndpointAddedEvent: portID: {0}, VID&PID: {1}", new object[]
			{
				connection.PortId,
				connection.Vid + "&" + connection.Pid
			});
			EventHandler<UsbDeviceEventArgs> deviceEndpointConnected = this.DeviceEndpointConnected;
			bool flag = deviceEndpointConnected != null;
			if (flag)
			{
				deviceEndpointConnected(this, new UsbDeviceEventArgs(connection));
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000C424 File Offset: 0x0000A624
		private void SendConnectionRemovedEvent(UsbDevice connection)
		{
			Tracer<UsbDeviceScanner>.WriteInformation("SendConnectionRemovedEvent: portID: {0}, VID&PID: {1}", new object[]
			{
				connection.PortId,
				connection.Vid + "&" + connection.Pid
			});
			EventHandler<UsbDeviceEventArgs> deviceDisconnected = this.DeviceDisconnected;
			bool flag = deviceDisconnected != null;
			if (flag)
			{
				deviceDisconnected(this, new UsbDeviceEventArgs(connection));
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000C484 File Offset: 0x0000A684
		private string GetInterfaceUnlockKey(DeviceInfo device)
		{
			string text3;
			try
			{
				string text;
				string text2;
				LucidConnectivityHelper.GetVidAndPid(device.InstanceId, out text, out text2);
				ConnectedDeviceMode deviceMode = LucidConnectivityHelper.GetDeviceMode(text, text2);
				ConnectedDeviceMode connectedDeviceMode = deviceMode;
				ConnectedDeviceMode connectedDeviceMode2 = connectedDeviceMode;
				if (connectedDeviceMode2 != ConnectedDeviceMode.Uefi)
				{
					text3 = device.ReadParentInstanceId();
				}
				else
				{
					text3 = device.InstanceId;
				}
			}
			catch (Exception ex)
			{
				Tracer<UsbDeviceScanner>.WriteError(ex, "Could not determine interface unlock key", new object[0]);
				text3 = string.Empty;
			}
			return text3;
		}

		// Token: 0x04000134 RID: 308
		private static readonly JsonConnectivity JsonConnectivity = new JsonConnectivity();

		// Token: 0x04000135 RID: 309
		private readonly Dictionary<string, Tuple<string, UsbDevice>> deviceDictionary = new Dictionary<string, Tuple<string, UsbDevice>>();

		// Token: 0x04000136 RID: 310
		private readonly Guid usbDeviceClassGuid = new Guid(2782707472U, 25904, 4562, 144, 31, 0, 192, 79, 185, 81, 237);

		// Token: 0x04000137 RID: 311
		private readonly InterfaceHandlingLocks interfaceLocks = new InterfaceHandlingLocks();

		// Token: 0x04000138 RID: 312
		private DeviceTypeMap supportedGuidMap;

		// Token: 0x04000139 RID: 313
		private IList<Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier> supportedDeviceIdentifiers;

		// Token: 0x0400013A RID: 314
		private DeviceWatcher deviceWatcher;

		// Token: 0x0400013B RID: 315
		private IDisposable deviceWatcherDisposableToken;
	}
}

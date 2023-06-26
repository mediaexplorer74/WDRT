using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.LucidConnectivity;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;

namespace Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Connectivity
{
	// Token: 0x0200000B RID: 11
	public class LumiaConnectivity
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600008B RID: 139 RVA: 0x00005D70 File Offset: 0x00003F70
		// (remove) Token: 0x0600008C RID: 140 RVA: 0x00005DA8 File Offset: 0x00003FA8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<DeviceConnectedEventArgs> DeviceConnected;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600008D RID: 141 RVA: 0x00005DE0 File Offset: 0x00003FE0
		// (remove) Token: 0x0600008E RID: 142 RVA: 0x00005E18 File Offset: 0x00004018
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<DeviceConnectedEventArgs> DeviceDisconnected;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600008F RID: 143 RVA: 0x00005E50 File Offset: 0x00004050
		// (remove) Token: 0x06000090 RID: 144 RVA: 0x00005E88 File Offset: 0x00004088
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<DeviceReadyChangedEventArgs> DeviceReadyChanged;

		// Token: 0x06000091 RID: 145 RVA: 0x00005EC0 File Offset: 0x000040C0
		public Collection<ConnectedDevice> GetAllConnectedDevices()
		{
			return new Collection<ConnectedDevice>(this.devices);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005EE0 File Offset: 0x000040E0
		public void Start(IList<DeviceIdentifier> deviceIdentifiers)
		{
			bool flag = this.usbDeviceDetector == null;
			if (flag)
			{
				this.usbDeviceDetector = new UsbDeviceScanner(deviceIdentifiers);
				this.usbDeviceDetector.DeviceConnected += this.HandleDeviceConnected;
				this.usbDeviceDetector.DeviceDisconnected += this.HandleDeviceDisconnected;
				this.usbDeviceDetector.DeviceEndpointConnected += this.HandleDeviceEndpointConnected;
				this.usbDeviceDetector.Start();
				ReadOnlyCollection<UsbDevice> readOnlyCollection = this.usbDeviceDetector.GetDevices();
				foreach (UsbDevice usbDevice in readOnlyCollection)
				{
					this.devices.Add(LucidConnectivityHelper.GetConnectedDeviceFromUsbDevice(usbDevice));
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005FB8 File Offset: 0x000041B8
		public void Stop()
		{
			bool flag = this.usbDeviceDetector != null;
			if (flag)
			{
				this.usbDeviceDetector.Stop();
				this.usbDeviceDetector.DeviceConnected -= this.HandleDeviceConnected;
				this.usbDeviceDetector.DeviceDisconnected -= this.HandleDeviceDisconnected;
				this.usbDeviceDetector.DeviceEndpointConnected -= this.HandleDeviceEndpointConnected;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000602C File Offset: 0x0000422C
		private void HandleDeviceConnected(object sender, UsbDeviceEventArgs e)
		{
			bool flag = e.UsbDevice != null;
			if (flag)
			{
				Tracer<LumiaConnectivity>.WriteInformation("HandleDeviceConnected: portID: {0}, VID&PID: {1}", new object[]
				{
					e.UsbDevice.PortId,
					e.UsbDevice.Vid + "&" + e.UsbDevice.Pid
				});
				foreach (ConnectedDevice connectedDevice in this.devices)
				{
					bool flag2 = connectedDevice.PortId == e.UsbDevice.PortId;
					if (flag2)
					{
						bool flag3 = !connectedDevice.IsDeviceConnected;
						if (flag3)
						{
							this.devices[this.devices.IndexOf(connectedDevice)].IsDeviceConnected = true;
							this.devices[this.devices.IndexOf(connectedDevice)].DeviceReady = false;
							this.devices[this.devices.IndexOf(connectedDevice)].DevicePath = string.Empty;
							this.devices[this.devices.IndexOf(connectedDevice)].Vid = e.UsbDevice.Vid;
							this.devices[this.devices.IndexOf(connectedDevice)].Pid = e.UsbDevice.Pid;
							ConnectedDeviceMode mode = this.devices[this.devices.IndexOf(connectedDevice)].Mode;
							ConnectedDeviceMode deviceMode = LucidConnectivityHelper.GetDeviceMode(e.UsbDevice.Vid, e.UsbDevice.Pid);
							this.devices[this.devices.IndexOf(connectedDevice)].Mode = deviceMode;
							this.devices[this.devices.IndexOf(connectedDevice)].TypeDesignator = e.UsbDevice.TypeDesignator;
							this.devices[this.devices.IndexOf(connectedDevice)].SalesName = e.UsbDevice.SalesName;
							bool flag4 = deviceMode != mode;
							if (flag4)
							{
								this.SendDeviceConnectedEvent(this.devices[this.devices.IndexOf(connectedDevice)]);
							}
							else
							{
								this.SendDeviceConnectedEvent(this.devices[this.devices.IndexOf(connectedDevice)]);
							}
						}
						return;
					}
				}
				ConnectedDevice connectedDevice2 = new ConnectedDevice(e.UsbDevice.PortId, e.UsbDevice.Vid, e.UsbDevice.Pid, LucidConnectivityHelper.GetDeviceMode(e.UsbDevice.Vid, e.UsbDevice.Pid), true, e.UsbDevice.TypeDesignator, e.UsbDevice.SalesName, e.UsbDevice.Path, e.UsbDevice.InstanceId);
				this.devices.Add(connectedDevice2);
				this.SendDeviceConnectedEvent(connectedDevice2);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006344 File Offset: 0x00004544
		private void HandleDeviceEndpointConnected(object sender, UsbDeviceEventArgs e)
		{
			bool flag = e.UsbDevice != null;
			if (flag)
			{
				Tracer<LumiaConnectivity>.WriteInformation("HandleDeviceEndpointConnected: portID: {0}, VID&PID: {1}", new object[]
				{
					e.UsbDevice.PortId,
					e.UsbDevice.Vid + "&" + e.UsbDevice.Pid
				});
				foreach (ConnectedDevice connectedDevice in this.devices)
				{
					bool flag2 = connectedDevice.PortId == e.UsbDevice.PortId;
					if (flag2)
					{
						ConnectedDeviceMode mode = this.devices[this.devices.IndexOf(connectedDevice)].Mode;
						ConnectedDeviceMode connectedDeviceMode = mode;
						if (connectedDeviceMode == ConnectedDeviceMode.Normal || connectedDeviceMode == ConnectedDeviceMode.Uefi)
						{
							bool flag3 = e.UsbDevice.Interfaces.Count > 0;
							if (flag3)
							{
								this.devices[this.devices.IndexOf(connectedDevice)].DeviceReady = true;
								this.devices[this.devices.IndexOf(connectedDevice)].DevicePath = e.UsbDevice.Interfaces[0].DevicePath;
								this.SendDeviceReadyChangedEvent(this.devices[this.devices.IndexOf(connectedDevice)]);
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000064C8 File Offset: 0x000046C8
		private void HandleDeviceDisconnected(object sender, UsbDeviceEventArgs e)
		{
			bool flag = e.UsbDevice != null;
			if (flag)
			{
				Tracer<LumiaConnectivity>.WriteInformation("HandleDeviceDisconnected: portID: {0}, VID&PID: {1}", new object[]
				{
					e.UsbDevice.PortId,
					e.UsbDevice.Vid + "&" + e.UsbDevice.Pid
				});
				foreach (ConnectedDevice connectedDevice in this.devices)
				{
					bool flag2 = connectedDevice.PortId == e.UsbDevice.PortId;
					if (flag2)
					{
						this.devices[this.devices.IndexOf(connectedDevice)].IsDeviceConnected = false;
						this.devices[this.devices.IndexOf(connectedDevice)].DeviceReady = false;
						this.devices[this.devices.IndexOf(connectedDevice)].DevicePath = string.Empty;
						this.SendDeviceDisconnectedEvent(this.devices[this.devices.IndexOf(connectedDevice)]);
						this.devices.Remove(connectedDevice);
						break;
					}
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006618 File Offset: 0x00004818
		private void SendDeviceConnectedEvent(ConnectedDevice device)
		{
			bool flag = !device.SuppressConnectedDisconnectedEvents;
			if (flag)
			{
				Tracer<LumiaConnectivity>.WriteInformation("SendDeviceConnectedEvent: portID: {0}, VID&PID: {1}, mode: {2}, connected: {3}, typeDesignator: {4}", new object[]
				{
					device.PortId,
					device.Vid + "&" + device.Pid,
					device.Mode,
					device.IsDeviceConnected,
					device.TypeDesignator
				});
				EventHandler<DeviceConnectedEventArgs> deviceConnected = this.DeviceConnected;
				bool flag2 = deviceConnected != null;
				if (flag2)
				{
					deviceConnected(this, new DeviceConnectedEventArgs(device));
				}
			}
			else
			{
				Tracer<LumiaConnectivity>.WriteInformation("SendDeviceConnectedEvent: event suppressed. portID: {0}, VID&PID: {1}, mode: {2}, connected: {3}, typeDesignator: {4}", new object[]
				{
					device.PortId,
					device.Vid + "&" + device.Pid,
					device.Mode,
					device.IsDeviceConnected,
					device.TypeDesignator
				});
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006708 File Offset: 0x00004908
		private void SendDeviceDisconnectedEvent(ConnectedDevice device)
		{
			bool flag = !device.SuppressConnectedDisconnectedEvents;
			if (flag)
			{
				Tracer<LumiaConnectivity>.WriteInformation("SendDeviceDisconnectedEvent: portID: {0}, VID&PID: {1}, mode: {2}, connected: {3}, typeDesignator: {4}", new object[]
				{
					device.PortId,
					device.Vid + "&" + device.Pid,
					device.Mode,
					device.IsDeviceConnected,
					device.TypeDesignator
				});
				EventHandler<DeviceConnectedEventArgs> deviceDisconnected = this.DeviceDisconnected;
				bool flag2 = deviceDisconnected != null;
				if (flag2)
				{
					deviceDisconnected(this, new DeviceConnectedEventArgs(device));
				}
			}
			else
			{
				Tracer<LumiaConnectivity>.WriteInformation("SendDeviceDisconnectedEvent: event suppressed. portID: {0}, VID&PID: {1}, mode: {2}, connected: {3}, typeDesignator: {4}", new object[]
				{
					device.PortId,
					device.Vid + "&" + device.Pid,
					device.Mode,
					device.IsDeviceConnected,
					device.TypeDesignator
				});
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000067F8 File Offset: 0x000049F8
		private void SendDeviceReadyChangedEvent(ConnectedDevice device)
		{
			Tracer<LumiaConnectivity>.WriteInformation("SendDeviceReadyChangedEvent: portID: {0}, VID&PID: {1}, mode: {2}, connected: {3}, typeDesignator: {4}", new object[]
			{
				device.PortId,
				device.Vid + "&" + device.Pid,
				device.Mode,
				device.IsDeviceConnected,
				device.TypeDesignator
			});
			EventHandler<DeviceReadyChangedEventArgs> deviceReadyChanged = this.DeviceReadyChanged;
			bool flag = deviceReadyChanged != null;
			if (flag)
			{
				deviceReadyChanged(this, new DeviceReadyChangedEventArgs(device, device.DeviceReady, device.Mode));
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006889 File Offset: 0x00004A89
		public void FillLumiaDeviceInfo(Phone phone, CancellationToken token)
		{
			this.usbDeviceDetector.FillDeviceInfo(phone, token);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000689C File Offset: 0x00004A9C
		public int ReadBatteryLevel(Phone phone)
		{
			return this.usbDeviceDetector.ReadBatteryLevel(phone);
		}

		// Token: 0x04000039 RID: 57
		private readonly Collection<ConnectedDevice> devices = new Collection<ConnectedDevice>();

		// Token: 0x0400003A RID: 58
		private UsbDeviceScanner usbDeviceDetector;
	}
}

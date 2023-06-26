using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.LucidConnectivity;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x0200000D RID: 13
	[Export]
	public class ManufacturerAutodetectionService : IDisposable
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000027B8 File Offset: 0x000009B8
		[ImportingConstructor]
		public ManufacturerAutodetectionService()
		{
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000084 RID: 132 RVA: 0x00003560 File Offset: 0x00001760
		// (remove) Token: 0x06000085 RID: 133 RVA: 0x00003598 File Offset: 0x00001798
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<UsbDeviceEventArgs> DeviceConnected;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000086 RID: 134 RVA: 0x000035D0 File Offset: 0x000017D0
		// (remove) Token: 0x06000087 RID: 135 RVA: 0x00003608 File Offset: 0x00001808
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<UsbDeviceEventArgs> DeviceDisconnected;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000088 RID: 136 RVA: 0x00003640 File Offset: 0x00001840
		// (remove) Token: 0x06000089 RID: 137 RVA: 0x00003678 File Offset: 0x00001878
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<UsbDeviceEventArgs> DeviceEndpointConnected;

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000036AD File Offset: 0x000018AD
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000036B5 File Offset: 0x000018B5
		private bool Disposed { get; set; }

		// Token: 0x0600008C RID: 140 RVA: 0x000036BE File Offset: 0x000018BE
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000036D0 File Offset: 0x000018D0
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.Disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.ReleaseManagedObjects();
				}
				this.ReleaseUnmanagedObjects();
				this.Disposed = true;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000213E File Offset: 0x0000033E
		protected virtual void ReleaseManagedObjects()
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000213E File Offset: 0x0000033E
		protected virtual void ReleaseUnmanagedObjects()
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003708 File Offset: 0x00001908
		public void Start(Collection<DeviceIdentifier> deviceIdentifiers)
		{
			Tracer<ManufacturerAutodetectionService>.LogEntry("Start");
			Tracer<ManufacturerAutodetectionService>.WriteInformation("Creating UsbDeviceScanner");
			this.usbDeviceDetector = new UsbDeviceScanner(deviceIdentifiers);
			Tracer<ManufacturerAutodetectionService>.WriteInformation("Starting UsbDeviceDetection");
			this.usbDeviceDetector.DeviceConnected += this.UsbDeviceDetectorOnDeviceConnected;
			this.usbDeviceDetector.DeviceDisconnected += this.UsbDeviceDetectorOnDeviceDisconnected;
			this.usbDeviceDetector.DeviceEndpointConnected += this.UsbDeviceDetectorOnDeviceEndpointConnected;
			this.usbDeviceDetector.Start();
			Tracer<ManufacturerAutodetectionService>.LogExit("Start");
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000037A4 File Offset: 0x000019A4
		public ReadOnlyCollection<UsbDevice> GetConnectedDevices()
		{
			return this.usbDeviceDetector.GetDevices();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000037C1 File Offset: 0x000019C1
		private void UsbDeviceDetectorOnDeviceConnected(object sender, UsbDeviceEventArgs usbDeviceEventArgs)
		{
			this.RaiseDeviceConnected(usbDeviceEventArgs);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000037CC File Offset: 0x000019CC
		private void UsbDeviceDetectorOnDeviceDisconnected(object sender, UsbDeviceEventArgs args)
		{
			this.RaiseDeviceDisconnected(args);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000037D7 File Offset: 0x000019D7
		private void UsbDeviceDetectorOnDeviceEndpointConnected(object sender, UsbDeviceEventArgs args)
		{
			this.RaiseDeviceEndpointConnected(args);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000037E4 File Offset: 0x000019E4
		private void RaiseDeviceConnected(UsbDeviceEventArgs usbDeviceEventArgs)
		{
			Action<UsbDeviceEventArgs> deviceConnected = this.DeviceConnected;
			bool flag = deviceConnected != null;
			if (flag)
			{
				deviceConnected(usbDeviceEventArgs);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000380C File Offset: 0x00001A0C
		private void RaiseDeviceDisconnected(UsbDeviceEventArgs usbDeviceEventArgs)
		{
			Action<UsbDeviceEventArgs> deviceDisconnected = this.DeviceDisconnected;
			bool flag = deviceDisconnected != null;
			if (flag)
			{
				deviceDisconnected(usbDeviceEventArgs);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003834 File Offset: 0x00001A34
		private void RaiseDeviceEndpointConnected(UsbDeviceEventArgs usbDeviceEventArgs)
		{
			Action<UsbDeviceEventArgs> deviceEndpointConnected = this.DeviceEndpointConnected;
			bool flag = deviceEndpointConnected != null;
			if (flag)
			{
				deviceEndpointConnected(usbDeviceEventArgs);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000385C File Offset: 0x00001A5C
		public void Stop()
		{
			Tracer<ManufacturerAutodetectionService>.LogEntry("Stop");
			bool flag = this.usbDeviceDetector != null;
			if (flag)
			{
				this.usbDeviceDetector.DeviceConnected -= this.UsbDeviceDetectorOnDeviceConnected;
				this.usbDeviceDetector.DeviceDisconnected -= this.UsbDeviceDetectorOnDeviceDisconnected;
				this.usbDeviceDetector.DeviceEndpointConnected -= this.UsbDeviceDetectorOnDeviceEndpointConnected;
				this.usbDeviceDetector.Stop();
				this.usbDeviceDetector = null;
			}
			Tracer<ManufacturerAutodetectionService>.LogExit("Stop");
		}

		// Token: 0x0400002E RID: 46
		private UsbDeviceScanner usbDeviceDetector;
	}
}

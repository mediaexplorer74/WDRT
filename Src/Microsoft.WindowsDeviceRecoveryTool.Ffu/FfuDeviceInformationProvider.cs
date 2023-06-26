using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using FFUComponents;
using Microsoft.WindowsDeviceRecoveryTool.Core;

namespace Microsoft.WindowsDeviceRecoveryTool.Ffu
{
	// Token: 0x02000005 RID: 5
	[Export]
	internal sealed class FfuDeviceInformationProvider : IFfuDeviceInformationProvider, IDeviceInformationProvider<FfuDeviceInformation>
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020CC File Offset: 0x000002CC
		public Task<FfuDeviceInformation> ReadInformationAsync(string usbDeviceInterfaceDevicePath, CancellationToken cancellationToken)
		{
			return Task.Run<FfuDeviceInformation>(() => this.ReadInformation(usbDeviceInterfaceDevicePath, cancellationToken), cancellationToken);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000210C File Offset: 0x0000030C
		private FfuDeviceInformation ReadInformation(string usbDeviceInterfaceDevicePath, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			FfuDeviceInformation ffuDeviceInformation;
			using (SimpleIODevice simpleIODevice = new SimpleIODevice(usbDeviceInterfaceDevicePath))
			{
				if (!simpleIODevice.IsConnected())
				{
					throw new InvalidOperationException("Device disconnected");
				}
				string deviceFriendlyName = simpleIODevice.DeviceFriendlyName;
				Guid deviceUniqueID = simpleIODevice.DeviceUniqueID;
				string usbDevicePath = simpleIODevice.UsbDevicePath;
				ffuDeviceInformation = new FfuDeviceInformation(deviceFriendlyName, usbDevicePath, deviceUniqueID);
			}
			return ffuDeviceInformation;
		}
	}
}

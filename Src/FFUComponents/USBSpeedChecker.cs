using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace FFUComponents
{
	// Token: 0x02000057 RID: 87
	public class USBSpeedChecker
	{
		// Token: 0x06000189 RID: 393 RVA: 0x000082E7 File Offset: 0x000064E7
		public USBSpeedChecker(string UsbDevicePath)
		{
			if (string.IsNullOrEmpty(UsbDevicePath))
			{
				throw new FFUException(Resources.ERROR_NULL_OR_EMPTY_STRING);
			}
			this.UsbDevicePath = UsbDevicePath;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000830C File Offset: 0x0000650C
		public ConnectionType GetConnectionSpeed()
		{
			char[] array = new char[] { '#' };
			string[] array2 = this.UsbDevicePath.Split(array);
			string text = array2[1];
			string text2 = array2[2];
			string text3 = string.Format("usb\\{0}\\{1}", text, text2);
			ConnectionType connectionType;
			try
			{
				DeviceSet deviceSet = new DeviceSet(text3);
				uint address = deviceSet.GetAddress();
				string parentId = deviceSet.GetParentId();
				DeviceSet deviceSet2 = new DeviceSet(parentId);
				string hubDevicePath = deviceSet2.GetHubDevicePath();
				USB_NODE_CONNECTION_INFORMATION_EX_V2 usb_NODE_CONNECTION_INFORMATION_EX_V = new USB_NODE_CONNECTION_INFORMATION_EX_V2(address);
				SafeFileHandle safeFileHandle2;
				SafeFileHandle safeFileHandle = (safeFileHandle2 = NativeMethods.CreateFile(hubDevicePath, 0U, 3U, IntPtr.Zero, 3U, 0U, IntPtr.Zero));
				bool flag;
				try
				{
					if (safeFileHandle.IsInvalid)
					{
						int num = Marshal.GetLastWin32Error();
						throw new FFUException(string.Format(CultureInfo.CurrentCulture, Resources.ERROR_INVALID_HANDLE, new object[] { hubDevicePath, num }));
					}
					uint num2;
					flag = NativeMethods.DeviceIoControl(safeFileHandle, NativeMethods.IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX_V2, usb_NODE_CONNECTION_INFORMATION_EX_V, (uint)Marshal.SizeOf<USB_NODE_CONNECTION_INFORMATION_EX_V2>(usb_NODE_CONNECTION_INFORMATION_EX_V), usb_NODE_CONNECTION_INFORMATION_EX_V, (uint)Marshal.SizeOf<USB_NODE_CONNECTION_INFORMATION_EX_V2>(usb_NODE_CONNECTION_INFORMATION_EX_V), out num2, IntPtr.Zero);
				}
				finally
				{
					if (safeFileHandle2 != null)
					{
						((IDisposable)safeFileHandle2).Dispose();
					}
				}
				if (!flag)
				{
					int num = Marshal.GetLastWin32Error();
					throw new FFUException(string.Format(CultureInfo.CurrentCulture, Resources.ERROR_DEVICE_IO_CONTROL, new object[] { num }));
				}
				if ((usb_NODE_CONNECTION_INFORMATION_EX_V.Flags & 1U) == 1U)
				{
					connectionType = ConnectionType.SuperSpeed3;
				}
				else
				{
					connectionType = ConnectionType.HighSpeed;
				}
			}
			catch (FFUException)
			{
				throw;
			}
			return connectionType;
		}

		// Token: 0x0400017A RID: 378
		private string UsbDevicePath;
	}
}

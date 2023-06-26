using System;
using Microsoft.Tools.Connectivity;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation.Services
{
	// Token: 0x02000008 RID: 8
	internal class IpDeviceCommunicator
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00003FAD File Offset: 0x000021AD
		public IpDeviceCommunicator(Guid id)
		{
			this.device = new RemoteDevice(id);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003FC4 File Offset: 0x000021C4
		public void Connect()
		{
			try
			{
				this.device.UserName = "UpdateUser";
				this.device.Connect();
			}
			catch (AccessDeniedException)
			{
				this.device.UserName = "SshRecoveryUser";
				this.device.Connect();
			}
			try
			{
				this.device.CreateDirectory("C:\\Data\\ProgramData\\Update");
			}
			catch
			{
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000404C File Offset: 0x0000224C
		public void Disconnect()
		{
			this.device.Disconnect();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000405C File Offset: 0x0000225C
		public string ExecuteCommand(IpDeviceCommand command, string args = null)
		{
			return command.Execute(this.device, args);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000407C File Offset: 0x0000227C
		public int ReadBatteryLevel()
		{
			string text = this.ExecuteCommand(IpDeviceCommunicator.DeviceUpdatePropertyBatteryLevel, null);
			try
			{
				return int.Parse(text);
			}
			catch
			{
				Tracer<IpDeviceCommunicator>.WriteError("Uncorrect format of battery level parameter {0}", new object[] { text });
			}
			return -1;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000040D0 File Offset: 0x000022D0
		public bool IsIpDevice()
		{
			bool flag;
			try
			{
				this.ExecuteCommand(IpDeviceCommunicator.DeviceUpdatePropertyFirmwareVersion, null);
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004108 File Offset: 0x00002308
		public bool GetDeviceProperties(ref IpDeviceCommunicator.DeviceProperties deviceProperties)
		{
			bool flag;
			try
			{
				deviceProperties.FirmwareVersion = this.ExecuteCommand(IpDeviceCommunicator.DeviceUpdatePropertyFirmwareVersion, null);
				deviceProperties.Name = this.ExecuteCommand(IpDeviceCommunicator.DeviceUpdatePropertyOemDeviceName, null);
				deviceProperties.UefiName = this.ExecuteCommand(IpDeviceCommunicator.DeviceUpdatePropertyUefiName, null);
				deviceProperties.BatteryLevel = this.ReadBatteryLevel();
				flag = true;
			}
			catch (Exception ex)
			{
				Tracer<IpDeviceCommunicator>.WriteError(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000417C File Offset: 0x0000237C
		public string GetOSVersion()
		{
			string text = this.ExecuteCommand(IpDeviceCommunicator.DeviceUpdatePropertyBuildNumber, null);
			string text2 = this.ExecuteCommand(IpDeviceCommunicator.DeviceUpdatePropertyBuildRevision, null);
			return "10.0." + text + "." + text2;
		}

		// Token: 0x04000019 RID: 25
		public const string ApplyUpdateDirectory = "C:\\Data\\ProgramData\\Update";

		// Token: 0x0400001A RID: 26
		public const string ApplyUpdateLogFile = "C:\\Data\\ProgramData\\Update\\ApplyUpdate.log";

		// Token: 0x0400001B RID: 27
		public const string UsoLogFile = "C:\\Data\\ProgramData\\USOShared\\UsoLogs.dudiag";

		// Token: 0x0400001C RID: 28
		public static readonly IpDeviceCommand DeviceUpdatePropertyFirmwareVersion = new IpDeviceDeviceUpdateUtilCommand("firmwareversion", "1");

		// Token: 0x0400001D RID: 29
		public static readonly IpDeviceCommand DeviceUpdatePropertyManufacturer = new IpDeviceDeviceUpdateUtilCommand("manufacturer", "2");

		// Token: 0x0400001E RID: 30
		public static readonly IpDeviceCommand DeviceUpdatePropertySerialNumber = new IpDeviceDeviceUpdateUtilCommand("serialnumber", "3");

		// Token: 0x0400001F RID: 31
		public static readonly IpDeviceCommand DeviceUpdatePropertyBuildBranch = new IpDeviceDeviceUpdateUtilCommand("buildbranch", "4");

		// Token: 0x04000020 RID: 32
		public static readonly IpDeviceCommand DeviceUpdatePropertyBuildNumber = new IpDeviceDeviceUpdateUtilCommand("buildnumber", "5");

		// Token: 0x04000021 RID: 33
		public static readonly IpDeviceCommand DeviceUpdatePropertyBuildTimestamp = new IpDeviceDeviceUpdateUtilCommand("buildtimestamp", "6");

		// Token: 0x04000022 RID: 34
		public static readonly IpDeviceCommand DeviceUpdatePropertyOemDeviceName = new IpDeviceDeviceUpdateUtilCommand("oemdevicename", "7");

		// Token: 0x04000023 RID: 35
		public static readonly IpDeviceCommand DeviceUpdatePropertyUefiName = new IpDeviceDeviceUpdateUtilCommand("uefiname", "8");

		// Token: 0x04000024 RID: 36
		public static readonly IpDeviceCommand DeviceUpdatePropertyBuildRevision = new IpDeviceDeviceUpdateUtilCommand("buildrevision", null);

		// Token: 0x04000025 RID: 37
		public static readonly IpDeviceCommand DeviceUpdatePropertyBatteryLevel = new IpDeviceDeviceUpdateUtilCommand("getbatterylevel", null);

		// Token: 0x04000026 RID: 38
		public static readonly IpDeviceCommand DeviceUpdateCommandRebootToUefi = new IpDeviceDeviceUpdateUtilCommand("reboottouefi", "4097");

		// Token: 0x04000027 RID: 39
		public static readonly IpDeviceCommand DeviceUpdateCommandSetTime = new IpDeviceDeviceUpdateUtilCommand("settime", "4098");

		// Token: 0x04000028 RID: 40
		public static readonly IpDeviceCommand DeviceUpdateCommandGetInstalledPackages = new IpDeviceDeviceUpdateUtilCommand("getinstalledpackages", null);

		// Token: 0x04000029 RID: 41
		private readonly RemoteDevice device;

		// Token: 0x02000015 RID: 21
		public struct DeviceProperties
		{
			// Token: 0x04000054 RID: 84
			public string FirmwareVersion;

			// Token: 0x04000055 RID: 85
			public string Name;

			// Token: 0x04000056 RID: 86
			public string UefiName;

			// Token: 0x04000057 RID: 87
			public int BatteryLevel;
		}
	}
}

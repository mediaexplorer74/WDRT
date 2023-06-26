using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using ClickerUtilityLibrary.Comm;
using ClickerUtilityLibrary.Comm.USBDriver;
using ClickerUtilityLibrary.DataModel;
using ClickerUtilityLibrary.Misc;

namespace ClickerUtilityLibrary
{
	// Token: 0x02000006 RID: 6
	public class ClickerFwUpdater : IDisposable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000021 RID: 33 RVA: 0x0000336C File Offset: 0x0000156C
		// (remove) Token: 0x06000022 RID: 34 RVA: 0x000033A4 File Offset: 0x000015A4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<FwUpdaterEventArgs> UpdaterEvent;

		// Token: 0x06000023 RID: 35 RVA: 0x000033DC File Offset: 0x000015DC
		protected virtual void OnUpdaterEvent(FwUpdaterEventArgs updaterEventArgs)
		{
			bool flag = this.UpdaterEvent != null;
			if (flag)
			{
				this.UpdaterEvent(this, updaterEventArgs);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003408 File Offset: 0x00001608
		public ClickerFwUpdater(ILogger Logger)
		{
			bool flag = Logger != null;
			if (flag)
			{
				this.mLogger = Logger;
			}
			CommandEngine.Instance.CommandEngineEvent += this.ReceivedEventHandler;
			bool flag2 = this.deviceList.Devices.Count > 0;
			if (flag2)
			{
				UsbDevice value = this.deviceList.Devices.First<KeyValuePair<string, UsbDevice>>().Value;
				CommandEngine.Instance.StartCommandEngine(BootLoaderProtocol.Instance, value);
			}
			else
			{
				CommandEngine.Instance.StartCommandEngine(BootLoaderProtocol.Instance);
			}
			this.InitializeDataElements();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000034F0 File Offset: 0x000016F0
		public ClickerFwUpdater()
			: this(null)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000034FC File Offset: 0x000016FC
		private void InitializeDataElements()
		{
			DataElement dataElement = DataElementDictionary.Instance[DataElementType.DI_FW_CONFIG_SIZE];
			dataElement.Data = 36;
			this.mFwVersion = DataElementDictionary.Instance[DataElementType.DI_FW_VER];
			this.mFwAddress = DataElementDictionary.Instance[DataElementType.DI_FW_ADDRESS];
			this.mFwSize = DataElementDictionary.Instance[DataElementType.DI_FW_SIZE];
			this.mChecksum = DataElementDictionary.Instance[DataElementType.DI_FW_CHECKSUM];
			this.mTransferOffset = DataElementDictionary.Instance[DataElementType.DI_FW_TRANSFER_OFFSET];
			this.mFwBinary = DataElementDictionary.Instance[DataElementType.DI_FW_BINARY];
			this.mFwUpdateTimestamp = DataElementDictionary.Instance[DataElementType.DI_FW_UPDATE_DATE];
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000035A0 File Offset: 0x000017A0
		public bool StartFirmwareDownload(string filename, ImageVersion version)
		{
			bool flag = this.RunBootLoader();
			bool flag2 = !flag;
			bool flag3;
			if (flag2)
			{
				this.LogError("Failed to run the boot loader.");
				flag3 = false;
			}
			else
			{
				flag = this.OpenFile(filename);
				bool flag4 = !flag;
				if (flag4)
				{
					this.LogError("Failed to open the file for firmware download.");
					flag3 = false;
				}
				else
				{
					this.LogInfo("Starting download of the application firmware.");
					this.WriteFirmwareConfiguration(version);
					bool flag5 = !this.mFlashingCompleted.WaitOne(50000);
					if (flag5)
					{
						this.LogError("Timed out waiting for firmware download to complete.");
						flag3 = false;
					}
					else
					{
						Thread.Sleep(500);
						ClickerFwUpdater.RunApplication();
						bool flag6 = !this.mDeviceConnectedToApp.WaitOne(5000);
						if (flag6)
						{
							this.LogError("Application firmware did not start.");
							flag3 = false;
						}
						else
						{
							this.LogInfo("Application firmware is now running.");
							FwUpdaterEventArgs fwUpdaterEventArgs = new FwUpdaterEventArgs
							{
								Type = FwUpdaterEventArgs.EventType.UpdateCompleted
							};
							this.OnUpdaterEvent(fwUpdaterEventArgs);
							flag3 = true;
						}
					}
				}
			}
			return flag3;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000369C File Offset: 0x0000189C
		public bool RunBootLoader()
		{
			bool flag = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToApplication;
			if (flag)
			{
				this.mDeviceConnectedToApp.Reset();
				ClickerFwUpdater.ResetDevice();
				bool flag2 = !this.mDeviceConnectedToBootLoader.WaitOne(5000);
				if (flag2)
				{
					this.LogError("Timed out waiting to connect to the boot loader.");
					return false;
				}
				ClickerFwUpdater.PingDevice();
			}
			else
			{
				this.mDeviceConnectedToBootLoader.Reset();
			}
			return true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003710 File Offset: 0x00001910
		private void LogInfo(string Value)
		{
			bool flag = this.mLogger != null;
			if (flag)
			{
				this.mLogger.LogInfo(Value);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000373C File Offset: 0x0000193C
		private void LogDbg(string Value)
		{
			bool flag = this.mLogger != null;
			if (flag)
			{
				this.mLogger.LogDebug(Value);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003768 File Offset: 0x00001968
		private void LogError(string Value)
		{
			bool flag = this.mLogger != null;
			if (flag)
			{
				this.mLogger.LogError(Value);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003794 File Offset: 0x00001994
		public static void RunApplication()
		{
			IPacket packet = BootLoaderProtocol.Instance.CreateNewPacket();
			BootLoaderProtocol.Instance.FormCommandPacket(packet, 6);
			CommandEngine.Instance.SendRawData(packet.RawPacket, packet.Length);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000037D4 File Offset: 0x000019D4
		public static void ResetDevice()
		{
			IPacket packet = AppProtocol.Instance.CreateNewPacket();
			AppProtocol.Instance.FormResetCommandPacket(packet);
			CommandEngine.Instance.SendRawData(packet.RawPacket, packet.Length);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003810 File Offset: 0x00001A10
		public static void ActivateShipMode()
		{
			IPacket packet = AppProtocol.Instance.CreateNewPacket();
			AppProtocol.Instance.FormActivateShipModeCommandPacket(packet);
			CommandEngine.Instance.SendRawData(packet.RawPacket, packet.Length);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000384C File Offset: 0x00001A4C
		public static void PingDevice()
		{
			IPacket packet = BootLoaderProtocol.Instance.CreateNewPacket();
			BootLoaderProtocol.Instance.FormCommandPacket(packet, 0);
			CommandEngine.Instance.SendRawData(packet.RawPacket, packet.Length);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000388C File Offset: 0x00001A8C
		public bool GetFirmwareVersion(out string version)
		{
			bool flag = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToBootLoader;
			if (flag)
			{
				CommandResponse.CommandSent(CommandResponse.CommandCode.FW_VER);
				IPacket packet = BootLoaderProtocol.Instance.CreateNewPacket();
				BootLoaderProtocol.Instance.FormCommandPacket(packet, 3);
				CommandEngine.Instance.SendRawData(packet.RawPacket, packet.Length);
			}
			else
			{
				bool flag2 = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToApplication;
				if (flag2)
				{
					CommandResponse.CommandSent(CommandResponse.CommandCode.FW_VER);
					IPacket packet2 = BootLoaderProtocol.Instance.CreateNewPacket();
					AppProtocol.Instance.FormGetFwVersionCommandPacket(packet2);
					CommandEngine.Instance.SendRawData(packet2.RawPacket, packet2.Length);
				}
			}
			bool flag3 = true;
			version = CommandResponse.GetResponse(CommandResponse.CommandCode.FW_VER);
			bool flag4 = version == null;
			if (flag4)
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000394C File Offset: 0x00001B4C
		public bool GetBoardId(out int id)
		{
			bool flag = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToBootLoader;
			if (flag)
			{
				CommandResponse.CommandSent(CommandResponse.CommandCode.HWID);
				IPacket packet = BootLoaderProtocol.Instance.CreateNewPacket();
				BootLoaderProtocol.Instance.FormCommandPacket(packet, 10);
				CommandEngine.Instance.SendRawData(packet.RawPacket, packet.Length);
			}
			else
			{
				bool flag2 = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToApplication;
				if (flag2)
				{
					CommandResponse.CommandSent(CommandResponse.CommandCode.HWID);
					IPacket packet2 = AppProtocol.Instance.CreateNewPacket();
					AppProtocol.Instance.FormGetBoardIdCommandPacket(packet2);
					CommandEngine.Instance.SendRawData(packet2.RawPacket, packet2.Length);
				}
			}
			bool flag3 = true;
			id = 0;
			string response = CommandResponse.GetResponse(CommandResponse.CommandCode.HWID);
			bool flag4 = response == null;
			if (flag4)
			{
				flag3 = false;
			}
			else
			{
				id = Convert.ToInt32(response, CultureInfo.InvariantCulture);
			}
			return flag3;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003A1C File Offset: 0x00001C1C
		public bool GetFirmwareUpdateTimestamp(out DateTime timestamp)
		{
			bool flag = true;
			timestamp = default(DateTime);
			bool flag2 = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToBootLoader;
			if (flag2)
			{
				CommandResponse.CommandSent(CommandResponse.CommandCode.UPDATE_DATE);
				IPacket packet = BootLoaderProtocol.Instance.CreateNewPacket();
				BootLoaderProtocol.Instance.FormCommandPacket(packet, 3);
				CommandEngine.Instance.SendRawData(packet.RawPacket, packet.Length);
			}
			else
			{
				bool flag3 = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToApplication;
				if (flag3)
				{
					return false;
				}
			}
			string response = CommandResponse.GetResponse(CommandResponse.CommandCode.UPDATE_DATE);
			bool flag4 = response == null;
			if (flag4)
			{
				flag = false;
			}
			else
			{
				uint num = Convert.ToUInt32(response, CultureInfo.InvariantCulture);
				timestamp = new DateTime(1970, 1, 1).AddSeconds(num);
			}
			return flag;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003ADC File Offset: 0x00001CDC
		public bool GetVitalProductData(out string bluetoothAddress, out string serialNumber, out int boardId)
		{
			bool flag = true;
			boardId = 0;
			bluetoothAddress = null;
			serialNumber = null;
			bool flag2 = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToBootLoader;
			if (flag2)
			{
				flag = false;
			}
			else
			{
				bool flag3 = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToApplication;
				if (flag3)
				{
					CommandResponse.CommandSent(CommandResponse.CommandCode.VPD);
					IPacket packet = BootLoaderProtocol.Instance.CreateNewPacket();
					AppProtocol.Instance.FormGetVitalProductDataCommandPacket(packet);
					CommandEngine.Instance.SendRawData(packet.RawPacket, packet.Length);
				}
				string response = CommandResponse.GetResponse(CommandResponse.CommandCode.VPD);
				bool flag4 = response == null;
				if (flag4)
				{
					flag = false;
				}
				else
				{
					string responsePattern = CommandResponse.GetResponsePattern(CommandResponse.CommandCode.VPD);
					Regex regex = new Regex(responsePattern);
					Match match = regex.Match(response);
					bool flag5 = !match.Success;
					if (flag5)
					{
						flag = false;
					}
					else
					{
						bool flag6 = match.Groups.Count != 4;
						if (flag6)
						{
							flag = false;
						}
						else
						{
							bluetoothAddress = match.Groups[1].Value;
							serialNumber = match.Groups[2].Value;
							boardId = Convert.ToInt32(match.Groups[3].Value, CultureInfo.InvariantCulture);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003C08 File Offset: 0x00001E08
		public bool GetFirmwareInformation(out string configurationType, out string releaseType, out string siliconType)
		{
			bool flag = true;
			configurationType = null;
			releaseType = null;
			siliconType = null;
			bool flag2 = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToBootLoader;
			if (flag2)
			{
				flag = false;
			}
			else
			{
				bool flag3 = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToApplication;
				if (flag3)
				{
					CommandResponse.CommandSent(CommandResponse.CommandCode.FW_CFG);
					IPacket packet = BootLoaderProtocol.Instance.CreateNewPacket();
					AppProtocol.Instance.FormGetFirmwareInformationPacket(packet);
					CommandEngine.Instance.SendRawData(packet.RawPacket, packet.Length);
				}
				string response = CommandResponse.GetResponse(CommandResponse.CommandCode.FW_CFG);
				bool flag4 = response == null;
				if (flag4)
				{
					flag = false;
				}
				else
				{
					string responsePattern = CommandResponse.GetResponsePattern(CommandResponse.CommandCode.FW_CFG);
					Regex regex = new Regex(responsePattern);
					Match match = regex.Match(response);
					bool flag5 = !match.Success;
					if (flag5)
					{
						flag = false;
					}
					else
					{
						bool flag6 = match.Groups.Count != 4;
						if (flag6)
						{
							flag = false;
						}
						else
						{
							configurationType = match.Groups[1].Value;
							releaseType = match.Groups[2].Value;
							siliconType = match.Groups[3].Value;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003D28 File Offset: 0x00001F28
		private void WriteFirmwareConfiguration(ImageVersion version)
		{
			try
			{
				IPacket packet = BootLoaderProtocol.Instance.CreateNewPacket();
				string text = string.Concat(new object[]
				{
					"Sending ",
					0,
					" / ",
					this.mFileStream.Length.ToString(CultureInfo.InvariantCulture)
				});
				this.LogDbg(text);
				bool flag = (uint)this.mFwAddress.Data != 131072U;
				if (flag)
				{
					this.mFwAddress.Data = 131072U;
				}
				this.mFwVersion.Data = version.ToInt32();
				this.mFwUpdateTimestamp.Data = (uint)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
				BootLoaderProtocol.Instance.FormCommandPacket(packet, 2);
				CommandEngine.Instance.SendRawData(packet.RawPacket, packet.Length);
				this.mFileStream.Position = 0L;
			}
			catch (Exception ex)
			{
				this.LogError(ex.Message);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003E70 File Offset: 0x00002070
		private bool OpenFile(string FileName)
		{
			try
			{
				this.mFileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
				bool flag = this.mFileStream.Length > (long)this.mBuffer.Length;
				if (flag)
				{
					return false;
				}
				this.mFwSize.Data = (uint)this.mFileStream.Length;
				this.mFileStream.Read(this.mBuffer, 0, (int)this.mFileStream.Length);
				this.mChecksum.Data = BootLoaderProtocol.Instance.CalculateChecksum(this.mBuffer, 0, (int)this.mFileStream.Length);
				this.mFileStream.Position = 0L;
				Array.Clear(this.mBuffer, 0, 1048576);
			}
			catch (Exception ex)
			{
				this.LogError(ex.Message);
			}
			return true;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003F64 File Offset: 0x00002164
		private void ReceivedEventHandler(object sender, FEvent receivedEventArgs)
		{
			FwUpdaterEventArgs fwUpdaterEventArgs = new FwUpdaterEventArgs();
			switch (receivedEventArgs.EventType)
			{
			case EventType.ExceptionMessage:
			{
				string text = DateTime.Now.ToString("HH:mm:ss: Error - ", CultureInfo.InvariantCulture) + receivedEventArgs.StringParameter;
				this.LogDbg(text);
				receivedEventArgs.StringParameter = "";
				break;
			}
			case EventType.OperationMessage:
			{
				string text = DateTime.Now.ToString("HH:mm:ss:\t", CultureInfo.InvariantCulture) + receivedEventArgs.StringParameter;
				this.LogDbg(text);
				LogManager.Instance.Log(receivedEventArgs.StringParameter);
				receivedEventArgs.StringParameter = "";
				break;
			}
			case EventType.PacketReceived:
			{
				FPacket fpacket = receivedEventArgs.ObjectParameter as FPacket;
				bool flag = fpacket == null;
				if (!flag)
				{
					bool flag2 = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToBootLoader;
					if (flag2)
					{
						this.ProcessBootLoaderPacket(fpacket);
					}
					else
					{
						bool flag3 = this.deviceStatus == ClickerFwUpdater.DeviceStatus.ConnectedToApplication;
						if (flag3)
						{
							this.ProcessApplicationPacket(fpacket);
						}
					}
				}
				break;
			}
			case EventType.UsbDeviceConnected:
			{
				string text2 = receivedEventArgs.ObjectParameter as string;
				bool flag4 = text2 != null && ClickerFwUpdater.IsBootLoaderUsbFriendlyName(text2);
				if (flag4)
				{
					this.deviceStatus = ClickerFwUpdater.DeviceStatus.ConnectedToBootLoader;
					CommandEngine.Instance.ChangeProtocol(BootLoaderProtocol.Instance);
					this.mDeviceConnectedToBootLoader.Set();
					string text = "Device is connected.(BL)";
					this.LogInfo(text);
					fwUpdaterEventArgs.Type = FwUpdaterEventArgs.EventType.ConnectedToBootLoader;
					this.OnUpdaterEvent(fwUpdaterEventArgs);
				}
				else
				{
					this.deviceStatus = ClickerFwUpdater.DeviceStatus.ConnectedToApplication;
					CommandEngine.Instance.ChangeProtocol(AppProtocol.Instance);
					this.mDeviceConnectedToApp.Set();
					string text = "Device is connected.(App)";
					this.LogInfo(text);
					fwUpdaterEventArgs.Type = FwUpdaterEventArgs.EventType.ConnectedToApplication;
					this.OnUpdaterEvent(fwUpdaterEventArgs);
				}
				break;
			}
			case EventType.UsbDeviceDisconnected:
			{
				this.deviceStatus = ClickerFwUpdater.DeviceStatus.Disconnected;
				string text = "Device is disconnected!";
				this.LogInfo(text);
				fwUpdaterEventArgs.Type = FwUpdaterEventArgs.EventType.DeviceDisconnected;
				this.OnUpdaterEvent(fwUpdaterEventArgs);
				break;
			}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004154 File Offset: 0x00002354
		public static bool IsBootLoaderUsbFriendlyName(string usbFriendlyName)
		{
			string[] array = new string[] { "FFU", "Recovery" };
			foreach (string text in array)
			{
				bool flag = usbFriendlyName.Contains(text) && !usbFriendlyName.Contains("Updater");
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000041C0 File Offset: 0x000023C0
		public static bool IsBootLoaderUpdaterUsbFriendlyName(string usbFriendlyName)
		{
			string[] array = new string[] { "Updater" };
			return array.Any((string name) => usbFriendlyName.Contains(name));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004200 File Offset: 0x00002400
		private void ProcessBootLoaderPacket(FPacket packet)
		{
			FwUpdaterEventArgs fwUpdaterEventArgs = new FwUpdaterEventArgs();
			try
			{
				bool flag = packet.Command == 2;
				if (flag)
				{
					IPacket packet2 = BootLoaderProtocol.Instance.CreateNewPacket();
					bool flag2 = this.mFileStream.Length < 4096L;
					if (flag2)
					{
						this.mFwBinary.Length = (int)((ushort)this.mFileStream.Length);
						this.mIsEOF = true;
					}
					else
					{
						this.mFwBinary.Length = 4096;
						this.mIsEOF = false;
					}
					this.mTransferOffset.Data = (long)((ulong)((uint)this.mFwAddress.Data) + (ulong)this.mFileStream.Position);
					this.mFileStream.Read(this.mFwBinary.GetRawData(), 0, this.mFwBinary.Length);
					BootLoaderProtocol.Instance.FormDataPacket(packet2);
					CommandEngine.Instance.SendRawData(packet2.RawPacket, packet2.Length);
					string text = "Sending " + this.mFileStream.Position.ToString(CultureInfo.InvariantCulture) + " / " + this.mFileStream.Length.ToString(CultureInfo.InvariantCulture);
					this.LogDbg(text);
					fwUpdaterEventArgs.Type = FwUpdaterEventArgs.EventType.UpdateProgress;
					fwUpdaterEventArgs.Parameters = (double)this.mFileStream.Position / (double)this.mFileStream.Length;
					this.OnUpdaterEvent(fwUpdaterEventArgs);
				}
				else
				{
					bool flag3 = packet.Command == 4;
					if (flag3)
					{
						IPacket packet3 = BootLoaderProtocol.Instance.CreateNewPacket();
						FCommand fcommand = CommandDictionary.Instance[4];
						bool flag4 = this.mFileStream == null;
						if (flag4)
						{
							bool flag5 = this.mLastPacketSent;
							if (flag5)
							{
								this.mFlashingCompleted.Set();
							}
						}
						else
						{
							bool flag6 = (uint)fcommand.ResponseArgs[0].Data > 0U;
							if (flag6)
							{
								this.LogError("Flash Error!");
							}
							else
							{
								bool flag7 = this.mIsEOF;
								if (flag7)
								{
									this.mFileStream.Close();
									this.mFileStream = null;
									this.mTransferOffset.Data = 0;
									this.mFwBinary.Length = 0;
									BootLoaderProtocol.Instance.FormDataPacket(packet3);
									CommandEngine.Instance.SendRawData(packet3.RawPacket, packet3.Length);
									this.mIsEOF = false;
									this.mLastPacketSent = true;
									this.LogInfo("Firmware is successfully flashed!");
								}
								else
								{
									bool flag8 = this.mFileStream.Length - this.mFileStream.Position <= 4096L;
									if (flag8)
									{
										this.mFwBinary.Length = (int)((ushort)(this.mFileStream.Length - this.mFileStream.Position));
										this.mIsEOF = true;
									}
									else
									{
										this.mFwBinary.Length = 4096;
										this.mIsEOF = false;
									}
									bool flag9 = this.mFwBinary.Length != 0;
									if (flag9)
									{
										this.mTransferOffset.Data = (long)((ulong)((uint)this.mFwAddress.Data) + (ulong)this.mFileStream.Position);
										this.mFileStream.Read(this.mFwBinary.GetRawData(), 0, this.mFwBinary.Length);
										BootLoaderProtocol.Instance.FormDataPacket(packet3);
										CommandEngine.Instance.SendRawData(packet3.RawPacket, packet3.Length);
									}
									string text = string.Concat(new object[]
									{
										"Sending ",
										this.mFileStream.Position,
										" / ",
										this.mFileStream.Length
									});
									this.LogDbg(text);
									fwUpdaterEventArgs.Type = FwUpdaterEventArgs.EventType.UpdateProgress;
									fwUpdaterEventArgs.Parameters = (double)this.mFileStream.Position / (double)this.mFileStream.Length;
									this.OnUpdaterEvent(fwUpdaterEventArgs);
								}
							}
						}
					}
					else
					{
						bool flag10 = packet.Command == 6;
						if (flag10)
						{
							FCommand fcommand2 = CommandDictionary.Instance[6];
							bool flag11 = (uint)fcommand2.ResponseArgs[0].Data > 0U;
							if (flag11)
							{
								this.LogError("Couldn't run application.");
							}
						}
						else
						{
							bool flag12 = packet.Command == 3;
							if (flag12)
							{
								DataElement dataElement = DataElementDictionary.Instance[DataElementType.DI_FW_VER];
								int num = Convert.ToInt32(dataElement.Data, CultureInfo.InvariantCulture);
								ImageVersion imageVersion = new ImageVersion(num);
								CommandResponse.UpdateResponseData(CommandResponse.CommandCode.FW_VER, imageVersion.ToString());
								DataElement dataElement2 = DataElementDictionary.Instance[DataElementType.DI_FW_UPDATE_DATE];
								CommandResponse.UpdateResponseData(CommandResponse.CommandCode.UPDATE_DATE, Convert.ToInt32(dataElement2.Data, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture));
							}
							else
							{
								bool flag13 = packet.Command == 10;
								if (flag13)
								{
									DataElement dataElement3 = DataElementDictionary.Instance[DataElementType.DI_FW_HWID];
									string text2 = Convert.ToInt32(dataElement3.Data, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
									CommandResponse.UpdateResponseData(CommandResponse.CommandCode.HWID, text2);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.LogError(ex.Message);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000475C File Offset: 0x0000295C
		private void ProcessApplicationPacket(FPacket packet)
		{
			string bodyAsString = AppProtocol.Instance.GetBodyAsString(packet);
			CommandResponse.ProcessCommandResponses(bodyAsString);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004780 File Offset: 0x00002980
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				CommandEngine.Instance.CommandEngineEvent -= this.ReceivedEventHandler;
				bool flag = this.mFileStream != null;
				if (flag)
				{
					this.mFileStream.Dispose();
				}
				bool flag2 = this.mDeviceConnectedToApp != null;
				if (flag2)
				{
					this.mDeviceConnectedToApp.Dispose();
				}
				bool flag3 = this.mDeviceConnectedToBootLoader != null;
				if (flag3)
				{
					this.mDeviceConnectedToBootLoader.Dispose();
				}
				bool flag4 = this.mFlashingCompleted != null;
				if (flag4)
				{
					this.mFlashingCompleted.Dispose();
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000481D File Offset: 0x00002A1D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000020 RID: 32
		private const ushort MAXIMUM_BIN_PARTITION = 4096;

		// Token: 0x04000021 RID: 33
		private const uint DEFAULT_FW_ADDRESS = 131072U;

		// Token: 0x04000022 RID: 34
		private const uint MAXIMUMMEMORYSIZE = 1048576U;

		// Token: 0x04000023 RID: 35
		private readonly UsbDevices deviceList = new UsbDevices();

		// Token: 0x04000024 RID: 36
		private ClickerFwUpdater.DeviceStatus deviceStatus = ClickerFwUpdater.DeviceStatus.Disconnected;

		// Token: 0x04000025 RID: 37
		private readonly AutoResetEvent mDeviceConnectedToBootLoader = new AutoResetEvent(false);

		// Token: 0x04000026 RID: 38
		private readonly AutoResetEvent mFlashingCompleted = new AutoResetEvent(false);

		// Token: 0x04000027 RID: 39
		private readonly AutoResetEvent mDeviceConnectedToApp = new AutoResetEvent(false);

		// Token: 0x04000028 RID: 40
		private FileStream mFileStream;

		// Token: 0x04000029 RID: 41
		private bool mIsEOF = true;

		// Token: 0x0400002A RID: 42
		private DataElement mTransferOffset;

		// Token: 0x0400002B RID: 43
		private DataElement mFwBinary;

		// Token: 0x0400002C RID: 44
		private DataElement mFwAddress;

		// Token: 0x0400002D RID: 45
		private DataElement mFwSize;

		// Token: 0x0400002E RID: 46
		private DataElement mChecksum;

		// Token: 0x0400002F RID: 47
		private DataElement mFwUpdateTimestamp;

		// Token: 0x04000030 RID: 48
		private DataElement mFwVersion;

		// Token: 0x04000031 RID: 49
		private readonly byte[] mBuffer = new byte[1048576];

		// Token: 0x04000032 RID: 50
		private bool mLastPacketSent;

		// Token: 0x04000033 RID: 51
		private readonly ILogger mLogger;

		// Token: 0x02000042 RID: 66
		private enum DeviceStatus
		{
			// Token: 0x04000182 RID: 386
			Disconnected,
			// Token: 0x04000183 RID: 387
			ConnectedToBootLoader,
			// Token: 0x04000184 RID: 388
			ConnectedToApplication
		}
	}
}

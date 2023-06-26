using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Services
{
	// Token: 0x02000009 RID: 9
	[Export]
	public class Thor2Service : IDisposable
	{
		// Token: 0x06000068 RID: 104 RVA: 0x0000504F File Offset: 0x0000324F
		[ImportingConstructor]
		public Thor2Service(ProcessManager processManager)
		{
			this.processManager = processManager;
			this.processManager.OnOutputDataReceived += this.Thor2ProcessOnOutputDataReceived;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000069 RID: 105 RVA: 0x00005078 File Offset: 0x00003278
		// (remove) Token: 0x0600006A RID: 106 RVA: 0x000050B0 File Offset: 0x000032B0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ProgressChangedEventArgs> ProgressChanged;

		// Token: 0x0600006B RID: 107 RVA: 0x000050E5 File Offset: 0x000032E5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000050F8 File Offset: 0x000032F8
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				this.disposed = true;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000511C File Offset: 0x0000331C
		public void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<Thor2Service>.LogEntry("FlashDevice");
			int num = 2;
			this.BootPhoneToUefiMode(phone, cancellationToken);
			Thor2ExitCode thor2ExitCode;
			for (;;)
			{
				this.RaiseProgressChangedEvent(0, "FlashingMessageCheckingAntiTheftStatus");
				thor2ExitCode = this.CheckResetProtectionStatus(phone, cancellationToken);
				bool flag = this.IsPhoneNotRespondingThor2ExitCode(thor2ExitCode);
				if (!flag)
				{
					goto IL_55;
				}
				bool flag2 = num > 0;
				if (!flag2)
				{
					this.RestartPhoneAndThrowResetProtectionException(phone, this.GetThor2ErrorDesciption(thor2ExitCode));
					goto IL_55;
				}
				IL_6C:
				if (!this.IsPhoneNotRespondingThor2ExitCode(thor2ExitCode) || num-- <= 0)
				{
					break;
				}
				continue;
				IL_55:
				this.RaiseProgressChangedEvent(0, "FlashingMessageConnectingWithPhone");
				thor2ExitCode = this.RunFlashProcess(phone, cancellationToken);
				goto IL_6C;
			}
			this.TryThrowThor2FlashException(thor2ExitCode);
			Tracer<Thor2Service>.LogExit("FlashDevice");
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000051C2 File Offset: 0x000033C2
		public void EmergencyFlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<Thor2Service>.LogEntry("EmergencyFlashDevice");
			this.RaiseProgressChangedEvent(0, "EmergencyFlashingMessage");
			this.RunEmergencyFlashProcess(phone, cancellationToken);
			Tracer<Thor2Service>.LogExit("EmergencyFlashDevice");
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000051F4 File Offset: 0x000033F4
		private Thor2ExitCode CheckResetProtectionStatus(Phone phone, CancellationToken cancellationToken)
		{
			VplPackageFileInfo vplPackageFileInfo = phone.PackageFileInfo as VplPackageFileInfo;
			bool flag = vplPackageFileInfo == null || string.IsNullOrWhiteSpace(vplPackageFileInfo.FfuFilePath);
			if (flag)
			{
				throw new CheckResetProtectionException("Can not initialize Reading Reset Protection Status");
			}
			this.antiTheftVerPhone = (this.antiTheftVerFfu = null);
			Thor2ExitCode thor2ExitCode = this.ReadAntiTheftVersionFromPhone(phone, cancellationToken);
			bool flag2 = thor2ExitCode == (Thor2ExitCode)3221225477U;
			if (flag2)
			{
				bool flag3 = this.lastRawMessageReceived != null;
				if (flag3)
				{
					long num = long.Parse(this.lastRawMessageReceived.Substring(14, 4), NumberStyles.HexNumber);
					num += (long)((ulong)(-100663296));
					Thor2ExitCode thor2ExitCode2 = (Thor2ExitCode)num;
					bool flag4 = Enum.IsDefined(typeof(Thor2ExitCode), thor2ExitCode2);
					if (flag4)
					{
						thor2ExitCode = thor2ExitCode2;
					}
				}
			}
			bool flag5 = thor2ExitCode == Thor2ExitCode.Thor2ErrorUefiInvalidParameter || thor2ExitCode == (Thor2ExitCode)4194304011U || this.IsPhoneNotRespondingThor2ExitCode(thor2ExitCode);
			Thor2ExitCode thor2ExitCode3;
			if (flag5)
			{
				thor2ExitCode3 = thor2ExitCode;
			}
			else
			{
				bool flag6 = thor2ExitCode > Thor2ExitCode.Thor2AllOk;
				if (flag6)
				{
					this.RestartPhoneAndThrowResetProtectionException(phone, this.GetThor2ErrorDesciption(thor2ExitCode));
				}
				Thor2ExitCode thor2ExitCode4 = this.ReadAntiTheftVersionFromFfu(vplPackageFileInfo.FfuFilePath, cancellationToken);
				bool flag7 = thor2ExitCode4 == Thor2ExitCode.Thor2NotResponding;
				if (flag7)
				{
					thor2ExitCode3 = thor2ExitCode;
				}
				else
				{
					bool flag8 = thor2ExitCode4 > Thor2ExitCode.Thor2AllOk;
					if (flag8)
					{
						this.RestartPhoneAndThrowResetProtectionException(phone, this.GetThor2ErrorDesciption(thor2ExitCode4));
					}
					bool flag9 = string.IsNullOrWhiteSpace(this.antiTheftVerFfu) || string.IsNullOrWhiteSpace(this.antiTheftVerPhone);
					if (flag9)
					{
						this.RestartPhoneAndThrowResetProtectionException(phone, string.Format("Reading Reset Protection status Failed. phone: {0}, ffu: {1}", this.antiTheftVerPhone, this.antiTheftVerFfu));
					}
					bool flag10 = VersionComparer.CompareVersions(this.antiTheftVerPhone, this.antiTheftVerFfu) > 0;
					if (flag10)
					{
						this.RestartPhoneAndThrowResetProtectionException(phone, string.Format("Reset Protection status Invalid. phone: {0}, ffu: {1}", this.antiTheftVerPhone, this.antiTheftVerFfu));
					}
					thor2ExitCode3 = Thor2ExitCode.Thor2AllOk;
				}
			}
			return thor2ExitCode3;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000053BC File Offset: 0x000035BC
		private bool IsPhoneNotRespondingThor2ExitCode(Thor2ExitCode phoneExitCode)
		{
			return phoneExitCode == Thor2ExitCode.Thor2NotResponding || phoneExitCode == Thor2ExitCode.Thor2ErrorConnectionNotFound || phoneExitCode == Thor2ExitCode.Thor2ErrorNoDeviceWithinTimeout || phoneExitCode == Thor2ExitCode.Thor2ErrorBootToFlashAppFailed;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000053EE File Offset: 0x000035EE
		private void RestartPhoneAndThrowResetProtectionException(Phone phone, string exceptionMessage)
		{
			this.RestartPhoneToNormalMode(phone);
			throw new CheckResetProtectionException(exceptionMessage);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005400 File Offset: 0x00003600
		private string GetThor2ErrorDesciption(Thor2ExitCode exitCode)
		{
			string text = string.Format("0x{0:X8}", (int)exitCode);
			return string.Format("Reset protection status reading failed with error {0}", string.Format("{0} ({1})", text, exitCode));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005440 File Offset: 0x00003640
		private void BootPhoneToUefiMode(Phone phone, CancellationToken cancellationToken)
		{
			this.RaiseProgressChangedEvent(0, "FlashingMessageChangeToFlashMode");
			string text = string.Format("-mode rnd -bootflashapp -conn {0}", phone.PortId);
			int num = 2;
			Thor2ExitCode thor2ExitCode;
			do
			{
				thor2ExitCode = this.processManager.RunThor2ProcessWithArguments(text, cancellationToken);
			}
			while (thor2ExitCode != Thor2ExitCode.Thor2AllOk && num-- > 0);
			bool flag = thor2ExitCode > Thor2ExitCode.Thor2AllOk;
			if (flag)
			{
				throw new FlashModeChangeException("Can not change device to flash mode.");
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000054A8 File Offset: 0x000036A8
		private void RestartPhoneToNormalMode(Phone phone)
		{
			Tracer<Thor2Service>.LogEntry("RestartPhoneToNormalMode");
			string text = string.Format("-mode rnd -reboot -conn {0}", phone.PortId);
			this.processManager.RunThor2ProcessWithArguments(text, CancellationToken.None);
			Tracer<Thor2Service>.LogExit("RestartPhoneToNormalMode");
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000054F0 File Offset: 0x000036F0
		private Thor2ExitCode ReadAntiTheftVersionFromPhone(Phone phone, CancellationToken cancellationToken)
		{
			string text = string.Format("-mode rnd -read_reset_protection_status -skip_gpt_check -skip_com_scan -disable_stdout_buffering -conn {0}", phone.PortId);
			return this.processManager.RunThor2ProcessWithArguments(text, cancellationToken);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005520 File Offset: 0x00003720
		private Thor2ExitCode ReadAntiTheftVersionFromFfu(string ffuFilePath, CancellationToken cancellationToken)
		{
			string text = string.Format("-mode ffureader -ffufile \"{0}\" -read_antitheft_version -disable_stdout_buffering", ffuFilePath);
			return this.processManager.RunThor2ProcessWithArguments(text, cancellationToken);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000554C File Offset: 0x0000374C
		private Thor2ExitCode RunFlashProcess(Phone phone, CancellationToken cancellationToken)
		{
			string text = string.Format("-mode vpl -vplfile \"{0}\" -skip_exit_on_post_op_failure -disable_stdout_buffering -conn \"{1}\" -maxtransfersizekb 128 -reboot", phone.PackageFilePath, phone.PortId);
			return this.processManager.RunThor2ProcessWithArguments(text, cancellationToken);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005584 File Offset: 0x00003784
		private void RunEmergencyFlashProcess(Phone phone, CancellationToken cancellationToken)
		{
			EmergencyPackageInfo emergencyPackageFileInfo = phone.EmergencyPackageFileInfo;
			bool flag = emergencyPackageFileInfo == null;
			if (flag)
			{
				throw new Exception("Can not initialize Emergency Flashing process");
			}
			bool flag2 = emergencyPackageFileInfo.IsAlphaCollins();
			VplPackageFileInfo vplPackageFileInfo = phone.PackageFileInfo as VplPackageFileInfo;
			bool flag3 = flag2 && (vplPackageFileInfo == null || string.IsNullOrWhiteSpace(vplPackageFileInfo.FfuFilePath));
			if (flag3)
			{
				throw new Exception("Can not initialize Emergency Flashing process");
			}
			bool flag4 = flag2;
			if (flag4)
			{
				this.RunEmergencyProcessForAlphaCollins(emergencyPackageFileInfo.HexFlasherFilePath, emergencyPackageFileInfo.MbnImageFilePath, vplPackageFileInfo.FfuFilePath, phone.PortId, cancellationToken);
			}
			else
			{
				this.RunEmergencyProcessForQuattro(phone.PortId, emergencyPackageFileInfo, cancellationToken);
			}
			Thread.Sleep(5000);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005630 File Offset: 0x00003830
		private void RunEmergencyProcessForQuattro(string portId, EmergencyPackageInfo emergencyPackageInfo, CancellationToken cancellationToken)
		{
			string text = string.Format("-mode emergency -conn \"{0}\" -hexfile \"{1}\" -edfile \"{2}\" -skipffuflash -disable_stdout_buffering", portId, emergencyPackageInfo.HexFlasherFilePath, emergencyPackageInfo.EdpImageFilePath);
			Thor2ExitCode thor2ExitCode = this.processManager.RunThor2ProcessWithArguments(text, cancellationToken);
			bool flag = Enum.IsDefined(typeof(Thor2EmergencyV3ExitCodes), (uint)thor2ExitCode);
			if (flag)
			{
				Thor2EmergencyV3ExitCodes thor2EmergencyV3ExitCodes = (Thor2EmergencyV3ExitCodes)thor2ExitCode;
				string text2 = string.Format("0x{0:X8}", (int)thor2ExitCode);
				string text3 = string.Format("Flashing failed with error {0}", string.Format("{0} ({1})", text2, thor2EmergencyV3ExitCodes));
				throw new FlashException(text3);
			}
			this.TryThrowThor2FlashException(thor2ExitCode);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000056C0 File Offset: 0x000038C0
		private void RunEmergencyProcessForAlphaCollins(string hexFlasherFilePath, string mbnImageFilePath, string ffuFilePath, string portId, CancellationToken cancellationToken)
		{
			string text = string.Format("-mode emergency -hexfile \"{0}\" -mbnfile \"{1}\" -ffufile \"{2}\" -conn \"{3}\" -skipffuflash -disable_stdout_buffering", new object[] { hexFlasherFilePath, mbnImageFilePath, ffuFilePath, portId });
			Thor2ExitCode thor2ExitCode = this.processManager.RunThor2ProcessWithArguments(text, cancellationToken);
			bool flag = Enum.IsDefined(typeof(Thor2EmergencyV1ExitCodes), (uint)thor2ExitCode);
			if (flag)
			{
				Thor2EmergencyV1ExitCodes thor2EmergencyV1ExitCodes = (Thor2EmergencyV1ExitCodes)thor2ExitCode;
				string text2 = string.Format("0x{0:X8}", (int)thor2ExitCode);
				string text3 = string.Format("Flashing failed with error {0}", string.Format("{0} ({1})", text2, thor2EmergencyV1ExitCodes));
				throw new FlashException(text3);
			}
			this.TryThrowThor2FlashException(thor2ExitCode);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000575C File Offset: 0x0000395C
		public void TryReadMissingInfoWithThor(Phone phone, CancellationToken token, bool raiseErrors = false)
		{
			Tracer<Thor2Service>.LogEntry("TryReadMissingInfoWithThor");
			this.deviceToFindInfo = phone;
			string text = string.Format("-mode rnd -readphoneinfo -conn \"{0}\" -disable_stdout_buffering", phone.PortId);
			Thor2ExitCode thor2ExitCode = this.processManager.RunThor2ProcessWithArguments(text, token);
			bool flag = raiseErrors && thor2ExitCode > Thor2ExitCode.Thor2AllOk;
			if (flag)
			{
				string text2 = string.Format("0x{0:X8}", (int)thor2ExitCode);
				string text3 = string.Format("Reading phone info failed with error {0}", string.Format("{0} ({1})", text2, thor2ExitCode));
				throw new ReadPhoneInformationException(text3);
			}
			this.deviceToFindInfo = null;
			Tracer<Thor2Service>.LogExit("TryReadMissingInfoWithThor");
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000057F4 File Offset: 0x000039F4
		public void RestartToNormalMode(Phone phone, CancellationToken token)
		{
			Tracer<Thor2Service>.LogEntry("RestartToNormalMode");
			string text = string.Format("-mode rnd -bootnormalmode -conn \"{0}\" -disable_stdout_buffering", phone.PortId);
			this.processManager.RunThor2ProcessWithArguments(text, token);
			Tracer<Thor2Service>.LogExit("RestartToNormalMode");
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005838 File Offset: 0x00003A38
		public bool IsDeviceConnected(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<Thor2Service>.LogEntry("IsDeviceConnected");
			this.deviceToFindInfo = phone;
			this.connectionEnumerationStarted = false;
			this.connectionList = new List<string>();
			this.processManager.RunThor2ProcessWithArguments("-mode list_connections -disable_stdout_buffering", cancellationToken);
			this.deviceToFindInfo = null;
			Tracer<Thor2Service>.LogExit("IsDeviceConnected");
			return this.connectionList.Any((string conn) => conn.Contains(phone.PortId));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000058BC File Offset: 0x00003ABC
		private void TryThrowThor2FlashException(Thor2ExitCode flashProcessExitCode)
		{
			bool flag = flashProcessExitCode == Thor2ExitCode.Thor2ErrorNoDeviceWithinTimeout || flashProcessExitCode == Thor2ExitCode.Thor2NotResponding || flashProcessExitCode == Thor2ExitCode.Thor2ErrorConnectionNotFound;
			if (flag)
			{
				Tracer<Thor2Service>.WriteInformation("No Device connected");
				throw new NoDeviceException(string.Format("Flashing failed with error {0}", "No device connected within the Timeout"));
			}
			string text = string.Format("0x{0:X8}", (int)flashProcessExitCode);
			string text2 = string.Format("Flashing failed with error {0}", string.Format("{0} ({1})", text, flashProcessExitCode));
			bool flag2 = text == "0xFA001106";
			if (flag2)
			{
				text2 = string.Format("Flashing failed with error {0}", string.Format("{0} ({1})", "0xFA001106", "SoftwareNotCorrectlySigned"));
				throw new SoftwareIsNotCorrectlySignedException(text2);
			}
			Thor2ErrorType errorType = Thor2Error.GetErrorType(flashProcessExitCode);
			bool flag3 = errorType > Thor2ErrorType.NoError;
			if (flag3)
			{
				Tracer<Thor2Service>.WriteInformation(text2);
				throw new FlashException(text2);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000598C File Offset: 0x00003B8C
		private void Thor2ProcessOnOutputDataReceived(DataReceivedEventArgs dataReceivedEventArgs)
		{
			try
			{
				string text = dataReceivedEventArgs.Data;
				bool flag = string.IsNullOrEmpty(text);
				if (!flag)
				{
					text = this.EscapeSpecialChars(text);
					bool flag2 = false;
					Tracer<Thor2Service>.WriteInformation(text);
					bool flag3 = string.IsNullOrEmpty(text);
					if (!flag3)
					{
						bool flag4 = text.Contains("Percents");
						if (flag4)
						{
							this.lastProgressPercentage = int.Parse(text.Substring(9));
							flag2 = true;
						}
						else
						{
							bool flag5 = text.StartsWith("[THOR2_flash_state]");
							if (flag5)
							{
								this.lastProgressMessage = "FlashingMessageInstallingSoftware";
								flag2 = true;
							}
						}
						bool flag6 = flag2;
						if (flag6)
						{
							this.RaiseProgressChangedEvent();
						}
						bool flag7 = text.ToLower().Contains("connection list start");
						if (flag7)
						{
							this.connectionEnumerationStarted = true;
						}
						else
						{
							bool flag8 = text.ToLower().Contains("connection list end");
							if (flag8)
							{
								this.connectionEnumerationStarted = false;
							}
							else
							{
								bool flag9 = this.connectionEnumerationStarted;
								if (flag9)
								{
									this.connectionList.Add(text);
								}
							}
						}
						bool flag10 = text.Contains("TYPE:");
						if (flag10)
						{
							this.deviceToFindInfo.HardwareModel = text.Remove(0, 5).Trim();
						}
						else
						{
							bool flag11 = text.Contains("CTR:");
							if (flag11)
							{
								this.deviceToFindInfo.HardwareVariant = text.Remove(0, 4).Trim();
							}
							else
							{
								bool flag12 = text.Contains("IMEI:");
								if (flag12)
								{
									this.deviceToFindInfo.Imei = text.Remove(0, 5).Trim();
								}
							}
						}
						bool flag13 = text.Contains("Reset Protection version:");
						if (flag13)
						{
							this.antiTheftVerPhone = text.Substring(25).Trim();
						}
						else
						{
							bool flag14 = text.Contains("Antitheft version:");
							if (flag14)
							{
								this.antiTheftVerFfu = text.Substring(18).Trim();
							}
						}
						bool flag15 = text.ToLower().Contains("received raw message");
						if (flag15)
						{
							this.lastRawMessageReceived = text.Substring(22).Trim();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Tracer<Thor2Service>.WriteWarning("Error parsing Thor2 output. Unable to parse string: {0}, exception {1}", new object[] { dataReceivedEventArgs.Data, ex });
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005BD0 File Offset: 0x00003DD0
		private string EscapeSpecialChars(string sourceString)
		{
			bool flag = string.IsNullOrWhiteSpace(sourceString);
			string text;
			if (flag)
			{
				text = sourceString;
			}
			else
			{
				text = sourceString.Replace("{", "{{").Replace("}", "}}");
			}
			return text;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005C10 File Offset: 0x00003E10
		private void RaiseProgressChangedEvent(int progress, string progressMessage)
		{
			this.lastProgressPercentage = progress;
			this.lastProgressMessage = progressMessage;
			this.RaiseProgressChangedEvent();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005C28 File Offset: 0x00003E28
		private void RaiseProgressChangedEvent()
		{
			Action<ProgressChangedEventArgs> progressChanged = this.ProgressChanged;
			bool flag = progressChanged != null;
			if (flag)
			{
				progressChanged(new ProgressChangedEventArgs(this.lastProgressPercentage, this.lastProgressMessage));
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005C5F File Offset: 0x00003E5F
		internal void ReleaseManagedObjects()
		{
			this.processManager.ReleaseManagedObjects();
		}

		// Token: 0x04000029 RID: 41
		private const string FlashingErrorMessage = "Flashing failed with error {0}";

		// Token: 0x0400002A RID: 42
		private const string ResetProtectionErrorMessage = "Reset protection status reading failed with error {0}";

		// Token: 0x0400002B RID: 43
		private const string ReadingInfoErrorMessage = "Reading phone info failed with error {0}";

		// Token: 0x0400002C RID: 44
		private readonly ProcessManager processManager;

		// Token: 0x0400002D RID: 45
		private Phone deviceToFindInfo;

		// Token: 0x0400002E RID: 46
		private List<string> connectionList;

		// Token: 0x0400002F RID: 47
		private int lastProgressPercentage;

		// Token: 0x04000030 RID: 48
		private bool disposed;

		// Token: 0x04000031 RID: 49
		private bool connectionEnumerationStarted;

		// Token: 0x04000032 RID: 50
		private string lastProgressMessage;

		// Token: 0x04000033 RID: 51
		private string antiTheftVerPhone;

		// Token: 0x04000034 RID: 52
		private string antiTheftVerFfu;

		// Token: 0x04000035 RID: 53
		private string lastRawMessageReceived;
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Contracts;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon
{
	// Token: 0x02000002 RID: 2
	public abstract class BaseAdaptation : IDisposable, IAdaptation, IPartImportsSatisfiedNotification
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (remove) Token: 0x06000002 RID: 2 RVA: 0x00002088 File Offset: 0x00000288
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Phone> DeviceInfoRead;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ProgressChangedEventArgs> ProgressChanged;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000212D File Offset: 0x0000032D
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002135 File Offset: 0x00000335
		private bool Disposed { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7
		public abstract PhoneTypes PhoneType { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8
		public abstract bool RecoverySupport { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9
		public abstract string ReportManufacturerName { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10
		public abstract string PackageExtension { get; }

		// Token: 0x0600000B RID: 11
		public abstract List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken);

		// Token: 0x0600000C RID: 12
		public abstract List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken);

		// Token: 0x0600000D RID: 13
		public abstract PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken);

		// Token: 0x0600000E RID: 14
		public abstract void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken);

		// Token: 0x0600000F RID: 15
		public abstract void FlashDevice(Phone phone, CancellationToken cancellationToken);

		// Token: 0x06000010 RID: 16
		public abstract bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken);

		// Token: 0x06000011 RID: 17
		public abstract void DownloadPackage(Phone phone, CancellationToken cancellationToken);

		// Token: 0x06000012 RID: 18
		public abstract void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken);

		// Token: 0x06000013 RID: 19
		public abstract SwVersionComparisonResult CompareFirmwareVersions(Phone phone);

		// Token: 0x06000014 RID: 20
		public abstract void ReadDeviceInfo(Phone currentPhone, CancellationToken cancellationToken);

		// Token: 0x06000015 RID: 21
		protected abstract void FillSupportedDeviceIdentifiers();

		// Token: 0x06000016 RID: 22 RVA: 0x0000213E File Offset: 0x0000033E
		protected virtual void InitializeManuallySupportedModels()
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002144 File Offset: 0x00000344
		public virtual List<Phone> ManuallySupportedModels()
		{
			return new List<Phone>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000215C File Offset: 0x0000035C
		public virtual List<Phone> ManuallySupportedVariants(Phone phone)
		{
			return new List<Phone>();
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002174 File Offset: 0x00000374
		public virtual string ManufacturerName
		{
			get
			{
				return "Microsoft";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000218C File Offset: 0x0000038C
		public virtual string ReportManufacturerProductLine
		{
			get
			{
				return "WindowsPhone";
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021A4 File Offset: 0x000003A4
		public virtual bool IsSupportedInNormalMode(UsbDevice usbDevice)
		{
			return this.SupportedNormalModeIds.Any((DeviceIdentifier deviceIdentifier) => deviceIdentifier.Vid.Equals(usbDevice.Vid) && deviceIdentifier.Pid.Equals(usbDevice.Pid));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000021DC File Offset: 0x000003DC
		public virtual bool IsSupportedInFlashMode(UsbDevice usbDevice)
		{
			return this.SupportedFlashModeIds.Any((DeviceIdentifier deviceIdentifier) => deviceIdentifier.Vid.Equals(usbDevice.Vid) && deviceIdentifier.Pid.Equals(usbDevice.Pid));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002214 File Offset: 0x00000414
		public virtual int ReadBatteryLevel(Phone phone)
		{
			return -1;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002228 File Offset: 0x00000428
		public virtual BatteryStatus ReadBatteryStatus(Phone phone)
		{
			return BatteryStatus.BatteryUnknown;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000223C File Offset: 0x0000043C
		public virtual bool CheckIfDeviceStillConnected(Phone phone)
		{
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002250 File Offset: 0x00000450
		public virtual ISalesNameProvider SalesNameProvider()
		{
			return null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002263 File Offset: 0x00000463
		protected void RaiseProgressPercentageChanged(int percentage, string message = null)
		{
			this.RaiseProgressPercentageChanged(new ProgressChangedEventArgs(percentage, message));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002274 File Offset: 0x00000474
		protected void RaiseProgressPercentageChanged(int percentage, string message, long downloadedSize, long totalSize, double bytesPerSecond, long secondsLeft)
		{
			this.RaiseProgressPercentageChanged(new ProgressChangedEventArgs(percentage, message, downloadedSize, totalSize, bytesPerSecond, secondsLeft));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000228C File Offset: 0x0000048C
		protected void RaiseProgressPercentageChanged(ProgressChangedEventArgs progressChangedEventArgs)
		{
			Action<ProgressChangedEventArgs> progressChanged = this.ProgressChanged;
			bool flag = progressChanged != null;
			if (flag)
			{
				progressChanged(progressChangedEventArgs);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022B4 File Offset: 0x000004B4
		protected void RaiseDeviceInfoRead(Phone phone)
		{
			Action<Phone> deviceInfoRead = this.DeviceInfoRead;
			bool flag = deviceInfoRead != null;
			if (flag)
			{
				deviceInfoRead(phone);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022DB File Offset: 0x000004DB
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000022F0 File Offset: 0x000004F0
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

		// Token: 0x06000027 RID: 39 RVA: 0x0000213E File Offset: 0x0000033E
		protected virtual void ReleaseManagedObjects()
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000213E File Offset: 0x0000033E
		protected virtual void ReleaseUnmanagedObjects()
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002328 File Offset: 0x00000528
		public byte[] GetImageData(Phone phone)
		{
			Stream imageDataStream = this.GetImageDataStream(phone);
			bool flag = imageDataStream != null;
			if (flag)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					imageDataStream.CopyTo(memoryStream);
					return memoryStream.ToArray();
				}
			}
			return null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002380 File Offset: 0x00000580
		public byte[] GetManufacturerImageData()
		{
			Stream manufacturerImageDataStream = this.GetManufacturerImageDataStream();
			bool flag = manufacturerImageDataStream != null;
			if (flag)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					manufacturerImageDataStream.CopyTo(memoryStream);
					return memoryStream.ToArray();
				}
			}
			return null;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023D8 File Offset: 0x000005D8
		protected virtual Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("default_device.png"));
			bool flag = !string.IsNullOrEmpty(text);
			Stream stream;
			if (flag)
			{
				stream = executingAssembly.GetManifestResourceStream(text);
			}
			else
			{
				stream = null;
			}
			return stream;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000243C File Offset: 0x0000063C
		protected virtual Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("default_device.png"));
			bool flag = !string.IsNullOrEmpty(text);
			Stream stream;
			if (flag)
			{
				stream = executingAssembly.GetManifestResourceStream(text);
			}
			else
			{
				stream = null;
			}
			return stream;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000249D File Offset: 0x0000069D
		public void OnImportsSatisfied()
		{
			this.FillSupportedDeviceIdentifiers();
			this.InitializeManuallySupportedModels();
		}

		// Token: 0x04000001 RID: 1
		public readonly Collection<DeviceIdentifier> SupportedNormalModeIds = new Collection<DeviceIdentifier>();

		// Token: 0x04000002 RID: 2
		public readonly Collection<DeviceIdentifier> SupportedFlashModeIds = new Collection<DeviceIdentifier>();

		// Token: 0x04000003 RID: 3
		public readonly Collection<DeviceIdentifier> SupportedRecoveryModeIds = new Collection<DeviceIdentifier>();

		// Token: 0x04000004 RID: 4
		public readonly Collection<DeviceIdentifier> SupportedEmergencyModeIds = new Collection<DeviceIdentifier>();
	}
}

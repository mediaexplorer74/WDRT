using System;
using ClickerUtilityLibrary.Misc;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.FawkesAdaptation
{
	// Token: 0x02000005 RID: 5
	public static class FawkesUtils
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002190 File Offset: 0x00000390
		public static bool TryParseImageVersion(string versionString, out ImageVersion imageVersion)
		{
			imageVersion = null;
			if (string.IsNullOrWhiteSpace(versionString))
			{
				return false;
			}
			string[] array = versionString.Split(new char[] { '.', '-' });
			if (array.Length == 0)
			{
				imageVersion = new ImageVersion(0);
				return true;
			}
			byte b = 0;
			short num = 0;
			byte b2;
			if (!byte.TryParse(array[0], out b2))
			{
				return false;
			}
			if (array.Length > 1)
			{
				if (!byte.TryParse(array[1], out b))
				{
					return false;
				}
				if (array.Length > 2 && !short.TryParse(array[2], out num))
				{
					return false;
				}
			}
			imageVersion = new ImageVersion(b2, b, num);
			return true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002214 File Offset: 0x00000414
		internal static void WriteFawkesDeviceInfo(this Phone phone, FawkesDeviceInfo deviceInfo)
		{
			phone.HardwareId = deviceInfo.HardwareId;
			phone.SalesName = deviceInfo.DeviceFriendlyName;
			phone.SoftwareVersion = deviceInfo.FirmwareVersion.ToString();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000223F File Offset: 0x0000043F
		internal static FawkesDeviceInfo ReadFawkesDeviceInfo(this Phone phone)
		{
			return new FawkesDeviceInfo(phone.SoftwareVersion, phone.HardwareId, phone.SalesName);
		}
	}
}

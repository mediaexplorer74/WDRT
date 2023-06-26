using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Nokia.Lucid.DeviceInformation;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.LucidConnectivity
{
	// Token: 0x0200002E RID: 46
	public class LucidConnectivityHelper
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000A680 File Offset: 0x00008880
		public static void GetVidAndPid(string deviceId, out string vid, out string pid)
		{
			vid = string.Empty;
			pid = string.Empty;
			try
			{
				string text = deviceId.Substring(deviceId.IndexOf("VID", StringComparison.OrdinalIgnoreCase), 17);
				vid = text.Substring(text.IndexOf("VID", StringComparison.OrdinalIgnoreCase) + 4, 4).ToUpper();
				pid = text.Substring(text.IndexOf("PID", StringComparison.OrdinalIgnoreCase) + 4, 4).ToUpper();
			}
			catch (Exception ex)
			{
				Tracer<LucidConnectivityHelper>.WriteError(ex, "Error extracting VID and PID: {0}", new object[] { ex.Message });
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000A720 File Offset: 0x00008920
		public static ConnectedDeviceMode GetDeviceMode(string vid, string pid)
		{
			string text = vid + "&" + pid;
			bool flag = text.Contains("&") && !text.Contains("VID_") && !text.Contains("PID_");
			if (flag)
			{
				text = text.Insert(0, "VID_");
				text = text.Insert(text.IndexOf("&", StringComparison.OrdinalIgnoreCase) + 1, "PID_");
			}
			string text2 = text;
			string text3 = text2;
			if (text3 != null)
			{
				int length = text3.Length;
				if (length == 17)
				{
					char c = text3[16];
					switch (c)
					{
					case '0':
						if (!(text3 == "VID_0421&PID_0660"))
						{
							if (text3 == "VID_045E&PID_0A00")
							{
								goto IL_1F5;
							}
							if (!(text3 == "VID_3495&PID_00E0"))
							{
								goto IL_223;
							}
							return ConnectedDeviceMode.KernelModeDebugging;
						}
						break;
					case '1':
						if (!(text3 == "VID_045E&PID_0A01"))
						{
							if (text3 == "VID_0421&PID_0661")
							{
								goto IL_1F5;
							}
							if (!(text3 == "VID_05C6&PID_9001"))
							{
								goto IL_223;
							}
							return ConnectedDeviceMode.QcomRmnetComposite;
						}
						break;
					case '2':
						if (!(text3 == "VID_045E&PID_0A02"))
						{
							goto IL_223;
						}
						goto IL_1FA;
					case '3':
						if (text3 == "VID_0421&PID_0713")
						{
							return ConnectedDeviceMode.Test;
						}
						if (!(text3 == "VID_045E&PID_0A03"))
						{
							goto IL_223;
						}
						goto IL_214;
					case '4':
						if (!(text3 == "VID_0421&PID_0714"))
						{
							goto IL_223;
						}
						goto IL_1FA;
					case '5':
					case '7':
						goto IL_223;
					case '6':
						if (!(text3 == "VID_05C6&PID_9006"))
						{
							goto IL_223;
						}
						return ConnectedDeviceMode.MassStorage;
					case '8':
						if (!(text3 == "VID_05C6&PID_9008"))
						{
							goto IL_223;
						}
						goto IL_214;
					default:
						switch (c)
						{
						case 'A':
							if (!(text3 == "VID_045E&PID_062A"))
							{
								goto IL_223;
							}
							return ConnectedDeviceMode.MsFlashing;
						case 'B':
							if (!(text3 == "VID_05C6&PID_319B"))
							{
								goto IL_223;
							}
							return ConnectedDeviceMode.QcomSerialComposite;
						case 'C':
							if (!(text3 == "VID_0421&PID_06FC"))
							{
								goto IL_223;
							}
							goto IL_1F5;
						case 'D':
							goto IL_223;
						case 'E':
							if (!(text3 == "VID_0421&PID_066E"))
							{
								goto IL_223;
							}
							goto IL_1FA;
						default:
							goto IL_223;
						}
						break;
					}
					return ConnectedDeviceMode.Label;
					IL_1F5:
					return ConnectedDeviceMode.Normal;
					IL_1FA:
					return ConnectedDeviceMode.Uefi;
					IL_214:
					return ConnectedDeviceMode.QcomDload;
				}
			}
			IL_223:
			return ConnectedDeviceMode.Unknown;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000A958 File Offset: 0x00008B58
		public static ConnectedDeviceMode GetDeviceModeFromDevicePath(string devicePath)
		{
			Tracer<LucidConnectivityHelper>.WriteInformation("DevicePath: {0}", new object[] { devicePath });
			ConnectedDeviceMode connectedDeviceMode;
			try
			{
				string text;
				string text2;
				LucidConnectivityHelper.GetVidAndPid(devicePath, out text, out text2);
				Tracer<LucidConnectivityHelper>.WriteInformation("Vid {0}, Pid {1}", new object[] { text, text2 });
				connectedDeviceMode = LucidConnectivityHelper.GetDeviceMode(text, text2);
			}
			catch (Exception ex)
			{
				Tracer<LucidConnectivityHelper>.WriteError(ex, "Failed to determine device mode from device path", new object[0]);
				connectedDeviceMode = ConnectedDeviceMode.Unknown;
			}
			return connectedDeviceMode;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000A9DC File Offset: 0x00008BDC
		public static void ParseTypeDesignatorAndSalesName(string busReportedDeviceDescription, out string typeDesignator, out string salesName)
		{
			typeDesignator = string.Empty;
			salesName = string.Empty;
			bool flag = busReportedDeviceDescription.Contains("|");
			if (flag)
			{
				string[] array = busReportedDeviceDescription.Split(new char[] { '|' });
				typeDesignator = ((array.Length != 0) ? array[0] : string.Empty);
				salesName = ((array.Length > 1) ? array[1] : string.Empty);
			}
			else
			{
				bool flag2 = busReportedDeviceDescription.Contains("(") && busReportedDeviceDescription.Contains(")");
				if (flag2)
				{
					int num = busReportedDeviceDescription.LastIndexOf('(');
					int num2 = busReportedDeviceDescription.IndexOf(')', Math.Max(num, 0));
					int num3 = busReportedDeviceDescription.IndexOf(" (", StringComparison.InvariantCulture);
					bool flag3 = num > -1 && num2 > num;
					if (flag3)
					{
						typeDesignator = busReportedDeviceDescription.Substring(num + 1, num2 - num - 1);
					}
					bool flag4 = num3 > -1;
					if (flag4)
					{
						salesName = busReportedDeviceDescription.Substring(0, num3);
					}
				}
				else
				{
					salesName = busReportedDeviceDescription;
				}
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		public static string GetHubAndPort(string locationInfo)
		{
			string text3;
			try
			{
				int num = locationInfo.IndexOf("HUB_#", StringComparison.OrdinalIgnoreCase);
				int num2 = locationInfo.IndexOf("PORT_#", StringComparison.OrdinalIgnoreCase);
				bool flag = num < 0 || num2 < 0;
				if (flag)
				{
					throw new Exception("Wrong string format");
				}
				string text = locationInfo.Substring(num + 5, 4);
				string text2 = locationInfo.Substring(num2 + 6, 4);
				text3 = string.Format("{0}:{1}", text, text2);
			}
			catch (Exception ex)
			{
				Tracer<LucidConnectivityHelper>.WriteError(ex, "Error extracting hub and port IDs", new object[0]);
				text3 = string.Empty;
			}
			return text3;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000AB70 File Offset: 0x00008D70
		public static string LocationPath2ControllerId(string controllerLocationPath)
		{
			Tracer<LucidConnectivityHelper>.WriteInformation("Getting controller ID for {0}", new object[] { controllerLocationPath });
			string text = string.Empty;
			int num = controllerLocationPath.IndexOf("#USBROOT(", StringComparison.InvariantCultureIgnoreCase);
			bool flag = num > 0;
			if (flag)
			{
				controllerLocationPath = controllerLocationPath.Substring(0, num);
				Tracer<LucidConnectivityHelper>.WriteInformation("Location path fixed: {0}", new object[] { controllerLocationPath });
			}
			MatchCollection matchCollection = Regex.Matches(controllerLocationPath, "\\(([a-z0-9]+)\\)", RegexOptions.IgnoreCase);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				bool flag2 = 2 == match.Groups.Count;
				if (flag2)
				{
					bool flag3 = !string.IsNullOrEmpty(text);
					if (flag3)
					{
						text += ".";
					}
					text += match.Groups[1].Value;
				}
			}
			Tracer<LucidConnectivityHelper>.WriteInformation("Controller ID: {0}", new object[] { text });
			return text;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000AC94 File Offset: 0x00008E94
		internal static string GetSuitableLabelModeInterfaceDevicePath(ReadOnlyCollection<UsbDeviceEndpoint> endpoints)
		{
			string text = string.Empty;
			using (IEnumerator<UsbDeviceEndpoint> enumerator = endpoints.Where((UsbDeviceEndpoint i) => i.DevicePath.IndexOf("mi_04", StringComparison.InvariantCultureIgnoreCase) > 0).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					UsbDeviceEndpoint usbDeviceEndpoint = enumerator.Current;
					text = usbDeviceEndpoint.DevicePath;
				}
			}
			return text;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000AD14 File Offset: 0x00008F14
		public static ConnectedDevice GetConnectedDeviceFromUsbDevice(UsbDevice usbDevice)
		{
			ConnectedDeviceMode deviceMode = LucidConnectivityHelper.GetDeviceMode(usbDevice.Vid, usbDevice.Pid);
			ConnectedDevice connectedDevice = new ConnectedDevice(usbDevice.PortId, usbDevice.Vid, usbDevice.Pid, deviceMode, true, usbDevice.TypeDesignator, usbDevice.SalesName, usbDevice.Path, usbDevice.InstanceId);
			string text = string.Empty;
			switch (deviceMode)
			{
			case ConnectedDeviceMode.Normal:
			case ConnectedDeviceMode.Uefi:
			{
				bool flag = usbDevice.Interfaces.Count > 0;
				if (flag)
				{
					text = usbDevice.Interfaces[0].DevicePath;
				}
				break;
			}
			case ConnectedDeviceMode.Label:
				text = LucidConnectivityHelper.GetSuitableLabelModeInterfaceDevicePath(usbDevice.Interfaces);
				break;
			}
			bool flag2 = string.IsNullOrEmpty(text);
			if (flag2)
			{
				connectedDevice.DeviceReady = false;
				connectedDevice.DevicePath = string.Empty;
			}
			else
			{
				connectedDevice.DeviceReady = true;
				connectedDevice.DevicePath = text;
			}
			return connectedDevice;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000AE00 File Offset: 0x00009000
		internal static bool IsWrongDefaultNcsdInterface(DeviceInfo device)
		{
			bool flag3;
			try
			{
				string text;
				string text2;
				LucidConnectivityHelper.GetVidAndPid(device.InstanceId, out text, out text2);
				bool flag = LucidConnectivityHelper.GetDeviceMode(text, text2) == ConnectedDeviceMode.Normal;
				if (flag)
				{
					string miIdentifierFromDeviceId = LucidConnectivityHelper.GetMiIdentifierFromDeviceId(device.InstanceId);
					string[] array = device.ReadSiblingInstanceIds();
					bool flag2 = array.Length == 3 && miIdentifierFromDeviceId != "03";
					if (flag2)
					{
						Tracer<LucidConnectivityHelper>.WriteInformation("Interface {0} has 3 siblings and is not MI_03. Ignoring interface.", new object[] { device.Path });
						return true;
					}
				}
				flag3 = false;
			}
			catch (Exception ex)
			{
				Tracer<LucidConnectivityHelper>.WriteError(ex, "Failed to check default NCSd interface", new object[0]);
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000AEB0 File Offset: 0x000090B0
		internal static string GetMiIdentifierFromDeviceId(string deviceId)
		{
			string text = string.Empty;
			int num = deviceId.IndexOf("mi_", StringComparison.InvariantCultureIgnoreCase);
			bool flag = num > 0;
			if (flag)
			{
				num += 3;
				int num2 = deviceId.IndexOf("\\", num, StringComparison.InvariantCultureIgnoreCase);
				bool flag2 = num2 > 0;
				if (flag2)
				{
					text = deviceId.Substring(num, num2 - num);
				}
			}
			return text;
		}
	}
}

using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting
{
	// Token: 0x02000019 RID: 25
	public class ReportBuilder
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00007750 File Offset: 0x00005950
		public static MsrReport Build(ReportData reportData, bool isInternal)
		{
			return new MsrReport(reportData.SessionId)
			{
				SystemInfo = reportData.SystemInfo,
				OsPlatform = (Environment.Is64BitOperatingSystem ? "x64" : "x86"),
				OsVersion = Environment.OSVersion.Version.ToString(),
				CountryCode = RegionAndLanguage.CurrentLocation,
				ActionDescription = reportData.Description,
				Imei = reportData.PhoneInfo.Imei,
				Vid = reportData.PhoneInfo.Vid,
				Pid = reportData.PhoneInfo.Pid,
				Mid = reportData.PhoneInfo.Mid,
				Cid = reportData.PhoneInfo.Cid,
				SerialNumber = reportData.PhoneInfo.SerialNumber,
				SalesName = reportData.PhoneInfo.SalesName,
				PhoneType = reportData.PhoneInfo.Type,
				SwVersion = reportData.PhoneInfo.SoftwareVersion,
				AkVersion = reportData.PhoneInfo.AkVersion,
				NewAkVersion = reportData.PhoneInfo.NewAkVersion,
				NewSwVersion = reportData.PhoneInfo.NewSoftwareVersion,
				DownloadDuration = reportData.DownloadDuration,
				UpdateDuration = reportData.UpdateDuration,
				ApiError = string.Format("{0:X8}", ReportBuilder.ApiError(reportData.Exception)),
				ApiErrorMessage = ReportBuilder.GetApiErrorMessage(reportData.Exception, "S_OK"),
				Uri = reportData.UriData,
				ApplicationVersion = ReportBuilder.AppVersion(isInternal),
				FirmwareGrading = ReportBuilder.FirmwareGradingCheck(reportData.PhoneInfo.SoftwareVersion, reportData.PhoneInfo.NewSoftwareVersion).ToString(),
				LocalPath = reportData.LocalPath,
				PackageSizeOnServer = reportData.PackageSize.ToString(CultureInfo.InvariantCulture),
				DownloadedBytes = reportData.DownloadedBytes.ToString(CultureInfo.InvariantCulture),
				ResumeCounter = reportData.ResumeCounter.ToString(CultureInfo.InvariantCulture),
				ManufacturerName = reportData.PhoneInfo.ReportManufacturerName,
				ManufacturerProductLine = reportData.PhoneInfo.ReportManufacturerProductLine,
				ManufacturerHardwareModel = reportData.PhoneInfo.HardwareModel,
				ManufacturerHardwareVariant = reportData.PhoneInfo.HardwareVariant,
				TimeStamp = TimeStampUtility.CreateTimeStamp(),
				UserSiteLanguage = ApplicationInfo.CurrentLanguageInRegistry.IetfLanguageTag,
				ApplicationName = ReportBuilder.environmentInfo.ApplicationName,
				ApplicationVendor = ReportBuilder.environmentInfo.ApplicationVendor,
				LastErrorData = ReportBuilder.GetApiErrorMessage(reportData.LastError, string.Empty),
				DebugField = ReportBuilder.GetApiErrorDetails(reportData.Exception ?? reportData.LastError, (reportData.Exception == null && reportData.LastError != null) ? ("Last error:" + Environment.NewLine) : string.Empty)
			};
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007A74 File Offset: 0x00005C74
		private static string GetApiErrorDetails(Exception exception, string prefix = "")
		{
			bool flag = exception == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				text = ReportBuilder.Trim(string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[] { prefix, exception }), 1994);
			}
			return text;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007ABC File Offset: 0x00005CBC
		private static string Trim(string baseString, int maxNoOfChars)
		{
			bool flag = string.IsNullOrWhiteSpace(baseString);
			string text;
			if (flag)
			{
				text = baseString;
			}
			else
			{
				text = new string(baseString.Take(maxNoOfChars).ToArray<char>());
			}
			return text;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007AF0 File Offset: 0x00005CF0
		private static int ApiError(Exception error)
		{
			COMException ex = error as COMException;
			return (ex == null) ? 0 : ex.ErrorCode;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007B18 File Offset: 0x00005D18
		private static string GetApiErrorMessage(Exception error, string defaultEmptyValue = "S_OK")
		{
			bool flag = error == null;
			string text;
			if (flag)
			{
				text = defaultEmptyValue;
			}
			else
			{
				text = error.Message + "|" + error.Source;
				bool flag2 = error.InnerException != null;
				if (flag2)
				{
					text = string.Concat(new string[]
					{
						text,
						"|",
						error.InnerException.Message,
						"|",
						error.InnerException.Source
					});
				}
			}
			return text;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007BA0 File Offset: 0x00005DA0
		public static string AppVersion(bool isInternal)
		{
			string text = ReportBuilder.MainAppVersion();
			if (isInternal)
			{
				text = "[INT]" + text;
			}
			return text;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007BCC File Offset: 0x00005DCC
		private static string MainAppVersion()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			bool flag = entryAssembly != null;
			string text4;
			if (flag)
			{
				Version version = entryAssembly.GetName().Version;
				string text = version.Major.ToString(CultureInfo.InvariantCulture);
				string text2 = version.Minor.ToString(CultureInfo.InvariantCulture);
				string text3 = version.Build.ToString(CultureInfo.InvariantCulture);
				text4 = string.Concat(new string[] { text, ".", text2, ".", text3 });
			}
			else
			{
				text4 = string.Empty;
			}
			return text4;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007C74 File Offset: 0x00005E74
		[Conditional("DAILY")]
		private static void FormatVersionString(ref string version)
		{
			int num;
			bool flag = int.TryParse(version, out num);
			if (flag)
			{
				bool flag2 = num > 0;
				if (flag2)
				{
					version = num.ToString("0000");
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007CAC File Offset: 0x00005EAC
		private static ReportBuilder.FirmwareGrading FirmwareGradingCheck(string currentFwVersion, string newFirmwareVersion)
		{
			bool flag = string.IsNullOrEmpty(currentFwVersion) || string.IsNullOrEmpty(newFirmwareVersion);
			ReportBuilder.FirmwareGrading firmwareGrading;
			if (flag)
			{
				firmwareGrading = ReportBuilder.FirmwareGrading.None;
			}
			else
			{
				bool flag2 = currentFwVersion == newFirmwareVersion;
				if (flag2)
				{
					firmwareGrading = ReportBuilder.FirmwareGrading.SameVersion;
				}
				else
				{
					string text = Regex.Replace(currentFwVersion, "[^\\d\\.]", string.Empty);
					string text2 = Regex.Replace(newFirmwareVersion, "[^\\d\\.]", string.Empty);
					bool flag3 = string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2);
					if (flag3)
					{
						firmwareGrading = ReportBuilder.FirmwareGrading.None;
					}
					else
					{
						ReportBuilder.VersionComparisonResult versionComparisonResult = ReportBuilder.VersionCompare(text, text2);
						bool flag4 = versionComparisonResult == ReportBuilder.VersionComparisonResult.FirstIsOlder;
						if (flag4)
						{
							firmwareGrading = ReportBuilder.FirmwareGrading.Upgrade;
						}
						else
						{
							bool flag5 = versionComparisonResult == ReportBuilder.VersionComparisonResult.FirstIsNewer;
							if (flag5)
							{
								firmwareGrading = ReportBuilder.FirmwareGrading.Downgrade;
							}
							else
							{
								firmwareGrading = ReportBuilder.FirmwareGrading.None;
							}
						}
					}
				}
			}
			return firmwareGrading;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007D58 File Offset: 0x00005F58
		private static ReportBuilder.VersionComparisonResult VersionCompare(string version1, string version2)
		{
			bool flag = string.IsNullOrEmpty(version1) || string.IsNullOrEmpty(version2);
			if (flag)
			{
				throw new ArgumentException("Version cannot be null or an empty string.");
			}
			version1 = version1.Trim();
			version2 = version2.Trim();
			string[] array = version1.Split(new char[] { '.' });
			string[] array2 = version2.Split(new char[] { '.' });
			int num = Math.Min(array.Length, array2.Length);
			int i = 0;
			while (i < num)
			{
				string text = array[i].Trim();
				string text2 = array2[i].Trim();
				long num2 = Convert.ToInt64(text);
				long num3 = Convert.ToInt64(text2);
				bool flag2 = num2 > num3;
				ReportBuilder.VersionComparisonResult versionComparisonResult;
				if (flag2)
				{
					versionComparisonResult = ReportBuilder.VersionComparisonResult.FirstIsNewer;
				}
				else
				{
					bool flag3 = num2 < num3;
					if (!flag3)
					{
						i++;
						continue;
					}
					versionComparisonResult = ReportBuilder.VersionComparisonResult.FirstIsOlder;
				}
				return versionComparisonResult;
			}
			bool flag4 = array.Length > array2.Length;
			if (flag4)
			{
				return ReportBuilder.VersionComparisonResult.FirstIsNewer;
			}
			return (array.Length < array2.Length) ? ReportBuilder.VersionComparisonResult.FirstIsOlder : ReportBuilder.VersionComparisonResult.BothAreSame;
		}

		// Token: 0x040000A1 RID: 161
		private const string InternalPrefix = "[INT]";

		// Token: 0x040000A2 RID: 162
		private const string VersionPattern = "[^\\d\\.]";

		// Token: 0x040000A3 RID: 163
		private static EnvironmentInfo environmentInfo = new EnvironmentInfo(new ApplicationInfo());

		// Token: 0x040000A4 RID: 164
		private const int DebugFieldLength = 2000;

		// Token: 0x040000A5 RID: 165
		private const string EmptyErrorDefaultValue = "S_OK";

		// Token: 0x02000056 RID: 86
		private enum FirmwareGrading
		{
			// Token: 0x04000204 RID: 516
			None,
			// Token: 0x04000205 RID: 517
			Downgrade,
			// Token: 0x04000206 RID: 518
			SameVersion,
			// Token: 0x04000207 RID: 519
			Upgrade
		}

		// Token: 0x02000057 RID: 87
		private enum VersionComparisonResult
		{
			// Token: 0x04000209 RID: 521
			FirstIsOlder = -1,
			// Token: 0x0400020A RID: 522
			BothAreSame,
			// Token: 0x0400020B RID: 523
			FirstIsNewer
		}
	}
}

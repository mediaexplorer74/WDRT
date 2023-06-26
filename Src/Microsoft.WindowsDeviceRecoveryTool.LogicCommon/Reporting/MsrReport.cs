using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Interfaces;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class MsrReport : IReport
	{
		// Token: 0x06000104 RID: 260 RVA: 0x000027B8 File Offset: 0x000009B8
		private MsrReport()
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005DEC File Offset: 0x00003FEC
		public MsrReport(string sessionId)
		{
			this.SessionId = sessionId;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005E00 File Offset: 0x00004000
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00005E18 File Offset: 0x00004018
		public string DownloadedBytes
		{
			get
			{
				return this.downloadedBytes;
			}
			set
			{
				this.downloadedBytes = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005E28 File Offset: 0x00004028
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00005E40 File Offset: 0x00004040
		public string PackageSizeOnServer
		{
			get
			{
				return this.packageSizeOnServer;
			}
			set
			{
				this.packageSizeOnServer = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005E50 File Offset: 0x00004050
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00005E68 File Offset: 0x00004068
		public string ResumeCounter
		{
			get
			{
				return this.resumeCounter;
			}
			set
			{
				this.resumeCounter = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005E78 File Offset: 0x00004078
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00005E90 File Offset: 0x00004090
		public string ActionDescription
		{
			get
			{
				return this.actionDescription;
			}
			set
			{
				this.actionDescription = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005EA0 File Offset: 0x000040A0
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00005EB8 File Offset: 0x000040B8
		public string SystemInfo
		{
			get
			{
				return this.systemInfo;
			}
			set
			{
				this.systemInfo = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005EC8 File Offset: 0x000040C8
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00005EE0 File Offset: 0x000040E0
		public string OsVersion
		{
			get
			{
				return this.osVersion;
			}
			set
			{
				this.osVersion = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005EF0 File Offset: 0x000040F0
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00005F08 File Offset: 0x00004108
		public string OsPlatform
		{
			get
			{
				return this.osPlatform;
			}
			set
			{
				this.osPlatform = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005F18 File Offset: 0x00004118
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005F30 File Offset: 0x00004130
		public string ManufacturerHardwareModel
		{
			get
			{
				return this.productType;
			}
			set
			{
				this.productType = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005F40 File Offset: 0x00004140
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00005F58 File Offset: 0x00004158
		public string ManufacturerHardwareVariant
		{
			get
			{
				return this.productCode;
			}
			set
			{
				this.productCode = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005F68 File Offset: 0x00004168
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00005F80 File Offset: 0x00004180
		public string Imei
		{
			get
			{
				return this.imei;
			}
			set
			{
				this.imei = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005F90 File Offset: 0x00004190
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00005FA8 File Offset: 0x000041A8
		public string Vid
		{
			get
			{
				return this.vid;
			}
			set
			{
				this.vid = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005FB8 File Offset: 0x000041B8
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00005FD0 File Offset: 0x000041D0
		public string Pid
		{
			get
			{
				return this.pid;
			}
			set
			{
				this.pid = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005FE0 File Offset: 0x000041E0
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00005FF8 File Offset: 0x000041F8
		public string Mid
		{
			get
			{
				return this.mid;
			}
			set
			{
				this.mid = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006008 File Offset: 0x00004208
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00006020 File Offset: 0x00004220
		public string Cid
		{
			get
			{
				return this.cid;
			}
			set
			{
				this.cid = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006030 File Offset: 0x00004230
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00006048 File Offset: 0x00004248
		public string SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
			set
			{
				this.serialNumber = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00006058 File Offset: 0x00004258
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00006070 File Offset: 0x00004270
		public string SalesName
		{
			get
			{
				return this.salesName;
			}
			set
			{
				this.salesName = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006080 File Offset: 0x00004280
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00006098 File Offset: 0x00004298
		public string SwVersion
		{
			get
			{
				return this.swVersion;
			}
			set
			{
				this.swVersion = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000060A8 File Offset: 0x000042A8
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000060C0 File Offset: 0x000042C0
		public string AkVersion
		{
			get
			{
				return this.akVersion;
			}
			set
			{
				this.akVersion = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000060D0 File Offset: 0x000042D0
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000060E8 File Offset: 0x000042E8
		public string NewAkVersion
		{
			get
			{
				return this.newAkVersion;
			}
			set
			{
				this.newAkVersion = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000060F8 File Offset: 0x000042F8
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00006110 File Offset: 0x00004310
		public string NewSwVersion
		{
			get
			{
				return this.newSwVersion;
			}
			set
			{
				this.newSwVersion = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006120 File Offset: 0x00004320
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00006138 File Offset: 0x00004338
		public string LocalPath
		{
			get
			{
				return this.localPath;
			}
			set
			{
				this.localPath = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006148 File Offset: 0x00004348
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00006160 File Offset: 0x00004360
		public string DebugField
		{
			get
			{
				return this.debugField;
			}
			set
			{
				string text = MsrReport.PrepareForCsvFormat(value);
				this.debugField = ((text.Length <= 2000) ? text : text.Substring(0, 2000));
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006198 File Offset: 0x00004398
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000061B0 File Offset: 0x000043B0
		public string ApiError
		{
			get
			{
				return this.apiError;
			}
			set
			{
				this.apiError = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000061C0 File Offset: 0x000043C0
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000061D8 File Offset: 0x000043D8
		public string ApiErrorMessage
		{
			get
			{
				return this.apiErrorMessage;
			}
			set
			{
				this.apiErrorMessage = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000061E8 File Offset: 0x000043E8
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006200 File Offset: 0x00004400
		public string ApplicationName
		{
			get
			{
				return this.applicationName;
			}
			set
			{
				this.applicationName = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006210 File Offset: 0x00004410
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00006228 File Offset: 0x00004428
		public string ApplicationVendor
		{
			get
			{
				return this.applicationVendor;
			}
			set
			{
				this.applicationVendor = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006238 File Offset: 0x00004438
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00006250 File Offset: 0x00004450
		public string ApplicationVersion
		{
			get
			{
				return this.applicationVersion;
			}
			set
			{
				this.applicationVersion = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006260 File Offset: 0x00004460
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00006278 File Offset: 0x00004478
		public string UserSiteLanguage
		{
			get
			{
				return this.userSiteLanguage;
			}
			set
			{
				this.userSiteLanguage = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006288 File Offset: 0x00004488
		// (set) Token: 0x0600013F RID: 319 RVA: 0x000062A0 File Offset: 0x000044A0
		public string CountryCode
		{
			get
			{
				return this.countryCode;
			}
			set
			{
				this.countryCode = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000062B0 File Offset: 0x000044B0
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000062C8 File Offset: 0x000044C8
		public string FirmwareGrading
		{
			get
			{
				return this.firmwareGrading;
			}
			set
			{
				this.firmwareGrading = MsrReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000062D8 File Offset: 0x000044D8
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000062F0 File Offset: 0x000044F0
		public string ReportVersion
		{
			get
			{
				return this.reportVersion;
			}
			set
			{
				this.reportVersion = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000062FC File Offset: 0x000044FC
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00006314 File Offset: 0x00004514
		public long TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
			set
			{
				this.timeStamp = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000631E File Offset: 0x0000451E
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00006326 File Offset: 0x00004526
		public long DownloadDuration { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000632F File Offset: 0x0000452F
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00006337 File Offset: 0x00004537
		public long UpdateDuration { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006340 File Offset: 0x00004540
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00006348 File Offset: 0x00004548
		public string SessionId { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006351 File Offset: 0x00004551
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00006359 File Offset: 0x00004559
		public UriData Uri { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006362 File Offset: 0x00004562
		// (set) Token: 0x0600014F RID: 335 RVA: 0x0000636A File Offset: 0x0000456A
		public PhoneTypes PhoneType { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006373 File Offset: 0x00004573
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000637B File Offset: 0x0000457B
		public string ManufacturerName { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006384 File Offset: 0x00004584
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000638C File Offset: 0x0000458C
		public string ManufacturerProductLine { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006395 File Offset: 0x00004595
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000639D File Offset: 0x0000459D
		public string PlatformId { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000063A6 File Offset: 0x000045A6
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000063AE File Offset: 0x000045AE
		public string PackageId { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000063B7 File Offset: 0x000045B7
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000063BF File Offset: 0x000045BF
		public string LastErrorData { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000063C8 File Offset: 0x000045C8
		// (set) Token: 0x0600015B RID: 347 RVA: 0x000063D0 File Offset: 0x000045D0
		public string Manufacturer { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000063D9 File Offset: 0x000045D9
		// (set) Token: 0x0600015D RID: 349 RVA: 0x000063E1 File Offset: 0x000045E1
		public bool Sent { get; private set; }

		// Token: 0x0600015E RID: 350 RVA: 0x000063EA File Offset: 0x000045EA
		public void MarkAsSent()
		{
			this.Sent = true;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000063F8 File Offset: 0x000045F8
		public string GetReportAsCsv()
		{
			List<string> list = new List<string>
			{
				this.SystemInfo,
				this.ActionDescription,
				this.ManufacturerHardwareModel,
				this.ManufacturerHardwareVariant,
				this.Imei,
				this.Vid,
				this.Pid,
				this.Mid,
				this.Cid,
				this.SerialNumber,
				this.SalesName,
				this.PhoneType.ToString(),
				this.SwVersion,
				this.AkVersion,
				this.NewAkVersion,
				this.NewSwVersion,
				this.DownloadDuration.ToString(),
				this.UpdateDuration.ToString(),
				this.ApiError,
				this.ApiErrorMessage,
				this.UriDataToUint(this.Uri).ToString(),
				this.ApplicationVersion,
				this.FirmwareGrading,
				this.LocalPath,
				this.PackageSizeOnServer,
				this.DownloadedBytes,
				this.ResumeCounter,
				this.ManufacturerName,
				this.ManufacturerProductLine
			};
			return string.Join(";", list);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000065C4 File Offset: 0x000047C4
		public string GetReportAsXml()
		{
			XDocument xdocument = new XDocument(new XDeclaration("1.0", "utf-8", "no"), new object[]
			{
				new XElement("Reports", new XElement("Report", new object[]
				{
					new XElement("reportVersion", "1.1"),
					new XElement("reportSessionId", this.SessionId),
					new XElement("reportTimeStamp", this.TimeStamp),
					new XElement("osSystemInfo", this.SystemInfo),
					new XElement("osVersion", this.OsVersion),
					new XElement("osPlatform", this.OsPlatform),
					new XElement("appApplicationName", this.ApplicationName),
					new XElement("appApplicationVendor", this.ApplicationVendor),
					new XElement("appApplicationVersion", this.ApplicationVersion),
					new XElement("appUserSiteLanguage", this.UserSiteLanguage),
					new XElement("appCountryCode", this.CountryCode),
					new XElement("flowUri", this.UriDataToUint(this.Uri).ToString()),
					new XElement("flowActionDescription", this.ActionDescription),
					new XElement("flowNewSwVersion", this.NewSwVersion),
					new XElement("flowNewAkVersion", this.NewAkVersion),
					new XElement("flowFirmwareGrading", this.FirmwareGrading),
					new XElement("flowLocalPath", this.LocalPath),
					new XElement("flowDownloadDuration", this.DownloadDuration),
					new XElement("flowUpdateDuration", this.UpdateDuration),
					new XElement("flowPackageSizeOnServer", this.PackageSizeOnServer),
					new XElement("flowDownloadedBytes", this.DownloadedBytes),
					new XElement("flowResumeCounter", this.ResumeCounter),
					new XElement("devManufacturer", this.Manufacturer),
					new XElement("devManufacturerName", this.ManufacturerName),
					new XElement("devManufacturerProductLine", this.ManufacturerProductLine),
					new XElement("devManufacturerHardwareModel", this.ManufacturerHardwareModel),
					new XElement("devManufacturerHardwareVariant", this.ManufacturerHardwareVariant),
					new XElement("devImei", this.Imei),
					new XElement("devVid", this.Vid),
					new XElement("devPid", this.Pid),
					new XElement("devMid", this.Mid),
					new XElement("devCid", this.Cid),
					new XElement("devSerialNumber", this.SerialNumber),
					new XElement("devSalesName", this.SalesName),
					new XElement("devPhoneType", this.PhoneType),
					new XElement("devSwVersion", this.SwVersion),
					new XElement("devAkVersion", this.AkVersion),
					new XElement("devPlatformId", this.PlatformId),
					new XElement("devPackageId", this.PackageId),
					new XElement("DebugField", this.DebugField),
					new XElement("ApiError", this.ApiError),
					new XElement("ApiErrorMessage", this.ApiErrorMessage),
					new XElement("LastErrorData", this.LastErrorData)
				}))
			});
			return xdocument.ToString();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006A70 File Offset: 0x00004C70
		public ReportUpdateStatus4Parameters CreateReportStatusParameters()
		{
			string text = this.FormatString(this.Imei, 100);
			bool flag = this.PhoneType == PhoneTypes.Htc;
			if (flag)
			{
				text = this.FormatString(this.SerialNumber, 100);
			}
			return new ReportUpdateStatus4Parameters
			{
				SystemInfo = this.SystemInfo,
				ActionDescription = this.FormatString(this.ActionDescription, 200),
				Uri = (long)this.Uri,
				UriDescription = this.FormatString(UriDataArgument.Description(this.Uri), 200),
				ApplicationName = this.FormatString(this.ApplicationName, 200),
				ApplicationVendorName = "Microsoft",
				ApplicationVersion = this.ApplicationVersion,
				ProductType = (string.IsNullOrWhiteSpace(this.ManufacturerHardwareModel) ? this.FormatString(this.SalesName, 100) : this.FormatString(this.ManufacturerHardwareModel, 100)),
				ProductCode = this.FormatString(this.ManufacturerHardwareVariant, 100),
				Imei = text,
				FirmwareVersionOld = this.FormatString(this.SwVersion, 200),
				FirmwareVersionNew = this.FormatString(this.NewSwVersion, 200),
				FwGrading = this.FirmwareGrading,
				Duration = this.DownloadDuration + this.UpdateDuration,
				DownloadDuration = this.DownloadDuration,
				UpdateDuration = this.UpdateDuration,
				ApiError = this.ApiError,
				ApiErrorText = this.FormatString(this.ApiErrorMessage, 400),
				TimeStamp = TimeStampUtility.CreateTimeStamp(),
				Ext1 = ((string.IsNullOrWhiteSpace(this.Vid) && string.IsNullOrWhiteSpace(this.Pid)) ? string.Empty : string.Format("Vid: {0}; Pid: {1};", this.Vid, this.Pid)),
				Ext2 = ((string.IsNullOrWhiteSpace(this.Mid) && string.IsNullOrWhiteSpace(this.Cid)) ? string.Empty : string.Format("Mid: {0}; Cid: {1};", this.Mid, this.Cid)),
				Ext3 = this.FormatString(this.SalesName, 100),
				Ext4 = this.PhoneType.ToString(),
				Ext7 = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", new object[]
				{
					string.Empty,
					this.AkVersion,
					this.NewAkVersion,
					this.DownloadedBytes,
					this.PackageSizeOnServer,
					this.ResumeCounter,
					text
				}),
				Ext8 = ApplicationInfo.CurrentLanguageInRegistry.EnglishName
			};
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006D34 File Offset: 0x00004F34
		private static string PrepareForCsvFormat(string field)
		{
			bool flag = !string.IsNullOrEmpty(field);
			string text;
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder(field);
				stringBuilder.Replace(',', ';');
				stringBuilder.Replace("\r\n", " ");
				stringBuilder.Replace('\r', ' ');
				stringBuilder.Replace('\n', ' ');
				text = stringBuilder.ToString();
			}
			else
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006D9C File Offset: 0x00004F9C
		private uint UriDataToUint(UriData uriData)
		{
			return (uint)uriData;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006DB4 File Offset: 0x00004FB4
		private string FormatString(string source, int maxLength)
		{
			bool flag = string.IsNullOrEmpty(source);
			string text;
			if (flag)
			{
				text = "Unknown";
			}
			else
			{
				text = this.Truncate(source, maxLength);
			}
			return text;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006DE4 File Offset: 0x00004FE4
		private string Truncate(string source, int length)
		{
			bool flag = source.Length > length;
			if (flag)
			{
				source = source.Substring(0, length);
			}
			return source;
		}

		// Token: 0x04000052 RID: 82
		public const string REPORTVERSION = "1.1";

		// Token: 0x04000053 RID: 83
		private string actionDescription;

		// Token: 0x04000054 RID: 84
		private string akVersion;

		// Token: 0x04000055 RID: 85
		private string apiError;

		// Token: 0x04000056 RID: 86
		private string apiErrorMessage;

		// Token: 0x04000057 RID: 87
		private string applicationVersion;

		// Token: 0x04000058 RID: 88
		private string applicationVendor;

		// Token: 0x04000059 RID: 89
		private string applicationName;

		// Token: 0x0400005A RID: 90
		private string cid;

		// Token: 0x0400005B RID: 91
		private string debugField;

		// Token: 0x0400005C RID: 92
		private string firmwareGrading;

		// Token: 0x0400005D RID: 93
		private string imei;

		// Token: 0x0400005E RID: 94
		private string localPath;

		// Token: 0x0400005F RID: 95
		private string mid;

		// Token: 0x04000060 RID: 96
		private string newAkVersion;

		// Token: 0x04000061 RID: 97
		private string newSwVersion;

		// Token: 0x04000062 RID: 98
		private string pid;

		// Token: 0x04000063 RID: 99
		private string productCode;

		// Token: 0x04000064 RID: 100
		private string productType;

		// Token: 0x04000065 RID: 101
		private string reportVersion;

		// Token: 0x04000066 RID: 102
		private string salesName;

		// Token: 0x04000067 RID: 103
		private string serialNumber;

		// Token: 0x04000068 RID: 104
		private string systemInfo;

		// Token: 0x04000069 RID: 105
		private string osVersion;

		// Token: 0x0400006A RID: 106
		private string osPlatform;

		// Token: 0x0400006B RID: 107
		private long timeStamp;

		// Token: 0x0400006C RID: 108
		private string userSiteLanguage;

		// Token: 0x0400006D RID: 109
		private string countryCode;

		// Token: 0x0400006E RID: 110
		private string vid;

		// Token: 0x0400006F RID: 111
		private string downloadedBytes;

		// Token: 0x04000070 RID: 112
		private string packageSizeOnServer;

		// Token: 0x04000071 RID: 113
		private string resumeCounter;

		// Token: 0x04000072 RID: 114
		private string swVersion;
	}
}

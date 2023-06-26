using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class Report
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006E10 File Offset: 0x00005010
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00006E28 File Offset: 0x00005028
		public string DownloadedBytes
		{
			get
			{
				return this.downloadedBytes;
			}
			set
			{
				this.downloadedBytes = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00006E38 File Offset: 0x00005038
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00006E50 File Offset: 0x00005050
		public string PackageSizeOnServer
		{
			get
			{
				return this.packageSizeOnServer;
			}
			set
			{
				this.packageSizeOnServer = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006E60 File Offset: 0x00005060
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00006E78 File Offset: 0x00005078
		public string ResumeCounter
		{
			get
			{
				return this.resumeCounter;
			}
			set
			{
				this.resumeCounter = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006E88 File Offset: 0x00005088
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00006EA0 File Offset: 0x000050A0
		public string ActionDescription
		{
			get
			{
				return this.actionDescription;
			}
			set
			{
				this.actionDescription = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00006EB0 File Offset: 0x000050B0
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00006EC8 File Offset: 0x000050C8
		public string SystemInfo
		{
			get
			{
				return this.systemInfo;
			}
			set
			{
				this.systemInfo = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00006ED8 File Offset: 0x000050D8
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00006EF0 File Offset: 0x000050F0
		public string ProductType
		{
			get
			{
				return this.productType;
			}
			set
			{
				this.productType = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00006F00 File Offset: 0x00005100
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00006F18 File Offset: 0x00005118
		public string ProductCode
		{
			get
			{
				return this.productCode;
			}
			set
			{
				this.productCode = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00006F28 File Offset: 0x00005128
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00006F40 File Offset: 0x00005140
		public string Imei
		{
			get
			{
				return this.imei;
			}
			set
			{
				this.imei = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00006F50 File Offset: 0x00005150
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00006F68 File Offset: 0x00005168
		public string Vid
		{
			get
			{
				return this.vid;
			}
			set
			{
				this.vid = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00006F78 File Offset: 0x00005178
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00006F90 File Offset: 0x00005190
		public string Pid
		{
			get
			{
				return this.pid;
			}
			set
			{
				this.pid = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00006FA0 File Offset: 0x000051A0
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00006FB8 File Offset: 0x000051B8
		public string Mid
		{
			get
			{
				return this.mid;
			}
			set
			{
				this.mid = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00006FC8 File Offset: 0x000051C8
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00006FE0 File Offset: 0x000051E0
		public string Cid
		{
			get
			{
				return this.cid;
			}
			set
			{
				this.cid = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00006FF0 File Offset: 0x000051F0
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00007008 File Offset: 0x00005208
		public string SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
			set
			{
				this.serialNumber = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00007018 File Offset: 0x00005218
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00007030 File Offset: 0x00005230
		public string SalesName
		{
			get
			{
				return this.salesName;
			}
			set
			{
				this.salesName = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00007040 File Offset: 0x00005240
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00007058 File Offset: 0x00005258
		public string SwVersion
		{
			get
			{
				return this.swVersion;
			}
			set
			{
				this.swVersion = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007068 File Offset: 0x00005268
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00007080 File Offset: 0x00005280
		public string AkVersion
		{
			get
			{
				return this.akVersion;
			}
			set
			{
				this.akVersion = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00007090 File Offset: 0x00005290
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000070A8 File Offset: 0x000052A8
		public string NewAkVersion
		{
			get
			{
				return this.newAkVersion;
			}
			set
			{
				this.newAkVersion = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000070B8 File Offset: 0x000052B8
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000070D0 File Offset: 0x000052D0
		public string NewSwVersion
		{
			get
			{
				return this.newSwVersion;
			}
			set
			{
				this.newSwVersion = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000070E0 File Offset: 0x000052E0
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000070F8 File Offset: 0x000052F8
		public string LocalPath
		{
			get
			{
				return this.localPath;
			}
			set
			{
				this.localPath = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007108 File Offset: 0x00005308
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00007120 File Offset: 0x00005320
		public string DebugField
		{
			get
			{
				return this.debugField;
			}
			set
			{
				string text = Report.PrepareForCsvFormat(value);
				this.debugField = ((text.Length <= 2000) ? text : text.Substring(0, 2000));
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007158 File Offset: 0x00005358
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00007170 File Offset: 0x00005370
		public string ApiError
		{
			get
			{
				return this.apiError;
			}
			set
			{
				this.apiError = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00007180 File Offset: 0x00005380
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00007198 File Offset: 0x00005398
		public string ApiErrorMessage
		{
			get
			{
				return this.apiErrorMessage;
			}
			set
			{
				this.apiErrorMessage = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000071A8 File Offset: 0x000053A8
		// (set) Token: 0x06000193 RID: 403 RVA: 0x000071C0 File Offset: 0x000053C0
		public string ApplicationVersion
		{
			get
			{
				return this.applicationVersion;
			}
			set
			{
				this.applicationVersion = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000071D0 File Offset: 0x000053D0
		// (set) Token: 0x06000195 RID: 405 RVA: 0x000071E8 File Offset: 0x000053E8
		public string FirmwareGrading
		{
			get
			{
				return this.firmwareGrading;
			}
			set
			{
				this.firmwareGrading = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000071F8 File Offset: 0x000053F8
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00007210 File Offset: 0x00005410
		public string ReportManufacturerName
		{
			get
			{
				return this.reportManufacturerName;
			}
			set
			{
				this.reportManufacturerName = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00007220 File Offset: 0x00005420
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00007238 File Offset: 0x00005438
		public string ReportManufacturerProductLine
		{
			get
			{
				return this.reportManufacturerProductLine;
			}
			set
			{
				this.reportManufacturerProductLine = Report.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00007247 File Offset: 0x00005447
		// (set) Token: 0x0600019B RID: 411 RVA: 0x0000724F File Offset: 0x0000544F
		public long DownloadDuration { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00007258 File Offset: 0x00005458
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00007260 File Offset: 0x00005460
		public long UpdateDuration { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00007269 File Offset: 0x00005469
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00007271 File Offset: 0x00005471
		public Guid SessionId { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000727A File Offset: 0x0000547A
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00007282 File Offset: 0x00005482
		public UriData Uri { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000728B File Offset: 0x0000548B
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00007293 File Offset: 0x00005493
		public PhoneTypes PhoneType { get; set; }

		// Token: 0x060001A4 RID: 420 RVA: 0x0000729C File Offset: 0x0000549C
		public string GetReportAsCsv()
		{
			List<string> list = new List<string>
			{
				this.SystemInfo,
				this.ActionDescription,
				this.ProductType,
				this.ProductCode,
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
				this.ReportManufacturerName,
				this.ReportManufacturerProductLine
			};
			return string.Join(";", list);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007468 File Offset: 0x00005668
		public MsrReport ConvertToMsrReport()
		{
			return new MsrReport(string.Format("{0}-{1}", this.Imei, Guid.NewGuid()))
			{
				ActionDescription = this.ActionDescription,
				AkVersion = this.AkVersion,
				ApiError = this.ApiError,
				ApiErrorMessage = this.ApiErrorMessage,
				ApplicationName = string.Empty,
				ApplicationVendor = string.Empty,
				ApplicationVersion = this.ApplicationVersion,
				Cid = this.Cid,
				DebugField = this.DebugField,
				DownloadDuration = this.DownloadDuration,
				DownloadedBytes = this.DownloadedBytes,
				FirmwareGrading = this.FirmwareGrading,
				Imei = this.Imei,
				LastErrorData = string.Empty,
				LocalPath = this.LocalPath,
				Manufacturer = string.Empty,
				ManufacturerHardwareModel = this.ProductType,
				ManufacturerHardwareVariant = this.ProductCode,
				ManufacturerName = this.ReportManufacturerName,
				Mid = this.Mid,
				NewAkVersion = this.NewAkVersion,
				NewSwVersion = this.NewSwVersion,
				PackageId = string.Empty,
				PackageSizeOnServer = this.PackageSizeOnServer,
				PhoneType = this.PhoneType,
				Pid = this.Pid,
				PlatformId = string.Empty,
				ManufacturerProductLine = this.ReportManufacturerProductLine,
				ReportVersion = "1.1",
				ResumeCounter = this.ResumeCounter,
				SalesName = this.SalesName,
				SerialNumber = this.SerialNumber,
				SwVersion = this.SwVersion,
				SystemInfo = this.SystemInfo,
				TimeStamp = 0L,
				UpdateDuration = this.UpdateDuration,
				Uri = this.Uri,
				UserSiteLanguage = string.Empty
			};
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000767C File Offset: 0x0000587C
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

		// Token: 0x060001A7 RID: 423 RVA: 0x000076E4 File Offset: 0x000058E4
		private uint UriDataToUint(UriData uriData)
		{
			return (uint)uriData;
		}

		// Token: 0x0400007F RID: 127
		private string actionDescription;

		// Token: 0x04000080 RID: 128
		private string akVersion;

		// Token: 0x04000081 RID: 129
		private string apiError;

		// Token: 0x04000082 RID: 130
		private string apiErrorMessage;

		// Token: 0x04000083 RID: 131
		private string applicationVersion;

		// Token: 0x04000084 RID: 132
		private string cid;

		// Token: 0x04000085 RID: 133
		private string debugField;

		// Token: 0x04000086 RID: 134
		private string firmwareGrading;

		// Token: 0x04000087 RID: 135
		private string imei;

		// Token: 0x04000088 RID: 136
		private string localPath;

		// Token: 0x04000089 RID: 137
		private string mid;

		// Token: 0x0400008A RID: 138
		private string newAkVersion;

		// Token: 0x0400008B RID: 139
		private string newSwVersion;

		// Token: 0x0400008C RID: 140
		private string pid;

		// Token: 0x0400008D RID: 141
		private string productCode;

		// Token: 0x0400008E RID: 142
		private string productType;

		// Token: 0x0400008F RID: 143
		private string salesName;

		// Token: 0x04000090 RID: 144
		private string serialNumber;

		// Token: 0x04000091 RID: 145
		private string systemInfo;

		// Token: 0x04000092 RID: 146
		private string vid;

		// Token: 0x04000093 RID: 147
		private string reportManufacturerName;

		// Token: 0x04000094 RID: 148
		private string reportManufacturerProductLine;

		// Token: 0x04000095 RID: 149
		private string downloadedBytes;

		// Token: 0x04000096 RID: 150
		private string packageSizeOnServer;

		// Token: 0x04000097 RID: 151
		private string resumeCounter;

		// Token: 0x04000098 RID: 152
		private string swVersion;
	}
}

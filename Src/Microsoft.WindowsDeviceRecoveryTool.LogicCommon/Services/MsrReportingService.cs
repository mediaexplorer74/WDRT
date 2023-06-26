using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Contracts;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Msr;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Interfaces;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x0200000F RID: 15
	[Export(typeof(IUseProxy))]
	[Export(typeof(MsrReportingService))]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class MsrReportingService : IUseProxy
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00003B56 File Offset: 0x00001D56
		[ImportingConstructor]
		public MsrReportingService()
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003B6B File Offset: 0x00001D6B
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003B73 File Offset: 0x00001D73
		public IManufacturerDataProvider ManufacturerDataProvider { get; set; }

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060000A7 RID: 167 RVA: 0x00003B7C File Offset: 0x00001D7C
		// (remove) Token: 0x060000A8 RID: 168 RVA: 0x00003BB4 File Offset: 0x00001DB4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action SessionReportsSendingCompleted;

		// Token: 0x060000A9 RID: 169 RVA: 0x00003BEC File Offset: 0x00001DEC
		private void Initialize()
		{
			this.msrServiceData = MsrServiceData.CreateServiceData();
			MsrReporting msrReporting = new MsrReporting(this);
			this.msrReportSender = new MsrReportSender(msrReporting);
			this.msrReportSender.SendOldReports();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003C24 File Offset: 0x00001E24
		public async Task SendReportAsync(IReport report)
		{
			bool sent = report.Sent;
			if (!sent)
			{
				string text = await this.ProvideReportUploadUrlAsync(report, null);
				string uploadUrl = text;
				text = null;
				if (!string.IsNullOrWhiteSpace(uploadUrl))
				{
					bool flag = await this.UploadWithHttpClientAsync(uploadUrl, report);
					if (flag)
					{
						report.MarkAsSent();
					}
				}
				else
				{
					Tracer<MsrReportingService>.WriteWarning("MSR Reporting bad request parameters, error 400", new object[0]);
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003C70 File Offset: 0x00001E70
		public void OperationStarted(Phone phone, ReportOperationType reportOperationType)
		{
			ReportData reportData = this.GetReportData(phone, reportOperationType, reportOperationType == ReportOperationType.DownloadPackage);
			if (reportOperationType > ReportOperationType.Recovery)
			{
				if (reportOperationType == ReportOperationType.DownloadPackage)
				{
					reportData.StartDownloadTimer();
					return;
				}
				if (reportOperationType != ReportOperationType.RecoveryAfterEmergencyFlashing)
				{
					return;
				}
			}
			reportData.StartUpdateTimer();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public void OperationSucceded(Phone phone, ReportOperationType reportOperationType, UriData uriData)
		{
			Tracer<MsrReportingService>.LogEntry("OperationSucceded");
			Tracer<MsrReportingService>.WriteInformation("Operation: {0} succeeded", new object[] { reportOperationType });
			ReportData reportData = this.GetReportData(phone, reportOperationType, false);
			reportData.SetPhoneInfo(phone);
			this.SendReport(reportData);
			this.RemoveReportData(phone, reportOperationType);
			Tracer<MsrReportingService>.LogExit("OperationSucceded");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003D18 File Offset: 0x00001F18
		public void OperationSucceded(Phone phone, ReportOperationType reportOperationType)
		{
			Tracer<MsrReportingService>.LogEntry("OperationSucceded");
			Tracer<MsrReportingService>.WriteInformation("Operation: {0} succeeded", new object[] { reportOperationType });
			ReportData reportData = this.GetReportData(phone, reportOperationType, false);
			switch (reportOperationType)
			{
			case ReportOperationType.Flashing:
				this.SendReport(reportData, UriData.FirmwareUpdateSuccessful);
				goto IL_E6;
			case ReportOperationType.Recovery:
				this.SendReport(reportData, UriData.DeadPhoneRecovered);
				goto IL_E6;
			case ReportOperationType.DownloadPackage:
			{
				bool flag = reportData.DownloadedBytes != 0L;
				if (flag)
				{
					this.SendReport(reportData, UriData.DownloadPackageSuccess);
				}
				else
				{
					this.msrReportSender.RemoveLocalReport(reportData.LocalPath);
				}
				goto IL_E6;
			}
			case ReportOperationType.EmergencyFlashing:
				reportData.SetPhoneInfo(phone);
				this.SendReport(reportData, UriData.EmergencyFlashingSuccesfullyFinished);
				goto IL_E6;
			case ReportOperationType.RecoveryAfterEmergencyFlashing:
				this.SendReport(reportData, UriData.DeadPhoneRecoveredAfterEmergencyFlashing);
				goto IL_E6;
			}
			this.msrReportSender.RemoveLocalReport(reportData.LocalPath);
			IL_E6:
			this.RemoveReportData(phone, reportOperationType);
			Tracer<MsrReportingService>.LogExit("OperationSucceded");
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003E1F File Offset: 0x0000201F
		public void SurveySucceded(SurveyReport survey)
		{
			survey.SessionId = this.GetSessionId();
			this.msrReportSender.SendReport(survey, ApplicationInfo.IsInternal());
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003E44 File Offset: 0x00002044
		public void PartialOperationSucceded(Phone phone, ReportOperationType reportOperationType, UriData uriData)
		{
			Tracer<MsrReportingService>.LogEntry("PartialOperationSucceded");
			Tracer<MsrReportingService>.WriteInformation("Operation: {0} succeeded", new object[] { reportOperationType });
			bool flag = reportOperationType == ReportOperationType.EmergencyFlashing;
			if (flag)
			{
				this.UpdateReportWithImeiNumber(phone, reportOperationType);
			}
			ReportData reportData = this.GetReportData(phone, reportOperationType, false);
			reportData.SetPhoneInfo(phone);
			reportData.SetResult(uriData, null);
			Tracer<MsrReportingService>.LogExit("PartialOperationSucceded");
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003EB0 File Offset: 0x000020B0
		public void OperationFailed(Phone phone, ReportOperationType reportOperationType, UriData resultUriData, Exception ex)
		{
			ReportData reportData = this.GetReportData(phone, reportOperationType, false);
			bool flag = ex != null;
			if (flag)
			{
				ex = ex.GetBaseException();
			}
			reportData.SetResult(resultUriData, ex);
			this.msrReportSender.SaveLocalReport(reportData);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003EF4 File Offset: 0x000020F4
		public void SetDownloadByteInformation(Phone phone, ReportOperationType reportOperationType, long currentDownloadedSize, long packageSize, bool isResumed)
		{
			ReportData reportData = this.GetReportData(phone, reportOperationType, isResumed);
			reportData.PackageSize = packageSize;
			reportData.DownloadedBytes = currentDownloadedSize;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003F20 File Offset: 0x00002120
		public void SendSessionReports()
		{
			this.msrReportSender.SessionReportsSendingCompleted += this.SessionReportsSendingCompleted;
			Dictionary<string, ReportData> dictionary = this.msrReports;
			lock (dictionary)
			{
				this.msrReportSender.SendSessionReports(this.msrReports.Values.ToList<ReportData>(), ApplicationInfo.IsInternal());
				this.msrReports.Clear();
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003FA0 File Offset: 0x000021A0
		public void StartFlowSession()
		{
			this.currentSessionId = Guid.NewGuid().ToString();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003FC8 File Offset: 0x000021C8
		private async Task<string> ProvideReportUploadUrlAsync(IReport report, ManufacturerInfo manufacturerInfo = null)
		{
			string result = null;
			HttpClientHandler httpClientHandler = new HttpClientHandler
			{
				Proxy = this.Proxy(),
				UseDefaultCredentials = false
			};
			using (HttpClient client = new HttpClient(httpClientHandler))
			{
				this.AddDefaultHeaders(client);
				DateTime time = DateTime.UtcNow;
				RequestBody requestBody = new RequestBody
				{
					ManufacturerName = ((manufacturerInfo != null) ? manufacturerInfo.ReportManufacturerName : report.ManufacturerName),
					ManufacturerProductLine = ((manufacturerInfo != null) ? manufacturerInfo.ReportProductLine : report.ManufacturerProductLine),
					ReportClassification = "Public",
					FileName = string.Format("{0:yyyyMMdd}{0:HHmmss_ff}_{1}_{2}_{3}{4}.xml", new object[]
					{
						time,
						report.ManufacturerHardwareModel,
						report.ManufacturerHardwareVariant,
						report.Imei,
						(report is SurveyReport) ? "_Survey" : string.Empty
					})
				};
				string postBody = requestBody.ToJsonString();
				HttpResponseMessage httpResponseMessage = await client.PostAsync(new Uri(this.msrServiceData.UploadApiUrl), new StringContent(postBody, Encoding.UTF8, "application/json"), CancellationToken.None);
				HttpResponseMessage response = httpResponseMessage;
				httpResponseMessage = null;
				try
				{
					if (response.IsSuccessStatusCode)
					{
						result = response.Headers.Location.AbsoluteUri;
					}
					else if (manufacturerInfo == null)
					{
						if (this.ManufacturerDataProvider == null)
						{
							Tracer<MsrReportingService>.WriteWarning("ManufacturerDataProvider value was not set", new object[0]);
							throw new HttpRequestException(string.Format("Could not provide reporting upload url. Status: {0}", response.StatusCode));
						}
						Tracer<MsrReportingService>.WriteWarning("Could not provide report upload url using params: {0}", new object[] { postBody });
						Tracer<MsrReportingService>.WriteInformation("Try use manufacturer extracted data for getting upload url");
						ManufacturerInfo minfo = this.ManufacturerDataProvider.GetAdaptationsData().FirstOrDefault((ManufacturerInfo mi) => mi.Type == report.PhoneType);
						if (minfo != null)
						{
							string text = await this.ProvideReportUploadUrlAsync(report, minfo);
							result = text;
							text = null;
						}
						else
						{
							Tracer<MsrReportingService>.WriteWarning("No manufacturer for reported type '{0}' found", new object[] { report.PhoneType });
						}
						minfo = null;
					}
				}
				finally
				{
					if (response != null)
					{
						((IDisposable)response).Dispose();
					}
				}
				response = null;
				requestBody = null;
				postBody = null;
			}
			HttpClient client = null;
			return result;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000401C File Offset: 0x0000221C
		private async Task<bool> UploadWithHttpClientAsync(string reportFileUri, IReport report)
		{
			HttpClientHandler httpClientHandler = new HttpClientHandler
			{
				Proxy = this.Proxy(),
				UseDefaultCredentials = false
			};
			bool isSuccessStatusCode;
			using (HttpClient client = new HttpClient(httpClientHandler))
			{
				string dateInRfc1123Format = DateTime.UtcNow.ToString("R");
				StringContent content = new StringContent(report.GetReportAsXml());
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("x-ms-blob-type", BlobType.BlockBlob.ToString());
				client.DefaultRequestHeaders.Add("x-ms-date", dateInRfc1123Format);
				HttpResponseMessage httpResponseMessage = await client.PutAsync(reportFileUri, content);
				HttpResponseMessage reqResult = httpResponseMessage;
				httpResponseMessage = null;
				isSuccessStatusCode = reqResult.IsSuccessStatusCode;
			}
			return isSuccessStatusCode;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004070 File Offset: 0x00002270
		private async Task UploadWithHttpClientAsync(string reportFileUri, string content)
		{
			HttpClientHandler httpClientHandler = new HttpClientHandler
			{
				Proxy = this.Proxy(),
				UseDefaultCredentials = false
			};
			using (HttpClient client = new HttpClient(httpClientHandler))
			{
				string dateInRfc1123Format = DateTime.UtcNow.ToString("R");
				StringContent stringContent = new StringContent(content);
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("x-ms-blob-type", BlobType.BlockBlob.ToString());
				client.DefaultRequestHeaders.Add("x-ms-date", dateInRfc1123Format);
				await client.PutAsync(reportFileUri, stringContent);
				dateInRfc1123Format = null;
				stringContent = null;
			}
			HttpClient client = null;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000040C2 File Offset: 0x000022C2
		private void SendReport(ReportData reportData, UriData reportResultUriData)
		{
			reportData.SetResult(reportResultUriData, null);
			this.SendReport(reportData);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000040D6 File Offset: 0x000022D6
		private void SendReport(ReportData reportData)
		{
			reportData.EndDataCollecting();
			this.msrReportSender.SendReport(reportData, ApplicationInfo.IsInternal());
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000040F4 File Offset: 0x000022F4
		private ReportData StartDataCollecting(string description, Phone phone)
		{
			ReportData reportData = new ReportData(description, this.GetSessionId());
			reportData.SetPhoneInfo(phone);
			return reportData;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000411C File Offset: 0x0000231C
		private ReportData GetReportData(Phone phone, ReportOperationType reportOperationType, bool resumeCounter = false)
		{
			string text = string.Format("{0}_{1}", phone.Imei, reportOperationType);
			Dictionary<string, ReportData> dictionary = this.msrReports;
			ReportData reportData2;
			lock (dictionary)
			{
				bool flag2 = this.msrReports.ContainsKey(text);
				if (flag2)
				{
					Tracer<MsrReportingService>.WriteInformation("Getting existing report from dictionary");
					if (resumeCounter)
					{
						ReportData reportData = this.msrReports[text];
						int resumeCounter2 = reportData.ResumeCounter;
						reportData.ResumeCounter = resumeCounter2 + 1;
						bool flag3 = this.msrReports[text].Exception != null;
						if (flag3)
						{
							bool flag4 = this.msrReports[text].LastError == null;
							if (flag4)
							{
								this.msrReports[text].LastError = this.msrReports[text].Exception;
							}
						}
					}
					reportData2 = this.msrReports[text];
				}
				else
				{
					Tracer<MsrReportingService>.WriteInformation("Create new report and add it to dictionary");
					ReportData reportData3 = this.StartDataCollecting(reportOperationType.ToString(), phone);
					this.msrReports.Add(text, reportData3);
					reportData2 = this.msrReports[text];
				}
			}
			return reportData2;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004264 File Offset: 0x00002464
		private void RemoveReportData(Phone phone, ReportOperationType reportOperationType)
		{
			string text = string.Format("{0}_{1}", phone.Imei, reportOperationType);
			Dictionary<string, ReportData> dictionary = this.msrReports;
			lock (dictionary)
			{
				bool flag2 = this.msrReports.ContainsKey(text);
				if (flag2)
				{
					this.msrReports.Remove(text);
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000042DC File Offset: 0x000024DC
		private void UpdateReportWithImeiNumber(Phone phone, ReportOperationType reportOperationType)
		{
			string text = string.Format("{0}_{1}", null, reportOperationType);
			string text2 = string.Format("{0}_{1}", phone.Imei, reportOperationType);
			Dictionary<string, ReportData> dictionary = this.msrReports;
			lock (dictionary)
			{
				bool flag2 = this.msrReports.ContainsKey(text);
				if (flag2)
				{
					ReportData reportData = this.msrReports[text];
					this.msrReports[text2] = reportData;
					this.msrReports.Remove(text);
				}
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004384 File Offset: 0x00002584
		private UriData GetDefaultFailUriData(ReportOperationType reportOperationType)
		{
			switch (reportOperationType)
			{
			case ReportOperationType.Flashing:
				return UriData.ProgrammingPhoneFailed;
			case ReportOperationType.Recovery:
				return UriData.DeadPhoneRecoveryFailed;
			case ReportOperationType.ReadDeviceInfo:
			case ReportOperationType.ReadDeviceInfoWithThor:
				break;
			case ReportOperationType.DownloadPackage:
				return UriData.FailedToDownloadVariantPackage;
			default:
				if (reportOperationType == ReportOperationType.RecoveryAfterEmergencyFlashing)
				{
					return UriData.RecoveryAfterEmergencyFlashingFailed;
				}
				break;
			}
			return UriData.InvalidUriCode;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000043D8 File Offset: 0x000025D8
		private void AddDefaultHeaders(HttpClient client)
		{
			client.Timeout = Timeout.InfiniteTimeSpan;
			client.DefaultRequestHeaders.UserAgent.TryParseAdd(this.msrServiceData.UserAgent);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000442C File Offset: 0x0000262C
		private string GetSessionId()
		{
			bool flag = string.IsNullOrEmpty(this.currentSessionId) || string.Equals(this.currentSessionId, Guid.Empty.ToString());
			if (flag)
			{
				this.StartFlowSession();
			}
			return this.currentSessionId;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000447F File Offset: 0x0000267F
		public void SetProxy(IWebProxy settings)
		{
			this.proxySettings = settings;
			this.Initialize();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004490 File Offset: 0x00002690
		private IWebProxy Proxy()
		{
			return this.proxySettings ?? WebRequest.GetSystemWebProxy();
		}

		// Token: 0x04000036 RID: 54
		private const string JsonContentType = "application/json";

		// Token: 0x04000037 RID: 55
		private readonly Dictionary<string, ReportData> msrReports = new Dictionary<string, ReportData>();

		// Token: 0x04000038 RID: 56
		private MsrReportSender msrReportSender;

		// Token: 0x04000039 RID: 57
		private MsrServiceData msrServiceData;

		// Token: 0x0400003A RID: 58
		private IWebProxy proxySettings;

		// Token: 0x0400003B RID: 59
		private string currentSessionId;
	}
}

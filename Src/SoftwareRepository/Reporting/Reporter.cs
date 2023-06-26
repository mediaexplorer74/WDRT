using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace SoftwareRepository.Reporting
{
	// Token: 0x0200001B RID: 27
	public class Reporter
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003F1A File Offset: 0x0000211A
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003F22 File Offset: 0x00002122
		[SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
		public string SoftwareRepositoryAlternativeBaseUrl { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003F2B File Offset: 0x0000212B
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003F33 File Offset: 0x00002133
		public string SoftwareRepositoryAuthenticationToken { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003F3C File Offset: 0x0000213C
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003F44 File Offset: 0x00002144
		public IWebProxy SoftwareRepositoryProxy { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003F4D File Offset: 0x0000214D
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003F55 File Offset: 0x00002155
		public string SoftwareRepositoryUserAgent { get; set; }

		// Token: 0x060000BE RID: 190 RVA: 0x00003F60 File Offset: 0x00002160
		public async Task<string> GetReportUploadLocationAsync(string manufacturerName, string manufacturerProductLine, string reportClassification, string fileName, CancellationToken cancellationToken)
		{
			string ret = string.Empty;
			ReportUploadLocationParameters reportUploadLocationParameters = new ReportUploadLocationParameters
			{
				ManufacturerName = manufacturerName,
				ManufacturerProductLine = manufacturerProductLine,
				ReportClassification = reportClassification,
				FileName = fileName
			};
			try
			{
				DataContractJsonSerializer contractJsonSerializer = new DataContractJsonSerializer(typeof(ReportUploadLocationParameters));
				MemoryStream memoryStream = new MemoryStream();
				contractJsonSerializer.WriteObject(memoryStream, reportUploadLocationParameters);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				StreamReader reader = new StreamReader(memoryStream);
				string postBody = reader.ReadToEnd();
				string baseUrl = "https://api.swrepository.com";
				bool flag = this.SoftwareRepositoryAlternativeBaseUrl != null;
				if (flag)
				{
					baseUrl = this.SoftwareRepositoryAlternativeBaseUrl;
				}
				Uri uri = new Uri(baseUrl + "/rest-api/report/1/uploadlocation");
				string userAgent = "SoftwareRepository";
				bool flag2 = this.SoftwareRepositoryUserAgent != null;
				if (flag2)
				{
					userAgent = this.SoftwareRepositoryUserAgent;
				}
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				HttpClient httpClient = null;
				bool flag3 = this.SoftwareRepositoryProxy != null;
				if (flag3)
				{
					HttpClientHandler handler = new HttpClientHandler();
					handler.Proxy = this.SoftwareRepositoryProxy;
					handler.UseProxy = true;
					httpClient = new HttpClient(handler);
					handler = null;
				}
				else
				{
					httpClient = new HttpClient();
				}
				httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd(userAgent);
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				bool flag4 = this.SoftwareRepositoryAuthenticationToken != null;
				if (flag4)
				{
					httpClient.DefaultRequestHeaders.Add("X-Authentication", this.SoftwareRepositoryAuthenticationToken);
					httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.SoftwareRepositoryAuthenticationToken);
				}
				HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uri, new StringContent(postBody, Encoding.UTF8, "application/json"), cancellationToken);
				HttpResponseMessage responseMsg = httpResponseMessage;
				httpResponseMessage = null;
				responseMsg.EnsureSuccessStatusCode();
				try
				{
					ret = responseMsg.Headers.First((KeyValuePair<string, IEnumerable<string>> h) => h.Key.Equals("X-Upload-Location")).Value.First<string>();
				}
				catch (InvalidOperationException)
				{
					if (responseMsg.Headers.Location != null)
					{
						ret = responseMsg.Headers.Location.AbsoluteUri;
					}
				}
				httpClient.Dispose();
				contractJsonSerializer = null;
				memoryStream = null;
				reader = null;
				postBody = null;
				baseUrl = null;
				uri = null;
				userAgent = null;
				httpClient = null;
				responseMsg = null;
			}
			catch (Exception ex)
			{
				throw new ReportException("Report exception", ex);
			}
			return ret;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003FCC File Offset: 0x000021CC
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public async Task<bool> UploadReportAsync(string manufacturerName, string manufacturerProductLine, string reportClassification, List<string> filePaths, CancellationToken cancellationToken)
		{
			try
			{
				foreach (string filePath in filePaths)
				{
					string fileName = Path.GetFileName(filePath);
					string text = await this.GetReportUploadLocationAsync(manufacturerName, manufacturerProductLine, reportClassification, fileName, cancellationToken);
					string uploadLocation = text;
					text = null;
					CloudBlockBlob blockBlob = new CloudBlockBlob(new Uri(uploadLocation));
					await blockBlob.UploadFromFileAsync(filePath, FileMode.Open);
					fileName = null;
					uploadLocation = null;
					blockBlob = null;
					filePath = null;
				}
				List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			}
			catch (Exception ex)
			{
				throw new ReportException("Cannot upload report.", ex);
			}
			return true;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004038 File Offset: 0x00002238
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "cancellationToken")]
		internal async Task SendDownloadReport(string id, string filename, List<string> url, int status, long time, long size, int connections, CancellationToken cancellationToken)
		{
			DownloadReport report = new DownloadReport
			{
				ApiVersion = "1",
				Id = id,
				FileName = filename,
				Url = url,
				Status = status,
				Time = time,
				Size = size,
				Connections = connections
			};
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DownloadReport));
			MemoryStream ms = new MemoryStream();
			serializer.WriteObject(ms, report);
			ms.Seek(0L, SeekOrigin.Begin);
			StreamReader reader = new StreamReader(ms);
			string postBody = reader.ReadToEnd();
			string baseUrl = "https://api.swrepository.com";
			bool flag = this.SoftwareRepositoryAlternativeBaseUrl != null;
			if (flag)
			{
				baseUrl = this.SoftwareRepositoryAlternativeBaseUrl;
			}
			string userAgent = "SoftwareRepository";
			bool flag2 = this.SoftwareRepositoryUserAgent != null;
			if (flag2)
			{
				userAgent = this.SoftwareRepositoryUserAgent;
			}
			Uri uri = new Uri(baseUrl + "/rest-api/discovery/1/report");
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			HttpClient httpClient = null;
			bool flag3 = this.SoftwareRepositoryProxy != null;
			if (flag3)
			{
				HttpClientHandler handler = new HttpClientHandler();
				handler.Proxy = this.SoftwareRepositoryProxy;
				handler.UseProxy = true;
				httpClient = new HttpClient(handler);
				handler = null;
			}
			else
			{
				httpClient = new HttpClient();
			}
			httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd(userAgent);
			HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uri, new StringContent(postBody, Encoding.UTF8, "application/json"));
			HttpResponseMessage responseMsg = httpResponseMessage;
			httpResponseMessage = null;
			if (responseMsg.StatusCode != HttpStatusCode.OK)
			{
				if (responseMsg.StatusCode != HttpStatusCode.BadRequest)
				{
					if (responseMsg.StatusCode == HttpStatusCode.Forbidden)
					{
					}
				}
			}
			httpClient.Dispose();
		}

		// Token: 0x0400006D RID: 109
		private const string DefaultSoftwareRepositoryBaseUrl = "https://api.swrepository.com";

		// Token: 0x0400006E RID: 110
		private const string DefaultSoftwareRepositoryDownloadReportUrl = "/rest-api/discovery/1/report";

		// Token: 0x0400006F RID: 111
		private const string DefaultSoftwareRepositoryUploadReport = "/rest-api/report";

		// Token: 0x04000070 RID: 112
		private const string DefaultSoftwareRepositoryReportUploadApiVersion = "/1";

		// Token: 0x04000071 RID: 113
		private const string DefaultSoftwareRepositoryReportUploadApi = "/uploadlocation";

		// Token: 0x04000072 RID: 114
		private const string DefaultSoftwareRepositoryUserAgent = "SoftwareRepository";
	}
}

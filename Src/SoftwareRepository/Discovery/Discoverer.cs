using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoftwareRepository.Discovery
{
	// Token: 0x0200001F RID: 31
	public class Discoverer
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004141 File Offset: 0x00002341
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004149 File Offset: 0x00002349
		public DiscoveryCondition DiscoveryCondition { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004152 File Offset: 0x00002352
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000415A File Offset: 0x0000235A
		[SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
		public string SoftwareRepositoryAlternativeBaseUrl { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004163 File Offset: 0x00002363
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000416B File Offset: 0x0000236B
		public string SoftwareRepositoryAuthenticationToken { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004174 File Offset: 0x00002374
		// (set) Token: 0x060000DA RID: 218 RVA: 0x0000417C File Offset: 0x0000237C
		public IWebProxy SoftwareRepositoryProxy { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004185 File Offset: 0x00002385
		// (set) Token: 0x060000DC RID: 220 RVA: 0x0000418D File Offset: 0x0000238D
		public string SoftwareRepositoryUserAgent { get; set; }

		// Token: 0x060000DD RID: 221 RVA: 0x00004198 File Offset: 0x00002398
		public async Task<DiscoveryResult> DiscoverAsync(string descriptor)
		{
			return await this.DiscoverAsync(descriptor, CancellationToken.None);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000041E4 File Offset: 0x000023E4
		public async Task<DiscoveryJsonResult> DiscoverJsonAsync(string descriptor)
		{
			return await this.DiscoverJsonAsync(descriptor, CancellationToken.None);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004230 File Offset: 0x00002430
		public async Task<DiscoveryResult> DiscoverAsync(string descriptor, CancellationToken cancellationToken)
		{
			DiscoveryResult discoveryResult = new DiscoveryResult();
			DiscoveryJsonResult discoveryJsonResult = await this.DiscoverJsonAsync(descriptor, cancellationToken);
			DiscoveryJsonResult jsonResult = discoveryJsonResult;
			discoveryJsonResult = null;
			discoveryResult.StatusCode = jsonResult.StatusCode;
			if (discoveryResult.StatusCode == HttpStatusCode.OK)
			{
				DataContractJsonSerializer contractJsonSerializer = new DataContractJsonSerializer(typeof(SoftwarePackages));
				MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonResult.Result));
				discoveryResult.Result = (SoftwarePackages)contractJsonSerializer.ReadObject(memoryStream);
				contractJsonSerializer = null;
				memoryStream = null;
			}
			return discoveryResult;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004284 File Offset: 0x00002484
		public async Task<DiscoveryJsonResult> DiscoverJsonAsync(string descriptor, CancellationToken cancellationToken)
		{
			DiscoveryJsonResult discoveryResult = new DiscoveryJsonResult();
			DiscoveryParameters discoveryParameters = null;
			try
			{
				DataContractJsonSerializer contractJsonSerializer = new DataContractJsonSerializer(typeof(DiscoveryParameters));
				MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(descriptor));
				discoveryParameters = (DiscoveryParameters)contractJsonSerializer.ReadObject(memoryStream);
				bool flag = discoveryParameters.APIVersion == null;
				if (flag)
				{
					discoveryParameters.APIVersion = "1";
				}
				bool flag2 = discoveryParameters.Condition == null;
				if (flag2)
				{
					discoveryParameters.Condition = new List<string>();
					discoveryParameters.Condition.Add("default");
				}
				bool flag3 = discoveryParameters.Response == null || discoveryParameters.Response.Count == 0;
				if (flag3)
				{
					discoveryParameters.Response = new List<string>();
					discoveryParameters.Response.Add("default");
				}
				DiscoveryJsonResult discoveryJsonResult = await this.DiscoverJsonAsync(discoveryParameters, cancellationToken);
				discoveryResult = discoveryJsonResult;
				discoveryJsonResult = null;
				contractJsonSerializer = null;
				memoryStream = null;
			}
			catch (Exception ex)
			{
				throw new DiscoveryException("Discovery exception", ex);
			}
			return discoveryResult;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000042D8 File Offset: 0x000024D8
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public async Task<DiscoveryResult> DiscoverAsync(string manufacturerName, string manufacturerProductLine, string packageType, string packageClass, [Optional] string packageTitle, [Optional] string packageSubtitle, [Optional] string packageRevision, [Optional] string packageSubRevision, [Optional] string packageState, [Optional] string manufacturerPackageId, [Optional] string manufacturerModelName, [Optional] string manufacturerVariantName, [Optional] string manufacturerPlatformId, [Optional] string manufacturerHardwareModel, [Optional] string manufacturerHardwareVariant, [Optional] string operatorName, [Optional] string customerName, [Optional] Dictionary<string, string> extendedAttributes, [Optional] List<string> responseFilter, [Optional] CancellationToken cancellationToken)
		{
			ExtendedAttributes extendedAttributesParam = null;
			bool flag = extendedAttributes != null && extendedAttributes.Count > 0;
			if (flag)
			{
				extendedAttributesParam = new ExtendedAttributes
				{
					Dictionary = extendedAttributes
				};
			}
			DiscoveryQueryParameters discoveryQueryParameters = new DiscoveryQueryParameters
			{
				ManufacturerName = manufacturerName,
				ManufacturerProductLine = manufacturerProductLine,
				PackageType = packageType,
				PackageClass = packageClass,
				PackageTitle = packageTitle,
				PackageSubtitle = packageSubtitle,
				PackageRevision = packageRevision,
				PackageSubRevision = packageSubRevision,
				PackageState = packageState,
				ManufacturerPackageId = manufacturerPackageId,
				ManufacturerModelName = manufacturerModelName,
				ManufacturerVariantName = manufacturerVariantName,
				ManufacturerPlatformId = manufacturerPlatformId,
				ManufacturerHardwareModel = manufacturerHardwareModel,
				ManufacturerHardwareVariant = manufacturerHardwareVariant,
				OperatorName = operatorName,
				CustomerName = customerName,
				ExtendedAttributes = extendedAttributesParam
			};
			DiscoveryParameters discoveryParameters = new DiscoveryParameters(this.DiscoveryCondition)
			{
				Query = discoveryQueryParameters
			};
			bool flag2 = responseFilter != null && responseFilter.Count > 0;
			if (flag2)
			{
				discoveryParameters.Response = responseFilter;
			}
			return await this.DiscoverAsync(discoveryParameters, cancellationToken);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000043BC File Offset: 0x000025BC
		public async Task<DiscoveryResult> DiscoverAsync(DiscoveryParameters discoveryParameters)
		{
			bool flag = discoveryParameters.Response == null || discoveryParameters.Response.Count == 0;
			if (flag)
			{
				discoveryParameters.Response = new List<string>();
				discoveryParameters.Response.Add("default");
			}
			return await this.DiscoverAsync(discoveryParameters, CancellationToken.None);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004408 File Offset: 0x00002608
		public async Task<DiscoveryResult> DiscoverAsync(DiscoveryParameters discoveryParameters, CancellationToken cancellationToken)
		{
			DiscoveryResult discoveryResult = new DiscoveryResult();
			DiscoveryJsonResult discoveryJsonResult = await this.DiscoverJsonAsync(discoveryParameters, cancellationToken);
			DiscoveryJsonResult jsonResult = discoveryJsonResult;
			discoveryJsonResult = null;
			discoveryResult.StatusCode = jsonResult.StatusCode;
			if (discoveryResult.StatusCode == HttpStatusCode.OK)
			{
				DataContractJsonSerializer contractJsonSerializer = new DataContractJsonSerializer(typeof(SoftwarePackages));
				MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonResult.Result));
				discoveryResult.Result = (SoftwarePackages)contractJsonSerializer.ReadObject(memoryStream);
				contractJsonSerializer = null;
				memoryStream = null;
			}
			return discoveryResult;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000445C File Offset: 0x0000265C
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public async Task<DiscoveryJsonResult> DiscoverJsonAsync(string manufacturerName, string manufacturerProductLine, string packageType, string packageClass, [Optional] string packageTitle, [Optional] string packageSubtitle, [Optional] string packageRevision, [Optional] string packageSubRevision, [Optional] string packageState, [Optional] string manufacturerPackageId, [Optional] string manufacturerModelName, [Optional] string manufacturerVariantName, [Optional] string manufacturerPlatformId, [Optional] string manufacturerHardwareModel, [Optional] string manufacturerHardwareVariant, [Optional] string operatorName, [Optional] string customerName, [Optional] Dictionary<string, string> extendedAttributes, [Optional] List<string> responseFilter, [Optional] CancellationToken cancellationToken)
		{
			ExtendedAttributes extendedAttributesParam = null;
			bool flag = extendedAttributes != null && extendedAttributes.Count > 0;
			if (flag)
			{
				extendedAttributesParam = new ExtendedAttributes
				{
					Dictionary = extendedAttributes
				};
			}
			DiscoveryQueryParameters discoveryQueryParameters = new DiscoveryQueryParameters
			{
				ManufacturerName = manufacturerName,
				ManufacturerProductLine = manufacturerProductLine,
				PackageType = packageType,
				PackageClass = packageClass,
				PackageTitle = packageTitle,
				PackageSubtitle = packageSubtitle,
				PackageRevision = packageRevision,
				PackageSubRevision = packageSubRevision,
				PackageState = packageState,
				ManufacturerPackageId = manufacturerPackageId,
				ManufacturerModelName = manufacturerModelName,
				ManufacturerVariantName = manufacturerVariantName,
				ManufacturerPlatformId = manufacturerPlatformId,
				ManufacturerHardwareModel = manufacturerHardwareModel,
				ManufacturerHardwareVariant = manufacturerHardwareVariant,
				OperatorName = operatorName,
				CustomerName = customerName,
				ExtendedAttributes = extendedAttributesParam
			};
			DiscoveryParameters discoveryParameters = new DiscoveryParameters(this.DiscoveryCondition)
			{
				Query = discoveryQueryParameters
			};
			bool flag2 = responseFilter != null && responseFilter.Count > 0;
			if (flag2)
			{
				discoveryParameters.Response = responseFilter;
			}
			return await this.DiscoverJsonAsync(discoveryParameters, cancellationToken);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004540 File Offset: 0x00002740
		public async Task<DiscoveryJsonResult> DiscoverJsonAsync(DiscoveryParameters discoveryParameters)
		{
			return await this.DiscoverJsonAsync(discoveryParameters, CancellationToken.None);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000458C File Offset: 0x0000278C
		public async Task<DiscoveryJsonResult> DiscoverJsonAsync(DiscoveryParameters discoveryParameters, CancellationToken cancellationToken)
		{
			DiscoveryJsonResult discoveryJsonResult = new DiscoveryJsonResult();
			try
			{
				DataContractJsonSerializer contractJsonSerializer = new DataContractJsonSerializer(typeof(DiscoveryParameters));
				MemoryStream memoryStream = new MemoryStream();
				contractJsonSerializer.WriteObject(memoryStream, discoveryParameters);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				StreamReader reader = new StreamReader(memoryStream);
				string postBody = reader.ReadToEnd();
				string baseUrl = "https://api.swrepository.com";
				bool flag = this.SoftwareRepositoryAlternativeBaseUrl != null;
				if (flag)
				{
					baseUrl = this.SoftwareRepositoryAlternativeBaseUrl;
				}
				Uri uri = new Uri(baseUrl + "/rest-api/discovery/1/package");
				string userAgent = "SoftwareRepository";
				bool flag2 = this.SoftwareRepositoryUserAgent != null;
				if (flag2)
				{
					userAgent = this.SoftwareRepositoryUserAgent;
				}
				HttpClient httpClient = null;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
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
				discoveryJsonResult.StatusCode = responseMsg.StatusCode;
				if (discoveryJsonResult.StatusCode == HttpStatusCode.OK)
				{
					HttpContent result = responseMsg.Content;
					DiscoveryJsonResult discoveryJsonResult2 = discoveryJsonResult;
					string text = await result.ReadAsStringAsync();
					discoveryJsonResult2.Result = text;
					discoveryJsonResult2 = null;
					text = null;
					result = null;
				}
				else
				{
					discoveryJsonResult.Result = string.Empty;
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
				throw new DiscoveryException("Discovery exception", ex);
			}
			return discoveryJsonResult;
		}

		// Token: 0x04000086 RID: 134
		private const string DefaultSoftwareRepositoryBaseUrl = "https://api.swrepository.com";

		// Token: 0x04000087 RID: 135
		private const string DefaultSoftwareRepositoryDiscovery = "/rest-api/discovery/1/package";

		// Token: 0x04000088 RID: 136
		private const string DefaultSoftwareRepositoryUserAgent = "SoftwareRepository";
	}
}

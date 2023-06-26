using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Msr
{
	// Token: 0x02000027 RID: 39
	[DataContract]
	public class MsrDownloadConfig
	{
		// Token: 0x06000296 RID: 662 RVA: 0x00009260 File Offset: 0x00007460
		private MsrDownloadConfig()
		{
			this.DownloadProgressUpdateIntervalMillis = 300;
			this.MaxNumberOfParallelDownloads = 5;
			this.ChunkSizeBytes = 3145728;
			this.MinimalReportedDownloadedBytesIncrease = 1024;
			this.ReportingProgressIntervalMillis = 100;
			this.NumberOfProgressEventsToSkipInUI = 100;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000092B4 File Offset: 0x000074B4
		static MsrDownloadConfig()
		{
			try
			{
				FlowConditionService flowConditionService = new FlowConditionService();
				MsrDownloadConfig.IsTestConfigEnabled = flowConditionService.IsTestConfigFileAvailable;
			}
			catch (Exception)
			{
				Tracer<FlowConditionService>.WriteWarning("Could not initialize flow condition service.", new object[0]);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00009314 File Offset: 0x00007514
		public static MsrDownloadConfig Instance
		{
			get
			{
				return MsrDownloadConfig.configInstance.Value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00009330 File Offset: 0x00007530
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00009338 File Offset: 0x00007538
		[DataMember]
		public int DownloadProgressUpdateIntervalMillis { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00009341 File Offset: 0x00007541
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00009349 File Offset: 0x00007549
		[DataMember]
		public int MaxNumberOfParallelDownloads { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00009352 File Offset: 0x00007552
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000935A File Offset: 0x0000755A
		[DataMember]
		public int ChunkSizeBytes { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00009363 File Offset: 0x00007563
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000936B File Offset: 0x0000756B
		[DataMember]
		public int MinimalReportedDownloadedBytesIncrease { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00009374 File Offset: 0x00007574
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000937C File Offset: 0x0000757C
		[DataMember]
		public int ReportingProgressIntervalMillis { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00009385 File Offset: 0x00007585
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000938D File Offset: 0x0000758D
		[DataMember]
		public int NumberOfProgressEventsToSkipInUI { get; private set; }

		// Token: 0x060002A5 RID: 677 RVA: 0x00009398 File Offset: 0x00007598
		private static MsrDownloadConfig ReadConfig()
		{
			bool flag = !MsrDownloadConfig.IsTestConfigEnabled;
			MsrDownloadConfig msrDownloadConfig;
			if (flag)
			{
				msrDownloadConfig = new MsrDownloadConfig();
			}
			else
			{
				bool flag2 = !File.Exists("./MsrDownloadConfig.xml");
				MsrDownloadConfig msrDownloadConfig2;
				if (flag2)
				{
					msrDownloadConfig2 = new MsrDownloadConfig();
					using (FileStream fileStream = new FileStream("./MsrDownloadConfig.xml", FileMode.OpenOrCreate, FileAccess.Write))
					{
						DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(MsrDownloadConfig));
						dataContractSerializer.WriteObject(fileStream, msrDownloadConfig2);
					}
				}
				else
				{
					using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(new FileStream("./MsrDownloadConfig.xml", FileMode.Open, FileAccess.Read), new XmlDictionaryReaderQuotas()))
					{
						DataContractSerializer dataContractSerializer2 = new DataContractSerializer(typeof(MsrDownloadConfig));
						msrDownloadConfig2 = (MsrDownloadConfig)dataContractSerializer2.ReadObject(xmlDictionaryReader, true);
					}
				}
				msrDownloadConfig = msrDownloadConfig2;
			}
			return msrDownloadConfig;
		}

		// Token: 0x04000116 RID: 278
		private const string ConfigPathConst = "./MsrDownloadConfig.xml";

		// Token: 0x04000117 RID: 279
		private static readonly Lazy<MsrDownloadConfig> configInstance = new Lazy<MsrDownloadConfig>(new Func<MsrDownloadConfig>(MsrDownloadConfig.ReadConfig));

		// Token: 0x04000118 RID: 280
		private static readonly bool IsTestConfigEnabled;
	}
}

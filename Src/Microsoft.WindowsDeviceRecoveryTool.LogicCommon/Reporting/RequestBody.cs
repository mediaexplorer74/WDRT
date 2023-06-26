using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting
{
	// Token: 0x0200001D RID: 29
	[DataContract]
	internal class RequestBody
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000084A2 File Offset: 0x000066A2
		// (set) Token: 0x0600023E RID: 574 RVA: 0x000084AA File Offset: 0x000066AA
		[DataMember(Name = "manufacturerName")]
		internal string ManufacturerName { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000084B3 File Offset: 0x000066B3
		// (set) Token: 0x06000240 RID: 576 RVA: 0x000084BB File Offset: 0x000066BB
		[DataMember(Name = "manufacturerProductLine")]
		internal string ManufacturerProductLine { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000084C4 File Offset: 0x000066C4
		// (set) Token: 0x06000242 RID: 578 RVA: 0x000084CC File Offset: 0x000066CC
		[DataMember(Name = "reportClassification")]
		internal string ReportClassification { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000084D5 File Offset: 0x000066D5
		// (set) Token: 0x06000244 RID: 580 RVA: 0x000084DD File Offset: 0x000066DD
		[DataMember(Name = "fileName")]
		internal string FileName { get; set; }

		// Token: 0x06000245 RID: 581 RVA: 0x000084E8 File Offset: 0x000066E8
		public string ToJsonString()
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				RequestBody.Serializer.WriteObject(memoryStream, this);
				memoryStream.Flush();
				memoryStream.Position = 0L;
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					text = streamReader.ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x040000E3 RID: 227
		private static readonly DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(RequestBody));
	}
}

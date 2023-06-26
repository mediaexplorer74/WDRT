using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SoftwareRepository.Streaming
{
	// Token: 0x02000013 RID: 19
	[DataContract]
	public class UrlResult
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003628 File Offset: 0x00001828
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003630 File Offset: 0x00001830
		[SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
		[DataMember(Name = "fileUrl")]
		public string FileUrl { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003639 File Offset: 0x00001839
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003641 File Offset: 0x00001841
		[DataMember(Name = "isSelected")]
		public bool IsSelected { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000364A File Offset: 0x0000184A
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003652 File Offset: 0x00001852
		[DataMember(Name = "testSpeed")]
		public double TestSpeed { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000365C File Offset: 0x0000185C
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003679 File Offset: 0x00001879
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
		[DataMember(Name = "displayTestSpeed")]
		public string DisplayTestSpeed
		{
			get
			{
				return this.TestSpeed.ToSpeedFormat();
			}
			private set
			{
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000367C File Offset: 0x0000187C
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003684 File Offset: 0x00001884
		[DataMember(Name = "error")]
		public string Error { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000368D File Offset: 0x0000188D
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003695 File Offset: 0x00001895
		[DataMember(Name = "bytesRead")]
		public long BytesRead { get; set; }
	}
}

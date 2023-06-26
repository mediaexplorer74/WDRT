using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Interfaces;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public class SurveyReport : IReport
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000857E File Offset: 0x0000677E
		public SurveyReport()
		{
			this.ActionDescription = "Survey";
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00008594 File Offset: 0x00006794
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000859C File Offset: 0x0000679C
		public string SessionId { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000085A5 File Offset: 0x000067A5
		// (set) Token: 0x0600024C RID: 588 RVA: 0x000085AD File Offset: 0x000067AD
		public string LocalPath { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000085B6 File Offset: 0x000067B6
		// (set) Token: 0x0600024E RID: 590 RVA: 0x000085BE File Offset: 0x000067BE
		public bool Question1 { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000085C7 File Offset: 0x000067C7
		// (set) Token: 0x06000250 RID: 592 RVA: 0x000085CF File Offset: 0x000067CF
		public bool Question2 { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000085D8 File Offset: 0x000067D8
		// (set) Token: 0x06000252 RID: 594 RVA: 0x000085E0 File Offset: 0x000067E0
		public bool Question3 { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000085E9 File Offset: 0x000067E9
		// (set) Token: 0x06000254 RID: 596 RVA: 0x000085F1 File Offset: 0x000067F1
		public bool Question4 { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000085FA File Offset: 0x000067FA
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00008602 File Offset: 0x00006802
		public bool Question5 { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000860C File Offset: 0x0000680C
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00008624 File Offset: 0x00006824
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = SurveyReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00008633 File Offset: 0x00006833
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000863B File Offset: 0x0000683B
		public bool InsiderProgramQuestion { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00008644 File Offset: 0x00006844
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000865C File Offset: 0x0000685C
		public string ManufacturerHardwareModel
		{
			get
			{
				return this.productType;
			}
			set
			{
				this.productType = SurveyReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000866C File Offset: 0x0000686C
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00008684 File Offset: 0x00006884
		public string ManufacturerHardwareVariant
		{
			get
			{
				return this.productCode;
			}
			set
			{
				this.productCode = SurveyReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00008694 File Offset: 0x00006894
		// (set) Token: 0x06000260 RID: 608 RVA: 0x000086AC File Offset: 0x000068AC
		public string Imei
		{
			get
			{
				return this.imei;
			}
			set
			{
				this.imei = SurveyReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000086BC File Offset: 0x000068BC
		// (set) Token: 0x06000262 RID: 610 RVA: 0x000086D4 File Offset: 0x000068D4
		public string ManufacturerName
		{
			get
			{
				return this.surveyManufacturerName;
			}
			set
			{
				this.surveyManufacturerName = SurveyReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000086E4 File Offset: 0x000068E4
		// (set) Token: 0x06000264 RID: 612 RVA: 0x000086FC File Offset: 0x000068FC
		public string ManufacturerProductLine
		{
			get
			{
				return this.surveyManufacturerProductLine;
			}
			set
			{
				this.surveyManufacturerProductLine = SurveyReport.PrepareForCsvFormat(value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000870B File Offset: 0x0000690B
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00008713 File Offset: 0x00006913
		public string ActionDescription { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000871C File Offset: 0x0000691C
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00008724 File Offset: 0x00006924
		public PhoneTypes PhoneType { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000872D File Offset: 0x0000692D
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00008735 File Offset: 0x00006935
		public bool Sent { get; private set; }

		// Token: 0x0600026B RID: 619 RVA: 0x0000873E File Offset: 0x0000693E
		public void MarkAsSent()
		{
			this.Sent = true;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000874C File Offset: 0x0000694C
		public string GetReportAsXml()
		{
			XDocument xdocument = new XDocument(new XDeclaration("1.0", "utf-8", "no"), new object[]
			{
				new XElement("Survey", new object[]
				{
					new XElement("reportSessionId", this.SessionId),
					new XElement("surveyCheckAnswer", new object[]
					{
						new XAttribute("questionId", "q1"),
						new XAttribute("id", "q1_1"),
						new XAttribute("questionContent", SurveyReport.ReadEnglishResource("Survey_Header1")),
						new XAttribute("answerContent", SurveyReport.ReadEnglishResource("Survey_Choice_AppsNotWorking1")),
						this.Question1
					}),
					new XElement("surveyCheckAnswer", new object[]
					{
						new XAttribute("questionId", "q1"),
						new XAttribute("id", "q1_2"),
						new XAttribute("questionContent", SurveyReport.ReadEnglishResource("Survey_Header1")),
						new XAttribute("answerContent", SurveyReport.ReadEnglishResource("Survey_Choice_PerformanceIssues1")),
						this.Question2
					}),
					new XElement("surveyCheckAnswer", new object[]
					{
						new XAttribute("questionId", "q1"),
						new XAttribute("id", "q1_3"),
						new XAttribute("questionContent", SurveyReport.ReadEnglishResource("Survey_Header1")),
						new XAttribute("answerContent", SurveyReport.ReadEnglishResource("Survey_Choice_PrevVersionFaster1")),
						this.Question3
					}),
					new XElement("surveyCheckAnswer", new object[]
					{
						new XAttribute("questionId", "q1"),
						new XAttribute("id", "q1_4"),
						new XAttribute("questionContent", SurveyReport.ReadEnglishResource("Survey_Header1")),
						new XAttribute("answerContent", SurveyReport.ReadEnglishResource("Survey_Choice_PrevVersionMoreReliable1")),
						this.Question4
					}),
					new XElement("surveyCheckAnswer", new object[]
					{
						new XAttribute("questionId", "q1"),
						new XAttribute("id", "q1_5"),
						new XAttribute("questionContent", SurveyReport.ReadEnglishResource("Survey_Header1")),
						new XAttribute("answerContent", SurveyReport.ReadEnglishResource("Survey_Choice_DeviceNotWorking1")),
						this.Question5
					}),
					new XElement("surveyOpenAnswer", new object[]
					{
						new XAttribute("questionId", "tellUsMore"),
						new XAttribute("id", "tellUsMore_userMessage"),
						new XAttribute("questionContent", SurveyReport.ReadEnglishResource("Survey_TellUsMore1")),
						new XAttribute("answerContent", "q1"),
						this.Description
					}),
					new XElement("surveyCheckAnswer", new object[]
					{
						new XAttribute("questionId", "q2"),
						new XAttribute("id", "q2_insiderProgram"),
						new XAttribute("questionContent", SurveyReport.ReadEnglishResource("Survey_Choice_MoreAbout_QuestionContent")),
						new XAttribute("answerContent", SurveyReport.ReadEnglishResource("Survey_Choice_InsiderProgram_AnswerContent")),
						this.InsiderProgramQuestion
					})
				})
			});
			return xdocument.ToString();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008B80 File Offset: 0x00006D80
		public string GetReportAsCsv()
		{
			List<string> list = new List<string>
			{
				this.Question1.ToString(),
				this.Question2.ToString(),
				this.Question3.ToString(),
				this.Question4.ToString(),
				this.Question5.ToString(),
				this.Description,
				this.ManufacturerHardwareModel,
				this.ManufacturerHardwareVariant,
				this.Imei,
				this.ManufacturerName,
				this.ManufacturerProductLine
			};
			return string.Join(";", list);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008C5C File Offset: 0x00006E5C
		public ReportUpdateStatus4Parameters CreateReportStatusParameters()
		{
			string text = this.FormatString(this.Imei, 100);
			return new ReportUpdateStatus4Parameters
			{
				SystemInfo = string.Empty,
				ActionDescription = this.FormatString(this.ActionDescription, 200),
				Uri = 0L,
				UriDescription = string.Empty,
				ApplicationName = string.Empty,
				ApplicationVendorName = "Microsoft",
				ApplicationVersion = string.Empty,
				ProductType = this.FormatString(this.ManufacturerHardwareModel, 100),
				ProductCode = this.FormatString(this.ManufacturerHardwareVariant, 100),
				Imei = text,
				FirmwareVersionOld = string.Empty,
				FirmwareVersionNew = string.Empty,
				FwGrading = string.Empty,
				Duration = 0L,
				DownloadDuration = 0L,
				UpdateDuration = 0L,
				ApiError = string.Empty,
				ApiErrorText = string.Empty,
				TimeStamp = TimeStampUtility.CreateTimeStamp(),
				Ext1 = this.FormatString(this.ActionDescription, 200),
				Ext2 = string.Empty,
				Ext3 = string.Empty,
				Ext4 = string.Format("{0}|{1}|{2}|{3}|{4}", new object[] { this.Question1, this.Question2, this.Question3, this.Question4, this.Question5 }),
				Ext7 = this.FormatString(this.Description, 200),
				Ext8 = ApplicationInfo.CurrentLanguageInRegistry.EnglishName
			};
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008E30 File Offset: 0x00007030
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

		// Token: 0x06000270 RID: 624 RVA: 0x00008E98 File Offset: 0x00007098
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

		// Token: 0x06000271 RID: 625 RVA: 0x00008EC8 File Offset: 0x000070C8
		private string Truncate(string source, int length)
		{
			bool flag = source.Length > length;
			if (flag)
			{
				source = source.Substring(0, length);
			}
			return source;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008EF4 File Offset: 0x000070F4
		private static string ReadEnglishResource(string resourceKey)
		{
			return LocalizationManager.Instance().EnglishResource(resourceKey) as string;
		}

		// Token: 0x040000E8 RID: 232
		private const string Survey = "Survey";

		// Token: 0x040000E9 RID: 233
		private string description;

		// Token: 0x040000EA RID: 234
		private string productType;

		// Token: 0x040000EB RID: 235
		private string productCode;

		// Token: 0x040000EC RID: 236
		private string imei;

		// Token: 0x040000ED RID: 237
		private string surveyManufacturerName;

		// Token: 0x040000EE RID: 238
		private string surveyManufacturerProductLine;
	}
}

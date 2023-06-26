using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Interfaces;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers
{
	// Token: 0x02000032 RID: 50
	internal class IOHelper
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000C698 File Offset: 0x0000A898
		public void CreateDir(string directory)
		{
			bool flag = !Directory.Exists(directory);
			if (flag)
			{
				Directory.CreateDirectory(directory);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		public void CreateDirForPath(string fullPath)
		{
			string directoryName = Path.GetDirectoryName(fullPath);
			bool flag = directoryName != null;
			if (flag)
			{
				this.CreateDir(directoryName);
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000C6E4 File Offset: 0x0000A8E4
		public void SerializeReport(string reportPath, IReport report)
		{
			this.CreateDirForPath(reportPath);
			using (FileStream fileStream = new FileStream(reportPath, FileMode.Create))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				report.LocalPath = reportPath;
				binaryFormatter.Serialize(fileStream, report);
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000C738 File Offset: 0x0000A938
		public void SaveReport(string reportPath, MsrReport report)
		{
			bool flag = reportPath != null;
			if (flag)
			{
				this.CreateDirForPath(reportPath);
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(MsrReport));
				using (StreamWriter streamWriter = new StreamWriter(reportPath))
				{
					xmlSerializer.Serialize(streamWriter, report);
					streamWriter.Close();
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		public void SaveReportAsCsv(string reportPath, IReport report)
		{
			bool flag = reportPath != null;
			if (flag)
			{
				this.CreateDirForPath(reportPath);
				string reportAsCsv = report.GetReportAsCsv();
				using (StreamWriter streamWriter = new StreamWriter(reportPath))
				{
					streamWriter.WriteLine(reportAsCsv);
					streamWriter.Close();
				}
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		public void SaveReportAsCsv(string reportPath, SurveyReport report)
		{
			bool flag = reportPath != null;
			if (flag)
			{
				this.CreateDirForPath(reportPath);
				string reportAsCsv = report.GetReportAsCsv();
				using (StreamWriter streamWriter = new StreamWriter(reportPath))
				{
					streamWriter.WriteLine(reportAsCsv);
					streamWriter.Close();
				}
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000C858 File Offset: 0x0000AA58
		public void SaveReport(string reportPath, ReportUpdateStatus4Parameters parameters)
		{
			bool flag = reportPath != null;
			if (flag)
			{
				this.CreateDirForPath(reportPath);
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(ReportUpdateStatus4Parameters));
				reportPath = reportPath.Replace(": ", string.Empty);
				using (StreamWriter streamWriter = new StreamWriter(reportPath))
				{
					xmlSerializer.Serialize(streamWriter, parameters);
					streamWriter.Close();
				}
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C8D0 File Offset: 0x0000AAD0
		public IReport DeserializeReport(string reportPath)
		{
			IReport report2;
			using (FileStream fileStream = new FileStream(reportPath, FileMode.Open))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				IReport report = (IReport)binaryFormatter.Deserialize(fileStream);
				report.LocalPath = reportPath;
				report2 = report;
			}
			return report2;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C924 File Offset: 0x0000AB24
		public Report DeserializeFireReport(string reportPath)
		{
			Report report2;
			using (FileStream fileStream = new FileStream(reportPath, FileMode.Open))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				Report report = (Report)binaryFormatter.Deserialize(fileStream);
				report.LocalPath = reportPath;
				report2 = report;
			}
			return report2;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C978 File Offset: 0x0000AB78
		public void DeleteFile(string fileName)
		{
			try
			{
				File.Delete(fileName);
			}
			catch (Exception ex)
			{
				Tracer<IOHelper>.WriteInformation("Delete fire report failed. " + ex.Message);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		public string[] GetFiles(string dir)
		{
			string[] files = Directory.GetFiles(dir);
			return files.Where((string x) => !x.Contains("msr_")).ToArray<string>();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000CA00 File Offset: 0x0000AC00
		public string[] GetMsrFiles(string dir)
		{
			return Directory.GetFiles(dir, "msr_*.*");
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000CA20 File Offset: 0x0000AC20
		public string GetReportFileExtension(ReportFileType reportFileType)
		{
			string text;
			switch (reportFileType)
			{
			case ReportFileType.Xml:
				text = "xml";
				break;
			case ReportFileType.Binary:
				text = "dat";
				break;
			case ReportFileType.Csv:
				text = "csv";
				break;
			default:
				text = "xml";
				break;
			}
			return text;
		}
	}
}

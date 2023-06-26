using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x0200001B RID: 27
	public class VariantInfo
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00005BD5 File Offset: 0x00003DD5
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00005BDD File Offset: 0x00003DDD
		public long Id { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005BE6 File Offset: 0x00003DE6
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00005BEE File Offset: 0x00003DEE
		public long Size { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005BF7 File Offset: 0x00003DF7
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00005BFF File Offset: 0x00003DFF
		public string Name { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005C08 File Offset: 0x00003E08
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00005C10 File Offset: 0x00003E10
		public string SoftwareVersion { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00005C19 File Offset: 0x00003E19
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00005C21 File Offset: 0x00003E21
		public string VariantVersion { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00005C2A File Offset: 0x00003E2A
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00005C32 File Offset: 0x00003E32
		public string AkVersion { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00005C3B File Offset: 0x00003E3B
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00005C43 File Offset: 0x00003E43
		public string ProductCode { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005C4C File Offset: 0x00003E4C
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00005C54 File Offset: 0x00003E54
		public string ProductType { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00005C5D File Offset: 0x00003E5D
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00005C65 File Offset: 0x00003E65
		public string Path { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00005C6E File Offset: 0x00003E6E
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00005C76 File Offset: 0x00003E76
		public string FfuFilePath { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005C7F File Offset: 0x00003E7F
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005C87 File Offset: 0x00003E87
		public bool IsLocal { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005C90 File Offset: 0x00003E90
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005C98 File Offset: 0x00003E98
		public bool OnlyLocal { get; set; }

		// Token: 0x060001AE RID: 430 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public static VariantInfo GetVariantInfo(string vplPath)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			string text7 = string.Empty;
			string text8 = System.IO.Path.GetDirectoryName(vplPath) ?? string.Empty;
			using (Stream stream = new FileStream(vplPath, FileMode.Open, FileAccess.Read))
			{
				using (XmlReader xmlReader = XmlReader.Create(stream))
				{
					while (xmlReader.Read())
					{
						string localName = xmlReader.LocalName;
						string text9 = localName;
						if (text9 != null)
						{
							int length = text9.Length;
							if (length <= 9)
							{
								if (length != 4)
								{
									if (length == 9)
									{
										char c = text9[0];
										if (c != 'A')
										{
											if (c == 'S')
											{
												if (text9 == "SwVersion")
												{
													text3 = xmlReader.ReadInnerXml();
												}
											}
										}
										else if (text9 == "AkVersion")
										{
											text7 = xmlReader.ReadInnerXml();
										}
									}
								}
								else if (text9 == "Name")
								{
									string text10 = xmlReader.ReadInnerXml();
									bool flag = text10.EndsWith(".ffu");
									if (flag)
									{
										text6 = System.IO.Path.Combine(text8, text10);
									}
								}
							}
							else if (length != 11)
							{
								if (length == 14)
								{
									char c = text9[0];
									if (c != 'T')
									{
										if (c == 'V')
										{
											if (text9 == "VariantVersion")
											{
												text4 = xmlReader.ReadInnerXml();
											}
										}
									}
									else if (text9 == "TypeDesignator")
									{
										text5 = xmlReader.ReadInnerXml();
									}
								}
							}
							else
							{
								char c = text9[0];
								if (c != 'D')
								{
									if (c == 'P')
									{
										if (text9 == "ProductCode")
										{
											text2 = xmlReader.ReadInnerXml();
										}
									}
								}
								else if (text9 == "Description")
								{
									text = xmlReader.ReadInnerXml();
								}
							}
						}
					}
				}
			}
			return new VariantInfo
			{
				Name = text,
				ProductCode = text2,
				ProductType = text5,
				SoftwareVersion = text3,
				Path = vplPath,
				AkVersion = text7,
				VariantVersion = text4,
				IsLocal = true,
				Size = VariantInfo.GetVariantSizeOnDisk(vplPath),
				FfuFilePath = text6
			};
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005F5C File Offset: 0x0000415C
		public static long GetVariantSizeOnDisk(string vplPath)
		{
			Tracer<VariantInfo>.LogEntry("GetVariantSizeOnDisk");
			long num = 0L;
			VplContent vplContent = new VplContent();
			vplContent.ParseVplFile(vplPath);
			string directoryName = System.IO.Path.GetDirectoryName(vplPath);
			bool flag = string.IsNullOrEmpty(directoryName);
			if (flag)
			{
				throw new DirectoryNotFoundException("Vpl directory not found");
			}
			List<string> list = (from file in vplContent.FileList
				where !string.IsNullOrEmpty(file.Name)
				select file.Name).ToList<string>();
			foreach (string text in list)
			{
				string text2 = System.IO.Path.Combine(directoryName, text);
				bool flag2 = File.Exists(text2);
				if (flag2)
				{
					FileInfo fileInfo = new FileInfo(text2);
					num += fileInfo.Length;
				}
			}
			FileInfo fileInfo2 = new FileInfo(vplPath);
			num += fileInfo2.Length;
			Tracer<VariantInfo>.WriteInformation("Size on disk: {0}", new object[] { num });
			Tracer<VariantInfo>.LogExit("GetVariantSizeOnDisk");
			return num;
		}
	}
}

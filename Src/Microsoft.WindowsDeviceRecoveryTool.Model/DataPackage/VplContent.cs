using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage
{
	// Token: 0x0200004F RID: 79
	public class VplContent
	{
		// Token: 0x0600027E RID: 638 RVA: 0x00006A7A File Offset: 0x00004C7A
		public VplContent()
		{
			this.fileList = new List<VplFile>();
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00006A8F File Offset: 0x00004C8F
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00006A97 File Offset: 0x00004C97
		public string VplFileName { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00006AA0 File Offset: 0x00004CA0
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00006AA8 File Offset: 0x00004CA8
		public string Description { get; private set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00006AB1 File Offset: 0x00004CB1
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00006AB9 File Offset: 0x00004CB9
		public string TypeDesignator { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00006AC2 File Offset: 0x00004CC2
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00006ACA File Offset: 0x00004CCA
		public string ProductCode { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00006AD3 File Offset: 0x00004CD3
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00006ADB File Offset: 0x00004CDB
		public string SoftwareVersion { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00006AE4 File Offset: 0x00004CE4
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00006AEC File Offset: 0x00004CEC
		public string VariantVersion { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00006AF5 File Offset: 0x00004CF5
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00006AFD File Offset: 0x00004CFD
		public Dictionary<int, string> RofsVersions { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00006B08 File Offset: 0x00004D08
		public ReadOnlyCollection<VplFile> FileList
		{
			get
			{
				return new ReadOnlyCollection<VplFile>(this.fileList);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00006B28 File Offset: 0x00004D28
		public static Dictionary<int, string> ParseRofsVersions(string vplFilePath)
		{
			XDocument xdocument = XDocument.Load(vplFilePath);
			XElement xelement = xdocument.Descendants("Variant").First<XElement>();
			return VplContent.ParseRofsVersions(xelement);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00006B60 File Offset: 0x00004D60
		public void ParseVplFile(string vplFilePath)
		{
			this.VplFileName = Path.GetFileName(vplFilePath);
			XDocument xdocument = XDocument.Load(vplFilePath);
			XElement xelement = xdocument.Descendants("Variant").First<XElement>();
			this.ParseVariantIdentification(xelement);
			this.ParseSwVersion(xelement);
			this.ParseVariantVersion(xelement);
			this.ParseFileList(xelement);
			this.RofsVersions = VplContent.ParseRofsVersions(xelement);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00006BC8 File Offset: 0x00004DC8
		private static Dictionary<int, string> ParseRofsVersions(XElement variant)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			IEnumerable<XElement> enumerable = variant.Descendants("FlashImage");
			try
			{
				foreach (XElement xelement in enumerable)
				{
					XElement xelement2 = xelement.Descendants("Version").FirstOrDefault<XElement>();
					XElement xelement3 = xelement.Descendants("RofsIndex").FirstOrDefault<XElement>();
					bool flag = xelement2 != null && xelement2.Value.Any<char>() && xelement3 != null;
					if (flag)
					{
						short num = Convert.ToInt16(xelement3.Value);
						bool flag2 = num < 1 || num > 6;
						if (flag2)
						{
							Tracer<VplContent>.WriteError(string.Format("Illegal ROFS index in VPL ({0}).", num), new object[0]);
							break;
						}
						dictionary.Add((int)num, xelement2.Value);
					}
					else
					{
						bool flag3 = xelement3 == null;
						if (flag3)
						{
							Tracer<VplContent>.WriteError("ROFS index element in VPL was invalid or missing", new object[0]);
							break;
						}
						Tracer<VplContent>.WriteError("ROFS version element in VPL was invalid or missing", new object[0]);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Tracer<VplContent>.WriteError(ex, "Problem with ROFS parsing.", new object[0]);
				throw;
			}
			return dictionary.OrderBy((KeyValuePair<int, string> elem) => elem.Key).ToDictionary((KeyValuePair<int, string> elem) => elem.Key, (KeyValuePair<int, string> elem) => elem.Value);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00006DB0 File Offset: 0x00004FB0
		private void ParseVariantIdentification(XElement variant)
		{
			XElement xelement = variant.Descendants("VariantIdentification").First<XElement>();
			XElement xelement2 = xelement.Descendants("Description").First<XElement>();
			this.Description = xelement2.Value;
			XElement xelement3 = xelement.Descendants("TypeDesignator").First<XElement>();
			this.TypeDesignator = xelement3.Value;
			XElement xelement4 = xelement.Descendants("ProductCode").First<XElement>();
			this.ProductCode = xelement4.Value;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00006E40 File Offset: 0x00005040
		private void ParseSwVersion(XElement variant)
		{
			XElement xelement = variant.Descendants("SwVersion").First<XElement>();
			this.SoftwareVersion = xelement.Value;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00006E74 File Offset: 0x00005074
		private void ParseVariantVersion(XElement variant)
		{
			XElement xelement = variant.Descendants("VariantVersion").First<XElement>();
			this.VariantVersion = xelement.Value;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00006EA8 File Offset: 0x000050A8
		private void ParseFileList(XElement variant)
		{
			this.fileList.Clear();
			XElement xelement = variant.Descendants("FileList").First<XElement>();
			IEnumerable<XElement> enumerable = xelement.Descendants("File");
			foreach (XElement xelement2 in enumerable)
			{
				this.ParseFile(xelement2);
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00006F2C File Offset: 0x0000512C
		private void ParseFile(XElement file)
		{
			string value = file.Descendants("Name").First<XElement>().Value;
			string value2 = file.Descendants("FileType").First<XElement>().Value;
			string text = string.Empty;
			XElement xelement = file.Element("FileSubType");
			bool flag = xelement != null;
			if (flag)
			{
				text = xelement.Value;
			}
			string text2 = "false";
			XElement xelement2 = file.Element("Signed");
			bool flag2 = xelement2 != null;
			if (flag2)
			{
				text2 = xelement2.Value;
			}
			string text3 = "false";
			XElement xelement3 = file.Element("Optional");
			bool flag3 = xelement3 != null;
			if (flag3)
			{
				text3 = xelement3.Value;
			}
			string text4 = string.Empty;
			XElement xelement4 = file.Element("Crc");
			bool flag4 = xelement4 != null;
			if (flag4)
			{
				text4 = xelement4.Value;
			}
			bool flag5 = string.CompareOrdinal(text2, "true") == 0;
			bool flag6 = string.CompareOrdinal(text3, "true") == 0;
			VplFile vplFile = new VplFile(value, value2, text, flag5, flag6, text4);
			this.fileList.Add(vplFile);
		}

		// Token: 0x04000227 RID: 551
		private readonly List<VplFile> fileList;
	}
}

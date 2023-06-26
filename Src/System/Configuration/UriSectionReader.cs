using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000074 RID: 116
	internal class UriSectionReader
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x0001F530 File Offset: 0x0001D730
		private UriSectionReader(string configFilePath, UriSectionData parentData)
		{
			this.configFilePath = configFilePath;
			this.sectionData = new UriSectionData();
			if (parentData != null)
			{
				this.sectionData.IriParsing = parentData.IriParsing;
				this.sectionData.IdnScope = parentData.IdnScope;
				foreach (KeyValuePair<string, SchemeSettingInternal> keyValuePair in parentData.SchemeSettings)
				{
					this.sectionData.SchemeSettings.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
		public static UriSectionData Read(string configFilePath)
		{
			return UriSectionReader.Read(configFilePath, null);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001F5E4 File Offset: 0x0001D7E4
		public static UriSectionData Read(string configFilePath, UriSectionData parentData)
		{
			UriSectionReader uriSectionReader = new UriSectionReader(configFilePath, parentData);
			return uriSectionReader.GetSectionData();
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001F600 File Offset: 0x0001D800
		private UriSectionData GetSectionData()
		{
			new FileIOPermission(FileIOPermissionAccess.Read, this.configFilePath).Assert();
			try
			{
				if (File.Exists(this.configFilePath))
				{
					using (FileStream fileStream = new FileStream(this.configFilePath, FileMode.Open, FileAccess.Read))
					{
						using (this.reader = XmlReader.Create(fileStream, new XmlReaderSettings
						{
							IgnoreComments = true,
							IgnoreWhitespace = true,
							IgnoreProcessingInstructions = true
						}))
						{
							if (this.ReadConfiguration())
							{
								return this.sectionData;
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return null;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001F6D4 File Offset: 0x0001D8D4
		private bool ReadConfiguration()
		{
			if (!this.ReadToUriSection())
			{
				return false;
			}
			while (this.reader.Read())
			{
				if (this.IsEndElement("uri"))
				{
					return true;
				}
				if (this.reader.NodeType != XmlNodeType.Element)
				{
					return false;
				}
				string name = this.reader.Name;
				if (UriSectionReader.AreEqual(name, "iriParsing"))
				{
					if (this.ReadIriParsing())
					{
						continue;
					}
				}
				else if (UriSectionReader.AreEqual(name, "idn"))
				{
					if (this.ReadIdnScope())
					{
						continue;
					}
				}
				else if (UriSectionReader.AreEqual(name, "schemeSettings") && this.ReadSchemeSettings())
				{
					continue;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001F76C File Offset: 0x0001D96C
		private bool ReadIriParsing()
		{
			string attribute = this.reader.GetAttribute("enabled");
			bool flag;
			if (bool.TryParse(attribute, out flag))
			{
				this.sectionData.IriParsing = new bool?(flag);
				return true;
			}
			return false;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001F7A8 File Offset: 0x0001D9A8
		private bool ReadIdnScope()
		{
			string attribute = this.reader.GetAttribute("enabled");
			bool flag;
			try
			{
				this.sectionData.IdnScope = new UriIdnScope?((UriIdnScope)Enum.Parse(typeof(UriIdnScope), attribute, true));
				flag = true;
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001F808 File Offset: 0x0001DA08
		private bool ReadSchemeSettings()
		{
			while (this.reader.Read())
			{
				if (this.IsEndElement("schemeSettings"))
				{
					return true;
				}
				if (this.reader.NodeType != XmlNodeType.Element)
				{
					return false;
				}
				string name = this.reader.Name;
				if (UriSectionReader.AreEqual(name, "add"))
				{
					if (this.ReadAddSchemeSetting())
					{
						continue;
					}
				}
				else if (UriSectionReader.AreEqual(name, "remove"))
				{
					if (this.ReadRemoveSchemeSetting())
					{
						continue;
					}
				}
				else if (UriSectionReader.AreEqual(name, "clear"))
				{
					this.ClearSchemeSetting();
					continue;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001F895 File Offset: 0x0001DA95
		private static bool AreEqual(string value1, string value2)
		{
			return string.Compare(value1, value2, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001F8A4 File Offset: 0x0001DAA4
		private bool ReadAddSchemeSetting()
		{
			string attribute = this.reader.GetAttribute("name");
			string attribute2 = this.reader.GetAttribute("genericUriParserOptions");
			if (string.IsNullOrEmpty(attribute) || string.IsNullOrEmpty(attribute2))
			{
				return false;
			}
			bool flag;
			try
			{
				GenericUriParserOptions genericUriParserOptions = (GenericUriParserOptions)Enum.Parse(typeof(GenericUriParserOptions), attribute2);
				SchemeSettingInternal schemeSettingInternal = new SchemeSettingInternal(attribute, genericUriParserOptions);
				this.sectionData.SchemeSettings[schemeSettingInternal.Name] = schemeSettingInternal;
				flag = true;
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001F938 File Offset: 0x0001DB38
		private bool ReadRemoveSchemeSetting()
		{
			string attribute = this.reader.GetAttribute("name");
			if (string.IsNullOrEmpty(attribute))
			{
				return false;
			}
			this.sectionData.SchemeSettings.Remove(attribute);
			return true;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001F973 File Offset: 0x0001DB73
		private void ClearSchemeSetting()
		{
			this.sectionData.SchemeSettings.Clear();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001F985 File Offset: 0x0001DB85
		private bool IsEndElement(string elementName)
		{
			return this.reader.NodeType == XmlNodeType.EndElement && string.Compare(this.reader.Name, elementName, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001F9B0 File Offset: 0x0001DBB0
		private bool ReadToUriSection()
		{
			if (!this.reader.ReadToFollowing("configuration"))
			{
				return false;
			}
			if (this.reader.Depth != 0)
			{
				return false;
			}
			while (this.reader.ReadToFollowing("uri"))
			{
				if (this.reader.Depth == 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000BE9 RID: 3049
		private const string rootElementName = "configuration";

		// Token: 0x04000BEA RID: 3050
		private string configFilePath;

		// Token: 0x04000BEB RID: 3051
		private XmlReader reader;

		// Token: 0x04000BEC RID: 3052
		private UriSectionData sectionData;
	}
}

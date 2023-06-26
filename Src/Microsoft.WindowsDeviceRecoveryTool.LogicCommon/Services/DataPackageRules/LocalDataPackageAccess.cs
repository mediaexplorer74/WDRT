using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Security;
using System.Xml;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services.DataPackageRules
{
	// Token: 0x02000015 RID: 21
	public sealed class LocalDataPackageAccess
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x000058B7 File Offset: 0x00003AB7
		public LocalDataPackageAccess()
			: this(new FileHelper())
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000058C6 File Offset: 0x00003AC6
		internal LocalDataPackageAccess(FileHelper fileHelper)
		{
			this.fileHelper = fileHelper;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000058D8 File Offset: 0x00003AD8
		public ReadOnlyCollection<string> GetVplPathList(string productType, string productCode, string searchPath)
		{
			ReadOnlyCollection<string> readOnlyCollection;
			try
			{
				object[] array = new object[] { string.IsNullOrEmpty(productCode) ? "*" : productCode };
				string text = Path.Combine(searchPath, productType);
				string text2 = string.Format(CultureInfo.CurrentCulture, "*_{0}_*.vpl", array);
				string[] filesFromDirectory = this.fileHelper.GetFilesFromDirectory(text, text2);
				readOnlyCollection = Array.AsReadOnly<string>(filesFromDirectory);
			}
			catch (ArgumentNullException ex)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex);
				readOnlyCollection = Array.AsReadOnly<string>(new string[0]);
			}
			catch (UnauthorizedAccessException ex2)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex2);
				readOnlyCollection = Array.AsReadOnly<string>(new string[0]);
			}
			catch (IOException ex3)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex3);
				readOnlyCollection = Array.AsReadOnly<string>(new string[0]);
			}
			return readOnlyCollection;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000059AC File Offset: 0x00003BAC
		public ReadOnlyCollection<string> GetVplPathList(string productType, string productCode, IEnumerable<string> searchPaths)
		{
			List<string> list = new List<string>();
			foreach (string text in searchPaths)
			{
				list.AddRange(this.GetVplPathList(productType, productCode, text));
			}
			return list.AsReadOnly();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005A14 File Offset: 0x00003C14
		public ReadOnlyCollection<string> GetVplPathList(string productType, string searchPath)
		{
			return this.GetVplPathList(productType, null, searchPath);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005A30 File Offset: 0x00003C30
		public ReadOnlyCollection<string> GetVplPathList(string productType, IEnumerable<string> searchPaths)
		{
			return this.GetVplPathList(productType, null, searchPaths);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005A4C File Offset: 0x00003C4C
		public string GetProductCodeFromVpl(string vplFilePath)
		{
			string text;
			try
			{
				using (Stream fileStream = this.GetFileStream(vplFilePath))
				{
					using (XmlReader xmlReader = XmlReader.Create(fileStream))
					{
						while (xmlReader.Read())
						{
							bool flag = xmlReader.LocalName == "ProductCode";
							if (flag)
							{
								return xmlReader.ReadInnerXml();
							}
						}
						text = string.Empty;
					}
				}
			}
			catch (ArgumentNullException ex)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex);
				text = string.Empty;
			}
			catch (SecurityException ex2)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex2);
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005B14 File Offset: 0x00003D14
		public string GetSoftwareVersionFromVpl(string vplFilePath)
		{
			string text;
			try
			{
				using (Stream fileStream = this.GetFileStream(vplFilePath))
				{
					using (XmlReader xmlReader = XmlReader.Create(fileStream))
					{
						while (xmlReader.Read())
						{
							bool flag = xmlReader.LocalName == "SwVersion";
							if (flag)
							{
								return xmlReader.ReadInnerXml();
							}
						}
						text = string.Empty;
					}
				}
			}
			catch (ArgumentNullException ex)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex);
				text = string.Empty;
			}
			catch (SecurityException ex2)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex2);
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005BDC File Offset: 0x00003DDC
		public string GetVariantVersionFromVpl(string vplFilePath)
		{
			string text;
			try
			{
				using (Stream fileStream = this.GetFileStream(vplFilePath))
				{
					using (XmlReader xmlReader = XmlReader.Create(fileStream))
					{
						while (xmlReader.Read())
						{
							bool flag = xmlReader.LocalName == "VariantVersion";
							if (flag)
							{
								return xmlReader.ReadInnerXml();
							}
						}
						text = string.Empty;
					}
				}
			}
			catch (ArgumentNullException ex)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex);
				text = string.Empty;
			}
			catch (SecurityException ex2)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex2);
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public string GetVariantDescriptionFromVpl(string vplFilePath)
		{
			string text;
			try
			{
				using (Stream fileStream = this.GetFileStream(vplFilePath))
				{
					using (XmlTextReader xmlTextReader = new XmlTextReader(fileStream))
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(xmlTextReader);
						text = xmlDocument.GetElementsByTagName("Description")[0].InnerText;
					}
				}
			}
			catch (NullReferenceException ex)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex);
				text = string.Empty;
			}
			catch (ArgumentNullException ex2)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex2);
				text = string.Empty;
			}
			catch (SecurityException ex3)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex3);
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005D7C File Offset: 0x00003F7C
		private Stream GetFileStream(string vplFilePath)
		{
			Stream stream;
			try
			{
				stream = this.fileHelper.GetFileStream(vplFilePath);
			}
			catch (ArgumentException ex)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex);
				stream = null;
			}
			catch (NotSupportedException ex2)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex2);
				stream = null;
			}
			catch (IOException ex3)
			{
				Tracer<LocalDataPackageAccess>.WriteError(ex3);
				stream = null;
			}
			return stream;
		}

		// Token: 0x04000051 RID: 81
		private readonly FileHelper fileHelper;
	}
}

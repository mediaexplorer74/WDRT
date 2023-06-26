using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000229 RID: 553
	internal static class ODataAtomReaderUtils
	{
		// Token: 0x0600116B RID: 4459 RVA: 0x00041290 File Offset: 0x0003F490
		internal static XmlReader CreateXmlReader(Stream stream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings)
		{
			XmlReaderSettings xmlReaderSettings = ODataAtomReaderUtils.CreateXmlReaderSettings(messageReaderSettings);
			if (encoding != null)
			{
				return XmlReader.Create(new StreamReader(stream, encoding), xmlReaderSettings);
			}
			return XmlReader.Create(stream, xmlReaderSettings);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x000412BC File Offset: 0x0003F4BC
		internal static bool ReadMetadataNullAttributeValue(string attributeValue)
		{
			return XmlConvert.ToBoolean(attributeValue);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000412C4 File Offset: 0x0003F4C4
		private static XmlReaderSettings CreateXmlReaderSettings(ODataMessageReaderSettings messageReaderSettings)
		{
			return new XmlReaderSettings
			{
				CheckCharacters = messageReaderSettings.CheckCharacters,
				ConformanceLevel = ConformanceLevel.Document,
				CloseInput = true,
				DtdProcessing = DtdProcessing.Prohibit
			};
		}
	}
}

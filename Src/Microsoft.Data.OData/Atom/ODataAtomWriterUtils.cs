using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200029A RID: 666
	internal static class ODataAtomWriterUtils
	{
		// Token: 0x06001685 RID: 5765 RVA: 0x00051F2C File Offset: 0x0005012C
		internal static XmlWriter CreateXmlWriter(Stream stream, ODataMessageWriterSettings messageWriterSettings, Encoding encoding)
		{
			XmlWriterSettings xmlWriterSettings = ODataAtomWriterUtils.CreateXmlWriterSettings(messageWriterSettings, encoding);
			XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings);
			if (messageWriterSettings.AlwaysUseDefaultXmlNamespaceForRootElement)
			{
				xmlWriter = new DefaultNamespaceCompensatingXmlWriter(xmlWriter);
			}
			return xmlWriter;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00051F59 File Offset: 0x00050159
		internal static void WriteError(XmlWriter writer, ODataError error, bool includeDebugInformation, int maxInnerErrorDepth)
		{
			ErrorUtils.WriteXmlError(writer, error, includeDebugInformation, maxInnerErrorDepth);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00051F64 File Offset: 0x00050164
		internal static void WriteETag(XmlWriter writer, string etag)
		{
			writer.WriteAttributeString("m", "etag", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", etag);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00051F7C File Offset: 0x0005017C
		internal static void WriteNullAttribute(XmlWriter writer)
		{
			writer.WriteAttributeString("m", "null", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", "true");
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00051F98 File Offset: 0x00050198
		internal static void WriteRaw(XmlWriter writer, string value)
		{
			ODataAtomWriterUtils.WritePreserveSpaceAttributeIfNeeded(writer, value);
			writer.WriteRaw(value);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00051FA8 File Offset: 0x000501A8
		internal static void WriteString(XmlWriter writer, string value)
		{
			ODataAtomWriterUtils.WritePreserveSpaceAttributeIfNeeded(writer, value);
			writer.WriteString(value);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00051FB8 File Offset: 0x000501B8
		private static XmlWriterSettings CreateXmlWriterSettings(ODataMessageWriterSettings messageWriterSettings, Encoding encoding)
		{
			return new XmlWriterSettings
			{
				CheckCharacters = messageWriterSettings.CheckCharacters,
				ConformanceLevel = ConformanceLevel.Document,
				OmitXmlDeclaration = false,
				Encoding = (encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble),
				NewLineHandling = NewLineHandling.Entitize,
				Indent = messageWriterSettings.Indent,
				CloseOutput = false
			};
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00052010 File Offset: 0x00050210
		private static void WritePreserveSpaceAttributeIfNeeded(XmlWriter writer, string value)
		{
			if (value == null)
			{
				return;
			}
			int length = value.Length;
			if (length > 0 && (char.IsWhiteSpace(value[0]) || char.IsWhiteSpace(value[length - 1])))
			{
				writer.WriteAttributeString("xml", "space", "http://www.w3.org/XML/1998/namespace", "preserve");
			}
		}
	}
}

using System;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200022D RID: 557
	internal static class XmlReaderExtensions
	{
		// Token: 0x060011B2 RID: 4530 RVA: 0x00042072 File Offset: 0x00040272
		[Conditional("DEBUG")]
		internal static void AssertNotBuffering(this BufferingXmlReader bufferedXmlReader)
		{
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00042074 File Offset: 0x00040274
		[Conditional("DEBUG")]
		internal static void AssertBuffering(this BufferingXmlReader bufferedXmlReader)
		{
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00042078 File Offset: 0x00040278
		internal static string ReadElementValue(this XmlReader reader)
		{
			string text = reader.ReadElementContentValue();
			reader.Read();
			return text;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00042094 File Offset: 0x00040294
		internal static string ReadFirstTextNodeValue(this XmlReader reader)
		{
			reader.MoveToElement();
			string text = null;
			if (!reader.IsEmptyElement)
			{
				bool flag = false;
				while (!flag && reader.Read())
				{
					XmlNodeType nodeType = reader.NodeType;
					switch (nodeType)
					{
					case XmlNodeType.Element:
						reader.SkipElementContent();
						continue;
					case XmlNodeType.Attribute:
						continue;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						break;
					default:
						switch (nodeType)
						{
						case XmlNodeType.SignificantWhitespace:
							break;
						case XmlNodeType.EndElement:
							flag = true;
							continue;
						default:
							continue;
						}
						break;
					}
					if (text == null)
					{
						text = reader.Value;
					}
				}
			}
			reader.Read();
			return text ?? string.Empty;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0004211C File Offset: 0x0004031C
		internal static string ReadElementContentValue(this XmlReader reader)
		{
			reader.MoveToElement();
			string text = null;
			if (reader.IsEmptyElement)
			{
				text = string.Empty;
			}
			else
			{
				StringBuilder stringBuilder = null;
				bool flag = false;
				while (!flag && reader.Read())
				{
					switch (reader.NodeType)
					{
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.SignificantWhitespace:
						if (text == null)
						{
							text = reader.Value;
							continue;
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append(text);
							stringBuilder.Append(reader.Value);
							continue;
						}
						stringBuilder.Append(reader.Value);
						continue;
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.Whitespace:
						continue;
					case XmlNodeType.EndElement:
						flag = true;
						continue;
					}
					throw new ODataException(Strings.XmlReaderExtension_InvalidNodeInStringValue(reader.NodeType));
				}
				if (stringBuilder != null)
				{
					text = stringBuilder.ToString();
				}
				else if (text == null)
				{
					text = string.Empty;
				}
			}
			return text;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0004220C File Offset: 0x0004040C
		internal static void SkipInsignificantNodes(this XmlReader reader)
		{
			for (;;)
			{
				XmlNodeType nodeType = reader.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.None:
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
					break;
				case XmlNodeType.Element:
					return;
				case XmlNodeType.Attribute:
				case XmlNodeType.CDATA:
				case XmlNodeType.EntityReference:
				case XmlNodeType.Entity:
					return;
				case XmlNodeType.Text:
					if (!XmlReaderExtensions.IsNullOrWhitespace(reader.Value))
					{
						return;
					}
					break;
				default:
					if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.XmlDeclaration)
					{
						return;
					}
					break;
				}
				if (!reader.Read())
				{
					return;
				}
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0004226B File Offset: 0x0004046B
		internal static void SkipElementContent(this XmlReader reader)
		{
			reader.MoveToElement();
			if (!reader.IsEmptyElement)
			{
				reader.Read();
				while (reader.NodeType != XmlNodeType.EndElement)
				{
					reader.Skip();
				}
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00042295 File Offset: 0x00040495
		internal static void ReadPayloadStart(this XmlReader reader)
		{
			reader.SkipInsignificantNodes();
			if (reader.NodeType != XmlNodeType.Element)
			{
				throw new ODataException(Strings.XmlReaderExtension_InvalidRootNode(reader.NodeType));
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x000422BC File Offset: 0x000404BC
		internal static void ReadPayloadEnd(this XmlReader reader)
		{
			reader.SkipInsignificantNodes();
			if (reader.NodeType != XmlNodeType.None && !reader.EOF)
			{
				throw new ODataException(Strings.XmlReaderExtension_InvalidRootNode(reader.NodeType));
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000422EA File Offset: 0x000404EA
		internal static bool NamespaceEquals(this XmlReader reader, string namespaceUri)
		{
			return object.ReferenceEquals(reader.NamespaceURI, namespaceUri);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000422F8 File Offset: 0x000404F8
		internal static bool LocalNameEquals(this XmlReader reader, string localName)
		{
			return object.ReferenceEquals(reader.LocalName, localName);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00042306 File Offset: 0x00040506
		internal static bool TryReadEmptyElement(this XmlReader reader)
		{
			reader.MoveToElement();
			return reader.IsEmptyElement || (reader.Read() && reader.NodeType == XmlNodeType.EndElement);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0004232E File Offset: 0x0004052E
		internal static bool TryReadToNextElement(this XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00042346 File Offset: 0x00040546
		private static bool IsNullOrWhitespace(string text)
		{
			return string.IsNullOrWhiteSpace(text);
		}
	}
}

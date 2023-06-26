using System;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001E1 RID: 481
	internal abstract class ODataAtomMetadataDeserializer : ODataAtomDeserializer
	{
		// Token: 0x06000EE0 RID: 3808 RVA: 0x00034B98 File Offset: 0x00032D98
		internal ODataAtomMetadataDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.AtomNamespace = nameTable.Add("http://www.w3.org/2005/Atom");
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00034BDA File Offset: 0x00032DDA
		protected bool ReadAtomMetadata
		{
			get
			{
				return base.AtomInputContext.MessageReaderSettings.EnableAtomMetadataReading;
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00034BEC File Offset: 0x00032DEC
		protected AtomPersonMetadata ReadAtomPersonConstruct(EpmTargetPathSegment epmTargetPathSegment)
		{
			AtomPersonMetadata atomPersonMetadata = new AtomPersonMetadata();
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.Read();
				for (;;)
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							goto IL_FE;
						}
					}
					else
					{
						EpmTargetPathSegment epmTargetPathSegment2;
						string localName;
						if (!base.XmlReader.NamespaceEquals(this.AtomNamespace) || !this.ShouldReadElement(epmTargetPathSegment, base.XmlReader.LocalName, out epmTargetPathSegment2) || (localName = base.XmlReader.LocalName) == null)
						{
							goto IL_FE;
						}
						if (!(localName == "name"))
						{
							if (!(localName == "uri"))
							{
								if (!(localName == "email"))
								{
									goto IL_FE;
								}
								atomPersonMetadata.Email = this.ReadElementStringValue();
							}
							else
							{
								Uri xmlBaseUri = base.XmlReader.XmlBaseUri;
								string text = this.ReadElementStringValue();
								if (epmTargetPathSegment2 != null)
								{
									atomPersonMetadata.UriFromEpm = text;
								}
								if (this.ReadAtomMetadata)
								{
									atomPersonMetadata.Uri = base.ProcessUriFromPayload(text, xmlBaseUri);
								}
							}
						}
						else
						{
							atomPersonMetadata.Name = this.ReadElementStringValue();
						}
					}
					IL_109:
					if (base.XmlReader.NodeType == XmlNodeType.EndElement)
					{
						break;
					}
					continue;
					IL_FE:
					base.XmlReader.Skip();
					goto IL_109;
				}
			}
			base.XmlReader.Read();
			return atomPersonMetadata;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00034D24 File Offset: 0x00032F24
		protected DateTimeOffset? ReadAtomDateConstruct()
		{
			string text = this.ReadElementStringValue();
			text = text.Trim();
			if (text.Length >= 20)
			{
				if (text[19] == '.')
				{
					int num = 20;
					while (text.Length > num && char.IsDigit(text[num]))
					{
						num++;
					}
					text = text.Substring(0, 19) + text.Substring(num);
				}
				DateTimeOffset dateTimeOffset;
				if (DateTimeOffset.TryParseExact(text, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out dateTimeOffset))
				{
					return new DateTimeOffset?(dateTimeOffset);
				}
				if (DateTimeOffset.TryParseExact(text, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTimeOffset))
				{
					return new DateTimeOffset?(dateTimeOffset);
				}
			}
			return new DateTimeOffset?(PlatformHelper.ConvertStringToDateTimeOffset(text));
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00034DDC File Offset: 0x00032FDC
		protected string ReadAtomDateConstructAsString()
		{
			return this.ReadElementStringValue();
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00034DF4 File Offset: 0x00032FF4
		protected AtomTextConstruct ReadAtomTextConstruct()
		{
			AtomTextConstruct atomTextConstruct = new AtomTextConstruct();
			string text = null;
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace) && string.CompareOrdinal(base.XmlReader.LocalName, "type") == 0)
				{
					text = base.XmlReader.Value;
				}
			}
			base.XmlReader.MoveToElement();
			if (text != null)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (text2 == "text")
					{
						atomTextConstruct.Kind = AtomTextConstructKind.Text;
						goto IL_C5;
					}
					if (text2 == "html")
					{
						atomTextConstruct.Kind = AtomTextConstructKind.Html;
						goto IL_C5;
					}
					if (text2 == "xhtml")
					{
						atomTextConstruct.Kind = AtomTextConstructKind.Xhtml;
						goto IL_C5;
					}
				}
				throw new ODataException(Strings.ODataAtomEntryMetadataDeserializer_InvalidTextConstructKind(text, base.XmlReader.LocalName));
			}
			atomTextConstruct.Kind = AtomTextConstructKind.Text;
			IL_C5:
			if (atomTextConstruct.Kind == AtomTextConstructKind.Xhtml)
			{
				atomTextConstruct.Text = base.XmlReader.ReadInnerXml();
			}
			else
			{
				atomTextConstruct.Text = this.ReadElementStringValue();
			}
			return atomTextConstruct;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00034EEF File Offset: 0x000330EF
		protected string ReadElementStringValue()
		{
			if (base.UseClientFormatBehavior)
			{
				return base.XmlReader.ReadFirstTextNodeValue();
			}
			return base.XmlReader.ReadElementValue();
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00034F10 File Offset: 0x00033110
		protected AtomTextConstruct ReadTitleElement()
		{
			return this.ReadAtomTextConstruct();
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00034F38 File Offset: 0x00033138
		protected bool ShouldReadElement(EpmTargetPathSegment parentSegment, string segmentName, out EpmTargetPathSegment subSegment)
		{
			subSegment = null;
			if (parentSegment != null)
			{
				subSegment = parentSegment.SubSegments.FirstOrDefault((EpmTargetPathSegment segment) => string.CompareOrdinal(segment.SegmentName, segmentName) == 0);
				if (subSegment != null && subSegment.EpmInfo != null && subSegment.EpmInfo.Attribute.KeepInContent)
				{
					return this.ReadAtomMetadata;
				}
			}
			return subSegment != null || this.ReadAtomMetadata;
		}

		// Token: 0x04000522 RID: 1314
		private readonly string EmptyNamespace;

		// Token: 0x04000523 RID: 1315
		private readonly string AtomNamespace;
	}
}

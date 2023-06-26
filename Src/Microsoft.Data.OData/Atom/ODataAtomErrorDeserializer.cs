using System;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001F3 RID: 499
	internal sealed class ODataAtomErrorDeserializer : ODataAtomDeserializer
	{
		// Token: 0x06000F58 RID: 3928 RVA: 0x00036E4A File Offset: 0x0003504A
		internal ODataAtomErrorDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00036E54 File Offset: 0x00035054
		internal static ODataError ReadErrorElement(BufferingXmlReader xmlReader, int maxInnerErrorDepth)
		{
			ODataError odataError = new ODataError();
			ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask duplicateErrorElementPropertyBitMask = ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask.None;
			if (!xmlReader.IsEmptyElement)
			{
				xmlReader.Read();
				for (;;)
				{
					XmlNodeType nodeType = xmlReader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							goto IL_E4;
						}
					}
					else
					{
						string localName;
						if (!xmlReader.NamespaceEquals(xmlReader.ODataMetadataNamespace) || (localName = xmlReader.LocalName) == null)
						{
							goto IL_E4;
						}
						if (!(localName == "code"))
						{
							if (!(localName == "message"))
							{
								if (!(localName == "innererror"))
								{
									goto IL_E4;
								}
								ODataAtomErrorDeserializer.VerifyErrorElementNotFound(ref duplicateErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask.InnerError, "innererror");
								odataError.InnerError = ODataAtomErrorDeserializer.ReadInnerErrorElement(xmlReader, 0, maxInnerErrorDepth);
							}
							else
							{
								ODataAtomErrorDeserializer.VerifyErrorElementNotFound(ref duplicateErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask.Message, "message");
								odataError.MessageLanguage = xmlReader.GetAttribute(xmlReader.XmlLangAttributeName, xmlReader.XmlNamespace);
								odataError.Message = xmlReader.ReadElementValue();
							}
						}
						else
						{
							ODataAtomErrorDeserializer.VerifyErrorElementNotFound(ref duplicateErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask.Code, "code");
							odataError.ErrorCode = xmlReader.ReadElementValue();
						}
					}
					IL_EA:
					if (xmlReader.NodeType == XmlNodeType.EndElement)
					{
						break;
					}
					continue;
					IL_E4:
					xmlReader.Skip();
					goto IL_EA;
				}
			}
			return odataError;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00036F5C File Offset: 0x0003515C
		internal ODataError ReadTopLevelError()
		{
			ODataError odataError2;
			try
			{
				base.XmlReader.DisableInStreamErrorDetection = true;
				base.ReadPayloadStart();
				if (!base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) || !base.XmlReader.LocalNameEquals(base.XmlReader.ODataErrorElementName))
				{
					throw new ODataErrorException(Strings.ODataAtomErrorDeserializer_InvalidRootElement(base.XmlReader.Name, base.XmlReader.NamespaceURI));
				}
				ODataError odataError = ODataAtomErrorDeserializer.ReadErrorElement(base.XmlReader, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
				base.XmlReader.Read();
				base.ReadPayloadEnd();
				odataError2 = odataError;
			}
			finally
			{
				base.XmlReader.DisableInStreamErrorDetection = false;
			}
			return odataError2;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003701C File Offset: 0x0003521C
		private static void VerifyErrorElementNotFound(ref ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask elementsFoundBitField, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask elementFoundBitMask, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName(elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00037037 File Offset: 0x00035237
		private static void VerifyInnerErrorElementNotFound(ref ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask elementsFoundBitField, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask elementFoundBitMask, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName(elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00037054 File Offset: 0x00035254
		private static ODataInnerError ReadInnerErrorElement(BufferingXmlReader xmlReader, int recursionDepth, int maxInnerErrorDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, maxInnerErrorDepth);
			ODataInnerError odataInnerError = new ODataInnerError();
			ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask duplicateInnerErrorElementPropertyBitMask = ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask.None;
			if (!xmlReader.IsEmptyElement)
			{
				xmlReader.Read();
				for (;;)
				{
					XmlNodeType nodeType = xmlReader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							goto IL_FC;
						}
					}
					else
					{
						string localName;
						if (!xmlReader.NamespaceEquals(xmlReader.ODataMetadataNamespace) || (localName = xmlReader.LocalName) == null)
						{
							goto IL_FC;
						}
						if (!(localName == "message"))
						{
							if (!(localName == "type"))
							{
								if (!(localName == "stacktrace"))
								{
									if (!(localName == "internalexception"))
									{
										goto IL_FC;
									}
									ODataAtomErrorDeserializer.VerifyInnerErrorElementNotFound(ref duplicateInnerErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask.InternalException, "internalexception");
									odataInnerError.InnerError = ODataAtomErrorDeserializer.ReadInnerErrorElement(xmlReader, recursionDepth, maxInnerErrorDepth);
								}
								else
								{
									ODataAtomErrorDeserializer.VerifyInnerErrorElementNotFound(ref duplicateInnerErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask.StackTrace, "stacktrace");
									odataInnerError.StackTrace = xmlReader.ReadElementValue();
								}
							}
							else
							{
								ODataAtomErrorDeserializer.VerifyInnerErrorElementNotFound(ref duplicateInnerErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask.TypeName, "type");
								odataInnerError.TypeName = xmlReader.ReadElementValue();
							}
						}
						else
						{
							ODataAtomErrorDeserializer.VerifyInnerErrorElementNotFound(ref duplicateInnerErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask.Message, "message");
							odataInnerError.Message = xmlReader.ReadElementValue();
						}
					}
					IL_102:
					if (xmlReader.NodeType == XmlNodeType.EndElement)
					{
						break;
					}
					continue;
					IL_FC:
					xmlReader.Skip();
					goto IL_102;
				}
			}
			xmlReader.Read();
			return odataInnerError;
		}

		// Token: 0x020001F4 RID: 500
		[Flags]
		private enum DuplicateErrorElementPropertyBitMask
		{
			// Token: 0x04000568 RID: 1384
			None = 0,
			// Token: 0x04000569 RID: 1385
			Code = 1,
			// Token: 0x0400056A RID: 1386
			Message = 2,
			// Token: 0x0400056B RID: 1387
			InnerError = 4
		}

		// Token: 0x020001F5 RID: 501
		[Flags]
		private enum DuplicateInnerErrorElementPropertyBitMask
		{
			// Token: 0x0400056D RID: 1389
			None = 0,
			// Token: 0x0400056E RID: 1390
			Message = 1,
			// Token: 0x0400056F RID: 1391
			TypeName = 2,
			// Token: 0x04000570 RID: 1392
			StackTrace = 4,
			// Token: 0x04000571 RID: 1393
			InternalException = 8
		}
	}
}

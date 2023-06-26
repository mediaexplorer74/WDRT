using System;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000E7 RID: 231
	internal sealed class AtomInstanceAnnotation
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x00013D3C File Offset: 0x00011F3C
		private AtomInstanceAnnotation(string target, string term, ODataValue value)
		{
			this.target = target;
			this.term = term;
			this.value = value;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00013D59 File Offset: 0x00011F59
		internal string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00013D61 File Offset: 0x00011F61
		internal string TermName
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00013D69 File Offset: 0x00011F69
		internal ODataValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00013D71 File Offset: 0x00011F71
		internal bool IsTargetingCurrentElement
		{
			get
			{
				return string.IsNullOrEmpty(this.Target) || string.Equals(this.Target, ".", StringComparison.Ordinal);
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00013D93 File Offset: 0x00011F93
		internal static AtomInstanceAnnotation CreateFrom(ODataInstanceAnnotation odataInstanceAnnotation, string target)
		{
			return new AtomInstanceAnnotation(target, odataInstanceAnnotation.Name, odataInstanceAnnotation.Value);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00013DA8 File Offset: 0x00011FA8
		internal static AtomInstanceAnnotation CreateFrom(ODataAtomInputContext inputContext, ODataAtomPropertyAndValueDeserializer propertyAndValueDeserializer)
		{
			BufferingXmlReader xmlReader = inputContext.XmlReader;
			string text = null;
			string text2 = null;
			string text3 = null;
			bool flag = false;
			bool flag2 = false;
			string text4 = null;
			string text5 = null;
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = null;
			XmlNameTable nameTable = xmlReader.NameTable;
			string text6 = nameTable.Get("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			string text7 = nameTable.Get("null");
			string text8 = nameTable.Get("type");
			string text9 = nameTable.Get(string.Empty);
			string text10 = nameTable.Get("term");
			string text11 = nameTable.Get("target");
			while (xmlReader.MoveToNextAttribute())
			{
				if (xmlReader.NamespaceEquals(text6))
				{
					if (xmlReader.LocalNameEquals(text8))
					{
						text3 = xmlReader.Value;
					}
					else if (xmlReader.LocalNameEquals(text7))
					{
						flag = ODataAtomReaderUtils.ReadMetadataNullAttributeValue(xmlReader.Value);
					}
				}
				else if (xmlReader.NamespaceEquals(text9))
				{
					if (xmlReader.LocalNameEquals(text10))
					{
						text = xmlReader.Value;
						if (propertyAndValueDeserializer.MessageReaderSettings.ShouldSkipAnnotation(text))
						{
							xmlReader.MoveToElement();
							return null;
						}
					}
					else if (xmlReader.LocalNameEquals(text11))
					{
						text2 = xmlReader.Value;
					}
					else
					{
						IEdmPrimitiveTypeReference edmPrimitiveTypeReference2 = AtomInstanceAnnotation.LookupEdmTypeByAttributeValueNotationName(xmlReader.LocalName);
						if (edmPrimitiveTypeReference2 != null)
						{
							if (edmPrimitiveTypeReference != null)
							{
								flag2 = true;
							}
							edmPrimitiveTypeReference = edmPrimitiveTypeReference2;
							text4 = xmlReader.LocalName;
							text5 = xmlReader.Value;
						}
					}
				}
			}
			xmlReader.MoveToElement();
			if (text == null)
			{
				throw new ODataException(Strings.AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement);
			}
			if (flag2)
			{
				throw new ODataException(Strings.AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes);
			}
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfValueTerm(text, propertyAndValueDeserializer.Model);
			ODataValue odataValue;
			if (flag)
			{
				ReaderValidationUtils.ValidateNullValue(propertyAndValueDeserializer.Model, edmTypeReference, propertyAndValueDeserializer.MessageReaderSettings, true, propertyAndValueDeserializer.Version, text);
				odataValue = new ODataNullValue();
			}
			else if (edmPrimitiveTypeReference != null)
			{
				odataValue = AtomInstanceAnnotation.GetValueFromAttributeValueNotation(edmTypeReference, edmPrimitiveTypeReference, text4, text5, text3, xmlReader.IsEmptyElement, propertyAndValueDeserializer.Model, propertyAndValueDeserializer.MessageReaderSettings, propertyAndValueDeserializer.Version);
			}
			else
			{
				odataValue = AtomInstanceAnnotation.ReadValueFromElementContent(propertyAndValueDeserializer, edmTypeReference);
			}
			xmlReader.Read();
			return new AtomInstanceAnnotation(text2, text, odataValue);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00013F8C File Offset: 0x0001218C
		internal static string LookupAttributeValueNotationNameByEdmTypeKind(EdmPrimitiveTypeKind typeKind)
		{
			if (typeKind != EdmPrimitiveTypeKind.Boolean)
			{
				switch (typeKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
					return "decimal";
				case EdmPrimitiveTypeKind.Double:
					return "float";
				case EdmPrimitiveTypeKind.Guid:
				case EdmPrimitiveTypeKind.Int16:
					break;
				case EdmPrimitiveTypeKind.Int32:
					return "int";
				default:
					if (typeKind == EdmPrimitiveTypeKind.String)
					{
						return "string";
					}
					break;
				}
				return null;
			}
			return "bool";
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00013FE4 File Offset: 0x000121E4
		internal static IEdmPrimitiveTypeReference LookupEdmTypeByAttributeValueNotationName(string attributeName)
		{
			if (attributeName != null)
			{
				if (attributeName == "int")
				{
					return EdmCoreModel.Instance.GetInt32(false);
				}
				if (attributeName == "string")
				{
					return EdmCoreModel.Instance.GetString(false);
				}
				if (attributeName == "float")
				{
					return EdmCoreModel.Instance.GetDouble(false);
				}
				if (attributeName == "bool")
				{
					return EdmCoreModel.Instance.GetBoolean(false);
				}
				if (attributeName == "decimal")
				{
					return EdmCoreModel.Instance.GetDecimal(false);
				}
			}
			return null;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00014078 File Offset: 0x00012278
		private static ODataValue ReadValueFromElementContent(ODataAtomPropertyAndValueDeserializer propertyAndValueDeserializer, IEdmTypeReference expectedType)
		{
			object obj = propertyAndValueDeserializer.ReadNonEntityValue(expectedType, null, null, true, false);
			return obj.ToODataValue();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00014098 File Offset: 0x00012298
		private static ODataPrimitiveValue GetValueFromAttributeValueNotation(IEdmTypeReference expectedTypeReference, IEdmPrimitiveTypeReference attributeValueNotationTypeReference, string attributeValueNotationAttributeName, string attributeValueNotationAttributeValue, string typeAttributeValue, bool positionedOnEmptyElement, IEdmModel model, ODataMessageReaderSettings messageReaderSettings, ODataVersion version)
		{
			if (!positionedOnEmptyElement)
			{
				throw new ODataException(Strings.AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement(attributeValueNotationAttributeName));
			}
			if (typeAttributeValue != null && !string.Equals(attributeValueNotationTypeReference.Definition.ODataFullName(), typeAttributeValue, StringComparison.Ordinal))
			{
				throw new ODataException(Strings.AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType(typeAttributeValue, attributeValueNotationAttributeName));
			}
			IEdmTypeReference edmTypeReference = ReaderValidationUtils.ResolveAndValidatePrimitiveTargetType(expectedTypeReference, EdmTypeKind.Primitive, attributeValueNotationTypeReference.Definition, attributeValueNotationTypeReference.ODataFullName(), attributeValueNotationTypeReference.Definition, model, messageReaderSettings, version);
			return new ODataPrimitiveValue(AtomValueUtils.ConvertStringToPrimitive(attributeValueNotationAttributeValue, edmTypeReference.AsPrimitive()));
		}

		// Token: 0x04000260 RID: 608
		private readonly string target;

		// Token: 0x04000261 RID: 609
		private readonly string term;

		// Token: 0x04000262 RID: 610
		private readonly ODataValue value;
	}
}

using System;
using System.Xml;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200021A RID: 538
	internal sealed class ODataAtomCollectionDeserializer : ODataAtomPropertyAndValueDeserializer
	{
		// Token: 0x060010B1 RID: 4273 RVA: 0x0003CF73 File Offset: 0x0003B173
		internal ODataAtomCollectionDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			this.duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0003CF88 File Offset: 0x0003B188
		internal ODataCollectionStart ReadCollectionStart(out bool isCollectionElementEmpty)
		{
			if (!base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
			{
				throw new ODataException(Strings.ODataAtomCollectionDeserializer_TopLevelCollectionElementWrongNamespace(base.XmlReader.NamespaceURI, base.XmlReader.ODataNamespace));
			}
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) && (base.XmlReader.LocalNameEquals(this.AtomTypeAttributeName) || base.XmlReader.LocalNameEquals(this.ODataNullAttributeName)))
				{
					throw new ODataException(Strings.ODataAtomCollectionDeserializer_TypeOrNullAttributeNotAllowed);
				}
			}
			base.XmlReader.MoveToElement();
			ODataCollectionStart odataCollectionStart = new ODataCollectionStart();
			odataCollectionStart.Name = base.XmlReader.LocalName;
			isCollectionElementEmpty = base.XmlReader.IsEmptyElement;
			if (!isCollectionElementEmpty)
			{
				base.XmlReader.Read();
			}
			return odataCollectionStart;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0003D065 File Offset: 0x0003B265
		internal void ReadCollectionEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0003D074 File Offset: 0x0003B274
		internal object ReadCollectionItem(IEdmTypeReference expectedItemType, CollectionWithoutExpectedTypeValidator collectionValidator)
		{
			if (!base.XmlReader.LocalNameEquals(this.ODataCollectionItemElementName))
			{
				throw new ODataException(Strings.ODataAtomCollectionDeserializer_WrongCollectionItemElementName(base.XmlReader.LocalName, base.XmlReader.ODataNamespace));
			}
			object obj = base.ReadNonEntityValue(expectedItemType, this.duplicatePropertyNamesChecker, collectionValidator, true, false);
			base.XmlReader.Read();
			return obj;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0003D0D4 File Offset: 0x0003B2D4
		internal void SkipToElementInODataNamespace()
		{
			for (;;)
			{
				XmlNodeType nodeType = base.XmlReader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						return;
					}
					base.XmlReader.Skip();
				}
				else
				{
					if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
					{
						break;
					}
					base.XmlReader.Skip();
				}
				if (base.XmlReader.EOF)
				{
					return;
				}
			}
		}

		// Token: 0x04000609 RID: 1545
		private readonly DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
	}
}

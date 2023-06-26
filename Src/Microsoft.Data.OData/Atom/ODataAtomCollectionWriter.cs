using System;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000275 RID: 629
	internal sealed class ODataAtomCollectionWriter : ODataCollectionWriterCore
	{
		// Token: 0x060014E4 RID: 5348 RVA: 0x0004E0BB File Offset: 0x0004C2BB
		internal ODataAtomCollectionWriter(ODataAtomOutputContext atomOutputContext, IEdmTypeReference itemTypeReference)
			: base(atomOutputContext, itemTypeReference)
		{
			this.atomOutputContext = atomOutputContext;
			this.atomCollectionSerializer = new ODataAtomCollectionSerializer(atomOutputContext);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0004E0D8 File Offset: 0x0004C2D8
		protected override void VerifyNotDisposed()
		{
			this.atomOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0004E0E5 File Offset: 0x0004C2E5
		protected override void FlushSynchronously()
		{
			this.atomOutputContext.Flush();
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0004E0F2 File Offset: 0x0004C2F2
		protected override Task FlushAsynchronously()
		{
			return this.atomOutputContext.FlushAsync();
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0004E0FF File Offset: 0x0004C2FF
		protected override void StartPayload()
		{
			this.atomCollectionSerializer.WritePayloadStart();
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0004E10C File Offset: 0x0004C30C
		protected override void EndPayload()
		{
			this.atomCollectionSerializer.WritePayloadEnd();
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0004E11C File Offset: 0x0004C31C
		protected override void StartCollection(ODataCollectionStart collectionStart)
		{
			string name = collectionStart.Name;
			if (name == null)
			{
				throw new ODataException(Strings.ODataAtomCollectionWriter_CollectionNameMustNotBeNull);
			}
			this.atomOutputContext.XmlWriter.WriteStartElement(name, this.atomCollectionSerializer.MessageWriterSettings.WriterBehavior.ODataNamespace);
			this.atomOutputContext.XmlWriter.WriteAttributeString("xmlns", "http://www.w3.org/2000/xmlns/", this.atomCollectionSerializer.MessageWriterSettings.WriterBehavior.ODataNamespace);
			this.atomCollectionSerializer.WriteDefaultNamespaceAttributes(ODataAtomSerializer.DefaultNamespaceFlags.ODataMetadata | ODataAtomSerializer.DefaultNamespaceFlags.GeoRss | ODataAtomSerializer.DefaultNamespaceFlags.Gml);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0004E1A0 File Offset: 0x0004C3A0
		protected override void EndCollection()
		{
			this.atomOutputContext.XmlWriter.WriteEndElement();
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0004E1B4 File Offset: 0x0004C3B4
		protected override void WriteCollectionItem(object item, IEdmTypeReference expectedItemType)
		{
			this.atomOutputContext.XmlWriter.WriteStartElement("element", this.atomCollectionSerializer.MessageWriterSettings.WriterBehavior.ODataNamespace);
			if (item == null)
			{
				ValidationUtils.ValidateNullCollectionItem(expectedItemType, this.atomOutputContext.MessageWriterSettings.WriterBehavior);
				this.atomOutputContext.XmlWriter.WriteAttributeString("null", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", "true");
			}
			else
			{
				ODataComplexValue odataComplexValue = item as ODataComplexValue;
				if (odataComplexValue != null)
				{
					this.atomCollectionSerializer.WriteComplexValue(odataComplexValue, expectedItemType, false, true, null, null, base.DuplicatePropertyNamesChecker, base.CollectionValidator, null, null, null);
					base.DuplicatePropertyNamesChecker.Clear();
				}
				else
				{
					this.atomCollectionSerializer.WritePrimitiveValue(item, base.CollectionValidator, expectedItemType, null);
				}
			}
			this.atomOutputContext.XmlWriter.WriteEndElement();
		}

		// Token: 0x0400075E RID: 1886
		private readonly ODataAtomOutputContext atomOutputContext;

		// Token: 0x0400075F RID: 1887
		private readonly ODataAtomCollectionSerializer atomCollectionSerializer;
	}
}

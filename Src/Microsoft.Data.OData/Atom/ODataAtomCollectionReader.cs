using System;
using System.Xml;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000227 RID: 551
	internal sealed class ODataAtomCollectionReader : ODataCollectionReaderCore
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x00040CC5 File Offset: 0x0003EEC5
		internal ODataAtomCollectionReader(ODataAtomInputContext atomInputContext, IEdmTypeReference expectedItemTypeReference)
			: base(atomInputContext, expectedItemTypeReference, null)
		{
			this.atomInputContext = atomInputContext;
			this.atomCollectionDeserializer = new ODataAtomCollectionDeserializer(atomInputContext);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00040CE4 File Offset: 0x0003EEE4
		protected override bool ReadAtStartImplementation()
		{
			this.atomCollectionDeserializer.ReadPayloadStart();
			bool flag;
			ODataCollectionStart odataCollectionStart = this.atomCollectionDeserializer.ReadCollectionStart(out flag);
			base.EnterScope(ODataCollectionReaderState.CollectionStart, odataCollectionStart, flag);
			return true;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00040D14 File Offset: 0x0003EF14
		protected override bool ReadAtCollectionStartImplementation()
		{
			this.atomCollectionDeserializer.SkipToElementInODataNamespace();
			if (this.atomCollectionDeserializer.XmlReader.NodeType == XmlNodeType.EndElement || base.IsCollectionElementEmpty)
			{
				this.atomCollectionDeserializer.ReadCollectionEnd();
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.atomCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.EnterScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00040D84 File Offset: 0x0003EF84
		protected override bool ReadAtValueImplementation()
		{
			this.atomCollectionDeserializer.SkipToElementInODataNamespace();
			if (this.atomInputContext.XmlReader.NodeType == XmlNodeType.EndElement)
			{
				this.atomCollectionDeserializer.ReadCollectionEnd();
				base.PopScope(ODataCollectionReaderState.Value);
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.atomCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.ReplaceScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00040DF2 File Offset: 0x0003EFF2
		protected override bool ReadAtCollectionEndImplementation()
		{
			this.atomCollectionDeserializer.ReadPayloadEnd();
			base.PopScope(ODataCollectionReaderState.CollectionEnd);
			base.ReplaceScope(ODataCollectionReaderState.Completed, null);
			return false;
		}

		// Token: 0x0400065D RID: 1629
		private readonly ODataAtomInputContext atomInputContext;

		// Token: 0x0400065E RID: 1630
		private readonly ODataAtomCollectionDeserializer atomCollectionDeserializer;
	}
}

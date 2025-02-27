﻿using System;
using System.Linq;
using System.Xml;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001F9 RID: 505
	internal abstract class ODataAtomEpmDeserializer : ODataAtomMetadataDeserializer
	{
		// Token: 0x06000F6D RID: 3949 RVA: 0x0003758D File Offset: 0x0003578D
		internal ODataAtomEpmDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00037598 File Offset: 0x00035798
		internal bool TryReadExtensionElementInEntryContent(IODataAtomReaderEntryState entryState)
		{
			ODataEntityPropertyMappingCache cachedEpm = entryState.CachedEpm;
			if (cachedEpm == null)
			{
				return false;
			}
			EpmTargetPathSegment nonSyndicationRoot = cachedEpm.EpmTargetTree.NonSyndicationRoot;
			return this.TryReadCustomEpmElement(entryState, nonSyndicationRoot);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00037600 File Offset: 0x00035800
		private bool TryReadCustomEpmElement(IODataAtomReaderEntryState entryState, EpmTargetPathSegment epmTargetPathSegment)
		{
			string localName = base.XmlReader.LocalName;
			string namespaceUri = base.XmlReader.NamespaceURI;
			EpmTargetPathSegment epmTargetPathSegment2 = epmTargetPathSegment.SubSegments.FirstOrDefault((EpmTargetPathSegment segment) => !segment.IsAttribute && string.CompareOrdinal(segment.SegmentName, localName) == 0 && string.CompareOrdinal(segment.SegmentNamespaceUri, namespaceUri) == 0);
			if (epmTargetPathSegment2 == null)
			{
				return false;
			}
			if (epmTargetPathSegment2.HasContent && entryState.EpmCustomReaderValueCache.Contains(epmTargetPathSegment2.EpmInfo))
			{
				return false;
			}
			while (base.XmlReader.MoveToNextAttribute())
			{
				this.ReadCustomEpmAttribute(entryState, epmTargetPathSegment2);
			}
			base.XmlReader.MoveToElement();
			if (epmTargetPathSegment2.HasContent)
			{
				string text = base.ReadElementStringValue();
				entryState.EpmCustomReaderValueCache.Add(epmTargetPathSegment2.EpmInfo, text);
			}
			else
			{
				if (!base.XmlReader.IsEmptyElement)
				{
					base.XmlReader.Read();
					while (base.XmlReader.NodeType != XmlNodeType.EndElement)
					{
						XmlNodeType nodeType = base.XmlReader.NodeType;
						if (nodeType != XmlNodeType.Element)
						{
							if (nodeType != XmlNodeType.EndElement)
							{
								base.XmlReader.Skip();
							}
						}
						else if (!this.TryReadCustomEpmElement(entryState, epmTargetPathSegment2))
						{
							base.XmlReader.Skip();
						}
					}
				}
				base.XmlReader.Read();
			}
			return true;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00037760 File Offset: 0x00035960
		private void ReadCustomEpmAttribute(IODataAtomReaderEntryState entryState, EpmTargetPathSegment epmTargetPathSegmentForElement)
		{
			string localName = base.XmlReader.LocalName;
			string namespaceUri = base.XmlReader.NamespaceURI;
			EpmTargetPathSegment epmTargetPathSegment = epmTargetPathSegmentForElement.SubSegments.FirstOrDefault((EpmTargetPathSegment segment) => segment.IsAttribute && string.CompareOrdinal(segment.AttributeName, localName) == 0 && string.CompareOrdinal(segment.SegmentNamespaceUri, namespaceUri) == 0);
			if (epmTargetPathSegment != null && !entryState.EpmCustomReaderValueCache.Contains(epmTargetPathSegment.EpmInfo))
			{
				entryState.EpmCustomReaderValueCache.Add(epmTargetPathSegment.EpmInfo, base.XmlReader.Value);
			}
		}
	}
}

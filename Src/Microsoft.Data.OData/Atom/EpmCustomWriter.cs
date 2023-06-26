using System;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000269 RID: 617
	internal sealed class EpmCustomWriter : EpmWriter
	{
		// Token: 0x06001462 RID: 5218 RVA: 0x0004C157 File Offset: 0x0004A357
		private EpmCustomWriter(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0004C160 File Offset: 0x0004A360
		internal static void WriteEntryEpm(XmlWriter writer, EpmTargetTree epmTargetTree, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType, ODataAtomOutputContext atomOutputContext)
		{
			EpmCustomWriter epmCustomWriter = new EpmCustomWriter(atomOutputContext);
			epmCustomWriter.WriteEntryEpm(writer, epmTargetTree, epmValueCache, entityType);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0004C17F File Offset: 0x0004A37F
		private static void WriteNamespaceDeclaration(XmlWriter writer, EpmTargetPathSegment targetSegment, ref string alreadyDeclaredPrefix)
		{
			if (alreadyDeclaredPrefix == null)
			{
				writer.WriteAttributeString("xmlns", targetSegment.SegmentNamespacePrefix, "http://www.w3.org/2000/xmlns/", targetSegment.SegmentNamespaceUri);
				alreadyDeclaredPrefix = targetSegment.SegmentNamespacePrefix;
			}
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0004C1AC File Offset: 0x0004A3AC
		private void WriteEntryEpm(XmlWriter writer, EpmTargetTree epmTargetTree, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType)
		{
			EpmTargetPathSegment nonSyndicationRoot = epmTargetTree.NonSyndicationRoot;
			if (nonSyndicationRoot.SubSegments.Count == 0)
			{
				return;
			}
			foreach (EpmTargetPathSegment epmTargetPathSegment in nonSyndicationRoot.SubSegments)
			{
				string text = null;
				this.WriteElementEpm(writer, epmTargetPathSegment, epmValueCache, entityType, ref text);
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0004C21C File Offset: 0x0004A41C
		private void WriteElementEpm(XmlWriter writer, EpmTargetPathSegment targetSegment, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType, ref string alreadyDeclaredPrefix)
		{
			string text = targetSegment.SegmentNamespacePrefix ?? string.Empty;
			writer.WriteStartElement(text, targetSegment.SegmentName, targetSegment.SegmentNamespaceUri);
			if (text.Length > 0)
			{
				EpmCustomWriter.WriteNamespaceDeclaration(writer, targetSegment, ref alreadyDeclaredPrefix);
			}
			foreach (EpmTargetPathSegment epmTargetPathSegment in targetSegment.SubSegments)
			{
				if (epmTargetPathSegment.IsAttribute)
				{
					this.WriteAttributeEpm(writer, epmTargetPathSegment, epmValueCache, entityType, ref alreadyDeclaredPrefix);
				}
			}
			if (targetSegment.HasContent)
			{
				string entryPropertyValueAsText = this.GetEntryPropertyValueAsText(targetSegment, epmValueCache, entityType);
				ODataAtomWriterUtils.WriteString(writer, entryPropertyValueAsText);
			}
			else
			{
				foreach (EpmTargetPathSegment epmTargetPathSegment2 in targetSegment.SubSegments)
				{
					if (!epmTargetPathSegment2.IsAttribute)
					{
						this.WriteElementEpm(writer, epmTargetPathSegment2, epmValueCache, entityType, ref alreadyDeclaredPrefix);
					}
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0004C328 File Offset: 0x0004A528
		private void WriteAttributeEpm(XmlWriter writer, EpmTargetPathSegment targetSegment, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType, ref string alreadyDeclaredPrefix)
		{
			string entryPropertyValueAsText = this.GetEntryPropertyValueAsText(targetSegment, epmValueCache, entityType);
			string text = targetSegment.SegmentNamespacePrefix ?? string.Empty;
			writer.WriteAttributeString(text, targetSegment.AttributeName, targetSegment.SegmentNamespaceUri, entryPropertyValueAsText);
			if (text.Length > 0)
			{
				EpmCustomWriter.WriteNamespaceDeclaration(writer, targetSegment, ref alreadyDeclaredPrefix);
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0004C378 File Offset: 0x0004A578
		private string GetEntryPropertyValueAsText(EpmTargetPathSegment targetSegment, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType)
		{
			object obj = base.ReadEntryPropertyValue(targetSegment.EpmInfo, epmValueCache, entityType);
			if (obj == null)
			{
				return string.Empty;
			}
			return EpmWriterUtils.GetPropertyValueAsText(obj);
		}
	}
}

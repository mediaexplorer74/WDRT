using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000074 RID: 116
	internal sealed class ReadingEntityInfo
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x000105A7 File Offset: 0x0000E7A7
		internal ReadingEntityInfo(XElement payload, Uri uri)
		{
			this.EntryPayload = payload;
			this.BaseUri = uri;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000105C0 File Offset: 0x0000E7C0
		internal static XmlReader BufferAndCacheEntryPayload(ODataEntry entry, XmlReader entryReader, Uri baseUri)
		{
			XElement xelement = XElement.Load(entryReader.ReadSubtree(), LoadOptions.None);
			entryReader.Read();
			entry.SetAnnotation<ReadingEntityInfo>(new ReadingEntityInfo(xelement, baseUri));
			XmlReader xmlReader = xelement.CreateReader();
			xmlReader.Read();
			return xmlReader;
		}

		// Token: 0x040002B7 RID: 695
		internal readonly XElement EntryPayload;

		// Token: 0x040002B8 RID: 696
		internal readonly Uri BaseUri;
	}
}

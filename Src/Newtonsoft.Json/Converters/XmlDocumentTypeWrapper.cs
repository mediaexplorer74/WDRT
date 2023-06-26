using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F1 RID: 241
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlDocumentTypeWrapper : XmlNodeWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000CA0 RID: 3232 RVA: 0x00032D6D File Offset: 0x00030F6D
		public XmlDocumentTypeWrapper(XmlDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00032D7D File Offset: 0x00030F7D
		public string Name
		{
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00032D8A File Offset: 0x00030F8A
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00032D97 File Offset: 0x00030F97
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00032DA4 File Offset: 0x00030FA4
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x00032DB1 File Offset: 0x00030FB1
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x040003EB RID: 1003
		private readonly XmlDocumentType _documentType;
	}
}

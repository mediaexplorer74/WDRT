using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F9 RID: 249
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDocumentTypeWrapper : XObjectWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000CDF RID: 3295 RVA: 0x000330D9 File Offset: 0x000312D9
		public XDocumentTypeWrapper(XDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x000330E9 File Offset: 0x000312E9
		public string Name
		{
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x000330F6 File Offset: 0x000312F6
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00033103 File Offset: 0x00031303
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00033110 File Offset: 0x00031310
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0003311D File Offset: 0x0003131D
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x040003F0 RID: 1008
		private readonly XDocumentType _documentType;
	}
}

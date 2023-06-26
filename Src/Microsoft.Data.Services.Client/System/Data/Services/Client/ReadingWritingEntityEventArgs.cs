using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace System.Data.Services.Client
{
	// Token: 0x02000110 RID: 272
	public sealed class ReadingWritingEntityEventArgs : EventArgs
	{
		// Token: 0x060008E8 RID: 2280 RVA: 0x00024BE7 File Offset: 0x00022DE7
		internal ReadingWritingEntityEventArgs(object entity, XElement data, Uri baseUri)
		{
			this.entity = entity;
			this.data = data;
			this.baseUri = baseUri;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00024C04 File Offset: 0x00022E04
		public object Entity
		{
			get
			{
				return this.entity;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00024C0C File Offset: 0x00022E0C
		public XElement Data
		{
			[DebuggerStepThrough]
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00024C14 File Offset: 0x00022E14
		public Uri BaseUri
		{
			[DebuggerStepThrough]
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x04000519 RID: 1305
		private object entity;

		// Token: 0x0400051A RID: 1306
		private XElement data;

		// Token: 0x0400051B RID: 1307
		private Uri baseUri;
	}
}

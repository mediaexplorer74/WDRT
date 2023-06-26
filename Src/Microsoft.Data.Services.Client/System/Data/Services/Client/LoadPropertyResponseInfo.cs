using System;
using System.Data.Services.Client.Metadata;

namespace System.Data.Services.Client
{
	// Token: 0x02000088 RID: 136
	internal class LoadPropertyResponseInfo : ResponseInfo
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x00013EF2 File Offset: 0x000120F2
		internal LoadPropertyResponseInfo(RequestInfo requestInfo, MergeOption mergeOption, EntityDescriptor entityDescriptor, ClientPropertyAnnotation property)
			: base(requestInfo, mergeOption)
		{
			this.EntityDescriptor = entityDescriptor;
			this.Property = property;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00013F0B File Offset: 0x0001210B
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00013F13 File Offset: 0x00012113
		internal EntityDescriptor EntityDescriptor { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00013F1C File Offset: 0x0001211C
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00013F24 File Offset: 0x00012124
		internal ClientPropertyAnnotation Property { get; private set; }
	}
}

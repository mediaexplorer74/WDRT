using System;
using System.Collections.Generic;

namespace System.Data.Services.Client.Serializers
{
	// Token: 0x02000018 RID: 24
	internal class EpmSourcePathSegment
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003BFE File Offset: 0x00001DFE
		internal EpmSourcePathSegment()
		{
			this.propertyName = null;
			this.subProperties = new List<EpmSourcePathSegment>();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003C18 File Offset: 0x00001E18
		internal EpmSourcePathSegment(string propertyName)
		{
			this.propertyName = propertyName;
			this.subProperties = new List<EpmSourcePathSegment>();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003C32 File Offset: 0x00001E32
		internal string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003C3A File Offset: 0x00001E3A
		internal List<EpmSourcePathSegment> SubProperties
		{
			get
			{
				return this.subProperties;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003C42 File Offset: 0x00001E42
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003C4A File Offset: 0x00001E4A
		internal EntityPropertyMappingInfo EpmInfo { get; set; }

		// Token: 0x0400016B RID: 363
		private readonly string propertyName;

		// Token: 0x0400016C RID: 364
		private readonly List<EpmSourcePathSegment> subProperties;
	}
}

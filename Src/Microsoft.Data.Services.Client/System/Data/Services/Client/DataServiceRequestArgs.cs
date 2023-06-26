using System;
using System.Collections.Generic;

namespace System.Data.Services.Client
{
	// Token: 0x020000FE RID: 254
	public class DataServiceRequestArgs
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x0002343C File Offset: 0x0002163C
		public DataServiceRequestArgs()
		{
			this.HeaderCollection = new HeaderCollection();
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0002344F File Offset: 0x0002164F
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x00023461 File Offset: 0x00021661
		public string AcceptContentType
		{
			get
			{
				return this.HeaderCollection.GetHeader("Accept");
			}
			set
			{
				this.HeaderCollection.SetHeader("Accept", value);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00023474 File Offset: 0x00021674
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x00023486 File Offset: 0x00021686
		public string ContentType
		{
			get
			{
				return this.HeaderCollection.GetHeader("Content-Type");
			}
			set
			{
				this.HeaderCollection.SetHeader("Content-Type", value);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00023499 File Offset: 0x00021699
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x000234AB File Offset: 0x000216AB
		public string Slug
		{
			get
			{
				return this.HeaderCollection.GetHeader("Slug");
			}
			set
			{
				this.HeaderCollection.SetHeader("Slug", value);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000234BE File Offset: 0x000216BE
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x000234D0 File Offset: 0x000216D0
		public Dictionary<string, string> Headers
		{
			get
			{
				return (Dictionary<string, string>)this.HeaderCollection.UnderlyingDictionary;
			}
			internal set
			{
				this.HeaderCollection = new HeaderCollection(value);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000234DE File Offset: 0x000216DE
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x000234E6 File Offset: 0x000216E6
		internal HeaderCollection HeaderCollection { get; private set; }
	}
}

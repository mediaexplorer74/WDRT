using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000281 RID: 641
	public sealed class AtomPersonMetadata : ODataAnnotatable
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0004E6FC File Offset: 0x0004C8FC
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x0004E704 File Offset: 0x0004C904
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0004E70D File Offset: 0x0004C90D
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x0004E715 File Offset: 0x0004C915
		public Uri Uri { get; set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0004E71E File Offset: 0x0004C91E
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x0004E726 File Offset: 0x0004C926
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0004E72F File Offset: 0x0004C92F
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x0004E737 File Offset: 0x0004C937
		internal string UriFromEpm
		{
			get
			{
				return this.uriFromEpm;
			}
			set
			{
				this.uriFromEpm = value;
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0004E740 File Offset: 0x0004C940
		public static AtomPersonMetadata ToAtomPersonMetadata(string name)
		{
			return new AtomPersonMetadata
			{
				Name = name
			};
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0004E75B File Offset: 0x0004C95B
		public static implicit operator AtomPersonMetadata(string name)
		{
			return AtomPersonMetadata.ToAtomPersonMetadata(name);
		}

		// Token: 0x040007C6 RID: 1990
		private string name;

		// Token: 0x040007C7 RID: 1991
		private string email;

		// Token: 0x040007C8 RID: 1992
		private string uriFromEpm;
	}
}

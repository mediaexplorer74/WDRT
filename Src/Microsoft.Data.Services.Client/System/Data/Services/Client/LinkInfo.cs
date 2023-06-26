using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000067 RID: 103
	public sealed class LinkInfo
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0000ED55 File Offset: 0x0000CF55
		internal LinkInfo(string propertyName)
		{
			this.name = propertyName;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000ED64 File Offset: 0x0000CF64
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public Uri NavigationLink
		{
			get
			{
				return this.navigationLink;
			}
			internal set
			{
				this.navigationLink = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000ED7D File Offset: 0x0000CF7D
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000ED85 File Offset: 0x0000CF85
		public Uri AssociationLink
		{
			get
			{
				return this.associationLink;
			}
			internal set
			{
				this.associationLink = value;
			}
		}

		// Token: 0x04000296 RID: 662
		private Uri navigationLink;

		// Token: 0x04000297 RID: 663
		private Uri associationLink;

		// Token: 0x04000298 RID: 664
		private string name;
	}
}

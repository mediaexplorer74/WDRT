using System;

namespace System.Data.Services.Common
{
	// Token: 0x020000F7 RID: 247
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class EntitySetAttribute : Attribute
	{
		// Token: 0x06000846 RID: 2118 RVA: 0x000232EF File Offset: 0x000214EF
		public EntitySetAttribute(string entitySet)
		{
			this.entitySet = entitySet;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x000232FE File Offset: 0x000214FE
		public string EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x040004E4 RID: 1252
		private readonly string entitySet;
	}
}

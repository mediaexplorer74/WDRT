using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000119 RID: 281
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class MediaEntryAttribute : Attribute
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x00025945 File Offset: 0x00023B45
		public MediaEntryAttribute(string mediaMemberName)
		{
			Util.CheckArgumentNull<string>(mediaMemberName, "mediaMemberName");
			this.mediaMemberName = mediaMemberName;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00025960 File Offset: 0x00023B60
		public string MediaMemberName
		{
			get
			{
				return this.mediaMemberName;
			}
		}

		// Token: 0x04000563 RID: 1379
		private readonly string mediaMemberName;
	}
}

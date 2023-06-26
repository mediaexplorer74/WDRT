using System;
using System.Text;

namespace System.Net.Cache
{
	// Token: 0x0200030B RID: 779
	internal class ResponseCacheControl
	{
		// Token: 0x06001BC2 RID: 7106 RVA: 0x00084174 File Offset: 0x00082374
		internal ResponseCacheControl()
		{
			this.MaxAge = (this.SMaxAge = -1);
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x00084198 File Offset: 0x00082398
		internal bool IsNotEmpty
		{
			get
			{
				return this.Public || this.Private || this.NoCache || this.NoStore || this.MustRevalidate || this.ProxyRevalidate || this.MaxAge != -1 || this.SMaxAge != -1;
			}
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x000841EC File Offset: 0x000823EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Public)
			{
				stringBuilder.Append(" public");
			}
			if (this.Private)
			{
				stringBuilder.Append(" private");
				if (this.PrivateHeaders != null)
				{
					stringBuilder.Append('=');
					for (int i = 0; i < this.PrivateHeaders.Length - 1; i++)
					{
						stringBuilder.Append(this.PrivateHeaders[i]).Append(',');
					}
					stringBuilder.Append(this.PrivateHeaders[this.PrivateHeaders.Length - 1]);
				}
			}
			if (this.NoCache)
			{
				stringBuilder.Append(" no-cache");
				if (this.NoCacheHeaders != null)
				{
					stringBuilder.Append('=');
					for (int j = 0; j < this.NoCacheHeaders.Length - 1; j++)
					{
						stringBuilder.Append(this.NoCacheHeaders[j]).Append(',');
					}
					stringBuilder.Append(this.NoCacheHeaders[this.NoCacheHeaders.Length - 1]);
				}
			}
			if (this.NoStore)
			{
				stringBuilder.Append(" no-store");
			}
			if (this.MustRevalidate)
			{
				stringBuilder.Append(" must-revalidate");
			}
			if (this.ProxyRevalidate)
			{
				stringBuilder.Append(" proxy-revalidate");
			}
			if (this.MaxAge != -1)
			{
				stringBuilder.Append(" max-age=").Append(this.MaxAge);
			}
			if (this.SMaxAge != -1)
			{
				stringBuilder.Append(" s-maxage=").Append(this.SMaxAge);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001B1A RID: 6938
		internal bool Public;

		// Token: 0x04001B1B RID: 6939
		internal bool Private;

		// Token: 0x04001B1C RID: 6940
		internal string[] PrivateHeaders;

		// Token: 0x04001B1D RID: 6941
		internal bool NoCache;

		// Token: 0x04001B1E RID: 6942
		internal string[] NoCacheHeaders;

		// Token: 0x04001B1F RID: 6943
		internal bool NoStore;

		// Token: 0x04001B20 RID: 6944
		internal bool MustRevalidate;

		// Token: 0x04001B21 RID: 6945
		internal bool ProxyRevalidate;

		// Token: 0x04001B22 RID: 6946
		internal int MaxAge;

		// Token: 0x04001B23 RID: 6947
		internal int SMaxAge;
	}
}

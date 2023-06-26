using System;

namespace System.Data.Services.Client
{
	// Token: 0x0200011B RID: 283
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class MimeTypePropertyAttribute : Attribute
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x00025968 File Offset: 0x00023B68
		public MimeTypePropertyAttribute(string dataPropertyName, string mimeTypePropertyName)
		{
			this.dataPropertyName = dataPropertyName;
			this.mimeTypePropertyName = mimeTypePropertyName;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0002597E File Offset: 0x00023B7E
		public string DataPropertyName
		{
			get
			{
				return this.dataPropertyName;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00025986 File Offset: 0x00023B86
		public string MimeTypePropertyName
		{
			get
			{
				return this.mimeTypePropertyName;
			}
		}

		// Token: 0x04000569 RID: 1385
		private readonly string dataPropertyName;

		// Token: 0x0400056A RID: 1386
		private readonly string mimeTypePropertyName;
	}
}

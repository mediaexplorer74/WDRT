using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class JsonExtensionDataAttribute : Attribute
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002FD8 File Offset: 0x000011D8
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002FE0 File Offset: 0x000011E0
		public bool WriteData { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002FE9 File Offset: 0x000011E9
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002FF1 File Offset: 0x000011F1
		public bool ReadData { get; set; }

		// Token: 0x0600009D RID: 157 RVA: 0x00002FFA File Offset: 0x000011FA
		public JsonExtensionDataAttribute()
		{
			this.WriteData = true;
			this.ReadData = true;
		}
	}
}

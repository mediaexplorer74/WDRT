using System;

namespace System.Reflection
{
	// Token: 0x0200013B RID: 315
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	internal sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x0002CD38 File Offset: 0x0002AF38
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0002CD4E File Offset: 0x0002AF4E
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x0002CD56 File Offset: 0x0002AF56
		public string Key { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0002CD5F File Offset: 0x0002AF5F
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x0002CD67 File Offset: 0x0002AF67
		public string Value { get; set; }
	}
}

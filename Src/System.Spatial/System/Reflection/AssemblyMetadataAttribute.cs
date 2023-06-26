using System;

namespace System.Reflection
{
	// Token: 0x0200008C RID: 140
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	internal sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x06000371 RID: 881 RVA: 0x00009A6B File Offset: 0x00007C6B
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00009A81 File Offset: 0x00007C81
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00009A89 File Offset: 0x00007C89
		public string Key { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00009A92 File Offset: 0x00007C92
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00009A9A File Offset: 0x00007C9A
		public string Value { get; set; }
	}
}

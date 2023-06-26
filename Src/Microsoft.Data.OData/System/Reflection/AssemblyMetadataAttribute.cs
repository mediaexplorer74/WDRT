using System;

namespace System.Reflection
{
	// Token: 0x020002B4 RID: 692
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	internal sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x060017AC RID: 6060 RVA: 0x00054FD0 File Offset: 0x000531D0
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x00054FE6 File Offset: 0x000531E6
		// (set) Token: 0x060017AE RID: 6062 RVA: 0x00054FEE File Offset: 0x000531EE
		public string Key { get; set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x00054FF7 File Offset: 0x000531F7
		// (set) Token: 0x060017B0 RID: 6064 RVA: 0x00054FFF File Offset: 0x000531FF
		public string Value { get; set; }
	}
}

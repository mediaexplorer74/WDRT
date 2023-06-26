using System;

namespace System.Reflection
{
	// Token: 0x02000241 RID: 577
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	internal sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x06000D46 RID: 3398 RVA: 0x0002A328 File Offset: 0x00028528
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0002A33E File Offset: 0x0002853E
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x0002A346 File Offset: 0x00028546
		public string Key { get; set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0002A34F File Offset: 0x0002854F
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x0002A357 File Offset: 0x00028557
		public string Value { get; set; }
	}
}

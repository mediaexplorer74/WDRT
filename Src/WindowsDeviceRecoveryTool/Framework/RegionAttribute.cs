using System;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x02000084 RID: 132
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class RegionAttribute : Attribute
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x00016B24 File Offset: 0x00014D24
		public RegionAttribute(params string[] names)
		{
			this.Names = new ReadOnlyCollection<string>(names);
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00016B3B File Offset: 0x00014D3B
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00016B43 File Offset: 0x00014D43
		public ReadOnlyCollection<string> Names { get; private set; }
	}
}

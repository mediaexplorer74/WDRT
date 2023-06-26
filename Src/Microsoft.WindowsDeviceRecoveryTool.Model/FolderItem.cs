using System;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000009 RID: 9
	public class FolderItem
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002B00 File Offset: 0x00000D00
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002B08 File Offset: 0x00000D08
		public string Title { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002B11 File Offset: 0x00000D11
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002B19 File Offset: 0x00000D19
		public string Path { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002B22 File Offset: 0x00000D22
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002B2A File Offset: 0x00000D2A
		public FolderType Type { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002B33 File Offset: 0x00000D33
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002B3B File Offset: 0x00000D3B
		public bool IsExtended { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002B44 File Offset: 0x00000D44
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002B4C File Offset: 0x00000D4C
		public ObservableCollection<FolderItem> Items { get; set; }

		// Token: 0x0600005A RID: 90 RVA: 0x00002B58 File Offset: 0x00000D58
		public override string ToString()
		{
			return this.Title;
		}
	}
}

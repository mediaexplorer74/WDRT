using System;
using Microsoft.WindowsDeviceRecoveryTool.Localization;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Assets
{
	// Token: 0x02000016 RID: 22
	public class ThemeStyle
	{
		// Token: 0x0600006F RID: 111 RVA: 0x0000351C File Offset: 0x0000171C
		public ThemeStyle(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003530 File Offset: 0x00001730
		public string LocalizedName
		{
			get
			{
				return LocalizationManager.GetTranslation(this.Name);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000354D File Offset: 0x0000174D
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003555 File Offset: 0x00001755
		public string Name { get; private set; }

		// Token: 0x06000073 RID: 115 RVA: 0x00003560 File Offset: 0x00001760
		public override string ToString()
		{
			return this.LocalizedName;
		}
	}
}

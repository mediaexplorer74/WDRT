using System;
using System.Drawing;
using System.Windows.Media;
using Microsoft.WindowsDeviceRecoveryTool.Localization;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Assets
{
	// Token: 0x02000017 RID: 23
	public class DictionaryStyle
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003578 File Offset: 0x00001778
		public DictionaryStyle(string name, string fileName, System.Drawing.Color color)
		{
			this.Name = name;
			this.FileName = fileName;
			this.MainColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(color.R, color.G, color.B));
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000035B8 File Offset: 0x000017B8
		public string LocalizedName
		{
			get
			{
				return LocalizationManager.GetTranslation(this.Name);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000035D5 File Offset: 0x000017D5
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000035DD File Offset: 0x000017DD
		public string Name { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000035E6 File Offset: 0x000017E6
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000035EE File Offset: 0x000017EE
		public System.Windows.Media.Brush MainColor { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000035F7 File Offset: 0x000017F7
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000035FF File Offset: 0x000017FF
		internal string FileName { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00003608 File Offset: 0x00001808
		public override string ToString()
		{
			return this.LocalizedName;
		}
	}
}

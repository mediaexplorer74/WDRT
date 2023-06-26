using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004A6 RID: 1190
	internal class GetNameItemEvent : EventArgs
	{
		// Token: 0x06004F2B RID: 20267 RVA: 0x00146033 File Offset: 0x00144233
		public GetNameItemEvent(object defName)
		{
			this.nameItem = defName;
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06004F2C RID: 20268 RVA: 0x00146042 File Offset: 0x00144242
		// (set) Token: 0x06004F2D RID: 20269 RVA: 0x0014604A File Offset: 0x0014424A
		public object Name
		{
			get
			{
				return this.nameItem;
			}
			set
			{
				this.nameItem = value;
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06004F2E RID: 20270 RVA: 0x00146053 File Offset: 0x00144253
		public string NameString
		{
			get
			{
				if (this.nameItem != null)
				{
					return this.nameItem.ToString();
				}
				return "";
			}
		}

		// Token: 0x0400343E RID: 13374
		private object nameItem;
	}
}

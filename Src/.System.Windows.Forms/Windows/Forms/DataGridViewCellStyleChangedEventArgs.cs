using System;

namespace System.Windows.Forms
{
	// Token: 0x020001B2 RID: 434
	internal class DataGridViewCellStyleChangedEventArgs : EventArgs
	{
		// Token: 0x06001EA7 RID: 7847 RVA: 0x0009081B File Offset: 0x0008EA1B
		internal DataGridViewCellStyleChangedEventArgs()
		{
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x00090823 File Offset: 0x0008EA23
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0009082B File Offset: 0x0008EA2B
		internal bool ChangeAffectsPreferredSize
		{
			get
			{
				return this.changeAffectsPreferredSize;
			}
			set
			{
				this.changeAffectsPreferredSize = value;
			}
		}

		// Token: 0x04000CEB RID: 3307
		private bool changeAffectsPreferredSize;
	}
}

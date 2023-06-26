using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004A8 RID: 1192
	internal class GetBoolValueEvent : EventArgs
	{
		// Token: 0x06004F33 RID: 20275 RVA: 0x0014606E File Offset: 0x0014426E
		public GetBoolValueEvent(bool defValue)
		{
			this.value = defValue;
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x0014607D File Offset: 0x0014427D
		// (set) Token: 0x06004F35 RID: 20277 RVA: 0x00146085 File Offset: 0x00144285
		public bool Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0400343F RID: 13375
		private bool value;
	}
}

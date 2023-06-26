using System;

namespace System.Data.Services.Client
{
	// Token: 0x020000F1 RID: 241
	internal class SaveChangesEventArgs : EventArgs
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x00022675 File Offset: 0x00020875
		public SaveChangesEventArgs(DataServiceResponse response)
		{
			this.response = response;
		}

		// Token: 0x040004C7 RID: 1223
		private DataServiceResponse response;
	}
}

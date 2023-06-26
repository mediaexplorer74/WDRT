using System;
using System.ComponentModel;

namespace System.Net.Security
{
	// Token: 0x02000351 RID: 849
	internal class ProtocolToken
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x0008F76B File Offset: 0x0008D96B
		internal bool Failed
		{
			get
			{
				return this.Status != SecurityStatus.OK && this.Status != SecurityStatus.ContinueNeeded;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x0008F787 File Offset: 0x0008D987
		internal bool Done
		{
			get
			{
				return this.Status == SecurityStatus.OK;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x0008F792 File Offset: 0x0008D992
		internal bool Renegotiate
		{
			get
			{
				return this.Status == SecurityStatus.Renegotiate;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x0008F7A1 File Offset: 0x0008D9A1
		internal bool CloseConnection
		{
			get
			{
				return this.Status == SecurityStatus.ContextExpired;
			}
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0008F7B0 File Offset: 0x0008D9B0
		internal ProtocolToken(byte[] data, SecurityStatus errorCode)
		{
			this.Status = errorCode;
			this.Payload = data;
			this.Size = ((data != null) ? data.Length : 0);
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0008F7D5 File Offset: 0x0008D9D5
		internal Win32Exception GetException()
		{
			if (!this.Done)
			{
				return new Win32Exception((int)this.Status);
			}
			return null;
		}

		// Token: 0x04001CCF RID: 7375
		internal SecurityStatus Status;

		// Token: 0x04001CD0 RID: 7376
		internal byte[] Payload;

		// Token: 0x04001CD1 RID: 7377
		internal int Size;
	}
}

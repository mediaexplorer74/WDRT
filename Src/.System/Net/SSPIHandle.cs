using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001F9 RID: 505
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct SSPIHandle
	{
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00064645 File Offset: 0x00062845
		public bool IsZero
		{
			get
			{
				return this.HandleHi == IntPtr.Zero && this.HandleLo == IntPtr.Zero;
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0006466B File Offset: 0x0006286B
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetToInvalid()
		{
			this.HandleHi = IntPtr.Zero;
			this.HandleLo = IntPtr.Zero;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00064683 File Offset: 0x00062883
		public override string ToString()
		{
			return this.HandleHi.ToString("x") + ":" + this.HandleLo.ToString("x");
		}

		// Token: 0x0400153A RID: 5434
		private IntPtr HandleHi;

		// Token: 0x0400153B RID: 5435
		private IntPtr HandleLo;
	}
}

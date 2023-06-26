using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002D4 RID: 724
	internal struct IPOptions
	{
		// Token: 0x060019AF RID: 6575 RVA: 0x0007DFD4 File Offset: 0x0007C1D4
		internal IPOptions(PingOptions options)
		{
			this.ttl = 128;
			this.tos = 0;
			this.flags = 0;
			this.optionsSize = 0;
			this.optionsData = IntPtr.Zero;
			if (options != null)
			{
				this.ttl = (byte)options.Ttl;
				if (options.DontFragment)
				{
					this.flags = 2;
				}
			}
		}

		// Token: 0x04001A1F RID: 6687
		internal byte ttl;

		// Token: 0x04001A20 RID: 6688
		internal byte tos;

		// Token: 0x04001A21 RID: 6689
		internal byte flags;

		// Token: 0x04001A22 RID: 6690
		internal byte optionsSize;

		// Token: 0x04001A23 RID: 6691
		internal IntPtr optionsData;
	}
}

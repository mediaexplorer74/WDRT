using System;

namespace System.Net
{
	// Token: 0x02000130 RID: 304
	internal struct TlsParamaters
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x0003DBD0 File Offset: 0x0003BDD0
		public TlsParamaters(SchProtocols protocols)
		{
			this.cAlpnIds = (this.cDisabledCrypto = 0);
			this.pDisabledCrypto = (this.rgstrAlpnIds = IntPtr.Zero);
			this.dwFlags = TlsParamaters.Flags.Zero;
			if (protocols != SchProtocols.Zero)
			{
				this.grbitDisabledProtocols = (uint)(protocols ^ (SchProtocols)(-1));
				return;
			}
			this.grbitDisabledProtocols = 0U;
		}

		// Token: 0x04001029 RID: 4137
		public int cAlpnIds;

		// Token: 0x0400102A RID: 4138
		public IntPtr rgstrAlpnIds;

		// Token: 0x0400102B RID: 4139
		public uint grbitDisabledProtocols;

		// Token: 0x0400102C RID: 4140
		public int cDisabledCrypto;

		// Token: 0x0400102D RID: 4141
		public IntPtr pDisabledCrypto;

		// Token: 0x0400102E RID: 4142
		public TlsParamaters.Flags dwFlags;

		// Token: 0x0200070C RID: 1804
		[Flags]
		public enum Flags
		{
			// Token: 0x040030F1 RID: 12529
			Zero = 0,
			// Token: 0x040030F2 RID: 12530
			TLS_PARAMS_OPTIONAL = 1
		}
	}
}

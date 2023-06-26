using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000127 RID: 295
	internal struct SSL_EXTRA_CERT_CHAIN_POLICY_PARA
	{
		// Token: 0x06000B31 RID: 2865 RVA: 0x0003D9A6 File Offset: 0x0003BBA6
		internal SSL_EXTRA_CERT_CHAIN_POLICY_PARA(bool amIServer)
		{
			this.u.cbStruct = SSL_EXTRA_CERT_CHAIN_POLICY_PARA.StructSize;
			this.u.cbSize = SSL_EXTRA_CERT_CHAIN_POLICY_PARA.StructSize;
			this.dwAuthType = (amIServer ? 1 : 2);
			this.fdwChecks = 0U;
			this.pwszServerName = null;
		}

		// Token: 0x04000FF4 RID: 4084
		internal SSL_EXTRA_CERT_CHAIN_POLICY_PARA.U u;

		// Token: 0x04000FF5 RID: 4085
		internal int dwAuthType;

		// Token: 0x04000FF6 RID: 4086
		internal uint fdwChecks;

		// Token: 0x04000FF7 RID: 4087
		internal unsafe char* pwszServerName;

		// Token: 0x04000FF8 RID: 4088
		private static readonly uint StructSize = (uint)Marshal.SizeOf(typeof(SSL_EXTRA_CERT_CHAIN_POLICY_PARA));

		// Token: 0x02000709 RID: 1801
		[StructLayout(LayoutKind.Explicit)]
		internal struct U
		{
			// Token: 0x040030DA RID: 12506
			[FieldOffset(0)]
			internal uint cbStruct;

			// Token: 0x040030DB RID: 12507
			[FieldOffset(0)]
			internal uint cbSize;
		}
	}
}

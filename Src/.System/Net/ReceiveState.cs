using System;

namespace System.Net
{
	// Token: 0x0200019A RID: 410
	internal class ReceiveState
	{
		// Token: 0x06000FEB RID: 4075 RVA: 0x000536AE File Offset: 0x000518AE
		internal ReceiveState(CommandStream connection)
		{
			this.Connection = connection;
			this.Resp = new ResponseDescription();
			this.Buffer = new byte[1024];
			this.ValidThrough = 0;
		}

		// Token: 0x040012FA RID: 4858
		private const int bufferSize = 1024;

		// Token: 0x040012FB RID: 4859
		internal ResponseDescription Resp;

		// Token: 0x040012FC RID: 4860
		internal int ValidThrough;

		// Token: 0x040012FD RID: 4861
		internal byte[] Buffer;

		// Token: 0x040012FE RID: 4862
		internal CommandStream Connection;
	}
}

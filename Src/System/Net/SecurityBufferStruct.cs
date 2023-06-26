using System;

namespace System.Net
{
	// Token: 0x02000131 RID: 305
	internal struct SecurityBufferStruct
	{
		// Token: 0x0400102F RID: 4143
		public int count;

		// Token: 0x04001030 RID: 4144
		public BufferType type;

		// Token: 0x04001031 RID: 4145
		public IntPtr token;

		// Token: 0x04001032 RID: 4146
		public static readonly int Size = sizeof(SecurityBufferStruct);
	}
}

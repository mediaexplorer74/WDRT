using System;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000132 RID: 306
	internal class SecurityBuffer
	{
		// Token: 0x06000B3B RID: 2875 RVA: 0x0003DC2C File Offset: 0x0003BE2C
		public SecurityBuffer(byte[] data, int offset, int size, BufferType tokentype)
		{
			this.offset = ((data == null || offset < 0) ? 0 : Math.Min(offset, data.Length));
			this.size = ((data == null || size < 0) ? 0 : Math.Min(size, data.Length - this.offset));
			this.type = tokentype;
			this.token = ((size == 0) ? null : data);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0003DC8D File Offset: 0x0003BE8D
		public SecurityBuffer(byte[] data, BufferType tokentype)
		{
			this.size = ((data == null) ? 0 : data.Length);
			this.type = tokentype;
			this.token = ((this.size == 0) ? null : data);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0003DCBD File Offset: 0x0003BEBD
		public SecurityBuffer(int size, BufferType tokentype)
		{
			this.size = size;
			this.type = tokentype;
			this.token = ((size == 0) ? null : new byte[size]);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0003DCE5 File Offset: 0x0003BEE5
		public SecurityBuffer(ChannelBinding binding)
		{
			this.size = ((binding == null) ? 0 : binding.Size);
			this.type = BufferType.ChannelBindings;
			this.unmanagedToken = binding;
		}

		// Token: 0x04001033 RID: 4147
		public int size;

		// Token: 0x04001034 RID: 4148
		public BufferType type;

		// Token: 0x04001035 RID: 4149
		public byte[] token;

		// Token: 0x04001036 RID: 4150
		public SafeHandle unmanagedToken;

		// Token: 0x04001037 RID: 4151
		public int offset;
	}
}

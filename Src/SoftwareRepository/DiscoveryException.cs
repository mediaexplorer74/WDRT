using System;
using System.Runtime.Serialization;

namespace SoftwareRepository
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public class DiscoveryException : Exception
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000222C File Offset: 0x0000042C
		public DiscoveryException()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002236 File Offset: 0x00000436
		public DiscoveryException(string message)
			: base(message)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002241 File Offset: 0x00000441
		public DiscoveryException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000224D File Offset: 0x0000044D
		protected DiscoveryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

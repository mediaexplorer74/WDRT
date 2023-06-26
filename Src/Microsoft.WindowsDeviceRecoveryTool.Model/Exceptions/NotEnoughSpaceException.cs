using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public class NotEnoughSpaceException : Exception
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000642F File Offset: 0x0000462F
		public NotEnoughSpaceException()
			: base("There is not enough space on disk")
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006350 File Offset: 0x00004550
		public NotEnoughSpaceException(string message)
			: base(message)
		{
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000635B File Offset: 0x0000455B
		public NotEnoughSpaceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006367 File Offset: 0x00004567
		protected NotEnoughSpaceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000643E File Offset: 0x0000463E
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00006446 File Offset: 0x00004646
		public long Available { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000644F File Offset: 0x0000464F
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00006457 File Offset: 0x00004657
		public long Needed { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00006460 File Offset: 0x00004660
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00006468 File Offset: 0x00004668
		public string Disk { get; set; }
	}
}

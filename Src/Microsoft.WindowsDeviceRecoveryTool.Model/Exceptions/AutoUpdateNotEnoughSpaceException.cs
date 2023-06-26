using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public class AutoUpdateNotEnoughSpaceException : Exception
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00006346 File Offset: 0x00004546
		public AutoUpdateNotEnoughSpaceException()
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006350 File Offset: 0x00004550
		public AutoUpdateNotEnoughSpaceException(string message)
			: base(message)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000635B File Offset: 0x0000455B
		public AutoUpdateNotEnoughSpaceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006367 File Offset: 0x00004567
		protected AutoUpdateNotEnoughSpaceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006373 File Offset: 0x00004573
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000637B File Offset: 0x0000457B
		public long Available { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00006384 File Offset: 0x00004584
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000638C File Offset: 0x0000458C
		public long Needed { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00006395 File Offset: 0x00004595
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000639D File Offset: 0x0000459D
		public string Disk { get; set; }
	}
}

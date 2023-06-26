using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x0200009A RID: 154
	public class ApplicationInvalidateSizeMessage
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x0001B304 File Offset: 0x00019504
		public ApplicationInvalidateSizeMessage(ApplicationInvalidateSizeMessage.DataType type)
		{
			this.Type = type;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001B316 File Offset: 0x00019516
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0001B31E File Offset: 0x0001951E
		public ApplicationInvalidateSizeMessage.DataType Type { get; private set; }

		// Token: 0x02000142 RID: 322
		public enum DataType
		{
			// Token: 0x0400040B RID: 1035
			Logs,
			// Token: 0x0400040C RID: 1036
			Reports,
			// Token: 0x0400040D RID: 1037
			Packages
		}
	}
}

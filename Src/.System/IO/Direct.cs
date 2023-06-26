using System;
using System.Security.Permissions;

namespace System.IO
{
	// Token: 0x02000400 RID: 1024
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal static class Direct
	{
		// Token: 0x040020BC RID: 8380
		public const int FILE_ACTION_ADDED = 1;

		// Token: 0x040020BD RID: 8381
		public const int FILE_ACTION_REMOVED = 2;

		// Token: 0x040020BE RID: 8382
		public const int FILE_ACTION_MODIFIED = 3;

		// Token: 0x040020BF RID: 8383
		public const int FILE_ACTION_RENAMED_OLD_NAME = 4;

		// Token: 0x040020C0 RID: 8384
		public const int FILE_ACTION_RENAMED_NEW_NAME = 5;

		// Token: 0x040020C1 RID: 8385
		public const int FILE_NOTIFY_CHANGE_FILE_NAME = 1;

		// Token: 0x040020C2 RID: 8386
		public const int FILE_NOTIFY_CHANGE_DIR_NAME = 2;

		// Token: 0x040020C3 RID: 8387
		public const int FILE_NOTIFY_CHANGE_NAME = 3;

		// Token: 0x040020C4 RID: 8388
		public const int FILE_NOTIFY_CHANGE_ATTRIBUTES = 4;

		// Token: 0x040020C5 RID: 8389
		public const int FILE_NOTIFY_CHANGE_SIZE = 8;

		// Token: 0x040020C6 RID: 8390
		public const int FILE_NOTIFY_CHANGE_LAST_WRITE = 16;

		// Token: 0x040020C7 RID: 8391
		public const int FILE_NOTIFY_CHANGE_LAST_ACCESS = 32;

		// Token: 0x040020C8 RID: 8392
		public const int FILE_NOTIFY_CHANGE_CREATION = 64;

		// Token: 0x040020C9 RID: 8393
		public const int FILE_NOTIFY_CHANGE_SECURITY = 256;
	}
}

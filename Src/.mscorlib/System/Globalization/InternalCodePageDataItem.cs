using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020003B8 RID: 952
	internal struct InternalCodePageDataItem
	{
		// Token: 0x04001425 RID: 5157
		internal ushort codePage;

		// Token: 0x04001426 RID: 5158
		internal ushort uiFamilyCodePage;

		// Token: 0x04001427 RID: 5159
		internal uint flags;

		// Token: 0x04001428 RID: 5160
		[SecurityCritical]
		internal unsafe sbyte* Names;
	}
}

using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020003B7 RID: 951
	internal struct InternalEncodingDataItem
	{
		// Token: 0x04001423 RID: 5155
		[SecurityCritical]
		internal unsafe sbyte* webName;

		// Token: 0x04001424 RID: 5156
		internal ushort codePage;
	}
}

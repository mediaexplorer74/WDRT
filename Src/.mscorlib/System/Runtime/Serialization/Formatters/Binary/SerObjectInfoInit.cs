using System;
using System.Collections;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007A1 RID: 1953
	internal sealed class SerObjectInfoInit
	{
		// Token: 0x0400270D RID: 9997
		internal Hashtable seenBeforeTable = new Hashtable();

		// Token: 0x0400270E RID: 9998
		internal int objectInfoIdCount = 1;

		// Token: 0x0400270F RID: 9999
		internal SerStack oiPool = new SerStack("SerObjectInfo Pool");
	}
}

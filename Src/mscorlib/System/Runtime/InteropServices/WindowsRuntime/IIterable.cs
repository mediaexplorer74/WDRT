using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A13 RID: 2579
	[Guid("faa585ea-6214-4217-afda-7f46de5869b3")]
	[ComImport]
	internal interface IIterable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060065D8 RID: 26072
		IIterator<T> First();
	}
}

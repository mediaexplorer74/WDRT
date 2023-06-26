using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CF RID: 2511
	internal sealed class EnumerableToIterableAdapter
	{
		// Token: 0x06006416 RID: 25622 RVA: 0x001567D3 File Offset: 0x001549D3
		private EnumerableToIterableAdapter()
		{
		}

		// Token: 0x06006417 RID: 25623 RVA: 0x001567DC File Offset: 0x001549DC
		[SecurityCritical]
		internal IIterator<T> First_Stub<T>()
		{
			IEnumerable<T> enumerable = JitHelpers.UnsafeCast<IEnumerable<T>>(this);
			return new EnumeratorToIteratorAdapter<T>(enumerable.GetEnumerator());
		}
	}
}

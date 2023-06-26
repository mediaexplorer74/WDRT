using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D4 RID: 2516
	internal sealed class VectorViewToReadOnlyCollectionAdapter
	{
		// Token: 0x06006432 RID: 25650 RVA: 0x00156CCB File Offset: 0x00154ECB
		private VectorViewToReadOnlyCollectionAdapter()
		{
		}

		// Token: 0x06006433 RID: 25651 RVA: 0x00156CD4 File Offset: 0x00154ED4
		[SecurityCritical]
		internal int Count<T>()
		{
			IVectorView<T> vectorView = JitHelpers.UnsafeCast<IVectorView<T>>(this);
			uint size = vectorView.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)size;
		}
	}
}

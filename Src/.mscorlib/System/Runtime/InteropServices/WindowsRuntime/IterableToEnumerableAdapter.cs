using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009EC RID: 2540
	internal sealed class IterableToEnumerableAdapter
	{
		// Token: 0x060064CA RID: 25802 RVA: 0x0015882B File Offset: 0x00156A2B
		private IterableToEnumerableAdapter()
		{
		}

		// Token: 0x060064CB RID: 25803 RVA: 0x00158834 File Offset: 0x00156A34
		[SecurityCritical]
		internal IEnumerator<T> GetEnumerator_Stub<T>()
		{
			IIterable<T> iterable = JitHelpers.UnsafeCast<IIterable<T>>(this);
			return new IteratorToEnumeratorAdapter<T>(iterable.First());
		}

		// Token: 0x060064CC RID: 25804 RVA: 0x00158854 File Offset: 0x00156A54
		[SecurityCritical]
		internal IEnumerator<T> GetEnumerator_Variance_Stub<T>() where T : class
		{
			bool flag;
			Delegate targetForAmbiguousVariantCall = StubHelpers.GetTargetForAmbiguousVariantCall(this, typeof(IEnumerable<T>).TypeHandle.Value, out flag);
			if (targetForAmbiguousVariantCall != null)
			{
				return JitHelpers.UnsafeCast<GetEnumerator_Delegate<T>>(targetForAmbiguousVariantCall)();
			}
			if (flag)
			{
				return JitHelpers.UnsafeCast<IEnumerator<T>>(this.GetEnumerator_Stub<string>());
			}
			return this.GetEnumerator_Stub<T>();
		}
	}
}

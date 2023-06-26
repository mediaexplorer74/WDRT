using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E8 RID: 2536
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class IVectorViewToIReadOnlyListAdapter
	{
		// Token: 0x060064B8 RID: 25784 RVA: 0x00158517 File Offset: 0x00156717
		private IVectorViewToIReadOnlyListAdapter()
		{
		}

		// Token: 0x060064B9 RID: 25785 RVA: 0x00158520 File Offset: 0x00156720
		[SecurityCritical]
		internal T Indexer_Get<T>(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVectorView<T> vectorView = JitHelpers.UnsafeCast<IVectorView<T>>(this);
			T at;
			try
			{
				at = vectorView.GetAt((uint)index);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
			return at;
		}

		// Token: 0x060064BA RID: 25786 RVA: 0x0015857C File Offset: 0x0015677C
		[SecurityCritical]
		internal T Indexer_Get_Variance<T>(int index) where T : class
		{
			bool flag;
			Delegate targetForAmbiguousVariantCall = StubHelpers.GetTargetForAmbiguousVariantCall(this, typeof(IReadOnlyList<T>).TypeHandle.Value, out flag);
			if (targetForAmbiguousVariantCall != null)
			{
				return JitHelpers.UnsafeCast<Indexer_Get_Delegate<T>>(targetForAmbiguousVariantCall)(index);
			}
			if (flag)
			{
				return JitHelpers.UnsafeCast<T>(this.Indexer_Get<string>(index));
			}
			return this.Indexer_Get<T>(index);
		}
	}
}

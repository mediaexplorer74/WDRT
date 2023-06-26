using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D0 RID: 2512
	internal sealed class EnumerableToBindableIterableAdapter
	{
		// Token: 0x06006418 RID: 25624 RVA: 0x001567FB File Offset: 0x001549FB
		private EnumerableToBindableIterableAdapter()
		{
		}

		// Token: 0x06006419 RID: 25625 RVA: 0x00156804 File Offset: 0x00154A04
		[SecurityCritical]
		internal IBindableIterator First_Stub()
		{
			IEnumerable enumerable = JitHelpers.UnsafeCast<IEnumerable>(this);
			return new EnumeratorToIteratorAdapter<object>(new EnumerableToBindableIterableAdapter.NonGenericToGenericEnumerator(enumerable.GetEnumerator()));
		}

		// Token: 0x02000C9D RID: 3229
		internal sealed class NonGenericToGenericEnumerator : IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06007147 RID: 28999 RVA: 0x00186FA9 File Offset: 0x001851A9
			public NonGenericToGenericEnumerator(IEnumerator enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x17001369 RID: 4969
			// (get) Token: 0x06007148 RID: 29000 RVA: 0x00186FB8 File Offset: 0x001851B8
			public object Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x06007149 RID: 29001 RVA: 0x00186FC5 File Offset: 0x001851C5
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x0600714A RID: 29002 RVA: 0x00186FD2 File Offset: 0x001851D2
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x0600714B RID: 29003 RVA: 0x00186FDF File Offset: 0x001851DF
			public void Dispose()
			{
			}

			// Token: 0x0400387F RID: 14463
			private IEnumerator enumerator;
		}
	}
}

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009ED RID: 2541
	internal sealed class BindableIterableToEnumerableAdapter
	{
		// Token: 0x060064CD RID: 25805 RVA: 0x001588A5 File Offset: 0x00156AA5
		private BindableIterableToEnumerableAdapter()
		{
		}

		// Token: 0x060064CE RID: 25806 RVA: 0x001588B0 File Offset: 0x00156AB0
		[SecurityCritical]
		internal IEnumerator GetEnumerator_Stub()
		{
			IBindableIterable bindableIterable = JitHelpers.UnsafeCast<IBindableIterable>(this);
			return new IteratorToEnumeratorAdapter<object>(new BindableIterableToEnumerableAdapter.NonGenericToGenericIterator(bindableIterable.First()));
		}

		// Token: 0x02000C9E RID: 3230
		private sealed class NonGenericToGenericIterator : IIterator<object>
		{
			// Token: 0x0600714C RID: 29004 RVA: 0x00186FE1 File Offset: 0x001851E1
			public NonGenericToGenericIterator(IBindableIterator iterator)
			{
				this.iterator = iterator;
			}

			// Token: 0x1700136A RID: 4970
			// (get) Token: 0x0600714D RID: 29005 RVA: 0x00186FF0 File Offset: 0x001851F0
			public object Current
			{
				get
				{
					return this.iterator.Current;
				}
			}

			// Token: 0x1700136B RID: 4971
			// (get) Token: 0x0600714E RID: 29006 RVA: 0x00186FFD File Offset: 0x001851FD
			public bool HasCurrent
			{
				get
				{
					return this.iterator.HasCurrent;
				}
			}

			// Token: 0x0600714F RID: 29007 RVA: 0x0018700A File Offset: 0x0018520A
			public bool MoveNext()
			{
				return this.iterator.MoveNext();
			}

			// Token: 0x06007150 RID: 29008 RVA: 0x00187017 File Offset: 0x00185217
			public int GetMany(object[] items)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04003880 RID: 14464
			private IBindableIterator iterator;
		}
	}
}

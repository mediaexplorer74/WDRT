using System;
using System.Collections;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DD RID: 2525
	internal sealed class ListToBindableVectorViewAdapter : IBindableVectorView, IBindableIterable
	{
		// Token: 0x06006481 RID: 25729 RVA: 0x00157CC3 File Offset: 0x00155EC3
		internal ListToBindableVectorViewAdapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			this.list = list;
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x00157CE0 File Offset: 0x00155EE0
		private static void EnsureIndexInt32(uint index, int listCapacity)
		{
			if (2147483647U <= index || index >= (uint)listCapacity)
			{
				Exception ex = new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x00157D1C File Offset: 0x00155F1C
		public IBindableIterator First()
		{
			IEnumerator enumerator = this.list.GetEnumerator();
			return new EnumeratorToIteratorAdapter<object>(new EnumerableToBindableIterableAdapter.NonGenericToGenericEnumerator(enumerator));
		}

		// Token: 0x06006484 RID: 25732 RVA: 0x00157D40 File Offset: 0x00155F40
		public object GetAt(uint index)
		{
			ListToBindableVectorViewAdapter.EnsureIndexInt32(index, this.list.Count);
			object obj;
			try
			{
				obj = this.list[(int)index];
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, ex, "ArgumentOutOfRange_IndexOutOfRange");
			}
			return obj;
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06006485 RID: 25733 RVA: 0x00157D90 File Offset: 0x00155F90
		public uint Size
		{
			get
			{
				return (uint)this.list.Count;
			}
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x00157DA0 File Offset: 0x00155FA0
		public bool IndexOf(object value, out uint index)
		{
			int num = this.list.IndexOf(value);
			if (-1 == num)
			{
				index = 0U;
				return false;
			}
			index = (uint)num;
			return true;
		}

		// Token: 0x04002CF6 RID: 11510
		private readonly IList list;
	}
}

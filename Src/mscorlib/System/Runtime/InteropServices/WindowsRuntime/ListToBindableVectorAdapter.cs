using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DC RID: 2524
	internal sealed class ListToBindableVectorAdapter
	{
		// Token: 0x06006475 RID: 25717 RVA: 0x00157A65 File Offset: 0x00155C65
		private ListToBindableVectorAdapter()
		{
		}

		// Token: 0x06006476 RID: 25718 RVA: 0x00157A70 File Offset: 0x00155C70
		[SecurityCritical]
		internal object GetAt(uint index)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
			object obj;
			try
			{
				obj = list[(int)index];
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, ex, "ArgumentOutOfRange_IndexOutOfRange");
			}
			return obj;
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x00157AC0 File Offset: 0x00155CC0
		[SecurityCritical]
		internal uint Size()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			return (uint)list.Count;
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x00157ADC File Offset: 0x00155CDC
		[SecurityCritical]
		internal IBindableVectorView GetView()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			return new ListToBindableVectorViewAdapter(list);
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x00157AF8 File Offset: 0x00155CF8
		[SecurityCritical]
		internal bool IndexOf(object value, out uint index)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			int num = list.IndexOf(value);
			if (-1 == num)
			{
				index = 0U;
				return false;
			}
			index = (uint)num;
			return true;
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x00157B24 File Offset: 0x00155D24
		[SecurityCritical]
		internal void SetAt(uint index, object value)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
			try
			{
				list[(int)index] = value;
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, ex, "ArgumentOutOfRange_IndexOutOfRange");
			}
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x00157B70 File Offset: 0x00155D70
		[SecurityCritical]
		internal void InsertAt(uint index, object value)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count + 1);
			try
			{
				list.Insert((int)index, value);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				ex.SetErrorCode(-2147483637);
				throw;
			}
		}

		// Token: 0x0600647C RID: 25724 RVA: 0x00157BBC File Offset: 0x00155DBC
		[SecurityCritical]
		internal void RemoveAt(uint index)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
			try
			{
				list.RemoveAt((int)index);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				ex.SetErrorCode(-2147483637);
				throw;
			}
		}

		// Token: 0x0600647D RID: 25725 RVA: 0x00157C04 File Offset: 0x00155E04
		[SecurityCritical]
		internal void Append(object value)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			list.Add(value);
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x00157C20 File Offset: 0x00155E20
		[SecurityCritical]
		internal void RemoveAtEnd()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			if (list.Count == 0)
			{
				Exception ex = new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRemoveLastFromEmptyCollection"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			uint count = (uint)list.Count;
			this.RemoveAt(count - 1U);
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x00157C6C File Offset: 0x00155E6C
		[SecurityCritical]
		internal void Clear()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			list.Clear();
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x00157C88 File Offset: 0x00155E88
		private static void EnsureIndexInt32(uint index, int listCapacity)
		{
			if (2147483647U <= index || index >= (uint)listCapacity)
			{
				Exception ex = new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D8 RID: 2520
	internal sealed class ListToVectorAdapter
	{
		// Token: 0x06006449 RID: 25673 RVA: 0x00157229 File Offset: 0x00155429
		private ListToVectorAdapter()
		{
		}

		// Token: 0x0600644A RID: 25674 RVA: 0x00157234 File Offset: 0x00155434
		[SecurityCritical]
		internal T GetAt<T>(uint index)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count);
			T t;
			try
			{
				t = list[(int)index];
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, ex, "ArgumentOutOfRange_IndexOutOfRange");
			}
			return t;
		}

		// Token: 0x0600644B RID: 25675 RVA: 0x00157284 File Offset: 0x00155484
		[SecurityCritical]
		internal uint Size<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			return (uint)list.Count;
		}

		// Token: 0x0600644C RID: 25676 RVA: 0x001572A0 File Offset: 0x001554A0
		[SecurityCritical]
		internal IReadOnlyList<T> GetView<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			IReadOnlyList<T> readOnlyList = list as IReadOnlyList<T>;
			if (readOnlyList == null)
			{
				readOnlyList = new ReadOnlyCollection<T>(list);
			}
			return readOnlyList;
		}

		// Token: 0x0600644D RID: 25677 RVA: 0x001572C8 File Offset: 0x001554C8
		[SecurityCritical]
		internal bool IndexOf<T>(T value, out uint index)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			int num = list.IndexOf(value);
			if (-1 == num)
			{
				index = 0U;
				return false;
			}
			index = (uint)num;
			return true;
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x001572F4 File Offset: 0x001554F4
		[SecurityCritical]
		internal void SetAt<T>(uint index, T value)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count);
			try
			{
				list[(int)index] = value;
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, ex, "ArgumentOutOfRange_IndexOutOfRange");
			}
		}

		// Token: 0x0600644F RID: 25679 RVA: 0x00157340 File Offset: 0x00155540
		[SecurityCritical]
		internal void InsertAt<T>(uint index, T value)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count + 1);
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

		// Token: 0x06006450 RID: 25680 RVA: 0x0015738C File Offset: 0x0015558C
		[SecurityCritical]
		internal void RemoveAt<T>(uint index)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count);
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

		// Token: 0x06006451 RID: 25681 RVA: 0x001573D4 File Offset: 0x001555D4
		[SecurityCritical]
		internal void Append<T>(T value)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			list.Add(value);
		}

		// Token: 0x06006452 RID: 25682 RVA: 0x001573F0 File Offset: 0x001555F0
		[SecurityCritical]
		internal void RemoveAtEnd<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			if (list.Count == 0)
			{
				Exception ex = new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRemoveLastFromEmptyCollection"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			uint count = (uint)list.Count;
			this.RemoveAt<T>(count - 1U);
		}

		// Token: 0x06006453 RID: 25683 RVA: 0x0015743C File Offset: 0x0015563C
		[SecurityCritical]
		internal void Clear<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			list.Clear();
		}

		// Token: 0x06006454 RID: 25684 RVA: 0x00157458 File Offset: 0x00155658
		[SecurityCritical]
		internal uint GetMany<T>(uint startIndex, T[] items)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			return ListToVectorAdapter.GetManyHelper<T>(list, startIndex, items);
		}

		// Token: 0x06006455 RID: 25685 RVA: 0x00157474 File Offset: 0x00155674
		[SecurityCritical]
		internal void ReplaceAll<T>(T[] items)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			list.Clear();
			if (items != null)
			{
				foreach (T t in items)
				{
					list.Add(t);
				}
			}
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x001574B0 File Offset: 0x001556B0
		private static void EnsureIndexInt32(uint index, int listCapacity)
		{
			if (2147483647U <= index || index >= (uint)listCapacity)
			{
				Exception ex = new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}

		// Token: 0x06006457 RID: 25687 RVA: 0x001574EC File Offset: 0x001556EC
		private static uint GetManyHelper<T>(IList<T> sourceList, uint startIndex, T[] items)
		{
			if ((ulong)startIndex == (ulong)((long)sourceList.Count))
			{
				return 0U;
			}
			ListToVectorAdapter.EnsureIndexInt32(startIndex, sourceList.Count);
			if (items == null)
			{
				return 0U;
			}
			uint num = Math.Min((uint)items.Length, (uint)(sourceList.Count - (int)startIndex));
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				items[(int)num2] = sourceList[(int)(num2 + startIndex)];
			}
			if (typeof(T) == typeof(string))
			{
				string[] array = items as string[];
				uint num3 = num;
				while ((ulong)num3 < (ulong)((long)items.Length))
				{
					array[(int)num3] = string.Empty;
					num3 += 1U;
				}
			}
			return num;
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D2 RID: 2514
	internal sealed class VectorToListAdapter
	{
		// Token: 0x06006420 RID: 25632 RVA: 0x00156959 File Offset: 0x00154B59
		private VectorToListAdapter()
		{
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x00156964 File Offset: 0x00154B64
		[SecurityCritical]
		internal T Indexer_Get<T>(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			return VectorToListAdapter.GetAt<T>(vector, (uint)index);
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x00156990 File Offset: 0x00154B90
		[SecurityCritical]
		internal void Indexer_Set<T>(int index, T value)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			VectorToListAdapter.SetAt<T>(vector, (uint)index, value);
		}

		// Token: 0x06006423 RID: 25635 RVA: 0x001569BC File Offset: 0x00154BBC
		[SecurityCritical]
		internal int IndexOf<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint num;
			if (!vector.IndexOf(item, out num))
			{
				return -1;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)num;
		}

		// Token: 0x06006424 RID: 25636 RVA: 0x001569F8 File Offset: 0x00154BF8
		[SecurityCritical]
		internal void Insert<T>(int index, T item)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			VectorToListAdapter.InsertAtHelper<T>(vector, (uint)index, item);
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x00156A24 File Offset: 0x00154C24
		[SecurityCritical]
		internal void RemoveAt<T>(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			VectorToListAdapter.RemoveAtHelper<T>(vector, (uint)index);
		}

		// Token: 0x06006426 RID: 25638 RVA: 0x00156A50 File Offset: 0x00154C50
		internal static T GetAt<T>(IVector<T> _this, uint index)
		{
			T at;
			try
			{
				at = _this.GetAt(index);
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

		// Token: 0x06006427 RID: 25639 RVA: 0x00156A94 File Offset: 0x00154C94
		private static void SetAt<T>(IVector<T> _this, uint index, T value)
		{
			try
			{
				_this.SetAt(index, value);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
		}

		// Token: 0x06006428 RID: 25640 RVA: 0x00156AD8 File Offset: 0x00154CD8
		private static void InsertAtHelper<T>(IVector<T> _this, uint index, T item)
		{
			try
			{
				_this.InsertAt(index, item);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
		}

		// Token: 0x06006429 RID: 25641 RVA: 0x00156B1C File Offset: 0x00154D1C
		internal static void RemoveAtHelper<T>(IVector<T> _this, uint index)
		{
			try
			{
				_this.RemoveAt(index);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
		}
	}
}

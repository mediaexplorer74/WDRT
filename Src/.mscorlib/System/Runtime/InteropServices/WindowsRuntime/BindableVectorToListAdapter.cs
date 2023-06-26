using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DA RID: 2522
	internal sealed class BindableVectorToListAdapter
	{
		// Token: 0x06006460 RID: 25696 RVA: 0x001576A6 File Offset: 0x001558A6
		private BindableVectorToListAdapter()
		{
		}

		// Token: 0x06006461 RID: 25697 RVA: 0x001576B0 File Offset: 0x001558B0
		[SecurityCritical]
		internal object Indexer_Get(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			return BindableVectorToListAdapter.GetAt(bindableVector, (uint)index);
		}

		// Token: 0x06006462 RID: 25698 RVA: 0x001576DC File Offset: 0x001558DC
		[SecurityCritical]
		internal void Indexer_Set(int index, object value)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			BindableVectorToListAdapter.SetAt(bindableVector, (uint)index, value);
		}

		// Token: 0x06006463 RID: 25699 RVA: 0x00157708 File Offset: 0x00155908
		[SecurityCritical]
		internal int Add(object value)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			bindableVector.Append(value);
			uint size = bindableVector.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)(size - 1U);
		}

		// Token: 0x06006464 RID: 25700 RVA: 0x00157748 File Offset: 0x00155948
		[SecurityCritical]
		internal bool Contains(object item)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num;
			return bindableVector.IndexOf(item, out num);
		}

		// Token: 0x06006465 RID: 25701 RVA: 0x00157768 File Offset: 0x00155968
		[SecurityCritical]
		internal void Clear()
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			bindableVector.Clear();
		}

		// Token: 0x06006466 RID: 25702 RVA: 0x00157782 File Offset: 0x00155982
		[SecurityCritical]
		internal bool IsFixedSize()
		{
			return false;
		}

		// Token: 0x06006467 RID: 25703 RVA: 0x00157785 File Offset: 0x00155985
		[SecurityCritical]
		internal bool IsReadOnly()
		{
			return false;
		}

		// Token: 0x06006468 RID: 25704 RVA: 0x00157788 File Offset: 0x00155988
		[SecurityCritical]
		internal int IndexOf(object item)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num;
			if (!bindableVector.IndexOf(item, out num))
			{
				return -1;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)num;
		}

		// Token: 0x06006469 RID: 25705 RVA: 0x001577C4 File Offset: 0x001559C4
		[SecurityCritical]
		internal void Insert(int index, object item)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			BindableVectorToListAdapter.InsertAtHelper(bindableVector, (uint)index, item);
		}

		// Token: 0x0600646A RID: 25706 RVA: 0x001577F0 File Offset: 0x001559F0
		[SecurityCritical]
		internal void Remove(object item)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num;
			bool flag = bindableVector.IndexOf(item, out num);
			if (flag)
			{
				if (2147483647U < num)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
				}
				BindableVectorToListAdapter.RemoveAtHelper(bindableVector, num);
			}
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x00157830 File Offset: 0x00155A30
		[SecurityCritical]
		internal void RemoveAt(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			BindableVectorToListAdapter.RemoveAtHelper(bindableVector, (uint)index);
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x0015785C File Offset: 0x00155A5C
		private static object GetAt(IBindableVector _this, uint index)
		{
			object at;
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

		// Token: 0x0600646D RID: 25709 RVA: 0x001578A0 File Offset: 0x00155AA0
		private static void SetAt(IBindableVector _this, uint index, object value)
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

		// Token: 0x0600646E RID: 25710 RVA: 0x001578E4 File Offset: 0x00155AE4
		private static void InsertAtHelper(IBindableVector _this, uint index, object item)
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

		// Token: 0x0600646F RID: 25711 RVA: 0x00157928 File Offset: 0x00155B28
		private static void RemoveAtHelper(IBindableVector _this, uint index)
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

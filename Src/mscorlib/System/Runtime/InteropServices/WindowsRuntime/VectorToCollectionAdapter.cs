using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D3 RID: 2515
	internal sealed class VectorToCollectionAdapter
	{
		// Token: 0x0600642A RID: 25642 RVA: 0x00156B60 File Offset: 0x00154D60
		private VectorToCollectionAdapter()
		{
		}

		// Token: 0x0600642B RID: 25643 RVA: 0x00156B68 File Offset: 0x00154D68
		[SecurityCritical]
		internal int Count<T>()
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint size = vector.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)size;
		}

		// Token: 0x0600642C RID: 25644 RVA: 0x00156B9C File Offset: 0x00154D9C
		[SecurityCritical]
		internal bool IsReadOnly<T>()
		{
			return false;
		}

		// Token: 0x0600642D RID: 25645 RVA: 0x00156BA0 File Offset: 0x00154DA0
		[SecurityCritical]
		internal void Add<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			vector.Append(item);
		}

		// Token: 0x0600642E RID: 25646 RVA: 0x00156BBC File Offset: 0x00154DBC
		[SecurityCritical]
		internal void Clear<T>()
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			vector.Clear();
		}

		// Token: 0x0600642F RID: 25647 RVA: 0x00156BD8 File Offset: 0x00154DD8
		[SecurityCritical]
		internal bool Contains<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint num;
			return vector.IndexOf(item, out num);
		}

		// Token: 0x06006430 RID: 25648 RVA: 0x00156BF8 File Offset: 0x00154DF8
		[SecurityCritical]
		internal void CopyTo<T>(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length <= arrayIndex && this.Count<T>() > 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
			}
			if (array.Length - arrayIndex < this.Count<T>())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			int num = this.Count<T>();
			for (int i = 0; i < num; i++)
			{
				array[i + arrayIndex] = VectorToListAdapter.GetAt<T>(vector, (uint)i);
			}
		}

		// Token: 0x06006431 RID: 25649 RVA: 0x00156C88 File Offset: 0x00154E88
		[SecurityCritical]
		internal bool Remove<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint num;
			if (!vector.IndexOf(item, out num))
			{
				return false;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			VectorToListAdapter.RemoveAtHelper<T>(vector, num);
			return true;
		}
	}
}

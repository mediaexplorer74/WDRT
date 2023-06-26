using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DB RID: 2523
	internal sealed class BindableVectorToCollectionAdapter
	{
		// Token: 0x06006470 RID: 25712 RVA: 0x0015796C File Offset: 0x00155B6C
		private BindableVectorToCollectionAdapter()
		{
		}

		// Token: 0x06006471 RID: 25713 RVA: 0x00157974 File Offset: 0x00155B74
		[SecurityCritical]
		internal int Count()
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint size = bindableVector.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)size;
		}

		// Token: 0x06006472 RID: 25714 RVA: 0x001579A8 File Offset: 0x00155BA8
		[SecurityCritical]
		internal bool IsSynchronized()
		{
			return false;
		}

		// Token: 0x06006473 RID: 25715 RVA: 0x001579AB File Offset: 0x00155BAB
		[SecurityCritical]
		internal object SyncRoot()
		{
			return this;
		}

		// Token: 0x06006474 RID: 25716 RVA: 0x001579B0 File Offset: 0x00155BB0
		[SecurityCritical]
		internal void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			int lowerBound = array.GetLowerBound(0);
			int num = this.Count();
			int length = array.GetLength(0);
			if (arrayIndex < lowerBound)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (num > length - (arrayIndex - lowerBound))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			if (arrayIndex - lowerBound > length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
			}
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				array.SetValue(bindableVector.GetAt(num2), (long)((ulong)num2 + (ulong)((long)arrayIndex)));
				num2 += 1U;
			}
		}
	}
}

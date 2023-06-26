using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009EA RID: 2538
	[DebuggerDisplay("Size = {Size}")]
	internal sealed class IReadOnlyListToIVectorViewAdapter
	{
		// Token: 0x060064C0 RID: 25792 RVA: 0x0015868D File Offset: 0x0015688D
		private IReadOnlyListToIVectorViewAdapter()
		{
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x00158698 File Offset: 0x00156898
		[SecurityCritical]
		internal T GetAt<T>(uint index)
		{
			IReadOnlyList<T> readOnlyList = JitHelpers.UnsafeCast<IReadOnlyList<T>>(this);
			IReadOnlyListToIVectorViewAdapter.EnsureIndexInt32(index, readOnlyList.Count);
			T t;
			try
			{
				t = readOnlyList[(int)index];
			}
			catch (ArgumentOutOfRangeException ex)
			{
				ex.SetErrorCode(-2147483637);
				throw;
			}
			return t;
		}

		// Token: 0x060064C2 RID: 25794 RVA: 0x001586E4 File Offset: 0x001568E4
		[SecurityCritical]
		internal uint Size<T>()
		{
			IReadOnlyList<T> readOnlyList = JitHelpers.UnsafeCast<IReadOnlyList<T>>(this);
			return (uint)readOnlyList.Count;
		}

		// Token: 0x060064C3 RID: 25795 RVA: 0x00158700 File Offset: 0x00156900
		[SecurityCritical]
		internal bool IndexOf<T>(T value, out uint index)
		{
			IReadOnlyList<T> readOnlyList = JitHelpers.UnsafeCast<IReadOnlyList<T>>(this);
			int num = -1;
			int count = readOnlyList.Count;
			for (int i = 0; i < count; i++)
			{
				if (EqualityComparer<T>.Default.Equals(value, readOnlyList[i]))
				{
					num = i;
					break;
				}
			}
			if (-1 == num)
			{
				index = 0U;
				return false;
			}
			index = (uint)num;
			return true;
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x00158750 File Offset: 0x00156950
		[SecurityCritical]
		internal uint GetMany<T>(uint startIndex, T[] items)
		{
			IReadOnlyList<T> readOnlyList = JitHelpers.UnsafeCast<IReadOnlyList<T>>(this);
			if ((ulong)startIndex == (ulong)((long)readOnlyList.Count))
			{
				return 0U;
			}
			IReadOnlyListToIVectorViewAdapter.EnsureIndexInt32(startIndex, readOnlyList.Count);
			if (items == null)
			{
				return 0U;
			}
			uint num = Math.Min((uint)items.Length, (uint)(readOnlyList.Count - (int)startIndex));
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				items[(int)num2] = readOnlyList[(int)(num2 + startIndex)];
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

		// Token: 0x060064C5 RID: 25797 RVA: 0x001587F0 File Offset: 0x001569F0
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

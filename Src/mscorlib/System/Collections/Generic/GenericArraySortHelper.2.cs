using System;
using System.Runtime.Versioning;

namespace System.Collections.Generic
{
	// Token: 0x020004E1 RID: 1249
	internal class GenericArraySortHelper<TKey, TValue> : IArraySortHelper<TKey, TValue> where TKey : IComparable<TKey>
	{
		// Token: 0x06003B7A RID: 15226 RVA: 0x000E3490 File Offset: 0x000E1690
		public void Sort(TKey[] keys, TValue[] values, int index, int length, IComparer<TKey> comparer)
		{
			try
			{
				if (comparer == null || comparer == Comparer<TKey>.Default)
				{
					if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
					{
						GenericArraySortHelper<TKey, TValue>.IntrospectiveSort(keys, values, index, length);
					}
					else
					{
						GenericArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, index, length + index - 1, 32);
					}
				}
				else if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					ArraySortHelper<TKey, TValue>.IntrospectiveSort(keys, values, index, length, comparer);
				}
				else
				{
					ArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, index, length + index - 1, comparer, 32);
				}
			}
			catch (IndexOutOfRangeException)
			{
				IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(comparer);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
			}
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x000E3530 File Offset: 0x000E1730
		private static void SwapIfGreaterWithItems(TKey[] keys, TValue[] values, int a, int b)
		{
			if (a != b && keys[a] != null && keys[a].CompareTo(keys[b]) > 0)
			{
				TKey tkey = keys[a];
				keys[a] = keys[b];
				keys[b] = tkey;
				if (values != null)
				{
					TValue tvalue = values[a];
					values[a] = values[b];
					values[b] = tvalue;
				}
			}
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x000E35AC File Offset: 0x000E17AC
		private static void Swap(TKey[] keys, TValue[] values, int i, int j)
		{
			if (i != j)
			{
				TKey tkey = keys[i];
				keys[i] = keys[j];
				keys[j] = tkey;
				if (values != null)
				{
					TValue tvalue = values[i];
					values[i] = values[j];
					values[j] = tvalue;
				}
			}
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000E35FC File Offset: 0x000E17FC
		private static void DepthLimitedQuickSort(TKey[] keys, TValue[] values, int left, int right, int depthLimit)
		{
			while (depthLimit != 0)
			{
				int num = left;
				int num2 = right;
				int num3 = num + (num2 - num >> 1);
				GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, num, num3);
				GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, num, num2);
				GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, num3, num2);
				TKey tkey = keys[num3];
				do
				{
					if (tkey == null)
					{
						while (keys[num2] != null)
						{
							num2--;
						}
					}
					else
					{
						while (tkey.CompareTo(keys[num]) > 0)
						{
							num++;
						}
						while (tkey.CompareTo(keys[num2]) < 0)
						{
							num2--;
						}
					}
					if (num > num2)
					{
						break;
					}
					if (num < num2)
					{
						TKey tkey2 = keys[num];
						keys[num] = keys[num2];
						keys[num2] = tkey2;
						if (values != null)
						{
							TValue tvalue = values[num];
							values[num] = values[num2];
							values[num2] = tvalue;
						}
					}
					num++;
					num2--;
				}
				while (num <= num2);
				depthLimit--;
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						GenericArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, left, num2, depthLimit);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						GenericArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, num, right, depthLimit);
					}
					right = num2;
				}
				if (left >= right)
				{
					return;
				}
			}
			GenericArraySortHelper<TKey, TValue>.Heapsort(keys, values, left, right);
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000E3731 File Offset: 0x000E1931
		internal static void IntrospectiveSort(TKey[] keys, TValue[] values, int left, int length)
		{
			if (length < 2)
			{
				return;
			}
			GenericArraySortHelper<TKey, TValue>.IntroSort(keys, values, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(keys.Length));
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000E3750 File Offset: 0x000E1950
		private static void IntroSort(TKey[] keys, TValue[] values, int lo, int hi, int depthLimit)
		{
			while (hi > lo)
			{
				int num = hi - lo + 1;
				if (num <= 16)
				{
					if (num == 1)
					{
						return;
					}
					if (num == 2)
					{
						GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, lo, hi);
						return;
					}
					if (num == 3)
					{
						GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, lo, hi - 1);
						GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, lo, hi);
						GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, hi - 1, hi);
						return;
					}
					GenericArraySortHelper<TKey, TValue>.InsertionSort(keys, values, lo, hi);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						GenericArraySortHelper<TKey, TValue>.Heapsort(keys, values, lo, hi);
						return;
					}
					depthLimit--;
					int num2 = GenericArraySortHelper<TKey, TValue>.PickPivotAndPartition(keys, values, lo, hi);
					GenericArraySortHelper<TKey, TValue>.IntroSort(keys, values, num2 + 1, hi, depthLimit);
					hi = num2 - 1;
				}
			}
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000E37E0 File Offset: 0x000E19E0
		private static int PickPivotAndPartition(TKey[] keys, TValue[] values, int lo, int hi)
		{
			int num = lo + (hi - lo) / 2;
			GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, lo, num);
			GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, lo, hi);
			GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, num, hi);
			TKey tkey = keys[num];
			GenericArraySortHelper<TKey, TValue>.Swap(keys, values, num, hi - 1);
			int i = lo;
			int j = hi - 1;
			while (i < j)
			{
				if (tkey == null)
				{
					while (i < hi - 1 && keys[++i] == null)
					{
					}
					while (j > lo)
					{
						if (keys[--j] == null)
						{
							break;
						}
					}
				}
				else
				{
					while (tkey.CompareTo(keys[++i]) > 0)
					{
					}
					while (tkey.CompareTo(keys[--j]) < 0)
					{
					}
				}
				if (i >= j)
				{
					break;
				}
				GenericArraySortHelper<TKey, TValue>.Swap(keys, values, i, j);
			}
			GenericArraySortHelper<TKey, TValue>.Swap(keys, values, i, hi - 1);
			return i;
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000E38B8 File Offset: 0x000E1AB8
		private static void Heapsort(TKey[] keys, TValue[] values, int lo, int hi)
		{
			int num = hi - lo + 1;
			for (int i = num / 2; i >= 1; i--)
			{
				GenericArraySortHelper<TKey, TValue>.DownHeap(keys, values, i, num, lo);
			}
			for (int j = num; j > 1; j--)
			{
				GenericArraySortHelper<TKey, TValue>.Swap(keys, values, lo, lo + j - 1);
				GenericArraySortHelper<TKey, TValue>.DownHeap(keys, values, 1, j - 1, lo);
			}
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x000E3908 File Offset: 0x000E1B08
		private static void DownHeap(TKey[] keys, TValue[] values, int i, int n, int lo)
		{
			TKey tkey = keys[lo + i - 1];
			TValue tvalue = ((values != null) ? values[lo + i - 1] : default(TValue));
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n && (keys[lo + num - 1] == null || keys[lo + num - 1].CompareTo(keys[lo + num]) < 0))
				{
					num++;
				}
				if (keys[lo + num - 1] == null || keys[lo + num - 1].CompareTo(tkey) < 0)
				{
					break;
				}
				keys[lo + i - 1] = keys[lo + num - 1];
				if (values != null)
				{
					values[lo + i - 1] = values[lo + num - 1];
				}
				i = num;
			}
			keys[lo + i - 1] = tkey;
			if (values != null)
			{
				values[lo + i - 1] = tvalue;
			}
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x000E3A14 File Offset: 0x000E1C14
		private static void InsertionSort(TKey[] keys, TValue[] values, int lo, int hi)
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				TKey tkey = keys[i + 1];
				TValue tvalue = ((values != null) ? values[i + 1] : default(TValue));
				while (num >= lo && (tkey == null || tkey.CompareTo(keys[num]) < 0))
				{
					keys[num + 1] = keys[num];
					if (values != null)
					{
						values[num + 1] = values[num];
					}
					num--;
				}
				keys[num + 1] = tkey;
				if (values != null)
				{
					values[num + 1] = tvalue;
				}
			}
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004DD RID: 1245
	[TypeDependency("System.Collections.Generic.GenericArraySortHelper`1")]
	internal class ArraySortHelper<T> : IArraySortHelper<T>
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06003B50 RID: 15184 RVA: 0x000E22EC File Offset: 0x000E04EC
		public static IArraySortHelper<T> Default
		{
			get
			{
				IArraySortHelper<T> arraySortHelper = ArraySortHelper<T>.defaultArraySortHelper;
				if (arraySortHelper == null)
				{
					arraySortHelper = ArraySortHelper<T>.CreateArraySortHelper();
				}
				return arraySortHelper;
			}
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000E230C File Offset: 0x000E050C
		[SecuritySafeCritical]
		private static IArraySortHelper<T> CreateArraySortHelper()
		{
			if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
			{
				ArraySortHelper<T>.defaultArraySortHelper = (IArraySortHelper<T>)RuntimeTypeHandle.Allocate(typeof(GenericArraySortHelper<string>).TypeHandle.Instantiate(new Type[] { typeof(T) }));
			}
			else
			{
				ArraySortHelper<T>.defaultArraySortHelper = new ArraySortHelper<T>();
			}
			return ArraySortHelper<T>.defaultArraySortHelper;
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000E2384 File Offset: 0x000E0584
		public void Sort(T[] keys, int index, int length, IComparer<T> comparer)
		{
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					ArraySortHelper<T>.IntrospectiveSort(keys, index, length, comparer);
				}
				else
				{
					ArraySortHelper<T>.DepthLimitedQuickSort(keys, index, length + index - 1, comparer, 32);
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

		// Token: 0x06003B53 RID: 15187 RVA: 0x000E23FC File Offset: 0x000E05FC
		public int BinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int num;
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				num = ArraySortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
			}
			return num;
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x000E2448 File Offset: 0x000E0648
		internal static int InternalBinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int i = index;
			int num = index + length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3 = comparer.Compare(array[num2], value);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000E2490 File Offset: 0x000E0690
		private static void SwapIfGreater(T[] keys, IComparer<T> comparer, int a, int b)
		{
			if (a != b && comparer.Compare(keys[a], keys[b]) > 0)
			{
				T t = keys[a];
				keys[a] = keys[b];
				keys[b] = t;
			}
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x000E24D8 File Offset: 0x000E06D8
		private static void Swap(T[] a, int i, int j)
		{
			if (i != j)
			{
				T t = a[i];
				a[i] = a[j];
				a[j] = t;
			}
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x000E2508 File Offset: 0x000E0708
		internal static void DepthLimitedQuickSort(T[] keys, int left, int right, IComparer<T> comparer, int depthLimit)
		{
			while (depthLimit != 0)
			{
				int num = left;
				int num2 = right;
				int num3 = num + (num2 - num >> 1);
				ArraySortHelper<T>.SwapIfGreater(keys, comparer, num, num3);
				ArraySortHelper<T>.SwapIfGreater(keys, comparer, num, num2);
				ArraySortHelper<T>.SwapIfGreater(keys, comparer, num3, num2);
				T t = keys[num3];
				for (;;)
				{
					if (comparer.Compare(keys[num], t) >= 0)
					{
						while (comparer.Compare(t, keys[num2]) < 0)
						{
							num2--;
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							T t2 = keys[num];
							keys[num] = keys[num2];
							keys[num2] = t2;
						}
						num++;
						num2--;
						if (num > num2)
						{
							break;
						}
					}
					else
					{
						num++;
					}
				}
				depthLimit--;
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						ArraySortHelper<T>.DepthLimitedQuickSort(keys, left, num2, comparer, depthLimit);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						ArraySortHelper<T>.DepthLimitedQuickSort(keys, num, right, comparer, depthLimit);
					}
					right = num2;
				}
				if (left >= right)
				{
					return;
				}
			}
			ArraySortHelper<T>.Heapsort(keys, left, right, comparer);
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000E25EF File Offset: 0x000E07EF
		internal static void IntrospectiveSort(T[] keys, int left, int length, IComparer<T> comparer)
		{
			if (length < 2)
			{
				return;
			}
			ArraySortHelper<T>.IntroSort(keys, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(keys.Length), comparer);
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000E2610 File Offset: 0x000E0810
		private static void IntroSort(T[] keys, int lo, int hi, int depthLimit, IComparer<T> comparer)
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
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
						return;
					}
					if (num == 3)
					{
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi - 1);
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, hi - 1, hi);
						return;
					}
					ArraySortHelper<T>.InsertionSort(keys, lo, hi, comparer);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						ArraySortHelper<T>.Heapsort(keys, lo, hi, comparer);
						return;
					}
					depthLimit--;
					int num2 = ArraySortHelper<T>.PickPivotAndPartition(keys, lo, hi, comparer);
					ArraySortHelper<T>.IntroSort(keys, num2 + 1, hi, depthLimit, comparer);
					hi = num2 - 1;
				}
			}
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x000E26AC File Offset: 0x000E08AC
		private static int PickPivotAndPartition(T[] keys, int lo, int hi, IComparer<T> comparer)
		{
			int num = lo + (hi - lo) / 2;
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, num);
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, num, hi);
			T t = keys[num];
			ArraySortHelper<T>.Swap(keys, num, hi - 1);
			int i = lo;
			int num2 = hi - 1;
			while (i < num2)
			{
				while (comparer.Compare(keys[++i], t) < 0)
				{
				}
				while (comparer.Compare(t, keys[--num2]) < 0)
				{
				}
				if (i >= num2)
				{
					break;
				}
				ArraySortHelper<T>.Swap(keys, i, num2);
			}
			ArraySortHelper<T>.Swap(keys, i, hi - 1);
			return i;
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000E273C File Offset: 0x000E093C
		private static void Heapsort(T[] keys, int lo, int hi, IComparer<T> comparer)
		{
			int num = hi - lo + 1;
			for (int i = num / 2; i >= 1; i--)
			{
				ArraySortHelper<T>.DownHeap(keys, i, num, lo, comparer);
			}
			for (int j = num; j > 1; j--)
			{
				ArraySortHelper<T>.Swap(keys, lo, lo + j - 1);
				ArraySortHelper<T>.DownHeap(keys, 1, j - 1, lo, comparer);
			}
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x000E278C File Offset: 0x000E098C
		private static void DownHeap(T[] keys, int i, int n, int lo, IComparer<T> comparer)
		{
			T t = keys[lo + i - 1];
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n && comparer.Compare(keys[lo + num - 1], keys[lo + num]) < 0)
				{
					num++;
				}
				if (comparer.Compare(t, keys[lo + num - 1]) >= 0)
				{
					break;
				}
				keys[lo + i - 1] = keys[lo + num - 1];
				i = num;
			}
			keys[lo + i - 1] = t;
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000E2814 File Offset: 0x000E0A14
		private static void InsertionSort(T[] keys, int lo, int hi, IComparer<T> comparer)
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				T t = keys[i + 1];
				while (num >= lo && comparer.Compare(t, keys[num]) < 0)
				{
					keys[num + 1] = keys[num];
					num--;
				}
				keys[num + 1] = t;
			}
		}

		// Token: 0x0400196A RID: 6506
		private static volatile IArraySortHelper<T> defaultArraySortHelper;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000255 RID: 597
	internal static class Utils
	{
		// Token: 0x060013A5 RID: 5029 RVA: 0x0004A088 File Offset: 0x00048288
		internal static bool TryDispose(object o)
		{
			IDisposable disposable = o as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
				return true;
			}
			return false;
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0004A0A8 File Offset: 0x000482A8
		internal static Task FlushAsync(this Stream stream)
		{
			return Task.Factory.StartNew(new Action(stream.Flush));
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0004A0C4 File Offset: 0x000482C4
		internal static KeyValuePair<int, T>[] StableSort<T>(this T[] array, Comparison<T> comparison)
		{
			ExceptionUtils.CheckArgumentNotNull<T[]>(array, "array");
			ExceptionUtils.CheckArgumentNotNull<Comparison<T>>(comparison, "comparison");
			KeyValuePair<int, T>[] array2 = new KeyValuePair<int, T>[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new KeyValuePair<int, T>(i, array[i]);
			}
			Array.Sort<KeyValuePair<int, T>>(array2, new Utils.StableComparer<T>(comparison));
			return array2;
		}

		// Token: 0x02000256 RID: 598
		private sealed class StableComparer<T> : IComparer<KeyValuePair<int, T>>
		{
			// Token: 0x060013A8 RID: 5032 RVA: 0x0004A124 File Offset: 0x00048324
			public StableComparer(Comparison<T> innerComparer)
			{
				this.innerComparer = innerComparer;
			}

			// Token: 0x060013A9 RID: 5033 RVA: 0x0004A134 File Offset: 0x00048334
			public int Compare(KeyValuePair<int, T> x, KeyValuePair<int, T> y)
			{
				int num = this.innerComparer(x.Value, y.Value);
				if (num == 0)
				{
					num = x.Key - y.Key;
				}
				return num;
			}

			// Token: 0x04000706 RID: 1798
			private readonly Comparison<T> innerComparer;
		}
	}
}

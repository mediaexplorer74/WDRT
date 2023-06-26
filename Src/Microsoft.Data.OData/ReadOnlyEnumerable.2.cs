using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Data.OData
{
	// Token: 0x02000246 RID: 582
	internal sealed class ReadOnlyEnumerable<T> : ReadOnlyEnumerable, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060012AC RID: 4780 RVA: 0x00046749 File Offset: 0x00044949
		internal ReadOnlyEnumerable()
			: this(new List<T>())
		{
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00046756 File Offset: 0x00044956
		internal ReadOnlyEnumerable(IList<T> sourceList)
			: base(sourceList)
		{
			this.sourceList = sourceList;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00046766 File Offset: 0x00044966
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceList.GetEnumerator();
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00046773 File Offset: 0x00044973
		internal static ReadOnlyEnumerable<T> Empty()
		{
			return ReadOnlyEnumerable<T>.EmptyInstance.Value;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0004677F File Offset: 0x0004497F
		internal void AddToSourceList(T itemToAdd)
		{
			this.sourceList.Add(itemToAdd);
		}

		// Token: 0x040006B4 RID: 1716
		private readonly IList<T> sourceList;

		// Token: 0x040006B5 RID: 1717
		private static readonly SimpleLazy<ReadOnlyEnumerable<T>> EmptyInstance = new SimpleLazy<ReadOnlyEnumerable<T>>(() => new ReadOnlyEnumerable<T>(new ReadOnlyCollection<T>(new List<T>(0))), true);
	}
}

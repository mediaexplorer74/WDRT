using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020003C8 RID: 968
	[Serializable]
	internal class TreeSet<T> : SortedSet<T>
	{
		// Token: 0x060024C9 RID: 9417 RVA: 0x000AB55B File Offset: 0x000A975B
		public TreeSet()
		{
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000AB563 File Offset: 0x000A9763
		public TreeSet(IComparer<T> comparer)
			: base(comparer)
		{
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000AB56C File Offset: 0x000A976C
		public TreeSet(ICollection<T> collection)
			: base(collection)
		{
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000AB575 File Offset: 0x000A9775
		public TreeSet(ICollection<T> collection, IComparer<T> comparer)
			: base(collection, comparer)
		{
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000AB57F File Offset: 0x000A977F
		public TreeSet(SerializationInfo siInfo, StreamingContext context)
			: base(siInfo, context)
		{
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000AB58C File Offset: 0x000A978C
		internal override bool AddIfNotPresent(T item)
		{
			bool flag = base.AddIfNotPresent(item);
			if (!flag)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_AddingDuplicate);
			}
			return flag;
		}
	}
}

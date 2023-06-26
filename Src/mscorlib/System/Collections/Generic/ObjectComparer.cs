using System;

namespace System.Collections.Generic
{
	// Token: 0x020004BB RID: 1211
	[Serializable]
	internal class ObjectComparer<T> : Comparer<T>
	{
		// Token: 0x06003A4B RID: 14923 RVA: 0x000DF632 File Offset: 0x000DD832
		public override int Compare(T x, T y)
		{
			return Comparer.Default.Compare(x, y);
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x000DF64C File Offset: 0x000DD84C
		public override bool Equals(object obj)
		{
			ObjectComparer<T> objectComparer = obj as ObjectComparer<T>;
			return objectComparer != null;
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x000DF664 File Offset: 0x000DD864
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}

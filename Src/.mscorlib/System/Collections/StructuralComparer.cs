using System;

namespace System.Collections
{
	// Token: 0x020004A7 RID: 1191
	[Serializable]
	internal class StructuralComparer : IComparer
	{
		// Token: 0x06003919 RID: 14617 RVA: 0x000DBD20 File Offset: 0x000D9F20
		public int Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				IStructuralComparable structuralComparable = x as IStructuralComparable;
				if (structuralComparable != null)
				{
					return structuralComparable.CompareTo(y, this);
				}
				return Comparer.Default.Compare(x, y);
			}
		}
	}
}

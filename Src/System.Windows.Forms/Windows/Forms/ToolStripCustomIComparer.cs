using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x020003E1 RID: 993
	internal class ToolStripCustomIComparer : IComparer
	{
		// Token: 0x060043C4 RID: 17348 RVA: 0x0011EBD4 File Offset: 0x0011CDD4
		int IComparer.Compare(object x, object y)
		{
			if (x.GetType() == y.GetType())
			{
				return 0;
			}
			if (x.GetType().IsAssignableFrom(y.GetType()))
			{
				return 1;
			}
			if (y.GetType().IsAssignableFrom(x.GetType()))
			{
				return -1;
			}
			return 0;
		}
	}
}

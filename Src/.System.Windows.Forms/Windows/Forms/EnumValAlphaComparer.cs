using System;
using System.Collections;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000248 RID: 584
	internal class EnumValAlphaComparer : IComparer
	{
		// Token: 0x06002511 RID: 9489 RVA: 0x000AD551 File Offset: 0x000AB751
		internal EnumValAlphaComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000AD569 File Offset: 0x000AB769
		public int Compare(object a, object b)
		{
			return this.m_compareInfo.Compare(a.ToString(), b.ToString());
		}

		// Token: 0x04000F5E RID: 3934
		private CompareInfo m_compareInfo;

		// Token: 0x04000F5F RID: 3935
		internal static readonly EnumValAlphaComparer Default = new EnumValAlphaComparer();
	}
}

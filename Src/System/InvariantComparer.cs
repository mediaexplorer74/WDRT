using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x0001D8D9 File Offset: 0x0001BAD9
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this.m_compareInfo.Compare(text, text2);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x04000517 RID: 1303
		private CompareInfo m_compareInfo;

		// Token: 0x04000518 RID: 1304
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}

using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000022BE File Offset: 0x000004BE
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022D8 File Offset: 0x000004D8
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

		// Token: 0x04000085 RID: 133
		private CompareInfo m_compareInfo;

		// Token: 0x04000086 RID: 134
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}

using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200011C RID: 284
	internal class AlphaSortedEnumConverter : EnumConverter
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x000181B9 File Offset: 0x000163B9
		public AlphaSortedEnumConverter(Type type)
			: base(type)
		{
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x000181C2 File Offset: 0x000163C2
		protected override IComparer Comparer
		{
			get
			{
				return EnumValAlphaComparer.Default;
			}
		}
	}
}

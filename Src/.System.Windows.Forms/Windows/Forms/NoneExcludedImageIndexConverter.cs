using System;

namespace System.Windows.Forms
{
	// Token: 0x0200030A RID: 778
	internal sealed class NoneExcludedImageIndexConverter : ImageIndexConverter
	{
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06003173 RID: 12659 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected override bool IncludeNoneAsStandardValue
		{
			get
			{
				return false;
			}
		}
	}
}

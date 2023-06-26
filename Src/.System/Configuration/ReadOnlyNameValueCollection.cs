using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x02000097 RID: 151
	internal class ReadOnlyNameValueCollection : NameValueCollection
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x0002294A File Offset: 0x00020B4A
		internal ReadOnlyNameValueCollection(IEqualityComparer equalityComparer)
			: base(equalityComparer)
		{
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00022953 File Offset: 0x00020B53
		internal ReadOnlyNameValueCollection(ReadOnlyNameValueCollection value)
			: base(value)
		{
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0002295C File Offset: 0x00020B5C
		internal void SetReadOnly()
		{
			base.IsReadOnly = true;
		}
	}
}

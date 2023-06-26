using System;
using System.Collections.Generic;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x0200004F RID: 79
	public class ResourceAclComparer : IEqualityComparer<ResourceAcl>
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00009784 File Offset: 0x00007984
		public bool Equals(ResourceAcl x, ResourceAcl y)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(x.Path) && !string.IsNullOrEmpty(y.Path))
			{
				flag = x.Path.Equals(y.Path, StringComparison.OrdinalIgnoreCase);
			}
			return flag;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000097C4 File Offset: 0x000079C4
		public int GetHashCode(ResourceAcl obj)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(obj.Path))
			{
				num = obj.Path.GetHashCode();
			}
			return num;
		}
	}
}

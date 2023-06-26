using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C6 RID: 198
	[NullableContext(1)]
	[Nullable(0)]
	public class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x0002D2A6 File Offset: 0x0002B4A6
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002D2AF File Offset: 0x0002B4AF
		public int GetHashCode(JToken obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}
	}
}

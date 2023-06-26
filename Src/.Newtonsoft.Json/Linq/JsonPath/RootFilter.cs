using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000DB RID: 219
	[NullableContext(1)]
	[Nullable(0)]
	internal class RootFilter : PathFilter
	{
		// Token: 0x06000C1C RID: 3100 RVA: 0x00030973 File Offset: 0x0002EB73
		private RootFilter()
		{
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0003097B File Offset: 0x0002EB7B
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			return new JToken[] { root };
		}

		// Token: 0x040003CD RID: 973
		public static readonly RootFilter Instance = new RootFilter();
	}
}

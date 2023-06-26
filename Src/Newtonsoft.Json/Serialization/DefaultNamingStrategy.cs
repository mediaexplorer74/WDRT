using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000075 RID: 117
	public class DefaultNamingStrategy : NamingStrategy
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x0001B5AF File Offset: 0x000197AF
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return name;
		}
	}
}

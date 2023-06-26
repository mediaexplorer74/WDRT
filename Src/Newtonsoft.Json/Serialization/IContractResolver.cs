using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007E RID: 126
	[NullableContext(1)]
	public interface IContractResolver
	{
		// Token: 0x0600067A RID: 1658
		JsonContract ResolveContract(Type type);
	}
}

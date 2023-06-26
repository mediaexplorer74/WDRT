using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000080 RID: 128
	[NullableContext(1)]
	public interface ISerializationBinder
	{
		// Token: 0x0600067F RID: 1663
		Type BindToType([Nullable(2)] string assemblyName, string typeName);

		// Token: 0x06000680 RID: 1664
		[NullableContext(2)]
		void BindToName([Nullable(1)] Type serializedType, out string assemblyName, out string typeName);
	}
}

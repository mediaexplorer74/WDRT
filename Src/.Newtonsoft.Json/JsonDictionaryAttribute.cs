using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200001E RID: 30
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonDictionaryAttribute : JsonContainerAttribute
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00002F90 File Offset: 0x00001190
		public JsonDictionaryAttribute()
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002F98 File Offset: 0x00001198
		[NullableContext(1)]
		public JsonDictionaryAttribute(string id)
			: base(id)
		{
		}
	}
}

using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E3 RID: 227
	public abstract class DateTimeConverterBase : JsonConverter
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x00031328 File Offset: 0x0002F528
		[NullableContext(1)]
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?));
		}
	}
}

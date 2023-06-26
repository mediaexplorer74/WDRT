using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E0 RID: 224
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CustomCreationConverter<[Nullable(2)] T> : JsonConverter
	{
		// Token: 0x06000C2E RID: 3118 RVA: 0x00030D11 File Offset: 0x0002EF11
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			throw new NotSupportedException("CustomCreationConverter should only be used while deserializing.");
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00030D20 File Offset: 0x0002EF20
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			T t = this.Create(objectType);
			if (t == null)
			{
				throw new JsonSerializationException("No object created.");
			}
			serializer.Populate(reader, t);
			return t;
		}

		// Token: 0x06000C30 RID: 3120
		public abstract T Create(Type objectType);

		// Token: 0x06000C31 RID: 3121 RVA: 0x00030D68 File Offset: 0x0002EF68
		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00030D7A File Offset: 0x0002EF7A
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}

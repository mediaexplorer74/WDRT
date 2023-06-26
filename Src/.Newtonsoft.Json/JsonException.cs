using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonException : Exception
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00002FA1 File Offset: 0x000011A1
		public JsonException()
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002FA9 File Offset: 0x000011A9
		public JsonException(string message)
			: base(message)
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002FB2 File Offset: 0x000011B2
		public JsonException(string message, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002FBC File Offset: 0x000011BC
		public JsonException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002FC6 File Offset: 0x000011C6
		internal static JsonException Create(IJsonLineInfo lineInfo, string path, string message)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			return new JsonException(message);
		}
	}
}

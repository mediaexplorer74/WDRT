using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonReaderException : JsonException
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004B1A File Offset: 0x00002D1A
		public int LineNumber { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00004B22 File Offset: 0x00002D22
		public int LinePosition { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004B2A File Offset: 0x00002D2A
		[Nullable(2)]
		public string Path
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004B32 File Offset: 0x00002D32
		public JsonReaderException()
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004B3A File Offset: 0x00002D3A
		public JsonReaderException(string message)
			: base(message)
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004B43 File Offset: 0x00002D43
		public JsonReaderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004B4D File Offset: 0x00002D4D
		public JsonReaderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004B57 File Offset: 0x00002D57
		public JsonReaderException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004B78 File Offset: 0x00002D78
		internal static JsonReaderException Create(JsonReader reader, string message)
		{
			return JsonReaderException.Create(reader, message, null);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004B82 File Offset: 0x00002D82
		internal static JsonReaderException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return JsonReaderException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004B98 File Offset: 0x00002D98
		internal static JsonReaderException Create([Nullable(2)] IJsonLineInfo lineInfo, string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			int num;
			int num2;
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				num = lineInfo.LineNumber;
				num2 = lineInfo.LinePosition;
			}
			else
			{
				num = 0;
				num2 = 0;
			}
			return new JsonReaderException(message, path, num, num2, ex);
		}
	}
}

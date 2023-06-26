using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000A9 RID: 169
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	[Serializable]
	public class JsonSchemaException : JsonException
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00025EBB File Offset: 0x000240BB
		public int LineNumber { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00025EC3 File Offset: 0x000240C3
		public int LinePosition { get; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00025ECB File Offset: 0x000240CB
		public string Path { get; }

		// Token: 0x060008F9 RID: 2297 RVA: 0x00025ED3 File Offset: 0x000240D3
		public JsonSchemaException()
		{
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00025EDB File Offset: 0x000240DB
		public JsonSchemaException(string message)
			: base(message)
		{
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00025EE4 File Offset: 0x000240E4
		public JsonSchemaException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00025EEE File Offset: 0x000240EE
		public JsonSchemaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00025EF8 File Offset: 0x000240F8
		internal JsonSchemaException(string message, Exception innerException, string path, int lineNumber, int linePosition)
			: base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}
	}
}

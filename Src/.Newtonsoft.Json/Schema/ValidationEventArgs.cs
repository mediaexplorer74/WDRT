using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B3 RID: 179
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x00027AA7 File Offset: 0x00025CA7
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00027AC1 File Offset: 0x00025CC1
		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00027AC9 File Offset: 0x00025CC9
		public string Path
		{
			get
			{
				return this._ex.Path;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00027AD6 File Offset: 0x00025CD6
		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		// Token: 0x04000356 RID: 854
		private readonly JsonSchemaException _ex;
	}
}

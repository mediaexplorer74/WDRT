using System;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000197 RID: 407
	public abstract class ODataParameterWriter
	{
		// Token: 0x06000C25 RID: 3109
		public abstract void WriteStart();

		// Token: 0x06000C26 RID: 3110
		public abstract Task WriteStartAsync();

		// Token: 0x06000C27 RID: 3111
		public abstract void WriteValue(string parameterName, object parameterValue);

		// Token: 0x06000C28 RID: 3112
		public abstract Task WriteValueAsync(string parameterName, object parameterValue);

		// Token: 0x06000C29 RID: 3113
		public abstract ODataCollectionWriter CreateCollectionWriter(string parameterName);

		// Token: 0x06000C2A RID: 3114
		public abstract Task<ODataCollectionWriter> CreateCollectionWriterAsync(string parameterName);

		// Token: 0x06000C2B RID: 3115
		public abstract void WriteEnd();

		// Token: 0x06000C2C RID: 3116
		public abstract Task WriteEndAsync();

		// Token: 0x06000C2D RID: 3117
		public abstract void Flush();

		// Token: 0x06000C2E RID: 3118
		public abstract Task FlushAsync();
	}
}

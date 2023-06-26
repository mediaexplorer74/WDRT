using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x0200013C RID: 316
	internal sealed class InstanceAnnotationWriteTracker
	{
		// Token: 0x06000872 RID: 2162 RVA: 0x0001B7AC File Offset: 0x000199AC
		public InstanceAnnotationWriteTracker()
		{
			this.writeStatus = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001B7C4 File Offset: 0x000199C4
		public bool IsAnnotationWritten(string key)
		{
			return this.writeStatus.Contains(key);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001B7D2 File Offset: 0x000199D2
		public bool MarkAnnotationWritten(string key)
		{
			return this.writeStatus.Add(key);
		}

		// Token: 0x04000334 RID: 820
		private readonly HashSet<string> writeStatus;
	}
}

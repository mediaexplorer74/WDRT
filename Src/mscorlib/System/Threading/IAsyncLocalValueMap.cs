using System;

namespace System.Threading
{
	// Token: 0x020004E6 RID: 1254
	internal interface IAsyncLocalValueMap
	{
		// Token: 0x06003B9C RID: 15260
		bool TryGetValue(IAsyncLocal key, out object value);

		// Token: 0x06003B9D RID: 15261
		IAsyncLocalValueMap Set(IAsyncLocal key, object value, bool treatNullValueAsNonexistent);
	}
}

using System;

namespace System.Net.Cache
{
	// Token: 0x02000317 RID: 791
	internal enum CacheValidationStatus
	{
		// Token: 0x04001B62 RID: 7010
		DoNotUseCache,
		// Token: 0x04001B63 RID: 7011
		Fail,
		// Token: 0x04001B64 RID: 7012
		DoNotTakeFromCache,
		// Token: 0x04001B65 RID: 7013
		RetryResponseFromCache,
		// Token: 0x04001B66 RID: 7014
		RetryResponseFromServer,
		// Token: 0x04001B67 RID: 7015
		ReturnCachedResponse,
		// Token: 0x04001B68 RID: 7016
		CombineCachedAndServerResponse,
		// Token: 0x04001B69 RID: 7017
		CacheResponse,
		// Token: 0x04001B6A RID: 7018
		UpdateResponseInformation,
		// Token: 0x04001B6B RID: 7019
		RemoveFromCache,
		// Token: 0x04001B6C RID: 7020
		DoNotUpdateCache,
		// Token: 0x04001B6D RID: 7021
		Continue
	}
}

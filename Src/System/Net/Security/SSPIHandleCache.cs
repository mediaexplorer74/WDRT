using System;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x02000353 RID: 851
	internal static class SSPIHandleCache
	{
		// Token: 0x06001E7E RID: 7806 RVA: 0x0008F9C4 File Offset: 0x0008DBC4
		internal static void CacheCredential(SafeFreeCredentials newHandle)
		{
			try
			{
				SafeCredentialReference safeCredentialReference = SafeCredentialReference.CreateReference(newHandle);
				if (safeCredentialReference != null)
				{
					int num = Interlocked.Increment(ref SSPIHandleCache._Current) & 31;
					safeCredentialReference = Interlocked.Exchange<SafeCredentialReference>(ref SSPIHandleCache._CacheSlots[num], safeCredentialReference);
					if (safeCredentialReference != null)
					{
						safeCredentialReference.Close();
					}
				}
			}
			catch (Exception ex)
			{
				NclUtilities.IsFatal(ex);
			}
		}

		// Token: 0x04001CD4 RID: 7380
		private const int c_MaxCacheSize = 31;

		// Token: 0x04001CD5 RID: 7381
		private static SafeCredentialReference[] _CacheSlots = new SafeCredentialReference[32];

		// Token: 0x04001CD6 RID: 7382
		private static int _Current = -1;
	}
}

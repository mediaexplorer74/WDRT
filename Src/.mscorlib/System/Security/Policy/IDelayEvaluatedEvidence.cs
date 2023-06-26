using System;

namespace System.Security.Policy
{
	// Token: 0x02000355 RID: 853
	internal interface IDelayEvaluatedEvidence
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002A88 RID: 10888
		bool IsVerified
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002A89 RID: 10889
		bool WasUsed { get; }

		// Token: 0x06002A8A RID: 10890
		void MarkUsed();
	}
}

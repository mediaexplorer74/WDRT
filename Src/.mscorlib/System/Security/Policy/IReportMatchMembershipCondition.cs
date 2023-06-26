using System;

namespace System.Security.Policy
{
	// Token: 0x02000357 RID: 855
	internal interface IReportMatchMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06002A8F RID: 10895
		bool Check(Evidence evidence, out object usedEvidence);
	}
}

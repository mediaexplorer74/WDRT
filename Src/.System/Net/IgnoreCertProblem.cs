using System;

namespace System.Net
{
	// Token: 0x02000124 RID: 292
	internal enum IgnoreCertProblem
	{
		// Token: 0x04000FDE RID: 4062
		not_time_valid = 1,
		// Token: 0x04000FDF RID: 4063
		ctl_not_time_valid,
		// Token: 0x04000FE0 RID: 4064
		not_time_nested = 4,
		// Token: 0x04000FE1 RID: 4065
		invalid_basic_constraints = 8,
		// Token: 0x04000FE2 RID: 4066
		all_not_time_valid = 7,
		// Token: 0x04000FE3 RID: 4067
		allow_unknown_ca = 16,
		// Token: 0x04000FE4 RID: 4068
		wrong_usage = 32,
		// Token: 0x04000FE5 RID: 4069
		invalid_name = 64,
		// Token: 0x04000FE6 RID: 4070
		invalid_policy = 128,
		// Token: 0x04000FE7 RID: 4071
		end_rev_unknown = 256,
		// Token: 0x04000FE8 RID: 4072
		ctl_signer_rev_unknown = 512,
		// Token: 0x04000FE9 RID: 4073
		ca_rev_unknown = 1024,
		// Token: 0x04000FEA RID: 4074
		root_rev_unknown = 2048,
		// Token: 0x04000FEB RID: 4075
		all_rev_unknown = 3840,
		// Token: 0x04000FEC RID: 4076
		none = 4095
	}
}

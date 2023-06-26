using System;

namespace System.Security.Principal
{
	// Token: 0x0200032E RID: 814
	[Serializable]
	internal enum TokenInformationClass
	{
		// Token: 0x04001060 RID: 4192
		TokenUser = 1,
		// Token: 0x04001061 RID: 4193
		TokenGroups,
		// Token: 0x04001062 RID: 4194
		TokenPrivileges,
		// Token: 0x04001063 RID: 4195
		TokenOwner,
		// Token: 0x04001064 RID: 4196
		TokenPrimaryGroup,
		// Token: 0x04001065 RID: 4197
		TokenDefaultDacl,
		// Token: 0x04001066 RID: 4198
		TokenSource,
		// Token: 0x04001067 RID: 4199
		TokenType,
		// Token: 0x04001068 RID: 4200
		TokenImpersonationLevel,
		// Token: 0x04001069 RID: 4201
		TokenStatistics,
		// Token: 0x0400106A RID: 4202
		TokenRestrictedSids,
		// Token: 0x0400106B RID: 4203
		TokenSessionId,
		// Token: 0x0400106C RID: 4204
		TokenGroupsAndPrivileges,
		// Token: 0x0400106D RID: 4205
		TokenSessionReference,
		// Token: 0x0400106E RID: 4206
		TokenSandBoxInert,
		// Token: 0x0400106F RID: 4207
		TokenAuditPolicy,
		// Token: 0x04001070 RID: 4208
		TokenOrigin,
		// Token: 0x04001071 RID: 4209
		TokenElevationType,
		// Token: 0x04001072 RID: 4210
		TokenLinkedToken,
		// Token: 0x04001073 RID: 4211
		TokenElevation,
		// Token: 0x04001074 RID: 4212
		TokenHasRestrictions,
		// Token: 0x04001075 RID: 4213
		TokenAccessInformation,
		// Token: 0x04001076 RID: 4214
		TokenVirtualizationAllowed,
		// Token: 0x04001077 RID: 4215
		TokenVirtualizationEnabled,
		// Token: 0x04001078 RID: 4216
		TokenIntegrityLevel,
		// Token: 0x04001079 RID: 4217
		TokenUIAccess,
		// Token: 0x0400107A RID: 4218
		TokenMandatoryPolicy,
		// Token: 0x0400107B RID: 4219
		TokenLogonSid,
		// Token: 0x0400107C RID: 4220
		TokenIsAppContainer,
		// Token: 0x0400107D RID: 4221
		TokenCapabilities,
		// Token: 0x0400107E RID: 4222
		TokenAppContainerSid,
		// Token: 0x0400107F RID: 4223
		TokenAppContainerNumber,
		// Token: 0x04001080 RID: 4224
		TokenUserClaimAttributes,
		// Token: 0x04001081 RID: 4225
		TokenDeviceClaimAttributes,
		// Token: 0x04001082 RID: 4226
		TokenRestrictedUserClaimAttributes,
		// Token: 0x04001083 RID: 4227
		TokenRestrictedDeviceClaimAttributes,
		// Token: 0x04001084 RID: 4228
		TokenDeviceGroups,
		// Token: 0x04001085 RID: 4229
		TokenRestrictedDeviceGroups,
		// Token: 0x04001086 RID: 4230
		MaxTokenInfoClass
	}
}

using System;

namespace System.Net
{
	// Token: 0x0200011D RID: 285
	internal enum SecurityStatus
	{
		// Token: 0x04000F81 RID: 3969
		OK,
		// Token: 0x04000F82 RID: 3970
		ContinueNeeded = 590610,
		// Token: 0x04000F83 RID: 3971
		CompleteNeeded,
		// Token: 0x04000F84 RID: 3972
		CompAndContinue,
		// Token: 0x04000F85 RID: 3973
		ContextExpired = 590615,
		// Token: 0x04000F86 RID: 3974
		CredentialsNeeded = 590624,
		// Token: 0x04000F87 RID: 3975
		Renegotiate,
		// Token: 0x04000F88 RID: 3976
		OutOfMemory = -2146893056,
		// Token: 0x04000F89 RID: 3977
		InvalidHandle,
		// Token: 0x04000F8A RID: 3978
		Unsupported,
		// Token: 0x04000F8B RID: 3979
		TargetUnknown,
		// Token: 0x04000F8C RID: 3980
		InternalError,
		// Token: 0x04000F8D RID: 3981
		PackageNotFound,
		// Token: 0x04000F8E RID: 3982
		NotOwner,
		// Token: 0x04000F8F RID: 3983
		CannotInstall,
		// Token: 0x04000F90 RID: 3984
		InvalidToken,
		// Token: 0x04000F91 RID: 3985
		CannotPack,
		// Token: 0x04000F92 RID: 3986
		QopNotSupported,
		// Token: 0x04000F93 RID: 3987
		NoImpersonation,
		// Token: 0x04000F94 RID: 3988
		LogonDenied,
		// Token: 0x04000F95 RID: 3989
		UnknownCredentials,
		// Token: 0x04000F96 RID: 3990
		NoCredentials,
		// Token: 0x04000F97 RID: 3991
		MessageAltered,
		// Token: 0x04000F98 RID: 3992
		OutOfSequence,
		// Token: 0x04000F99 RID: 3993
		NoAuthenticatingAuthority,
		// Token: 0x04000F9A RID: 3994
		IncompleteMessage = -2146893032,
		// Token: 0x04000F9B RID: 3995
		IncompleteCredentials = -2146893024,
		// Token: 0x04000F9C RID: 3996
		BufferNotEnough,
		// Token: 0x04000F9D RID: 3997
		WrongPrincipal,
		// Token: 0x04000F9E RID: 3998
		TimeSkew = -2146893020,
		// Token: 0x04000F9F RID: 3999
		UntrustedRoot,
		// Token: 0x04000FA0 RID: 4000
		IllegalMessage,
		// Token: 0x04000FA1 RID: 4001
		CertUnknown,
		// Token: 0x04000FA2 RID: 4002
		CertExpired,
		// Token: 0x04000FA3 RID: 4003
		AlgorithmMismatch = -2146893007,
		// Token: 0x04000FA4 RID: 4004
		SecurityQosFailed,
		// Token: 0x04000FA5 RID: 4005
		SmartcardLogonRequired = -2146892994,
		// Token: 0x04000FA6 RID: 4006
		UnsupportedPreauth = -2146892989,
		// Token: 0x04000FA7 RID: 4007
		BadBinding = -2146892986
	}
}

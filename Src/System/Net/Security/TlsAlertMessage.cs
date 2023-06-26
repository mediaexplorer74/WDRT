using System;

namespace System.Net.Security
{
	// Token: 0x02000362 RID: 866
	internal enum TlsAlertMessage
	{
		// Token: 0x04001D46 RID: 7494
		CloseNotify,
		// Token: 0x04001D47 RID: 7495
		UnexpectedMessage = 10,
		// Token: 0x04001D48 RID: 7496
		BadRecordMac = 20,
		// Token: 0x04001D49 RID: 7497
		DecryptionFailed,
		// Token: 0x04001D4A RID: 7498
		RecordOverflow,
		// Token: 0x04001D4B RID: 7499
		DecompressionFail = 30,
		// Token: 0x04001D4C RID: 7500
		HandshakeFailure = 40,
		// Token: 0x04001D4D RID: 7501
		BadCertificate = 42,
		// Token: 0x04001D4E RID: 7502
		UnsupportedCert,
		// Token: 0x04001D4F RID: 7503
		CertificateRevoked,
		// Token: 0x04001D50 RID: 7504
		CertificateExpired,
		// Token: 0x04001D51 RID: 7505
		CertificateUnknown,
		// Token: 0x04001D52 RID: 7506
		IllegalParameter,
		// Token: 0x04001D53 RID: 7507
		UnknownCA,
		// Token: 0x04001D54 RID: 7508
		AccessDenied,
		// Token: 0x04001D55 RID: 7509
		DecodeError,
		// Token: 0x04001D56 RID: 7510
		DecryptError,
		// Token: 0x04001D57 RID: 7511
		ExportRestriction = 60,
		// Token: 0x04001D58 RID: 7512
		ProtocolVersion = 70,
		// Token: 0x04001D59 RID: 7513
		InsuffientSecurity,
		// Token: 0x04001D5A RID: 7514
		InternalError = 80,
		// Token: 0x04001D5B RID: 7515
		UserCanceled = 90,
		// Token: 0x04001D5C RID: 7516
		NoRenegotiation = 100,
		// Token: 0x04001D5D RID: 7517
		UnsupportedExt = 110
	}
}

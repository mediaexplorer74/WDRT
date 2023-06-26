using System;

// Token: 0x02000003 RID: 3
internal static class Interop
{
	// Token: 0x020006AE RID: 1710
	internal static class SChannel
	{
		// Token: 0x04002EA5 RID: 11941
		public const int SCHANNEL_RENEGOTIATE = 0;

		// Token: 0x04002EA6 RID: 11942
		public const int SCHANNEL_SHUTDOWN = 1;

		// Token: 0x04002EA7 RID: 11943
		public const int SCHANNEL_ALERT = 2;

		// Token: 0x04002EA8 RID: 11944
		public const int SCHANNEL_SESSION = 3;

		// Token: 0x04002EA9 RID: 11945
		public const int TLS1_ALERT_WARNING = 1;

		// Token: 0x04002EAA RID: 11946
		public const int TLS1_ALERT_FATAL = 2;

		// Token: 0x04002EAB RID: 11947
		public const int TLS1_ALERT_CLOSE_NOTIFY = 0;

		// Token: 0x04002EAC RID: 11948
		public const int TLS1_ALERT_UNEXPECTED_MESSAGE = 10;

		// Token: 0x04002EAD RID: 11949
		public const int TLS1_ALERT_BAD_RECORD_MAC = 20;

		// Token: 0x04002EAE RID: 11950
		public const int TLS1_ALERT_DECRYPTION_FAILED = 21;

		// Token: 0x04002EAF RID: 11951
		public const int TLS1_ALERT_RECORD_OVERFLOW = 22;

		// Token: 0x04002EB0 RID: 11952
		public const int TLS1_ALERT_DECOMPRESSION_FAIL = 30;

		// Token: 0x04002EB1 RID: 11953
		public const int TLS1_ALERT_HANDSHAKE_FAILURE = 40;

		// Token: 0x04002EB2 RID: 11954
		public const int TLS1_ALERT_BAD_CERTIFICATE = 42;

		// Token: 0x04002EB3 RID: 11955
		public const int TLS1_ALERT_UNSUPPORTED_CERT = 43;

		// Token: 0x04002EB4 RID: 11956
		public const int TLS1_ALERT_CERTIFICATE_REVOKED = 44;

		// Token: 0x04002EB5 RID: 11957
		public const int TLS1_ALERT_CERTIFICATE_EXPIRED = 45;

		// Token: 0x04002EB6 RID: 11958
		public const int TLS1_ALERT_CERTIFICATE_UNKNOWN = 46;

		// Token: 0x04002EB7 RID: 11959
		public const int TLS1_ALERT_ILLEGAL_PARAMETER = 47;

		// Token: 0x04002EB8 RID: 11960
		public const int TLS1_ALERT_UNKNOWN_CA = 48;

		// Token: 0x04002EB9 RID: 11961
		public const int TLS1_ALERT_ACCESS_DENIED = 49;

		// Token: 0x04002EBA RID: 11962
		public const int TLS1_ALERT_DECODE_ERROR = 50;

		// Token: 0x04002EBB RID: 11963
		public const int TLS1_ALERT_DECRYPT_ERROR = 51;

		// Token: 0x04002EBC RID: 11964
		public const int TLS1_ALERT_EXPORT_RESTRICTION = 60;

		// Token: 0x04002EBD RID: 11965
		public const int TLS1_ALERT_PROTOCOL_VERSION = 70;

		// Token: 0x04002EBE RID: 11966
		public const int TLS1_ALERT_INSUFFIENT_SECURITY = 71;

		// Token: 0x04002EBF RID: 11967
		public const int TLS1_ALERT_INTERNAL_ERROR = 80;

		// Token: 0x04002EC0 RID: 11968
		public const int TLS1_ALERT_USER_CANCELED = 90;

		// Token: 0x04002EC1 RID: 11969
		public const int TLS1_ALERT_NO_RENEGOTIATION = 100;

		// Token: 0x04002EC2 RID: 11970
		public const int TLS1_ALERT_UNSUPPORTED_EXT = 110;

		// Token: 0x020008C9 RID: 2249
		public struct SCHANNEL_ALERT_TOKEN
		{
			// Token: 0x04003B48 RID: 15176
			public uint dwTokenType;

			// Token: 0x04003B49 RID: 15177
			public uint dwAlertType;

			// Token: 0x04003B4A RID: 15178
			public uint dwAlertNumber;
		}
	}
}

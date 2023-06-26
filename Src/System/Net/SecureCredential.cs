using System;
using System.Diagnostics;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x0200012E RID: 302
	internal struct SecureCredential
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0003DA64 File Offset: 0x0003BC64
		public SecureCredential(int version, X509Certificate certificate, SecureCredential.Flags flags, SchProtocols protocols, EncryptionPolicy policy)
		{
			this.rootStore = (this.phMappers = (this.palgSupportedAlgs = (this.certContextArray = IntPtr.Zero)));
			this.cCreds = (this.cMappers = (this.cSupportedAlgs = 0));
			if (policy == EncryptionPolicy.RequireEncryption)
			{
				this.dwMinimumCipherStrength = 0;
				this.dwMaximumCipherStrength = 0;
			}
			else if (policy == EncryptionPolicy.AllowNoEncryption)
			{
				this.dwMinimumCipherStrength = -1;
				this.dwMaximumCipherStrength = 0;
			}
			else
			{
				if (policy != EncryptionPolicy.NoEncryption)
				{
					throw new ArgumentException(System.SR.GetString("net_invalid_enum", new object[] { "EncryptionPolicy" }), "policy");
				}
				this.dwMinimumCipherStrength = -1;
				this.dwMaximumCipherStrength = -1;
			}
			this.dwSessionLifespan = (this.reserved = 0);
			this.version = version;
			this.dwFlags = flags;
			this.grbitEnabledProtocols = protocols;
			if (certificate != null)
			{
				this.certContextArray = certificate.Handle;
				this.cCreds = 1;
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0003DB4D File Offset: 0x0003BD4D
		[Conditional("TRAVE")]
		internal void DebugDump()
		{
		}

		// Token: 0x0400100E RID: 4110
		public const int CurrentVersion = 4;

		// Token: 0x0400100F RID: 4111
		public int version;

		// Token: 0x04001010 RID: 4112
		public int cCreds;

		// Token: 0x04001011 RID: 4113
		public IntPtr certContextArray;

		// Token: 0x04001012 RID: 4114
		private readonly IntPtr rootStore;

		// Token: 0x04001013 RID: 4115
		public int cMappers;

		// Token: 0x04001014 RID: 4116
		private readonly IntPtr phMappers;

		// Token: 0x04001015 RID: 4117
		public int cSupportedAlgs;

		// Token: 0x04001016 RID: 4118
		private readonly IntPtr palgSupportedAlgs;

		// Token: 0x04001017 RID: 4119
		public SchProtocols grbitEnabledProtocols;

		// Token: 0x04001018 RID: 4120
		public int dwMinimumCipherStrength;

		// Token: 0x04001019 RID: 4121
		public int dwMaximumCipherStrength;

		// Token: 0x0400101A RID: 4122
		public int dwSessionLifespan;

		// Token: 0x0400101B RID: 4123
		public SecureCredential.Flags dwFlags;

		// Token: 0x0400101C RID: 4124
		public int reserved;

		// Token: 0x0200070A RID: 1802
		[Flags]
		public enum Flags
		{
			// Token: 0x040030DD RID: 12509
			Zero = 0,
			// Token: 0x040030DE RID: 12510
			NoSystemMapper = 2,
			// Token: 0x040030DF RID: 12511
			NoNameCheck = 4,
			// Token: 0x040030E0 RID: 12512
			ValidateManual = 8,
			// Token: 0x040030E1 RID: 12513
			NoDefaultCred = 16,
			// Token: 0x040030E2 RID: 12514
			ValidateAuto = 32,
			// Token: 0x040030E3 RID: 12515
			SendAuxRecord = 2097152,
			// Token: 0x040030E4 RID: 12516
			UseStrongCrypto = 4194304
		}
	}
}

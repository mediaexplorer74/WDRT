using System;
using System.Net.Security;

namespace System.Net
{
	// Token: 0x0200012F RID: 303
	internal struct SecureCredential2
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x0003DB50 File Offset: 0x0003BD50
		public SecureCredential2(SecureCredential2.Flags flags, SchProtocols protocols, EncryptionPolicy policy)
		{
			this.rootStore = (this.phMappers = IntPtr.Zero);
			this.pTlsParameters = null;
			this.certContextArray = null;
			this.cCreds = (this.cMappers = (this.cTlsParameters = (this.dwCredformat = 0)));
			this.dwSessionLifespan = 0;
			this.version = 5;
			this.dwFlags = flags;
			if (policy == EncryptionPolicy.AllowNoEncryption)
			{
				this.dwFlags |= SecureCredential2.Flags.AllowNullEencryption;
			}
		}

		// Token: 0x0400101D RID: 4125
		public const int CurrentVersion = 5;

		// Token: 0x0400101E RID: 4126
		public int version;

		// Token: 0x0400101F RID: 4127
		public int dwCredformat;

		// Token: 0x04001020 RID: 4128
		public int cCreds;

		// Token: 0x04001021 RID: 4129
		public unsafe void** certContextArray;

		// Token: 0x04001022 RID: 4130
		private readonly IntPtr rootStore;

		// Token: 0x04001023 RID: 4131
		public int cMappers;

		// Token: 0x04001024 RID: 4132
		private readonly IntPtr phMappers;

		// Token: 0x04001025 RID: 4133
		public int dwSessionLifespan;

		// Token: 0x04001026 RID: 4134
		public SecureCredential2.Flags dwFlags;

		// Token: 0x04001027 RID: 4135
		public int cTlsParameters;

		// Token: 0x04001028 RID: 4136
		public unsafe TlsParamaters* pTlsParameters;

		// Token: 0x0200070B RID: 1803
		[Flags]
		public enum Flags
		{
			// Token: 0x040030E6 RID: 12518
			Zero = 0,
			// Token: 0x040030E7 RID: 12519
			NoSystemMapper = 2,
			// Token: 0x040030E8 RID: 12520
			NoNameCheck = 4,
			// Token: 0x040030E9 RID: 12521
			ValidateManual = 8,
			// Token: 0x040030EA RID: 12522
			NoDefaultCred = 16,
			// Token: 0x040030EB RID: 12523
			ValidateAuto = 32,
			// Token: 0x040030EC RID: 12524
			SendAuxRecord = 2097152,
			// Token: 0x040030ED RID: 12525
			UseStrongCrypto = 4194304,
			// Token: 0x040030EE RID: 12526
			UsePresharedKeyOnly = 8388608,
			// Token: 0x040030EF RID: 12527
			AllowNullEencryption = 33554432
		}
	}
}

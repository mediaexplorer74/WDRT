using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Policy
{
	// Token: 0x02000101 RID: 257
	internal class ParsedData
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000DC64 File Offset: 0x0000BE64
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		public bool RequestsShellIntegration
		{
			get
			{
				return this.requestsShellIntegration;
			}
			set
			{
				this.requestsShellIntegration = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000DC75 File Offset: 0x0000BE75
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000DC7D File Offset: 0x0000BE7D
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
			set
			{
				this.certificate = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000DC86 File Offset: 0x0000BE86
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000DC8E File Offset: 0x0000BE8E
		public string AppName
		{
			get
			{
				return this.appName;
			}
			set
			{
				this.appName = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000DC97 File Offset: 0x0000BE97
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000DC9F File Offset: 0x0000BE9F
		public string AppPublisher
		{
			get
			{
				return this.appPublisher;
			}
			set
			{
				this.appPublisher = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000DCB0 File Offset: 0x0000BEB0
		public string AuthenticodedPublisher
		{
			get
			{
				return this.authenticodedPublisher;
			}
			set
			{
				this.authenticodedPublisher = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000DCB9 File Offset: 0x0000BEB9
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000DCC1 File Offset: 0x0000BEC1
		public bool UseManifestForTrust
		{
			get
			{
				return this.disallowTrustOverride;
			}
			set
			{
				this.disallowTrustOverride = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000DCCA File Offset: 0x0000BECA
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000DCD2 File Offset: 0x0000BED2
		public string SupportUrl
		{
			get
			{
				return this.supportUrl;
			}
			set
			{
				this.supportUrl = value;
			}
		}

		// Token: 0x0400043D RID: 1085
		private bool requestsShellIntegration;

		// Token: 0x0400043E RID: 1086
		private string appName;

		// Token: 0x0400043F RID: 1087
		private string appPublisher;

		// Token: 0x04000440 RID: 1088
		private string supportUrl;

		// Token: 0x04000441 RID: 1089
		private string authenticodedPublisher;

		// Token: 0x04000442 RID: 1090
		private bool disallowTrustOverride;

		// Token: 0x04000443 RID: 1091
		private X509Certificate2 certificate;
	}
}

using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B4 RID: 692
	internal struct AsnReaderOptions
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x00086C93 File Offset: 0x00084E93
		// (set) Token: 0x060024E9 RID: 9449 RVA: 0x00086CA9 File Offset: 0x00084EA9
		public int UtcTimeTwoDigitYearMax
		{
			get
			{
				if (this._twoDigitYearMax == 0)
				{
					return 2049;
				}
				return (int)this._twoDigitYearMax;
			}
			set
			{
				if (value < 1 || value > 9999)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._twoDigitYearMax = (ushort)value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x00086CCA File Offset: 0x00084ECA
		// (set) Token: 0x060024EB RID: 9451 RVA: 0x00086CD2 File Offset: 0x00084ED2
		public bool SkipSetSortOrderVerification
		{
			get
			{
				return this._skipSetSortOrderVerification;
			}
			set
			{
				this._skipSetSortOrderVerification = value;
			}
		}

		// Token: 0x04000DCD RID: 3533
		private const int DefaultTwoDigitMax = 2049;

		// Token: 0x04000DCE RID: 3534
		private ushort _twoDigitYearMax;

		// Token: 0x04000DCF RID: 3535
		private bool _skipSetSortOrderVerification;
	}
}

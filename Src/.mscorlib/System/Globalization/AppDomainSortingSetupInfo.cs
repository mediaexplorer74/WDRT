using System;

namespace System.Globalization
{
	// Token: 0x0200039E RID: 926
	internal sealed class AppDomainSortingSetupInfo
	{
		// Token: 0x06002DB6 RID: 11702 RVA: 0x000B0261 File Offset: 0x000AE461
		internal AppDomainSortingSetupInfo()
		{
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000B026C File Offset: 0x000AE46C
		internal AppDomainSortingSetupInfo(AppDomainSortingSetupInfo copy)
		{
			this._useV2LegacySorting = copy._useV2LegacySorting;
			this._useV4LegacySorting = copy._useV4LegacySorting;
			this._pfnIsNLSDefinedString = copy._pfnIsNLSDefinedString;
			this._pfnCompareStringEx = copy._pfnCompareStringEx;
			this._pfnLCMapStringEx = copy._pfnLCMapStringEx;
			this._pfnFindNLSStringEx = copy._pfnFindNLSStringEx;
			this._pfnFindStringOrdinal = copy._pfnFindStringOrdinal;
			this._pfnCompareStringOrdinal = copy._pfnCompareStringOrdinal;
			this._pfnGetNLSVersionEx = copy._pfnGetNLSVersionEx;
		}

		// Token: 0x0400129E RID: 4766
		internal IntPtr _pfnIsNLSDefinedString;

		// Token: 0x0400129F RID: 4767
		internal IntPtr _pfnCompareStringEx;

		// Token: 0x040012A0 RID: 4768
		internal IntPtr _pfnLCMapStringEx;

		// Token: 0x040012A1 RID: 4769
		internal IntPtr _pfnFindNLSStringEx;

		// Token: 0x040012A2 RID: 4770
		internal IntPtr _pfnCompareStringOrdinal;

		// Token: 0x040012A3 RID: 4771
		internal IntPtr _pfnGetNLSVersionEx;

		// Token: 0x040012A4 RID: 4772
		internal IntPtr _pfnFindStringOrdinal;

		// Token: 0x040012A5 RID: 4773
		internal bool _useV2LegacySorting;

		// Token: 0x040012A6 RID: 4774
		internal bool _useV4LegacySorting;
	}
}

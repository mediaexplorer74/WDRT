using System;
using System.Runtime.Versioning;

namespace System.Collections.Generic
{
	// Token: 0x020004DC RID: 1244
	internal static class IntrospectiveSortUtilities
	{
		// Token: 0x06003B4E RID: 15182 RVA: 0x000E22A8 File Offset: 0x000E04A8
		internal static int FloorLog2(int n)
		{
			int num = 0;
			while (n >= 1)
			{
				num++;
				n /= 2;
			}
			return num;
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x000E22C7 File Offset: 0x000E04C7
		internal static void ThrowOrIgnoreBadComparer(object comparer)
		{
			if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[] { comparer }));
			}
		}

		// Token: 0x04001968 RID: 6504
		internal const int IntrosortSizeThreshold = 16;

		// Token: 0x04001969 RID: 6505
		internal const int QuickSortDepthThreshold = 32;
	}
}

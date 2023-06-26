using System;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004E4 RID: 1252
	internal class CategoryEntry
	{
		// Token: 0x06002F5F RID: 12127 RVA: 0x000D5C80 File Offset: 0x000D3E80
		internal CategoryEntry(NativeMethods.PERF_OBJECT_TYPE perfObject)
		{
			this.NameIndex = perfObject.ObjectNameTitleIndex;
			this.HelpIndex = perfObject.ObjectHelpTitleIndex;
			this.CounterIndexes = new int[perfObject.NumCounters];
			this.HelpIndexes = new int[perfObject.NumCounters];
		}

		// Token: 0x040027D7 RID: 10199
		internal int NameIndex;

		// Token: 0x040027D8 RID: 10200
		internal int HelpIndex;

		// Token: 0x040027D9 RID: 10201
		internal int[] CounterIndexes;

		// Token: 0x040027DA RID: 10202
		internal int[] HelpIndexes;
	}
}

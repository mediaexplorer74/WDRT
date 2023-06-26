using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000055 RID: 85
	public sealed class ReadingEntryArgs
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0000CF96 File Offset: 0x0000B196
		public ReadingEntryArgs(ODataEntry entry)
		{
			this.Entry = entry;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000CFA5 File Offset: 0x0000B1A5
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000CFAD File Offset: 0x0000B1AD
		public ODataEntry Entry { get; private set; }
	}
}

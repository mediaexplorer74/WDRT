using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001F7 RID: 503
	internal sealed class EpmCustomReader : EpmReader
	{
		// Token: 0x06000F66 RID: 3942 RVA: 0x0003747F File Offset: 0x0003567F
		private EpmCustomReader(IODataAtomReaderEntryState entryState, ODataAtomInputContext inputContext)
			: base(entryState, inputContext)
		{
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003748C File Offset: 0x0003568C
		internal static void ReadEntryEpm(IODataAtomReaderEntryState entryState, ODataAtomInputContext inputContext)
		{
			EpmCustomReader epmCustomReader = new EpmCustomReader(entryState, inputContext);
			epmCustomReader.ReadEntryEpm();
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x000374A8 File Offset: 0x000356A8
		private void ReadEntryEpm()
		{
			EpmCustomReaderValueCache epmCustomReaderValueCache = base.EntryState.EpmCustomReaderValueCache;
			foreach (KeyValuePair<EntityPropertyMappingInfo, string> keyValuePair in epmCustomReaderValueCache.CustomEpmValues)
			{
				base.SetEntryEpmValue(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}

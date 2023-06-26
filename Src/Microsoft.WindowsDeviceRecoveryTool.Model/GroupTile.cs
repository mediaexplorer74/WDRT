using System;
using System.Collections.Generic;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x0200000A RID: 10
	public class GroupTile : Tile
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002B70 File Offset: 0x00000D70
		public GroupTile(IEnumerable<Tile> tilesInGroup)
		{
			bool flag = tilesInGroup == null;
			if (flag)
			{
				throw new ArgumentNullException("tilesInGroup");
			}
			this.TilesInGroup = tilesInGroup;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002BA1 File Offset: 0x00000DA1
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002BA9 File Offset: 0x00000DA9
		public IEnumerable<Tile> TilesInGroup { get; private set; }
	}
}

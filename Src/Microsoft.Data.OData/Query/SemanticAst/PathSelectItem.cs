using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000074 RID: 116
	public sealed class PathSelectItem : SelectItem
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000A81C File Offset: 0x00008A1C
		public PathSelectItem(ODataSelectPath selectedPath)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataSelectPath>(selectedPath, "selectedPath");
			this.selectedPath = selectedPath;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000A836 File Offset: 0x00008A36
		public ODataSelectPath SelectedPath
		{
			get
			{
				return this.selectedPath;
			}
		}

		// Token: 0x040000BF RID: 191
		private readonly ODataSelectPath selectedPath;
	}
}

using System;
using System.Globalization;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000204 RID: 516
	internal sealed class EpmAttributeNameBuilder
	{
		// Token: 0x06000FD0 RID: 4048 RVA: 0x00039DCB File Offset: 0x00037FCB
		internal EpmAttributeNameBuilder()
		{
			this.suffix = string.Empty;
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00039DDE File Offset: 0x00037FDE
		internal string EpmKeepInContent
		{
			get
			{
				return "FC_KeepInContent" + this.suffix;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00039DF0 File Offset: 0x00037FF0
		internal string EpmSourcePath
		{
			get
			{
				return "FC_SourcePath" + this.suffix;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00039E02 File Offset: 0x00038002
		internal string EpmTargetPath
		{
			get
			{
				return "FC_TargetPath" + this.suffix;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00039E14 File Offset: 0x00038014
		internal string EpmContentKind
		{
			get
			{
				return "FC_ContentKind" + this.suffix;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00039E26 File Offset: 0x00038026
		internal string EpmNsPrefix
		{
			get
			{
				return "FC_NsPrefix" + this.suffix;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00039E38 File Offset: 0x00038038
		internal string EpmNsUri
		{
			get
			{
				return "FC_NsUri" + this.suffix;
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00039E4A File Offset: 0x0003804A
		internal void MoveNext()
		{
			this.index++;
			this.suffix = "_" + this.index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x040005BA RID: 1466
		private const string Separator = "_";

		// Token: 0x040005BB RID: 1467
		private int index;

		// Token: 0x040005BC RID: 1468
		private string suffix;
	}
}

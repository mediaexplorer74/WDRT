using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;

namespace Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives
{
	// Token: 0x02000007 RID: 7
	public sealed class VariantInfo
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000022FF File Offset: 0x000004FF
		public VariantInfo(string name, IdentificationInfo identificationInfo, QueryParameters msrQueryParameters)
		{
			this.Name = name;
			this.IdentificationInfo = identificationInfo;
			this.MsrQueryParameters = msrQueryParameters;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000231C File Offset: 0x0000051C
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002324 File Offset: 0x00000524
		public string Name { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000232D File Offset: 0x0000052D
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002335 File Offset: 0x00000535
		public IdentificationInfo IdentificationInfo { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000233E File Offset: 0x0000053E
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002346 File Offset: 0x00000546
		public QueryParameters MsrQueryParameters { get; private set; }
	}
}

using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Contracts
{
	// Token: 0x0200003C RID: 60
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class ExportAdaptationAttribute : ExportAttribute
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000D169 File Offset: 0x0000B369
		public ExportAdaptationAttribute()
			: base(typeof(IAdaptation))
		{
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000D17D File Offset: 0x0000B37D
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000D185 File Offset: 0x0000B385
		public PhoneTypes Type { get; set; }
	}
}

using System;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers
{
	// Token: 0x02000036 RID: 54
	[AttributeUsage(AttributeTargets.All)]
	public sealed class UriDescriptionAttribute : Attribute
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0000CDBA File Offset: 0x0000AFBA
		public UriDescriptionAttribute(string value)
		{
			this.value = value;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000183 RID: 387
		private readonly string value;
	}
}

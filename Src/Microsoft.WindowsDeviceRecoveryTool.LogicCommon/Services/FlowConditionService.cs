using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x0200000B RID: 11
	[Export]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class FlowConditionService
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003488 File Offset: 0x00001688
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000034B2 File Offset: 0x000016B2
		public bool UseSignatureCheck
		{
			get
			{
				bool flag = !this.IsSignatureCheckChoiceAvailable;
				return flag || this.useSignatureCheck;
			}
			set
			{
				this.useSignatureCheck = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000034BC File Offset: 0x000016BC
		public bool IsSignatureCheckChoiceAvailable
		{
			get
			{
				return this.IsTestConfigFileAvailable;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000034D4 File Offset: 0x000016D4
		public bool IsManualSelectionAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000034E8 File Offset: 0x000016E8
		public bool IsTestConfigFileAvailable
		{
			get
			{
				this.Initialize();
				return this.isTestConfigFileAvailable;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003508 File Offset: 0x00001708
		private void Initialize()
		{
			bool flag = this.initialized;
			if (!flag)
			{
				try
				{
					this.isTestConfigFileAvailable = false;
					this.initialized = true;
				}
				catch (Exception ex)
				{
					Tracer<FlowConditionService>.WriteWarning(ex, "Could not be initialized", new object[0]);
				}
			}
		}

		// Token: 0x0400002A RID: 42
		private const string TestConfigFileName = "test.cfg";

		// Token: 0x0400002B RID: 43
		private bool initialized;

		// Token: 0x0400002C RID: 44
		private bool isTestConfigFileAvailable;

		// Token: 0x0400002D RID: 45
		private bool useSignatureCheck;
	}
}

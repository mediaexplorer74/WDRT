using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic
{
	// Token: 0x020000E3 RID: 227
	[Export]
	public class AppContext : NotificationObject
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00020F9C File Offset: 0x0001F19C
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x00020FB4 File Offset: 0x0001F1B4
		public bool IsCloseAppEnabled
		{
			get
			{
				return this.isCloseAppEnabled;
			}
			set
			{
				base.SetValue<bool>(() => this.IsCloseAppEnabled, ref this.isCloseAppEnabled, value);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00020FF4 File Offset: 0x0001F1F4
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0002100C File Offset: 0x0001F20C
		public bool IsExecutingBackgroundOperation
		{
			get
			{
				return this.isExecutingBackgroundOperation;
			}
			set
			{
				base.SetValue<bool>(() => this.IsExecutingBackgroundOperation, ref this.isExecutingBackgroundOperation, value);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0002104C File Offset: 0x0001F24C
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x00021064 File Offset: 0x0001F264
		public bool IsMachineStateRunning
		{
			get
			{
				return this.isMachineStateRunning;
			}
			set
			{
				base.SetValue<bool>(() => this.IsMachineStateRunning, ref this.isMachineStateRunning, value);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x000210A4 File Offset: 0x0001F2A4
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x000210BC File Offset: 0x0001F2BC
		public bool IsUpdate
		{
			get
			{
				return this.isUpdate;
			}
			set
			{
				base.SetValue<bool>(() => this.IsUpdate, ref this.isUpdate, value);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x000210FC File Offset: 0x0001F2FC
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x00021114 File Offset: 0x0001F314
		public Phone CurrentPhone
		{
			get
			{
				return this.currentPhone;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					Tracer<AppContext>.WriteInformation("Set current phone to NULL");
				}
				else
				{
					Tracer<AppContext>.WriteInformation("Set current phone to: {0}", new object[] { value });
				}
				base.SetValue<Phone>(() => this.CurrentPhone, ref this.currentPhone, value);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00021190 File Offset: 0x0001F390
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x000211A8 File Offset: 0x0001F3A8
		public PhoneTypes SelectedManufacturer
		{
			get
			{
				return this.selectedManufacturer;
			}
			set
			{
				base.SetValue<PhoneTypes>(() => this.SelectedManufacturer, ref this.selectedManufacturer, value);
			}
		}

		// Token: 0x04000337 RID: 823
		private bool isMachineStateRunning;

		// Token: 0x04000338 RID: 824
		private bool isUpdate;

		// Token: 0x04000339 RID: 825
		private bool isCloseAppEnabled = true;

		// Token: 0x0400033A RID: 826
		private Phone currentPhone;

		// Token: 0x0400033B RID: 827
		private PhoneTypes selectedManufacturer;

		// Token: 0x0400033C RID: 828
		private bool isExecutingBackgroundOperation;
	}
}

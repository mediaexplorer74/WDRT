using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000004 RID: 4
	public class DetectionParameters
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000027B5 File Offset: 0x000009B5
		public DetectionParameters(PhoneTypes phoneTypes, PhoneModes phoneModes)
		{
			this.PhoneTypes = phoneTypes;
			this.PhoneModes = phoneModes;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000027CF File Offset: 0x000009CF
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000027D7 File Offset: 0x000009D7
		public PhoneTypes PhoneTypes { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000027E0 File Offset: 0x000009E0
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000027E8 File Offset: 0x000009E8
		public PhoneModes PhoneModes { get; private set; }
	}
}

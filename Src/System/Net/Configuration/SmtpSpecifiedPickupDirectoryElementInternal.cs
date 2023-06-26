using System;

namespace System.Net.Configuration
{
	// Token: 0x02000347 RID: 839
	internal sealed class SmtpSpecifiedPickupDirectoryElementInternal
	{
		// Token: 0x06001E14 RID: 7700 RVA: 0x0008D4A3 File Offset: 0x0008B6A3
		internal SmtpSpecifiedPickupDirectoryElementInternal(SmtpSpecifiedPickupDirectoryElement element)
		{
			this.pickupDirectoryLocation = element.PickupDirectoryLocation;
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x0008D4B7 File Offset: 0x0008B6B7
		internal string PickupDirectoryLocation
		{
			get
			{
				return this.pickupDirectoryLocation;
			}
		}

		// Token: 0x04001C9B RID: 7323
		private string pickupDirectoryLocation;
	}
}

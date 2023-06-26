using System;

namespace System.Net.Mail
{
	/// <summary>Describes the delivery notification options for email.</summary>
	// Token: 0x0200026F RID: 623
	[Flags]
	public enum DeliveryNotificationOptions
	{
		/// <summary>No notification information will be sent. The mail server will utilize its configured behavior to determine whether it should generate a delivery notification.</summary>
		// Token: 0x040017C1 RID: 6081
		None = 0,
		/// <summary>Notify if the delivery is successful.</summary>
		// Token: 0x040017C2 RID: 6082
		OnSuccess = 1,
		/// <summary>Notify if the delivery is unsuccessful.</summary>
		// Token: 0x040017C3 RID: 6083
		OnFailure = 2,
		/// <summary>Notify if the delivery is delayed.</summary>
		// Token: 0x040017C4 RID: 6084
		Delay = 4,
		/// <summary>A notification should not be generated under any circumstances.</summary>
		// Token: 0x040017C5 RID: 6085
		Never = 134217728
	}
}

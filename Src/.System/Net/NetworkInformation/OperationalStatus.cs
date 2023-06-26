using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the operational state of a network interface.</summary>
	// Token: 0x020002E6 RID: 742
	[global::__DynamicallyInvokable]
	public enum OperationalStatus
	{
		/// <summary>The network interface is up; it can transmit data packets.</summary>
		// Token: 0x04001A4D RID: 6733
		[global::__DynamicallyInvokable]
		Up = 1,
		/// <summary>The network interface is unable to transmit data packets.</summary>
		// Token: 0x04001A4E RID: 6734
		[global::__DynamicallyInvokable]
		Down,
		/// <summary>The network interface is running tests.</summary>
		// Token: 0x04001A4F RID: 6735
		[global::__DynamicallyInvokable]
		Testing,
		/// <summary>The network interface status is not known.</summary>
		// Token: 0x04001A50 RID: 6736
		[global::__DynamicallyInvokable]
		Unknown,
		/// <summary>The network interface is not in a condition to transmit data packets; it is waiting for an external event.</summary>
		// Token: 0x04001A51 RID: 6737
		[global::__DynamicallyInvokable]
		Dormant,
		/// <summary>The network interface is unable to transmit data packets because of a missing component, typically a hardware component.</summary>
		// Token: 0x04001A52 RID: 6738
		[global::__DynamicallyInvokable]
		NotPresent,
		/// <summary>The network interface is unable to transmit data packets because it runs on top of one or more other interfaces, and at least one of these "lower layer" interfaces is down.</summary>
		// Token: 0x04001A53 RID: 6739
		[global::__DynamicallyInvokable]
		LowerLayerDown
	}
}

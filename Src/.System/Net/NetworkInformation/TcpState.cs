using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the states of a Transmission Control Protocol (TCP) connection.</summary>
	// Token: 0x02000306 RID: 774
	[global::__DynamicallyInvokable]
	public enum TcpState
	{
		/// <summary>The TCP connection state is unknown.</summary>
		// Token: 0x04001ADD RID: 6877
		[global::__DynamicallyInvokable]
		Unknown,
		/// <summary>The TCP connection is closed.</summary>
		// Token: 0x04001ADE RID: 6878
		[global::__DynamicallyInvokable]
		Closed,
		/// <summary>The local endpoint of the TCP connection is listening for a connection request from any remote endpoint.</summary>
		// Token: 0x04001ADF RID: 6879
		[global::__DynamicallyInvokable]
		Listen,
		/// <summary>The local endpoint of the TCP connection has sent the remote endpoint a segment header with the synchronize (SYN) control bit set and is waiting for a matching connection request.</summary>
		// Token: 0x04001AE0 RID: 6880
		[global::__DynamicallyInvokable]
		SynSent,
		/// <summary>The local endpoint of the TCP connection has sent and received a connection request and is waiting for an acknowledgment.</summary>
		// Token: 0x04001AE1 RID: 6881
		[global::__DynamicallyInvokable]
		SynReceived,
		/// <summary>The TCP handshake is complete. The connection has been established and data can be sent.</summary>
		// Token: 0x04001AE2 RID: 6882
		[global::__DynamicallyInvokable]
		Established,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the remote endpoint or for an acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x04001AE3 RID: 6883
		[global::__DynamicallyInvokable]
		FinWait1,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the remote endpoint.</summary>
		// Token: 0x04001AE4 RID: 6884
		[global::__DynamicallyInvokable]
		FinWait2,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the local user.</summary>
		// Token: 0x04001AE5 RID: 6885
		[global::__DynamicallyInvokable]
		CloseWait,
		/// <summary>The local endpoint of the TCP connection is waiting for an acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x04001AE6 RID: 6886
		[global::__DynamicallyInvokable]
		Closing,
		/// <summary>The local endpoint of the TCP connection is waiting for the final acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x04001AE7 RID: 6887
		[global::__DynamicallyInvokable]
		LastAck,
		/// <summary>The local endpoint of the TCP connection is waiting for enough time to pass to ensure that the remote endpoint received the acknowledgement of its connection termination request.</summary>
		// Token: 0x04001AE8 RID: 6888
		[global::__DynamicallyInvokable]
		TimeWait,
		/// <summary>The transmission control buffer (TCB) for the TCP connection is being deleted.</summary>
		// Token: 0x04001AE9 RID: 6889
		[global::__DynamicallyInvokable]
		DeleteTcb
	}
}

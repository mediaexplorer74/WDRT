using System;

namespace System.Net.Sockets
{
	/// <summary>Defines error codes for the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
	// Token: 0x0200037D RID: 893
	[global::__DynamicallyInvokable]
	public enum SocketError
	{
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> operation succeeded.</summary>
		// Token: 0x04001EAA RID: 7850
		[global::__DynamicallyInvokable]
		Success,
		/// <summary>An unspecified <see cref="T:System.Net.Sockets.Socket" /> error has occurred.</summary>
		// Token: 0x04001EAB RID: 7851
		[global::__DynamicallyInvokable]
		SocketError = -1,
		/// <summary>A blocking <see cref="T:System.Net.Sockets.Socket" /> call was canceled.</summary>
		// Token: 0x04001EAC RID: 7852
		[global::__DynamicallyInvokable]
		Interrupted = 10004,
		/// <summary>An attempt was made to access a <see cref="T:System.Net.Sockets.Socket" /> in a way that is forbidden by its access permissions.</summary>
		// Token: 0x04001EAD RID: 7853
		[global::__DynamicallyInvokable]
		AccessDenied = 10013,
		/// <summary>An invalid pointer address was detected by the underlying socket provider.</summary>
		// Token: 0x04001EAE RID: 7854
		[global::__DynamicallyInvokable]
		Fault,
		/// <summary>An invalid argument was supplied to a <see cref="T:System.Net.Sockets.Socket" /> member.</summary>
		// Token: 0x04001EAF RID: 7855
		[global::__DynamicallyInvokable]
		InvalidArgument = 10022,
		/// <summary>There are too many open sockets in the underlying socket provider.</summary>
		// Token: 0x04001EB0 RID: 7856
		[global::__DynamicallyInvokable]
		TooManyOpenSockets = 10024,
		/// <summary>An operation on a nonblocking socket cannot be completed immediately.</summary>
		// Token: 0x04001EB1 RID: 7857
		[global::__DynamicallyInvokable]
		WouldBlock = 10035,
		/// <summary>A blocking operation is in progress.</summary>
		// Token: 0x04001EB2 RID: 7858
		[global::__DynamicallyInvokable]
		InProgress,
		/// <summary>The nonblocking <see cref="T:System.Net.Sockets.Socket" /> already has an operation in progress.</summary>
		// Token: 0x04001EB3 RID: 7859
		[global::__DynamicallyInvokable]
		AlreadyInProgress,
		/// <summary>A <see cref="T:System.Net.Sockets.Socket" /> operation was attempted on a non-socket.</summary>
		// Token: 0x04001EB4 RID: 7860
		[global::__DynamicallyInvokable]
		NotSocket,
		/// <summary>A required address was omitted from an operation on a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04001EB5 RID: 7861
		[global::__DynamicallyInvokable]
		DestinationAddressRequired,
		/// <summary>The datagram is too long.</summary>
		// Token: 0x04001EB6 RID: 7862
		[global::__DynamicallyInvokable]
		MessageSize,
		/// <summary>The protocol type is incorrect for this <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04001EB7 RID: 7863
		[global::__DynamicallyInvokable]
		ProtocolType,
		/// <summary>An unknown, invalid, or unsupported option or level was used with a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04001EB8 RID: 7864
		[global::__DynamicallyInvokable]
		ProtocolOption,
		/// <summary>The protocol is not implemented or has not been configured.</summary>
		// Token: 0x04001EB9 RID: 7865
		[global::__DynamicallyInvokable]
		ProtocolNotSupported,
		/// <summary>The support for the specified socket type does not exist in this address family.</summary>
		// Token: 0x04001EBA RID: 7866
		[global::__DynamicallyInvokable]
		SocketNotSupported,
		/// <summary>The address family is not supported by the protocol family.</summary>
		// Token: 0x04001EBB RID: 7867
		[global::__DynamicallyInvokable]
		OperationNotSupported,
		/// <summary>The protocol family is not implemented or has not been configured.</summary>
		// Token: 0x04001EBC RID: 7868
		[global::__DynamicallyInvokable]
		ProtocolFamilyNotSupported,
		/// <summary>The address family specified is not supported. This error is returned if the IPv6 address family was specified and the IPv6 stack is not installed on the local machine. This error is returned if the IPv4 address family was specified and the IPv4 stack is not installed on the local machine.</summary>
		// Token: 0x04001EBD RID: 7869
		[global::__DynamicallyInvokable]
		AddressFamilyNotSupported,
		/// <summary>Only one use of an address is normally permitted.</summary>
		// Token: 0x04001EBE RID: 7870
		[global::__DynamicallyInvokable]
		AddressAlreadyInUse,
		/// <summary>The selected IP address is not valid in this context.</summary>
		// Token: 0x04001EBF RID: 7871
		[global::__DynamicallyInvokable]
		AddressNotAvailable,
		/// <summary>The network is not available.</summary>
		// Token: 0x04001EC0 RID: 7872
		[global::__DynamicallyInvokable]
		NetworkDown,
		/// <summary>No route to the remote host exists.</summary>
		// Token: 0x04001EC1 RID: 7873
		[global::__DynamicallyInvokable]
		NetworkUnreachable,
		/// <summary>The application tried to set <see cref="F:System.Net.Sockets.SocketOptionName.KeepAlive" /> on a connection that has already timed out.</summary>
		// Token: 0x04001EC2 RID: 7874
		[global::__DynamicallyInvokable]
		NetworkReset,
		/// <summary>The connection was aborted by the .NET Framework or the underlying socket provider.</summary>
		// Token: 0x04001EC3 RID: 7875
		[global::__DynamicallyInvokable]
		ConnectionAborted,
		/// <summary>The connection was reset by the remote peer.</summary>
		// Token: 0x04001EC4 RID: 7876
		[global::__DynamicallyInvokable]
		ConnectionReset,
		/// <summary>No free buffer space is available for a <see cref="T:System.Net.Sockets.Socket" /> operation.</summary>
		// Token: 0x04001EC5 RID: 7877
		[global::__DynamicallyInvokable]
		NoBufferSpaceAvailable,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is already connected.</summary>
		// Token: 0x04001EC6 RID: 7878
		[global::__DynamicallyInvokable]
		IsConnected,
		/// <summary>The application tried to send or receive data, and the <see cref="T:System.Net.Sockets.Socket" /> is not connected.</summary>
		// Token: 0x04001EC7 RID: 7879
		[global::__DynamicallyInvokable]
		NotConnected,
		/// <summary>A request to send or receive data was disallowed because the <see cref="T:System.Net.Sockets.Socket" /> has already been closed.</summary>
		// Token: 0x04001EC8 RID: 7880
		[global::__DynamicallyInvokable]
		Shutdown,
		/// <summary>The connection attempt timed out, or the connected host has failed to respond.</summary>
		// Token: 0x04001EC9 RID: 7881
		[global::__DynamicallyInvokable]
		TimedOut = 10060,
		/// <summary>The remote host is actively refusing a connection.</summary>
		// Token: 0x04001ECA RID: 7882
		[global::__DynamicallyInvokable]
		ConnectionRefused,
		/// <summary>The operation failed because the remote host is down.</summary>
		// Token: 0x04001ECB RID: 7883
		[global::__DynamicallyInvokable]
		HostDown = 10064,
		/// <summary>There is no network route to the specified host.</summary>
		// Token: 0x04001ECC RID: 7884
		[global::__DynamicallyInvokable]
		HostUnreachable,
		/// <summary>Too many processes are using the underlying socket provider.</summary>
		// Token: 0x04001ECD RID: 7885
		[global::__DynamicallyInvokable]
		ProcessLimit = 10067,
		/// <summary>The network subsystem is unavailable.</summary>
		// Token: 0x04001ECE RID: 7886
		[global::__DynamicallyInvokable]
		SystemNotReady = 10091,
		/// <summary>The version of the underlying socket provider is out of range.</summary>
		// Token: 0x04001ECF RID: 7887
		[global::__DynamicallyInvokable]
		VersionNotSupported,
		/// <summary>The underlying socket provider has not been initialized.</summary>
		// Token: 0x04001ED0 RID: 7888
		[global::__DynamicallyInvokable]
		NotInitialized,
		/// <summary>A graceful shutdown is in progress.</summary>
		// Token: 0x04001ED1 RID: 7889
		[global::__DynamicallyInvokable]
		Disconnecting = 10101,
		/// <summary>The specified class was not found.</summary>
		// Token: 0x04001ED2 RID: 7890
		[global::__DynamicallyInvokable]
		TypeNotFound = 10109,
		/// <summary>No such host is known. The name is not an official host name or alias.</summary>
		// Token: 0x04001ED3 RID: 7891
		[global::__DynamicallyInvokable]
		HostNotFound = 11001,
		/// <summary>The name of the host could not be resolved. Try again later.</summary>
		// Token: 0x04001ED4 RID: 7892
		[global::__DynamicallyInvokable]
		TryAgain,
		/// <summary>The error is unrecoverable or the requested database cannot be located.</summary>
		// Token: 0x04001ED5 RID: 7893
		[global::__DynamicallyInvokable]
		NoRecovery,
		/// <summary>The requested name or IP address was not found on the name server.</summary>
		// Token: 0x04001ED6 RID: 7894
		[global::__DynamicallyInvokable]
		NoData,
		/// <summary>The application has initiated an overlapped operation that cannot be completed immediately.</summary>
		// Token: 0x04001ED7 RID: 7895
		[global::__DynamicallyInvokable]
		IOPending = 997,
		/// <summary>The overlapped operation was aborted due to the closure of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04001ED8 RID: 7896
		[global::__DynamicallyInvokable]
		OperationAborted = 995
	}
}

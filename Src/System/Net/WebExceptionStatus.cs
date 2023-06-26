using System;

namespace System.Net
{
	/// <summary>Defines status codes for the <see cref="T:System.Net.WebException" /> class.</summary>
	// Token: 0x0200017F RID: 383
	[global::__DynamicallyInvokable]
	public enum WebExceptionStatus
	{
		/// <summary>No error was encountered.</summary>
		// Token: 0x0400121D RID: 4637
		[global::__DynamicallyInvokable]
		Success,
		/// <summary>The name resolver service could not resolve the host name.</summary>
		// Token: 0x0400121E RID: 4638
		NameResolutionFailure,
		/// <summary>The remote service point could not be contacted at the transport level.</summary>
		// Token: 0x0400121F RID: 4639
		[global::__DynamicallyInvokable]
		ConnectFailure,
		/// <summary>A complete response was not received from the remote server.</summary>
		// Token: 0x04001220 RID: 4640
		ReceiveFailure,
		/// <summary>A complete request could not be sent to the remote server.</summary>
		// Token: 0x04001221 RID: 4641
		[global::__DynamicallyInvokable]
		SendFailure,
		/// <summary>The request was a pipelined request and the connection was closed before the response was received.</summary>
		// Token: 0x04001222 RID: 4642
		PipelineFailure,
		/// <summary>The request was canceled, the <see cref="M:System.Net.WebRequest.Abort" /> method was called, or an unclassifiable error occurred. This is the default value for <see cref="P:System.Net.WebException.Status" />.</summary>
		// Token: 0x04001223 RID: 4643
		[global::__DynamicallyInvokable]
		RequestCanceled,
		/// <summary>The response received from the server was complete but indicated a protocol-level error. For example, an HTTP protocol error such as 401 Access Denied would use this status.</summary>
		// Token: 0x04001224 RID: 4644
		ProtocolError,
		/// <summary>The connection was prematurely closed.</summary>
		// Token: 0x04001225 RID: 4645
		ConnectionClosed,
		/// <summary>A server certificate could not be validated.</summary>
		// Token: 0x04001226 RID: 4646
		TrustFailure,
		/// <summary>An error occurred while establishing a connection using SSL.</summary>
		// Token: 0x04001227 RID: 4647
		SecureChannelFailure,
		/// <summary>The server response was not a valid HTTP response.</summary>
		// Token: 0x04001228 RID: 4648
		ServerProtocolViolation,
		/// <summary>The connection for a request that specifies the Keep-alive header was closed unexpectedly.</summary>
		// Token: 0x04001229 RID: 4649
		KeepAliveFailure,
		/// <summary>An internal asynchronous request is pending.</summary>
		// Token: 0x0400122A RID: 4650
		[global::__DynamicallyInvokable]
		Pending,
		/// <summary>No response was received during the time-out period for a request.</summary>
		// Token: 0x0400122B RID: 4651
		Timeout,
		/// <summary>The name resolver service could not resolve the proxy host name.</summary>
		// Token: 0x0400122C RID: 4652
		ProxyNameResolutionFailure,
		/// <summary>An exception of unknown type has occurred.</summary>
		// Token: 0x0400122D RID: 4653
		[global::__DynamicallyInvokable]
		UnknownError,
		/// <summary>A message was received that exceeded the specified limit when sending a request or receiving a response from the server.</summary>
		// Token: 0x0400122E RID: 4654
		[global::__DynamicallyInvokable]
		MessageLengthLimitExceeded,
		/// <summary>The specified cache entry was not found.</summary>
		// Token: 0x0400122F RID: 4655
		CacheEntryNotFound,
		/// <summary>The request was not permitted by the cache policy. In general, this occurs when a request is not cacheable and the effective policy prohibits sending the request to the server. You might receive this status if a request method implies the presence of a request body, a request method requires direct interaction with the server, or a request contains a conditional header.</summary>
		// Token: 0x04001230 RID: 4656
		RequestProhibitedByCachePolicy,
		/// <summary>This request was not permitted by the proxy.</summary>
		// Token: 0x04001231 RID: 4657
		RequestProhibitedByProxy
	}
}

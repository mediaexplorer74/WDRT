using System;

namespace System.Net.WebSockets
{
	/// <summary>Represents well known WebSocket close codes as defined in section 11.7 of the WebSocket protocol spec.</summary>
	// Token: 0x02000232 RID: 562
	public enum WebSocketCloseStatus
	{
		/// <summary>(1000) The connection has closed after the request was fulfilled.</summary>
		// Token: 0x04001675 RID: 5749
		NormalClosure = 1000,
		/// <summary>(1001) Indicates an endpoint is being removed. Either the server or client will become unavailable.</summary>
		// Token: 0x04001676 RID: 5750
		EndpointUnavailable,
		/// <summary>(1002) The client or server is terminating the connection because of a protocol error.</summary>
		// Token: 0x04001677 RID: 5751
		ProtocolError,
		/// <summary>(1003) The client or server is terminating the connection because it cannot accept the data type it received.</summary>
		// Token: 0x04001678 RID: 5752
		InvalidMessageType,
		/// <summary>No error specified.</summary>
		// Token: 0x04001679 RID: 5753
		Empty = 1005,
		/// <summary>(1007) The client or server is terminating the connection because it has received data inconsistent with the message type.</summary>
		// Token: 0x0400167A RID: 5754
		InvalidPayloadData = 1007,
		/// <summary>(1008) The connection will be closed because an endpoint has received a message that violates its policy.</summary>
		// Token: 0x0400167B RID: 5755
		PolicyViolation,
		/// <summary>(1004) Reserved for future use.</summary>
		// Token: 0x0400167C RID: 5756
		MessageTooBig,
		/// <summary>(1010) The client is terminating the connection because it expected the server to negotiate an extension.</summary>
		// Token: 0x0400167D RID: 5757
		MandatoryExtension,
		/// <summary>The connection will be closed by the server because of an error on the server.</summary>
		// Token: 0x0400167E RID: 5758
		InternalServerError
	}
}

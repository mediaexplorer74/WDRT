using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides methods used for security and transport sinks.</summary>
	// Token: 0x02000845 RID: 2117
	[ComVisible(true)]
	public interface IServerChannelSink : IChannelSinkBase
	{
		/// <summary>Requests message processing from the current sink.</summary>
		/// <param name="sinkStack">A stack of channel sinks that called the current sink.</param>
		/// <param name="requestMsg">The message that contains the request.</param>
		/// <param name="requestHeaders">Headers retrieved from the incoming message from the client.</param>
		/// <param name="requestStream">The stream that needs to be to processed and passed on to the deserialization sink.</param>
		/// <param name="responseMsg">When this method returns, contains a <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> that holds the response message. This parameter is passed uninitialized.</param>
		/// <param name="responseHeaders">When this method returns, contains a <see cref="T:System.Runtime.Remoting.Channels.ITransportHeaders" /> that holds the headers that are to be added to return message heading to the client. This parameter is passed uninitialized.</param>
		/// <param name="responseStream">When this method returns, contains a <see cref="T:System.IO.Stream" /> that is heading back to the transport sink. This parameter is passed uninitialized.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Channels.ServerProcessing" /> status value that provides information about how message was processed.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005A36 RID: 23094
		[SecurityCritical]
		ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream);

		/// <summary>Requests processing from the current sink of the response from a method call sent asynchronously.</summary>
		/// <param name="sinkStack">A stack of sinks leading back to the server transport sink.</param>
		/// <param name="state">Information generated on the request side that is associated with this sink.</param>
		/// <param name="msg">The response message.</param>
		/// <param name="headers">The headers to add to the return message heading to the client.</param>
		/// <param name="stream">The stream heading back to the transport sink.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005A37 RID: 23095
		[SecurityCritical]
		void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream);

		/// <summary>Returns the <see cref="T:System.IO.Stream" /> onto which the provided response message is to be serialized.</summary>
		/// <param name="sinkStack">A stack of sinks leading back to the server transport sink.</param>
		/// <param name="state">The state that has been pushed to the stack by this sink.</param>
		/// <param name="msg">The response message to serialize.</param>
		/// <param name="headers">The headers to put in the response stream to the client.</param>
		/// <returns>The <see cref="T:System.IO.Stream" /> onto which the provided response message is to be serialized.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005A38 RID: 23096
		[SecurityCritical]
		Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers);

		/// <summary>Gets the next server channel sink in the server sink chain.</summary>
		/// <returns>The next server channel sink in the server sink chain.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have the required <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Infrastructure" /> permission.</exception>
		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06005A39 RID: 23097
		IServerChannelSink NextChannelSink
		{
			[SecurityCritical]
			get;
		}
	}
}

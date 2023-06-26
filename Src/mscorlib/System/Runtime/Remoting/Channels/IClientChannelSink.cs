using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides required functions and properties for client channel sinks.</summary>
	// Token: 0x02000843 RID: 2115
	[ComVisible(true)]
	public interface IClientChannelSink : IChannelSinkBase
	{
		/// <summary>Requests message processing from the current sink.</summary>
		/// <param name="msg">The message to process.</param>
		/// <param name="requestHeaders">The headers to add to the outgoing message heading to the server.</param>
		/// <param name="requestStream">The stream headed to the transport sink.</param>
		/// <param name="responseHeaders">When this method returns, contains a <see cref="T:System.Runtime.Remoting.Channels.ITransportHeaders" /> interface that holds the headers that the server returned. This parameter is passed uninitialized.</param>
		/// <param name="responseStream">When this method returns, contains a <see cref="T:System.IO.Stream" /> coming back from the transport sink. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005A31 RID: 23089
		[SecurityCritical]
		void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream);

		/// <summary>Requests asynchronous processing of a method call on the current sink.</summary>
		/// <param name="sinkStack">A stack of channel sinks that called this sink.</param>
		/// <param name="msg">The message to process.</param>
		/// <param name="headers">The headers to add to the outgoing message heading to the server.</param>
		/// <param name="stream">The stream headed to the transport sink.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005A32 RID: 23090
		[SecurityCritical]
		void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream);

		/// <summary>Requests asynchronous processing of a response to a method call on the current sink.</summary>
		/// <param name="sinkStack">A stack of sinks that called this sink.</param>
		/// <param name="state">Information generated on the request side that is associated with this sink.</param>
		/// <param name="headers">The headers retrieved from the server response stream.</param>
		/// <param name="stream">The stream coming back from the transport sink.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005A33 RID: 23091
		[SecurityCritical]
		void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream);

		/// <summary>Returns the <see cref="T:System.IO.Stream" /> onto which the provided message is to be serialized.</summary>
		/// <param name="msg">The <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> containing details about the method call.</param>
		/// <param name="headers">The headers to add to the outgoing message heading to the server.</param>
		/// <returns>The <see cref="T:System.IO.Stream" /> onto which the provided message is to be serialized.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005A34 RID: 23092
		[SecurityCritical]
		Stream GetRequestStream(IMessage msg, ITransportHeaders headers);

		/// <summary>Gets the next client channel sink in the client sink chain.</summary>
		/// <returns>The next client channel sink in the client sink chain.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06005A35 RID: 23093
		IClientChannelSink NextChannelSink
		{
			[SecurityCritical]
			get;
		}
	}
}

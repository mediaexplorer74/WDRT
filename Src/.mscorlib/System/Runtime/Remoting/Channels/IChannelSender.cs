using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides required functions and properties for the sender channels.</summary>
	// Token: 0x0200083C RID: 2108
	[ComVisible(true)]
	public interface IChannelSender : IChannel
	{
		/// <summary>Returns a channel message sink that delivers messages to the specified URL or channel data object.</summary>
		/// <param name="url">The URL to which the new sink will deliver messages. Can be <see langword="null" />.</param>
		/// <param name="remoteChannelData">The channel data object of the remote host to which the new sink will deliver messages. Can be <see langword="null" />.</param>
		/// <param name="objectURI">When this method returns, contains a URI of the new channel message sink that delivers messages to the specified URL or channel data object. This parameter is passed uninitialized.</param>
		/// <returns>A channel message sink that delivers messages to the specified URL or channel data object, or <see langword="null" /> if the channel cannot connect to the given endpoint.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005A21 RID: 23073
		[SecurityCritical]
		IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI);
	}
}

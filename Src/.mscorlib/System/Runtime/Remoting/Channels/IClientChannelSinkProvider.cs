using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Creates client channel sinks for the client channel through which remoting messages flow.</summary>
	// Token: 0x0200083F RID: 2111
	[ComVisible(true)]
	public interface IClientChannelSinkProvider
	{
		/// <summary>Creates a sink chain.</summary>
		/// <param name="channel">Channel for which the current sink chain is being constructed.</param>
		/// <param name="url">The URL of the object to connect to. This parameter can be <see langword="null" /> if the connection is based entirely on the information contained in the <paramref name="remoteChannelData" /> parameter.</param>
		/// <param name="remoteChannelData">A channel data object that describes a channel on the remote server.</param>
		/// <returns>The first sink of the newly formed channel sink chain, or <see langword="null" />, which indicates that this provider will not or cannot provide a connection for this endpoint.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005A2A RID: 23082
		[SecurityCritical]
		IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData);

		/// <summary>Gets or sets the next sink provider in the channel sink provider chain.</summary>
		/// <returns>The next sink provider in the channel sink provider chain.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06005A2B RID: 23083
		// (set) Token: 0x06005A2C RID: 23084
		IClientChannelSinkProvider Next
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}

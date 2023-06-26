using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides the stack functionality for a stack of server channel sinks.</summary>
	// Token: 0x0200082E RID: 2094
	[ComVisible(true)]
	public interface IServerChannelSinkStack : IServerResponseChannelSinkStack
	{
		/// <summary>Pushes the specified sink and information associated with it onto the sink stack.</summary>
		/// <param name="sink">The sink to push onto the sink stack.</param>
		/// <param name="state">Information generated on the request side that is needed on the response side.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060059C3 RID: 22979
		[SecurityCritical]
		void Push(IServerChannelSink sink, object state);

		/// <summary>Pops the information associated with all the sinks from the sink stack up to and including the specified sink.</summary>
		/// <param name="sink">The sink to remove and return from the sink stack.</param>
		/// <returns>Information generated on the request side and associated with the specified sink.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060059C4 RID: 22980
		[SecurityCritical]
		object Pop(IServerChannelSink sink);

		/// <summary>Stores a message sink and its associated state for later asynchronous processing.</summary>
		/// <param name="sink">A server channel sink.</param>
		/// <param name="state">The state associated with <paramref name="sink" />.</param>
		// Token: 0x060059C5 RID: 22981
		[SecurityCritical]
		void Store(IServerChannelSink sink, object state);

		/// <summary>Stores a message sink and its associated state, and then dispatches a message asynchronously, using the sink just stored and any other stored sinks.</summary>
		/// <param name="sink">A server channel sink.</param>
		/// <param name="state">The state associated with <paramref name="sink" />.</param>
		// Token: 0x060059C6 RID: 22982
		[SecurityCritical]
		void StoreAndDispatch(IServerChannelSink sink, object state);

		/// <summary>Presents a callback delegate to handle a callback after a message has been dispatched asynchronously.</summary>
		/// <param name="ar">The status and state of an asynchronous operation on a remote object.</param>
		// Token: 0x060059C7 RID: 22983
		[SecurityCritical]
		void ServerCallback(IAsyncResult ar);
	}
}

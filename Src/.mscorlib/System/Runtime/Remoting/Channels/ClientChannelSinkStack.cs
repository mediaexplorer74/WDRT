using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Holds the stack of client channel sinks that must be invoked during an asynchronous message response decoding.</summary>
	// Token: 0x0200082D RID: 2093
	[SecurityCritical]
	[ComVisible(true)]
	public class ClientChannelSinkStack : IClientChannelSinkStack, IClientResponseChannelSinkStack
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.ClientChannelSinkStack" /> class with default values.</summary>
		// Token: 0x060059BC RID: 22972 RVA: 0x0013D80D File Offset: 0x0013BA0D
		public ClientChannelSinkStack()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.ClientChannelSinkStack" /> class with the specified reply sink.</summary>
		/// <param name="replySink">The <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> that the current stack can use to reply to messages.</param>
		// Token: 0x060059BD RID: 22973 RVA: 0x0013D815 File Offset: 0x0013BA15
		public ClientChannelSinkStack(IMessageSink replySink)
		{
			this._replySink = replySink;
		}

		/// <summary>Pushes the specified sink and information that is associated with it onto the sink stack.</summary>
		/// <param name="sink">The sink to push onto the sink stack.</param>
		/// <param name="state">Information generated on the request side that is needed on the response side.</param>
		// Token: 0x060059BE RID: 22974 RVA: 0x0013D824 File Offset: 0x0013BA24
		[SecurityCritical]
		public void Push(IClientChannelSink sink, object state)
		{
			this._stack = new ClientChannelSinkStack.SinkStack
			{
				PrevStack = this._stack,
				Sink = sink,
				State = state
			};
		}

		/// <summary>Pops the information that is associated with all the sinks from the sink stack up to and including the specified sink.</summary>
		/// <param name="sink">The sink to remove and return from the sink stack.</param>
		/// <returns>Information generated on the request side and associated with the specified sink.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty, or the specified sink was never pushed onto the current stack.</exception>
		// Token: 0x060059BF RID: 22975 RVA: 0x0013D858 File Offset: 0x0013BA58
		[SecurityCritical]
		public object Pop(IClientChannelSink sink)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopOnEmptySinkStack"));
			}
			while (this._stack.Sink != sink)
			{
				this._stack = this._stack.PrevStack;
				if (this._stack == null)
				{
					break;
				}
			}
			if (this._stack.Sink == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopFromSinkStackWithoutPush"));
			}
			object state = this._stack.State;
			this._stack = this._stack.PrevStack;
			return state;
		}

		/// <summary>Requests asynchronous processing of a method call on the sinks that are in the current sink stack.</summary>
		/// <param name="headers">The headers that are retrieved from the server response stream.</param>
		/// <param name="stream">The stream that is returning from the transport sink.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty.</exception>
		// Token: 0x060059C0 RID: 22976 RVA: 0x0013D8E0 File Offset: 0x0013BAE0
		[SecurityCritical]
		public void AsyncProcessResponse(ITransportHeaders headers, Stream stream)
		{
			if (this._replySink != null)
			{
				if (this._stack == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallAPRWhenStackEmpty"));
				}
				IClientChannelSink sink = this._stack.Sink;
				object state = this._stack.State;
				this._stack = this._stack.PrevStack;
				sink.AsyncProcessResponse(this, state, headers, stream);
			}
		}

		/// <summary>Dispatches the specified reply message on the reply sink.</summary>
		/// <param name="msg">The <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> to dispatch.</param>
		// Token: 0x060059C1 RID: 22977 RVA: 0x0013D940 File Offset: 0x0013BB40
		[SecurityCritical]
		public void DispatchReplyMessage(IMessage msg)
		{
			if (this._replySink != null)
			{
				this._replySink.SyncProcessMessage(msg);
			}
		}

		/// <summary>Dispatches the specified exception on the reply sink.</summary>
		/// <param name="e">The exception to dispatch to the server.</param>
		// Token: 0x060059C2 RID: 22978 RVA: 0x0013D957 File Offset: 0x0013BB57
		[SecurityCritical]
		public void DispatchException(Exception e)
		{
			this.DispatchReplyMessage(new ReturnMessage(e, null));
		}

		// Token: 0x040028DB RID: 10459
		private ClientChannelSinkStack.SinkStack _stack;

		// Token: 0x040028DC RID: 10460
		private IMessageSink _replySink;

		// Token: 0x02000C71 RID: 3185
		private class SinkStack
		{
			// Token: 0x040037FE RID: 14334
			public ClientChannelSinkStack.SinkStack PrevStack;

			// Token: 0x040037FF RID: 14335
			public IClientChannelSink Sink;

			// Token: 0x04003800 RID: 14336
			public object State;
		}
	}
}

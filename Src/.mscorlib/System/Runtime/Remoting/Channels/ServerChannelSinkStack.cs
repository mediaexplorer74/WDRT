using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Holds the stack of server channel sinks.</summary>
	// Token: 0x02000830 RID: 2096
	[SecurityCritical]
	[ComVisible(true)]
	public class ServerChannelSinkStack : IServerChannelSinkStack, IServerResponseChannelSinkStack
	{
		/// <summary>Pushes the specified sink and information associated with it onto the sink stack.</summary>
		/// <param name="sink">The sink to push onto the sink stack.</param>
		/// <param name="state">Information generated on the request side that is needed on the response side.</param>
		// Token: 0x060059CA RID: 22986 RVA: 0x0013D968 File Offset: 0x0013BB68
		[SecurityCritical]
		public void Push(IServerChannelSink sink, object state)
		{
			this._stack = new ServerChannelSinkStack.SinkStack
			{
				PrevStack = this._stack,
				Sink = sink,
				State = state
			};
		}

		/// <summary>Pops the information associated with all the sinks from the sink stack up to and including the specified sink.</summary>
		/// <param name="sink">The sink to remove and return from the sink stack.</param>
		/// <returns>Information generated on the request side and associated with the specified sink.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty, or the specified sink was never pushed onto the current stack.</exception>
		// Token: 0x060059CB RID: 22987 RVA: 0x0013D99C File Offset: 0x0013BB9C
		[SecurityCritical]
		public object Pop(IServerChannelSink sink)
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

		/// <summary>Stores a message sink and its associated state for later asynchronous processing.</summary>
		/// <param name="sink">A server channel sink.</param>
		/// <param name="state">The state associated with <paramref name="sink" />.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty.  
		///  -or-  
		///  The specified sink was never pushed onto the current stack.</exception>
		// Token: 0x060059CC RID: 22988 RVA: 0x0013DA24 File Offset: 0x0013BC24
		[SecurityCritical]
		public void Store(IServerChannelSink sink, object state)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_StoreOnEmptySinkStack"));
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
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_StoreOnSinkStackWithoutPush"));
			}
			this._rememberedStack = new ServerChannelSinkStack.SinkStack
			{
				PrevStack = this._rememberedStack,
				Sink = sink,
				State = state
			};
			this.Pop(sink);
		}

		/// <summary>Stores a message sink and its associated state, and then dispatches a message asynchronously, using the sink just stored and any other stored sinks.</summary>
		/// <param name="sink">A server channel sink.</param>
		/// <param name="state">The state associated with <paramref name="sink" />.</param>
		// Token: 0x060059CD RID: 22989 RVA: 0x0013DABC File Offset: 0x0013BCBC
		[SecurityCritical]
		public void StoreAndDispatch(IServerChannelSink sink, object state)
		{
			this.Store(sink, state);
			this.FlipRememberedStack();
			CrossContextChannel.DoAsyncDispatch(this._asyncMsg, null);
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x0013DADC File Offset: 0x0013BCDC
		private void FlipRememberedStack()
		{
			if (this._stack != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallFRSWhenStackEmtpy"));
			}
			while (this._rememberedStack != null)
			{
				this._stack = new ServerChannelSinkStack.SinkStack
				{
					PrevStack = this._stack,
					Sink = this._rememberedStack.Sink,
					State = this._rememberedStack.State
				};
				this._rememberedStack = this._rememberedStack.PrevStack;
			}
		}

		/// <summary>Requests asynchronous processing of a method call on the sinks in the current sink stack.</summary>
		/// <param name="msg">The message to be serialized onto the requested stream.</param>
		/// <param name="headers">The headers retrieved from the server response stream.</param>
		/// <param name="stream">The stream coming back from the transport sink.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty.</exception>
		// Token: 0x060059CF RID: 22991 RVA: 0x0013DB58 File Offset: 0x0013BD58
		[SecurityCritical]
		public void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallAPRWhenStackEmpty"));
			}
			IServerChannelSink sink = this._stack.Sink;
			object state = this._stack.State;
			this._stack = this._stack.PrevStack;
			sink.AsyncProcessResponse(this, state, msg, headers, stream);
		}

		/// <summary>Returns the <see cref="T:System.IO.Stream" /> onto which the specified message is to be serialized.</summary>
		/// <param name="msg">The message to be serialized onto the requested stream.</param>
		/// <param name="headers">The headers retrieved from the server response stream.</param>
		/// <returns>The <see cref="T:System.IO.Stream" /> onto which the specified message is to be serialized.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The sink stack is empty.</exception>
		// Token: 0x060059D0 RID: 22992 RVA: 0x0013DBB4 File Offset: 0x0013BDB4
		[SecurityCritical]
		public Stream GetResponseStream(IMessage msg, ITransportHeaders headers)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallGetResponseStreamWhenStackEmpty"));
			}
			IServerChannelSink sink = this._stack.Sink;
			object state = this._stack.State;
			this._stack = this._stack.PrevStack;
			Stream responseStream = sink.GetResponseStream(this, state, msg, headers);
			this.Push(sink, state);
			return responseStream;
		}

		// Token: 0x17000EDE RID: 3806
		// (set) Token: 0x060059D1 RID: 22993 RVA: 0x0013DC16 File Offset: 0x0013BE16
		internal object ServerObject
		{
			set
			{
				this._serverObject = value;
			}
		}

		/// <summary>Provides a <see cref="T:System.AsyncCallback" /> delegate to handle a callback after a message has been dispatched asynchronously.</summary>
		/// <param name="ar">The status and state of an asynchronous operation on a remote object.</param>
		// Token: 0x060059D2 RID: 22994 RVA: 0x0013DC20 File Offset: 0x0013BE20
		[SecurityCritical]
		public void ServerCallback(IAsyncResult ar)
		{
			if (this._asyncEnd != null)
			{
				RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this._asyncEnd);
				MethodInfo methodInfo = (MethodInfo)this._msg.MethodBase;
				RemotingMethodCachedData reflectionCachedData2 = InternalRemotingServices.GetReflectionCachedData(methodInfo);
				ParameterInfo[] parameters = reflectionCachedData.Parameters;
				object[] array = new object[parameters.Length];
				array[parameters.Length - 1] = ar;
				object[] args = this._msg.Args;
				AsyncMessageHelper.GetOutArgs(reflectionCachedData2.Parameters, args, array);
				StackBuilderSink stackBuilderSink = new StackBuilderSink(this._serverObject);
				object[] array2;
				object obj = stackBuilderSink.PrivateProcessMessage(this._asyncEnd.MethodHandle, Message.CoerceArgs(this._asyncEnd, array, parameters), this._serverObject, out array2);
				if (array2 != null)
				{
					array2 = ArgMapper.ExpandAsyncEndArgsToSyncArgs(reflectionCachedData2, array2);
				}
				stackBuilderSink.CopyNonByrefOutArgsFromOriginalArgs(reflectionCachedData2, args, ref array2);
				IMessage message = new ReturnMessage(obj, array2, this._msg.ArgCount, Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext, this._msg);
				this.AsyncProcessResponse(message, null, null);
			}
		}

		// Token: 0x040028DD RID: 10461
		private ServerChannelSinkStack.SinkStack _stack;

		// Token: 0x040028DE RID: 10462
		private ServerChannelSinkStack.SinkStack _rememberedStack;

		// Token: 0x040028DF RID: 10463
		private IMessage _asyncMsg;

		// Token: 0x040028E0 RID: 10464
		private MethodInfo _asyncEnd;

		// Token: 0x040028E1 RID: 10465
		private object _serverObject;

		// Token: 0x040028E2 RID: 10466
		private IMethodCallMessage _msg;

		// Token: 0x02000C72 RID: 3186
		private class SinkStack
		{
			// Token: 0x04003801 RID: 14337
			public ServerChannelSinkStack.SinkStack PrevStack;

			// Token: 0x04003802 RID: 14338
			public IServerChannelSink Sink;

			// Token: 0x04003803 RID: 14339
			public object State;
		}
	}
}

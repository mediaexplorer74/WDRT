using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000832 RID: 2098
	internal class CrossContextChannel : InternalSink, IMessageSink
	{
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x060059D6 RID: 22998 RVA: 0x0013DD85 File Offset: 0x0013BF85
		// (set) Token: 0x060059D7 RID: 22999 RVA: 0x0013DD9B File Offset: 0x0013BF9B
		private static CrossContextChannel messageSink
		{
			get
			{
				return Thread.GetDomain().RemotingData.ChannelServicesData.xctxmessageSink;
			}
			set
			{
				Thread.GetDomain().RemotingData.ChannelServicesData.xctxmessageSink = value;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x060059D8 RID: 23000 RVA: 0x0013DDB4 File Offset: 0x0013BFB4
		internal static IMessageSink MessageSink
		{
			get
			{
				if (CrossContextChannel.messageSink == null)
				{
					CrossContextChannel crossContextChannel = new CrossContextChannel();
					object obj = CrossContextChannel.staticSyncObject;
					lock (obj)
					{
						if (CrossContextChannel.messageSink == null)
						{
							CrossContextChannel.messageSink = crossContextChannel;
						}
					}
				}
				return CrossContextChannel.messageSink;
			}
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x0013DE0C File Offset: 0x0013C00C
		[SecurityCritical]
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessage message = args[0] as IMessage;
			Context context = args[1] as Context;
			if (RemotingServices.CORProfilerTrackRemoting())
			{
				Guid guid = Guid.Empty;
				if (RemotingServices.CORProfilerTrackRemotingCookie())
				{
					object obj = message.Properties["CORProfilerCookie"];
					if (obj != null)
					{
						guid = (Guid)obj;
					}
				}
				RemotingServices.CORProfilerRemotingServerReceivingMessage(guid, false);
			}
			context.NotifyDynamicSinks(message, false, true, false, true);
			IMessage message2 = context.GetServerContextChain().SyncProcessMessage(message);
			context.NotifyDynamicSinks(message2, false, false, false, true);
			if (RemotingServices.CORProfilerTrackRemoting())
			{
				Guid guid2;
				RemotingServices.CORProfilerRemotingServerSendingReply(out guid2, false);
				if (RemotingServices.CORProfilerTrackRemotingCookie())
				{
					message2.Properties["CORProfilerCookie"] = guid2;
				}
			}
			return message2;
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x0013DEBC File Offset: 0x0013C0BC
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			object[] array = new object[2];
			IMessage message = null;
			try
			{
				IMessage message2 = InternalSink.ValidateMessage(reqMsg);
				if (message2 != null)
				{
					return message2;
				}
				ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
				array[0] = reqMsg;
				array[1] = serverIdentity.ServerContext;
				message = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(serverIdentity.ServerContext, CrossContextChannel.s_xctxDel, array);
			}
			catch (Exception ex)
			{
				message = new ReturnMessage(ex, (IMethodCallMessage)reqMsg);
				if (reqMsg != null)
				{
					((ReturnMessage)message).SetLogicalCallContext((LogicalCallContext)reqMsg.Properties[Message.CallContextKey]);
				}
			}
			return message;
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x0013DF60 File Offset: 0x0013C160
		[SecurityCritical]
		internal static object AsyncProcessMessageCallback(object[] args)
		{
			AsyncWorkItem asyncWorkItem = null;
			IMessage message = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			Context context = (Context)args[2];
			Context context2 = (Context)args[3];
			if (messageSink != null)
			{
				asyncWorkItem = new AsyncWorkItem(messageSink, context);
			}
			context2.NotifyDynamicSinks(message, false, true, true, true);
			return context2.GetServerContextChain().AsyncProcessMessage(message, asyncWorkItem);
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x0013DFC4 File Offset: 0x0013C1C4
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			object[] array = new object[4];
			IMessageCtrl messageCtrl = null;
			if (message != null)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(message);
				}
			}
			else
			{
				ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
				if (RemotingServices.CORProfilerTrackRemotingAsync())
				{
					Guid guid = Guid.Empty;
					if (RemotingServices.CORProfilerTrackRemotingCookie())
					{
						object obj = reqMsg.Properties["CORProfilerCookie"];
						if (obj != null)
						{
							guid = (Guid)obj;
						}
					}
					RemotingServices.CORProfilerRemotingServerReceivingMessage(guid, true);
					if (replySink != null)
					{
						IMessageSink messageSink = new ServerAsyncReplyTerminatorSink(replySink);
						replySink = messageSink;
					}
				}
				Context serverContext = serverIdentity.ServerContext;
				if (serverContext.IsThreadPoolAware)
				{
					array[0] = reqMsg;
					array[1] = replySink;
					array[2] = Thread.CurrentContext;
					array[3] = serverContext;
					InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(CrossContextChannel.AsyncProcessMessageCallback);
					messageCtrl = (IMessageCtrl)Thread.CurrentThread.InternalCrossContextCallback(serverContext, internalCrossContextDelegate, array);
				}
				else
				{
					AsyncWorkItem asyncWorkItem = new AsyncWorkItem(reqMsg, replySink, Thread.CurrentContext, serverIdentity);
					WaitCallback waitCallback = new WaitCallback(asyncWorkItem.FinishAsyncWork);
					ThreadPool.QueueUserWorkItem(waitCallback);
				}
			}
			return messageCtrl;
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x0013E0C0 File Offset: 0x0013C2C0
		[SecurityCritical]
		internal static object DoAsyncDispatchCallback(object[] args)
		{
			AsyncWorkItem asyncWorkItem = null;
			IMessage message = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			Context context = (Context)args[2];
			Context context2 = (Context)args[3];
			if (messageSink != null)
			{
				asyncWorkItem = new AsyncWorkItem(messageSink, context);
			}
			return context2.GetServerContextChain().AsyncProcessMessage(message, asyncWorkItem);
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x0013E114 File Offset: 0x0013C314
		[SecurityCritical]
		internal static IMessageCtrl DoAsyncDispatch(IMessage reqMsg, IMessageSink replySink)
		{
			object[] array = new object[4];
			ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
			if (RemotingServices.CORProfilerTrackRemotingAsync())
			{
				Guid guid = Guid.Empty;
				if (RemotingServices.CORProfilerTrackRemotingCookie())
				{
					object obj = reqMsg.Properties["CORProfilerCookie"];
					if (obj != null)
					{
						guid = (Guid)obj;
					}
				}
				RemotingServices.CORProfilerRemotingServerReceivingMessage(guid, true);
				if (replySink != null)
				{
					IMessageSink messageSink = new ServerAsyncReplyTerminatorSink(replySink);
					replySink = messageSink;
				}
			}
			Context serverContext = serverIdentity.ServerContext;
			array[0] = reqMsg;
			array[1] = replySink;
			array[2] = Thread.CurrentContext;
			array[3] = serverContext;
			InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(CrossContextChannel.DoAsyncDispatchCallback);
			return (IMessageCtrl)Thread.CurrentThread.InternalCrossContextCallback(serverContext, internalCrossContextDelegate, array);
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x060059DF RID: 23007 RVA: 0x0013E1BA File Offset: 0x0013C3BA
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x040028E3 RID: 10467
		private const string _channelName = "XCTX";

		// Token: 0x040028E4 RID: 10468
		private const int _channelCapability = 0;

		// Token: 0x040028E5 RID: 10469
		private const string _channelURI = "XCTX_URI";

		// Token: 0x040028E6 RID: 10470
		private static object staticSyncObject = new object();

		// Token: 0x040028E7 RID: 10471
		private static InternalCrossContextDelegate s_xctxDel = new InternalCrossContextDelegate(CrossContextChannel.SyncProcessMessageCallback);
	}
}

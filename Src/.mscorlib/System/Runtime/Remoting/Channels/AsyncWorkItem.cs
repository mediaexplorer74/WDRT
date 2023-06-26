using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000833 RID: 2099
	internal class AsyncWorkItem : IMessageSink
	{
		// Token: 0x060059E1 RID: 23009 RVA: 0x0013E1C5 File Offset: 0x0013C3C5
		[SecurityCritical]
		internal AsyncWorkItem(IMessageSink replySink, Context oldCtx)
			: this(null, replySink, oldCtx, null)
		{
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x0013E1D1 File Offset: 0x0013C3D1
		[SecurityCritical]
		internal AsyncWorkItem(IMessage reqMsg, IMessageSink replySink, Context oldCtx, ServerIdentity srvID)
		{
			this._reqMsg = reqMsg;
			this._replySink = replySink;
			this._oldCtx = oldCtx;
			this._callCtx = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			this._srvID = srvID;
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x0013E20C File Offset: 0x0013C40C
		[SecurityCritical]
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessageSink messageSink = (IMessageSink)args[0];
			IMessage message = (IMessage)args[1];
			return messageSink.SyncProcessMessage(message);
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x0013E234 File Offset: 0x0013C434
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage message = null;
			if (this._replySink != null)
			{
				Thread.CurrentContext.NotifyDynamicSinks(msg, false, false, true, true);
				object[] array = new object[] { this._replySink, msg };
				InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(AsyncWorkItem.SyncProcessMessageCallback);
				message = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(this._oldCtx, internalCrossContextDelegate, array);
			}
			return message;
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x0013E295 File Offset: 0x0013C495
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x060059E6 RID: 23014 RVA: 0x0013E2A6 File Offset: 0x0013C4A6
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x0013E2B0 File Offset: 0x0013C4B0
		[SecurityCritical]
		internal static object FinishAsyncWorkCallback(object[] args)
		{
			AsyncWorkItem asyncWorkItem = (AsyncWorkItem)args[0];
			Context serverContext = asyncWorkItem._srvID.ServerContext;
			LogicalCallContext logicalCallContext = CallContext.SetLogicalCallContext(asyncWorkItem._callCtx);
			serverContext.NotifyDynamicSinks(asyncWorkItem._reqMsg, false, true, true, true);
			IMessageCtrl messageCtrl = serverContext.GetServerContextChain().AsyncProcessMessage(asyncWorkItem._reqMsg, asyncWorkItem);
			CallContext.SetLogicalCallContext(logicalCallContext);
			return null;
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x0013E30C File Offset: 0x0013C50C
		[SecurityCritical]
		internal virtual void FinishAsyncWork(object stateIgnored)
		{
			InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(AsyncWorkItem.FinishAsyncWorkCallback);
			object[] array = new object[] { this };
			Thread.CurrentThread.InternalCrossContextCallback(this._srvID.ServerContext, internalCrossContextDelegate, array);
		}

		// Token: 0x040028E8 RID: 10472
		private IMessageSink _replySink;

		// Token: 0x040028E9 RID: 10473
		private ServerIdentity _srvID;

		// Token: 0x040028EA RID: 10474
		private Context _oldCtx;

		// Token: 0x040028EB RID: 10475
		[SecurityCritical]
		private LogicalCallContext _callCtx;

		// Token: 0x040028EC RID: 10476
		private IMessage _reqMsg;
	}
}

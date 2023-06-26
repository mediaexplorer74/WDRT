using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Enforces a synchronization domain for the current context and all contexts that share the same instance.</summary>
	// Token: 0x02000817 RID: 2071
	[SecurityCritical]
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class SynchronizationAttribute : ContextAttribute, IContributeServerContextSink, IContributeClientContextSink
	{
		/// <summary>Gets or sets a Boolean value indicating whether the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> implementing this instance of <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> is locked.</summary>
		/// <returns>A Boolean value indicating whether the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> implementing this instance of <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> is locked.</returns>
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060058FA RID: 22778 RVA: 0x0013AA40 File Offset: 0x00138C40
		// (set) Token: 0x060058FB RID: 22779 RVA: 0x0013AA48 File Offset: 0x00138C48
		public virtual bool Locked
		{
			get
			{
				return this._locked;
			}
			set
			{
				this._locked = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether reentry is required.</summary>
		/// <returns>A Boolean value indicating whether reentry is required.</returns>
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x0013AA51 File Offset: 0x00138C51
		public virtual bool IsReEntrant
		{
			get
			{
				return this._bReEntrant;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x060058FD RID: 22781 RVA: 0x0013AA59 File Offset: 0x00138C59
		// (set) Token: 0x060058FE RID: 22782 RVA: 0x0013AA61 File Offset: 0x00138C61
		internal string SyncCallOutLCID
		{
			get
			{
				return this._syncLcid;
			}
			set
			{
				this._syncLcid = value;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x060058FF RID: 22783 RVA: 0x0013AA6A File Offset: 0x00138C6A
		internal ArrayList AsyncCallOutLCIDList
		{
			get
			{
				return this._asyncLcidList;
			}
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x0013AA74 File Offset: 0x00138C74
		internal bool IsKnownLCID(IMessage reqMsg)
		{
			string logicalCallID = ((LogicalCallContext)reqMsg.Properties[Message.CallContextKey]).RemotingData.LogicalCallID;
			return logicalCallID.Equals(this._syncLcid) || this._asyncLcidList.Contains(logicalCallID);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with default values.</summary>
		// Token: 0x06005901 RID: 22785 RVA: 0x0013AABD File Offset: 0x00138CBD
		public SynchronizationAttribute()
			: this(4, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a Boolean value indicating whether reentry is required.</summary>
		/// <param name="reEntrant">A Boolean value indicating whether reentry is required.</param>
		// Token: 0x06005902 RID: 22786 RVA: 0x0013AAC7 File Offset: 0x00138CC7
		public SynchronizationAttribute(bool reEntrant)
			: this(4, reEntrant)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a flag indicating the behavior of the object to which this attribute is applied.</summary>
		/// <param name="flag">An integer value indicating the behavior of the object to which this attribute is applied.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter was not one of the defined flags.</exception>
		// Token: 0x06005903 RID: 22787 RVA: 0x0013AAD1 File Offset: 0x00138CD1
		public SynchronizationAttribute(int flag)
			: this(flag, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a flag indicating the behavior of the object to which this attribute is applied, and a Boolean value indicating whether reentry is required.</summary>
		/// <param name="flag">An integer value indicating the behavior of the object to which this attribute is applied.</param>
		/// <param name="reEntrant">
		///   <see langword="true" /> if reentry is required, and callouts must be intercepted and serialized; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter was not one of the defined flags.</exception>
		// Token: 0x06005904 RID: 22788 RVA: 0x0013AADB File Offset: 0x00138CDB
		public SynchronizationAttribute(int flag, bool reEntrant)
			: base("Synchronization")
		{
			this._bReEntrant = reEntrant;
			if (flag - 1 <= 1 || flag == 4 || flag == 8)
			{
				this._flavor = flag;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "flag");
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x0013AB19 File Offset: 0x00138D19
		internal void Dispose()
		{
			if (this._waitHandle != null)
			{
				this._waitHandle.Unregister(null);
			}
		}

		/// <summary>Returns a Boolean value indicating whether the context parameter meets the context attribute's requirements.</summary>
		/// <param name="ctx">The context to check.</param>
		/// <param name="msg">Information gathered at construction time of the context bound object marked by this attribute. The <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> can inspect, add to, and remove properties from the context while determining if the context is acceptable to it.</param>
		/// <returns>
		///   <see langword="true" /> if the passed in context is OK; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ctx" /> or <paramref name="msg" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005906 RID: 22790 RVA: 0x0013AB30 File Offset: 0x00138D30
		[SecurityCritical]
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			if (ctx == null)
			{
				throw new ArgumentNullException("ctx");
			}
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			bool flag = true;
			if (this._flavor == 8)
			{
				flag = false;
			}
			else
			{
				SynchronizationAttribute synchronizationAttribute = (SynchronizationAttribute)ctx.GetProperty("Synchronization");
				if ((this._flavor == 1 && synchronizationAttribute != null) || (this._flavor == 4 && synchronizationAttribute == null))
				{
					flag = false;
				}
				if (this._flavor == 4)
				{
					this._cliCtxAttr = synchronizationAttribute;
				}
			}
			return flag;
		}

		/// <summary>Adds the <see langword="Synchronized" /> context property to the specified <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</summary>
		/// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the property.</param>
		// Token: 0x06005907 RID: 22791 RVA: 0x0013ABA4 File Offset: 0x00138DA4
		[SecurityCritical]
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (this._flavor == 1 || this._flavor == 2 || ctorMsg == null)
			{
				return;
			}
			if (this._cliCtxAttr != null)
			{
				ctorMsg.ContextProperties.Add(this._cliCtxAttr);
				this._cliCtxAttr = null;
				return;
			}
			ctorMsg.ContextProperties.Add(this);
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x0013ABF8 File Offset: 0x00138DF8
		internal virtual void InitIfNecessary()
		{
			lock (this)
			{
				if (this._asyncWorkEvent == null)
				{
					this._asyncWorkEvent = new AutoResetEvent(false);
					this._workItemQueue = new Queue();
					this._asyncLcidList = new ArrayList();
					WaitOrTimerCallback waitOrTimerCallback = new WaitOrTimerCallback(this.DispatcherCallBack);
					this._waitHandle = ThreadPool.RegisterWaitForSingleObject(this._asyncWorkEvent, waitOrTimerCallback, null, SynchronizationAttribute._timeOut, false);
				}
			}
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x0013AC80 File Offset: 0x00138E80
		private void DispatcherCallBack(object stateIgnored, bool ignored)
		{
			Queue workItemQueue = this._workItemQueue;
			WorkItem workItem;
			lock (workItemQueue)
			{
				workItem = (WorkItem)this._workItemQueue.Dequeue();
			}
			this.ExecuteWorkItem(workItem);
			this.HandleWorkCompletion();
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x0013ACD8 File Offset: 0x00138ED8
		internal virtual void HandleThreadExit()
		{
			this.HandleWorkCompletion();
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x0013ACE0 File Offset: 0x00138EE0
		internal virtual void HandleThreadReEntry()
		{
			WorkItem workItem = new WorkItem(null, null, null);
			workItem.SetDummy();
			this.HandleWorkRequest(workItem);
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x0013AD04 File Offset: 0x00138F04
		internal virtual void HandleWorkCompletion()
		{
			WorkItem workItem = null;
			bool flag = false;
			Queue workItemQueue = this._workItemQueue;
			lock (workItemQueue)
			{
				if (this._workItemQueue.Count >= 1)
				{
					workItem = (WorkItem)this._workItemQueue.Peek();
					flag = true;
					workItem.SetSignaled();
				}
				else
				{
					this._locked = false;
				}
			}
			if (flag)
			{
				if (workItem.IsAsync())
				{
					this._asyncWorkEvent.Set();
					return;
				}
				WorkItem workItem2 = workItem;
				lock (workItem2)
				{
					Monitor.Pulse(workItem);
				}
			}
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x0013ADBC File Offset: 0x00138FBC
		internal virtual void HandleWorkRequest(WorkItem work)
		{
			if (!this.IsNestedCall(work._reqMsg))
			{
				if (work.IsAsync())
				{
					bool flag = true;
					Queue workItemQueue = this._workItemQueue;
					lock (workItemQueue)
					{
						work.SetWaiting();
						this._workItemQueue.Enqueue(work);
						if (!this._locked && this._workItemQueue.Count == 1)
						{
							work.SetSignaled();
							this._locked = true;
							this._asyncWorkEvent.Set();
						}
						return;
					}
				}
				lock (work)
				{
					Queue workItemQueue2 = this._workItemQueue;
					bool flag;
					lock (workItemQueue2)
					{
						if (!this._locked && this._workItemQueue.Count == 0)
						{
							this._locked = true;
							flag = false;
						}
						else
						{
							flag = true;
							work.SetWaiting();
							this._workItemQueue.Enqueue(work);
						}
					}
					if (flag)
					{
						Monitor.Wait(work);
						if (!work.IsDummy())
						{
							this.DispatcherCallBack(null, true);
							return;
						}
						Queue workItemQueue3 = this._workItemQueue;
						lock (workItemQueue3)
						{
							this._workItemQueue.Dequeue();
							return;
						}
					}
					if (!work.IsDummy())
					{
						work.SetSignaled();
						this.ExecuteWorkItem(work);
						this.HandleWorkCompletion();
					}
					return;
				}
			}
			work.SetSignaled();
			work.Execute();
		}

		// Token: 0x0600590E RID: 22798 RVA: 0x0013AF5C File Offset: 0x0013915C
		internal void ExecuteWorkItem(WorkItem work)
		{
			work.Execute();
		}

		// Token: 0x0600590F RID: 22799 RVA: 0x0013AF64 File Offset: 0x00139164
		internal bool IsNestedCall(IMessage reqMsg)
		{
			bool flag = false;
			if (!this.IsReEntrant)
			{
				string syncCallOutLCID = this.SyncCallOutLCID;
				if (syncCallOutLCID != null)
				{
					LogicalCallContext logicalCallContext = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
					if (logicalCallContext != null && syncCallOutLCID.Equals(logicalCallContext.RemotingData.LogicalCallID))
					{
						flag = true;
					}
				}
				if (!flag && this.AsyncCallOutLCIDList.Count > 0)
				{
					LogicalCallContext logicalCallContext2 = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
					if (this.AsyncCallOutLCIDList.Contains(logicalCallContext2.RemotingData.LogicalCallID))
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		/// <summary>Creates a synchronized dispatch sink and chains it in front of the provided chain of sinks at the context boundary on the server end of a remoting call.</summary>
		/// <param name="nextSink">The chain of sinks composed so far.</param>
		/// <returns>The composite sink chain with the new synchronized dispatch sink.</returns>
		// Token: 0x06005910 RID: 22800 RVA: 0x0013AFF8 File Offset: 0x001391F8
		[SecurityCritical]
		public virtual IMessageSink GetServerContextSink(IMessageSink nextSink)
		{
			this.InitIfNecessary();
			return new SynchronizedServerContextSink(this, nextSink);
		}

		/// <summary>Creates a CallOut sink and chains it in front of the provided chain of sinks at the context boundary on the client end of a remoting call.</summary>
		/// <param name="nextSink">The chain of sinks composed so far.</param>
		/// <returns>The composite sink chain with the new CallOut sink.</returns>
		// Token: 0x06005911 RID: 22801 RVA: 0x0013B014 File Offset: 0x00139214
		[SecurityCritical]
		public virtual IMessageSink GetClientContextSink(IMessageSink nextSink)
		{
			this.InitIfNecessary();
			return new SynchronizedClientContextSink(this, nextSink);
		}

		/// <summary>Indicates that the class to which this attribute is applied cannot be created in a context that has synchronization. This field is constant.</summary>
		// Token: 0x04002883 RID: 10371
		public const int NOT_SUPPORTED = 1;

		/// <summary>Indicates that the class to which this attribute is applied is not dependent on whether the context has synchronization. This field is constant.</summary>
		// Token: 0x04002884 RID: 10372
		public const int SUPPORTED = 2;

		/// <summary>Indicates that the class to which this attribute is applied must be created in a context that has synchronization. This field is constant.</summary>
		// Token: 0x04002885 RID: 10373
		public const int REQUIRED = 4;

		/// <summary>Indicates that the class to which this attribute is applied must be created in a context with a new instance of the synchronization property each time. This field is constant.</summary>
		// Token: 0x04002886 RID: 10374
		public const int REQUIRES_NEW = 8;

		// Token: 0x04002887 RID: 10375
		private const string PROPERTY_NAME = "Synchronization";

		// Token: 0x04002888 RID: 10376
		private static readonly int _timeOut = -1;

		// Token: 0x04002889 RID: 10377
		[NonSerialized]
		internal AutoResetEvent _asyncWorkEvent;

		// Token: 0x0400288A RID: 10378
		[NonSerialized]
		private RegisteredWaitHandle _waitHandle;

		// Token: 0x0400288B RID: 10379
		[NonSerialized]
		internal Queue _workItemQueue;

		// Token: 0x0400288C RID: 10380
		[NonSerialized]
		internal bool _locked;

		// Token: 0x0400288D RID: 10381
		internal bool _bReEntrant;

		// Token: 0x0400288E RID: 10382
		internal int _flavor;

		// Token: 0x0400288F RID: 10383
		[NonSerialized]
		private SynchronizationAttribute _cliCtxAttr;

		// Token: 0x04002890 RID: 10384
		[NonSerialized]
		private string _syncLcid;

		// Token: 0x04002891 RID: 10385
		[NonSerialized]
		private ArrayList _asyncLcidList;
	}
}

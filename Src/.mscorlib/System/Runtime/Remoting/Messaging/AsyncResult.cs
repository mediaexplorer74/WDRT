using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Encapsulates the results of an asynchronous operation on a delegate.</summary>
	// Token: 0x02000853 RID: 2131
	[ComVisible(true)]
	public class AsyncResult : IAsyncResult, IMessageSink
	{
		// Token: 0x06005A7F RID: 23167 RVA: 0x0013F4FA File Offset: 0x0013D6FA
		[SecurityCritical]
		internal AsyncResult(Message m)
		{
			m.GetAsyncBeginInfo(out this._acbd, out this._asyncState);
			this._asyncDelegate = (Delegate)m.GetThisPtr();
		}

		/// <summary>Gets a value indicating whether the server has completed the call.</summary>
		/// <returns>
		///   <see langword="true" /> after the server has completed the call; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06005A80 RID: 23168 RVA: 0x0013F525 File Offset: 0x0013D725
		public virtual bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
		}

		/// <summary>Gets the delegate object on which the asynchronous call was invoked.</summary>
		/// <returns>The delegate object on which the asynchronous call was invoked.</returns>
		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06005A81 RID: 23169 RVA: 0x0013F52D File Offset: 0x0013D72D
		public virtual object AsyncDelegate
		{
			get
			{
				return this._asyncDelegate;
			}
		}

		/// <summary>Gets the object provided as the last parameter of a <see langword="BeginInvoke" /> method call.</summary>
		/// <returns>The object provided as the last parameter of a <see langword="BeginInvoke" /> method call.</returns>
		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06005A82 RID: 23170 RVA: 0x0013F535 File Offset: 0x0013D735
		public virtual object AsyncState
		{
			get
			{
				return this._asyncState;
			}
		}

		/// <summary>Gets a value indicating whether the <see langword="BeginInvoke" /> call completed synchronously.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="BeginInvoke" /> call completed synchronously; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06005A83 RID: 23171 RVA: 0x0013F53D File Offset: 0x0013D73D
		public virtual bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see langword="EndInvoke" /> has been called on the current <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />.</summary>
		/// <returns>
		///   <see langword="true" /> if <see langword="EndInvoke" /> has been called on the current <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06005A84 RID: 23172 RVA: 0x0013F540 File Offset: 0x0013D740
		// (set) Token: 0x06005A85 RID: 23173 RVA: 0x0013F548 File Offset: 0x0013D748
		public bool EndInvokeCalled
		{
			get
			{
				return this._endInvokeCalled;
			}
			set
			{
				this._endInvokeCalled = value;
			}
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x0013F554 File Offset: 0x0013D754
		private void FaultInWaitHandle()
		{
			lock (this)
			{
				if (this._AsyncWaitHandle == null)
				{
					this._AsyncWaitHandle = new ManualResetEvent(false);
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that encapsulates Win32 synchronization handles, and allows the implementation of various synchronization schemes.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that encapsulates Win32 synchronization handles, and allows the implementation of various synchronization schemes.</returns>
		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06005A87 RID: 23175 RVA: 0x0013F5A0 File Offset: 0x0013D7A0
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				this.FaultInWaitHandle();
				return this._AsyncWaitHandle;
			}
		}

		/// <summary>Sets an <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> for the current remote method call, which provides a way to control asynchronous messages after they have been dispatched.</summary>
		/// <param name="mc">The <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> for the current remote method call.</param>
		// Token: 0x06005A88 RID: 23176 RVA: 0x0013F5AE File Offset: 0x0013D7AE
		public virtual void SetMessageCtrl(IMessageCtrl mc)
		{
			this._mc = mc;
		}

		/// <summary>Synchronously processes a response message returned by a method call on a remote object.</summary>
		/// <param name="msg">A response message to a method call on a remote object.</param>
		/// <returns>Returns <see langword="null" />.</returns>
		// Token: 0x06005A89 RID: 23177 RVA: 0x0013F5B8 File Offset: 0x0013D7B8
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			if (msg == null)
			{
				this._replyMsg = new ReturnMessage(new RemotingException(Environment.GetResourceString("Remoting_NullMessage")), new ErrorMessage());
			}
			else if (!(msg is IMethodReturnMessage))
			{
				this._replyMsg = new ReturnMessage(new RemotingException(Environment.GetResourceString("Remoting_Message_BadType")), new ErrorMessage());
			}
			else
			{
				this._replyMsg = msg;
			}
			this._isCompleted = true;
			this.FaultInWaitHandle();
			this._AsyncWaitHandle.Set();
			if (this._acbd != null)
			{
				this._acbd(this);
			}
			return null;
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface.</summary>
		/// <param name="msg">The request <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		/// <param name="replySink">The response <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface.</param>
		/// <returns>No value is returned.</returns>
		// Token: 0x06005A8A RID: 23178 RVA: 0x0013F647 File Offset: 0x0013D847
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		/// <summary>Gets the next message sink in the sink chain.</summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface that represents the next message sink in the sink chain.</returns>
		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06005A8B RID: 23179 RVA: 0x0013F658 File Offset: 0x0013D858
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		/// <summary>Gets the response message for the asynchronous call.</summary>
		/// <returns>A remoting message that should represent a response to a method call on a remote object.</returns>
		// Token: 0x06005A8C RID: 23180 RVA: 0x0013F65B File Offset: 0x0013D85B
		public virtual IMessage GetReplyMessage()
		{
			return this._replyMsg;
		}

		// Token: 0x0400290D RID: 10509
		private IMessageCtrl _mc;

		// Token: 0x0400290E RID: 10510
		private AsyncCallback _acbd;

		// Token: 0x0400290F RID: 10511
		private IMessage _replyMsg;

		// Token: 0x04002910 RID: 10512
		private bool _isCompleted;

		// Token: 0x04002911 RID: 10513
		private bool _endInvokeCalled;

		// Token: 0x04002912 RID: 10514
		private ManualResetEvent _AsyncWaitHandle;

		// Token: 0x04002913 RID: 10515
		private Delegate _asyncDelegate;

		// Token: 0x04002914 RID: 10516
		private object _asyncState;
	}
}

using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020001DC RID: 476
	internal class PooledStream : Stream
	{
		// Token: 0x06001270 RID: 4720 RVA: 0x00062837 File Offset: 0x00060A37
		internal PooledStream(object owner)
		{
			this.m_Owner = new WeakReference(owner);
			this.m_PooledCount = -1;
			this.m_Initalizing = true;
			this.m_NetworkStream = new NetworkStream();
			this.m_CreateTime = DateTime.UtcNow;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0006286F File Offset: 0x00060A6F
		internal PooledStream(ConnectionPool connectionPool, TimeSpan lifetime, bool checkLifetime)
		{
			this.m_ConnectionPool = connectionPool;
			this.m_Lifetime = lifetime;
			this.m_CheckLifetime = checkLifetime;
			this.m_Initalizing = true;
			this.m_NetworkStream = new NetworkStream();
			this.m_CreateTime = DateTime.UtcNow;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x000628A9 File Offset: 0x00060AA9
		internal bool JustConnected
		{
			get
			{
				if (this.m_JustConnected)
				{
					this.m_JustConnected = false;
					return true;
				}
				return false;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x000628BD File Offset: 0x00060ABD
		internal IPAddress ServerAddress
		{
			get
			{
				return this.m_ServerAddress;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x000628C5 File Offset: 0x00060AC5
		internal bool IsInitalizing
		{
			get
			{
				return this.m_Initalizing;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x000628D0 File Offset: 0x00060AD0
		// (set) Token: 0x06001276 RID: 4726 RVA: 0x00062919 File Offset: 0x00060B19
		internal bool CanBePooled
		{
			get
			{
				if (this.m_Initalizing)
				{
					return true;
				}
				if (!this.m_NetworkStream.Connected)
				{
					return false;
				}
				WeakReference owner = this.m_Owner;
				return !this.m_ConnectionIsDoomed && (owner == null || !owner.IsAlive);
			}
			set
			{
				this.m_ConnectionIsDoomed |= !value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x0006292C File Offset: 0x00060B2C
		internal bool IsEmancipated
		{
			get
			{
				WeakReference owner = this.m_Owner;
				return 0 >= this.m_PooledCount && (owner == null || !owner.IsAlive);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00062960 File Offset: 0x00060B60
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x00062988 File Offset: 0x00060B88
		internal object Owner
		{
			get
			{
				WeakReference owner = this.m_Owner;
				if (owner != null && owner.IsAlive)
				{
					return owner.Target;
				}
				return null;
			}
			set
			{
				lock (this)
				{
					if (this.m_Owner != null)
					{
						this.m_Owner.Target = value;
					}
				}
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x000629D4 File Offset: 0x00060BD4
		internal ConnectionPool Pool
		{
			get
			{
				return this.m_ConnectionPool;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x000629DC File Offset: 0x00060BDC
		internal virtual ServicePoint ServicePoint
		{
			get
			{
				return this.Pool.ServicePoint;
			}
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x000629E9 File Offset: 0x00060BE9
		internal bool Activate(object owningObject, GeneralAsyncDelegate asyncCallback)
		{
			return this.Activate(owningObject, asyncCallback != null, asyncCallback);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x000629F8 File Offset: 0x00060BF8
		protected bool Activate(object owningObject, bool async, GeneralAsyncDelegate asyncCallback)
		{
			bool flag;
			try
			{
				if (this.m_Initalizing)
				{
					IPAddress ipaddress = null;
					this.m_AsyncCallback = asyncCallback;
					Socket connection = this.ServicePoint.GetConnection(this, owningObject, async, out ipaddress, ref this.m_AbortSocket, ref this.m_AbortSocket6);
					if (connection != null)
					{
						if (Logging.On)
						{
							Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_socket_connected", new object[] { connection.LocalEndPoint, connection.RemoteEndPoint }));
						}
						this.m_NetworkStream.InitNetworkStream(connection, FileAccess.ReadWrite);
						this.m_ServerAddress = ipaddress;
						this.m_Initalizing = false;
						this.m_JustConnected = true;
						this.m_AbortSocket = null;
						this.m_AbortSocket6 = null;
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					if (async && asyncCallback != null)
					{
						asyncCallback(owningObject, this);
					}
					flag = true;
				}
			}
			catch
			{
				this.m_Initalizing = false;
				throw;
			}
			return flag;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00062AD4 File Offset: 0x00060CD4
		internal void Deactivate()
		{
			this.m_AsyncCallback = null;
			if (!this.m_ConnectionIsDoomed && this.m_CheckLifetime)
			{
				this.CheckLifetime();
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00062AF4 File Offset: 0x00060CF4
		internal virtual void ConnectionCallback(object owningObject, Exception e, Socket socket, IPAddress address)
		{
			object obj = null;
			if (e != null)
			{
				this.m_Initalizing = false;
				obj = e;
			}
			else
			{
				try
				{
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_socket_connected", new object[] { socket.LocalEndPoint, socket.RemoteEndPoint }));
					}
					this.m_NetworkStream.InitNetworkStream(socket, FileAccess.ReadWrite);
					obj = this;
				}
				catch (Exception ex)
				{
					if (NclUtilities.IsFatal(ex))
					{
						throw;
					}
					obj = ex;
				}
				this.m_ServerAddress = address;
				this.m_Initalizing = false;
				this.m_JustConnected = true;
			}
			if (this.m_AsyncCallback != null)
			{
				this.m_AsyncCallback(owningObject, obj);
			}
			this.m_AbortSocket = null;
			this.m_AbortSocket6 = null;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00062BB0 File Offset: 0x00060DB0
		protected void CheckLifetime()
		{
			bool flag = !this.m_ConnectionIsDoomed;
			if (flag)
			{
				TimeSpan timeSpan = DateTime.UtcNow.Subtract(this.m_CreateTime);
				this.m_ConnectionIsDoomed = 0 < TimeSpan.Compare(this.m_Lifetime, timeSpan);
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00062BF4 File Offset: 0x00060DF4
		internal void UpdateLifetime()
		{
			int connectionLeaseTimeout = this.ServicePoint.ConnectionLeaseTimeout;
			TimeSpan maxValue;
			if (connectionLeaseTimeout == -1)
			{
				maxValue = TimeSpan.MaxValue;
				this.m_CheckLifetime = false;
			}
			else
			{
				maxValue = new TimeSpan(0, 0, 0, 0, connectionLeaseTimeout);
				this.m_CheckLifetime = true;
			}
			if (maxValue != this.m_Lifetime)
			{
				this.m_Lifetime = maxValue;
			}
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00062C48 File Offset: 0x00060E48
		internal void PrePush(object expectedOwner)
		{
			lock (this)
			{
				if (expectedOwner == null)
				{
					if (this.m_Owner != null && this.m_Owner.Target != null)
					{
						throw new InternalException();
					}
				}
				else if (this.m_Owner == null || this.m_Owner.Target != expectedOwner)
				{
					throw new InternalException();
				}
				this.m_PooledCount++;
				if (1 != this.m_PooledCount)
				{
					throw new InternalException();
				}
				if (this.m_Owner != null)
				{
					this.m_Owner.Target = null;
				}
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00062CE8 File Offset: 0x00060EE8
		internal void PostPop(object newOwner)
		{
			lock (this)
			{
				if (this.m_Owner == null)
				{
					this.m_Owner = new WeakReference(newOwner);
				}
				else
				{
					if (this.m_Owner.Target != null)
					{
						throw new InternalException();
					}
					this.m_Owner.Target = newOwner;
				}
				this.m_PooledCount--;
				if (this.Pool != null)
				{
					if (this.m_PooledCount != 0)
					{
						throw new InternalException();
					}
				}
				else if (-1 != this.m_PooledCount)
				{
					throw new InternalException();
				}
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00062D88 File Offset: 0x00060F88
		protected bool UsingSecureStream
		{
			get
			{
				return this.m_NetworkStream is TlsStream;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x00062D98 File Offset: 0x00060F98
		// (set) Token: 0x06001286 RID: 4742 RVA: 0x00062DA0 File Offset: 0x00060FA0
		internal NetworkStream NetworkStream
		{
			get
			{
				return this.m_NetworkStream;
			}
			set
			{
				this.m_Initalizing = false;
				this.m_NetworkStream = value;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00062DB0 File Offset: 0x00060FB0
		protected Socket Socket
		{
			get
			{
				return this.m_NetworkStream.InternalSocket;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00062DBD File Offset: 0x00060FBD
		public override bool CanRead
		{
			get
			{
				return this.m_NetworkStream.CanRead;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00062DCA File Offset: 0x00060FCA
		public override bool CanSeek
		{
			get
			{
				return this.m_NetworkStream.CanSeek;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x00062DD7 File Offset: 0x00060FD7
		public override bool CanWrite
		{
			get
			{
				return this.m_NetworkStream.CanWrite;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00062DE4 File Offset: 0x00060FE4
		public override bool CanTimeout
		{
			get
			{
				return this.m_NetworkStream.CanTimeout;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00062DF1 File Offset: 0x00060FF1
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x00062DFE File Offset: 0x00060FFE
		public override int ReadTimeout
		{
			get
			{
				return this.m_NetworkStream.ReadTimeout;
			}
			set
			{
				this.m_NetworkStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x00062E0C File Offset: 0x0006100C
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x00062E19 File Offset: 0x00061019
		public override int WriteTimeout
		{
			get
			{
				return this.m_NetworkStream.WriteTimeout;
			}
			set
			{
				this.m_NetworkStream.WriteTimeout = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00062E27 File Offset: 0x00061027
		public override long Length
		{
			get
			{
				return this.m_NetworkStream.Length;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x00062E34 File Offset: 0x00061034
		// (set) Token: 0x06001292 RID: 4754 RVA: 0x00062E41 File Offset: 0x00061041
		public override long Position
		{
			get
			{
				return this.m_NetworkStream.Position;
			}
			set
			{
				this.m_NetworkStream.Position = value;
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00062E4F File Offset: 0x0006104F
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_NetworkStream.Seek(offset, origin);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00062E60 File Offset: 0x00061060
		public override int Read(byte[] buffer, int offset, int size)
		{
			int num2;
			try
			{
				if (ServicePointManager.UseSafeSynchronousClose)
				{
					int num = Interlocked.Increment(ref this.m_SynchronousIOClosingState);
					if ((num & 1610612736) != 0)
					{
						throw new ObjectDisposedException(base.GetType().FullName);
					}
				}
				num2 = this.m_NetworkStream.Read(buffer, offset, size);
			}
			finally
			{
				if (ServicePointManager.UseSafeSynchronousClose && 536870912 == Interlocked.Decrement(ref this.m_SynchronousIOClosingState))
				{
					try
					{
						this.TryCloseNetworkStream(false, 0);
					}
					catch
					{
					}
				}
			}
			return num2;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00062EF0 File Offset: 0x000610F0
		public override void Write(byte[] buffer, int offset, int size)
		{
			try
			{
				if (ServicePointManager.UseSafeSynchronousClose)
				{
					int num = Interlocked.Increment(ref this.m_SynchronousIOClosingState);
					if ((num & 1610612736) != 0)
					{
						throw new ObjectDisposedException(base.GetType().FullName);
					}
				}
				this.m_NetworkStream.Write(buffer, offset, size);
			}
			finally
			{
				if (ServicePointManager.UseSafeSynchronousClose && 536870912 == Interlocked.Decrement(ref this.m_SynchronousIOClosingState))
				{
					try
					{
						this.TryCloseNetworkStream(false, 0);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00062F80 File Offset: 0x00061180
		internal void MultipleWrite(BufferOffsetSize[] buffers)
		{
			this.m_NetworkStream.MultipleWrite(buffers);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00062F90 File Offset: 0x00061190
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.m_Owner = null;
					this.m_ConnectionIsDoomed = true;
					this.CloseSocket();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00062FD0 File Offset: 0x000611D0
		private int InterlockedOr(ref int location1, int bitMask)
		{
			int num;
			do
			{
				num = Volatile.Read(ref location1);
			}
			while (num != Interlocked.CompareExchange(ref location1, num | bitMask, num));
			return num;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00062FF4 File Offset: 0x000611F4
		private bool TryCloseNetworkStream(bool closeWithTimeout, int timeout)
		{
			if (!ServicePointManager.UseSafeSynchronousClose)
			{
				return false;
			}
			if (536870912 == Interlocked.CompareExchange(ref this.m_SynchronousIOClosingState, 1073741824, 536870912))
			{
				if (closeWithTimeout)
				{
					this.m_NetworkStream.Close(timeout);
				}
				else
				{
					this.m_NetworkStream.Close();
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00063048 File Offset: 0x00061248
		private bool CancelPendingIoAndCloseIfSafe(bool closeWithTimeout, int timeout)
		{
			if (this.TryCloseNetworkStream(closeWithTimeout, timeout))
			{
				return true;
			}
			try
			{
				Socket internalSocket = this.m_NetworkStream.InternalSocket;
				UnsafeNclNativeMethods.CancelIoEx(internalSocket.SafeHandle, IntPtr.Zero);
			}
			catch
			{
			}
			return this.TryCloseNetworkStream(closeWithTimeout, timeout);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0006309C File Offset: 0x0006129C
		private void CloseConnectingSockets(bool useTimeout, int timeout)
		{
			Socket abortSocket = this.m_AbortSocket;
			Socket abortSocket2 = this.m_AbortSocket6;
			if (abortSocket != null)
			{
				if (ServicePointManager.UseSafeSynchronousClose)
				{
					try
					{
						UnsafeNclNativeMethods.CancelIoEx(abortSocket.SafeHandle, IntPtr.Zero);
					}
					catch
					{
					}
				}
				if (useTimeout)
				{
					abortSocket.Close(timeout);
				}
				else
				{
					abortSocket.Close();
				}
				this.m_AbortSocket = null;
			}
			if (abortSocket2 != null)
			{
				if (ServicePointManager.UseSafeSynchronousClose)
				{
					try
					{
						UnsafeNclNativeMethods.CancelIoEx(abortSocket2.SafeHandle, IntPtr.Zero);
					}
					catch
					{
					}
				}
				if (useTimeout)
				{
					abortSocket2.Close(timeout);
				}
				else
				{
					abortSocket2.Close();
				}
				this.m_AbortSocket6 = null;
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00063148 File Offset: 0x00061348
		internal void CloseSocket()
		{
			if (!ServicePointManager.UseSafeSynchronousClose)
			{
				this.m_NetworkStream.Close();
			}
			else
			{
				this.InterlockedOr(ref this.m_SynchronousIOClosingState, 536870912);
				this.CancelPendingIoAndCloseIfSafe(false, 0);
			}
			this.CloseConnectingSockets(false, 0);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00063181 File Offset: 0x00061381
		public void Close(int timeout)
		{
			if (!ServicePointManager.UseSafeSynchronousClose)
			{
				this.m_NetworkStream.Close(timeout);
			}
			else
			{
				this.InterlockedOr(ref this.m_SynchronousIOClosingState, 536870912);
				this.CancelPendingIoAndCloseIfSafe(true, timeout);
			}
			this.CloseConnectingSockets(true, timeout);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x000631BB File Offset: 0x000613BB
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x000631CF File Offset: 0x000613CF
		internal virtual IAsyncResult UnsafeBeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.UnsafeBeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000631E3 File Offset: 0x000613E3
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_NetworkStream.EndRead(asyncResult);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000631F1 File Offset: 0x000613F1
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00063205 File Offset: 0x00061405
		internal virtual IAsyncResult UnsafeBeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.UnsafeBeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00063219 File Offset: 0x00061419
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_NetworkStream.EndWrite(asyncResult);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00063227 File Offset: 0x00061427
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		internal IAsyncResult BeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginMultipleWrite(buffers, callback, state);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00063237 File Offset: 0x00061437
		internal void EndMultipleWrite(IAsyncResult asyncResult)
		{
			this.m_NetworkStream.EndMultipleWrite(asyncResult);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00063245 File Offset: 0x00061445
		public override void Flush()
		{
			this.m_NetworkStream.Flush();
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00063252 File Offset: 0x00061452
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this.m_NetworkStream.FlushAsync(cancellationToken);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00063260 File Offset: 0x00061460
		public override void SetLength(long value)
		{
			this.m_NetworkStream.SetLength(value);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0006326E File Offset: 0x0006146E
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			this.m_NetworkStream.SetSocketTimeoutOption(mode, timeout, silent);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0006327E File Offset: 0x0006147E
		internal bool Poll(int microSeconds, SelectMode mode)
		{
			return this.m_NetworkStream.Poll(microSeconds, mode);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0006328D File Offset: 0x0006148D
		internal bool PollRead()
		{
			return this.m_NetworkStream.PollRead();
		}

		// Token: 0x040014EF RID: 5359
		private const int ClosingFlag = 536870912;

		// Token: 0x040014F0 RID: 5360
		private const int ClosedFlag = 1073741824;

		// Token: 0x040014F1 RID: 5361
		private bool m_CheckLifetime;

		// Token: 0x040014F2 RID: 5362
		private TimeSpan m_Lifetime;

		// Token: 0x040014F3 RID: 5363
		private DateTime m_CreateTime;

		// Token: 0x040014F4 RID: 5364
		private bool m_ConnectionIsDoomed;

		// Token: 0x040014F5 RID: 5365
		private ConnectionPool m_ConnectionPool;

		// Token: 0x040014F6 RID: 5366
		private WeakReference m_Owner;

		// Token: 0x040014F7 RID: 5367
		private int m_PooledCount;

		// Token: 0x040014F8 RID: 5368
		private bool m_Initalizing;

		// Token: 0x040014F9 RID: 5369
		private IPAddress m_ServerAddress;

		// Token: 0x040014FA RID: 5370
		private NetworkStream m_NetworkStream;

		// Token: 0x040014FB RID: 5371
		private Socket m_AbortSocket;

		// Token: 0x040014FC RID: 5372
		private Socket m_AbortSocket6;

		// Token: 0x040014FD RID: 5373
		private bool m_JustConnected;

		// Token: 0x040014FE RID: 5374
		private int m_SynchronousIOClosingState;

		// Token: 0x040014FF RID: 5375
		private GeneralAsyncDelegate m_AsyncCallback;
	}
}

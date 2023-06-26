using System;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x02000399 RID: 921
	internal abstract class MultipleConnectAsync
	{
		// Token: 0x06002256 RID: 8790 RVA: 0x000A3D00 File Offset: 0x000A1F00
		public bool StartConnectAsync(SocketAsyncEventArgs args, DnsEndPoint endPoint)
		{
			object obj = this.lockObject;
			bool flag2;
			lock (obj)
			{
				this.userArgs = args;
				this.endPoint = endPoint;
				if (this.state == MultipleConnectAsync.State.Canceled)
				{
					this.SyncFail(new SocketException(SocketError.OperationAborted));
					flag2 = false;
				}
				else
				{
					this.state = MultipleConnectAsync.State.DnsQuery;
					IAsyncResult asyncResult = Dns.BeginGetHostAddresses(endPoint.Host, new AsyncCallback(this.DnsCallback), null);
					if (asyncResult.CompletedSynchronously)
					{
						flag2 = this.DoDnsCallback(asyncResult, true);
					}
					else
					{
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000A3D9C File Offset: 0x000A1F9C
		private void DnsCallback(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				this.DoDnsCallback(result, false);
			}
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000A3DB0 File Offset: 0x000A1FB0
		private bool DoDnsCallback(IAsyncResult result, bool sync)
		{
			Exception ex = null;
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.state == MultipleConnectAsync.State.Canceled)
				{
					return true;
				}
				try
				{
					this.addressList = Dns.EndGetHostAddresses(result);
				}
				catch (Exception ex2)
				{
					this.state = MultipleConnectAsync.State.Completed;
					ex = ex2;
				}
				if (ex == null)
				{
					this.state = MultipleConnectAsync.State.ConnectAttempt;
					this.internalArgs = new SocketAsyncEventArgs();
					this.internalArgs.Completed += this.InternalConnectCallback;
					this.internalArgs.SetBuffer(this.userArgs.Buffer, this.userArgs.Offset, this.userArgs.Count);
					ex = this.AttemptConnection();
					if (ex != null)
					{
						this.state = MultipleConnectAsync.State.Completed;
					}
				}
			}
			return ex == null || this.Fail(sync, ex);
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000A3EA0 File Offset: 0x000A20A0
		private void InternalConnectCallback(object sender, SocketAsyncEventArgs args)
		{
			Exception ex = null;
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.state == MultipleConnectAsync.State.Canceled)
				{
					ex = new SocketException(SocketError.OperationAborted);
				}
				else if (args.SocketError == SocketError.Success)
				{
					this.state = MultipleConnectAsync.State.Completed;
				}
				else if (args.SocketError == SocketError.OperationAborted)
				{
					ex = new SocketException(SocketError.OperationAborted);
					this.state = MultipleConnectAsync.State.Canceled;
				}
				else
				{
					SocketError socketError = args.SocketError;
					Exception ex2 = this.AttemptConnection();
					if (ex2 == null)
					{
						return;
					}
					SocketException ex3 = ex2 as SocketException;
					if (ex3 != null && ex3.SocketErrorCode == SocketError.NoData)
					{
						ex = new SocketException(socketError);
					}
					else
					{
						ex = ex2;
					}
					this.state = MultipleConnectAsync.State.Completed;
				}
			}
			if (ex == null)
			{
				this.Succeed();
				return;
			}
			this.AsyncFail(ex);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000A3F7C File Offset: 0x000A217C
		private Exception AttemptConnection()
		{
			try
			{
				Socket socket = null;
				IPAddress ipaddress = this.GetNextAddress(out socket);
				if (ipaddress == null)
				{
					return new SocketException(SocketError.NoData);
				}
				this.internalArgs.RemoteEndPoint = new IPEndPoint(ipaddress, this.endPoint.Port);
				if (!socket.ConnectAsync(this.internalArgs))
				{
					return new SocketException(this.internalArgs.SocketError);
				}
			}
			catch (ObjectDisposedException)
			{
				return new SocketException(SocketError.OperationAborted);
			}
			catch (Exception ex)
			{
				return ex;
			}
			return null;
		}

		// Token: 0x0600225B RID: 8795
		protected abstract void OnSucceed();

		// Token: 0x0600225C RID: 8796 RVA: 0x000A4018 File Offset: 0x000A2218
		protected void Succeed()
		{
			this.OnSucceed();
			this.userArgs.FinishWrapperConnectSuccess(this.internalArgs.ConnectSocket, this.internalArgs.BytesTransferred, this.internalArgs.SocketFlags);
			this.internalArgs.Dispose();
		}

		// Token: 0x0600225D RID: 8797
		protected abstract void OnFail(bool abortive);

		// Token: 0x0600225E RID: 8798 RVA: 0x000A4057 File Offset: 0x000A2257
		private bool Fail(bool sync, Exception e)
		{
			if (sync)
			{
				this.SyncFail(e);
				return false;
			}
			this.AsyncFail(e);
			return true;
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000A4070 File Offset: 0x000A2270
		private void SyncFail(Exception e)
		{
			this.OnFail(false);
			if (this.internalArgs != null)
			{
				this.internalArgs.Dispose();
			}
			SocketException ex = e as SocketException;
			if (ex != null)
			{
				this.userArgs.FinishConnectByNameSyncFailure(ex, 0, SocketFlags.None);
				return;
			}
			throw e;
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000A40B1 File Offset: 0x000A22B1
		private void AsyncFail(Exception e)
		{
			this.OnFail(false);
			if (this.internalArgs != null)
			{
				this.internalArgs.Dispose();
			}
			this.userArgs.FinishOperationAsyncFailure(e, 0, SocketFlags.None);
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000A40DC File Offset: 0x000A22DC
		public void Cancel()
		{
			bool flag = false;
			object obj = this.lockObject;
			lock (obj)
			{
				switch (this.state)
				{
				case MultipleConnectAsync.State.NotStarted:
					flag = true;
					break;
				case MultipleConnectAsync.State.DnsQuery:
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.CallAsyncFail));
					flag = true;
					break;
				case MultipleConnectAsync.State.ConnectAttempt:
					flag = true;
					break;
				}
				this.state = MultipleConnectAsync.State.Canceled;
			}
			if (flag)
			{
				this.OnFail(true);
			}
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000A4164 File Offset: 0x000A2364
		private void CallAsyncFail(object ignored)
		{
			this.AsyncFail(new SocketException(SocketError.OperationAborted));
		}

		// Token: 0x06002263 RID: 8803
		protected abstract IPAddress GetNextAddress(out Socket attemptSocket);

		// Token: 0x04001F62 RID: 8034
		protected SocketAsyncEventArgs userArgs;

		// Token: 0x04001F63 RID: 8035
		protected SocketAsyncEventArgs internalArgs;

		// Token: 0x04001F64 RID: 8036
		protected DnsEndPoint endPoint;

		// Token: 0x04001F65 RID: 8037
		protected IPAddress[] addressList;

		// Token: 0x04001F66 RID: 8038
		protected int nextAddress;

		// Token: 0x04001F67 RID: 8039
		private MultipleConnectAsync.State state;

		// Token: 0x04001F68 RID: 8040
		private object lockObject = new object();

		// Token: 0x020007E0 RID: 2016
		private enum State
		{
			// Token: 0x040034BF RID: 13503
			NotStarted,
			// Token: 0x040034C0 RID: 13504
			DnsQuery,
			// Token: 0x040034C1 RID: 13505
			ConnectAttempt,
			// Token: 0x040034C2 RID: 13506
			Completed,
			// Token: 0x040034C3 RID: 13507
			Canceled
		}
	}
}

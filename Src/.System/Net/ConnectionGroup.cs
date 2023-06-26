using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001A4 RID: 420
	internal class ConnectionGroup
	{
		// Token: 0x06001035 RID: 4149 RVA: 0x000569A4 File Offset: 0x00054BA4
		internal ConnectionGroup(ServicePoint servicePoint, string connName)
		{
			this.m_ServicePoint = servicePoint;
			this.m_ConnectionLimit = servicePoint.ConnectionLimit;
			this.m_ConnectionList = new ArrayList(3);
			this.m_Name = ConnectionGroup.MakeQueryStr(connName);
			this.m_AbortDelegate = new HttpAbortDelegate(this.Abort);
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x000569FB File Offset: 0x00054BFB
		internal string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00056A03 File Offset: 0x00054C03
		internal ServicePoint ServicePoint
		{
			get
			{
				return this.m_ServicePoint;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x00056A0B File Offset: 0x00054C0B
		internal int CurrentConnections
		{
			get
			{
				return this.m_ConnectionList.Count;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x00056A18 File Offset: 0x00054C18
		// (set) Token: 0x0600103A RID: 4154 RVA: 0x00056A20 File Offset: 0x00054C20
		internal int ConnectionLimit
		{
			get
			{
				return this.m_ConnectionLimit;
			}
			set
			{
				this.m_ConnectionLimit = value;
				this.PruneExcesiveConnections();
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x00056A30 File Offset: 0x00054C30
		private ManualResetEvent AsyncWaitHandle
		{
			get
			{
				if (this.m_Event == null)
				{
					Interlocked.CompareExchange(ref this.m_Event, new ManualResetEvent(false), null);
				}
				return (ManualResetEvent)this.m_Event;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x00056A68 File Offset: 0x00054C68
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x00056AC4 File Offset: 0x00054CC4
		private Queue AuthenticationRequestQueue
		{
			get
			{
				if (this.m_AuthenticationRequestQueue == null)
				{
					ArrayList connectionList = this.m_ConnectionList;
					lock (connectionList)
					{
						if (this.m_AuthenticationRequestQueue == null)
						{
							this.m_AuthenticationRequestQueue = new Queue();
						}
					}
				}
				return this.m_AuthenticationRequestQueue;
			}
			set
			{
				this.m_AuthenticationRequestQueue = value;
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00056ACD File Offset: 0x00054CCD
		internal static string MakeQueryStr(string connName)
		{
			if (connName != null)
			{
				return connName;
			}
			return "";
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00056ADC File Offset: 0x00054CDC
		internal void Associate(Connection connection)
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				this.m_ConnectionList.Add(connection);
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00056B24 File Offset: 0x00054D24
		internal void Disassociate(Connection connection)
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				this.m_ConnectionList.Remove(connection);
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00056B6C File Offset: 0x00054D6C
		internal void ConnectionGoneIdle()
		{
			if (this.m_AuthenticationGroup)
			{
				ArrayList connectionList = this.m_ConnectionList;
				lock (connectionList)
				{
					this.AsyncWaitHandle.Set();
				}
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00056BBC File Offset: 0x00054DBC
		internal void IncrementConnection()
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				this.m_ActiveConnections++;
				if (this.m_ActiveConnections == 1)
				{
					this.CancelIdleTimer();
				}
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00056C14 File Offset: 0x00054E14
		internal void DecrementConnection()
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				this.m_ActiveConnections--;
				if (this.m_ActiveConnections == 0)
				{
					this.m_ExpiringTimer = this.ServicePoint.CreateConnectionGroupTimer(this);
				}
				else if (this.m_ActiveConnections < 0)
				{
					this.m_ActiveConnections = 0;
				}
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00056C88 File Offset: 0x00054E88
		internal void CancelIdleTimer()
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				TimerThread.Timer expiringTimer = this.m_ExpiringTimer;
				this.m_ExpiringTimer = null;
				if (expiringTimer != null)
				{
					expiringTimer.Cancel();
				}
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00056CDC File Offset: 0x00054EDC
		private bool Abort(HttpWebRequest request, WebException webException)
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				this.AsyncWaitHandle.Set();
			}
			return true;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00056D24 File Offset: 0x00054F24
		private void PruneAbortedRequests()
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				Queue queue = new Queue();
				foreach (object obj in this.AuthenticationRequestQueue)
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)obj;
					if (!httpWebRequest.Aborted)
					{
						queue.Enqueue(httpWebRequest);
					}
				}
				this.AuthenticationRequestQueue = queue;
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00056DC4 File Offset: 0x00054FC4
		private void PruneExcesiveConnections()
		{
			ArrayList arrayList = new ArrayList();
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				int connectionLimit = this.ConnectionLimit;
				if (this.CurrentConnections > connectionLimit)
				{
					int num = this.CurrentConnections - connectionLimit;
					for (int i = 0; i < num; i++)
					{
						arrayList.Add(this.m_ConnectionList[i]);
					}
					this.m_ConnectionList.RemoveRange(0, num);
				}
			}
			foreach (object obj in arrayList)
			{
				Connection connection = (Connection)obj;
				connection.CloseOnIdle();
			}
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00056E9C File Offset: 0x0005509C
		internal void DisableKeepAliveOnConnections()
		{
			ArrayList arrayList = new ArrayList();
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				foreach (object obj in this.m_ConnectionList)
				{
					Connection connection = (Connection)obj;
					arrayList.Add(connection);
				}
				this.m_ConnectionList.Clear();
			}
			foreach (object obj2 in arrayList)
			{
				Connection connection2 = (Connection)obj2;
				connection2.CloseOnIdle();
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00056F80 File Offset: 0x00055180
		private Connection FindMatchingConnection(HttpWebRequest request, string connName, out Connection leastbusyConnection)
		{
			bool flag = false;
			leastbusyConnection = null;
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				int num = int.MaxValue;
				foreach (object obj in this.m_ConnectionList)
				{
					Connection connection = (Connection)obj;
					if (connection.LockedRequest == request)
					{
						leastbusyConnection = connection;
						return connection;
					}
					if (!connection.NonKeepAliveRequestPipelined && connection.BusyCount < num && connection.LockedRequest == null)
					{
						leastbusyConnection = connection;
						num = connection.BusyCount;
						if (num == 0)
						{
							flag = true;
						}
					}
				}
				if (!flag && this.CurrentConnections < this.ConnectionLimit)
				{
					leastbusyConnection = new Connection(this);
				}
			}
			return null;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00057074 File Offset: 0x00055274
		private Connection FindConnectionAuthenticationGroup(HttpWebRequest request, string connName)
		{
			Connection connection = null;
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				Connection connection2 = this.FindMatchingConnection(request, connName, out connection);
				if (connection2 != null)
				{
					connection2.MarkAsReserved();
					return connection2;
				}
				if (this.AuthenticationRequestQueue.Count == 0)
				{
					if (connection != null)
					{
						if (request.LockConnection)
						{
							this.m_NtlmNegGroup = true;
							this.m_IISVersion = connection.IISVersion;
						}
						if (request.LockConnection || (this.m_NtlmNegGroup && !request.Pipelined && request.UnsafeOrProxyAuthenticatedConnectionSharing && this.m_IISVersion >= 6))
						{
							connection.LockedRequest = request;
						}
						connection.MarkAsReserved();
						return connection;
					}
				}
				else if (connection != null)
				{
					this.AsyncWaitHandle.Set();
				}
				this.AuthenticationRequestQueue.Enqueue(request);
			}
			Connection connection3;
			for (;;)
			{
				request.AbortDelegate = this.m_AbortDelegate;
				if (!request.Aborted)
				{
					this.AsyncWaitHandle.WaitOne();
				}
				ArrayList connectionList2 = this.m_ConnectionList;
				lock (connectionList2)
				{
					if (!request.Aborted)
					{
						this.FindMatchingConnection(request, connName, out connection);
						if (this.AuthenticationRequestQueue.Peek() == request)
						{
							this.AuthenticationRequestQueue.Dequeue();
							if (connection != null)
							{
								if (request.LockConnection)
								{
									this.m_NtlmNegGroup = true;
									this.m_IISVersion = connection.IISVersion;
								}
								if (request.LockConnection || (this.m_NtlmNegGroup && !request.Pipelined && request.UnsafeOrProxyAuthenticatedConnectionSharing && this.m_IISVersion >= 6))
								{
									connection.LockedRequest = request;
								}
								connection.MarkAsReserved();
								connection3 = connection;
								break;
							}
							this.AuthenticationRequestQueue.Enqueue(request);
						}
						if (connection == null)
						{
							this.AsyncWaitHandle.Reset();
						}
						continue;
					}
					this.PruneAbortedRequests();
					connection3 = null;
				}
				break;
			}
			return connection3;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00057254 File Offset: 0x00055454
		internal Connection FindConnection(HttpWebRequest request, string connName, out bool forcedsubmit)
		{
			Connection connection = null;
			Connection connection2 = null;
			bool flag = false;
			ArrayList arrayList = new ArrayList();
			forcedsubmit = false;
			if (this.m_AuthenticationGroup || request.LockConnection)
			{
				this.m_AuthenticationGroup = true;
				return this.FindConnectionAuthenticationGroup(request, connName);
			}
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				int num = int.MaxValue;
				bool flag3 = false;
				foreach (object obj in this.m_ConnectionList)
				{
					Connection connection3 = (Connection)obj;
					bool flag4 = false;
					if (!connection3.IsInitalizing && !connection3.NetworkStream.Connected)
					{
						arrayList.Add(connection3);
					}
					else if (flag3)
					{
						flag4 = !connection3.NonKeepAliveRequestPipelined && num > connection3.BusyCount;
					}
					else
					{
						flag4 = !connection3.NonKeepAliveRequestPipelined || num > connection3.BusyCount;
					}
					if (flag4)
					{
						connection = connection3;
						num = connection3.BusyCount;
						if (!flag3)
						{
							flag3 = !connection3.NonKeepAliveRequestPipelined;
						}
						if (flag3 && num == 0)
						{
							flag = true;
							break;
						}
					}
				}
				foreach (object obj2 in arrayList)
				{
					Connection connection4 = (Connection)obj2;
					connection4.RemoveFromConnectionList();
				}
				if (!flag && this.CurrentConnections < this.ConnectionLimit)
				{
					connection2 = new Connection(this);
					forcedsubmit = false;
				}
				else
				{
					connection2 = connection;
					forcedsubmit = !flag3;
				}
				connection2.MarkAsReserved();
			}
			return connection2;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00057440 File Offset: 0x00055640
		[Conditional("DEBUG")]
		internal void DebugMembers(int requestHash)
		{
			foreach (object obj in this.m_ConnectionList)
			{
				Connection connection = (Connection)obj;
			}
		}

		// Token: 0x0400135C RID: 4956
		private const int DefaultConnectionListSize = 3;

		// Token: 0x0400135D RID: 4957
		private ServicePoint m_ServicePoint;

		// Token: 0x0400135E RID: 4958
		private string m_Name;

		// Token: 0x0400135F RID: 4959
		private int m_ConnectionLimit;

		// Token: 0x04001360 RID: 4960
		private ArrayList m_ConnectionList;

		// Token: 0x04001361 RID: 4961
		private object m_Event;

		// Token: 0x04001362 RID: 4962
		private Queue m_AuthenticationRequestQueue;

		// Token: 0x04001363 RID: 4963
		internal bool m_AuthenticationGroup;

		// Token: 0x04001364 RID: 4964
		private HttpAbortDelegate m_AbortDelegate;

		// Token: 0x04001365 RID: 4965
		private bool m_NtlmNegGroup;

		// Token: 0x04001366 RID: 4966
		private int m_IISVersion = -1;

		// Token: 0x04001367 RID: 4967
		private int m_ActiveConnections;

		// Token: 0x04001368 RID: 4968
		private TimerThread.Timer m_ExpiringTimer;
	}
}

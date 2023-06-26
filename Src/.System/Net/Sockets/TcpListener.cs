using System;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Listens for connections from TCP network clients.</summary>
	// Token: 0x02000387 RID: 903
	public class TcpListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class with the specified local endpoint.</summary>
		/// <param name="localEP">An <see cref="T:System.Net.IPEndPoint" /> that represents the local endpoint to which to bind the listener <see cref="T:System.Net.Sockets.Socket" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localEP" /> is <see langword="null" />.</exception>
		// Token: 0x060021B1 RID: 8625 RVA: 0x000A173C File Offset: 0x0009F93C
		public TcpListener(IPEndPoint localEP)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpListener", localEP);
			}
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			this.m_ServerSocketEP = localEP;
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpListener", null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class that listens for incoming connection attempts on the specified local IP address and port number.</summary>
		/// <param name="localaddr">An <see cref="T:System.Net.IPAddress" /> that represents the local IP address.</param>
		/// <param name="port">The port on which to listen for incoming connection attempts.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localaddr" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x060021B2 RID: 8626 RVA: 0x000A17AC File Offset: 0x0009F9AC
		public TcpListener(IPAddress localaddr, int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpListener", localaddr);
			}
			if (localaddr == null)
			{
				throw new ArgumentNullException("localaddr");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_ServerSocketEP = new IPEndPoint(localaddr, port);
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpListener", null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class that listens on the specified port.</summary>
		/// <param name="port">The port on which to listen for incoming connection attempts.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x060021B3 RID: 8627 RVA: 0x000A1838 File Offset: 0x0009FA38
		[Obsolete("This method has been deprecated. Please use TcpListener(IPAddress localaddr, int port) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public TcpListener(int port)
		{
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_ServerSocketEP = new IPEndPoint(IPAddress.Any, port);
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
		}

		/// <summary>Creates a new <see cref="T:System.Net.Sockets.TcpListener" /> instance to listen on the specified port.</summary>
		/// <param name="port">The port on which to listen for incoming connection attempts.</param>
		/// <returns>A new <see cref="T:System.Net.Sockets.TcpListener" /> instance to listen on the specified port.</returns>
		// Token: 0x060021B4 RID: 8628 RVA: 0x000A1888 File Offset: 0x0009FA88
		public static TcpListener Create(int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "TcpListener.Create", "Port: " + port.ToString());
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			TcpListener tcpListener = new TcpListener(IPAddress.IPv6Any, port);
			tcpListener.Server.DualMode = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "TcpListener.Create", "Port: " + port.ToString());
			}
			return tcpListener;
		}

		/// <summary>Gets the underlying network <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The underlying <see cref="T:System.Net.Sockets.Socket" />.</returns>
		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x000A190F File Offset: 0x0009FB0F
		public Socket Server
		{
			get
			{
				return this.m_ServerSocket;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.Sockets.TcpListener" /> is actively listening for client connections.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="T:System.Net.Sockets.TcpListener" /> is actively listening; otherwise, <see langword="false" />.</returns>
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x000A1917 File Offset: 0x0009FB17
		protected bool Active
		{
			get
			{
				return this.m_Active;
			}
		}

		/// <summary>Gets the underlying <see cref="T:System.Net.EndPoint" /> of the current <see cref="T:System.Net.Sockets.TcpListener" />.</summary>
		/// <returns>The <see cref="T:System.Net.EndPoint" /> to which the <see cref="T:System.Net.Sockets.Socket" /> is bound.</returns>
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x000A191F File Offset: 0x0009FB1F
		public EndPoint LocalEndpoint
		{
			get
			{
				if (!this.m_Active)
				{
					return this.m_ServerSocketEP;
				}
				return this.m_ServerSocket.LocalEndPoint;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.TcpListener" /> allows only one underlying socket to listen to a specific port.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.TcpListener" /> allows only one <see cref="T:System.Net.Sockets.TcpListener" /> to listen to a specific port; otherwise, <see langword="false" />. . The default is <see langword="true" /> for Windows Server 2003 and Windows XP Service Pack 2 and later, and <see langword="false" /> for all other versions.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.TcpListener" /> has been started. Call the <see cref="M:System.Net.Sockets.TcpListener.Stop" /> method and then set the <see cref="P:System.Net.Sockets.Socket.ExclusiveAddressUse" /> property.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x000A193B File Offset: 0x0009FB3B
		// (set) Token: 0x060021B9 RID: 8633 RVA: 0x000A1948 File Offset: 0x0009FB48
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.m_ServerSocket.ExclusiveAddressUse;
			}
			set
			{
				if (this.m_Active)
				{
					throw new InvalidOperationException(SR.GetString("net_tcplistener_mustbestopped"));
				}
				this.m_ServerSocket.ExclusiveAddressUse = value;
				this.m_ExclusiveAddressUse = value;
			}
		}

		/// <summary>Enables or disables Network Address Translation (NAT) traversal on a <see cref="T:System.Net.Sockets.TcpListener" /> instance.</summary>
		/// <param name="allowed">A Boolean value that specifies whether to enable or disable NAT traversal.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.Sockets.TcpListener.AllowNatTraversal(System.Boolean)" /> method was called after calling the <see cref="M:System.Net.Sockets.TcpListener.Start" /> method</exception>
		// Token: 0x060021BA RID: 8634 RVA: 0x000A1975 File Offset: 0x0009FB75
		public void AllowNatTraversal(bool allowed)
		{
			if (this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_tcplistener_mustbestopped"));
			}
			if (allowed)
			{
				this.m_ServerSocket.SetIPProtectionLevel(IPProtectionLevel.Unrestricted);
				return;
			}
			this.m_ServerSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
		}

		/// <summary>Starts listening for incoming connection requests.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation for a detailed description of the error.</exception>
		// Token: 0x060021BB RID: 8635 RVA: 0x000A19AD File Offset: 0x0009FBAD
		public void Start()
		{
			this.Start(int.MaxValue);
		}

		/// <summary>Starts listening for incoming connection requests with a maximum number of pending connection.</summary>
		/// <param name="backlog">The maximum length of the pending connections queue.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while accessing the socket.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="backlog" /> parameter is less than zero or exceeds the maximum number of permitted connections.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is null.</exception>
		// Token: 0x060021BC RID: 8636 RVA: 0x000A19BC File Offset: 0x0009FBBC
		public void Start(int backlog)
		{
			if (backlog > 2147483647 || backlog < 0)
			{
				throw new ArgumentOutOfRangeException("backlog");
			}
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Start", null);
			}
			if (this.m_ServerSocket == null)
			{
				throw new InvalidOperationException(SR.GetString("net_InvalidSocketHandle"));
			}
			if (this.m_Active)
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Sockets, this, "Start", null);
				}
				return;
			}
			this.m_ServerSocket.Bind(this.m_ServerSocketEP);
			try
			{
				this.m_ServerSocket.Listen(backlog);
			}
			catch (SocketException)
			{
				this.Stop();
				throw;
			}
			this.m_Active = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Start", null);
			}
		}

		/// <summary>Closes the listener.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation for a detailed description of the error.</exception>
		// Token: 0x060021BD RID: 8637 RVA: 0x000A1A88 File Offset: 0x0009FC88
		public void Stop()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Stop", null);
			}
			if (this.m_ServerSocket != null)
			{
				this.m_ServerSocket.Close();
				this.m_ServerSocket = null;
			}
			this.m_Active = false;
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (this.m_ExclusiveAddressUse)
			{
				this.m_ServerSocket.ExclusiveAddressUse = true;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Stop", null);
			}
		}

		/// <summary>Determines if there are pending connection requests.</summary>
		/// <returns>
		///   <see langword="true" /> if connections are pending; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />.</exception>
		// Token: 0x060021BE RID: 8638 RVA: 0x000A1B12 File Offset: 0x0009FD12
		public bool Pending()
		{
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_stopped"));
			}
			return this.m_ServerSocket.Poll(0, SelectMode.SelectRead);
		}

		/// <summary>Accepts a pending connection request.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />.</exception>
		// Token: 0x060021BF RID: 8639 RVA: 0x000A1B3C File Offset: 0x0009FD3C
		public Socket AcceptSocket()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "AcceptSocket", null);
			}
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_stopped"));
			}
			Socket socket = this.m_ServerSocket.Accept();
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "AcceptSocket", socket);
			}
			return socket;
		}

		/// <summary>Accepts a pending connection request.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.TcpClient" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation for a detailed description of the error.</exception>
		// Token: 0x060021C0 RID: 8640 RVA: 0x000A1BA0 File Offset: 0x0009FDA0
		public TcpClient AcceptTcpClient()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "AcceptTcpClient", null);
			}
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_stopped"));
			}
			Socket socket = this.m_ServerSocket.Accept();
			TcpClient tcpClient = new TcpClient(socket);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "AcceptTcpClient", tcpClient);
			}
			return tcpClient;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object containing information about the accept operation. This object is passed to the <paramref name="callback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous creation of the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x060021C1 RID: 8641 RVA: 0x000A1C0C File Offset: 0x0009FE0C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAcceptSocket(AsyncCallback callback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "BeginAcceptSocket", null);
			}
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_stopped"));
			}
			IAsyncResult asyncResult = this.m_ServerSocket.BeginAccept(callback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "BeginAcceptSocket", null);
			}
			return asyncResult;
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> to handle remote host communication.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> returned by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptSocket(System.AsyncCallback,System.Object)" /> method.</param>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" />.  
		///  The <see cref="T:System.Net.Sockets.Socket" /> used to send and receive data.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> parameter was not created by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptSocket(System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.Sockets.TcpListener.EndAcceptSocket(System.IAsyncResult)" /> method was previously called.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the <see cref="T:System.Net.Sockets.Socket" />.</exception>
		// Token: 0x060021C2 RID: 8642 RVA: 0x000A1C70 File Offset: 0x0009FE70
		public Socket EndAcceptSocket(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "EndAcceptSocket", null);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			Socket socket = ((lazyAsyncResult == null) ? null : (lazyAsyncResult.AsyncObject as Socket));
			if (socket == null)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			Socket socket2 = socket.EndAccept(asyncResult);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "EndAcceptSocket", socket2);
			}
			return socket2;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object containing information about the accept operation. This object is passed to the <paramref name="callback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous creation of the <see cref="T:System.Net.Sockets.TcpClient" />.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x060021C3 RID: 8643 RVA: 0x000A1CF8 File Offset: 0x0009FEF8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "BeginAcceptTcpClient", null);
			}
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_stopped"));
			}
			IAsyncResult asyncResult = this.m_ServerSocket.BeginAccept(callback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "BeginAcceptTcpClient", null);
			}
			return asyncResult;
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.TcpClient" /> to handle remote host communication.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> returned by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptTcpClient(System.AsyncCallback,System.Object)" /> method.</param>
		/// <returns>A <see cref="T:System.Net.Sockets.TcpClient" />.  
		///  The <see cref="T:System.Net.Sockets.TcpClient" /> used to send and receive data.</returns>
		// Token: 0x060021C4 RID: 8644 RVA: 0x000A1D5C File Offset: 0x0009FF5C
		public TcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "EndAcceptTcpClient", null);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			Socket socket = ((lazyAsyncResult == null) ? null : (lazyAsyncResult.AsyncObject as Socket));
			if (socket == null)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			Socket socket2 = socket.EndAccept(asyncResult);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "EndAcceptTcpClient", socket2);
			}
			return new TcpClient(socket2);
		}

		/// <summary>Accepts a pending connection request as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Net.Sockets.Socket" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />.</exception>
		// Token: 0x060021C5 RID: 8645 RVA: 0x000A1DE6 File Offset: 0x0009FFE6
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Socket> AcceptSocketAsync()
		{
			return Task<Socket>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAcceptSocket), new Func<IAsyncResult, Socket>(this.EndAcceptSocket), null);
		}

		/// <summary>Accepts a pending connection request as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Net.Sockets.TcpClient" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation for a detailed description of the error.</exception>
		// Token: 0x060021C6 RID: 8646 RVA: 0x000A1E0B File Offset: 0x000A000B
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<TcpClient> AcceptTcpClientAsync()
		{
			return Task<TcpClient>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAcceptTcpClient), new Func<IAsyncResult, TcpClient>(this.EndAcceptTcpClient), null);
		}

		// Token: 0x04001F2F RID: 7983
		private IPEndPoint m_ServerSocketEP;

		// Token: 0x04001F30 RID: 7984
		private Socket m_ServerSocket;

		// Token: 0x04001F31 RID: 7985
		private bool m_Active;

		// Token: 0x04001F32 RID: 7986
		private bool m_ExclusiveAddressUse;
	}
}

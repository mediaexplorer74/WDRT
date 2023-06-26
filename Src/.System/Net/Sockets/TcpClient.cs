using System;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Provides client connections for TCP network services.</summary>
	// Token: 0x02000386 RID: 902
	public class TcpClient : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpClient" /> class and binds it to the specified local endpoint.</summary>
		/// <param name="localEP">The <see cref="T:System.Net.IPEndPoint" /> to which you bind the TCP <see cref="T:System.Net.Sockets.Socket" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The  <paramref name="localEP" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002186 RID: 8582 RVA: 0x000A0C50 File Offset: 0x0009EE50
		public TcpClient(IPEndPoint localEP)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpClient", localEP);
			}
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			this.m_Family = localEP.AddressFamily;
			this.initialize();
			this.Client.Bind(localEP);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpClient", "");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpClient" /> class.</summary>
		// Token: 0x06002187 RID: 8583 RVA: 0x000A0CCA File Offset: 0x0009EECA
		public TcpClient()
			: this(AddressFamily.InterNetwork)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpClient", null);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpClient", null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpClient" /> class with the specified family.</summary>
		/// <param name="family">The <see cref="P:System.Net.IPAddress.AddressFamily" /> of the IP protocol.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="family" /> parameter is not equal to AddressFamily.InterNetwork  
		///  -or-  
		///  The <paramref name="family" /> parameter is not equal to AddressFamily.InterNetworkV6</exception>
		// Token: 0x06002188 RID: 8584 RVA: 0x000A0D04 File Offset: 0x0009EF04
		public TcpClient(AddressFamily family)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpClient", family);
			}
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException(SR.GetString("net_protocol_invalid_family", new object[] { "TCP" }), "family");
			}
			this.m_Family = family;
			this.initialize();
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpClient", null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpClient" /> class and connects to the specified port on the specified host.</summary>
		/// <param name="hostname">The DNS name of the remote host to which you intend to connect.</param>
		/// <param name="port">The port number of the remote host to which you intend to connect.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostname" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="port" /> parameter is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06002189 RID: 8585 RVA: 0x000A0D8C File Offset: 0x0009EF8C
		public TcpClient(string hostname, int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpClient", hostname);
			}
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			try
			{
				this.Connect(hostname, port);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (this.m_ClientSocket != null)
				{
					this.m_ClientSocket.Close();
				}
				throw ex;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpClient", null);
			}
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000A0E44 File Offset: 0x0009F044
		internal TcpClient(Socket acceptedSocket)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpClient", acceptedSocket);
			}
			this.Client = acceptedSocket;
			this.m_Active = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpClient", null);
			}
		}

		/// <summary>Gets or sets the underlying <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The underlying network <see cref="T:System.Net.Sockets.Socket" />.</returns>
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x000A0E9C File Offset: 0x0009F09C
		// (set) Token: 0x0600218C RID: 8588 RVA: 0x000A0EA4 File Offset: 0x0009F0A4
		public Socket Client
		{
			get
			{
				return this.m_ClientSocket;
			}
			set
			{
				this.m_ClientSocket = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether a connection has been made.</summary>
		/// <returns>
		///   <see langword="true" /> if the connection has been made; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x000A0EAD File Offset: 0x0009F0AD
		// (set) Token: 0x0600218E RID: 8590 RVA: 0x000A0EB5 File Offset: 0x0009F0B5
		protected bool Active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
			}
		}

		/// <summary>Gets the amount of data that has been received from the network and is available to be read.</summary>
		/// <returns>The number of bytes of data received from the network and available to be read.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x000A0EBE File Offset: 0x0009F0BE
		public int Available
		{
			get
			{
				return this.m_ClientSocket.Available;
			}
		}

		/// <summary>Gets a value indicating whether the underlying <see cref="T:System.Net.Sockets.Socket" /> for a <see cref="T:System.Net.Sockets.TcpClient" /> is connected to a remote host.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Net.Sockets.TcpClient.Client" /> socket was connected to a remote resource as of the most recent operation; otherwise, <see langword="false" />.</returns>
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x000A0ECB File Offset: 0x0009F0CB
		public bool Connected
		{
			get
			{
				return this.m_ClientSocket.Connected;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.TcpClient" /> allows only one client to use a port.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.TcpClient" /> allows only one client to use a specific port; otherwise, <see langword="false" />. The default is <see langword="true" /> for Windows Server 2003 and Windows XP Service Pack 2 and later, and <see langword="false" /> for all other versions.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x000A0ED8 File Offset: 0x0009F0D8
		// (set) Token: 0x06002192 RID: 8594 RVA: 0x000A0EE5 File Offset: 0x0009F0E5
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.m_ClientSocket.ExclusiveAddressUse;
			}
			set
			{
				this.m_ClientSocket.ExclusiveAddressUse = value;
			}
		}

		/// <summary>Connects the client to the specified port on the specified host.</summary>
		/// <param name="hostname">The DNS name of the remote host to which you intend to connect.</param>
		/// <param name="port">The port number of the remote host to which you intend to connect.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostname" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="port" /> parameter is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.TcpClient" /> is closed.</exception>
		// Token: 0x06002193 RID: 8595 RVA: 0x000A0EF4 File Offset: 0x0009F0F4
		public void Connect(string hostname, int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", hostname);
			}
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (this.m_Active)
			{
				throw new SocketException(SocketError.IsConnected);
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
			Exception ex = null;
			Socket socket = null;
			Socket socket2 = null;
			try
			{
				if (this.m_ClientSocket == null)
				{
					if (Socket.OSSupportsIPv4)
					{
						socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					}
					if (Socket.OSSupportsIPv6)
					{
						socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
					}
				}
				foreach (IPAddress ipaddress in hostAddresses)
				{
					try
					{
						if (this.m_ClientSocket == null)
						{
							if (ipaddress.AddressFamily == AddressFamily.InterNetwork && socket2 != null)
							{
								socket2.Connect(ipaddress, port);
								this.m_ClientSocket = socket2;
								if (socket != null)
								{
									socket.Close();
								}
							}
							else if (socket != null)
							{
								socket.Connect(ipaddress, port);
								this.m_ClientSocket = socket;
								if (socket2 != null)
								{
									socket2.Close();
								}
							}
							this.m_Family = ipaddress.AddressFamily;
							this.m_Active = true;
							break;
						}
						if (ipaddress.AddressFamily == this.m_Family)
						{
							this.Connect(new IPEndPoint(ipaddress, port));
							this.m_Active = true;
							break;
						}
					}
					catch (Exception ex2)
					{
						if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
						{
							throw;
						}
						ex = ex2;
					}
				}
			}
			catch (Exception ex3)
			{
				if (ex3 is ThreadAbortException || ex3 is StackOverflowException || ex3 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex3;
			}
			finally
			{
				if (!this.m_Active)
				{
					if (socket != null)
					{
						socket.Close();
					}
					if (socket2 != null)
					{
						socket2.Close();
					}
					if (ex != null)
					{
						throw ex;
					}
					throw new SocketException(SocketError.NotConnected);
				}
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Connect", null);
			}
		}

		/// <summary>Connects the client to a remote TCP host using the specified IP address and port number.</summary>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> of the host to which you intend to connect.</param>
		/// <param name="port">The port number to which you intend to connect.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.TcpClient" /> is closed.</exception>
		// Token: 0x06002194 RID: 8596 RVA: 0x000A1124 File Offset: 0x0009F324
		public void Connect(IPAddress address, int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", address);
			}
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			IPEndPoint ipendPoint = new IPEndPoint(address, port);
			this.Connect(ipendPoint);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Connect", null);
			}
		}

		/// <summary>Connects the client to a remote TCP host using the specified remote network endpoint.</summary>
		/// <param name="remoteEP">The <see cref="T:System.Net.IPEndPoint" /> to which you intend to connect.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="remoteEp" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.TcpClient" /> is closed.</exception>
		// Token: 0x06002195 RID: 8597 RVA: 0x000A11AC File Offset: 0x0009F3AC
		public void Connect(IPEndPoint remoteEP)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", remoteEP);
			}
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			this.Client.Connect(remoteEP);
			this.m_Active = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Connect", null);
			}
		}

		/// <summary>Connects the client to a remote TCP host using the specified IP addresses and port number.</summary>
		/// <param name="ipAddresses">The <see cref="T:System.Net.IPAddress" /> array of the host to which you intend to connect.</param>
		/// <param name="port">The port number to which you intend to connect.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ipAddresses" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets that use the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> flag or the <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> flag.</exception>
		// Token: 0x06002196 RID: 8598 RVA: 0x000A1224 File Offset: 0x0009F424
		public void Connect(IPAddress[] ipAddresses, int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", ipAddresses);
			}
			this.Client.Connect(ipAddresses, port);
			this.m_Active = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Connect", null);
			}
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The remote host is specified by a host name (<see cref="T:System.String" />) and a port number (<see cref="T:System.Int32" />).</summary>
		/// <param name="host">The name of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous connection.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		// Token: 0x06002197 RID: 8599 RVA: 0x000A1278 File Offset: 0x0009F478
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "BeginConnect", host);
			}
			IAsyncResult asyncResult = this.Client.BeginConnect(host, port, requestCallback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "BeginConnect", null);
			}
			return asyncResult;
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The remote host is specified by an <see cref="T:System.Net.IPAddress" /> and a port number (<see cref="T:System.Int32" />).</summary>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous connection.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		// Token: 0x06002198 RID: 8600 RVA: 0x000A12C8 File Offset: 0x0009F4C8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback requestCallback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "BeginConnect", address);
			}
			IAsyncResult asyncResult = this.Client.BeginConnect(address, port, requestCallback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "BeginConnect", null);
			}
			return asyncResult;
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The remote host is specified by an <see cref="T:System.Net.IPAddress" /> array and a port number (<see cref="T:System.Int32" />).</summary>
		/// <param name="addresses">At least one <see cref="T:System.Net.IPAddress" /> that designates the remote hosts.</param>
		/// <param name="port">The port number of the remote hosts.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous connection.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="addresses" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		// Token: 0x06002199 RID: 8601 RVA: 0x000A1318 File Offset: 0x0009F518
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback requestCallback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "BeginConnect", addresses);
			}
			IAsyncResult asyncResult = this.Client.BeginConnect(addresses, port, requestCallback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "BeginConnect", null);
			}
			return asyncResult;
		}

		/// <summary>Ends a pending asynchronous connection attempt.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object returned by a call to <see cref="Overload:System.Net.Sockets.TcpClient.BeginConnect" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> parameter was not returned by a call to a <see cref="Overload:System.Net.Sockets.TcpClient.BeginConnect" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.Sockets.TcpClient.EndConnect(System.IAsyncResult)" /> method was previously called for the asynchronous connection.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the <see cref="T:System.Net.Sockets.Socket" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x0600219A RID: 8602 RVA: 0x000A1368 File Offset: 0x0009F568
		public void EndConnect(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "EndConnect", asyncResult);
			}
			this.Client.EndConnect(asyncResult);
			this.m_Active = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "EndConnect", null);
			}
		}

		/// <summary>Connects the client to a remote TCP host using the specified IP address and port number as an asynchronous operation.</summary>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> of the host to which you intend to connect.</param>
		/// <param name="port">The port number to which you intend to connect.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.TcpClient" /> is closed.</exception>
		// Token: 0x0600219B RID: 8603 RVA: 0x000A13B8 File Offset: 0x0009F5B8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task ConnectAsync(IPAddress address, int port)
		{
			return Task.Factory.FromAsync<IPAddress, int>(new Func<IPAddress, int, AsyncCallback, object, IAsyncResult>(this.BeginConnect), new Action<IAsyncResult>(this.EndConnect), address, port, null);
		}

		/// <summary>Connects the client to the specified TCP port on the specified host as an asynchronous operation.</summary>
		/// <param name="host">The DNS name of the remote host to which you intend to connect.</param>
		/// <param name="port">The port number of the remote host to which you intend to connect.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostname" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="port" /> parameter is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.TcpClient" /> is closed.</exception>
		// Token: 0x0600219C RID: 8604 RVA: 0x000A13DF File Offset: 0x0009F5DF
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task ConnectAsync(string host, int port)
		{
			return Task.Factory.FromAsync<string, int>(new Func<string, int, AsyncCallback, object, IAsyncResult>(this.BeginConnect), new Action<IAsyncResult>(this.EndConnect), host, port, null);
		}

		/// <summary>Connects the client to a remote TCP host using the specified IP addresses and port number as an asynchronous operation.</summary>
		/// <param name="addresses">The <see cref="T:System.Net.IPAddress" /> array of the host to which you intend to connect.</param>
		/// <param name="port">The port number to which you intend to connect.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ipAddresses" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets that use the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> flag or the <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> flag.</exception>
		// Token: 0x0600219D RID: 8605 RVA: 0x000A1406 File Offset: 0x0009F606
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task ConnectAsync(IPAddress[] addresses, int port)
		{
			return Task.Factory.FromAsync<IPAddress[], int>(new Func<IPAddress[], int, AsyncCallback, object, IAsyncResult>(this.BeginConnect), new Action<IAsyncResult>(this.EndConnect), addresses, port, null);
		}

		/// <summary>Returns the <see cref="T:System.Net.Sockets.NetworkStream" /> used to send and receive data.</summary>
		/// <returns>The underlying <see cref="T:System.Net.Sockets.NetworkStream" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.TcpClient" /> is not connected to a remote host.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.TcpClient" /> has been closed.</exception>
		// Token: 0x0600219E RID: 8606 RVA: 0x000A1430 File Offset: 0x0009F630
		public NetworkStream GetStream()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "GetStream", "");
			}
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.Client.Connected)
			{
				throw new InvalidOperationException(SR.GetString("net_notconnected"));
			}
			if (this.m_DataStream == null)
			{
				this.m_DataStream = new NetworkStream(this.Client, true);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "GetStream", this.m_DataStream);
			}
			return this.m_DataStream;
		}

		/// <summary>Disposes this <see cref="T:System.Net.Sockets.TcpClient" /> instance and requests that the underlying TCP connection be closed.</summary>
		// Token: 0x0600219F RID: 8607 RVA: 0x000A14CC File Offset: 0x0009F6CC
		public void Close()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Close", "");
			}
			((IDisposable)this).Dispose();
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Close", "");
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.TcpClient" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">Set to <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060021A0 RID: 8608 RVA: 0x000A150C File Offset: 0x0009F70C
		protected virtual void Dispose(bool disposing)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Dispose", "");
			}
			if (this.m_CleanedUp)
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Sockets, this, "Dispose", "");
				}
				return;
			}
			if (disposing)
			{
				IDisposable dataStream = this.m_DataStream;
				if (dataStream != null)
				{
					dataStream.Dispose();
				}
				else
				{
					Socket client = this.Client;
					if (client != null)
					{
						try
						{
							client.InternalShutdown(SocketShutdown.Both);
						}
						finally
						{
							client.Close();
							this.Client = null;
						}
					}
				}
				GC.SuppressFinalize(this);
			}
			this.m_CleanedUp = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Dispose", "");
			}
		}

		/// <summary>Releases the managed and unmanaged resources used by the <see cref="T:System.Net.Sockets.TcpClient" />.</summary>
		// Token: 0x060021A1 RID: 8609 RVA: 0x000A15CC File Offset: 0x0009F7CC
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Frees resources used by the <see cref="T:System.Net.Sockets.TcpClient" /> class.</summary>
		// Token: 0x060021A2 RID: 8610 RVA: 0x000A15D8 File Offset: 0x0009F7D8
		~TcpClient()
		{
			this.Dispose(false);
		}

		/// <summary>Gets or sets the size of the receive buffer.</summary>
		/// <returns>The size of the receive buffer, in bytes. The default value is 8192 bytes.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when setting the buffer size.  
		///  -or-  
		///  In .NET Compact Framework applications, you cannot set this property. For a workaround, see the Platform Note in Remarks.</exception>
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x000A1608 File Offset: 0x0009F808
		// (set) Token: 0x060021A4 RID: 8612 RVA: 0x000A161A File Offset: 0x0009F81A
		public int ReceiveBufferSize
		{
			get
			{
				return this.numericOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer);
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, value);
			}
		}

		/// <summary>Gets or sets the size of the send buffer.</summary>
		/// <returns>The size of the send buffer, in bytes. The default value is 8192 bytes.</returns>
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000A1632 File Offset: 0x0009F832
		// (set) Token: 0x060021A6 RID: 8614 RVA: 0x000A1644 File Offset: 0x0009F844
		public int SendBufferSize
		{
			get
			{
				return this.numericOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer);
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, value);
			}
		}

		/// <summary>Gets or sets the amount of time a <see cref="T:System.Net.Sockets.TcpClient" /> will wait to receive data once a read operation is initiated.</summary>
		/// <returns>The time-out value of the connection in milliseconds. The default value is 0.</returns>
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x000A165C File Offset: 0x0009F85C
		// (set) Token: 0x060021A8 RID: 8616 RVA: 0x000A166E File Offset: 0x0009F86E
		public int ReceiveTimeout
		{
			get
			{
				return this.numericOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
			}
		}

		/// <summary>Gets or sets the amount of time a <see cref="T:System.Net.Sockets.TcpClient" /> will wait for a send operation to complete successfully.</summary>
		/// <returns>The send time-out value, in milliseconds. The default is 0.</returns>
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x000A1686 File Offset: 0x0009F886
		// (set) Token: 0x060021AA RID: 8618 RVA: 0x000A1698 File Offset: 0x0009F898
		public int SendTimeout
		{
			get
			{
				return this.numericOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
			}
		}

		/// <summary>Gets or sets information about the linger state of the associated socket.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.LingerOption" />. By default, lingering is disabled.</returns>
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x000A16B0 File Offset: 0x0009F8B0
		// (set) Token: 0x060021AC RID: 8620 RVA: 0x000A16CC File Offset: 0x0009F8CC
		public LingerOption LingerState
		{
			get
			{
				return (LingerOption)this.Client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger);
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, value);
			}
		}

		/// <summary>Gets or sets a value that disables a delay when send or receive buffers are not full.</summary>
		/// <returns>
		///   <see langword="true" /> if the delay is disabled; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x000A16E4 File Offset: 0x0009F8E4
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x000A16F3 File Offset: 0x0009F8F3
		public bool NoDelay
		{
			get
			{
				return this.numericOption(SocketOptionLevel.Tcp, SocketOptionName.Debug) != 0;
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug, value ? 1 : 0);
			}
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000A1709 File Offset: 0x0009F909
		private void initialize()
		{
			this.Client = new Socket(this.m_Family, SocketType.Stream, ProtocolType.Tcp);
			this.m_Active = false;
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000A1725 File Offset: 0x0009F925
		private int numericOption(SocketOptionLevel optionLevel, SocketOptionName optionName)
		{
			return (int)this.Client.GetSocketOption(optionLevel, optionName);
		}

		// Token: 0x04001F2A RID: 7978
		private Socket m_ClientSocket;

		// Token: 0x04001F2B RID: 7979
		private bool m_Active;

		// Token: 0x04001F2C RID: 7980
		private NetworkStream m_DataStream;

		// Token: 0x04001F2D RID: 7981
		private AddressFamily m_Family = AddressFamily.InterNetwork;

		// Token: 0x04001F2E RID: 7982
		private bool m_CleanedUp;
	}
}

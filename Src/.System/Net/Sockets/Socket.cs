using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Implements the Berkeley sockets interface.</summary>
	// Token: 0x02000376 RID: 886
	public class Socket : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.Socket" /> class using the specified socket type and protocol.</summary>
		/// <param name="socketType">One of the <see cref="T:System.Net.Sockets.SocketType" /> values.</param>
		/// <param name="protocolType">One of the <see cref="T:System.Net.Sockets.ProtocolType" /> values.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">The combination of  <paramref name="socketType" /> and <paramref name="protocolType" /> results in an invalid socket.</exception>
		// Token: 0x06002017 RID: 8215 RVA: 0x00095E99 File Offset: 0x00094099
		public Socket(SocketType socketType, ProtocolType protocolType)
			: this(AddressFamily.InterNetworkV6, socketType, protocolType)
		{
			this.DualMode = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.Socket" /> class using the specified address family, socket type and protocol.</summary>
		/// <param name="addressFamily">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</param>
		/// <param name="socketType">One of the <see cref="T:System.Net.Sockets.SocketType" /> values.</param>
		/// <param name="protocolType">One of the <see cref="T:System.Net.Sockets.ProtocolType" /> values.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">The combination of <paramref name="addressFamily" />, <paramref name="socketType" />, and <paramref name="protocolType" /> results in an invalid socket.</exception>
		// Token: 0x06002018 RID: 8216 RVA: 0x00095EAC File Offset: 0x000940AC
		public Socket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			this.willBlock = true;
			this.willBlockInternal = true;
			this.m_CloseTimeout = -1;
			base..ctor();
			Socket.s_LoggingEnabled = Logging.On;
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Socket", addressFamily);
			}
			Socket.InitializeSockets();
			this.m_Handle = SafeCloseSocket.CreateWSASocket(addressFamily, socketType, protocolType);
			if (this.m_Handle.IsInvalid)
			{
				throw new SocketException();
			}
			this.addressFamily = addressFamily;
			this.socketType = socketType;
			this.protocolType = protocolType;
			IPProtectionLevel ipprotectionLevel = SettingsSectionInternal.Section.IPProtectionLevel;
			if (ipprotectionLevel != IPProtectionLevel.Unspecified)
			{
				this.SetIPProtectionLevel(ipprotectionLevel);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Socket", null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.Socket" /> class using the specified value returned from <see cref="M:System.Net.Sockets.Socket.DuplicateAndClose(System.Int32)" />.</summary>
		/// <param name="socketInformation">The socket information returned by <see cref="M:System.Net.Sockets.Socket.DuplicateAndClose(System.Int32)" />.</param>
		// Token: 0x06002019 RID: 8217 RVA: 0x00095F6C File Offset: 0x0009416C
		public unsafe Socket(SocketInformation socketInformation)
		{
			this.willBlock = true;
			this.willBlockInternal = true;
			this.m_CloseTimeout = -1;
			base..ctor();
			Socket.s_LoggingEnabled = Logging.On;
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Socket", this.addressFamily);
			}
			ExceptionHelper.UnrestrictedSocketPermission.Demand();
			Socket.InitializeSockets();
			if (socketInformation.ProtocolInformation == null || socketInformation.ProtocolInformation.Length < Socket.protocolInformationSize)
			{
				throw new ArgumentException(SR.GetString("net_sockets_invalid_socketinformation"), "socketInformation.ProtocolInformation");
			}
			byte[] array;
			byte* ptr;
			if ((array = socketInformation.ProtocolInformation) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			this.m_Handle = SafeCloseSocket.CreateWSASocket(ptr);
			UnsafeNclNativeMethods.OSSOCK.WSAPROTOCOL_INFO wsaprotocol_INFO = (UnsafeNclNativeMethods.OSSOCK.WSAPROTOCOL_INFO)Marshal.PtrToStructure((IntPtr)((void*)ptr), typeof(UnsafeNclNativeMethods.OSSOCK.WSAPROTOCOL_INFO));
			this.addressFamily = wsaprotocol_INFO.iAddressFamily;
			this.socketType = (SocketType)wsaprotocol_INFO.iSocketType;
			this.protocolType = (ProtocolType)wsaprotocol_INFO.iProtocol;
			array = null;
			if (this.m_Handle.IsInvalid)
			{
				SocketException ex = new SocketException();
				if (ex.ErrorCode == 10022)
				{
					throw new ArgumentException(SR.GetString("net_sockets_invalid_socketinformation"), "socketInformation");
				}
				throw ex;
			}
			else
			{
				if (this.addressFamily != AddressFamily.InterNetwork && this.addressFamily != AddressFamily.InterNetworkV6)
				{
					throw new NotSupportedException(SR.GetString("net_invalidversion"));
				}
				this.m_IsConnected = socketInformation.IsConnected;
				this.willBlock = !socketInformation.IsNonBlocking;
				this.InternalSetBlocking(this.willBlock);
				this.isListening = socketInformation.IsListening;
				this.UseOnlyOverlappedIO = socketInformation.UseOnlyOverlappedIO;
				if (socketInformation.RemoteEndPoint != null)
				{
					this.m_RightEndPoint = socketInformation.RemoteEndPoint;
					this.m_RemoteEndPoint = socketInformation.RemoteEndPoint;
				}
				else
				{
					EndPoint endPoint = null;
					if (this.addressFamily == AddressFamily.InterNetwork)
					{
						endPoint = IPEndPoint.Any;
					}
					else if (this.addressFamily == AddressFamily.InterNetworkV6)
					{
						endPoint = IPEndPoint.IPv6Any;
					}
					SocketAddress socketAddress = endPoint.Serialize();
					SocketError socketError;
					try
					{
						socketError = UnsafeNclNativeMethods.OSSOCK.getsockname(this.m_Handle, socketAddress.m_Buffer, ref socketAddress.m_Size);
					}
					catch (ObjectDisposedException)
					{
						socketError = SocketError.NotSocket;
					}
					if (socketError == SocketError.Success)
					{
						try
						{
							this.m_RightEndPoint = endPoint.Create(socketAddress);
						}
						catch
						{
						}
					}
				}
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exit(Logging.Sockets, this, "Socket", null);
				}
				return;
			}
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000961D4 File Offset: 0x000943D4
		private Socket(SafeCloseSocket fd)
		{
			this.willBlock = true;
			this.willBlockInternal = true;
			this.m_CloseTimeout = -1;
			base..ctor();
			Socket.s_LoggingEnabled = Logging.On;
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Socket", null);
			}
			Socket.InitializeSockets();
			if (fd == null || fd.IsInvalid)
			{
				throw new ArgumentException(SR.GetString("net_InvalidSocketHandle"));
			}
			this.m_Handle = fd;
			this.addressFamily = AddressFamily.Unknown;
			this.socketType = SocketType.Unknown;
			this.protocolType = ProtocolType.Unknown;
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Socket", null);
			}
		}

		/// <summary>Gets a value indicating whether IPv4 support is available and enabled on the current host.</summary>
		/// <returns>
		///   <see langword="true" /> if the current host supports the IPv4 protocol; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x00096278 File Offset: 0x00094478
		[Obsolete("SupportsIPv4 is obsoleted for this type, please use OSSupportsIPv4 instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static bool SupportsIPv4
		{
			get
			{
				Socket.InitializeSockets();
				return Socket.s_SupportsIPv4;
			}
		}

		/// <summary>Indicates whether the underlying operating system and network adaptors support Internet Protocol version 4 (IPv4).</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system and network adaptors support the IPv4 protocol; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x00096286 File Offset: 0x00094486
		public static bool OSSupportsIPv4
		{
			get
			{
				Socket.InitializeSockets();
				return Socket.s_SupportsIPv4;
			}
		}

		/// <summary>Gets a value that indicates whether the Framework supports IPv6 for certain obsolete <see cref="T:System.Net.Dns" /> members.</summary>
		/// <returns>
		///   <see langword="true" /> if the Framework supports IPv6 for certain obsolete <see cref="T:System.Net.Dns" /> methods; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x00096294 File Offset: 0x00094494
		[Obsolete("SupportsIPv6 is obsoleted for this type, please use OSSupportsIPv6 instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static bool SupportsIPv6
		{
			get
			{
				Socket.InitializeSockets();
				return Socket.s_SupportsIPv6;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x000962A2 File Offset: 0x000944A2
		internal static bool LegacySupportsIPv6
		{
			get
			{
				Socket.InitializeSockets();
				return Socket.s_SupportsIPv6;
			}
		}

		/// <summary>Indicates whether the underlying operating system and network adaptors support Internet Protocol version 6 (IPv6).</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system and network adaptors support the IPv6 protocol; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x000962B0 File Offset: 0x000944B0
		public static bool OSSupportsIPv6
		{
			get
			{
				Socket.InitializeSockets();
				return Socket.s_OSSupportsIPv6;
			}
		}

		/// <summary>Gets the amount of data that has been received from the network and is available to be read.</summary>
		/// <returns>The number of bytes of data received from the network and available to be read.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x000962C0 File Offset: 0x000944C0
		public int Available
		{
			get
			{
				if (this.CleanedUp)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				int num = 0;
				SocketError socketError = UnsafeNclNativeMethods.OSSOCK.ioctlsocket(this.m_Handle, 1074030207, ref num);
				if (socketError == SocketError.SocketError)
				{
					SocketException ex = new SocketException();
					this.UpdateStatusAfterSocketError(ex);
					if (Socket.s_LoggingEnabled)
					{
						Logging.Exception(Logging.Sockets, this, "Available", ex);
					}
					throw ex;
				}
				return num;
			}
		}

		/// <summary>Gets the local endpoint.</summary>
		/// <returns>The <see cref="T:System.Net.EndPoint" /> that the <see cref="T:System.Net.Sockets.Socket" /> is using for communications.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x0009632C File Offset: 0x0009452C
		public EndPoint LocalEndPoint
		{
			get
			{
				if (this.CleanedUp)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.m_NonBlockingConnectInProgress && this.Poll(0, SelectMode.SelectWrite))
				{
					this.m_IsConnected = true;
					this.m_RightEndPoint = this.m_NonBlockingConnectRightEndPoint;
					this.m_NonBlockingConnectInProgress = false;
				}
				if (this.m_RightEndPoint == null)
				{
					return null;
				}
				SocketAddress socketAddress = this.m_RightEndPoint.Serialize();
				SocketError socketError = UnsafeNclNativeMethods.OSSOCK.getsockname(this.m_Handle, socketAddress.m_Buffer, ref socketAddress.m_Size);
				if (socketError != SocketError.Success)
				{
					SocketException ex = new SocketException();
					this.UpdateStatusAfterSocketError(ex);
					if (Socket.s_LoggingEnabled)
					{
						Logging.Exception(Logging.Sockets, this, "LocalEndPoint", ex);
					}
					throw ex;
				}
				return this.m_RightEndPoint.Create(socketAddress);
			}
		}

		/// <summary>Gets the remote endpoint.</summary>
		/// <returns>The <see cref="T:System.Net.EndPoint" /> with which the <see cref="T:System.Net.Sockets.Socket" /> is communicating.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x000963E4 File Offset: 0x000945E4
		public EndPoint RemoteEndPoint
		{
			get
			{
				if (this.CleanedUp)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.m_RemoteEndPoint == null)
				{
					if (this.m_NonBlockingConnectInProgress && this.Poll(0, SelectMode.SelectWrite))
					{
						this.m_IsConnected = true;
						this.m_RightEndPoint = this.m_NonBlockingConnectRightEndPoint;
						this.m_NonBlockingConnectInProgress = false;
					}
					if (this.m_RightEndPoint == null)
					{
						return null;
					}
					SocketAddress socketAddress = this.m_RightEndPoint.Serialize();
					SocketError socketError = UnsafeNclNativeMethods.OSSOCK.getpeername(this.m_Handle, socketAddress.m_Buffer, ref socketAddress.m_Size);
					if (socketError != SocketError.Success)
					{
						SocketException ex = new SocketException();
						this.UpdateStatusAfterSocketError(ex);
						if (Socket.s_LoggingEnabled)
						{
							Logging.Exception(Logging.Sockets, this, "RemoteEndPoint", ex);
						}
						throw ex;
					}
					try
					{
						this.m_RemoteEndPoint = this.m_RightEndPoint.Create(socketAddress);
					}
					catch
					{
					}
				}
				return this.m_RemoteEndPoint;
			}
		}

		/// <summary>Gets the operating system handle for the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that represents the operating system handle for the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x000964CC File Offset: 0x000946CC
		public IntPtr Handle
		{
			get
			{
				ExceptionHelper.UnmanagedPermission.Demand();
				return this.m_Handle.DangerousGetHandle();
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x000964E3 File Offset: 0x000946E3
		internal SafeCloseSocket SafeHandle
		{
			get
			{
				return this.m_Handle;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Net.Sockets.Socket" /> is in blocking mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.Socket" /> will block; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x000964EB File Offset: 0x000946EB
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x000964F4 File Offset: 0x000946F4
		public bool Blocking
		{
			get
			{
				return this.willBlock;
			}
			set
			{
				if (this.CleanedUp)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				bool flag;
				SocketError socketError = this.InternalSetBlocking(value, out flag);
				if (socketError != SocketError.Success)
				{
					SocketException ex = new SocketException(socketError);
					this.UpdateStatusAfterSocketError(ex);
					if (Socket.s_LoggingEnabled)
					{
						Logging.Exception(Logging.Sockets, this, "Blocking", ex);
					}
					throw ex;
				}
				this.willBlock = flag;
			}
		}

		/// <summary>Specifies whether the socket should only use Overlapped I/O mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.Socket" /> uses only overlapped I/O; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The socket has been bound to a completion port.</exception>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x00096558 File Offset: 0x00094758
		// (set) Token: 0x06002028 RID: 8232 RVA: 0x00096560 File Offset: 0x00094760
		public bool UseOnlyOverlappedIO
		{
			get
			{
				return this.useOverlappedIO;
			}
			set
			{
				if (this.m_BoundToThreadPool)
				{
					throw new InvalidOperationException(SR.GetString("net_io_completionportwasbound"));
				}
				this.useOverlappedIO = value;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Net.Sockets.Socket" /> is connected to a remote host as of the last <see cref="Overload:System.Net.Sockets.Socket.Send" /> or <see cref="Overload:System.Net.Sockets.Socket.Receive" /> operation.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.Socket" /> was connected to a remote resource as of the most recent operation; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x00096581 File Offset: 0x00094781
		public bool Connected
		{
			get
			{
				if (this.m_NonBlockingConnectInProgress && this.Poll(0, SelectMode.SelectWrite))
				{
					this.m_IsConnected = true;
					this.m_RightEndPoint = this.m_NonBlockingConnectRightEndPoint;
					this.m_NonBlockingConnectInProgress = false;
				}
				return this.m_IsConnected;
			}
		}

		/// <summary>Gets the address family of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</returns>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x000965B5 File Offset: 0x000947B5
		public AddressFamily AddressFamily
		{
			get
			{
				return this.addressFamily;
			}
		}

		/// <summary>Gets the type of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.SocketType" /> values.</returns>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x000965BD File Offset: 0x000947BD
		public SocketType SocketType
		{
			get
			{
				return this.socketType;
			}
		}

		/// <summary>Gets the protocol type of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.ProtocolType" /> values.</returns>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x000965C5 File Offset: 0x000947C5
		public ProtocolType ProtocolType
		{
			get
			{
				return this.protocolType;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.Socket" /> is bound to a specific local port.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.Socket" /> is bound to a local port; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x000965CD File Offset: 0x000947CD
		public bool IsBound
		{
			get
			{
				return this.m_RightEndPoint != null;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> allows only one process to bind to a port.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.Socket" /> allows only one socket to bind to a specific port; otherwise, <see langword="false" />. The default is <see langword="true" /> for Windows Server 2003 and Windows XP Service Pack 2, and <see langword="false" /> for all other versions.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> has been called for this <see cref="T:System.Net.Sockets.Socket" />.</exception>
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x000965D8 File Offset: 0x000947D8
		// (set) Token: 0x0600202F RID: 8239 RVA: 0x000965F1 File Offset: 0x000947F1
		public bool ExclusiveAddressUse
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse) != 0;
			}
			set
			{
				if (this.IsBound)
				{
					throw new InvalidOperationException(SR.GetString("net_sockets_mustnotbebound"));
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, value ? 1 : 0);
			}
		}

		/// <summary>Gets or sets a value that specifies the size of the receive buffer of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the receive buffer. The default is 8192.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than 0.</exception>
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x0009661F File Offset: 0x0009481F
		// (set) Token: 0x06002031 RID: 8241 RVA: 0x00096636 File Offset: 0x00094836
		public int ReceiveBufferSize
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, value);
			}
		}

		/// <summary>Gets or sets a value that specifies the size of the send buffer of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the send buffer. The default is 8192.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than 0.</exception>
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x00096658 File Offset: 0x00094858
		// (set) Token: 0x06002033 RID: 8243 RVA: 0x0009666F File Offset: 0x0009486F
		public int SendBufferSize
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, value);
			}
		}

		/// <summary>Gets or sets a value that specifies the amount of time after which a synchronous <see cref="Overload:System.Net.Sockets.Socket.Receive" /> call will time out.</summary>
		/// <returns>The time-out value, in milliseconds. The default value is 0, which indicates an infinite time-out period. Specifying -1 also indicates an infinite time-out period.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than -1.</exception>
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x00096691 File Offset: 0x00094891
		// (set) Token: 0x06002035 RID: 8245 RVA: 0x000966A8 File Offset: 0x000948A8
		public int ReceiveTimeout
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value == -1)
				{
					value = 0;
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
			}
		}

		/// <summary>Gets or sets a value that specifies the amount of time after which a synchronous <see cref="Overload:System.Net.Sockets.Socket.Send" /> call will time out.</summary>
		/// <returns>The time-out value, in milliseconds. If you set the property with a value between 1 and 499, the value will be changed to 500. The default value is 0, which indicates an infinite time-out period. Specifying -1 also indicates an infinite time-out period.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than -1.</exception>
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x000966D1 File Offset: 0x000948D1
		// (set) Token: 0x06002037 RID: 8247 RVA: 0x000966E8 File Offset: 0x000948E8
		public int SendTimeout
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value == -1)
				{
					value = 0;
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
			}
		}

		/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> will delay closing a socket in an attempt to send all pending data.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.LingerOption" /> that specifies how to linger while closing a socket.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x00096711 File Offset: 0x00094911
		// (set) Token: 0x06002039 RID: 8249 RVA: 0x00096728 File Offset: 0x00094928
		public LingerOption LingerState
		{
			get
			{
				return (LingerOption)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger);
			}
			set
			{
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, value);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the stream <see cref="T:System.Net.Sockets.Socket" /> is using the Nagle algorithm.</summary>
		/// <returns>
		///   <see langword="false" /> if the <see cref="T:System.Net.Sockets.Socket" /> uses the Nagle algorithm; otherwise, <see langword="true" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the <see cref="T:System.Net.Sockets.Socket" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x0009673B File Offset: 0x0009493B
		// (set) Token: 0x0600203B RID: 8251 RVA: 0x0009674F File Offset: 0x0009494F
		public bool NoDelay
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug) != 0;
			}
			set
			{
				this.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug, value ? 1 : 0);
			}
		}

		/// <summary>Gets or sets a value that specifies the Time To Live (TTL) value of Internet Protocol (IP) packets sent by the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The TTL value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The TTL value can't be set to a negative number.</exception>
		/// <exception cref="T:System.NotSupportedException">This property can be set only for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. This error is also returned when an attempt was made to set TTL to a value higher than 255.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x00096760 File Offset: 0x00094960
		// (set) Token: 0x0600203D RID: 8253 RVA: 0x000967B0 File Offset: 0x000949B0
		public short Ttl
		{
			get
			{
				if (this.addressFamily == AddressFamily.InterNetwork)
				{
					return (short)((int)this.GetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress));
				}
				if (this.addressFamily == AddressFamily.InterNetworkV6)
				{
					return (short)((int)this.GetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.ReuseAddress));
				}
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
			set
			{
				if (value < 0 || value > 255)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.addressFamily == AddressFamily.InterNetwork)
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, (int)value);
					return;
				}
				if (this.addressFamily == AddressFamily.InterNetworkV6)
				{
					this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.ReuseAddress, (int)value);
					return;
				}
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> allows Internet Protocol (IP) datagrams to be fragmented.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.Socket" /> allows datagram fragmentation; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This property can be set only for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x0009680B File Offset: 0x00094A0B
		// (set) Token: 0x0600203F RID: 8255 RVA: 0x00096839 File Offset: 0x00094A39
		public bool DontFragment
		{
			get
			{
				if (this.addressFamily == AddressFamily.InterNetwork)
				{
					return (int)this.GetSocketOption(SocketOptionLevel.IP, SocketOptionName.DontFragment) != 0;
				}
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
			set
			{
				if (this.addressFamily == AddressFamily.InterNetwork)
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DontFragment, value ? 1 : 0);
					return;
				}
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
		}

		/// <summary>Gets or sets a value that specifies whether outgoing multicast packets are delivered to the sending application.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.Socket" /> receives outgoing multicast packets; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x00096864 File Offset: 0x00094A64
		// (set) Token: 0x06002041 RID: 8257 RVA: 0x000968BC File Offset: 0x00094ABC
		public bool MulticastLoopback
		{
			get
			{
				if (this.addressFamily == AddressFamily.InterNetwork)
				{
					return (int)this.GetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback) != 0;
				}
				if (this.addressFamily == AddressFamily.InterNetworkV6)
				{
					return (int)this.GetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.MulticastLoopback) != 0;
				}
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
			set
			{
				if (this.addressFamily == AddressFamily.InterNetwork)
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback, value ? 1 : 0);
					return;
				}
				if (this.addressFamily == AddressFamily.InterNetworkV6)
				{
					this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.MulticastLoopback, value ? 1 : 0);
					return;
				}
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> can send or receive broadcast packets.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.Socket" /> allows broadcast packets; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">This option is valid for a datagram socket only.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x0009690E File Offset: 0x00094B0E
		// (set) Token: 0x06002043 RID: 8259 RVA: 0x00096927 File Offset: 0x00094B27
		public bool EnableBroadcast
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast) != 0;
			}
			set
			{
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, value ? 1 : 0);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> is a dual-mode socket used for both IPv4 and IPv6.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.Socket" /> is a  dual-mode socket; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x0009693D File Offset: 0x00094B3D
		// (set) Token: 0x06002045 RID: 8261 RVA: 0x0009696B File Offset: 0x00094B6B
		public bool DualMode
		{
			get
			{
				if (this.AddressFamily != AddressFamily.InterNetworkV6)
				{
					throw new NotSupportedException(SR.GetString("net_invalidversion"));
				}
				return (int)this.GetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only) == 0;
			}
			set
			{
				if (this.AddressFamily != AddressFamily.InterNetworkV6)
				{
					throw new NotSupportedException(SR.GetString("net_invalidversion"));
				}
				this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, value ? 0 : 1);
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x00096998 File Offset: 0x00094B98
		private bool IsDualMode
		{
			get
			{
				return this.AddressFamily == AddressFamily.InterNetworkV6 && this.DualMode;
			}
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000969AC File Offset: 0x00094BAC
		internal bool CanTryAddressFamily(AddressFamily family)
		{
			return family == this.addressFamily || (family == AddressFamily.InterNetwork && this.IsDualMode);
		}

		/// <summary>Associates a <see cref="T:System.Net.Sockets.Socket" /> with a local endpoint.</summary>
		/// <param name="localEP">The local <see cref="T:System.Net.EndPoint" /> to associate with the <see cref="T:System.Net.Sockets.Socket" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		// Token: 0x06002048 RID: 8264 RVA: 0x000969C8 File Offset: 0x00094BC8
		public void Bind(EndPoint localEP)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Bind", localEP);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			EndPoint endPoint = localEP;
			IPEndPoint ipendPoint = localEP as IPEndPoint;
			if (ipendPoint != null)
			{
				ipendPoint = ipendPoint.Snapshot();
				endPoint = this.RemapIPEndPoint(ipendPoint);
				SocketPermission socketPermission = new SocketPermission(NetworkAccess.Accept, this.Transport, ipendPoint.Address.ToString(), ipendPoint.Port);
				socketPermission.Demand();
			}
			else
			{
				ExceptionHelper.UnmanagedPermission.Demand();
			}
			SocketAddress socketAddress = this.CallSerializeCheckDnsEndPoint(endPoint);
			this.DoBind(endPoint, socketAddress);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Bind", "");
			}
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00096A94 File Offset: 0x00094C94
		internal void InternalBind(EndPoint localEP)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "InternalBind", localEP);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			EndPoint endPoint = localEP;
			SocketAddress socketAddress = this.SnapshotAndSerialize(ref endPoint);
			this.DoBind(endPoint, socketAddress);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "InternalBind", "");
			}
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00096B08 File Offset: 0x00094D08
		private void DoBind(EndPoint endPointSnapshot, SocketAddress socketAddress)
		{
			IPEndPoint ipendPoint = endPointSnapshot as IPEndPoint;
			if (!Socket.OSSupportsIPv4 && ipendPoint != null && ipendPoint.Address.IsIPv4MappedToIPv6)
			{
				SocketException ex = new SocketException(SocketError.InvalidArgument);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "DoBind", ex);
				}
				throw ex;
			}
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.bind(this.m_Handle, socketAddress.m_Buffer, socketAddress.m_Size);
			if (socketError != SocketError.Success)
			{
				SocketException ex2 = new SocketException();
				this.UpdateStatusAfterSocketError(ex2);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "DoBind", ex2);
				}
				throw ex2;
			}
			if (this.m_RightEndPoint == null)
			{
				this.m_RightEndPoint = endPointSnapshot;
			}
		}

		/// <summary>Establishes a connection to a remote host.</summary>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote device.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> has been placed in a listening state by calling <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />.</exception>
		// Token: 0x0600204B RID: 8267 RVA: 0x00096BB4 File Offset: 0x00094DB4
		public void Connect(EndPoint remoteEP)
		{
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (this.m_IsDisconnected)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_disconnectedConnect"));
			}
			if (this.isListening)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustnotlisten"));
			}
			this.ValidateBlockingMode();
			DnsEndPoint dnsEndPoint = remoteEP as DnsEndPoint;
			if (dnsEndPoint == null)
			{
				EndPoint endPoint = remoteEP;
				SocketAddress socketAddress = this.CheckCacheRemote(ref endPoint, true);
				if (!this.Blocking)
				{
					this.m_NonBlockingConnectRightEndPoint = endPoint;
					this.m_NonBlockingConnectInProgress = true;
				}
				this.DoConnect(endPoint, socketAddress);
				return;
			}
			if (dnsEndPoint.AddressFamily != AddressFamily.Unspecified && !this.CanTryAddressFamily(dnsEndPoint.AddressFamily))
			{
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
			this.Connect(dnsEndPoint.Host, dnsEndPoint.Port);
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by an IP address and a port number.</summary>
		/// <param name="address">The IP address of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> has been placed in a listening state by calling <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />.</exception>
		// Token: 0x0600204C RID: 8268 RVA: 0x00096C8C File Offset: 0x00094E8C
		public void Connect(IPAddress address, int port)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", address);
			}
			if (this.CleanedUp)
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
			if (!this.CanTryAddressFamily(address.AddressFamily))
			{
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
			IPEndPoint ipendPoint = new IPEndPoint(address, port);
			this.Connect(ipendPoint);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Connect", null);
			}
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by a host name and a port number.</summary>
		/// <param name="host">The name of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> has been placed in a listening state by calling <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />.</exception>
		// Token: 0x0600204D RID: 8269 RVA: 0x00096D34 File Offset: 0x00094F34
		public void Connect(string host, int port)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", host);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (this.addressFamily != AddressFamily.InterNetwork && this.addressFamily != AddressFamily.InterNetworkV6)
			{
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(host);
			this.Connect(hostAddresses, port);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Connect", null);
			}
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by an array of IP addresses and a port number.</summary>
		/// <param name="addresses">The IP addresses of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addresses" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> has been placed in a listening state by calling <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />.</exception>
		// Token: 0x0600204E RID: 8270 RVA: 0x00096DE4 File Offset: 0x00094FE4
		public void Connect(IPAddress[] addresses, int port)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", addresses);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			if (addresses.Length == 0)
			{
				throw new ArgumentException(SR.GetString("net_sockets_invalid_ipaddress_length"), "addresses");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (this.addressFamily != AddressFamily.InterNetwork && this.addressFamily != AddressFamily.InterNetworkV6)
			{
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
			Exception ex = null;
			foreach (IPAddress ipaddress in addresses)
			{
				if (this.CanTryAddressFamily(ipaddress.AddressFamily))
				{
					try
					{
						this.Connect(new IPEndPoint(ipaddress, port));
						ex = null;
						break;
					}
					catch (Exception ex2)
					{
						if (NclUtilities.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			if (!this.Connected)
			{
				throw new ArgumentException(SR.GetString("net_invalidAddressList"), "addresses");
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Connect", null);
			}
		}

		/// <summary>Closes the <see cref="T:System.Net.Sockets.Socket" /> connection and releases all associated resources.</summary>
		// Token: 0x0600204F RID: 8271 RVA: 0x00096F14 File Offset: 0x00095114
		public void Close()
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Close", null);
			}
			((IDisposable)this).Dispose();
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Close", null);
			}
		}

		/// <summary>Closes the <see cref="T:System.Net.Sockets.Socket" /> connection and releases all associated resources with a specified timeout to allow queued data to be sent.</summary>
		/// <param name="timeout">Wait up to <paramref name="timeout" /> seconds to send any remaining data, then close the socket.</param>
		// Token: 0x06002050 RID: 8272 RVA: 0x00096F50 File Offset: 0x00095150
		public void Close(int timeout)
		{
			if (timeout < -1)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			this.m_CloseTimeout = timeout;
			((IDisposable)this).Dispose();
		}

		/// <summary>Places a <see cref="T:System.Net.Sockets.Socket" /> in a listening state.</summary>
		/// <param name="backlog">The maximum length of the pending connections queue.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002051 RID: 8273 RVA: 0x00096F70 File Offset: 0x00095170
		public void Listen(int backlog)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Listen", backlog);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.listen(this.m_Handle, backlog);
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "Listen", ex);
				}
				throw ex;
			}
			this.isListening = true;
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Listen", "");
			}
		}

		/// <summary>Creates a new <see cref="T:System.Net.Sockets.Socket" /> for a newly created connection.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> for a newly created connection.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.Accept" />.</exception>
		// Token: 0x06002052 RID: 8274 RVA: 0x00097014 File Offset: 0x00095214
		public Socket Accept()
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Accept", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.m_RightEndPoint == null)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustbind"));
			}
			if (!this.isListening)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustlisten"));
			}
			if (this.m_IsDisconnected)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_disconnectedAccept"));
			}
			this.ValidateBlockingMode();
			SocketAddress socketAddress = this.m_RightEndPoint.Serialize();
			SafeCloseSocket safeCloseSocket = SafeCloseSocket.Accept(this.m_Handle, socketAddress.m_Buffer, ref socketAddress.m_Size);
			if (safeCloseSocket.IsInvalid)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "Accept", ex);
				}
				throw ex;
			}
			Socket socket = this.CreateAcceptSocket(safeCloseSocket, this.m_RightEndPoint.Create(socketAddress), false);
			if (Socket.s_LoggingEnabled)
			{
				Logging.PrintInfo(Logging.Sockets, socket, SR.GetString("net_log_socket_accepted", new object[] { socket.RemoteEndPoint, socket.LocalEndPoint }));
				Logging.Exit(Logging.Sockets, this, "Accept", socket);
			}
			return socket;
		}

		/// <summary>Sends the specified number of bytes of data to a connected <see cref="T:System.Net.Sockets.Socket" />, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="size">The number of bytes to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is less than 0 or exceeds the size of the buffer.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.  
		/// -or-  
		/// An operating system error occurs while accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002053 RID: 8275 RVA: 0x00097158 File Offset: 0x00095358
		public int Send(byte[] buffer, int size, SocketFlags socketFlags)
		{
			return this.Send(buffer, 0, size, socketFlags);
		}

		/// <summary>Sends data to a connected <see cref="T:System.Net.Sockets.Socket" /> using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002054 RID: 8276 RVA: 0x00097164 File Offset: 0x00095364
		public int Send(byte[] buffer, SocketFlags socketFlags)
		{
			return this.Send(buffer, 0, (buffer != null) ? buffer.Length : 0, socketFlags);
		}

		/// <summary>Sends data to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002055 RID: 8277 RVA: 0x00097178 File Offset: 0x00095378
		public int Send(byte[] buffer)
		{
			return this.Send(buffer, 0, (buffer != null) ? buffer.Length : 0, SocketFlags.None);
		}

		/// <summary>Sends the set of buffers in the list to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002056 RID: 8278 RVA: 0x0009718C File Offset: 0x0009538C
		public int Send(IList<ArraySegment<byte>> buffers)
		{
			return this.Send(buffers, SocketFlags.None);
		}

		/// <summary>Sends the set of buffers in the list to a connected <see cref="T:System.Net.Sockets.Socket" />, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002057 RID: 8279 RVA: 0x00097198 File Offset: 0x00095398
		public int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			SocketError socketError;
			int num = this.Send(buffers, socketFlags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Sends the set of buffers in the list to a connected <see cref="T:System.Net.Sockets.Socket" />, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002058 RID: 8280 RVA: 0x000971BC File Offset: 0x000953BC
		public int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Send", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			if (buffers.Count == 0)
			{
				throw new ArgumentException(SR.GetString("net_sockets_zerolist", new object[] { "buffers" }), "buffers");
			}
			this.ValidateBlockingMode();
			errorCode = SocketError.Success;
			int count = buffers.Count;
			WSABuffer[] array = new WSABuffer[count];
			GCHandle[] array2 = null;
			int num;
			try
			{
				array2 = new GCHandle[count];
				for (int i = 0; i < count; i++)
				{
					ArraySegment<byte> arraySegment = buffers[i];
					ValidationHelper.ValidateSegment(arraySegment);
					array2[i] = GCHandle.Alloc(arraySegment.Array, GCHandleType.Pinned);
					array[i].Length = arraySegment.Count;
					array[i].Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(arraySegment.Array, arraySegment.Offset);
				}
				errorCode = UnsafeNclNativeMethods.OSSOCK.WSASend_Blocking(this.m_Handle.DangerousGetHandle(), array, count, out num, socketFlags, SafeNativeOverlapped.Zero, IntPtr.Zero);
				if (errorCode == SocketError.SocketError)
				{
					errorCode = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			finally
			{
				if (array2 != null)
				{
					for (int j = 0; j < array2.Length; j++)
					{
						if (array2[j].IsAllocated)
						{
							array2[j].Free();
						}
					}
				}
			}
			if (errorCode != SocketError.Success)
			{
				this.UpdateStatusAfterSocketError(errorCode);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "Send", new SocketException(errorCode));
					Logging.Exit(Logging.Sockets, this, "Send", 0);
				}
				return 0;
			}
			if (Socket.s_PerfCountersEnabled && num > 0)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesSent, (long)num);
				if (this.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsSent);
				}
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Send", num);
			}
			return num;
		}

		/// <summary>Sends the file <paramref name="fileName" /> to a connected <see cref="T:System.Net.Sockets.Socket" /> object with the <see cref="F:System.Net.Sockets.TransmitFileOptions.UseDefaultWorkerThread" /> transmit flag.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the path and name of the file to be sent. This parameter can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The socket is not connected to a remote host.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> object is not in blocking mode and cannot accept this synchronous call.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x06002059 RID: 8281 RVA: 0x000973C8 File Offset: 0x000955C8
		public void SendFile(string fileName)
		{
			this.SendFile(fileName, null, null, TransmitFileOptions.UseDefaultWorkerThread);
		}

		/// <summary>Sends the file <paramref name="fileName" /> and buffers of data to a connected <see cref="T:System.Net.Sockets.Socket" /> object using the specified <see cref="T:System.Net.Sockets.TransmitFileOptions" /> value.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the path and name of the file to be sent. This parameter can be <see langword="null" />.</param>
		/// <param name="preBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent before the file is sent. This parameter can be <see langword="null" />.</param>
		/// <param name="postBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent after the file is sent. This parameter can be <see langword="null" />.</param>
		/// <param name="flags">One or more of <see cref="T:System.Net.Sockets.TransmitFileOptions" /> values.</param>
		/// <exception cref="T:System.NotSupportedException">The operating system is not Windows NT or later.  
		/// -or-
		///  The socket is not connected to a remote host.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> object is not in blocking mode and cannot accept this synchronous call.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x0600205A RID: 8282 RVA: 0x000973D4 File Offset: 0x000955D4
		public void SendFile(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "SendFile", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.Connected)
			{
				throw new NotSupportedException(SR.GetString("net_notconnected"));
			}
			this.ValidateBlockingMode();
			TransmitFileOverlappedAsyncResult transmitFileOverlappedAsyncResult = new TransmitFileOverlappedAsyncResult(this);
			FileStream fileStream = null;
			if (fileName != null && fileName.Length > 0)
			{
				fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			SafeHandle safeHandle = null;
			if (fileStream != null)
			{
				ExceptionHelper.UnmanagedPermission.Assert();
				try
				{
					safeHandle = fileStream.SafeFileHandle;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			SocketError socketError = SocketError.Success;
			try
			{
				transmitFileOverlappedAsyncResult.SetUnmanagedStructures(preBuffer, postBuffer, fileStream, TransmitFileOptions.UseDefaultWorkerThread, true);
				if ((safeHandle != null) ? (!UnsafeNclNativeMethods.OSSOCK.TransmitFile_Blocking(this.m_Handle.DangerousGetHandle(), safeHandle, 0, 0, SafeNativeOverlapped.Zero, transmitFileOverlappedAsyncResult.TransmitFileBuffers, flags)) : (!UnsafeNclNativeMethods.OSSOCK.TransmitFile_Blocking2(this.m_Handle.DangerousGetHandle(), IntPtr.Zero, 0, 0, SafeNativeOverlapped.Zero, transmitFileOverlappedAsyncResult.TransmitFileBuffers, flags)))
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			finally
			{
				transmitFileOverlappedAsyncResult.SyncReleaseUnmanagedStructures();
			}
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "SendFile", ex);
				}
				throw ex;
			}
			if ((transmitFileOverlappedAsyncResult.Flags & (TransmitFileOptions.Disconnect | TransmitFileOptions.ReuseSocket)) != TransmitFileOptions.UseDefaultWorkerThread)
			{
				this.SetToDisconnected();
				this.m_RemoteEndPoint = null;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "SendFile", socketError);
			}
		}

		/// <summary>Sends the specified number of bytes of data to a connected <see cref="T:System.Net.Sockets.Socket" />, starting at the specified offset, and using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="offset">The position in the data buffer at which to begin sending data.</param>
		/// <param name="size">The number of bytes to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.  
		/// -or-  
		/// An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x0600205B RID: 8283 RVA: 0x00097564 File Offset: 0x00095764
		public int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags)
		{
			SocketError socketError;
			int num = this.Send(buffer, offset, size, socketFlags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Sends the specified number of bytes of data to a connected <see cref="T:System.Net.Sockets.Socket" />, starting at the specified offset, and using the specified <see cref="T:System.Net.Sockets.SocketFlags" /></summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="offset">The position in the data buffer at which to begin sending data.</param>
		/// <param name="size">The number of bytes to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.  
		/// -or-  
		/// An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x0600205C RID: 8284 RVA: 0x0009758C File Offset: 0x0009578C
		public unsafe int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Send", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			errorCode = SocketError.Success;
			this.ValidateBlockingMode();
			int num;
			if (buffer.Length == 0)
			{
				num = UnsafeNclNativeMethods.OSSOCK.send(this.m_Handle.DangerousGetHandle(), null, 0, socketFlags);
			}
			else
			{
				fixed (byte[] array = buffer)
				{
					byte* ptr;
					if (buffer == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					num = UnsafeNclNativeMethods.OSSOCK.send(this.m_Handle.DangerousGetHandle(), ptr + offset, size, socketFlags);
				}
			}
			if (num == -1)
			{
				errorCode = (SocketError)Marshal.GetLastWin32Error();
				this.UpdateStatusAfterSocketError(errorCode);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "Send", new SocketException(errorCode));
					Logging.Exit(Logging.Sockets, this, "Send", 0);
				}
				return 0;
			}
			if (Socket.s_PerfCountersEnabled && num > 0)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesSent, (long)num);
				if (this.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsSent);
				}
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Dump(Logging.Sockets, this, "Send", buffer, offset, size);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Send", num);
			}
			return num;
		}

		/// <summary>Sends the specified number of bytes of data to the specified endpoint, starting at the specified location in the buffer, and using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="offset">The position in the data buffer at which to begin sending data.</param>
		/// <param name="size">The number of bytes to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination location for the data.</param>
		/// <returns>The number of bytes sent.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.  
		/// -or-  
		/// An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions.</exception>
		// Token: 0x0600205D RID: 8285 RVA: 0x0009771C File Offset: 0x0009591C
		public unsafe int SendTo(byte[] buffer, int offset, int size, SocketFlags socketFlags, EndPoint remoteEP)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "SendTo", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.ValidateBlockingMode();
			EndPoint endPoint = remoteEP;
			SocketAddress socketAddress = this.CheckCacheRemote(ref endPoint, false);
			int num;
			if (buffer.Length == 0)
			{
				num = UnsafeNclNativeMethods.OSSOCK.sendto(this.m_Handle.DangerousGetHandle(), null, 0, socketFlags, socketAddress.m_Buffer, socketAddress.m_Size);
			}
			else
			{
				fixed (byte[] array = buffer)
				{
					byte* ptr;
					if (buffer == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					num = UnsafeNclNativeMethods.OSSOCK.sendto(this.m_Handle.DangerousGetHandle(), ptr + offset, size, socketFlags, socketAddress.m_Buffer, socketAddress.m_Size);
				}
			}
			if (num == -1)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "SendTo", ex);
				}
				throw ex;
			}
			if (this.m_RightEndPoint == null)
			{
				this.m_RightEndPoint = endPoint;
			}
			if (Socket.s_PerfCountersEnabled && num > 0)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesSent, (long)num);
				if (this.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsSent);
				}
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Dump(Logging.Sockets, this, "SendTo", buffer, offset, size);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "SendTo", num);
			}
			return num;
		}

		/// <summary>Sends the specified number of bytes of data to the specified endpoint using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="size">The number of bytes to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination location for the data.</param>
		/// <returns>The number of bytes sent.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="size" /> exceeds the size of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x0600205E RID: 8286 RVA: 0x000978CF File Offset: 0x00095ACF
		public int SendTo(byte[] buffer, int size, SocketFlags socketFlags, EndPoint remoteEP)
		{
			return this.SendTo(buffer, 0, size, socketFlags, remoteEP);
		}

		/// <summary>Sends data to a specific endpoint using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination location for the data.</param>
		/// <returns>The number of bytes sent.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x0600205F RID: 8287 RVA: 0x000978DD File Offset: 0x00095ADD
		public int SendTo(byte[] buffer, SocketFlags socketFlags, EndPoint remoteEP)
		{
			return this.SendTo(buffer, 0, (buffer != null) ? buffer.Length : 0, socketFlags, remoteEP);
		}

		/// <summary>Sends data to the specified endpoint.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination for the data.</param>
		/// <returns>The number of bytes sent.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002060 RID: 8288 RVA: 0x000978F2 File Offset: 0x00095AF2
		public int SendTo(byte[] buffer, EndPoint remoteEP)
		{
			return this.SendTo(buffer, 0, (buffer != null) ? buffer.Length : 0, SocketFlags.None, remoteEP);
		}

		/// <summary>Receives the specified number of bytes of data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> exceeds the size of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions.</exception>
		// Token: 0x06002061 RID: 8289 RVA: 0x00097907 File Offset: 0x00095B07
		public int Receive(byte[] buffer, int size, SocketFlags socketFlags)
		{
			return this.Receive(buffer, 0, size, socketFlags);
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions.</exception>
		// Token: 0x06002062 RID: 8290 RVA: 0x00097913 File Offset: 0x00095B13
		public int Receive(byte[] buffer, SocketFlags socketFlags)
		{
			return this.Receive(buffer, 0, (buffer != null) ? buffer.Length : 0, socketFlags);
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions.</exception>
		// Token: 0x06002063 RID: 8291 RVA: 0x00097927 File Offset: 0x00095B27
		public int Receive(byte[] buffer)
		{
			return this.Receive(buffer, 0, (buffer != null) ? buffer.Length : 0, SocketFlags.None);
		}

		/// <summary>Receives the specified number of bytes from a bound <see cref="T:System.Net.Sockets.Socket" /> into the specified offset position of the receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> to store the received data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.  
		/// -or-  
		/// The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.  
		/// -or-  
		/// An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions.</exception>
		// Token: 0x06002064 RID: 8292 RVA: 0x0009793C File Offset: 0x00095B3C
		public int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags)
		{
			SocketError socketError;
			int num = this.Receive(buffer, offset, size, socketFlags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="offset">The position in the <paramref name="buffer" /> parameter to store the received data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.  
		/// -or-  
		/// The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property is not set.  
		/// -or-  
		/// An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions.</exception>
		// Token: 0x06002065 RID: 8293 RVA: 0x00097964 File Offset: 0x00095B64
		public unsafe int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Receive", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.ValidateBlockingMode();
			errorCode = SocketError.Success;
			int num;
			if (buffer.Length == 0)
			{
				num = UnsafeNclNativeMethods.OSSOCK.recv(this.m_Handle.DangerousGetHandle(), null, 0, socketFlags);
			}
			else
			{
				fixed (byte[] array = buffer)
				{
					byte* ptr;
					if (buffer == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					num = UnsafeNclNativeMethods.OSSOCK.recv(this.m_Handle.DangerousGetHandle(), ptr + offset, size, socketFlags);
				}
			}
			if (num == -1)
			{
				errorCode = (SocketError)Marshal.GetLastWin32Error();
				this.UpdateStatusAfterSocketError(errorCode);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "Receive", new SocketException(errorCode));
					Logging.Exit(Logging.Sockets, this, "Receive", 0);
				}
				return 0;
			}
			if (Socket.s_PerfCountersEnabled)
			{
				bool flag = (socketFlags & SocketFlags.Peek) > SocketFlags.None;
				if (num > 0 && !flag)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesReceived, (long)num);
					if (this.Transport == TransportType.Udp)
					{
						NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsReceived);
					}
				}
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Dump(Logging.Sockets, this, "Receive", buffer, offset, num);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Receive", num);
			}
			return num;
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into the list of receive buffers.</summary>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the received data.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002066 RID: 8294 RVA: 0x00097AFC File Offset: 0x00095CFC
		public int Receive(IList<ArraySegment<byte>> buffers)
		{
			return this.Receive(buffers, SocketFlags.None);
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into the list of receive buffers, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="buffers" />.Count is zero.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002067 RID: 8295 RVA: 0x00097B08 File Offset: 0x00095D08
		public int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			SocketError socketError;
			int num = this.Receive(buffers, socketFlags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into the list of receive buffers, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="buffers" />.Count is zero.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002068 RID: 8296 RVA: 0x00097B2C File Offset: 0x00095D2C
		public int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Receive", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			if (buffers.Count == 0)
			{
				throw new ArgumentException(SR.GetString("net_sockets_zerolist", new object[] { "buffers" }), "buffers");
			}
			this.ValidateBlockingMode();
			int count = buffers.Count;
			WSABuffer[] array = new WSABuffer[count];
			GCHandle[] array2 = null;
			errorCode = SocketError.Success;
			int num;
			try
			{
				array2 = new GCHandle[count];
				for (int i = 0; i < count; i++)
				{
					ArraySegment<byte> arraySegment = buffers[i];
					ValidationHelper.ValidateSegment(arraySegment);
					array2[i] = GCHandle.Alloc(arraySegment.Array, GCHandleType.Pinned);
					array[i].Length = arraySegment.Count;
					array[i].Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(arraySegment.Array, arraySegment.Offset);
				}
				errorCode = UnsafeNclNativeMethods.OSSOCK.WSARecv_Blocking(this.m_Handle.DangerousGetHandle(), array, count, out num, ref socketFlags, SafeNativeOverlapped.Zero, IntPtr.Zero);
				if (errorCode == SocketError.SocketError)
				{
					errorCode = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			finally
			{
				if (array2 != null)
				{
					for (int j = 0; j < array2.Length; j++)
					{
						if (array2[j].IsAllocated)
						{
							array2[j].Free();
						}
					}
				}
			}
			if (errorCode != SocketError.Success)
			{
				this.UpdateStatusAfterSocketError(errorCode);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "Receive", new SocketException(errorCode));
					Logging.Exit(Logging.Sockets, this, "Receive", 0);
				}
				return 0;
			}
			if (Socket.s_PerfCountersEnabled)
			{
				bool flag = (socketFlags & SocketFlags.Peek) > SocketFlags.None;
				if (num > 0 && !flag)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesReceived, (long)num);
					if (this.Transport == TransportType.Udp)
					{
						NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsReceived);
					}
				}
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Receive", num);
			}
			return num;
		}

		/// <summary>Receives the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint and packet information.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data.</param>
		/// <param name="offset">The position in the <paramref name="buffer" /> parameter to store the received data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server.</param>
		/// <param name="ipPacketInformation">An <see cref="T:System.Net.Sockets.IPPacketInformation" /> holding address and interface information.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// - or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of the <paramref name="buffer" /> minus the value of the offset parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.  
		/// -or-  
		/// The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.  
		/// -or-  
		/// The .NET Framework is running on an AMD 64-bit processor.  
		/// -or-  
		/// An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP.</exception>
		// Token: 0x06002069 RID: 8297 RVA: 0x00097D44 File Offset: 0x00095F44
		public int ReceiveMessageFrom(byte[] buffer, int offset, int size, ref SocketFlags socketFlags, ref EndPoint remoteEP, out IPPacketInformation ipPacketInformation)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "ReceiveMessageFrom", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (!this.CanTryAddressFamily(remoteEP.AddressFamily))
			{
				throw new ArgumentException(SR.GetString("net_InvalidEndPointAddressFamily", new object[] { remoteEP.AddressFamily, this.addressFamily }), "remoteEP");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (this.m_RightEndPoint == null)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustbind"));
			}
			this.ValidateBlockingMode();
			EndPoint endPoint = remoteEP;
			SocketAddress socketAddress = this.SnapshotAndSerialize(ref endPoint);
			ReceiveMessageOverlappedAsyncResult receiveMessageOverlappedAsyncResult = new ReceiveMessageOverlappedAsyncResult(this, null, null);
			receiveMessageOverlappedAsyncResult.SetUnmanagedStructures(buffer, offset, size, socketAddress, socketFlags);
			SocketAddress socketAddress2 = endPoint.Serialize();
			int num = 0;
			SocketError socketError = SocketError.Success;
			this.SetReceivingPacketInformation();
			try
			{
				if (this.WSARecvMsg_Blocking(this.m_Handle.DangerousGetHandle(), Marshal.UnsafeAddrOfPinnedArrayElement(receiveMessageOverlappedAsyncResult.m_MessageBuffer, 0), out num, IntPtr.Zero, IntPtr.Zero) == SocketError.SocketError)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			finally
			{
				receiveMessageOverlappedAsyncResult.SyncReleaseUnmanagedStructures();
			}
			if (socketError != SocketError.Success && socketError != SocketError.MessageSize)
			{
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "ReceiveMessageFrom", ex);
				}
				throw ex;
			}
			if (!socketAddress2.Equals(receiveMessageOverlappedAsyncResult.m_SocketAddress))
			{
				try
				{
					remoteEP = endPoint.Create(receiveMessageOverlappedAsyncResult.m_SocketAddress);
				}
				catch
				{
				}
				if (this.m_RightEndPoint == null)
				{
					this.m_RightEndPoint = endPoint;
				}
			}
			socketFlags = receiveMessageOverlappedAsyncResult.m_flags;
			ipPacketInformation = receiveMessageOverlappedAsyncResult.m_IPPacketInformation;
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "ReceiveMessageFrom", socketError);
			}
			return num;
		}

		/// <summary>Receives the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data.</param>
		/// <param name="offset">The position in the <paramref name="buffer" /> parameter to store the received data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of the <paramref name="buffer" /> minus the value of the offset parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.  
		/// -or-  
		/// The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.  
		/// -or-  
		/// An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x0600206A RID: 8298 RVA: 0x00097F6C File Offset: 0x0009616C
		public unsafe int ReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "ReceiveFrom", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (!this.CanTryAddressFamily(remoteEP.AddressFamily))
			{
				throw new ArgumentException(SR.GetString("net_InvalidEndPointAddressFamily", new object[] { remoteEP.AddressFamily, this.addressFamily }), "remoteEP");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (this.m_RightEndPoint == null)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustbind"));
			}
			this.ValidateBlockingMode();
			EndPoint endPoint = remoteEP;
			SocketAddress socketAddress = this.SnapshotAndSerialize(ref endPoint);
			SocketAddress socketAddress2 = endPoint.Serialize();
			int num;
			if (buffer.Length == 0)
			{
				num = UnsafeNclNativeMethods.OSSOCK.recvfrom(this.m_Handle.DangerousGetHandle(), null, 0, socketFlags, socketAddress.m_Buffer, ref socketAddress.m_Size);
			}
			else
			{
				fixed (byte[] array = buffer)
				{
					byte* ptr;
					if (buffer == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					num = UnsafeNclNativeMethods.OSSOCK.recvfrom(this.m_Handle.DangerousGetHandle(), ptr + offset, size, socketFlags, socketAddress.m_Buffer, ref socketAddress.m_Size);
				}
			}
			SocketException ex = null;
			if (num == -1)
			{
				ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "ReceiveFrom", ex);
				}
				if (ex.ErrorCode != 10040)
				{
					throw ex;
				}
			}
			if (!socketAddress2.Equals(socketAddress))
			{
				try
				{
					remoteEP = endPoint.Create(socketAddress);
				}
				catch
				{
				}
				if (this.m_RightEndPoint == null)
				{
					this.m_RightEndPoint = endPoint;
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			if (Socket.s_PerfCountersEnabled && num > 0)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesReceived, (long)num);
				if (this.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsReceived);
				}
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Dump(Logging.Sockets, this, "ReceiveFrom", buffer, offset, size);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "ReceiveFrom", num);
			}
			return num;
		}

		/// <summary>Receives the specified number of bytes into the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.  
		/// -or-  
		/// The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.  
		/// -or-  
		/// An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions.</exception>
		// Token: 0x0600206B RID: 8299 RVA: 0x000981CC File Offset: 0x000963CC
		public int ReceiveFrom(byte[] buffer, int size, SocketFlags socketFlags, ref EndPoint remoteEP)
		{
			return this.ReceiveFrom(buffer, 0, size, socketFlags, ref remoteEP);
		}

		/// <summary>Receives a datagram into the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions.</exception>
		// Token: 0x0600206C RID: 8300 RVA: 0x000981DA File Offset: 0x000963DA
		public int ReceiveFrom(byte[] buffer, SocketFlags socketFlags, ref EndPoint remoteEP)
		{
			return this.ReceiveFrom(buffer, 0, (buffer != null) ? buffer.Length : 0, socketFlags, ref remoteEP);
		}

		/// <summary>Receives a datagram into the data buffer and stores the endpoint.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions.</exception>
		// Token: 0x0600206D RID: 8301 RVA: 0x000981EF File Offset: 0x000963EF
		public int ReceiveFrom(byte[] buffer, ref EndPoint remoteEP)
		{
			return this.ReceiveFrom(buffer, 0, (buffer != null) ? buffer.Length : 0, SocketFlags.None, ref remoteEP);
		}

		/// <summary>Sets low-level operating modes for the <see cref="T:System.Net.Sockets.Socket" /> using numerical control codes.</summary>
		/// <param name="ioControlCode">An <see cref="T:System.Int32" /> value that specifies the control code of the operation to perform.</param>
		/// <param name="optionInValue">A <see cref="T:System.Byte" /> array that contains the input data required by the operation.</param>
		/// <param name="optionOutValue">A <see cref="T:System.Byte" /> array that contains the output data returned by the operation.</param>
		/// <returns>The number of bytes in the <paramref name="optionOutValue" /> parameter.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to change the blocking mode without using the <see cref="P:System.Net.Sockets.Socket.Blocking" /> property.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions.</exception>
		// Token: 0x0600206E RID: 8302 RVA: 0x00098204 File Offset: 0x00096404
		public int IOControl(int ioControlCode, byte[] optionInValue, byte[] optionOutValue)
		{
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (ioControlCode == -2147195266)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_useblocking"));
			}
			ExceptionHelper.UnmanagedPermission.Demand();
			int num = 0;
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAIoctl_Blocking(this.m_Handle.DangerousGetHandle(), ioControlCode, optionInValue, (optionInValue != null) ? optionInValue.Length : 0, optionOutValue, (optionOutValue != null) ? optionOutValue.Length : 0, out num, SafeNativeOverlapped.Zero, IntPtr.Zero);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "IOControl", ex);
				}
				throw ex;
			}
			return num;
		}

		/// <summary>Sets low-level operating modes for the <see cref="T:System.Net.Sockets.Socket" /> using the <see cref="T:System.Net.Sockets.IOControlCode" /> enumeration to specify control codes.</summary>
		/// <param name="ioControlCode">A <see cref="T:System.Net.Sockets.IOControlCode" /> value that specifies the control code of the operation to perform.</param>
		/// <param name="optionInValue">An array of type <see cref="T:System.Byte" /> that contains the input data required by the operation.</param>
		/// <param name="optionOutValue">An array of type <see cref="T:System.Byte" /> that contains the output data returned by the operation.</param>
		/// <returns>The number of bytes in the <paramref name="optionOutValue" /> parameter.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to change the blocking mode without using the <see cref="P:System.Net.Sockets.Socket.Blocking" /> property.</exception>
		// Token: 0x0600206F RID: 8303 RVA: 0x000982AE File Offset: 0x000964AE
		public int IOControl(IOControlCode ioControlCode, byte[] optionInValue, byte[] optionOutValue)
		{
			return this.IOControl((int)ioControlCode, optionInValue, optionOutValue);
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000982BC File Offset: 0x000964BC
		internal int IOControl(IOControlCode ioControlCode, IntPtr optionInValue, int inValueSize, IntPtr optionOutValue, int outValueSize)
		{
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if ((int)ioControlCode == -2147195266)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_useblocking"));
			}
			int num = 0;
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAIoctl_Blocking_Internal(this.m_Handle.DangerousGetHandle(), (uint)ioControlCode, optionInValue, inValueSize, optionOutValue, outValueSize, out num, SafeNativeOverlapped.Zero, IntPtr.Zero);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "IOControl", ex);
				}
				throw ex;
			}
			return num;
		}

		/// <summary>Set the IP protection level on a socket.</summary>
		/// <param name="level">The IP protection level to set on this socket.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="level" /> parameter cannot be <see cref="F:System.Net.Sockets.IPProtectionLevel.Unspecified" />. The IP protection level cannot be set to unspecified.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Net.Sockets.AddressFamily" /> of the socket must be either <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />.</exception>
		// Token: 0x06002071 RID: 8305 RVA: 0x00098350 File Offset: 0x00096550
		public void SetIPProtectionLevel(IPProtectionLevel level)
		{
			if (level == IPProtectionLevel.Unspecified)
			{
				throw new ArgumentException(SR.GetString("net_sockets_invalid_optionValue_all"), "level");
			}
			if (this.addressFamily == AddressFamily.InterNetworkV6)
			{
				this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPProtectionLevel, (int)level);
				return;
			}
			if (this.addressFamily == AddressFamily.InterNetwork)
			{
				this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IPProtectionLevel, (int)level);
				return;
			}
			throw new NotSupportedException(SR.GetString("net_invalidversion"));
		}

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified integer value.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values.</param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values.</param>
		/// <param name="optionValue">A value of the option.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002072 RID: 8306 RVA: 0x000983AF File Offset: 0x000965AF
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue)
		{
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			this.CheckSetOptionPermissions(optionLevel, optionName);
			this.SetSocketOption(optionLevel, optionName, optionValue, false);
		}

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified value, represented as a byte array.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values.</param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values.</param>
		/// <param name="optionValue">An array of type <see cref="T:System.Byte" /> that represents the value of the option.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002073 RID: 8307 RVA: 0x000983DC File Offset: 0x000965DC
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, byte[] optionValue)
		{
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			this.CheckSetOptionPermissions(optionLevel, optionName);
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_Handle, optionLevel, optionName, optionValue, (optionValue != null) ? optionValue.Length : 0);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "SetSocketOption", ex);
				}
				throw ex;
			}
		}

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values.</param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values.</param>
		/// <param name="optionValue">The value of the option, represented as a <see cref="T:System.Boolean" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x06002074 RID: 8308 RVA: 0x0009844F File Offset: 0x0009664F
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, bool optionValue)
		{
			this.SetSocketOption(optionLevel, optionName, optionValue ? 1 : 0);
		}

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified value, represented as an object.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values.</param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values.</param>
		/// <param name="optionValue">A <see cref="T:System.Net.Sockets.LingerOption" /> or <see cref="T:System.Net.Sockets.MulticastOption" /> that contains the value of the option.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="optionValue" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002075 RID: 8309 RVA: 0x00098460 File Offset: 0x00096660
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, object optionValue)
		{
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (optionValue == null)
			{
				throw new ArgumentNullException("optionValue");
			}
			this.CheckSetOptionPermissions(optionLevel, optionName);
			if (optionLevel == SocketOptionLevel.Socket && optionName == SocketOptionName.Linger)
			{
				LingerOption lingerOption = optionValue as LingerOption;
				if (lingerOption == null)
				{
					throw new ArgumentException(SR.GetString("net_sockets_invalid_optionValue", new object[] { "LingerOption" }), "optionValue");
				}
				if (lingerOption.LingerTime < 0 || lingerOption.LingerTime > 65535)
				{
					throw new ArgumentException(SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[] { 0, 65535 }), "optionValue.LingerTime");
				}
				this.setLingerOption(lingerOption);
				return;
			}
			else if (optionLevel == SocketOptionLevel.IP && (optionName == SocketOptionName.AddMembership || optionName == SocketOptionName.DropMembership))
			{
				MulticastOption multicastOption = optionValue as MulticastOption;
				if (multicastOption == null)
				{
					throw new ArgumentException(SR.GetString("net_sockets_invalid_optionValue", new object[] { "MulticastOption" }), "optionValue");
				}
				this.setMulticastOption(optionName, multicastOption);
				return;
			}
			else
			{
				if (optionLevel != SocketOptionLevel.IPv6 || (optionName != SocketOptionName.AddMembership && optionName != SocketOptionName.DropMembership))
				{
					throw new ArgumentException(SR.GetString("net_sockets_invalid_optionValue_all"), "optionValue");
				}
				IPv6MulticastOption pv6MulticastOption = optionValue as IPv6MulticastOption;
				if (pv6MulticastOption == null)
				{
					throw new ArgumentException(SR.GetString("net_sockets_invalid_optionValue", new object[] { "IPv6MulticastOption" }), "optionValue");
				}
				this.setIPv6MulticastOption(optionName, pv6MulticastOption);
				return;
			}
		}

		/// <summary>Returns the value of a specified <see cref="T:System.Net.Sockets.Socket" /> option, represented as an object.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values.</param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values.</param>
		/// <returns>An object that represents the value of the option. When the <paramref name="optionName" /> parameter is set to <see cref="F:System.Net.Sockets.SocketOptionName.Linger" /> the return value is an instance of the <see cref="T:System.Net.Sockets.LingerOption" /> class. When <paramref name="optionName" /> is set to <see cref="F:System.Net.Sockets.SocketOptionName.AddMembership" /> or <see cref="F:System.Net.Sockets.SocketOptionName.DropMembership" />, the return value is an instance of the <see cref="T:System.Net.Sockets.MulticastOption" /> class. When <paramref name="optionName" /> is any other value, the return value is an integer.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.  
		///  -or-  
		///  <paramref name="optionName" /> was set to the unsupported value <see cref="F:System.Net.Sockets.SocketOptionName.MaxConnections" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002076 RID: 8310 RVA: 0x000985C8 File Offset: 0x000967C8
		public object GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName)
		{
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (optionLevel == SocketOptionLevel.Socket && optionName == SocketOptionName.Linger)
			{
				return this.getLingerOpt();
			}
			if (optionLevel == SocketOptionLevel.IP && (optionName == SocketOptionName.AddMembership || optionName == SocketOptionName.DropMembership))
			{
				return this.getMulticastOpt(optionName);
			}
			if (optionLevel == SocketOptionLevel.IPv6 && (optionName == SocketOptionName.AddMembership || optionName == SocketOptionName.DropMembership))
			{
				return this.getIPv6MulticastOpt(optionName);
			}
			int num = 0;
			int num2 = 4;
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.getsockopt(this.m_Handle, optionLevel, optionName, out num, ref num2);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "GetSocketOption", ex);
				}
				throw ex;
			}
			return num;
		}

		/// <summary>Returns the specified <see cref="T:System.Net.Sockets.Socket" /> option setting, represented as a byte array.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values.</param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values.</param>
		/// <param name="optionValue">An array of type <see cref="T:System.Byte" /> that is to receive the option setting.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.  
		/// -or-
		///  In .NET Compact Framework applications, the Windows CE default buffer space is set to 32768 bytes. You can change the per socket buffer space by calling <see cref="Overload:System.Net.Sockets.Socket.SetSocketOption" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002077 RID: 8311 RVA: 0x0009867C File Offset: 0x0009687C
		public void GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, byte[] optionValue)
		{
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			int num = ((optionValue != null) ? optionValue.Length : 0);
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.getsockopt(this.m_Handle, optionLevel, optionName, optionValue, ref num);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "GetSocketOption", ex);
				}
				throw ex;
			}
		}

		/// <summary>Returns the value of the specified <see cref="T:System.Net.Sockets.Socket" /> option in an array.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values.</param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values.</param>
		/// <param name="optionLength">The length, in bytes, of the expected return value.</param>
		/// <returns>An array of type <see cref="T:System.Byte" /> that contains the value of the socket option.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.  
		/// -or-
		///  In .NET Compact Framework applications, the Windows CE default buffer space is set to 32768 bytes. You can change the per socket buffer space by calling <see cref="Overload:System.Net.Sockets.Socket.SetSocketOption" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002078 RID: 8312 RVA: 0x000986EC File Offset: 0x000968EC
		public byte[] GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionLength)
		{
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			byte[] array = new byte[optionLength];
			int num = optionLength;
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.getsockopt(this.m_Handle, optionLevel, optionName, array, ref num);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "GetSocketOption", ex);
				}
				throw ex;
			}
			if (optionLength != num)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		/// <summary>Determines the status of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="microSeconds">The time to wait for a response, in microseconds.</param>
		/// <param name="mode">One of the <see cref="T:System.Net.Sockets.SelectMode" /> values.</param>
		/// <returns>The status of the <see cref="T:System.Net.Sockets.Socket" /> based on the polling mode value passed in the <paramref name="mode" /> parameter.  
		///   Mode  
		///
		///   Return Value  
		///
		///  <see cref="F:System.Net.Sockets.SelectMode.SelectRead" /><see langword="true" /> if <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> has been called and a connection is pending;  
		///
		///  -or-  
		///
		///  <see langword="true" /> if data is available for reading;  
		///
		///  -or-  
		///
		///  <see langword="true" /> if the connection has been closed, reset, or terminated;  
		///
		///  otherwise, returns <see langword="false" />.  
		///
		///  <see cref="F:System.Net.Sockets.SelectMode.SelectWrite" /><see langword="true" />, if processing a <see cref="M:System.Net.Sockets.Socket.Connect(System.Net.EndPoint)" />, and the connection has succeeded;  
		///
		///  -or-  
		///
		///  <see langword="true" /> if data can be sent;  
		///
		///  otherwise, returns <see langword="false" />.  
		///
		///  <see cref="F:System.Net.Sockets.SelectMode.SelectError" /><see langword="true" /> if processing a <see cref="M:System.Net.Sockets.Socket.Connect(System.Net.EndPoint)" /> that does not block, and the connection has failed;  
		///
		///  -or-  
		///
		///  <see langword="true" /> if <see cref="F:System.Net.Sockets.SocketOptionName.OutOfBandInline" /> is not set and out-of-band data is available;  
		///
		///  otherwise, returns <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="mode" /> parameter is not one of the <see cref="T:System.Net.Sockets.SelectMode" /> values.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks below.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002079 RID: 8313 RVA: 0x00098774 File Offset: 0x00096974
		public bool Poll(int microSeconds, SelectMode mode)
		{
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			IntPtr intPtr = this.m_Handle.DangerousGetHandle();
			IntPtr[] array = new IntPtr[]
			{
				(IntPtr)1,
				intPtr
			};
			TimeValue timeValue = default(TimeValue);
			int num;
			if (microSeconds != -1)
			{
				Socket.MicrosecondsToTimeValue((long)((ulong)microSeconds), ref timeValue);
				num = UnsafeNclNativeMethods.OSSOCK.select(0, (mode == SelectMode.SelectRead) ? array : null, (mode == SelectMode.SelectWrite) ? array : null, (mode == SelectMode.SelectError) ? array : null, ref timeValue);
			}
			else
			{
				num = UnsafeNclNativeMethods.OSSOCK.select(0, (mode == SelectMode.SelectRead) ? array : null, (mode == SelectMode.SelectWrite) ? array : null, (mode == SelectMode.SelectError) ? array : null, IntPtr.Zero);
			}
			if (num == -1)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "Poll", ex);
				}
				throw ex;
			}
			return (int)array[0] != 0 && array[1] == intPtr;
		}

		/// <summary>Determines the status of one or more sockets.</summary>
		/// <param name="checkRead">An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Net.Sockets.Socket" /> instances to check for readability.</param>
		/// <param name="checkWrite">An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Net.Sockets.Socket" /> instances to check for writability.</param>
		/// <param name="checkError">An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Net.Sockets.Socket" /> instances to check for errors.</param>
		/// <param name="microSeconds">The time-out value, in microseconds. A -1 value indicates an infinite time-out.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="checkRead" /> parameter is <see langword="null" /> or empty.  
		///  -and-  
		///  The <paramref name="checkWrite" /> parameter is <see langword="null" /> or empty  
		///  -and-  
		///  The <paramref name="checkError" /> parameter is <see langword="null" /> or empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x0600207A RID: 8314 RVA: 0x0009885C File Offset: 0x00096A5C
		public static void Select(IList checkRead, IList checkWrite, IList checkError, int microSeconds)
		{
			if ((checkRead == null || checkRead.Count == 0) && (checkWrite == null || checkWrite.Count == 0) && (checkError == null || checkError.Count == 0))
			{
				throw new ArgumentNullException(SR.GetString("net_sockets_empty_select"));
			}
			if (checkRead != null && checkRead.Count > 65536)
			{
				throw new ArgumentOutOfRangeException("checkRead", SR.GetString("net_sockets_toolarge_select", new object[]
				{
					"checkRead",
					65536.ToString(NumberFormatInfo.CurrentInfo)
				}));
			}
			if (checkWrite != null && checkWrite.Count > 65536)
			{
				throw new ArgumentOutOfRangeException("checkWrite", SR.GetString("net_sockets_toolarge_select", new object[]
				{
					"checkWrite",
					65536.ToString(NumberFormatInfo.CurrentInfo)
				}));
			}
			if (checkError != null && checkError.Count > 65536)
			{
				throw new ArgumentOutOfRangeException("checkError", SR.GetString("net_sockets_toolarge_select", new object[]
				{
					"checkError",
					65536.ToString(NumberFormatInfo.CurrentInfo)
				}));
			}
			IntPtr[] array = Socket.SocketListToFileDescriptorSet(checkRead);
			IntPtr[] array2 = Socket.SocketListToFileDescriptorSet(checkWrite);
			IntPtr[] array3 = Socket.SocketListToFileDescriptorSet(checkError);
			int num;
			if (microSeconds != -1)
			{
				TimeValue timeValue = default(TimeValue);
				Socket.MicrosecondsToTimeValue((long)((ulong)microSeconds), ref timeValue);
				num = UnsafeNclNativeMethods.OSSOCK.select(0, array, array2, array3, ref timeValue);
			}
			else
			{
				num = UnsafeNclNativeMethods.OSSOCK.select(0, array, array2, array3, IntPtr.Zero);
			}
			if (num == -1)
			{
				throw new SocketException();
			}
			Socket.SelectFileDescriptor(checkRead, array);
			Socket.SelectFileDescriptor(checkWrite, array2);
			Socket.SelectFileDescriptor(checkError, array3);
		}

		/// <summary>Sends the file <paramref name="fileName" /> to a connected <see cref="T:System.Net.Sockets.Socket" /> object using the <see cref="F:System.Net.Sockets.TransmitFileOptions.UseDefaultWorkerThread" /> flag.</summary>
		/// <param name="fileName">A string that contains the path and name of the file to send. This parameter can be <see langword="null" />.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that represents the asynchronous send.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The socket is not connected to a remote host.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below.</exception>
		// Token: 0x0600207B RID: 8315 RVA: 0x000989DB File Offset: 0x00096BDB
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSendFile(string fileName, AsyncCallback callback, object state)
		{
			return this.BeginSendFile(fileName, null, null, TransmitFileOptions.UseDefaultWorkerThread, callback, state);
		}

		/// <summary>Begins an asynchronous request for a remote host connection.</summary>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote host.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connection.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> has been placed in a listening state by calling <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />, or an asynchronous operation is already in progress.</exception>
		// Token: 0x0600207C RID: 8316 RVA: 0x000989EC File Offset: 0x00096BEC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginConnect(EndPoint remoteEP, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginConnect", remoteEP);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (this.isListening)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustnotlisten"));
			}
			if (Interlocked.Exchange(ref this.asyncConnectOperationLock, 1) != 0)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_no_duplicate_async"));
			}
			IAsyncResult asyncResult;
			try
			{
				DnsEndPoint dnsEndPoint = remoteEP as DnsEndPoint;
				if (dnsEndPoint != null)
				{
					if (dnsEndPoint.AddressFamily != AddressFamily.Unspecified && !this.CanTryAddressFamily(dnsEndPoint.AddressFamily))
					{
						throw new NotSupportedException(SR.GetString("net_invalidversion"));
					}
					asyncResult = this.InternalBeginConnectHostName(dnsEndPoint.Host, dnsEndPoint.Port, callback, state);
				}
				else if (this.CanUseConnectEx(remoteEP))
				{
					asyncResult = this.BeginConnectEx(remoteEP, true, callback, state);
				}
				else
				{
					EndPoint endPoint = remoteEP;
					SocketAddress socketAddress = this.CheckCacheRemote(ref endPoint, true);
					ConnectAsyncResult connectAsyncResult = new ConnectAsyncResult(this, endPoint, state, callback);
					connectAsyncResult.StartPostingAsyncOp(false);
					this.DoBeginConnect(endPoint, socketAddress, connectAsyncResult);
					connectAsyncResult.FinishPostingAsyncOp(ref this.Caches.ConnectClosureCache);
					if (Socket.s_LoggingEnabled)
					{
						Logging.Exit(Logging.Sockets, this, "BeginConnect", connectAsyncResult);
					}
					asyncResult = connectAsyncResult;
				}
			}
			catch
			{
				Interlocked.Exchange(ref this.asyncConnectOperationLock, 0);
				throw;
			}
			return asyncResult;
		}

		/// <summary>Duplicates the socket reference for the target process, and closes the socket for this process.</summary>
		/// <param name="targetProcessId">The ID of the target process where a duplicate of the socket reference is created.</param>
		/// <returns>The socket reference to be passed to the target process.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="targetProcessID" /> is not a valid process id.  
		/// -or-  
		/// Duplication of the socket reference failed.</exception>
		// Token: 0x0600207D RID: 8317 RVA: 0x00098B4C File Offset: 0x00096D4C
		public unsafe SocketInformation DuplicateAndClose(int targetProcessId)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "DuplicateAndClose", null);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			ExceptionHelper.UnrestrictedSocketPermission.Demand();
			SocketInformation socketInformation = default(SocketInformation);
			socketInformation.ProtocolInformation = new byte[Socket.protocolInformationSize];
			byte[] array;
			byte* ptr;
			if ((array = socketInformation.ProtocolInformation) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			SocketError socketError = (SocketError)UnsafeNclNativeMethods.OSSOCK.WSADuplicateSocket(this.m_Handle, (uint)targetProcessId, ptr);
			array = null;
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException();
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "DuplicateAndClose", ex);
				}
				throw ex;
			}
			socketInformation.IsConnected = this.Connected;
			socketInformation.IsNonBlocking = !this.Blocking;
			socketInformation.IsListening = this.isListening;
			socketInformation.UseOnlyOverlappedIO = this.UseOnlyOverlappedIO;
			socketInformation.RemoteEndPoint = this.m_RemoteEndPoint;
			this.Close(-1);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "DuplicateAndClose", null);
			}
			return socketInformation;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x00098C6C File Offset: 0x00096E6C
		internal IAsyncResult UnsafeBeginConnect(EndPoint remoteEP, AsyncCallback callback, object state)
		{
			if (this.CanUseConnectEx(remoteEP))
			{
				return this.BeginConnectEx(remoteEP, false, callback, state);
			}
			EndPoint endPoint = remoteEP;
			SocketAddress socketAddress = this.SnapshotAndSerialize(ref endPoint);
			ConnectAsyncResult connectAsyncResult = new ConnectAsyncResult(this, endPoint, state, callback);
			this.DoBeginConnect(endPoint, socketAddress, connectAsyncResult);
			return connectAsyncResult;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00098CAC File Offset: 0x00096EAC
		private void DoBeginConnect(EndPoint endPointSnapshot, SocketAddress socketAddress, LazyAsyncResult asyncResult)
		{
			EndPoint rightEndPoint = this.m_RightEndPoint;
			if (this.m_AcceptQueueOrConnectResult != null)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_no_duplicate_async"));
			}
			this.m_AcceptQueueOrConnectResult = asyncResult;
			if (!this.SetAsyncEventSelect(AsyncEventBits.FdConnect))
			{
				this.m_AcceptQueueOrConnectResult = null;
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			IntPtr intPtr = this.m_Handle.DangerousGetHandle();
			if (this.m_RightEndPoint == null)
			{
				this.m_RightEndPoint = endPointSnapshot;
			}
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAConnect(intPtr, socketAddress.m_Buffer, socketAddress.m_Size, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (socketError != SocketError.Success)
			{
				socketError = (SocketError)Marshal.GetLastWin32Error();
			}
			if (socketError != SocketError.WouldBlock)
			{
				bool flag = true;
				if (socketError == SocketError.Success)
				{
					this.SetToConnected();
				}
				else
				{
					asyncResult.ErrorCode = (int)socketError;
				}
				if (Interlocked.Exchange<RegisteredWaitHandle>(ref this.m_RegisteredWait, null) == null)
				{
					flag = false;
				}
				this.UnsetAsyncEventSelect();
				if (socketError != SocketError.Success)
				{
					this.m_RightEndPoint = rightEndPoint;
					SocketException ex = new SocketException(socketError);
					this.UpdateStatusAfterSocketError(ex);
					this.m_AcceptQueueOrConnectResult = null;
					if (Socket.s_LoggingEnabled)
					{
						Logging.Exception(Logging.Sockets, this, "BeginConnect", ex);
					}
					throw ex;
				}
				if (flag)
				{
					asyncResult.InvokeCallback();
					return;
				}
			}
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x00098DC8 File Offset: 0x00096FC8
		private bool CanUseConnectEx(EndPoint remoteEP)
		{
			return this.socketType == SocketType.Stream && (this.m_RightEndPoint != null || remoteEP.GetType() == typeof(IPEndPoint)) && (Thread.CurrentThread.IsThreadPoolThread || SettingsSectionInternal.Section.AlwaysUseCompletionPortsForConnect || this.m_IsDisconnected);
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00098E20 File Offset: 0x00097020
		private void ConnectCallback()
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)this.m_AcceptQueueOrConnectResult;
			if (lazyAsyncResult.InternalPeekCompleted)
			{
				return;
			}
			NetworkEvents networkEvents = default(NetworkEvents);
			networkEvents.Events = AsyncEventBits.FdConnect;
			SocketError socketError = SocketError.OperationAborted;
			object obj = null;
			try
			{
				if (!this.CleanedUp)
				{
					try
					{
						socketError = UnsafeNclNativeMethods.OSSOCK.WSAEnumNetworkEvents(this.m_Handle, this.m_AsyncEvent.SafeWaitHandle, ref networkEvents);
						if (socketError != SocketError.Success)
						{
							socketError = (SocketError)Marshal.GetLastWin32Error();
						}
						else
						{
							socketError = (SocketError)networkEvents.ErrorCodes[4];
						}
						this.UnsetAsyncEventSelect();
					}
					catch (ObjectDisposedException)
					{
						socketError = SocketError.OperationAborted;
					}
				}
				if (socketError == SocketError.Success)
				{
					this.SetToConnected();
				}
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
				obj = ex;
			}
			if (!lazyAsyncResult.InternalPeekCompleted)
			{
				lazyAsyncResult.ErrorCode = (int)socketError;
				lazyAsyncResult.InvokeCallback(obj);
			}
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The host is specified by a host name and a port number.</summary>
		/// <param name="host">The name of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the connect operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connection.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> has been placed in a listening state by calling <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />, or an asynchronous operation is already in progress.</exception>
		// Token: 0x06002082 RID: 8322 RVA: 0x00098EF0 File Offset: 0x000970F0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginConnect", host);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (this.addressFamily != AddressFamily.InterNetwork && this.addressFamily != AddressFamily.InterNetworkV6)
			{
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
			if (this.isListening)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustnotlisten"));
			}
			if (Interlocked.Exchange(ref this.asyncConnectOperationLock, 1) != 0)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_no_duplicate_async"));
			}
			IAsyncResult asyncResult;
			try
			{
				Socket.MultipleAddressConnectAsyncResult multipleAddressConnectAsyncResult = this.InternalBeginConnectHostName(host, port, requestCallback, state);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exit(Logging.Sockets, this, "BeginConnect", multipleAddressConnectAsyncResult);
				}
				asyncResult = multipleAddressConnectAsyncResult;
			}
			catch
			{
				Interlocked.Exchange(ref this.asyncConnectOperationLock, 0);
				throw;
			}
			return asyncResult;
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The host is specified by an <see cref="T:System.Net.IPAddress" /> and a port number.</summary>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the connect operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connection.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Net.Sockets.Socket" /> is not in the socket family.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> has been placed in a listening state by calling <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />, or an asynchronous operation is already in progress.</exception>
		// Token: 0x06002083 RID: 8323 RVA: 0x00098FF8 File Offset: 0x000971F8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback requestCallback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginConnect", address);
			}
			if (this.CleanedUp)
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
			if (!this.CanTryAddressFamily(address.AddressFamily))
			{
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
			IAsyncResult asyncResult = this.BeginConnect(new IPEndPoint(address, port), requestCallback, state);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginConnect", asyncResult);
			}
			return asyncResult;
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The host is specified by an <see cref="T:System.Net.IPAddress" /> array and a port number.</summary>
		/// <param name="addresses">At least one <see cref="T:System.Net.IPAddress" />, designating the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the connect operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connections.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addresses" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets that use <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> has been placed in a listening state by calling <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />, or an asynchronous operation is already in progress.</exception>
		// Token: 0x06002084 RID: 8324 RVA: 0x000990A4 File Offset: 0x000972A4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback requestCallback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginConnect", addresses);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			if (addresses.Length == 0)
			{
				throw new ArgumentException(SR.GetString("net_invalidAddressList"), "addresses");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (this.addressFamily != AddressFamily.InterNetwork && this.addressFamily != AddressFamily.InterNetworkV6)
			{
				throw new NotSupportedException(SR.GetString("net_invalidversion"));
			}
			if (this.isListening)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustnotlisten"));
			}
			if (Interlocked.Exchange(ref this.asyncConnectOperationLock, 1) != 0)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_no_duplicate_async"));
			}
			IAsyncResult asyncResult;
			try
			{
				Socket.MultipleAddressConnectAsyncResult multipleAddressConnectAsyncResult = new Socket.MultipleAddressConnectAsyncResult(addresses, port, this, state, requestCallback);
				multipleAddressConnectAsyncResult.StartPostingAsyncOp(false);
				if (Socket.DoMultipleAddressConnectCallback(Socket.PostOneBeginConnect(multipleAddressConnectAsyncResult), multipleAddressConnectAsyncResult))
				{
					multipleAddressConnectAsyncResult.InvokeCallback();
				}
				multipleAddressConnectAsyncResult.FinishPostingAsyncOp(ref this.Caches.ConnectClosureCache);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exit(Logging.Sockets, this, "BeginConnect", multipleAddressConnectAsyncResult);
				}
				asyncResult = multipleAddressConnectAsyncResult;
			}
			catch
			{
				Interlocked.Exchange(ref this.asyncConnectOperationLock, 0);
				throw;
			}
			return asyncResult;
		}

		/// <summary>Begins an asynchronous request to disconnect from a remote endpoint.</summary>
		/// <param name="reuseSocket">
		///   <see langword="true" /> if this socket can be reused after the connection is closed; otherwise, <see langword="false" />.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous operation.</returns>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x06002085 RID: 8325 RVA: 0x000991F4 File Offset: 0x000973F4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginDisconnect(bool reuseSocket, AsyncCallback callback, object state)
		{
			DisconnectOverlappedAsyncResult disconnectOverlappedAsyncResult = new DisconnectOverlappedAsyncResult(this, state, callback);
			disconnectOverlappedAsyncResult.StartPostingAsyncOp(false);
			this.DoBeginDisconnect(reuseSocket, disconnectOverlappedAsyncResult);
			disconnectOverlappedAsyncResult.FinishPostingAsyncOp();
			return disconnectOverlappedAsyncResult;
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00099224 File Offset: 0x00097424
		private void DoBeginDisconnect(bool reuseSocket, DisconnectOverlappedAsyncResult asyncResult)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginDisconnect", null);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			asyncResult.SetUnmanagedStructures(null);
			SocketError socketError = SocketError.Success;
			if (!this.DisconnectEx(this.m_Handle, asyncResult.OverlappedHandle, reuseSocket ? 2 : 0, 0))
			{
				socketError = (SocketError)Marshal.GetLastWin32Error();
			}
			if (socketError == SocketError.Success)
			{
				this.SetToDisconnected();
				this.m_RemoteEndPoint = null;
			}
			socketError = asyncResult.CheckAsyncCallOverlappedResult(socketError);
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginDisconnect", ex);
				}
				throw ex;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginDisconnect", asyncResult);
			}
		}

		/// <summary>Closes the socket connection and allows reuse of the socket.</summary>
		/// <param name="reuseSocket">
		///   <see langword="true" /> if this socket can be reused after the current connection is closed; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">This method requires Windows 2000 or earlier, or the exception will be thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x06002087 RID: 8327 RVA: 0x000992F0 File Offset: 0x000974F0
		public void Disconnect(bool reuseSocket)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Disconnect", null);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			SocketError socketError = SocketError.Success;
			if (!this.DisconnectEx_Blocking(this.m_Handle.DangerousGetHandle(), IntPtr.Zero, reuseSocket ? 2 : 0, 0))
			{
				socketError = (SocketError)Marshal.GetLastWin32Error();
			}
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "Disconnect", ex);
				}
				throw ex;
			}
			this.SetToDisconnected();
			this.m_RemoteEndPoint = null;
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Disconnect", null);
			}
		}

		/// <summary>Ends a pending asynchronous connection request.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginConnect(System.Net.EndPoint,System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndConnect(System.IAsyncResult)" /> was previously called for the asynchronous connection.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002088 RID: 8328 RVA: 0x000993AC File Offset: 0x000975AC
		public void EndConnect(IAsyncResult asyncResult)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "EndConnect", asyncResult);
			}
			try
			{
				this.InternalEndConnect(asyncResult);
			}
			finally
			{
				Interlocked.Exchange(ref this.asyncConnectOperationLock, 0);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "EndConnect", "");
			}
		}

		/// <summary>Ends a pending asynchronous disconnect request.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information and any user-defined data for this asynchronous operation.</param>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginDisconnect(System.Boolean,System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndDisconnect(System.IAsyncResult)" /> was previously called for the asynchronous connection.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.Net.WebException">The disconnect request has timed out.</exception>
		// Token: 0x06002089 RID: 8329 RVA: 0x0009941C File Offset: 0x0009761C
		public void EndDisconnect(IAsyncResult asyncResult)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "EndDisconnect", asyncResult);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null || lazyAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndDisconnect" }));
			}
			lazyAsyncResult.InternalWaitForCompletion();
			lazyAsyncResult.EndCalled = true;
			if (lazyAsyncResult.ErrorCode != 0)
			{
				SocketException ex = new SocketException(lazyAsyncResult.ErrorCode);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "EndDisconnect", ex);
				}
				throw ex;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "EndDisconnect", null);
			}
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to send.</param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to begin sending data.</param>
		/// <param name="size">The number of bytes to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is less than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x0600208A RID: 8330 RVA: 0x00099518 File Offset: 0x00097718
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			SocketError socketError;
			IAsyncResult asyncResult = this.BeginSend(buffer, offset, size, socketFlags, out socketError, callback, state);
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				throw new SocketException(socketError);
			}
			return asyncResult;
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to send.</param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to begin sending data.</param>
		/// <param name="size">The number of bytes to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is less than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x0600208B RID: 8331 RVA: 0x0009954C File Offset: 0x0009774C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginSend", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			OverlappedAsyncResult overlappedAsyncResult = new OverlappedAsyncResult(this, state, callback);
			overlappedAsyncResult.StartPostingAsyncOp(false);
			errorCode = this.DoBeginSend(buffer, offset, size, socketFlags, overlappedAsyncResult);
			if (errorCode != SocketError.Success && errorCode != SocketError.IOPending)
			{
				overlappedAsyncResult = null;
			}
			else
			{
				overlappedAsyncResult.FinishPostingAsyncOp(ref this.Caches.SendClosureCache);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginSend", overlappedAsyncResult);
			}
			return overlappedAsyncResult;
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0009962C File Offset: 0x0009782C
		internal IAsyncResult UnsafeBeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "UnsafeBeginSend", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			OverlappedAsyncResult overlappedAsyncResult = new OverlappedAsyncResult(this, state, callback);
			SocketError socketError = this.DoBeginSend(buffer, offset, size, socketFlags, overlappedAsyncResult);
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				throw new SocketException(socketError);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "UnsafeBeginSend", overlappedAsyncResult);
			}
			return overlappedAsyncResult;
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000996B8 File Offset: 0x000978B8
		private SocketError DoBeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, OverlappedAsyncResult asyncResult)
		{
			SocketError socketError = SocketError.SocketError;
			try
			{
				asyncResult.SetUnmanagedStructures(buffer, offset, size, null, false, ref this.Caches.SendOverlappedCache);
				int num;
				socketError = UnsafeNclNativeMethods.OSSOCK.WSASend(this.m_Handle, ref asyncResult.m_SingleBuffer, 1, out num, socketFlags, asyncResult.OverlappedHandle, IntPtr.Zero);
				if (socketError != SocketError.Success)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			finally
			{
				socketError = asyncResult.CheckAsyncCallOverlappedResult(socketError);
			}
			if (socketError != SocketError.Success)
			{
				asyncResult.ExtractCache(ref this.Caches.SendOverlappedCache);
				this.UpdateStatusAfterSocketError(socketError);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginSend", new SocketException(socketError));
				}
			}
			return socketError;
		}

		/// <summary>Sends a file and buffers of data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <param name="fileName">A string that contains the path and name of the file to be sent. This parameter can be <see langword="null" />.</param>
		/// <param name="preBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent before the file is sent. This parameter can be <see langword="null" />.</param>
		/// <param name="postBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent after the file is sent. This parameter can be <see langword="null" />.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Net.Sockets.TransmitFileOptions" /> values.</param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate to be invoked when this operation completes. This parameter can be <see langword="null" />.</param>
		/// <param name="state">A user-defined object that contains state information for this request. This parameter can be <see langword="null" />.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below.</exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is not Windows NT or later.  
		/// -or-
		///  The socket is not connected to a remote host.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found.</exception>
		// Token: 0x0600208E RID: 8334 RVA: 0x00099764 File Offset: 0x00097964
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSendFile(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags, AsyncCallback callback, object state)
		{
			TransmitFileOverlappedAsyncResult transmitFileOverlappedAsyncResult = new TransmitFileOverlappedAsyncResult(this, state, callback);
			transmitFileOverlappedAsyncResult.StartPostingAsyncOp(false);
			this.DoBeginSendFile(fileName, preBuffer, postBuffer, flags, transmitFileOverlappedAsyncResult);
			transmitFileOverlappedAsyncResult.FinishPostingAsyncOp(ref this.Caches.SendClosureCache);
			return transmitFileOverlappedAsyncResult;
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000997A4 File Offset: 0x000979A4
		private void DoBeginSendFile(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags, TransmitFileOverlappedAsyncResult asyncResult)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginSendFile", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.Connected)
			{
				throw new NotSupportedException(SR.GetString("net_notconnected"));
			}
			FileStream fileStream = null;
			if (fileName != null && fileName.Length > 0)
			{
				fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			SafeHandle safeHandle = null;
			if (fileStream != null)
			{
				ExceptionHelper.UnmanagedPermission.Assert();
				try
				{
					safeHandle = fileStream.SafeFileHandle;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			SocketError socketError = SocketError.SocketError;
			try
			{
				asyncResult.SetUnmanagedStructures(preBuffer, postBuffer, fileStream, flags, ref this.Caches.SendOverlappedCache);
				bool flag;
				if (safeHandle != null)
				{
					flag = UnsafeNclNativeMethods.OSSOCK.TransmitFile(this.m_Handle, safeHandle, 0, 0, asyncResult.OverlappedHandle, asyncResult.TransmitFileBuffers, flags);
				}
				else
				{
					flag = UnsafeNclNativeMethods.OSSOCK.TransmitFile2(this.m_Handle, IntPtr.Zero, 0, 0, asyncResult.OverlappedHandle, asyncResult.TransmitFileBuffers, flags);
				}
				if (!flag)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
				else
				{
					socketError = SocketError.Success;
				}
			}
			finally
			{
				socketError = asyncResult.CheckAsyncCallOverlappedResult(socketError);
			}
			if (socketError != SocketError.Success)
			{
				asyncResult.ExtractCache(ref this.Caches.SendOverlappedCache);
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginSendFile", ex);
				}
				throw ex;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginSendFile", socketError);
			}
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that contains the data to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002090 RID: 8336 RVA: 0x00099948 File Offset: 0x00097B48
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			SocketError socketError;
			IAsyncResult asyncResult = this.BeginSend(buffers, socketFlags, out socketError, callback, state);
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				throw new SocketException(socketError);
			}
			return asyncResult;
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that contains the data to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002091 RID: 8337 RVA: 0x00099978 File Offset: 0x00097B78
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginSend", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			if (buffers.Count == 0)
			{
				throw new ArgumentException(SR.GetString("net_sockets_zerolist", new object[] { "buffers" }), "buffers");
			}
			OverlappedAsyncResult overlappedAsyncResult = new OverlappedAsyncResult(this, state, callback);
			overlappedAsyncResult.StartPostingAsyncOp(false);
			errorCode = this.DoBeginSend(buffers, socketFlags, overlappedAsyncResult);
			overlappedAsyncResult.FinishPostingAsyncOp(ref this.Caches.SendClosureCache);
			if (errorCode != SocketError.Success && errorCode != SocketError.IOPending)
			{
				overlappedAsyncResult = null;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginSend", overlappedAsyncResult);
			}
			return overlappedAsyncResult;
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00099A50 File Offset: 0x00097C50
		private SocketError DoBeginSend(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, OverlappedAsyncResult asyncResult)
		{
			SocketError socketError = SocketError.SocketError;
			try
			{
				asyncResult.SetUnmanagedStructures(buffers, ref this.Caches.SendOverlappedCache);
				int num;
				socketError = UnsafeNclNativeMethods.OSSOCK.WSASend(this.m_Handle, asyncResult.m_WSABuffers, asyncResult.m_WSABuffers.Length, out num, socketFlags, asyncResult.OverlappedHandle, IntPtr.Zero);
				if (socketError != SocketError.Success)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			finally
			{
				socketError = asyncResult.CheckAsyncCallOverlappedResult(socketError);
			}
			if (socketError != SocketError.Success)
			{
				asyncResult.ExtractCache(ref this.Caches.SendOverlappedCache);
				this.UpdateStatusAfterSocketError(socketError);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginSend", new SocketException(socketError));
				}
			}
			return socketError;
		}

		/// <summary>Ends a pending asynchronous send.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information for this asynchronous operation.</param>
		/// <returns>If successful, the number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />; otherwise, an invalid <see cref="T:System.Net.Sockets.Socket" /> error.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSend(System.IAsyncResult)" /> was previously called for the asynchronous send.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002093 RID: 8339 RVA: 0x00099AF8 File Offset: 0x00097CF8
		public int EndSend(IAsyncResult asyncResult)
		{
			SocketError socketError;
			int num = this.EndSend(asyncResult, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Ends a pending asynchronous send.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information for this asynchronous operation.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <returns>If successful, the number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />; otherwise, an invalid <see cref="T:System.Net.Sockets.Socket" /> error.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSend(System.IAsyncResult)" /> was previously called for the asynchronous send.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002094 RID: 8340 RVA: 0x00099B1C File Offset: 0x00097D1C
		public int EndSend(IAsyncResult asyncResult, out SocketError errorCode)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "EndSend", asyncResult);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			OverlappedAsyncResult overlappedAsyncResult = asyncResult as OverlappedAsyncResult;
			if (overlappedAsyncResult == null || overlappedAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (overlappedAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndSend" }));
			}
			int num = (int)overlappedAsyncResult.InternalWaitForCompletion();
			overlappedAsyncResult.EndCalled = true;
			overlappedAsyncResult.ExtractCache(ref this.Caches.SendOverlappedCache);
			if (Socket.s_PerfCountersEnabled && num > 0)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesSent, (long)num);
				if (this.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsSent);
				}
			}
			errorCode = (SocketError)overlappedAsyncResult.ErrorCode;
			if (errorCode != SocketError.Success)
			{
				this.UpdateStatusAfterSocketError(errorCode);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "EndSend", new SocketException(errorCode));
					Logging.Exit(Logging.Sockets, this, "EndSend", 0);
				}
				return 0;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "EndSend", num);
			}
			return num;
		}

		/// <summary>Ends a pending asynchronous send of a file.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information for this asynchronous operation.</param>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is empty.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSendFile(System.String,System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSendFile(System.IAsyncResult)" /> was previously called for the asynchronous <see cref="M:System.Net.Sockets.Socket.BeginSendFile(System.String,System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below.</exception>
		// Token: 0x06002095 RID: 8341 RVA: 0x00099C78 File Offset: 0x00097E78
		public void EndSendFile(IAsyncResult asyncResult)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "EndSendFile", asyncResult);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			TransmitFileOverlappedAsyncResult transmitFileOverlappedAsyncResult = asyncResult as TransmitFileOverlappedAsyncResult;
			if (transmitFileOverlappedAsyncResult == null || transmitFileOverlappedAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (transmitFileOverlappedAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndSendFile" }));
			}
			transmitFileOverlappedAsyncResult.InternalWaitForCompletion();
			transmitFileOverlappedAsyncResult.EndCalled = true;
			transmitFileOverlappedAsyncResult.ExtractCache(ref this.Caches.SendOverlappedCache);
			if ((transmitFileOverlappedAsyncResult.Flags & (TransmitFileOptions.Disconnect | TransmitFileOptions.ReuseSocket)) != TransmitFileOptions.UseDefaultWorkerThread)
			{
				this.SetToDisconnected();
				this.m_RemoteEndPoint = null;
			}
			if (transmitFileOverlappedAsyncResult.ErrorCode != 0)
			{
				SocketException ex = new SocketException(transmitFileOverlappedAsyncResult.ErrorCode);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "EndSendFile", ex);
				}
				throw ex;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "EndSendFile", "");
			}
		}

		/// <summary>Sends data asynchronously to a specific remote host.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to send.</param>
		/// <param name="offset">The zero-based position in <paramref name="buffer" /> at which to begin sending data.</param>
		/// <param name="size">The number of bytes to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote device.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		// Token: 0x06002096 RID: 8342 RVA: 0x00099DA0 File Offset: 0x00097FA0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSendTo(byte[] buffer, int offset, int size, SocketFlags socketFlags, EndPoint remoteEP, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginSendTo", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			EndPoint endPoint = remoteEP;
			SocketAddress socketAddress = this.CheckCacheRemote(ref endPoint, false);
			OverlappedAsyncResult overlappedAsyncResult = new OverlappedAsyncResult(this, state, callback);
			overlappedAsyncResult.StartPostingAsyncOp(false);
			this.DoBeginSendTo(buffer, offset, size, socketFlags, endPoint, socketAddress, overlappedAsyncResult);
			overlappedAsyncResult.FinishPostingAsyncOp(ref this.Caches.SendClosureCache);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginSendTo", overlappedAsyncResult);
			}
			return overlappedAsyncResult;
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x00099E88 File Offset: 0x00098088
		private void DoBeginSendTo(byte[] buffer, int offset, int size, SocketFlags socketFlags, EndPoint endPointSnapshot, SocketAddress socketAddress, OverlappedAsyncResult asyncResult)
		{
			EndPoint rightEndPoint = this.m_RightEndPoint;
			SocketError socketError = SocketError.SocketError;
			try
			{
				asyncResult.SetUnmanagedStructures(buffer, offset, size, socketAddress, false, ref this.Caches.SendOverlappedCache);
				if (this.m_RightEndPoint == null)
				{
					this.m_RightEndPoint = endPointSnapshot;
				}
				int num;
				socketError = UnsafeNclNativeMethods.OSSOCK.WSASendTo(this.m_Handle, ref asyncResult.m_SingleBuffer, 1, out num, socketFlags, asyncResult.GetSocketAddressPtr(), asyncResult.SocketAddress.Size, asyncResult.OverlappedHandle, IntPtr.Zero);
				if (socketError != SocketError.Success)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			catch (ObjectDisposedException)
			{
				this.m_RightEndPoint = rightEndPoint;
				throw;
			}
			finally
			{
				socketError = asyncResult.CheckAsyncCallOverlappedResult(socketError);
			}
			if (socketError != SocketError.Success)
			{
				this.m_RightEndPoint = rightEndPoint;
				asyncResult.ExtractCache(ref this.Caches.SendOverlappedCache);
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginSendTo", ex);
				}
				throw ex;
			}
		}

		/// <summary>Ends a pending asynchronous send to a specific location.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</param>
		/// <returns>If successful, the number of bytes sent; otherwise, an invalid <see cref="T:System.Net.Sockets.Socket" /> error.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSendTo(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint,System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSendTo(System.IAsyncResult)" /> was previously called for the asynchronous send.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06002098 RID: 8344 RVA: 0x00099F80 File Offset: 0x00098180
		public int EndSendTo(IAsyncResult asyncResult)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "EndSendTo", asyncResult);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			OverlappedAsyncResult overlappedAsyncResult = asyncResult as OverlappedAsyncResult;
			if (overlappedAsyncResult == null || overlappedAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (overlappedAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndSendTo" }));
			}
			int num = (int)overlappedAsyncResult.InternalWaitForCompletion();
			overlappedAsyncResult.EndCalled = true;
			overlappedAsyncResult.ExtractCache(ref this.Caches.SendOverlappedCache);
			if (Socket.s_PerfCountersEnabled && num > 0)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesSent, (long)num);
				if (this.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsSent);
				}
			}
			if (overlappedAsyncResult.ErrorCode != 0)
			{
				SocketException ex = new SocketException(overlappedAsyncResult.ErrorCode);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "EndSendTo", ex);
				}
				throw ex;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "EndSendTo", num);
			}
			return num;
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to store the received data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		// Token: 0x06002099 RID: 8345 RVA: 0x0009A0C8 File Offset: 0x000982C8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			SocketError socketError;
			IAsyncResult asyncResult = this.BeginReceive(buffer, offset, size, socketFlags, out socketError, callback, state);
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				throw new SocketException(socketError);
			}
			return asyncResult;
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> to store the received data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		// Token: 0x0600209A RID: 8346 RVA: 0x0009A0FC File Offset: 0x000982FC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginReceive", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			OverlappedAsyncResult overlappedAsyncResult = new OverlappedAsyncResult(this, state, callback);
			overlappedAsyncResult.StartPostingAsyncOp(false);
			errorCode = this.DoBeginReceive(buffer, offset, size, socketFlags, overlappedAsyncResult);
			if (errorCode != SocketError.Success && errorCode != SocketError.IOPending)
			{
				overlappedAsyncResult = null;
			}
			else
			{
				overlappedAsyncResult.FinishPostingAsyncOp(ref this.Caches.ReceiveClosureCache);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginReceive", overlappedAsyncResult);
			}
			return overlappedAsyncResult;
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x0009A1DC File Offset: 0x000983DC
		internal IAsyncResult UnsafeBeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "UnsafeBeginReceive", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			OverlappedAsyncResult overlappedAsyncResult = new OverlappedAsyncResult(this, state, callback);
			this.DoBeginReceive(buffer, offset, size, socketFlags, overlappedAsyncResult);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "UnsafeBeginReceive", overlappedAsyncResult);
			}
			return overlappedAsyncResult;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x0009A254 File Offset: 0x00098454
		private SocketError DoBeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, OverlappedAsyncResult asyncResult)
		{
			SocketError socketError = SocketError.SocketError;
			try
			{
				asyncResult.SetUnmanagedStructures(buffer, offset, size, null, false, ref this.Caches.ReceiveOverlappedCache);
				int num;
				socketError = UnsafeNclNativeMethods.OSSOCK.WSARecv(this.m_Handle, ref asyncResult.m_SingleBuffer, 1, out num, ref socketFlags, asyncResult.OverlappedHandle, IntPtr.Zero);
				if (socketError != SocketError.Success)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			finally
			{
				socketError = asyncResult.CheckAsyncCallOverlappedResult(socketError);
			}
			if (socketError != SocketError.Success)
			{
				asyncResult.ExtractCache(ref this.Caches.ReceiveOverlappedCache);
				this.UpdateStatusAfterSocketError(socketError);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginReceive", new SocketException(socketError));
				}
				asyncResult.InvokeCallback(new SocketException(socketError));
			}
			return socketError;
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x0600209D RID: 8349 RVA: 0x0009A30C File Offset: 0x0009850C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginReceive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			SocketError socketError;
			IAsyncResult asyncResult = this.BeginReceive(buffers, socketFlags, out socketError, callback, state);
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				throw new SocketException(socketError);
			}
			return asyncResult;
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x0600209E RID: 8350 RVA: 0x0009A33C File Offset: 0x0009853C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginReceive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginReceive", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			if (buffers.Count == 0)
			{
				throw new ArgumentException(SR.GetString("net_sockets_zerolist", new object[] { "buffers" }), "buffers");
			}
			OverlappedAsyncResult overlappedAsyncResult = new OverlappedAsyncResult(this, state, callback);
			overlappedAsyncResult.StartPostingAsyncOp(false);
			errorCode = this.DoBeginReceive(buffers, socketFlags, overlappedAsyncResult);
			if (errorCode != SocketError.Success && errorCode != SocketError.IOPending)
			{
				overlappedAsyncResult = null;
			}
			else
			{
				overlappedAsyncResult.FinishPostingAsyncOp(ref this.Caches.ReceiveClosureCache);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginReceive", overlappedAsyncResult);
			}
			return overlappedAsyncResult;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x0009A418 File Offset: 0x00098618
		private SocketError DoBeginReceive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, OverlappedAsyncResult asyncResult)
		{
			SocketError socketError = SocketError.SocketError;
			try
			{
				asyncResult.SetUnmanagedStructures(buffers, ref this.Caches.ReceiveOverlappedCache);
				int num;
				socketError = UnsafeNclNativeMethods.OSSOCK.WSARecv(this.m_Handle, asyncResult.m_WSABuffers, asyncResult.m_WSABuffers.Length, out num, ref socketFlags, asyncResult.OverlappedHandle, IntPtr.Zero);
				if (socketError != SocketError.Success)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			finally
			{
				socketError = asyncResult.CheckAsyncCallOverlappedResult(socketError);
			}
			if (socketError != SocketError.Success)
			{
				asyncResult.ExtractCache(ref this.Caches.ReceiveOverlappedCache);
				this.UpdateStatusAfterSocketError(socketError);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginReceive", new SocketException(socketError));
				}
			}
			return socketError;
		}

		/// <summary>Ends a pending asynchronous read.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceive(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> was previously called for the asynchronous read.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x060020A0 RID: 8352 RVA: 0x0009A4C4 File Offset: 0x000986C4
		public int EndReceive(IAsyncResult asyncResult)
		{
			SocketError socketError;
			int num = this.EndReceive(asyncResult, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Ends a pending asynchronous read.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <returns>The number of bytes received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceive(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> was previously called for the asynchronous read.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x060020A1 RID: 8353 RVA: 0x0009A4E8 File Offset: 0x000986E8
		public int EndReceive(IAsyncResult asyncResult, out SocketError errorCode)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "EndReceive", asyncResult);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			OverlappedAsyncResult overlappedAsyncResult = asyncResult as OverlappedAsyncResult;
			if (overlappedAsyncResult == null || overlappedAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (overlappedAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndReceive" }));
			}
			int num = (int)overlappedAsyncResult.InternalWaitForCompletion();
			overlappedAsyncResult.EndCalled = true;
			overlappedAsyncResult.ExtractCache(ref this.Caches.ReceiveOverlappedCache);
			if (Socket.s_PerfCountersEnabled && num > 0)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesReceived, (long)num);
				if (this.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsReceived);
				}
			}
			errorCode = (SocketError)overlappedAsyncResult.ErrorCode;
			if (errorCode != SocketError.Success)
			{
				this.UpdateStatusAfterSocketError(errorCode);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "EndReceive", new SocketException(errorCode));
					Logging.Exit(Logging.Sockets, this, "EndReceive", 0);
				}
				return 0;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "EndReceive", num);
			}
			return num;
		}

		/// <summary>Begins to asynchronously receive the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint and packet information.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to store the data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the source of the data.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP.</exception>
		// Token: 0x060020A2 RID: 8354 RVA: 0x0009A644 File Offset: 0x00098844
		public IAsyncResult BeginReceiveMessageFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginReceiveMessageFrom", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (!this.CanTryAddressFamily(remoteEP.AddressFamily))
			{
				throw new ArgumentException(SR.GetString("net_InvalidEndPointAddressFamily", new object[] { remoteEP.AddressFamily, this.addressFamily }), "remoteEP");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (this.m_RightEndPoint == null)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustbind"));
			}
			ReceiveMessageOverlappedAsyncResult receiveMessageOverlappedAsyncResult = new ReceiveMessageOverlappedAsyncResult(this, state, callback);
			receiveMessageOverlappedAsyncResult.StartPostingAsyncOp(false);
			EndPoint rightEndPoint = this.m_RightEndPoint;
			SocketAddress socketAddress = this.SnapshotAndSerialize(ref remoteEP);
			SocketError socketError = SocketError.SocketError;
			try
			{
				receiveMessageOverlappedAsyncResult.SetUnmanagedStructures(buffer, offset, size, socketAddress, socketFlags, ref this.Caches.ReceiveOverlappedCache);
				receiveMessageOverlappedAsyncResult.SocketAddressOriginal = remoteEP.Serialize();
				this.SetReceivingPacketInformation();
				if (this.m_RightEndPoint == null)
				{
					this.m_RightEndPoint = remoteEP;
				}
				int num;
				socketError = this.WSARecvMsg(this.m_Handle, Marshal.UnsafeAddrOfPinnedArrayElement(receiveMessageOverlappedAsyncResult.m_MessageBuffer, 0), out num, receiveMessageOverlappedAsyncResult.OverlappedHandle, IntPtr.Zero);
				if (socketError != SocketError.Success)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
					if (socketError == SocketError.MessageSize)
					{
						socketError = SocketError.IOPending;
					}
				}
			}
			catch (ObjectDisposedException)
			{
				this.m_RightEndPoint = rightEndPoint;
				throw;
			}
			finally
			{
				socketError = receiveMessageOverlappedAsyncResult.CheckAsyncCallOverlappedResult(socketError);
			}
			if (socketError != SocketError.Success)
			{
				this.m_RightEndPoint = rightEndPoint;
				receiveMessageOverlappedAsyncResult.ExtractCache(ref this.Caches.ReceiveOverlappedCache);
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginReceiveMessageFrom", ex);
				}
				throw ex;
			}
			receiveMessageOverlappedAsyncResult.FinishPostingAsyncOp(ref this.Caches.ReceiveClosureCache);
			if (receiveMessageOverlappedAsyncResult.CompletedSynchronously && !receiveMessageOverlappedAsyncResult.SocketAddressOriginal.Equals(receiveMessageOverlappedAsyncResult.SocketAddress))
			{
				try
				{
					remoteEP = remoteEP.Create(receiveMessageOverlappedAsyncResult.SocketAddress);
				}
				catch
				{
				}
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginReceiveMessageFrom", receiveMessageOverlappedAsyncResult);
			}
			return receiveMessageOverlappedAsyncResult;
		}

		/// <summary>Ends a pending asynchronous read from a specific endpoint. This method also reveals more information about the packet than <see cref="M:System.Net.Sockets.Socket.EndReceiveFrom(System.IAsyncResult,System.Net.EndPoint@)" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values for the received packet.</param>
		/// <param name="endPoint">The source <see cref="T:System.Net.EndPoint" />.</param>
		/// <param name="ipPacketInformation">The <see cref="T:System.Net.IPAddress" /> and interface of the received packet.</param>
		/// <returns>If successful, the number of bytes received. If unsuccessful, returns 0.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />  
		/// -or-  
		/// <paramref name="endPoint" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint@,System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> was previously called for the asynchronous read.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x060020A3 RID: 8355 RVA: 0x0009A8B8 File Offset: 0x00098AB8
		public int EndReceiveMessageFrom(IAsyncResult asyncResult, ref SocketFlags socketFlags, ref EndPoint endPoint, out IPPacketInformation ipPacketInformation)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "EndReceiveMessageFrom", asyncResult);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			if (!this.CanTryAddressFamily(endPoint.AddressFamily))
			{
				throw new ArgumentException(SR.GetString("net_InvalidEndPointAddressFamily", new object[] { endPoint.AddressFamily, this.addressFamily }), "endPoint");
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			ReceiveMessageOverlappedAsyncResult receiveMessageOverlappedAsyncResult = asyncResult as ReceiveMessageOverlappedAsyncResult;
			if (receiveMessageOverlappedAsyncResult == null || receiveMessageOverlappedAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (receiveMessageOverlappedAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndReceiveMessageFrom" }));
			}
			SocketAddress socketAddress = this.SnapshotAndSerialize(ref endPoint);
			int num = (int)receiveMessageOverlappedAsyncResult.InternalWaitForCompletion();
			receiveMessageOverlappedAsyncResult.EndCalled = true;
			receiveMessageOverlappedAsyncResult.ExtractCache(ref this.Caches.ReceiveOverlappedCache);
			receiveMessageOverlappedAsyncResult.SocketAddress.SetSize(receiveMessageOverlappedAsyncResult.GetSocketAddressSizePtr());
			if (!socketAddress.Equals(receiveMessageOverlappedAsyncResult.SocketAddress))
			{
				try
				{
					endPoint = endPoint.Create(receiveMessageOverlappedAsyncResult.SocketAddress);
				}
				catch
				{
				}
			}
			if (Socket.s_PerfCountersEnabled && num > 0)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesReceived, (long)num);
				if (this.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsReceived);
				}
			}
			if (receiveMessageOverlappedAsyncResult.ErrorCode != 0 && receiveMessageOverlappedAsyncResult.ErrorCode != 10040)
			{
				SocketException ex = new SocketException(receiveMessageOverlappedAsyncResult.ErrorCode);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "EndReceiveMessageFrom", ex);
				}
				throw ex;
			}
			socketFlags = receiveMessageOverlappedAsyncResult.m_flags;
			ipPacketInformation = receiveMessageOverlappedAsyncResult.m_IPPacketInformation;
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "EndReceiveMessageFrom", num);
			}
			return num;
		}

		/// <summary>Begins to asynchronously receive data from a specified network device.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to store the data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the source of the data.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="remoteEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="size" /> is less than 0.  
		/// -or-  
		/// <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		// Token: 0x060020A4 RID: 8356 RVA: 0x0009AAC0 File Offset: 0x00098CC0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginReceiveFrom", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (!this.CanTryAddressFamily(remoteEP.AddressFamily))
			{
				throw new ArgumentException(SR.GetString("net_InvalidEndPointAddressFamily", new object[] { remoteEP.AddressFamily, this.addressFamily }), "remoteEP");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (this.m_RightEndPoint == null)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustbind"));
			}
			SocketAddress socketAddress = this.SnapshotAndSerialize(ref remoteEP);
			OverlappedAsyncResult overlappedAsyncResult = new ReceiveFromOverlappedAsyncResult(this, state, callback);
			overlappedAsyncResult.StartPostingAsyncOp(false);
			this.DoBeginReceiveFrom(buffer, offset, size, socketFlags, remoteEP, socketAddress, overlappedAsyncResult);
			overlappedAsyncResult.FinishPostingAsyncOp(ref this.Caches.ReceiveClosureCache);
			if (overlappedAsyncResult.CompletedSynchronously && !overlappedAsyncResult.SocketAddressOriginal.Equals(overlappedAsyncResult.SocketAddress))
			{
				try
				{
					remoteEP = remoteEP.Create(overlappedAsyncResult.SocketAddress);
				}
				catch
				{
				}
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginReceiveFrom", overlappedAsyncResult);
			}
			return overlappedAsyncResult;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x0009AC4C File Offset: 0x00098E4C
		private void DoBeginReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, EndPoint endPointSnapshot, SocketAddress socketAddress, OverlappedAsyncResult asyncResult)
		{
			EndPoint rightEndPoint = this.m_RightEndPoint;
			SocketError socketError = SocketError.SocketError;
			try
			{
				asyncResult.SetUnmanagedStructures(buffer, offset, size, socketAddress, true, ref this.Caches.ReceiveOverlappedCache);
				asyncResult.SocketAddressOriginal = endPointSnapshot.Serialize();
				if (this.m_RightEndPoint == null)
				{
					this.m_RightEndPoint = endPointSnapshot;
				}
				int num;
				socketError = UnsafeNclNativeMethods.OSSOCK.WSARecvFrom(this.m_Handle, ref asyncResult.m_SingleBuffer, 1, out num, ref socketFlags, asyncResult.GetSocketAddressPtr(), asyncResult.GetSocketAddressSizePtr(), asyncResult.OverlappedHandle, IntPtr.Zero);
				if (socketError != SocketError.Success)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			catch (ObjectDisposedException)
			{
				this.m_RightEndPoint = rightEndPoint;
				throw;
			}
			finally
			{
				socketError = asyncResult.CheckAsyncCallOverlappedResult(socketError);
			}
			if (socketError != SocketError.Success)
			{
				this.m_RightEndPoint = rightEndPoint;
				asyncResult.ExtractCache(ref this.Caches.ReceiveOverlappedCache);
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginReceiveFrom", ex);
				}
				throw ex;
			}
		}

		/// <summary>Ends a pending asynchronous read from a specific endpoint.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</param>
		/// <param name="endPoint">The source <see cref="T:System.Net.EndPoint" />.</param>
		/// <returns>If successful, the number of bytes received. If unsuccessful, returns 0.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceiveFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint@,System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceiveFrom(System.IAsyncResult,System.Net.EndPoint@)" /> was previously called for the asynchronous read.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x060020A6 RID: 8358 RVA: 0x0009AD4C File Offset: 0x00098F4C
		public int EndReceiveFrom(IAsyncResult asyncResult, ref EndPoint endPoint)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "EndReceiveFrom", asyncResult);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			if (!this.CanTryAddressFamily(endPoint.AddressFamily))
			{
				throw new ArgumentException(SR.GetString("net_InvalidEndPointAddressFamily", new object[] { endPoint.AddressFamily, this.addressFamily }), "endPoint");
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			OverlappedAsyncResult overlappedAsyncResult = asyncResult as OverlappedAsyncResult;
			if (overlappedAsyncResult == null || overlappedAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (overlappedAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndReceiveFrom" }));
			}
			SocketAddress socketAddress = this.SnapshotAndSerialize(ref endPoint);
			int num = (int)overlappedAsyncResult.InternalWaitForCompletion();
			overlappedAsyncResult.EndCalled = true;
			overlappedAsyncResult.ExtractCache(ref this.Caches.ReceiveOverlappedCache);
			if (!socketAddress.Equals(overlappedAsyncResult.SocketAddress))
			{
				try
				{
					endPoint = endPoint.Create(overlappedAsyncResult.SocketAddress);
				}
				catch
				{
				}
			}
			if (Socket.s_PerfCountersEnabled && num > 0)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesReceived, (long)num);
				if (this.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsReceived);
				}
			}
			if (overlappedAsyncResult.ErrorCode != 0)
			{
				SocketException ex = new SocketException(overlappedAsyncResult.ErrorCode);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "EndReceiveFrom", ex);
				}
				throw ex;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "EndReceiveFrom", num);
			}
			return num;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous <see cref="T:System.Net.Sockets.Socket" /> creation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method.</exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.  
		///  -or-  
		///  The accepted socket is bound.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="receiveSize" /> is less than 0.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x060020A7 RID: 8359 RVA: 0x0009AF24 File Offset: 0x00099124
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAccept(AsyncCallback callback, object state)
		{
			if (this.CanUseAcceptEx)
			{
				return this.BeginAccept(0, callback, state);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginAccept", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			AcceptAsyncResult acceptAsyncResult = new AcceptAsyncResult(this, state, callback);
			acceptAsyncResult.StartPostingAsyncOp(false);
			this.DoBeginAccept(acceptAsyncResult);
			acceptAsyncResult.FinishPostingAsyncOp(ref this.Caches.AcceptClosureCache);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginAccept", acceptAsyncResult);
			}
			return acceptAsyncResult;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0009AFC0 File Offset: 0x000991C0
		private void DoBeginAccept(LazyAsyncResult asyncResult)
		{
			if (this.m_RightEndPoint == null)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustbind"));
			}
			if (!this.isListening)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustlisten"));
			}
			bool flag = false;
			SocketError socketError = SocketError.Success;
			Queue acceptQueue = this.GetAcceptQueue();
			lock (this)
			{
				if (acceptQueue.Count == 0)
				{
					SocketAddress socketAddress = this.m_RightEndPoint.Serialize();
					this.InternalSetBlocking(false);
					SafeCloseSocket safeCloseSocket = null;
					try
					{
						safeCloseSocket = SafeCloseSocket.Accept(this.m_Handle, socketAddress.m_Buffer, ref socketAddress.m_Size);
						socketError = (SocketError)(safeCloseSocket.IsInvalid ? Marshal.GetLastWin32Error() : 0);
					}
					catch (ObjectDisposedException)
					{
						socketError = SocketError.NotSocket;
					}
					if (socketError != SocketError.WouldBlock)
					{
						if (socketError == SocketError.Success)
						{
							asyncResult.Result = this.CreateAcceptSocket(safeCloseSocket, this.m_RightEndPoint.Create(socketAddress), false);
						}
						else
						{
							asyncResult.ErrorCode = (int)socketError;
						}
						this.InternalSetBlocking(true);
						flag = true;
					}
					else
					{
						acceptQueue.Enqueue(asyncResult);
						if (!this.SetAsyncEventSelect(AsyncEventBits.FdAccept))
						{
							acceptQueue.Dequeue();
							throw new ObjectDisposedException(base.GetType().FullName);
						}
					}
				}
				else
				{
					acceptQueue.Enqueue(asyncResult);
				}
			}
			if (!flag)
			{
				return;
			}
			if (socketError == SocketError.Success)
			{
				asyncResult.InvokeCallback();
				return;
			}
			SocketException ex = new SocketException(socketError);
			this.UpdateStatusAfterSocketError(ex);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exception(Logging.Sockets, this, "BeginAccept", ex);
			}
			throw ex;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0009B140 File Offset: 0x00099340
		private void CompleteAcceptResults(object nullState)
		{
			Queue acceptQueue = this.GetAcceptQueue();
			bool flag = true;
			while (flag)
			{
				LazyAsyncResult lazyAsyncResult = null;
				lock (this)
				{
					if (acceptQueue.Count == 0)
					{
						break;
					}
					lazyAsyncResult = (LazyAsyncResult)acceptQueue.Dequeue();
					if (acceptQueue.Count == 0)
					{
						flag = false;
					}
				}
				try
				{
					lazyAsyncResult.InvokeCallback(new SocketException(SocketError.OperationAborted));
				}
				catch
				{
					if (flag)
					{
						ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.CompleteAcceptResults), null);
					}
					throw;
				}
			}
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0009B1E0 File Offset: 0x000993E0
		private void AcceptCallback(object nullState)
		{
			bool flag = true;
			Queue acceptQueue = this.GetAcceptQueue();
			while (flag)
			{
				LazyAsyncResult lazyAsyncResult = null;
				SocketError socketError = SocketError.OperationAborted;
				SocketAddress socketAddress = null;
				SafeCloseSocket safeCloseSocket = null;
				Exception ex = null;
				object obj = null;
				lock (this)
				{
					if (acceptQueue.Count == 0)
					{
						break;
					}
					lazyAsyncResult = (LazyAsyncResult)acceptQueue.Peek();
					if (!this.CleanedUp)
					{
						socketAddress = this.m_RightEndPoint.Serialize();
						try
						{
							safeCloseSocket = SafeCloseSocket.Accept(this.m_Handle, socketAddress.m_Buffer, ref socketAddress.m_Size);
							socketError = (SocketError)(safeCloseSocket.IsInvalid ? Marshal.GetLastWin32Error() : 0);
						}
						catch (ObjectDisposedException)
						{
							socketError = SocketError.OperationAborted;
						}
						catch (Exception ex2)
						{
							if (NclUtilities.IsFatal(ex2))
							{
								throw;
							}
							ex = ex2;
						}
					}
					if (socketError == SocketError.WouldBlock && ex == null)
					{
						try
						{
							this.m_AsyncEvent.Reset();
							if (this.SetAsyncEventSelect(AsyncEventBits.FdAccept))
							{
								break;
							}
						}
						catch (ObjectDisposedException)
						{
						}
						ex = new ObjectDisposedException(base.GetType().FullName);
					}
					if (ex != null)
					{
						obj = ex;
					}
					else if (socketError == SocketError.Success)
					{
						obj = this.CreateAcceptSocket(safeCloseSocket, this.m_RightEndPoint.Create(socketAddress), true);
					}
					else
					{
						lazyAsyncResult.ErrorCode = (int)socketError;
					}
					acceptQueue.Dequeue();
					if (acceptQueue.Count == 0)
					{
						if (!this.CleanedUp)
						{
							this.UnsetAsyncEventSelect();
						}
						flag = false;
					}
				}
				try
				{
					lazyAsyncResult.InvokeCallback(obj);
				}
				catch
				{
					if (flag)
					{
						ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.AcceptCallback), nullState);
					}
					throw;
				}
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x0009B3D8 File Offset: 0x000995D8
		private bool CanUseAcceptEx
		{
			get
			{
				return Thread.CurrentThread.IsThreadPoolThread || SettingsSectionInternal.Section.AlwaysUseCompletionPortsForAccept || this.m_IsDisconnected;
			}
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt and receives the first block of data sent by the client application.</summary>
		/// <param name="receiveSize">The number of bytes to accept from the sender.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous <see cref="T:System.Net.Sockets.Socket" /> creation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method.</exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.  
		///  -or-  
		///  The accepted socket is bound.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="receiveSize" /> is less than 0.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x060020AC RID: 8364 RVA: 0x0009B3FA File Offset: 0x000995FA
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAccept(int receiveSize, AsyncCallback callback, object state)
		{
			return this.BeginAccept(null, receiveSize, callback, state);
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt from a specified socket and receives the first block of data sent by the client application.</summary>
		/// <param name="acceptSocket">The accepted <see cref="T:System.Net.Sockets.Socket" /> object. This value may be <see langword="null" />.</param>
		/// <param name="receiveSize">The maximum number of bytes to receive.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous <see cref="T:System.Net.Sockets.Socket" /> object creation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method.</exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.  
		///  -or-  
		///  The accepted socket is bound.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="receiveSize" /> is less than 0.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x060020AD RID: 8365 RVA: 0x0009B408 File Offset: 0x00099608
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAccept(Socket acceptSocket, int receiveSize, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginAccept", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (receiveSize < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			AcceptOverlappedAsyncResult acceptOverlappedAsyncResult = new AcceptOverlappedAsyncResult(this, state, callback);
			acceptOverlappedAsyncResult.StartPostingAsyncOp(false);
			this.DoBeginAccept(acceptSocket, receiveSize, acceptOverlappedAsyncResult);
			acceptOverlappedAsyncResult.FinishPostingAsyncOp(ref this.Caches.AcceptClosureCache);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginAccept", acceptOverlappedAsyncResult);
			}
			return acceptOverlappedAsyncResult;
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0009B4A4 File Offset: 0x000996A4
		private void DoBeginAccept(Socket acceptSocket, int receiveSize, AcceptOverlappedAsyncResult asyncResult)
		{
			if (this.m_RightEndPoint == null)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustbind"));
			}
			if (!this.isListening)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustlisten"));
			}
			if (acceptSocket == null)
			{
				acceptSocket = new Socket(this.addressFamily, this.socketType, this.protocolType);
			}
			else if (acceptSocket.m_RightEndPoint != null)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_namedmustnotbebound", new object[] { "acceptSocket" }));
			}
			asyncResult.AcceptSocket = acceptSocket;
			int num = this.m_RightEndPoint.Serialize().Size + 16;
			byte[] array = new byte[receiveSize + num * 2];
			asyncResult.SetUnmanagedStructures(array, num);
			SocketError socketError = SocketError.Success;
			int num2;
			if (!this.AcceptEx(this.m_Handle, acceptSocket.m_Handle, Marshal.UnsafeAddrOfPinnedArrayElement(asyncResult.Buffer, 0), receiveSize, num, num, out num2, asyncResult.OverlappedHandle))
			{
				socketError = (SocketError)Marshal.GetLastWin32Error();
			}
			socketError = asyncResult.CheckAsyncCallOverlappedResult(socketError);
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginAccept", ex);
				}
				throw ex;
			}
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> to handle remote host communication.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information for this asynchronous operation as well as any user defined data.</param>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> to handle communication with the remote host.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndAccept(System.IAsyncResult)" /> method was previously called.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method.</exception>
		// Token: 0x060020AF RID: 8367 RVA: 0x0009B5C0 File Offset: 0x000997C0
		public Socket EndAccept(IAsyncResult asyncResult)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "EndAccept", asyncResult);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult != null && asyncResult is AcceptOverlappedAsyncResult)
			{
				byte[] array;
				int num;
				return this.EndAccept(out array, out num, asyncResult);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AcceptAsyncResult acceptAsyncResult = asyncResult as AcceptAsyncResult;
			if (acceptAsyncResult == null || acceptAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (acceptAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndAccept" }));
			}
			object obj = acceptAsyncResult.InternalWaitForCompletion();
			acceptAsyncResult.EndCalled = true;
			Exception ex = obj as Exception;
			if (ex != null)
			{
				throw ex;
			}
			if (acceptAsyncResult.ErrorCode != 0)
			{
				SocketException ex2 = new SocketException(acceptAsyncResult.ErrorCode);
				this.UpdateStatusAfterSocketError(ex2);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "EndAccept", ex2);
				}
				throw ex2;
			}
			Socket socket = (Socket)obj;
			if (Socket.s_LoggingEnabled)
			{
				Logging.PrintInfo(Logging.Sockets, socket, SR.GetString("net_log_socket_accepted", new object[] { socket.RemoteEndPoint, socket.LocalEndPoint }));
				Logging.Exit(Logging.Sockets, this, "EndAccept", obj);
			}
			return socket;
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> object to handle remote host communication. This method returns a buffer that contains the initial data transferred.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the bytes transferred.</param>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information for this asynchronous operation as well as any user defined data.</param>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> object to handle communication with the remote host.</returns>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is empty.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndAccept(System.IAsyncResult)" /> method was previously called.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the <see cref="T:System.Net.Sockets.Socket" /></exception>
		// Token: 0x060020B0 RID: 8368 RVA: 0x0009B718 File Offset: 0x00099918
		public Socket EndAccept(out byte[] buffer, IAsyncResult asyncResult)
		{
			byte[] array;
			int num;
			Socket socket = this.EndAccept(out array, out num, asyncResult);
			buffer = new byte[num];
			Array.Copy(array, buffer, num);
			return socket;
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> object to handle remote host communication. This method returns a buffer that contains the initial data and the number of bytes transferred.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the bytes transferred.</param>
		/// <param name="bytesTransferred">The number of bytes transferred.</param>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information for this asynchronous operation as well as any user defined data.</param>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> object to handle communication with the remote host.</returns>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is empty.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndAccept(System.IAsyncResult)" /> method was previously called.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the <see cref="T:System.Net.Sockets.Socket" />.</exception>
		// Token: 0x060020B1 RID: 8369 RVA: 0x0009B744 File Offset: 0x00099944
		public Socket EndAccept(out byte[] buffer, out int bytesTransferred, IAsyncResult asyncResult)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "EndAccept", asyncResult);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AcceptOverlappedAsyncResult acceptOverlappedAsyncResult = asyncResult as AcceptOverlappedAsyncResult;
			if (acceptOverlappedAsyncResult == null || acceptOverlappedAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (acceptOverlappedAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndAccept" }));
			}
			Socket socket = (Socket)acceptOverlappedAsyncResult.InternalWaitForCompletion();
			bytesTransferred = acceptOverlappedAsyncResult.BytesTransferred;
			buffer = acceptOverlappedAsyncResult.Buffer;
			acceptOverlappedAsyncResult.EndCalled = true;
			if (Socket.s_PerfCountersEnabled && bytesTransferred > 0)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesReceived, (long)bytesTransferred);
			}
			if (acceptOverlappedAsyncResult.ErrorCode != 0)
			{
				SocketException ex = new SocketException(acceptOverlappedAsyncResult.ErrorCode);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "EndAccept", ex);
				}
				throw ex;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.PrintInfo(Logging.Sockets, socket, SR.GetString("net_log_socket_accepted", new object[] { socket.RemoteEndPoint, socket.LocalEndPoint }));
				Logging.Exit(Logging.Sockets, this, "EndAccept", socket);
			}
			return socket;
		}

		/// <summary>Disables sends and receives on a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="how">One of the <see cref="T:System.Net.Sockets.SocketShutdown" /> values that specifies the operation that will no longer be allowed.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x060020B2 RID: 8370 RVA: 0x0009B8A0 File Offset: 0x00099AA0
		public void Shutdown(SocketShutdown how)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Shutdown", how);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.shutdown(this.m_Handle, (int)how);
			socketError = (SocketError)((socketError != SocketError.SocketError) ? 0 : Marshal.GetLastWin32Error());
			if (socketError != SocketError.Success && socketError != SocketError.NotSocket)
			{
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "Shutdown", ex);
				}
				throw ex;
			}
			this.SetToDisconnected();
			this.InternalSetBlocking(this.willBlockInternal);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "Shutdown", "");
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x0009B964 File Offset: 0x00099B64
		private static object InternalSyncObject
		{
			get
			{
				if (Socket.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref Socket.s_InternalSyncObject, obj, null);
				}
				return Socket.s_InternalSyncObject;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x0009B990 File Offset: 0x00099B90
		private Socket.CacheSet Caches
		{
			get
			{
				if (this.m_Caches == null)
				{
					this.m_Caches = new Socket.CacheSet();
				}
				return this.m_Caches;
			}
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x0009B9AB File Offset: 0x00099BAB
		private void EnsureDynamicWinsockMethods()
		{
			if (this.m_DynamicWinsockMethods == null)
			{
				this.m_DynamicWinsockMethods = DynamicWinsockMethods.GetMethods(this.addressFamily, this.socketType, this.protocolType);
			}
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0009B9D4 File Offset: 0x00099BD4
		private bool AcceptEx(SafeCloseSocket listenSocketHandle, SafeCloseSocket acceptSocketHandle, IntPtr buffer, int len, int localAddressLength, int remoteAddressLength, out int bytesReceived, SafeHandle overlapped)
		{
			this.EnsureDynamicWinsockMethods();
			AcceptExDelegate @delegate = this.m_DynamicWinsockMethods.GetDelegate<AcceptExDelegate>(listenSocketHandle);
			return @delegate(listenSocketHandle, acceptSocketHandle, buffer, len, localAddressLength, remoteAddressLength, out bytesReceived, overlapped);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0009BA08 File Offset: 0x00099C08
		internal void GetAcceptExSockaddrs(IntPtr buffer, int receiveDataLength, int localAddressLength, int remoteAddressLength, out IntPtr localSocketAddress, out int localSocketAddressLength, out IntPtr remoteSocketAddress, out int remoteSocketAddressLength)
		{
			this.EnsureDynamicWinsockMethods();
			GetAcceptExSockaddrsDelegate @delegate = this.m_DynamicWinsockMethods.GetDelegate<GetAcceptExSockaddrsDelegate>(this.m_Handle);
			@delegate(buffer, receiveDataLength, localAddressLength, remoteAddressLength, out localSocketAddress, out localSocketAddressLength, out remoteSocketAddress, out remoteSocketAddressLength);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0009BA40 File Offset: 0x00099C40
		private bool DisconnectEx(SafeCloseSocket socketHandle, SafeHandle overlapped, int flags, int reserved)
		{
			this.EnsureDynamicWinsockMethods();
			DisconnectExDelegate @delegate = this.m_DynamicWinsockMethods.GetDelegate<DisconnectExDelegate>(socketHandle);
			return @delegate(socketHandle, overlapped, flags, reserved);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0009BA6C File Offset: 0x00099C6C
		private bool DisconnectEx_Blocking(IntPtr socketHandle, IntPtr overlapped, int flags, int reserved)
		{
			this.EnsureDynamicWinsockMethods();
			DisconnectExDelegate_Blocking @delegate = this.m_DynamicWinsockMethods.GetDelegate<DisconnectExDelegate_Blocking>(this.m_Handle);
			return @delegate(socketHandle, overlapped, flags, reserved);
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x0009BA9C File Offset: 0x00099C9C
		private bool ConnectEx(SafeCloseSocket socketHandle, IntPtr socketAddress, int socketAddressSize, IntPtr buffer, int dataLength, out int bytesSent, SafeHandle overlapped)
		{
			this.EnsureDynamicWinsockMethods();
			ConnectExDelegate @delegate = this.m_DynamicWinsockMethods.GetDelegate<ConnectExDelegate>(socketHandle);
			return @delegate(socketHandle, socketAddress, socketAddressSize, buffer, dataLength, out bytesSent, overlapped);
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0009BAD0 File Offset: 0x00099CD0
		private SocketError WSARecvMsg(SafeCloseSocket socketHandle, IntPtr msg, out int bytesTransferred, SafeHandle overlapped, IntPtr completionRoutine)
		{
			this.EnsureDynamicWinsockMethods();
			WSARecvMsgDelegate @delegate = this.m_DynamicWinsockMethods.GetDelegate<WSARecvMsgDelegate>(socketHandle);
			return @delegate(socketHandle, msg, out bytesTransferred, overlapped, completionRoutine);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x0009BB00 File Offset: 0x00099D00
		private SocketError WSARecvMsg_Blocking(IntPtr socketHandle, IntPtr msg, out int bytesTransferred, IntPtr overlapped, IntPtr completionRoutine)
		{
			this.EnsureDynamicWinsockMethods();
			WSARecvMsgDelegate_Blocking @delegate = this.m_DynamicWinsockMethods.GetDelegate<WSARecvMsgDelegate_Blocking>(this.m_Handle);
			return @delegate(socketHandle, msg, out bytesTransferred, overlapped, completionRoutine);
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0009BB34 File Offset: 0x00099D34
		private bool TransmitPackets(SafeCloseSocket socketHandle, IntPtr packetArray, int elementCount, int sendSize, SafeNativeOverlapped overlapped, TransmitFileOptions flags)
		{
			this.EnsureDynamicWinsockMethods();
			TransmitPacketsDelegate @delegate = this.m_DynamicWinsockMethods.GetDelegate<TransmitPacketsDelegate>(socketHandle);
			return @delegate(socketHandle, packetArray, elementCount, sendSize, overlapped, flags);
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0009BB63 File Offset: 0x00099D63
		private Queue GetAcceptQueue()
		{
			if (this.m_AcceptQueueOrConnectResult == null)
			{
				Interlocked.CompareExchange(ref this.m_AcceptQueueOrConnectResult, new Queue(16), null);
			}
			return (Queue)this.m_AcceptQueueOrConnectResult;
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x0009BB8C File Offset: 0x00099D8C
		internal bool CleanedUp
		{
			get
			{
				return this.m_IntCleanedUp == 1;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x0009BB97 File Offset: 0x00099D97
		internal TransportType Transport
		{
			get
			{
				if (this.protocolType == ProtocolType.Tcp)
				{
					return TransportType.Tcp;
				}
				if (this.protocolType != ProtocolType.Udp)
				{
					return TransportType.All;
				}
				return TransportType.Udp;
			}
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0009BBB4 File Offset: 0x00099DB4
		private void CheckSetOptionPermissions(SocketOptionLevel optionLevel, SocketOptionName optionName)
		{
			if ((optionLevel != SocketOptionLevel.Tcp || (optionName != SocketOptionName.Debug && optionName != SocketOptionName.AcceptConnection && optionName != SocketOptionName.AcceptConnection)) && (optionLevel != SocketOptionLevel.Udp || (optionName != SocketOptionName.Debug && optionName != SocketOptionName.ChecksumCoverage)) && (optionLevel != SocketOptionLevel.Socket || (optionName != SocketOptionName.KeepAlive && optionName != SocketOptionName.Linger && optionName != SocketOptionName.DontLinger && optionName != SocketOptionName.SendBuffer && optionName != SocketOptionName.ReceiveBuffer && optionName != SocketOptionName.SendTimeout && optionName != SocketOptionName.ExclusiveAddressUse && optionName != SocketOptionName.ReceiveTimeout)) && (optionLevel != SocketOptionLevel.IPv6 || optionName != SocketOptionName.IPProtectionLevel))
			{
				ExceptionHelper.UnmanagedPermission.Demand();
			}
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x0009BC34 File Offset: 0x00099E34
		private SocketAddress SnapshotAndSerialize(ref EndPoint remoteEP)
		{
			IPEndPoint ipendPoint = remoteEP as IPEndPoint;
			if (ipendPoint != null)
			{
				ipendPoint = ipendPoint.Snapshot();
				remoteEP = this.RemapIPEndPoint(ipendPoint);
			}
			return this.CallSerializeCheckDnsEndPoint(remoteEP);
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x0009BC64 File Offset: 0x00099E64
		private SocketAddress CallSerializeCheckDnsEndPoint(EndPoint remoteEP)
		{
			if (remoteEP is DnsEndPoint)
			{
				throw new ArgumentException(SR.GetString("net_sockets_invalid_dnsendpoint", new object[] { "remoteEP" }), "remoteEP");
			}
			return remoteEP.Serialize();
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x0009BC97 File Offset: 0x00099E97
		private IPEndPoint RemapIPEndPoint(IPEndPoint input)
		{
			if (input.AddressFamily == AddressFamily.InterNetwork && this.IsDualMode)
			{
				return new IPEndPoint(input.Address.MapToIPv6(), input.Port);
			}
			return input;
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x0009BCC4 File Offset: 0x00099EC4
		private SocketAddress CheckCacheRemote(ref EndPoint remoteEP, bool isOverwrite)
		{
			IPEndPoint ipendPoint = remoteEP as IPEndPoint;
			if (ipendPoint != null)
			{
				ipendPoint = ipendPoint.Snapshot();
				remoteEP = this.RemapIPEndPoint(ipendPoint);
			}
			SocketAddress socketAddress = this.CallSerializeCheckDnsEndPoint(remoteEP);
			SocketAddress permittedRemoteAddress = this.m_PermittedRemoteAddress;
			if (permittedRemoteAddress != null && permittedRemoteAddress.Equals(socketAddress))
			{
				return permittedRemoteAddress;
			}
			if (ipendPoint != null)
			{
				SocketPermission socketPermission = new SocketPermission(NetworkAccess.Connect, this.Transport, ipendPoint.Address.ToString(), ipendPoint.Port);
				socketPermission.Demand();
			}
			else
			{
				ExceptionHelper.UnmanagedPermission.Demand();
			}
			if (this.m_PermittedRemoteAddress == null || isOverwrite)
			{
				this.m_PermittedRemoteAddress = socketAddress;
			}
			return socketAddress;
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x0009BD54 File Offset: 0x00099F54
		internal static void InitializeSockets()
		{
			if (!Socket.s_Initialized)
			{
				object internalSyncObject = Socket.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (!Socket.s_Initialized)
					{
						WSAData wsadata = default(WSAData);
						SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAStartup(514, out wsadata);
						if (socketError != SocketError.Success)
						{
							throw new SocketException(socketError);
						}
						bool flag2 = true;
						bool flag3 = true;
						SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = UnsafeNclNativeMethods.OSSOCK.WSASocket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP, IntPtr.Zero, 0U, (SocketConstructorFlags)0);
						if (innerSafeCloseSocket.IsInvalid)
						{
							socketError = (SocketError)Marshal.GetLastWin32Error();
							if (socketError == SocketError.AddressFamilyNotSupported)
							{
								flag2 = false;
							}
						}
						innerSafeCloseSocket.Close();
						SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket2 = UnsafeNclNativeMethods.OSSOCK.WSASocket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.IP, IntPtr.Zero, 0U, (SocketConstructorFlags)0);
						if (innerSafeCloseSocket2.IsInvalid)
						{
							socketError = (SocketError)Marshal.GetLastWin32Error();
							if (socketError == SocketError.AddressFamilyNotSupported)
							{
								flag3 = false;
							}
						}
						innerSafeCloseSocket2.Close();
						if (flag3)
						{
							Socket.s_OSSupportsIPv6 = true;
							flag3 = SettingsSectionInternal.Section.Ipv6Enabled;
						}
						Socket.s_SupportsIPv4 = flag2;
						Socket.s_SupportsIPv6 = flag3;
						Socket.s_PerfCountersEnabled = NetworkingPerfCounters.Instance.Enabled;
						Socket.s_Initialized = true;
					}
				}
			}
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x0009BE74 File Offset: 0x0009A074
		private Socket.MultipleAddressConnectAsyncResult InternalBeginConnectHostName(string host, int port, AsyncCallback requestCallback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "InternalBeginConnectHostName", host);
			}
			Socket.MultipleAddressConnectAsyncResult multipleAddressConnectAsyncResult = new Socket.MultipleAddressConnectAsyncResult(null, port, this, state, requestCallback);
			multipleAddressConnectAsyncResult.StartPostingAsyncOp(false);
			IAsyncResult asyncResult = Dns.UnsafeBeginGetHostAddresses(host, new AsyncCallback(Socket.DnsCallback), multipleAddressConnectAsyncResult);
			if (asyncResult.CompletedSynchronously && Socket.DoDnsCallback(asyncResult, multipleAddressConnectAsyncResult))
			{
				multipleAddressConnectAsyncResult.InvokeCallback();
			}
			multipleAddressConnectAsyncResult.FinishPostingAsyncOp(ref this.Caches.ConnectClosureCache);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "InternalBeginConnectHostName", multipleAddressConnectAsyncResult);
			}
			return multipleAddressConnectAsyncResult;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x0009BF08 File Offset: 0x0009A108
		private void InternalEndConnect(IAsyncResult asyncResult)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "InternalEndConnect", asyncResult);
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = null;
			EndPoint endPoint = null;
			ConnectOverlappedAsyncResult connectOverlappedAsyncResult = asyncResult as ConnectOverlappedAsyncResult;
			if (connectOverlappedAsyncResult == null)
			{
				Socket.MultipleAddressConnectAsyncResult multipleAddressConnectAsyncResult = asyncResult as Socket.MultipleAddressConnectAsyncResult;
				if (multipleAddressConnectAsyncResult == null)
				{
					ConnectAsyncResult connectAsyncResult = asyncResult as ConnectAsyncResult;
					if (connectAsyncResult != null)
					{
						endPoint = connectAsyncResult.RemoteEndPoint;
						lazyAsyncResult = connectAsyncResult;
					}
				}
				else
				{
					endPoint = multipleAddressConnectAsyncResult.RemoteEndPoint;
					lazyAsyncResult = multipleAddressConnectAsyncResult;
				}
			}
			else
			{
				endPoint = connectOverlappedAsyncResult.RemoteEndPoint;
				lazyAsyncResult = connectOverlappedAsyncResult;
			}
			if (lazyAsyncResult == null || lazyAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "InternalEndConnect" }));
			}
			lazyAsyncResult.InternalWaitForCompletion();
			lazyAsyncResult.EndCalled = true;
			this.m_AcceptQueueOrConnectResult = null;
			if (lazyAsyncResult.Result is Exception)
			{
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "InternalEndConnect", (Exception)lazyAsyncResult.Result);
				}
				throw (Exception)lazyAsyncResult.Result;
			}
			if (lazyAsyncResult.ErrorCode != 0)
			{
				SocketException ex = new SocketException(lazyAsyncResult.ErrorCode, endPoint);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "InternalEndConnect", ex);
				}
				throw ex;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.PrintInfo(Logging.Sockets, this, SR.GetString("net_log_socket_connected", new object[] { this.LocalEndPoint, this.RemoteEndPoint }));
				Logging.Exit(Logging.Sockets, this, "InternalEndConnect", "");
			}
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x0009C0BC File Offset: 0x0009A2BC
		internal void InternalConnect(EndPoint remoteEP)
		{
			EndPoint endPoint = remoteEP;
			SocketAddress socketAddress = this.SnapshotAndSerialize(ref endPoint);
			this.DoConnect(endPoint, socketAddress);
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0009C0DC File Offset: 0x0009A2DC
		private void DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", endPointSnapshot);
			}
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAConnect(this.m_Handle.DangerousGetHandle(), socketAddress.m_Buffer, socketAddress.m_Size, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException(endPointSnapshot);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "Connect", ex);
				}
				throw ex;
			}
			if (this.m_RightEndPoint == null)
			{
				this.m_RightEndPoint = endPointSnapshot;
			}
			this.SetToConnected();
			if (Socket.s_LoggingEnabled)
			{
				Logging.PrintInfo(Logging.Sockets, this, SR.GetString("net_log_socket_connected", new object[] { this.LocalEndPoint, this.RemoteEndPoint }));
				Logging.Exit(Logging.Sockets, this, "Connect", "");
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.Socket" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x060020CB RID: 8395 RVA: 0x0009C1C4 File Offset: 0x0009A3C4
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			try
			{
				if (Socket.s_LoggingEnabled)
				{
					Logging.Enter(Logging.Sockets, this, "Dispose", null);
				}
				goto IL_34;
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
				goto IL_34;
			}
			IL_2E:
			Thread.SpinWait(1);
			IL_34:
			int num;
			if ((num = Interlocked.CompareExchange(ref this.m_IntCleanedUp, 1, 0)) == 2)
			{
				goto IL_2E;
			}
			if (num == 1)
			{
				try
				{
					if (Socket.s_LoggingEnabled)
					{
						Logging.Exit(Logging.Sockets, this, "Dispose", null);
					}
				}
				catch (Exception ex2)
				{
					if (NclUtilities.IsFatal(ex2))
					{
						throw;
					}
				}
				return;
			}
			this.SetToDisconnected();
			AsyncEventBits asyncEventBits = AsyncEventBits.FdNone;
			if (this.m_BlockEventBits != AsyncEventBits.FdNone)
			{
				this.UnsetAsyncEventSelect();
				if (this.m_BlockEventBits == AsyncEventBits.FdConnect)
				{
					LazyAsyncResult lazyAsyncResult = this.m_AcceptQueueOrConnectResult as LazyAsyncResult;
					if (lazyAsyncResult != null && !lazyAsyncResult.InternalPeekCompleted)
					{
						asyncEventBits = AsyncEventBits.FdConnect;
					}
				}
				else if (this.m_BlockEventBits == AsyncEventBits.FdAccept)
				{
					Queue queue = this.m_AcceptQueueOrConnectResult as Queue;
					if (queue != null && queue.Count != 0)
					{
						asyncEventBits = AsyncEventBits.FdAccept;
					}
				}
			}
			try
			{
				int closeTimeout = this.m_CloseTimeout;
				if (closeTimeout == 0)
				{
					this.m_Handle.Dispose();
				}
				else
				{
					if (!this.willBlock || !this.willBlockInternal)
					{
						int num2 = 0;
						SocketError socketError = UnsafeNclNativeMethods.OSSOCK.ioctlsocket(this.m_Handle, -2147195266, ref num2);
					}
					if (closeTimeout < 0)
					{
						this.m_Handle.CloseAsIs();
					}
					else
					{
						SocketError socketError = UnsafeNclNativeMethods.OSSOCK.shutdown(this.m_Handle, 1);
						socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_Handle, SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, ref closeTimeout, 4);
						if (socketError != SocketError.Success)
						{
							this.m_Handle.Dispose();
						}
						else
						{
							socketError = (SocketError)UnsafeNclNativeMethods.OSSOCK.recv(this.m_Handle.DangerousGetHandle(), null, 0, SocketFlags.None);
							if (socketError != SocketError.Success)
							{
								this.m_Handle.Dispose();
							}
							else
							{
								int num3 = 0;
								if (UnsafeNclNativeMethods.OSSOCK.ioctlsocket(this.m_Handle, 1074030207, ref num3) != SocketError.Success || num3 != 0)
								{
									this.m_Handle.Dispose();
								}
								else
								{
									this.m_Handle.CloseAsIs();
								}
							}
						}
					}
				}
			}
			catch (ObjectDisposedException)
			{
			}
			if (this.m_Caches != null)
			{
				OverlappedCache.InterlockedFree(ref this.m_Caches.SendOverlappedCache);
				OverlappedCache.InterlockedFree(ref this.m_Caches.ReceiveOverlappedCache);
			}
			if (asyncEventBits == AsyncEventBits.FdConnect)
			{
				ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(((LazyAsyncResult)this.m_AcceptQueueOrConnectResult).InvokeCallback), new SocketException(SocketError.OperationAborted));
			}
			else if (asyncEventBits == AsyncEventBits.FdAccept)
			{
				ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.CompleteAcceptResults), null);
			}
			if (this.m_AsyncEvent != null)
			{
				this.m_AsyncEvent.Close();
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
		// Token: 0x060020CC RID: 8396 RVA: 0x0009C448 File Offset: 0x0009A648
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Frees resources used by the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
		// Token: 0x060020CD RID: 8397 RVA: 0x0009C458 File Offset: 0x0009A658
		~Socket()
		{
			this.Dispose(false);
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0009C488 File Offset: 0x0009A688
		internal void InternalShutdown(SocketShutdown how)
		{
			if (this.CleanedUp || this.m_Handle.IsInvalid)
			{
				return;
			}
			try
			{
				UnsafeNclNativeMethods.OSSOCK.shutdown(this.m_Handle, (int)how);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x0009C4D0 File Offset: 0x0009A6D0
		internal void SetReceivingPacketInformation()
		{
			if (!this.m_ReceivingPacketInformation)
			{
				IPEndPoint ipendPoint = this.m_RightEndPoint as IPEndPoint;
				IPAddress ipaddress = ((ipendPoint != null) ? ipendPoint.Address : null);
				if (this.addressFamily == AddressFamily.InterNetwork || (ipaddress != null && this.IsDualMode && (ipaddress.IsIPv4MappedToIPv6 || ipaddress.Equals(IPAddress.IPv6Any))))
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
				}
				if (this.addressFamily == AddressFamily.InterNetworkV6 && (ipaddress == null || !ipaddress.IsIPv4MappedToIPv6))
				{
					this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.PacketInformation, true);
				}
				this.m_ReceivingPacketInformation = true;
			}
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x0009C558 File Offset: 0x0009A758
		internal void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue, bool silent)
		{
			if (silent && (this.CleanedUp || this.m_Handle.IsInvalid))
			{
				return;
			}
			SocketError socketError = SocketError.Success;
			try
			{
				socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_Handle, optionLevel, optionName, ref optionValue, 4);
			}
			catch
			{
				if (silent && this.m_Handle.IsInvalid)
				{
					return;
				}
				throw;
			}
			if (optionName == SocketOptionName.PacketInformation && optionValue == 0 && socketError == SocketError.Success)
			{
				this.m_ReceivingPacketInformation = false;
			}
			if (silent)
			{
				return;
			}
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "SetSocketOption", ex);
				}
				throw ex;
			}
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x0009C600 File Offset: 0x0009A800
		private void setMulticastOption(SocketOptionName optionName, MulticastOption MR)
		{
			IPMulticastRequest ipmulticastRequest = default(IPMulticastRequest);
			ipmulticastRequest.MulticastAddress = (int)MR.Group.m_Address;
			if (MR.LocalAddress != null)
			{
				ipmulticastRequest.InterfaceAddress = (int)MR.LocalAddress.m_Address;
			}
			else
			{
				int num = IPAddress.HostToNetworkOrder(MR.InterfaceIndex);
				ipmulticastRequest.InterfaceAddress = num;
			}
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_Handle, SocketOptionLevel.IP, optionName, ref ipmulticastRequest, IPMulticastRequest.Size);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "setMulticastOption", ex);
				}
				throw ex;
			}
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x0009C69C File Offset: 0x0009A89C
		private void setIPv6MulticastOption(SocketOptionName optionName, IPv6MulticastOption MR)
		{
			IPv6MulticastRequest pv6MulticastRequest = default(IPv6MulticastRequest);
			pv6MulticastRequest.MulticastAddress = MR.Group.GetAddressBytes();
			pv6MulticastRequest.InterfaceIndex = (int)MR.InterfaceIndex;
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_Handle, SocketOptionLevel.IPv6, optionName, ref pv6MulticastRequest, IPv6MulticastRequest.Size);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "setIPv6MulticastOption", ex);
				}
				throw ex;
			}
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x0009C714 File Offset: 0x0009A914
		private void setLingerOption(LingerOption lref)
		{
			Linger linger = default(Linger);
			linger.OnOff = (lref.Enabled ? 1 : 0);
			linger.Time = (ushort)lref.LingerTime;
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_Handle, SocketOptionLevel.Socket, SocketOptionName.Linger, ref linger, 4);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "setLingerOption", ex);
				}
				throw ex;
			}
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x0009C790 File Offset: 0x0009A990
		private LingerOption getLingerOpt()
		{
			Linger linger = default(Linger);
			int num = 4;
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.getsockopt(this.m_Handle, SocketOptionLevel.Socket, SocketOptionName.Linger, out linger, ref num);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "getLingerOpt", ex);
				}
				throw ex;
			}
			return new LingerOption(linger.OnOff > 0, (int)linger.Time);
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x0009C808 File Offset: 0x0009AA08
		private MulticastOption getMulticastOpt(SocketOptionName optionName)
		{
			IPMulticastRequest ipmulticastRequest = default(IPMulticastRequest);
			int size = IPMulticastRequest.Size;
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.getsockopt(this.m_Handle, SocketOptionLevel.IP, optionName, out ipmulticastRequest, ref size);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "getMulticastOpt", ex);
				}
				throw ex;
			}
			IPAddress ipaddress = new IPAddress(ipmulticastRequest.MulticastAddress);
			IPAddress ipaddress2 = new IPAddress(ipmulticastRequest.InterfaceAddress);
			return new MulticastOption(ipaddress, ipaddress2);
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x0009C88C File Offset: 0x0009AA8C
		private IPv6MulticastOption getIPv6MulticastOpt(SocketOptionName optionName)
		{
			IPv6MulticastRequest pv6MulticastRequest = default(IPv6MulticastRequest);
			int size = IPv6MulticastRequest.Size;
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.getsockopt(this.m_Handle, SocketOptionLevel.IP, optionName, out pv6MulticastRequest, ref size);
			if (socketError == SocketError.SocketError)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "getIPv6MulticastOpt", ex);
				}
				throw ex;
			}
			return new IPv6MulticastOption(new IPAddress(pv6MulticastRequest.MulticastAddress), (long)pv6MulticastRequest.InterfaceIndex);
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x0009C904 File Offset: 0x0009AB04
		private SocketError InternalSetBlocking(bool desired, out bool current)
		{
			if (this.CleanedUp)
			{
				current = this.willBlock;
				return SocketError.Success;
			}
			int num = (desired ? 0 : (-1));
			SocketError socketError;
			try
			{
				socketError = UnsafeNclNativeMethods.OSSOCK.ioctlsocket(this.m_Handle, -2147195266, ref num);
				if (socketError == SocketError.SocketError)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			catch (ObjectDisposedException)
			{
				socketError = SocketError.NotSocket;
			}
			if (socketError == SocketError.Success)
			{
				this.willBlockInternal = num == 0;
			}
			current = this.willBlockInternal;
			return socketError;
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x0009C97C File Offset: 0x0009AB7C
		internal void InternalSetBlocking(bool desired)
		{
			bool flag;
			this.InternalSetBlocking(desired, out flag);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x0009C994 File Offset: 0x0009AB94
		private static IntPtr[] SocketListToFileDescriptorSet(IList socketList)
		{
			if (socketList == null || socketList.Count == 0)
			{
				return null;
			}
			IntPtr[] array = new IntPtr[socketList.Count + 1];
			array[0] = (IntPtr)socketList.Count;
			for (int i = 0; i < socketList.Count; i++)
			{
				if (!(socketList[i] is Socket))
				{
					throw new ArgumentException(SR.GetString("net_sockets_select", new object[]
					{
						socketList[i].GetType().FullName,
						typeof(Socket).FullName
					}), "socketList");
				}
				array[i + 1] = ((Socket)socketList[i]).m_Handle.DangerousGetHandle();
			}
			return array;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x0009CA48 File Offset: 0x0009AC48
		private static void SelectFileDescriptor(IList socketList, IntPtr[] fileDescriptorSet)
		{
			if (socketList == null || socketList.Count == 0)
			{
				return;
			}
			if ((int)fileDescriptorSet[0] == 0)
			{
				socketList.Clear();
				return;
			}
			lock (socketList)
			{
				for (int i = 0; i < socketList.Count; i++)
				{
					Socket socket = socketList[i] as Socket;
					int num = 0;
					while (num < (int)fileDescriptorSet[0] && !(fileDescriptorSet[num + 1] == socket.m_Handle.DangerousGetHandle()))
					{
						num++;
					}
					if (num == (int)fileDescriptorSet[0])
					{
						socketList.RemoveAt(i--);
					}
				}
			}
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x0009CB00 File Offset: 0x0009AD00
		private static void MicrosecondsToTimeValue(long microSeconds, ref TimeValue socketTime)
		{
			socketTime.Seconds = (int)(microSeconds / 1000000L);
			socketTime.Microseconds = (int)(microSeconds % 1000000L);
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x0009CB20 File Offset: 0x0009AD20
		private IAsyncResult BeginConnectEx(EndPoint remoteEP, bool flowContext, AsyncCallback callback, object state)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginConnectEx", "");
			}
			EndPoint endPoint = remoteEP;
			SocketAddress socketAddress = (flowContext ? this.CheckCacheRemote(ref endPoint, true) : this.SnapshotAndSerialize(ref endPoint));
			if (this.m_RightEndPoint == null)
			{
				if (endPoint.AddressFamily == AddressFamily.InterNetwork)
				{
					this.InternalBind(new IPEndPoint(IPAddress.Any, 0));
				}
				else
				{
					this.InternalBind(new IPEndPoint(IPAddress.IPv6Any, 0));
				}
			}
			ConnectOverlappedAsyncResult connectOverlappedAsyncResult = new ConnectOverlappedAsyncResult(this, endPoint, state, callback);
			if (flowContext)
			{
				connectOverlappedAsyncResult.StartPostingAsyncOp(false);
			}
			connectOverlappedAsyncResult.SetUnmanagedStructures(socketAddress.m_Buffer);
			EndPoint rightEndPoint = this.m_RightEndPoint;
			if (this.m_RightEndPoint == null)
			{
				this.m_RightEndPoint = endPoint;
			}
			SocketError socketError = SocketError.Success;
			try
			{
				int num;
				if (!this.ConnectEx(this.m_Handle, Marshal.UnsafeAddrOfPinnedArrayElement(socketAddress.m_Buffer, 0), socketAddress.m_Size, IntPtr.Zero, 0, out num, connectOverlappedAsyncResult.OverlappedHandle))
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			catch
			{
				connectOverlappedAsyncResult.InternalCleanup();
				this.m_RightEndPoint = rightEndPoint;
				throw;
			}
			if (socketError == SocketError.Success)
			{
				this.SetToConnected();
			}
			socketError = connectOverlappedAsyncResult.CheckAsyncCallOverlappedResult(socketError);
			if (socketError != SocketError.Success)
			{
				this.m_RightEndPoint = rightEndPoint;
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginConnectEx", ex);
				}
				throw ex;
			}
			connectOverlappedAsyncResult.FinishPostingAsyncOp(ref this.Caches.ConnectClosureCache);
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginConnectEx", connectOverlappedAsyncResult);
			}
			return connectOverlappedAsyncResult;
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0009CCA8 File Offset: 0x0009AEA8
		internal void MultipleSend(BufferOffsetSize[] buffers, SocketFlags socketFlags)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "MultipleSend", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			WSABuffer[] array = new WSABuffer[buffers.Length];
			GCHandle[] array2 = null;
			SocketError socketError;
			try
			{
				array2 = new GCHandle[buffers.Length];
				for (int i = 0; i < buffers.Length; i++)
				{
					array2[i] = GCHandle.Alloc(buffers[i].Buffer, GCHandleType.Pinned);
					array[i].Length = buffers[i].Size;
					array[i].Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(buffers[i].Buffer, buffers[i].Offset);
				}
				int num;
				socketError = UnsafeNclNativeMethods.OSSOCK.WSASend_Blocking(this.m_Handle.DangerousGetHandle(), array, array.Length, out num, socketFlags, SafeNativeOverlapped.Zero, IntPtr.Zero);
			}
			finally
			{
				if (array2 != null)
				{
					for (int j = 0; j < array2.Length; j++)
					{
						if (array2[j].IsAllocated)
						{
							array2[j].Free();
						}
					}
				}
			}
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException();
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "MultipleSend", ex);
				}
				throw ex;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "MultipleSend", "");
			}
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x0009CE18 File Offset: 0x0009B018
		private static void DnsCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			bool flag = false;
			Socket.MultipleAddressConnectAsyncResult multipleAddressConnectAsyncResult = (Socket.MultipleAddressConnectAsyncResult)result.AsyncState;
			try
			{
				flag = Socket.DoDnsCallback(result, multipleAddressConnectAsyncResult);
			}
			catch (Exception ex)
			{
				multipleAddressConnectAsyncResult.InvokeCallback(ex);
			}
			if (flag)
			{
				multipleAddressConnectAsyncResult.InvokeCallback();
			}
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x0009CE6C File Offset: 0x0009B06C
		private static bool DoDnsCallback(IAsyncResult result, Socket.MultipleAddressConnectAsyncResult context)
		{
			IPAddress[] array = Dns.EndGetHostAddresses(result);
			context.addresses = array;
			return Socket.DoMultipleAddressConnectCallback(Socket.PostOneBeginConnect(context), context);
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x0009CE94 File Offset: 0x0009B094
		private static object PostOneBeginConnect(Socket.MultipleAddressConnectAsyncResult context)
		{
			IPAddress ipaddress = context.addresses[context.index];
			if (context.socket.CanTryAddressFamily(ipaddress.AddressFamily))
			{
				try
				{
					EndPoint endPoint = new IPEndPoint(ipaddress, context.port);
					context.socket.CheckCacheRemote(ref endPoint, true);
					IAsyncResult asyncResult = context.socket.UnsafeBeginConnect(endPoint, new AsyncCallback(Socket.MultipleAddressConnectCallback), context);
					if (asyncResult.CompletedSynchronously)
					{
						return asyncResult;
					}
				}
				catch (Exception ex)
				{
					if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
					{
						throw;
					}
					return ex;
				}
				return null;
			}
			if (context.lastException == null)
			{
				return new ArgumentException(SR.GetString("net_invalidAddressList"), "context");
			}
			return context.lastException;
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x0009CF64 File Offset: 0x0009B164
		private static void MultipleAddressConnectCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			bool flag = false;
			Socket.MultipleAddressConnectAsyncResult multipleAddressConnectAsyncResult = (Socket.MultipleAddressConnectAsyncResult)result.AsyncState;
			try
			{
				flag = Socket.DoMultipleAddressConnectCallback(result, multipleAddressConnectAsyncResult);
			}
			catch (Exception ex)
			{
				multipleAddressConnectAsyncResult.InvokeCallback(ex);
			}
			if (flag)
			{
				multipleAddressConnectAsyncResult.InvokeCallback();
			}
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x0009CFB8 File Offset: 0x0009B1B8
		private static bool DoMultipleAddressConnectCallback(object result, Socket.MultipleAddressConnectAsyncResult context)
		{
			while (result != null)
			{
				Exception ex = result as Exception;
				if (ex == null)
				{
					try
					{
						context.socket.InternalEndConnect((IAsyncResult)result);
					}
					catch (Exception ex2)
					{
						ex = ex2;
					}
				}
				if (ex == null)
				{
					return true;
				}
				int num = context.index + 1;
				context.index = num;
				if (num >= context.addresses.Length)
				{
					throw ex;
				}
				context.lastException = ex;
				result = Socket.PostOneBeginConnect(context);
			}
			return false;
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x0009D030 File Offset: 0x0009B230
		internal IAsyncResult BeginMultipleSend(BufferOffsetSize[] buffers, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			OverlappedAsyncResult overlappedAsyncResult = new OverlappedAsyncResult(this, state, callback);
			overlappedAsyncResult.StartPostingAsyncOp(false);
			this.DoBeginMultipleSend(buffers, socketFlags, overlappedAsyncResult);
			overlappedAsyncResult.FinishPostingAsyncOp(ref this.Caches.SendClosureCache);
			return overlappedAsyncResult;
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x0009D06C File Offset: 0x0009B26C
		internal IAsyncResult UnsafeBeginMultipleSend(BufferOffsetSize[] buffers, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			OverlappedAsyncResult overlappedAsyncResult = new OverlappedAsyncResult(this, state, callback);
			this.DoBeginMultipleSend(buffers, socketFlags, overlappedAsyncResult);
			return overlappedAsyncResult;
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x0009D090 File Offset: 0x0009B290
		private void DoBeginMultipleSend(BufferOffsetSize[] buffers, SocketFlags socketFlags, OverlappedAsyncResult asyncResult)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "BeginMultipleSend", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			SocketError socketError = SocketError.SocketError;
			try
			{
				asyncResult.SetUnmanagedStructures(buffers, ref this.Caches.SendOverlappedCache);
				int num;
				socketError = UnsafeNclNativeMethods.OSSOCK.WSASend(this.m_Handle, asyncResult.m_WSABuffers, asyncResult.m_WSABuffers.Length, out num, socketFlags, asyncResult.OverlappedHandle, IntPtr.Zero);
				if (socketError != SocketError.Success)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			finally
			{
				socketError = asyncResult.CheckAsyncCallOverlappedResult(socketError);
			}
			if (socketError != SocketError.Success)
			{
				asyncResult.ExtractCache(ref this.Caches.SendOverlappedCache);
				SocketException ex = new SocketException(socketError);
				this.UpdateStatusAfterSocketError(ex);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "BeginMultipleSend", ex);
				}
				throw ex;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "BeginMultipleSend", asyncResult);
			}
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x0009D18C File Offset: 0x0009B38C
		internal int EndMultipleSend(IAsyncResult asyncResult)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "EndMultipleSend", asyncResult);
			}
			OverlappedAsyncResult overlappedAsyncResult = asyncResult as OverlappedAsyncResult;
			int num = (int)overlappedAsyncResult.InternalWaitForCompletion();
			overlappedAsyncResult.EndCalled = true;
			overlappedAsyncResult.ExtractCache(ref this.Caches.SendOverlappedCache);
			if (Socket.s_PerfCountersEnabled && num > 0)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesSent, (long)num);
				if (this.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsSent);
				}
			}
			if (overlappedAsyncResult.ErrorCode != 0)
			{
				SocketException ex = new SocketException(overlappedAsyncResult.ErrorCode);
				if (Socket.s_LoggingEnabled)
				{
					Logging.Exception(Logging.Sockets, this, "EndMultipleSend", ex);
				}
				throw ex;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "EndMultipleSend", num);
			}
			return num;
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x0009D25C File Offset: 0x0009B45C
		private Socket CreateAcceptSocket(SafeCloseSocket fd, EndPoint remoteEP, bool needCancelSelect)
		{
			Socket socket = new Socket(fd);
			return this.UpdateAcceptSocket(socket, remoteEP, needCancelSelect);
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x0009D27C File Offset: 0x0009B47C
		internal Socket UpdateAcceptSocket(Socket socket, EndPoint remoteEP, bool needCancelSelect)
		{
			socket.addressFamily = this.addressFamily;
			socket.socketType = this.socketType;
			socket.protocolType = this.protocolType;
			socket.m_RightEndPoint = this.m_RightEndPoint;
			socket.m_RemoteEndPoint = remoteEP;
			socket.SetToConnected();
			socket.willBlock = this.willBlock;
			if (needCancelSelect)
			{
				socket.UnsetAsyncEventSelect();
			}
			else
			{
				socket.InternalSetBlocking(this.willBlock);
			}
			return socket;
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x0009D2EA File Offset: 0x0009B4EA
		internal void SetToConnected()
		{
			if (this.m_IsConnected)
			{
				return;
			}
			this.m_IsConnected = true;
			this.m_IsDisconnected = false;
			if (Socket.s_PerfCountersEnabled)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketConnectionsEstablished);
			}
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x0009D317 File Offset: 0x0009B517
		internal void SetToDisconnected()
		{
			if (!this.m_IsConnected)
			{
				return;
			}
			this.m_IsConnected = false;
			this.m_IsDisconnected = true;
			if (!this.CleanedUp)
			{
				this.UnsetAsyncEventSelect();
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x0009D33E File Offset: 0x0009B53E
		internal void UpdateStatusAfterSocketError(SocketException socketException)
		{
			this.UpdateStatusAfterSocketError((SocketError)socketException.NativeErrorCode);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x0009D34C File Offset: 0x0009B54C
		internal void UpdateStatusAfterSocketError(SocketError errorCode)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.PrintError(Logging.Sockets, this, "UpdateStatusAfterSocketError", errorCode.ToString());
			}
			if (this.m_IsConnected && (this.m_Handle.IsInvalid || (errorCode != SocketError.WouldBlock && errorCode != SocketError.IOPending && errorCode != SocketError.NoBufferSpaceAvailable && errorCode != SocketError.TimedOut)))
			{
				this.SetToDisconnected();
			}
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0009D3BC File Offset: 0x0009B5BC
		private void UnsetAsyncEventSelect()
		{
			RegisteredWaitHandle registeredWait = this.m_RegisteredWait;
			if (registeredWait != null)
			{
				this.m_RegisteredWait = null;
				registeredWait.Unregister(null);
			}
			SocketError socketError = SocketError.NotSocket;
			try
			{
				socketError = UnsafeNclNativeMethods.OSSOCK.WSAEventSelect(this.m_Handle, IntPtr.Zero, AsyncEventBits.FdNone);
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
			}
			if (this.m_AsyncEvent != null)
			{
				try
				{
					this.m_AsyncEvent.Reset();
				}
				catch (ObjectDisposedException)
				{
				}
			}
			if (socketError == SocketError.SocketError)
			{
				this.UpdateStatusAfterSocketError(socketError);
			}
			this.InternalSetBlocking(this.willBlock);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x0009D458 File Offset: 0x0009B658
		private bool SetAsyncEventSelect(AsyncEventBits blockEventBits)
		{
			if (this.m_RegisteredWait != null)
			{
				return false;
			}
			if (this.m_AsyncEvent == null)
			{
				Interlocked.CompareExchange<ManualResetEvent>(ref this.m_AsyncEvent, new ManualResetEvent(false), null);
				if (Socket.s_RegisteredWaitCallback == null)
				{
					Socket.s_RegisteredWaitCallback = new WaitOrTimerCallback(Socket.RegisteredWaitCallback);
				}
			}
			if (Interlocked.CompareExchange(ref this.m_IntCleanedUp, 2, 0) != 0)
			{
				return false;
			}
			try
			{
				this.m_BlockEventBits = blockEventBits;
				this.m_RegisteredWait = ThreadPool.UnsafeRegisterWaitForSingleObject(this.m_AsyncEvent, Socket.s_RegisteredWaitCallback, this, -1, true);
			}
			finally
			{
				Interlocked.Exchange(ref this.m_IntCleanedUp, 0);
			}
			SocketError socketError = SocketError.NotSocket;
			try
			{
				socketError = UnsafeNclNativeMethods.OSSOCK.WSAEventSelect(this.m_Handle, this.m_AsyncEvent.SafeWaitHandle, blockEventBits);
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
			}
			if (socketError == SocketError.SocketError)
			{
				this.UpdateStatusAfterSocketError(socketError);
			}
			this.willBlockInternal = false;
			return socketError == SocketError.Success;
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x0009D54C File Offset: 0x0009B74C
		private static void RegisteredWaitCallback(object state, bool timedOut)
		{
			Socket socket = (Socket)state;
			if (Interlocked.Exchange<RegisteredWaitHandle>(ref socket.m_RegisteredWait, null) != null)
			{
				AsyncEventBits blockEventBits = socket.m_BlockEventBits;
				if (blockEventBits != AsyncEventBits.FdAccept)
				{
					if (blockEventBits == AsyncEventBits.FdConnect)
					{
						socket.ConnectCallback();
						return;
					}
				}
				else
				{
					socket.AcceptCallback(null);
				}
			}
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0009D58C File Offset: 0x0009B78C
		private void ValidateBlockingMode()
		{
			if (this.willBlock && !this.willBlockInternal)
			{
				throw new InvalidOperationException(SR.GetString("net_invasync"));
			}
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0009D5B0 File Offset: 0x0009B7B0
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal void BindToCompletionPort()
		{
			if (!this.m_BoundToThreadPool && !Socket.UseOverlappedIO)
			{
				lock (this)
				{
					if (!this.m_BoundToThreadPool)
					{
						try
						{
							ThreadPool.BindHandle(this.m_Handle);
							this.m_BoundToThreadPool = true;
						}
						catch (Exception ex)
						{
							if (NclUtilities.IsFatal(ex))
							{
								throw;
							}
							this.Close(0);
							throw;
						}
					}
				}
			}
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <returns>
		///   <see langword="true" /> if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.  
		/// <see langword="false" /> if the I/O operation completed synchronously. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <exception cref="T:System.ArgumentException">An argument is not valid. This exception occurs if the buffer provided is not large enough. The buffer must be at least 2 * (sizeof(SOCKADDR_STORAGE + 16) bytes.  
		///  This exception also occurs if multiple buffers are specified, the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is not null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument is out of range. The exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Count" /> is less than 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">An invalid operation was requested. This exception occurs if the accepting <see cref="T:System.Net.Sockets.Socket" /> is not listening for connections or the accepted socket is bound.  
		///  You must call the <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> method before calling the <see cref="M:System.Net.Sockets.Socket.AcceptAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.  
		///  This exception also occurs if the socket is already connected or a socket operation was already in progress using the specified <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x060020F2 RID: 8434 RVA: 0x0009D634 File Offset: 0x0009B834
		public bool AcceptAsync(SocketAsyncEventArgs e)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "AcceptAsync", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (e.m_BufferList != null)
			{
				throw new ArgumentException(SR.GetString("net_multibuffernotsupported"), "BufferList");
			}
			if (this.m_RightEndPoint == null)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustbind"));
			}
			if (!this.isListening)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustlisten"));
			}
			if (e.AcceptSocket == null)
			{
				e.AcceptSocket = new Socket(this.addressFamily, this.socketType, this.protocolType);
			}
			else if (e.AcceptSocket.m_RightEndPoint != null && !e.AcceptSocket.m_IsDisconnected)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_namedmustnotbebound", new object[] { "AcceptSocket" }));
			}
			e.StartOperationCommon(this);
			e.StartOperationAccept();
			this.BindToCompletionPort();
			SocketError socketError = SocketError.Success;
			int num;
			try
			{
				if (!this.AcceptEx(this.m_Handle, e.AcceptSocket.m_Handle, (e.m_PtrSingleBuffer != IntPtr.Zero) ? e.m_PtrSingleBuffer : e.m_PtrAcceptBuffer, (e.m_PtrSingleBuffer != IntPtr.Zero) ? (e.Count - e.m_AcceptAddressBufferCount) : 0, e.m_AcceptAddressBufferCount / 2, e.m_AcceptAddressBufferCount / 2, out num, e.m_PtrNativeOverlapped))
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			catch (Exception ex)
			{
				e.Complete();
				throw ex;
			}
			bool flag;
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				e.FinishOperationSyncFailure(socketError, num, SocketFlags.None);
				flag = false;
			}
			else
			{
				flag = true;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "AcceptAsync", flag);
			}
			return flag;
		}

		/// <summary>Begins an asynchronous request for a connection to a remote host.</summary>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <returns>
		///   <see langword="true" /> if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.  
		/// <see langword="false" /> if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <exception cref="T:System.ArgumentException">An argument is not valid. This exception occurs if multiple buffers are specified, the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is not null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter cannot be null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is listening or a socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method. This exception also occurs if the local endpoint and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> are not the same address family.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		// Token: 0x060020F3 RID: 8435 RVA: 0x0009D808 File Offset: 0x0009BA08
		public bool ConnectAsync(SocketAsyncEventArgs e)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "ConnectAsync", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (e.m_BufferList != null)
			{
				throw new ArgumentException(SR.GetString("net_multibuffernotsupported"), "BufferList");
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (this.isListening)
			{
				throw new InvalidOperationException(SR.GetString("net_sockets_mustnotlisten"));
			}
			EndPoint remoteEndPoint = e.RemoteEndPoint;
			DnsEndPoint dnsEndPoint = remoteEndPoint as DnsEndPoint;
			bool flag;
			if (dnsEndPoint != null)
			{
				if (Socket.s_LoggingEnabled)
				{
					Logging.PrintInfo(Logging.Sockets, "Socket#" + ValidationHelper.HashString(this) + "::ConnectAsync " + SR.GetString("net_log_socket_connect_dnsendpoint"));
				}
				if (dnsEndPoint.AddressFamily != AddressFamily.Unspecified && !this.CanTryAddressFamily(dnsEndPoint.AddressFamily))
				{
					throw new NotSupportedException(SR.GetString("net_invalidversion"));
				}
				MultipleConnectAsync multipleConnectAsync = new SingleSocketMultipleConnectAsync(this, true);
				e.StartOperationCommon(this);
				e.StartOperationWrapperConnect(multipleConnectAsync);
				flag = multipleConnectAsync.StartConnectAsync(e, dnsEndPoint);
			}
			else
			{
				if (!this.CanTryAddressFamily(e.RemoteEndPoint.AddressFamily))
				{
					throw new NotSupportedException(SR.GetString("net_invalidversion"));
				}
				e.m_SocketAddress = this.CheckCacheRemote(ref remoteEndPoint, false);
				if (this.m_RightEndPoint == null)
				{
					if (remoteEndPoint.AddressFamily == AddressFamily.InterNetwork)
					{
						this.InternalBind(new IPEndPoint(IPAddress.Any, 0));
					}
					else
					{
						this.InternalBind(new IPEndPoint(IPAddress.IPv6Any, 0));
					}
				}
				EndPoint rightEndPoint = this.m_RightEndPoint;
				if (this.m_RightEndPoint == null)
				{
					this.m_RightEndPoint = remoteEndPoint;
				}
				e.StartOperationCommon(this);
				e.StartOperationConnect();
				this.BindToCompletionPort();
				SocketError socketError = SocketError.Success;
				int num;
				try
				{
					if (!this.ConnectEx(this.m_Handle, e.m_PtrSocketAddressBuffer, e.m_SocketAddress.m_Size, e.m_PtrSingleBuffer, e.Count, out num, e.m_PtrNativeOverlapped))
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
				}
				catch (Exception ex)
				{
					this.m_RightEndPoint = rightEndPoint;
					e.Complete();
					throw ex;
				}
				if (socketError != SocketError.Success && socketError != SocketError.IOPending)
				{
					e.FinishOperationSyncFailure(socketError, num, SocketFlags.None);
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "ConnectAsync", flag);
			}
			return flag;
		}

		/// <summary>Begins an asynchronous request for a connection to a remote host.</summary>
		/// <param name="socketType">One of the <see cref="T:System.Net.Sockets.SocketType" /> values.</param>
		/// <param name="protocolType">One of the <see cref="T:System.Net.Sockets.ProtocolType" /> values.</param>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <returns>
		///   <see langword="true" /> if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.  
		/// <see langword="false" /> if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <exception cref="T:System.ArgumentException">An argument is not valid. This exception occurs if multiple buffers are specified, the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is not null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter cannot be null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is listening or a socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method. This exception also occurs if the local endpoint and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> are not the same address family.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		// Token: 0x060020F4 RID: 8436 RVA: 0x0009DA54 File Offset: 0x0009BC54
		public static bool ConnectAsync(SocketType socketType, ProtocolType protocolType, SocketAsyncEventArgs e)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, null, "ConnectAsync", "");
			}
			if (e.m_BufferList != null)
			{
				throw new ArgumentException(SR.GetString("net_multibuffernotsupported"), "BufferList");
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			EndPoint remoteEndPoint = e.RemoteEndPoint;
			DnsEndPoint dnsEndPoint = remoteEndPoint as DnsEndPoint;
			bool flag;
			if (dnsEndPoint != null)
			{
				Socket socket = null;
				MultipleConnectAsync multipleConnectAsync;
				if (dnsEndPoint.AddressFamily == AddressFamily.Unspecified)
				{
					multipleConnectAsync = new MultipleSocketMultipleConnectAsync(socketType, protocolType);
				}
				else
				{
					socket = new Socket(dnsEndPoint.AddressFamily, socketType, protocolType);
					multipleConnectAsync = new SingleSocketMultipleConnectAsync(socket, false);
				}
				e.StartOperationCommon(socket);
				e.StartOperationWrapperConnect(multipleConnectAsync);
				flag = multipleConnectAsync.StartConnectAsync(e, dnsEndPoint);
			}
			else
			{
				Socket socket2 = new Socket(remoteEndPoint.AddressFamily, socketType, protocolType);
				flag = socket2.ConnectAsync(e);
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, null, "ConnectAsync", flag);
			}
			return flag;
		}

		/// <summary>Cancels an asynchronous request for a remote host connection.</summary>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object used to request the connection to the remote host by calling one of the <see cref="M:System.Net.Sockets.Socket.ConnectAsync(System.Net.Sockets.SocketType,System.Net.Sockets.ProtocolType,System.Net.Sockets.SocketAsyncEventArgs)" /> methods.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter cannot be null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		// Token: 0x060020F5 RID: 8437 RVA: 0x0009DB42 File Offset: 0x0009BD42
		public static void CancelConnectAsync(SocketAsyncEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			e.CancelConnectAsync();
		}

		/// <summary>Begins an asynchronous request to disconnect from a remote endpoint.</summary>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <returns>
		///   <see langword="true" /> if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.  
		/// <see langword="false" /> if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x060020F6 RID: 8438 RVA: 0x0009DB58 File Offset: 0x0009BD58
		public bool DisconnectAsync(SocketAsyncEventArgs e)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "DisconnectAsync", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			e.StartOperationCommon(this);
			e.StartOperationDisconnect();
			this.BindToCompletionPort();
			SocketError socketError = SocketError.Success;
			try
			{
				if (!this.DisconnectEx(this.m_Handle, e.m_PtrNativeOverlapped, e.DisconnectReuseSocket ? 2 : 0, 0))
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
			}
			catch (Exception ex)
			{
				e.Complete();
				throw ex;
			}
			bool flag;
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				e.FinishOperationSyncFailure(socketError, 0, SocketFlags.None);
				flag = false;
			}
			else
			{
				flag = true;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "DisconnectAsync", flag);
			}
			return flag;
		}

		/// <summary>Begins an asynchronous request to receive data from a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <returns>
		///   <see langword="true" /> if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.  
		/// <see langword="false" /> if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <exception cref="T:System.ArgumentException">An argument was invalid. The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> or <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> properties on the <paramref name="e" /> parameter must reference valid buffers. One or the other of these properties may be set, but not both at the same time.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x060020F7 RID: 8439 RVA: 0x0009DC30 File Offset: 0x0009BE30
		public bool ReceiveAsync(SocketAsyncEventArgs e)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "ReceiveAsync", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			e.StartOperationCommon(this);
			e.StartOperationReceive();
			this.BindToCompletionPort();
			SocketFlags socketFlags = e.m_SocketFlags;
			int num;
			SocketError socketError;
			try
			{
				if (e.m_Buffer != null)
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.WSARecv(this.m_Handle, ref e.m_WSABuffer, 1, out num, ref socketFlags, e.m_PtrNativeOverlapped, IntPtr.Zero);
				}
				else
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.WSARecv(this.m_Handle, e.m_WSABufferArray, e.m_WSABufferArray.Length, out num, ref socketFlags, e.m_PtrNativeOverlapped, IntPtr.Zero);
				}
			}
			catch (Exception ex)
			{
				e.Complete();
				throw ex;
			}
			if (socketError != SocketError.Success)
			{
				socketError = (SocketError)Marshal.GetLastWin32Error();
			}
			bool flag;
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				e.FinishOperationSyncFailure(socketError, num, socketFlags);
				flag = false;
			}
			else
			{
				flag = true;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "ReceiveAsync", flag);
			}
			return flag;
		}

		/// <summary>Begins to asynchronously receive data from a specified network device.</summary>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <returns>
		///   <see langword="true" /> if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.  
		/// <see langword="false" /> if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x060020F8 RID: 8440 RVA: 0x0009DD44 File Offset: 0x0009BF44
		public bool ReceiveFromAsync(SocketAsyncEventArgs e)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "ReceiveFromAsync", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("RemoteEndPoint");
			}
			if (!this.CanTryAddressFamily(e.RemoteEndPoint.AddressFamily))
			{
				throw new ArgumentException(SR.GetString("net_InvalidEndPointAddressFamily", new object[]
				{
					e.RemoteEndPoint.AddressFamily,
					this.addressFamily
				}), "RemoteEndPoint");
			}
			EndPoint remoteEndPoint = e.RemoteEndPoint;
			e.m_SocketAddress = this.SnapshotAndSerialize(ref remoteEndPoint);
			e.RemoteEndPoint = remoteEndPoint;
			e.StartOperationCommon(this);
			e.StartOperationReceiveFrom();
			this.BindToCompletionPort();
			SocketFlags socketFlags = e.m_SocketFlags;
			int num;
			SocketError socketError;
			try
			{
				if (e.m_Buffer != null)
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.WSARecvFrom(this.m_Handle, ref e.m_WSABuffer, 1, out num, ref socketFlags, e.m_PtrSocketAddressBuffer, e.m_PtrSocketAddressBufferSize, e.m_PtrNativeOverlapped, IntPtr.Zero);
				}
				else
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.WSARecvFrom(this.m_Handle, e.m_WSABufferArray, e.m_WSABufferArray.Length, out num, ref socketFlags, e.m_PtrSocketAddressBuffer, e.m_PtrSocketAddressBufferSize, e.m_PtrNativeOverlapped, IntPtr.Zero);
				}
			}
			catch (Exception ex)
			{
				e.Complete();
				throw ex;
			}
			if (socketError != SocketError.Success)
			{
				socketError = (SocketError)Marshal.GetLastWin32Error();
			}
			bool flag;
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				e.FinishOperationSyncFailure(socketError, num, socketFlags);
				flag = false;
			}
			else
			{
				flag = true;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "ReceiveFromAsync", flag);
			}
			return flag;
		}

		/// <summary>Begins to asynchronously receive the specified number of bytes of data into the specified location in the data buffer, using the specified <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.SocketFlags" />, and stores the endpoint and packet information.</summary>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <returns>
		///   <see langword="true" /> if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.  
		/// <see langword="false" /> if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		// Token: 0x060020F9 RID: 8441 RVA: 0x0009DEF4 File Offset: 0x0009C0F4
		public bool ReceiveMessageFromAsync(SocketAsyncEventArgs e)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "ReceiveMessageFromAsync", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("RemoteEndPoint");
			}
			if (!this.CanTryAddressFamily(e.RemoteEndPoint.AddressFamily))
			{
				throw new ArgumentException(SR.GetString("net_InvalidEndPointAddressFamily", new object[]
				{
					e.RemoteEndPoint.AddressFamily,
					this.addressFamily
				}), "RemoteEndPoint");
			}
			EndPoint remoteEndPoint = e.RemoteEndPoint;
			e.m_SocketAddress = this.SnapshotAndSerialize(ref remoteEndPoint);
			e.RemoteEndPoint = remoteEndPoint;
			this.SetReceivingPacketInformation();
			e.StartOperationCommon(this);
			e.StartOperationReceiveMessageFrom();
			this.BindToCompletionPort();
			int num;
			SocketError socketError;
			try
			{
				socketError = this.WSARecvMsg(this.m_Handle, e.m_PtrWSAMessageBuffer, out num, e.m_PtrNativeOverlapped, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				e.Complete();
				throw ex;
			}
			if (socketError != SocketError.Success)
			{
				socketError = (SocketError)Marshal.GetLastWin32Error();
			}
			bool flag;
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				e.FinishOperationSyncFailure(socketError, num, SocketFlags.None);
				flag = false;
			}
			else
			{
				flag = true;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "ReceiveMessageFromAsync", flag);
			}
			return flag;
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <returns>
		///   <see langword="true" /> if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.  
		/// <see langword="false" /> if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> or <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> properties on the <paramref name="e" /> parameter must reference valid buffers. One or the other of these properties may be set, but not both at the same time.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">The <see cref="T:System.Net.Sockets.Socket" /> is not yet connected or was not obtained via an <see cref="M:System.Net.Sockets.Socket.Accept" />, <see cref="M:System.Net.Sockets.Socket.AcceptAsync(System.Net.Sockets.SocketAsyncEventArgs)" />,or <see cref="Overload:System.Net.Sockets.Socket.BeginAccept" />, method.</exception>
		// Token: 0x060020FA RID: 8442 RVA: 0x0009E050 File Offset: 0x0009C250
		public bool SendAsync(SocketAsyncEventArgs e)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "SendAsync", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			e.StartOperationCommon(this);
			e.StartOperationSend();
			this.BindToCompletionPort();
			int num;
			SocketError socketError;
			try
			{
				if (e.m_Buffer != null)
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.WSASend(this.m_Handle, ref e.m_WSABuffer, 1, out num, e.m_SocketFlags, e.m_PtrNativeOverlapped, IntPtr.Zero);
				}
				else
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.WSASend(this.m_Handle, e.m_WSABufferArray, e.m_WSABufferArray.Length, out num, e.m_SocketFlags, e.m_PtrNativeOverlapped, IntPtr.Zero);
				}
			}
			catch (Exception ex)
			{
				e.Complete();
				throw ex;
			}
			if (socketError != SocketError.Success)
			{
				socketError = (SocketError)Marshal.GetLastWin32Error();
			}
			bool flag;
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				e.FinishOperationSyncFailure(socketError, num, SocketFlags.None);
				flag = false;
			}
			else
			{
				flag = true;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "SendAsync", flag);
			}
			return flag;
		}

		/// <summary>Sends a collection of files or in memory data buffers asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <returns>
		///   <see langword="true" /> if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.  
		/// <see langword="false" /> if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in the <see cref="P:System.Net.Sockets.SendPacketsElement.FilePath" /> property was not found.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method. This exception also occurs if the <see cref="T:System.Net.Sockets.Socket" /> is not connected to a remote host.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">A connectionless <see cref="T:System.Net.Sockets.Socket" /> is being used and the file being sent exceeds the maximum packet size of the underlying transport.</exception>
		// Token: 0x060020FB RID: 8443 RVA: 0x0009E164 File Offset: 0x0009C364
		public bool SendPacketsAsync(SocketAsyncEventArgs e)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "SendPacketsAsync", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.Connected)
			{
				throw new NotSupportedException(SR.GetString("net_notconnected"));
			}
			e.StartOperationCommon(this);
			e.StartOperationSendPackets();
			this.BindToCompletionPort();
			bool flag2;
			if (e.m_SendPacketsDescriptor.Length != 0)
			{
				bool flag;
				try
				{
					flag = this.TransmitPackets(this.m_Handle, e.m_PtrSendPacketsDescriptor, e.m_SendPacketsDescriptor.Length, e.m_SendPacketsSendSize, e.m_PtrNativeOverlapped, e.m_SendPacketsFlags);
				}
				catch (Exception)
				{
					e.Complete();
					throw;
				}
				SocketError socketError;
				if (!flag)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
				else
				{
					socketError = SocketError.Success;
				}
				if (socketError != SocketError.Success && socketError != SocketError.IOPending)
				{
					e.FinishOperationSyncFailure(socketError, 0, SocketFlags.None);
					flag2 = false;
				}
				else
				{
					flag2 = true;
				}
			}
			else
			{
				e.FinishOperationSuccess(SocketError.Success, 0, SocketFlags.None);
				flag2 = false;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "SendPacketsAsync", flag2);
			}
			return flag2;
		}

		/// <summary>Sends data asynchronously to a specific remote host.</summary>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <returns>
		///   <see langword="true" /> if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.  
		/// <see langword="false" /> if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">The protocol specified is connection-oriented, but the <see cref="T:System.Net.Sockets.Socket" /> is not yet connected.</exception>
		// Token: 0x060020FC RID: 8444 RVA: 0x0009E278 File Offset: 0x0009C478
		public bool SendToAsync(SocketAsyncEventArgs e)
		{
			if (Socket.s_LoggingEnabled)
			{
				Logging.Enter(Logging.Sockets, this, "SendToAsync", "");
			}
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("RemoteEndPoint");
			}
			EndPoint remoteEndPoint = e.RemoteEndPoint;
			e.m_SocketAddress = this.CheckCacheRemote(ref remoteEndPoint, false);
			e.StartOperationCommon(this);
			e.StartOperationSendTo();
			this.BindToCompletionPort();
			int num;
			SocketError socketError;
			try
			{
				if (e.m_Buffer != null)
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.WSASendTo(this.m_Handle, ref e.m_WSABuffer, 1, out num, e.m_SocketFlags, e.m_PtrSocketAddressBuffer, e.m_SocketAddress.m_Size, e.m_PtrNativeOverlapped, IntPtr.Zero);
				}
				else
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.WSASendTo(this.m_Handle, e.m_WSABufferArray, e.m_WSABufferArray.Length, out num, e.m_SocketFlags, e.m_PtrSocketAddressBuffer, e.m_SocketAddress.m_Size, e.m_PtrNativeOverlapped, IntPtr.Zero);
				}
			}
			catch (Exception ex)
			{
				e.Complete();
				throw ex;
			}
			if (socketError != SocketError.Success)
			{
				socketError = (SocketError)Marshal.GetLastWin32Error();
			}
			bool flag;
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				e.FinishOperationSyncFailure(socketError, num, SocketFlags.None);
				flag = false;
			}
			else
			{
				flag = true;
			}
			if (Socket.s_LoggingEnabled)
			{
				Logging.Exit(Logging.Sockets, this, "SendToAsync", flag);
			}
			return flag;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0009E3D8 File Offset: 0x0009C5D8
		internal Task<Socket> AcceptAsync(Socket acceptSocket)
		{
			Socket.TaskSocketAsyncEventArgs<Socket> taskSocketAsyncEventArgs = Interlocked.Exchange<Socket.TaskSocketAsyncEventArgs<Socket>>(ref LazyInitializer.EnsureInitialized<Socket.CachedTaskEventArgs>(ref this._cachedTaskEventArgs).Accept, Socket.s_rentedSocketSentinel);
			if (taskSocketAsyncEventArgs == Socket.s_rentedSocketSentinel)
			{
				return this.AcceptAsyncApm(acceptSocket);
			}
			if (taskSocketAsyncEventArgs == null)
			{
				taskSocketAsyncEventArgs = new Socket.TaskSocketAsyncEventArgs<Socket>();
				taskSocketAsyncEventArgs.Completed += Socket.AcceptCompletedHandler;
			}
			taskSocketAsyncEventArgs.AcceptSocket = acceptSocket;
			Task<Socket> task;
			if (this.AcceptAsync(taskSocketAsyncEventArgs))
			{
				bool flag;
				task = taskSocketAsyncEventArgs.GetCompletionResponsibility(out flag).Task;
				if (flag)
				{
					this.ReturnSocketAsyncEventArgs(taskSocketAsyncEventArgs);
				}
			}
			else
			{
				task = ((taskSocketAsyncEventArgs.SocketError == SocketError.Success) ? Task.FromResult<Socket>(taskSocketAsyncEventArgs.AcceptSocket) : Task.FromException<Socket>(Socket.GetException(taskSocketAsyncEventArgs.SocketError, false)));
				this.ReturnSocketAsyncEventArgs(taskSocketAsyncEventArgs);
			}
			return task;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x0009E480 File Offset: 0x0009C680
		private Task<Socket> AcceptAsyncApm(Socket acceptSocket)
		{
			TaskCompletionSource<Socket> taskCompletionSource = new TaskCompletionSource<Socket>(this);
			this.BeginAccept(acceptSocket, 0, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<Socket> taskCompletionSource2 = (TaskCompletionSource<Socket>)iar.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(((Socket)taskCompletionSource2.Task.AsyncState).EndAccept(iar));
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x0009E4C4 File Offset: 0x0009C6C4
		internal Task ConnectAsync(EndPoint remoteEP)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(this);
			this.BeginConnect(remoteEP, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<bool> taskCompletionSource2 = (TaskCompletionSource<bool>)iar.AsyncState;
				try
				{
					((Socket)taskCompletionSource2.Task.AsyncState).EndConnect(iar);
					taskCompletionSource2.TrySetResult(true);
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0009E508 File Offset: 0x0009C708
		internal Task ConnectAsync(IPAddress address, int port)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(this);
			this.BeginConnect(address, port, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<bool> taskCompletionSource2 = (TaskCompletionSource<bool>)iar.AsyncState;
				try
				{
					((Socket)taskCompletionSource2.Task.AsyncState).EndConnect(iar);
					taskCompletionSource2.TrySetResult(true);
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x0009E54C File Offset: 0x0009C74C
		internal Task ConnectAsync(IPAddress[] addresses, int port)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(this);
			this.BeginConnect(addresses, port, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<bool> taskCompletionSource2 = (TaskCompletionSource<bool>)iar.AsyncState;
				try
				{
					((Socket)taskCompletionSource2.Task.AsyncState).EndConnect(iar);
					taskCompletionSource2.TrySetResult(true);
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0009E590 File Offset: 0x0009C790
		internal Task ConnectAsync(string host, int port)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(this);
			this.BeginConnect(host, port, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<bool> taskCompletionSource2 = (TaskCompletionSource<bool>)iar.AsyncState;
				try
				{
					((Socket)taskCompletionSource2.Task.AsyncState).EndConnect(iar);
					taskCompletionSource2.TrySetResult(true);
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0009E5D4 File Offset: 0x0009C7D4
		internal Task<int> ReceiveAsync(ArraySegment<byte> buffer, SocketFlags socketFlags, bool fromNetworkStream)
		{
			Socket.ValidateBuffer(buffer);
			Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs = this.RentSocketAsyncEventArgs(true);
			if (int32TaskSocketAsyncEventArgs != null)
			{
				Socket.ConfigureBuffer(int32TaskSocketAsyncEventArgs, buffer, socketFlags, fromNetworkStream);
				return this.GetTaskForSendReceive(this.ReceiveAsync(int32TaskSocketAsyncEventArgs), int32TaskSocketAsyncEventArgs, fromNetworkStream, true);
			}
			return this.ReceiveAsyncApm(buffer, socketFlags);
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0009E614 File Offset: 0x0009C814
		private Task<int> ReceiveAsyncApm(ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(this);
			this.BeginReceive(buffer.Array, buffer.Offset, buffer.Count, socketFlags, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<int> taskCompletionSource2 = (TaskCompletionSource<int>)iar.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(((Socket)taskCompletionSource2.Task.AsyncState).EndReceive(iar));
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0009E66C File Offset: 0x0009C86C
		internal Task<int> ReceiveAsync(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			Socket.ValidateBuffersList(buffers);
			Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs = this.RentSocketAsyncEventArgs(true);
			if (int32TaskSocketAsyncEventArgs != null)
			{
				Socket.ConfigureBufferList(int32TaskSocketAsyncEventArgs, buffers, socketFlags);
				return this.GetTaskForSendReceive(this.ReceiveAsync(int32TaskSocketAsyncEventArgs), int32TaskSocketAsyncEventArgs, false, true);
			}
			return this.ReceiveAsyncApm(buffers, socketFlags);
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x0009E6AC File Offset: 0x0009C8AC
		private Task<int> ReceiveAsyncApm(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(this);
			this.BeginReceive(buffers, socketFlags, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<int> taskCompletionSource2 = (TaskCompletionSource<int>)iar.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(((Socket)taskCompletionSource2.Task.AsyncState).EndReceive(iar));
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x0009E6F0 File Offset: 0x0009C8F0
		internal Task<SocketReceiveFromResult> ReceiveFromAsync(ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			Socket.StateTaskCompletionSource<EndPoint, SocketReceiveFromResult> stateTaskCompletionSource = new Socket.StateTaskCompletionSource<EndPoint, SocketReceiveFromResult>(this)
			{
				_field1 = remoteEndPoint
			};
			this.BeginReceiveFrom(buffer.Array, buffer.Offset, buffer.Count, socketFlags, ref stateTaskCompletionSource._field1, delegate(IAsyncResult iar)
			{
				Socket.StateTaskCompletionSource<EndPoint, SocketReceiveFromResult> stateTaskCompletionSource2 = (Socket.StateTaskCompletionSource<EndPoint, SocketReceiveFromResult>)iar.AsyncState;
				try
				{
					int num = ((Socket)stateTaskCompletionSource2.Task.AsyncState).EndReceiveFrom(iar, ref stateTaskCompletionSource2._field1);
					stateTaskCompletionSource2.TrySetResult(new SocketReceiveFromResult
					{
						ReceivedBytes = num,
						RemoteEndPoint = stateTaskCompletionSource2._field1
					});
				}
				catch (Exception ex)
				{
					stateTaskCompletionSource2.TrySetException(ex);
				}
			}, stateTaskCompletionSource);
			return stateTaskCompletionSource.Task;
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x0009E754 File Offset: 0x0009C954
		internal Task<SocketReceiveMessageFromResult> ReceiveMessageFromAsync(ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			Socket.StateTaskCompletionSource<SocketFlags, EndPoint, SocketReceiveMessageFromResult> stateTaskCompletionSource = new Socket.StateTaskCompletionSource<SocketFlags, EndPoint, SocketReceiveMessageFromResult>(this)
			{
				_field1 = socketFlags,
				_field2 = remoteEndPoint
			};
			this.BeginReceiveMessageFrom(buffer.Array, buffer.Offset, buffer.Count, socketFlags, ref stateTaskCompletionSource._field2, delegate(IAsyncResult iar)
			{
				Socket.StateTaskCompletionSource<SocketFlags, EndPoint, SocketReceiveMessageFromResult> stateTaskCompletionSource2 = (Socket.StateTaskCompletionSource<SocketFlags, EndPoint, SocketReceiveMessageFromResult>)iar.AsyncState;
				try
				{
					IPPacketInformation ippacketInformation;
					int num = ((Socket)stateTaskCompletionSource2.Task.AsyncState).EndReceiveMessageFrom(iar, ref stateTaskCompletionSource2._field1, ref stateTaskCompletionSource2._field2, out ippacketInformation);
					stateTaskCompletionSource2.TrySetResult(new SocketReceiveMessageFromResult
					{
						ReceivedBytes = num,
						RemoteEndPoint = stateTaskCompletionSource2._field2,
						SocketFlags = stateTaskCompletionSource2._field1,
						PacketInformation = ippacketInformation
					});
				}
				catch (Exception ex)
				{
					stateTaskCompletionSource2.TrySetException(ex);
				}
			}, stateTaskCompletionSource);
			return stateTaskCompletionSource.Task;
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0009E7C0 File Offset: 0x0009C9C0
		internal Task<int> SendAsync(ArraySegment<byte> buffer, SocketFlags socketFlags, bool fromNetworkStream)
		{
			Socket.ValidateBuffer(buffer);
			Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs = this.RentSocketAsyncEventArgs(false);
			if (int32TaskSocketAsyncEventArgs != null)
			{
				Socket.ConfigureBuffer(int32TaskSocketAsyncEventArgs, buffer, socketFlags, fromNetworkStream);
				return this.GetTaskForSendReceive(this.SendAsync(int32TaskSocketAsyncEventArgs), int32TaskSocketAsyncEventArgs, fromNetworkStream, false);
			}
			return this.SendAsyncApm(buffer, socketFlags);
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0009E800 File Offset: 0x0009CA00
		private Task<int> SendAsyncApm(ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(this);
			this.BeginSend(buffer.Array, buffer.Offset, buffer.Count, socketFlags, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<int> taskCompletionSource2 = (TaskCompletionSource<int>)iar.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(((Socket)taskCompletionSource2.Task.AsyncState).EndSend(iar));
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x0009E858 File Offset: 0x0009CA58
		internal Task<int> SendAsync(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			Socket.ValidateBuffersList(buffers);
			Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs = this.RentSocketAsyncEventArgs(false);
			if (int32TaskSocketAsyncEventArgs != null)
			{
				Socket.ConfigureBufferList(int32TaskSocketAsyncEventArgs, buffers, socketFlags);
				return this.GetTaskForSendReceive(this.SendAsync(int32TaskSocketAsyncEventArgs), int32TaskSocketAsyncEventArgs, false, false);
			}
			return this.SendAsyncApm(buffers, socketFlags);
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x0009E898 File Offset: 0x0009CA98
		private Task<int> SendAsyncApm(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(this);
			this.BeginSend(buffers, socketFlags, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<int> taskCompletionSource2 = (TaskCompletionSource<int>)iar.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(((Socket)taskCompletionSource2.Task.AsyncState).EndSend(iar));
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x0009E8DC File Offset: 0x0009CADC
		internal Task<int> SendToAsync(ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEP)
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(this);
			this.BeginSendTo(buffer.Array, buffer.Offset, buffer.Count, socketFlags, remoteEP, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<int> taskCompletionSource2 = (TaskCompletionSource<int>)iar.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(((Socket)taskCompletionSource2.Task.AsyncState).EndSendTo(iar));
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x0009E934 File Offset: 0x0009CB34
		private static void ValidateBuffer(ArraySegment<byte> buffer)
		{
			if (buffer.Array == null)
			{
				throw new ArgumentNullException("Array");
			}
			if (buffer.Offset < 0 || buffer.Offset > buffer.Array.Length)
			{
				throw new ArgumentOutOfRangeException("Offset");
			}
			if (buffer.Count < 0 || buffer.Count > buffer.Array.Length - buffer.Offset)
			{
				throw new ArgumentOutOfRangeException("Count");
			}
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x0009E9AB File Offset: 0x0009CBAB
		private static void ValidateBuffersList(IList<ArraySegment<byte>> buffers)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			if (buffers.Count == 0)
			{
				throw new ArgumentException(string.Format("net_sockets_zerolist", "buffers"), "buffers");
			}
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x0009E9DD File Offset: 0x0009CBDD
		private static void ConfigureBuffer(Socket.Int32TaskSocketAsyncEventArgs saea, ArraySegment<byte> buffer, SocketFlags socketFlags, bool wrapExceptionsInIOExceptions)
		{
			if (saea.BufferList != null)
			{
				saea.BufferList = null;
			}
			saea.SetBuffer(buffer.Array, buffer.Offset, buffer.Count);
			saea.SocketFlags = socketFlags;
			saea._wrapExceptionsInIOExceptions = wrapExceptionsInIOExceptions;
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x0009EA17 File Offset: 0x0009CC17
		private static void ConfigureBufferList(Socket.Int32TaskSocketAsyncEventArgs saea, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			if (saea.Buffer != null)
			{
				saea.SetBuffer(null, 0, 0);
			}
			saea.BufferList = buffers;
			saea.SocketFlags = socketFlags;
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x0009EA38 File Offset: 0x0009CC38
		private Task<int> GetTaskForSendReceive(bool pending, Socket.Int32TaskSocketAsyncEventArgs saea, bool fromNetworkStream, bool isReceive)
		{
			Task<int> task;
			if (pending)
			{
				bool flag;
				task = saea.GetCompletionResponsibility(out flag).Task;
				if (flag)
				{
					this.ReturnSocketAsyncEventArgs(saea, isReceive);
				}
			}
			else
			{
				if (saea.SocketError == SocketError.Success)
				{
					int bytesTransferred = saea.BytesTransferred;
					if (bytesTransferred == 0 || (fromNetworkStream & !isReceive))
					{
						task = Socket.s_zeroTask;
					}
					else
					{
						Task<int> successfullyCompletedTask = saea._successfullyCompletedTask;
						task = ((successfullyCompletedTask != null && successfullyCompletedTask.Result == bytesTransferred) ? successfullyCompletedTask : (saea._successfullyCompletedTask = Task.FromResult<int>(bytesTransferred)));
					}
				}
				else
				{
					task = Task.FromException<int>(Socket.GetException(saea.SocketError, fromNetworkStream));
				}
				this.ReturnSocketAsyncEventArgs(saea, isReceive);
			}
			return task;
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0009EAD4 File Offset: 0x0009CCD4
		private static void CompleteAccept(Socket s, Socket.TaskSocketAsyncEventArgs<Socket> saea)
		{
			SocketError socketError = saea.SocketError;
			Socket acceptSocket = saea.AcceptSocket;
			bool flag;
			AsyncTaskMethodBuilder<Socket> completionResponsibility = saea.GetCompletionResponsibility(out flag);
			if (flag)
			{
				s.ReturnSocketAsyncEventArgs(saea);
			}
			if (socketError == SocketError.Success)
			{
				completionResponsibility.SetResult(acceptSocket);
				return;
			}
			completionResponsibility.SetException(Socket.GetException(socketError, false));
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0009EB1C File Offset: 0x0009CD1C
		private static void CompleteSendReceive(Socket s, Socket.Int32TaskSocketAsyncEventArgs saea, bool isReceive)
		{
			SocketError socketError = saea.SocketError;
			int bytesTransferred = saea.BytesTransferred;
			bool wrapExceptionsInIOExceptions = saea._wrapExceptionsInIOExceptions;
			bool flag;
			AsyncTaskMethodBuilder<int> completionResponsibility = saea.GetCompletionResponsibility(out flag);
			if (flag)
			{
				s.ReturnSocketAsyncEventArgs(saea, isReceive);
			}
			if (socketError == SocketError.Success)
			{
				completionResponsibility.SetResult(bytesTransferred);
				return;
			}
			completionResponsibility.SetException(Socket.GetException(socketError, wrapExceptionsInIOExceptions));
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0009EB70 File Offset: 0x0009CD70
		private static Exception GetException(SocketError error, bool wrapExceptionsInIOExceptions = false)
		{
			Exception ex = new SocketException((int)error);
			if (!wrapExceptionsInIOExceptions)
			{
				return ex;
			}
			return new IOException(string.Format("net_io_readwritefailure", ex.Message), ex);
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0009EBA0 File Offset: 0x0009CDA0
		private Socket.Int32TaskSocketAsyncEventArgs RentSocketAsyncEventArgs(bool isReceive)
		{
			Socket.CachedTaskEventArgs cachedTaskEventArgs = LazyInitializer.EnsureInitialized<Socket.CachedTaskEventArgs>(ref this._cachedTaskEventArgs);
			Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs = (isReceive ? Interlocked.Exchange<Socket.Int32TaskSocketAsyncEventArgs>(ref cachedTaskEventArgs.Receive, Socket.s_rentedInt32Sentinel) : Interlocked.Exchange<Socket.Int32TaskSocketAsyncEventArgs>(ref cachedTaskEventArgs.Send, Socket.s_rentedInt32Sentinel));
			if (int32TaskSocketAsyncEventArgs == Socket.s_rentedInt32Sentinel)
			{
				return null;
			}
			if (int32TaskSocketAsyncEventArgs == null)
			{
				int32TaskSocketAsyncEventArgs = new Socket.Int32TaskSocketAsyncEventArgs();
				int32TaskSocketAsyncEventArgs.Completed += (isReceive ? Socket.ReceiveCompletedHandler : Socket.SendCompletedHandler);
			}
			return int32TaskSocketAsyncEventArgs;
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0009EC08 File Offset: 0x0009CE08
		private void ReturnSocketAsyncEventArgs(Socket.Int32TaskSocketAsyncEventArgs saea, bool isReceive)
		{
			saea._accessed = false;
			saea._builder = default(AsyncTaskMethodBuilder<int>);
			saea._wrapExceptionsInIOExceptions = false;
			if (isReceive)
			{
				Volatile.Write<Socket.Int32TaskSocketAsyncEventArgs>(ref this._cachedTaskEventArgs.Receive, saea);
				return;
			}
			Volatile.Write<Socket.Int32TaskSocketAsyncEventArgs>(ref this._cachedTaskEventArgs.Send, saea);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x0009EC55 File Offset: 0x0009CE55
		private void ReturnSocketAsyncEventArgs(Socket.TaskSocketAsyncEventArgs<Socket> saea)
		{
			saea.AcceptSocket = null;
			saea._accessed = false;
			saea._builder = default(AsyncTaskMethodBuilder<Socket>);
			Volatile.Write<Socket.TaskSocketAsyncEventArgs<Socket>>(ref this._cachedTaskEventArgs.Accept, saea);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0009EC84 File Offset: 0x0009CE84
		private void DisposeCachedTaskSocketAsyncEventArgs()
		{
			Socket.CachedTaskEventArgs cachedTaskEventArgs = this._cachedTaskEventArgs;
			if (cachedTaskEventArgs != null)
			{
				Socket.TaskSocketAsyncEventArgs<Socket> taskSocketAsyncEventArgs = Interlocked.Exchange<Socket.TaskSocketAsyncEventArgs<Socket>>(ref cachedTaskEventArgs.Accept, Socket.s_rentedSocketSentinel);
				if (taskSocketAsyncEventArgs != null)
				{
					taskSocketAsyncEventArgs.Dispose();
				}
				Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs = Interlocked.Exchange<Socket.Int32TaskSocketAsyncEventArgs>(ref cachedTaskEventArgs.Receive, Socket.s_rentedInt32Sentinel);
				if (int32TaskSocketAsyncEventArgs != null)
				{
					int32TaskSocketAsyncEventArgs.Dispose();
				}
				Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs2 = Interlocked.Exchange<Socket.Int32TaskSocketAsyncEventArgs>(ref cachedTaskEventArgs.Send, Socket.s_rentedInt32Sentinel);
				if (int32TaskSocketAsyncEventArgs2 == null)
				{
					return;
				}
				int32TaskSocketAsyncEventArgs2.Dispose();
			}
		}

		// Token: 0x04001E20 RID: 7712
		internal const int DefaultCloseTimeout = -1;

		// Token: 0x04001E21 RID: 7713
		private object m_AcceptQueueOrConnectResult;

		// Token: 0x04001E22 RID: 7714
		private int asyncConnectOperationLock;

		// Token: 0x04001E23 RID: 7715
		private SafeCloseSocket m_Handle;

		// Token: 0x04001E24 RID: 7716
		internal EndPoint m_RightEndPoint;

		// Token: 0x04001E25 RID: 7717
		internal EndPoint m_RemoteEndPoint;

		// Token: 0x04001E26 RID: 7718
		private bool m_IsConnected;

		// Token: 0x04001E27 RID: 7719
		private bool m_IsDisconnected;

		// Token: 0x04001E28 RID: 7720
		private bool willBlock;

		// Token: 0x04001E29 RID: 7721
		private bool willBlockInternal;

		// Token: 0x04001E2A RID: 7722
		private bool isListening;

		// Token: 0x04001E2B RID: 7723
		private bool m_NonBlockingConnectInProgress;

		// Token: 0x04001E2C RID: 7724
		private EndPoint m_NonBlockingConnectRightEndPoint;

		// Token: 0x04001E2D RID: 7725
		private AddressFamily addressFamily;

		// Token: 0x04001E2E RID: 7726
		private SocketType socketType;

		// Token: 0x04001E2F RID: 7727
		private ProtocolType protocolType;

		// Token: 0x04001E30 RID: 7728
		private Socket.CacheSet m_Caches;

		// Token: 0x04001E31 RID: 7729
		internal static volatile bool UseOverlappedIO;

		// Token: 0x04001E32 RID: 7730
		private bool useOverlappedIO;

		// Token: 0x04001E33 RID: 7731
		private bool m_BoundToThreadPool;

		// Token: 0x04001E34 RID: 7732
		private bool m_ReceivingPacketInformation;

		// Token: 0x04001E35 RID: 7733
		private ManualResetEvent m_AsyncEvent;

		// Token: 0x04001E36 RID: 7734
		private RegisteredWaitHandle m_RegisteredWait;

		// Token: 0x04001E37 RID: 7735
		private AsyncEventBits m_BlockEventBits;

		// Token: 0x04001E38 RID: 7736
		private SocketAddress m_PermittedRemoteAddress;

		// Token: 0x04001E39 RID: 7737
		private DynamicWinsockMethods m_DynamicWinsockMethods;

		// Token: 0x04001E3A RID: 7738
		private static object s_InternalSyncObject;

		// Token: 0x04001E3B RID: 7739
		private int m_CloseTimeout;

		// Token: 0x04001E3C RID: 7740
		private int m_IntCleanedUp;

		// Token: 0x04001E3D RID: 7741
		private const int microcnv = 1000000;

		// Token: 0x04001E3E RID: 7742
		private static readonly int protocolInformationSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.WSAPROTOCOL_INFO));

		// Token: 0x04001E3F RID: 7743
		internal static volatile bool s_SupportsIPv4;

		// Token: 0x04001E40 RID: 7744
		internal static volatile bool s_SupportsIPv6;

		// Token: 0x04001E41 RID: 7745
		internal static volatile bool s_OSSupportsIPv6;

		// Token: 0x04001E42 RID: 7746
		internal static volatile bool s_Initialized;

		// Token: 0x04001E43 RID: 7747
		private static volatile WaitOrTimerCallback s_RegisteredWaitCallback;

		// Token: 0x04001E44 RID: 7748
		private static volatile bool s_LoggingEnabled;

		// Token: 0x04001E45 RID: 7749
		internal static volatile bool s_PerfCountersEnabled;

		// Token: 0x04001E46 RID: 7750
		private static readonly EventHandler<SocketAsyncEventArgs> AcceptCompletedHandler = delegate(object s, SocketAsyncEventArgs e)
		{
			Socket.CompleteAccept((Socket)s, (Socket.TaskSocketAsyncEventArgs<Socket>)e);
		};

		// Token: 0x04001E47 RID: 7751
		private static readonly EventHandler<SocketAsyncEventArgs> ReceiveCompletedHandler = delegate(object s, SocketAsyncEventArgs e)
		{
			Socket.CompleteSendReceive((Socket)s, (Socket.Int32TaskSocketAsyncEventArgs)e, true);
		};

		// Token: 0x04001E48 RID: 7752
		private static readonly EventHandler<SocketAsyncEventArgs> SendCompletedHandler = delegate(object s, SocketAsyncEventArgs e)
		{
			Socket.CompleteSendReceive((Socket)s, (Socket.Int32TaskSocketAsyncEventArgs)e, false);
		};

		// Token: 0x04001E49 RID: 7753
		private static readonly Socket.TaskSocketAsyncEventArgs<Socket> s_rentedSocketSentinel = new Socket.TaskSocketAsyncEventArgs<Socket>();

		// Token: 0x04001E4A RID: 7754
		private static readonly Socket.Int32TaskSocketAsyncEventArgs s_rentedInt32Sentinel = new Socket.Int32TaskSocketAsyncEventArgs();

		// Token: 0x04001E4B RID: 7755
		private static readonly Task<int> s_zeroTask = Task.FromResult<int>(0);

		// Token: 0x04001E4C RID: 7756
		private Socket.CachedTaskEventArgs _cachedTaskEventArgs;

		// Token: 0x020007D6 RID: 2006
		private class CacheSet
		{
			// Token: 0x04003491 RID: 13457
			internal CallbackClosure ConnectClosureCache;

			// Token: 0x04003492 RID: 13458
			internal CallbackClosure AcceptClosureCache;

			// Token: 0x04003493 RID: 13459
			internal CallbackClosure SendClosureCache;

			// Token: 0x04003494 RID: 13460
			internal CallbackClosure ReceiveClosureCache;

			// Token: 0x04003495 RID: 13461
			internal OverlappedCache SendOverlappedCache;

			// Token: 0x04003496 RID: 13462
			internal OverlappedCache ReceiveOverlappedCache;
		}

		// Token: 0x020007D7 RID: 2007
		private class MultipleAddressConnectAsyncResult : ContextAwareResult
		{
			// Token: 0x06004392 RID: 17298 RVA: 0x0011C87A File Offset: 0x0011AA7A
			internal MultipleAddressConnectAsyncResult(IPAddress[] addresses, int port, Socket socket, object myState, AsyncCallback myCallBack)
				: base(socket, myState, myCallBack)
			{
				this.addresses = addresses;
				this.port = port;
				this.socket = socket;
			}

			// Token: 0x17000F4F RID: 3919
			// (get) Token: 0x06004393 RID: 17299 RVA: 0x0011C89C File Offset: 0x0011AA9C
			internal EndPoint RemoteEndPoint
			{
				get
				{
					if (this.addresses != null && this.index > 0 && this.index < this.addresses.Length)
					{
						return new IPEndPoint(this.addresses[this.index], this.port);
					}
					return null;
				}
			}

			// Token: 0x04003497 RID: 13463
			internal Socket socket;

			// Token: 0x04003498 RID: 13464
			internal IPAddress[] addresses;

			// Token: 0x04003499 RID: 13465
			internal int index;

			// Token: 0x0400349A RID: 13466
			internal int port;

			// Token: 0x0400349B RID: 13467
			internal Exception lastException;
		}

		// Token: 0x020007D8 RID: 2008
		private class StateTaskCompletionSource<TField1, TResult> : TaskCompletionSource<TResult>
		{
			// Token: 0x06004394 RID: 17300 RVA: 0x0011C8D9 File Offset: 0x0011AAD9
			public StateTaskCompletionSource(object baseState)
				: base(baseState)
			{
			}

			// Token: 0x0400349C RID: 13468
			internal TField1 _field1;
		}

		// Token: 0x020007D9 RID: 2009
		private class StateTaskCompletionSource<TField1, TField2, TResult> : Socket.StateTaskCompletionSource<TField1, TResult>
		{
			// Token: 0x06004395 RID: 17301 RVA: 0x0011C8E2 File Offset: 0x0011AAE2
			public StateTaskCompletionSource(object baseState)
				: base(baseState)
			{
			}

			// Token: 0x0400349D RID: 13469
			internal TField2 _field2;
		}

		// Token: 0x020007DA RID: 2010
		private sealed class CachedTaskEventArgs
		{
			// Token: 0x0400349E RID: 13470
			public Socket.TaskSocketAsyncEventArgs<Socket> Accept;

			// Token: 0x0400349F RID: 13471
			public Socket.Int32TaskSocketAsyncEventArgs Receive;

			// Token: 0x040034A0 RID: 13472
			public Socket.Int32TaskSocketAsyncEventArgs Send;
		}

		// Token: 0x020007DB RID: 2011
		private class TaskSocketAsyncEventArgs<TResult> : SocketAsyncEventArgs
		{
			// Token: 0x06004397 RID: 17303 RVA: 0x0011C8F4 File Offset: 0x0011AAF4
			internal AsyncTaskMethodBuilder<TResult> GetCompletionResponsibility(out bool responsibleForReturningToPool)
			{
				AsyncTaskMethodBuilder<TResult> builder;
				lock (this)
				{
					responsibleForReturningToPool = this._accessed;
					this._accessed = true;
					Task<TResult> task = this._builder.Task;
					builder = this._builder;
				}
				return builder;
			}

			// Token: 0x040034A1 RID: 13473
			internal AsyncTaskMethodBuilder<TResult> _builder;

			// Token: 0x040034A2 RID: 13474
			internal bool _accessed;
		}

		// Token: 0x020007DC RID: 2012
		private sealed class Int32TaskSocketAsyncEventArgs : Socket.TaskSocketAsyncEventArgs<int>
		{
			// Token: 0x040034A3 RID: 13475
			internal Task<int> _successfullyCompletedTask;

			// Token: 0x040034A4 RID: 13476
			internal bool _wrapExceptionsInIOExceptions;
		}
	}
}

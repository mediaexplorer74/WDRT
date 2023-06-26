using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Provides the underlying stream of data for network access.</summary>
	// Token: 0x02000370 RID: 880
	public class NetworkStream : Stream
	{
		// Token: 0x06001FD7 RID: 8151 RVA: 0x00094DC8 File Offset: 0x00092FC8
		internal NetworkStream()
		{
			this.m_OwnsSocket = true;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.  
		///  -or-  
		///  The <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.  
		///  -or-  
		///  The <paramref name="socket" /> parameter is in a nonblocking state.</exception>
		// Token: 0x06001FD8 RID: 8152 RVA: 0x00094DEC File Offset: 0x00092FEC
		public NetworkStream(Socket socket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, FileAccess.ReadWrite);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified <see cref="T:System.Net.Sockets.Socket" /> ownership.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data.</param>
		/// <param name="ownsSocket">Set to <see langword="true" /> to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> will take ownership of the <see cref="T:System.Net.Sockets.Socket" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.  
		///  -or-  
		///  the value of the <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.  
		///  -or-  
		///  the <paramref name="socket" /> parameter is in a nonblocking state.</exception>
		// Token: 0x06001FD9 RID: 8153 RVA: 0x00094E1F File Offset: 0x0009301F
		public NetworkStream(Socket socket, bool ownsSocket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, FileAccess.ReadWrite);
			this.m_OwnsSocket = ownsSocket;
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x00094E5C File Offset: 0x0009305C
		internal NetworkStream(NetworkStream networkStream, bool ownsSocket)
		{
			Socket socket = networkStream.Socket;
			if (socket == null)
			{
				throw new ArgumentNullException("networkStream");
			}
			this.InitNetworkStream(socket, FileAccess.ReadWrite);
			this.m_OwnsSocket = ownsSocket;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified access rights.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data.</param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values that specify the type of access given to the <see cref="T:System.Net.Sockets.NetworkStream" /> over the provided <see cref="T:System.Net.Sockets.Socket" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.  
		///  -or-  
		///  the <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.  
		///  -or-  
		///  the <paramref name="socket" /> parameter is in a nonblocking state.</exception>
		// Token: 0x06001FDB RID: 8155 RVA: 0x00094EA8 File Offset: 0x000930A8
		public NetworkStream(Socket socket, FileAccess access)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, access);
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified access rights and the specified <see cref="T:System.Net.Sockets.Socket" /> ownership.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data.</param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values that specifies the type of access given to the <see cref="T:System.Net.Sockets.NetworkStream" /> over the provided <see cref="T:System.Net.Sockets.Socket" />.</param>
		/// <param name="ownsSocket">Set to <see langword="true" /> to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> will take ownership of the <see cref="T:System.Net.Sockets.Socket" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.  
		///  -or-  
		///  The <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.  
		///  -or-  
		///  The <paramref name="socket" /> parameter is in a nonblocking state.</exception>
		// Token: 0x06001FDC RID: 8156 RVA: 0x00094EDB File Offset: 0x000930DB
		public NetworkStream(Socket socket, FileAccess access, bool ownsSocket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, access);
			this.m_OwnsSocket = ownsSocket;
		}

		/// <summary>Gets the underlying <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> that represents the underlying network connection.</returns>
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x00094F15 File Offset: 0x00093115
		protected Socket Socket
		{
			get
			{
				return this.m_StreamSocket;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x00094F20 File Offset: 0x00093120
		internal Socket InternalSocket
		{
			get
			{
				Socket streamSocket = this.m_StreamSocket;
				if (this.m_CleanedUp || streamSocket == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return streamSocket;
			}
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x00094F54 File Offset: 0x00093154
		internal void InternalAbortSocket()
		{
			if (!this.m_OwnsSocket)
			{
				throw new InvalidOperationException();
			}
			Socket streamSocket = this.m_StreamSocket;
			if (this.m_CleanedUp || streamSocket == null)
			{
				return;
			}
			try
			{
				streamSocket.Close(0);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x00094FA4 File Offset: 0x000931A4
		internal void ConvertToNotSocketOwner()
		{
			this.m_OwnsSocket = false;
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> can be read.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> can be read; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00094FB3 File Offset: 0x000931B3
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x00094FBB File Offset: 0x000931BB
		protected bool Readable
		{
			get
			{
				return this.m_Readable;
			}
			set
			{
				this.m_Readable = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> is writable.</summary>
		/// <returns>
		///   <see langword="true" /> if data can be written to the stream; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x00094FC4 File Offset: 0x000931C4
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x00094FCC File Offset: 0x000931CC
		protected bool Writeable
		{
			get
			{
				return this.m_Writeable;
			}
			set
			{
				this.m_Writeable = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> supports reading.</summary>
		/// <returns>
		///   <see langword="true" /> if data can be read from the stream; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00094FD5 File Offset: 0x000931D5
		public override bool CanRead
		{
			get
			{
				return this.m_Readable;
			}
		}

		/// <summary>Gets a value that indicates whether the stream supports seeking. This property is not currently supported.This property always returns <see langword="false" />.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases to indicate that <see cref="T:System.Net.Sockets.NetworkStream" /> cannot seek a specific location in the stream.</returns>
		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x00094FDD File Offset: 0x000931DD
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> supports writing.</summary>
		/// <returns>
		///   <see langword="true" /> if data can be written to the <see cref="T:System.Net.Sockets.NetworkStream" />; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x00094FE0 File Offset: 0x000931E0
		public override bool CanWrite
		{
			get
			{
				return this.m_Writeable;
			}
		}

		/// <summary>Indicates whether timeout properties are usable for <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x00094FE8 File Offset: 0x000931E8
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the amount of time that a read operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time, in milliseconds, that will elapse before a read operation fails. The default value, <see cref="F:System.Threading.Timeout.Infinite" />, specifies that the read operation does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x00094FEC File Offset: 0x000931EC
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x0009501A File Offset: 0x0009321A
		public override int ReadTimeout
		{
			get
			{
				int num = (int)this.m_StreamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
				}
				this.SetSocketTimeoutOption(SocketShutdown.Receive, value, false);
			}
		}

		/// <summary>Gets or sets the amount of time that a write operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time, in milliseconds, that will elapse before a write operation fails. The default value, <see cref="F:System.Threading.Timeout.Infinite" />, specifies that the write operation does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x00095044 File Offset: 0x00093244
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x00095072 File Offset: 0x00093272
		public override int WriteTimeout
		{
			get
			{
				int num = (int)this.m_StreamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
				}
				this.SetSocketTimeoutOption(SocketShutdown.Send, value, false);
			}
		}

		/// <summary>Gets a value that indicates whether data is available on the <see cref="T:System.Net.Sockets.NetworkStream" /> to be read.</summary>
		/// <returns>
		///   <see langword="true" /> if data is available on the stream to be read; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code and refer to the Windows Sockets version 2 API error code documentation for a detailed description of the error.</exception>
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x0009509C File Offset: 0x0009329C
		public virtual bool DataAvailable
		{
			get
			{
				if (this.m_CleanedUp)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				Socket streamSocket = this.m_StreamSocket;
				if (streamSocket == null)
				{
					throw new IOException(SR.GetString("net_io_readfailure", new object[] { SR.GetString("net_io_connectionclosed") }));
				}
				return streamSocket.Available != 0;
			}
		}

		/// <summary>Gets the length of the data available on the stream. This property is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The length of the data available on the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property.</exception>
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x000950FA File Offset: 0x000932FA
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		/// <summary>Gets or sets the current position in the stream. This property is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The current position in the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property.</exception>
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x0009510B File Offset: 0x0009330B
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x0009511C File Offset: 0x0009331C
		public override long Position
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		/// <summary>Sets the current position of the stream to the given value. This method is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="offset">This parameter is not used.</param>
		/// <param name="origin">This parameter is not used.</param>
		/// <returns>The position in the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property.</exception>
		// Token: 0x06001FF1 RID: 8177 RVA: 0x0009512D File Offset: 0x0009332D
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x00095140 File Offset: 0x00093340
		internal void InitNetworkStream(Socket socket, FileAccess Access)
		{
			if (!socket.Blocking)
			{
				throw new IOException(SR.GetString("net_sockets_blocking"));
			}
			if (!socket.Connected)
			{
				throw new IOException(SR.GetString("net_notconnected"));
			}
			if (socket.SocketType != SocketType.Stream)
			{
				throw new IOException(SR.GetString("net_notstream"));
			}
			this.m_StreamSocket = socket;
			switch (Access)
			{
			case FileAccess.Read:
				this.m_Readable = true;
				return;
			case FileAccess.Write:
				this.m_Writeable = true;
				return;
			}
			this.m_Readable = true;
			this.m_Writeable = true;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000951D4 File Offset: 0x000933D4
		internal bool PollRead()
		{
			if (this.m_CleanedUp)
			{
				return false;
			}
			Socket streamSocket = this.m_StreamSocket;
			return streamSocket != null && streamSocket.Poll(0, SelectMode.SelectRead);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00095204 File Offset: 0x00093404
		internal bool Poll(int microSeconds, SelectMode mode)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			return streamSocket.Poll(microSeconds, mode);
		}

		/// <summary>Reads data from the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the location in memory to store data read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin storing the data to.</param>
		/// <param name="size">The number of bytes to read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <returns>The number of bytes read from the <see cref="T:System.Net.Sockets.NetworkStream" />, or 0 if the socket is closed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.  
		///  -or-  
		///  The <paramref name="size" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.  
		///  -or-  
		///  There is a failure reading from the network.</exception>
		// Token: 0x06001FF5 RID: 8181 RVA: 0x00095264 File Offset: 0x00093464
		public override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			bool canRead = this.CanRead;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
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
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			int num2;
			try
			{
				int num = streamSocket.Receive(buffer, offset, size, SocketFlags.None);
				num2 = num;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { ex.Message }), ex);
			}
			return num2;
		}

		/// <summary>Writes data to the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to write to the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> from which to start writing data.</param>
		/// <param name="size">The number of bytes to write to the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.  
		///  -or-  
		///  The <paramref name="size" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.IO.IOException">There was a failure while writing to the network.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.  
		///  -or-  
		///  There was a failure reading from the network.</exception>
		// Token: 0x06001FF6 RID: 8182 RVA: 0x00095370 File Offset: 0x00093570
		public override void Write(byte[] buffer, int offset, int size)
		{
			bool canWrite = this.CanWrite;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException(SR.GetString("net_readonlystream"));
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
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			try
			{
				streamSocket.Send(buffer, offset, size, SocketFlags.None);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { ex.Message }), ex);
			}
		}

		/// <summary>Closes the <see cref="T:System.Net.Sockets.NetworkStream" /> after waiting the specified time to allow data to be sent.</summary>
		/// <param name="timeout">A 32-bit signed integer that specifies the number of milliseconds to wait to send any remaining data before closing.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1.</exception>
		// Token: 0x06001FF7 RID: 8183 RVA: 0x00095474 File Offset: 0x00093674
		public void Close(int timeout)
		{
			if (timeout < -1)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			this.m_CloseTimeout = timeout;
			this.Close();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.NetworkStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001FF8 RID: 8184 RVA: 0x00095494 File Offset: 0x00093694
		protected override void Dispose(bool disposing)
		{
			bool cleanedUp = this.m_CleanedUp;
			this.m_CleanedUp = true;
			if (!cleanedUp && disposing && this.m_StreamSocket != null)
			{
				this.m_Readable = false;
				this.m_Writeable = false;
				if (this.m_OwnsSocket)
				{
					Socket streamSocket = this.m_StreamSocket;
					if (streamSocket != null)
					{
						streamSocket.InternalShutdown(SocketShutdown.Both);
						streamSocket.Close(this.m_CloseTimeout);
					}
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		// Token: 0x06001FF9 RID: 8185 RVA: 0x00095500 File Offset: 0x00093700
		~NetworkStream()
		{
			this.Dispose(false);
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x00095530 File Offset: 0x00093730
		internal bool Connected
		{
			get
			{
				Socket streamSocket = this.m_StreamSocket;
				return !this.m_CleanedUp && streamSocket != null && streamSocket.Connected;
			}
		}

		/// <summary>Begins an asynchronous read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the location in memory to store data read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin storing the data.</param>
		/// <param name="size">The number of bytes to read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate that is executed when <see cref="M:System.Net.Sockets.NetworkStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> completes.</param>
		/// <param name="state">An object that contains any additional user-defined data.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="offset" /> parameter is greater than the length of the <paramref name="buffer" /> paramater.  
		///  -or-  
		///  The <paramref name="size" /> is less than 0.  
		///  -or-  
		///  The <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.  
		///  -or-  
		///  There was a failure while reading from the network.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.</exception>
		// Token: 0x06001FFB RID: 8187 RVA: 0x0009555C File Offset: 0x0009375C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canRead = this.CanRead;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
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
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			IAsyncResult asyncResult2;
			try
			{
				IAsyncResult asyncResult = streamSocket.BeginReceive(buffer, offset, size, SocketFlags.None, callback, state);
				asyncResult2 = asyncResult;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { ex.Message }), ex);
			}
			return asyncResult2;
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0009566C File Offset: 0x0009386C
		internal virtual IAsyncResult UnsafeBeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canRead = this.CanRead;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			IAsyncResult asyncResult2;
			try
			{
				IAsyncResult asyncResult = streamSocket.UnsafeBeginReceive(buffer, offset, size, SocketFlags.None, callback, state);
				asyncResult2 = asyncResult;
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { ex.Message }), ex);
			}
			return asyncResult2;
		}

		/// <summary>Handles the end of an asynchronous read.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that represents an asynchronous call.</param>
		/// <returns>The number of bytes read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.</exception>
		// Token: 0x06001FFD RID: 8189 RVA: 0x00095730 File Offset: 0x00093930
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			int num2;
			try
			{
				int num = streamSocket.EndReceive(asyncResult);
				num2 = num;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { ex.Message }), ex);
			}
			return num2;
		}

		/// <summary>Begins an asynchronous write to a stream.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to write to the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin sending the data.</param>
		/// <param name="size">The number of bytes to write to the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate that is executed when <see cref="M:System.Net.Sockets.NetworkStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> completes.</param>
		/// <param name="state">An object that contains any additional user-defined data.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.  
		///  -or-  
		///  The <paramref name="size" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.  
		///  -or-  
		///  There was a failure while writing to the network.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.</exception>
		// Token: 0x06001FFE RID: 8190 RVA: 0x000957EC File Offset: 0x000939EC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canWrite = this.CanWrite;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException(SR.GetString("net_readonlystream"));
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
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			IAsyncResult asyncResult2;
			try
			{
				IAsyncResult asyncResult = streamSocket.BeginSend(buffer, offset, size, SocketFlags.None, callback, state);
				asyncResult2 = asyncResult;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { ex.Message }), ex);
			}
			return asyncResult2;
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000958FC File Offset: 0x00093AFC
		internal virtual IAsyncResult UnsafeBeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canWrite = this.CanWrite;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException(SR.GetString("net_readonlystream"));
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			IAsyncResult asyncResult2;
			try
			{
				IAsyncResult asyncResult = streamSocket.UnsafeBeginSend(buffer, offset, size, SocketFlags.None, callback, state);
				asyncResult2 = asyncResult;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { ex.Message }), ex);
			}
			return asyncResult2;
		}

		/// <summary>Handles the end of an asynchronous write.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.  
		///  -or-  
		///  An error occurred while writing to the network.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.</exception>
		// Token: 0x06002000 RID: 8192 RVA: 0x000959D4 File Offset: 0x00093BD4
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			try
			{
				streamSocket.EndSend(asyncResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { ex.Message }), ex);
			}
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00095A90 File Offset: 0x00093C90
		internal virtual void MultipleWrite(BufferOffsetSize[] buffers)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			try
			{
				streamSocket.MultipleSend(buffers, SocketFlags.None);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { ex.Message }), ex);
			}
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00095B30 File Offset: 0x00093D30
		internal virtual IAsyncResult BeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			IAsyncResult asyncResult2;
			try
			{
				IAsyncResult asyncResult = streamSocket.BeginMultipleSend(buffers, SocketFlags.None, callback, state);
				asyncResult2 = asyncResult;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { ex.Message }), ex);
			}
			return asyncResult2;
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x00095BD4 File Offset: 0x00093DD4
		internal virtual IAsyncResult UnsafeBeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			IAsyncResult asyncResult2;
			try
			{
				IAsyncResult asyncResult = streamSocket.UnsafeBeginMultipleSend(buffers, SocketFlags.None, callback, state);
				asyncResult2 = asyncResult;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { ex.Message }), ex);
			}
			return asyncResult2;
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00095C78 File Offset: 0x00093E78
		internal virtual void EndMultipleWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			try
			{
				streamSocket.EndMultipleSend(asyncResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[] { ex.Message }), ex);
			}
		}

		/// <summary>Flushes data from the stream. This method is reserved for future use.</summary>
		// Token: 0x06002005 RID: 8197 RVA: 0x00095D18 File Offset: 0x00093F18
		public override void Flush()
		{
		}

		/// <summary>Flushes data from the stream as an asynchronous operation.</summary>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06002006 RID: 8198 RVA: 0x00095D1A File Offset: 0x00093F1A
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		/// <summary>Sets the length of the stream. This method always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">This parameter is not used.</param>
		/// <exception cref="T:System.NotSupportedException">Any use of this property.</exception>
		// Token: 0x06002007 RID: 8199 RVA: 0x00095D21 File Offset: 0x00093F21
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00095D34 File Offset: 0x00093F34
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			if (timeout < 0)
			{
				timeout = 0;
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				return;
			}
			if ((mode == SocketShutdown.Send || mode == SocketShutdown.Both) && timeout != this.m_CurrentWriteTimeout)
			{
				streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout, silent);
				this.m_CurrentWriteTimeout = timeout;
			}
			if ((mode == SocketShutdown.Receive || mode == SocketShutdown.Both) && timeout != this.m_CurrentReadTimeout)
			{
				streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout, silent);
				this.m_CurrentReadTimeout = timeout;
			}
		}

		// Token: 0x04001DD2 RID: 7634
		private Socket m_StreamSocket;

		// Token: 0x04001DD3 RID: 7635
		private bool m_Readable;

		// Token: 0x04001DD4 RID: 7636
		private bool m_Writeable;

		// Token: 0x04001DD5 RID: 7637
		private bool m_OwnsSocket;

		// Token: 0x04001DD6 RID: 7638
		private int m_CloseTimeout = -1;

		// Token: 0x04001DD7 RID: 7639
		private volatile bool m_CleanedUp;

		// Token: 0x04001DD8 RID: 7640
		private int m_CurrentReadTimeout = -1;

		// Token: 0x04001DD9 RID: 7641
		private int m_CurrentWriteTimeout = -1;
	}
}

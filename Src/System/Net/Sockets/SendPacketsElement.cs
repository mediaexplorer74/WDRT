using System;

namespace System.Net.Sockets
{
	/// <summary>Represents an element in a <see cref="T:System.Net.Sockets.SendPacketsElement" /> array.</summary>
	// Token: 0x0200037A RID: 890
	public class SendPacketsElement
	{
		// Token: 0x0600211E RID: 8478 RVA: 0x0009ED91 File Offset: 0x0009CF91
		private SendPacketsElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified file.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		// Token: 0x0600211F RID: 8479 RVA: 0x0009ED99 File Offset: 0x0009CF99
		public SendPacketsElement(string filepath)
			: this(filepath, 0, 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified filename path, offset, and count.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the file to the location in the file to start sending the file specified in the <paramref name="filepath" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, the entire file is sent.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero.  
		///  The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the file indicated by the <paramref name="filepath" /> parameter.</exception>
		// Token: 0x06002120 RID: 8480 RVA: 0x0009EDA5 File Offset: 0x0009CFA5
		public SendPacketsElement(string filepath, int offset, int count)
			: this(filepath, offset, count, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified filename path, buffer offset, and count with an option to combine this element with the next element in a single send request from the sockets layer to the transport.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the file to the location in the file to start sending the file specified in the <paramref name="filepath" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, the entire file is sent.</param>
		/// <param name="endOfPacket">A Boolean value that specifies that this element should not be combined with the next element in a single send request from the sockets layer to the transport. This flag is used for granular control of the content of each message on a datagram or message-oriented socket.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero.  
		///  The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the file indicated by the <paramref name="filepath" /> parameter.</exception>
		// Token: 0x06002121 RID: 8481 RVA: 0x0009EDB4 File Offset: 0x0009CFB4
		public SendPacketsElement(string filepath, int offset, int count, bool endOfPacket)
		{
			if (filepath == null)
			{
				throw new ArgumentNullException("filepath");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Initialize(filepath, null, offset, count, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.File, endOfPacket);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		// Token: 0x06002122 RID: 8482 RVA: 0x0009EE00 File Offset: 0x0009D000
		public SendPacketsElement(byte[] buffer)
			: this(buffer, 0, (buffer != null) ? buffer.Length : 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer, buffer offset, and count.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the <paramref name="buffer" /> to the location in the <paramref name="buffer" /> to start sending the data specified in the <paramref name="buffer" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, no bytes are sent.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero.  
		///  The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the buffer</exception>
		// Token: 0x06002123 RID: 8483 RVA: 0x0009EE14 File Offset: 0x0009D014
		public SendPacketsElement(byte[] buffer, int offset, int count)
			: this(buffer, offset, count, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer, buffer offset, and count with an option to combine this element with the next element in a single send request from the sockets layer to the transport.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the <paramref name="buffer" /> to the location in the <paramref name="buffer" /> to start sending the data specified in the <paramref name="buffer" /> parameter.</param>
		/// <param name="count">The number bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, no bytes are sent.</param>
		/// <param name="endOfPacket">A Boolean value that specifies that this element should not be combined with the next element in a single send request from the sockets layer to the transport. This flag is used for granular control of the content of each message on a datagram or message-oriented socket.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero.  
		///  The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the buffer</exception>
		// Token: 0x06002124 RID: 8484 RVA: 0x0009EE20 File Offset: 0x0009D020
		public SendPacketsElement(byte[] buffer, int offset, int count, bool endOfPacket)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Initialize(null, buffer, offset, count, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.Memory, endOfPacket);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0009EE7A File Offset: 0x0009D07A
		private void Initialize(string filePath, byte[] buffer, int offset, int count, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags flags, bool endOfPacket)
		{
			this.m_FilePath = filePath;
			this.m_Buffer = buffer;
			this.m_Offset = offset;
			this.m_Count = count;
			this.m_Flags = flags;
			if (endOfPacket)
			{
				this.m_Flags |= UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.EndOfPacket;
			}
		}

		/// <summary>Gets the filename of the file to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="filepath" /> parameter.</summary>
		/// <returns>The filename of the file to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="filepath" /> parameter.</returns>
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x0009EEB3 File Offset: 0x0009D0B3
		public string FilePath
		{
			get
			{
				return this.m_FilePath;
			}
		}

		/// <summary>Gets the buffer to be sent if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="buffer" /> parameter.</summary>
		/// <returns>The byte buffer to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="buffer" /> parameter.</returns>
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x0009EEBB File Offset: 0x0009D0BB
		public byte[] Buffer
		{
			get
			{
				return this.m_Buffer;
			}
		}

		/// <summary>Gets the count of bytes to be sent.</summary>
		/// <returns>The count of bytes to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="count" /> parameter.</returns>
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x0009EEC3 File Offset: 0x0009D0C3
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		/// <summary>Gets the offset, in bytes, from the beginning of the data buffer or file to the location in the buffer or file to start sending the data.</summary>
		/// <returns>The offset, in bytes, from the beginning of the data buffer or file to the location in the buffer or file to start sending the data.</returns>
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x0009EECB File Offset: 0x0009D0CB
		public int Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		/// <summary>Gets a Boolean value that indicates if this element should not be combined with the next element in a single send request from the sockets layer to the transport.</summary>
		/// <returns>A Boolean value that indicates if this element should not be combined with the next element in a single send request.</returns>
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x0009EED3 File Offset: 0x0009D0D3
		public bool EndOfPacket
		{
			get
			{
				return (this.m_Flags & UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.EndOfPacket) > UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.None;
			}
		}

		// Token: 0x04001E59 RID: 7769
		internal string m_FilePath;

		// Token: 0x04001E5A RID: 7770
		internal byte[] m_Buffer;

		// Token: 0x04001E5B RID: 7771
		internal int m_Offset;

		// Token: 0x04001E5C RID: 7772
		internal int m_Count;

		// Token: 0x04001E5D RID: 7773
		internal UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags m_Flags;
	}
}

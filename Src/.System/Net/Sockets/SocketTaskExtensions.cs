using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>This class contains extension methods to the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
	// Token: 0x02000384 RID: 900
	public static class SocketTaskExtensions
	{
		/// <summary>Performs an asynchronous operation on to accept an incoming connection attempt on the socket.</summary>
		/// <param name="socket">The socket that is listening for connections.</param>
		/// <returns>An asynchronous task that completes with a <see cref="T:System.Net.Sockets.Socket" /> to handle communication with the remote host.</returns>
		// Token: 0x06002179 RID: 8569 RVA: 0x000A0BCB File Offset: 0x0009EDCB
		public static Task<Socket> AcceptAsync(this Socket socket)
		{
			return socket.AcceptAsync(null);
		}

		/// <summary>Performs an asynchronous operation on to accept an incoming connection attempt on the socket.</summary>
		/// <param name="socket">The socket that is listening for incoming connections.</param>
		/// <param name="acceptSocket">The accepted <see cref="T:System.Net.Sockets.Socket" /> object. This value may be <see langword="null" />.</param>
		/// <returns>An asynchronous task that completes with a <see cref="T:System.Net.Sockets.Socket" /> to handle communication with the remote host.</returns>
		// Token: 0x0600217A RID: 8570 RVA: 0x000A0BD4 File Offset: 0x0009EDD4
		public static Task<Socket> AcceptAsync(this Socket socket, Socket acceptSocket)
		{
			return socket.AcceptAsync(acceptSocket);
		}

		/// <summary>Establishes a connection to a remote host.</summary>
		/// <param name="socket">The socket that is used for establishing a connection.</param>
		/// <param name="remoteEP">An EndPoint that represents the remote device.</param>
		/// <returns>An asynchronous Task.</returns>
		// Token: 0x0600217B RID: 8571 RVA: 0x000A0BDD File Offset: 0x0009EDDD
		public static Task ConnectAsync(this Socket socket, EndPoint remoteEP)
		{
			return socket.ConnectAsync(remoteEP);
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by an IP address and a port number.</summary>
		/// <param name="socket">The socket to perform the connect operation on.</param>
		/// <param name="address">The IP address of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		// Token: 0x0600217C RID: 8572 RVA: 0x000A0BE6 File Offset: 0x0009EDE6
		public static Task ConnectAsync(this Socket socket, IPAddress address, int port)
		{
			return socket.ConnectAsync(address, port);
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by an array of IP addresses and a port number.</summary>
		/// <param name="socket">The socket that the connect operation is performed on.</param>
		/// <param name="addresses">The IP addresses of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <returns>A task that represents the asynchronous connect operation.</returns>
		// Token: 0x0600217D RID: 8573 RVA: 0x000A0BF0 File Offset: 0x0009EDF0
		public static Task ConnectAsync(this Socket socket, IPAddress[] addresses, int port)
		{
			return socket.ConnectAsync(addresses, port);
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by a host name and a port number.</summary>
		/// <param name="socket">The socket to perform the connect operation on.</param>
		/// <param name="host">The name of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <returns>An asynchronous task.</returns>
		// Token: 0x0600217E RID: 8574 RVA: 0x000A0BFA File Offset: 0x0009EDFA
		public static Task ConnectAsync(this Socket socket, string host, int port)
		{
			return socket.ConnectAsync(host, port);
		}

		/// <summary>Receives data from a connected socket.</summary>
		/// <param name="socket">The socket to perform the receive operation on.</param>
		/// <param name="buffer">An array that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>A task that represents the asynchronous receive operation. The value of the <paramref name="TResult" /> parameter contains the number of bytes received.</returns>
		// Token: 0x0600217F RID: 8575 RVA: 0x000A0C04 File Offset: 0x0009EE04
		public static Task<int> ReceiveAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			return socket.ReceiveAsync(buffer, socketFlags, false);
		}

		/// <summary>Receives data from a connected socket.</summary>
		/// <param name="socket">The socket to perform the receive operation on.</param>
		/// <param name="buffers">An array that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>A task that represents the asynchronous receive operation. The value of the <paramref name="TResult" /> parameter contains the number of bytes received.</returns>
		// Token: 0x06002180 RID: 8576 RVA: 0x000A0C0F File Offset: 0x0009EE0F
		public static Task<int> ReceiveAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			return socket.ReceiveAsync(buffers, socketFlags);
		}

		/// <summary>Receives data from a specified network device.</summary>
		/// <param name="socket">The socket to perform the ReceiveFrom operation on.</param>
		/// <param name="buffer">An array of type Byte that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEndPoint">An EndPoint that represents the source of the data.</param>
		/// <returns>An asynchronous Task that completes with a SocketReceiveFromResult struct.</returns>
		// Token: 0x06002181 RID: 8577 RVA: 0x000A0C19 File Offset: 0x0009EE19
		public static Task<SocketReceiveFromResult> ReceiveFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			return socket.ReceiveFromAsync(buffer, socketFlags, remoteEndPoint);
		}

		/// <summary>Receives the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint and packet information.</summary>
		/// <param name="socket">The socket to perform the operation on.</param>
		/// <param name="buffer">An array that is the storage location for received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEndPoint">An <see cref="T:System.Net.EndPoint" />, that represents the remote server.</param>
		/// <returns>An asynchronous Task that completes with a <see cref="T:System.Net.Sockets.SocketReceiveMessageFromResult" /> struct.</returns>
		// Token: 0x06002182 RID: 8578 RVA: 0x000A0C24 File Offset: 0x0009EE24
		public static Task<SocketReceiveMessageFromResult> ReceiveMessageFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			return socket.ReceiveMessageFromAsync(buffer, socketFlags, remoteEndPoint);
		}

		/// <summary>Sends data to a connected socket.</summary>
		/// <param name="socket">The socket to perform the operation on.</param>
		/// <param name="buffer">An array of type Byte that contains the data to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>An asynchronous task that completes with number of bytes sent to the socket if the operation was successful. Otherwise, the task will complete with an invalid socket error.</returns>
		// Token: 0x06002183 RID: 8579 RVA: 0x000A0C2F File Offset: 0x0009EE2F
		public static Task<int> SendAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			return socket.SendAsync(buffer, socketFlags, false);
		}

		/// <summary>Sends data to a connected socket.</summary>
		/// <param name="socket">The socket to perform the operation on.</param>
		/// <param name="buffers">An array that contains the data to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>An asynchronous task that completes with number of bytes sent to the socket if the operation was successful. Otherwise, the task will complete with an invalid socket error.</returns>
		// Token: 0x06002184 RID: 8580 RVA: 0x000A0C3A File Offset: 0x0009EE3A
		public static Task<int> SendAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			return socket.SendAsync(buffers, socketFlags);
		}

		/// <summary>Sends data asynchronously to a specific remote host.</summary>
		/// <param name="socket">The socket to perform the operation on.</param>
		/// <param name="buffer">An array that contains the data to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote device.</param>
		/// <returns>An asynchronous task that completes with number of bytes sent if the operation was successful. Otherwise, the task will complete with an invalid socket error.</returns>
		// Token: 0x06002185 RID: 8581 RVA: 0x000A0C44 File Offset: 0x0009EE44
		public static Task<int> SendToAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEP)
		{
			return socket.SendToAsync(buffer, socketFlags, remoteEP);
		}
	}
}

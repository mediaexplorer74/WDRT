using System;

namespace System.Net.Sockets
{
	/// <summary>Presents UDP receive result information from a call to the <see cref="M:System.Net.Sockets.UdpClient.ReceiveAsync" /> method.</summary>
	// Token: 0x0200038A RID: 906
	public struct UdpReceiveResult : IEquatable<UdpReceiveResult>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpReceiveResult" /> class.</summary>
		/// <param name="buffer">A buffer for data to receive in the UDP packet.</param>
		/// <param name="remoteEndPoint">The remote endpoint of the UDP packet.</param>
		// Token: 0x060021FC RID: 8700 RVA: 0x000A2C24 File Offset: 0x000A0E24
		public UdpReceiveResult(byte[] buffer, IPEndPoint remoteEndPoint)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEndPoint");
			}
			this.m_buffer = buffer;
			this.m_remoteEndPoint = remoteEndPoint;
		}

		/// <summary>Gets a buffer with the data received in the UDP packet.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array with the data received in the UDP packet.</returns>
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x000A2C50 File Offset: 0x000A0E50
		public byte[] Buffer
		{
			get
			{
				return this.m_buffer;
			}
		}

		/// <summary>Gets the remote endpoint from which the UDP packet was received.</summary>
		/// <returns>The remote endpoint from which the UDP packet was received.</returns>
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x000A2C58 File Offset: 0x000A0E58
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.m_remoteEndPoint;
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		// Token: 0x060021FF RID: 8703 RVA: 0x000A2C60 File Offset: 0x000A0E60
		public override int GetHashCode()
		{
			if (this.m_buffer == null)
			{
				return 0;
			}
			return this.m_buffer.GetHashCode() ^ this.m_remoteEndPoint.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.Net.Sockets.UdpReceiveResult" /> and equals the value of the instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002200 RID: 8704 RVA: 0x000A2C83 File Offset: 0x000A0E83
		public override bool Equals(object obj)
		{
			return obj is UdpReceiveResult && this.Equals((UdpReceiveResult)obj);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="other">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="other" /> is an instance of <see cref="T:System.Net.Sockets.UdpReceiveResult" /> and equals the value of the instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002201 RID: 8705 RVA: 0x000A2C9B File Offset: 0x000A0E9B
		public bool Equals(UdpReceiveResult other)
		{
			return object.Equals(this.m_buffer, other.m_buffer) && object.Equals(this.m_remoteEndPoint, other.m_remoteEndPoint);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instances are equivalent.</summary>
		/// <param name="left">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002202 RID: 8706 RVA: 0x000A2CC3 File Offset: 0x000A0EC3
		public static bool operator ==(UdpReceiveResult left, UdpReceiveResult right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instances are not equal.</summary>
		/// <param name="left">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the left of the not equal operator.</param>
		/// <param name="right">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the right of the not equal operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are unequal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002203 RID: 8707 RVA: 0x000A2CCD File Offset: 0x000A0ECD
		public static bool operator !=(UdpReceiveResult left, UdpReceiveResult right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001F41 RID: 8001
		private byte[] m_buffer;

		// Token: 0x04001F42 RID: 8002
		private IPEndPoint m_remoteEndPoint;
	}
}

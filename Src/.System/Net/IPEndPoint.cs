using System;
using System.Globalization;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Represents a network endpoint as an IP address and a port number.</summary>
	// Token: 0x0200014C RID: 332
	[global::__DynamicallyInvokable]
	[Serializable]
	public class IPEndPoint : EndPoint
	{
		/// <summary>Gets the Internet Protocol (IP) address family.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />.</returns>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0003F28F File Offset: 0x0003D48F
		[global::__DynamicallyInvokable]
		public override AddressFamily AddressFamily
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Address.AddressFamily;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">The IP address of the Internet host.</param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.  
		/// -or-  
		/// <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.  
		/// -or-  
		/// <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF.</exception>
		// Token: 0x06000B8E RID: 2958 RVA: 0x0003F29C File Offset: 0x0003D49C
		[global::__DynamicallyInvokable]
		public IPEndPoint(long address, int port)
		{
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_Port = port;
			this.m_Address = new IPAddress(address);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" />.</param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.  
		/// -or-  
		/// <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.  
		/// -or-  
		/// <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF.</exception>
		// Token: 0x06000B8F RID: 2959 RVA: 0x0003F2CA File Offset: 0x0003D4CA
		[global::__DynamicallyInvokable]
		public IPEndPoint(IPAddress address, int port)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_Port = port;
			this.m_Address = address;
		}

		/// <summary>Gets or sets the IP address of the endpoint.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance containing the IP address of the endpoint.</returns>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0003F301 File Offset: 0x0003D501
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0003F309 File Offset: 0x0003D509
		[global::__DynamicallyInvokable]
		public IPAddress Address
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Address;
			}
			[global::__DynamicallyInvokable]
			set
			{
				this.m_Address = value;
			}
		}

		/// <summary>Gets or sets the port number of the endpoint.</summary>
		/// <returns>An integer value in the range <see cref="F:System.Net.IPEndPoint.MinPort" /> to <see cref="F:System.Net.IPEndPoint.MaxPort" /> indicating the port number of the endpoint.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value that was specified for a set operation is less than <see cref="F:System.Net.IPEndPoint.MinPort" /> or greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0003F312 File Offset: 0x0003D512
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x0003F31A File Offset: 0x0003D51A
		[global::__DynamicallyInvokable]
		public int Port
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Port;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (!ValidationHelper.ValidateTcpPort(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_Port = value;
			}
		}

		/// <summary>Returns the IP address and port number of the specified endpoint.</summary>
		/// <returns>A string containing the IP address and the port number of the specified endpoint (for example, 192.168.1.2:80).</returns>
		// Token: 0x06000B94 RID: 2964 RVA: 0x0003F338 File Offset: 0x0003D538
		[global::__DynamicallyInvokable]
		public override string ToString()
		{
			string text;
			if (this.m_Address.AddressFamily == AddressFamily.InterNetworkV6)
			{
				text = "[{0}]:{1}";
			}
			else
			{
				text = "{0}:{1}";
			}
			return string.Format(text, this.m_Address.ToString(), this.Port.ToString(NumberFormatInfo.InvariantInfo));
		}

		/// <summary>Serializes endpoint information into a <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <returns>A <see cref="T:System.Net.SocketAddress" /> instance containing the socket address for the endpoint.</returns>
		// Token: 0x06000B95 RID: 2965 RVA: 0x0003F386 File Offset: 0x0003D586
		[global::__DynamicallyInvokable]
		public override SocketAddress Serialize()
		{
			return new SocketAddress(this.Address, this.Port);
		}

		/// <summary>Creates an endpoint from a socket address.</summary>
		/// <param name="socketAddress">The <see cref="T:System.Net.SocketAddress" /> to use for the endpoint.</param>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> instance using the specified socket address.</returns>
		/// <exception cref="T:System.ArgumentException">The AddressFamily of <paramref name="socketAddress" /> is not equal to the AddressFamily of the current instance.  
		///  -or-  
		///  <paramref name="socketAddress" />.Size &lt; 8.</exception>
		// Token: 0x06000B96 RID: 2966 RVA: 0x0003F39C File Offset: 0x0003D59C
		[global::__DynamicallyInvokable]
		public override EndPoint Create(SocketAddress socketAddress)
		{
			if (socketAddress.Family != this.AddressFamily)
			{
				throw new ArgumentException(SR.GetString("net_InvalidAddressFamily", new object[]
				{
					socketAddress.Family.ToString(),
					base.GetType().FullName,
					this.AddressFamily.ToString()
				}), "socketAddress");
			}
			if (socketAddress.Size < 8)
			{
				throw new ArgumentException(SR.GetString("net_InvalidSocketAddressSize", new object[]
				{
					socketAddress.GetType().FullName,
					base.GetType().FullName
				}), "socketAddress");
			}
			return socketAddress.GetIPEndPoint();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <param name="comparand">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000B97 RID: 2967 RVA: 0x0003F454 File Offset: 0x0003D654
		[global::__DynamicallyInvokable]
		public override bool Equals(object comparand)
		{
			return comparand is IPEndPoint && ((IPEndPoint)comparand).m_Address.Equals(this.m_Address) && ((IPEndPoint)comparand).m_Port == this.m_Port;
		}

		/// <summary>Returns a hash value for a <see cref="T:System.Net.IPEndPoint" /> instance.</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06000B98 RID: 2968 RVA: 0x0003F48D File Offset: 0x0003D68D
		[global::__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_Address.GetHashCode() ^ this.m_Port;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0003F4A1 File Offset: 0x0003D6A1
		internal IPEndPoint Snapshot()
		{
			return new IPEndPoint(this.Address.Snapshot(), this.Port);
		}

		/// <summary>Specifies the minimum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port" /> property. This field is read-only.</summary>
		// Token: 0x040010FB RID: 4347
		[global::__DynamicallyInvokable]
		public const int MinPort = 0;

		/// <summary>Specifies the maximum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port" /> property. The MaxPort value is set to 0x0000FFFF. This field is read-only.</summary>
		// Token: 0x040010FC RID: 4348
		[global::__DynamicallyInvokable]
		public const int MaxPort = 65535;

		// Token: 0x040010FD RID: 4349
		private IPAddress m_Address;

		// Token: 0x040010FE RID: 4350
		private int m_Port;

		// Token: 0x040010FF RID: 4351
		internal const int AnyPort = 0;

		// Token: 0x04001100 RID: 4352
		internal static IPEndPoint Any = new IPEndPoint(IPAddress.Any, 0);

		// Token: 0x04001101 RID: 4353
		internal static IPEndPoint IPv6Any = new IPEndPoint(IPAddress.IPv6Any, 0);
	}
}

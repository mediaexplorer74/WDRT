using System;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Represents a network endpoint as a host name or a string representation of an IP address and a port number.</summary>
	// Token: 0x020000E0 RID: 224
	[global::__DynamicallyInvokable]
	public class DnsEndPoint : EndPoint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsEndPoint" /> class with the host name or string representation of an IP address and a port number.</summary>
		/// <param name="host">The host name or a string representation of the IP address.</param>
		/// <param name="port">The port number associated with the address, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="host" /> parameter contains an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is a <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.  
		/// -or-  
		/// <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x060007B1 RID: 1969 RVA: 0x0002B03F File Offset: 0x0002923F
		[global::__DynamicallyInvokable]
		public DnsEndPoint(string host, int port)
			: this(host, port, AddressFamily.Unspecified)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsEndPoint" /> class with the host name or string representation of an IP address, a port number, and an address family.</summary>
		/// <param name="host">The host name or a string representation of the IP address.</param>
		/// <param name="port">The port number associated with the address, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <param name="addressFamily">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="host" /> parameter contains an empty string.  
		///  -or-  
		///  <paramref name="addressFamily" /> is <see cref="F:System.Net.Sockets.AddressFamily.Unknown" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is a <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.  
		/// -or-  
		/// <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x060007B2 RID: 1970 RVA: 0x0002B04C File Offset: 0x0002924C
		[global::__DynamicallyInvokable]
		public DnsEndPoint(string host, int port, AddressFamily addressFamily)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (string.IsNullOrEmpty(host))
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "host" }));
			}
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (addressFamily != AddressFamily.InterNetwork && addressFamily != AddressFamily.InterNetworkV6 && addressFamily != AddressFamily.Unspecified)
			{
				throw new ArgumentException(SR.GetString("net_sockets_invalid_optionValue_all"), "addressFamily");
			}
			this.m_Host = host;
			this.m_Port = port;
			this.m_Family = addressFamily;
		}

		/// <summary>Compares two <see cref="T:System.Net.DnsEndPoint" /> objects.</summary>
		/// <param name="comparand">A <see cref="T:System.Net.DnsEndPoint" /> instance to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Net.DnsEndPoint" /> instances are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007B3 RID: 1971 RVA: 0x0002B0E0 File Offset: 0x000292E0
		[global::__DynamicallyInvokable]
		public override bool Equals(object comparand)
		{
			DnsEndPoint dnsEndPoint = comparand as DnsEndPoint;
			return dnsEndPoint != null && (this.m_Family == dnsEndPoint.m_Family && this.m_Port == dnsEndPoint.m_Port) && this.m_Host == dnsEndPoint.m_Host;
		}

		/// <summary>Returns a hash value for a <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>An integer hash value for the <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x060007B4 RID: 1972 RVA: 0x0002B128 File Offset: 0x00029328
		[global::__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.ToString());
		}

		/// <summary>Returns the host name or string representation of the IP address and port number of the <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>A string containing the address family, host name or IP address string, and the port number of the specified <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x060007B5 RID: 1973 RVA: 0x0002B13C File Offset: 0x0002933C
		[global::__DynamicallyInvokable]
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.m_Family.ToString(),
				"/",
				this.m_Host,
				":",
				this.m_Port.ToString()
			});
		}

		/// <summary>Gets the host name or string representation of the Internet Protocol (IP) address of the host.</summary>
		/// <returns>A host name or string representation of an IP address.</returns>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0002B18F File Offset: 0x0002938F
		[global::__DynamicallyInvokable]
		public string Host
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Host;
			}
		}

		/// <summary>Gets the Internet Protocol (IP) address family.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</returns>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0002B197 File Offset: 0x00029397
		[global::__DynamicallyInvokable]
		public override AddressFamily AddressFamily
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Family;
			}
		}

		/// <summary>Gets the port number of the <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>An integer value in the range 0 to 0xffff indicating the port number of the <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0002B19F File Offset: 0x0002939F
		[global::__DynamicallyInvokable]
		public int Port
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Port;
			}
		}

		// Token: 0x04000D27 RID: 3367
		private string m_Host;

		// Token: 0x04000D28 RID: 3368
		private int m_Port;

		// Token: 0x04000D29 RID: 3369
		private AddressFamily m_Family;
	}
}

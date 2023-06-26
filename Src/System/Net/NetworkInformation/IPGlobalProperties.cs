using System;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the network connectivity of the local computer.</summary>
	// Token: 0x020002A1 RID: 673
	[global::__DynamicallyInvokable]
	public abstract class IPGlobalProperties
	{
		/// <summary>Gets an object that provides information about the local computer's network connectivity and traffic statistics.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.IPGlobalProperties" /> object that contains information about the local computer.</returns>
		// Token: 0x06001905 RID: 6405 RVA: 0x0007DB1D File Offset: 0x0007BD1D
		[global::__DynamicallyInvokable]
		public static IPGlobalProperties GetIPGlobalProperties()
		{
			new NetworkInformationPermission(NetworkInformationAccess.Read).Demand();
			return new SystemIPGlobalProperties();
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0007DB2F File Offset: 0x0007BD2F
		internal static IPGlobalProperties InternalGetIPGlobalProperties()
		{
			return new SystemIPGlobalProperties();
		}

		/// <summary>Returns information about the Internet Protocol version 4 (IPv4) and IPv6 User Datagram Protocol (UDP) listeners on the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> array that contains objects that describe the UDP listeners, or an empty array if no UDP listeners are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetUdpTable" /> failed.</exception>
		// Token: 0x06001907 RID: 6407
		[global::__DynamicallyInvokable]
		public abstract IPEndPoint[] GetActiveUdpListeners();

		/// <summary>Returns endpoint information about the Internet Protocol version 4 (IPv4) and IPv6 Transmission Control Protocol (TCP) listeners on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.IPEndPoint" /> array that contains objects that describe the active TCP listeners, or an empty array, if no active TCP listeners are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function <see langword="GetTcpTable" /> failed.</exception>
		// Token: 0x06001908 RID: 6408
		[global::__DynamicallyInvokable]
		public abstract IPEndPoint[] GetActiveTcpListeners();

		/// <summary>Returns information about the Internet Protocol version 4 (IPv4) and IPv6 Transmission Control Protocol (TCP) connections on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpConnectionInformation" /> array that contains objects that describe the active TCP connections, or an empty array if no active TCP connections are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function <see langword="GetTcpTable" /> failed.</exception>
		// Token: 0x06001909 RID: 6409
		[global::__DynamicallyInvokable]
		public abstract TcpConnectionInformation[] GetActiveTcpConnections();

		/// <summary>Gets the Dynamic Host Configuration Protocol (DHCP) scope name.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's DHCP scope name.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed.</exception>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600190A RID: 6410
		[global::__DynamicallyInvokable]
		public abstract string DhcpScopeName
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the domain in which the local computer is registered.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's domain name. If the computer does not belong to a domain, returns <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed.</exception>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600190B RID: 6411
		[global::__DynamicallyInvokable]
		public abstract string DomainName
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the host name for the local computer.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's NetBIOS name.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed.</exception>
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600190C RID: 6412
		[global::__DynamicallyInvokable]
		public abstract string HostName
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that specifies whether the local computer is acting as a Windows Internet Name Service (WINS) proxy.</summary>
		/// <returns>
		///   <see langword="true" /> if the local computer is a WINS proxy; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed.</exception>
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600190D RID: 6413
		[global::__DynamicallyInvokable]
		public abstract bool IsWinsProxy
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the Network Basic Input/Output System (NetBIOS) node type of the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetBiosNodeType" /> value.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed.</exception>
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600190E RID: 6414
		[global::__DynamicallyInvokable]
		public abstract NetBiosNodeType NodeType
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Provides Transmission Control Protocol/Internet Protocol version 4 (TCP/IPv4) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpStatistics" /> object that provides TCP/IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetTcpStatistics" /> failed.</exception>
		// Token: 0x0600190F RID: 6415
		[global::__DynamicallyInvokable]
		public abstract TcpStatistics GetTcpIPv4Statistics();

		/// <summary>Provides Transmission Control Protocol/Internet Protocol version 6 (TCP/IPv6) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpStatistics" /> object that provides TCP/IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetTcpStatistics" /> failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6.</exception>
		// Token: 0x06001910 RID: 6416
		[global::__DynamicallyInvokable]
		public abstract TcpStatistics GetTcpIPv6Statistics();

		/// <summary>Provides User Datagram Protocol/Internet Protocol version 4 (UDP/IPv4) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.UdpStatistics" /> object that provides UDP/IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetUdpStatistics failed.</exception>
		// Token: 0x06001911 RID: 6417
		[global::__DynamicallyInvokable]
		public abstract UdpStatistics GetUdpIPv4Statistics();

		/// <summary>Provides User Datagram Protocol/Internet Protocol version 6 (UDP/IPv6) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.UdpStatistics" /> object that provides UDP/IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetUdpStatistics" /> failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6.</exception>
		// Token: 0x06001912 RID: 6418
		[global::__DynamicallyInvokable]
		public abstract UdpStatistics GetUdpIPv6Statistics();

		/// <summary>Provides Internet Control Message Protocol (ICMP) version 4 statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IcmpV4Statistics" /> object that provides ICMP version 4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function <see langword="GetIcmpStatistics" /> failed.</exception>
		// Token: 0x06001913 RID: 6419
		[global::__DynamicallyInvokable]
		public abstract IcmpV4Statistics GetIcmpV4Statistics();

		/// <summary>Provides Internet Control Message Protocol (ICMP) version 6 statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IcmpV6Statistics" /> object that provides ICMP version 6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function <see langword="GetIcmpStatisticsEx" /> failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer's operating system is not Windows XP or later.</exception>
		// Token: 0x06001914 RID: 6420
		[global::__DynamicallyInvokable]
		public abstract IcmpV6Statistics GetIcmpV6Statistics();

		/// <summary>Provides Internet Protocol version 4 (IPv4) statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> object that provides IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetIpStatistics" /> failed.</exception>
		// Token: 0x06001915 RID: 6421
		[global::__DynamicallyInvokable]
		public abstract IPGlobalStatistics GetIPv4GlobalStatistics();

		/// <summary>Provides Internet Protocol version 6 (IPv6) statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> object that provides IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetIpStatistics" /> failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6.</exception>
		// Token: 0x06001916 RID: 6422
		[global::__DynamicallyInvokable]
		public abstract IPGlobalStatistics GetIPv6GlobalStatistics();

		/// <summary>Retrieves the stable unicast IP address table on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> that contains a list of stable unicast IP addresses on the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the native <see langword="GetAdaptersAddresses" /> function failed.</exception>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented on the platform. This method uses the native <see langword="NotifyStableUnicastIpAddressTable" /> function that is supported on Windows Vista and later.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have necessary <see cref="F:System.Net.NetworkInformation.NetworkInformationAccess.Read" /> permission.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The call to the native <see langword="NotifyStableUnicastIpAddressTable" /> function failed.</exception>
		// Token: 0x06001917 RID: 6423 RVA: 0x0007DB36 File Offset: 0x0007BD36
		[global::__DynamicallyInvokable]
		public virtual UnicastIPAddressInformationCollection GetUnicastAddresses()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Begins an asynchronous request to retrieve the stable unicast IP address table on the local computer.</summary>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented on the platform. This method uses the native <see langword="NotifyStableUnicastIpAddressTable" /> function that is supported on Windows Vista and later.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The call to the native <see langword="NotifyStableUnicastIpAddressTable" /> function failed.</exception>
		// Token: 0x06001918 RID: 6424 RVA: 0x0007DB3D File Offset: 0x0007BD3D
		[global::__DynamicallyInvokable]
		public virtual IAsyncResult BeginGetUnicastAddresses(AsyncCallback callback, object state)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Ends a pending asynchronous request to retrieve the stable unicast IP address table on the local computer.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the native <see langword="GetAdaptersAddresses" /> function failed.</exception>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented on the platform. This method uses the native <see langword="NotifyStableUnicastIpAddressTable" /> function that is supported on Windows Vista and later.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have necessary <see cref="F:System.Net.NetworkInformation.NetworkInformationAccess.Read" /> permission.</exception>
		// Token: 0x06001919 RID: 6425 RVA: 0x0007DB44 File Offset: 0x0007BD44
		[global::__DynamicallyInvokable]
		public virtual UnicastIPAddressInformationCollection EndGetUnicastAddresses(IAsyncResult asyncResult)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Retrieves the stable unicast IP address table on the local computer as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the native <see langword="GetAdaptersAddresses" /> function failed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have necessary <see cref="F:System.Net.NetworkInformation.NetworkInformationAccess.Read" /> permission.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The call to the native <see langword="NotifyStableUnicastIpAddressTable" /> function failed.</exception>
		// Token: 0x0600191A RID: 6426 RVA: 0x0007DB4B File Offset: 0x0007BD4B
		[global::__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<UnicastIPAddressInformationCollection> GetUnicastAddressesAsync()
		{
			return Task<UnicastIPAddressInformationCollection>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetUnicastAddresses), new Func<IAsyncResult, UnicastIPAddressInformationCollection>(this.EndGetUnicastAddresses), null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPGlobalProperties" /> class.</summary>
		// Token: 0x0600191B RID: 6427 RVA: 0x0007DB72 File Offset: 0x0007BD72
		[global::__DynamicallyInvokable]
		protected IPGlobalProperties()
		{
		}
	}
}

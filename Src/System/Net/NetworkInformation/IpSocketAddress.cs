using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002BB RID: 699
	internal struct IpSocketAddress
	{
		// Token: 0x060019AC RID: 6572 RVA: 0x0007DED4 File Offset: 0x0007C0D4
		internal IPAddress MarshalIPAddress()
		{
			AddressFamily addressFamily = ((this.addressLength > 16) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork);
			SocketAddress socketAddress = new SocketAddress(addressFamily, this.addressLength);
			Marshal.Copy(this.address, socketAddress.m_Buffer, 0, this.addressLength);
			return socketAddress.GetIPAddress();
		}

		// Token: 0x04001936 RID: 6454
		internal IntPtr address;

		// Token: 0x04001937 RID: 6455
		internal int addressLength;
	}
}

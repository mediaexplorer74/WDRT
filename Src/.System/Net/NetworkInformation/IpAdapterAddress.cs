using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002BC RID: 700
	internal struct IpAdapterAddress
	{
		// Token: 0x060019AD RID: 6573 RVA: 0x0007DF1C File Offset: 0x0007C11C
		internal static IPAddressCollection MarshalIpAddressCollection(IntPtr ptr)
		{
			IPAddressCollection ipaddressCollection = new IPAddressCollection();
			while (ptr != IntPtr.Zero)
			{
				IpAdapterAddress ipAdapterAddress = (IpAdapterAddress)Marshal.PtrToStructure(ptr, typeof(IpAdapterAddress));
				IPAddress ipaddress = ipAdapterAddress.address.MarshalIPAddress();
				ipaddressCollection.InternalAdd(ipaddress);
				ptr = ipAdapterAddress.next;
			}
			return ipaddressCollection;
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0007DF74 File Offset: 0x0007C174
		internal static IPAddressInformationCollection MarshalIpAddressInformationCollection(IntPtr ptr)
		{
			IPAddressInformationCollection ipaddressInformationCollection = new IPAddressInformationCollection();
			while (ptr != IntPtr.Zero)
			{
				IpAdapterAddress ipAdapterAddress = (IpAdapterAddress)Marshal.PtrToStructure(ptr, typeof(IpAdapterAddress));
				IPAddress ipaddress = ipAdapterAddress.address.MarshalIPAddress();
				ipaddressInformationCollection.InternalAdd(new SystemIPAddressInformation(ipaddress, ipAdapterAddress.flags));
				ptr = ipAdapterAddress.next;
			}
			return ipaddressInformationCollection;
		}

		// Token: 0x04001938 RID: 6456
		internal uint length;

		// Token: 0x04001939 RID: 6457
		internal AdapterAddressFlags flags;

		// Token: 0x0400193A RID: 6458
		internal IntPtr next;

		// Token: 0x0400193B RID: 6459
		internal IpSocketAddress address;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C2 RID: 706
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct MibIfRow2
	{
		// Token: 0x0400197D RID: 6525
		private const int GuidLength = 16;

		// Token: 0x0400197E RID: 6526
		private const int IfMaxStringSize = 256;

		// Token: 0x0400197F RID: 6527
		private const int IfMaxPhysAddressLength = 32;

		// Token: 0x04001980 RID: 6528
		internal ulong interfaceLuid;

		// Token: 0x04001981 RID: 6529
		internal uint interfaceIndex;

		// Token: 0x04001982 RID: 6530
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] interfaceGuid;

		// Token: 0x04001983 RID: 6531
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 257)]
		internal char[] alias;

		// Token: 0x04001984 RID: 6532
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 257)]
		internal char[] description;

		// Token: 0x04001985 RID: 6533
		internal uint physicalAddressLength;

		// Token: 0x04001986 RID: 6534
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		internal byte[] physicalAddress;

		// Token: 0x04001987 RID: 6535
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		internal byte[] permanentPhysicalAddress;

		// Token: 0x04001988 RID: 6536
		internal uint mtu;

		// Token: 0x04001989 RID: 6537
		internal NetworkInterfaceType type;

		// Token: 0x0400198A RID: 6538
		internal InterfaceTunnelType tunnelType;

		// Token: 0x0400198B RID: 6539
		internal uint mediaType;

		// Token: 0x0400198C RID: 6540
		internal uint physicalMediumType;

		// Token: 0x0400198D RID: 6541
		internal uint accessType;

		// Token: 0x0400198E RID: 6542
		internal uint directionType;

		// Token: 0x0400198F RID: 6543
		internal byte interfaceAndOperStatusFlags;

		// Token: 0x04001990 RID: 6544
		internal OperationalStatus operStatus;

		// Token: 0x04001991 RID: 6545
		internal uint adminStatus;

		// Token: 0x04001992 RID: 6546
		internal uint mediaConnectState;

		// Token: 0x04001993 RID: 6547
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] networkGuid;

		// Token: 0x04001994 RID: 6548
		internal InterfaceConnectionType connectionType;

		// Token: 0x04001995 RID: 6549
		internal ulong transmitLinkSpeed;

		// Token: 0x04001996 RID: 6550
		internal ulong receiveLinkSpeed;

		// Token: 0x04001997 RID: 6551
		internal ulong inOctets;

		// Token: 0x04001998 RID: 6552
		internal ulong inUcastPkts;

		// Token: 0x04001999 RID: 6553
		internal ulong inNUcastPkts;

		// Token: 0x0400199A RID: 6554
		internal ulong inDiscards;

		// Token: 0x0400199B RID: 6555
		internal ulong inErrors;

		// Token: 0x0400199C RID: 6556
		internal ulong inUnknownProtos;

		// Token: 0x0400199D RID: 6557
		internal ulong inUcastOctets;

		// Token: 0x0400199E RID: 6558
		internal ulong inMulticastOctets;

		// Token: 0x0400199F RID: 6559
		internal ulong inBroadcastOctets;

		// Token: 0x040019A0 RID: 6560
		internal ulong outOctets;

		// Token: 0x040019A1 RID: 6561
		internal ulong outUcastPkts;

		// Token: 0x040019A2 RID: 6562
		internal ulong outNUcastPkts;

		// Token: 0x040019A3 RID: 6563
		internal ulong outDiscards;

		// Token: 0x040019A4 RID: 6564
		internal ulong outErrors;

		// Token: 0x040019A5 RID: 6565
		internal ulong outUcastOctets;

		// Token: 0x040019A6 RID: 6566
		internal ulong outMulticastOctets;

		// Token: 0x040019A7 RID: 6567
		internal ulong outBroadcastOctets;

		// Token: 0x040019A8 RID: 6568
		internal ulong outQLen;
	}
}

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000300 RID: 768
	internal class SystemNetworkInterface : NetworkInterface
	{
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0008160A File Offset: 0x0007F80A
		internal static int InternalLoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.GetBestInterfaceForAddress(IPAddress.Loopback);
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x00081616 File Offset: 0x0007F816
		internal static int InternalIPv6LoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.GetBestInterfaceForAddress(IPAddress.IPv6Loopback);
			}
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00081624 File Offset: 0x0007F824
		private static int GetBestInterfaceForAddress(IPAddress addr)
		{
			SocketAddress socketAddress = new SocketAddress(addr);
			int num;
			int bestInterfaceEx = (int)UnsafeNetInfoNativeMethods.GetBestInterfaceEx(socketAddress.m_Buffer, out num);
			if (bestInterfaceEx != 0)
			{
				throw new NetworkInformationException(bestInterfaceEx);
			}
			return num;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00081654 File Offset: 0x0007F854
		internal static bool InternalGetIsNetworkAvailable()
		{
			try
			{
				NetworkInterface[] networkInterfaces = SystemNetworkInterface.GetNetworkInterfaces();
				foreach (NetworkInterface networkInterface in networkInterfaces)
				{
					if (networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Tunnel && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
					{
						return true;
					}
				}
			}
			catch (NetworkInformationException ex)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, "SystemNetworkInterface", "InternalGetIsNetworkAvailable", ex);
				}
			}
			return false;
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x000816D8 File Offset: 0x0007F8D8
		internal static NetworkInterface[] GetNetworkInterfaces()
		{
			AddressFamily addressFamily = AddressFamily.Unspecified;
			uint num = 0U;
			SafeLocalFree safeLocalFree = null;
			FixedInfo fixedInfo = SystemIPGlobalProperties.GetFixedInfo();
			List<SystemNetworkInterface> list = new List<SystemNetworkInterface>();
			GetAdaptersAddressesFlags getAdaptersAddressesFlags = GetAdaptersAddressesFlags.IncludeWins | GetAdaptersAddressesFlags.IncludeGateways;
			uint num2 = UnsafeNetInfoNativeMethods.GetAdaptersAddresses(addressFamily, (uint)getAdaptersAddressesFlags, IntPtr.Zero, SafeLocalFree.Zero, ref num);
			while (num2 == 111U)
			{
				try
				{
					safeLocalFree = SafeLocalFree.LocalAlloc((int)num);
					num2 = UnsafeNetInfoNativeMethods.GetAdaptersAddresses(addressFamily, (uint)getAdaptersAddressesFlags, IntPtr.Zero, safeLocalFree, ref num);
					if (num2 == 0U)
					{
						IntPtr intPtr = safeLocalFree.DangerousGetHandle();
						while (intPtr != IntPtr.Zero)
						{
							IpAdapterAddresses ipAdapterAddresses = (IpAdapterAddresses)Marshal.PtrToStructure(intPtr, typeof(IpAdapterAddresses));
							list.Add(new SystemNetworkInterface(fixedInfo, ipAdapterAddresses));
							intPtr = ipAdapterAddresses.next;
						}
					}
				}
				finally
				{
					if (safeLocalFree != null)
					{
						safeLocalFree.Close();
					}
					safeLocalFree = null;
				}
			}
			if (num2 == 232U || num2 == 87U)
			{
				return new SystemNetworkInterface[0];
			}
			if (num2 != 0U)
			{
				throw new NetworkInformationException((int)num2);
			}
			return list.ToArray();
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x000817D4 File Offset: 0x0007F9D4
		internal SystemNetworkInterface(FixedInfo fixedInfo, IpAdapterAddresses ipAdapterAddresses)
		{
			this.id = ipAdapterAddresses.AdapterName;
			this.name = ipAdapterAddresses.friendlyName;
			this.description = ipAdapterAddresses.description;
			this.index = ipAdapterAddresses.index;
			this.physicalAddress = ipAdapterAddresses.address;
			this.addressLength = ipAdapterAddresses.addressLength;
			this.type = ipAdapterAddresses.type;
			this.operStatus = ipAdapterAddresses.operStatus;
			this.speed = (long)ipAdapterAddresses.receiveLinkSpeed;
			this.ipv6Index = ipAdapterAddresses.ipv6Index;
			this.adapterFlags = ipAdapterAddresses.flags;
			this.interfaceProperties = new SystemIPInterfaceProperties(fixedInfo, ipAdapterAddresses);
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x00081878 File Offset: 0x0007FA78
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x00081880 File Offset: 0x0007FA80
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x00081888 File Offset: 0x0007FA88
		public override string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00081890 File Offset: 0x0007FA90
		public override PhysicalAddress GetPhysicalAddress()
		{
			byte[] array = new byte[this.addressLength];
			Array.Copy(this.physicalAddress, array, (long)((ulong)this.addressLength));
			return new PhysicalAddress(array);
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x000818C2 File Offset: 0x0007FAC2
		public override NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000818CA File Offset: 0x0007FACA
		public override IPInterfaceProperties GetIPProperties()
		{
			return this.interfaceProperties;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000818D2 File Offset: 0x0007FAD2
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			return new SystemIPv4InterfaceStatistics((long)((ulong)this.index));
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x000818E0 File Offset: 0x0007FAE0
		public override IPInterfaceStatistics GetIPStatistics()
		{
			return new SystemIPInterfaceStatistics((long)((ulong)this.index));
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x000818EE File Offset: 0x0007FAEE
		public override bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			return (networkInterfaceComponent == NetworkInterfaceComponent.IPv6 && (this.adapterFlags & AdapterFlags.IPv6Enabled) != (AdapterFlags)0) || (networkInterfaceComponent == NetworkInterfaceComponent.IPv4 && (this.adapterFlags & AdapterFlags.IPv4Enabled) != (AdapterFlags)0);
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x00081918 File Offset: 0x0007FB18
		public override OperationalStatus OperationalStatus
		{
			get
			{
				return this.operStatus;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x00081920 File Offset: 0x0007FB20
		public override long Speed
		{
			get
			{
				return this.speed;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x00081928 File Offset: 0x0007FB28
		public override bool IsReceiveOnly
		{
			get
			{
				return (this.adapterFlags & AdapterFlags.ReceiveOnly) > (AdapterFlags)0;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x00081935 File Offset: 0x0007FB35
		public override bool SupportsMulticast
		{
			get
			{
				return (this.adapterFlags & AdapterFlags.NoMulticast) == (AdapterFlags)0;
			}
		}

		// Token: 0x04001AC4 RID: 6852
		private string name;

		// Token: 0x04001AC5 RID: 6853
		private string id;

		// Token: 0x04001AC6 RID: 6854
		private string description;

		// Token: 0x04001AC7 RID: 6855
		private byte[] physicalAddress;

		// Token: 0x04001AC8 RID: 6856
		private uint addressLength;

		// Token: 0x04001AC9 RID: 6857
		private NetworkInterfaceType type;

		// Token: 0x04001ACA RID: 6858
		private OperationalStatus operStatus;

		// Token: 0x04001ACB RID: 6859
		private long speed;

		// Token: 0x04001ACC RID: 6860
		private uint index;

		// Token: 0x04001ACD RID: 6861
		private uint ipv6Index;

		// Token: 0x04001ACE RID: 6862
		private AdapterFlags adapterFlags;

		// Token: 0x04001ACF RID: 6863
		private SystemIPInterfaceProperties interfaceProperties;
	}
}

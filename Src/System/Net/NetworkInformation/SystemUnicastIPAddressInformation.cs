using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FD RID: 765
	internal class SystemUnicastIPAddressInformation : UnicastIPAddressInformation
	{
		// Token: 0x06001B08 RID: 6920 RVA: 0x000812B8 File Offset: 0x0007F4B8
		internal SystemUnicastIPAddressInformation(IpAdapterUnicastAddress adapterAddress)
		{
			IPAddress ipaddress = adapterAddress.address.MarshalIPAddress();
			this.innerInfo = new SystemIPAddressInformation(ipaddress, adapterAddress.flags);
			this.prefixOrigin = adapterAddress.prefixOrigin;
			this.suffixOrigin = adapterAddress.suffixOrigin;
			this.dadState = adapterAddress.dadState;
			this.validLifetime = adapterAddress.validLifetime;
			this.preferredLifetime = adapterAddress.preferredLifetime;
			this.dhcpLeaseLifetime = (long)((ulong)adapterAddress.leaseLifetime);
			this.prefixLength = adapterAddress.prefixLength;
			if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
			{
				this.ipv4Mask = SystemUnicastIPAddressInformation.PrefixLengthToSubnetMask(this.prefixLength, ipaddress.AddressFamily);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0008135F File Offset: 0x0007F55F
		public override IPAddress Address
		{
			get
			{
				return this.innerInfo.Address;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0008136C File Offset: 0x0007F56C
		public override IPAddress IPv4Mask
		{
			get
			{
				if (this.Address.AddressFamily != AddressFamily.InterNetwork)
				{
					return IPAddress.Any;
				}
				return this.ipv4Mask;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x00081388 File Offset: 0x0007F588
		public override int PrefixLength
		{
			get
			{
				return (int)this.prefixLength;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00081390 File Offset: 0x0007F590
		public override bool IsTransient
		{
			get
			{
				return this.innerInfo.IsTransient;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0008139D File Offset: 0x0007F59D
		public override bool IsDnsEligible
		{
			get
			{
				return this.innerInfo.IsDnsEligible;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x000813AA File Offset: 0x0007F5AA
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return this.prefixOrigin;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000813B2 File Offset: 0x0007F5B2
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return this.suffixOrigin;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x000813BA File Offset: 0x0007F5BA
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return this.dadState;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x000813C2 File Offset: 0x0007F5C2
		public override long AddressValidLifetime
		{
			get
			{
				return (long)((ulong)this.validLifetime);
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x000813CB File Offset: 0x0007F5CB
		public override long AddressPreferredLifetime
		{
			get
			{
				return (long)((ulong)this.preferredLifetime);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x000813D4 File Offset: 0x0007F5D4
		public override long DhcpLeaseLifetime
		{
			get
			{
				return this.dhcpLeaseLifetime;
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000813DC File Offset: 0x0007F5DC
		internal static UnicastIPAddressInformationCollection MarshalUnicastIpAddressInformationCollection(IntPtr ptr)
		{
			UnicastIPAddressInformationCollection unicastIPAddressInformationCollection = new UnicastIPAddressInformationCollection();
			while (ptr != IntPtr.Zero)
			{
				IpAdapterUnicastAddress ipAdapterUnicastAddress = (IpAdapterUnicastAddress)Marshal.PtrToStructure(ptr, typeof(IpAdapterUnicastAddress));
				unicastIPAddressInformationCollection.InternalAdd(new SystemUnicastIPAddressInformation(ipAdapterUnicastAddress));
				ptr = ipAdapterUnicastAddress.next;
			}
			return unicastIPAddressInformationCollection;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0008142C File Offset: 0x0007F62C
		private static IPAddress PrefixLengthToSubnetMask(byte prefixLength, AddressFamily family)
		{
			byte[] array;
			if (family == AddressFamily.InterNetwork)
			{
				array = new byte[4];
			}
			else
			{
				array = new byte[16];
			}
			for (int i = 0; i < (int)prefixLength; i++)
			{
				byte[] array2 = array;
				int num = i / 8;
				array2[num] |= (byte)(128 >> i % 8);
			}
			return new IPAddress(array);
		}

		// Token: 0x04001AB1 RID: 6833
		private long dhcpLeaseLifetime;

		// Token: 0x04001AB2 RID: 6834
		private SystemIPAddressInformation innerInfo;

		// Token: 0x04001AB3 RID: 6835
		private IPAddress ipv4Mask;

		// Token: 0x04001AB4 RID: 6836
		private PrefixOrigin prefixOrigin;

		// Token: 0x04001AB5 RID: 6837
		private SuffixOrigin suffixOrigin;

		// Token: 0x04001AB6 RID: 6838
		private DuplicateAddressDetectionState dadState;

		// Token: 0x04001AB7 RID: 6839
		private uint validLifetime;

		// Token: 0x04001AB8 RID: 6840
		private uint preferredLifetime;

		// Token: 0x04001AB9 RID: 6841
		private byte prefixLength;
	}
}

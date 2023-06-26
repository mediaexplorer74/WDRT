using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FE RID: 766
	internal class SystemIPv4InterfaceProperties : IPv4InterfaceProperties
	{
		// Token: 0x06001B16 RID: 6934 RVA: 0x0008147C File Offset: 0x0007F67C
		internal SystemIPv4InterfaceProperties(FixedInfo fixedInfo, IpAdapterAddresses ipAdapterAddresses)
		{
			this.index = ipAdapterAddresses.index;
			this.routingEnabled = fixedInfo.EnableRouting;
			this.dhcpEnabled = (ipAdapterAddresses.flags & AdapterFlags.DhcpEnabled) > (AdapterFlags)0;
			this.haveWins = ipAdapterAddresses.firstWinsServerAddress != IntPtr.Zero;
			this.mtu = ipAdapterAddresses.mtu;
			this.GetPerAdapterInfo(ipAdapterAddresses.index);
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x000814E7 File Offset: 0x0007F6E7
		public override bool UsesWins
		{
			get
			{
				return this.haveWins;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x000814EF File Offset: 0x0007F6EF
		public override bool IsDhcpEnabled
		{
			get
			{
				return this.dhcpEnabled;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x000814F7 File Offset: 0x0007F6F7
		public override bool IsForwardingEnabled
		{
			get
			{
				return this.routingEnabled;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x000814FF File Offset: 0x0007F6FF
		public override bool IsAutomaticPrivateAddressingEnabled
		{
			get
			{
				return this.autoConfigEnabled;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x00081507 File Offset: 0x0007F707
		public override bool IsAutomaticPrivateAddressingActive
		{
			get
			{
				return this.autoConfigActive;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0008150F File Offset: 0x0007F70F
		public override int Mtu
		{
			get
			{
				return (int)this.mtu;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x00081517 File Offset: 0x0007F717
		public override int Index
		{
			get
			{
				return (int)this.index;
			}
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00081520 File Offset: 0x0007F720
		private void GetPerAdapterInfo(uint index)
		{
			if (index != 0U)
			{
				uint num = 0U;
				SafeLocalFree safeLocalFree = null;
				uint num2 = UnsafeNetInfoNativeMethods.GetPerAdapterInfo(index, SafeLocalFree.Zero, ref num);
				while (num2 == 111U)
				{
					try
					{
						safeLocalFree = SafeLocalFree.LocalAlloc((int)num);
						num2 = UnsafeNetInfoNativeMethods.GetPerAdapterInfo(index, safeLocalFree, ref num);
						if (num2 == 0U)
						{
							IpPerAdapterInfo ipPerAdapterInfo = (IpPerAdapterInfo)Marshal.PtrToStructure(safeLocalFree.DangerousGetHandle(), typeof(IpPerAdapterInfo));
							this.autoConfigEnabled = ipPerAdapterInfo.autoconfigEnabled;
							this.autoConfigActive = ipPerAdapterInfo.autoconfigActive;
						}
					}
					finally
					{
						if (safeLocalFree != null)
						{
							safeLocalFree.Close();
						}
					}
				}
				if (num2 != 0U)
				{
					throw new NetworkInformationException((int)num2);
				}
			}
		}

		// Token: 0x04001ABA RID: 6842
		private bool haveWins;

		// Token: 0x04001ABB RID: 6843
		private bool dhcpEnabled;

		// Token: 0x04001ABC RID: 6844
		private bool routingEnabled;

		// Token: 0x04001ABD RID: 6845
		private bool autoConfigEnabled;

		// Token: 0x04001ABE RID: 6846
		private bool autoConfigActive;

		// Token: 0x04001ABF RID: 6847
		private uint index;

		// Token: 0x04001AC0 RID: 6848
		private uint mtu;
	}
}

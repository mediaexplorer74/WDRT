using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FF RID: 767
	internal class SystemIPv6InterfaceProperties : IPv6InterfaceProperties
	{
		// Token: 0x06001B1F RID: 6943 RVA: 0x000815B8 File Offset: 0x0007F7B8
		internal SystemIPv6InterfaceProperties(uint index, uint mtu, uint[] zoneIndices)
		{
			this.index = index;
			this.mtu = mtu;
			this.zoneIndices = zoneIndices;
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x000815D5 File Offset: 0x0007F7D5
		public override int Index
		{
			get
			{
				return (int)this.index;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x000815DD File Offset: 0x0007F7DD
		public override int Mtu
		{
			get
			{
				return (int)this.mtu;
			}
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000815E5 File Offset: 0x0007F7E5
		public override long GetScopeId(ScopeLevel scopeLevel)
		{
			if (scopeLevel < ScopeLevel.None || scopeLevel >= (ScopeLevel)this.zoneIndices.Length)
			{
				throw new ArgumentOutOfRangeException("scopeLevel");
			}
			return (long)((ulong)this.zoneIndices[(int)scopeLevel]);
		}

		// Token: 0x04001AC1 RID: 6849
		private uint index;

		// Token: 0x04001AC2 RID: 6850
		private uint mtu;

		// Token: 0x04001AC3 RID: 6851
		private uint[] zoneIndices;
	}
}

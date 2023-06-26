using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000301 RID: 769
	internal class SystemTcpConnectionInformation : TcpConnectionInformation
	{
		// Token: 0x06001B36 RID: 6966 RVA: 0x00081944 File Offset: 0x0007FB44
		internal SystemTcpConnectionInformation(MibTcpRow row)
		{
			this.state = row.state;
			int num = ((int)row.localPort1 << 8) | (int)row.localPort2;
			int num2 = ((this.state == TcpState.Listen) ? 0 : (((int)row.remotePort1 << 8) | (int)row.remotePort2));
			this.localEndPoint = new IPEndPoint((long)((ulong)row.localAddr), num);
			this.remoteEndPoint = new IPEndPoint((long)((ulong)row.remoteAddr), num2);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000819B8 File Offset: 0x0007FBB8
		internal SystemTcpConnectionInformation(MibTcp6RowOwnerPid row)
		{
			this.state = row.state;
			int num = ((int)row.localPort1 << 8) | (int)row.localPort2;
			int num2 = ((this.state == TcpState.Listen) ? 0 : (((int)row.remotePort1 << 8) | (int)row.remotePort2));
			this.localEndPoint = new IPEndPoint(new IPAddress(row.localAddr, (long)((ulong)row.localScopeId)), num);
			this.remoteEndPoint = new IPEndPoint(new IPAddress(row.remoteAddr, (long)((ulong)row.remoteScopeId)), num2);
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x00081A3F File Offset: 0x0007FC3F
		public override TcpState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00081A47 File Offset: 0x0007FC47
		public override IPEndPoint LocalEndPoint
		{
			get
			{
				return this.localEndPoint;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x00081A4F File Offset: 0x0007FC4F
		public override IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
		}

		// Token: 0x04001AD0 RID: 6864
		private IPEndPoint localEndPoint;

		// Token: 0x04001AD1 RID: 6865
		private IPEndPoint remoteEndPoint;

		// Token: 0x04001AD2 RID: 6866
		private TcpState state;
	}
}

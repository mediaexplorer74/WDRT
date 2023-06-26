using System;

namespace System.Net.Sockets
{
	// Token: 0x0200039B RID: 923
	internal class MultipleSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x06002269 RID: 8809 RVA: 0x000A4213 File Offset: 0x000A2413
		public MultipleSocketMultipleConnectAsync(SocketType socketType, ProtocolType protocolType)
		{
			if (Socket.OSSupportsIPv4)
			{
				this.socket4 = new Socket(AddressFamily.InterNetwork, socketType, protocolType);
			}
			if (Socket.OSSupportsIPv6)
			{
				this.socket6 = new Socket(AddressFamily.InterNetworkV6, socketType, protocolType);
			}
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000A4248 File Offset: 0x000A2448
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			IPAddress ipaddress = null;
			attemptSocket = null;
			while (attemptSocket == null)
			{
				if (this.nextAddress >= this.addressList.Length)
				{
					return null;
				}
				ipaddress = this.addressList[this.nextAddress];
				this.nextAddress++;
				if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					attemptSocket = this.socket6;
				}
				else if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					attemptSocket = this.socket4;
				}
			}
			return ipaddress;
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000A42B4 File Offset: 0x000A24B4
		protected override void OnSucceed()
		{
			if (this.socket4 != null && !this.socket4.Connected)
			{
				this.socket4.Close();
			}
			if (this.socket6 != null && !this.socket6.Connected)
			{
				this.socket6.Close();
			}
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000A4301 File Offset: 0x000A2501
		protected override void OnFail(bool abortive)
		{
			if (this.socket4 != null)
			{
				this.socket4.Close();
			}
			if (this.socket6 != null)
			{
				this.socket6.Close();
			}
		}

		// Token: 0x04001F6B RID: 8043
		private Socket socket4;

		// Token: 0x04001F6C RID: 8044
		private Socket socket6;
	}
}

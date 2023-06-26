using System;

namespace System.Net.Sockets
{
	// Token: 0x0200039A RID: 922
	internal class SingleSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x06002265 RID: 8805 RVA: 0x000A4189 File Offset: 0x000A2389
		public SingleSocketMultipleConnectAsync(Socket socket, bool userSocket)
		{
			this.socket = socket;
			this.userSocket = userSocket;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000A41A0 File Offset: 0x000A23A0
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			attemptSocket = this.socket;
			while (this.nextAddress < this.addressList.Length)
			{
				IPAddress ipaddress = this.addressList[this.nextAddress];
				this.nextAddress++;
				if (this.socket.CanTryAddressFamily(ipaddress.AddressFamily))
				{
					return ipaddress;
				}
			}
			return null;
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000A41F9 File Offset: 0x000A23F9
		protected override void OnFail(bool abortive)
		{
			if (abortive || !this.userSocket)
			{
				this.socket.Close();
			}
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000A4211 File Offset: 0x000A2411
		protected override void OnSucceed()
		{
		}

		// Token: 0x04001F69 RID: 8041
		private Socket socket;

		// Token: 0x04001F6A RID: 8042
		private bool userSocket;
	}
}

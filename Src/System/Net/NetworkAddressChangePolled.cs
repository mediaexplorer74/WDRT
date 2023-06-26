using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace System.Net
{
	// Token: 0x02000118 RID: 280
	internal class NetworkAddressChangePolled : IDisposable
	{
		// Token: 0x06000B16 RID: 2838 RVA: 0x0003D1C4 File Offset: 0x0003B3C4
		internal NetworkAddressChangePolled()
		{
			Socket.InitializeSockets();
			if (Socket.OSSupportsIPv4)
			{
				int num = -1;
				this.ipv4Socket = SafeCloseSocketAndEvent.CreateWSASocketWithEvent(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP, true, false);
				UnsafeNclNativeMethods.OSSOCK.ioctlsocket(this.ipv4Socket, -2147195266, ref num);
			}
			if (Socket.OSSupportsIPv6)
			{
				int num = -1;
				this.ipv6Socket = SafeCloseSocketAndEvent.CreateWSASocketWithEvent(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.IP, true, false);
				UnsafeNclNativeMethods.OSSOCK.ioctlsocket(this.ipv6Socket, -2147195266, ref num);
			}
			this.Setup(StartIPOptions.Both);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0003D23C File Offset: 0x0003B43C
		private void Setup(StartIPOptions startIPOptions)
		{
			if (Socket.OSSupportsIPv4 && (startIPOptions & StartIPOptions.StartIPv4) != StartIPOptions.None)
			{
				int num;
				SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAIoctl_Blocking(this.ipv4Socket.DangerousGetHandle(), 671088663, null, 0, null, 0, out num, SafeNativeOverlapped.Zero, IntPtr.Zero);
				if (socketError != SocketError.Success)
				{
					NetworkInformationException ex = new NetworkInformationException();
					if ((long)ex.ErrorCode != 10035L)
					{
						this.Dispose();
						return;
					}
				}
				socketError = UnsafeNclNativeMethods.OSSOCK.WSAEventSelect(this.ipv4Socket, this.ipv4Socket.GetEventHandle().SafeWaitHandle, AsyncEventBits.FdAddressListChange);
				if (socketError != SocketError.Success)
				{
					this.Dispose();
					return;
				}
			}
			if (Socket.OSSupportsIPv6 && (startIPOptions & StartIPOptions.StartIPv6) != StartIPOptions.None)
			{
				int num;
				SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAIoctl_Blocking(this.ipv6Socket.DangerousGetHandle(), 671088663, null, 0, null, 0, out num, SafeNativeOverlapped.Zero, IntPtr.Zero);
				if (socketError != SocketError.Success)
				{
					NetworkInformationException ex2 = new NetworkInformationException();
					if ((long)ex2.ErrorCode != 10035L)
					{
						this.Dispose();
						return;
					}
				}
				socketError = UnsafeNclNativeMethods.OSSOCK.WSAEventSelect(this.ipv6Socket, this.ipv6Socket.GetEventHandle().SafeWaitHandle, AsyncEventBits.FdAddressListChange);
				if (socketError != SocketError.Success)
				{
					this.Dispose();
					return;
				}
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0003D344 File Offset: 0x0003B544
		internal bool CheckAndReset()
		{
			if (!this.disposed)
			{
				lock (this)
				{
					if (!this.disposed)
					{
						StartIPOptions startIPOptions = StartIPOptions.None;
						if (this.ipv4Socket != null && this.ipv4Socket.GetEventHandle().WaitOne(0, false))
						{
							startIPOptions |= StartIPOptions.StartIPv4;
						}
						if (this.ipv6Socket != null && this.ipv6Socket.GetEventHandle().WaitOne(0, false))
						{
							startIPOptions |= StartIPOptions.StartIPv6;
						}
						if (startIPOptions != StartIPOptions.None)
						{
							this.Setup(startIPOptions);
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0003D3DC File Offset: 0x0003B5DC
		public void Dispose()
		{
			if (!this.disposed)
			{
				lock (this)
				{
					if (!this.disposed)
					{
						if (this.ipv6Socket != null)
						{
							this.ipv6Socket.Close();
							this.ipv6Socket = null;
						}
						if (this.ipv4Socket != null)
						{
							this.ipv4Socket.Close();
							this.ipv6Socket = null;
						}
						this.disposed = true;
					}
				}
			}
		}

		// Token: 0x04000F61 RID: 3937
		private bool disposed;

		// Token: 0x04000F62 RID: 3938
		private SafeCloseSocketAndEvent ipv4Socket;

		// Token: 0x04000F63 RID: 3939
		private SafeCloseSocketAndEvent ipv6Socket;
	}
}

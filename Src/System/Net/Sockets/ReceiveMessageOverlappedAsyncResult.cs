using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x0200039E RID: 926
	internal class ReceiveMessageOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002283 RID: 8835 RVA: 0x000A47B2 File Offset: 0x000A29B2
		internal ReceiveMessageOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback)
			: base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x000A47BD File Offset: 0x000A29BD
		internal IntPtr GetSocketAddressSizePtr()
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, this.m_SocketAddress.GetAddressSizeOffset());
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x000A47DA File Offset: 0x000A29DA
		internal SocketAddress SocketAddress
		{
			get
			{
				return this.m_SocketAddress;
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000A47E4 File Offset: 0x000A29E4
		internal unsafe void SetUnmanagedStructures(byte[] buffer, int offset, int size, SocketAddress socketAddress, SocketFlags socketFlags)
		{
			this.m_MessageBuffer = new byte[ReceiveMessageOverlappedAsyncResult.s_WSAMsgSize];
			this.m_WSABufferArray = new byte[ReceiveMessageOverlappedAsyncResult.s_WSABufferSize];
			IPAddress ipaddress = ((socketAddress.Family == AddressFamily.InterNetworkV6) ? socketAddress.GetIPAddress() : null);
			bool flag = ((Socket)base.AsyncObject).AddressFamily == AddressFamily.InterNetwork || (ipaddress != null && ipaddress.IsIPv4MappedToIPv6);
			bool flag2 = ((Socket)base.AsyncObject).AddressFamily == AddressFamily.InterNetworkV6;
			if (flag)
			{
				this.m_ControlBuffer = new byte[ReceiveMessageOverlappedAsyncResult.s_ControlDataSize];
			}
			else if (flag2)
			{
				this.m_ControlBuffer = new byte[ReceiveMessageOverlappedAsyncResult.s_ControlDataIPv6Size];
			}
			object[] array = new object[(this.m_ControlBuffer != null) ? 5 : 4];
			array[0] = buffer;
			array[1] = this.m_MessageBuffer;
			array[2] = this.m_WSABufferArray;
			this.m_SocketAddress = socketAddress;
			this.m_SocketAddress.CopyAddressSizeIntoBuffer();
			array[3] = this.m_SocketAddress.m_Buffer;
			if (this.m_ControlBuffer != null)
			{
				array[4] = this.m_ControlBuffer;
			}
			base.SetUnmanagedStructures(array);
			this.m_WSABuffer = (WSABuffer*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSABufferArray, 0);
			this.m_WSABuffer->Length = size;
			this.m_WSABuffer->Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, offset);
			this.m_Message = (UnsafeNclNativeMethods.OSSOCK.WSAMsg*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(this.m_MessageBuffer, 0);
			this.m_Message->socketAddress = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, 0);
			this.m_Message->addressLength = (uint)this.m_SocketAddress.Size;
			this.m_Message->buffers = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSABufferArray, 0);
			this.m_Message->count = 1U;
			if (this.m_ControlBuffer != null)
			{
				this.m_Message->controlBuffer.Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_ControlBuffer, 0);
				this.m_Message->controlBuffer.Length = this.m_ControlBuffer.Length;
			}
			this.m_Message->flags = socketFlags;
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000A49D2 File Offset: 0x000A2BD2
		internal void SetUnmanagedStructures(byte[] buffer, int offset, int size, SocketAddress socketAddress, SocketFlags socketFlags, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffer, offset, size, socketAddress, socketFlags);
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x000A49EC File Offset: 0x000A2BEC
		private unsafe void InitIPPacketInformation()
		{
			IPAddress ipaddress = null;
			if (this.m_ControlBuffer.Length == ReceiveMessageOverlappedAsyncResult.s_ControlDataSize)
			{
				UnsafeNclNativeMethods.OSSOCK.ControlData controlData = (UnsafeNclNativeMethods.OSSOCK.ControlData)Marshal.PtrToStructure(this.m_Message->controlBuffer.Pointer, typeof(UnsafeNclNativeMethods.OSSOCK.ControlData));
				if (controlData.length != UIntPtr.Zero)
				{
					ipaddress = new IPAddress((long)((ulong)controlData.address));
				}
				this.m_IPPacketInformation = new IPPacketInformation((ipaddress != null) ? ipaddress : IPAddress.None, (int)controlData.index);
				return;
			}
			if (this.m_ControlBuffer.Length == ReceiveMessageOverlappedAsyncResult.s_ControlDataIPv6Size)
			{
				UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6 controlDataIPv = (UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6)Marshal.PtrToStructure(this.m_Message->controlBuffer.Pointer, typeof(UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6));
				if (controlDataIPv.length != UIntPtr.Zero)
				{
					ipaddress = new IPAddress(controlDataIPv.address);
				}
				this.m_IPPacketInformation = new IPPacketInformation((ipaddress != null) ? ipaddress : IPAddress.IPv6None, (int)controlDataIPv.index);
				return;
			}
			this.m_IPPacketInformation = default(IPPacketInformation);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000A4AE6 File Offset: 0x000A2CE6
		internal void SyncReleaseUnmanagedStructures()
		{
			this.InitIPPacketInformation();
			this.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000A4AF4 File Offset: 0x000A2CF4
		protected unsafe override void ForceReleaseUnmanagedStructures()
		{
			this.m_flags = this.m_Message->flags;
			base.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000A4B0D File Offset: 0x000A2D0D
		internal override object PostCompletion(int numBytes)
		{
			this.InitIPPacketInformation();
			if (base.ErrorCode == 0 && Logging.On)
			{
				this.LogBuffer(numBytes);
			}
			return numBytes;
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000A4B31 File Offset: 0x000A2D31
		private unsafe void LogBuffer(int size)
		{
			Logging.Dump(Logging.Sockets, base.AsyncObject, "PostCompletion", this.m_WSABuffer->Pointer, Math.Min(this.m_WSABuffer->Length, size));
		}

		// Token: 0x04001F74 RID: 8052
		private unsafe UnsafeNclNativeMethods.OSSOCK.WSAMsg* m_Message;

		// Token: 0x04001F75 RID: 8053
		internal SocketAddress SocketAddressOriginal;

		// Token: 0x04001F76 RID: 8054
		internal SocketAddress m_SocketAddress;

		// Token: 0x04001F77 RID: 8055
		private unsafe WSABuffer* m_WSABuffer;

		// Token: 0x04001F78 RID: 8056
		private byte[] m_WSABufferArray;

		// Token: 0x04001F79 RID: 8057
		private byte[] m_ControlBuffer;

		// Token: 0x04001F7A RID: 8058
		internal byte[] m_MessageBuffer;

		// Token: 0x04001F7B RID: 8059
		internal SocketFlags m_flags;

		// Token: 0x04001F7C RID: 8060
		private static readonly int s_ControlDataSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.ControlData));

		// Token: 0x04001F7D RID: 8061
		private static readonly int s_ControlDataIPv6Size = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6));

		// Token: 0x04001F7E RID: 8062
		private static readonly int s_WSABufferSize = Marshal.SizeOf(typeof(WSABuffer));

		// Token: 0x04001F7F RID: 8063
		private static readonly int s_WSAMsgSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.WSAMsg));

		// Token: 0x04001F80 RID: 8064
		internal IPPacketInformation m_IPPacketInformation;
	}
}

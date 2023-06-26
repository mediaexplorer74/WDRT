using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x0200038B RID: 907
	internal class AcceptOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002204 RID: 8708 RVA: 0x000A2CDA File Offset: 0x000A0EDA
		internal AcceptOverlappedAsyncResult(Socket listenSocket, object asyncState, AsyncCallback asyncCallback)
			: base(listenSocket, asyncState, asyncCallback)
		{
			this.m_ListenSocket = listenSocket;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000A2CEC File Offset: 0x000A0EEC
		internal override object PostCompletion(int numBytes)
		{
			SocketError socketError = (SocketError)base.ErrorCode;
			SocketAddress socketAddress = null;
			if (socketError == SocketError.Success)
			{
				this.m_LocalBytesTransferred = numBytes;
				if (Logging.On)
				{
					this.LogBuffer((long)numBytes);
				}
				socketAddress = this.m_ListenSocket.m_RightEndPoint.Serialize();
				try
				{
					IntPtr intPtr;
					int num;
					IntPtr intPtr2;
					this.m_ListenSocket.GetAcceptExSockaddrs(Marshal.UnsafeAddrOfPinnedArrayElement(this.m_Buffer, 0), this.m_Buffer.Length - this.m_AddressBufferLength * 2, this.m_AddressBufferLength, this.m_AddressBufferLength, out intPtr, out num, out intPtr2, out socketAddress.m_Size);
					Marshal.Copy(intPtr2, socketAddress.m_Buffer, 0, socketAddress.m_Size);
					IntPtr intPtr3 = this.m_ListenSocket.SafeHandle.DangerousGetHandle();
					socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_AcceptSocket.SafeHandle, SocketOptionLevel.Socket, SocketOptionName.UpdateAcceptContext, ref intPtr3, Marshal.SizeOf(intPtr3));
					if (socketError == SocketError.SocketError)
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.OperationAborted;
				}
				base.ErrorCode = (int)socketError;
			}
			if (socketError == SocketError.Success)
			{
				return this.m_ListenSocket.UpdateAcceptSocket(this.m_AcceptSocket, this.m_ListenSocket.m_RightEndPoint.Create(socketAddress), false);
			}
			return null;
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000A2E14 File Offset: 0x000A1014
		internal void SetUnmanagedStructures(byte[] buffer, int addressBufferLength)
		{
			base.SetUnmanagedStructures(buffer);
			this.m_AddressBufferLength = addressBufferLength;
			this.m_Buffer = buffer;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000A2E2C File Offset: 0x000A102C
		private void LogBuffer(long size)
		{
			IntPtr intPtr = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_Buffer, 0);
			if (intPtr != IntPtr.Zero)
			{
				if (size > -1L)
				{
					Logging.Dump(Logging.Sockets, this.m_ListenSocket, "PostCompletion", intPtr, (int)Math.Min(size, (long)this.m_Buffer.Length));
					return;
				}
				Logging.Dump(Logging.Sockets, this.m_ListenSocket, "PostCompletion", intPtr, this.m_Buffer.Length);
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x000A2E9D File Offset: 0x000A109D
		internal byte[] Buffer
		{
			get
			{
				return this.m_Buffer;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x000A2EA5 File Offset: 0x000A10A5
		internal int BytesTransferred
		{
			get
			{
				return this.m_LocalBytesTransferred;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (set) Token: 0x0600220A RID: 8714 RVA: 0x000A2EAD File Offset: 0x000A10AD
		internal Socket AcceptSocket
		{
			set
			{
				this.m_AcceptSocket = value;
			}
		}

		// Token: 0x04001F43 RID: 8003
		private int m_LocalBytesTransferred;

		// Token: 0x04001F44 RID: 8004
		private Socket m_ListenSocket;

		// Token: 0x04001F45 RID: 8005
		private Socket m_AcceptSocket;

		// Token: 0x04001F46 RID: 8006
		private int m_AddressBufferLength;

		// Token: 0x04001F47 RID: 8007
		private byte[] m_Buffer;
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x0200039C RID: 924
	internal class OverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x0600226D RID: 8813 RVA: 0x000A4329 File Offset: 0x000A2529
		internal OverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback)
			: base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000A4334 File Offset: 0x000A2534
		internal IntPtr GetSocketAddressPtr()
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, 0);
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000A4347 File Offset: 0x000A2547
		internal IntPtr GetSocketAddressSizePtr()
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, this.m_SocketAddress.GetAddressSizeOffset());
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000A4364 File Offset: 0x000A2564
		internal SocketAddress SocketAddress
		{
			get
			{
				return this.m_SocketAddress;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x000A436C File Offset: 0x000A256C
		// (set) Token: 0x06002272 RID: 8818 RVA: 0x000A4374 File Offset: 0x000A2574
		internal SocketAddress SocketAddressOriginal
		{
			get
			{
				return this.m_SocketAddressOriginal;
			}
			set
			{
				this.m_SocketAddressOriginal = value;
			}
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000A4380 File Offset: 0x000A2580
		internal void SetUnmanagedStructures(byte[] buffer, int offset, int size, SocketAddress socketAddress, bool pinSocketAddress)
		{
			this.m_SocketAddress = socketAddress;
			if (pinSocketAddress && this.m_SocketAddress != null)
			{
				object[] array = new object[2];
				array[0] = buffer;
				this.m_SocketAddress.CopyAddressSizeIntoBuffer();
				array[1] = this.m_SocketAddress.m_Buffer;
				base.SetUnmanagedStructures(array);
			}
			else
			{
				base.SetUnmanagedStructures(buffer);
			}
			this.m_SingleBuffer.Length = size;
			this.m_SingleBuffer.Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, offset);
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000A43F5 File Offset: 0x000A25F5
		internal void SetUnmanagedStructures(byte[] buffer, int offset, int size, SocketAddress socketAddress, bool pinSocketAddress, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffer, offset, size, socketAddress, pinSocketAddress);
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000A440C File Offset: 0x000A260C
		internal void SetUnmanagedStructures(BufferOffsetSize[] buffers)
		{
			this.m_WSABuffers = new WSABuffer[buffers.Length];
			object[] array = new object[buffers.Length];
			for (int i = 0; i < buffers.Length; i++)
			{
				array[i] = buffers[i].Buffer;
			}
			base.SetUnmanagedStructures(array);
			for (int j = 0; j < buffers.Length; j++)
			{
				this.m_WSABuffers[j].Length = buffers[j].Size;
				this.m_WSABuffers[j].Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(buffers[j].Buffer, buffers[j].Offset);
			}
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000A449D File Offset: 0x000A269D
		internal void SetUnmanagedStructures(BufferOffsetSize[] buffers, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffers);
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000A44B0 File Offset: 0x000A26B0
		internal void SetUnmanagedStructures(IList<ArraySegment<byte>> buffers)
		{
			int count = buffers.Count;
			ArraySegment<byte>[] array = new ArraySegment<byte>[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = buffers[i];
				ValidationHelper.ValidateSegment(array[i]);
			}
			this.m_WSABuffers = new WSABuffer[count];
			object[] array2 = new object[count];
			for (int j = 0; j < count; j++)
			{
				array2[j] = array[j].Array;
			}
			base.SetUnmanagedStructures(array2);
			for (int k = 0; k < count; k++)
			{
				this.m_WSABuffers[k].Length = array[k].Count;
				this.m_WSABuffers[k].Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(array[k].Array, array[k].Offset);
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000A458C File Offset: 0x000A278C
		internal void SetUnmanagedStructures(IList<ArraySegment<byte>> buffers, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffers);
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000A459C File Offset: 0x000A279C
		internal override object PostCompletion(int numBytes)
		{
			if (base.ErrorCode == 0 && Logging.On)
			{
				this.LogBuffer(numBytes);
			}
			return numBytes;
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000A45BC File Offset: 0x000A27BC
		private void LogBuffer(int size)
		{
			if (size > -1)
			{
				if (this.m_WSABuffers != null)
				{
					foreach (WSABuffer wsabuffer in this.m_WSABuffers)
					{
						Logging.Dump(Logging.Sockets, base.AsyncObject, "PostCompletion", wsabuffer.Pointer, Math.Min(wsabuffer.Length, size));
						if ((size -= wsabuffer.Length) <= 0)
						{
							return;
						}
					}
					return;
				}
				Logging.Dump(Logging.Sockets, base.AsyncObject, "PostCompletion", this.m_SingleBuffer.Pointer, Math.Min(this.m_SingleBuffer.Length, size));
			}
		}

		// Token: 0x04001F6D RID: 8045
		private SocketAddress m_SocketAddress;

		// Token: 0x04001F6E RID: 8046
		private SocketAddress m_SocketAddressOriginal;

		// Token: 0x04001F6F RID: 8047
		internal WSABuffer m_SingleBuffer;

		// Token: 0x04001F70 RID: 8048
		internal WSABuffer[] m_WSABuffers;
	}
}

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x0200039D RID: 925
	internal class TransmitFileOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x0600227B RID: 8827 RVA: 0x000A465C File Offset: 0x000A285C
		internal TransmitFileOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback)
			: base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000A4667 File Offset: 0x000A2867
		internal TransmitFileOverlappedAsyncResult(Socket socket)
			: base(socket)
		{
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000A4670 File Offset: 0x000A2870
		internal void SetUnmanagedStructures(byte[] preBuffer, byte[] postBuffer, FileStream fileStream, TransmitFileOptions flags, bool sync)
		{
			this.m_fileStream = fileStream;
			this.m_flags = flags;
			this.m_buffers = null;
			int num = 0;
			if (preBuffer != null && preBuffer.Length != 0)
			{
				num++;
			}
			if (postBuffer != null && postBuffer.Length != 0)
			{
				num++;
			}
			if (num != 0)
			{
				num++;
				object[] array = new object[num];
				this.m_buffers = new TransmitFileBuffers();
				array[--num] = this.m_buffers;
				if (preBuffer != null && preBuffer.Length != 0)
				{
					this.m_buffers.preBufferLength = preBuffer.Length;
					array[--num] = preBuffer;
				}
				if (postBuffer != null && postBuffer.Length != 0)
				{
					this.m_buffers.postBufferLength = postBuffer.Length;
					array[num - 1] = postBuffer;
				}
				if (sync)
				{
					base.PinUnmanagedObjects(array);
				}
				else
				{
					base.SetUnmanagedStructures(array);
				}
				if (preBuffer != null && preBuffer.Length != 0)
				{
					this.m_buffers.preBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(preBuffer, 0);
				}
				if (postBuffer != null && postBuffer.Length != 0)
				{
					this.m_buffers.postBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(postBuffer, 0);
					return;
				}
			}
			else if (!sync)
			{
				base.SetUnmanagedStructures(null);
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000A4762 File Offset: 0x000A2962
		internal void SetUnmanagedStructures(byte[] preBuffer, byte[] postBuffer, FileStream fileStream, TransmitFileOptions flags, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(preBuffer, postBuffer, fileStream, flags, false);
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000A4778 File Offset: 0x000A2978
		protected override void ForceReleaseUnmanagedStructures()
		{
			if (this.m_fileStream != null)
			{
				this.m_fileStream.Close();
				this.m_fileStream = null;
			}
			base.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x000A479A File Offset: 0x000A299A
		internal void SyncReleaseUnmanagedStructures()
		{
			this.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x000A47A2 File Offset: 0x000A29A2
		internal TransmitFileBuffers TransmitFileBuffers
		{
			get
			{
				return this.m_buffers;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x000A47AA File Offset: 0x000A29AA
		internal TransmitFileOptions Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x04001F71 RID: 8049
		private FileStream m_fileStream;

		// Token: 0x04001F72 RID: 8050
		private TransmitFileOptions m_flags;

		// Token: 0x04001F73 RID: 8051
		private TransmitFileBuffers m_buffers;
	}
}

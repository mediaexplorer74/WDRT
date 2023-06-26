using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	// Token: 0x0200017C RID: 380
	public class UploadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x06000DFF RID: 3583 RVA: 0x00049851 File Offset: 0x00047A51
		internal UploadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesSent, long totalBytesToSend, long bytesReceived, long totalBytesToReceive)
			: base(progressPercentage, userToken)
		{
			this.m_BytesReceived = bytesReceived;
			this.m_TotalBytesToReceive = totalBytesToReceive;
			this.m_BytesSent = bytesSent;
			this.m_TotalBytesToSend = totalBytesToSend;
		}

		/// <summary>Gets the number of bytes received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes received.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0004987A File Offset: 0x00047A7A
		public long BytesReceived
		{
			get
			{
				return this.m_BytesReceived;
			}
		}

		/// <summary>Gets the total number of bytes in a <see cref="T:System.Net.WebClient" /> data upload operation.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be received.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00049882 File Offset: 0x00047A82
		public long TotalBytesToReceive
		{
			get
			{
				return this.m_TotalBytesToReceive;
			}
		}

		/// <summary>Gets the number of bytes sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes sent.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0004988A File Offset: 0x00047A8A
		public long BytesSent
		{
			get
			{
				return this.m_BytesSent;
			}
		}

		/// <summary>Gets the total number of bytes to send.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be sent.</returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00049892 File Offset: 0x00047A92
		public long TotalBytesToSend
		{
			get
			{
				return this.m_TotalBytesToSend;
			}
		}

		// Token: 0x04001210 RID: 4624
		private long m_BytesReceived;

		// Token: 0x04001211 RID: 4625
		private long m_TotalBytesToReceive;

		// Token: 0x04001212 RID: 4626
		private long m_BytesSent;

		// Token: 0x04001213 RID: 4627
		private long m_TotalBytesToSend;
	}
}

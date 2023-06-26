using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	// Token: 0x0200017A RID: 378
	public class DownloadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x06000DF8 RID: 3576 RVA: 0x00049828 File Offset: 0x00047A28
		internal DownloadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesReceived, long totalBytesToReceive)
			: base(progressPercentage, userToken)
		{
			this.m_BytesReceived = bytesReceived;
			this.m_TotalBytesToReceive = totalBytesToReceive;
		}

		/// <summary>Gets the number of bytes received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes received.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00049841 File Offset: 0x00047A41
		public long BytesReceived
		{
			get
			{
				return this.m_BytesReceived;
			}
		}

		/// <summary>Gets the total number of bytes in a <see cref="T:System.Net.WebClient" /> data download operation.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be received.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00049849 File Offset: 0x00047A49
		public long TotalBytesToReceive
		{
			get
			{
				return this.m_TotalBytesToReceive;
			}
		}

		// Token: 0x0400120E RID: 4622
		private long m_BytesReceived;

		// Token: 0x0400120F RID: 4623
		private long m_TotalBytesToReceive;
	}
}

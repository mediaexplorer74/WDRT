using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadDataCompleted" /> event.</summary>
	// Token: 0x02000170 RID: 368
	public class DownloadDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DDA RID: 3546 RVA: 0x00049783 File Offset: 0x00047983
		internal DownloadDataCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets the data that is downloaded by a <see cref="Overload:System.Net.WebClient.DownloadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the downloaded data.</returns>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00049796 File Offset: 0x00047996
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04001209 RID: 4617
		private byte[] m_Result;
	}
}

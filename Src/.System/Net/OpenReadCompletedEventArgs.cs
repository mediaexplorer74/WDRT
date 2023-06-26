using System;
using System.ComponentModel;
using System.IO;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.OpenReadCompleted" /> event.</summary>
	// Token: 0x0200016A RID: 362
	public class OpenReadCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DC8 RID: 3528 RVA: 0x00049720 File Offset: 0x00047920
		internal OpenReadCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets a readable stream that contains data downloaded by a <see cref="Overload:System.Net.WebClient.DownloadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that contains the downloaded data.</returns>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00049733 File Offset: 0x00047933
		public Stream Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04001206 RID: 4614
		private Stream m_Result;
	}
}

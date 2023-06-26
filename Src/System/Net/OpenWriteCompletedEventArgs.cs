using System;
using System.ComponentModel;
using System.IO;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.OpenWriteCompleted" /> event.</summary>
	// Token: 0x0200016C RID: 364
	public class OpenWriteCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DCE RID: 3534 RVA: 0x00049741 File Offset: 0x00047941
		internal OpenWriteCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets a writable stream that is used to send data to a server.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> where you can write data to be uploaded.</returns>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00049754 File Offset: 0x00047954
		public Stream Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04001207 RID: 4615
		private Stream m_Result;
	}
}

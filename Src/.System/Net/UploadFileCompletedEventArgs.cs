using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadFileCompleted" /> event.</summary>
	// Token: 0x02000176 RID: 374
	public class UploadFileCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DEC RID: 3564 RVA: 0x000497E6 File Offset: 0x000479E6
		internal UploadFileCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets the server reply to a data upload operation that is started by calling an <see cref="Overload:System.Net.WebClient.UploadFileAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the server reply.</returns>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x000497F9 File Offset: 0x000479F9
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x0400120C RID: 4620
		private byte[] m_Result;
	}
}

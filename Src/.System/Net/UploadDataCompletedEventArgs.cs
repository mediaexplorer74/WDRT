using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadDataCompleted" /> event.</summary>
	// Token: 0x02000174 RID: 372
	public class UploadDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DE6 RID: 3558 RVA: 0x000497C5 File Offset: 0x000479C5
		internal UploadDataCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets the server reply to a data upload operation started by calling an <see cref="Overload:System.Net.WebClient.UploadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the server reply.</returns>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x000497D8 File Offset: 0x000479D8
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x0400120B RID: 4619
		private byte[] m_Result;
	}
}

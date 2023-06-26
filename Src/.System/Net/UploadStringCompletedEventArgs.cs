using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadStringCompleted" /> event.</summary>
	// Token: 0x02000172 RID: 370
	public class UploadStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x000497A4 File Offset: 0x000479A4
		internal UploadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets the server reply to a string upload operation that is started by calling an <see cref="Overload:System.Net.WebClient.UploadStringAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the server reply.</returns>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x000497B7 File Offset: 0x000479B7
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x0400120A RID: 4618
		private string m_Result;
	}
}

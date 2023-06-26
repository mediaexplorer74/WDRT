using System;
using System.ComponentModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</summary>
	// Token: 0x020002E9 RID: 745
	public class PingCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001A10 RID: 6672 RVA: 0x0007E910 File Offset: 0x0007CB10
		internal PingCompletedEventArgs(PingReply reply, Exception error, bool cancelled, object userToken)
			: base(error, cancelled, userToken)
		{
			this.reply = reply;
		}

		/// <summary>Gets an object that contains data that describes an attempt to send an Internet Control Message Protocol (ICMP) echo request message and receive a corresponding ICMP echo reply message.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that describes the results of the ICMP echo request.</returns>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0007E923 File Offset: 0x0007CB23
		public PingReply Reply
		{
			get
			{
				return this.reply;
			}
		}

		// Token: 0x04001A58 RID: 6744
		private PingReply reply;
	}
}

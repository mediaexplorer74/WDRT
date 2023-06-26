using System;
using System.ComponentModel;

namespace System.Net.Mail
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.Mail.SmtpClient.SendCompleted" /> event.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> containing event data.</param>
	// Token: 0x02000278 RID: 632
	// (Invoke) Token: 0x060017A9 RID: 6057
	public delegate void SendCompletedEventHandler(object sender, AsyncCompletedEventArgs e);
}

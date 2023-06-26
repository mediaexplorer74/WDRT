using System;

namespace System.Net.Mail
{
	/// <summary>Specifies the outcome of sending email by using the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
	// Token: 0x02000295 RID: 661
	public enum SmtpStatusCode
	{
		/// <summary>A system status or system Help reply.</summary>
		// Token: 0x04001859 RID: 6233
		SystemStatus = 211,
		/// <summary>A Help message was returned by the service.</summary>
		// Token: 0x0400185A RID: 6234
		HelpMessage = 214,
		/// <summary>The SMTP service is ready.</summary>
		// Token: 0x0400185B RID: 6235
		ServiceReady = 220,
		/// <summary>The SMTP service is closing the transmission channel.</summary>
		// Token: 0x0400185C RID: 6236
		ServiceClosingTransmissionChannel,
		/// <summary>The email was successfully sent to the SMTP service.</summary>
		// Token: 0x0400185D RID: 6237
		Ok = 250,
		/// <summary>The user mailbox is not located on the receiving server; the server forwards the email.</summary>
		// Token: 0x0400185E RID: 6238
		UserNotLocalWillForward,
		/// <summary>The specified user is not local, but the receiving SMTP service accepted the message and attempted to deliver it. This status code is defined in RFC 1123, which is available at https://www.ietf.org.</summary>
		// Token: 0x0400185F RID: 6239
		CannotVerifyUserWillAttemptDelivery,
		/// <summary>The SMTP service is ready to receive the email content.</summary>
		// Token: 0x04001860 RID: 6240
		StartMailInput = 354,
		/// <summary>The SMTP service is not available; the server is closing the transmission channel.</summary>
		// Token: 0x04001861 RID: 6241
		ServiceNotAvailable = 421,
		/// <summary>The destination mailbox is in use.</summary>
		// Token: 0x04001862 RID: 6242
		MailboxBusy = 450,
		/// <summary>The SMTP service cannot complete the request. This error can occur if the client's IP address cannot be resolved (that is, a reverse lookup failed). You can also receive this error if the client domain has been identified as an open relay or source for unsolicited email (spam). For details, see RFC 2505, which is available at https://www.ietf.org.</summary>
		// Token: 0x04001863 RID: 6243
		LocalErrorInProcessing,
		/// <summary>The SMTP service does not have sufficient storage to complete the request.</summary>
		// Token: 0x04001864 RID: 6244
		InsufficientStorage,
		/// <summary>The client was not authenticated or is not allowed to send mail using the specified SMTP host.</summary>
		// Token: 0x04001865 RID: 6245
		ClientNotPermitted = 454,
		/// <summary>The SMTP service does not recognize the specified command.</summary>
		// Token: 0x04001866 RID: 6246
		CommandUnrecognized = 500,
		/// <summary>The syntax used to specify a command or parameter is incorrect.</summary>
		// Token: 0x04001867 RID: 6247
		SyntaxError,
		/// <summary>The SMTP service does not implement the specified command.</summary>
		// Token: 0x04001868 RID: 6248
		CommandNotImplemented,
		/// <summary>The commands were sent in the incorrect sequence.</summary>
		// Token: 0x04001869 RID: 6249
		BadCommandSequence,
		/// <summary>The SMTP server is configured to accept only TLS connections, and the SMTP client is attempting to connect by using a non-TLS connection. The solution is for the user to set EnableSsl=true on the SMTP Client.</summary>
		// Token: 0x0400186A RID: 6250
		MustIssueStartTlsFirst = 530,
		/// <summary>The SMTP service does not implement the specified command parameter.</summary>
		// Token: 0x0400186B RID: 6251
		CommandParameterNotImplemented = 504,
		/// <summary>The destination mailbox was not found or could not be accessed.</summary>
		// Token: 0x0400186C RID: 6252
		MailboxUnavailable = 550,
		/// <summary>The user mailbox is not located on the receiving server. You should resend using the supplied address information.</summary>
		// Token: 0x0400186D RID: 6253
		UserNotLocalTryAlternatePath,
		/// <summary>The message is too large to be stored in the destination mailbox.</summary>
		// Token: 0x0400186E RID: 6254
		ExceededStorageAllocation,
		/// <summary>The syntax used to specify the destination mailbox is incorrect.</summary>
		// Token: 0x0400186F RID: 6255
		MailboxNameNotAllowed,
		/// <summary>The transaction failed.</summary>
		// Token: 0x04001870 RID: 6256
		TransactionFailed,
		/// <summary>The transaction could not occur. You receive this error when the specified SMTP host cannot be found.</summary>
		// Token: 0x04001871 RID: 6257
		GeneralFailure = -1
	}
}

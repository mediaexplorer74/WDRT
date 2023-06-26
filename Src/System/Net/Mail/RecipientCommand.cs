using System;

namespace System.Net.Mail
{
	// Token: 0x02000285 RID: 645
	internal static class RecipientCommand
	{
		// Token: 0x06001815 RID: 6165 RVA: 0x0007AC32 File Offset: 0x00078E32
		internal static IAsyncResult BeginSend(SmtpConnection conn, string to, AsyncCallback callback, object state)
		{
			RecipientCommand.PrepareCommand(conn, to);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0007AC44 File Offset: 0x00078E44
		private static bool CheckResponse(SmtpStatusCode statusCode, string response)
		{
			if (statusCode <= SmtpStatusCode.MailboxBusy)
			{
				if (statusCode - SmtpStatusCode.Ok <= 1)
				{
					return true;
				}
				if (statusCode != SmtpStatusCode.MailboxBusy)
				{
					goto IL_34;
				}
			}
			else if (statusCode != SmtpStatusCode.InsufficientStorage && statusCode - SmtpStatusCode.MailboxUnavailable > 3)
			{
				goto IL_34;
			}
			return false;
			IL_34:
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), response);
			}
			throw new SmtpException(statusCode, response, true);
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0007ACA8 File Offset: 0x00078EA8
		internal static bool EndSend(IAsyncResult result, out string response)
		{
			SmtpStatusCode smtpStatusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out response);
			return RecipientCommand.CheckResponse(smtpStatusCode, response);
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0007ACCC File Offset: 0x00078ECC
		private static void PrepareCommand(SmtpConnection conn, string to)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.Recipient);
			conn.BufferBuilder.Append(to, true);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0007AD20 File Offset: 0x00078F20
		internal static bool Send(SmtpConnection conn, string to, out string response)
		{
			RecipientCommand.PrepareCommand(conn, to);
			SmtpStatusCode smtpStatusCode = CheckCommand.Send(conn, out response);
			return RecipientCommand.CheckResponse(smtpStatusCode, response);
		}
	}
}

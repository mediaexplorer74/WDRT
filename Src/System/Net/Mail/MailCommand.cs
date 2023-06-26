using System;

namespace System.Net.Mail
{
	// Token: 0x02000284 RID: 644
	internal static class MailCommand
	{
		// Token: 0x06001810 RID: 6160 RVA: 0x0007AB27 File Offset: 0x00078D27
		internal static IAsyncResult BeginSend(SmtpConnection conn, byte[] command, MailAddress from, bool allowUnicode, AsyncCallback callback, object state)
		{
			MailCommand.PrepareCommand(conn, command, from, allowUnicode);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0007AB3C File Offset: 0x00078D3C
		private static void CheckResponse(SmtpStatusCode statusCode, string response)
		{
			if (statusCode == SmtpStatusCode.Ok)
			{
				return;
			}
			if (statusCode - SmtpStatusCode.LocalErrorInProcessing > 1 && statusCode != SmtpStatusCode.ExceededStorageAllocation)
			{
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), response);
			}
			throw new SmtpException(statusCode, response, true);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0007AB7C File Offset: 0x00078D7C
		internal static void EndSend(IAsyncResult result)
		{
			string text;
			SmtpStatusCode smtpStatusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out text);
			MailCommand.CheckResponse(smtpStatusCode, text);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0007ABA0 File Offset: 0x00078DA0
		private static void PrepareCommand(SmtpConnection conn, byte[] command, MailAddress from, bool allowUnicode)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(command);
			string smtpAddress = from.GetSmtpAddress(allowUnicode);
			conn.BufferBuilder.Append(smtpAddress, allowUnicode);
			if (allowUnicode)
			{
				conn.BufferBuilder.Append(" BODY=8BITMIME SMTPUTF8");
			}
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0007AC0C File Offset: 0x00078E0C
		internal static void Send(SmtpConnection conn, byte[] command, MailAddress from, bool allowUnicode)
		{
			MailCommand.PrepareCommand(conn, command, from, allowUnicode);
			string text;
			SmtpStatusCode smtpStatusCode = CheckCommand.Send(conn, out text);
			MailCommand.CheckResponse(smtpStatusCode, text);
		}
	}
}

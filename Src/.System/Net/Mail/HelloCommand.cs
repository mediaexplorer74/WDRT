using System;

namespace System.Net.Mail
{
	// Token: 0x02000282 RID: 642
	internal static class HelloCommand
	{
		// Token: 0x06001806 RID: 6150 RVA: 0x0007A985 File Offset: 0x00078B85
		internal static IAsyncResult BeginSend(SmtpConnection conn, string domain, AsyncCallback callback, object state)
		{
			HelloCommand.PrepareCommand(conn, domain);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0007A996 File Offset: 0x00078B96
		private static void CheckResponse(SmtpStatusCode statusCode, string serverResponse)
		{
			if (statusCode == SmtpStatusCode.Ok)
			{
				return;
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), serverResponse);
			}
			throw new SmtpException(statusCode, serverResponse, true);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0007A9C4 File Offset: 0x00078BC4
		internal static void EndSend(IAsyncResult result)
		{
			string text;
			SmtpStatusCode smtpStatusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out text);
			HelloCommand.CheckResponse(smtpStatusCode, text);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0007A9E8 File Offset: 0x00078BE8
		private static void PrepareCommand(SmtpConnection conn, string domain)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.Hello);
			conn.BufferBuilder.Append(domain);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0007AA3C File Offset: 0x00078C3C
		internal static void Send(SmtpConnection conn, string domain)
		{
			HelloCommand.PrepareCommand(conn, domain);
			string text;
			SmtpStatusCode smtpStatusCode = CheckCommand.Send(conn, out text);
			HelloCommand.CheckResponse(smtpStatusCode, text);
		}
	}
}

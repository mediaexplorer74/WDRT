using System;

namespace System.Net.Mail
{
	// Token: 0x02000283 RID: 643
	internal static class StartTlsCommand
	{
		// Token: 0x0600180B RID: 6155 RVA: 0x0007AA60 File Offset: 0x00078C60
		internal static IAsyncResult BeginSend(SmtpConnection conn, AsyncCallback callback, object state)
		{
			StartTlsCommand.PrepareCommand(conn);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0007AA70 File Offset: 0x00078C70
		private static void CheckResponse(SmtpStatusCode statusCode, string response)
		{
			if (statusCode == SmtpStatusCode.ServiceReady)
			{
				return;
			}
			if (statusCode != SmtpStatusCode.ClientNotPermitted)
			{
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), response);
			}
			throw new SmtpException(statusCode, response, true);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0007AAA8 File Offset: 0x00078CA8
		internal static void EndSend(IAsyncResult result)
		{
			string text;
			SmtpStatusCode smtpStatusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out text);
			StartTlsCommand.CheckResponse(smtpStatusCode, text);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0007AACA File Offset: 0x00078CCA
		private static void PrepareCommand(SmtpConnection conn)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.StartTls);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0007AB04 File Offset: 0x00078D04
		internal static void Send(SmtpConnection conn)
		{
			StartTlsCommand.PrepareCommand(conn);
			string text;
			SmtpStatusCode smtpStatusCode = CheckCommand.Send(conn, out text);
			StartTlsCommand.CheckResponse(smtpStatusCode, text);
		}
	}
}

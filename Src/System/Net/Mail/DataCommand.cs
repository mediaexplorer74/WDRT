using System;

namespace System.Net.Mail
{
	// Token: 0x0200027F RID: 639
	internal static class DataCommand
	{
		// Token: 0x060017F9 RID: 6137 RVA: 0x0007A6E5 File Offset: 0x000788E5
		internal static IAsyncResult BeginSend(SmtpConnection conn, AsyncCallback callback, object state)
		{
			DataCommand.PrepareCommand(conn);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0007A6F5 File Offset: 0x000788F5
		private static void CheckResponse(SmtpStatusCode statusCode, string serverResponse)
		{
			if (statusCode == SmtpStatusCode.StartMailInput)
			{
				return;
			}
			if (statusCode != SmtpStatusCode.LocalErrorInProcessing && statusCode != SmtpStatusCode.TransactionFailed)
			{
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), serverResponse);
			}
			throw new SmtpException(statusCode, serverResponse, true);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0007A734 File Offset: 0x00078934
		internal static void EndSend(IAsyncResult result)
		{
			string text;
			SmtpStatusCode smtpStatusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out text);
			DataCommand.CheckResponse(smtpStatusCode, text);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0007A756 File Offset: 0x00078956
		private static void PrepareCommand(SmtpConnection conn)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.Data);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0007A780 File Offset: 0x00078980
		internal static void Send(SmtpConnection conn)
		{
			DataCommand.PrepareCommand(conn);
			string text;
			SmtpStatusCode smtpStatusCode = CheckCommand.Send(conn, out text);
			DataCommand.CheckResponse(smtpStatusCode, text);
		}
	}
}

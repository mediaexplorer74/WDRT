using System;

namespace System.Net.Mail
{
	// Token: 0x02000280 RID: 640
	internal static class DataStopCommand
	{
		// Token: 0x060017FE RID: 6142 RVA: 0x0007A7A4 File Offset: 0x000789A4
		private static void CheckResponse(SmtpStatusCode statusCode, string serverResponse)
		{
			if (statusCode <= SmtpStatusCode.InsufficientStorage)
			{
				if (statusCode == SmtpStatusCode.Ok)
				{
					return;
				}
				if (statusCode - SmtpStatusCode.LocalErrorInProcessing > 1)
				{
				}
			}
			else if (statusCode != SmtpStatusCode.ExceededStorageAllocation && statusCode != SmtpStatusCode.TransactionFailed)
			{
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), serverResponse);
			}
			throw new SmtpException(statusCode, serverResponse, true);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0007A801 File Offset: 0x00078A01
		private static void PrepareCommand(SmtpConnection conn)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.DataStop);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0007A82C File Offset: 0x00078A2C
		internal static void Send(SmtpConnection conn)
		{
			DataStopCommand.PrepareCommand(conn);
			string text;
			SmtpStatusCode smtpStatusCode = CheckCommand.Send(conn, out text);
			DataStopCommand.CheckResponse(smtpStatusCode, text);
		}
	}
}

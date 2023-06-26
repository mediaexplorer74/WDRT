using System;

namespace System.Net.Mail
{
	// Token: 0x02000281 RID: 641
	internal static class EHelloCommand
	{
		// Token: 0x06001801 RID: 6145 RVA: 0x0007A84F File Offset: 0x00078A4F
		internal static IAsyncResult BeginSend(SmtpConnection conn, string domain, AsyncCallback callback, object state)
		{
			EHelloCommand.PrepareCommand(conn, domain);
			return ReadLinesCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0007A860 File Offset: 0x00078A60
		private static string[] CheckResponse(LineInfo[] lines)
		{
			if (lines == null || lines.Length == 0)
			{
				throw new SmtpException(SR.GetString("SmtpEhloResponseInvalid"));
			}
			if (lines[0].StatusCode == SmtpStatusCode.Ok)
			{
				string[] array = new string[lines.Length - 1];
				for (int i = 1; i < lines.Length; i++)
				{
					array[i - 1] = lines[i].Line;
				}
				return array;
			}
			if (lines[0].StatusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), lines[0].Line);
			}
			throw new SmtpException(lines[0].StatusCode, lines[0].Line, true);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0007A910 File Offset: 0x00078B10
		internal static string[] EndSend(IAsyncResult result)
		{
			return EHelloCommand.CheckResponse(ReadLinesCommand.EndSend(result));
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0007A920 File Offset: 0x00078B20
		private static void PrepareCommand(SmtpConnection conn, string domain)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.EHello);
			conn.BufferBuilder.Append(domain);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0007A971 File Offset: 0x00078B71
		internal static string[] Send(SmtpConnection conn, string domain)
		{
			EHelloCommand.PrepareCommand(conn, domain);
			return EHelloCommand.CheckResponse(ReadLinesCommand.Send(conn));
		}
	}
}

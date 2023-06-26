using System;

namespace System.Net.Mail
{
	// Token: 0x0200027E RID: 638
	internal static class AuthCommand
	{
		// Token: 0x060017F1 RID: 6129 RVA: 0x0007A5F8 File Offset: 0x000787F8
		internal static IAsyncResult BeginSend(SmtpConnection conn, string type, string message, AsyncCallback callback, object state)
		{
			AuthCommand.PrepareCommand(conn, type, message);
			return ReadLinesCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0007A60B File Offset: 0x0007880B
		internal static IAsyncResult BeginSend(SmtpConnection conn, string message, AsyncCallback callback, object state)
		{
			AuthCommand.PrepareCommand(conn, message);
			return ReadLinesCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0007A61C File Offset: 0x0007881C
		private static LineInfo CheckResponse(LineInfo[] lines)
		{
			if (lines == null || lines.Length == 0)
			{
				throw new SmtpException(SR.GetString("SmtpAuthResponseInvalid"));
			}
			return lines[0];
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0007A63C File Offset: 0x0007883C
		internal static LineInfo EndSend(IAsyncResult result)
		{
			return AuthCommand.CheckResponse(ReadLinesCommand.EndSend(result));
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0007A64C File Offset: 0x0007884C
		private static void PrepareCommand(SmtpConnection conn, string type, string message)
		{
			conn.BufferBuilder.Append(SmtpCommands.Auth);
			conn.BufferBuilder.Append(type);
			conn.BufferBuilder.Append(32);
			conn.BufferBuilder.Append(message);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0007A69E File Offset: 0x0007889E
		private static void PrepareCommand(SmtpConnection conn, string message)
		{
			conn.BufferBuilder.Append(message);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0007A6BC File Offset: 0x000788BC
		internal static LineInfo Send(SmtpConnection conn, string type, string message)
		{
			AuthCommand.PrepareCommand(conn, type, message);
			return AuthCommand.CheckResponse(ReadLinesCommand.Send(conn));
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0007A6D1 File Offset: 0x000788D1
		internal static LineInfo Send(SmtpConnection conn, string message)
		{
			AuthCommand.PrepareCommand(conn, message);
			return AuthCommand.CheckResponse(ReadLinesCommand.Send(conn));
		}
	}
}

using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x0200027D RID: 637
	internal static class ReadLinesCommand
	{
		// Token: 0x060017EB RID: 6123 RVA: 0x0007A424 File Offset: 0x00078624
		internal static IAsyncResult BeginSend(SmtpConnection conn, AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(conn, callback, state);
			multiAsyncResult.Enter();
			IAsyncResult asyncResult = conn.BeginFlush(ReadLinesCommand.onWrite, multiAsyncResult);
			if (asyncResult.CompletedSynchronously)
			{
				conn.EndFlush(asyncResult);
				multiAsyncResult.Leave();
			}
			SmtpReplyReader nextReplyReader = conn.Reader.GetNextReplyReader();
			multiAsyncResult.Enter();
			IAsyncResult asyncResult2 = nextReplyReader.BeginReadLines(ReadLinesCommand.onReadLines, multiAsyncResult);
			if (asyncResult2.CompletedSynchronously)
			{
				LineInfo[] array = conn.Reader.CurrentReader.EndReadLines(asyncResult2);
				if (!(multiAsyncResult.Result is Exception))
				{
					multiAsyncResult.Result = array;
				}
				multiAsyncResult.Leave();
			}
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0007A4C0 File Offset: 0x000786C0
		internal static LineInfo[] EndSend(IAsyncResult result)
		{
			object obj = MultiAsyncResult.End(result);
			if (obj is Exception)
			{
				throw (Exception)obj;
			}
			return (LineInfo[])obj;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0007A4EC File Offset: 0x000786EC
		private static void OnReadLines(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				try
				{
					SmtpConnection smtpConnection = (SmtpConnection)multiAsyncResult.Context;
					LineInfo[] array = smtpConnection.Reader.CurrentReader.EndReadLines(result);
					if (!(multiAsyncResult.Result is Exception))
					{
						multiAsyncResult.Result = array;
					}
					multiAsyncResult.Leave();
				}
				catch (Exception ex)
				{
					multiAsyncResult.Leave(ex);
				}
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0007A564 File Offset: 0x00078764
		private static void OnWrite(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				try
				{
					SmtpConnection smtpConnection = (SmtpConnection)multiAsyncResult.Context;
					smtpConnection.EndFlush(result);
					multiAsyncResult.Leave();
				}
				catch (Exception ex)
				{
					multiAsyncResult.Leave(ex);
				}
			}
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0007A5BC File Offset: 0x000787BC
		internal static LineInfo[] Send(SmtpConnection conn)
		{
			conn.Flush();
			return conn.Reader.GetNextReplyReader().ReadLines();
		}

		// Token: 0x0400180D RID: 6157
		private static AsyncCallback onReadLines = new AsyncCallback(ReadLinesCommand.OnReadLines);

		// Token: 0x0400180E RID: 6158
		private static AsyncCallback onWrite = new AsyncCallback(ReadLinesCommand.OnWrite);
	}
}

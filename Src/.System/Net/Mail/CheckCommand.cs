using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x0200027C RID: 636
	internal static class CheckCommand
	{
		// Token: 0x060017E5 RID: 6117 RVA: 0x0007A218 File Offset: 0x00078418
		internal static IAsyncResult BeginSend(SmtpConnection conn, AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(conn, callback, state);
			multiAsyncResult.Enter();
			IAsyncResult asyncResult = conn.BeginFlush(CheckCommand.onWrite, multiAsyncResult);
			if (asyncResult.CompletedSynchronously)
			{
				conn.EndFlush(asyncResult);
				multiAsyncResult.Leave();
			}
			SmtpReplyReader nextReplyReader = conn.Reader.GetNextReplyReader();
			multiAsyncResult.Enter();
			IAsyncResult asyncResult2 = nextReplyReader.BeginReadLine(CheckCommand.onReadLine, multiAsyncResult);
			if (asyncResult2.CompletedSynchronously)
			{
				LineInfo lineInfo = nextReplyReader.EndReadLine(asyncResult2);
				if (!(multiAsyncResult.Result is Exception))
				{
					multiAsyncResult.Result = lineInfo;
				}
				multiAsyncResult.Leave();
			}
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0007A2B0 File Offset: 0x000784B0
		internal static object EndSend(IAsyncResult result, out string response)
		{
			object obj = MultiAsyncResult.End(result);
			if (obj is Exception)
			{
				throw (Exception)obj;
			}
			LineInfo lineInfo = (LineInfo)obj;
			response = lineInfo.Line;
			return lineInfo.StatusCode;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0007A2F0 File Offset: 0x000784F0
		private static void OnReadLine(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				try
				{
					SmtpConnection smtpConnection = (SmtpConnection)multiAsyncResult.Context;
					LineInfo lineInfo = smtpConnection.Reader.CurrentReader.EndReadLine(result);
					if (!(multiAsyncResult.Result is Exception))
					{
						multiAsyncResult.Result = lineInfo;
					}
					multiAsyncResult.Leave();
				}
				catch (Exception ex)
				{
					multiAsyncResult.Leave(ex);
				}
			}
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0007A36C File Offset: 0x0007856C
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

		// Token: 0x060017E9 RID: 6121 RVA: 0x0007A3C4 File Offset: 0x000785C4
		internal static SmtpStatusCode Send(SmtpConnection conn, out string response)
		{
			conn.Flush();
			SmtpReplyReader nextReplyReader = conn.Reader.GetNextReplyReader();
			LineInfo lineInfo = nextReplyReader.ReadLine();
			response = lineInfo.Line;
			nextReplyReader.Close();
			return lineInfo.StatusCode;
		}

		// Token: 0x0400180B RID: 6155
		private static AsyncCallback onReadLine = new AsyncCallback(CheckCommand.OnReadLine);

		// Token: 0x0400180C RID: 6156
		private static AsyncCallback onWrite = new AsyncCallback(CheckCommand.OnWrite);
	}
}

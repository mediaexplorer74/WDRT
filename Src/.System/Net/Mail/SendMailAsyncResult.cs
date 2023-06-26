using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x02000299 RID: 665
	internal class SendMailAsyncResult : LazyAsyncResult
	{
		// Token: 0x060018AC RID: 6316 RVA: 0x0007D405 File Offset: 0x0007B605
		internal SendMailAsyncResult(SmtpConnection connection, MailAddress from, MailAddressCollection toCollection, bool allowUnicode, string deliveryNotify, AsyncCallback callback, object state)
			: base(null, state, callback)
		{
			this.toCollection = toCollection;
			this.connection = connection;
			this.from = from;
			this.deliveryNotify = deliveryNotify;
			this.allowUnicode = allowUnicode;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0007D442 File Offset: 0x0007B642
		internal void Send()
		{
			this.SendMailFrom();
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0007D44C File Offset: 0x0007B64C
		internal static MailWriter End(IAsyncResult result)
		{
			SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result;
			object obj = sendMailAsyncResult.InternalWaitForCompletion();
			if (obj is Exception && (!(obj is SmtpFailedRecipientException) || ((SmtpFailedRecipientException)obj).fatal))
			{
				throw (Exception)obj;
			}
			return new MailWriter(sendMailAsyncResult.stream);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0007D498 File Offset: 0x0007B698
		private void SendMailFrom()
		{
			IAsyncResult asyncResult = MailCommand.BeginSend(this.connection, SmtpCommands.Mail, this.from, this.allowUnicode, SendMailAsyncResult.sendMailFromCompleted, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return;
			}
			MailCommand.EndSend(asyncResult);
			this.SendToCollection();
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0007D4E0 File Offset: 0x0007B6E0
		private static void SendMailFromCompleted(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result.AsyncState;
				try
				{
					MailCommand.EndSend(result);
					sendMailAsyncResult.SendToCollection();
				}
				catch (Exception ex)
				{
					sendMailAsyncResult.InvokeCallback(ex);
				}
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0007D52C File Offset: 0x0007B72C
		private void SendToCollection()
		{
			while (this.toIndex < this.toCollection.Count)
			{
				SmtpConnection smtpConnection = this.connection;
				Collection<MailAddress> collection = this.toCollection;
				int num = this.toIndex;
				this.toIndex = num + 1;
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)RecipientCommand.BeginSend(smtpConnection, collection[num].GetSmtpAddress(this.allowUnicode) + this.deliveryNotify, SendMailAsyncResult.sendToCollectionCompleted, this);
				if (!multiAsyncResult.CompletedSynchronously)
				{
					return;
				}
				string text;
				if (!RecipientCommand.EndSend(multiAsyncResult, out text))
				{
					this.failedRecipientExceptions.Add(new SmtpFailedRecipientException(this.connection.Reader.StatusCode, this.toCollection[this.toIndex - 1].GetSmtpAddress(this.allowUnicode), text));
				}
			}
			this.SendData();
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0007D5F8 File Offset: 0x0007B7F8
		private static void SendToCollectionCompleted(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result.AsyncState;
				try
				{
					string text;
					if (!RecipientCommand.EndSend(result, out text))
					{
						sendMailAsyncResult.failedRecipientExceptions.Add(new SmtpFailedRecipientException(sendMailAsyncResult.connection.Reader.StatusCode, sendMailAsyncResult.toCollection[sendMailAsyncResult.toIndex - 1].GetSmtpAddress(sendMailAsyncResult.allowUnicode), text));
						if (sendMailAsyncResult.failedRecipientExceptions.Count == sendMailAsyncResult.toCollection.Count)
						{
							SmtpFailedRecipientException ex;
							if (sendMailAsyncResult.toCollection.Count == 1)
							{
								ex = (SmtpFailedRecipientException)sendMailAsyncResult.failedRecipientExceptions[0];
							}
							else
							{
								ex = new SmtpFailedRecipientsException(sendMailAsyncResult.failedRecipientExceptions, true);
							}
							ex.fatal = true;
							sendMailAsyncResult.InvokeCallback(ex);
							return;
						}
					}
					sendMailAsyncResult.SendToCollection();
				}
				catch (Exception ex2)
				{
					sendMailAsyncResult.InvokeCallback(ex2);
				}
			}
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0007D6E4 File Offset: 0x0007B8E4
		private void SendData()
		{
			IAsyncResult asyncResult = DataCommand.BeginSend(this.connection, SendMailAsyncResult.sendDataCompleted, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return;
			}
			DataCommand.EndSend(asyncResult);
			this.stream = this.connection.GetClosableStream();
			if (this.failedRecipientExceptions.Count > 1)
			{
				base.InvokeCallback(new SmtpFailedRecipientsException(this.failedRecipientExceptions, this.failedRecipientExceptions.Count == this.toCollection.Count));
				return;
			}
			if (this.failedRecipientExceptions.Count == 1)
			{
				base.InvokeCallback(this.failedRecipientExceptions[0]);
				return;
			}
			base.InvokeCallback();
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0007D784 File Offset: 0x0007B984
		private static void SendDataCompleted(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result.AsyncState;
				try
				{
					DataCommand.EndSend(result);
					sendMailAsyncResult.stream = sendMailAsyncResult.connection.GetClosableStream();
					if (sendMailAsyncResult.failedRecipientExceptions.Count > 1)
					{
						sendMailAsyncResult.InvokeCallback(new SmtpFailedRecipientsException(sendMailAsyncResult.failedRecipientExceptions, sendMailAsyncResult.failedRecipientExceptions.Count == sendMailAsyncResult.toCollection.Count));
					}
					else if (sendMailAsyncResult.failedRecipientExceptions.Count == 1)
					{
						sendMailAsyncResult.InvokeCallback(sendMailAsyncResult.failedRecipientExceptions[0]);
					}
					else
					{
						sendMailAsyncResult.InvokeCallback();
					}
				}
				catch (Exception ex)
				{
					sendMailAsyncResult.InvokeCallback(ex);
				}
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0007D83C File Offset: 0x0007BA3C
		internal SmtpFailedRecipientException GetFailedRecipientException()
		{
			if (this.failedRecipientExceptions.Count == 1)
			{
				return (SmtpFailedRecipientException)this.failedRecipientExceptions[0];
			}
			if (this.failedRecipientExceptions.Count > 1)
			{
				return new SmtpFailedRecipientsException(this.failedRecipientExceptions, false);
			}
			return null;
		}

		// Token: 0x04001888 RID: 6280
		private SmtpConnection connection;

		// Token: 0x04001889 RID: 6281
		private MailAddress from;

		// Token: 0x0400188A RID: 6282
		private string deliveryNotify;

		// Token: 0x0400188B RID: 6283
		private static AsyncCallback sendMailFromCompleted = new AsyncCallback(SendMailAsyncResult.SendMailFromCompleted);

		// Token: 0x0400188C RID: 6284
		private static AsyncCallback sendToCollectionCompleted = new AsyncCallback(SendMailAsyncResult.SendToCollectionCompleted);

		// Token: 0x0400188D RID: 6285
		private static AsyncCallback sendDataCompleted = new AsyncCallback(SendMailAsyncResult.SendDataCompleted);

		// Token: 0x0400188E RID: 6286
		private ArrayList failedRecipientExceptions = new ArrayList();

		// Token: 0x0400188F RID: 6287
		private Stream stream;

		// Token: 0x04001890 RID: 6288
		private MailAddressCollection toCollection;

		// Token: 0x04001891 RID: 6289
		private int toIndex;

		// Token: 0x04001892 RID: 6290
		private bool allowUnicode;
	}
}

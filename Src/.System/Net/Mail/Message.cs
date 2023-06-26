using System;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000273 RID: 627
	internal class Message
	{
		// Token: 0x0600177F RID: 6015 RVA: 0x00077DE8 File Offset: 0x00075FE8
		internal Message()
		{
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00077DF8 File Offset: 0x00075FF8
		internal Message(string from, string to)
			: this()
		{
			if (from == null)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null)
			{
				throw new ArgumentNullException("to");
			}
			if (from == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "from" }), "from");
			}
			if (to == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "to" }), "to");
			}
			this.from = new MailAddress(from);
			this.to = new MailAddressCollection { to };
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00077EA7 File Offset: 0x000760A7
		internal Message(MailAddress from, MailAddress to)
			: this()
		{
			this.from = from;
			this.To.Add(to);
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x00077EC2 File Offset: 0x000760C2
		// (set) Token: 0x06001783 RID: 6019 RVA: 0x00077ED5 File Offset: 0x000760D5
		public MailPriority Priority
		{
			get
			{
				if (this.priority != (MailPriority)(-1))
				{
					return this.priority;
				}
				return MailPriority.Normal;
			}
			set
			{
				this.priority = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00077EDE File Offset: 0x000760DE
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x00077EE6 File Offset: 0x000760E6
		internal MailAddress From
		{
			get
			{
				return this.from;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.from = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x00077EFD File Offset: 0x000760FD
		// (set) Token: 0x06001787 RID: 6023 RVA: 0x00077F05 File Offset: 0x00076105
		internal MailAddress Sender
		{
			get
			{
				return this.sender;
			}
			set
			{
				this.sender = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00077F0E File Offset: 0x0007610E
		// (set) Token: 0x06001789 RID: 6025 RVA: 0x00077F16 File Offset: 0x00076116
		internal MailAddress ReplyTo
		{
			get
			{
				return this.replyTo;
			}
			set
			{
				this.replyTo = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x00077F1F File Offset: 0x0007611F
		internal MailAddressCollection ReplyToList
		{
			get
			{
				if (this.replyToList == null)
				{
					this.replyToList = new MailAddressCollection();
				}
				return this.replyToList;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x00077F3A File Offset: 0x0007613A
		internal MailAddressCollection To
		{
			get
			{
				if (this.to == null)
				{
					this.to = new MailAddressCollection();
				}
				return this.to;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x00077F55 File Offset: 0x00076155
		internal MailAddressCollection Bcc
		{
			get
			{
				if (this.bcc == null)
				{
					this.bcc = new MailAddressCollection();
				}
				return this.bcc;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x00077F70 File Offset: 0x00076170
		internal MailAddressCollection CC
		{
			get
			{
				if (this.cc == null)
				{
					this.cc = new MailAddressCollection();
				}
				return this.cc;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x00077F8B File Offset: 0x0007618B
		// (set) Token: 0x0600178F RID: 6031 RVA: 0x00077F94 File Offset: 0x00076194
		internal string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				Encoding encoding = null;
				try
				{
					encoding = MimeBasePart.DecodeEncoding(value);
				}
				catch (ArgumentException)
				{
				}
				if (encoding != null && value != null)
				{
					try
					{
						value = MimeBasePart.DecodeHeaderValue(value);
						this.subjectEncoding = this.subjectEncoding ?? encoding;
					}
					catch (FormatException)
					{
					}
				}
				if (value != null && MailBnfHelper.HasCROrLF(value))
				{
					throw new ArgumentException(SR.GetString("MailSubjectInvalidFormat"));
				}
				this.subject = value;
				if (this.subject != null)
				{
					this.subject = this.subject.Normalize(NormalizationForm.FormC);
					if (this.subjectEncoding == null && !MimeBasePart.IsAscii(this.subject, false))
					{
						this.subjectEncoding = Encoding.GetEncoding("utf-8");
					}
				}
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x00078054 File Offset: 0x00076254
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x0007805C File Offset: 0x0007625C
		internal Encoding SubjectEncoding
		{
			get
			{
				return this.subjectEncoding;
			}
			set
			{
				this.subjectEncoding = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x00078065 File Offset: 0x00076265
		internal HeaderCollection Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new HeaderCollection();
					if (Logging.On)
					{
						Logging.Associate(Logging.Web, this, this.headers);
					}
				}
				return this.headers;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x00078098 File Offset: 0x00076298
		// (set) Token: 0x06001794 RID: 6036 RVA: 0x000780A0 File Offset: 0x000762A0
		internal Encoding HeadersEncoding
		{
			get
			{
				return this.headersEncoding;
			}
			set
			{
				this.headersEncoding = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x000780A9 File Offset: 0x000762A9
		internal HeaderCollection EnvelopeHeaders
		{
			get
			{
				if (this.envelopeHeaders == null)
				{
					this.envelopeHeaders = new HeaderCollection();
					if (Logging.On)
					{
						Logging.Associate(Logging.Web, this, this.envelopeHeaders);
					}
				}
				return this.envelopeHeaders;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x000780DC File Offset: 0x000762DC
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x000780E4 File Offset: 0x000762E4
		internal virtual MimeBasePart Content
		{
			get
			{
				return this.content;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.content = value;
			}
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000780FC File Offset: 0x000762FC
		internal void EmptySendCallback(IAsyncResult result)
		{
			Exception ex = null;
			if (result.CompletedSynchronously)
			{
				return;
			}
			Message.EmptySendContext emptySendContext = (Message.EmptySendContext)result.AsyncState;
			try
			{
				emptySendContext.writer.EndGetContentStream(result).Close();
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			emptySendContext.result.InvokeCallback(ex);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00078154 File Offset: 0x00076354
		internal virtual IAsyncResult BeginSend(BaseWriter writer, bool sendEnvelope, bool allowUnicode, AsyncCallback callback, object state)
		{
			this.PrepareHeaders(sendEnvelope, allowUnicode);
			writer.WriteHeaders(this.Headers, allowUnicode);
			if (this.Content != null)
			{
				return this.Content.BeginSend(writer, callback, allowUnicode, state);
			}
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, state, callback);
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.EmptySendCallback), new Message.EmptySendContext(writer, lazyAsyncResult));
			if (asyncResult.CompletedSynchronously)
			{
				writer.EndGetContentStream(asyncResult).Close();
			}
			return lazyAsyncResult;
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000781CC File Offset: 0x000763CC
		internal virtual void EndSend(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (this.Content != null)
			{
				this.Content.EndSend(asyncResult);
				return;
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null || lazyAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"));
			}
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndSend" }));
			}
			lazyAsyncResult.InternalWaitForCompletion();
			lazyAsyncResult.EndCalled = true;
			if (lazyAsyncResult.Result is Exception)
			{
				throw (Exception)lazyAsyncResult.Result;
			}
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0007826C File Offset: 0x0007646C
		internal virtual void Send(BaseWriter writer, bool sendEnvelope, bool allowUnicode)
		{
			if (sendEnvelope)
			{
				this.PrepareEnvelopeHeaders(sendEnvelope, allowUnicode);
				writer.WriteHeaders(this.EnvelopeHeaders, allowUnicode);
			}
			this.PrepareHeaders(sendEnvelope, allowUnicode);
			writer.WriteHeaders(this.Headers, allowUnicode);
			if (this.Content != null)
			{
				this.Content.Send(writer, allowUnicode);
				return;
			}
			writer.GetContentStream().Close();
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x000782C8 File Offset: 0x000764C8
		internal void PrepareEnvelopeHeaders(bool sendEnvelope, bool allowUnicode)
		{
			if (this.headersEncoding == null)
			{
				this.headersEncoding = Encoding.GetEncoding("utf-8");
			}
			this.EncodeHeaders(this.EnvelopeHeaders, allowUnicode);
			string @string = MailHeaderInfo.GetString(MailHeaderID.XSender);
			if (!this.IsHeaderSet(@string))
			{
				MailAddress mailAddress = this.Sender ?? this.From;
				this.EnvelopeHeaders.InternalSet(@string, mailAddress.Encode(@string.Length, allowUnicode));
			}
			string string2 = MailHeaderInfo.GetString(MailHeaderID.XReceiver);
			this.EnvelopeHeaders.Remove(string2);
			foreach (MailAddress mailAddress2 in this.To)
			{
				this.EnvelopeHeaders.InternalAdd(string2, mailAddress2.Encode(string2.Length, allowUnicode));
			}
			foreach (MailAddress mailAddress3 in this.CC)
			{
				this.EnvelopeHeaders.InternalAdd(string2, mailAddress3.Encode(string2.Length, allowUnicode));
			}
			foreach (MailAddress mailAddress4 in this.Bcc)
			{
				this.EnvelopeHeaders.InternalAdd(string2, mailAddress4.Encode(string2.Length, allowUnicode));
			}
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00078448 File Offset: 0x00076648
		internal void PrepareHeaders(bool sendEnvelope, bool allowUnicode)
		{
			if (this.headersEncoding == null)
			{
				this.headersEncoding = Encoding.GetEncoding("utf-8");
			}
			this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentType));
			this.Headers[MailHeaderInfo.GetString(MailHeaderID.MimeVersion)] = "1.0";
			string text = MailHeaderInfo.GetString(MailHeaderID.Sender);
			if (this.Sender != null)
			{
				this.Headers.InternalAdd(text, this.Sender.Encode(text.Length, allowUnicode));
			}
			else
			{
				this.Headers.Remove(text);
			}
			text = MailHeaderInfo.GetString(MailHeaderID.From);
			this.Headers.InternalAdd(text, this.From.Encode(text.Length, allowUnicode));
			text = MailHeaderInfo.GetString(MailHeaderID.To);
			if (this.To.Count > 0)
			{
				this.Headers.InternalAdd(text, this.To.Encode(text.Length, allowUnicode));
			}
			else
			{
				this.Headers.Remove(text);
			}
			text = MailHeaderInfo.GetString(MailHeaderID.Cc);
			if (this.CC.Count > 0)
			{
				this.Headers.InternalAdd(text, this.CC.Encode(text.Length, allowUnicode));
			}
			else
			{
				this.Headers.Remove(text);
			}
			text = MailHeaderInfo.GetString(MailHeaderID.ReplyTo);
			if (this.ReplyTo != null)
			{
				this.Headers.InternalAdd(text, this.ReplyTo.Encode(text.Length, allowUnicode));
			}
			else if (this.ReplyToList.Count > 0)
			{
				this.Headers.InternalAdd(text, this.ReplyToList.Encode(text.Length, allowUnicode));
			}
			else
			{
				this.Headers.Remove(text);
			}
			this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.Bcc));
			if (this.priority == MailPriority.High)
			{
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.XPriority)] = "1";
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.Priority)] = "urgent";
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.Importance)] = "high";
			}
			else if (this.priority == MailPriority.Low)
			{
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.XPriority)] = "5";
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.Priority)] = "non-urgent";
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.Importance)] = "low";
			}
			else if (this.priority != (MailPriority)(-1))
			{
				this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.XPriority));
				this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.Priority));
				this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.Importance));
			}
			this.Headers.InternalAdd(MailHeaderInfo.GetString(MailHeaderID.Date), MailBnfHelper.GetDateTimeString(DateTime.Now, null));
			text = MailHeaderInfo.GetString(MailHeaderID.Subject);
			if (!string.IsNullOrEmpty(this.subject))
			{
				if (allowUnicode)
				{
					this.Headers.InternalAdd(text, this.subject);
				}
				else
				{
					this.Headers.InternalAdd(text, MimeBasePart.EncodeHeaderValue(this.subject, this.subjectEncoding, MimeBasePart.ShouldUseBase64Encoding(this.subjectEncoding), text.Length));
				}
			}
			else
			{
				this.Headers.Remove(text);
			}
			this.EncodeHeaders(this.headers, allowUnicode);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0007876C File Offset: 0x0007696C
		internal void EncodeHeaders(HeaderCollection headers, bool allowUnicode)
		{
			if (this.headersEncoding == null)
			{
				this.headersEncoding = Encoding.GetEncoding("utf-8");
			}
			for (int i = 0; i < headers.Count; i++)
			{
				string key = headers.GetKey(i);
				if (MailHeaderInfo.IsUserSettable(key))
				{
					string[] values = headers.GetValues(key);
					string text = string.Empty;
					for (int j = 0; j < values.Length; j++)
					{
						if (MimeBasePart.IsAscii(values[j], false) || (allowUnicode && MailHeaderInfo.AllowsUnicode(key) && !MailBnfHelper.HasCROrLF(values[j])))
						{
							text = values[j];
						}
						else
						{
							text = MimeBasePart.EncodeHeaderValue(values[j], this.headersEncoding, MimeBasePart.ShouldUseBase64Encoding(this.headersEncoding), key.Length);
						}
						if (j == 0)
						{
							headers.Set(key, text);
						}
						else
						{
							headers.Add(key, text);
						}
					}
				}
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0007883C File Offset: 0x00076A3C
		private bool IsHeaderSet(string headerName)
		{
			for (int i = 0; i < this.Headers.Count; i++)
			{
				if (string.Compare(this.Headers.GetKey(i), headerName, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040017D4 RID: 6100
		private MailAddress from;

		// Token: 0x040017D5 RID: 6101
		private MailAddress sender;

		// Token: 0x040017D6 RID: 6102
		private MailAddressCollection replyToList;

		// Token: 0x040017D7 RID: 6103
		private MailAddress replyTo;

		// Token: 0x040017D8 RID: 6104
		private MailAddressCollection to;

		// Token: 0x040017D9 RID: 6105
		private MailAddressCollection cc;

		// Token: 0x040017DA RID: 6106
		private MailAddressCollection bcc;

		// Token: 0x040017DB RID: 6107
		private MimeBasePart content;

		// Token: 0x040017DC RID: 6108
		private HeaderCollection headers;

		// Token: 0x040017DD RID: 6109
		private HeaderCollection envelopeHeaders;

		// Token: 0x040017DE RID: 6110
		private string subject;

		// Token: 0x040017DF RID: 6111
		private Encoding subjectEncoding;

		// Token: 0x040017E0 RID: 6112
		private Encoding headersEncoding;

		// Token: 0x040017E1 RID: 6113
		private MailPriority priority = (MailPriority)(-1);

		// Token: 0x0200079D RID: 1949
		internal class EmptySendContext
		{
			// Token: 0x060042D0 RID: 17104 RVA: 0x0011774B File Offset: 0x0011594B
			internal EmptySendContext(BaseWriter writer, LazyAsyncResult result)
			{
				this.writer = writer;
				this.result = result;
			}

			// Token: 0x0400338F RID: 13199
			internal LazyAsyncResult result;

			// Token: 0x04003390 RID: 13200
			internal BaseWriter writer;
		}
	}
}

using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000249 RID: 585
	internal class MimeBasePart
	{
		// Token: 0x06001618 RID: 5656 RVA: 0x00072B32 File Offset: 0x00070D32
		internal MimeBasePart()
		{
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00072B3A File Offset: 0x00070D3A
		internal static bool ShouldUseBase64Encoding(Encoding encoding)
		{
			return encoding == Encoding.Unicode || encoding == Encoding.UTF8 || encoding == Encoding.UTF32 || encoding == Encoding.BigEndianUnicode;
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00072B5F File Offset: 0x00070D5F
		internal static string EncodeHeaderValue(string value, Encoding encoding, bool base64Encoding)
		{
			return MimeBasePart.EncodeHeaderValue(value, encoding, base64Encoding, 0);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00072B6C File Offset: 0x00070D6C
		internal static string EncodeHeaderValue(string value, Encoding encoding, bool base64Encoding, int headerLength)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (MimeBasePart.IsAscii(value, false))
			{
				return value;
			}
			if (encoding == null)
			{
				encoding = Encoding.GetEncoding("utf-8");
			}
			EncodedStreamFactory encodedStreamFactory = new EncodedStreamFactory();
			IEncodableStream encoderForHeader = encodedStreamFactory.GetEncoderForHeader(encoding, base64Encoding, headerLength);
			byte[] bytes = encoding.GetBytes(value);
			encoderForHeader.EncodeBytes(bytes, 0, bytes.Length);
			return encoderForHeader.GetEncodedString();
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00072BC4 File Offset: 0x00070DC4
		internal static string DecodeHeaderValue(string value)
		{
			if (value == null || value.Length == 0)
			{
				return string.Empty;
			}
			string text = string.Empty;
			string[] array = value.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text2 in array)
			{
				string[] array3 = text2.Split(new char[] { '?' });
				if (array3.Length != 5 || array3[0] != "=" || array3[4] != "=")
				{
					return value;
				}
				string text3 = array3[1];
				bool flag = array3[2] == "B";
				byte[] bytes = Encoding.ASCII.GetBytes(array3[3]);
				EncodedStreamFactory encodedStreamFactory = new EncodedStreamFactory();
				IEncodableStream encoderForHeader = encodedStreamFactory.GetEncoderForHeader(Encoding.GetEncoding(text3), flag, 0);
				int num = encoderForHeader.DecodeBytes(bytes, 0, bytes.Length);
				Encoding encoding = Encoding.GetEncoding(text3);
				text += encoding.GetString(bytes, 0, num);
			}
			return text;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00072CC8 File Offset: 0x00070EC8
		internal static Encoding DecodeEncoding(string value)
		{
			if (value == null || value.Length == 0)
			{
				return null;
			}
			string[] array = value.Split(new char[] { '?', '\r', '\n' });
			if (array.Length < 5 || array[0] != "=" || array[4] != "=")
			{
				return null;
			}
			string text = array[1];
			return Encoding.GetEncoding(text);
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00072D2C File Offset: 0x00070F2C
		internal static bool IsAscii(string value, bool permitCROrLF)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (char c in value)
			{
				if (c > '\u007f')
				{
					return false;
				}
				if (!permitCROrLF && (c == '\r' || c == '\n'))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00072D7C File Offset: 0x00070F7C
		internal static bool IsAnsi(string value, bool permitCROrLF)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (char c in value)
			{
				if (c > 'ÿ')
				{
					return false;
				}
				if (!permitCROrLF && (c == '\r' || c == '\n'))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x00072DCC File Offset: 0x00070FCC
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x00072DDF File Offset: 0x00070FDF
		internal string ContentID
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentID));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)] = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x00072E0D File Offset: 0x0007100D
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x00072E20 File Offset: 0x00071020
		internal string ContentLocation
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentLocation));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)] = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00072E50 File Offset: 0x00071050
		internal NameValueCollection Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new HeaderCollection();
				}
				if (this.contentType == null)
				{
					this.contentType = new ContentType();
				}
				this.contentType.PersistIfNeeded(this.headers, false);
				if (this.contentDisposition != null)
				{
					this.contentDisposition.PersistIfNeeded(this.headers, false);
				}
				return this.headers;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x00072EB5 File Offset: 0x000710B5
		// (set) Token: 0x06001626 RID: 5670 RVA: 0x00072ED0 File Offset: 0x000710D0
		internal ContentType ContentType
		{
			get
			{
				if (this.contentType == null)
				{
					this.contentType = new ContentType();
				}
				return this.contentType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.contentType = value;
				this.contentType.PersistIfNeeded((HeaderCollection)this.Headers, true);
			}
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00072F00 File Offset: 0x00071100
		internal void PrepareHeaders(bool allowUnicode)
		{
			this.contentType.PersistIfNeeded((HeaderCollection)this.Headers, false);
			this.headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.contentType.Encode(allowUnicode));
			if (this.contentDisposition != null)
			{
				this.contentDisposition.PersistIfNeeded((HeaderCollection)this.Headers, false);
				this.headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.contentDisposition.Encode(allowUnicode));
			}
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00072F7D File Offset: 0x0007117D
		internal virtual void Send(BaseWriter writer, bool allowUnicode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00072F84 File Offset: 0x00071184
		internal virtual IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00072F8C File Offset: 0x0007118C
		internal void EndSend(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as MimeBasePart.MimePartAsyncResult;
			if (lazyAsyncResult == null || lazyAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
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

		// Token: 0x04001711 RID: 5905
		protected ContentType contentType;

		// Token: 0x04001712 RID: 5906
		protected ContentDisposition contentDisposition;

		// Token: 0x04001713 RID: 5907
		private HeaderCollection headers;

		// Token: 0x04001714 RID: 5908
		internal const string defaultCharSet = "utf-8";

		// Token: 0x02000795 RID: 1941
		internal class MimePartAsyncResult : LazyAsyncResult
		{
			// Token: 0x060042B6 RID: 17078 RVA: 0x00117388 File Offset: 0x00115588
			internal MimePartAsyncResult(MimeBasePart part, object state, AsyncCallback callback)
				: base(part, state, callback)
			{
			}
		}
	}
}

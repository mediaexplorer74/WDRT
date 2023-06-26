using System;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an email message that can be sent using the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
	// Token: 0x02000270 RID: 624
	public class MailMessage : IDisposable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.MailMessage" /> class.</summary>
		// Token: 0x06001751 RID: 5969 RVA: 0x000773D8 File Offset: 0x000755D8
		public MailMessage()
		{
			this.message = new Message();
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, this.message);
			}
			string from = SmtpClient.MailConfiguration.Smtp.From;
			if (from != null && from.Length > 0)
			{
				this.message.From = new MailAddress(from);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class by using the specified <see cref="T:System.String" /> class objects.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address of the sender of the email message.</param>
		/// <param name="to">A <see cref="T:System.String" /> that contains the addresses of the recipients of the email message. Multiple email addresses must be separated with a comma character (",").</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="to" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" /> ("").  
		/// -or-  
		/// <paramref name="to" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06001752 RID: 5970 RVA: 0x00077450 File Offset: 0x00075650
		public MailMessage(string from, string to)
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
			this.message = new Message(from, to);
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, this.message);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address of the sender of the email message.</param>
		/// <param name="to">A <see cref="T:System.String" /> that contains the addresses of the recipients of the email message. Multiple email addresses must be separated with a comma character (",").</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject text.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="to" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" /> ("").  
		/// -or-  
		/// <paramref name="to" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06001753 RID: 5971 RVA: 0x00077516 File Offset: 0x00075716
		public MailMessage(string from, string to, string subject, string body)
			: this(from, to)
		{
			this.Subject = subject;
			this.Body = body;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class by using the specified <see cref="T:System.Net.Mail.MailAddress" /> class objects.</summary>
		/// <param name="from">A <see cref="T:System.Net.Mail.MailAddress" /> that contains the address of the sender of the email message.</param>
		/// <param name="to">A <see cref="T:System.Net.Mail.MailAddress" /> that contains the address of the recipient of the email message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="to" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06001754 RID: 5972 RVA: 0x00077530 File Offset: 0x00075730
		public MailMessage(MailAddress from, MailAddress to)
		{
			if (from == null)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null)
			{
				throw new ArgumentNullException("to");
			}
			this.message = new Message(from, to);
		}

		/// <summary>Gets or sets the from address for this email message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailAddress" /> that contains the from address information.</returns>
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0007757E File Offset: 0x0007577E
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x0007758B File Offset: 0x0007578B
		public MailAddress From
		{
			get
			{
				return this.message.From;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.message.From = value;
			}
		}

		/// <summary>Gets or sets the sender's address for this email message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailAddress" /> that contains the sender's address information.</returns>
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x000775A7 File Offset: 0x000757A7
		// (set) Token: 0x06001758 RID: 5976 RVA: 0x000775B4 File Offset: 0x000757B4
		public MailAddress Sender
		{
			get
			{
				return this.message.Sender;
			}
			set
			{
				this.message.Sender = value;
			}
		}

		/// <summary>Gets or sets the ReplyTo address for the mail message.</summary>
		/// <returns>A MailAddress that indicates the value of the <see cref="P:System.Net.Mail.MailMessage.ReplyTo" /> field.</returns>
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x000775C2 File Offset: 0x000757C2
		// (set) Token: 0x0600175A RID: 5978 RVA: 0x000775CF File Offset: 0x000757CF
		[Obsolete("ReplyTo is obsoleted for this type.  Please use ReplyToList instead which can accept multiple addresses. http://go.microsoft.com/fwlink/?linkid=14202")]
		public MailAddress ReplyTo
		{
			get
			{
				return this.message.ReplyTo;
			}
			set
			{
				this.message.ReplyTo = value;
			}
		}

		/// <summary>Gets the list of addresses to reply to for the mail message.</summary>
		/// <returns>The list of the addresses to reply to for the mail message.</returns>
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x000775DD File Offset: 0x000757DD
		public MailAddressCollection ReplyToList
		{
			get
			{
				return this.message.ReplyToList;
			}
		}

		/// <summary>Gets the address collection that contains the recipients of this email message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x000775EA File Offset: 0x000757EA
		public MailAddressCollection To
		{
			get
			{
				return this.message.To;
			}
		}

		/// <summary>Gets the address collection that contains the blind carbon copy (BCC) recipients for this email message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x000775F7 File Offset: 0x000757F7
		public MailAddressCollection Bcc
		{
			get
			{
				return this.message.Bcc;
			}
		}

		/// <summary>Gets the address collection that contains the carbon copy (CC) recipients for this email message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00077604 File Offset: 0x00075804
		public MailAddressCollection CC
		{
			get
			{
				return this.message.CC;
			}
		}

		/// <summary>Gets or sets the priority of this email message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailPriority" /> that contains the priority of this message.</returns>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x00077611 File Offset: 0x00075811
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x0007761E File Offset: 0x0007581E
		public MailPriority Priority
		{
			get
			{
				return this.message.Priority;
			}
			set
			{
				this.message.Priority = value;
			}
		}

		/// <summary>Gets or sets the delivery notifications for this email message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.DeliveryNotificationOptions" /> value that contains the delivery notifications for this message.</returns>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x0007762C File Offset: 0x0007582C
		// (set) Token: 0x06001762 RID: 5986 RVA: 0x00077634 File Offset: 0x00075834
		public DeliveryNotificationOptions DeliveryNotificationOptions
		{
			get
			{
				return this.deliveryStatusNotification;
			}
			set
			{
				if ((DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay) < value && value != DeliveryNotificationOptions.Never)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.deliveryStatusNotification = value;
			}
		}

		/// <summary>Gets or sets the subject line for this email message.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the subject content.</returns>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00077654 File Offset: 0x00075854
		// (set) Token: 0x06001764 RID: 5988 RVA: 0x00077674 File Offset: 0x00075874
		public string Subject
		{
			get
			{
				if (this.message.Subject == null)
				{
					return string.Empty;
				}
				return this.message.Subject;
			}
			set
			{
				this.message.Subject = value;
			}
		}

		/// <summary>Gets or sets the encoding used for the subject content for this email message.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> that was used to encode the <see cref="P:System.Net.Mail.MailMessage.Subject" /> property.</returns>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x00077682 File Offset: 0x00075882
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x0007768F File Offset: 0x0007588F
		public Encoding SubjectEncoding
		{
			get
			{
				return this.message.SubjectEncoding;
			}
			set
			{
				this.message.SubjectEncoding = value;
			}
		}

		/// <summary>Gets the email headers that are transmitted with this email message.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the email headers.</returns>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x0007769D File Offset: 0x0007589D
		public NameValueCollection Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		/// <summary>Gets or sets the encoding used for the user-defined custom headers for this email message.</summary>
		/// <returns>The encoding used for user-defined custom headers for this email message.</returns>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x000776AA File Offset: 0x000758AA
		// (set) Token: 0x06001769 RID: 5993 RVA: 0x000776B7 File Offset: 0x000758B7
		public Encoding HeadersEncoding
		{
			get
			{
				return this.message.HeadersEncoding;
			}
			set
			{
				this.message.HeadersEncoding = value;
			}
		}

		/// <summary>Gets or sets the message body.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the body text.</returns>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x000776C5 File Offset: 0x000758C5
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x000776DC File Offset: 0x000758DC
		public string Body
		{
			get
			{
				if (this.body == null)
				{
					return string.Empty;
				}
				return this.body;
			}
			set
			{
				this.body = value;
				if (this.bodyEncoding == null && this.body != null)
				{
					if (MimeBasePart.IsAscii(this.body, true))
					{
						this.bodyEncoding = Encoding.ASCII;
						return;
					}
					this.bodyEncoding = Encoding.GetEncoding("utf-8");
				}
			}
		}

		/// <summary>Gets or sets the encoding used to encode the message body.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> applied to the contents of the <see cref="P:System.Net.Mail.MailMessage.Body" />.</returns>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x0007772A File Offset: 0x0007592A
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x00077732 File Offset: 0x00075932
		public Encoding BodyEncoding
		{
			get
			{
				return this.bodyEncoding;
			}
			set
			{
				this.bodyEncoding = value;
			}
		}

		/// <summary>Gets or sets the transfer encoding used to encode the message body.</summary>
		/// <returns>A <see cref="T:System.Net.Mime.TransferEncoding" /> applied to the contents of the <see cref="P:System.Net.Mail.MailMessage.Body" />.</returns>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x0007773B File Offset: 0x0007593B
		// (set) Token: 0x0600176F RID: 5999 RVA: 0x00077743 File Offset: 0x00075943
		public TransferEncoding BodyTransferEncoding
		{
			get
			{
				return this.bodyTransferEncoding;
			}
			set
			{
				this.bodyTransferEncoding = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the mail message body is in HTML.</summary>
		/// <returns>
		///   <see langword="true" /> if the message body is in HTML; else <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x0007774C File Offset: 0x0007594C
		// (set) Token: 0x06001771 RID: 6001 RVA: 0x00077754 File Offset: 0x00075954
		public bool IsBodyHtml
		{
			get
			{
				return this.isBodyHtml;
			}
			set
			{
				this.isBodyHtml = value;
			}
		}

		/// <summary>Gets the attachment collection used to store data attached to this email message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.AttachmentCollection" />.</returns>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0007775D File Offset: 0x0007595D
		public AttachmentCollection Attachments
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.attachments == null)
				{
					this.attachments = new AttachmentCollection();
				}
				return this.attachments;
			}
		}

		/// <summary>Gets the attachment collection used to store alternate forms of the message body.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.AlternateViewCollection" />.</returns>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x00077791 File Offset: 0x00075991
		public AlternateViewCollection AlternateViews
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.views == null)
				{
					this.views = new AlternateViewCollection();
				}
				return this.views;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.MailMessage" />.</summary>
		// Token: 0x06001774 RID: 6004 RVA: 0x000777C5 File Offset: 0x000759C5
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Mail.MailMessage" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001775 RID: 6005 RVA: 0x000777D0 File Offset: 0x000759D0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.views != null)
				{
					this.views.Dispose();
				}
				if (this.attachments != null)
				{
					this.attachments.Dispose();
				}
				if (this.bodyView != null)
				{
					this.bodyView.Dispose();
				}
			}
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00077828 File Offset: 0x00075A28
		private void SetContent(bool allowUnicode)
		{
			if (this.bodyView != null)
			{
				this.bodyView.Dispose();
				this.bodyView = null;
			}
			if (this.AlternateViews.Count == 0 && this.Attachments.Count == 0)
			{
				if (!string.IsNullOrEmpty(this.body))
				{
					this.bodyView = AlternateView.CreateAlternateViewFromString(this.body, this.bodyEncoding, this.isBodyHtml ? "text/html" : null);
					this.message.Content = this.bodyView.MimePart;
				}
			}
			else if (this.AlternateViews.Count == 0 && this.Attachments.Count > 0)
			{
				MimeMultiPart mimeMultiPart = new MimeMultiPart(MimeMultiPartType.Mixed);
				if (!string.IsNullOrEmpty(this.body))
				{
					this.bodyView = AlternateView.CreateAlternateViewFromString(this.body, this.bodyEncoding, this.isBodyHtml ? "text/html" : null);
				}
				else
				{
					this.bodyView = AlternateView.CreateAlternateViewFromString(string.Empty);
				}
				mimeMultiPart.Parts.Add(this.bodyView.MimePart);
				foreach (Attachment attachment in this.Attachments)
				{
					if (attachment != null)
					{
						attachment.PrepareForSending(allowUnicode);
						mimeMultiPart.Parts.Add(attachment.MimePart);
					}
				}
				this.message.Content = mimeMultiPart;
			}
			else
			{
				MimeMultiPart mimeMultiPart2 = null;
				MimeMultiPart mimeMultiPart3 = new MimeMultiPart(MimeMultiPartType.Alternative);
				if (!string.IsNullOrEmpty(this.body))
				{
					this.bodyView = AlternateView.CreateAlternateViewFromString(this.body, this.bodyEncoding, null);
					mimeMultiPart3.Parts.Add(this.bodyView.MimePart);
				}
				foreach (AlternateView alternateView in this.AlternateViews)
				{
					if (alternateView != null)
					{
						alternateView.PrepareForSending(allowUnicode);
						if (alternateView.LinkedResources.Count > 0)
						{
							MimeMultiPart mimeMultiPart4 = new MimeMultiPart(MimeMultiPartType.Related);
							mimeMultiPart4.ContentType.Parameters["type"] = alternateView.ContentType.MediaType;
							mimeMultiPart4.ContentLocation = alternateView.MimePart.ContentLocation;
							mimeMultiPart4.Parts.Add(alternateView.MimePart);
							foreach (LinkedResource linkedResource in alternateView.LinkedResources)
							{
								linkedResource.PrepareForSending(allowUnicode);
								mimeMultiPart4.Parts.Add(linkedResource.MimePart);
							}
							mimeMultiPart3.Parts.Add(mimeMultiPart4);
						}
						else
						{
							mimeMultiPart3.Parts.Add(alternateView.MimePart);
						}
					}
				}
				if (this.Attachments.Count > 0)
				{
					mimeMultiPart2 = new MimeMultiPart(MimeMultiPartType.Mixed);
					mimeMultiPart2.Parts.Add(mimeMultiPart3);
					MimeMultiPart mimeMultiPart5 = new MimeMultiPart(MimeMultiPartType.Mixed);
					foreach (Attachment attachment2 in this.Attachments)
					{
						if (attachment2 != null)
						{
							attachment2.PrepareForSending(allowUnicode);
							mimeMultiPart5.Parts.Add(attachment2.MimePart);
						}
					}
					mimeMultiPart2.Parts.Add(mimeMultiPart5);
					this.message.Content = mimeMultiPart2;
				}
				else if (mimeMultiPart3.Parts.Count == 1 && string.IsNullOrEmpty(this.body))
				{
					this.message.Content = mimeMultiPart3.Parts[0];
				}
				else
				{
					this.message.Content = mimeMultiPart3;
				}
			}
			if (this.bodyView != null && this.bodyTransferEncoding != TransferEncoding.Unknown)
			{
				this.bodyView.TransferEncoding = this.bodyTransferEncoding;
			}
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00077C20 File Offset: 0x00075E20
		internal void Send(BaseWriter writer, bool sendEnvelope, bool allowUnicode)
		{
			this.SetContent(allowUnicode);
			this.message.Send(writer, sendEnvelope, allowUnicode);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00077C37 File Offset: 0x00075E37
		internal IAsyncResult BeginSend(BaseWriter writer, bool sendEnvelope, bool allowUnicode, AsyncCallback callback, object state)
		{
			this.SetContent(allowUnicode);
			return this.message.BeginSend(writer, sendEnvelope, allowUnicode, callback, state);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00077C52 File Offset: 0x00075E52
		internal void EndSend(IAsyncResult asyncResult)
		{
			this.message.EndSend(asyncResult);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00077C60 File Offset: 0x00075E60
		internal string BuildDeliveryStatusNotificationString()
		{
			if (this.deliveryStatusNotification == DeliveryNotificationOptions.None)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(" NOTIFY=");
			bool flag = false;
			if (this.deliveryStatusNotification == DeliveryNotificationOptions.Never)
			{
				stringBuilder.Append("NEVER");
				return stringBuilder.ToString();
			}
			if ((this.deliveryStatusNotification & DeliveryNotificationOptions.OnSuccess) > DeliveryNotificationOptions.None)
			{
				stringBuilder.Append("SUCCESS");
				flag = true;
			}
			if ((this.deliveryStatusNotification & DeliveryNotificationOptions.OnFailure) > DeliveryNotificationOptions.None)
			{
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("FAILURE");
				flag = true;
			}
			if ((this.deliveryStatusNotification & DeliveryNotificationOptions.Delay) > DeliveryNotificationOptions.None)
			{
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("DELAY");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040017C6 RID: 6086
		private AlternateViewCollection views;

		// Token: 0x040017C7 RID: 6087
		private AttachmentCollection attachments;

		// Token: 0x040017C8 RID: 6088
		private AlternateView bodyView;

		// Token: 0x040017C9 RID: 6089
		private string body = string.Empty;

		// Token: 0x040017CA RID: 6090
		private Encoding bodyEncoding;

		// Token: 0x040017CB RID: 6091
		private TransferEncoding bodyTransferEncoding = TransferEncoding.Unknown;

		// Token: 0x040017CC RID: 6092
		private bool isBodyHtml;

		// Token: 0x040017CD RID: 6093
		private bool disposed;

		// Token: 0x040017CE RID: 6094
		private Message message;

		// Token: 0x040017CF RID: 6095
		private DeliveryNotificationOptions deliveryStatusNotification;
	}
}

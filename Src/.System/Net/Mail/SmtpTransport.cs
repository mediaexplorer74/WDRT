using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Mail
{
	// Token: 0x02000298 RID: 664
	internal class SmtpTransport
	{
		// Token: 0x06001895 RID: 6293 RVA: 0x0007CF56 File Offset: 0x0007B156
		internal SmtpTransport(SmtpClient client)
			: this(client, SmtpAuthenticationManager.GetModules())
		{
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0007CF64 File Offset: 0x0007B164
		internal SmtpTransport(SmtpClient client, ISmtpAuthenticationModule[] authenticationModules)
		{
			this.client = client;
			if (authenticationModules == null)
			{
				throw new ArgumentNullException("authenticationModules");
			}
			this.authenticationModules = authenticationModules;
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0007CF9E File Offset: 0x0007B19E
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x0007CFA6 File Offset: 0x0007B1A6
		internal ICredentialsByHost Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0007CFAF File Offset: 0x0007B1AF
		// (set) Token: 0x0600189A RID: 6298 RVA: 0x0007CFB7 File Offset: 0x0007B1B7
		internal bool IdentityRequired
		{
			get
			{
				return this.m_IdentityRequired;
			}
			set
			{
				this.m_IdentityRequired = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x0007CFC0 File Offset: 0x0007B1C0
		internal bool IsConnected
		{
			get
			{
				return this.connection != null && this.connection.IsConnected;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x0007CFD7 File Offset: 0x0007B1D7
		// (set) Token: 0x0600189D RID: 6301 RVA: 0x0007CFDF File Offset: 0x0007B1DF
		internal int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeout = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x0007CFF7 File Offset: 0x0007B1F7
		// (set) Token: 0x0600189F RID: 6303 RVA: 0x0007CFFF File Offset: 0x0007B1FF
		internal bool EnableSsl
		{
			get
			{
				return this.enableSsl;
			}
			set
			{
				this.enableSsl = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0007D008 File Offset: 0x0007B208
		internal X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.clientCertificates == null)
				{
					this.clientCertificates = new X509CertificateCollection();
				}
				return this.clientCertificates;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0007D023 File Offset: 0x0007B223
		internal bool ServerSupportsEai
		{
			get
			{
				return this.connection != null && this.connection.ServerSupportsEai;
			}
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0007D03C File Offset: 0x0007B23C
		private void UpdateServicePoint(ServicePoint servicePoint)
		{
			if (this.lastUsedServicePoint == null)
			{
				this.lastUsedServicePoint = servicePoint;
				return;
			}
			if (this.lastUsedServicePoint.Host != servicePoint.Host || this.lastUsedServicePoint.Port != servicePoint.Port)
			{
				ConnectionPoolManager.CleanupConnectionPool(servicePoint, "");
				this.lastUsedServicePoint = servicePoint;
			}
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0007D098 File Offset: 0x0007B298
		internal void GetConnection(ServicePoint servicePoint)
		{
			this.UpdateServicePoint(servicePoint);
			this.connection = new SmtpConnection(this, this.client, this.credentials, this.authenticationModules);
			this.connection.Timeout = this.timeout;
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, this.connection);
			}
			if (this.EnableSsl)
			{
				this.connection.EnableSsl = true;
				this.connection.ClientCertificates = this.ClientCertificates;
			}
			this.connection.GetConnection(servicePoint);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0007D124 File Offset: 0x0007B324
		internal IAsyncResult BeginGetConnection(ServicePoint servicePoint, ContextAwareResult outerResult, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult = null;
			try
			{
				this.UpdateServicePoint(servicePoint);
				this.connection = new SmtpConnection(this, this.client, this.credentials, this.authenticationModules);
				this.connection.Timeout = this.timeout;
				if (Logging.On)
				{
					Logging.Associate(Logging.Web, this, this.connection);
				}
				if (this.EnableSsl)
				{
					this.connection.EnableSsl = true;
					this.connection.ClientCertificates = this.ClientCertificates;
				}
				asyncResult = this.connection.BeginGetConnection(servicePoint, outerResult, callback, state);
			}
			catch (Exception ex)
			{
				throw new SmtpException(System.SR.GetString("MailHostNotFound"), ex);
			}
			return asyncResult;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0007D1DC File Offset: 0x0007B3DC
		internal void EndGetConnection(IAsyncResult result)
		{
			try
			{
				this.connection.EndGetConnection(result);
			}
			finally
			{
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0007D208 File Offset: 0x0007B408
		internal IAsyncResult BeginSendMail(MailAddress sender, MailAddressCollection recipients, string deliveryNotify, bool allowUnicode, AsyncCallback callback, object state)
		{
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			SendMailAsyncResult sendMailAsyncResult = new SendMailAsyncResult(this.connection, sender, recipients, allowUnicode, this.connection.DSNEnabled ? deliveryNotify : null, callback, state);
			sendMailAsyncResult.Send();
			return sendMailAsyncResult;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0007D25D File Offset: 0x0007B45D
		internal void ReleaseConnection()
		{
			if (this.connection != null)
			{
				this.connection.ReleaseConnection();
			}
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0007D272 File Offset: 0x0007B472
		internal void Abort()
		{
			if (this.connection != null)
			{
				this.connection.Abort();
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0007D287 File Offset: 0x0007B487
		internal MailWriter EndSendMail(IAsyncResult result)
		{
			return SendMailAsyncResult.End(result);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0007D290 File Offset: 0x0007B490
		internal MailWriter SendMail(MailAddress sender, MailAddressCollection recipients, string deliveryNotify, bool allowUnicode, out SmtpFailedRecipientException exception)
		{
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			MailCommand.Send(this.connection, SmtpCommands.Mail, sender, allowUnicode);
			this.failedRecipientExceptions.Clear();
			exception = null;
			foreach (MailAddress mailAddress in recipients)
			{
				string smtpAddress = mailAddress.GetSmtpAddress(allowUnicode);
				string text = smtpAddress + (this.connection.DSNEnabled ? deliveryNotify : string.Empty);
				string text2;
				if (!RecipientCommand.Send(this.connection, text, out text2))
				{
					this.failedRecipientExceptions.Add(new SmtpFailedRecipientException(this.connection.Reader.StatusCode, smtpAddress, text2));
				}
			}
			if (this.failedRecipientExceptions.Count > 0)
			{
				if (this.failedRecipientExceptions.Count == 1)
				{
					exception = (SmtpFailedRecipientException)this.failedRecipientExceptions[0];
				}
				else
				{
					exception = new SmtpFailedRecipientsException(this.failedRecipientExceptions, this.failedRecipientExceptions.Count == recipients.Count);
				}
				if (this.failedRecipientExceptions.Count == recipients.Count)
				{
					exception.fatal = true;
					throw exception;
				}
			}
			DataCommand.Send(this.connection);
			return new MailWriter(this.connection.GetClosableStream());
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0007D3F8 File Offset: 0x0007B5F8
		internal void CloseIdleConnections(ServicePoint servicePoint)
		{
			ConnectionPoolManager.CleanupConnectionPool(servicePoint, "");
		}

		// Token: 0x0400187D RID: 6269
		internal const int DefaultPort = 25;

		// Token: 0x0400187E RID: 6270
		private ISmtpAuthenticationModule[] authenticationModules;

		// Token: 0x0400187F RID: 6271
		private SmtpConnection connection;

		// Token: 0x04001880 RID: 6272
		private SmtpClient client;

		// Token: 0x04001881 RID: 6273
		private ICredentialsByHost credentials;

		// Token: 0x04001882 RID: 6274
		private int timeout = 100000;

		// Token: 0x04001883 RID: 6275
		private ArrayList failedRecipientExceptions = new ArrayList();

		// Token: 0x04001884 RID: 6276
		private bool m_IdentityRequired;

		// Token: 0x04001885 RID: 6277
		private bool enableSsl;

		// Token: 0x04001886 RID: 6278
		private X509CertificateCollection clientCertificates;

		// Token: 0x04001887 RID: 6279
		private ServicePoint lastUsedServicePoint;
	}
}

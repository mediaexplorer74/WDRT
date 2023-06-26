using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Configuration;
using System.Net.NetworkInformation;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Mail
{
	/// <summary>Allows applications to send email by using the Simple Mail Transfer Protocol (SMTP).</summary>
	// Token: 0x0200027B RID: 635
	public class SmtpClient : IDisposable
	{
		/// <summary>Occurs when an asynchronous email send operation completes.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060017AC RID: 6060 RVA: 0x00078B34 File Offset: 0x00076D34
		// (remove) Token: 0x060017AD RID: 6061 RVA: 0x00078B6C File Offset: 0x00076D6C
		public event SendCompletedEventHandler SendCompleted;

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class by using configuration file settings.</summary>
		// Token: 0x060017AE RID: 6062 RVA: 0x00078BA4 File Offset: 0x00076DA4
		public SmtpClient()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, "SmtpClient", ".ctor", "");
			}
			try
			{
				this.Initialize();
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, "SmtpClient", ".ctor", this);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class that sends email by using the specified SMTP server.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host computer used for SMTP transactions.</param>
		// Token: 0x060017AF RID: 6063 RVA: 0x00078C10 File Offset: 0x00076E10
		public SmtpClient(string host)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, "SmtpClient", ".ctor", "host=" + host);
			}
			try
			{
				this.host = host;
				this.Initialize();
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, "SmtpClient", ".ctor", this);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class that sends email by using the specified SMTP server and port.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host used for SMTP transactions.</param>
		/// <param name="port">An <see cref="T:System.Int32" /> greater than zero that contains the port to be used on <paramref name="host" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> cannot be less than zero.</exception>
		// Token: 0x060017B0 RID: 6064 RVA: 0x00078C88 File Offset: 0x00076E88
		public SmtpClient(string host, int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, "SmtpClient", ".ctor", "host=" + host + ", port=" + port.ToString());
			}
			try
			{
				if (port < 0)
				{
					throw new ArgumentOutOfRangeException("port");
				}
				this.host = host;
				this.port = port;
				this.Initialize();
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, "SmtpClient", ".ctor", this);
				}
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00078D20 File Offset: 0x00076F20
		private void Initialize()
		{
			if (this.port == SmtpClient.defaultPort || this.port == 0)
			{
				new SmtpPermission(SmtpAccess.Connect).Demand();
			}
			else
			{
				new SmtpPermission(SmtpAccess.ConnectToUnrestrictedPort).Demand();
			}
			this.transport = new SmtpTransport(this);
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, this.transport);
			}
			this.onSendCompletedDelegate = new SendOrPostCallback(this.SendCompletedWaitCallback);
			if (SmtpClient.MailConfiguration.Smtp != null)
			{
				if (SmtpClient.MailConfiguration.Smtp.Network != null)
				{
					if (this.host == null || this.host.Length == 0)
					{
						this.host = SmtpClient.MailConfiguration.Smtp.Network.Host;
					}
					if (this.port == 0)
					{
						this.port = SmtpClient.MailConfiguration.Smtp.Network.Port;
					}
					this.transport.Credentials = SmtpClient.MailConfiguration.Smtp.Network.Credential;
					this.transport.EnableSsl = SmtpClient.MailConfiguration.Smtp.Network.EnableSsl;
					if (SmtpClient.MailConfiguration.Smtp.Network.TargetName != null)
					{
						this.targetName = SmtpClient.MailConfiguration.Smtp.Network.TargetName;
					}
					this.clientDomain = SmtpClient.MailConfiguration.Smtp.Network.ClientDomain;
				}
				this.deliveryFormat = SmtpClient.MailConfiguration.Smtp.DeliveryFormat;
				this.deliveryMethod = SmtpClient.MailConfiguration.Smtp.DeliveryMethod;
				if (SmtpClient.MailConfiguration.Smtp.SpecifiedPickupDirectory != null)
				{
					this.pickupDirectoryLocation = SmtpClient.MailConfiguration.Smtp.SpecifiedPickupDirectory.PickupDirectoryLocation;
				}
			}
			if (this.host != null && this.host.Length != 0)
			{
				this.host = this.host.Trim();
			}
			if (this.port == 0)
			{
				this.port = SmtpClient.defaultPort;
			}
			if (this.targetName == null)
			{
				this.targetName = "SMTPSVC/" + this.host;
			}
			if (this.clientDomain == null)
			{
				string text = IPGlobalProperties.InternalGetIPGlobalProperties().HostName;
				IdnMapping idnMapping = new IdnMapping();
				try
				{
					text = idnMapping.GetAscii(text);
				}
				catch (ArgumentException)
				{
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (char c in text)
				{
					if (c <= '\u007f')
					{
						stringBuilder.Append(c);
					}
				}
				if (stringBuilder.Length > 0)
				{
					this.clientDomain = stringBuilder.ToString();
					return;
				}
				this.clientDomain = "LocalHost";
			}
		}

		/// <summary>Gets or sets the name or IP address of the host used for SMTP transactions.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name or IP address of the computer to use for SMTP transactions.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is equal to <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x00078FC0 File Offset: 0x000771C0
		// (set) Token: 0x060017B3 RID: 6067 RVA: 0x00078FC8 File Offset: 0x000771C8
		public string Host
		{
			get
			{
				return this.host;
			}
			set
			{
				if (this.InCall)
				{
					throw new InvalidOperationException(System.SR.GetString("SmtpInvalidOperationDuringSend"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == string.Empty)
				{
					throw new ArgumentException(System.SR.GetString("net_emptystringset"), "value");
				}
				value = value.Trim();
				if (value != this.host)
				{
					this.host = value;
					this.servicePointChanged = true;
				}
			}
		}

		/// <summary>Gets or sets the port used for SMTP transactions.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the port number on the SMTP host. The default value is 25.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x00079041 File Offset: 0x00077241
		// (set) Token: 0x060017B5 RID: 6069 RVA: 0x0007904C File Offset: 0x0007724C
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				if (this.InCall)
				{
					throw new InvalidOperationException(System.SR.GetString("SmtpInvalidOperationDuringSend"));
				}
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != SmtpClient.defaultPort)
				{
					new SmtpPermission(SmtpAccess.ConnectToUnrestrictedPort).Demand();
				}
				if (value != this.port)
				{
					this.port = value;
					this.servicePointChanged = true;
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the default credentials are used; otherwise <see langword="false" />. The default value is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000790AA File Offset: 0x000772AA
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x000790C1 File Offset: 0x000772C1
		public bool UseDefaultCredentials
		{
			get
			{
				return this.transport.Credentials is SystemNetworkCredential;
			}
			set
			{
				if (this.InCall)
				{
					throw new InvalidOperationException(System.SR.GetString("SmtpInvalidOperationDuringSend"));
				}
				this.transport.Credentials = (value ? CredentialCache.DefaultNetworkCredentials : null);
			}
		}

		/// <summary>Gets or sets the credentials used to authenticate the sender.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentialsByHost" /> that represents the credentials to use for authentication; or <see langword="null" /> if no credentials have been specified.</returns>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000790F1 File Offset: 0x000772F1
		// (set) Token: 0x060017B9 RID: 6073 RVA: 0x000790FE File Offset: 0x000772FE
		public ICredentialsByHost Credentials
		{
			get
			{
				return this.transport.Credentials;
			}
			set
			{
				if (this.InCall)
				{
					throw new InvalidOperationException(System.SR.GetString("SmtpInvalidOperationDuringSend"));
				}
				this.transport.Credentials = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the amount of time after which a synchronous <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> call times out.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that specifies the time-out value in milliseconds. The default value is 100,000 (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation was less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x00079124 File Offset: 0x00077324
		// (set) Token: 0x060017BB RID: 6075 RVA: 0x00079131 File Offset: 0x00077331
		public int Timeout
		{
			get
			{
				return this.transport.Timeout;
			}
			set
			{
				if (this.InCall)
				{
					throw new InvalidOperationException(System.SR.GetString("SmtpInvalidOperationDuringSend"));
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.transport.Timeout = value;
			}
		}

		/// <summary>Gets the network connection used to transmit the email message.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> that connects to the <see cref="P:System.Net.Mail.SmtpClient.Host" /> property used for SMTP.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Net.Mail.SmtpClient.Host" /> is <see langword="null" /> or the empty string ("").  
		/// -or-  
		/// <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero.</exception>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x00079166 File Offset: 0x00077366
		public ServicePoint ServicePoint
		{
			get
			{
				this.CheckHostAndPort();
				if (this.servicePoint == null || this.servicePointChanged)
				{
					this.servicePoint = ServicePointManager.FindServicePoint(this.host, this.port);
					this.servicePointChanged = false;
				}
				return this.servicePoint;
			}
		}

		/// <summary>Specifies how outgoing email messages will be handled.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpDeliveryMethod" /> that indicates how email messages are delivered.</returns>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000791A2 File Offset: 0x000773A2
		// (set) Token: 0x060017BE RID: 6078 RVA: 0x000791AA File Offset: 0x000773AA
		public SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return this.deliveryMethod;
			}
			set
			{
				this.deliveryMethod = value;
			}
		}

		/// <summary>Gets or sets the delivery format used by <see cref="T:System.Net.Mail.SmtpClient" /> to send email.</summary>
		/// <returns>The delivery format used by <see cref="T:System.Net.Mail.SmtpClient" />.</returns>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x000791B3 File Offset: 0x000773B3
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x000791BB File Offset: 0x000773BB
		public SmtpDeliveryFormat DeliveryFormat
		{
			get
			{
				return this.deliveryFormat;
			}
			set
			{
				this.deliveryFormat = value;
			}
		}

		/// <summary>Gets or sets the folder where applications save mail messages to be processed by the local SMTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the pickup directory for mail messages.</returns>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x000791C4 File Offset: 0x000773C4
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x000791CC File Offset: 0x000773CC
		public string PickupDirectoryLocation
		{
			get
			{
				return this.pickupDirectoryLocation;
			}
			set
			{
				this.pickupDirectoryLocation = value;
			}
		}

		/// <summary>Specify whether the <see cref="T:System.Net.Mail.SmtpClient" /> uses Secure Sockets Layer (SSL) to encrypt the connection.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Mail.SmtpClient" /> uses SSL; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x000791D5 File Offset: 0x000773D5
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x000791E2 File Offset: 0x000773E2
		public bool EnableSsl
		{
			get
			{
				return this.transport.EnableSsl;
			}
			set
			{
				this.transport.EnableSsl = value;
			}
		}

		/// <summary>Specify which certificates should be used to establish the Secure Sockets Layer (SSL) connection.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />, holding one or more client certificates. The default value is derived from the mail configuration attributes in a configuration file.</returns>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x000791F0 File Offset: 0x000773F0
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.transport.ClientCertificates;
			}
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) to use for authentication when using extended protection.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the SPN to use for extended protection. The default value for this SPN is of the form "SMTPSVC/&lt;host&gt;" where &lt;host&gt; is the hostname of the SMTP mail server.</returns>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x00079206 File Offset: 0x00077406
		// (set) Token: 0x060017C6 RID: 6086 RVA: 0x000791FD File Offset: 0x000773FD
		public string TargetName
		{
			get
			{
				return this.targetName;
			}
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0007920E File Offset: 0x0007740E
		private bool ServerSupportsEai
		{
			get
			{
				return this.transport.ServerSupportsEai;
			}
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0007921B File Offset: 0x0007741B
		private bool IsUnicodeSupported()
		{
			if (this.DeliveryMethod == SmtpDeliveryMethod.Network)
			{
				return this.ServerSupportsEai && this.DeliveryFormat == SmtpDeliveryFormat.International;
			}
			return this.DeliveryFormat == SmtpDeliveryFormat.International;
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00079244 File Offset: 0x00077444
		internal MailWriter GetFileMailWriter(string pickupDirectory)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, "SmtpClient.Send() pickupDirectory=" + pickupDirectory);
			}
			if (!Path.IsPathRooted(pickupDirectory))
			{
				throw new SmtpException(System.SR.GetString("SmtpNeedAbsolutePickupDirectory"));
			}
			string text2;
			do
			{
				string text = Guid.NewGuid().ToString() + ".eml";
				text2 = Path.Combine(pickupDirectory, text);
			}
			while (File.Exists(text2));
			FileStream fileStream = new FileStream(text2, FileMode.CreateNew);
			return new MailWriter(fileStream);
		}

		/// <summary>Raises the <see cref="E:System.Net.Mail.SmtpClient.SendCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains event data.</param>
		// Token: 0x060017CB RID: 6091 RVA: 0x000792C1 File Offset: 0x000774C1
		protected void OnSendCompleted(AsyncCompletedEventArgs e)
		{
			if (this.SendCompleted != null)
			{
				this.SendCompleted(this, e);
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x000792D8 File Offset: 0x000774D8
		private void SendCompletedWaitCallback(object operationState)
		{
			this.OnSendCompleted((AsyncCompletedEventArgs)operationState);
		}

		/// <summary>Sends the specified email message to an SMTP server for delivery. The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the addresses that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="recipients" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.  
		/// -or-  
		/// <paramref name="recipients" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is <see langword="null" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.  
		///  -or-  
		///  Authentication failed.  
		///  -or-  
		///  The operation timed out.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true" /> but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true," /> but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientException">The <paramref name="message" /> could not be delivered to one of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The <paramref name="message" /> could not be delivered to two or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x060017CD RID: 6093 RVA: 0x000792E8 File Offset: 0x000774E8
		public void Send(string from, string recipients, string subject, string body)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			MailMessage mailMessage = new MailMessage(from, recipients, subject, body);
			this.Send(mailMessage);
		}

		/// <summary>Sends the specified message to an SMTP server for delivery.</summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.MailMessage.From" /> is <see langword="null" />.  
		///  -or-  
		///  There are no recipients specified in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, and <see cref="P:System.Net.Mail.MailMessage.Bcc" /> properties.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is <see langword="null" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.  
		///  -or-  
		///  Authentication failed.  
		///  -or-  
		///  The operation timed out.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true" /> but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true," /> but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientException">The <paramref name="message" /> could not be delivered to one of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The <paramref name="message" /> could not be delivered to two or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x060017CE RID: 6094 RVA: 0x00079320 File Offset: 0x00077520
		public void Send(MailMessage message)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "Send", message);
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			try
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.Web, this, "Send", "DeliveryMethod=" + this.DeliveryMethod.ToString());
				}
				if (Logging.On)
				{
					Logging.Associate(Logging.Web, this, message);
				}
				SmtpFailedRecipientException ex = null;
				if (this.InCall)
				{
					throw new InvalidOperationException(System.SR.GetString("net_inasync"));
				}
				if (message == null)
				{
					throw new ArgumentNullException("message");
				}
				if (this.DeliveryMethod == SmtpDeliveryMethod.Network)
				{
					this.CheckHostAndPort();
				}
				MailAddressCollection mailAddressCollection = new MailAddressCollection();
				if (message.From == null)
				{
					throw new InvalidOperationException(System.SR.GetString("SmtpFromRequired"));
				}
				if (message.To != null)
				{
					foreach (MailAddress mailAddress in message.To)
					{
						mailAddressCollection.Add(mailAddress);
					}
				}
				if (message.Bcc != null)
				{
					foreach (MailAddress mailAddress2 in message.Bcc)
					{
						mailAddressCollection.Add(mailAddress2);
					}
				}
				if (message.CC != null)
				{
					foreach (MailAddress mailAddress3 in message.CC)
					{
						mailAddressCollection.Add(mailAddress3);
					}
				}
				if (mailAddressCollection.Count == 0)
				{
					throw new InvalidOperationException(System.SR.GetString("SmtpRecipientRequired"));
				}
				this.transport.IdentityRequired = false;
				try
				{
					this.InCall = true;
					this.timedOut = false;
					this.timer = new Timer(new TimerCallback(this.TimeOutCallback), null, this.Timeout, this.Timeout);
					string pickupDirectory = this.PickupDirectoryLocation;
					switch (this.DeliveryMethod)
					{
					case SmtpDeliveryMethod.Network:
						goto IL_241;
					case SmtpDeliveryMethod.SpecifiedPickupDirectory:
						break;
					case SmtpDeliveryMethod.PickupDirectoryFromIis:
						pickupDirectory = IisPickupDirectory.GetPickupDirectory();
						break;
					default:
						goto IL_241;
					}
					if (this.EnableSsl)
					{
						throw new SmtpException(System.SR.GetString("SmtpPickupDirectoryDoesnotSupportSsl"));
					}
					bool flag = this.IsUnicodeSupported();
					this.ValidateUnicodeRequirement(message, mailAddressCollection, flag);
					MailWriter mailWriter = this.GetFileMailWriter(pickupDirectory);
					goto IL_281;
					IL_241:
					this.GetConnection();
					flag = this.IsUnicodeSupported();
					this.ValidateUnicodeRequirement(message, mailAddressCollection, flag);
					mailWriter = this.transport.SendMail(message.Sender ?? message.From, mailAddressCollection, message.BuildDeliveryStatusNotificationString(), flag, out ex);
					IL_281:
					this.message = message;
					message.Send(mailWriter, this.DeliveryMethod > SmtpDeliveryMethod.Network, flag);
					mailWriter.Close();
					this.transport.ReleaseConnection();
					if (this.DeliveryMethod == SmtpDeliveryMethod.Network && ex != null)
					{
						throw ex;
					}
				}
				catch (Exception ex2)
				{
					if (Logging.On)
					{
						Logging.Exception(Logging.Web, this, "Send", ex2);
					}
					if (ex2 is SmtpFailedRecipientException && !((SmtpFailedRecipientException)ex2).fatal)
					{
						throw;
					}
					this.Abort();
					if (this.timedOut)
					{
						throw new SmtpException(System.SR.GetString("net_timeout"));
					}
					if (ex2 is SecurityException || ex2 is AuthenticationException || ex2 is SmtpException)
					{
						throw;
					}
					throw new SmtpException(System.SR.GetString("SmtpSendMailFailure"), ex2);
				}
				finally
				{
					this.InCall = false;
					if (this.timer != null)
					{
						this.timer.Dispose();
					}
				}
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "Send", null);
				}
			}
		}

		/// <summary>Sends an email message to an SMTP server for delivery. The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects. This method does not block the calling thread and allows the caller to pass an object to the method that is invoked when the operation completes.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the address that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="recipient" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.  
		/// -or-  
		/// <paramref name="recipient" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is <see langword="null" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.  
		///  -or-  
		///  Authentication failed.  
		///  -or-  
		///  The operation timed out.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true" /> but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true," /> but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.  
		///  -or-  
		///  The message could not be delivered to one or more of the recipients in <paramref name="recipients" />.</exception>
		// Token: 0x060017CF RID: 6095 RVA: 0x00079738 File Offset: 0x00077938
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string from, string recipients, string subject, string body, object userToken)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			this.SendAsync(new MailMessage(from, recipients, subject, body), userToken);
		}

		/// <summary>Sends the specified email message to an SMTP server for delivery. This method does not block the calling thread and allows the caller to pass an object to the method that is invoked when the operation completes.</summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is <see langword="null" />.  
		/// -or-  
		/// <see cref="P:System.Net.Mail.MailMessage.From" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.  
		///  -or-  
		///  There are no recipients specified in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, and <see cref="P:System.Net.Mail.MailMessage.Bcc" /> properties.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is <see langword="null" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.  
		///  -or-  
		///  Authentication failed.  
		///  -or-  
		///  The operation timed out.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true" /> but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true," /> but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.  
		///  -or-  
		///  The <paramref name="message" /> could not be delivered to one or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x060017D0 RID: 6096 RVA: 0x00079768 File Offset: 0x00077968
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(MailMessage message, object userToken)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "SendAsync", "DeliveryMethod=" + this.DeliveryMethod.ToString());
			}
			try
			{
				if (this.InCall)
				{
					throw new InvalidOperationException(System.SR.GetString("net_inasync"));
				}
				if (message == null)
				{
					throw new ArgumentNullException("message");
				}
				if (this.DeliveryMethod == SmtpDeliveryMethod.Network)
				{
					this.CheckHostAndPort();
				}
				this.recipients = new MailAddressCollection();
				if (message.From == null)
				{
					throw new InvalidOperationException(System.SR.GetString("SmtpFromRequired"));
				}
				if (message.To != null)
				{
					foreach (MailAddress mailAddress in message.To)
					{
						this.recipients.Add(mailAddress);
					}
				}
				if (message.Bcc != null)
				{
					foreach (MailAddress mailAddress2 in message.Bcc)
					{
						this.recipients.Add(mailAddress2);
					}
				}
				if (message.CC != null)
				{
					foreach (MailAddress mailAddress3 in message.CC)
					{
						this.recipients.Add(mailAddress3);
					}
				}
				if (this.recipients.Count == 0)
				{
					throw new InvalidOperationException(System.SR.GetString("SmtpRecipientRequired"));
				}
				try
				{
					this.InCall = true;
					this.cancelled = false;
					this.message = message;
					string pickupDirectory = this.PickupDirectoryLocation;
					CredentialCache credentialCache;
					this.transport.IdentityRequired = this.Credentials != null && (this.Credentials is SystemNetworkCredential || (credentialCache = this.Credentials as CredentialCache) == null || credentialCache.IsDefaultInCache);
					this.asyncOp = AsyncOperationManager.CreateOperation(userToken);
					switch (this.DeliveryMethod)
					{
					case SmtpDeliveryMethod.Network:
						goto IL_2AB;
					case SmtpDeliveryMethod.SpecifiedPickupDirectory:
						break;
					case SmtpDeliveryMethod.PickupDirectoryFromIis:
						pickupDirectory = IisPickupDirectory.GetPickupDirectory();
						break;
					default:
						goto IL_2AB;
					}
					if (this.EnableSsl)
					{
						throw new SmtpException(System.SR.GetString("SmtpPickupDirectoryDoesnotSupportSsl"));
					}
					this.writer = this.GetFileMailWriter(pickupDirectory);
					bool flag = this.IsUnicodeSupported();
					this.ValidateUnicodeRequirement(message, this.recipients, flag);
					message.Send(this.writer, true, flag);
					if (this.writer != null)
					{
						this.writer.Close();
					}
					this.transport.ReleaseConnection();
					AsyncCompletedEventArgs asyncCompletedEventArgs = new AsyncCompletedEventArgs(null, false, this.asyncOp.UserSuppliedState);
					this.InCall = false;
					this.asyncOp.PostOperationCompleted(this.onSendCompletedDelegate, asyncCompletedEventArgs);
					goto IL_326;
					IL_2AB:
					this.operationCompletedResult = new ContextAwareResult(this.transport.IdentityRequired, true, null, this, SmtpClient._ContextSafeCompleteCallback);
					object obj = this.operationCompletedResult.StartPostingAsyncOp();
					lock (obj)
					{
						this.transport.BeginGetConnection(this.ServicePoint, this.operationCompletedResult, new AsyncCallback(this.ConnectCallback), this.operationCompletedResult);
						this.operationCompletedResult.FinishPostingAsyncOp();
					}
					IL_326:;
				}
				catch (Exception ex)
				{
					this.InCall = false;
					if (Logging.On)
					{
						Logging.Exception(Logging.Web, this, "Send", ex);
					}
					if (ex is SmtpFailedRecipientException && !((SmtpFailedRecipientException)ex).fatal)
					{
						throw;
					}
					this.Abort();
					if (this.timedOut)
					{
						throw new SmtpException(System.SR.GetString("net_timeout"));
					}
					if (ex is SecurityException || ex is AuthenticationException || ex is SmtpException)
					{
						throw;
					}
					throw new SmtpException(System.SR.GetString("SmtpSendMailFailure"), ex);
				}
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "SendAsync", null);
				}
			}
		}

		/// <summary>Cancels an asynchronous operation to send an email message.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x060017D1 RID: 6097 RVA: 0x00079BD8 File Offset: 0x00077DD8
		public void SendAsyncCancel()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "SendAsyncCancel", null);
			}
			try
			{
				if (this.InCall && !this.cancelled)
				{
					this.cancelled = true;
					this.Abort();
				}
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "SendAsyncCancel", null);
				}
			}
		}

		/// <summary>Sends the specified message to an SMTP server for delivery as an asynchronous operation. . The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the addresses that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="recipients" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.  
		/// -or-  
		/// <paramref name="recipients" /> is <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x060017D2 RID: 6098 RVA: 0x00079C64 File Offset: 0x00077E64
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task SendMailAsync(string from, string recipients, string subject, string body)
		{
			MailMessage mailMessage = new MailMessage(from, recipients, subject, body);
			return this.SendMailAsync(mailMessage);
		}

		/// <summary>Sends the specified message to an SMTP server for delivery as an asynchronous operation.</summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is <see langword="null" />.</exception>
		// Token: 0x060017D3 RID: 6099 RVA: 0x00079C84 File Offset: 0x00077E84
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task SendMailAsync(MailMessage message)
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			SendCompletedEventHandler handler = null;
			handler = delegate(object sender, AsyncCompletedEventArgs e)
			{
				this.HandleCompletion(tcs, e, handler);
			};
			this.SendCompleted += handler;
			try
			{
				this.SendAsync(message, tcs);
			}
			catch
			{
				this.SendCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00079D08 File Offset: 0x00077F08
		private void HandleCompletion(TaskCompletionSource<object> tcs, AsyncCompletedEventArgs e, SendCompletedEventHandler handler)
		{
			if (e.UserState == tcs)
			{
				try
				{
					this.SendCompleted -= handler;
				}
				finally
				{
					if (e.Error != null)
					{
						tcs.TrySetException(e.Error);
					}
					else if (e.Cancelled)
					{
						tcs.TrySetCanceled();
					}
					else
					{
						tcs.TrySetResult(null);
					}
				}
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x00079D68 File Offset: 0x00077F68
		// (set) Token: 0x060017D6 RID: 6102 RVA: 0x00079D70 File Offset: 0x00077F70
		internal bool InCall
		{
			get
			{
				return this.inCall;
			}
			set
			{
				this.inCall = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x00079D79 File Offset: 0x00077F79
		internal static MailSettingsSectionGroupInternal MailConfiguration
		{
			get
			{
				if (SmtpClient.mailConfiguration == null)
				{
					SmtpClient.mailConfiguration = MailSettingsSectionGroupInternal.GetSection();
				}
				return SmtpClient.mailConfiguration;
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00079D98 File Offset: 0x00077F98
		private void CheckHostAndPort()
		{
			if (this.host == null || this.host.Length == 0)
			{
				throw new InvalidOperationException(System.SR.GetString("UnspecifiedHost"));
			}
			if (this.port <= 0 || this.port > 65535)
			{
				throw new InvalidOperationException(System.SR.GetString("InvalidPort"));
			}
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00079DF0 File Offset: 0x00077FF0
		private void TimeOutCallback(object state)
		{
			if (!this.timedOut)
			{
				this.timedOut = true;
				this.Abort();
			}
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00079E08 File Offset: 0x00078008
		private void Complete(Exception exception, IAsyncResult result)
		{
			ContextAwareResult contextAwareResult = (ContextAwareResult)result.AsyncState;
			try
			{
				if (this.cancelled)
				{
					exception = null;
					this.Abort();
				}
				else if (exception != null && (!(exception is SmtpFailedRecipientException) || ((SmtpFailedRecipientException)exception).fatal))
				{
					this.Abort();
					if (!(exception is SmtpException))
					{
						exception = new SmtpException(System.SR.GetString("SmtpSendMailFailure"), exception);
					}
				}
				else
				{
					if (this.writer != null)
					{
						try
						{
							this.writer.Close();
						}
						catch (SmtpException ex)
						{
							exception = ex;
						}
					}
					this.transport.ReleaseConnection();
				}
			}
			finally
			{
				contextAwareResult.InvokeCallback(exception);
			}
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00079EBC File Offset: 0x000780BC
		private static void ContextSafeCompleteCallback(IAsyncResult ar)
		{
			ContextAwareResult contextAwareResult = (ContextAwareResult)ar;
			SmtpClient smtpClient = (SmtpClient)ar.AsyncState;
			Exception ex = contextAwareResult.Result as Exception;
			AsyncOperation asyncOperation = smtpClient.asyncOp;
			AsyncCompletedEventArgs asyncCompletedEventArgs = new AsyncCompletedEventArgs(ex, smtpClient.cancelled, asyncOperation.UserSuppliedState);
			smtpClient.InCall = false;
			smtpClient.failedRecipientException = null;
			asyncOperation.PostOperationCompleted(smtpClient.onSendCompletedDelegate, asyncCompletedEventArgs);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00079F20 File Offset: 0x00078120
		private void SendMessageCallback(IAsyncResult result)
		{
			try
			{
				this.message.EndSend(result);
				this.Complete(this.failedRecipientException, result);
			}
			catch (Exception ex)
			{
				this.Complete(ex, result);
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00079F64 File Offset: 0x00078164
		private void SendMailCallback(IAsyncResult result)
		{
			try
			{
				this.writer = this.transport.EndSendMail(result);
				SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result;
				this.failedRecipientException = sendMailAsyncResult.GetFailedRecipientException();
			}
			catch (Exception ex)
			{
				this.Complete(ex, result);
				return;
			}
			try
			{
				if (this.cancelled)
				{
					this.Complete(null, result);
				}
				else
				{
					this.message.BeginSend(this.writer, this.DeliveryMethod > SmtpDeliveryMethod.Network, this.ServerSupportsEai, new AsyncCallback(this.SendMessageCallback), result.AsyncState);
				}
			}
			catch (Exception ex2)
			{
				this.Complete(ex2, result);
			}
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0007A014 File Offset: 0x00078214
		private void ConnectCallback(IAsyncResult result)
		{
			try
			{
				this.transport.EndGetConnection(result);
				if (this.cancelled)
				{
					this.Complete(null, result);
				}
				else
				{
					bool flag = this.IsUnicodeSupported();
					this.ValidateUnicodeRequirement(this.message, this.recipients, flag);
					this.transport.BeginSendMail(this.message.Sender ?? this.message.From, this.recipients, this.message.BuildDeliveryStatusNotificationString(), flag, new AsyncCallback(this.SendMailCallback), result.AsyncState);
				}
			}
			catch (Exception ex)
			{
				this.Complete(ex, result);
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0007A0C0 File Offset: 0x000782C0
		private void ValidateUnicodeRequirement(MailMessage message, MailAddressCollection recipients, bool allowUnicode)
		{
			foreach (MailAddress mailAddress in recipients)
			{
				mailAddress.GetSmtpAddress(allowUnicode);
			}
			if (message.Sender != null)
			{
				message.Sender.GetSmtpAddress(allowUnicode);
			}
			message.From.GetSmtpAddress(allowUnicode);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0007A12C File Offset: 0x0007832C
		private void GetConnection()
		{
			if (!this.transport.IsConnected)
			{
				this.transport.GetConnection(this.ServicePoint);
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0007A14C File Offset: 0x0007834C
		private void Abort()
		{
			try
			{
				this.transport.Abort();
			}
			catch
			{
			}
		}

		/// <summary>Sends a QUIT message to the SMTP server, gracefully ends the TCP connection, and releases all resources used by the current instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
		// Token: 0x060017E2 RID: 6114 RVA: 0x0007A17C File Offset: 0x0007837C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Sends a QUIT message to the SMTP server, gracefully ends the TCP connection, releases all resources used by the current instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x060017E3 RID: 6115 RVA: 0x0007A18C File Offset: 0x0007838C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				if (this.InCall && !this.cancelled)
				{
					this.cancelled = true;
					this.Abort();
				}
				if (this.transport != null && this.servicePoint != null)
				{
					this.transport.CloseIdleConnections(this.servicePoint);
				}
				if (this.timer != null)
				{
					this.timer.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x040017F0 RID: 6128
		private string host;

		// Token: 0x040017F1 RID: 6129
		private int port;

		// Token: 0x040017F2 RID: 6130
		private bool inCall;

		// Token: 0x040017F3 RID: 6131
		private bool cancelled;

		// Token: 0x040017F4 RID: 6132
		private bool timedOut;

		// Token: 0x040017F5 RID: 6133
		private string targetName;

		// Token: 0x040017F6 RID: 6134
		private SmtpDeliveryMethod deliveryMethod;

		// Token: 0x040017F7 RID: 6135
		private SmtpDeliveryFormat deliveryFormat;

		// Token: 0x040017F8 RID: 6136
		private string pickupDirectoryLocation;

		// Token: 0x040017F9 RID: 6137
		private SmtpTransport transport;

		// Token: 0x040017FA RID: 6138
		private MailMessage message;

		// Token: 0x040017FB RID: 6139
		private MailWriter writer;

		// Token: 0x040017FC RID: 6140
		private MailAddressCollection recipients;

		// Token: 0x040017FD RID: 6141
		private SendOrPostCallback onSendCompletedDelegate;

		// Token: 0x040017FE RID: 6142
		private Timer timer;

		// Token: 0x040017FF RID: 6143
		private static volatile MailSettingsSectionGroupInternal mailConfiguration;

		// Token: 0x04001800 RID: 6144
		private ContextAwareResult operationCompletedResult;

		// Token: 0x04001801 RID: 6145
		private AsyncOperation asyncOp;

		// Token: 0x04001802 RID: 6146
		private static AsyncCallback _ContextSafeCompleteCallback = new AsyncCallback(SmtpClient.ContextSafeCompleteCallback);

		// Token: 0x04001803 RID: 6147
		private static int defaultPort = 25;

		// Token: 0x04001804 RID: 6148
		internal string clientDomain;

		// Token: 0x04001805 RID: 6149
		private bool disposed;

		// Token: 0x04001806 RID: 6150
		private bool servicePointChanged;

		// Token: 0x04001807 RID: 6151
		private ServicePoint servicePoint;

		// Token: 0x04001808 RID: 6152
		private SmtpFailedRecipientException failedRecipientException;

		// Token: 0x04001809 RID: 6153
		private const int maxPortValue = 65535;
	}
}

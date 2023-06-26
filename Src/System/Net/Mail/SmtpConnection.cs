using System;
using System.Globalization;
using System.IO;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Net.Mail
{
	// Token: 0x02000288 RID: 648
	internal class SmtpConnection
	{
		// Token: 0x0600181E RID: 6174 RVA: 0x0007AEED File Offset: 0x000790ED
		private static PooledStream CreateSmtpPooledStream(ConnectionPool pool)
		{
			return new SmtpPooledStream(pool, TimeSpan.MaxValue, false);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0007AEFC File Offset: 0x000790FC
		internal SmtpConnection(SmtpTransport parent, SmtpClient client, ICredentialsByHost credentials, ISmtpAuthenticationModule[] authenticationModules)
		{
			this.client = client;
			this.credentials = credentials;
			this.authenticationModules = authenticationModules;
			this.parent = parent;
			this.onCloseHandler = new EventHandler(this.OnClose);
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0007AF54 File Offset: 0x00079154
		internal BufferBuilder BufferBuilder
		{
			get
			{
				return this.bufferBuilder;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x0007AF5C File Offset: 0x0007915C
		internal bool IsConnected
		{
			get
			{
				return this.isConnected;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x0007AF64 File Offset: 0x00079164
		internal bool IsStreamOpen
		{
			get
			{
				return this.isStreamOpen;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x0007AF6C File Offset: 0x0007916C
		internal bool DSNEnabled
		{
			get
			{
				return this.pooledStream != null && ((SmtpPooledStream)this.pooledStream).dsnEnabled;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0007AF88 File Offset: 0x00079188
		internal SmtpReplyReaderFactory Reader
		{
			get
			{
				return this.responseReader;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0007AF90 File Offset: 0x00079190
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x0007AF98 File Offset: 0x00079198
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

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x0007AFA1 File Offset: 0x000791A1
		// (set) Token: 0x06001828 RID: 6184 RVA: 0x0007AFA9 File Offset: 0x000791A9
		internal int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x0007AFB2 File Offset: 0x000791B2
		// (set) Token: 0x0600182A RID: 6186 RVA: 0x0007AFBA File Offset: 0x000791BA
		internal X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.clientCertificates;
			}
			set
			{
				this.clientCertificates = value;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x0007AFC4 File Offset: 0x000791C4
		internal bool ServerSupportsEai
		{
			get
			{
				SmtpPooledStream smtpPooledStream = (SmtpPooledStream)this.pooledStream;
				return smtpPooledStream.serverSupportsEai;
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0007AFE4 File Offset: 0x000791E4
		internal IAsyncResult BeginGetConnection(ServicePoint servicePoint, ContextAwareResult outerResult, AsyncCallback callback, object state)
		{
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, servicePoint);
			}
			if (this.EnableSsl && this.ClientCertificates != null && this.ClientCertificates.Count > 0)
			{
				this.connectionPool = ConnectionPoolManager.GetConnectionPool(servicePoint, this.ClientCertificates.GetHashCode().ToString(NumberFormatInfo.InvariantInfo), SmtpConnection.m_CreateConnectionCallback);
			}
			else
			{
				this.connectionPool = ConnectionPoolManager.GetConnectionPool(servicePoint, "", SmtpConnection.m_CreateConnectionCallback);
			}
			SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = new SmtpConnection.ConnectAndHandshakeAsyncResult(this, servicePoint.Host, servicePoint.Port, outerResult, callback, state);
			connectAndHandshakeAsyncResult.GetConnection(false);
			return connectAndHandshakeAsyncResult;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0007B082 File Offset: 0x00079282
		internal IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			return this.pooledStream.UnsafeBeginWrite(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length, callback, state);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0007B0A8 File Offset: 0x000792A8
		internal void EndFlush(IAsyncResult result)
		{
			this.pooledStream.EndWrite(result);
			this.bufferBuilder.Reset();
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0007B0C1 File Offset: 0x000792C1
		internal void Flush()
		{
			this.pooledStream.Write(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length);
			this.bufferBuilder.Reset();
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0007B0F0 File Offset: 0x000792F0
		internal void ReleaseConnection()
		{
			if (!this.isClosed)
			{
				lock (this)
				{
					if (!this.isClosed && this.pooledStream != null)
					{
						if (this.channelBindingToken != null)
						{
							this.channelBindingToken.Close();
						}
						((SmtpPooledStream)this.pooledStream).previouslyUsed = true;
						this.connectionPool.PutConnection(this.pooledStream, this.pooledStream.Owner, this.Timeout);
					}
					this.isClosed = true;
				}
			}
			this.isConnected = false;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0007B194 File Offset: 0x00079394
		internal void Abort()
		{
			if (!this.isClosed)
			{
				lock (this)
				{
					if (!this.isClosed && this.pooledStream != null)
					{
						if (this.channelBindingToken != null)
						{
							this.channelBindingToken.Close();
						}
						this.pooledStream.Close(0);
						this.connectionPool.PutConnection(this.pooledStream, this.pooledStream.Owner, this.Timeout, false);
					}
					this.isClosed = true;
				}
			}
			this.isConnected = false;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0007B234 File Offset: 0x00079434
		internal void ParseExtensions(string[] extensions)
		{
			this.supportedAuth = SupportedAuth.None;
			foreach (string text in extensions)
			{
				if (string.Compare(text, 0, "auth", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
				{
					string[] array = text.Remove(0, 4).Split(new char[] { ' ', '=' }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string text2 in array)
					{
						if (string.Compare(text2, "login", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.supportedAuth |= SupportedAuth.Login;
						}
						else if (string.Compare(text2, "ntlm", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.supportedAuth |= SupportedAuth.NTLM;
						}
						else if (string.Compare(text2, "gssapi", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.supportedAuth |= SupportedAuth.GSSAPI;
						}
						else if (string.Compare(text2, "wdigest", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.supportedAuth |= SupportedAuth.WDigest;
						}
					}
				}
				else if (string.Compare(text, 0, "dsn ", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
				{
					((SmtpPooledStream)this.pooledStream).dsnEnabled = true;
				}
				else if (string.Compare(text, 0, "STARTTLS", 0, 8, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.serverSupportsStartTls = true;
				}
				else if (string.Compare(text, 0, "SMTPUTF8", 0, 8, StringComparison.OrdinalIgnoreCase) == 0)
				{
					((SmtpPooledStream)this.pooledStream).serverSupportsEai = true;
				}
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0007B394 File Offset: 0x00079594
		internal bool AuthSupported(ISmtpAuthenticationModule module)
		{
			if (module is SmtpLoginAuthenticationModule)
			{
				if ((this.supportedAuth & SupportedAuth.Login) > SupportedAuth.None)
				{
					return true;
				}
			}
			else if (module is SmtpNegotiateAuthenticationModule)
			{
				if ((this.supportedAuth & SupportedAuth.GSSAPI) > SupportedAuth.None)
				{
					this.sawNegotiate = true;
					return true;
				}
			}
			else if (module is SmtpNtlmAuthenticationModule)
			{
				if (!this.sawNegotiate && (this.supportedAuth & SupportedAuth.NTLM) > SupportedAuth.None)
				{
					return true;
				}
			}
			else if (module is SmtpDigestAuthenticationModule && (this.supportedAuth & SupportedAuth.WDigest) > SupportedAuth.None)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0007B408 File Offset: 0x00079608
		internal void GetConnection(ServicePoint servicePoint)
		{
			if (this.isConnected)
			{
				throw new InvalidOperationException(System.SR.GetString("SmtpAlreadyConnected"));
			}
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, servicePoint);
			}
			this.connectionPool = ConnectionPoolManager.GetConnectionPool(servicePoint, "", SmtpConnection.m_CreateConnectionCallback);
			PooledStream pooledStream = this.connectionPool.GetConnection(this, null, this.Timeout);
			while (((SmtpPooledStream)pooledStream).creds != null && ((SmtpPooledStream)pooledStream).creds != this.credentials)
			{
				this.connectionPool.PutConnection(pooledStream, pooledStream.Owner, this.Timeout, false);
				pooledStream = this.connectionPool.GetConnection(this, null, this.Timeout);
			}
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, pooledStream);
			}
			lock (this)
			{
				this.pooledStream = pooledStream;
			}
			((SmtpPooledStream)pooledStream).creds = this.credentials;
			this.responseReader = new SmtpReplyReaderFactory(pooledStream.NetworkStream);
			pooledStream.UpdateLifetime();
			if (((SmtpPooledStream)pooledStream).previouslyUsed)
			{
				this.isConnected = true;
				return;
			}
			LineInfo lineInfo = this.responseReader.GetNextReplyReader().ReadLine();
			SmtpStatusCode statusCode = lineInfo.StatusCode;
			if (statusCode != SmtpStatusCode.ServiceReady)
			{
				throw new SmtpException(lineInfo.StatusCode, lineInfo.Line, true);
			}
			try
			{
				this.extensions = EHelloCommand.Send(this, this.client.clientDomain);
				this.ParseExtensions(this.extensions);
			}
			catch (SmtpException ex)
			{
				if (ex.StatusCode != SmtpStatusCode.CommandUnrecognized && ex.StatusCode != SmtpStatusCode.CommandNotImplemented)
				{
					throw ex;
				}
				HelloCommand.Send(this, this.client.clientDomain);
				this.supportedAuth = SupportedAuth.Login;
			}
			if (this.enableSsl)
			{
				if (!this.serverSupportsStartTls && !(pooledStream.NetworkStream is TlsStream))
				{
					throw new SmtpException(System.SR.GetString("MailServerDoesNotSupportStartTls"));
				}
				StartTlsCommand.Send(this);
				TlsStream tlsStream = new TlsStream(servicePoint.Host, pooledStream.NetworkStream, ServicePointManager.CheckCertificateRevocationList, (SslProtocols)ServicePointManager.SecurityProtocol, this.clientCertificates, servicePoint, this.client, null);
				pooledStream.NetworkStream = tlsStream;
				this.channelBindingToken = tlsStream.GetChannelBinding(ChannelBindingKind.Unique);
				this.responseReader = new SmtpReplyReaderFactory(pooledStream.NetworkStream);
				this.extensions = EHelloCommand.Send(this, this.client.clientDomain);
				this.ParseExtensions(this.extensions);
			}
			if (this.credentials != null)
			{
				for (int i = 0; i < this.authenticationModules.Length; i++)
				{
					if (this.AuthSupported(this.authenticationModules[i]))
					{
						NetworkCredential credential = this.credentials.GetCredential(servicePoint.Host, servicePoint.Port, this.authenticationModules[i].AuthenticationType);
						if (credential != null)
						{
							Authorization authorization = this.SetContextAndTryAuthenticate(this.authenticationModules[i], credential, null);
							if (authorization != null && authorization.Message != null)
							{
								lineInfo = AuthCommand.Send(this, this.authenticationModules[i].AuthenticationType, authorization.Message);
								if (lineInfo.StatusCode != SmtpStatusCode.CommandParameterNotImplemented)
								{
									while (lineInfo.StatusCode == (SmtpStatusCode)334)
									{
										authorization = this.authenticationModules[i].Authenticate(lineInfo.Line, null, this, this.client.TargetName, this.channelBindingToken);
										if (authorization == null)
										{
											throw new SmtpException(System.SR.GetString("SmtpAuthenticationFailed"));
										}
										lineInfo = AuthCommand.Send(this, authorization.Message);
										if (lineInfo.StatusCode == (SmtpStatusCode)235)
										{
											this.authenticationModules[i].CloseContext(this);
											this.isConnected = true;
											return;
										}
									}
								}
							}
						}
					}
				}
			}
			this.isConnected = true;
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0007B7CC File Offset: 0x000799CC
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private Authorization SetContextAndTryAuthenticate(ISmtpAuthenticationModule module, NetworkCredential credential, ContextAwareResult context)
		{
			if (credential is SystemNetworkCredential)
			{
				WindowsIdentity windowsIdentity = ((context == null) ? null : context.Identity);
				try
				{
					IDisposable disposable = ((windowsIdentity == null) ? null : windowsIdentity.Impersonate());
					if (disposable != null)
					{
						using (disposable)
						{
							return module.Authenticate(null, credential, this, this.client.TargetName, this.channelBindingToken);
						}
					}
					ExecutionContext executionContext = ((context == null) ? null : context.ContextCopy);
					if (executionContext != null)
					{
						SmtpConnection.AuthenticateCallbackContext authenticateCallbackContext = new SmtpConnection.AuthenticateCallbackContext(this, module, credential, this.client.TargetName, this.channelBindingToken);
						ExecutionContext.Run(executionContext, SmtpConnection.s_AuthenticateCallback, authenticateCallbackContext);
						return authenticateCallbackContext.result;
					}
					return module.Authenticate(null, credential, this, this.client.TargetName, this.channelBindingToken);
				}
				catch
				{
					throw;
				}
			}
			return module.Authenticate(null, credential, this, this.client.TargetName, this.channelBindingToken);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0007B8CC File Offset: 0x00079ACC
		private static void AuthenticateCallback(object state)
		{
			SmtpConnection.AuthenticateCallbackContext authenticateCallbackContext = (SmtpConnection.AuthenticateCallbackContext)state;
			authenticateCallbackContext.result = authenticateCallbackContext.module.Authenticate(null, authenticateCallbackContext.credential, authenticateCallbackContext.thisPtr, authenticateCallbackContext.spn, authenticateCallbackContext.token);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0007B90A File Offset: 0x00079B0A
		internal void EndGetConnection(IAsyncResult result)
		{
			SmtpConnection.ConnectAndHandshakeAsyncResult.End(result);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0007B914 File Offset: 0x00079B14
		internal Stream GetClosableStream()
		{
			ClosableStream closableStream = new ClosableStream(this.pooledStream.NetworkStream, this.onCloseHandler);
			this.isStreamOpen = true;
			return closableStream;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0007B940 File Offset: 0x00079B40
		private void OnClose(object sender, EventArgs args)
		{
			this.isStreamOpen = false;
			DataStopCommand.Send(this);
		}

		// Token: 0x04001824 RID: 6180
		private static readonly CreateConnectionDelegate m_CreateConnectionCallback = new CreateConnectionDelegate(SmtpConnection.CreateSmtpPooledStream);

		// Token: 0x04001825 RID: 6181
		private static readonly ContextCallback s_AuthenticateCallback = new ContextCallback(SmtpConnection.AuthenticateCallback);

		// Token: 0x04001826 RID: 6182
		private BufferBuilder bufferBuilder = new BufferBuilder();

		// Token: 0x04001827 RID: 6183
		private bool isConnected;

		// Token: 0x04001828 RID: 6184
		private bool isClosed;

		// Token: 0x04001829 RID: 6185
		private bool isStreamOpen;

		// Token: 0x0400182A RID: 6186
		private bool sawNegotiate;

		// Token: 0x0400182B RID: 6187
		private EventHandler onCloseHandler;

		// Token: 0x0400182C RID: 6188
		internal SmtpTransport parent;

		// Token: 0x0400182D RID: 6189
		internal SmtpClient client;

		// Token: 0x0400182E RID: 6190
		private SmtpReplyReaderFactory responseReader;

		// Token: 0x0400182F RID: 6191
		private const int sizeOfAuthString = 5;

		// Token: 0x04001830 RID: 6192
		private const int sizeOfAuthExtension = 4;

		// Token: 0x04001831 RID: 6193
		private const string authExtension = "auth";

		// Token: 0x04001832 RID: 6194
		private const string authLogin = "login";

		// Token: 0x04001833 RID: 6195
		private const string authNtlm = "ntlm";

		// Token: 0x04001834 RID: 6196
		private const string authGssapi = "gssapi";

		// Token: 0x04001835 RID: 6197
		private const string authWDigest = "wdigest";

		// Token: 0x04001836 RID: 6198
		private PooledStream pooledStream;

		// Token: 0x04001837 RID: 6199
		private ConnectionPool connectionPool;

		// Token: 0x04001838 RID: 6200
		private SupportedAuth supportedAuth;

		// Token: 0x04001839 RID: 6201
		private bool serverSupportsStartTls;

		// Token: 0x0400183A RID: 6202
		private ISmtpAuthenticationModule[] authenticationModules;

		// Token: 0x0400183B RID: 6203
		private ICredentialsByHost credentials;

		// Token: 0x0400183C RID: 6204
		private int timeout = 100000;

		// Token: 0x0400183D RID: 6205
		private string[] extensions;

		// Token: 0x0400183E RID: 6206
		private ChannelBinding channelBindingToken;

		// Token: 0x0400183F RID: 6207
		private bool enableSsl;

		// Token: 0x04001840 RID: 6208
		private X509CertificateCollection clientCertificates;

		// Token: 0x0200079F RID: 1951
		private class AuthenticateCallbackContext
		{
			// Token: 0x060042D3 RID: 17107 RVA: 0x00117783 File Offset: 0x00115983
			internal AuthenticateCallbackContext(SmtpConnection thisPtr, ISmtpAuthenticationModule module, NetworkCredential credential, string spn, ChannelBinding Token)
			{
				this.thisPtr = thisPtr;
				this.module = module;
				this.credential = credential;
				this.spn = spn;
				this.token = Token;
				this.result = null;
			}

			// Token: 0x04003394 RID: 13204
			internal readonly SmtpConnection thisPtr;

			// Token: 0x04003395 RID: 13205
			internal readonly ISmtpAuthenticationModule module;

			// Token: 0x04003396 RID: 13206
			internal readonly NetworkCredential credential;

			// Token: 0x04003397 RID: 13207
			internal readonly string spn;

			// Token: 0x04003398 RID: 13208
			internal readonly ChannelBinding token;

			// Token: 0x04003399 RID: 13209
			internal Authorization result;
		}

		// Token: 0x020007A0 RID: 1952
		private class ConnectAndHandshakeAsyncResult : LazyAsyncResult
		{
			// Token: 0x060042D4 RID: 17108 RVA: 0x001177B7 File Offset: 0x001159B7
			internal ConnectAndHandshakeAsyncResult(SmtpConnection connection, string host, int port, ContextAwareResult outerResult, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this.connection = connection;
				this.host = host;
				this.port = port;
				this.m_OuterResult = outerResult;
			}

			// Token: 0x060042D5 RID: 17109 RVA: 0x001177E8 File Offset: 0x001159E8
			private static void ConnectionCreatedCallback(object request, object state)
			{
				SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)request;
				if (state is Exception)
				{
					connectAndHandshakeAsyncResult.InvokeCallback((Exception)state);
					return;
				}
				SmtpPooledStream smtpPooledStream = (SmtpPooledStream)((PooledStream)state);
				try
				{
					while (smtpPooledStream.creds != null && smtpPooledStream.creds != connectAndHandshakeAsyncResult.connection.credentials)
					{
						connectAndHandshakeAsyncResult.connection.connectionPool.PutConnection(smtpPooledStream, smtpPooledStream.Owner, connectAndHandshakeAsyncResult.connection.Timeout, false);
						smtpPooledStream = (SmtpPooledStream)connectAndHandshakeAsyncResult.connection.connectionPool.GetConnection(connectAndHandshakeAsyncResult, SmtpConnection.ConnectAndHandshakeAsyncResult.m_ConnectionCreatedCallback, connectAndHandshakeAsyncResult.connection.Timeout);
						if (smtpPooledStream == null)
						{
							return;
						}
					}
					if (Logging.On)
					{
						Logging.Associate(Logging.Web, connectAndHandshakeAsyncResult.connection, smtpPooledStream);
					}
					smtpPooledStream.Owner = connectAndHandshakeAsyncResult.connection;
					smtpPooledStream.creds = connectAndHandshakeAsyncResult.connection.credentials;
					SmtpConnection smtpConnection = connectAndHandshakeAsyncResult.connection;
					lock (smtpConnection)
					{
						if (connectAndHandshakeAsyncResult.connection.isClosed)
						{
							connectAndHandshakeAsyncResult.connection.connectionPool.PutConnection(smtpPooledStream, smtpPooledStream.Owner, connectAndHandshakeAsyncResult.connection.Timeout, false);
							connectAndHandshakeAsyncResult.InvokeCallback(null);
							return;
						}
						connectAndHandshakeAsyncResult.connection.pooledStream = smtpPooledStream;
					}
					connectAndHandshakeAsyncResult.Handshake();
				}
				catch (Exception ex)
				{
					connectAndHandshakeAsyncResult.InvokeCallback(ex);
				}
			}

			// Token: 0x060042D6 RID: 17110 RVA: 0x0011796C File Offset: 0x00115B6C
			internal static void End(IAsyncResult result)
			{
				SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result;
				object obj = connectAndHandshakeAsyncResult.InternalWaitForCompletion();
				if (obj is Exception)
				{
					throw (Exception)obj;
				}
			}

			// Token: 0x060042D7 RID: 17111 RVA: 0x00117998 File Offset: 0x00115B98
			internal void GetConnection(bool synchronous)
			{
				if (this.connection.isConnected)
				{
					throw new InvalidOperationException(System.SR.GetString("SmtpAlreadyConnected"));
				}
				SmtpPooledStream smtpPooledStream = (SmtpPooledStream)this.connection.connectionPool.GetConnection(this, synchronous ? null : SmtpConnection.ConnectAndHandshakeAsyncResult.m_ConnectionCreatedCallback, this.connection.Timeout);
				if (smtpPooledStream != null)
				{
					try
					{
						while (smtpPooledStream.creds != null && smtpPooledStream.creds != this.connection.credentials)
						{
							this.connection.connectionPool.PutConnection(smtpPooledStream, smtpPooledStream.Owner, this.connection.Timeout, false);
							smtpPooledStream = (SmtpPooledStream)this.connection.connectionPool.GetConnection(this, synchronous ? null : SmtpConnection.ConnectAndHandshakeAsyncResult.m_ConnectionCreatedCallback, this.connection.Timeout);
							if (smtpPooledStream == null)
							{
								return;
							}
						}
						smtpPooledStream.creds = this.connection.credentials;
						smtpPooledStream.Owner = this.connection;
						SmtpConnection smtpConnection = this.connection;
						lock (smtpConnection)
						{
							this.connection.pooledStream = smtpPooledStream;
						}
						this.Handshake();
					}
					catch (Exception ex)
					{
						base.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x060042D8 RID: 17112 RVA: 0x00117ADC File Offset: 0x00115CDC
			private void Handshake()
			{
				this.connection.responseReader = new SmtpReplyReaderFactory(this.connection.pooledStream.NetworkStream);
				this.connection.pooledStream.UpdateLifetime();
				if (((SmtpPooledStream)this.connection.pooledStream).previouslyUsed)
				{
					this.connection.isConnected = true;
					base.InvokeCallback();
					return;
				}
				SmtpReplyReader nextReplyReader = this.connection.Reader.GetNextReplyReader();
				IAsyncResult asyncResult = nextReplyReader.BeginReadLine(SmtpConnection.ConnectAndHandshakeAsyncResult.handshakeCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				LineInfo lineInfo = nextReplyReader.EndReadLine(asyncResult);
				if (lineInfo.StatusCode != SmtpStatusCode.ServiceReady)
				{
					throw new SmtpException(lineInfo.StatusCode, lineInfo.Line, true);
				}
				try
				{
					this.SendEHello();
				}
				catch
				{
					this.SendHello();
				}
			}

			// Token: 0x060042D9 RID: 17113 RVA: 0x00117BBC File Offset: 0x00115DBC
			private static void HandshakeCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						try
						{
							LineInfo lineInfo = connectAndHandshakeAsyncResult.connection.Reader.CurrentReader.EndReadLine(result);
							if (lineInfo.StatusCode != SmtpStatusCode.ServiceReady)
							{
								connectAndHandshakeAsyncResult.InvokeCallback(new SmtpException(lineInfo.StatusCode, lineInfo.Line, true));
							}
							else if (!connectAndHandshakeAsyncResult.SendEHello())
							{
							}
						}
						catch (SmtpException)
						{
							if (!connectAndHandshakeAsyncResult.SendHello())
							{
							}
						}
					}
					catch (Exception ex)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x060042DA RID: 17114 RVA: 0x00117C60 File Offset: 0x00115E60
			private bool SendEHello()
			{
				IAsyncResult asyncResult = EHelloCommand.BeginSend(this.connection, this.connection.client.clientDomain, SmtpConnection.ConnectAndHandshakeAsyncResult.sendEHelloCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				this.connection.extensions = EHelloCommand.EndSend(asyncResult);
				this.connection.ParseExtensions(this.connection.extensions);
				if (this.connection.pooledStream.NetworkStream is TlsStream)
				{
					this.Authenticate();
					return true;
				}
				if (this.connection.EnableSsl)
				{
					if (!this.connection.serverSupportsStartTls && !(this.connection.pooledStream.NetworkStream is TlsStream))
					{
						throw new SmtpException(System.SR.GetString("MailServerDoesNotSupportStartTls"));
					}
					this.SendStartTls();
				}
				else
				{
					this.Authenticate();
				}
				return true;
			}

			// Token: 0x060042DB RID: 17115 RVA: 0x00117D34 File Offset: 0x00115F34
			private static void SendEHelloCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						try
						{
							connectAndHandshakeAsyncResult.connection.extensions = EHelloCommand.EndSend(result);
							connectAndHandshakeAsyncResult.connection.ParseExtensions(connectAndHandshakeAsyncResult.connection.extensions);
							if (connectAndHandshakeAsyncResult.connection.pooledStream.NetworkStream is TlsStream)
							{
								connectAndHandshakeAsyncResult.Authenticate();
								return;
							}
						}
						catch (SmtpException ex)
						{
							if (ex.StatusCode != SmtpStatusCode.CommandUnrecognized && ex.StatusCode != SmtpStatusCode.CommandNotImplemented)
							{
								throw ex;
							}
							if (!connectAndHandshakeAsyncResult.SendHello())
							{
								return;
							}
						}
						if (connectAndHandshakeAsyncResult.connection.EnableSsl)
						{
							if (!connectAndHandshakeAsyncResult.connection.serverSupportsStartTls && !(connectAndHandshakeAsyncResult.connection.pooledStream.NetworkStream is TlsStream))
							{
								throw new SmtpException(System.SR.GetString("MailServerDoesNotSupportStartTls"));
							}
							connectAndHandshakeAsyncResult.SendStartTls();
						}
						else
						{
							connectAndHandshakeAsyncResult.Authenticate();
						}
					}
					catch (Exception ex2)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(ex2);
					}
				}
			}

			// Token: 0x060042DC RID: 17116 RVA: 0x00117E44 File Offset: 0x00116044
			private bool SendHello()
			{
				IAsyncResult asyncResult = HelloCommand.BeginSend(this.connection, this.connection.client.clientDomain, SmtpConnection.ConnectAndHandshakeAsyncResult.sendHelloCallback, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.connection.supportedAuth = SupportedAuth.Login;
					HelloCommand.EndSend(asyncResult);
					this.Authenticate();
					return true;
				}
				return false;
			}

			// Token: 0x060042DD RID: 17117 RVA: 0x00117E98 File Offset: 0x00116098
			private static void SendHelloCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						HelloCommand.EndSend(result);
						connectAndHandshakeAsyncResult.Authenticate();
					}
					catch (Exception ex)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x060042DE RID: 17118 RVA: 0x00117EE4 File Offset: 0x001160E4
			private bool SendStartTls()
			{
				IAsyncResult asyncResult = StartTlsCommand.BeginSend(this.connection, new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.SendStartTlsCallback), this);
				if (asyncResult.CompletedSynchronously)
				{
					StartTlsCommand.EndSend(asyncResult);
					TlsStream tlsStream = new TlsStream(this.connection.pooledStream.ServicePoint.Host, this.connection.pooledStream.NetworkStream, ServicePointManager.CheckCertificateRevocationList, (SslProtocols)ServicePointManager.SecurityProtocol, this.connection.ClientCertificates, this.connection.pooledStream.ServicePoint, this.connection.client, this.m_OuterResult.ContextCopy);
					this.connection.pooledStream.NetworkStream = tlsStream;
					this.connection.responseReader = new SmtpReplyReaderFactory(this.connection.pooledStream.NetworkStream);
					this.SendEHello();
					return true;
				}
				return false;
			}

			// Token: 0x060042DF RID: 17119 RVA: 0x00117FBC File Offset: 0x001161BC
			private static void SendStartTlsCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						StartTlsCommand.EndSend(result);
						TlsStream tlsStream = new TlsStream(connectAndHandshakeAsyncResult.connection.pooledStream.ServicePoint.Host, connectAndHandshakeAsyncResult.connection.pooledStream.NetworkStream, ServicePointManager.CheckCertificateRevocationList, (SslProtocols)ServicePointManager.SecurityProtocol, connectAndHandshakeAsyncResult.connection.ClientCertificates, connectAndHandshakeAsyncResult.connection.pooledStream.ServicePoint, connectAndHandshakeAsyncResult.connection.client, connectAndHandshakeAsyncResult.m_OuterResult.ContextCopy);
						connectAndHandshakeAsyncResult.connection.pooledStream.NetworkStream = tlsStream;
						connectAndHandshakeAsyncResult.connection.responseReader = new SmtpReplyReaderFactory(connectAndHandshakeAsyncResult.connection.pooledStream.NetworkStream);
						connectAndHandshakeAsyncResult.SendEHello();
					}
					catch (Exception ex)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x060042E0 RID: 17120 RVA: 0x001180A0 File Offset: 0x001162A0
			private void Authenticate()
			{
				if (this.connection.credentials != null)
				{
					ISmtpAuthenticationModule smtpAuthenticationModule;
					for (;;)
					{
						int num = this.currentModule + 1;
						this.currentModule = num;
						if (num >= this.connection.authenticationModules.Length)
						{
							goto IL_139;
						}
						smtpAuthenticationModule = this.connection.authenticationModules[this.currentModule];
						if (this.connection.AuthSupported(smtpAuthenticationModule))
						{
							NetworkCredential credential = this.connection.credentials.GetCredential(this.host, this.port, smtpAuthenticationModule.AuthenticationType);
							if (credential != null)
							{
								Authorization authorization = this.connection.SetContextAndTryAuthenticate(smtpAuthenticationModule, credential, this.m_OuterResult);
								if (authorization != null && authorization.Message != null)
								{
									IAsyncResult asyncResult = AuthCommand.BeginSend(this.connection, this.connection.authenticationModules[this.currentModule].AuthenticationType, authorization.Message, SmtpConnection.ConnectAndHandshakeAsyncResult.authenticateCallback, this);
									if (!asyncResult.CompletedSynchronously)
									{
										break;
									}
									LineInfo lineInfo = AuthCommand.EndSend(asyncResult);
									if (lineInfo.StatusCode == (SmtpStatusCode)334)
									{
										this.authResponse = lineInfo.Line;
										if (!this.AuthenticateContinue())
										{
											return;
										}
									}
									else if (lineInfo.StatusCode == (SmtpStatusCode)235)
									{
										goto Block_9;
									}
								}
							}
						}
					}
					return;
					Block_9:
					smtpAuthenticationModule.CloseContext(this.connection);
					this.connection.isConnected = true;
				}
				IL_139:
				this.connection.isConnected = true;
				base.InvokeCallback();
			}

			// Token: 0x060042E1 RID: 17121 RVA: 0x001181F8 File Offset: 0x001163F8
			private static void AuthenticateCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						LineInfo lineInfo = AuthCommand.EndSend(result);
						if (lineInfo.StatusCode == (SmtpStatusCode)334)
						{
							connectAndHandshakeAsyncResult.authResponse = lineInfo.Line;
							if (!connectAndHandshakeAsyncResult.AuthenticateContinue())
							{
								return;
							}
						}
						else if (lineInfo.StatusCode == (SmtpStatusCode)235)
						{
							connectAndHandshakeAsyncResult.connection.authenticationModules[connectAndHandshakeAsyncResult.currentModule].CloseContext(connectAndHandshakeAsyncResult.connection);
							connectAndHandshakeAsyncResult.connection.isConnected = true;
							connectAndHandshakeAsyncResult.InvokeCallback();
							return;
						}
						connectAndHandshakeAsyncResult.Authenticate();
					}
					catch (Exception ex)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x060042E2 RID: 17122 RVA: 0x001182AC File Offset: 0x001164AC
			private bool AuthenticateContinue()
			{
				for (;;)
				{
					Authorization authorization = this.connection.authenticationModules[this.currentModule].Authenticate(this.authResponse, null, this.connection, this.connection.client.TargetName, this.connection.channelBindingToken);
					if (authorization == null)
					{
						break;
					}
					IAsyncResult asyncResult = AuthCommand.BeginSend(this.connection, authorization.Message, SmtpConnection.ConnectAndHandshakeAsyncResult.authenticateContinueCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					LineInfo lineInfo = AuthCommand.EndSend(asyncResult);
					if (lineInfo.StatusCode == (SmtpStatusCode)235)
					{
						goto Block_2;
					}
					if (lineInfo.StatusCode != (SmtpStatusCode)334)
					{
						return true;
					}
					this.authResponse = lineInfo.Line;
				}
				throw new SmtpException(System.SR.GetString("SmtpAuthenticationFailed"));
				Block_2:
				this.connection.authenticationModules[this.currentModule].CloseContext(this.connection);
				this.connection.isConnected = true;
				base.InvokeCallback();
				return false;
			}

			// Token: 0x060042E3 RID: 17123 RVA: 0x00118398 File Offset: 0x00116598
			private static void AuthenticateContinueCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						LineInfo lineInfo = AuthCommand.EndSend(result);
						if (lineInfo.StatusCode == (SmtpStatusCode)235)
						{
							connectAndHandshakeAsyncResult.connection.authenticationModules[connectAndHandshakeAsyncResult.currentModule].CloseContext(connectAndHandshakeAsyncResult.connection);
							connectAndHandshakeAsyncResult.connection.isConnected = true;
							connectAndHandshakeAsyncResult.InvokeCallback();
						}
						else
						{
							if (lineInfo.StatusCode == (SmtpStatusCode)334)
							{
								connectAndHandshakeAsyncResult.authResponse = lineInfo.Line;
								if (!connectAndHandshakeAsyncResult.AuthenticateContinue())
								{
									return;
								}
							}
							connectAndHandshakeAsyncResult.Authenticate();
						}
					}
					catch (Exception ex)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x0400339A RID: 13210
			private static readonly GeneralAsyncDelegate m_ConnectionCreatedCallback = new GeneralAsyncDelegate(SmtpConnection.ConnectAndHandshakeAsyncResult.ConnectionCreatedCallback);

			// Token: 0x0400339B RID: 13211
			private string authResponse;

			// Token: 0x0400339C RID: 13212
			private SmtpConnection connection;

			// Token: 0x0400339D RID: 13213
			private int currentModule = -1;

			// Token: 0x0400339E RID: 13214
			private int port;

			// Token: 0x0400339F RID: 13215
			private static AsyncCallback handshakeCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.HandshakeCallback);

			// Token: 0x040033A0 RID: 13216
			private static AsyncCallback sendEHelloCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.SendEHelloCallback);

			// Token: 0x040033A1 RID: 13217
			private static AsyncCallback sendHelloCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.SendHelloCallback);

			// Token: 0x040033A2 RID: 13218
			private static AsyncCallback authenticateCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.AuthenticateCallback);

			// Token: 0x040033A3 RID: 13219
			private static AsyncCallback authenticateContinueCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.AuthenticateContinueCallback);

			// Token: 0x040033A4 RID: 13220
			private string host;

			// Token: 0x040033A5 RID: 13221
			private readonly ContextAwareResult m_OuterResult;
		}
	}
}

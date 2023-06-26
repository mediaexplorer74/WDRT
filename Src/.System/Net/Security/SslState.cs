using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x02000360 RID: 864
	internal class SslState
	{
		// Token: 0x06001F4C RID: 8012 RVA: 0x00091F2B File Offset: 0x0009012B
		internal SslState(Stream innerStream, bool isHTTP, EncryptionPolicy encryptionPolicy)
			: this(innerStream, null, null, encryptionPolicy)
		{
			this._ForceBufferingLastHandshakePayload = isHTTP;
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x00091F3E File Offset: 0x0009013E
		internal SslState(Stream innerStream, RemoteCertValidationCallback certValidationCallback, LocalCertSelectionCallback certSelectionCallback, EncryptionPolicy encryptionPolicy)
		{
			this._InnerStream = innerStream;
			this._Reader = new FixedSizeReader(innerStream);
			this._CertValidationDelegate = certValidationCallback;
			this._CertSelectionDelegate = certSelectionCallback;
			this._EncryptionPolicy = encryptionPolicy;
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00091F70 File Offset: 0x00090170
		internal void ValidateCreateContext(bool isServer, string targetHost, SslProtocols enabledSslProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool remoteCertRequired, bool checkCertRevocationStatus)
		{
			this.ValidateCreateContext(isServer, targetHost, enabledSslProtocols, serverCertificate, clientCertificates, remoteCertRequired, checkCertRevocationStatus, !isServer);
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00091F94 File Offset: 0x00090194
		internal void ValidateCreateContext(bool isServer, string targetHost, SslProtocols enabledSslProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool remoteCertRequired, bool checkCertRevocationStatus, bool checkCertName)
		{
			if (this._Exception != null && !this._CanRetryAuthentication)
			{
				throw this._Exception;
			}
			if (this.Context != null && this.Context.IsValidContext)
			{
				throw new InvalidOperationException(System.SR.GetString("net_auth_reauth"));
			}
			if (this.Context != null && this.IsServer != isServer)
			{
				throw new InvalidOperationException(System.SR.GetString("net_auth_client_server"));
			}
			if (targetHost == null)
			{
				throw new ArgumentNullException("targetHost");
			}
			if (isServer)
			{
				enabledSslProtocols &= (SslProtocols)1073747285;
				if (serverCertificate == null)
				{
					throw new ArgumentNullException("serverCertificate");
				}
			}
			else
			{
				enabledSslProtocols &= (SslProtocols)(-2147472726);
			}
			if (ServicePointManager.DisableSystemDefaultTlsVersions && enabledSslProtocols == SslProtocols.None)
			{
				throw new ArgumentException(System.SR.GetString("net_invalid_enum", new object[] { "SslProtocolType" }), "sslProtocolType");
			}
			if (clientCertificates == null)
			{
				clientCertificates = new X509CertificateCollection();
			}
			if (targetHost.Length == 0)
			{
				targetHost = "?" + Interlocked.Increment(ref SslState.UniqueNameInteger).ToString(NumberFormatInfo.InvariantInfo);
			}
			this._Exception = null;
			try
			{
				this._Context = new SecureChannel(targetHost, isServer, (SchProtocols)enabledSslProtocols, serverCertificate, clientCertificates, remoteCertRequired, checkCertName, checkCertRevocationStatus, this._EncryptionPolicy, this._CertSelectionDelegate);
			}
			catch (Win32Exception ex)
			{
				throw new AuthenticationException(System.SR.GetString("net_auth_SSPI"), ex);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x000920E4 File Offset: 0x000902E4
		internal bool IsAuthenticated
		{
			get
			{
				return this._Context != null && this._Context.IsValidContext && this._Exception == null && this.HandshakeCompleted;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0009210B File Offset: 0x0009030B
		internal bool IsMutuallyAuthenticated
		{
			get
			{
				return this.IsAuthenticated && (this.Context.IsServer ? this.Context.LocalServerCertificate : this.Context.LocalClientCertificate) != null && this.Context.IsRemoteCertificateAvailable;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x00092149 File Offset: 0x00090349
		internal bool RemoteCertRequired
		{
			get
			{
				return this.Context == null || this.Context.RemoteCertRequired;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x00092160 File Offset: 0x00090360
		internal bool IsServer
		{
			get
			{
				return this.Context != null && this.Context.IsServer;
			}
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00092177 File Offset: 0x00090377
		internal void SetCertValidationDelegate(RemoteCertValidationCallback certValidationCallback)
		{
			this._CertValidationDelegate = certValidationCallback;
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x00092180 File Offset: 0x00090380
		internal X509Certificate LocalCertificate
		{
			get
			{
				this.CheckThrow(true, false);
				return this.InternalLocalCertificate;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x00092190 File Offset: 0x00090390
		internal X509Certificate InternalLocalCertificate
		{
			get
			{
				if (!this.Context.IsServer)
				{
					return this.Context.LocalClientCertificate;
				}
				return this.Context.LocalServerCertificate;
			}
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x000921B6 File Offset: 0x000903B6
		internal ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (this.Context != null)
			{
				return this.Context.GetChannelBinding(kind);
			}
			return null;
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x000921CE File Offset: 0x000903CE
		internal bool CheckCertRevocationStatus
		{
			get
			{
				return this.Context != null && this.Context.CheckCertRevocationStatus;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x000921E5 File Offset: 0x000903E5
		internal SecurityStatus LastSecurityStatus
		{
			get
			{
				return this._SecurityStatus;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x000921ED File Offset: 0x000903ED
		internal bool IsCertValidationFailed
		{
			get
			{
				return this._CertValidationFailed;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x000921F5 File Offset: 0x000903F5
		internal bool IsShutdown
		{
			get
			{
				return this._Shutdown;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x000921FD File Offset: 0x000903FD
		internal bool DataAvailable
		{
			get
			{
				return this.IsAuthenticated && (this.SecureStream.DataAvailable || this._QueuedReadCount != 0);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x00092224 File Offset: 0x00090424
		internal CipherAlgorithmType CipherAlgorithm
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return CipherAlgorithmType.None;
				}
				return (CipherAlgorithmType)connectionInfo.DataCipherAlg;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001F5E RID: 8030 RVA: 0x00092250 File Offset: 0x00090450
		internal int CipherStrength
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return 0;
				}
				return connectionInfo.DataKeySize;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0009227C File Offset: 0x0009047C
		internal HashAlgorithmType HashAlgorithm
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return HashAlgorithmType.None;
				}
				return (HashAlgorithmType)connectionInfo.DataHashAlg;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001F60 RID: 8032 RVA: 0x000922A8 File Offset: 0x000904A8
		internal int HashStrength
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return 0;
				}
				return connectionInfo.DataHashKeySize;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x000922D4 File Offset: 0x000904D4
		internal ExchangeAlgorithmType KeyExchangeAlgorithm
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return ExchangeAlgorithmType.None;
				}
				return (ExchangeAlgorithmType)connectionInfo.KeyExchangeAlg;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x00092300 File Offset: 0x00090500
		internal int KeyExchangeStrength
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return 0;
				}
				return connectionInfo.KeyExchKeySize;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0009232C File Offset: 0x0009052C
		internal SslProtocols SslProtocol
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return SslProtocols.None;
				}
				SslProtocols sslProtocols = (SslProtocols)connectionInfo.Protocol;
				if ((sslProtocols & SslProtocols.Ssl2) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Ssl2;
				}
				if ((sslProtocols & SslProtocols.Ssl3) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Ssl3;
				}
				if ((sslProtocols & SslProtocols.Tls) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Tls;
				}
				if ((sslProtocols & SslProtocols.Tls11) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Tls11;
				}
				if ((sslProtocols & SslProtocols.Tls12) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Tls12;
				}
				if ((sslProtocols & SslProtocols.Tls13) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Tls13;
				}
				return sslProtocols;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x000923B4 File Offset: 0x000905B4
		internal Stream InnerStream
		{
			get
			{
				return this._InnerStream;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x000923BC File Offset: 0x000905BC
		internal _SslStream SecureStream
		{
			get
			{
				this.CheckThrow(true, false);
				if (this._SecureStream == null)
				{
					Interlocked.CompareExchange<_SslStream>(ref this._SecureStream, new _SslStream(this), null);
				}
				return this._SecureStream;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x000923E7 File Offset: 0x000905E7
		internal int HeaderSize
		{
			get
			{
				return this.Context.HeaderSize;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x000923F4 File Offset: 0x000905F4
		internal int MaxDataSize
		{
			get
			{
				return this.Context.MaxDataSize;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x00092401 File Offset: 0x00090601
		internal byte[] LastPayload
		{
			get
			{
				return this._LastPayload;
			}
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00092409 File Offset: 0x00090609
		internal void LastPayloadConsumed()
		{
			this._LastPayload = null;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00092412 File Offset: 0x00090612
		private Exception SetException(Exception e)
		{
			if (this._Exception == null)
			{
				this._Exception = e;
			}
			if (this._Exception != null && this.Context != null)
			{
				this.Context.Close();
			}
			return this._Exception;
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x00092444 File Offset: 0x00090644
		private bool HandshakeCompleted
		{
			get
			{
				return this._HandshakeCompleted;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x0009244C File Offset: 0x0009064C
		private SecureChannel Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x00092454 File Offset: 0x00090654
		internal void CheckThrow(bool authSuccessCheck, bool shutdownCheck = false)
		{
			if (this._Exception != null)
			{
				throw this._Exception;
			}
			if (authSuccessCheck && !this.IsAuthenticated)
			{
				throw new InvalidOperationException(System.SR.GetString("net_auth_noauth"));
			}
			if (shutdownCheck && this._Shutdown && !LocalAppContextSwitches.DontEnableTlsAlerts)
			{
				throw new InvalidOperationException("net_ssl_io_already_shutdown");
			}
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x000924A8 File Offset: 0x000906A8
		internal void Flush()
		{
			this.InnerStream.Flush();
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x000924B5 File Offset: 0x000906B5
		internal void Close()
		{
			this._Exception = new ObjectDisposedException("SslStream");
			if (this.Context != null)
			{
				this.Context.Close();
			}
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000924DA File Offset: 0x000906DA
		internal SecurityStatus EncryptData(byte[] buffer, int offset, int count, ref byte[] outBuffer, out int outSize)
		{
			this.CheckThrow(true, false);
			return this.Context.Encrypt(buffer, offset, count, ref outBuffer, out outSize);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000924F6 File Offset: 0x000906F6
		internal SecurityStatus DecryptData(byte[] buffer, ref int offset, ref int count)
		{
			this.CheckThrow(true, false);
			return this.PrivateDecryptData(buffer, ref offset, ref count);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x00092509 File Offset: 0x00090709
		private SecurityStatus PrivateDecryptData(byte[] buffer, ref int offset, ref int count)
		{
			return this.Context.Decrypt(buffer, ref offset, ref count);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0009251C File Offset: 0x0009071C
		private Exception EnqueueOldKeyDecryptedData(byte[] buffer, int offset, int count)
		{
			lock (this)
			{
				if (this._QueuedReadCount + count > 131072)
				{
					return new IOException(System.SR.GetString("net_auth_ignored_reauth", new object[] { 131072.ToString(NumberFormatInfo.CurrentInfo) }));
				}
				if (count != 0)
				{
					this._QueuedReadData = SslState.EnsureBufferSize(this._QueuedReadData, this._QueuedReadCount, this._QueuedReadCount + count);
					Buffer.BlockCopy(buffer, offset, this._QueuedReadData, this._QueuedReadCount, count);
					this._QueuedReadCount += count;
					this.FinishHandshakeRead(2);
				}
			}
			return null;
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000925DC File Offset: 0x000907DC
		internal int CheckOldKeyDecryptedData(byte[] buffer, int offset, int count)
		{
			this.CheckThrow(true, false);
			if (this._QueuedReadData != null)
			{
				int num = Math.Min(this._QueuedReadCount, count);
				Buffer.BlockCopy(this._QueuedReadData, 0, buffer, offset, num);
				this._QueuedReadCount -= num;
				if (this._QueuedReadCount == 0)
				{
					this._QueuedReadData = null;
				}
				else
				{
					Buffer.BlockCopy(this._QueuedReadData, num, this._QueuedReadData, 0, this._QueuedReadCount);
				}
				return num;
			}
			return -1;
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x00092650 File Offset: 0x00090850
		internal void ProcessAuthentication(LazyAsyncResult lazyResult)
		{
			if (Interlocked.Exchange(ref this._NestedAuth, 1) == 1)
			{
				throw new InvalidOperationException(System.SR.GetString("net_io_invalidnestedcall", new object[]
				{
					(lazyResult == null) ? "BeginAuthenticate" : "Authenticate",
					"authenticate"
				}));
			}
			try
			{
				this.CheckThrow(false, false);
				AsyncProtocolRequest asyncProtocolRequest = null;
				if (lazyResult != null)
				{
					asyncProtocolRequest = new AsyncProtocolRequest(lazyResult);
					asyncProtocolRequest.Buffer = null;
				}
				this._CachedSession = SslState.CachedSessionStatus.Unknown;
				this.ForceAuthentication(this.Context.IsServer, null, asyncProtocolRequest, false);
				if (lazyResult == null && Logging.On)
				{
					Logging.PrintInfo(Logging.Web, System.SR.GetString("net_log_sspi_selected_cipher_suite", new object[] { "ProcessAuthentication", this.SslProtocol, this.CipherAlgorithm, this.CipherStrength, this.HashAlgorithm, this.HashStrength, this.KeyExchangeAlgorithm, this.KeyExchangeStrength }));
				}
			}
			catch (Exception)
			{
				this._NestedAuth = 0;
				throw;
			}
			finally
			{
				if (lazyResult == null)
				{
					this._NestedAuth = 0;
				}
			}
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0009279C File Offset: 0x0009099C
		internal void ReplyOnReAuthentication(byte[] buffer)
		{
			lock (this)
			{
				this._LockReadState = 2;
				if (this._PendingReHandshake)
				{
					this.FinishRead(buffer);
					return;
				}
			}
			this.ForceAuthentication(false, buffer, new AsyncProtocolRequest(new LazyAsyncResult(this, null, new AsyncCallback(this.RehandshakeCompleteCallback)))
			{
				Buffer = buffer
			}, true);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x00092814 File Offset: 0x00090A14
		private void ForceAuthentication(bool receiveFirst, byte[] buffer, AsyncProtocolRequest asyncRequest, bool renegotiation = false)
		{
			if (this.CheckEnqueueHandshake(buffer, asyncRequest))
			{
				return;
			}
			SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
			if (connectionInfo == null || (connectionInfo.Protocol & 12288) == 0)
			{
				this._Framing = SslState.Framing.None;
			}
			try
			{
				if (receiveFirst)
				{
					this.StartReceiveBlob(buffer, asyncRequest);
				}
				else
				{
					this.StartSendBlob(buffer, (buffer == null) ? 0 : buffer.Length, asyncRequest, renegotiation);
				}
			}
			catch (Exception ex)
			{
				this._Framing = SslState.Framing.None;
				this._HandshakeCompleted = false;
				if (this.SetException(ex) == ex)
				{
					throw;
				}
				throw this._Exception;
			}
			finally
			{
				if (this._Exception != null)
				{
					this.FinishHandshake(null, null);
				}
			}
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000928C4 File Offset: 0x00090AC4
		internal void EndProcessAuthentication(IAsyncResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = result as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentException(System.SR.GetString("net_io_async_result", new object[] { result.GetType().FullName }), "asyncResult");
			}
			if (Interlocked.Exchange(ref this._NestedAuth, 0) == 0)
			{
				throw new InvalidOperationException(System.SR.GetString("net_io_invalidendcall", new object[] { "EndAuthenticate" }));
			}
			this.InternalEndProcessAuthentication(lazyAsyncResult);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, System.SR.GetString("net_log_sspi_selected_cipher_suite", new object[] { "EndProcessAuthentication", this.SslProtocol, this.CipherAlgorithm, this.CipherStrength, this.HashAlgorithm, this.HashStrength, this.KeyExchangeAlgorithm, this.KeyExchangeStrength }));
			}
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x000929D4 File Offset: 0x00090BD4
		internal void InternalEndProcessAuthentication(LazyAsyncResult lazyResult)
		{
			lazyResult.InternalWaitForCompletion();
			Exception ex = lazyResult.Result as Exception;
			if (ex != null)
			{
				this._Framing = SslState.Framing.None;
				this._HandshakeCompleted = false;
				throw this.SetException(ex);
			}
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x00092A10 File Offset: 0x00090C10
		private void StartSendBlob(byte[] incoming, int count, AsyncProtocolRequest asyncRequest, bool renegotiation = false)
		{
			ProtocolToken protocolToken = this.Context.NextMessage(incoming, 0, count);
			this._SecurityStatus = protocolToken.Status;
			if (protocolToken.Size != 0)
			{
				if (this.Context.IsServer && this._CachedSession == SslState.CachedSessionStatus.Unknown)
				{
					this._CachedSession = ((protocolToken.Size < 200) ? SslState.CachedSessionStatus.IsCached : SslState.CachedSessionStatus.IsNotCached);
				}
				if (this._Framing == SslState.Framing.Unified)
				{
					this._Framing = this.DetectFraming(protocolToken.Payload, protocolToken.Payload.Length);
				}
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				bool flag = renegotiation && !this.Context.IsServer && connectionInfo != null && (connectionInfo.Protocol & 12288) != 0;
				if (protocolToken.Done && this._ForceBufferingLastHandshakePayload && this.InnerStream.GetType() == typeof(NetworkStream) && !this._PendingReHandshake && !flag)
				{
					this._LastPayload = protocolToken.Payload;
				}
				else if (asyncRequest == null)
				{
					this.InnerStream.Write(protocolToken.Payload, 0, protocolToken.Size);
				}
				else
				{
					asyncRequest.AsyncState = protocolToken;
					IAsyncResult asyncResult = this.InnerStream.BeginWrite(protocolToken.Payload, 0, protocolToken.Size, SslState._WriteCallback, asyncRequest);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.InnerStream.EndWrite(asyncResult);
				}
			}
			this.CheckCompletionBeforeNextReceive(protocolToken, asyncRequest);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x00092B6C File Offset: 0x00090D6C
		private void CheckCompletionBeforeNextReceive(ProtocolToken message, AsyncProtocolRequest asyncRequest)
		{
			if (message.Failed)
			{
				this.StartSendAuthResetSignal(null, asyncRequest, new AuthenticationException(System.SR.GetString("net_auth_SSPI"), message.GetException()));
				return;
			}
			if (!message.Done || this._PendingReHandshake)
			{
				this.StartReceiveBlob(message.Payload, asyncRequest);
				return;
			}
			ProtocolToken protocolToken = null;
			if (!this.CompleteHandshake(ref protocolToken))
			{
				this.StartSendAuthResetSignal(protocolToken, asyncRequest, new AuthenticationException(System.SR.GetString("net_ssl_io_cert_validation"), null));
				return;
			}
			this.FinishHandshake(null, asyncRequest);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00092BEC File Offset: 0x00090DEC
		private void StartReceiveBlob(byte[] buffer, AsyncProtocolRequest asyncRequest)
		{
			if (this._PendingReHandshake)
			{
				if (this.CheckEnqueueHandshakeRead(ref buffer, asyncRequest))
				{
					return;
				}
				if (!this._PendingReHandshake)
				{
					this.ProcessReceivedBlob(buffer, buffer.Length, asyncRequest);
					return;
				}
			}
			buffer = SslState.EnsureBufferSize(buffer, 0, 5);
			int num;
			if (asyncRequest == null)
			{
				num = this._Reader.ReadPacket(buffer, 0, 5);
			}
			else
			{
				asyncRequest.SetNextRequest(buffer, 0, 5, SslState._PartialFrameCallback);
				this._Reader.AsyncReadPacket(asyncRequest);
				if (!asyncRequest.MustCompleteSynchronously)
				{
					return;
				}
				num = asyncRequest.Result;
			}
			this.StartReadFrame(buffer, num, asyncRequest);
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x00092C74 File Offset: 0x00090E74
		private void StartReadFrame(byte[] buffer, int readBytes, AsyncProtocolRequest asyncRequest)
		{
			if (readBytes == 0)
			{
				throw new IOException(System.SR.GetString("net_auth_eof"));
			}
			if (this._Framing == SslState.Framing.None)
			{
				this._Framing = this.DetectFraming(buffer, readBytes);
			}
			int num = this.GetRemainingFrameSize(buffer, readBytes);
			if (num < 0)
			{
				throw new IOException(System.SR.GetString("net_ssl_io_frame"));
			}
			if (num == 0)
			{
				throw new AuthenticationException(System.SR.GetString("net_auth_eof"), null);
			}
			buffer = SslState.EnsureBufferSize(buffer, readBytes, readBytes + num);
			if (asyncRequest == null)
			{
				num = this._Reader.ReadPacket(buffer, readBytes, num);
			}
			else
			{
				asyncRequest.SetNextRequest(buffer, readBytes, num, SslState._ReadFrameCallback);
				this._Reader.AsyncReadPacket(asyncRequest);
				if (!asyncRequest.MustCompleteSynchronously)
				{
					return;
				}
				num = asyncRequest.Result;
				if (num == 0)
				{
					readBytes = 0;
				}
			}
			this.ProcessReceivedBlob(buffer, readBytes + num, asyncRequest);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x00092D38 File Offset: 0x00090F38
		private void ProcessReceivedBlob(byte[] buffer, int count, AsyncProtocolRequest asyncRequest)
		{
			if (count == 0)
			{
				throw new AuthenticationException(System.SR.GetString("net_auth_eof"), null);
			}
			if (this._PendingReHandshake)
			{
				int num = 0;
				SecurityStatus securityStatus = this.PrivateDecryptData(buffer, ref num, ref count);
				if (securityStatus == SecurityStatus.OK)
				{
					Exception ex = this.EnqueueOldKeyDecryptedData(buffer, num, count);
					if (ex != null)
					{
						this.StartSendAuthResetSignal(null, asyncRequest, ex);
						return;
					}
					this._Framing = SslState.Framing.None;
					this.StartReceiveBlob(buffer, asyncRequest);
					return;
				}
				else
				{
					if (securityStatus != SecurityStatus.Renegotiate)
					{
						ProtocolToken protocolToken = new ProtocolToken(null, securityStatus);
						this.StartSendAuthResetSignal(null, asyncRequest, new AuthenticationException(System.SR.GetString("net_auth_SSPI"), protocolToken.GetException()));
						return;
					}
					this._PendingReHandshake = false;
					if (num != 0)
					{
						Buffer.BlockCopy(buffer, num, buffer, 0, count);
					}
				}
			}
			this.StartSendBlob(buffer, count, asyncRequest, false);
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x00092DE8 File Offset: 0x00090FE8
		private void StartSendAuthResetSignal(ProtocolToken message, AsyncProtocolRequest asyncRequest, Exception exception)
		{
			if (message == null || message.Size == 0)
			{
				throw exception;
			}
			if (asyncRequest == null)
			{
				this.InnerStream.Write(message.Payload, 0, message.Size);
			}
			else
			{
				asyncRequest.AsyncState = exception;
				IAsyncResult asyncResult = this.InnerStream.BeginWrite(message.Payload, 0, message.Size, SslState._WriteCallback, asyncRequest);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				this.InnerStream.EndWrite(asyncResult);
			}
			throw exception;
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x00092E5B File Offset: 0x0009105B
		private bool CompleteHandshake(ref ProtocolToken alertToken)
		{
			this.Context.ProcessHandshakeSuccess();
			if (!this.Context.VerifyRemoteCertificate(this._CertValidationDelegate, ref alertToken))
			{
				this._HandshakeCompleted = false;
				this._CertValidationFailed = true;
				return false;
			}
			this._CertValidationFailed = false;
			this._HandshakeCompleted = true;
			return true;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x00092E9C File Offset: 0x0009109C
		private static void WriteCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)transportResult.AsyncState;
			SslState sslState = (SslState)asyncProtocolRequest.AsyncObject;
			try
			{
				sslState.InnerStream.EndWrite(transportResult);
				object asyncState = asyncProtocolRequest.AsyncState;
				Exception ex = asyncState as Exception;
				if (ex != null)
				{
					throw ex;
				}
				sslState.CheckCompletionBeforeNextReceive((ProtocolToken)asyncState, asyncProtocolRequest);
			}
			catch (Exception ex2)
			{
				if (asyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				sslState.FinishHandshake(ex2, asyncProtocolRequest);
			}
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00092F20 File Offset: 0x00091120
		private static void PartialFrameCallback(AsyncProtocolRequest asyncRequest)
		{
			SslState sslState = (SslState)asyncRequest.AsyncObject;
			try
			{
				sslState.StartReadFrame(asyncRequest.Buffer, asyncRequest.Result, asyncRequest);
			}
			catch (Exception ex)
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				sslState.FinishHandshake(ex, asyncRequest);
			}
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00092F74 File Offset: 0x00091174
		private static void ReadFrameCallback(AsyncProtocolRequest asyncRequest)
		{
			SslState sslState = (SslState)asyncRequest.AsyncObject;
			try
			{
				if (asyncRequest.Result == 0)
				{
					asyncRequest.Offset = 0;
				}
				sslState.ProcessReceivedBlob(asyncRequest.Buffer, asyncRequest.Offset + asyncRequest.Result, asyncRequest);
			}
			catch (Exception ex)
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				sslState.FinishHandshake(ex, asyncRequest);
			}
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00092FE0 File Offset: 0x000911E0
		private bool CheckEnqueueHandshakeRead(ref byte[] buffer, AsyncProtocolRequest request)
		{
			LazyAsyncResult lazyAsyncResult = null;
			lock (this)
			{
				if (this._LockReadState == 6)
				{
					return false;
				}
				int num = Interlocked.Exchange(ref this._LockReadState, 2);
				if (num != 4)
				{
					return false;
				}
				if (request != null)
				{
					this._QueuedReadStateRequest = request;
					return true;
				}
				lazyAsyncResult = new LazyAsyncResult(null, null, null);
				this._QueuedReadStateRequest = lazyAsyncResult;
			}
			lazyAsyncResult.InternalWaitForCompletion();
			buffer = (byte[])lazyAsyncResult.Result;
			return false;
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00093074 File Offset: 0x00091274
		private void FinishHandshakeRead(int newState)
		{
			lock (this)
			{
				int num = Interlocked.Exchange(ref this._LockReadState, newState);
				if (num == 6)
				{
					this._LockReadState = 4;
					object queuedReadStateRequest = this._QueuedReadStateRequest;
					if (queuedReadStateRequest != null)
					{
						this._QueuedReadStateRequest = null;
						if (queuedReadStateRequest is LazyAsyncResult)
						{
							((LazyAsyncResult)queuedReadStateRequest).InvokeCallback();
						}
						else
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.CompleteRequestWaitCallback), queuedReadStateRequest);
						}
					}
				}
			}
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x00093100 File Offset: 0x00091300
		internal int CheckEnqueueRead(byte[] buffer, int offset, int count, AsyncProtocolRequest request)
		{
			int num = Interlocked.CompareExchange(ref this._LockReadState, 4, 0);
			if (num != 2)
			{
				return this.CheckOldKeyDecryptedData(buffer, offset, count);
			}
			LazyAsyncResult lazyAsyncResult = null;
			lock (this)
			{
				int num2 = this.CheckOldKeyDecryptedData(buffer, offset, count);
				if (num2 != -1)
				{
					return num2;
				}
				if (this._LockReadState != 2)
				{
					this._LockReadState = 4;
					return -1;
				}
				this._LockReadState = 6;
				if (request != null)
				{
					this._QueuedReadStateRequest = request;
					return 0;
				}
				lazyAsyncResult = new LazyAsyncResult(null, null, null);
				this._QueuedReadStateRequest = lazyAsyncResult;
			}
			lazyAsyncResult.InternalWaitForCompletion();
			int num3;
			lock (this)
			{
				num3 = this.CheckOldKeyDecryptedData(buffer, offset, count);
			}
			return num3;
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000931E4 File Offset: 0x000913E4
		internal void FinishRead(byte[] renegotiateBuffer)
		{
			int num = Interlocked.CompareExchange(ref this._LockReadState, 0, 4);
			if (num != 2)
			{
				return;
			}
			lock (this)
			{
				LazyAsyncResult lazyAsyncResult = this._QueuedReadStateRequest as LazyAsyncResult;
				if (lazyAsyncResult != null)
				{
					this._QueuedReadStateRequest = null;
					lazyAsyncResult.InvokeCallback(renegotiateBuffer);
				}
				else
				{
					AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)this._QueuedReadStateRequest;
					asyncProtocolRequest.Buffer = renegotiateBuffer;
					this._QueuedReadStateRequest = null;
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.AsyncResumeHandshakeRead), asyncProtocolRequest);
				}
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x0009327C File Offset: 0x0009147C
		internal bool CheckEnqueueWrite(AsyncProtocolRequest asyncRequest)
		{
			this._QueuedWriteStateRequest = null;
			int num = Interlocked.CompareExchange(ref this._LockWriteState, 1, 0);
			if (num != 2)
			{
				return false;
			}
			LazyAsyncResult lazyAsyncResult = null;
			lock (this)
			{
				if (this._LockWriteState != 2)
				{
					this.CheckThrow(true, false);
					return false;
				}
				this._LockWriteState = 3;
				if (asyncRequest != null)
				{
					this._QueuedWriteStateRequest = asyncRequest;
					return true;
				}
				lazyAsyncResult = new LazyAsyncResult(null, null, null);
				this._QueuedWriteStateRequest = lazyAsyncResult;
			}
			lazyAsyncResult.InternalWaitForCompletion();
			this.CheckThrow(true, false);
			return false;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00093320 File Offset: 0x00091520
		internal void FinishWrite()
		{
			int num = Interlocked.CompareExchange(ref this._LockWriteState, 0, 1);
			if (num != 2)
			{
				return;
			}
			lock (this)
			{
				object queuedWriteStateRequest = this._QueuedWriteStateRequest;
				if (queuedWriteStateRequest != null)
				{
					this._QueuedWriteStateRequest = null;
					if (queuedWriteStateRequest is LazyAsyncResult)
					{
						((LazyAsyncResult)queuedWriteStateRequest).InvokeCallback();
					}
					else
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.AsyncResumeHandshake), queuedWriteStateRequest);
					}
				}
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000933A4 File Offset: 0x000915A4
		internal IAsyncResult BeginShutdown(AsyncCallback asyncCallback, object asyncState)
		{
			this.CheckThrow(true, true);
			ProtocolToken protocolToken = this.Context.CreateShutdownToken();
			return this.InnerStream.BeginWrite(protocolToken.Payload, 0, protocolToken.Payload.Length, asyncCallback, asyncState);
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000933E1 File Offset: 0x000915E1
		internal void EndShutdown(IAsyncResult result)
		{
			this.CheckThrow(true, true);
			this.InnerStream.EndWrite(result);
			this._Shutdown = true;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x00093400 File Offset: 0x00091600
		private bool CheckEnqueueHandshake(byte[] buffer, AsyncProtocolRequest asyncRequest)
		{
			LazyAsyncResult lazyAsyncResult = null;
			lock (this)
			{
				if (this._LockWriteState == 3)
				{
					return false;
				}
				int num = Interlocked.Exchange(ref this._LockWriteState, 2);
				if (num != 1)
				{
					return false;
				}
				if (asyncRequest != null)
				{
					asyncRequest.Buffer = buffer;
					this._QueuedWriteStateRequest = asyncRequest;
					return true;
				}
				lazyAsyncResult = new LazyAsyncResult(null, null, null);
				this._QueuedWriteStateRequest = lazyAsyncResult;
			}
			lazyAsyncResult.InternalWaitForCompletion();
			return false;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0009348C File Offset: 0x0009168C
		private void FinishHandshake(Exception e, AsyncProtocolRequest asyncRequest)
		{
			try
			{
				lock (this)
				{
					if (e != null)
					{
						this.SetException(e);
					}
					this.FinishHandshakeRead(0);
					int num = Interlocked.CompareExchange(ref this._LockWriteState, 0, 2);
					if (num == 3)
					{
						this._LockWriteState = 1;
						object queuedWriteStateRequest = this._QueuedWriteStateRequest;
						if (queuedWriteStateRequest != null)
						{
							this._QueuedWriteStateRequest = null;
							if (queuedWriteStateRequest is LazyAsyncResult)
							{
								((LazyAsyncResult)queuedWriteStateRequest).InvokeCallback();
							}
							else
							{
								ThreadPool.QueueUserWorkItem(new WaitCallback(this.CompleteRequestWaitCallback), queuedWriteStateRequest);
							}
						}
					}
				}
			}
			finally
			{
				if (asyncRequest != null)
				{
					if (e != null)
					{
						asyncRequest.CompleteWithError(e);
					}
					else
					{
						asyncRequest.CompleteUser();
					}
				}
			}
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0009354C File Offset: 0x0009174C
		private static byte[] EnsureBufferSize(byte[] buffer, int copyCount, int size)
		{
			if (buffer == null || buffer.Length < size)
			{
				byte[] array = buffer;
				buffer = new byte[size];
				if (array != null && copyCount != 0)
				{
					Buffer.BlockCopy(array, 0, buffer, 0, copyCount);
				}
			}
			return buffer;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00093580 File Offset: 0x00091780
		private SslState.Framing DetectFraming(byte[] bytes, int length)
		{
			int num = -1;
			if (bytes[0] == 22 || bytes[0] == 23 || bytes[0] == 21)
			{
				if (length < 3)
				{
					return SslState.Framing.Invalid;
				}
				num = ((int)bytes[1] << 8) | (int)bytes[2];
				if (num < 768 || num >= 1280)
				{
					return SslState.Framing.Invalid;
				}
				return SslState.Framing.SinceSSL3;
			}
			else
			{
				if (length < 3)
				{
					return SslState.Framing.Invalid;
				}
				if (bytes[2] > 8)
				{
					return SslState.Framing.Invalid;
				}
				if (bytes[2] == 1)
				{
					if (length >= 5)
					{
						num = ((int)bytes[3] << 8) | (int)bytes[4];
					}
				}
				else if (bytes[2] == 4 && length >= 7)
				{
					num = ((int)bytes[5] << 8) | (int)bytes[6];
				}
				if (num != -1)
				{
					if (this._Framing == SslState.Framing.None)
					{
						if (num != 2 && (num < 512 || num >= 1280))
						{
							return SslState.Framing.Invalid;
						}
					}
					else if (num != 2)
					{
						return SslState.Framing.Invalid;
					}
				}
				if (!this.Context.IsServer || this._Framing == SslState.Framing.Unified)
				{
					return SslState.Framing.BeforeSSL3;
				}
				return SslState.Framing.Unified;
			}
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00093644 File Offset: 0x00091844
		internal int GetRemainingFrameSize(byte[] buffer, int dataSize)
		{
			int num = -1;
			switch (this._Framing)
			{
			case SslState.Framing.BeforeSSL3:
			case SslState.Framing.Unified:
				if (dataSize < 2)
				{
					throw new IOException(System.SR.GetString("net_ssl_io_frame"));
				}
				if ((buffer[0] & 128) != 0)
				{
					num = (((int)(buffer[0] & 127) << 8) | (int)buffer[1]) + 2;
					num -= dataSize;
				}
				else
				{
					num = (((int)(buffer[0] & 63) << 8) | (int)buffer[1]) + 3;
					num -= dataSize;
				}
				break;
			case SslState.Framing.SinceSSL3:
				if (dataSize < 5)
				{
					throw new IOException(System.SR.GetString("net_ssl_io_frame"));
				}
				num = (((int)buffer[3] << 8) | (int)buffer[4]) + 5;
				num -= dataSize;
				break;
			}
			return num;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000936E0 File Offset: 0x000918E0
		private void AsyncResumeHandshake(object state)
		{
			AsyncProtocolRequest asyncProtocolRequest = state as AsyncProtocolRequest;
			try
			{
				this.ForceAuthentication(this.Context.IsServer, asyncProtocolRequest.Buffer, asyncProtocolRequest, false);
			}
			catch (Exception ex)
			{
				asyncProtocolRequest.CompleteWithError(ex);
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0009372C File Offset: 0x0009192C
		private void AsyncResumeHandshakeRead(object state)
		{
			AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)state;
			try
			{
				if (this._PendingReHandshake)
				{
					this.StartReceiveBlob(asyncProtocolRequest.Buffer, asyncProtocolRequest);
				}
				else
				{
					this.ProcessReceivedBlob(asyncProtocolRequest.Buffer, (asyncProtocolRequest.Buffer == null) ? 0 : asyncProtocolRequest.Buffer.Length, asyncProtocolRequest);
				}
			}
			catch (Exception ex)
			{
				if (asyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				this.FinishHandshake(ex, asyncProtocolRequest);
			}
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000937A0 File Offset: 0x000919A0
		private void CompleteRequestWaitCallback(object state)
		{
			AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)state;
			if (asyncProtocolRequest.MustCompleteSynchronously)
			{
				throw new InternalException();
			}
			asyncProtocolRequest.CompleteRequest(0);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x000937CC File Offset: 0x000919CC
		private void RehandshakeCompleteCallback(IAsyncResult result)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			Exception ex = lazyAsyncResult.InternalWaitForCompletion() as Exception;
			if (ex != null)
			{
				this.FinishHandshake(ex, null);
			}
		}

		// Token: 0x04001D0D RID: 7437
		private static int UniqueNameInteger = 123;

		// Token: 0x04001D0E RID: 7438
		private static AsyncProtocolCallback _PartialFrameCallback = new AsyncProtocolCallback(SslState.PartialFrameCallback);

		// Token: 0x04001D0F RID: 7439
		private static AsyncProtocolCallback _ReadFrameCallback = new AsyncProtocolCallback(SslState.ReadFrameCallback);

		// Token: 0x04001D10 RID: 7440
		private static AsyncCallback _WriteCallback = new AsyncCallback(SslState.WriteCallback);

		// Token: 0x04001D11 RID: 7441
		private RemoteCertValidationCallback _CertValidationDelegate;

		// Token: 0x04001D12 RID: 7442
		private LocalCertSelectionCallback _CertSelectionDelegate;

		// Token: 0x04001D13 RID: 7443
		private bool _CanRetryAuthentication;

		// Token: 0x04001D14 RID: 7444
		private Stream _InnerStream;

		// Token: 0x04001D15 RID: 7445
		private _SslStream _SecureStream;

		// Token: 0x04001D16 RID: 7446
		private FixedSizeReader _Reader;

		// Token: 0x04001D17 RID: 7447
		private int _NestedAuth;

		// Token: 0x04001D18 RID: 7448
		private SecureChannel _Context;

		// Token: 0x04001D19 RID: 7449
		private bool _HandshakeCompleted;

		// Token: 0x04001D1A RID: 7450
		private bool _CertValidationFailed;

		// Token: 0x04001D1B RID: 7451
		private bool _Shutdown;

		// Token: 0x04001D1C RID: 7452
		private SecurityStatus _SecurityStatus;

		// Token: 0x04001D1D RID: 7453
		private Exception _Exception;

		// Token: 0x04001D1E RID: 7454
		private SslState.CachedSessionStatus _CachedSession;

		// Token: 0x04001D1F RID: 7455
		private byte[] _QueuedReadData;

		// Token: 0x04001D20 RID: 7456
		private int _QueuedReadCount;

		// Token: 0x04001D21 RID: 7457
		private bool _PendingReHandshake;

		// Token: 0x04001D22 RID: 7458
		private const int _ConstMaxQueuedReadBytes = 131072;

		// Token: 0x04001D23 RID: 7459
		private const int LockNone = 0;

		// Token: 0x04001D24 RID: 7460
		private const int LockWrite = 1;

		// Token: 0x04001D25 RID: 7461
		private const int LockHandshake = 2;

		// Token: 0x04001D26 RID: 7462
		private const int LockPendingWrite = 3;

		// Token: 0x04001D27 RID: 7463
		private const int LockRead = 4;

		// Token: 0x04001D28 RID: 7464
		private const int LockPendingRead = 6;

		// Token: 0x04001D29 RID: 7465
		private int _LockWriteState;

		// Token: 0x04001D2A RID: 7466
		private object _QueuedWriteStateRequest;

		// Token: 0x04001D2B RID: 7467
		private int _LockReadState;

		// Token: 0x04001D2C RID: 7468
		private object _QueuedReadStateRequest;

		// Token: 0x04001D2D RID: 7469
		private bool _ForceBufferingLastHandshakePayload;

		// Token: 0x04001D2E RID: 7470
		private byte[] _LastPayload;

		// Token: 0x04001D2F RID: 7471
		private readonly EncryptionPolicy _EncryptionPolicy;

		// Token: 0x04001D30 RID: 7472
		private SslState.Framing _Framing;

		// Token: 0x020007D2 RID: 2002
		private enum CachedSessionStatus : byte
		{
			// Token: 0x04003481 RID: 13441
			Unknown,
			// Token: 0x04003482 RID: 13442
			IsNotCached,
			// Token: 0x04003483 RID: 13443
			IsCached,
			// Token: 0x04003484 RID: 13444
			Renegotiated
		}

		// Token: 0x020007D3 RID: 2003
		private enum Framing
		{
			// Token: 0x04003486 RID: 13446
			None,
			// Token: 0x04003487 RID: 13447
			BeforeSSL3,
			// Token: 0x04003488 RID: 13448
			SinceSSL3,
			// Token: 0x04003489 RID: 13449
			Unified,
			// Token: 0x0400348A RID: 13450
			Invalid
		}

		// Token: 0x020007D4 RID: 2004
		private enum FrameType : byte
		{
			// Token: 0x0400348C RID: 13452
			ChangeCipherSpec = 20,
			// Token: 0x0400348D RID: 13453
			Alert,
			// Token: 0x0400348E RID: 13454
			Handshake,
			// Token: 0x0400348F RID: 13455
			AppData
		}
	}
}

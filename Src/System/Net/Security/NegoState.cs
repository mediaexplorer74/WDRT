using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x0200035F RID: 863
	internal class NegoState
	{
		// Token: 0x06001F2C RID: 7980 RVA: 0x00091143 File Offset: 0x0008F343
		internal NegoState(Stream innerStream, bool leaveStreamOpen)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this._InnerStream = innerStream;
			this._LeaveStreamOpen = leaveStreamOpen;
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x00091167 File Offset: 0x0008F367
		internal static string DefaultPackage
		{
			get
			{
				return "Negotiate";
			}
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00091170 File Offset: 0x0008F370
		internal void ValidateCreateContext(string package, NetworkCredential credential, string servicePrincipalName, ExtendedProtectionPolicy policy, ProtectionLevel protectionLevel, TokenImpersonationLevel impersonationLevel)
		{
			if (policy != null)
			{
				if (!AuthenticationManager.OSSupportsExtendedProtection)
				{
					if (policy.PolicyEnforcement == PolicyEnforcement.Always)
					{
						throw new PlatformNotSupportedException(SR.GetString("security_ExtendedProtection_NoOSSupport"));
					}
				}
				else if (policy.CustomChannelBinding == null && policy.CustomServiceNames == null)
				{
					throw new ArgumentException(SR.GetString("net_auth_must_specify_extended_protection_scheme"), "policy");
				}
				this._ExtendedProtectionPolicy = policy;
			}
			else
			{
				this._ExtendedProtectionPolicy = new ExtendedProtectionPolicy(PolicyEnforcement.Never);
			}
			this.ValidateCreateContext(package, true, credential, servicePrincipalName, this._ExtendedProtectionPolicy.CustomChannelBinding, protectionLevel, impersonationLevel);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x000911F8 File Offset: 0x0008F3F8
		internal void ValidateCreateContext(string package, bool isServer, NetworkCredential credential, string servicePrincipalName, ChannelBinding channelBinding, ProtectionLevel protectionLevel, TokenImpersonationLevel impersonationLevel)
		{
			if (this._Exception != null && !this._CanRetryAuthentication)
			{
				throw this._Exception;
			}
			if (this._Context != null && this._Context.IsValidContext)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_reauth"));
			}
			if (credential == null)
			{
				throw new ArgumentNullException("credential");
			}
			if (servicePrincipalName == null)
			{
				throw new ArgumentNullException("servicePrincipalName");
			}
			if (impersonationLevel != TokenImpersonationLevel.Identification && impersonationLevel != TokenImpersonationLevel.Impersonation && impersonationLevel != TokenImpersonationLevel.Delegation)
			{
				throw new ArgumentOutOfRangeException("impersonationLevel", impersonationLevel.ToString(), SR.GetString("net_auth_supported_impl_levels"));
			}
			if (this._Context != null && this.IsServer != isServer)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_client_server"));
			}
			this._Exception = null;
			this._RemoteOk = false;
			this._Framer = new StreamFramer(this._InnerStream);
			this._Framer.WriteHeader.MessageId = 22;
			this._ExpectedProtectionLevel = protectionLevel;
			this._ExpectedImpersonationLevel = (isServer ? impersonationLevel : TokenImpersonationLevel.None);
			this._WriteSequenceNumber = 0U;
			this._ReadSequenceNumber = 0U;
			ContextFlags contextFlags = ContextFlags.Connection;
			if (protectionLevel == ProtectionLevel.None && !isServer)
			{
				package = "NTLM";
			}
			else if (protectionLevel == ProtectionLevel.EncryptAndSign)
			{
				contextFlags |= ContextFlags.Confidentiality;
			}
			else if (protectionLevel == ProtectionLevel.Sign)
			{
				contextFlags |= ContextFlags.ReplayDetect | ContextFlags.SequenceDetect | ContextFlags.AcceptStream;
			}
			if (isServer)
			{
				if (this._ExtendedProtectionPolicy.PolicyEnforcement == PolicyEnforcement.WhenSupported)
				{
					contextFlags |= ContextFlags.AllowMissingBindings;
				}
				if (this._ExtendedProtectionPolicy.PolicyEnforcement != PolicyEnforcement.Never && this._ExtendedProtectionPolicy.ProtectionScenario == ProtectionScenario.TrustedProxy)
				{
					contextFlags |= ContextFlags.ProxyBindings;
				}
			}
			else
			{
				if (protectionLevel != ProtectionLevel.None)
				{
					contextFlags |= ContextFlags.MutualAuth;
				}
				if (impersonationLevel == TokenImpersonationLevel.Identification)
				{
					contextFlags |= ContextFlags.AcceptIntegrity;
				}
				if (impersonationLevel == TokenImpersonationLevel.Delegation)
				{
					contextFlags |= ContextFlags.Delegate;
				}
			}
			this._CanRetryAuthentication = false;
			if (!(credential is SystemNetworkCredential))
			{
				ExceptionHelper.ControlPrincipalPermission.Demand();
			}
			try
			{
				this._Context = new NTAuthentication(isServer, package, credential, servicePrincipalName, contextFlags, channelBinding);
			}
			catch (Win32Exception ex)
			{
				throw new AuthenticationException(SR.GetString("net_auth_SSPI"), ex);
			}
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000913E4 File Offset: 0x0008F5E4
		private Exception SetException(Exception e)
		{
			if (this._Exception == null || !(this._Exception is ObjectDisposedException))
			{
				this._Exception = e;
			}
			if (this._Exception != null && this._Context != null)
			{
				this._Context.CloseContext();
			}
			return this._Exception;
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x00091423 File Offset: 0x0008F623
		internal bool IsAuthenticated
		{
			get
			{
				return this._Context != null && this.HandshakeComplete && this._Exception == null && this._RemoteOk;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x00091445 File Offset: 0x0008F645
		internal bool IsMutuallyAuthenticated
		{
			get
			{
				return this.IsAuthenticated && !this._Context.IsNTLM && this._Context.IsMutualAuthFlag;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x0009146B File Offset: 0x0008F66B
		internal bool IsEncrypted
		{
			get
			{
				return this.IsAuthenticated && this._Context.IsConfidentialityFlag;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x00091482 File Offset: 0x0008F682
		internal bool IsSigned
		{
			get
			{
				return this.IsAuthenticated && (this._Context.IsIntegrityFlag || this._Context.IsConfidentialityFlag);
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x000914A8 File Offset: 0x0008F6A8
		internal bool IsServer
		{
			get
			{
				return this._Context != null && this._Context.IsServer;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x000914BF File Offset: 0x0008F6BF
		internal bool CanGetSecureStream
		{
			get
			{
				return this._Context.IsConfidentialityFlag || this._Context.IsIntegrityFlag;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x000914DB File Offset: 0x0008F6DB
		internal TokenImpersonationLevel AllowedImpersonation
		{
			get
			{
				this.CheckThrow(true);
				return this.PrivateImpersonationLevel;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x000914EA File Offset: 0x0008F6EA
		private TokenImpersonationLevel PrivateImpersonationLevel
		{
			get
			{
				if (this._Context.IsDelegationFlag && this._Context.ProtocolName != "NTLM")
				{
					return TokenImpersonationLevel.Delegation;
				}
				if (!this._Context.IsIdentifyFlag)
				{
					return TokenImpersonationLevel.Impersonation;
				}
				return TokenImpersonationLevel.Identification;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x00091522 File Offset: 0x0008F722
		private bool HandshakeComplete
		{
			get
			{
				return this._Context.IsCompleted && this._Context.IsValidContext;
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00091540 File Offset: 0x0008F740
		internal IIdentity GetIdentity()
		{
			this.CheckThrow(true);
			string text = (this._Context.IsServer ? this._Context.AssociatedName : this._Context.Spn);
			string text2 = "NTLM";
			text2 = this._Context.ProtocolName;
			if (this._Context.IsServer)
			{
				SafeCloseHandle safeCloseHandle = null;
				try
				{
					safeCloseHandle = this._Context.GetContextToken();
					string protocolName = this._Context.ProtocolName;
					return new WindowsIdentity(safeCloseHandle.DangerousGetHandle(), protocolName, WindowsAccountType.Normal, true);
				}
				catch (SecurityException)
				{
				}
				finally
				{
					if (safeCloseHandle != null)
					{
						safeCloseHandle.Close();
					}
				}
			}
			return new GenericIdentity(text, text2);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00091600 File Offset: 0x0008F800
		internal void CheckThrow(bool authSucessCheck)
		{
			if (this._Exception != null)
			{
				throw this._Exception;
			}
			if (authSucessCheck && !this.IsAuthenticated)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_noauth"));
			}
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0009162C File Offset: 0x0008F82C
		internal void Close()
		{
			this._Exception = new ObjectDisposedException("NegotiateStream");
			if (this._Context != null)
			{
				this._Context.CloseContext();
			}
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00091654 File Offset: 0x0008F854
		internal void ProcessAuthentication(LazyAsyncResult lazyResult)
		{
			this.CheckThrow(false);
			if (Interlocked.Exchange(ref this._NestedAuth, 1) == 1)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					(lazyResult == null) ? "BeginAuthenticate" : "Authenticate",
					"authenticate"
				}));
			}
			try
			{
				if (this._Context.IsServer)
				{
					this.StartReceiveBlob(lazyResult);
				}
				else
				{
					this.StartSendBlob(null, lazyResult);
				}
			}
			catch (Exception ex)
			{
				ex = this.SetException(ex);
				throw;
			}
			finally
			{
				if (lazyResult == null || this._Exception != null)
				{
					this._NestedAuth = 0;
				}
			}
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00091704 File Offset: 0x0008F904
		internal void EndProcessAuthentication(IAsyncResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = result as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("net_io_async_result", new object[] { result.GetType().FullName }), "asyncResult");
			}
			if (Interlocked.Exchange(ref this._NestedAuth, 0) == 0)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndAuthenticate" }));
			}
			lazyAsyncResult.InternalWaitForCompletion();
			Exception ex = lazyAsyncResult.Result as Exception;
			if (ex != null)
			{
				ex = this.SetException(ex);
				throw ex;
			}
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000917A0 File Offset: 0x0008F9A0
		private bool CheckSpn()
		{
			if (this._Context.IsKerberos)
			{
				return true;
			}
			if (this._ExtendedProtectionPolicy.PolicyEnforcement == PolicyEnforcement.Never || this._ExtendedProtectionPolicy.CustomServiceNames == null)
			{
				return true;
			}
			if (!AuthenticationManager.OSSupportsExtendedProtection)
			{
				return true;
			}
			string clientSpecifiedSpn = this._Context.ClientSpecifiedSpn;
			if (string.IsNullOrEmpty(clientSpecifiedSpn))
			{
				return this._ExtendedProtectionPolicy.PolicyEnforcement == PolicyEnforcement.WhenSupported;
			}
			return this._ExtendedProtectionPolicy.CustomServiceNames.Contains(clientSpecifiedSpn);
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00091818 File Offset: 0x0008FA18
		private void StartSendBlob(byte[] message, LazyAsyncResult lazyResult)
		{
			Win32Exception ex = null;
			if (message != NegoState._EmptyMessage)
			{
				message = this.GetOutgoingBlob(message, ref ex);
			}
			if (ex != null)
			{
				this.StartSendAuthResetSignal(lazyResult, message, ex);
				return;
			}
			if (this.HandshakeComplete)
			{
				if (this._Context.IsServer && !this.CheckSpn())
				{
					Exception ex2 = new AuthenticationException(SR.GetString("net_auth_bad_client_creds_or_target_mismatch"));
					int num = 1790;
					message = new byte[8];
					for (int i = message.Length - 1; i >= 0; i--)
					{
						message[i] = (byte)(num & 255);
						num = (int)((uint)num >> 8);
					}
					this.StartSendAuthResetSignal(lazyResult, message, ex2);
					return;
				}
				if (this.PrivateImpersonationLevel < this._ExpectedImpersonationLevel)
				{
					Exception ex3 = new AuthenticationException(SR.GetString("net_auth_context_expectation", new object[]
					{
						this._ExpectedImpersonationLevel.ToString(),
						this.PrivateImpersonationLevel.ToString()
					}));
					int num2 = 1790;
					message = new byte[8];
					for (int j = message.Length - 1; j >= 0; j--)
					{
						message[j] = (byte)(num2 & 255);
						num2 = (int)((uint)num2 >> 8);
					}
					this.StartSendAuthResetSignal(lazyResult, message, ex3);
					return;
				}
				ProtectionLevel protectionLevel = (this._Context.IsConfidentialityFlag ? ProtectionLevel.EncryptAndSign : (this._Context.IsIntegrityFlag ? ProtectionLevel.Sign : ProtectionLevel.None));
				if (protectionLevel < this._ExpectedProtectionLevel)
				{
					Exception ex4 = new AuthenticationException(SR.GetString("net_auth_context_expectation", new object[]
					{
						protectionLevel.ToString(),
						this._ExpectedProtectionLevel.ToString()
					}));
					int num3 = 1790;
					message = new byte[8];
					for (int k = message.Length - 1; k >= 0; k--)
					{
						message[k] = (byte)(num3 & 255);
						num3 = (int)((uint)num3 >> 8);
					}
					this.StartSendAuthResetSignal(lazyResult, message, ex4);
					return;
				}
				this._Framer.WriteHeader.MessageId = 20;
				if (this._Context.IsServer)
				{
					this._RemoteOk = true;
					if (message == null)
					{
						message = NegoState._EmptyMessage;
					}
				}
			}
			else if (message == null || message == NegoState._EmptyMessage)
			{
				throw new InternalException();
			}
			if (message != null)
			{
				if (lazyResult == null)
				{
					this._Framer.WriteMessage(message);
				}
				else
				{
					IAsyncResult asyncResult = this._Framer.BeginWriteMessage(message, NegoState._WriteCallback, lazyResult);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this._Framer.EndWriteMessage(asyncResult);
				}
			}
			this.CheckCompletionBeforeNextReceive(lazyResult);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00091A78 File Offset: 0x0008FC78
		private void CheckCompletionBeforeNextReceive(LazyAsyncResult lazyResult)
		{
			if (this.HandshakeComplete && this._RemoteOk)
			{
				if (lazyResult != null)
				{
					lazyResult.InvokeCallback();
				}
				return;
			}
			this.StartReceiveBlob(lazyResult);
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00091A9C File Offset: 0x0008FC9C
		private void StartReceiveBlob(LazyAsyncResult lazyResult)
		{
			byte[] array;
			if (lazyResult == null)
			{
				array = this._Framer.ReadMessage();
			}
			else
			{
				IAsyncResult asyncResult = this._Framer.BeginReadMessage(NegoState._ReadCallback, lazyResult);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				array = this._Framer.EndReadMessage(asyncResult);
			}
			this.ProcessReceivedBlob(array, lazyResult);
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00091AEC File Offset: 0x0008FCEC
		private void ProcessReceivedBlob(byte[] message, LazyAsyncResult lazyResult)
		{
			if (message == null)
			{
				throw new AuthenticationException(SR.GetString("net_auth_eof"), null);
			}
			if (this._Framer.ReadHeader.MessageId == 21)
			{
				Win32Exception ex = null;
				if (message.Length >= 8)
				{
					long num = 0L;
					for (int i = 0; i < 8; i++)
					{
						num = (num << 8) + (long)((ulong)message[i]);
					}
					ex = new Win32Exception((int)num);
				}
				if (ex != null)
				{
					if (ex.NativeErrorCode == -2146893044)
					{
						throw new InvalidCredentialException(SR.GetString("net_auth_bad_client_creds"), ex);
					}
					if (ex.NativeErrorCode == 1790)
					{
						throw new AuthenticationException(SR.GetString("net_auth_context_expectation_remote"), ex);
					}
				}
				throw new AuthenticationException(SR.GetString("net_auth_alert"), ex);
			}
			if (this._Framer.ReadHeader.MessageId == 20)
			{
				this._RemoteOk = true;
			}
			else if (this._Framer.ReadHeader.MessageId != 22)
			{
				throw new AuthenticationException(SR.GetString("net_io_header_id", new object[]
				{
					"MessageId",
					this._Framer.ReadHeader.MessageId,
					22
				}), null);
			}
			this.CheckCompletionBeforeNextSend(message, lazyResult);
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00091C14 File Offset: 0x0008FE14
		private void CheckCompletionBeforeNextSend(byte[] message, LazyAsyncResult lazyResult)
		{
			if (!this.HandshakeComplete)
			{
				this.StartSendBlob(message, lazyResult);
				return;
			}
			if (!this._RemoteOk)
			{
				throw new AuthenticationException(SR.GetString("net_io_header_id", new object[]
				{
					"MessageId",
					this._Framer.ReadHeader.MessageId,
					20
				}), null);
			}
			if (lazyResult != null)
			{
				lazyResult.InvokeCallback();
			}
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00091C84 File Offset: 0x0008FE84
		private void StartSendAuthResetSignal(LazyAsyncResult lazyResult, byte[] message, Exception exception)
		{
			this._Framer.WriteHeader.MessageId = 21;
			Win32Exception ex = exception as Win32Exception;
			if (ex != null && ex.NativeErrorCode == -2146893044)
			{
				if (this.IsServer)
				{
					exception = new InvalidCredentialException(SR.GetString("net_auth_bad_client_creds"), exception);
				}
				else
				{
					exception = new InvalidCredentialException(SR.GetString("net_auth_bad_client_creds_or_target_mismatch"), exception);
				}
			}
			if (!(exception is AuthenticationException))
			{
				exception = new AuthenticationException(SR.GetString("net_auth_SSPI"), exception);
			}
			if (lazyResult == null)
			{
				this._Framer.WriteMessage(message);
			}
			else
			{
				lazyResult.Result = exception;
				IAsyncResult asyncResult = this._Framer.BeginWriteMessage(message, NegoState._WriteCallback, lazyResult);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				this._Framer.EndWriteMessage(asyncResult);
			}
			this._CanRetryAuthentication = true;
			throw exception;
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00091D4C File Offset: 0x0008FF4C
		private static void WriteCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)transportResult.AsyncState;
			try
			{
				NegoState negoState = (NegoState)lazyAsyncResult.AsyncObject;
				negoState._Framer.EndWriteMessage(transportResult);
				if (lazyAsyncResult.Result is Exception)
				{
					negoState._CanRetryAuthentication = true;
					throw (Exception)lazyAsyncResult.Result;
				}
				negoState.CheckCompletionBeforeNextReceive(lazyAsyncResult);
			}
			catch (Exception ex)
			{
				if (lazyAsyncResult.InternalPeekCompleted)
				{
					throw;
				}
				lazyAsyncResult.InvokeCallback(ex);
			}
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00091DD4 File Offset: 0x0008FFD4
		private static void ReadCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)transportResult.AsyncState;
			try
			{
				NegoState negoState = (NegoState)lazyAsyncResult.AsyncObject;
				byte[] array = negoState._Framer.EndReadMessage(transportResult);
				negoState.ProcessReceivedBlob(array, lazyAsyncResult);
			}
			catch (Exception ex)
			{
				if (lazyAsyncResult.InternalPeekCompleted)
				{
					throw;
				}
				lazyAsyncResult.InvokeCallback(ex);
			}
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00091E40 File Offset: 0x00090040
		private byte[] GetOutgoingBlob(byte[] incomingBlob, ref Win32Exception e)
		{
			SecurityStatus securityStatus;
			byte[] array = this._Context.GetOutgoingBlob(incomingBlob, false, out securityStatus);
			if ((securityStatus & (SecurityStatus)(-2147483648)) != SecurityStatus.OK)
			{
				e = new Win32Exception((int)securityStatus);
				array = new byte[8];
				for (int i = array.Length - 1; i >= 0; i--)
				{
					array[i] = (byte)(securityStatus & (SecurityStatus)255);
					securityStatus >>= 8;
				}
			}
			if (array != null && array.Length == 0)
			{
				array = NegoState._EmptyMessage;
			}
			return array;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00091EA2 File Offset: 0x000900A2
		internal int EncryptData(byte[] buffer, int offset, int count, ref byte[] outBuffer)
		{
			this.CheckThrow(true);
			this._WriteSequenceNumber += 1U;
			return this._Context.Encrypt(buffer, offset, count, ref outBuffer, this._WriteSequenceNumber);
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00091ECF File Offset: 0x000900CF
		internal int DecryptData(byte[] buffer, int offset, int count, out int newOffset)
		{
			this.CheckThrow(true);
			this._ReadSequenceNumber += 1U;
			return this._Context.Decrypt(buffer, offset, count, out newOffset, this._ReadSequenceNumber);
		}

		// Token: 0x04001CFA RID: 7418
		private const int ERROR_TRUST_FAILURE = 1790;

		// Token: 0x04001CFB RID: 7419
		private static readonly byte[] _EmptyMessage = new byte[0];

		// Token: 0x04001CFC RID: 7420
		private static readonly AsyncCallback _ReadCallback = new AsyncCallback(NegoState.ReadCallback);

		// Token: 0x04001CFD RID: 7421
		private static readonly AsyncCallback _WriteCallback = new AsyncCallback(NegoState.WriteCallback);

		// Token: 0x04001CFE RID: 7422
		private Stream _InnerStream;

		// Token: 0x04001CFF RID: 7423
		private bool _LeaveStreamOpen;

		// Token: 0x04001D00 RID: 7424
		private Exception _Exception;

		// Token: 0x04001D01 RID: 7425
		private StreamFramer _Framer;

		// Token: 0x04001D02 RID: 7426
		private NTAuthentication _Context;

		// Token: 0x04001D03 RID: 7427
		private int _NestedAuth;

		// Token: 0x04001D04 RID: 7428
		internal const int c_MaxReadFrameSize = 65536;

		// Token: 0x04001D05 RID: 7429
		internal const int c_MaxWriteDataSize = 64512;

		// Token: 0x04001D06 RID: 7430
		private bool _CanRetryAuthentication;

		// Token: 0x04001D07 RID: 7431
		private ProtectionLevel _ExpectedProtectionLevel;

		// Token: 0x04001D08 RID: 7432
		private TokenImpersonationLevel _ExpectedImpersonationLevel;

		// Token: 0x04001D09 RID: 7433
		private uint _WriteSequenceNumber;

		// Token: 0x04001D0A RID: 7434
		private uint _ReadSequenceNumber;

		// Token: 0x04001D0B RID: 7435
		private ExtendedProtectionPolicy _ExtendedProtectionPolicy;

		// Token: 0x04001D0C RID: 7436
		private bool _RemoteOk;
	}
}

using System;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Security
{
	/// <summary>Provides a stream that uses the Negotiate security protocol to authenticate the client, and optionally the server, in client-server communication.</summary>
	// Token: 0x02000357 RID: 855
	public class NegotiateStream : AuthenticatedStream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.NegotiateStream" /> class using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.NegotiateStream" /> for sending and receiving data.</param>
		// Token: 0x06001E89 RID: 7817 RVA: 0x0008FAF0 File Offset: 0x0008DCF0
		public NegotiateStream(Stream innerStream)
			: this(innerStream, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.NegotiateStream" /> class using the specified <see cref="T:System.IO.Stream" /> and stream closure behavior.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.NegotiateStream" /> for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">
		///   <see langword="true" /> to indicate that closing this <see cref="T:System.Net.Security.NegotiateStream" /> has no effect on <paramref name="innerStream" />; <see langword="false" /> to indicate that closing this <see cref="T:System.Net.Security.NegotiateStream" /> also closes <paramref name="innerStream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x06001E8A RID: 7818 RVA: 0x0008FAFA File Offset: 0x0008DCFA
		public NegotiateStream(Stream innerStream, bool leaveInnerStreamOpen)
			: base(innerStream, leaveInnerStreamOpen)
		{
			this._NegoState = new NegoState(innerStream, leaveInnerStreamOpen);
			this._Package = NegoState.DefaultPackage;
			this.InitializeStreamPart();
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection.</summary>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06001E8B RID: 7819 RVA: 0x0008FB22 File Offset: 0x0008DD22
		public virtual void AuthenticateAsClient()
		{
			this.AuthenticateAsClient((NetworkCredential)CredentialCache.DefaultCredentials, null, string.Empty, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified client credential.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.</exception>
		// Token: 0x06001E8C RID: 7820 RVA: 0x0008FB3C File Offset: 0x0008DD3C
		public virtual void AuthenticateAsClient(NetworkCredential credential, string targetName)
		{
			this.AuthenticateAsClient(credential, null, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified client credential and the channel binding.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06001E8D RID: 7821 RVA: 0x0008FB49 File Offset: 0x0008DD49
		public virtual void AuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName)
		{
			this.AuthenticateAsClient(credential, binding, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials and authentication options.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowedImpersonationLevel" /> is not a valid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06001E8E RID: 7822 RVA: 0x0008FB56 File Offset: 0x0008DD56
		public virtual void AuthenticateAsClient(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			this.AuthenticateAsClient(credential, null, targetName, requiredProtectionLevel, allowedImpersonationLevel);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credential, authentication options, and channel binding.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowedImpersonationLevel" /> is not a valid value.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06001E8F RID: 7823 RVA: 0x0008FB64 File Offset: 0x0008DD64
		public virtual void AuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			this._NegoState.ValidateCreateContext(this._Package, false, credential, targetName, binding, requiredProtectionLevel, allowedImpersonationLevel);
			this._NegoState.ProcessAuthentication(null);
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. This method does not block.</summary>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06001E90 RID: 7824 RVA: 0x0008FB8B File Offset: 0x0008DD8B
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient((NetworkCredential)CredentialCache.DefaultCredentials, null, string.Empty, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="targetName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06001E91 RID: 7825 RVA: 0x0008FBA7 File Offset: 0x0008DDA7
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, string targetName, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(credential, null, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials and channel binding. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="targetName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06001E92 RID: 7826 RVA: 0x0008FBB7 File Offset: 0x0008DDB7
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(credential, binding, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials and authentication options. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="targetName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06001E93 RID: 7827 RVA: 0x0008FBC8 File Offset: 0x0008DDC8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(credential, null, targetName, requiredProtectionLevel, allowedImpersonationLevel, asyncCallback, asyncState);
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials, authentication options, and channel binding. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="targetName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06001E94 RID: 7828 RVA: 0x0008FBDC File Offset: 0x0008DDDC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			this._NegoState.ValidateCreateContext(this._Package, false, credential, targetName, binding, requiredProtectionLevel, allowedImpersonationLevel);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this._NegoState, asyncState, asyncCallback);
			this._NegoState.ProcessAuthentication(lazyAsyncResult);
			return lazyAsyncResult;
		}

		/// <summary>Ends a pending asynchronous client authentication operation that was started with a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsClient" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsClient" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsClient" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending client authentication to complete.</exception>
		// Token: 0x06001E95 RID: 7829 RVA: 0x0008FC1F File Offset: 0x0008DE1F
		public virtual void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			this._NegoState.EndProcessAuthentication(asyncResult);
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection.</summary>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x06001E96 RID: 7830 RVA: 0x0008FC2D File Offset: 0x0008DE2D
		public virtual void AuthenticateAsServer()
		{
			this.AuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, null, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified extended protection policy.</summary>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x06001E97 RID: 7831 RVA: 0x0008FC42 File Offset: 0x0008DE42
		public virtual void AuthenticateAsServer(ExtendedProtectionPolicy policy)
		{
			this.AuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, policy, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified server credentials and authentication options.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the server.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to try to r-authenticate.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x06001E98 RID: 7832 RVA: 0x0008FC57 File Offset: 0x0008DE57
		public virtual void AuthenticateAsServer(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			this.AuthenticateAsServer(credential, null, requiredProtectionLevel, requiredImpersonationLevel);
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified server credentials, authentication options, and extended protection policy.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to try to r-authenticate.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x06001E99 RID: 7833 RVA: 0x0008FC63 File Offset: 0x0008DE63
		public virtual void AuthenticateAsServer(NetworkCredential credential, ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			this._NegoState.ValidateCreateContext(this._Package, credential, string.Empty, policy, requiredProtectionLevel, requiredImpersonationLevel);
			this._NegoState.ProcessAuthentication(null);
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. This method does not block.</summary>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x06001E9A RID: 7834 RVA: 0x0008FC8C File Offset: 0x0008DE8C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, null, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified extended protection policy. This method does not block.</summary>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x06001E9B RID: 7835 RVA: 0x0008FCA3 File Offset: 0x0008DEA3
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(ExtendedProtectionPolicy policy, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, policy, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified server credentials and authentication options. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x06001E9C RID: 7836 RVA: 0x0008FCBA File Offset: 0x0008DEBA
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer(credential, null, requiredProtectionLevel, requiredImpersonationLevel, asyncCallback, asyncState);
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified server credentials, authentication options, and extended protection policy. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x06001E9D RID: 7837 RVA: 0x0008FCCC File Offset: 0x0008DECC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(NetworkCredential credential, ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			this._NegoState.ValidateCreateContext(this._Package, credential, string.Empty, policy, requiredProtectionLevel, requiredImpersonationLevel);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this._NegoState, asyncState, asyncCallback);
			this._NegoState.ProcessAuthentication(lazyAsyncResult);
			return lazyAsyncResult;
		}

		/// <summary>Ends a pending asynchronous client authentication operation that was started with a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsServer" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsServer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsServer" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending authentication to complete.</exception>
		// Token: 0x06001E9E RID: 7838 RVA: 0x0008FD11 File Offset: 0x0008DF11
		public virtual void EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			this._NegoState.EndProcessAuthentication(asyncResult);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06001E9F RID: 7839 RVA: 0x0008FD1F File Offset: 0x0008DF1F
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync()
		{
			return Task.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsClient), new Action<IAsyncResult>(this.EndAuthenticateAsClient), null);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified client credential.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.</exception>
		// Token: 0x06001EA0 RID: 7840 RVA: 0x0008FD46 File Offset: 0x0008DF46
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, string targetName)
		{
			return Task.Factory.FromAsync<NetworkCredential, string>(new Func<NetworkCredential, string, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsClient), new Action<IAsyncResult>(this.EndAuthenticateAsClient), credential, targetName, null);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified credentials and authentication options.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowedImpersonationLevel" /> is not a valid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06001EA1 RID: 7841 RVA: 0x0008FD70 File Offset: 0x0008DF70
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			return Task.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginAuthenticateAsClient(credential, targetName, requiredProtectionLevel, allowedImpersonationLevel, callback, state), new Action<IAsyncResult>(this.EndAuthenticateAsClient), null);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified client credential and the channel binding.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06001EA2 RID: 7842 RVA: 0x0008FDCB File Offset: 0x0008DFCB
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, ChannelBinding binding, string targetName)
		{
			return Task.Factory.FromAsync<NetworkCredential, ChannelBinding, string>(new Func<NetworkCredential, ChannelBinding, string, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsClient), new Action<IAsyncResult>(this.EndAuthenticateAsClient), credential, binding, targetName, null);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified credential, authentication options, and channel binding.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowedImpersonationLevel" /> is not a valid value.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06001EA3 RID: 7843 RVA: 0x0008FDF8 File Offset: 0x0008DFF8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			return Task.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginAuthenticateAsClient(credential, binding, targetName, requiredProtectionLevel, allowedImpersonationLevel, callback, state), new Action<IAsyncResult>(this.EndAuthenticateAsClient), null);
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x06001EA4 RID: 7844 RVA: 0x0008FE5B File Offset: 0x0008E05B
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsServerAsync()
		{
			return Task.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsServer), new Action<IAsyncResult>(this.EndAuthenticateAsServer), null);
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified extended protection policy.</summary>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x06001EA5 RID: 7845 RVA: 0x0008FE82 File Offset: 0x0008E082
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsServerAsync(ExtendedProtectionPolicy policy)
		{
			return Task.Factory.FromAsync<ExtendedProtectionPolicy>(new Func<ExtendedProtectionPolicy, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsServer), new Action<IAsyncResult>(this.EndAuthenticateAsServer), policy, null);
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified server credentials and authentication options.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the server.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to try to r-authenticate.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x06001EA6 RID: 7846 RVA: 0x0008FEAA File Offset: 0x0008E0AA
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsServerAsync(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			return Task.Factory.FromAsync<NetworkCredential, ProtectionLevel, TokenImpersonationLevel>(new Func<NetworkCredential, ProtectionLevel, TokenImpersonationLevel, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsServer), new Action<IAsyncResult>(this.EndAuthenticateAsServer), credential, requiredProtectionLevel, requiredImpersonationLevel, null);
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified server credentials, authentication options, and extended protection policy.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to try to r-authenticate.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x06001EA7 RID: 7847 RVA: 0x0008FED4 File Offset: 0x0008E0D4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsServerAsync(NetworkCredential credential, ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			return Task.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginAuthenticateAsServer(credential, policy, requiredProtectionLevel, requiredImpersonationLevel, callback, state), new Action<IAsyncResult>(this.EndAuthenticateAsClient), null);
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether authentication was successful.</summary>
		/// <returns>
		///   <see langword="true" /> if successful authentication occurred; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0008FF2F File Offset: 0x0008E12F
		public override bool IsAuthenticated
		{
			get
			{
				return this._NegoState.IsAuthenticated;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both the server and the client have been authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if the server has been authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x0008FF3C File Offset: 0x0008E13C
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				return this._NegoState.IsMutuallyAuthenticated;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this <see cref="T:System.Net.Security.NegotiateStream" /> uses data encryption.</summary>
		/// <returns>
		///   <see langword="true" /> if data is encrypted before being transmitted over the network and decrypted when it reaches the remote endpoint; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0008FF49 File Offset: 0x0008E149
		public override bool IsEncrypted
		{
			get
			{
				return this._NegoState.IsEncrypted;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the data sent using this stream is signed.</summary>
		/// <returns>
		///   <see langword="true" /> if the data is signed before being transmitted; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x0008FF56 File Offset: 0x0008E156
		public override bool IsSigned
		{
			get
			{
				return this._NegoState.IsSigned;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the local side of the connection used by this <see cref="T:System.Net.Security.NegotiateStream" /> was authenticated as the server.</summary>
		/// <returns>
		///   <see langword="true" /> if the local endpoint was successfully authenticated as the server side of the authenticated connection; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x0008FF63 File Offset: 0x0008E163
		public override bool IsServer
		{
			get
			{
				return this._NegoState.IsServer;
			}
		}

		/// <summary>Gets a value that indicates how the server can use the client's credentials.</summary>
		/// <returns>One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">Authentication failed or has not occurred.</exception>
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x0008FF70 File Offset: 0x0008E170
		public virtual TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this._NegoState.AllowedImpersonation;
			}
		}

		/// <summary>Gets information about the identity of the remote party sharing this authenticated stream.</summary>
		/// <returns>An <see cref="T:System.Security.Principal.IIdentity" /> object that describes the identity of the remote endpoint.</returns>
		/// <exception cref="T:System.InvalidOperationException">Authentication failed or has not occurred.</exception>
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x0008FF7D File Offset: 0x0008E17D
		public virtual IIdentity RemoteIdentity
		{
			get
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				if (this._RemoteIdentity == null)
				{
					this._RemoteIdentity = this._NegoState.GetIdentity();
				}
				return this._RemoteIdentity;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is seekable.</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x0008FFA9 File Offset: 0x0008E1A9
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is readable.</summary>
		/// <returns>
		///   <see langword="true" /> if authentication has occurred and the underlying stream is readable; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x0008FFAC File Offset: 0x0008E1AC
		public override bool CanRead
		{
			get
			{
				return this.IsAuthenticated && base.InnerStream.CanRead;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream supports time-outs.</summary>
		/// <returns>
		///   <see langword="true" /> if the underlying stream supports time-outs; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x0008FFC3 File Offset: 0x0008E1C3
		public override bool CanTimeout
		{
			get
			{
				return base.InnerStream.CanTimeout;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is writable.</summary>
		/// <returns>
		///   <see langword="true" /> if authentication has occurred and the underlying stream is writable; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x0008FFD0 File Offset: 0x0008E1D0
		public override bool CanWrite
		{
			get
			{
				return this.IsAuthenticated && base.InnerStream.CanWrite;
			}
		}

		/// <summary>Gets or sets the amount of time a read operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time that will elapse before a read operation fails.</returns>
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x0008FFE7 File Offset: 0x0008E1E7
		// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x0008FFF4 File Offset: 0x0008E1F4
		public override int ReadTimeout
		{
			get
			{
				return base.InnerStream.ReadTimeout;
			}
			set
			{
				base.InnerStream.ReadTimeout = value;
			}
		}

		/// <summary>Gets or sets the amount of time a write operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time that will elapse before a write operation fails.</returns>
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x00090002 File Offset: 0x0008E202
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x0009000F File Offset: 0x0008E20F
		public override int WriteTimeout
		{
			get
			{
				return base.InnerStream.WriteTimeout;
			}
			set
			{
				base.InnerStream.WriteTimeout = value;
			}
		}

		/// <summary>Gets the length of the underlying stream.</summary>
		/// <returns>A <see cref="T:System.Int64" /> that specifies the length of the underlying stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Getting the value of this property is not supported when the underlying stream is a <see cref="T:System.Net.Sockets.NetworkStream" />.</exception>
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x0009001D File Offset: 0x0008E21D
		public override long Length
		{
			get
			{
				return base.InnerStream.Length;
			}
		}

		/// <summary>Gets or sets the current position in the underlying stream.</summary>
		/// <returns>A <see cref="T:System.Int64" /> that specifies the current position in the underlying stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Setting this property is not supported.  
		/// -or-
		///  Getting the value of this property is not supported when the underlying stream is a <see cref="T:System.Net.Sockets.NetworkStream" />.</exception>
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x0009002A File Offset: 0x0008E22A
		// (set) Token: 0x06001EB9 RID: 7865 RVA: 0x00090037 File Offset: 0x0008E237
		public override long Position
		{
			get
			{
				return base.InnerStream.Position;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		/// <summary>Sets the length of the underlying stream.</summary>
		/// <param name="value">An <see cref="T:System.Int64" /> value that specifies the length of the stream.</param>
		// Token: 0x06001EBA RID: 7866 RVA: 0x00090048 File Offset: 0x0008E248
		public override void SetLength(long value)
		{
			base.InnerStream.SetLength(value);
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="offset">This value is ignored.</param>
		/// <param name="origin">This value is ignored.</param>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Seeking is not supported on <see cref="T:System.Net.Security.NegotiateStream" />.</exception>
		// Token: 0x06001EBB RID: 7867 RVA: 0x00090056 File Offset: 0x0008E256
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		/// <summary>Causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x06001EBC RID: 7868 RVA: 0x00090067 File Offset: 0x0008E267
		public override void Flush()
		{
			base.InnerStream.Flush();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Security.NegotiateStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001EBD RID: 7869 RVA: 0x00090074 File Offset: 0x0008E274
		protected override void Dispose(bool disposing)
		{
			try
			{
				this._NegoState.Close();
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Reads data from this stream and stores it in the specified array.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that receives the bytes read from the stream.</param>
		/// <param name="offset">A <see cref="T:System.Int32" /> containing the zero-based location in <paramref name="buffer" /> at which to begin storing the data read from this stream.</param>
		/// <param name="count">A <see cref="T:System.Int32" /> containing the maximum number of bytes to read from the stream.</param>
		/// <returns>A <see cref="T:System.Int32" /> value that specifies the number of bytes read from the underlying stream. When there is no more data to be read, returns 0.</returns>
		/// <exception cref="T:System.IO.IOException">The read operation failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		/// <exception cref="T:System.NotSupportedException">A <see cref="M:System.Net.Security.NegotiateStream.Read(System.Byte[],System.Int32,System.Int32)" /> operation is already in progress.</exception>
		// Token: 0x06001EBE RID: 7870 RVA: 0x000900A8 File Offset: 0x0008E2A8
		public override int Read(byte[] buffer, int offset, int count)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				return base.InnerStream.Read(buffer, offset, count);
			}
			return this.ProcessRead(buffer, offset, count, null);
		}

		/// <summary>Write the specified number of <see cref="T:System.Byte" />s to the underlying stream using the specified buffer and offset.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that supplies the bytes written to the stream.</param>
		/// <param name="offset">An <see cref="T:System.Int32" /> containing the zero-based location in <paramref name="buffer" /> at which to begin reading bytes to be written to the stream.</param>
		/// <param name="count">A <see cref="T:System.Int32" /> containing the number of bytes to read from <paramref name="buffer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset is less than 0" />.  
		/// -or-
		///  <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-
		///  <paramref name="offset" /> plus count is greater than the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.  
		/// -or-
		///  Encryption is in use, but the data could not be encrypted.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a write operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06001EBF RID: 7871 RVA: 0x000900DC File Offset: 0x0008E2DC
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				base.InnerStream.Write(buffer, offset, count);
				return;
			}
			this.ProcessWrite(buffer, offset, count, null);
		}

		/// <summary>Begins an asynchronous read operation that reads data from the stream and stores it in the specified array.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that receives the bytes read from the stream.</param>
		/// <param name="offset">The zero-based location in <paramref name="buffer" /> at which to begin storing the data read from this stream.</param>
		/// <param name="count">The maximum number of bytes to read from the stream.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the read operation is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the read operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-
		///  <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-
		///  <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed.  
		/// -or-
		///  Encryption is in use, but the data could not be decrypted.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a read operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06001EC0 RID: 7872 RVA: 0x00090110 File Offset: 0x0008E310
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				return base.InnerStream.BeginRead(buffer, offset, count, asyncCallback, asyncState);
			}
			BufferAsyncResult bufferAsyncResult = new BufferAsyncResult(this, buffer, offset, count, asyncState, asyncCallback);
			AsyncProtocolRequest asyncProtocolRequest = new AsyncProtocolRequest(bufferAsyncResult);
			this.ProcessRead(buffer, offset, count, asyncProtocolRequest);
			return bufferAsyncResult;
		}

		/// <summary>Ends an asynchronous read operation that was started with a call to <see cref="M:System.Net.Security.NegotiateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /></param>
		/// <returns>A <see cref="T:System.Int32" /> value that specifies the number of bytes read from the underlying stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The asyncResult was not created by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending read operation to complete.
		/// -or-
		/// Authentication has not occurred.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed.</exception>
		// Token: 0x06001EC1 RID: 7873 RVA: 0x0009016C File Offset: 0x0008E36C
		public override int EndRead(IAsyncResult asyncResult)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				return base.InnerStream.EndRead(asyncResult);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			BufferAsyncResult bufferAsyncResult = asyncResult as BufferAsyncResult;
			if (bufferAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("net_io_async_result", new object[] { asyncResult.GetType().FullName }), "asyncResult");
			}
			if (Interlocked.Exchange(ref this._NestedRead, 0) == 0)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndRead" }));
			}
			bufferAsyncResult.InternalWaitForCompletion();
			if (!(bufferAsyncResult.Result is Exception))
			{
				return (int)bufferAsyncResult.Result;
			}
			if (bufferAsyncResult.Result is IOException)
			{
				throw (Exception)bufferAsyncResult.Result;
			}
			throw new IOException(SR.GetString("net_io_read"), (Exception)bufferAsyncResult.Result);
		}

		/// <summary>Begins an asynchronous write operation that writes <see cref="T:System.Byte" />s from the specified buffer to the stream.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that supplies the bytes to be written to the stream.</param>
		/// <param name="offset">The zero-based location in <paramref name="buffer" /> at which to begin reading bytes to be written to the stream.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> value that specifies the number of bytes to read from <paramref name="buffer" />.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the write operation is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset is less than 0" />.  
		/// -or-
		///  <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-
		///  <paramref name="offset" /> plus count is greater than the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.  
		/// -or-
		///  Encryption is in use, but the data could not be encrypted.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a write operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06001EC2 RID: 7874 RVA: 0x00090260 File Offset: 0x0008E460
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				return base.InnerStream.BeginWrite(buffer, offset, count, asyncCallback, asyncState);
			}
			BufferAsyncResult bufferAsyncResult = new BufferAsyncResult(this, buffer, offset, count, true, asyncState, asyncCallback);
			AsyncProtocolRequest asyncProtocolRequest = new AsyncProtocolRequest(bufferAsyncResult);
			this.ProcessWrite(buffer, offset, count, asyncProtocolRequest);
			return bufferAsyncResult;
		}

		/// <summary>Ends an asynchronous write operation that was started with a call to <see cref="M:System.Net.Security.NegotiateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /></param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The asyncResult was not created by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending write operation to complete.
		/// -or-
		/// Authentication has not occurred.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.</exception>
		// Token: 0x06001EC3 RID: 7875 RVA: 0x000902BC File Offset: 0x0008E4BC
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				base.InnerStream.EndWrite(asyncResult);
				return;
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			BufferAsyncResult bufferAsyncResult = asyncResult as BufferAsyncResult;
			if (bufferAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("net_io_async_result", new object[] { asyncResult.GetType().FullName }), "asyncResult");
			}
			if (Interlocked.Exchange(ref this._NestedWrite, 0) == 0)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndWrite" }));
			}
			bufferAsyncResult.InternalWaitForCompletion();
			if (!(bufferAsyncResult.Result is Exception))
			{
				return;
			}
			if (bufferAsyncResult.Result is IOException)
			{
				throw (Exception)bufferAsyncResult.Result;
			}
			throw new IOException(SR.GetString("net_io_write"), (Exception)bufferAsyncResult.Result);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000903A4 File Offset: 0x0008E5A4
		private void InitializeStreamPart()
		{
			this._ReadHeader = new byte[4];
			this._FrameReader = new FixedSizeReader(base.InnerStream);
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x000903C3 File Offset: 0x0008E5C3
		private byte[] InternalBuffer
		{
			get
			{
				return this._InternalBuffer;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x000903CB File Offset: 0x0008E5CB
		private int InternalOffset
		{
			get
			{
				return this._InternalOffset;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x000903D3 File Offset: 0x0008E5D3
		private int InternalBufferCount
		{
			get
			{
				return this._InternalBufferCount;
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x000903DB File Offset: 0x0008E5DB
		private void DecrementInternalBufferCount(int decrCount)
		{
			this._InternalOffset += decrCount;
			this._InternalBufferCount -= decrCount;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x000903F9 File Offset: 0x0008E5F9
		private void EnsureInternalBufferSize(int bytes)
		{
			this._InternalBufferCount = bytes;
			this._InternalOffset = 0;
			if (this.InternalBuffer == null || this.InternalBuffer.Length < bytes)
			{
				this._InternalBuffer = new byte[bytes];
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00090428 File Offset: 0x0008E628
		private void AdjustInternalBufferOffsetSize(int bytes, int offset)
		{
			this._InternalBufferCount = bytes;
			this._InternalOffset = offset;
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x00090438 File Offset: 0x0008E638
		private void ValidateParameters(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("net_offset_plus_count"));
			}
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00090490 File Offset: 0x0008E690
		private void ProcessWrite(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			this.ValidateParameters(buffer, offset, count);
			if (Interlocked.Exchange(ref this._NestedWrite, 1) == 1)
			{
				throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					(asyncRequest != null) ? "BeginWrite" : "Write",
					"write"
				}));
			}
			bool flag = false;
			try
			{
				this.StartWriting(buffer, offset, count, asyncRequest);
			}
			catch (Exception ex)
			{
				flag = true;
				if (ex is IOException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_write"), ex);
			}
			finally
			{
				if (asyncRequest == null || flag)
				{
					this._NestedWrite = 0;
				}
			}
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x00090540 File Offset: 0x0008E740
		private void StartWriting(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			if (count >= 0)
			{
				byte[] array = null;
				for (;;)
				{
					int num = Math.Min(count, 64512);
					int num2;
					try
					{
						num2 = this._NegoState.EncryptData(buffer, offset, num, ref array);
					}
					catch (Exception ex)
					{
						throw new IOException(SR.GetString("net_io_encrypt"), ex);
					}
					if (asyncRequest != null)
					{
						asyncRequest.SetNextRequest(buffer, offset + num, count - num, null);
						IAsyncResult asyncResult = base.InnerStream.BeginWrite(array, 0, num2, NegotiateStream._WriteCallback, asyncRequest);
						if (!asyncResult.CompletedSynchronously)
						{
							break;
						}
						base.InnerStream.EndWrite(asyncResult);
					}
					else
					{
						base.InnerStream.Write(array, 0, num2);
					}
					offset += num;
					count -= num;
					if (count == 0)
					{
						goto IL_9B;
					}
				}
				return;
			}
			IL_9B:
			if (asyncRequest != null)
			{
				asyncRequest.CompleteUser();
			}
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x00090604 File Offset: 0x0008E804
		private int ProcessRead(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			this.ValidateParameters(buffer, offset, count);
			if (Interlocked.Exchange(ref this._NestedRead, 1) == 1)
			{
				throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					(asyncRequest != null) ? "BeginRead" : "Read",
					"read"
				}));
			}
			bool flag = false;
			int num2;
			try
			{
				if (this.InternalBufferCount != 0)
				{
					int num = ((this.InternalBufferCount > count) ? count : this.InternalBufferCount);
					if (num != 0)
					{
						Buffer.BlockCopy(this.InternalBuffer, this.InternalOffset, buffer, offset, num);
						this.DecrementInternalBufferCount(num);
					}
					if (asyncRequest != null)
					{
						asyncRequest.CompleteUser(num);
					}
					num2 = num;
				}
				else
				{
					num2 = this.StartReading(buffer, offset, count, asyncRequest);
				}
			}
			catch (Exception ex)
			{
				flag = true;
				if (ex is IOException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_read"), ex);
			}
			finally
			{
				if (asyncRequest == null || flag)
				{
					this._NestedRead = 0;
				}
			}
			return num2;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00090704 File Offset: 0x0008E904
		private int StartReading(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			int num;
			while ((num = this.StartFrameHeader(buffer, offset, count, asyncRequest)) == -1)
			{
			}
			return num;
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00090724 File Offset: 0x0008E924
		private int StartFrameHeader(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			int num;
			if (asyncRequest != null)
			{
				asyncRequest.SetNextRequest(this._ReadHeader, 0, this._ReadHeader.Length, NegotiateStream._ReadCallback);
				this._FrameReader.AsyncReadPacket(asyncRequest);
				if (!asyncRequest.MustCompleteSynchronously)
				{
					return 0;
				}
				num = asyncRequest.Result;
			}
			else
			{
				num = this._FrameReader.ReadPacket(this._ReadHeader, 0, this._ReadHeader.Length);
			}
			return this.StartFrameBody(num, buffer, offset, count, asyncRequest);
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x0009079C File Offset: 0x0008E99C
		private int StartFrameBody(int readBytes, byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			if (readBytes == 0)
			{
				if (asyncRequest != null)
				{
					asyncRequest.CompleteUser(0);
				}
				return 0;
			}
			readBytes = (int)this._ReadHeader[3];
			readBytes = (readBytes << 8) | (int)this._ReadHeader[2];
			readBytes = (readBytes << 8) | (int)this._ReadHeader[1];
			readBytes = (readBytes << 8) | (int)this._ReadHeader[0];
			if (readBytes <= 4 || readBytes > 65536)
			{
				throw new IOException(SR.GetString("net_frame_read_size"));
			}
			this.EnsureInternalBufferSize(readBytes);
			if (asyncRequest != null)
			{
				asyncRequest.SetNextRequest(this.InternalBuffer, 0, readBytes, NegotiateStream._ReadCallback);
				this._FrameReader.AsyncReadPacket(asyncRequest);
				if (!asyncRequest.MustCompleteSynchronously)
				{
					return 0;
				}
				readBytes = asyncRequest.Result;
			}
			else
			{
				readBytes = this._FrameReader.ReadPacket(this.InternalBuffer, 0, readBytes);
			}
			return this.ProcessFrameBody(readBytes, buffer, offset, count, asyncRequest);
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00090874 File Offset: 0x0008EA74
		private int ProcessFrameBody(int readBytes, byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			if (readBytes == 0)
			{
				throw new IOException(SR.GetString("net_io_eof"));
			}
			int num;
			readBytes = this._NegoState.DecryptData(this.InternalBuffer, 0, readBytes, out num);
			this.AdjustInternalBufferOffsetSize(readBytes, num);
			if (readBytes == 0 && count != 0)
			{
				return -1;
			}
			if (readBytes > count)
			{
				readBytes = count;
			}
			Buffer.BlockCopy(this.InternalBuffer, this.InternalOffset, buffer, offset, readBytes);
			this.DecrementInternalBufferCount(readBytes);
			if (asyncRequest != null)
			{
				asyncRequest.CompleteUser(readBytes);
			}
			return readBytes;
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x000908F4 File Offset: 0x0008EAF4
		private static void WriteCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)transportResult.AsyncState;
			try
			{
				NegotiateStream negotiateStream = (NegotiateStream)asyncProtocolRequest.AsyncObject;
				negotiateStream.InnerStream.EndWrite(transportResult);
				if (asyncProtocolRequest.Count == 0)
				{
					asyncProtocolRequest.Count = -1;
				}
				negotiateStream.StartWriting(asyncProtocolRequest.Buffer, asyncProtocolRequest.Offset, asyncProtocolRequest.Count, asyncProtocolRequest);
			}
			catch (Exception ex)
			{
				if (asyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				asyncProtocolRequest.CompleteWithError(ex);
			}
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0009097C File Offset: 0x0008EB7C
		private static void ReadCallback(AsyncProtocolRequest asyncRequest)
		{
			try
			{
				NegotiateStream negotiateStream = (NegotiateStream)asyncRequest.AsyncObject;
				BufferAsyncResult bufferAsyncResult = (BufferAsyncResult)asyncRequest.UserAsyncResult;
				if (asyncRequest.Buffer == negotiateStream._ReadHeader)
				{
					negotiateStream.StartFrameBody(asyncRequest.Result, bufferAsyncResult.Buffer, bufferAsyncResult.Offset, bufferAsyncResult.Count, asyncRequest);
				}
				else if (-1 == negotiateStream.ProcessFrameBody(asyncRequest.Result, bufferAsyncResult.Buffer, bufferAsyncResult.Offset, bufferAsyncResult.Count, asyncRequest))
				{
					negotiateStream.StartReading(bufferAsyncResult.Buffer, bufferAsyncResult.Offset, bufferAsyncResult.Count, asyncRequest);
				}
			}
			catch (Exception ex)
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				asyncRequest.CompleteWithError(ex);
			}
		}

		// Token: 0x04001CE1 RID: 7393
		private NegoState _NegoState;

		// Token: 0x04001CE2 RID: 7394
		private string _Package;

		// Token: 0x04001CE3 RID: 7395
		private IIdentity _RemoteIdentity;

		// Token: 0x04001CE4 RID: 7396
		private static AsyncCallback _WriteCallback = new AsyncCallback(NegotiateStream.WriteCallback);

		// Token: 0x04001CE5 RID: 7397
		private static AsyncProtocolCallback _ReadCallback = new AsyncProtocolCallback(NegotiateStream.ReadCallback);

		// Token: 0x04001CE6 RID: 7398
		private int _NestedWrite;

		// Token: 0x04001CE7 RID: 7399
		private int _NestedRead;

		// Token: 0x04001CE8 RID: 7400
		private byte[] _ReadHeader;

		// Token: 0x04001CE9 RID: 7401
		private byte[] _InternalBuffer;

		// Token: 0x04001CEA RID: 7402
		private int _InternalOffset;

		// Token: 0x04001CEB RID: 7403
		private int _InternalBufferCount;

		// Token: 0x04001CEC RID: 7404
		private FixedSizeReader _FrameReader;
	}
}

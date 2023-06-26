using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x020000C9 RID: 201
	internal class ServerCertValidationCallback
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x0002540F File Offset: 0x0002360F
		internal ServerCertValidationCallback(RemoteCertificateValidationCallback validationCallback)
		{
			this.m_ValidationCallback = validationCallback;
			this.m_Context = ExecutionContext.Capture();
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00025429 File Offset: 0x00023629
		internal RemoteCertificateValidationCallback ValidationCallback
		{
			get
			{
				return this.m_ValidationCallback;
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00025434 File Offset: 0x00023634
		internal void Callback(object state)
		{
			ServerCertValidationCallback.CallbackContext callbackContext = (ServerCertValidationCallback.CallbackContext)state;
			callbackContext.result = this.m_ValidationCallback(callbackContext.request, callbackContext.certificate, callbackContext.chain, callbackContext.sslPolicyErrors);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00025474 File Offset: 0x00023674
		internal bool Invoke(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (this.m_Context == null)
			{
				return this.m_ValidationCallback(request, certificate, chain, sslPolicyErrors);
			}
			ExecutionContext executionContext = this.m_Context.CreateCopy();
			ServerCertValidationCallback.CallbackContext callbackContext = new ServerCertValidationCallback.CallbackContext(request, certificate, chain, sslPolicyErrors);
			ExecutionContext.Run(executionContext, new ContextCallback(this.Callback), callbackContext);
			return callbackContext.result;
		}

		// Token: 0x04000C8A RID: 3210
		private readonly RemoteCertificateValidationCallback m_ValidationCallback;

		// Token: 0x04000C8B RID: 3211
		private readonly ExecutionContext m_Context;

		// Token: 0x020006EE RID: 1774
		private class CallbackContext
		{
			// Token: 0x0600404E RID: 16462 RVA: 0x0010DA61 File Offset: 0x0010BC61
			internal CallbackContext(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				this.request = request;
				this.certificate = certificate;
				this.chain = chain;
				this.sslPolicyErrors = sslPolicyErrors;
			}

			// Token: 0x04003068 RID: 12392
			internal readonly object request;

			// Token: 0x04003069 RID: 12393
			internal readonly X509Certificate certificate;

			// Token: 0x0400306A RID: 12394
			internal readonly X509Chain chain;

			// Token: 0x0400306B RID: 12395
			internal readonly SslPolicyErrors sslPolicyErrors;

			// Token: 0x0400306C RID: 12396
			internal bool result;
		}
	}
}

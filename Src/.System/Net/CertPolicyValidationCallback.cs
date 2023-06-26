using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x020000C8 RID: 200
	internal class CertPolicyValidationCallback
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x00025325 File Offset: 0x00023525
		internal CertPolicyValidationCallback()
		{
			this.m_CertificatePolicy = new DefaultCertPolicy();
			this.m_Context = null;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0002533F File Offset: 0x0002353F
		internal CertPolicyValidationCallback(ICertificatePolicy certificatePolicy)
		{
			this.m_CertificatePolicy = certificatePolicy;
			this.m_Context = ExecutionContext.Capture();
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00025359 File Offset: 0x00023559
		internal ICertificatePolicy CertificatePolicy
		{
			get
			{
				return this.m_CertificatePolicy;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00025361 File Offset: 0x00023561
		internal bool UsesDefault
		{
			get
			{
				return this.m_Context == null;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0002536C File Offset: 0x0002356C
		internal void Callback(object state)
		{
			CertPolicyValidationCallback.CallbackContext callbackContext = (CertPolicyValidationCallback.CallbackContext)state;
			callbackContext.result = callbackContext.policyWrapper.CheckErrors(callbackContext.hostName, callbackContext.certificate, callbackContext.chain, callbackContext.sslPolicyErrors);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000253AC File Offset: 0x000235AC
		internal bool Invoke(string hostName, ServicePoint servicePoint, X509Certificate certificate, WebRequest request, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			PolicyWrapper policyWrapper = new PolicyWrapper(this.m_CertificatePolicy, servicePoint, request);
			if (this.m_Context == null)
			{
				return policyWrapper.CheckErrors(hostName, certificate, chain, sslPolicyErrors);
			}
			ExecutionContext executionContext = this.m_Context.CreateCopy();
			CertPolicyValidationCallback.CallbackContext callbackContext = new CertPolicyValidationCallback.CallbackContext(policyWrapper, hostName, certificate, chain, sslPolicyErrors);
			ExecutionContext.Run(executionContext, new ContextCallback(this.Callback), callbackContext);
			return callbackContext.result;
		}

		// Token: 0x04000C88 RID: 3208
		private readonly ICertificatePolicy m_CertificatePolicy;

		// Token: 0x04000C89 RID: 3209
		private readonly ExecutionContext m_Context;

		// Token: 0x020006ED RID: 1773
		private class CallbackContext
		{
			// Token: 0x0600404D RID: 16461 RVA: 0x0010DA34 File Offset: 0x0010BC34
			internal CallbackContext(PolicyWrapper policyWrapper, string hostName, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				this.policyWrapper = policyWrapper;
				this.hostName = hostName;
				this.certificate = certificate;
				this.chain = chain;
				this.sslPolicyErrors = sslPolicyErrors;
			}

			// Token: 0x04003062 RID: 12386
			internal readonly PolicyWrapper policyWrapper;

			// Token: 0x04003063 RID: 12387
			internal readonly string hostName;

			// Token: 0x04003064 RID: 12388
			internal readonly X509Certificate certificate;

			// Token: 0x04003065 RID: 12389
			internal readonly X509Chain chain;

			// Token: 0x04003066 RID: 12390
			internal readonly SslPolicyErrors sslPolicyErrors;

			// Token: 0x04003067 RID: 12391
			internal bool result;
		}
	}
}

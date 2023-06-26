using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	/// <summary>Validates a server certificate.</summary>
	// Token: 0x02000110 RID: 272
	public interface ICertificatePolicy
	{
		/// <summary>Validates a server certificate.</summary>
		/// <param name="srvPoint">The <see cref="T:System.Net.ServicePoint" /> that will use the certificate.</param>
		/// <param name="certificate">The certificate to validate.</param>
		/// <param name="request">The request that received the certificate.</param>
		/// <param name="certificateProblem">The problem that was encountered when using the certificate.</param>
		/// <returns>
		///   <see langword="true" /> if the certificate should be honored; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000AFB RID: 2811
		bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem);
	}
}

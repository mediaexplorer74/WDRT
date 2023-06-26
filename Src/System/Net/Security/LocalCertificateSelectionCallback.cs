using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	/// <summary>Selects the local Secure Sockets Layer (SSL) certificate used for authentication.</summary>
	/// <param name="sender">An object that contains state information for this validation.</param>
	/// <param name="targetHost">The host server specified by the client.</param>
	/// <param name="localCertificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> containing local certificates.</param>
	/// <param name="remoteCertificate">The certificate used to authenticate the remote party.</param>
	/// <param name="acceptableIssuers">A <see cref="T:System.String" /> array of certificate issuers acceptable to the remote party.</param>
	/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> used for establishing an SSL connection.</returns>
	// Token: 0x0200035B RID: 859
	// (Invoke) Token: 0x06001EDB RID: 7899
	public delegate X509Certificate LocalCertificateSelectionCallback(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers);
}
